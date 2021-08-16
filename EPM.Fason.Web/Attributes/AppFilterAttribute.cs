using EPM.Fason.Dto.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing; 

namespace EPM.Fason.Web.Attributes
{
    public class AppFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ActionName;
            var user = new CookieHelper().GetUserFromCookie(filterContext.HttpContext);
            if (actionName == "Login" || actionName == "LoginInspector" || actionName == "LoginKontrol")
            {
                if (actionName == "LogOut" || actionName == "Login" || actionName == "LoginInspector")
                    new CookieHelper().DeleteUserCookie(filterContext.HttpContext);
            }
            else
            {
                if (user == null || user.USER_ID == 0)
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Login" } });
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //string actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ActionName;
            //if (actionName == "Login" || actionName == "LoginKontrol")
            //{
            //    if (actionName == "LogOut" || actionName == "Login")
            //        new CookieHelper().DeleteUserCookie(filterContext.HttpContext);
            //}
            //else
            //{
            //    var user = new CookieHelper().GetUserFromCookie(filterContext.HttpContext);
            //    if (user == null || (user.FIRMA_ID == 0 && user.USER_ID == 0))
            //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Login" } });
            //}
            //base.OnActionExecuted(filterContext);
        }

    }
}
