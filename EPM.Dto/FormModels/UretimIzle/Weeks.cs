using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.FormModels.UretimIzle
{
    public class Weeks
    {
        private string id="";
        public string ID
        {
            get
            {
                if (id == null || id == "")
                    id = Guid.NewGuid().ToString(); 
                return id;
            }
            set { id = value; }

        }
        public int WEEK { get; set; }
        public int YEAR { get; set; }
    }
}
