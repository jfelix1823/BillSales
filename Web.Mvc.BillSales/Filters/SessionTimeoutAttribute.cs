using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Web.Mvc.BillSales.Models;

namespace Web.Mvc.BillSales.Filters
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private ISession _session => _httpContextAccessor.HttpContext.Session;

        //public SessionTimeoutAttribute(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            ISession _session = _httpContextAccessor.HttpContext.Session;

            try
            {
                var datosUser = _session.GetComplexData<UsuariosViewModel>("UsuariosViewModel");

                if (datosUser == null)
                {
                    filterContext.Result = new RedirectResult("~/Account/Logout");
                    return;
                }

                //if (datosUser.Ent_Parametrizacion_acceso == null)
                //{
                //    filterContext.Result = new RedirectResult("~/Account/Logout");
                //    return;
                //}

                //HttpContext ctx = HttpContext.Current;
                //if (HttpContext.Current.Session["userId"] == null)
                //{
                //    filterContext.Result = new RedirectResult("~/Account/Login");
                //    return;
                //}                
            }
            catch(Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Account/Logout");
                return;
                throw ex;
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }

}
