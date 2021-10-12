using EPM.Dto.Base;
using EPM.Production.Monitoring.Dto.Models;
using EPM.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.ServiceHelper
{
    public class MonitoringServiceHelper : EPMHttpCaller
    {
        public static List<HaftaModel> GetTerminList(FilterModel model)
        {
            string apiUrl = "GetTerminList";
            var list = PostRequest<FilterModel, List<HaftaModel>>(EPMServiceType.Monitoring, apiUrl, model);
            return list;
        }

        public static List<ProductModel> GetProductList(List<HaftaModel> model,FilterModel filterModel)
        {
            string apiUrl = "GetProductList";
            var list = PostRequest<Tuple<List<HaftaModel>, FilterModel>, List<ProductModel>>(EPMServiceType.Monitoring, apiUrl, new Tuple<List<HaftaModel>, FilterModel>(model,filterModel));
            return list;
        }

        public static object GetProductionDetails(List<HaftaModel> haftaModel, List<ProductModel> productModel, FilterModel filterModel)
        {
            string apiUrl = "GetProductionDetails";
            var list = PostRequest<Tuple<List<HaftaModel>, List<ProductModel>, FilterModel>, Tuple<List<PlanModel>, EPM_TRACKING_PROCESS_VALUES, List<MarketReleasedModel>>>(EPMServiceType.Monitoring, apiUrl, new Tuple<List<HaftaModel>, List<ProductModel>, FilterModel>(haftaModel, productModel, filterModel));
            return list;
        }
        public static object GetProductionDetailsByDate(List<HaftaModel> haftaModel, List<ProductModel> productModel, FilterModel filterModel,DateTime tarih)
        {
            string apiUrl = "GetProductionDetailsByDate";
            var list = PostRequest<Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, DateTime>, EPM_TRACKING_PROCESS_VALUES>(EPMServiceType.Monitoring, apiUrl, new Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, DateTime>(haftaModel, productModel, filterModel, tarih));
            return list;
        }
    }
}
