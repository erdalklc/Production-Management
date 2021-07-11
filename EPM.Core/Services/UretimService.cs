using Dapper.Oracle;
using EPM.Core.FormModels.Uretim;
using EPM.Core.Helpers;
using EPM.Core.Loglar;
using EPM.Core.Managers;
using EPM.Core.Models;
using EPM.Core.Nesneler;
using ExcelDataReader;
using Microsoft.AspNetCore.Http; 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Core.Services
{
    public class UretimService : IUretimService
    {
        public List<UretimListesi> UretimListesi(HttpContext context, string MODEL, string SEZON, string URETIM_TIPI, int DURUM)
        {
            MODEL = MODEL.InjectionString();
            SEZON = SEZON.InjectionString();
            URETIM_TIPI = URETIM_TIPI.InjectionString();
            List<UretimListesi> modelIcUretim = new List<UretimListesi>();
            List<UretimListesi> modelFason = new List<UretimListesi>();
            List<UretimListesi> returnList = new List<UretimListesi>();
            if (URETIM_TIPI == "İÇ ÜRETİM")
            {
                string sql = @"SELECT 
                                    TAKIP_NO
                                    ,MODEL
                                    ,'' RENK
                                    ,SEZON
                                    ,ADET
                                    ,TARIH
                                    ,RECETE
                                    ,AGENT_NAME OLUSTURAN
                                    ,'' FIRMA_ADI
                                    ,CREATION_DATE
                                    ,END_DATE
                                    ,PROCESSNAME
                                    ,OLMASIGEREKEN
                                    ,PROCESSINFO
                                     FROM (
                                    SELECT DISTINCT A.*
                                    ,CASE WHEN TANIMLANAN=0 THEN 'TAKIP BASLATILMADI' ELSE  CASE WHEN TAMAMLANAN =TANIMLANAN THEN 'TAMAMLANDI' ELSE CASE WHEN PROCESSNAME IS NULL THEN 'YOK' ELSE PROCESSNAME END END END AS PROCESSINFO 
                                    FROM (SELECT
                                    PH.SEGMENT1 AS TAKIP_NO 
                                    ,PH.COMMENTS AS MODEL
                                    ,PH.ATTRIBUTE1 AS SEZON
                                    ,CAST(PH.ATTRIBUTE11 AS NUMBER) AS ADET
                                    ,TO_DATE(PH.ATTRIBUTE12,'DD.MM.YYYY') AS TARIH
                                    ,(SELECT ADI FROM FDEIT005.EPM_PRODUCTION_RECIPE WHERE ID=PH.ATTRIBUTE9) RECETE
                                    ,CAST(PH.ATTRIBUTE9 AS NUMBER) AS RECETEID
                                    ,AG.FIRST_NAME ||' '||AG.LAST_NAME AS AGENT_NAME,PH.CREATION_DATE 
                                    ,(SELECT END_DATE FROM (SELECT PI.PROCESSNAME,
                                             PL.END_DATE,
                                             PL.HEADER_ID,
                                             RD.HEADER_ID AS RECETE_ID,
                                             (SELECT COUNT (*)
                                                FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                               WHERE STATUS = 2 AND PL2.HEADER_ID = PL.HEADER_ID)
                                                AS TAMAMLANAN,
                                             (SELECT COUNT (*)
                                                FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                               WHERE PL2.HEADER_ID = PL.HEADER_ID)
                                                AS TANIMLANAN
                                        FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL
                                             INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI
                                                ON PI.ID = PL.PROCESS_ID
                                             INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD
                                                ON RD.PROCESS_ID = PL.PROCESS_ID
                                       WHERE PL.STATUS = 1
                                    ORDER BY PL.HEADER_ID, RD.QUEUE)DE WHERE DE.HEADER_ID = PH.SEGMENT1
                                         AND DE.RECETE_ID = CAST (PH.ATTRIBUTE9 AS NUMBER) AND ROWNUM=1) END_DATE, 
                                   (SELECT PROCESSNAME FROM (SELECT PI.PROCESSNAME,
                                             PL.END_DATE,
                                             PL.HEADER_ID,
                                             RD.HEADER_ID AS RECETE_ID,
                                             (SELECT COUNT (*)
                                                FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                               WHERE STATUS = 2 AND PL2.HEADER_ID = PL.HEADER_ID)
                                                AS TAMAMLANAN,
                                             (SELECT COUNT (*)
                                                FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                               WHERE PL2.HEADER_ID = PL.HEADER_ID)
                                                AS TANIMLANAN
                                        FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL
                                             INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI
                                                ON PI.ID = PL.PROCESS_ID
                                             INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD
                                                ON RD.PROCESS_ID = PL.PROCESS_ID
                                       WHERE PL.STATUS = 1
                                    ORDER BY PL.HEADER_ID, RD.QUEUE)DE WHERE DE.HEADER_ID = PH.SEGMENT1
                                         AND DE.RECETE_ID = CAST (PH.ATTRIBUTE9 AS NUMBER) AND ROWNUM=1) PROCESSNAME  
                                         ,(SELECT FDEIT005.URETIM_FND_STATE(CAST (PH.SEGMENT1 AS NUMBER)) FROM DUAL) OLMASIGEREKEN
                                    ,(SELECT COUNT(*)  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2 WHERE STATUS IN (2,4) AND PL2.HEADER_ID=PH.SEGMENT1) AS TAMAMLANAN
                                    ,(SELECT COUNT(*)  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2 WHERE   PL2.HEADER_ID=PH.SEGMENT1) AS TANIMLANAN
                                    ,(SELECT MIN(START_DATE) FROM  FDEIT005.EPM_PRODUCTION_TRACKING_LIST WHERE PROCESS_ID=(SELECT ID FROM FDEIT005.EPM_PRODUCTION_PROCESS_INFO WHERE PROCESSTAG=4) AND HEADER_ID=CAST(PH.SEGMENT1 AS NUMBER)) KUMAS_START
                                    FROM APPS.PO_HEADERS_ALL PH 
                                    INNER JOIN APPS.PER_PEOPLE_F AG ON AG.PERSON_ID=PH.AGENT_ID  
                                    where   AG.EMPLOYEE_NUMBER IS NOT NULL  --AND  PH.ATTRIBUTE1 ='{0}'
                                    AND TRUNC (SYSDATE) BETWEEN AG.EFFECTIVE_START_DATE AND AG.EFFECTIVE_END_DATE
                                    AND PH.ATTRIBUTE1 IS NOT NULL
                                    AND PH.ATTRIBUTE11 IS NOT NULL
                                    AND PH.ATTRIBUTE9 IS NOT NULL
                                    AND PH.ATTRIBUTE12 IS NOT NULL ) A
                                    ) B WHERE 0=0";
                if (MODEL != "")
                    sql += " AND MODEL LIKE '" + MODEL + "%'";
                if (SEZON != "")
                    sql += " AND SEZON='" + SEZON + "'";
                if (DURUM == 1)
                    sql += " AND END_DATE<SYSDATE";
                else if (DURUM == 2)
                    sql += " AND END_DATE>=SYSDATE";
                modelIcUretim = OracleServer.DeserializeList<UretimListesi>(sql);

                foreach (var item in modelIcUretim)
                {
                    try
                    {
                        if (item.MODEL.Contains('-'))
                        {
                            item.RENK = item.MODEL.ToString().Split('-')[1].ToString();
                            item.MODEL = item.MODEL.ToString().Split('-')[0].ToString();
                        }
                        else
                        {
                            item.RENK = item.MODEL.ToString().Split('.')[1].ToString();
                            item.MODEL = item.MODEL.ToString().Split('.')[0].ToString();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }


                foreach (var item in modelIcUretim)
                    returnList.Add(item);

            }



            if (URETIM_TIPI == "FASON")
            {
                string sql = @"SELECT * FROM (SELECT H.ID AS TAKIP_NO,
        MODEL,
       RENK,
       SEZON,
       ADET,
       H.TAKE_DATE AS TARIH,
       'FASON' AS RECETE,
       US.DESCRIPTION AS OLUSTURAN,
       WF.ADI AS FIRMA_ADI, 
       CREATE_DATE AS CREATION_DATE, 
       (SELECT PROCESSNAME FROM FDEIT005.URETIM_FASON_PROCESSINFO WHERE ID = (SELECT PROCESS_ID FROM FDEIT005.URETIM_FASON_TAKIP_L WHERE HEADER_ID = H.ID AND STATUS IN(1, 2) AND ROWNUM = 1)) AS DEVAMEDEN,
        NVL((SELECT PLANNING_DATE FROM FDEIT005.URETIM_FASON_TAKIP_L WHERE HEADER_ID = H.ID AND STATUS IN(1, 2) AND ROWNUM = 1),SYSDATE) AS END_DATE,
       H.STATUS AS SIPARISDURUMU
  FROM FDEIT005.URETIM_FASON_TAKIP_H H
       INNER JOIN apps.fnd_user US ON US.USER_ID = H.CREATED_BY
       INNER JOIN FDEIT005.URETIM_FASON_WEB_FIRMA WF ON WF.ID = H.FIRMA_ID ";
                if (MODEL != "")
                    sql += " AND MODEL='" + MODEL + "'";
                if (SEZON != "")
                    sql += " AND SEZON='" + SEZON + "'";
                sql += ") A WHERE 0=0";

                if (DURUM == 1)
                    sql += " AND END_DATE<SYSDATE";
                else if (DURUM == 2)
                    sql += " AND END_DATE>=SYSDATE";

                modelFason = OracleServer.DeserializeList<UretimListesi>(sql);
                foreach (var item in modelFason)
                    returnList.Add(item);
            }

            return returnList;
        }


        void CreateFields(HttpRequest request)
        {
            WebLogin user = new CookieHelper().GetUserFromCookie(request.HttpContext); 
            var myFile = request.Form.Files["myFile"];

            using (var reader = ExcelReaderFactory.CreateReader(myFile.OpenReadStream()))
            {
                List<EPM_PRODUCTION_BRANDS> brandList = OracleServer.DeserializeList<EPM_PRODUCTION_BRANDS>("SELECT * FROM FDEIT005.EPM_PRODUCTION_BRANDS1");
                List<EPM_PRODUCTION_SUB_BRANDS> subBrandList = OracleServer.DeserializeList<EPM_PRODUCTION_SUB_BRANDS>("SELECT * FROM FDEIT005.EPM_PRODUCTION_SUB_BRANDS");
                List<EPM_PRODUCTION_FABRIC_TYPES> fabricTypes = OracleServer.DeserializeList<EPM_PRODUCTION_FABRIC_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_FABRIC_TYPES");
                List<EPM_PRODUCTION_MARKET> marketList = OracleServer.DeserializeList<EPM_PRODUCTION_MARKET>("SELECT * FROM FDEIT005.EPM_PRODUCTION_MARKET");
                List<EPM_PRODUCTION_ORDER_TYPES> orderTypes = OracleServer.DeserializeList<EPM_PRODUCTION_ORDER_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_ORDER_TYPES");
                List<EPM_PRODUCTION_SEASON> seasonList = OracleServer.DeserializeList<EPM_PRODUCTION_SEASON>("SELECT * FROM FDEIT005.EPM_PRODUCTION_SEASON");
                List<EPM_PRODUCTION_TYPES> productionTypes = OracleServer.DeserializeList<EPM_PRODUCTION_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_TYPES");
                List<EPM_PRODUCT_GROUP> productGroups = OracleServer.DeserializeList<EPM_PRODUCT_GROUP>("SELECT * FROM FDEIT005.EPM_PRODUCT_GROUP");
                List<EPM_PRODUCTION_RECIPE> productionRecipe = OracleServer.DeserializeList<EPM_PRODUCTION_RECIPE>("SELECT * FROM FDEIT005.EPM_PRODUCTION_RECIPE");
                List<EPM_PRODUCTION_BAND_GROUP> productionBandGroup = OracleServer.DeserializeList<EPM_PRODUCTION_BAND_GROUP>("SELECT * FROM FDEIT005.EPM_PRODUCTION_BAND_GROUP");
                int i = 0;
                while (reader.Read())
                {
                    if (i != 0)
                    { 
                        if(productGroups.Find(ob => ob.ADI == reader.GetValue(4).ToStringParse()) == null)
                        {
                            if (reader.GetValue(4).ToStringParse() != "")
                            { 
                                EPM_PRODUCT_GROUP gr = new EPM_PRODUCT_GROUP();
                                gr.ADI = reader.GetValue(4).ToStringParse();
                                OracleServer.Serialize(gr);
                            }
                        }

                        if (marketList.Find(ob => ob.ADI == reader.GetValue(10).ToStringParse()) == null)
                        {
                            if (reader.GetValue(10).ToStringParse() != "")
                            {
                                EPM_PRODUCTION_MARKET gr = new EPM_PRODUCTION_MARKET();
                                gr.ADI = reader.GetValue(10).ToStringParse();
                                OracleServer.Serialize(gr);
                            }
                        }
                        if (fabricTypes.Find(ob => ob.ADI == reader.GetValue(5).ToStringParse()) == null)
                        {
                            if (reader.GetValue(5).ToStringParse() != "")
                            {
                                EPM_PRODUCTION_FABRIC_TYPES gr = new EPM_PRODUCTION_FABRIC_TYPES();
                                gr.ADI = reader.GetValue(5).ToStringParse();
                                OracleServer.Serialize(gr);
                            }
                        }
                    }
                }
            }
        }

        public object[] CheckForErrors(HttpRequest request
            , List<EPM_PRODUCTION_BRANDS> brandList
            , List<EPM_PRODUCTION_SUB_BRANDS> subBrandList
            , List<EPM_PRODUCTION_FABRIC_TYPES> fabricTypes
            , List<EPM_PRODUCTION_MARKET> marketList
            , List<EPM_PRODUCTION_ORDER_TYPES> orderTypes
            , List<EPM_PRODUCTION_SEASON> seasonList
            , List<EPM_PRODUCTION_TYPES> productionTypes
            , List<EPM_PRODUCT_GROUP> productGroups
            , List<EPM_PRODUCTION_RECIPE> productionRecipe
            , List<EPM_PRODUCTION_BAND_GROUP> productionBandGroup)
        {
            object[] err = { true, "" };
            var myFile = request.Form.Files["myFile"];
            using (var reader = ExcelReaderFactory.CreateReader(myFile.OpenReadStream()))
            {
                int i = 0;
                while (reader.Read())
                {
                    if (i == 0)
                    {
                        if (reader.GetValue(0).ToStringParse() != "BRAND")
                            err[1] += "0.Kolon BRAND olmalıdır<br>";
                        if (reader.GetValue(1).ToStringParse() != "SUB_BRAND")
                            err[1] += "1.Kolon SUB_BRAND olmalıdır<br>";
                        if (reader.GetValue(2).ToStringParse() != "SEASON")
                            err[1] += "2.Kolon SEASON olmalıdır<br>"; 
                        if (reader.GetValue(3).ToStringParse() != "ORDER_TYPE")
                            err[1] += "3.Kolon ORDER_TYPE olmalıdır<br>"; 
                        if (reader.GetValue(4).ToStringParse() != "PRODUCT_GROUP")
                            err[1] += "4.Kolon PRODUCT_GROUP olmalıdır<br>";
                        if (reader.GetValue(5).ToStringParse() != "MODEL")
                            err[1] += "5.Kolon MODEL olmalıdır<br>";
                        if (reader.GetValue(6).ToStringParse() != "COLOR")
                            err[1] += "6.Kolon COLOR olmalıdır<br>";
                        if (reader.GetValue(7).ToStringParse() != "PRODUCT_SIZE")
                            err[1] += "7.Kolon PRODUCT_SIZE olmalıdır<br>"; 
                        if (reader.GetValue(8).ToStringParse() != "QUANTITY")
                            err[1] += "8.Kolon QUANTITY olmalıdır<br>";
                        if (reader.GetValue(9).ToStringParse() != "MARKET")
                            err[1] += "9.Kolon MARKET olmalıdır<br>";
                        if (reader.GetValue(10).ToStringParse() != "DEADLINE")
                            err[1] += "10.Kolon DEADLINE olmalıdır<br>";
                        if (reader.GetValue(11).ToStringParse() != "SHIPMENT_DATE")
                            err[1] += "11.Kolon SHIPMENT_DATE olmalıdır<br>";
                        if (reader.GetValue(12).ToStringParse() != "COLLECTION_TYPE")
                            err[1] += "12.Kolon COLLECTION_TYPE olmalıdır<br>";
                        if (reader.GetValue(13).ToStringParse() != "ROYALTY")
                            err[1] += "13.Kolon ROYALTY olmalıdır<br>";
                        if (reader.GetValue(14).ToStringParse() != "ANA_TEMA")
                            err[1] += "14.Kolon ANA_TEMA olmalıdır<br>";
                        if (reader.GetValue(15).ToStringParse() != "TEMA")
                            err[1] += "15.Kolon TEMA olmalıdır<br>";
                        if (reader.GetValue(16).ToStringParse() != "PRODUCTION_TYPE")
                            err[1] += "16.Kolon PRODUCTION_TYPE olmalıdır<br>";
                        if (reader.GetValue(17).ToStringParse() != "RECIPE")
                            err[1] += "17.Kolon RECIPE olmalıdır<br>";
                        if (reader.GetValue(18).ToStringParse() != "FABRIC_TYPE")
                            err[1] += "18.Kolon FABRIC_TYPE olmalıdır<br>";
                        if (reader.GetValue(19).ToStringParse() != "BAND_NAME")
                            err[1] += "19.Kolon BAND_NAME olmalıdır<br>";
                        if (reader.GetValue(20).ToStringParse() != "ATTRIBUTE1")
                            err[1] += "20.Kolon ATTRIBUTE1 olmalıdır<br>";
                        if (reader.GetValue(21).ToStringParse() != "ATTRIBUTE2")
                            err[1] += "21.Kolon ATTRIBUTE2 olmalıdır<br>";
                        if (reader.GetValue(22).ToStringParse() != "ATTRIBUTE3")
                            err[1] += "22.Kolon ATTRIBUTE3 olmalıdır<br>";
                        if (reader.GetValue(23).ToStringParse() != "ATTRIBUTE4")
                            err[1] += "23.Kolon ATTRIBUTE4 olmalıdır<br>";
                        if (reader.GetValue(24).ToStringParse() != "ATTRIBUTE5")
                            err[1] += "24.Kolon ATTRIBUTE5 olmalıdır<br>";
                        if (reader.GetValue(25).ToStringParse() != "ATTRIBUTE6")
                            err[1] += "25.Kolon ATTRIBUTE6 olmalıdır<br>";
                        if (reader.GetValue(26).ToStringParse() != "ATTRIBUTE7")
                            err[1] += "26.Kolon ATTRIBUTE7 olmalıdır<br>";
                        if (reader.GetValue(27).ToStringParse() != "ATTRIBUTE8")
                            err[1] += "27.Kolon ATTRIBUTE8 olmalıdır<br>";
                        if (reader.GetValue(28).ToStringParse() != "ATTRIBUTE9")
                            err[1] += "28.Kolon ATTRIBUTE9 olmalıdır<br>";
                        if (reader.GetValue(29).ToStringParse() != "ATTRIBUTE10")
                            err[1] += "29.Kolon ATTRIBUTE10 olmalıdır<br>";
                        if (reader.GetValue(30).ToStringParse() != "PLAN_YEAR")
                            err[1] += "30.Kolon PLAN_YEAR olmalıdır<br>";
                        if (reader.GetValue(31).ToStringParse() != "PLAN_WEEK")
                            err[1] += "31.Kolon PLAN_WEEK olmalıdır<br>";

                        if (err[1].ToStringParse() != "")
                            err[0] = false;

                        if (!(bool)err[0])
                            break;
                    }


                    if (i > 0)
                    {
                        if (seasonList.Count(ob => ob.ADI == reader.GetValue(2).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] = (i + 1) + " Satırındaki " + reader.GetValue(2).ToStringParse() + " Bilgisi SEASON Alanında Bulunamadı<br>";
                        }
                        if (brandList.Count(ob => ob.ADI == reader.GetValue(0).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] = (i + 1) + " Satırındaki " + reader.GetValue(0).ToStringParse() + " Bilgisi BRAND Alanında Bulunamadı<br>";
                        }
                        if (subBrandList.Count(ob => ob.ADI == reader.GetValue(1).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] = (i + 1) + " Satırındaki " + reader.GetValue(1).ToStringParse() + " Bilgisi SUB_BRAND Alanında Bulunamadı<br>";
                        }
                        if (productGroups.Count(ob => ob.ADI == reader.GetValue(4).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] = (i + 1) + " Satırındaki " + reader.GetValue(4).ToStringParse() + " Bilgisi PRODUCT_GROUP Alanında Bulunamadı<br>";
                        }
                        if (fabricTypes.Count(ob => ob.ADI == reader.GetValue(18).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] = (i + 1) + " Satırındaki " + reader.GetValue(18).ToStringParse() + " Bilgisi FABRIC_TYPE Alanında Bulunamadı<br>";
                        }
                        if (productionTypes.Count(ob => ob.ADI == reader.GetValue(16).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] = (i + 1) + " Satırındaki " + reader.GetValue(16).ToStringParse() + " Bilgisi PRODUCTION_TYPE Alanında Bulunamadı<br>";
                        }
                        if (orderTypes.Count(ob => ob.ADI == reader.GetValue(3).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] = (i + 1) + " Satırındaki " + reader.GetValue(3).ToStringParse() + " Bilgisi ORDER_TYPE Alanında Bulunamadı<br>";
                        }
                        if (productionRecipe.Count(ob => ob.ADI == reader.GetValue(17).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] = (i + 1) + " Satırındaki " + reader.GetValue(17).ToStringParse() + " Bilgisi RECIPE Alanında Bulunamadı<br>";
                        }
                        if (marketList.Count(ob => ob.ADI == reader.GetValue(9).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] = (i + 1) + " Satırındaki " + reader.GetValue(9).ToStringParse() + " Bilgisi MARKET Alanında Bulunamadı<br>";
                        }
                        

                    }
                    
                    i++;
                }
            }
            return err;
        }

        public object[] UretimListesiAktarExcelYukle(HttpRequest request)
        {
            object[] obj = { true, "" };
            WebLogin user = new CookieHelper().GetUserFromCookie(request.HttpContext);
            try
            {
                var myFile = request.Form.Files["myFile"];
                List<UretimListesiAktarim> aktarimList = new List<UretimListesiAktarim>();
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                List<EPM_PRODUCTION_BRANDS> brandList = OracleServer.DeserializeList<EPM_PRODUCTION_BRANDS>("SELECT * FROM FDEIT005.EPM_PRODUCTION_BRANDS");
                List<EPM_PRODUCTION_SUB_BRANDS> subBrandList = OracleServer.DeserializeList<EPM_PRODUCTION_SUB_BRANDS>("SELECT * FROM FDEIT005.EPM_PRODUCTION_SUB_BRANDS");
                List<EPM_PRODUCTION_FABRIC_TYPES> fabricTypes = OracleServer.DeserializeList<EPM_PRODUCTION_FABRIC_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_FABRIC_TYPES");
                List<EPM_PRODUCTION_MARKET> marketList = OracleServer.DeserializeList<EPM_PRODUCTION_MARKET>("SELECT * FROM FDEIT005.EPM_PRODUCTION_MARKET");
                List<EPM_PRODUCTION_ORDER_TYPES> orderTypes = OracleServer.DeserializeList<EPM_PRODUCTION_ORDER_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_ORDER_TYPES");
                List<EPM_PRODUCTION_SEASON> seasonList = OracleServer.DeserializeList<EPM_PRODUCTION_SEASON>("SELECT * FROM FDEIT005.EPM_PRODUCTION_SEASON");
                List<EPM_PRODUCTION_TYPES> productionTypes = OracleServer.DeserializeList<EPM_PRODUCTION_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_TYPES");
                List<EPM_PRODUCT_GROUP> productGroups = OracleServer.DeserializeList<EPM_PRODUCT_GROUP>("SELECT * FROM FDEIT005.EPM_PRODUCT_GROUP");
                List<EPM_PRODUCTION_RECIPE> productionRecipe = OracleServer.DeserializeList<EPM_PRODUCTION_RECIPE>("SELECT * FROM FDEIT005.EPM_PRODUCTION_RECIPE");
                List<EPM_PRODUCTION_BAND_GROUP> productionBandGroup = OracleServer.DeserializeList<EPM_PRODUCTION_BAND_GROUP>("SELECT * FROM FDEIT005.EPM_PRODUCTION_BAND_GROUP");

                object[] kontrol = CheckForErrors(request, brandList, subBrandList, fabricTypes, marketList, orderTypes, seasonList, productionTypes, productGroups, productionRecipe, productionBandGroup);

                if (!(bool)kontrol[0])
                {
                    obj[0] = false;
                    obj[1] = kontrol[1];  
                    request.HttpContext.Response.StatusCode = 400;
                    new MailHelper().SendEMail(user.EMAIL, "Üretim Listesi Aktarımı HK.","Aktarım Tamamlanamadı\n" +obj[1].ToString());
                    return obj;
                }

                using (var reader = ExcelReaderFactory.CreateReader(myFile.OpenReadStream()))
                {

                    int i = 0;
                    while (reader.Read())
                    {
                        if (i != 0)
                        {
                            UretimListesiAktarim aktarim = new UretimListesiAktarim();

                            aktarim.BRAND = brandList.Find(ob => ob.ADI == reader.GetValue(0).ToStringParse().Trim()).ID;
                            aktarim.SUB_BRAND = subBrandList.Find(ob => ob.ADI == reader.GetValue(1).ToStringParse()).ID;
                            aktarim.SEASON = seasonList.Find(ob => ob.ADI == reader.GetValue(2).ToStringParse().Trim()).ID;
                            aktarim.ORDER_TYPE = orderTypes.Find(ob => ob.ADI == reader.GetValue(3).ToStringParse().Trim()).ID;
                            aktarim.PRODUCT_GROUP = productGroups.Find(ob => ob.ADI == reader.GetValue(4).ToStringParse().Trim()).ID;
                            aktarim.MODEL = reader.GetValue(5).ToStringParse();
                            aktarim.COLOR = reader.GetValue(6).ToStringParse();
                            aktarim.PRODUCT_SIZE = reader.GetValue(7).ToStringParse();
                            aktarim.QUANTITY = reader.GetValue(8).IntParse();
                            aktarim.MARKET = marketList.Find(ob => ob.ADI == reader.GetValue(9).ToStringParse()).ID;
                            aktarim.DEADLINE = reader.GetValue(10).DatetimeParse();
                            aktarim.SHIPMENT_DATE = reader.GetValue(11).DatetimeParse();
                            aktarim.COLLECTION_TYPE = reader.GetValue(12).IntParse();
                            aktarim.ROYALTY = reader.GetValue(13).ToStringParse() == "0" ? "" : reader.GetValue(13).ToStringParse();
                            aktarim.ANA_TEMA = reader.GetValue(14).ToStringParse() == "0" ? "" : reader.GetValue(14).ToStringParse();
                            aktarim.TEMA = reader.GetValue(15).ToStringParse() == "0" ? "" : reader.GetValue(15).ToStringParse();
                            aktarim.PRODUCTION_TYPE = productionTypes.Find(ob => ob.ADI == reader.GetValue(16).ToStringParse().Trim()).ID;
                            aktarim.RECIPE = productionRecipe.Find(ob => ob.ADI == reader.GetValue(17).ToStringParse().Trim()).ID;
                            aktarim.FABRIC_TYPE = fabricTypes.Find(ob => ob.ADI == reader.GetValue(18).ToStringParse().Trim()).ID;
                            aktarim.BAND_ID = productionBandGroup.Find(ob => ob.ADI == (reader.GetValue(19).ToStringParse() == "0" ? " " : reader.GetValue(19).ToStringParse())).ID; 
                            aktarim.ATTRIBUTE1 = reader.GetValue(20).ToStringParse();
                            aktarim.ATTRIBUTE2 = reader.GetValue(21).ToStringParse();
                            aktarim.ATTRIBUTE3 = reader.GetValue(22).ToStringParse();
                            aktarim.ATTRIBUTE4 = reader.GetValue(23).ToStringParse();
                            aktarim.ATTRIBUTE5 = reader.GetValue(24).ToStringParse();
                            aktarim.ATTRIBUTE6 = reader.GetValue(25).ToStringParse();
                            aktarim.ATTRIBUTE7 = reader.GetValue(26).ToStringParse();
                            aktarim.ATTRIBUTE8 = reader.GetValue(27).ToStringParse();
                            aktarim.ATTRIBUTE9 = reader.GetValue(28).ToStringParse();
                            aktarim.ATTRIBUTE10 = reader.GetValue(29).ToStringParse();
                            aktarim.PLAN_YEAR = reader.GetValue(30).IntParse();
                            aktarim.PLAN_WEEK = reader.GetValue(31).IntParse(); 
                            aktarim.CREATED_BY = user.USER_CODE;
                            aktarim.APPROVAL_STATUS = true;
                            aktarim.STATUS = 0;
                            aktarimList.Add(aktarim);
                        }
                        i++;

                    }

                    List<EPM_MASTER_PRODUCTION_H> header = new List<EPM_MASTER_PRODUCTION_H>();

                    foreach (var item in aktarimList)
                    {
                        EPM_MASTER_PRODUCTION_H h = header.Find(ob => ob.SEASON == item.SEASON
                        && ob.BRAND == item.BRAND
                        && ob.SUB_BRAND == item.SUB_BRAND
                        && ob.MODEL == item.MODEL
                        && ob.COLOR == item.COLOR
                        && ob.PRODUCTION_TYPE == item.PRODUCTION_TYPE
                        && ob.FABRIC_TYPE == item.FABRIC_TYPE
                        && ob.PRODUCT_GROUP == item.PRODUCT_GROUP
                        && ob.ORDER_TYPE == item.ORDER_TYPE
                        && ob.RECIPE == item.RECIPE
                        && ob.BAND_ID == item.BAND_ID
                        && ob.SHIPMENT_DATE == item.SHIPMENT_DATE
                        && ob.ATTRIBUTE1 == item.ATTRIBUTE1
                        && ob.ATTRIBUTE2 == item.ATTRIBUTE2
                        && ob.ATTRIBUTE3 == item.ATTRIBUTE3
                        && ob.ATTRIBUTE4 == item.ATTRIBUTE4
                        && ob.ATTRIBUTE5 == item.ATTRIBUTE5
                        && ob.ATTRIBUTE6 == item.ATTRIBUTE6
                        && ob.ATTRIBUTE7 == item.ATTRIBUTE7
                        && ob.ATTRIBUTE8 == item.ATTRIBUTE8
                        && ob.ATTRIBUTE9 == item.ATTRIBUTE9
                        && ob.ATTRIBUTE10 == item.ATTRIBUTE10
                        && ob.COLLECTION_TYPE == item.COLLECTION_TYPE
                        && ob.ROYALTY == item.ROYALTY
                        && ob.TEMA == item.TEMA
                        && ob.ANA_TEMA == item.ANA_TEMA
                        && ob.DEADLINE == item.DEADLINE);
                        if (h == null)
                        {
                            h = new EPM_MASTER_PRODUCTION_H()
                            {
                                BRAND = item.BRAND,
                                SUB_BRAND = item.SUB_BRAND,
                                SEASON = item.SEASON,
                                MODEL = item.MODEL,
                                COLOR = item.COLOR,
                                PRODUCT_GROUP = item.PRODUCT_GROUP,
                                PRODUCTION_TYPE = item.PRODUCTION_TYPE,
                                FABRIC_TYPE = item.FABRIC_TYPE,
                                ORDER_TYPE = item.ORDER_TYPE,
                                RECIPE = item.RECIPE,
                                DEADLINE = item.DEADLINE,
                                BAND_ID = item.BAND_ID,
                                SHIPMENT_DATE = item.SHIPMENT_DATE,
                                ATTRIBUTE1 = item.ATTRIBUTE1,
                                ATTRIBUTE2 = item.ATTRIBUTE2,
                                ATTRIBUTE3 = item.ATTRIBUTE3,
                                ATTRIBUTE4 = item.ATTRIBUTE4,
                                ATTRIBUTE5 = item.ATTRIBUTE5,
                                ATTRIBUTE6 = item.ATTRIBUTE6,
                                ATTRIBUTE7 = item.ATTRIBUTE7,
                                ATTRIBUTE8 = item.ATTRIBUTE8,
                                ATTRIBUTE9 = item.ATTRIBUTE9,
                                ATTRIBUTE10 = item.ATTRIBUTE10,
                                COLLECTION_TYPE = item.COLLECTION_TYPE,
                                ROYALTY = item.ROYALTY,
                                ANA_TEMA = item.ANA_TEMA,
                                TEMA = item.TEMA,
                                CREATED_BY = user.USER_CODE,
                                APPROVAL_STATUS = 1,
                                STATUS = 0
                            };
                            h.DETAIL.Add(new EPM_MASTER_PRODUCTION_D()
                            {
                                MARKET = item.MARKET,
                                QUANTITY = item.QUANTITY,
                                PRODUCT_SIZE = item.PRODUCT_SIZE
                            });
                            if(item.PLAN_YEAR!=0 && item.PLAN_WEEK != 0)
                            {
                                h.PLAN.Add(new EPM_PRODUCTION_PLAN()
                                {
                                    MARKET_ID = item.MARKET,
                                    QUANTITY = item.QUANTITY,
                                    WEEK = item.PLAN_WEEK,
                                    YEAR = item.PLAN_YEAR,
                                    CREATED_BY = item.CREATED_BY
                                }); 
                            }
                           
                            header.Add(h);
                        }
                        else
                        {
                            h.DETAIL.Add(new EPM_MASTER_PRODUCTION_D()
                            {
                                MARKET = item.MARKET,
                                QUANTITY = item.QUANTITY,
                                PRODUCT_SIZE = item.PRODUCT_SIZE
                            });
                        }
                    }
                    
                    InsertUretimlistesiAktarim(user, header);
                    obj[0] = true;


                }
            }
            catch (Exception ex)
            {
                request.HttpContext.Response.StatusCode = 400;
                string text = "Merhaba " + user.USER_CODE + ",<br>";
                text += "Üretim listesi aktarımı tamamlanamamıştır.<br>";
                text += "Alınan hata bilgisi :" + ex.Message + "<br>";
                text += "Lütfen sistem yöneticinize başvurunuz";
                obj[0] = false;
                obj[1] = text;
                new MailHelper().SendEMail(user.EMAIL, "Üretim Listesi Aktarımı HK.", "Aktarım Tamamlanamadı\n" + text);
            }

            return obj;
        }

        async void InsertUretimlistesiAktarim(WebLogin user, List<EPM_MASTER_PRODUCTION_H> header)
        {
            await Task.Run(() =>
             {

                 try
                 {
                     for (int a = 0; a < header.Count; a++)
                     {
                         EPM_MASTER_PRODUCTION_H hh = header[a];
                         hh.CREATE_DATE = DateTime.Now;
                         hh = OracleServer.Serialize(hh);

                         for (int b = 0; b < hh.DETAIL.Count; b++)
                         {
                             EPM_MASTER_PRODUCTION_D dd = hh.DETAIL[b];
                             dd.HEADER_ID = hh.ID;
                             OracleServer.Serialize(dd);
                         }
                         for (int p = 0; p < hh.PLAN.Count; p++)
                         {
                             EPM_PRODUCTION_PLAN plan = hh.PLAN[p];
                             plan.HEADER_ID = hh.ID;
                             OracleServer.Serialize(plan);
                         }
                     }
                     string text = "Merhaba " + user.USER_CODE + ",<br>";
                     text += "Üretim listesi aktarımı başarıyla tamamlanmıştır.<br>";
                     text += header.Count + " Adet onaylı üretim girişi oluşturulmuştur. Lütfen oluşturulan girişi üretim planına dahil etmeyi unutmayınız ";
                     new MailHelper().SendEMail(user.EMAIL, "Üretim Listesi Aktarımı HK.", text);
                 }
                 catch (Exception ex)
                 {
                     string text = "Merhaba " + user.USER_CODE + ",<br>";
                     text += "Üretim listesi aktarımı tamamlanamamıştır.<br>";
                     text += "Alınan hata bilgisi :" + ex.Message + "<br>";
                     text += "Lütfen sistem yöneticinize başvurunuz";
                     new MailHelper().SendEMail(user.EMAIL, "Üretim Listesi Aktarımı HK.", text);
                 }

             });

        }

        public Tuple<UretimOnayliListe, List<UretimListesiAktarim>> UretimOnayliListe(Tuple<UretimOnayliListe, List<UretimListesiAktarim>> model)
        {
            return new Tuple<UretimOnayliListe, List<UretimListesiAktarim>>(new UretimOnayliListe(), new List<UretimListesiAktarim>());
        }

        public IEnumerable<EPM_PRODUCTION_BRANDS> GetBrandList(HttpContext context, bool hepsi = true)
        {
            WebLogin user = new CookieHelper().GetUserFromCookie(context);
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32,ParameterDirection.Input);
            dynamicParameters.Add(":P_USER_CODE", user.USER_CODE, OracleMappingType.Varchar2,ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET",dbType: OracleMappingType.RefCursor,direction: ParameterDirection.Output); 
            IEnumerable<EPM_PRODUCTION_BRANDS> brandList = OracleServer.DeserializeListPROC<EPM_PRODUCTION_BRANDS>("FDEIT005.GET_EPM_BRANDS", dynamicParameters);
             
            return brandList;
        }
        public IEnumerable<EPM_PRODUCTION_SUB_BRANDS> GetSubBrandList(HttpContext context, bool hepsi = true)
        {
            WebLogin user = new CookieHelper().GetUserFromCookie(context);
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_USER_CODE", user.USER_CODE, OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<EPM_PRODUCTION_SUB_BRANDS> brandList = OracleServer.DeserializeListPROC<EPM_PRODUCTION_SUB_BRANDS>("FDEIT005.GET_EPM_SUB_BRANDS", dynamicParameters);

            return brandList;
        }
        public IEnumerable<EPM_PRODUCTION_SEASON> GetSeasonList()
        {
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters(); 
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<EPM_PRODUCTION_SEASON> seasonList = OracleServer.DeserializeListPROC<EPM_PRODUCTION_SEASON>("FDEIT005.GET_EPM_SEASONS", dynamicParameters);
            return seasonList; 
        }

        public IEnumerable<EPM_PRODUCTION_MARKET> GetMarketList(bool hepsi = true)
        {
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32, ParameterDirection.Input); 
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<EPM_PRODUCTION_MARKET> marketList = OracleServer.DeserializeListPROC<EPM_PRODUCTION_MARKET>("FDEIT005.GET_EPM_MARKETS", dynamicParameters); 
            return marketList;
        }

        public IEnumerable<EPM_PRODUCTION_ORDER_TYPES> GetOrderList(bool hepsi = true)
        {
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<EPM_PRODUCTION_ORDER_TYPES> orderTypes = OracleServer.DeserializeListPROC<EPM_PRODUCTION_ORDER_TYPES>("FDEIT005.GET_EPM_ORDERTYPES", dynamicParameters);
            return orderTypes; 
        }

        public IEnumerable<EPM_PRODUCTION_FABRIC_TYPES> GetFabricTypes(HttpContext context, bool hepsi = true)
        {

            WebLogin user = new CookieHelper().GetUserFromCookie(context);
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_USER_CODE", user.USER_CODE, OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<EPM_PRODUCTION_FABRIC_TYPES> fabricTypes = OracleServer.DeserializeListPROC<EPM_PRODUCTION_FABRIC_TYPES>("FDEIT005.GET_EPM_FABRICTYPES", dynamicParameters); 
            return fabricTypes; 
        }

        public IEnumerable<EPM_PRODUCTION_TYPES> GetProductionTypes(HttpContext context, bool hepsi = true)
        {
            WebLogin user = new CookieHelper().GetUserFromCookie(context);
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_USER_CODE", user.USER_CODE, OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<EPM_PRODUCTION_TYPES> productionTypes = OracleServer.DeserializeListPROC<EPM_PRODUCTION_TYPES>("FDEIT005.GET_EPM_PRODUCTIONTYPES", dynamicParameters);
            return productionTypes;

             
        }

        public IEnumerable<EPM_PRODUCT_COLLECTION_TYPES> GetCollectionTypes(bool hepsi = true)
        {
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<EPM_PRODUCT_COLLECTION_TYPES> list = OracleServer.DeserializeListPROC<EPM_PRODUCT_COLLECTION_TYPES>("FDEIT005.GET_EPM_COLLECTIONTYPES", dynamicParameters);
            return list; 
        }

        public IEnumerable<EPM_PRODUCTION_BAND_GROUP> GetBandList(bool hepsi = true)
        {
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<EPM_PRODUCTION_BAND_GROUP> list = OracleServer.DeserializeListPROC<EPM_PRODUCTION_BAND_GROUP>("FDEIT005.GET_EPM_BANDGROUPS", dynamicParameters);
            return list;
        }
       
        public IEnumerable<EPM_PRODUCT_GROUP> GetProductGroupList(bool hepsi = true)
        {
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<EPM_PRODUCT_GROUP> list = OracleServer.DeserializeListPROC<EPM_PRODUCT_GROUP>("FDEIT005.GET_EPM_PRODUCTGROUPS", dynamicParameters);
            return list;
        }

        public IEnumerable<EPM_PRODUCTION_RECIPE> GetRecipeList(bool hepsi = true)
        {
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<EPM_PRODUCTION_RECIPE> list = OracleServer.DeserializeListPROC<EPM_PRODUCTION_RECIPE>("FDEIT005.GET_EPM_RECIPES", dynamicParameters);
            return list;
        }

        public IEnumerable<EPM_PRODUCTION_RECIPE> GetRecipeListByType(bool hepsi = true,int TYPE=1)
        {
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_TYPE", Convert.ToInt32(TYPE), OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<EPM_PRODUCTION_RECIPE> list = OracleServer.DeserializeListPROC<EPM_PRODUCTION_RECIPE>("FDEIT005.GET_EPM_RECIPES_BYTYPE", dynamicParameters);
            return list;
        }

        public IEnumerable<ENUMMODEL> GetApprovalStatueList()
        {
            List<ENUMMODEL> enums = ((APPROVAL_STATUSES[])Enum.GetValues(typeof(APPROVAL_STATUSES))).Select(c => new ENUMMODEL() { ID = (int)c, ADI = c.ToString() }).ToList();
            return enums;
        }

        public IEnumerable<MasterList> OnayliUretimListesi(HttpContext context, UretimOnayliListe liste)
        { 
            WebLogin user = new CookieHelper().GetUserFromCookie(context);
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_BRAND", liste.BRAND, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_SEASON", liste.SEASON, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_MODEL", liste.MODEL.ToStringParse(), OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_COLOR", liste.COLOR.ToStringParse(), OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_PRODUCT_GROUP", liste.PRODUCT_GROUP, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_FABRIC_TYPE", liste.FABRIC_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_PRODUCTION_TYPE", liste.PRODUCTION_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECIPE", liste.RECIPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_ORDER_TYPE", liste.ORDER_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_APPROVAL_STATUS", liste.APPROVAL_STATUS, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_BAND_ID", liste.BAND_ID, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_USER_CODE", user.USER_CODE, OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<MasterList> productionTypes = OracleServer.DeserializeListPROC<MasterList>("FDEIT005.GET_EPM_PRODUCTION_MASTER", dynamicParameters);
            return productionTypes; 

        }


        public IEnumerable<VerticalList> UretimListesiDikey(HttpContext context, UretimOnayliListe liste)
        {
            WebLogin user = new CookieHelper().GetUserFromCookie(context);


            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_BRAND", liste.BRAND, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_SEASON", liste.SEASON, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_MODEL", liste.MODEL.ToStringParse(), OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_COLOR", liste.COLOR.ToStringParse(), OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_PRODUCT_GROUP", liste.PRODUCT_GROUP, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_FABRIC_TYPE", liste.FABRIC_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_PRODUCTION_TYPE", liste.PRODUCTION_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECIPE", liste.RECIPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_ORDER_TYPE", liste.ORDER_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_APPROVAL_STATUS", liste.APPROVAL_STATUS, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_BAND_ID", liste.BAND_ID, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_USER_CODE", user.USER_CODE, OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            IEnumerable<VerticalList> productionTypes = OracleServer.DeserializeListPROC<VerticalList>("FDEIT005.GET_EPM_PRODUCTION_VERTICAL_M", dynamicParameters);
            return productionTypes; 
        }

        public IEnumerable<DetailList> OnayliUretimListesiDetail(int ID)
        {
            return OracleServer.DeserializeList<DetailList>(@" SELECT D.*
,H.EXPECTED_COST 
,H.CURRENCY_UNIT 
,0 AS REALESED_COST
FROM FDEIT005.EPM_MASTER_PRODUCTION_D D  
INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H ON H.ID=D.HEADER_ID 
WHERE HEADER_ID=" + ID);
        }

        public object[] UretimOnayliDetailUpdate(HttpContext context, int Key, string Values, ILogService logRepository)
        {
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(Values);
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                LOG_EPM_MASTER_PRODUCTION_D log = new LOG_EPM_MASTER_PRODUCTION_D();
                EPM_MASTER_PRODUCTION_D detail = OracleServer.Deserialize<EPM_MASTER_PRODUCTION_D>(@"SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_D D  WHERE D.ID=" + Key);
                log.OLD_VALUE = Extension.GetPropValue(detail, dic.Keys.ToList()[0].ToString());
                JsonConvert.PopulateObject(Values, detail);
                log.NEW_VALUE = Extension.GetPropValue(detail, dic.Keys.ToList()[0].ToString());
                OracleServer.Serialize<EPM_MASTER_PRODUCTION_D>(detail);
                //detail.SaveLog(context);
                log.CHANGED_COLUMN = dic.Keys.ToList()[0].ToString();
                log.CREATE_DATE = DateTime.Now;
                log.CHANGED_BY = new CookieHelper().GetUserFromCookie(context).USER_CODE;
                log.DETAIL_ID = detail.ID;
                logRepository.SaveDetail(log);
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public object[] UretimOnayliDetailDelete(int Key)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                EPM_MASTER_PRODUCTION_D detail = OracleServer.Deserialize<EPM_MASTER_PRODUCTION_D>(@"SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_D D  WHERE D.ID=" + Key);

                OracleServer.ExecSql("DELETE FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE ID=" + detail.ID);
                OracleServer.ExecSql("DELETE FROM FDEIT005.EPM_PRODUCTION_PLAN WHERE HEADER_ID=" + detail.HEADER_ID + " AND MARKET_ID=" + detail.MARKET);
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public EPM_MASTER_PRODUCTION_D UretimOnayliDetailInsert(string Values)
        {
            EPM_MASTER_PRODUCTION_D detail = new EPM_MASTER_PRODUCTION_D();
            JsonConvert.PopulateObject(Values, detail);
            OracleServer.Serialize<EPM_MASTER_PRODUCTION_D>(detail);
            return detail;

        }

        public object[] UretimOnayliUpdate(HttpContext context, int Key, string Values,ILogService logRepository)
        {
            Dictionary<string, object> dic= JsonConvert.DeserializeObject<Dictionary<string, object>>(Values);
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                LOG_EPM_MASTER_PRODUCTION_H log = new LOG_EPM_MASTER_PRODUCTION_H();
                EPM_MASTER_PRODUCTION_H detail = OracleServer.Deserialize<EPM_MASTER_PRODUCTION_H>(@"SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_H D  WHERE D.ID=" + Key);
                log.OLD_VALUE = Extension.GetPropValue(detail, dic.Keys.ToList()[0].ToString());
                JsonConvert.PopulateObject(Values, detail);
                log.NEW_VALUE = Extension.GetPropValue(detail, dic.Keys.ToList()[0].ToString());
                OracleServer.Serialize<EPM_MASTER_PRODUCTION_H>(detail); 
                log.CHANGED_COLUMN = dic.Keys.ToList()[0].ToString();
                log.CREATE_DATE = DateTime.Now;
                log.CHANGED_BY = new CookieHelper().GetUserFromCookie(context).USER_CODE;
                log.HEADER_ID = detail.ID;
                logRepository.SaveMaster(log);
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public object[] UretimOnayliDelete(int Key)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {

                OracleServer.ExecSql("DELETE FROM FDEIT005.EPM_MASTER_PRODUCTION_H WHERE ID=" + Key);
                OracleServer.ExecSql("DELETE FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE HEADER_ID=" + Key);
                OracleServer.ExecSql("DELETE FROM FDEIT005.EPM_PRODUCTION_PLAN WHERE HEADER_ID=" + Key);
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public EPM_MASTER_PRODUCTION_H UretimOnayliInsert(string Values)
        {
            EPM_MASTER_PRODUCTION_H detail = new EPM_MASTER_PRODUCTION_H();
            JsonConvert.PopulateObject(Values, detail);
            OracleServer.Serialize<EPM_MASTER_PRODUCTION_H>(detail);
            return detail;

        }

        public IEnumerable<EPM_PRODUCTION_CURRENCY_UNITS> GetCurrencyUnits()
        { 
            return OracleServer.DeserializeList<EPM_PRODUCTION_CURRENCY_UNITS>("SELECT ID,CURRENCY_UNIT FROM FDEIT005.EPM_PRODUCTION_CURRENCY_UNITS");
        }

        public IEnumerable<LOG_EPM_MASTER_PRODUCTION_H> OnayliUretimListesiLog(int HEADER_ID, ILogService logRepository)
        {
            return logRepository.GetMaster().ToList().FindAll(ob=>ob.HEADER_ID==HEADER_ID);
        }

        public IEnumerable<LOG_EPM_MASTER_PRODUCTION_D> OnayliUretimListesiLogDetail(int DETAIL_ID, ILogService logRepository)
        {
            return logRepository.GetDetail().ToList().FindAll(ob => ob.DETAIL_ID == DETAIL_ID);
        }

       
    }
}
