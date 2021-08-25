using EPM.Dto.FormModels.SatinAlma;
using EPM.Dto.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Service.Base
{
    public interface ISatinAlmaService
    {
        public List<DepoBilgileri> GetInventories(int ORGANIZATION_ID = 105);

        public List<SaticiBilgileri> GetVendorList(int ORGANIZATION_ID = 105);

        public List<ParaBirimleri> GetParaBirimleri(int ORGANIZATION_ID = 105, int VENDOR_SITE_ID = 0);

        public OperationResult SatinAlmaListesiniAktarExcelYukle(HttpRequest request,int ORGANIZATION_ID);

        public void SatinAlmaAktar(HttpContext context, JObject obj);
    }
}
