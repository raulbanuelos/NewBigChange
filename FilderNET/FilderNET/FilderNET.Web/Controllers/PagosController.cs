using FilderNET.Web.Models;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilderNET.Web.Controllers
{
    public class PagosController : Controller
    {
        public ActionResult VerPagoIngreso()
        {

            List<DO_Pago> dO_PagosSupervisores =  DataManager.GetPagoSupervisores();
            List<DO_Pago> dO_PagosPromotores = DataManager.GetPagoIngresoGeneral();

            List<DO_Pago> dO_Pagos = new List<DO_Pago>();
            foreach (DO_Pago item in dO_PagosPromotores)
            {
                dO_Pagos.Add(item);
            }

            foreach (DO_Pago item in dO_PagosSupervisores)
            {
                dO_Pagos.Add(item);
            }

            dO_Pagos = dO_Pagos.OrderBy(x => x.NOMBRE_PROMOTOR).ToList();

            return View(dO_Pagos);
        }

        public JsonResult Pagar()
        {
            int y = DataManager.SetPagoPromotor(out List<DO_Pago> pagos);

            var jsonResult = Json(y, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            DataManager.InsertLog(((ModeloUsuarios)Session["USER_LOGGED"]).NOMBRE_COMPLETO, "Se realiza el pago semanal a promotores y supervisores.");

            return jsonResult;
        }

        public FileResult DownloadCaratula()
        {
            List<DO_Pago> dO_PagosPromotor = DataManager.GetPagoIngresoGeneral();
            List<DO_Pago> dO_PagosPSupervisor = DataManager.GetPagoSupervisores();

            List<DO_Pago> dO_Pagos = new List<DO_Pago>();

            foreach (DO_Pago pago in dO_PagosPromotor)
            {
                dO_Pagos.Add(pago);
            }

            foreach (DO_Pago pago in dO_PagosPSupervisor)
            {
                dO_Pagos.Add(pago);
            }

            var queryNames = from a in dO_Pagos
                             group a by a.NOMBRE_PROMOTOR into newGroup
                             orderby newGroup.Key
                             select newGroup;

            var ar = queryNames.ToArray();
            string[] vs = new string[ar.Length];

            for (int i = 0; i < ar.Length; i++)
            {
                vs[i] = ar[i].Key;
            }

            SLDocument sLDocument = new SLDocument();

            for (int i = 0; i < vs.Length; i++)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("NOMBRE_CLIENTE", typeof(string));
                dataTable.Columns.Add("SERVICIO", typeof(string));
                dataTable.Columns.Add("TIPO_LINEA", typeof(string));
                dataTable.Columns.Add("PAQUETE", typeof(string));
                dataTable.Columns.Add("NOMBRE_PROMOTOR", typeof(string));
                dataTable.Columns.Add("FOLIO SIAC", typeof(string));
                dataTable.Columns.Add("INGRESO", typeof(double));
                dataTable.Columns.Add("PAGO POR", typeof(string));

                List<DO_Pago> pagoByUsuario = dO_Pagos.Where(x => x.NOMBRE_PROMOTOR == vs[i]).ToList();

                double montoTotal = 0;

                foreach (DO_Pago pago in pagoByUsuario)
                {
                    dataTable.Rows.Add(pago.NOMBRE_CLIENTE, pago.SERVICIO, pago.TIPO_LINEA, pago.PAQUETE, pago.NOMBRE_PROMOTOR,
                        pago.FOLIO_SIAC, pago.INGRESO, pago.CONCEPTO);
                    montoTotal += pago.INGRESO;
                }

                dataTable.Rows.Add("", "", "", "", "",
                        "TOTAL", montoTotal, "");

                sLDocument.AddWorksheet(vs[i]);

                sLDocument.ImportDataTable(1, 1, dataTable, true);
            }

            string path = Server.MapPath("~/Files/PIPES/") + "CARATULA_NOMINA" + DateTime.Now.Year + " " + DateTime.Now.Month + " " + DateTime.Now.Day + " " + DateTime.Now.Hour + " " + DateTime.Now.Minute + " " + DateTime.Now.Second + ".xlsx";
            sLDocument.SaveAs(path);

            DataManager.InsertLog(((ModeloUsuarios)Session["USER_LOGGED"]).NOMBRE_COMPLETO, "Se crea la caratula en excel.");

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "CARATULA_NOMINA.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            
        }
    }
}