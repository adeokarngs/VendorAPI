using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Authentication
{
    public class RegistrationDto
    {
        public string email { get; set; }
        public string password { get; set; }
        public string invitationId { get; set; }
        public int roleId { get; set; }
    }
}
