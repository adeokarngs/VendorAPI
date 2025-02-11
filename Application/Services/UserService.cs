using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.UserDto;
using Application.Interfaces;
using Application.Utility.ServerSideGrid;
using Domain.Entities;
using Domain.Interface;
using Mapster;

namespace Application.Services
{
    public class UserService : BaseService<User>,IUserService
    {
        
        public UserService(IUnitOfWork repository) : base(repository)
        {
            
        }


        public async  Task<GridResponse<List<User>>> GetGrid(GridRequest request)
        {

            IQueryable<User> users  = GetAllAsync([u=>u.Role,
                    u => u.Vendor,
                    u=>u.Consultant]).AsQueryable();

            var lstUsers = await GridHelperService.GetPagedList(users, request);
            // Set "MiddleName" to null for all users
            lstUsers?.ForEach(user => user.Password = null);
            var response = new GridResponse<List<User>>(lstUsers, users.Count());

            return response;
        }
     
    }
}
