using FilderNET.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilderNET.Web.Controllers
{
    public class PromotorController : Controller
    {
        // GET: Promotor
        public ActionResult VerPagoIngreso()
        {
            return View(DataManager.GetPagoIngresoPromotor(((ModeloUsuarios)Session["USER_LOGGED"]).USUARIO));
        }

        public ActionResult VerGlobal()
        {
            return View(DataManager.GetAllDataGlobalByPromotor(((ModeloUsuarios)Session["USER_LOGGED"]).USUARIO));
        }
    }
}