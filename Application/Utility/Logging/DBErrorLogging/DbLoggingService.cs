using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interface;
using Newtonsoft.Json;

namespace Application.Utility.Logging.DBErrorLogging
{
    public interface IDbLoggingService : IBaseService<ErrorLog>
    {
        Task LogErrorAsync(Exception exception, string url = null, string payload = null);
    }
    public class DbLoggingService : BaseService<ErrorLog>, IDbLoggingService
    {
        public DbLoggingService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task LogErrorAsync(Exception exception, string url = null, string payload = null)
        {   
            var errorLog = new ErrorLog
            {
                timestamp = DateTime.UtcNow,
                message = exception.Message,
                stackTrace = exception.StackTrace,
                url = url,
                payload = payload,
                type = exception.GetType().Name
            };



            await AddAsync(errorLog);

        }
    }
}
