using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Accudelta.Web.Models;

namespace Accudelta.Web.Controllers
{
    public partial class BaseController : Controller
    {

        protected JsonResult Json(object data)
        {
            return new JsonResult() { MaxJsonLength = Int32.MaxValue, Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        protected JsonResult Json(bool result, string msg)
        {
            //if (!result)
            //    Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return Json(result, null, msg);
        }
        protected JsonResult Json(bool result, object data)
        {
            return Json(result, data, "");

        }
        protected JsonResult Json(bool result, object data, string msg)
        {
            JsonResponse jresponse = new JsonResponse() { Data = data, Msg = msg, Result = result };
            //MaxJsonLenght para que no devuelve a error JsonSerializer MaxJsonlenght 
            return new JsonResult() { MaxJsonLength = Int32.MaxValue, Data = jresponse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var controller = filterContext.RouteData.Values["controller"];
            var action = filterContext.RouteData.Values["action"];
            var usuario = HttpContext.User.Identity.Name;


            Exception ex = filterContext.Exception;
            string errorResult = String.Empty;
            if (ex.InnerException == null)
                errorResult = ex.Message;
            else
                errorResult = ex.Message + " | " + ex.InnerException.InnerException.Message;


            filterContext.ExceptionHandled = true;
            string path = controller + "/" + action;

            //TODO Save exception log4net

            filterContext.Result = Json(false, errorResult + " | Ruta: " + path + " | Usuario: " + usuario);

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}