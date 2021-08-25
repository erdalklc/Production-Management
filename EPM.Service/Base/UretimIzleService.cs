using EPM.Dto.FormModels.UretimIzle;
using EPM.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Service.Base
{
    public class UretimIzleService : IUretimIzleService
    {
        private readonly IEPMRepository _epmRepository;
        public UretimIzleService(IEPMRepository epmRepository)
        {
            _epmRepository = epmRepository;
        }

        public IEnumerable<Weeks> GetWeeks(int SEASON)
        {
            string sql = string.Format(@"SELECT DISTINCT PL.WEEK,YEAR FROM FDEIT005.EPM_PRODUCTION_PLAN PL
INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H ON PL.HEADER_ID=H.ID WHERE H.SEASON={0} ORDER BY YEAR,WEEK", SEASON);

            return _epmRepository.DeserializeList<Weeks>(sql);
        }

        public IEnumerable<string> GetModels(int SEASON, int YEAR, int WEEK)
        {
            string sql = string.Format(@"
SELECT DISTINCT H.MODEL FROM EPM_MASTER_PRODUCTION_H H
INNER JOIN EPM_PRODUCTION_PLAN PL ON PL.HEADER_ID =H.ID WHERE H.SEASON={0} AND PL.WEEK={1} AND PL.YEAR={2}", SEASON,WEEK,YEAR);

            return _epmRepository.DeserializeList<string>(sql);
        }
    }
}
