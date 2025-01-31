using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Invitation : Base
    {
        public Invitation() { }
        public Guid Code { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int  RoleId { get; set; }
       
        public Role Role { get; set; }
        public string Email {   get; set; }
        public DateTime ExpiresAt { get; set; } = DateTime.Now.AddDays(4);
        public bool IsUsed { get; set; } = false;
    }
}
