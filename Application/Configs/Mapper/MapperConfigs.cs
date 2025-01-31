using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.UserDto;
using Domain.Entities;
using Mapster;

namespace Application.Configs.Mapper
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            // Custom mapping configuration for User to UserGridResponseDto
            TypeAdapterConfig<User, UserGridResponseDto>
                .NewConfig()
                .Map(dest => dest.name, src => src.Role.Name); // Mapping Role.Name to RoleName in DTO
        }
    }
}
