using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Master : Base
    {
        public Master() { }

        [Required]
        [MaxLength(55)]
        [MinLength(2)]
        public string Type { get; set; }

        [Required]
        [MaxLength(6)]
        [MinLength(2)]
        public string Code { get; set; }


        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string Value { get; set; }

        public int ParentId { get; set; }
        public bool IsActive { get; set; } = true;

       
    }
}
