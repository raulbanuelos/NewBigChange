using FilderNET.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilderNET.Web.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Personal
        public ActionResult Index()
        {
            return View(DataManager.GetAllUsuarios());
        }

        public ActionResult AltaPersona()
        {
            ViewBag.PERSONAL = convert(DataManager.GetAllUsuarios());
            CargarJerarquias();
            
            return View();
        }

        private void CargarJerarquias()
        {
            List<SelectListItem> selectListItemsJerarquia = new List<SelectListItem>();

            selectListItemsJerarquia.Add(new SelectListItem { Value = "1", Text = "PROMOTOR" });
            selectListItemsJerarquia.Add(new SelectListItem { Value = "2", Text = "SUPERVISOR" });
            selectListItemsJerarquia.Add(new SelectListItem { Value = "3", Text = "LIDER" });

            if ((bool)Session["BAN_MASTER"] || (bool)Session["BAN_SISTEMAS"] || (bool)Session["BAN_DUENO"])
            {
                selectListItemsJerarquia.Add(new SelectListItem { Value = "4", Text = "MASTER" });
                selectListItemsJerarquia.Add(new SelectListItem { Value = "7", Text = "ADMINISTRATIVO" });
            }

            if ((bool)Session["BAN_SISTEMAS"])
            {
                selectListItemsJerarquia.Add(new SelectListItem { Value = "6", Text = "SISTEMAS" });
                selectListItemsJerarquia.Add(new SelectListItem { Value = "5", Text = "DUEÑO" });
            }
            ViewBag.JERARQUIAS = selectListItemsJerarquia;
        }

        /// <summary>
        /// Método que genera una cadena aleatorea del un tamaño deceado.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string GetRandomString(int length)
        {
            Random random = new Random();
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPost]
        //public ActionResult SavePersona([Bind(Include = "USUARIO,CONTRASENA,APELLIDO_PATERNO,APELLIDO_MATERNO,NOMBRE,CURP,RFC,TELEFONO,EMAIL,ACTIVO,SUPERVISOR_SELECTED,FECHA_NACIMIENTO")] ModeloUsuarios modeloUsuario)
        public ActionResult SavePersona(ModeloUsuarios modeloUsuario, HttpPostedFileBase somefile)
        {
            string pathPhoto = string.Empty;
            string filename = string.Empty;
            if (somefile != null && somefile.ContentLength > 0)
            {
                filename = Path.GetFileName(somefile.FileName);
                pathPhoto = Path.Combine(Server.MapPath("~/assets/images/users/"), filename);

                if (System.IO.File.Exists(pathPhoto))
                {
                    string[] vec = filename.Split('.');
                    string nombre = vec[0];
                    string ext = vec[1];

                    string rand =GetRandomString(3);
                    filename = nombre + "_" + rand + "." +  ext;
                    pathPhoto = Path.Combine(Server.MapPath("~/assets/images/users/"), filename);
                    while (System.IO.File.Exists(pathPhoto))
                    {
                        rand = GetRandomString(3);
                        filename = nombre + "_" + rand + "." + ext;
                        pathPhoto = Path.Combine(Server.MapPath("~/assets/images/users/"), filename);
                    }
                    
                    MemoryStream memoryStream = new MemoryStream();
                    somefile.InputStream.CopyTo(memoryStream);
                    byte[] data = memoryStream.ToArray();

                    System.IO.File.WriteAllBytes(pathPhoto, data);

                }
                else
                {
                    somefile.SaveAs(pathPhoto);
                }
                
            }

            modeloUsuario.ID_JERARQUIA = Convert.ToInt32(modeloUsuario.JERARQUIA_SELECTED);
            modeloUsuario.ID_JEFE = Convert.ToInt32(modeloUsuario.SUPERVISOR_SELECTED);

            if (!string.IsNullOrWhiteSpace(filename))
            {
                modeloUsuario.FOTO = "~/assets/images/users/" + filename;
            }
            
            int r = DataManager.SetNewUsuario(modeloUsuario);

            if (r>0)
                return View("Index", DataManager.GetAllUsuarios());
            else
                return View("AltaPersona");
        }

        public ActionResult SaveChangePersona([Bind(Include = "ID_USUARIO,ID_JERARQUIA,USUARIO,APELLIDO_PATERNO,APELLIDO_MATERNO,NOMBRE,CURP,RFC,TELEFONO,EMAIL,ACTIVO,SUPERVISOR_SELECTED,FECHA_NACIMIENTO,JERARQUIA_SELECTED")] ModeloUsuarios modeloUsuario)
        {
            modeloUsuario.ID_JEFE = Convert.ToInt32(modeloUsuario.SUPERVISOR_SELECTED);
            modeloUsuario.ID_JERARQUIA = Convert.ToInt32(modeloUsuario.JERARQUIA_SELECTED);
            int r = DataManager.UpdateDataUsuario(modeloUsuario);
            
            if (r > 0)
                return View("Index", DataManager.GetAllUsuarios());
            else
            {
                ModeloUsuarios modelo = DataManager.GetUsuario(modeloUsuario.ID_USUARIO);
                modelo.SUPERVISOR_SELECTED = Convert.ToString(modelo.ID_JEFE);
                modelo.JERARQUIA_SELECTED = Convert.ToString(modelo.ID_JERARQUIA);
                ViewBag.PERSONAL = convert(DataManager.GetAllUsuarios());

                return View("EditPersona", modelo);
            }  
        }
        
        public ActionResult EditPersona(int idUsuario)
        {
            ModeloUsuarios modelo = DataManager.GetUsuario(idUsuario);
            modelo.SUPERVISOR_SELECTED = Convert.ToString(modelo.ID_JEFE);
            modelo.JERARQUIA_SELECTED = Convert.ToString(modelo.ID_JERARQUIA);
            ViewBag.PERSONAL = convert(DataManager.GetAllUsuarios());
            CargarJerarquias();

            return View(modelo);
        }

        private List<SelectListItem> convert(List<ModeloUsuarios> usuarios)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (var item in usuarios)
            {
                SelectListItem selectListItem = new SelectListItem();

                selectListItem.Text = item.NOMBRE + " " + item.APELLIDO_PATERNO + " " + item.APELLIDO_MATERNO;
                selectListItem.Value = item.ID_USUARIO.ToString();
                
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }

        
    }
}