using EPM.Core.FormModels.UretimTakip;
using EPM.Core.Managers;
using EPM.Core.Nesneler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Core.Helpers
{
    public class EgemenDevanlayHelper
    {
        public string BantBitisleriSQL(string REFKESIMFOYLERI, int PO_HEADER_ID)
        {
            EPM_MASTER_PRODUCTION_H master = OracleServer.Deserialize<EPM_MASTER_PRODUCTION_H>(OracleServer.ReadInteger("SELECT HEADER_ID FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS WHERE PO_HEADER_ID=" + PO_HEADER_ID + ""));
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
        ", REFKESIMFOYLERI, master.COLOR);
            return sql;
        }
        public DataTable BANTBITISBILGILERI(string REFKESIMFOYLERI, int PO_HEADER_ID)
        {
            string sql = BantBitisleriSQL(REFKESIMFOYLERI, PO_HEADER_ID);
            DataTable dt = FirebirdServer.QueryFill(sql, (FirebirdServer.FirebirdConnectionDB)FirebirdConnectionDB.DEVANLAY);
            return dt;
        }
        public int BantBitisleriToplam(string REFKESIMFOYLERI, int PO_HEADER_ID)
        {
            string sql = "SELECT SUM(MIKTAR) FROM ("+BantBitisleriSQL(REFKESIMFOYLERI, PO_HEADER_ID) + ") A";
            return FirebirdServer.ReadInteger(sql);
        }

        public string KaliteBitisleriSQL(string REFKESIMFOYLERI, int PO_HEADER_ID)
        {
            EPM_MASTER_PRODUCTION_H master = OracleServer.Deserialize<EPM_MASTER_PRODUCTION_H>(OracleServer.ReadInteger("SELECT HEADER_ID FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS WHERE PO_HEADER_ID=" + PO_HEADER_ID + ""));
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
        ", REFKESIMFOYLERI, master.COLOR);
            return sql;
        }
        public int KaliteBitisleriToplam(string REFKESIMFOYLERI, int PO_HEADER_ID)
        {
            string sql = "SELECT SUM(MIKTAR) FROM (" + KaliteBitisleriSQL(REFKESIMFOYLERI, PO_HEADER_ID) + ") A";
            return FirebirdServer.ReadInteger(sql);
        }
        public List<BantBitisleri> KALITEBITISBILGILERILIST(string REFKESIMFOYLERI, int PO_HEADER_ID)
        {
            string sql = KaliteBitisleriSQL(REFKESIMFOYLERI, PO_HEADER_ID);
            return FirebirdServer.DeserializeList< BantBitisleri>(sql, (FirebirdServer.FirebirdConnectionDB)FirebirdConnectionDB.DEVANLAY); 
        }
        public DataTable KALITEBITISBILGILERI(string REFKESIMFOYLERI, int PO_HEADER_ID)
        {
            string sql = KaliteBitisleriSQL(REFKESIMFOYLERI,PO_HEADER_ID);
            DataTable dt = FirebirdServer.QueryFill(sql, (FirebirdServer.FirebirdConnectionDB)FirebirdConnectionDB.DEVANLAY);
            return dt;
        }

        public string KaliteGerceklesenSQL(string MODEL,string RENK,string BEDEN,string PAZAR,int SEZON)
        {
            string sql = @"
 SELECT BR.ADI AS BRAND_NAME,
       S.ADI AS SEASON_NAME,
       M.ADI AS MARKET_NAME,
       O.ADI AS ORDER_TYPE,
       NVL(EG.FASON_FIRMA,'İÇ ÜRETİM') AS EXTERNAL_COMPANY,
       EG.KESIM_FOYU_NO,
       PG.ADI AS PRODUCT_GROUP,
       FT.ADI AS FABRIC_TYPE,
       CT.ADI AS COLLECTION,
       H.TEMA,
       H.ANA_TEMA,
       H.ROYALTY,
       H.DEADLINE,
       H.MODEL,
       H.COLOR,
       D.PRODUCT_SIZE,
       D.QUANTITY PLANNED,
       EG.BIRINCI_KALITE RELEASED
  FROM FDEIT005.EPM_PRODUCTION_EGEMEN EG
        LEFT JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H ON   0=0  
            AND EG.SEASON = H.SEASON
             AND EG.SIPARIS_TIPI = H.ORDER_TYPE
             AND EG.MODEL = H.MODEL
             AND EG.COLOR = H.COLOR
       LEFT JOIN  FDEIT005.EPM_MASTER_PRODUCTION_D D ON D.HEADER_ID = H.ID AND EG.MARKET = D.MARKET AND EG.PRODUCT_SIZE = D.PRODUCT_SIZE 
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_SEASON S ON S.ID = EG.SEASON
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_MARKET M ON M.ID = EG.MARKET
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_ORDER_TYPES O ON O.ID = EG.SIPARIS_TIPI
       LEFT JOIN  FDEIT005.EPM_PRODUCT_COLLECTION_TYPES CT
          ON CT.ID = H.COLLECTION_TYPE
       LEFT JOIN  FDEIT005.EPM_PRODUCT_GROUP PG ON PG.ID = H.PRODUCT_GROUP
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_FABRIC_TYPES FT ON FT.ID = H.FABRIC_TYPE
       LEFT JOIN  FDEIT005.EPM_PRODUCTION_BRANDS BR ON BR.ID = H.BRAND 
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

        public List<KaliteGerceklesen> KALITEGERCEKLESENLER(string MODEL, string RENK, string BEDEN, string PAZAR, int SEZON)
        {
            string sql = KaliteGerceklesenSQL(MODEL,RENK,BEDEN,PAZAR,SEZON);
            return OracleServer.DeserializeList<KaliteGerceklesen>(sql);
        }
    }
}
