using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_MASTER_PRODUCTION_H")]
    public class EPM_MASTER_PRODUCTION_H
    {
        [Dapper.Contrib.Extensions.Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Marka ID Bilgisi Gereklidir")]
        public int BRAND { get; set; }
        [Required(ErrorMessage = "Alt Marka ID Bilgisi Gereklidir")]
        public int SUB_BRAND { get; set; }
        [Required(ErrorMessage = "Season Bilgisi Gereklidir")]
        public int SEASON { get; set; }
        [Required(ErrorMessage = "Model Bilgisi Gereklidir")]
        public string MODEL { get; set; }
        [Required(ErrorMessage = "Color Bilgisi Gereklidir")]
        public string COLOR { get; set; }
        [Required(ErrorMessage = "Product Group Bilgisi Gereklidir")]
        public int PRODUCT_GROUP { get; set; }
        [Required(ErrorMessage = "Fabric Type Bilgisi Gereklidir")]
        public int FABRIC_TYPE { get; set; }
        [Required(ErrorMessage = "Production Type Bilgisi Gereklidir")]
        public int PRODUCTION_TYPE { get; set; }
        [Required(ErrorMessage = "Order Type Bilgisi Gereklidir")]
        public int ORDER_TYPE { get; set; } 
        public int BAND_ID { get; set; }
        [Required(ErrorMessage = "Recipe Bilgisi Gereklidir")]
        public int RECIPE { get; set; }
        [Required(ErrorMessage = "Deadline Bilgisi Gereklidir")]
        public DateTime? DEADLINE { get; set; } 
        public DateTime? DEADLINE_2 { get; set; } 
        public DateTime? DEADLINE_3 { get; set; } 
        public DateTime? DEADLINE_4 { get; set; }
        public DateTime SHIPMENT_DATE { get; set; }
        public string ATTRIBUTE1 { get; set; } = "";
        public string ATTRIBUTE2 { get; set; } = "";
        public string ATTRIBUTE3 { get; set; } = "";
        public string ATTRIBUTE4 { get; set; } = "";
        public string ATTRIBUTE5 { get; set; } = "";
        public string ATTRIBUTE6 { get; set; }
        public string ATTRIBUTE7 { get; set; } = "";
        public string ATTRIBUTE8 { get; set; } = "";
        public string ATTRIBUTE9 { get; set; } = "";
        public string ATTRIBUTE10 { get; set; } = "";
        public string TEMA { get; set; } = "";
        public string ANA_TEMA { get; set; } = "";
        public string ROYALTY { get; set; } = "";
        public int COLLECTION_TYPE { get; set; }
        public int APPROVAL_STATUS { get; set; } = 1;
        public int FLAG_ID { get; set; } = 2;
        public string CREATED_BY { get; set; } = "";
        public DateTime CREATE_DATE { get; set; } = DateTime.Now;
        public int STATUS { get; set; } = 0; 
        public List<EPM_MASTER_PRODUCTION_D> DETAIL=new List<EPM_MASTER_PRODUCTION_D>();
        public List<EPM_PRODUCTION_PLAN> PLAN=new List<EPM_PRODUCTION_PLAN>();
    }
}
