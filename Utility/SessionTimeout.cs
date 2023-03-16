using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSJu.Utility
{
    public class SessionTimeout : ActionFilterAttribute
    {
       
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (context.HttpContext.Session == null ||
                             !context.HttpContext.Session.TryGetValue("User", out byte[] val))
            {
                context.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Login",
                        action = "LogOut"
                    }));
            }
            //else if (context.HttpContext.Session == null ||
            //                 !context.HttpContext.Session.TryGetValue("atype", out byte[] _val))
            //{
            //    context.Result =
            //       new RedirectToRouteResult(new RouteValueDictionary(new
            //       {
            //           controller = "account",
            //           action = "index"
            //       }));
            //}
            //else if (AuditService.LoginResp.UserId == 0)
            //{
            //    context.Result =
            //      new RedirectToRouteResult(new RouteValueDictionary(new
            //      {
            //          controller = "Account",
            //          action = "Logout"
            //      }));
            //}
            base.OnActionExecuting(context);
        }
    }
}
