using EPM.Track.Dto.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Track.Service.Helpers
{
    public class EgemenDevanlayHelper
    {
        public string BantBitisleriSQL(string EPM_MASTER_PRODUCTION_H_COLOR, string REFKESIMFOYLERI)
        { 
            string sql = string.Format(@" 
Select IsEmriOperasyonDetay.IsEmriKayitId, IsEmri.IsEmriYil, IsEmri.IsEmriId, IsEmri.PazarId, Pazar.Adi AS PAZARADI, IsEmriOperasyonDetay.IsEmriOperasyonID,
             IsEmriOperasyonDetay.IsEmriOperasyonDetayId, IsEmriOperasyon.OperasyonId, Operasyon.Adi, IsEmriOperasyonDetay.Miktar,
             IsEmri.KesimFoyuKayitId, IsEmri.KesimFoyuDetayId, KesimFoyu.KesimFoyuYil, KesimFoyu.KesimFoyuId, KesimFoyu.ReferansKesimFoyuNo,
             KesimFoyuDetay.ReferansIsEmriNo, KesimFoyu.ModelId, Model.Adi,MODEL.kisaadi AS MODELADI, KesimFoyuDetay.BedenId, KesimFoyuDetay.RenkId, Renk.Adi, Renk.TransferKodu AS RENKADI,
             IsEmriOperasyonDetay.MakinaId, IsEmriOperasyon.StandartSure, IsEmriOperasyonDetay.GirisPersonelId, GP.Adi AS PERSONELADI,
             IsEmriOperasyonDetay.GirisTarihi, IsEmriOperasyonDetay.CikisTarihi, IsEmriOperasyonDetay.IscilikMaliyeti,
             IsEmriOperasyonDetay.AraVermeSuresi,
             KesimFoyu.SezonID, Sezon.Adi, Gp.DepartmanId, Departman.Adi, Operasyon.GenelIEOperasyonGrupId,   KesimFoyu.SiparisTipi,
             Operasyon.OperasyonKisaAdi, IsEmri.DemetNo, Operasyon.SonOperasyonmu, KesimFoyu.Notu, Operasyon.DigerAdi, KesimFoyuDetay.MalzemeBilgisi, KesimFoyu.TerminTarihi,
             KesimFoyu.BaslamaTarihi, KesimFoyuDetay.PlanlananMiktar from IsEmriOperasyonDetay
    INNER join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    INNER join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    INNER join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    INNER join Departman on Departman.DepartmanID = GP.DepartmanId
    INNER join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    INNER join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    INNER join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    INNER join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID   and KesimFoyu.referanskesimfoyuno IN ({0})
    INNER join Model On Model.ModelId = KesimFoyu.ModelId
    INNER join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    INNER Join Pazar on Pazar.PazarId = IsEmri.PazarId
    INNER join Sezon on Sezon.SezonID = KesimFoyu.SezonID 
    where  (IsEmriOperasyonDetay.MakinaId <> 'XXXX')        AND OPERASYON.bantsonoperasyonmu=1 and Renk.TransferKodu='{1}'
        ", REFKESIMFOYLERI, EPM_MASTER_PRODUCTION_H_COLOR);
            return sql;
        }
      

        public string KaliteBitisleriSQL(string EPM_MASTER_PRODUCTION_H_COLOR,string REFKESIMFOYLERI)
        { 
            string sql = string.Format(@" 
Select IsEmriOperasyonDetay.IsEmriKayitId, IsEmri.IsEmriYil, IsEmri.IsEmriId, IsEmri.PazarId, Pazar.Adi AS PAZARADI, IsEmriOperasyonDetay.IsEmriOperasyonID,
             IsEmriOperasyonDetay.IsEmriOperasyonDetayId, IsEmriOperasyon.OperasyonId, Operasyon.Adi, IsEmriOperasyonDetay.Miktar,
             IsEmri.KesimFoyuKayitId, IsEmri.KesimFoyuDetayId, KesimFoyu.KesimFoyuYil, KesimFoyu.KesimFoyuId, KesimFoyu.ReferansKesimFoyuNo,
             KesimFoyuDetay.ReferansIsEmriNo, KesimFoyu.ModelId, Model.Adi,MODEL.kisaadi AS MODELADI, KesimFoyuDetay.BedenId, KesimFoyuDetay.RenkId, Renk.Adi, Renk.TransferKodu AS RENKADI,
             IsEmriOperasyonDetay.MakinaId, IsEmriOperasyon.StandartSure, IsEmriOperasyonDetay.GirisPersonelId, GP.Adi AS PERSONELADI,
             IsEmriOperasyonDetay.GirisTarihi, IsEmriOperasyonDetay.CikisTarihi, IsEmriOperasyonDetay.IscilikMaliyeti,
             IsEmriOperasyonDetay.AraVermeSuresi,
             KesimFoyu.SezonID, Sezon.Adi, Gp.DepartmanId, Departman.Adi, Operasyon.GenelIEOperasyonGrupId,   KesimFoyu.SiparisTipi,
             Operasyon.OperasyonKisaAdi, IsEmri.DemetNo, Operasyon.SonOperasyonmu, KesimFoyu.Notu, Operasyon.DigerAdi, KesimFoyuDetay.MalzemeBilgisi, KesimFoyu.TerminTarihi,
             KesimFoyu.BaslamaTarihi, KesimFoyuDetay.PlanlananMiktar from IsEmriOperasyonDetay
    INNER join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    INNER join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    INNER join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    INNER join Departman on Departman.DepartmanID = GP.DepartmanId
    INNER join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    INNER join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    INNER join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    INNER join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID   and KesimFoyu.referanskesimfoyuno IN ({0})
    INNER join Model On Model.ModelId = KesimFoyu.ModelId
    INNER join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    INNER Join Pazar on Pazar.PazarId = IsEmri.PazarId
    INNER join Sezon on Sezon.SezonID = KesimFoyu.SezonID 
    where  (IsEmriOperasyonDetay.MakinaId <> 'XXXX')      AND OPERASYON.OPERASYONID IN (141,69,39,664) and Renk.TransferKodu='{1}'
        ", REFKESIMFOYLERI, EPM_MASTER_PRODUCTION_H_COLOR);
            return sql;
        }

        public string KaliteGerceklesenSQL(string MODEL, string RENK, string BEDEN, string PAZAR, int SEZON)
        {
            string sql = @"
 SELECT BR.ADI AS BRAND_NAME,
       BRS.ADI AS SUB_BRAND_NAME,
       S.ADI AS SEASON_NAME,
       M.ADI AS MARKET_NAME,
       O.ADI AS ORDER_TYPE,  
       PG.ADI AS PRODUCT_GROUP,
       FT.ADI AS FABRIC_TYPE,
       CT.ADI AS COLLECTION,
       R.ADI AS RECIPE,
       PT.ADI AS PRODUCTION_TYPE,
       H.TEMA,
       H.ANA_TEMA,
       H.ROYALTY,
       H.DEADLINE,
       H.MODEL,
       H.COLOR,
       D.PRODUCT_SIZE,
       D.QUANTITY PLANNED, 
       (SELECT SUM(BIRINCI_KALITE) FROM FDEIT005.EPM_PRODUCTION_EGEMEN EPG WHERE EPG.MARKET=D.MARKET AND EPG.PRODUCT_SIZE=D.PRODUCT_SIZE AND EPG.SEASON = H.SEASON
             AND EPG.SIPARIS_TIPI = H.ORDER_TYPE
             AND EPG.MODEL = H.MODEL
             AND EPG.COLOR = H.COLOR )RELEASED
  FROM FDEIT005.EPM_MASTER_PRODUCTION_H H  
       LEFT JOIN  FDEIT005.EPM_MASTER_PRODUCTION_D D ON D.HEADER_ID = H.ID  
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_SEASON S ON S.ID = H.SEASON
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_MARKET M ON M.ID = D.MARKET
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_RECIPE R ON R.ID=H.RECIPE
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_TYPES PT ON PT.ID=H.PRODUCTION_TYPE
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_ORDER_TYPES O ON O.ID = H.ORDER_TYPE 
       LEFT JOIN  FDEIT005.EPM_PRODUCT_COLLECTION_TYPES CT
          ON CT.ID = H.COLLECTION_TYPE
       LEFT JOIN  FDEIT005.EPM_PRODUCT_GROUP PG ON PG.ID = H.PRODUCT_GROUP
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_FABRIC_TYPES FT ON FT.ID = H.FABRIC_TYPE
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_BRANDS BR ON BR.ID = H.BRAND
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_SUB_BRANDS BRS ON BRS.ID = H.SUB_BRAND  
 WHERE 0=0
  ";
            if (MODEL.ToStringParse() != "")
                sql += " AND H.MODEL='" + MODEL + "'";

            if (RENK.ToStringParse() != "")
                sql += " AND H.COLOR='" + RENK + "'";

            if (BEDEN.ToStringParse() != "")
                sql += " AND D.PRODUCT_SIZE='" + BEDEN + "'";

            if (PAZAR.ToStringParse() != "")
                sql += " AND M.ADI='" + PAZAR + "'";

            if (SEZON.ToStringParse() != "")
                sql += " AND S.ID=" + SEZON + "";


            return sql;

        }

        public string KaliteBitisleriToplam(string EPM_MASTER_PRODUCTION_H_COLOR,string REFKESIMFOYLERI)
        {
            return "SELECT SUM(MIKTAR) FROM (" + KaliteBitisleriSQL(EPM_MASTER_PRODUCTION_H_COLOR,REFKESIMFOYLERI) + ") A"; 
        }

        public string BantBitisleriToplam(string EPM_MASTER_PRODUCTION_H_COLOR, string REFKESIMFOYLERI)
        {
            return "SELECT SUM(MIKTAR) FROM (" + BantBitisleriSQL(EPM_MASTER_PRODUCTION_H_COLOR,REFKESIMFOYLERI) + ") A"; 
        }

    }
}
