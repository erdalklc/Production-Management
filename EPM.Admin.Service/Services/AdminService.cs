using EPM.Admin.Dto.Admin;
using EPM.Admin.Dto.Extensions;
using EPM.Admin.Repository.Repository;
using EPM.Dto.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;

namespace EPM.Admin.Service.Services
{
    public class AdminService:IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public List<KullaniciListe> GetUserList()
        {
            List<KullaniciListe> liste = new List<KullaniciListe>();
            using (var principalContext = new PrincipalContext(ContextType.Domain, "eren.holding"))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(principalContext)))
                {
                    List<UserPrincipal> users = searcher.FindAll().Select(u => (UserPrincipal)u).Where(ob => ob.Enabled == true && ob.EmailAddress != "" && ob.EmailAddress != null).ToList();
                    users.ForEach(us =>
                    {
                        KullaniciListe l = new KullaniciListe();
                        l.USER_CODE = us.Name;
                        l.USER_EMAIL = us.EmailAddress;
                        l.USER_NAME = us.DisplayName;
                        liste.Add(l);
                    });
                }
            }
            liste = liste.OrderBy(ob => ob.USER_CODE).ToList();
            return liste;
        }

        public List<KullaniciYetki> GetUserResponsibiity(string USER_CODE)
        {
            string sql = string.Format(@"SELECT NVL(B.ID,0) AS ID,'{0}' AS USER_CODE,R.ID AS RESPONSIBILITY_ID,R.ADI RESPONSIBILITY_NAME ,CASE WHEN B.ID IS NULL THEN 0 ELSE 1 END AS ISACTIVE  FROM FDEIT005.EPM_PRODUCTION_RESPONSIBILITY R
LEFT JOIN (
SELECT * FROM (SELECT UR.ID
,UR.RESPONSIBILITY_ID
,UR.USER_ID
,RSP.ADI AS RESPONSIBILITY_NAME
 FROM FDEIT005.EPM_USER_RESPONSIBILITY UR
INNER JOIN FDEIT005.EPM_PRODUCTION_RESPONSIBILITY RSP ON RSP.ID=UR.RESPONSIBILITY_ID 
WHERE USER_CODE='{0}' ) A )B ON B.RESPONSIBILITY_ID = R.ID
ORDER BY RESPONSIBILITY_ID
", USER_CODE);
            List<KullaniciYetki> res = _adminRepository.DeserializeList<KullaniciYetki>(sql);
            return res;
        }

        public List<KullaniciFabricTypes> GetUserFabricTypes(string USER_CODE)
        {
            string sql = string.Format(@"SELECT '{0}' AS USER_ID,R.ID AS FABRIC_TYPE_ID,R.ADI FABRIC_NAME ,CASE WHEN B.ID IS NULL THEN 0 ELSE 1 END AS ISACTIVE  FROM FDEIT005.EPM_PRODUCTION_FABRIC_TYPES R
LEFT JOIN (
SELECT * FROM (SELECT UR.ID
,UR.FABRIC_TYPE_ID
,UR.USER_CODE
,RSP.ADI AS RESPONSIBILITY_NAME
 FROM FDEIT005.EPM_USER_FABRIC_TYPES UR
INNER JOIN FDEIT005.EPM_PRODUCTION_FABRIC_TYPES RSP ON RSP.ID=UR.FABRIC_TYPE_ID 
WHERE USER_CODE='{0}' ) A )B ON B.FABRIC_TYPE_ID = R.ID
ORDER BY FABRIC_TYPE_ID", USER_CODE);

            List<KullaniciFabricTypes> res = _adminRepository.DeserializeList<KullaniciFabricTypes>(sql);
            return res;
        }

        public List<KullaniciMarka> GetUserBrands(string USER_CODE)
        {
            string sql = string.Format(@"SELECT '{0}' AS USER_ID,R.ID AS BRAND_ID,R.ADI BRAND_NAME ,CASE WHEN B.ID IS NULL THEN 0 ELSE 1 END AS ISACTIVE  FROM FDEIT005.EPM_PRODUCTION_BRANDS R
LEFT JOIN (
SELECT * FROM (SELECT UR.ID
,UR.BRAND_ID
,UR.USER_CODE
,RSP.ADI AS RESPONSIBILITY_NAME
 FROM FDEIT005.EPM_USER_BRANDS UR
INNER JOIN FDEIT005.EPM_PRODUCTION_BRANDS RSP ON RSP.ID=UR.BRAND_ID 
WHERE USER_CODE='{0}' ) A )B ON B.BRAND_ID = R.ID
ORDER BY BRAND_ID", USER_CODE);

            List<KullaniciMarka> res = _adminRepository.DeserializeList<KullaniciMarka>(sql);
            return res;
        }

        public List<KullaniciProductionTypes> GetUserProductionTypes(string USER_CODE)
        {
            string sql = string.Format(@"SELECT '{0}' AS USER_CODE,R.ID AS PRODUCTION_TYPE_ID,R.ADI PRODUCTION_NAME ,CASE WHEN B.ID IS NULL THEN 0 ELSE 1 END AS ISACTIVE  FROM FDEIT005.EPM_PRODUCTION_TYPES R
LEFT JOIN (
SELECT * FROM (SELECT UR.ID
,UR.PRODUCTION_TYPE_ID
,UR.USER_CODE
,RSP.ADI AS RESPONSIBILITY_NAME
 FROM FDEIT005.EPM_USER_PRODUCTION_TYPES UR
INNER JOIN FDEIT005.EPM_PRODUCTION_TYPES RSP ON RSP.ID=UR.PRODUCTION_TYPE_ID 
WHERE USER_CODE='{0}' ) A )B ON B.PRODUCTION_TYPE_ID = R.ID", USER_CODE);

            List<KullaniciProductionTypes> res = _adminRepository.DeserializeList<KullaniciProductionTypes>(sql);
            return res;
        }

        public object[] UserBrandsUpdate(int key, string values, string USER_CODE)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                KullaniciMarka yetki = new KullaniciMarka();
                JsonConvert.PopulateObject(values, yetki);
                yetki.USER_CODE = USER_CODE;
                yetki.BRAND_ID = key;
                EPM_USER_BRANDS res = _adminRepository.Deserialize<EPM_USER_BRANDS>(String.Format("SELECT * FROM FDEIT005.EPM_USER_BRANDS WHERE USER_CODE='{0}' AND BRAND_ID={1}", yetki.USER_CODE, yetki.BRAND_ID));

                if (yetki.ISACTIVE)
                {
                    if (res == null)
                        res = new EPM_USER_BRANDS();
                    if (res.ID.IntParse() == 0)
                    {
                        res.USER_CODE = yetki.USER_CODE;
                        res.BRAND_ID = yetki.BRAND_ID;
                        _adminRepository.Serialize(res);
                    }
                }
                else
                {
                    _adminRepository.ExecSql(String.Format("DELETE FROM FDEIT005.EPM_USER_BRANDS WHERE USER_CODE='{0}' AND BRAND_ID={1}", yetki.USER_CODE, yetki.BRAND_ID));
                }
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public object[] UserResponsibilityUpdate(int key, string values, string USER_CODE)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                KullaniciYetki yetki = new KullaniciYetki();
                JsonConvert.PopulateObject(values, yetki);
                yetki.USER_CODE = USER_CODE;
                yetki.RESPONSIBILITY_ID = key;
                EPM_USER_RESPONSIBILITY res = _adminRepository.Deserialize<EPM_USER_RESPONSIBILITY>(String.Format("SELECT * FROM FDEIT005.EPM_USER_RESPONSIBILITY WHERE USER_CODE='{0}' AND RESPONSIBILITY_ID={1}", yetki.USER_CODE, yetki.RESPONSIBILITY_ID));

                if (yetki.ISACTIVE)
                {
                    if (res == null)
                        res = new EPM_USER_RESPONSIBILITY();
                    if (res.ID.IntParse() == 0)
                    {
                        res.USER_CODE = yetki.USER_CODE;
                        res.RESPONSIBILITY_ID = yetki.RESPONSIBILITY_ID;
                        _adminRepository.Serialize(res);
                    }
                }
                else
                {
                    _adminRepository.ExecSql(String.Format("DELETE FROM FDEIT005.EPM_USER_RESPONSIBILITY WHERE USER_CODE='{0}' AND RESPONSIBILITY_ID={1}", yetki.USER_CODE, yetki.RESPONSIBILITY_ID));
                }
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public object[] UserFabricTypeUpdate(int key, string values, string USER_CODE)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                KullaniciFabricTypes yetki = new KullaniciFabricTypes();
                JsonConvert.PopulateObject(values, yetki);
                yetki.USER_CODE = USER_CODE;
                yetki.FABRIC_TYPE_ID = key;
                EPM_USER_FABRIC_TYPES res = _adminRepository.Deserialize<EPM_USER_FABRIC_TYPES>(String.Format("SELECT * FROM FDEIT005.EPM_USER_FABRIC_TYPES WHERE USER_CODE='{0}' AND FABRIC_TYPE_ID={1}", yetki.USER_CODE, yetki.FABRIC_TYPE_ID));

                if (yetki.ISACTIVE)
                {
                    if (res == null)
                        res = new EPM_USER_FABRIC_TYPES();
                    if (res.ID.IntParse() == 0)
                    {
                        res.USER_CODE = yetki.USER_CODE;
                        res.FABRIC_TYPE_ID = yetki.FABRIC_TYPE_ID;
                        _adminRepository.Serialize(res);
                    }
                }
                else
                {
                    _adminRepository.ExecSql(String.Format("DELETE FROM FDEIT005.EPM_USER_FABRIC_TYPES WHERE USER_CODE='{0}' AND FABRIC_TYPE_ID={1}", yetki.USER_CODE, yetki.FABRIC_TYPE_ID));
                }
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public object[] UserProductionTypeUpdate(int key, string values, string USER_ID)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                KullaniciProductionTypes yetki = new KullaniciProductionTypes();
                JsonConvert.PopulateObject(values, yetki);
                yetki.USER_CODE = USER_ID;
                yetki.PRODUCTION_TYPE_ID = key;
                EPM_USER_PRODUCTION_TYPES res = _adminRepository.Deserialize<EPM_USER_PRODUCTION_TYPES>(String.Format("SELECT * FROM FDEIT005.EPM_USER_PRODUCTION_TYPES WHERE USER_CODE='{0}' AND PRODUCTION_TYPE_ID={1}", yetki.USER_CODE, yetki.PRODUCTION_TYPE_ID));

                if (yetki.ISACTIVE)
                {
                    if (res == null)
                        res = new EPM_USER_PRODUCTION_TYPES();
                    if (res.ID.IntParse() == 0)
                    {
                        res.USER_CODE = yetki.USER_CODE;
                        res.PRODUCTION_TYPE_ID = yetki.PRODUCTION_TYPE_ID;
                        _adminRepository.Serialize(res);
                    }
                }
                else
                {
                    _adminRepository.ExecSql(String.Format("DELETE FROM FDEIT005.EPM_USER_PRODUCTION_TYPES WHERE USER_CODE='{0}' AND PRODUCTION_TYPE_ID={1}", yetki.USER_CODE, yetki.PRODUCTION_TYPE_ID));
                }
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }
    }
}
