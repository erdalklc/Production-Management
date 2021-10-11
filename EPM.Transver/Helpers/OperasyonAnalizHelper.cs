using EPM.Core.Managers;
using EPM.Transver.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Transver.Helpers
{
    public class OperasyonAnalizHelper
    {
        public string GetOperasyonAnalizBySeason(string SEASON)
        {
            string sql = string.Format(@"Select Model.kisaadi MODEL_ADI, renk.transferkodu RENK_ADI,SEZON.adi SEZON_ADI,IsEmriOperasyon.OperasyonId OPERASYON_ID,SUM(IsEmriOperasyonDetay.Miktar) MIKTAR from IsEmriOperasyonDetay
    left join IsEmriOperasyon on IsEmriOperasyon.IsEmriOperasyonID = IsEmriOperasyonDetay.IsEmriOperasyonId
    left join Operasyon on Operasyon.OperasyonID = IsEmriOperasyon.OperasyonId  
    left join IsEmri on IsEmri.IsEmriKayitID = IsEmriOperasyonDetay.IsEmriKayitID
    left join KesimFoyuDetay on KesimFoyuDetay.KesimFoyuDetayID = IsEmri.KesimFoyuDetayId
    left join KesimFoyu on KesimFoyu.KesimFoyuKayitID = KesimFoyuDetay.KesimFoyuKayitID
    left join Model On Model.ModelId = KesimFoyu.ModelId
    left join Renk On Renk.RenkId = KesimFoyuDetay.RenkId
    left join Sezon on Sezon.SezonID = KesimFoyu.SezonID
    left join GenelIEOperasyonGrup on GenelIEOperasyonGrup.GenelIEOperasyonGrupID = Operasyon.GenelIEOperasyonGrupId
    left join Vardiya On Vardiya.VardiyaId = IsEmriOperasyonDetay.VardiyaId
    left join OperasyonGrup on OperasyonGrup.OperasyonGrupID = Operasyon.OperasyonGrupId
    where IsEmriOperasyon.OperasyonId IN (181,1160,745,743,744,5,600,9595,845,670,80,8631,8632,8633,9576,5515,5516,5517) and
          (IsEmriOperasyonDetay.MakinaId <> 'XXXX')  
          and   sezon.adi='{0}'
          GROUP BY Model.kisaadi , renk.transferkodu ,SEZON.adi ,IsEmriOperasyon.OperasyonId
",SEASON);
            return sql;
        }

        public void  OperasyonAnalizEt()
        {
            string[] seasons = { "21-22 KIS", "2021 YAZ" };

            foreach (var item in seasons)
            {

                var list= FirebirdServer.DeserializeList<OperasyonModel>(GetOperasyonAnalizBySeason(item));
                Console.WriteLine("(Operasyon) (Veriler Analiz Edildi) (" + item + ") !\t" + DateTime.Now.ToString("g"));
                string sql = "DELETE FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE SEZON_ADI='" + item + "'";
                OracleServer.ExecSql(sql);
               
                OracleServer.BulkInsert(list);
            }
        }
    }
}
