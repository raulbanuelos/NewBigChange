using FilderNET.DataAccess.ServiceObjects;
using FilderNET.Web.Models;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilderNET.Web.Controllers
{
    public class BonoController : Controller
    {
        
        public ActionResult CargarBono()
        {
            return View();
        }

        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Files/Bono/"), fileName);
                file.SaveAs(path);

                List<DO_Bono> lBonoActual = DataManager.GetBono();
                SLDocument sl = new SLDocument(path);

                List<DO_Bono> listaBono = new List<DO_Bono>();

                int iRow = 2;
                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow,1)))
                {
                    DO_Bono bono = new DO_Bono();

                    bono.Quincena = sl.GetCellValueAsDateTime(iRow, 1);
                    bono.Rubro = sl.GetCellValueAsString(iRow, 2);
                    bono.Folio = sl.GetCellValueAsString(iRow, 3);
                    bono.ImporteEfectividad = sl.GetCellValueAsDouble(iRow, 4);
                    bono.ImporteComercio = sl.GetCellValueAsDouble(iRow, 5);
                    bono.ImporteCalidad = sl.GetCellValueAsDouble(iRow, 6);

                    listaBono.Add(bono);
                    
                    iRow++;
                }

                int lotes = 3000;
                int row = 1;
                try
                {
                    using (var Conexion = new EntitiesFilder())
                    {
                        foreach (var item in listaBono)
                        {
                            TBL_NET_BONO_HISTORICO historico = new TBL_NET_BONO_HISTORICO();

                            historico.QUINCENA = item.Quincena;
                            historico.RUBRO = item.Rubro;
                            historico.FOLIO = item.Folio;
                            historico.IMPORTE_EFECTIVIDAD = item.ImporteEfectividad;
                            historico.IMPORTE_COMERCIO = item.ImporteComercio;
                            historico.IMPORTE_CALIDAD = item.ImporteCalidad;

                            Conexion.TBL_NET_BONO_HISTORICO.Add(historico);

                            if (row % lotes == 0)
                                Conexion.SaveChanges();

                            row++;
                        }

                        Conexion.SaveChanges();
                    }

                    List<DO_Bono> listaActualizar = new List<DO_Bono>();
                    List<DO_Bono> listaInsertar = new List<DO_Bono>();

                    foreach (var item in listaBono)
                    {
                        if (lBonoActual.Where(x=> x.Folio == item.Folio).ToList().Count > 0)
                        {
                            listaActualizar.Add(item);
                        }else
                        {
                            listaInsertar.Add(item);
                        }
                    }

                    

                    row = 1;
                    using (var Conexion = new EntitiesFilder())
                    {
                        foreach (var item in listaInsertar)
                        {
                            TBL_NET_BONO bono = new TBL_NET_BONO();
                            bono.QUINCENA = item.Quincena;
                            bono.RUBRO = item.Rubro;
                            bono.FOLIO = item.Folio;
                            bono.IMPORTE_CALIDAD = item.ImporteCalidad;
                            bono.IMPORTE_COMERCIO = item.ImporteComercio;
                            bono.IMPORTE_EFECTIVIDAD = item.ImporteEfectividad;

                            Conexion.TBL_NET_BONO.Add(bono);

                            if (row % lotes == 0)
                                Conexion.SaveChanges();

                            row++;
                        }
                        Conexion.SaveChanges();
                    }

                    row = 1;
                    using (var Conexion = new EntitiesFilder())
                    {
                        foreach (var item in listaActualizar)
                        {
                            TBL_NET_BONO bono = Conexion.TBL_NET_BONO.Where(x => x.FOLIO == item.Folio).FirstOrDefault();

                            bono.QUINCENA = item.Quincena;
                            bono.RUBRO = item.Rubro;
                            if (bono.IMPORTE_CALIDAD == 0)
                                bono.IMPORTE_CALIDAD = item.ImporteCalidad;

                            if (bono.IMPORTE_COMERCIO == 0)
                                bono.IMPORTE_COMERCIO = item.ImporteComercio;

                            if (bono.IMPORTE_EFECTIVIDAD == 0)
                                bono.IMPORTE_EFECTIVIDAD = item.ImporteEfectividad;

                            Conexion.Entry(bono).State = System.Data.Entity.EntityState.Modified;

                            if (row % lotes == 0)
                                Conexion.SaveChanges();

                            row++;
                        }
                        Conexion.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return RedirectToAction("CargarBono");
        }
    }
}