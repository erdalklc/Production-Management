using EPM.Core.FormModels.UretimIzle;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Services
{
    public interface IUretimIzleService
    {
        public IEnumerable<Weeks> GetWeeks(int SEASON);

        public IEnumerable<string> GetModels(int SEASON, int YEAR, int WEEK);
    }
}
