using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interface;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class FileRepository : BaseRepository<Files>, IFileRepository
    {
        public FileRepository(AppDbContext context) : base(context) { }
    }
}
