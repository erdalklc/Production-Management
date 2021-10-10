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


    }
}
