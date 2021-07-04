﻿using EPM.Core.FormModels.UretimIzle;
using EPM.Core.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Services
{
    public class UretimIzleService : IUretimIzleService
    {

        public IEnumerable<Weeks> GetWeeks(int SEASON)
        {
            string sql = string.Format(@"SELECT DISTINCT PL.WEEK,YEAR FROM FDEIT005.EPM_PRODUCTION_PLAN PL
INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H ON PL.HEADER_ID=H.ID WHERE H.SEASON={0} ORDER BY YEAR,WEEK", SEASON);

            return OracleServer.DeserializeList<Weeks>(sql);
        }

        public IEnumerable<string> GetModels(int SEASON, int YEAR, int WEEK)
        {
            string sql = string.Format(@"
SELECT DISTINCT H.MODEL FROM EPM_MASTER_PRODUCTION_H H
INNER JOIN EPM_PRODUCTION_PLAN PL ON PL.HEADER_ID =H.ID WHERE H.SEASON={0} AND PL.WEEK={1} AND PL.YEAR={2}", SEASON,WEEK,YEAR);

            return OracleServer.DeserializeList<string>(sql);
        }
    }
}
