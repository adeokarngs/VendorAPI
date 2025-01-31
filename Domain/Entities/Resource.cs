using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Resource : Base
    {
        public Resource() { }

        // Resource Name
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        // Gender (Reference to Master table)
        public Master Gender { get; set; }  // Assuming Gender is a Master table (like an enum)

        // Age of the employee (Resource)
        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65.")]
        public int Age { get; set; }

        // Contact Details
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(255)]
        public string Email { get; set; }

        // Job Details
        [Required]
        [StringLength(200, ErrorMessage = "Job title must be between 2 and 100 characters.")]
        public string JobTitle { get; set; }


        // public int MainSkillRid { get; set; }
        public Master? MainSkill { get; set; }

        public ICollection<SkillMapping> SubSkill { get; set; }
      
    }

   
}
