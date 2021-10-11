using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Monitoring.Service.Helpers
{
    public class EgemenDevanlayHelper
    {
        public string BantBitisleriByDate(DateTime BasTarih,DateTime BitTarih,string SEASON,string MODEL,string RENK)
        {
            string sql = string.Format(@"SELECT SUM(Miktar) FROM (Select  IsEmriOperasyonDetay.Miktar from IsEmriOperasyonDetay
    INNER join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    INNER join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    INNER join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    INNER join Departman on Departman.DepartmanID = GP.DepartmanId
    INNER join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    INNER join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    INNER join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    INNER join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID   
    INNER join Model On Model.ModelId = KesimFoyu.ModelId
    INNER join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    INNER Join Pazar on Pazar.PazarId = IsEmri.PazarId
    INNER join Sezon on Sezon.SezonID = KesimFoyu.SezonID 
    where  (IsEmriOperasyonDetay.MakinaId <> 'XXXX')        AND OPERASYON.bantsonoperasyonmu=1 AND Sezon.Adi='{0}' AND IsEmriOperasyonDetay.CikisTarihi BETWEEN '{1}' AND '{2}' AND Model.kisaadi='{3}' AND Renk.TransferKodu='{4}' ) A", SEASON,BasTarih,BitTarih,MODEL,RENK);
            return sql;
        }
        public string KaliteBitisleriByDate(DateTime BasTarih, DateTime BitTarih, string SEASON, string MODEL, string RENK)
        {
            string sql = string.Format(@" SELECT SUM(Miktar) FROM (
Select  IsEmriOperasyonDetay.Miktar from IsEmriOperasyonDetay
    INNER join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    INNER join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    INNER join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    INNER join Departman on Departman.DepartmanID = GP.DepartmanId
    INNER join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    INNER join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    INNER join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    INNER join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID   
    INNER join Model On Model.ModelId = KesimFoyu.ModelId
    INNER join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    INNER Join Pazar on Pazar.PazarId = IsEmri.PazarId
    INNER join Sezon on Sezon.SezonID = KesimFoyu.SezonID 
    where  (IsEmriOperasyonDetay.MakinaId <> 'XXXX')      AND OPERASYON.bantsonoperasyonmu=1 AND Sezon.Adi='{0}' AND IsEmriOperasyonDetay.CikisTarihi BETWEEN '{1}' AND '{2}' AND Model.kisaadi='{3}' AND Renk.TransferKodu='{4}' ) A", SEASON, BasTarih, BitTarih, MODEL, RENK);
            return sql;
        }

        public string KesimAdediniGetir(string sezon,string model,string renk)
        {

            string sql = string.Format(@"Select SUM(IsEmriOperasyonDetay.Miktar)  from IsEmriOperasyonDetay
      left join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    left join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    left join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    left join PersonelGrup on PersonelGrup.PersonelGrupId = Gp.PersonelGrupId
    left join Departman on Departman.DepartmanID = GP.DepartmanId
    left join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    left join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    left join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    left join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID
    left join Model On Model.ModelId = KesimFoyu.ModelId
    left join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    Left Join Pazar on Pazar.PazarId = IsEmri.PazarId
    left join Sezon on Sezon.SezonID = KesimFoyu.SezonID
    left join GenelIEOperasyonGrup on GenelIEOperasyonGrup.GenelIEOperasyonGrupID = Operasyon.GenelIEOperasyonGrupId
    left join Vardiya On Vardiya.VardiyaId = IsEmriOperasyonDetay.VardiyaId
    Left Join IslemTipi on IslemTipi.IslemTipiId = IsEmri.IslemTipiId
    left join OperasyonGrup on OperasyonGrup.OperasyonGrupID = Operasyon.OperasyonGrupId  WHERE Model.kisaadi='{0}' AND Renk.TransferKodu='{1}' AND Sezon.Adi='{2}'
AND IsEmriOperasyon.OperasyonId IN (5515,5516,5517) ", model, renk, sezon);

            return sql;
        }
        public string KesimAdediniGetirByDate(string sezon, string model, string renk,DateTime basTarih, DateTime bitTarih)
        {

            string sql = string.Format(@"Select SUM(IsEmriOperasyonDetay.Miktar)  from IsEmriOperasyonDetay
       left join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    left join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    left join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    left join PersonelGrup on PersonelGrup.PersonelGrupId = Gp.PersonelGrupId
    left join Departman on Departman.DepartmanID = GP.DepartmanId
    left join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    left join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    left join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    left join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID
    left join Model On Model.ModelId = KesimFoyu.ModelId
    left join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    Left Join Pazar on Pazar.PazarId = IsEmri.PazarId
    left join Sezon on Sezon.SezonID = KesimFoyu.SezonID
    left join GenelIEOperasyonGrup on GenelIEOperasyonGrup.GenelIEOperasyonGrupID = Operasyon.GenelIEOperasyonGrupId
    left join Vardiya On Vardiya.VardiyaId = IsEmriOperasyonDetay.VardiyaId
    Left Join IslemTipi on IslemTipi.IslemTipiId = IsEmri.IslemTipiId
    left join OperasyonGrup on OperasyonGrup.OperasyonGrupID = Operasyon.OperasyonGrupId WHERE Model.kisaadi='{0}' AND Renk.TransferKodu='{1}' AND Sezon.Adi='{2}'
AND IsEmriOperasyon.OperasyonId IN (5515,5516,5517) AND IsEmriOperasyonDetay.CIKISTARIHI BETWEEN  '{3}' AND '{4}' ", model, renk, sezon, basTarih, bitTarih);

            return sql;
        }

        public string TasnifAdediniGetir(string sezon, string model, string renk)
        {

            string sql = string.Format(@"Select SUM(IsEmriOperasyonDetay.Miktar)  from IsEmriOperasyonDetay
     left join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    left join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    left join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    left join PersonelGrup on PersonelGrup.PersonelGrupId = Gp.PersonelGrupId
    left join Departman on Departman.DepartmanID = GP.DepartmanId
    left join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    left join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    left join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    left join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID
    left join Model On Model.ModelId = KesimFoyu.ModelId
    left join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    Left Join Pazar on Pazar.PazarId = IsEmri.PazarId
    left join Sezon on Sezon.SezonID = KesimFoyu.SezonID
    left join GenelIEOperasyonGrup on GenelIEOperasyonGrup.GenelIEOperasyonGrupID = Operasyon.GenelIEOperasyonGrupId
    left join Vardiya On Vardiya.VardiyaId = IsEmriOperasyonDetay.VardiyaId
    Left Join IslemTipi on IslemTipi.IslemTipiId = IsEmri.IslemTipiId
    left join OperasyonGrup on OperasyonGrup.OperasyonGrupID = Operasyon.OperasyonGrupId WHERE Model.kisaadi='{0}' AND Renk.TransferKodu='{1}' AND Sezon.Adi='{2}'
AND IsEmriOperasyon.OperasyonId IN (8631,8632,8633,9576) ", model, renk, sezon);

            return sql;
        }
        public string TasnifAdediniGetirByDate(string sezon, string model, string renk, DateTime basTarih, DateTime bitTarih)
        {

            string sql = string.Format(@"Select SUM(IsEmriOperasyonDetay.Miktar)  from IsEmriOperasyonDetay
      left join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    left join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    left join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    left join PersonelGrup on PersonelGrup.PersonelGrupId = Gp.PersonelGrupId
    left join Departman on Departman.DepartmanID = GP.DepartmanId
    left join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    left join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    left join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    left join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID
    left join Model On Model.ModelId = KesimFoyu.ModelId
    left join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    Left Join Pazar on Pazar.PazarId = IsEmri.PazarId
    left join Sezon on Sezon.SezonID = KesimFoyu.SezonID
    left join GenelIEOperasyonGrup on GenelIEOperasyonGrup.GenelIEOperasyonGrupID = Operasyon.GenelIEOperasyonGrupId
    left join Vardiya On Vardiya.VardiyaId = IsEmriOperasyonDetay.VardiyaId
    Left Join IslemTipi on IslemTipi.IslemTipiId = IsEmri.IslemTipiId
    left join OperasyonGrup on OperasyonGrup.OperasyonGrupID = Operasyon.OperasyonGrupId  WHERE Model.kisaadi='{0}' AND Renk.TransferKodu='{1}' AND Sezon.Adi='{2}'
AND IsEmriOperasyon.OperasyonId IN (8631,8632,8633,9576) AND IsEmriOperasyonDetay.CIKISTARIHI BETWEEN  '{3}' AND '{4}'  ", model, renk, sezon, basTarih, bitTarih);

            return sql;
        }

        public string BantAdediniGetir(string sezon, string model, string renk)
        {

            string sql = string.Format(@"Select SUM(IsEmriOperasyonDetay.Miktar)  from IsEmriOperasyonDetay
      left join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    left join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    left join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    left join PersonelGrup on PersonelGrup.PersonelGrupId = Gp.PersonelGrupId
    left join Departman on Departman.DepartmanID = GP.DepartmanId
    left join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    left join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    left join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    left join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID
    left join Model On Model.ModelId = KesimFoyu.ModelId
    left join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    Left Join Pazar on Pazar.PazarId = IsEmri.PazarId
    left join Sezon on Sezon.SezonID = KesimFoyu.SezonID
    left join GenelIEOperasyonGrup on GenelIEOperasyonGrup.GenelIEOperasyonGrupID = Operasyon.GenelIEOperasyonGrupId
    left join Vardiya On Vardiya.VardiyaId = IsEmriOperasyonDetay.VardiyaId
    Left Join IslemTipi on IslemTipi.IslemTipiId = IsEmri.IslemTipiId
    left join OperasyonGrup on OperasyonGrup.OperasyonGrupID = Operasyon.OperasyonGrupId  WHERE Model.kisaadi='{0}' AND Renk.TransferKodu='{1}' AND Sezon.Adi='{2}'
AND IsEmriOperasyon.OperasyonId IN (5,600,9595,845,670,80) ", model, renk, sezon);

            return sql;
        }

        public string BantAdediniGetirByDate(string sezon, string model, string renk, DateTime basTarih, DateTime bitTarih)
        {

            string sql = string.Format(@"Select SUM(IsEmriOperasyonDetay.Miktar)  from IsEmriOperasyonDetay
       left join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    left join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    left join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    left join PersonelGrup on PersonelGrup.PersonelGrupId = Gp.PersonelGrupId
    left join Departman on Departman.DepartmanID = GP.DepartmanId
    left join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    left join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    left join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    left join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID
    left join Model On Model.ModelId = KesimFoyu.ModelId
    left join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    Left Join Pazar on Pazar.PazarId = IsEmri.PazarId
    left join Sezon on Sezon.SezonID = KesimFoyu.SezonID
    left join GenelIEOperasyonGrup on GenelIEOperasyonGrup.GenelIEOperasyonGrupID = Operasyon.GenelIEOperasyonGrupId
    left join Vardiya On Vardiya.VardiyaId = IsEmriOperasyonDetay.VardiyaId
    Left Join IslemTipi on IslemTipi.IslemTipiId = IsEmri.IslemTipiId
    left join OperasyonGrup on OperasyonGrup.OperasyonGrupID = Operasyon.OperasyonGrupId  WHERE Model.kisaadi='{0}' AND Renk.TransferKodu='{1}' AND Sezon.Adi='{2}'
AND IsEmriOperasyon.OperasyonId IN (5,600,9595,845,670,80) AND IsEmriOperasyonDetay.CIKISTARIHI BETWEEN  '{3}' AND '{4}' ", model, renk, sezon, basTarih, bitTarih);

            return sql;
        }

        public string KaliteAdediniGetir(string sezon, string model, string renk)
        {

            string sql = string.Format(@"Select SUM(IsEmriOperasyonDetay.Miktar)  from IsEmriOperasyonDetay
     left join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    left join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    left join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    left join PersonelGrup on PersonelGrup.PersonelGrupId = Gp.PersonelGrupId
    left join Departman on Departman.DepartmanID = GP.DepartmanId
    left join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    left join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    left join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    left join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID
    left join Model On Model.ModelId = KesimFoyu.ModelId
    left join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    Left Join Pazar on Pazar.PazarId = IsEmri.PazarId
    left join Sezon on Sezon.SezonID = KesimFoyu.SezonID
    left join GenelIEOperasyonGrup on GenelIEOperasyonGrup.GenelIEOperasyonGrupID = Operasyon.GenelIEOperasyonGrupId
    left join Vardiya On Vardiya.VardiyaId = IsEmriOperasyonDetay.VardiyaId
    Left Join IslemTipi on IslemTipi.IslemTipiId = IsEmri.IslemTipiId
    left join OperasyonGrup on OperasyonGrup.OperasyonGrupID = Operasyon.OperasyonGrupId  WHERE Model.kisaadi='{0}' AND Renk.TransferKodu='{1}' AND Sezon.Adi='{2}'
AND IsEmriOperasyon.OperasyonId IN (181,1160,745,743,744) ", model, renk, sezon);

            return sql;
        }
        public string KaliteAdediniGetirByDate(string sezon, string model, string renk, DateTime basTarih, DateTime bitTarih)
        {

            string sql = string.Format(@"Select SUM(IsEmriOperasyonDetay.Miktar)  from IsEmriOperasyonDetay
      left join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    left join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId
    left join Personel Gp on Gp.PersonelId = IsEmriOperasyonDetay.GirisPersonelId
    left join PersonelGrup on PersonelGrup.PersonelGrupId = Gp.PersonelGrupId
    left join Departman on Departman.DepartmanID = GP.DepartmanId
    left join Makina  on Makina.MakinaId = IsEmriOperasyonDetay.MakinaId
    left join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    left join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    left join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID
    left join Model On Model.ModelId = KesimFoyu.ModelId
    left join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    Left Join Pazar on Pazar.PazarId = IsEmri.PazarId
    left join Sezon on Sezon.SezonID = KesimFoyu.SezonID
    left join GenelIEOperasyonGrup on GenelIEOperasyonGrup.GenelIEOperasyonGrupID = Operasyon.GenelIEOperasyonGrupId
    left join Vardiya On Vardiya.VardiyaId = IsEmriOperasyonDetay.VardiyaId
    Left Join IslemTipi on IslemTipi.IslemTipiId = IsEmri.IslemTipiId
    left join OperasyonGrup on OperasyonGrup.OperasyonGrupID = Operasyon.OperasyonGrupId  WHERE Model.kisaadi='{0}' AND Renk.TransferKodu='{1}' AND Sezon.Adi='{2}'
AND IsEmriOperasyon.OperasyonId IN (181,1160,745,743,744) AND IsEmriOperasyonDetay.CIKISTARIHI BETWEEN  '{3}' AND '{4}' ", model, renk, sezon, basTarih, bitTarih);

            return sql;
        }

    }
}
