using FilderNET.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilderNET.Web.Controllers
{
    public class GlobalController : Controller
    {
        // GET: Global
        public ActionResult Index()
        {
            return View(DataManager.GetAllDataGlobal());
        }

        public ActionResult EditGlobal(int ID_GLOBAL)
        {
            return View(DataManager.GetDataFolioSIACGlobal(ID_GLOBAL));
        }

        public ActionResult DeleteGlobal(int idGlobal)
        {
            DataManager.DeleteDataGlobal(idGlobal);

            return RedirectToAction("Index", "Global");
        }
        
        public ActionResult GuardarGlobal([Bind(Include = "ID_GLOBAL,ID_PROMOTOR,FECHA_ELABORACION,FILDER,FECHA_CAPTURA,MESA_CONTROL,NOMBRE_CLIENTE,TEL_CONTACTO,SERVICIO,TIPO,PAQUETE,FOLIO_SIAC,ESTATUS_PAGO_INGRESO,ESTATUS_PAGO_POSTEO,OBSERVACIONES")] ModeloGlobal modeloGlobal)
        {
            if (DataManager.UpdateDataGlobal(modeloGlobal) > 0)
            {
                DataManager.InsertLog(((ModeloUsuarios)Session["USER_LOGGED"]).NOMBRE_COMPLETO, "Cambio de registro en id_global:" + modeloGlobal.ID_GLOBAL);
                return View("Index", DataManager.GetAllDataGlobal());
            }
            else
            {
                return View("EditGlobal", modeloGlobal);
            }
        }

        public ActionResult NewGlobal()
        {
            ViewBag.PROMOTORES = convert(DataManager.GetAllUsuarios());

            ViewBag.SERVICIOS = DataManager.GetAllServicios();

            ViewBag.TIPOS = DataManager.GetAllTipo();

            ViewBag.PAQUETES = DataManager.GetAllPaquetes();

            return View();
        }
        
        //public ActionResult SaveNewGlobal([Bind(Include = "FECHA_ELABORACION,FILDER,FECHA_CAPTURA,MESA_CONTROL,NOMBRE_CLIENTE,TEL_CONTACTO,SERVICIO,TIPO,PAQUETE,FOLIO_SIAC,PROMOTOR_SELECTED,SERVICIO_SELECTED,TIPO_SELECTED,PAQUETE_SELECTED,OBSERVACIONES")] ModeloGlobal modeloGlobal)
        public ActionResult SaveNewGlobal(string fechaElaboracion, string filder, string fechaCaptura, string mesaControl, string nombreCliente, string telefono, string servicio, string tipo, string paquete, string promotor, string folioSIAC, string observaciones)
        {
            ModeloGlobal modeloGlobal = new ModeloGlobal();
            modeloGlobal.FECHA_ELABORACION = Convert.ToDateTime(fechaElaboracion);
            modeloGlobal.FILDER = filder;
            modeloGlobal.FECHA_CAPTURA = Convert.ToDateTime(fechaCaptura);
            modeloGlobal.MESA_CONTROL = mesaControl;
            modeloGlobal.NOMBRE_CLIENTE = nombreCliente;
            modeloGlobal.TEL_CONTACTO = telefono;
            modeloGlobal.SERVICIO = servicio;
            modeloGlobal.TIPO = tipo;
            modeloGlobal.PAQUETE = paquete;
            modeloGlobal.ID_PROMOTOR = Convert.ToInt32(promotor);
            modeloGlobal.FOLIO_SIAC = folioSIAC;
            modeloGlobal.OBSERVACIONES = observaciones;

            bool ExistsFolioSIAC = false;

            if (!string.IsNullOrEmpty(modeloGlobal.FOLIO_SIAC))
                ExistsFolioSIAC = DataManager.ExistsFolioSIAC(modeloGlobal.FOLIO_SIAC);
            
            if (ExistsFolioSIAC)
            {
                return Json("FolioSIACError", JsonRequestBehavior.AllowGet);
            }

            if (DataManager.SetNewDataGlobal(modeloGlobal) > 0)
            {
                DataManager.InsertLog(((ModeloUsuarios)Session["USER_LOGGED"]).NOMBRE_COMPLETO, "Se da de alta un nuevo registro en global, FOLIO SIAC:" + modeloGlobal.FOLIO_SIAC + ", NOMBRE CLIENTE:" + modeloGlobal.NOMBRE_CLIENTE);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            //modeloGlobal.ID_PROMOTOR = Convert.ToInt32(modeloGlobal.PROMOTOR_SELECTED);
            //modeloGlobal.SERVICIO = modeloGlobal.SERVICIO_SELECTED;

            //if (DataManager.SetNewDataGlobal(modeloGlobal) > 0)
            //{
            //    return View("Index", DataManager.GetAllDataGlobal());
            //}
            //else
            //{
            //    return View("NewGlobal", modeloGlobal);
            //}

            
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

            return selectListItems.OrderBy(x => x.Text).ToList();
        }

        [HttpPost]
        public ActionResult UploadFiles([Bind(Include = "ID_GLOBAL,FOLIO_SIAC")] ModeloGlobal modeloGlobal)
        {
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var ext = Path.GetExtension(file.FileName);
                        var pathDirectory = Path.Combine(Server.MapPath("~/Files/Global/" + modeloGlobal.FOLIO_SIAC + "/"));

                        var path = Path.Combine(Server.MapPath("~/Files/Global/" + modeloGlobal.FOLIO_SIAC + "/"), filename);

                        if (!Directory.Exists(pathDirectory))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(pathDirectory);
                        }

                        file.SaveAs(path);
                        DataManager.InsertArchivoGlobal(ext, modeloGlobal.ID_GLOBAL, filename, "~/Files/Global/" + modeloGlobal.FOLIO_SIAC + "/" + filename);
                    }

                }
            }
            return View("EditGlobal", modeloGlobal.FOLIO_SIAC);
        }
    }
}