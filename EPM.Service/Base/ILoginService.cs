using EPM.Dto.FormModels.Login;
using EPM.Dto.Models;
using Microsoft.AspNetCore.Http;
namespace EPM.Service.Base
{
    public interface ILoginService
    {
        public void LoginLog(WebLogin login,ILogService logRepository);

        public WebLogin LoginAD(HttpContext context, Login model, IADAccountService _aDAccountRepository);
    }
}
