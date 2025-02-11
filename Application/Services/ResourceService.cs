
using System.Resources;
using Application.Dto.Authentication;
using Application.Dto.Resource;
using Application.Interfaces;
using Application.Utility.ServerSideGrid;
using Domain.Entities;
using Domain.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ResourceService : BaseService<Resource>, IResourceService
    {
        private IUnitOfWork _unitOfWork;
        public ResourceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GridResponse<List<Resource>>> GetGrid(GridRequest<ResourceGridKeys> request)
        {

            IQueryable<Resource> resources = GetByConditionAsync(r => r.VendorDetailsId == request.uData.VendorDetailsId);

            GridRequest oRequest = request.Adapt<GridRequest>();
            var lstResources = await GridHelperService.GetPagedList(resources, oRequest);
            var response = new GridResponse<List<Resource>>(lstResources, resources.Count());

            return response;
        }

        public override async Task<Resource> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id, query =>
            query

            .Include(r => r.VendorDetails)

            .Include(r => r.Addresses)

            .Include(r => r.Contacts)

            .Include(x => x.Gender)

            .Include(x => x.MainSkills)
            .ThenInclude(ms => ms.Skill)  // Include children of MainSkills

            .Include(x => x.SubSkills).ThenInclude(ms => ms.Skill)
            );
        }

        public override async Task<Resource> UpdateAsync(Resource request)
        {
            var existingResource = await GetByIdAsync((int)request.Id);
            if (existingResource == null)
            {
                throw new Exception("Resource not found");
            }

            // Update scalar properties
            existingResource.Name = request.Name;
            existingResource.VendorDetailsId = request.VendorDetailsId;
            existingResource.GenderId = request.GenderId;
            existingResource.Age = request.Age;
            existingResource.PhoneNumber = request.PhoneNumber;
            existingResource.Email = request.Email;
            existingResource.JobTitle = request.JobTitle;
            // ... update other properties as needed

            // Replace Contacts
            if (existingResource.Contacts != null)
                existingResource.Contacts.Clear();
            else
                existingResource.Contacts = new List<Contact>();

            // Re-add the contacts from the request by creating new instances
            if (request.Contacts != null)
            {
                foreach (var contactDto in request.Contacts)
                {
                    // Create a new instance for each contact (this avoids tracking conflicts)
                    var newContact = new Contact
                    {
                        // For an update scenario, if contactDto.Id > 0 you may want to retain it,
                        // or simply set it to 0 (or leave it unset) for a full replacement.
                        Id = null,
                        PhoneNo = contactDto.PhoneNo,
                        CreatedBy = contactDto.CreatedBy,
                        UpdatedBy = contactDto.UpdatedBy,
                        CreatedDate = contactDto.CreatedDate,
                        UpdatedDate = contactDto.UpdatedDate,
                        ResourceId = existingResource.Id
                    };
                    existingResource.Contacts.Add(newContact);
                }
            }


            // Replace Addresses
            existingResource.Addresses.Clear();
            foreach (var addrDto in request.Addresses)
            {
                var newAddress = addrDto.Adapt<Address>();
                newAddress.Id = null;

                existingResource.Addresses.Add(newAddress);
            }

            // Replace MainSkills (delete all existing, then add new mappings)
            existingResource.MainSkills.Clear();
            foreach (var ms in request.MainSkills)
            {
                existingResource.MainSkills.Add(new MainSkillMapping
                {
                    ResourceId = (int)existingResource.Id,
                    SkillId = ms.SkillId,
                    // Optionally, set additional properties if needed
                });
            }

            //// Replace SubSkills
            existingResource.SubSkills.Clear();
            foreach (var ss in request.SubSkills)
            {
                existingResource.SubSkills.Add(new SubSkillMapping
                {
                    ResourceId = (int)existingResource.Id,
                    SkillId = ss.SkillId,
                    // Optionally, set additional properties if needed
                });
            }

            var response = await _unitOfWork.Repository<Resource>().UpdateAsync(existingResource);
            await _unitOfWork.SaveAsync();
            return response;

        }



        public async Task<GridResponse<List<Resource>>> SearchResource(GridRequest<SearchResource> request)
        {
            var queryString = request.uData.query;


            IQueryable<Resource> query = GetAllAsync().AsNoTracking();


            query = query
                .Include(g => g.Gender).AsNoTracking()
                .Include(g => g.VendorDetails).AsNoTracking()
                 .Include(r => r.MainSkills).ThenInclude(ms => ms.Skill).AsNoTracking()
                 .Include(r => r.SubSkills).ThenInclude(ss => ss.Skill).AsNoTracking()
                 .Include(r => r.Addresses).AsNoTracking()
                 .Include(r => r.Contacts).AsNoTracking();

            // If a search string is provided, filter by that string.
            if (!string.IsNullOrEmpty(queryString))
            {
                query = query.Where(r =>
                    r.Name.Contains(queryString) ||
                    (r.MainSkills != null && r.MainSkills.Any(ms => ms.Skill != null && ms.Skill.Value.Contains(queryString))) ||
                    (r.SubSkills != null && r.SubSkills.Any(ss => ss.Skill != null && ss.Skill.Value.Contains(queryString))) ||
                    (r.Addresses != null && r.Addresses.Any(a =>
                        a.AddressLine1.Contains(queryString) ||
                        a.AddressLine2.Contains(queryString))) ||
                    (r.Contacts != null && r.Contacts.Any(c => c.PhoneNo.Contains(queryString)))
                ).AsNoTracking();
            }
            GridRequest oRequest = request.Adapt<GridRequest>();
            var lstResources = await GridHelperService.GetPagedList(query, oRequest);
            var response = new GridResponse<List<Resource>>(lstResources, query.Count());
            return response;
        }

    }
}
