
using EPM.Fason.Dto.Extensions;
using EPM.Fason.Dto.Fason;
using EPM.Fason.Repository.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static EPM.Fason.Dto.Extensions.Enums;

namespace EPM.Fason.Service.Services
{
    public class AktarimService : IAktarimService
    {
        private readonly IFasonRepository _fasonRepository;
        public AktarimService(IFasonRepository fasonRepository)
        {
            _fasonRepository = fasonRepository;
        }

        public object[] FasonUretimInsert(PRODUCTION_HEADER header)
        {
            object[] obj = { true, header.ENTEGRATION_ID.ToString()};
            try
            {
                string sql = "DELETE PRODUCTION_HEADER WHERE ENTEGRATION_ID=" + header.ENTEGRATION_ID;
                _fasonRepository.ExecSql(sql);

                sql = "DELETE PRODUCTION_DETAIL WHERE ENTEGRATION_HEADER_ID=" + header.ENTEGRATION_ID;
                _fasonRepository.ExecSql(sql);

                _fasonRepository.Serialize(header);

                foreach (var item in header.DETAIL)
                    _fasonRepository.Serialize(item);
            }
            catch (Exception ex)
            {
                obj[0] = false;
                obj[1] = ex.Message;
            }
            

            return obj;
        } 
         

        public List<PRODUCTION_PROCESS> GetProcessList()
        {
            return _fasonRepository.DeserializeList<PRODUCTION_PROCESS>("SELECT * FROM PRODUCTION_PROCESS ORDER BY QUEUE");
        }

        public List<PRODUCTION_FASON_USERS> GetFasonUsers()
        {
            return _fasonRepository.DeserializeList<PRODUCTION_FASON_USERS>("SELECT * FROM PRODUCTION_FASON_USERS");
        }

        public object[] SiparisOlustur(CREATEORDER order)
        {
            object[] obj = { true, order.header.ENTEGRATION_ID.ToString() };
            try
            {
                string sql = "DELETE PRODUCTION_HEADER WHERE ENTEGRATION_ID=" + order.header.ENTEGRATION_ID;
                _fasonRepository.ExecSql(sql);

                sql = "DELETE PRODUCTION_DETAIL WHERE ENTEGRATION_HEADER_ID=" + order.header.ENTEGRATION_ID;
                _fasonRepository.ExecSql(sql);

                sql = "DELETE PRODUCTION_LIST_L WHERE HEADER_ID=(SELECT ID FROM PRODUCTION_LIST_H WHERE ENTEGRATION_ID=" + order.header.ENTEGRATION_ID + " )";
                _fasonRepository.ExecSql(sql);

                sql = "DELETE PRODUCTION_LIST_H WHERE ENTEGRATION_ID=" + order.header.ENTEGRATION_ID;
                _fasonRepository.ExecSql(sql);

                _fasonRepository.Serialize(order.header);

                foreach (var item in order.header.DETAIL)
                    _fasonRepository.Serialize(item);

               


                PRODUCTION_LIST_H h = new PRODUCTION_LIST_H();
                h.ENTEGRATION_ID = order.header.ENTEGRATION_ID;
                h.STATUS = (int)HEADERSTATUS.WAITINGFORSTART;
                h.FIRMA_ID = order.formHeader.FIRMA_ID;
                h.END_DATE = order.formHeader.TERMIN_TARIHI;
                _fasonRepository.Serialize(h);
                DateTime end_date = order.formHeader.TERMIN_TARIHI;
                for (int i = order.plan.Count; i > 0; i--)
                {
                    PRODUCTION_PROCESS p = order.plan[i - 1];
                    PRODUCTION_LIST_L l = new PRODUCTION_LIST_L();
                    l.HEADER_ID = h.ID;
                    l.PROCESS_ID = p.ID;
                    l.QUEUE = i;
                    if (i == 1)
                        l.STATUS = (int)LINESTATUS.WAITINGFORSTART;
                    else l.STATUS = (int)LINESTATUS.WAITING;
                    l.END_DATE = end_date;
                    l.START_DATE = end_date.AddBusinessDays(-p.TIME);
                    end_date = l.START_DATE;
                    l.TIME = p.TIME;
                    l.HANDLE_SIDE = p.HANDLE_SIDE;
                    _fasonRepository.Serialize(l);
                }
            }
            catch (Exception ex)
            {
                obj[0] = false;
                obj[1] = ex.Message;
            }

             

            return obj;
        }
        List<PRODUCTION_LIST_V> GetProcessList(int ENTEGRASYON_ID)
        {
            string sql = string.Format(@"SELECT PL.ID DETAIL_ID,PL.HEADER_ID,PH.ENTEGRATION_ID,PR.NAME PROCESS_NAME,PL.START_DATE,PL.END_DATE,PL.STATUS,dbo.SENDSTATUSEX(PL.STATUS) STATUS_EX,PL.QUEUE FROM PRODUCTION_LIST_L PL
INNER JOIN PRODUCTION_LIST_H PH ON PH.ID=PL.HEADER_ID
INNER JOIN PRODUCTION_PROCESS PR ON PR.ID=PL.PROCESS_ID WHERE PH.ENTEGRATION_ID={0} ORDER BY PL.QUEUE", ENTEGRASYON_ID);
            var list = _fasonRepository.DeserializeList<PRODUCTION_LIST_V>(sql);
            return list;
        }
        public List<PRODUCTION_STATUS> GetProductionStatus(int[] ids)
        {
            string sql = string.Format(@"

SELECT        PRL.STATUS, dbo.SENDSTATUSEX(PRL.STATUS) AS STATUSEX, PRC.NAME PROCESS_NAME, PRH.ENTEGRATION_ID, PRL.END_DATE, PRL.START_DATE, PRH.END_DATE AS DEADLINE_CUSTOMER,USR.NAME as COMPANY_NAME
                               FROM            dbo.PRODUCTION_LIST_L AS PRL 
							   INNER JOIN dbo.PRODUCTION_LIST_H AS PRH ON PRH.ID = PRL.HEADER_ID 
														 INNER JOIN dbo.PRODUCTION_PROCESS AS PRC ON PRC.ID = PRL.PROCESS_ID
														 INNER JOIN dbo.PRODUCTION_FASON_USERS USR ON USR.ID=PRH.FIRMA_ID
                               WHERE        (PRL.STATUS NOT IN (0, 3)) AND PRH.ENTEGRATION_ID IN ({0})
                ", string.Join(',', ids));

           return _fasonRepository.DeserializeList<PRODUCTION_STATUS>(sql);
        }

        public List<PRODUCTION_LIST_V> SurecDurumuGetir(int ENTEGRATION_ID)
        {
            return GetProcessList(ENTEGRATION_ID);
        }
    }
}
