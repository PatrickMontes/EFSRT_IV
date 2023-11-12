using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ProyectoIncaKancha.Models;
using ProyectoIncaKancha.Logica;
using System.Web.Security;

namespace ProyectoIncaKancha.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correo, string clave)
        {
            Usuarios objeto = new LO_Usuarios().EncontrarUsuario(correo, clave);

            if (objeto.Nombres != null)
            {


                FormsAuthentication.SetAuthCookie(objeto.Correo, false);

                Session["Usuario"] = objeto;

                return RedirectToAction("ListaProductos", "Inventario");
            }



            return View();
        }

    }
}