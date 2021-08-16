using System;

namespace EPM.WMS.Dto.Integration
{
    public class EmailModel
    {
        public int PrevailingContactEmailId { get; set; }
        public int PrevailingContactId { get; set; }
        public string Code { get; set; }
        public int ContactEmailTypeUDCId { get; set; }
        public string Email { get; set; }
        public int IsDefault { get; set; }
        public int IsEmailAllowed { get; set; }
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
