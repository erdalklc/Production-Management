using System;

namespace EPM.WMS.Dto.Login
{
    public class LoginReturnModel
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public UserModel User { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
