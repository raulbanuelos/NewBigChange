using FilderNET.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilderNET.Web.Controllers
{
    public class SupervisorController : Controller
    {
        public ActionResult GetPago()
        {
            string nombreCompleto = ((ModeloUsuarios)Session["USER_LOGGED"]).NOMBRE + " " + ((ModeloUsuarios)Session["USER_LOGGED"]).APELLIDO_PATERNO + " " + ((ModeloUsuarios)Session["USER_LOGGED"]).APELLIDO_MATERNO;

            return View(DataManager.GetPagoSupervisor(((ModeloUsuarios)Session["USER_LOGGED"]).ID_USUARIO,nombreCompleto));
        }

        public ActionResult GetGlobalSupervisor()
        {
            return View(DataManager.GetAllDataGlobalSupervisor(((ModeloUsuarios)Session["USER_LOGGED"]).ID_USUARIO));
        }
    }
}