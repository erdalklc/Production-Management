using EPM.Dto.Base;
using EPM.Fason.Dto.Fason;
using EPM.Production.Monitoring.Dto.Models;
using EPM.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

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

        public static List<ProductModel> GetProductList(List<HaftaModel> model,List<EPM_PRODUCT_GROUP> productGroupModel,List<EPM_PRODUCTION_MARKET> marketModel,FilterModel filterModel)
        {
            string apiUrl = "GetProductList";
            var list = PostRequest<Tuple<List<HaftaModel>, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>, FilterModel>, List<ProductModel>>(EPMServiceType.Monitoring, apiUrl, new Tuple<List<HaftaModel>, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>, FilterModel>(model,productGroupModel,marketModel,filterModel));
            return list;
        }

        public static object GetProductionDetails(List<HaftaModel> haftaModel, List<ProductModel> productModel, FilterModel filterModel, List<EPM_PRODUCT_GROUP> productGroupModel, List<EPM_PRODUCTION_MARKET> marketModel)
        {
            string apiUrl = "GetProductionDetails";
            var list = PostRequest<Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>>, Tuple<List<PlanModel>, EPM_TRACKING_PROCESS_VALUES, List<MarketReleasedModel>, List<ProductionModel>>>(EPMServiceType.Monitoring, apiUrl, new Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>>(haftaModel, productModel, filterModel,productGroupModel,marketModel));

            var Ids = list.Item4.Where(ob => ob.PRODUCTION_TYPE == 2).Select(ob => ob.ID).ToArray();
            if (Ids.Length > 0)
            {
                var listFason = PostRequest<int[], List<PRODUCTION_STATUS>>(EPMServiceType.Fason, "SendProductionsStatues", list.Item4.Where(ob => ob.PRODUCTION_TYPE == 2).Select(ob => ob.ID).ToArray());

                foreach (var item in listFason)
                {
                    var tt = list.Item4.Find(ob => ob.ID == item.ENTEGRATION_ID);
                    if (tt != null)
                    {
                        tt.PROCESS_INFO = item.PROCESS_NAME + " " + item.STATUSEX;
                        tt.COMPANY_NAME = item.COMPANY_NAME;
                    }
                }
            }
            return list;
        }
        public static object GetProductionDetailsByDate(List<HaftaModel> haftaModel, List<ProductModel> productModel, FilterModel filterModel,DateTime tarih)
        {
            string apiUrl = "GetProductionDetailsByDate";
            var list = PostRequest<Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, DateTime>, EPM_TRACKING_PROCESS_VALUES>(EPMServiceType.Monitoring, apiUrl, new Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, DateTime>(haftaModel, productModel, filterModel, tarih));
            return list;
        }
        public static object GetMarketList()
        {
            string apiUrl = "GetMarketList";
            var list = GetRequest<List<EPM_PRODUCTION_MARKET>>(EPMServiceType.Monitoring, apiUrl);
            return list;
        }
        public static object GetProductGroup()
        {
            string apiUrl = "GetProductGroup";
            var list = GetRequest<List<EPM_PRODUCT_GROUP>>(EPMServiceType.Monitoring, apiUrl);
            return list;
        }
    }
}
