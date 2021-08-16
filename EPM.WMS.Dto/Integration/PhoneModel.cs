using System;
using System.ComponentModel.DataAnnotations;

namespace EPM.WMS.Dto.Integration
{
    public class PhoneModel
    {
        public int PrevailingContactPhoneId { get; set; }
        public int PrevailingContactId { get; set; }
        public string Code { get; set; }
        
        [Required]
        [MaxLength(5)]
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public int ContactPhoneTypeUDCId { get; set; }
        public int IsDefault { get; set; }
        public int IsSmsAllowed { get; set; }
        public int IsPublished { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int IsActive { get; set; }
        public int LastModifiedUserId { get; set; }
        public int TransactionUserId { get; set; }
        public int DisplayOrder { get; set; }
        public int OwnerCompanyId { get; set; }
        public object Note { get; set; }
    }
}
