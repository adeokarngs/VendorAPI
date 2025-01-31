using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interface;

namespace Application.Services
{
    public class MasterService : BaseService<Master>, IMasterService
    {
        public MasterService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task <List<Master>> GetByType(string[] type)
        {
            List<Master> finalMasterList = new List<Master>();
            foreach (var item in type)
            {
                var foundItem = await GetByConditionAsync(x => x.Type == item);
                if (foundItem.Count() > 0)
                {
                    finalMasterList.AddRange(foundItem);
                    foundItem = [];
                }
            }
            return finalMasterList;
        }
        public async Task<List<Master>> GetByParentId(int parentId)
        {
            return (await GetByConditionAsync(x=>x.ParentId == parentId)).ToList();    
        }
    }
}
