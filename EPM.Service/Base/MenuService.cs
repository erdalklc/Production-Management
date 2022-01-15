using EPM.Dto.Base;
using EPM.Dto.Models;
using EPM.Repository.Base;
using EPM.Tools.Extensions;
using EPM.Tools.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic; 
using System.Text;

namespace EPM.Service.Base
{
    public class MenuService :IMenuService
    {
        private readonly IEPMRepository _epmRepository;
        private readonly ICacheService _cacheService;
        public MenuService(IEPMRepository epmRepository, ICacheService cacheService)
        {
            _epmRepository = epmRepository;
            _cacheService = cacheService;
        }

        public List<Menu> GetMenuList(HttpContext context)
        {
            List<Menu> menu = new List<Menu>();
            menu = _cacheService.Get<List<Menu>>(6, "MENU");
            if (menu == null)
            {
                menu = _epmRepository.DeserializeList<Menu>("SELECT * FROM FDEIT005.EPM_WEB_MENU");
                _cacheService.Add(6, "MENU", menu);
            }
            try
            { 
                List<EPM_USER_RESPONSIBILITY> res = new CookieHelper().GetObjectFromCookie<List<EPM_USER_RESPONSIBILITY>>(context, "EPM_USER_RESPONSIBILITY");
                foreach (var item in menu)
                {
                    if (item.CATEGORY_ID.IntParse() > 0)
                    {
                        if (res.FindAll(ob => ob.RESPONSIBILITY_ID == item.RESPONSIBILITYS.IntParse()).Count > 0)
                            item.ISVISIBLE = true;
                        else item.ISVISIBLE = false;
                    }
                    else item.ISVISIBLE = true;
                }

                var list = menu.FindAll(ob => ob.CATEGORY_ID.IntParse() == 0);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (menu.FindAll(ob => ob.CATEGORY_ID == item.ID && ob.ISVISIBLE).Count == 0)
                        {
                            menu.Find(ob => ob.ID == item.ID).ISVISIBLE = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }


            return menu;
        }

        public bool CanUserEditPlan(HttpContext context)
        {
            bool yes = true;
            List<EPM_USER_RESPONSIBILITY> res = new CookieHelper().GetObjectFromCookie<List<EPM_USER_RESPONSIBILITY>>(context, "EPM_USER_RESPONSIBILITY");

            if (res.FindAll(ob => ob.RESPONSIBILITY_ID == 1).Count > 0)
                yes = true;
            else yes = false;

            return yes;
        }

        public bool CanUserEditUretim(HttpContext context)
        {
            bool yes = true;
            List<EPM_USER_RESPONSIBILITY> res = new CookieHelper().GetObjectFromCookie<List<EPM_USER_RESPONSIBILITY>>(context, "EPM_USER_RESPONSIBILITY");

            if (res.FindAll(ob => ob.RESPONSIBILITY_ID == 62).Count > 0)
                yes = true;
            else yes = false;

            return yes;
        }

        public bool CanUserEditUretimTakip(HttpContext context)
        {
            bool yes = true;
            List<EPM_USER_RESPONSIBILITY> res = new CookieHelper().GetObjectFromCookie<List<EPM_USER_RESPONSIBILITY>>(context, "EPM_USER_RESPONSIBILITY");

            if (res.FindAll(ob => ob.RESPONSIBILITY_ID == 4).Count > 0)
                yes = true;
            else yes = false;

            return yes;
        }

        public bool OnayliKullanici(HttpContext context)
        {
            bool yes = true;
            List<EPM_USER_RESPONSIBILITY> res = new CookieHelper().GetObjectFromCookie<List<EPM_USER_RESPONSIBILITY>>(context, "EPM_USER_RESPONSIBILITY");

            if (res.FindAll(ob => ob.RESPONSIBILITY_ID == 81).Count > 0)
                yes = true;
            else yes = false;

            return yes;
        }

        public bool CanUserEnterTools(HttpContext context)
        {
            bool yes = true;
            List<EPM_USER_RESPONSIBILITY> res = new CookieHelper().GetObjectFromCookie<List<EPM_USER_RESPONSIBILITY>>(context, "EPM_USER_RESPONSIBILITY");

            if (res.FindAll(ob => ob.RESPONSIBILITY_ID == 41).Count > 0)
                yes = true;
            else yes = false;

            return yes;
        }

        public bool CanEditPlan(HttpContext context)
        {
            bool yes = true;
            List<EPM_USER_RESPONSIBILITY> res = new CookieHelper().GetObjectFromCookie<List<EPM_USER_RESPONSIBILITY>>(context, "EPM_USER_RESPONSIBILITY");

            if (res.FindAll(ob => ob.RESPONSIBILITY_ID == 1).Count > 0)
                yes = true;
            else yes = false;

            return yes;
        }
    }
}
