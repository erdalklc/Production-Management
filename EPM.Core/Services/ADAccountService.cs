using EPM.Core.Loglar;
using EPM.Core.Managers; 
using EPM.Dto.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;

namespace EPM.Core.Services
{
    public class ADAccountService :IADAccountService
    {
        public IMongoCollection<ADAccount> _accounts; 

        public ADAccountService()
        {
            //MongoServer.RemoveAll<ADAccount>(ADCollectionName);
            _accounts= MongoServer.GetList<ADAccount>(ADCollectionName); 
        }

        public string ADCollectionName = "ADAccoutList";
        public string LogLoginName = "LOG_Login";
        public List<ADAccount> GetAccounts()
        {
            List<ADAccount> collet = _accounts.Find(_=>true).ToList();
            if (collet != null && collet.Count==0)
            {
                AddAccounts(GetAccountsFromAD());
                _accounts = MongoServer.GetList<ADAccount>(ADCollectionName);
                collet = _accounts.Find(_ => true).ToList();
            } 
            return collet;
        }

        public void AddAccounts(List<ADAccount> accounts)
        {
            MongoServer.AddList(ADCollectionName, accounts);
        } 
         

        public List<ADAccount> GetAccountsFromAD()
        {
            List<ADAccount> accounts = new List<ADAccount>();
            using (var principalContext = new PrincipalContext(ContextType.Domain, "eren.holding"))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(principalContext)))
                {
                    List<UserPrincipal> users = searcher.FindAll().Select(u => (UserPrincipal)u).Where(ob => ob.Enabled == true && ob.EmailAddress!=null && (ob.EmailAddress.Contains("erenholding") || ob.EmailAddress.Contains("erenperakende") || ob.EmailAddress.Contains("erenretail")) && ob.Enabled.Value.BooleanParse()).ToList();

                    users.ForEach(ob =>
                    {
                        ADAccount acc = new ADAccount();
                        acc.USER_CODE = ob.Name;
                        acc.USER_EMAIL = ob.EmailAddress;
                        acc.USER_NAME = ob.DisplayName;
                        accounts.Add(acc);
                    }); 
                }
            }

            return accounts;
        }
    }
}
