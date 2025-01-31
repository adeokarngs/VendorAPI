using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contact:Base
    {
        public int ConsultantId { get; set; } 
        public string PhoneCode { get; set; }
        public string PhoneNo { get; set; }
    }
}
