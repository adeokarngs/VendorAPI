﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Authentication
{
    public class InvitationDto
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
