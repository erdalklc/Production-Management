using EPM.Dto.Base;
using EPM.Dto.Models;
using EPM.Repository.Base;
using EPM.Tools.Extensions;
using EPM.Tools.Helpers;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Service.Base
{
    public class UretimService : IUretimService
    {
        private readonly IMenuService _menuService;
        private readonly IMailService _mailService;
        private readonly IEPMRepository _epmRepository;
        public UretimService(IEPMRepository epmRepository, IMenuService menuService, IMailService mailService)
        {
            _epmRepository = epmRepository; _menuService = menuService;_mailService = mailService;
        }

        private object[] CheckForErrors(HttpRequest request
            , List<EPM_PRODUCTION_BRANDS> brandList
            , List<EPM_PRODUCTION_SUB_BRANDS> subBrandList
            , List<EPM_PRODUCTION_FABRIC_TYPES> fabricTypes
            , List<EPM_PRODUCTION_MARKET> marketList
            , List<EPM_PRODUCTION_ORDER_TYPES> orderTypes
            , List<EPM_PRODUCTION_SEASON> seasonList
            , List<EPM_PRODUCTION_TYPES> productionTypes
            , List<EPM_PRODUCT_GROUP> productGroups
            , List<EPM_PRODUCTION_RECIPE> productionRecipe
            , List<EPM_PRODUCTION_BAND_GROUP> productionBandGroup, List<EPM_PRODUCTION_SEASON_WEEKS> weeks)
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
                        //if (reader.GetValue(10).ToStringParse() != "DEADLINE")
                        //    err[1] += "10.Kolon DEADLINE olmalıdır<br>";
                        //if (reader.GetValue(11).ToStringParse() != "SHIPMENT_DATE")
                        //    err[1] += "11.Kolon SHIPMENT_DATE olmalıdır<br>";
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
                            err[1] += (i + 1) + " Satırındaki " + reader.GetValue(2).ToStringParse() + " Bilgisi SEASON Alanında Bulunamadı<br>";
                        }
                        if (brandList.Count(ob => ob.ADI == reader.GetValue(0).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] += (i + 1) + " Satırındaki " + reader.GetValue(0).ToStringParse() + " Bilgisi BRAND Alanında Bulunamadı<br>";
                        }
                        if (subBrandList.Count(ob => ob.ADI == reader.GetValue(1).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] += (i + 1) + " Satırındaki " + reader.GetValue(1).ToStringParse() + " Bilgisi SUB_BRAND Alanında Bulunamadı<br>";
                        }
                        if (productGroups.Count(ob => ob.ADI == reader.GetValue(4).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] += (i + 1) + " Satırındaki " + reader.GetValue(4).ToStringParse() + " Bilgisi PRODUCT_GROUP Alanında Bulunamadı<br>";
                        }
                        if (fabricTypes.Count(ob => ob.ADI == reader.GetValue(18).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] += (i + 1) + " Satırındaki " + reader.GetValue(18).ToStringParse() + " Bilgisi FABRIC_TYPE Alanında Bulunamadı<br>";
                        }
                        if (productionTypes.Count(ob => ob.ADI == reader.GetValue(16).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] += (i + 1) + " Satırındaki " + reader.GetValue(16).ToStringParse() + " Bilgisi PRODUCTION_TYPE Alanında Bulunamadı<br>";
                        }
                        if (orderTypes.Count(ob => ob.ADI == reader.GetValue(3).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] += (i + 1) + " Satırındaki " + reader.GetValue(3).ToStringParse() + " Bilgisi ORDER_TYPE Alanında Bulunamadı<br>";
                        }
                        if (productionRecipe.Count(ob => ob.ADI == reader.GetValue(17).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] += (i + 1) + " Satırındaki " + reader.GetValue(17).ToStringParse() + " Bilgisi RECIPE Alanında Bulunamadı<br>";
                        }
                        if (marketList.Count(ob => ob.ADI == reader.GetValue(9).ToStringParse()) == 0)
                        {
                            err[0] = false;
                            err[1] += (i + 1) + " Satırındaki " + reader.GetValue(9).ToStringParse() + " Bilgisi MARKET Alanında Bulunamadı<br>";
                        }
                        if((bool)err[0])
                        {
                            if(reader.GetValue(30).IntParse()!=0 && reader.GetValue(31).IntParse() != 0)
                            {
                                EPM_PRODUCTION_SEASON_WEEKS week = weeks.Find(ob => ob.SEASON_ID == seasonList.Find(ob => ob.ADI == reader.GetValue(2).ToStringParse()).ID);
                                if (week != null)
                                {
                                    if (week.START_YEAR <= reader.GetValue(30).IntParse() && week.END_YEAR >= reader.GetValue(30).IntParse())
                                    {

                                        if (week.START_YEAR == reader.GetValue(30).IntParse())
                                        {
                                            if (!(week.START_WEEK <= reader.GetValue(31).IntParse()))
                                            {
                                                err[0] = false;
                                                err[1] += (i + 1) + " Satırında girilen Plan haftası Sezon sınırlarında değil<br>";
                                            }
                                        }

                                        if (week.END_YEAR == reader.GetValue(30).IntParse())
                                        {
                                            if (!(week.END_WEEK >= reader.GetValue(31).IntParse()))
                                            {
                                                err[0] = false;
                                                err[1] += (i + 1) + " Satırında girilen Plan haftası Sezon sınırlarında değil<br>";
                                            }

                                        }
                                    }
                                    else
                                    {
                                        err[0] = false;
                                        err[1] += (i + 1) + " Satırında girilen Plan Yılı Sezon sınırlarında değil<br>";
                                    }
                                }
                                else
                                {

                                    err[0] = false;
                                    err[1] += (i + 1) + " Satırında Sezon bilgisine ait hafta-yıl bilgisi bulunamadı<br>";
                                }
                            }
                            
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
            WebLogin user = new CookieHelper().GetObjectFromCookie<WebLogin>(request.HttpContext, "USER");
            var OnayliBelge = _menuService.OnayliKullanici(request.HttpContext);
            try
            {
                var myFile = request.Form.Files["myFile"];
                List<UretimListesiAktarim> aktarimList = new List<UretimListesiAktarim>();
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                List<EPM_PRODUCTION_BRANDS> brandList = _epmRepository.DeserializeList<EPM_PRODUCTION_BRANDS>("SELECT * FROM FDEIT005.EPM_PRODUCTION_BRANDS");
                List<EPM_PRODUCTION_SUB_BRANDS> subBrandList = _epmRepository.DeserializeList<EPM_PRODUCTION_SUB_BRANDS>("SELECT * FROM FDEIT005.EPM_PRODUCTION_SUB_BRANDS");
                List<EPM_PRODUCTION_FABRIC_TYPES> fabricTypes = _epmRepository.DeserializeList<EPM_PRODUCTION_FABRIC_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_FABRIC_TYPES");
                List<EPM_PRODUCTION_MARKET> marketList = _epmRepository.DeserializeList<EPM_PRODUCTION_MARKET>("SELECT * FROM FDEIT005.EPM_PRODUCTION_MARKET");
                List<EPM_PRODUCTION_ORDER_TYPES> orderTypes = _epmRepository.DeserializeList<EPM_PRODUCTION_ORDER_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_ORDER_TYPES");
                List<EPM_PRODUCTION_SEASON> seasonList = _epmRepository.DeserializeList<EPM_PRODUCTION_SEASON>("SELECT * FROM FDEIT005.EPM_PRODUCTION_SEASON");
                List<EPM_PRODUCTION_TYPES> productionTypes = _epmRepository.DeserializeList<EPM_PRODUCTION_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_TYPES");
                List<EPM_PRODUCT_GROUP> productGroups = _epmRepository.DeserializeList<EPM_PRODUCT_GROUP>("SELECT * FROM FDEIT005.EPM_PRODUCT_GROUP");
                List<EPM_PRODUCTION_RECIPE> productionRecipe = _epmRepository.DeserializeList<EPM_PRODUCTION_RECIPE>("SELECT * FROM FDEIT005.EPM_PRODUCTION_RECIPE");
                List<EPM_PRODUCTION_BAND_GROUP> productionBandGroup = _epmRepository.DeserializeList<EPM_PRODUCTION_BAND_GROUP>("SELECT * FROM FDEIT005.EPM_PRODUCTION_BAND_GROUP");
                List<EPM_PRODUCTION_SEASON_WEEKS> weeks = _epmRepository.DeserializeList<EPM_PRODUCTION_SEASON_WEEKS>("SELECT * FROM FDEIT005.EPM_PRODUCTION_SEASON_WEEKS");

                object[] kontrol = CheckForErrors(request, brandList, subBrandList, fabricTypes, marketList, orderTypes, seasonList, productionTypes, productGroups, productionRecipe, productionBandGroup, weeks);

                if (!(bool)kontrol[0])
                {
                    obj[0] = false;
                    obj[1] = "Aktarım Tamamlanamadı<br>"+kontrol[1];  
                    request.HttpContext.Response.StatusCode = 400;
                    _mailService.SendEMail(user.EMAIL, "Üretim Listesi Aktarımı HK.","Aktarım Tamamlanamadı\n" +obj[1].ToString());
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
                            aktarim.DEADLINE = reader.GetValue(10).DatetimeParse().Date;
                            aktarim.SHIPMENT_DATE = reader.GetValue(11).DatetimeParse().Date;
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
                            if (OnayliBelge)
                                aktarim.APPROVAL_STATUS = true;
                            else aktarim.APPROVAL_STATUS = false;
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
                            if (item.PLAN_YEAR != 0 && item.PLAN_WEEK != 0)
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
                _mailService.SendEMail(user.EMAIL, "Üretim Listesi Aktarımı HK.", "Aktarım Tamamlanamadı\n" + text);
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
                         hh = _epmRepository.Serialize(hh);

                         for (int b = 0; b < hh.DETAIL.Count; b++)
                         {
                             EPM_MASTER_PRODUCTION_D dd = hh.DETAIL[b];
                             dd.HEADER_ID = hh.ID;
                             _epmRepository.Serialize(dd);
                         }
                         hh.PLAN = (from val in hh.PLAN
                                   group val by new { val.ID, val.HEADER_ID, val.MARKET_ID, val.WEEK, val.YEAR, val.CREATED_BY } into grouped
                                   select new EPM_PRODUCTION_PLAN() { ID = grouped.Key.ID, HEADER_ID =grouped.Key.HEADER_ID, MARKET_ID = grouped.Key.MARKET_ID, WEEK = grouped.Key.WEEK, YEAR = grouped.Key.YEAR, CREATED_BY = grouped.Key.CREATED_BY, QUANTITY = grouped.Sum(o=>o.QUANTITY) }).ToList();
                         for (int p = 0; p < hh.PLAN.Count; p++)
                         {
                             EPM_PRODUCTION_PLAN plan = hh.PLAN[p];
                             plan.HEADER_ID = hh.ID;
                             _epmRepository.Serialize(plan);
                         }
                     }
                     string text = "Merhaba " + user.USER_CODE + ",<br>";
                     text += "Üretim listesi aktarımı başarıyla tamamlanmıştır.<br>";
                     text += header.Count + " Adet onaylı üretim girişi oluşturulmuştur. Lütfen oluşturulan girişi üretim planına dahil etmeyi unutmayınız ";
                     _mailService.SendEMail(user.EMAIL, "Üretim Listesi Aktarımı HK.", text);
                 }
                 catch (Exception ex)
                 {
                     string text = "Merhaba " + user.USER_CODE + ",<br>";
                     text += "Üretim listesi aktarımı tamamlanamamıştır.<br>";
                     text += "Alınan hata bilgisi :" + ex.Message + "<br>";
                     text += "Lütfen sistem yöneticinize başvurunuz";
                     _mailService.SendEMail(user.EMAIL, "Üretim Listesi Aktarımı HK.", text);
                 }

             });

        }

     

       
    }
}
