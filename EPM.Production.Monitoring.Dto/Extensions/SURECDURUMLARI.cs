using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EPM.Production.Monitoring.Dto.Extensions
{
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
}
