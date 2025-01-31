using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utility.ExceptionHandling.ExceptionTypes
{
    public class BusinessException:Exception
    {
        public int ErrorCode { get; set; }
        public string ErrorType { get; set; }
        public DateTime Timestamp { get; set; }
        public BusinessException() 
        {
        }
        public BusinessException(string message):base(message) {
            Timestamp = DateTime.UtcNow; // Store the time the error occurred
            ErrorCode = (int)ErrorCodes.BusinessException; // Example: a generic error code
            ErrorType = "BusinessException"; // You can use this to classify the error
        }

       
    }
}
