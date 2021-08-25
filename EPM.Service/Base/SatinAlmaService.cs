using EPM.Dto.Base;
using EPM.Dto.FormModels.SatinAlma;
using EPM.Dto.Models;
using EPM.Repository.Base;
using EPM.Tools.Extensions;
using EPM.Tools.Helpers;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EPM.Service.Base
{
    public class SatinAlmaService: ISatinAlmaService
    {
        private readonly IEPMRepository _epmRepository;
        public SatinAlmaService(IEPMRepository epmRepository)
        {
            _epmRepository = epmRepository;
        }
        public List<DepoBilgileri> GetInventories(int ORGANIZATION_ID = 105)
        {
            string SQL = string.Format(@"SELECT SECONDARY_INVENTORY_NAME AS CODE, ORGANIZATION_ID, DESCRIPTION
    FROM APPS.mtl_secondary_inventories
   WHERE organization_id = {0} AND disable_date IS NULL
ORDER BY creation_date ASC",ORGANIZATION_ID);

            return _epmRepository.DeserializeList<DepoBilgileri>(SQL);
        }


        public List<SaticiBilgileri> GetVendorList(int ORGANIZATION_ID = 105)
        {
            string SQL = string.Format(@"SELECT   pov.vendor_name, pos.vendor_site_id
                            FROM po.po_vendor_sites_all pos, hr_locations hr, 
                            po.po_vendors pov, apps.org_organization_definitions ood 
                            WHERE hr.location_id = pos.bill_to_location_id 
                            AND pos.vendor_id = pov.vendor_id and pov.END_DATE_ACTIVE is null 
                            and pos.INACTIVE_DATE is null
                            AND ood.organization_id = hr.inventory_organization_id 
                            AND ood.operating_unit = pos.org_id 
                            AND hr.inventory_organization_id = {0}
                             ORDER BY 1", ORGANIZATION_ID);

            return _epmRepository.DeserializeList<SaticiBilgileri>(SQL);
        }

        public List<ParaBirimleri> GetParaBirimleri(int ORGANIZATION_ID = 105, int VENDOR_SITE_ID = 0)
        {
            string sql = string.Format(@"
SELECT NVL (pos.invoice_currency_code, '-') para_birimi
    FROM po.po_vendor_sites_all pos,
         apps.hr_locations hr,
         po.po_vendors pov,
         apps.org_organization_definitions ood
   WHERE     hr.location_id = pos.bill_to_location_id
         AND pos.vendor_id = pov.vendor_id
         AND ood.organization_id = hr.inventory_organization_id
         AND ood.operating_unit = pos.org_id
         AND hr.inventory_organization_id = {0}
         AND pos.vendor_site_id = {1}
ORDER BY 1
                        ",ORGANIZATION_ID,VENDOR_SITE_ID);
            string paraBirimi = _epmRepository.ReadString(sql);

            sql = string.Format(@"  SELECT currency_code
    FROM apps.FND_CURRENCIES
   WHERE     currency_flag = 'Y'
         AND enabled_flag = 'Y'
         AND end_date_Active IS NULL
         AND currency_code != '{0}'
ORDER BY 1",paraBirimi);

            List<ParaBirimleri> birimler = _epmRepository.DeserializeList<ParaBirimleri>(sql);

            birimler.Insert(0, new ParaBirimleri() { CURRENCY_CODE = paraBirimi });
            birimler.RemoveAll(ob => ob.CURRENCY_CODE == "-");
            birimler.RemoveAll(ob => ob.CURRENCY_CODE == "");

            return birimler;
        }

        public OperationResult SatinAlmaListesiniAktarExcelYukle(HttpRequest request,int ORGANIZATION_ID)
        {
            WebLogin user = new CookieHelper().GetObjectFromCookie<WebLogin>(request.HttpContext,"USER");
            _epmRepository.ExecSql("DELETE FDEIT005.EPM_CREATE_PO_TEMP WHERE USER_CODE='" + user.USER_CODE + "'");
            OperationResult result = new OperationResult();
            result.ISOK = true;
            try
            {
                List<EPM_CREATE_PO_TEMP> items = new List<EPM_CREATE_PO_TEMP>();
                var myFile = request.Form.Files["myFile"];
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(myFile.OpenReadStream()))
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        if (i != 0)
                        {
                            if (!result.ISOK)
                                break;
                            EPM_CREATE_PO_TEMP item = new EPM_CREATE_PO_TEMP()
                            {
                                SIPARIS_NO = reader.GetValue(0).ToStringParse(),
                                KALEM_KODU = reader.GetValue(1).ToStringParse(),
                                ADET = reader.GetValue(2).DecimalParse(),
                                BIRIM_FIYAT = reader.GetValue(3).DecimalParse(),
                                SEZON = reader.GetValue(4).ToStringParse(),
                                MASTER_SIPARIS = reader.GetValue(5).ToStringParse(),
                                SATIR_TERMIN = reader.GetValue(5).DatetimeParse(),
                                USER_CODE = user.USER_CODE
                            };
                            result.ISOK = KalemKoduVarmi(item.KALEM_KODU, ORGANIZATION_ID);
                            result.SYSTEMMESSAGE = result.ISOK ? "Başarılı" :item.KALEM_KODU +" Kalem Kodu Sistemde Tanımlı Değil";
                            if (result.ISOK)
                            {
                                result.ISOK = KalemKategorisiVarmi(item.KALEM_KODU, ORGANIZATION_ID);
                                result.SYSTEMMESSAGE = result.ISOK ? "Başarılı" : item.KALEM_KODU + " Kalem Kategorisi Sistemde Tanımlı Değil";
                                if (result.ISOK)
                                {
                                    if (item.ADET <= 0)
                                    {
                                        result.ISOK = false;
                                        result.SYSTEMMESSAGE = item.KALEM_KODU + " Kalemi için Geçersiz Adet Girişi";
                                    }

                                    if (item.BIRIM_FIYAT <= 0)
                                    {
                                        result.ISOK = false;
                                        result.SYSTEMMESSAGE = item.KALEM_KODU + " Kalemi için Geçersiz Fiyat Girişi";
                                    }
                                    if (item.BIRIM_FIYAT <= 0)
                                    {
                                        result.ISOK = false;
                                        result.SYSTEMMESSAGE = item.KALEM_KODU + " Kalemi için Geçersiz Fiyat Girişi";
                                    }
                                    items.Add(item);
                                }
                                

                                
                            }
                            
                        }
                        i++;
                    } 
                }
                if(result.ISOK)
                foreach (EPM_CREATE_PO_TEMP item in items)
                        _epmRepository.Serialize(item); 
            }
            catch (Exception ex)
            {
                result.ISOK = false;
                result.SYSTEMMESSAGE = ex.Message;
            } 
            return result;
        }
        int KalemItemId(string KALEM_KODU, int ORGANIZATION_ID)
        {
            string sql = string.Format(@"SELECT INVENTORY_ITEM_ID
                              FROM INV.MTL_SYSTEM_ITEMS_B
                             WHERE     ORGANIZATION_ID = {0}
                           AND SEGMENT1 || '.' || SEGMENT2 || '.' || SEGMENT3 = '{1}'", ORGANIZATION_ID, KALEM_KODU);

            return _epmRepository.ReadInteger(sql);
        }
        bool KalemKoduVarmi(string KALEM_KODU, int ORGANIZATION_ID)
        {  
            return KalemItemId(KALEM_KODU,ORGANIZATION_ID) > 0;
        }

        bool KalemKategorisiVarmi(string KALEM_KODU,int ORGANIZATION_ID)
        {
            string sql = string.Format(@"SELECT ITEM_ID from ITEMS   WHERE ORGANIZATION_ID =  {0}
                                        AND ITEM_CATEGORY_ID!=143 AND KALEM_KODU = '{1}'", ORGANIZATION_ID, KALEM_KODU);

            return _epmRepository.ReadInteger(sql) > 0;
        }

        void InitSatinAlma(HttpContext context)
        {
            WebLogin login = new CookieHelper().GetObjectFromCookie<WebLogin>(context,"USER");
            //OracleServer.ExecSql("call apps.xxod_pkg.apps_initialize (" + login.USER_ID + ", 50257, 201, NULL, NULL)");
        }

        long HeaderInsert(WebLogin login, int ORGANIZATION_ID,string STOK_YERI,int VENDOR_SITE_ID,string PB,DateTime  TERMIN,string SEZON,string SIP_NO)
        { 
            long batch = 0;
            try
            {
                using (OracleConnection sqlConnection = new OracleConnection(""))
                {
                    sqlConnection.Open();
                    string sql = "apps.xxer_po_pkg.po_headers_insert";
                    OracleCommand header = new OracleCommand(sql, sqlConnection);
                    header.CommandType = System.Data.CommandType.StoredProcedure;

                    OracleParameter p1 = new OracleParameter("P_SIPARIS_NO", OracleDbType.Varchar2);
                    p1.Direction = ParameterDirection.Input;
                    p1.Value = SIP_NO;
                    header.Parameters.Add(p1);

                    OracleParameter p2 = new OracleParameter("P_ORGANIZATION_ID", OracleDbType.Int32);
                    p2.Direction = ParameterDirection.Input;
                    p2.Value = ORGANIZATION_ID;
                    header.Parameters.Add(p2);

                    OracleParameter p4 = new OracleParameter("P_VENDOR_SITE_ID", OracleDbType.Int32);
                    p4.Direction = ParameterDirection.Input;
                    p4.Value = VENDOR_SITE_ID;
                    header.Parameters.Add(p4);


                    OracleParameter sonucPrm = new OracleParameter("O_BATCH_ID", OracleDbType.Int32);
                    sonucPrm.Direction = ParameterDirection.Output;
                    header.Parameters.Add(sonucPrm);

                    header.ExecuteNonQuery();

                    batch = Convert.ToInt64(sonucPrm.Value.ToString());

                    if (batch > 0)
                    {
                        //sql = string.Format(@"update xxer.od_po_headers_interface set subinventory='{0}',source = 'EPM_CREATE_PO_',currency_code = '{1}',termin = to_date('{2}','dd.mm.RRRR'),
                        //created_by ={3},creation_date = sysdate,last_updated_by ={3},last_update_date = sysdate,sezon = '{4}' WHERE batch_id={5}", STOK_YERI, PB, TERMIN.Date.ToString("d"), login.USER_ID, SEZON, batch);
                        //OracleServer.ExecSql(sql);
                    }

                }
            }
            catch (Exception ex)
            {
                batch = -3;
            }
            
            return batch;
        }

        int TalepOlustur(long batchId,WebLogin login)
        {
            int talepNo = 0;
            try
            {
                using (OracleConnection sqlConnection = new OracleConnection(""))
                {
                    sqlConnection.Open();
                    string sql = "apps.xxer_po_pkg.PO_ARAYUZE_AL";
                    OracleCommand header = new OracleCommand(sql, sqlConnection);
                    header.CommandType = System.Data.CommandType.StoredProcedure;

                    OracleParameter p1 = new OracleParameter("P_BATCH_ID", OracleDbType.Int32);
                    p1.Direction = ParameterDirection.Input;
                    p1.Value = batchId;
                    header.Parameters.Add(p1);

                    //OracleParameter p2 = new OracleParameter("P_USER_ID", OracleDbType.Int32);
                    //p2.Direction = ParameterDirection.Input;
                    //p2.Value = login.USER_ID;
                    //header.Parameters.Add(p2);

                    OracleParameter p4 = new OracleParameter("P_SIP_DURUMU", OracleDbType.Varchar2);
                    p4.Direction = ParameterDirection.Input;
                    p4.Value = "APPROVED";
                    header.Parameters.Add(p4);


                    OracleParameter sonucPrm = new OracleParameter("O_TALEP_NO", OracleDbType.Int32);
                    sonucPrm.Direction = ParameterDirection.Output;
                    header.Parameters.Add(sonucPrm);

                    header.ExecuteNonQuery();

                    talepNo = Convert.ToInt32(sonucPrm.Value.ToString());

                     

                }
            }
            catch (Exception ex)
            { 
            }
            return talepNo;

        }
        public void SatinAlmaAktar(HttpContext context,JObject obj)
        {
            WebLogin login = new CookieHelper().GetObjectFromCookie<WebLogin>(context,"USER");
            List<EPM_CREATE_PO_TEMP> aktarim = _epmRepository.DeserializeList<EPM_CREATE_PO_TEMP>("SELECT * FROM FDEIT005.EPM_CREATE_PO_TEMP WHERE USER_CODE='" + login.USER_CODE+"'");
            dynamic values = obj;
            string STOK_YERI = Convert.ToString(values.STOK_YERI);
            int VENDOR_SITE_ID = Convert.ToInt32(values.TEDARIK.Value);
            int ORGANIZATION_ID =105;
            string PB = Convert.ToString(values.PB.Value);
            DateTime TERMIN = Convert.ToDateTime(values.TERMIN);
            string SEZON = aktarim[0].SEZON;
            string SIP_NO = aktarim[0].SIPARIS_NO;

            InitSatinAlma(context);
            long BatchId = HeaderInsert(login, ORGANIZATION_ID, STOK_YERI, VENDOR_SITE_ID, PB, TERMIN, SEZON, SIP_NO);

            foreach (var item in aktarim)
            {
                //string sql = "insert into xxer.od_po_lines_interface(BATCH_ID, SIPARIS_NO, " +
                //                        "KALEM_KODU,  ITEM_ID, ADET, SATIR_TERMIN, BIRIM_FIYAT, CREATED_BY, " +
                //                        "CREATION_DATE, STATUS) " +
                //                        "values(" + BatchId + ", '" + item.SIPARIS_NO + "', '" + item.KALEM_KODU + "', " + KalemItemId(item.KALEM_KODU, ORGANIZATION_ID) + ", " + item.ADET + ", to_Date('" + item.SATIR_TERMIN.Date.ToString("d") + "','dd.mm.RRRR'), " +
                //                        item.BIRIM_FIYAT.ToString().Replace(",", ".") +
                //                        ", " + login.USER_ID + ", sysdate, 'NEW')";
                //OracleServer.ExecSql(sql);
            }
            TalepOlustur(BatchId, login);
        }
    }
}
