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
    public class PIPESController : Controller
    {
        public ActionResult CargarPIPES()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadPIPES()
        {
            string resultado = "";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    
                    var path = Path.Combine(Server.MapPath("~/Files/PIPES/"), filename);
                    
                    file.SaveAs(path);

                    SLDocument sL = new SLDocument(path);

                    List<ModeloFolios> list = new List<ModeloFolios>();
                    list = DataManager.GetAllFolios();

                    List<ModeloFolios> ListaActualizar = new List<ModeloFolios>();
                    List<ModeloFolios> ListaInsertar = new List<ModeloFolios>();

                    try
                    {
                        int row = 2;
                        while (!string.IsNullOrEmpty(sL.GetCellValueAsString(row, 2)))
                        {
                            ModeloFolios modeloFolio = new ModeloFolios();

                            string codigoNominaOperador = sL.GetCellValueAsString(row, 3);

                            modeloFolio.FECHA_CAPTURA = sL.GetCellValueAsDateTime(row, 1);
                            modeloFolio.ESTRATEGIA = sL.GetCellValueAsString(row, 2);
                            modeloFolio.PROMOTOR = sL.GetCellValueAsString(row, 3);
                            modeloFolio.FOLIO_SIAC = sL.GetCellValueAsString(row, 4);
                            modeloFolio.ESTATUS_SIAC = sL.GetCellValueAsString(row, 5);
                            modeloFolio.TIPO_LINEA = sL.GetCellValueAsString(row, 6);
                            modeloFolio.LINEA_CONTRATADA = sL.GetCellValueAsString(row, 7);
                            modeloFolio.AREA = sL.GetCellValueAsString(row, 8);
                            modeloFolio.DIVICION = sL.GetCellValueAsString(row, 9);
                            modeloFolio.TIENDA = sL.GetCellValueAsString(row, 10);
                            modeloFolio.PAQUETE = sL.GetCellValueAsString(row, 11);
                            modeloFolio.OBSERVACIONES = sL.GetCellValueAsString(row, 12);
                            modeloFolio.RESPUESTA_TELMEX = sL.GetCellValueAsString(row, 13);
                            modeloFolio.MOTIVO_RECHAZO = sL.GetCellValueAsString(row, 14);
                            modeloFolio.TELEFONO_ASIGNADO = sL.GetCellValueAsString(row, 15);
                            modeloFolio.TELEFONO_PORTADO = sL.GetCellValueAsString(row, 16);
                            modeloFolio.OS_ALTA_LINEA_MULTIORDEN = sL.GetCellValueAsString(row, 17);
                            modeloFolio.FECHA_OS_ALTA_LINEA_MULTIORDEN = sL.GetCellValueAsString(row, 18);
                            modeloFolio.ORDEN_SERVICIO_TV = sL.GetCellValueAsString(row, 19);
                            modeloFolio.FECHA_OS_TV = sL.GetCellValueAsString(row, 20);
                            modeloFolio.CAMPANA = sL.GetCellValueAsString(row, 21);
                            modeloFolio.ESTATUS_ATENCION_MULTIORDEN = sL.GetCellValueAsString(row, 22);
                            modeloFolio.ESTATUS_PISA_MULTIORDEN = sL.GetCellValueAsString(row, 23);
                            modeloFolio.PISA_OS_FECHA_POSTEO_MULTIORDEN = sL.GetCellValueAsString(row, 24);
                            modeloFolio.ESTATUS_PISA_TV = sL.GetCellValueAsString(row, 25);
                            modeloFolio.PISA_OS_FECHA_POSTEO_TV = sL.GetCellValueAsString(row, 26);
                            modeloFolio.FECHA_CAMBIO_ESTATUS_SIAC = sL.GetCellValueAsString(row, 27);
                            modeloFolio.CLAVE_EMPRESA = sL.GetCellValueAsString(row, 28);
                            modeloFolio.NOMBRE_EMPRESA = sL.GetCellValueAsString(row, 29);
                            modeloFolio.SERVICIO_FACTURACION_TERCEROS = sL.GetCellValueAsString(row, 30);
                            modeloFolio.ETAPA_PISA_MULTIORDEN = sL.GetCellValueAsString(row, 31);
                            modeloFolio.ETAPA_PISA_TV = sL.GetCellValueAsString(row, 32);
                            modeloFolio.ETIQUETA_TRAFICO_VOZ = sL.GetCellValueAsString(row, 33);
                            modeloFolio.TRAFICO_VOZ_ENTRANTE = sL.GetCellValueAsString(row, 34);
                            modeloFolio.TRAFICO_VOZ_SALIENTE = sL.GetCellValueAsString(row, 35);
                            modeloFolio.FECHA_TRAFICO_VOZ = sL.GetCellValueAsString(row, 36);
                            modeloFolio.TRAFICO_DATOS = sL.GetCellValueAsString(row, 37);
                            modeloFolio.FECHA_TRAFICO_DATOS = sL.GetCellValueAsString(row, 38);
                            modeloFolio.FECHA_FACTURCION = sL.GetCellValueAsString(row, 39);
                            modeloFolio.DESCRIPCION_VALIDA_ADEUDO = sL.GetCellValueAsString(row, 40);
                            modeloFolio.CORREO_ELECTRONICO = sL.GetCellValueAsString(row, 41);
                            modeloFolio.FECHA_NACIMIENTO = sL.GetCellValueAsString(row, 42);
                            modeloFolio.ID = sL.GetCellValueAsString(row, 43);
                            modeloFolio.TERMINAL = sL.GetCellValueAsString(row, 44);
                            modeloFolio.DISTRITO = sL.GetCellValueAsString(row, 45);
                            modeloFolio.TECELULAR = sL.GetCellValueAsString(row, 46);

                            //No estan en base de datos.
                            //Faltan los que no están en base de datos.

                            if (list.Where(x => x.FOLIO_SIAC == modeloFolio.FOLIO_SIAC).ToList().Count == 0)
                                ListaInsertar.Add(modeloFolio);
                            else
                                ListaActualizar.Add(modeloFolio);

                            row++;
                        }

                        //Insertamos los registros.
                        int lotes = 3000;
                        row = 1;

                        using (var db = new EntitiesFilder())
                        {
                            foreach (ModeloFolios folio in ListaInsertar)
                            {
                                TBL_FOLIOS tBL_FOLIOS = new TBL_FOLIOS();

                                tBL_FOLIOS.FECHA_CAPTURA = folio.FECHA_CAPTURA;
                                tBL_FOLIOS.ESTRATEGIA = folio.ESTRATEGIA;
                                tBL_FOLIOS.PROMOTOR = folio.PROMOTOR;
                                tBL_FOLIOS.FOLIO_SIAC = folio.FOLIO_SIAC;
                                tBL_FOLIOS.ESTATUS_SIAC = folio.ESTATUS_SIAC;
                                tBL_FOLIOS.TIPO_LINEA = folio.TIPO_LINEA;
                                tBL_FOLIOS.LINEA_CONTRATADA = folio.LINEA_CONTRATADA;
                                tBL_FOLIOS.AREA = folio.AREA;
                                tBL_FOLIOS.DIVICION = folio.DIVICION;
                                tBL_FOLIOS.TIENDA = folio.TIENDA;
                                tBL_FOLIOS.PAQUETE = folio.PAQUETE;
                                tBL_FOLIOS.OBSERVACIONES = folio.OBSERVACIONES;
                                tBL_FOLIOS.RESPUESTA_TELMEX = folio.RESPUESTA_TELMEX;
                                tBL_FOLIOS.MOTIVO_RECHAZO = folio.MOTIVO_RECHAZO;
                                tBL_FOLIOS.TELEFONO_ASIGNADO = folio.TELEFONO_ASIGNADO;
                                tBL_FOLIOS.TELEFONO_PORTADO = folio.TELEFONO_PORTADO;
                                tBL_FOLIOS.OS_ALTA_LINEA_MULTIORDEN = folio.OS_ALTA_LINEA_MULTIORDEN;
                                tBL_FOLIOS.FECHA_OS_ALTA_LINEA_MULTIORDEN = folio.FECHA_OS_ALTA_LINEA_MULTIORDEN;
                                tBL_FOLIOS.ORDEN_SERVICIO_TV = folio.ORDEN_SERVICIO_TV;
                                tBL_FOLIOS.FECHA_OS_TV = folio.FECHA_OS_TV;
                                tBL_FOLIOS.CAMPANA = folio.CAMPANA;
                                tBL_FOLIOS.ESTATUS_ATENCION_MULTIORDEN = folio.ESTATUS_ATENCION_MULTIORDEN;
                                tBL_FOLIOS.ESTATUS_PISA_MULTIORDEN = folio.ESTATUS_PISA_MULTIORDEN;
                                tBL_FOLIOS.PISA_OS_FECHA_POSTEO_MULTIORDEN = folio.PISA_OS_FECHA_POSTEO_MULTIORDEN;
                                tBL_FOLIOS.ESTATUS_PISA_TV = folio.ESTATUS_PISA_TV;
                                tBL_FOLIOS.PISA_OS_FECHA_POSTEO_TV = folio.PISA_OS_FECHA_POSTEO_TV;
                                tBL_FOLIOS.FECHA_CAMBIO_ESTATUS_SIAC = folio.FECHA_CAMBIO_ESTATUS_SIAC;
                                tBL_FOLIOS.CLAVE_EMPRESA = folio.CLAVE_EMPRESA;
                                tBL_FOLIOS.NOMBRE_EMPRESA = folio.NOMBRE_EMPRESA;
                                tBL_FOLIOS.SERVICIO_FACTURACION_TERCEROS = folio.SERVICIO_FACTURACION_TERCEROS;
                                tBL_FOLIOS.ETAPA_PISA_MULTIORDEN = folio.ETAPA_PISA_MULTIORDEN;
                                tBL_FOLIOS.ETAPA_PISA_TV = folio.ETAPA_PISA_TV;
                                tBL_FOLIOS.ETIQUETA_TRAFICO_VOZ = folio.ETIQUETA_TRAFICO_VOZ;
                                tBL_FOLIOS.TRAFICO_VOZ_ENTRANTE = folio.TRAFICO_VOZ_ENTRANTE;
                                tBL_FOLIOS.TRAFICO_VOZ_SALIENTE = folio.TRAFICO_VOZ_SALIENTE;
                                tBL_FOLIOS.FECHA_TRAFICO_VOZ = folio.FECHA_TRAFICO_VOZ;
                                tBL_FOLIOS.TRAFICO_DATOS = folio.TRAFICO_DATOS;
                                tBL_FOLIOS.FECHA_TRAFICO_DATOS = folio.FECHA_TRAFICO_DATOS;
                                tBL_FOLIOS.FECHA_FACTURCION = folio.FECHA_FACTURCION;
                                tBL_FOLIOS.DESCRIPCION_VALIDA_ADEUDO = folio.DESCRIPCION_VALIDA_ADEUDO;
                                tBL_FOLIOS.CORREO_ELECTRONICO = folio.CORREO_ELECTRONICO;
                                tBL_FOLIOS.FECHA_NACIMIENTO = folio.FECHA_NACIMIENTO;
                                tBL_FOLIOS.ID = folio.ID;
                                tBL_FOLIOS.TERMINAL = folio.TERMINAL;
                                tBL_FOLIOS.DISTRITO = folio.DISTRITO;
                                tBL_FOLIOS.TECELULAR = folio.TECELULAR;

                                db.TBL_FOLIOS.Add(tBL_FOLIOS);

                                if (row % lotes == 0)
                                    db.SaveChanges();

                                row++;
                            }

                            db.SaveChanges();
                        }

                        row = 1;
                        using (var db = new EntitiesFilder())
                        {
                            foreach (var folio in ListaActualizar)
                            {
                                TBL_FOLIOS tBL_FOLIOS = db.TBL_FOLIOS.Where(x => x.FOLIO_SIAC == folio.FOLIO_SIAC).FirstOrDefault();

                                tBL_FOLIOS.FECHA_CAPTURA = folio.FECHA_CAPTURA;
                                tBL_FOLIOS.ESTRATEGIA = folio.ESTRATEGIA;
                                tBL_FOLIOS.PROMOTOR = folio.PROMOTOR;
                                tBL_FOLIOS.ESTATUS_SIAC = folio.ESTATUS_SIAC;
                                tBL_FOLIOS.TIPO_LINEA = folio.TIPO_LINEA;
                                tBL_FOLIOS.LINEA_CONTRATADA = folio.LINEA_CONTRATADA;
                                tBL_FOLIOS.AREA = folio.AREA;
                                tBL_FOLIOS.DIVICION = folio.DIVICION;
                                tBL_FOLIOS.TIENDA = folio.TIENDA;
                                tBL_FOLIOS.PAQUETE = folio.PAQUETE;
                                tBL_FOLIOS.OBSERVACIONES = folio.OBSERVACIONES;
                                tBL_FOLIOS.RESPUESTA_TELMEX = folio.RESPUESTA_TELMEX;
                                tBL_FOLIOS.MOTIVO_RECHAZO = folio.MOTIVO_RECHAZO;
                                tBL_FOLIOS.TELEFONO_ASIGNADO = folio.TELEFONO_ASIGNADO;
                                tBL_FOLIOS.TELEFONO_PORTADO = folio.TELEFONO_PORTADO;
                                tBL_FOLIOS.OS_ALTA_LINEA_MULTIORDEN = folio.OS_ALTA_LINEA_MULTIORDEN;
                                tBL_FOLIOS.FECHA_OS_ALTA_LINEA_MULTIORDEN = folio.FECHA_OS_ALTA_LINEA_MULTIORDEN;
                                tBL_FOLIOS.ORDEN_SERVICIO_TV = folio.ORDEN_SERVICIO_TV;
                                tBL_FOLIOS.FECHA_OS_TV = folio.FECHA_OS_TV;
                                tBL_FOLIOS.CAMPANA = folio.CAMPANA;
                                tBL_FOLIOS.ESTATUS_ATENCION_MULTIORDEN = folio.ESTATUS_ATENCION_MULTIORDEN;
                                tBL_FOLIOS.ESTATUS_PISA_MULTIORDEN = folio.ESTATUS_PISA_MULTIORDEN;
                                tBL_FOLIOS.PISA_OS_FECHA_POSTEO_MULTIORDEN = folio.PISA_OS_FECHA_POSTEO_MULTIORDEN;
                                tBL_FOLIOS.ESTATUS_PISA_TV = folio.ESTATUS_PISA_TV;
                                tBL_FOLIOS.PISA_OS_FECHA_POSTEO_TV = folio.PISA_OS_FECHA_POSTEO_TV;
                                tBL_FOLIOS.FECHA_CAMBIO_ESTATUS_SIAC = folio.FECHA_CAMBIO_ESTATUS_SIAC;
                                tBL_FOLIOS.CLAVE_EMPRESA = folio.CLAVE_EMPRESA;
                                tBL_FOLIOS.NOMBRE_EMPRESA = folio.NOMBRE_EMPRESA;
                                tBL_FOLIOS.SERVICIO_FACTURACION_TERCEROS = folio.SERVICIO_FACTURACION_TERCEROS;
                                tBL_FOLIOS.ETAPA_PISA_MULTIORDEN = folio.ETAPA_PISA_MULTIORDEN;
                                tBL_FOLIOS.ETAPA_PISA_TV = folio.ETAPA_PISA_TV;
                                tBL_FOLIOS.ETIQUETA_TRAFICO_VOZ = folio.ETIQUETA_TRAFICO_VOZ;
                                tBL_FOLIOS.TRAFICO_VOZ_ENTRANTE = folio.TRAFICO_VOZ_ENTRANTE;
                                tBL_FOLIOS.TRAFICO_VOZ_SALIENTE = folio.TRAFICO_VOZ_SALIENTE;
                                tBL_FOLIOS.FECHA_TRAFICO_VOZ = folio.FECHA_TRAFICO_VOZ;
                                tBL_FOLIOS.TRAFICO_DATOS = folio.TRAFICO_DATOS;
                                tBL_FOLIOS.FECHA_TRAFICO_DATOS = folio.FECHA_TRAFICO_DATOS;
                                tBL_FOLIOS.FECHA_FACTURCION = folio.FECHA_FACTURCION;
                                tBL_FOLIOS.DESCRIPCION_VALIDA_ADEUDO = folio.DESCRIPCION_VALIDA_ADEUDO;
                                tBL_FOLIOS.CORREO_ELECTRONICO = folio.CORREO_ELECTRONICO;
                                tBL_FOLIOS.FECHA_NACIMIENTO = folio.FECHA_NACIMIENTO;
                                tBL_FOLIOS.ID = folio.ID;
                                tBL_FOLIOS.TERMINAL = folio.TERMINAL;
                                tBL_FOLIOS.DISTRITO = folio.DISTRITO;
                                tBL_FOLIOS.TECELULAR = folio.TECELULAR;

                                //Se cambia el estado de registro a modificado.
                                db.Entry(tBL_FOLIOS).State = EntityState.Modified;

                                if (row % lotes == 0)
                                    db.SaveChanges();

                                row++;
                            }

                            db.SaveChanges();
                        }
                        resultado = "Existoso!";
                    }
                    catch (Exception er)
                    {
                        resultado = "Error al subir el archivo." + er.Message;
                    }
                }
                else
                {
                    resultado = "El archivo esta vacío";
                }
            }
            else
            {
                resultado = "No se selecciono archivos";
            }

            DataManager.InsertLog(((ModeloUsuarios)Session["USER_LOGGED"]).NOMBRE_COMPLETO, "Se actualiza PIPES, resultado:" + resultado);
            return View("CargarPIPES");
        }
    }
}