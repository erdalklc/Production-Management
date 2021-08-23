namespace EPM.WMS.Dto.Login
{
    public class UserModel
    {
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public int CompanyIsActive { get; set; }
        public int UserCompanyId { get; set; }
        public int UserCompanyIsActive { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public string EmailAddress { get; set; }
        public string Pwd { get; set; }
        public int IsPublished { get; set; }
        public int IsActive { get; set; }
        public string LastModifiedDate { get; set; }
        public int LastModifiedUserId { get; set; }
        public int TransactionUserId { get; set; }
        public int LoginExpireDay { get; set; }
    }
}
