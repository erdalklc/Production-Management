using Newtonsoft.Json;
using System;

namespace EPM.WMS.Dto.Integration
{
    public class AddressModel
    {
        [JsonProperty("PrevailingAddressId")]
        public int? PrevailingAddressId { get; set; }
       
        [JsonProperty("PrevailingId")]
        public int? PrevailingId { get; set; }
        
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
        
        [JsonProperty("AddressTypeUDCId")]
        public int? AddressTypeUDCId { get; set; }
        
        [JsonProperty("Line")]
        public string Line { get; set; }
        
        [JsonProperty("TaxNo")]
        public string TaxNo { get; set; }
        
        [JsonProperty("TaxOffice")]
        public string TaxOffice { get; set; }
        
        [JsonProperty("PostCode")]
        public string PostCode { get; set; }
        
        [JsonProperty("CountryId")]
        public int? CountryId { get; set; }
        
        [JsonProperty("ProvinceId")]
        public int? ProvinceId { get; set; }
        
        [JsonProperty("DistrictId")]
        public int? DistrictId { get; set; }
        
        [JsonProperty("TownId")]
        public int? TownId { get; set; }
        
        [JsonProperty("Note")]
        public string Note { get; set; }
        
        [JsonProperty("IsDefault")]
        public int IsDefault { get; set; }
        
        [JsonProperty("DisplayOrder")]
        public int DisplayOrder { get; set; }
        
        [JsonProperty("IsPublished")]
        public int IsPublished { get; set; }

        [JsonProperty("LastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }

        [JsonProperty("IsActive")]
        public int IsActive { get; set; }

        [JsonProperty("LastModifiedUserId")]
        public int LastModifiedUserId { get; set; }

        [JsonProperty("TransactionUserId")]
        public int TransactionUserId { get; set; }

        [JsonProperty("OwnerCompanyId")]
        public int OwnerCompanyId { get; set; }
    }
}
