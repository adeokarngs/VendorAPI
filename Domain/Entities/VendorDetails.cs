using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VendorDetails : Base
    {
        public int UserId { get; set; }
        //public ICollection<Address> Addresses { get; set; }


        [Required(ErrorMessage = "Email ID is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Name of the Vendor is required.")]
        [MaxLength(150, ErrorMessage = "Vendor Name cannot exceed 150 characters.")]
        public string VendorName { get; set; }

        [Required(ErrorMessage = "Company Name is required.")]
        [MaxLength(200, ErrorMessage = "Company Name cannot exceed 200 characters.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Company Address with PIN CODE is required.")]
        [MaxLength(500, ErrorMessage = "Company Address cannot exceed 500 characters.")]
        public string CompanyAddress { get; set; }

        [Required(ErrorMessage = "CIN / LLPIN is required if you are a corporate entity.")]
        [MaxLength(20, ErrorMessage = "CIN / LLPIN cannot exceed 20 characters.")]
        public string CinLlpIn { get; set; }

        [Required(ErrorMessage = "Constitution of Business is required.")]
        public int ConstitutionOfBusinessRid { get; set; }

        public Master? ConstitutionOfBusiness { get; set; }

        [Required(ErrorMessage = "Company Established is required.")]
        public DateTime CompanyEstablished { get; set; }

        [Required(ErrorMessage = "Company incorporation number & certificate is required.")]
        [MaxLength(50, ErrorMessage = "Company Incorporation number & certificate cannot exceed 50 characters.")]
        public string CompanyIncorporationNumberAndCertificate { get; set; }

        [Required(ErrorMessage = "Please upload PAN card (company).")]
        [MaxLength(100, ErrorMessage = "PAN card file name cannot exceed 100 characters.")]
        public string PanCardFile { get; set; }

        [Required(ErrorMessage = "MSME number is required.")]
        public Guid MsmeNumber { get; set; }

        [Required(ErrorMessage = "GSTIN / Other Tax Identification Number is required.")]
        public Guid GstinOrTaxId { get; set; }

        [Required(ErrorMessage = "SEZ Entity/Other Special Entity details are required.")]
        [MaxLength(500, ErrorMessage = "SEZ Entity details cannot exceed 500 characters.")]
        public string SezEntityDetails { get; set; }

        [Required(ErrorMessage = "GST Billing Address is required.")]
        public Guid GstBillingAddress { get; set; }

        [Required(ErrorMessage = "GST Filing Frequency is required.")]
        public int GstFilingFrequencyRid { get; set; }

        public Master? GstFilingFrequency { get; set; }

        [Required(ErrorMessage = "Company Linkedin Profile / other social media accounts is required.")]
        [MaxLength(200, ErrorMessage = "LinkedIn profile cannot exceed 200 characters.")]
        public string LinkedinProfile { get; set; }

        [Required(ErrorMessage = "Company Website is required.")]
        [MaxLength(200, ErrorMessage = "Company website cannot exceed 200 characters.")]
        public string CompanyWebsite { get; set; }

        [Required(ErrorMessage = "Company team size is required.")]
        public int TeamSize { get; set; }

        [Required(ErrorMessage = "How you can assist us is required.")]
        public int AssistanceDetailsRid { get; set; }
        public Master? AssistanceDetails { get; set; }

        [MaxLength(500, ErrorMessage = "Relocation States cannot exceed 500 characters.")]
        public string? RelocationStates { get; set; }

        [Required(ErrorMessage = "Vendor Contact number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string VendorContactNumber { get; set; }

        public string? AlternativeContactPointName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? AlternativeContactNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? AlternativeContactEmail { get; set; }

        public decimal? QuarterlyRevenue { get; set; } = 0;

        [Required(ErrorMessage = "Background verification consent is required.")]
        public int OpenToBackgroundVerificationRid { get; set; }
        public Master? OpenToBackgroundVerification { get; set; }

        [Required(ErrorMessage = "Previous involvement with NGenious Solutions is required.")]
        public int PreviousInvolvementWithUsRid { get; set; }
        public Master? PreviousInvolvementWithUs { get; set; }
    }
}
