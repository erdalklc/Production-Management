using EPM.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Service.Base
{
    public interface IADAccountService
    { 
        public List<ADAccount> GetAccounts();

        public void AddAccounts(List<ADAccount> accounts); 

        public List<ADAccount> GetAccountsFromAD();
    }
}
