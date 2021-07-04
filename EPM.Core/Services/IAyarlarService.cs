using EPM.Core.FormModels.Ayarlar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Services
{
    public interface IAyarlarService
    {

        public IEnumerable<KullaniciListe> GetUserList();

        public IEnumerable<KullaniciYetki> GetUserResponsibiity(string USER_CODE);
          
        public object[] UserResponsibilityUpdate(int key, string values, string USER_CODE);

        public object[] UserFabricTypeUpdate(int key, string values, string USER_CODE);

        public IEnumerable<KullaniciFabricTypes> GetUserFabricTypes(string USER_CODE);

        public IEnumerable<KullaniciProductionTypes> GetUserProductionTypes(string USER_CODE);

        public object[] UserProductionTypeUpdate(int key, string values, string USER_CODE);

        public object[] UserBrandsUpdate(int key, string values, string USER_CODE);

        public IEnumerable<KullaniciMarka> GetUserBrands(string USER_CODE);
    }
}
