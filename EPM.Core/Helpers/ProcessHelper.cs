using EPM.Core.Managers;
using EPM.Core.Models;
using EPM.Core.Nesneler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EPM.Core.Helpers
{
    public class ProcessHelper
    {
        private static void ONCEKILERITAMAMLA(List<EPM_PRODUCTION_TRACKING_LIST> uretimList, int a)
        {
            for (int i = 0; i < a; i++)
            {
                EPM_PRODUCTION_TRACKING_LIST uretim = uretimList[i];
                uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                uretim = OracleServer.Serialize(uretim);
            }
        }

        static void SONRAKINEILERLET(List<EPM_PRODUCTION_TRACKING_LIST> uretimList, int i)
        {
            if (uretimList.Count - 1 >= i + 1)
            {
                uretimList[i + 1].STATUS = (int)SURECDURUMLARI.BASLADI;
                uretimList[i + 1] = OracleServer.Serialize(uretimList[i + 1]);
            }
        }

        static bool ONCEKILERDEBASLAMISVARMI(List<EPM_PRODUCTION_TRACKING_LIST> uretimList, int i)
        {
            bool evet = false;

            for (int a = 0; a < i; a++)
            {
                if (uretimList[a].STATUS == (int)SURECDURUMLARI.BASLADI)
                {
                    evet = true;
                    break;
                }
            }

            return evet;
        }

        public static void SETPROCESS(EPM_MASTER_PRODUCTION_ORDERS order)
        {
            OracleServer.ExecSql("DELETE FDEIT005.EPM_PRODUCTION_TRACKING_LIST WHERE PO_HEADER_ID=" + order.PO_HEADER_ID + " AND HEADER_ID=" + order.HEADER_ID + "");
            EPM_MASTER_PRODUCTION_H master = OracleServer.Deserialize<EPM_MASTER_PRODUCTION_H>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_H WHERE ID=" + order.HEADER_ID);
            List<EPM_MASTER_PRODUCTION_D> detaiList = OracleServer.DeserializeList<EPM_MASTER_PRODUCTION_D>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE HEADER_ID=" + master.ID); 
            DataTable dtDetail = OracleServer.QueryFill(ServiceHelper.DETAILSQL() + " AND PO_HEADER_ID=" + order.PO_HEADER_ID + " ORDER BY DETAIL_TAKIP_NO");
            EPM_PRODUCTION_TRACKING_LIST.CREATELIST(order);
            foreach (DataRow rowDetail in dtDetail.Rows)
            {

                int DETAIL_ID = rowDetail["DETAIL_TAKIP_NO"].IntParse();
                string TAKIP_NO = rowDetail["TAKIP_NO"].ToString();
                int ITEM_ID = rowDetail["ITEM_ID"].IntParse();
                string RENKDETAY = rowDetail["RENKDETAY"].ToString();
                string ANAMODEL = master.MODEL;
                int HEADER_ID = master.ID;
                int URETIM_ADET = detaiList.Sum(ob => ob.QUANTITY).IntParse();
                int TEDARIK = rowDetail["TEDARIK"].IntParse();
                DateTime URETIMTARIHI = master.DEADLINE;
                string KesimFoyleri = "";

                List<ReceteProcessModel> m = OracleServer.DeserializeList<ReceteProcessModel>(@"
                                SELECT RH.ADI,
                                     PI.PROCESS_NAME,
                                     PI.PROCESS_TIME, 
                                     RD.PROCESS_ID,
                                     RD.QUEUE,
                                     RH.ID AS RECETEHEADERID
                                FROM FDEIT005.EPM_PRODUCTION_RECIPE RH
                                     INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD ON RD.HEADER_ID = RH.ID
                                     INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI ON PI.ID = RD.PROCESS_ID
                            ORDER BY RECETEHEADERID, QUEUE").FindAll(ob => ob.RECETEHEADERID == master.RECIPE).OrderByDescending(ob => ob.QUEUE).ToList();
                List<EPM_PRODUCTION_TRACKING_LIST> uretimList = EPM_PRODUCTION_TRACKING_LIST.GETLISTMODEL(order.PO_HEADER_ID, DETAIL_ID, master.RECIPE,order.HEADER_ID);
                DataRow[] rows;
                if (uretimList.Count > 0)
                {

                    DataTable dtOrme = FirebirdServer.QueryFill(EgemenOrmeHelper.ORMEALINANSIPARISSATIRTAKIP(TAKIP_NO.ToString(), DETAIL_ID.ToString()), (FirebirdServer.FirebirdConnectionDB)FirebirdConnectionDB.ORME);
                    DataTable dtKumasDepo = OracleServer.QueryFill(KumasDepoHelper.KumasDepoHareketleri(ITEM_ID, order.PO_HEADER_ID));
                    DataTable dtKesimTasnif = OracleServer.QueryFill(KesimFoyuHelper.KesimFoyleri(ITEM_ID, order.PO_HEADER_ID, RENKDETAY, ANAMODEL));
                    DataTable dtBant = new DataTable();
                    DataTable dtKalite = new DataTable();
                    if (dtKesimTasnif.Rows.Count > 0)
                    {
                        KesimFoyleri = "";
                        List<string> kFoy = new DataView(dtKesimTasnif).ToTable(true, "WIP_ENTITY_NAME").AsEnumerable().Select(ob => ob.Field<string>("WIP_ENTITY_NAME")).ToList();
                        foreach (string s in kFoy)
                            KesimFoyleri += "'" + s + "',";
                        KesimFoyleri = KesimFoyleri.TrimEnd(',');
                        dtBant = new EgemenDevanlayHelper().BANTBITISBILGILERI(KesimFoyleri, order.PO_HEADER_ID);
                        dtKalite = new EgemenDevanlayHelper().KALITEBITISBILGILERI(KesimFoyleri, order.PO_HEADER_ID);
                    }
                    else
                        KesimFoyleri = "";

                    for (int i = 0; i < uretimList.Count; i++)
                    {
                        EPM_PRODUCTION_TRACKING_LIST uretim = uretimList[i];
                        SURECLER surec = (SURECLER)m.Find(ob => ob.PROCESS_ID == uretim.PROCESS_ID).PROCESS_ID;

                        int planlanan = dtKesimTasnif.Compute("SUM(PLANLANAN_KESIM)", string.Empty).IntParse();
                        int kesilen = dtKesimTasnif.Compute("SUM(FIILI_KESIM)", string.Empty).IntParse();
                        int tasnif = dtKesimTasnif.Compute("SUM(TASNIF_MIKTARI)", string.Empty).IntParse();
                        int bant = 0;
                        if (dtBant.Rows.Count > 0)
                            bant = dtBant.Compute("SUM(MIKTAR)", string.Empty).IntParse();
                        switch (surec)
                        {
                            case SURECLER.IPLIK: //İPLİK
                                rows = dtOrme.Select("(HAREKET_ADI='Alış' AND URUN_TIPI_ADI='İplik') OR (HAREKET_ADI='Stok Kaydırma (Giriş)' AND URUN_TIPI_ADI='İplik')");
                                uretim.BEKLENEN_MIKTAR = TEDARIK;
                                if (rows != null && rows.Length > 0)
                                {
                                    decimal adetIplik = rows.CopyToDataTable<DataRow>().Compute("SUM(MIKTAR)", string.Empty).DecimalParse();
                                    if (adetIplik < TEDARIK)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        uretim.GERCEKLESEN_MIKTAR = adetIplik;
                                    }
                                    else
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        uretim.GERCEKLESEN_MIKTAR = adetIplik;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                OracleServer.Serialize(uretim);
                                break;
                            case SURECLER.ORGU: //ÖRGÜ
                                rows = dtOrme.Select("(HAREKET_ADI='Alış' AND URUN_TIPI_ADI='Kumaş') OR (HAREKET_ADI='Stok Kaydırma (Giriş)' AND URUN_TIPI_ADI='Kumaş')");
                                uretim.BEKLENEN_MIKTAR = TEDARIK;
                                if (rows != null && rows.Length > 0)
                                {
                                    decimal adetKumas = rows.CopyToDataTable<DataRow>().Compute("SUM(MIKTAR)", string.Empty).DecimalParse();
                                    if (adetKumas < TEDARIK)
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumas;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumas;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                OracleServer.Serialize(uretim);
                                break;
                            case SURECLER.KUMASBOYA://KUMAŞ BOYA
                                rows = dtOrme.Select("HAREKET_ADI='Fason Üretime Çıkış' AND FIRMAID='F005'");
                                uretim.BEKLENEN_MIKTAR = TEDARIK;
                                if (rows != null && rows.Length > 0)
                                {
                                    decimal adetKumasBoya = rows.CopyToDataTable<DataRow>().Compute("SUM(MIKTAR)", string.Empty).DecimalParse();
                                    if (adetKumasBoya < TEDARIK)
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumasBoya;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumasBoya;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                OracleServer.Serialize(uretim);
                                break;
                            case SURECLER.KUMASDEPO://KUMAŞ DEPO
                                uretim.BEKLENEN_MIKTAR = TEDARIK;
                                if (dtKumasDepo.Rows.Count > 0)
                                {
                                    decimal adetKumasDepo = dtKumasDepo.Select("TRANSACTION_TYPE_ID=18").CopyToDataTable<DataRow>().Compute("SUM(ISLEM_MIKTARI)", string.Empty).DecimalParse();
                                    if (adetKumasDepo < TEDARIK)
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumasDepo;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumasDepo;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                OracleServer.Serialize(uretim);
                                break;
                            case SURECLER.KESIM://KESİM 
                                uretim.BEKLENEN_MIKTAR = planlanan;
                                uretim.GERCEKLESEN_MIKTAR = kesilen;
                                if (planlanan > 0 & kesilen > 0)
                                {
                                    ONCEKILERITAMAMLA(uretimList, i);
                                    if (kesilen >= planlanan)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else if (kesilen < planlanan)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                OracleServer.Serialize(uretim);

                                break;
                            case SURECLER.TASNIF://TASNİF
                                uretim.BEKLENEN_MIKTAR = kesilen;
                                uretim.GERCEKLESEN_MIKTAR = tasnif;
                                if (planlanan > 0 & tasnif > 0)
                                {
                                    ONCEKILERITAMAMLA(uretimList, i);
                                    if (tasnif >= kesilen && tasnif > 0)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else if (tasnif < kesilen)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                OracleServer.Serialize(uretim);
                                break;
                            case SURECLER.BANT://BANT
                                uretim.BEKLENEN_MIKTAR = URETIM_ADET;
                                uretim.GERCEKLESEN_MIKTAR = bant;
                                if (dtBant.Rows.Count > 0)
                                {
                                    ONCEKILERITAMAMLA(uretimList, i);
                                    if (bant >= URETIM_ADET)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                OracleServer.Serialize(uretim);
                                break;
                            case SURECLER.KALITE://KALİTE
                                int kaliteMiktar = 0;
                                if (dtKalite.Rows.Count > 0)
                                    kaliteMiktar = dtKalite.Compute("SUM(MIKTAR)", string.Empty).IntParse();
                                uretim.BEKLENEN_MIKTAR = URETIM_ADET;
                                uretim.GERCEKLESEN_MIKTAR = kaliteMiktar;
                                if (dtKalite.Rows.Count > 0)
                                {
                                    ONCEKILERITAMAMLA(uretimList, i);
                                    if (kaliteMiktar >= URETIM_ADET)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    if (kaliteMiktar < URETIM_ADET)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                OracleServer.Serialize(uretim);
                                break;
                            default: break;
                        }
                    }
                }
                else
                    OracleServer.ExecSql("DELETE FDEIT005.EPM_PRODUCTION_TRACKING_LIST WHERE PO_HEADER_ID=" + order.PO_HEADER_ID + " AND HEADER_ID=" + order.HEADER_ID + "");
            }
        }
    }
}
