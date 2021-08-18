using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_AQL_INLINE
    {
        [Dapper.Contrib.Extensions.Key]
        public int ID { get; set; }
        public int HEADER_ID { get; set; }
        public int USER_ID { get; set; }
        [Required(ErrorMessage = "PROCESS GEREKLİDİR")]
        public int PROCESS_ID { get; set; }
        [Required(ErrorMessage = "AÇIKLAMA GEREKLİDİR")]
        public string DESCRIPTION { get; set; }
    }
}
