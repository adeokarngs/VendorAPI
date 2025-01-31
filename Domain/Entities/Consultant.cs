using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Consultant:Base
    {
        public int UserId { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public string SocialMediaAccounts { get; set; }
        public ICollection<Contact> AlternateContact { get; set; }
        public ICollection<SkillMapping> SubSkill { get; set; }
        public bool IsOpenForVerification { get; set; }
    }
}
