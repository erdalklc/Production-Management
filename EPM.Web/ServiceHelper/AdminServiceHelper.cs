using EPM.Admin.Dto.Admin;
using EPM.Tools.Helpers; 
using System.Collections.Generic; 

namespace EPM.Web.ServiceHelper
{
    public class AdminServiceHelper : EPMHttpCaller
    {
        public static List<KullaniciListe> GetUserList()
        {
            string apiUrl = "GetUserList";
            var list = GetRequest<List<KullaniciListe>>(EPMServiceType.Admin, apiUrl);
            return list;
        }

        public static List<KullaniciYetki> GetUserResponsibiity(string USER_CODE)
        {
            string apiUrl = "GetUserResponsibiity";
            var list = PostRequest<object[], List<KullaniciYetki>>(EPMServiceType.Admin, apiUrl, new object[] { USER_CODE });
            return list;
        }

        public static object[] UserResponsibilityUpdate(int key, string values, string USER_CODE)
        {
            string apiUrl = "UserResponsibilityUpdate";
            var list = PostRequest<object[], object[]>(EPMServiceType.Admin, apiUrl, new object[] { key, values, USER_CODE });
            return list;
        }

        public static object[] UserFabricTypeUpdate(int key, string values, string USER_CODE)
        {
            string apiUrl = "UserFabricTypeUpdate";
            var list = PostRequest<object[], object[]>(EPMServiceType.Admin, apiUrl, new object[] { key, values, USER_CODE });
            return list;
        }

        public static List<KullaniciFabricTypes> GetUserFabricTypes(string USER_CODE)
        {
            string apiUrl = "GetUserFabricTypes";
            var list = PostRequest<object[], List<KullaniciFabricTypes>>(EPMServiceType.Admin, apiUrl, new object[] { USER_CODE });
            return list;
        }

        public static List<KullaniciProductionTypes> GetUserProductionTypes(string USER_CODE)
        {
            string apiUrl = "GetUserProductionTypes";
            var list = PostRequest<object[], List<KullaniciProductionTypes>>(EPMServiceType.Admin, apiUrl, new object[] { USER_CODE });
            return list;
        }

        public static object[] UserProductionTypeUpdate(int key, string values, string USER_CODE)
        {
            string apiUrl = "UserProductionTypeUpdate";
            var list = PostRequest<object[], object[]>(EPMServiceType.Admin, apiUrl, new object[] { key, values, USER_CODE });
            return list;
        }

        public static object[] UserBrandsUpdate(int key, string values, string USER_CODE)
        {
            string apiUrl = "UserBrandsUpdate";
            var list = PostRequest<object[], object[]>(EPMServiceType.Admin, apiUrl, new object[] { key, values, USER_CODE });
            return list;
        }

        public static List<KullaniciMarka> GetUserBrands(string USER_CODE)
        {
            string apiUrl = "GetUserBrands";
            var list = PostRequest<object[], List<KullaniciMarka>>(EPMServiceType.Admin, apiUrl, new object[] { USER_CODE });
            return list;
        }
    }
}
