using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ErrorLog
    {
        public int id { get; set; }
        public int ida { get; set; }                 
        public DateTime timestamp { get; set; }     
        public string? message { get; set; }         
        public string? stackTrace { get; set; }     
        public string? type { get; set; }
        public string? url { get; set; }
        public string? payload { get; set; }          
    }

}
