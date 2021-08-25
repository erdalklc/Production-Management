using EPM.Dto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EPM.Service.Base
{
    public interface IUretimService
    {  

        public object[] UretimListesiAktarExcelYukle(HttpRequest request); 
    }
}
