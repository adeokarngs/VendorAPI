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
    public class FilesService:BaseService<Files>,IFileService
    {
        public FilesService(IUnitOfWork repository) : base(repository)
        {
        }
    }
}
