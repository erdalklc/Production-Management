using EPM.Core.Managers;
using EPM.Transver.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Transver.Helpers
{
    public class AktarimHelper
    {
        public AktarimHelper()
        {
           // OracleServer.ExecSql("TRUNCATE TABLE FDEIT005.EPM_PRODUCTION_EGEMEN");
        }

        public List<LacosteUretim> KaliteGerceklesenler()
        {
            string sql = @"
Select  Sezon.adi AS SEZON_ADI
,   MODEL.kisaadi AS MODEL_ADI
,   Renk.TransferKodu AS RENK_ADI
,   KesimFoyuDetay.BedenId AS BEDEN_ADI
,   Pazar.Adi AS PAZAR_ADI
,   Firma.Adi AS FASON_FIRMA  
,   CASE WHEN kesimfoyu.SIPARISTIPI   IS NULL THEN 'FIRST' ELSE kesimfoyu.siparistipi END AS SIPARIS_TIPI
,   KesimFoyu.REFERANSKESIMFOYUNO   AS KESIM_FOYU_NO
,   SUM(IsEmri.IsEmriMiktari) AS DEMET_MIKTARI
,   SUM(IsEmri.KayipMiktar) AS KAYIP_MIKTAR
,   SUM(IsEmri.IsEmriMiktari-IsEmri.IkinciKaliteMiktari) AS BIRINCI_KALITE_SAYISI
,   SUM(IsEmri.IkinciKaliteMiktari) AS IKINCI_KALITE_SAYISI
              from KaliteTeslimFisi
  left join KaliteTeslimFisiDetay On KaliteTeslimFisiDetay.KaliteTeslimFisiID = KaliteTeslimFisi.KaliteTeslimFisiID
  left join IsEmri On IsEmri.IsEmriKayitID = KaliteTeslimFisiDetay.IsEmriKayitID
  left join KesimFoyuDetay On KesimFoyuDetay.KesimFoyuDetayId = IsEmri.KesimFoyuDetayId
  left join KesimFoyu On KesimFoyu.KesimFoyuKayitId = KesimFoyuDetay.KesimFoyuKayitId
  left join Model On Model.ModelId = KesimFoyu.ModelId
  left join Departman On Departman.Departmanid = Model.DepartmanID
  left join Unite On Unite.UniteID= Departman.UniteID
  left join Renk On Renk.RenkId = KesimFoyuDetay.RenkId         
  LEFT JOIN SEZON ON SEZON.sezonid=KesimFoyu.sezonid
          LEFT JOIN FIRMA ON FIRMA.FIRMAID=KesimFoyu.FirmaId
  Left Join Kullanici KTK on KTK.KullaniciId = IsEmri.KaliteTeslimKullaniciId
  Left Join IslemTipi on IslemTipi.IslemTipiId = IsEmri.IslemTipiId
  Left Join Pazar on Pazar.PazarId = IsEmri.PazarId  where KesimFoyu.sezonid>35
  group by Sezon.adi
,   MODEL.kisaadi
,   Renk.TransferKodu
,   KesimFoyuDetay.BedenId
,   Pazar.Adi
,   Firma.Adi
,   kesimfoyu.SIPARISTIPI
,   KesimFoyu.referanskesimfoyuno";

            return FirebirdServer.DeserializeList<LacosteUretim>(sql);
        }
    }
}
