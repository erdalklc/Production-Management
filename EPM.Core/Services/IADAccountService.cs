using EPM.Core.Managers;
using EPM.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Services
{
    public interface IADAccountService
    { 
        public List<ADAccount> GetAccounts();

        public void AddAccounts(List<ADAccount> accounts);

         

        public List<ADAccount> GetAccountsFromAD();
    }
}
