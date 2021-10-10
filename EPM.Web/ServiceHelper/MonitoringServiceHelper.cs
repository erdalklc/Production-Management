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

        public static List<ProductModel> GetProductList(HaftaModel model,FilterModel filterModel)
        {
            string apiUrl = "GetProductList";
            var list = PostRequest<Tuple<HaftaModel,FilterModel>, List<ProductModel>>(EPMServiceType.Monitoring, apiUrl, new Tuple<HaftaModel, FilterModel>(model,filterModel));
            return list;
        }

        public static object GetProductionDetails(HaftaModel haftaModel, ProductModel productModel, FilterModel filterModel)
        {
            string apiUrl = "GetProductionDetails";
            var list = PostRequest<Tuple<HaftaModel, ProductModel, FilterModel>, Tuple<EPM_MASTER_PRODUCTION_H, List<PlanModel>, EPM_TRACKING_PROCESS_VALUES>>(EPMServiceType.Monitoring, apiUrl, new Tuple<HaftaModel, ProductModel, FilterModel>(haftaModel, productModel, filterModel));
            return list;
        }
        public static object GetProductionDetailsByDate(HaftaModel haftaModel, ProductModel productModel, FilterModel filterModel,DateTime tarih)
        {
            string apiUrl = "GetProductionDetailsByDate";
            var list = PostRequest<Tuple<HaftaModel, ProductModel, FilterModel, DateTime>, EPM_TRACKING_PROCESS_VALUES>(EPMServiceType.Monitoring, apiUrl, new Tuple<HaftaModel, ProductModel, FilterModel, DateTime>(haftaModel, productModel, filterModel, tarih));
            return list;
        }
    }
}
