using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Authentication
{
    public class InvitationDto
    {
        public string name { get; set; }

        public string email { get; set; }
        public int roleId { get; set; }
    }
}
