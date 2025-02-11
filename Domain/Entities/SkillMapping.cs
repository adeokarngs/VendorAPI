using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MainSkillMapping : Base
    {
        public int SkillId { get; set; }
        public int? ResourceId { get; set; }
        public int? ConsultantId { get; set; }
        public virtual Master? Skill { get; set; }
    }
    public class SubSkillMapping : Base
    {
        public int SkillId { get; set; }
        public int? ResourceId { get; set; }
        public int? ConsultantId { get; set; }
        public virtual Master? Skill { get; set; }
    }
}
