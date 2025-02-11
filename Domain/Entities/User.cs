using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : Base
    {
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]    
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public int RoleId { get; set; }

        // Navigation property for Role
        public Role? Role { get; set; }

        // Navigation property for Vendor
        public VendorDetails? Vendor { get; set; }

        // Navigation property for Consultant
        public Consultant? Consultant { get; set; }


    }
}
