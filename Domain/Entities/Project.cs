using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Project:Base
    {
        public Project() { }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(155)]
        public string ProjectName { get; set; }
        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string? ProjectDescription { get; set; }
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
      
    }
}
