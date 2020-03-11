using FilderNET.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilderNET.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            InicializarSession((ModeloUsuarios)Session["USER_LOGGED"]);
            return View();
        }

        private void InicializarSession(ModeloUsuarios modeloUsuarios)
        {
            Session["BAN_PROMOTOR"] = modeloUsuarios.ID_JERARQUIA == 1 ? true : false;
            Session["BAN_SUPERVISOR"] = modeloUsuarios.ID_JERARQUIA == 2 ? true : false;
            Session["BAN_LIDER"] = modeloUsuarios.ID_JERARQUIA == 3 ? true : false;
            Session["BAN_MASTER"] = modeloUsuarios.ID_JERARQUIA == 4 ? true : false;
            Session["BAN_DUENO"] = modeloUsuarios.ID_JERARQUIA == 5 ? true : false;
            Session["BAN_SISTEMAS"] = modeloUsuarios.ID_JERARQUIA == 6 ? true : false;
            Session["BAN_ADMINISTRATIVO"] = modeloUsuarios.ID_JERARQUIA == 7 ? true : false;
            Session["NOMBRE_USUARIO"] = modeloUsuarios.NOMBRE;
            Session["FOTO_USUARIO"] = modeloUsuarios.FOTO;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CerrarSession()
        {
            Session.Abandon();

            return RedirectToAction("LoginNET", "Login");
        }
    }
}