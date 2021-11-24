using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EPM.Production.Dto.Extensions
{ 
    
    public enum ModalType
    {
        Question,
        Information,
        Exclamation
    }
    public enum DetailStatus
    {
        Started = 1,
        Waiting = 2,
        Done = 3
    }
    public enum DialogResultF
    {
        Yes, No, OK
    }
    public enum ProcessTags
    {
        IPLIK = 1,
        ORGU = 2,
        BOYAMA = 3,
        KUMAS = 4,
        KESIM = 5,
        TASNIF = 6,
        BANT = 7,
        KALITE = 8,
        ORGUDEPO = 9
    }
    public enum FirebirdConnectionDB
    {
        DEVANLAY = 1,
        HAMIPLIK = 2,
        KUMASBOYA = 3,
        IPLIKBOYA = 4,
        ORME = 5,
        TRIKO = 6,
        ERENTEKSTIL = 7,
    }

    public enum MALAZEMETIPLERI
    {
        KUMAS = 1,
        YAKA = 2,
        MALZEME = 3
    }

    public enum SURECDURUMLARI
    {
        [Description("BAŞLADI")]
        BASLADI = 1,
        [Description("BEKLEMEDE")]
        BEKLEMEDE = 0,
        [Description("TAMAMLANDI")]
        TAMAMLANDI = 2,
        [Description("BAŞLAMASI BEKLENİYOR")]
        BASLAMABEKLENIYOR = 3,
        [Description("EKSİK TAMAMLANAN")]
        EKSIKTAMAMLANAN = 4,
        [Description("VERİ BULUNAMADI")]
        VERIBULUNAMADI = 5
    }


    public enum FASONSURECDURUMLARI
    {
        [Description("BEKLEMEDE")]
        BEKLEMEDE = 1,
        [Description("SIRADA")]
        SIRADA = 0,
        [Description("BAŞLADI")]
        BASLADI = 2,
        [Description("TAMAMLANDI")]
        TAMAMLADI = 3
    }

    public enum FASONSIPARISCDURUMLARI
    {
        [Description("TAMAMLANDI ONAYI BEKLİYOR")]
        ONAYBEKLIYOR = 1,
        [Description("DEVAM EDİYOR")]
        DEVAMEDIYOR = 0,
        [Description("TAMAMLANDI")]
        TAMAMLANDI = 2
    }

    public enum APPROVAL_STATUSES
    {
        [Description("ONAY BEKLEYEN İŞLEM")]
        ONAYSIZ = 0,
        [Description("ONAYLANMIŞ İŞLEM")]
        ONAYLI = 1,
    }

    public enum STATUSES
    {
        [Description("DEVAM EDEN SİPARİŞLER")]
        ONAYSIZ = 0,
        [Description("İPTAL EDİLEN SİPARİŞLER")]
        ONAYLI = 1,
    }
    public class ENUMMODEL
    {
        public int ID { get; set; }
        public string ADI { get; set; }
    }

    public enum SURECLER
    {
        IPLIK=1,
        ORGU=2,
        KUMASBOYA=3,
        KUMASDEPO=8,
        KESIM=4,
        TASNIF=5,
        BANT=6,
        KALITE=7
    }
}
