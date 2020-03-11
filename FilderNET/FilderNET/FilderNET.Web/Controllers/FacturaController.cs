using FilderNET.DataAccess.ServiceObjects;
using FilderNET.Web.Models;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilderNET.Web.Controllers
{
    public class FacturaController : Controller
    {
        public ActionResult CargarFactura()
        {
            return View();
        }

        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (file.ContentLength >0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Files/Factura"), fileName);
                file.SaveAs(path);

                List<DO_Factura> facturaActual = DataManager.GetFactura();

                SLDocument sl = new SLDocument(path);
                List<DO_Factura> listaFactura = new List<DO_Factura>();

                int iRow = 2;
                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                {
                    DO_Factura factura = new DO_Factura();
                    factura.Quincena = sl.GetCellValueAsDateTime(iRow, 2);
                    factura.Folio = sl.GetCellValueAsString(iRow, 3);
                    factura.ImporteBase = sl.GetCellValueAsDouble(iRow, 4);
                    factura.ImportePosteo = sl.GetCellValueAsDouble(iRow, 5);
                    factura.PisaE = sl.GetCellValueAsString(iRow, 6);
                    factura.Procede2 = sl.GetCellValueAsString(iRow, 7);
                    factura.Pago2 = sl.GetCellValueAsString(iRow, 8);
                    factura.Fecha = sl.GetCellValueAsString(iRow, 9);
                    factura.OSPago = sl.GetCellValueAsString(iRow, 10);
                    factura.Estatus = sl.GetCellValueAsString(iRow, 11);
                    factura.LineaContratada = sl.GetCellValueAsString(iRow, 12);
                    factura.Paquete = sl.GetCellValueAsString(iRow, 13);

                    listaFactura.Add(factura);
                    iRow++;
                }

                int lotes = 3000;
                int row = 1;
                try
                {
                    using (var Conexion = new EntitiesFilder())
                    {
                        foreach (var item in listaFactura)
                        {
                            TBL_NET_HISTORICO_FACTURA historico = new TBL_NET_HISTORICO_FACTURA();
                            historico.QUINCENA = item.Quincena;
                            historico.FOLIO_SIAC = item.Folio;
                            historico.IMPORTE_BASE = item.ImporteBase;
                            historico.IMPORTE_POSTEO = item.ImportePosteo;
                            historico.PISA_E = item.PisaE;
                            historico.PROCEDE2 = item.Procede2;
                            historico.PAGO2 = item.Pago2;
                            historico.FECHA = item.Fecha;
                            historico.OS_PAGO = item.OSPago;
                            historico.ESTATUS = item.Estatus;
                            historico.LINEA_CONTRATADA = item.LineaContratada;
                            historico.PAQUETE = item.Paquete;

                            Conexion.TBL_NET_HISTORICO_FACTURA.Add(historico);

                            if (row % lotes == 0)
                                Conexion.SaveChanges();

                            row++;
                        }

                        Conexion.SaveChanges();
                    }

                    List<DO_Factura> listaActualizar = new List<DO_Factura>();
                    List<DO_Factura> listaInsertar = new List<DO_Factura>();

                    foreach (var item in listaFactura)
                    {
                        if (facturaActual.Where(x => x.Folio == item.Folio).ToList().Count > 0)
                        {
                            listaActualizar.Add(item);
                        }
                        else
                        {
                            listaInsertar.Add(item);
                        }
                    }

                    row = 1;
                    using (var Conexion = new EntitiesFilder())
                    {
                        foreach (var item in listaInsertar)
                        {
                            TBL_NET_FACTURA factura = new TBL_NET_FACTURA();
                            factura.QUINCENA = item.Quincena;
                            factura.FOLIO_SIAC = item.Folio;
                            factura.IMPORTE_BASE = item.ImporteBase;
                            factura.IMPORTE_POSTEO = item.ImportePosteo;
                            factura.PISA_E = item.PisaE;
                            factura.PROCEDE2 = item.Procede2;
                            factura.PAGO2 = item.Pago2;
                            factura.FECHA = item.Fecha;
                            factura.OS_PAGO = item.OSPago;
                            factura.ESTATUS = item.Estatus;
                            factura.LINEA_CONTRATADA = item.LineaContratada;
                            factura.PAQUETE = item.Paquete;

                            Conexion.TBL_NET_FACTURA.Add(factura);

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
                            TBL_NET_FACTURA factura = Conexion.TBL_NET_FACTURA.Where(x => x.FOLIO_SIAC == item.Folio).FirstOrDefault();

                            factura.QUINCENA = item.Quincena;
                            factura.IMPORTE_BASE = item.ImporteBase;
                            factura.IMPORTE_POSTEO = item.ImportePosteo;
                            factura.PISA_E = item.PisaE;
                            factura.PROCEDE2 = item.Procede2;
                            factura.PAGO2 = item.Pago2;
                            factura.FECHA = item.Fecha;
                            factura.OS_PAGO = item.OSPago;
                            factura.ESTATUS = item.Estatus;
                            factura.LINEA_CONTRATADA = item.LineaContratada;
                            factura.PAQUETE = item.Paquete;

                            Conexion.Entry(factura).State = EntityState.Modified;

                            if (row % lotes == 0)
                                Conexion.SaveChanges();

                            row++;
                        }

                        Conexion.SaveChanges();
                    }
                }
                catch (Exception er)
                {
                    string a = er.Message;
                }
            }

            return RedirectToAction("CargarFactura");

        }
    }
}