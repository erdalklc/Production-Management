using System;

namespace EPM.WMS.Dto.Integration
{
    public class PrevailingModel
    {
        public int PrevailingId { get; set; }
        public int ParentPrevailingId { get; set; }
        public int PrevailingTypeUDCId { get; set; }
        public int IsIndividual { get; set; }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        public int ChannelDeviceUDCId { get; set; }
        public int ChannelOrganizationId { get; set; }
        public int IsPublished { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime DeactivationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int IsActive { get; set; }
        public int LastModifiedUserId { get; set; }
        public int TransactionUserId { get; set; }
        public int IsFacetedNavigationSelected { get; set; }
        public int IsFacetedSearchIncluded { get; set; }
        public int DisplayOrder { get; set; }
        public int OwnerCompanyId { get; set; }
        public string Note { get; set; }
        public string SecondaryCode { get; set; }
    }
}
