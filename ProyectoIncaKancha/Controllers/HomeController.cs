using ProyectoIncaKancha.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProyectoIncaKancha.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [PermisosRol(Models.Rol.Administrador)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SinPermiso()
        {
            ViewBag.Message = "Usted no tiene permiso para ver esta pagina";

            return View();
        }

        public ActionResult CerrarSesion()
        {


            FormsAuthentication.SignOut();
            Session["Usuario"] = null;


            return RedirectToAction("Login", "Acceso");
        }
    }
}