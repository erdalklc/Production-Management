using EPM.Dto.FormModels.UretimIzle;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Service.Base
{
    public interface IUretimIzleService
    {
        public IEnumerable<Weeks> GetWeeks(int SEASON);

        public IEnumerable<string> GetModels(int SEASON, int YEAR, int WEEK);
    }
}
