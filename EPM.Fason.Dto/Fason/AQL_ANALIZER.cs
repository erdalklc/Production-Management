using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class AQL_ANALIZER
    {
        public int MIN_QUANTITY { get; set; }
        public int MAX_QUANTITY { get; set; }
        public int SAMPLE_QUANTITY { get; set; }
        public int REJECT_QUANTITY { get; set; }
        public int ACCEPT_QUANTITY { get; set; }
        public AQL_ANALIZER()
        {

        }
        public AQL_ANALIZER(int _MIN_QUANTITY,int _MAX_QUANTITY, int _SAMPLE_QUANTITY, int _ACCEPT_QUANTITY, int _REJECT_QUANTITY )
        {
            MIN_QUANTITY = _MIN_QUANTITY;
            MAX_QUANTITY = _MAX_QUANTITY;
            SAMPLE_QUANTITY = _SAMPLE_QUANTITY;
            REJECT_QUANTITY = _REJECT_QUANTITY;
            ACCEPT_QUANTITY = _ACCEPT_QUANTITY;
        }
        public AQL_ANALIZER GetAQLPointsByOrder(int ORDER_COUNT)
        {
            var list = GetAQLPoints();

            return list.Find(ob => ob.MIN_QUANTITY < ORDER_COUNT && ob.MAX_QUANTITY > ORDER_COUNT);
        }
        private List<AQL_ANALIZER> GetAQLPoints()
        {
            List<AQL_ANALIZER> list = new List<AQL_ANALIZER>();
            list.Add(new AQL_ANALIZER(2, 8, 2, 0, 1));
            list.Add(new AQL_ANALIZER(9,15,3,0,1));
            list.Add(new AQL_ANALIZER(16,25,5,0,1));
            list.Add(new AQL_ANALIZER(26,50,8,0,1));
            list.Add(new AQL_ANALIZER(51,90,13,0,1));
            list.Add(new AQL_ANALIZER(91,150,20,0,1));
            list.Add(new AQL_ANALIZER(151,280,32, 1,2));
            list.Add(new AQL_ANALIZER(281,500,50,2,3));
            list.Add(new AQL_ANALIZER(501,1200,80,3,4));
            list.Add(new AQL_ANALIZER(1201,3200,125,5,6));
            list.Add(new AQL_ANALIZER(3201,10000,200,7,8));
            list.Add(new AQL_ANALIZER(10001,35000,315,10,11));
            list.Add(new AQL_ANALIZER(35001, 150000,500,14,15));
            return list;
        }

    }
    
}
