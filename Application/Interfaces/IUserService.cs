using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.UserDto;
using Application.Utility.ServerSideGrid;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<GridResponse<List<User>>> GetGrid(GridRequest request);
    }
}
