using System;

namespace EPM.WMS.Dto.Integration
{
    public class ContactModel
    {
        public int PrevailingContactId { get; set; }
        public int ParentPrevailingContactId { get; set; }
        public int PrevailingId { get; set; }
        public string Code { get; set; }
        public int ContactTypeUDCId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public DateTime? Birthday { get; set; }
        public int IsDefault { get; set; }
        public int IsPublished { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int IsActive { get; set; }
        public int LastModifiedUserId { get; set; }
        public int TransactionUserId { get; set; }
        public int DisplayOrder { get; set; }
        public int OwnerCompanyId { get; set; }
        public string Note { get; set; }
    }
}
