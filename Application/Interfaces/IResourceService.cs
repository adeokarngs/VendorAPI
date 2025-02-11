using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Authentication;
using Application.Dto.Resource;
using Application.Utility.ServerSideGrid;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IResourceService:IBaseService<Resource>
    {
        Task<GridResponse<List<Resource>>> GetGrid(GridRequest<ResourceGridKeys> request);
        Task<GridResponse<List<Resource>>> SearchResource(GridRequest<SearchResource> request);
    }
}
