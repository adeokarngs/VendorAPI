using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Authentication
{
    public class Token
    {

        public Token() { }
        public string value { get; set; }
        public DateTime? expires { get; set; }
    }
}
