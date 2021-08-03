using EPM.Core.Managers;
using EPM.Core.Nesneler;
using EPM.Transver.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Transver
{
    public class Aktarim
    {

        public void AktarKaliteGerceklesen()
        {
            OracleServer.ExecSql("CALL FDEIT005.EGEMEN_GERCEKLESENLER_SIFIRLA()"); 
            List<EPM_PRODUCTION_MARKET> marketList = OracleServer.DeserializeList<EPM_PRODUCTION_MARKET>("SELECT * FROM FDEIT005.EPM_PRODUCTION_MARKET");
            List<EPM_PRODUCTION_ORDER_TYPES> orderTypes = OracleServer.DeserializeList<EPM_PRODUCTION_ORDER_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_ORDER_TYPES");
            List<EPM_PRODUCTION_SEASON> seasonList = OracleServer.DeserializeList<EPM_PRODUCTION_SEASON>("SELECT * FROM FDEIT005.EPM_PRODUCTION_SEASON");
            AktarimHelper helper = new AktarimHelper();
            var gerceklesenler=helper.KaliteGerceklesenler();
            List<EPM_PRODUCTION_EGEMEN> egemenAktarim = new List<EPM_PRODUCTION_EGEMEN>();


            foreach (var item in gerceklesenler)
            {
                if(item.BEDEN_ADI!=null && item.SEZON_ADI!=null && item.MODEL_ADI!=null && item.PAZAR_ADI != null)
                { 
                        EPM_PRODUCTION_EGEMEN aktarilacak = new EPM_PRODUCTION_EGEMEN();
                        aktarilacak.TYPE = 1;
                        aktarilacak.BIRINCI_KALITE = item.BIRINCI_KALITE_SAYISI;
                        aktarilacak.DEMET = item.DEMET_MIKTARI;
                        aktarilacak.IKINCI_KALITE = item.IKINCI_KALITE_SAYISI;
                        aktarilacak.KAYIP = item.KAYIP_MIKTAR;
                        aktarilacak.MODEL = item.MODEL_ADI;
                        aktarilacak.COLOR = item.RENK_ADI;
                        aktarilacak.PRODUCT_SIZE = item.BEDEN_ADI;
                        aktarilacak.SEASON = seasonList.Find(ob => ob.EGEMEN_ADI == item.SEZON_ADI).ID;
                        aktarilacak.SIPARIS_TIPI = orderTypes.Find(ob => ob.ADI == item.SIPARIS_TIPI).ID;
                        aktarilacak.MARKET = marketList.Find(ob => ob.EGEMEN_ADI == item.PAZAR_ADI).ID;
                        aktarilacak.FASON_FIRMA = item.FASON_FIRMA;
                        aktarilacak.KESIM_FOYU_NO = item.KESIM_FOYU_NO;
                        egemenAktarim.Add(aktarilacak); 
                }
               
            }
            OracleServer.BulkInsert(egemenAktarim);
        }
    }
}
