using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMasterService:IBaseService<Master>
    {
        Task<List<Master>> GetByType(string[] type);
        Task<List<Master>> GetByParentId(int parentId);
    }
}
