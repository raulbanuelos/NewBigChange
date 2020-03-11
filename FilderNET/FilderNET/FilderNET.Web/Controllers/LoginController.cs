using FilderNET.Web.Models;
using System.Web.Mvc;

namespace FilderNET.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginNET()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ingresar([Bind(Include = "Usuario,Contrasena")] DO_Usuario persona)
        {
            ModeloUsuarios modelo = DataManager.GetLogin(persona.Usuario, persona.Contrasena);
            if (modelo == null)
            {
                DataManager.InsertLog(persona.Usuario, "Intento de accesar a la aplicación fallido");
                return View("LoginNET");
            } 
            else
            {
                Session["USER_LOGGED"] = modelo;
                DataManager.InsertLog(modelo.NOMBRE_COMPLETO, "Se ingresa a la aplicación");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}