using EPM.Dto.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Service.Base
{
    public interface IMenuService
    {
        public List<Menu> GetMenuList(HttpContext context);

        public bool CanUserEditPlan(HttpContext context);

        public bool CanUserEditUretim(HttpContext context);

        public bool CanUserEditUretimTakip(HttpContext context);

        public bool OnayliKullanici(HttpContext context);

        public bool CanUserEnterTools(HttpContext context);
    }
}
