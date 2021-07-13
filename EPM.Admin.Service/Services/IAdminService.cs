using EPM.Admin.Dto.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Admin.Service.Services
{
    public interface IAdminService
    {
        public List<KullaniciListe> GetUserList();

        public List<KullaniciYetki> GetUserResponsibiity(string USER_CODE);

        public object[] UserResponsibilityUpdate(int key, string values, string USER_CODE);

        public object[] UserFabricTypeUpdate(int key, string values, string USER_CODE);

        public List<KullaniciFabricTypes> GetUserFabricTypes(string USER_CODE);

        public List<KullaniciProductionTypes> GetUserProductionTypes(string USER_CODE);

        public object[] UserProductionTypeUpdate(int key, string values, string USER_CODE);

        public object[] UserBrandsUpdate(int key, string values, string USER_CODE);

        public List<KullaniciMarka> GetUserBrands(string USER_CODE);
    }
}
