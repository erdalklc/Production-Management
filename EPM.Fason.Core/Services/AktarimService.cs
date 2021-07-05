using EPM.Fason.Core.Managers;
using EPM.Fason.Core.Models;
using EPM.Fason.Core.Nesneler;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Core.Services
{
    public class AktarimService : IAktarimService
    {
        public object[] FasonUretimInsert(PRODUCTION_HEADER header)
        {
            object[] obj = { true, header.ENTEGRATION_ID.ToString()};
            try
            {
                string sql = "DELETE PRODUCTION_HEADER WHERE ENTEGRATION_ID=" + header.ENTEGRATION_ID;
                MSSQLServer.ExecSql(sql);

                sql = "DELETE PRODUCTION_DETAIL WHERE ENTEGRATION_HEADER_ID=" + header.ENTEGRATION_ID;
                MSSQLServer.ExecSql(sql);

                MSSQLServer.Serialize(header);

                foreach (var item in header.DETAIL)
                    MSSQLServer.Serialize(item);
            }
            catch (Exception ex)
            {
                obj[0] = false;
                obj[1] = ex.Message;
            }
            

            return obj;
        } 

        public PRODUCTION_STATUS GetProductionStatus(int _HEADER_ID)
        { 
            return new PRODUCTION_STATUS() { HEADER_ID = _HEADER_ID, LAST_STATE = "BEKLENİYOR", MUST_STATE = "AYARLANIYOR" };
        }

        public List<PRODUCTION_PROCESS> GetProcessList()
        {
            return MSSQLServer.DeserializeList<PRODUCTION_PROCESS>("SELECT * FROM PRODUCTION_PROCESS");
        }

        public List<PRODUCTION_FASON_USERS> GetFasonUsers()
        {
            return MSSQLServer.DeserializeList<PRODUCTION_FASON_USERS>("SELECT * FROM PRODUCTION_FASON_USERS");
        }


    }
}
