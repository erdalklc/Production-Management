namespace EPM.WMS.Dto.Dto
{
    /// <summary>
    ///    -- ADDRESS1 => GENELDE MAĞAZA ADI OLUYOR AMA BAZILARI DİREK ADRES BİLGİSİ GEÇİLMİŞ
    ///    -- ADDRESS2 => ADRES AÇIKLMASAININ 1. KISMI 
    ///    -- ADDRESS3 => ADRES AÇIKLAMASANIN 2. KISMI
    ///    -- ADDRESS4 => NULL GEÇENLER STATE LERİ NULL OLMAYANLARIN STATE DEĞERLERİ ADDRESS4 KOLONUNDA YAZMAKTADIR
    /// </summary>
    public class LogisticSellerAddressDto
    {
        public int ID { get; set; }
        public int ORG_ID { get; set; }
        public string ATTRIBUTE2 { get; set; }
        public string ADRES { get; set; }
        public string COUNTRY { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string POSTAL_CODE { get; set; }
        public string PROVINCE { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string ADDRESS4 { get; set; }
        public string PRIMARY_FLAG { get; set; }
    }
}
