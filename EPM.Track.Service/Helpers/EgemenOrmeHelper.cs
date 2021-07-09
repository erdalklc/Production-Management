using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Track.Service.Helpers
{
    public class EgemenOrmeHelper
    {
        public string ORMEALINANSIPARISSATIRTAKIP(string SIPARISNO, string TAKIPNO)
        {
            string sql = string.Format(@"SELECT                
                        UrunTipi.Adi URUN_TIPI_ADI
                        , Urun.UrunId
                        , Urun.Adi as URUN_ADI
                        , Urun.RenkId
                        , Renk.Adi RENK_ADI
                        , StokHareket.Tarih
                        , StokHareket.HareketTipi
                        , StokHareket.HamIade
                        , StokHareket.tamirsevki
                        , UPPER(StokHareket.BirimId) BIRIMID
                        , StokHareket.Miktar AS MIKTAR
                        , (CASE WHEN   StokHareket.HareketTipi=0 THEN 'Devir..'
                                  WHEN    StokHareket.HareketTipi=1 THEN 'Alış'
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=0 and StokHareket.tamirsevki=0 THEN 'Satış'
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=0 and StokHareket.tamirsevki=1 THEN 'Satış (Tamir)'
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=1  THEN 'Fason Hammadde İade'
                                  WHEN    StokHareket.HareketTipi=3 THEN 'Alış İade'
                                  WHEN    StokHareket.HareketTipi=4 THEN 'Satış İade'
                                  WHEN    StokHareket.HareketTipi=5 THEN 'Fason İşlenmek Üzere Giriş'
                                  WHEN    StokHareket.HareketTipi=6 THEN 'Fason Üretime Çıkış'
                                  WHEN    StokHareket.HareketTipi=7 THEN 'Üretime Çıkış'
                                  WHEN    StokHareket.HareketTipi=8 and StokHareket.HamIade=0 THEN 'Fason Üretim'
                                  WHEN    StokHareket.HareketTipi=8 and StokHareket.HamIade=1 THEN 'Fason Üretim İade'
                                  WHEN    StokHareket.HareketTipi=9 and StokHareket.HamIade=0 THEN 'Üretim'
                                  WHEN    StokHareket.HareketTipi=9 and StokHareket.HamIade=1 THEN 'Üretim İade'
                                  WHEN    StokHareket.HareketTipi=10 THEN 'Sakat'
                                  WHEN    StokHareket.HareketTipi=11 THEN 'Gelen Reklamasyon'
                                  WHEN    StokHareket.HareketTipi=12 THEN 'Giden Reklamasyon'
                                  WHEN    StokHareket.HareketTipi=15 THEN 'Depo Transfer'
                                  WHEN    StokHareket.HareketTipi=13 THEN 'Stok Kaydırma (Çıkış)'
                                  WHEN    StokHareket.HareketTipi=14 THEN 'Stok Kaydırma (Giriş)'
                                  ELSE 'TANIMSIZ' END || CASE WHEN StokHareket.Tamir=1 THEN 'TAMİR' ELSE '' END   ) HAREKET_ADI                              
                        , (CASE WHEN   StokHareket.HareketTipi=0 THEN 1
                                  WHEN    StokHareket.HareketTipi=1 THEN 1
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=0 and StokHareket.tamirsevki=0 THEN 0
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=0 and StokHareket.tamirsevki=1 THEN 0
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=1  THEN 'Fason Hammadde İade'
                                  WHEN    StokHareket.HareketTipi=3 THEN 0
                                  WHEN    StokHareket.HareketTipi=4 THEN 1
                                  WHEN    StokHareket.HareketTipi=5 THEN 1
                                  WHEN    StokHareket.HareketTipi=6 THEN 0
                                  WHEN    StokHareket.HareketTipi=7 THEN 0
                                  WHEN    StokHareket.HareketTipi=8 and StokHareket.HamIade=0 THEN 1
                                  WHEN    StokHareket.HareketTipi=8 and StokHareket.HamIade=1 THEN 0
                                  WHEN    StokHareket.HareketTipi=9 and StokHareket.HamIade=0 THEN 1
                                  WHEN    StokHareket.HareketTipi=9 and StokHareket.HamIade=1 THEN 0
                                  WHEN    StokHareket.HareketTipi=10 THEN 0
                                  WHEN    StokHareket.HareketTipi=11 THEN 1
                                  WHEN    StokHareket.HareketTipi=12 THEN 0
                                  WHEN    StokHareket.HareketTipi=15 THEN 1
                                  WHEN    StokHareket.HareketTipi=13 THEN 0
                                  WHEN    StokHareket.HareketTipi=14 THEN 1
                                  ELSE 0 END     ) HAREKETGIRISCIKIS
                        ,CASE WHEN StokHareket.Tamir=1 THEN 'TAMİR' ELSE '' END
                        , UretimIslemTipi.Adi AS ISLEM_TIPI_ADI
                        , StokHareket.FirmaId
                        , Firma.Adi FIRMA_ADI
                        , IK.Adi KULLANICI_ADI
                        , UretimIslemTipi.Adi ISLEM_ADI
                        FROM StokHareket
                  left join Urun on Urun.UrunKayitId = StokHareket.UrunKayitId
                  left join UrunTipi on UrunTipi.UrunTipiId = Urun.UrunTipiId
                  left join Renk on Renk.RenkId = Urun.RenkId 
                  left join Firma on Firma.FirmaId = StokHareket.FirmaId  
                  left join Kullanici IK on IK.KullaniciId  = StokHareket.InsertKullaniciId  
                  left join UretimIslemTipi on UretimIslemTipi.UretimIslemTipiId  = StokHareket.UretimIslemTipiId 
                  where StokHareket.AlinanSiparisKayitId IN  (Select AlinanSiparisKayitId from AlinanSiparis where  AlinanSiparis.musterireferansno = '{0}')
                          --and stokhareket.urunkayitid IN (select  AlinanSiparisDetay.urunkayitid from AlinanSiparisDetay where AlinanSiparisDetay.musterikaliteadi='{1}')
and Urun.urunid IN (SELECT urunid From Urun WHERE UrunKayitId IN (SELECT  AlinanSiparisDetay.urunkayitid from AlinanSiparisDetay where AlinanSiparisDetay.musterikaliteadi='{1}'))
                  order by StokHareket.Tarih, StokHareket.StokHareketId", SIPARISNO, TAKIPNO);
            return sql;
        }

        public static void ALINANSIPARISGIRILENLER()
        {
            string sql = @"SELECT DISTINCT SP.MUSTERIREFERANSNO ,SPD.MUSTERIKALITEADI FROM AlinanSiparis SP 
INNER JOIN ALINANSIPARISDETAY SPD ON SP.ALINANSIPARISKAYITID =SPD.ALINANSIPARISKAYITID 
WHERE SP.musterireferansno IS NOT NULL AND SP.musterireferansno <>'' AND FIRMAID ='M001'
AND SPD.musterikaliteadi IS NOT NULL AND SPD.musterikaliteadi<>''";
        }
        public string ORMEALINANSIPARISTAKIP(string SIPARISNOLAR)
        {
            string sql = string.Format(@"SELECT                
                        UrunTipi.Adi URUNTIPIADI
                        , Urun.UrunId
                        , Urun.Adi as URUNADI
                        , Urun.RenkId
                        , Renk.Adi RENKADI
                        , StokHareket.Tarih
                        , StokHareket.HareketTipi
                        , StokHareket.HamIade
                        , StokHareket.tamirsevki
                        , UPPER(StokHareket.BirimId) BIRIMID
                        , StokHareket.Miktar AS MIKTAR
                        , (CASE WHEN   StokHareket.HareketTipi=0 THEN 'Devir..'
                                  WHEN    StokHareket.HareketTipi=1 THEN 'Alış'
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=0 and StokHareket.tamirsevki=0 THEN 'Satış'
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=0 and StokHareket.tamirsevki=1 THEN 'Satış (Tamir)'
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=1  THEN 'Fason Hammadde İade'
                                  WHEN    StokHareket.HareketTipi=3 THEN 'Alış İade'
                                  WHEN    StokHareket.HareketTipi=4 THEN 'Satış İade'
                                  WHEN    StokHareket.HareketTipi=5 THEN 'Fason İşlenmek Üzere Giriş'
                                  WHEN    StokHareket.HareketTipi=6 THEN 'Fason Üretime Çıkış'
                                  WHEN    StokHareket.HareketTipi=7 THEN 'Üretime Çıkış'
                                  WHEN    StokHareket.HareketTipi=8 and StokHareket.HamIade=0 THEN 'Fason Üretim'
                                  WHEN    StokHareket.HareketTipi=8 and StokHareket.HamIade=1 THEN 'Fason Üretim İade'
                                  WHEN    StokHareket.HareketTipi=9 and StokHareket.HamIade=0 THEN 'Üretim'
                                  WHEN    StokHareket.HareketTipi=9 and StokHareket.HamIade=1 THEN 'Üretim İade'
                                  WHEN    StokHareket.HareketTipi=10 THEN 'Sakat'
                                  WHEN    StokHareket.HareketTipi=11 THEN 'Gelen Reklamasyon'
                                  WHEN    StokHareket.HareketTipi=12 THEN 'Giden Reklamasyon'
                                  WHEN    StokHareket.HareketTipi=15 THEN 'Depo Transfer'
                                  WHEN    StokHareket.HareketTipi=13 THEN 'Stok Kaydırma (Çıkış)'
                                  WHEN    StokHareket.HareketTipi=14 THEN 'Stok Kaydırma (Giriş)'
                                  ELSE 'TANIMSIZ' END || CASE WHEN StokHareket.Tamir=1 THEN 'TAMİR' ELSE '' END   ) HAREKETADI                              
                        , (CASE WHEN   StokHareket.HareketTipi=0 THEN 1
                                  WHEN    StokHareket.HareketTipi=1 THEN 1
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=0 and StokHareket.tamirsevki=0 THEN 0
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=0 and StokHareket.tamirsevki=1 THEN 0
                                  WHEN    StokHareket.HareketTipi=2 and StokHareket.HamIade=1  THEN 'Fason Hammadde İade'
                                  WHEN    StokHareket.HareketTipi=3 THEN 0
                                  WHEN    StokHareket.HareketTipi=4 THEN 1
                                  WHEN    StokHareket.HareketTipi=5 THEN 1
                                  WHEN    StokHareket.HareketTipi=6 THEN 0
                                  WHEN    StokHareket.HareketTipi=7 THEN 0
                                  WHEN    StokHareket.HareketTipi=8 and StokHareket.HamIade=0 THEN 1
                                  WHEN    StokHareket.HareketTipi=8 and StokHareket.HamIade=1 THEN 0
                                  WHEN    StokHareket.HareketTipi=9 and StokHareket.HamIade=0 THEN 1
                                  WHEN    StokHareket.HareketTipi=9 and StokHareket.HamIade=1 THEN 0
                                  WHEN    StokHareket.HareketTipi=10 THEN 0
                                  WHEN    StokHareket.HareketTipi=11 THEN 1
                                  WHEN    StokHareket.HareketTipi=12 THEN 0
                                  WHEN    StokHareket.HareketTipi=15 THEN 1
                                  WHEN    StokHareket.HareketTipi=13 THEN 0
                                  WHEN    StokHareket.HareketTipi=14 THEN 1
                                  ELSE 0 END     ) HAREKETGIRISCIKIS
                        ,CASE WHEN StokHareket.Tamir=1 THEN 'TAMİR' ELSE '' END
                        , UretimIslemTipi.Adi AS ISLEMTIPIADI
                        , StokHareket.FirmaId
                        , Firma.Adi FIRMADI
                        , IK.Adi KULLANICIADI
                        , UretimIslemTipi.Adi ISLEMADI
                        FROM StokHareket
                  left join Urun on Urun.UrunKayitId = StokHareket.UrunKayitId
                  left join UrunTipi on UrunTipi.UrunTipiId = Urun.UrunTipiId
                  left join Renk on Renk.RenkId = Urun.RenkId 
                  left join Firma on Firma.FirmaId = StokHareket.FirmaId  
                  left join Kullanici IK on IK.KullaniciId  = StokHareket.InsertKullaniciId  
                  left join UretimIslemTipi on UretimIslemTipi.UretimIslemTipiId  = StokHareket.UretimIslemTipiId 
                  where StokHareket.AlinanSiparisKayitId IN  (Select AlinanSiparisKayitId from AlinanSiparis where  AlinanSiparis.musterireferansno IN ({0})) 
                  order by StokHareket.Tarih, StokHareket.StokHareketId", SIPARISNOLAR);
            return sql;
        }
    }
}
