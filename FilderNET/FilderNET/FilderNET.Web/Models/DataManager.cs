using FilderNET.DataAccess.ServiceObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Mvc;

namespace FilderNET.Web.Models
{
    public static class DataManager
    {
        #region Métodos

        #region Global

        /// <summary>
        /// Obtenemos el global del supervisor.
        /// </summary>
        /// <param name="idSupervisor"></param>
        /// <returns></returns>
        public static List<ModeloGlobal> GetAllDataGlobalSupervisor(int idSupervisor)
        {
            SO_GLOBAL servicio = new SO_GLOBAL();

            List<ModeloGlobal> AllDataGlobal = new List<ModeloGlobal>();

            DataSet Obj = servicio.GetAllDataGlobalSupervisor(idSupervisor);

            if (Obj != null)
            {
                if (Obj.Tables.Count > 0 && Obj.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in Obj.Tables[0].Rows)
                    {
                        ModeloGlobal DataGlobal = new ModeloGlobal();

                        DataGlobal.ID_GLOBAL = Convert.ToInt32(item["ID_GLOBAL"].ToString());
                        DataGlobal.ID_PROMOTOR = Convert.ToInt32(item["ID_PROMOTOR"].ToString());
                        DataGlobal.FECHA_ELABORACION = Convert.ToDateTime(item["FECHA_ELABORACION"].ToString());
                        DataGlobal.FILDER = item["FILDER"].ToString();
                        DataGlobal.FECHA_CAPTURA = Convert.ToDateTime(item["FECHA_CAPTURA"].ToString());
                        DataGlobal.MESA_CONTROL = item["MESA_CONTROL"].ToString();
                        DataGlobal.NOMBRE_CLIENTE = item["NOMBRE_CLIENTE"].ToString();
                        DataGlobal.TEL_CONTACTO = item["TEL_CONTACTO"].ToString();
                        DataGlobal.SERVICIO = item["SERVICIO"].ToString();
                        DataGlobal.TIPO = item["TIPO"].ToString();
                        DataGlobal.PAQUETE = item["PAQUETE"].ToString();
                        DataGlobal.FOLIO_SIAC = item["FOLIO_SIAC"].ToString();
                        DataGlobal.PAGO_A_PROMOTOR = item["PAGO_A_PROMOTOR"].ToString();
                        DataGlobal.NOMBRE_PROMOTOR = item["NOMBRE_COMPLETO_PROMOTOR"].ToString();
                        DataGlobal.ESTATUS_PAGO_INGRESO = Convert.ToBoolean(item["ESTATUS_PAGO_INGRESO"].ToString());

                        if (string.IsNullOrEmpty((item["FECHA_PAGO_INGRESO"].ToString())))
                        {
                            DataGlobal.FECHA_PAGO_INGRESO = DateTime.MinValue;
                        }
                        else
                        {
                            DataGlobal.FECHA_PAGO_INGRESO = Convert.ToDateTime(item["FECHA_PAGO_INGRESO"].ToString());
                        }
                        DataGlobal.ESTATUS_PAGO_POSTEO = Convert.ToBoolean(item["ESTATUS_PAGO_POSTEO"].ToString());

                        if (string.IsNullOrEmpty((item["FECHA_PAGO_POSTEO"].ToString())))
                        {
                            DataGlobal.FECHA_PAGO_POSTEO = DateTime.MinValue;
                        }
                        else
                        {
                            DataGlobal.FECHA_PAGO_POSTEO = Convert.ToDateTime(item["FECHA_PAGO_POSTEO"].ToString());
                        }

                        DataGlobal.ESTATUS_SIAC = item["ESTATUS_SIAC"].ToString();
                        DataGlobal.ESTATUS_PISA_MULTIORDEN = item["ESTATUS_PISA_MULTIORDEN"].ToString();
                        DataGlobal.AREA = item["AREA"].ToString();

                        AllDataGlobal.Add(DataGlobal);
                    }
                }
            }
            return AllDataGlobal;
        }

        public static List<DO_Bono> GetBono()
        {
            List<DO_Bono> lista = new List<DO_Bono>();
            SO_Bono serviceBono = new SO_Bono();

            IList informacion = serviceBono.Get();

            if (informacion != null)
            {
                foreach (var item in informacion)
                {
                    Type type = item.GetType();

                    DO_Bono bono = new DO_Bono();

                    bono.ID = Convert.ToInt32(type.GetProperty("ID_BONO").GetValue(item,null));
                    bono.Quincena = Convert.ToDateTime(type.GetProperty("QUINCENA").GetValue(item, null));
                    bono.Rubro = Convert.ToString(type.GetProperty("RUBRO").GetValue(item, null));
                    bono.Folio = Convert.ToString(type.GetProperty("FOLIO").GetValue(item, null));
                    bono.ImporteEfectividad = Convert.ToDouble(type.GetProperty("IMPORTE_EFECTIVIDAD").GetValue(item, null));
                    bono.ImporteComercio = Convert.ToDouble(type.GetProperty("IMPORTE_COMERCIO").GetValue(item, null));
                    bono.ImporteCalidad = Convert.ToDouble(type.GetProperty("IMPORTE_CALIDAD").GetValue(item, null));

                    lista.Add(bono);
                }
            }
            return lista;
        }

        /// <summary>
        /// Método para Obtener los datos de la BD que tengan EstatusPago = 0
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static List<ModeloGlobal> GetDataEstatusPagadoGlobal()
        {
            SO_GLOBAL servicio = new SO_GLOBAL();

            List<ModeloGlobal> DataEstatusPagado = new List<ModeloGlobal>();

            DataSet Obj = servicio.GetAllDataGlobalEstatusPago();

            if (servicio != null)
            {
                if (Obj.Tables.Count > 0 && Obj.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in Obj.Tables[0].Rows)
                    {
                        ModeloGlobal DataGlobal = new ModeloGlobal();

                        DataGlobal.ID_GLOBAL = Convert.ToInt32(item["ID_GLOBAL"]);
                        DataGlobal.ID_PROMOTOR = Convert.ToInt32(item["ID_PROMOTOR"]);
                        DataGlobal.FECHA_ELABORACION = Convert.ToDateTime(item["FECHA_ELABORACION"]);
                        DataGlobal.FILDER = Convert.ToString(item["FILDER"]);
                        DataGlobal.FECHA_CAPTURA = Convert.ToDateTime(item["FECHA_CAPTURA"]);
                        DataGlobal.MESA_CONTROL = Convert.ToString(item["MESA_CONTROL"]);
                        DataGlobal.NOMBRE_CLIENTE = Convert.ToString(item["NOMBRE_CLIENTE"]);
                        DataGlobal.TEL_CONTACTO = Convert.ToString(item["TEL_CONTACTO"]);
                        DataGlobal.SERVICIO = Convert.ToString(item["SERVICIO"]);
                        DataGlobal.TIPO = Convert.ToString(item["TIPO"]);
                        DataGlobal.PAQUETE = Convert.ToString(item["PAQUETE"]);
                        DataGlobal.FOLIO_SIAC = Convert.ToString(item["FOLIO_SIAC"]);
                        DataGlobal.PAGO_A_PROMOTOR = Convert.ToString(item["PAGO_A_PROMOTOR"]);
                        DataGlobal.ESTATUS_PAGO_INGRESO = Convert.ToBoolean(item["ESTATUS_PAGO_INGRESO"]);
                        DataGlobal.FECHA_PAGO_INGRESO = Convert.ToDateTime(item["FECHA_PAGO_INGRESO"]);
                        DataGlobal.ESTATUS_PAGO_POSTEO = Convert.ToBoolean(item["ESTATUS_PAGO_POSTEO"]);
                        DataGlobal.FECHA_PAGO_POSTEO = Convert.ToDateTime(item["FECHA_PAGO_POSTEO"]);

                        DataEstatusPagado.Add(DataGlobal);

                    }
                }
            }
            return DataEstatusPagado;
        }

        /// <summary>
        /// Método para obtener los datos de la tabla filtrados por el folio SIAC
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static ModeloGlobal GetDataFolioSIACGlobal(int idGlobal)
        {
            SO_GLOBAL servicio = new SO_GLOBAL();

            ModeloGlobal DataGlobal = new ModeloGlobal();

            IList Obj = servicio.GetDataGlobalFolioSIAC(idGlobal);

            if (Obj != null)
            {
                foreach (var item in Obj)
                {
                    System.Type tipo = item.GetType();

                    DataGlobal.NOMBRE_PROMOTOR = (string)tipo.GetProperty("NOMBRE_PROMOTOR").GetValue(item, null);
                    DataGlobal.ID_GLOBAL = (int)tipo.GetProperty("ID_GLOBAL").GetValue(item, null);
                    DataGlobal.ID_PROMOTOR = (int)tipo.GetProperty("ID_PROMOTOR").GetValue(item, null);
                    DataGlobal.FECHA_ELABORACION = (DateTime)tipo.GetProperty("FECHA_ELABORACION").GetValue(item, null);
                    DataGlobal.FILDER = (string)tipo.GetProperty("FILDER").GetValue(item, null);
                    DataGlobal.FECHA_CAPTURA = (DateTime)tipo.GetProperty("FECHA_CAPTURA").GetValue(item, null);
                    DataGlobal.MESA_CONTROL = (string)tipo.GetProperty("MESA_CONTROL").GetValue(item, null);
                    DataGlobal.NOMBRE_CLIENTE = (string)tipo.GetProperty("NOMBRE_CLIENTE").GetValue(item, null);
                    DataGlobal.TEL_CONTACTO = (string)tipo.GetProperty("TEL_CONTACTO").GetValue(item, null);
                    DataGlobal.SERVICIO = (string)tipo.GetProperty("SERVICIO").GetValue(item, null);
                    DataGlobal.TIPO = (string)tipo.GetProperty("TIPO").GetValue(item, null);
                    DataGlobal.PAQUETE = (string)tipo.GetProperty("PAQUETE").GetValue(item, null);
                    DataGlobal.FOLIO_SIAC = (string)tipo.GetProperty("FOLIO_SIAC").GetValue(item, null);
                    DataGlobal.PAGO_A_PROMOTOR = (string)tipo.GetProperty("PAGO_A_PROMOTOR").GetValue(item, null);
                    DataGlobal.ESTATUS_PAGO_INGRESO = (bool)tipo.GetProperty("ESTATUS_PAGO_INGRESO").GetValue(item, null);
                    DataGlobal.FECHA_PAGO_INGRESO = Convert.ToDateTime(tipo.GetProperty("FECHA_PAGO_INGRESO").GetValue(item, null));
                    DataGlobal.ESTATUS_PAGO_POSTEO = (bool)tipo.GetProperty("ESTATUS_PAGO_POSTEO").GetValue(item, null);
                    DataGlobal.FECHA_PAGO_POSTEO = Convert.ToDateTime(tipo.GetProperty("FECHA_PAGO_POSTEO").GetValue(item, null));
                    DataGlobal.OBSERVACIONES = (string)tipo.GetProperty("OBSERVACIONES").GetValue(item, null);
                }

                DataGlobal.ARCHIVOS = GetFiles(DataGlobal.ID_GLOBAL);
            }
            return DataGlobal;
        }

        /// <summary>
        /// Método para obtener los datos de la tabla filtrados por el folio SIAC
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static ModeloGlobal GetDataFolioSIACGlobal(string folioSIAC)
        {
            SO_GLOBAL servicio = new SO_GLOBAL();

            ModeloGlobal DataGlobal = new ModeloGlobal();

            IList Obj = servicio.GetDataGlobalFolioSIAC(folioSIAC);

            if (Obj != null)
            {
                foreach (var item in Obj)
                {
                    System.Type tipo = item.GetType();

                    DataGlobal.NOMBRE_PROMOTOR = (string)tipo.GetProperty("NOMBRE_PROMOTOR").GetValue(item, null);
                    DataGlobal.ID_GLOBAL = (int)tipo.GetProperty("ID_GLOBAL").GetValue(item, null);
                    DataGlobal.ID_PROMOTOR = (int)tipo.GetProperty("ID_PROMOTOR").GetValue(item, null);
                    DataGlobal.FECHA_ELABORACION = (DateTime)tipo.GetProperty("FECHA_ELABORACION").GetValue(item, null);
                    DataGlobal.FILDER = (string)tipo.GetProperty("FILDER").GetValue(item, null);
                    DataGlobal.FECHA_CAPTURA = (DateTime)tipo.GetProperty("FECHA_CAPTURA").GetValue(item, null);
                    DataGlobal.MESA_CONTROL = (string)tipo.GetProperty("MESA_CONTROL").GetValue(item, null);
                    DataGlobal.NOMBRE_CLIENTE = (string)tipo.GetProperty("NOMBRE_CLIENTE").GetValue(item, null);
                    DataGlobal.TEL_CONTACTO = (string)tipo.GetProperty("TEL_CONTACTO").GetValue(item, null);
                    DataGlobal.SERVICIO = (string)tipo.GetProperty("SERVICIO").GetValue(item, null);
                    DataGlobal.TIPO = (string)tipo.GetProperty("TIPO").GetValue(item, null);
                    DataGlobal.PAQUETE = (string)tipo.GetProperty("PAQUETE").GetValue(item, null);
                    DataGlobal.FOLIO_SIAC = (string)tipo.GetProperty("FOLIO_SIAC").GetValue(item, null);
                    DataGlobal.PAGO_A_PROMOTOR = (string)tipo.GetProperty("PAGO_A_PROMOTOR").GetValue(item, null);
                    DataGlobal.ESTATUS_PAGO_INGRESO = (bool)tipo.GetProperty("ESTATUS_PAGO_INGRESO").GetValue(item, null);
                    DataGlobal.FECHA_PAGO_INGRESO = Convert.ToDateTime(tipo.GetProperty("FECHA_PAGO_INGRESO").GetValue(item, null));
                    DataGlobal.ESTATUS_PAGO_POSTEO = (bool)tipo.GetProperty("ESTATUS_PAGO_POSTEO").GetValue(item, null);
                    DataGlobal.FECHA_PAGO_POSTEO = Convert.ToDateTime(tipo.GetProperty("FECHA_PAGO_POSTEO").GetValue(item, null));
                    DataGlobal.OBSERVACIONES = (string)tipo.GetProperty("OBSERVACIONES").GetValue(item, null);
                }

                DataGlobal.ARCHIVOS = GetFiles(DataGlobal.ID_GLOBAL);
            }
            return DataGlobal;
        }

        /// <summary>
        /// Método que obtiene todos los registros ordenados por la fecha de captura descendiente
        /// </summary>
        /// <returns></returns>
        public static List<ModeloGlobal> GetAllDataGlobal()
        {
            SO_GLOBAL servicio = new SO_GLOBAL();

            List<ModeloGlobal> AllDataGlobal = new List<ModeloGlobal>();

            DataSet Obj = servicio.GetAllDatGlobal();

            if (Obj != null)
            {
                if (Obj.Tables.Count > 0 && Obj.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in Obj.Tables[0].Rows)
                    {
                        ModeloGlobal DataGlobal = new ModeloGlobal();

                        DataGlobal.ID_GLOBAL = Convert.ToInt32(item["ID_GLOBAL"].ToString());
                        DataGlobal.ID_PROMOTOR = Convert.ToInt32(item["ID_PROMOTOR"].ToString());
                        DataGlobal.FECHA_ELABORACION = Convert.ToDateTime(item["FECHA_ELABORACION"].ToString());
                        DataGlobal.FILDER = item["FILDER"].ToString();
                        DataGlobal.FECHA_CAPTURA = Convert.ToDateTime(item["FECHA_CAPTURA"].ToString());
                        DataGlobal.MESA_CONTROL = item["MESA_CONTROL"].ToString();
                        DataGlobal.NOMBRE_CLIENTE = item["NOMBRE_CLIENTE"].ToString();
                        DataGlobal.TEL_CONTACTO = item["TEL_CONTACTO"].ToString();
                        DataGlobal.SERVICIO = item["SERVICIO"].ToString();
                        DataGlobal.TIPO = item["TIPO"].ToString();
                        DataGlobal.PAQUETE = item["PAQUETE"].ToString();
                        DataGlobal.FOLIO_SIAC = item["FOLIO_SIAC"].ToString();
                        DataGlobal.PAGO_A_PROMOTOR = item["PAGO_A_PROMOTOR"].ToString();
                        DataGlobal.NOMBRE_PROMOTOR = item["NOMBRE_COMPLETO_PROMOTOR"].ToString();
                        DataGlobal.ESTATUS_PAGO_INGRESO = Convert.ToBoolean(item["ESTATUS_PAGO_INGRESO"].ToString());

                        if (string.IsNullOrEmpty((item["FECHA_PAGO_INGRESO"].ToString())))
                        {
                            DataGlobal.FECHA_PAGO_INGRESO = DateTime.MinValue;
                        }
                        else
                        {
                            DataGlobal.FECHA_PAGO_INGRESO = Convert.ToDateTime(item["FECHA_PAGO_INGRESO"].ToString());
                        }
                        DataGlobal.ESTATUS_PAGO_POSTEO = Convert.ToBoolean(item["ESTATUS_PAGO_POSTEO"].ToString());

                        if (string.IsNullOrEmpty((item["FECHA_PAGO_POSTEO"].ToString())))
                        {
                            DataGlobal.FECHA_PAGO_POSTEO = DateTime.MinValue;
                        }
                        else
                        {
                            DataGlobal.FECHA_PAGO_POSTEO = Convert.ToDateTime(item["FECHA_PAGO_POSTEO"].ToString());
                        }

                        DataGlobal.ESTATUS_SIAC = item["ESTATUS_SIAC"].ToString();
                        DataGlobal.ESTATUS_PISA_MULTIORDEN = item["ESTATUS_PISA_MULTIORDEN"].ToString();
                        DataGlobal.AREA = item["AREA"].ToString();

                        DataGlobal.FACTURA_INGRESO = Convert.ToDouble(item["IMPORTE_BASE"].ToString());
                        DataGlobal.FACTURA_POSTEO = Convert.ToDouble(item["IMPORTE_POSTEO"].ToString());
                        DataGlobal.FACTURA_BONO_CALIDAD = Convert.ToDouble(item["IMPORTE_CALIDAD"].ToString());
                        DataGlobal.FACTURA_BONO_COMERCIO = Convert.ToDouble(item["IMPORTE_COMERCIO"].ToString());
                        DataGlobal.FACTURA_BONO_EFECTIVIDAD = Convert.ToDouble(item["IMPORTE_EFECTIVIDAD"].ToString());

                        AllDataGlobal.Add(DataGlobal);
                    }
                }
            }
            return AllDataGlobal;
        }

        public static List<ModeloGlobal> GetAllDataGlobalByPromotor(string promotor)
        {
            //GetAllDataGlobalByPromotor

            SO_GLOBAL servicio = new SO_GLOBAL();

            List<ModeloGlobal> AllDataGlobal = new List<ModeloGlobal>();

            IList Obj = servicio.GetAllDataGlobalByPromotor(promotor);

            if (Obj != null)
            {
                foreach (var item in Obj)
                {
                    System.Type tipo = item.GetType();

                    ModeloGlobal DataGlobal = new ModeloGlobal();

                    DataGlobal.ID_GLOBAL = (int)tipo.GetProperty("ID_GLOBAL").GetValue(item, null);
                    DataGlobal.ID_PROMOTOR = (int)tipo.GetProperty("ID_PROMOTOR").GetValue(item, null);
                    DataGlobal.FECHA_ELABORACION = Convert.ToDateTime(tipo.GetProperty("FECHA_ELABORACION").GetValue(item, null));
                    DataGlobal.FILDER = (string)tipo.GetProperty("FILDER").GetValue(item, null);
                    DataGlobal.FECHA_CAPTURA = Convert.ToDateTime(tipo.GetProperty("FECHA_CAPTURA").GetValue(item, null));
                    DataGlobal.MESA_CONTROL = (string)tipo.GetProperty("MESA_CONTROL").GetValue(item, null);
                    DataGlobal.NOMBRE_CLIENTE = (string)tipo.GetProperty("NOMBRE_CLIENTE").GetValue(item, null);
                    DataGlobal.TEL_CONTACTO = (string)tipo.GetProperty("TEL_CONTACTO").GetValue(item, null);
                    DataGlobal.SERVICIO = (string)tipo.GetProperty("SERVICIO").GetValue(item, null);
                    DataGlobal.TIPO = (string)tipo.GetProperty("TIPO").GetValue(item, null);
                    DataGlobal.PAQUETE = (string)tipo.GetProperty("PAQUETE").GetValue(item, null);
                    DataGlobal.FOLIO_SIAC = (string)tipo.GetProperty("FOLIO_SIAC").GetValue(item, null);
                    DataGlobal.PAGO_A_PROMOTOR = (string)tipo.GetProperty("PAGO_A_PROMOTOR").GetValue(item, null);
                    DataGlobal.NOMBRE_PROMOTOR = (string)tipo.GetProperty("NOMBRE_COMPLETO_PROMOTOR").GetValue(item, null);
                    DataGlobal.ESTATUS_PAGO_INGRESO = (bool)tipo.GetProperty("ESTATUS_PAGO_INGRESO").GetValue(item, null);
                    DataGlobal.FECHA_PAGO_INGRESO = Convert.ToDateTime(tipo.GetProperty("FECHA_PAGO_INGRESO").GetValue(item, null));
                    DataGlobal.ESTATUS_PAGO_POSTEO = (bool)tipo.GetProperty("ESTATUS_PAGO_POSTEO").GetValue(item, null);
                    DataGlobal.FECHA_PAGO_POSTEO = Convert.ToDateTime(tipo.GetProperty("FECHA_PAGO_POSTEO").GetValue(item, null));
                    DataGlobal.ESTATUS_SIAC = (string)tipo.GetProperty("ESTATUS_SIAC").GetValue(item, null);
                    DataGlobal.ESTATUS_PISA_MULTIORDEN = (string)tipo.GetProperty("ESTATUS_PISA_MULTIORDEN").GetValue(item, null);
                    DataGlobal.OBSERVACIONES =  (string)tipo.GetProperty("OBSERVACIONES").GetValue(item, null);
                    AllDataGlobal.Add(DataGlobal);
                }
            }
            return AllDataGlobal;
        }

        /// <summary>
        /// Método que obtiene todos los datos filtrados por el id del promotor y el estatus del pago
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static List<ModeloGlobal> GetDataPromotorEstatusPagadoGlobal(ModeloGlobal Data)
        {
            SO_GLOBAL servicio = new SO_GLOBAL();

            List<ModeloGlobal> DataPromotorEstatusPagado = new List<ModeloGlobal>();

            IList obj = servicio.GetDataGlobalPromotorEstatusPago(Data.ID_PROMOTOR, Data.ESTATUS_PAGO_INGRESO);

            if (obj != null)
            {
                foreach (var item in obj)
                {
                    System.Type tipo = item.GetType();

                    ModeloGlobal DataGlobal = new ModeloGlobal();

                    DataGlobal.ID_GLOBAL = (int)tipo.GetProperty("ID_GLOBAL").GetValue(item, null);
                    DataGlobal.ID_PROMOTOR = (int)tipo.GetProperty("ID_PROMOTOR").GetValue(item, null);
                    DataGlobal.FECHA_ELABORACION = (DateTime)tipo.GetProperty("FECHA_ELABORACION").GetValue(item, null);
                    DataGlobal.FILDER = (string)tipo.GetProperty("FILDER").GetValue(item, null);
                    DataGlobal.FECHA_CAPTURA = (DateTime)tipo.GetProperty("FECHA_CAPTURA").GetValue(item, null);
                    DataGlobal.MESA_CONTROL = (string)tipo.GetProperty("MESA_CONTROL").GetValue(item, null);
                    DataGlobal.NOMBRE_CLIENTE = (string)tipo.GetProperty("NOMBRE_CLIENTE").GetValue(item, null);
                    DataGlobal.TEL_CONTACTO = (string)tipo.GetProperty("TEL_CONTACTO").GetValue(item, null);
                    DataGlobal.SERVICIO = (string)tipo.GetProperty("SERVICIO").GetValue(item, null);
                    DataGlobal.TIPO = (string)tipo.GetProperty("TIPO").GetValue(item, null);
                    DataGlobal.PAQUETE = (string)tipo.GetProperty("PAQUETE").GetValue(item, null);
                    DataGlobal.FOLIO_SIAC = (string)tipo.GetProperty("FOLIO_SIAC").GetValue(item, null);
                    DataGlobal.PAGO_A_PROMOTOR = (string)tipo.GetProperty("PAGO_A_PROMOTOR").GetValue(item, null);
                    DataGlobal.ESTATUS_PAGO_INGRESO = (bool)tipo.GetProperty("ESTATUS_PAGO_INGRESO").GetValue(item, null);
                    DataGlobal.FECHA_PAGO_INGRESO = Convert.ToDateTime(tipo.GetProperty("FECHA_PAGO_INGRESO").GetValue(item, null));
                    DataGlobal.ESTATUS_PAGO_POSTEO = (bool)tipo.GetProperty("ESTATUS_PAGO_POSTEO").GetValue(item, null);
                    DataGlobal.FECHA_PAGO_POSTEO = Convert.ToDateTime(tipo.GetProperty("FECHA_PAGO_POSTEO").GetValue(item, null));

                    DataPromotorEstatusPagado.Add(DataGlobal);
                }
            }
            return DataPromotorEstatusPagado;
        }

        /// <summary>
        /// Método que obtiene los datos filtrados por ID PROMOTOR
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static List<ModeloGlobal> GetDataPromotorGlobal(ModeloGlobal Data)
        {
            SO_GLOBAL servicio = new SO_GLOBAL();

            List<ModeloGlobal> DatosPromotor = new List<ModeloGlobal>();

            IList obj = servicio.GetDataGlobalPromotor(Data.ID_PROMOTOR);

            if (obj != null)
            {
                foreach (var item in obj)
                {
                    System.Type tipo = item.GetType();

                    ModeloGlobal DataGlobal = new ModeloGlobal();

                    DataGlobal.ID_GLOBAL = (int)tipo.GetProperty("ID_GLOBAL").GetValue(item, null);
                    DataGlobal.ID_PROMOTOR = (int)tipo.GetProperty("ID_PROMOTOR").GetValue(item, null);
                    DataGlobal.FECHA_ELABORACION = (DateTime)tipo.GetProperty("FECHA_ELABORACION").GetValue(item, null);
                    DataGlobal.FILDER = (string)tipo.GetProperty("FILDER").GetValue(item, null);
                    DataGlobal.FECHA_CAPTURA = (DateTime)tipo.GetProperty("FECHA_CAPTURA").GetValue(item, null);
                    DataGlobal.MESA_CONTROL = (string)tipo.GetProperty("MESA_CONTROL").GetValue(item, null);
                    DataGlobal.NOMBRE_CLIENTE = (string)tipo.GetProperty("NOMBRE_CLIENTE").GetValue(item, null);
                    DataGlobal.TEL_CONTACTO = (string)tipo.GetProperty("TEL_CONTACTO").GetValue(item, null);
                    DataGlobal.SERVICIO = (string)tipo.GetProperty("SERVICIO").GetValue(item, null);
                    DataGlobal.TIPO = (string)tipo.GetProperty("TIPO").GetValue(item, null);
                    DataGlobal.PAQUETE = (string)tipo.GetProperty("PAQUETE").GetValue(item, null);
                    DataGlobal.FOLIO_SIAC = (string)tipo.GetProperty("FOLIO_SIAC").GetValue(item, null);
                    DataGlobal.PAGO_A_PROMOTOR = (string)tipo.GetProperty("PAGO_A_PROMOTOR").GetValue(item, null);
                    DataGlobal.ESTATUS_PAGO_INGRESO = (bool)tipo.GetProperty("ESTATUS_PAGO_INGRESO").GetValue(item, null);
                    DataGlobal.FECHA_PAGO_INGRESO = Convert.ToDateTime(tipo.GetProperty("FECHA_PAGO_INGRESO").GetValue(item, null));
                    DataGlobal.ESTATUS_PAGO_POSTEO = (bool)tipo.GetProperty("ESTATUS_PAGO_POSTEO").GetValue(item, null);
                    DataGlobal.FECHA_PAGO_POSTEO = Convert.ToDateTime(tipo.GetProperty("FECHA_PAGO_POSTEO").GetValue(item, null));

                    DatosPromotor.Add(DataGlobal);
                }
            }
            return DatosPromotor;
        }

        /// <summary>
        /// Método que inserta un nuevo registro en la tabla de GLOBAL
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int SetNewDataGlobal(ModeloGlobal Data)
        {
            SO_GLOBAL servicio = new SO_GLOBAL();

            return servicio.SetNewDataGlobal(Data.ID_PROMOTOR, Data.FECHA_ELABORACION, Data.FILDER, Data.FECHA_CAPTURA, Data.MESA_CONTROL,
                                             Data.NOMBRE_CLIENTE, Data.TEL_CONTACTO, Data.SERVICIO, Data.TIPO, Data.PAQUETE, Data.FOLIO_SIAC,
                                             Data.PAGO_A_PROMOTOR,Data.ESTATUS_PAGO_INGRESO,Data.ESTATUS_PAGO_POSTEO, Data.OBSERVACIONES);
        }

        /// <summary>
        /// Método que modifica un registro en la tabla de GLOBAL
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int UpdateDataGlobal(ModeloGlobal Data)
        {
            SO_GLOBAL servicio = new SO_GLOBAL();

            return servicio.UpdateDataGlobal(Data.ID_GLOBAL, Data.ID_PROMOTOR, Data.FECHA_ELABORACION, Data.FILDER, Data.FECHA_CAPTURA, Data.MESA_CONTROL,
                                             Data.NOMBRE_CLIENTE, Data.TEL_CONTACTO, Data.SERVICIO, Data.TIPO, Data.PAQUETE, Data.FOLIO_SIAC,
                                             Data.PAGO_A_PROMOTOR,Data.ESTATUS_PAGO_INGRESO,Data.ESTATUS_PAGO_POSTEO, Data.OBSERVACIONES);
        }

        /// <summary>
        /// Método que borra un registro de la tabla GLOBAL
        /// </summary>
        /// <param name="ID_GLOBAL"></param>
        /// <returns></returns>
        public static int DeleteDataGlobal(int idGlobal)
        {
            SO_GLOBAL servicio = new SO_GLOBAL();
            SO_ArchivoGlobal archivoGlobal = new SO_ArchivoGlobal();

            int r = archivoGlobal.Delete(idGlobal);

            r = servicio.DeleteDataGlobal(idGlobal);
            
            return r;
        }

        public static bool ExistsFolioSIAC(string folioSIAC)
        {
            SO_GLOBAL sO_GLOBAL = new SO_GLOBAL();

            IList informacionBD = sO_GLOBAL.GetFolioSIAC(folioSIAC);

            bool respuesta = false;

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    respuesta = true;
                }
            }

            return respuesta;
        }

        #endregion

        #region Usuarios

        /// <summary>
        /// Método que obtiene todos los usuarios
        /// </summary>
        /// <returns></returns>
        public static List<ModeloUsuarios> GetAllUsuarios()
        {
            SO_USUARIOS servicio = new SO_USUARIOS();

            List<ModeloUsuarios> ListaDataUsuarios = new List<ModeloUsuarios>();

            IList Obj = servicio.GetAllUsuarios();

            if (Obj != null)
            {
                foreach (var item in Obj)
                {
                    System.Type tipo = item.GetType();

                    ModeloUsuarios Data = new ModeloUsuarios();

                    Data.ID_USUARIO = (int)tipo.GetProperty("ID_USUARIO").GetValue(item, null);
                    Data.ID_JERARQUIA = (int)tipo.GetProperty("ID_JERARQUIA").GetValue(item, null);
                    Data.ID_JEFE = (int)tipo.GetProperty("ID_JEFE").GetValue(item, null);
                    Data.USUARIO = (string)tipo.GetProperty("USUARIO").GetValue(item, null);
                    Data.CONTRASENA = (string)tipo.GetProperty("CONTRASENA").GetValue(item, null);
                    Data.APELLIDO_PATERNO = (string)tipo.GetProperty("APELLIDO_PATERNO").GetValue(item, null);
                    Data.APELLIDO_MATERNO = (string)tipo.GetProperty("APELLIDO_MATERNO").GetValue(item, null);
                    Data.NOMBRE = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    Data.FECHA_NACIMIENTO = (DateTime)tipo.GetProperty("FECHA_NACIMIENTO").GetValue(item, null);
                    Data.CURP = (string)tipo.GetProperty("CURP").GetValue(item, null);
                    Data.RFC = (string)tipo.GetProperty("RFC").GetValue(item, null);
                    Data.TELEFONO = (string)tipo.GetProperty("TELEFONO").GetValue(item, null);
                    Data.EMAIL = (string)tipo.GetProperty("EMAIL").GetValue(item, null);
                    Data.FOTO = (string)tipo.GetProperty("FOTO").GetValue(item, null);
                    if (string.IsNullOrEmpty(Data.FOTO))
                        Data.FOTO = "~/ assets / images / users / default - user.png";
                    Data.ACTIVO = (bool)tipo.GetProperty("ACTIVO").GetValue(item, null);
                    Data.FECHA_CREACION = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    Data.FECHA_ACTUALIZACION = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);

                    ListaDataUsuarios.Add(Data);
                }
            }
            return ListaDataUsuarios;
        }

        /// <summary>
        /// Método que obttiene los datos de un usuario en especifico, lo busca por nombre de usuario, nombre, apellido materno y apellido paterno
        /// </summary>
        /// <param name="USUARIO_BUSCAR"></param>
        /// <returns></returns>
        public static List<ModeloUsuarios> GetAllUsuariosFiltrado(string USUARIO_BUSCAR)
        {
            SO_USUARIOS servicio = new SO_USUARIOS();

            List<ModeloUsuarios> ListaDataUsuarios = new List<ModeloUsuarios>();

            IList Obj = servicio.GetAllUsuarioFiltrado(USUARIO_BUSCAR);

            if (Obj != null)
            {
                foreach (var item in Obj)
                {
                    System.Type tipo = item.GetType();

                    ModeloUsuarios Data = new ModeloUsuarios();

                    Data.ID_USUARIO = (int)tipo.GetProperty("ID_USUARIO").GetValue(item, null);
                    Data.ID_JERARQUIA = (int)tipo.GetProperty("ID_JERARQUIA").GetValue(item, null);
                    Data.ID_JEFE = (int)tipo.GetProperty("ID_JEFE").GetValue(item, null);
                    Data.USUARIO = (string)tipo.GetProperty("USUARIO").GetValue(item, null);
                    Data.CONTRASENA = (string)tipo.GetProperty("CONTRASENA").GetValue(item, null);
                    Data.APELLIDO_PATERNO = (string)tipo.GetProperty("APELLIDO_PATERNO").GetValue(item, null);
                    Data.APELLIDO_MATERNO = (string)tipo.GetProperty("APELLIDO_MATERNO").GetValue(item, null);
                    Data.NOMBRE = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    Data.FECHA_NACIMIENTO = (DateTime)tipo.GetProperty("FECHA_NACIMIENTO").GetValue(item, null);
                    Data.CURP = (string)tipo.GetProperty("CURP").GetValue(item, null);
                    Data.RFC = (string)tipo.GetProperty("RFC").GetValue(item, null);
                    Data.TELEFONO = (string)tipo.GetProperty("TELEFONO").GetValue(item, null);
                    Data.EMAIL = (string)tipo.GetProperty("EMAIL").GetValue(item, null);
                    Data.FOTO = (string)tipo.GetProperty("FOTO").GetValue(item, null);
                    if (string.IsNullOrEmpty(Data.FOTO))
                        Data.FOTO = "~/ assets / images / users / default - user.png";
                    Data.ACTIVO = (bool)tipo.GetProperty("ACTIVO").GetValue(item, null);
                    Data.FECHA_CREACION = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    Data.FECHA_ACTUALIZACION = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);

                    ListaDataUsuarios.Add(Data);
                }
            }
            return ListaDataUsuarios;
        }

        /// <summary>
        /// Método que crea un nuevo usuario
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int SetNewUsuario(ModeloUsuarios Data)
        {
            SO_USUARIOS servicio = new SO_USUARIOS();

            return servicio.SetNewUsuario(
                                            Data.ID_JERARQUIA,
                                            Data.ID_JEFE,
                                            Data.USUARIO,
                                            Data.CONTRASENA,
                                            Data.APELLIDO_PATERNO,
                                            Data.APELLIDO_MATERNO,
                                            Data.NOMBRE,
                                            Data.FECHA_NACIMIENTO,
                                            Data.CURP,
                                            Data.RFC,
                                            Data.TELEFONO,
                                            Data.EMAIL,
                                            Data.FOTO,
                                            Data.ACTIVO,
                                            Data.FECHA_CREACION,
                                            Data.FECHA_ACTUALIZACION);
                                                                        
        }

        /// <summary>
        /// Método que modifica los datos de un usuario
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int UpdateDataUsuario(ModeloUsuarios Data)
        {
            SO_USUARIOS servicio = new SO_USUARIOS();

            return servicio.UpdateUsuario(  
                                            Data.ID_USUARIO,                            
                                            Data.ID_JERARQUIA,
                                            Data.ID_JEFE,
                                            Data.USUARIO,
                                            Data.CONTRASENA,
                                            Data.APELLIDO_PATERNO,
                                            Data.APELLIDO_MATERNO,
                                            Data.NOMBRE,
                                            Data.FECHA_NACIMIENTO,
                                            Data.CURP,
                                            Data.RFC,
                                            Data.TELEFONO,
                                            Data.EMAIL,
                                            Data.FOTO,
                                            Data.ACTIVO,
                                            Data.FECHA_CREACION,
                                            Data.FECHA_ACTUALIZACION);
        }

        /// <summary>
        /// Método que elimina un usuario
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int DeleteUsuario(ModeloUsuarios Data)
        {
            SO_USUARIOS servicio = new SO_USUARIOS();

            return servicio.DeleteUsuario(Data.ID_USUARIO);
        }

        /// <summary>
        /// Método que obtiene la información del usuario a partir de un usuario y una contraseña.
        /// </summary>
        /// <returns>Usuario que contiene la información del usuario con las credenciales enviadas.</returns>
        public static ModeloUsuarios GetLogin(string Usuario, string Contrasena)
        {
            //Declaramos un objeto de tipo Usuario que será el que retornemos en el método.
            ModeloUsuarios usuario = null;

            //Inicializamos los servicios de usuario.
            SO_USUARIOS ServiceUsuario = new SO_USUARIOS();

            //Ejecutamos el método del login y la información resultante la asignamos a un objeto local.
            DataSet InformacionBD = ServiceUsuario.GetLogin(Usuario, Contrasena);

            //Comparamos si la información obtenida de la consulta es diferente de nulo.
            if (InformacionBD != null)
            {

                //Comparamos si la información obtenida contiene al menos una tabla y esa tabla contiene al menos un registro.
                if (InformacionBD.Tables.Count > 0 && InformacionBD.Tables[0].Rows.Count > 0)
                {

                    //Itermamos los registro de la tabla cero.
                    foreach (DataRow element in InformacionBD.Tables[0].Rows)
                    {

                        //Inicializamos el objeto Usuario.
                        usuario = new ModeloUsuarios();

                        //Asignamos los valores de la información a las propiedades del usuario correspondientes. 
                        usuario.APELLIDO_PATERNO = Convert.ToString(element["APELLIDO_MATERNO"]);
                        usuario.APELLIDO_MATERNO = Convert.ToString(element["APELLIDO_MATERNO"]);
                        usuario.NOMBRE = Convert.ToString(element["NOMBRE"]);
                        usuario.USUARIO = Convert.ToString(element["USUARIO"]);
                        usuario.ID_JERARQUIA = Convert.ToInt32(element["ID_JERARQUIA"]);
                        usuario.FOTO = Convert.ToString(element["FOTO"]);
                        usuario.ID_USUARIO = Convert.ToInt32(element["ID_USUARIO"]);

                        if (string.IsNullOrEmpty(usuario.FOTO))
                            usuario.FOTO = "~/assets/images/users/default-user.png";
                    }
                }
            }
            return usuario;
        }

        public static ModeloUsuarios GetUsuario(int idUsuario)
        {
            SO_USUARIOS servicio = new SO_USUARIOS();

            ModeloUsuarios objUsuario = new ModeloUsuarios();

            IList Obj = servicio.GetUsuario(idUsuario);

            if (Obj != null)
            {
                foreach (var item in Obj)
                {
                    System.Type tipo = item.GetType();

                    objUsuario = new ModeloUsuarios();

                    objUsuario.ID_USUARIO = (int)tipo.GetProperty("ID_USUARIO").GetValue(item, null);
                    objUsuario.ID_JERARQUIA = (int)tipo.GetProperty("ID_JERARQUIA").GetValue(item, null);
                    objUsuario.ID_JEFE = (int)tipo.GetProperty("ID_JEFE").GetValue(item, null);
                    objUsuario.USUARIO = (string)tipo.GetProperty("USUARIO").GetValue(item, null);
                    objUsuario.CONTRASENA = (string)tipo.GetProperty("CONTRASENA").GetValue(item, null);
                    objUsuario.APELLIDO_PATERNO = (string)tipo.GetProperty("APELLIDO_PATERNO").GetValue(item, null);
                    objUsuario.APELLIDO_MATERNO = (string)tipo.GetProperty("APELLIDO_MATERNO").GetValue(item, null);
                    objUsuario.NOMBRE = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    objUsuario.FECHA_NACIMIENTO = (DateTime)tipo.GetProperty("FECHA_NACIMIENTO").GetValue(item, null);
                    objUsuario.CURP = (string)tipo.GetProperty("CURP").GetValue(item, null);
                    objUsuario.RFC = (string)tipo.GetProperty("RFC").GetValue(item, null);
                    objUsuario.TELEFONO = (string)tipo.GetProperty("TELEFONO").GetValue(item, null);
                    objUsuario.EMAIL = (string)tipo.GetProperty("EMAIL").GetValue(item, null);
                    objUsuario.FOTO = (string)tipo.GetProperty("FOTO").GetValue(item, null);
                    if (string.IsNullOrEmpty(objUsuario.FOTO))
                        objUsuario.FOTO = "~/ assets / images / users / default - user.png";
                    objUsuario.ACTIVO = (bool)tipo.GetProperty("ACTIVO").GetValue(item, null);
                    objUsuario.FECHA_CREACION = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    objUsuario.FECHA_ACTUALIZACION = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
                    
                }
            }
            return objUsuario;
        }

        #endregion

        #region Folios

        /// <summary>
        /// Método para eliminar un folio existente mediante el FOLIO_SIAC
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int DeleteFolio(ModeloFolios Data)
        {
            SO_FOLIO servicio = new SO_FOLIO();

            return servicio.DeleteFolio(Data.FOLIO_SIAC);
        }

        /// <summary>
        /// Método que obtiene todos los folios
        /// </summary>
        /// <returns></returns>
        public static List<ModeloFolios> GetAllFolios()
        {
            SO_FOLIO servicio = new SO_FOLIO();

            List<ModeloFolios> ListaDataFolios = new List<ModeloFolios>();

            IList Obj = servicio.GetAllFolios();

            if (Obj != null)
            {
                foreach (var item in Obj)
                {
                    System.Type tipo = item.GetType();

                    ModeloFolios Data = new ModeloFolios();

                    
                    Data.FOLIO_SIAC = (string)tipo.GetProperty("FOLIO_SIAC").GetValue(item, null);
                    Data.FECHA_CAPTURA = (DateTime)tipo.GetProperty("FECHA_CAPTURA").GetValue(item, null);
                    Data.ESTRATEGIA = (string)tipo.GetProperty("ESTRATEGIA").GetValue(item, null);
                    Data.PROMOTOR = (string)tipo.GetProperty("PROMOTOR").GetValue(item, null);
                    Data.ESTATUS_SIAC = (string)tipo.GetProperty("ESTATUS_SIAC").GetValue(item, null);
                    Data.TIPO_LINEA = (string)tipo.GetProperty("TIPO_LINEA").GetValue(item, null);
                    Data.LINEA_CONTRATADA = (string)tipo.GetProperty("LINEA_CONTRATADA").GetValue(item, null);
                    Data.AREA = (string)tipo.GetProperty("AREA").GetValue(item, null);
                    Data.DIVICION = (string)tipo.GetProperty("DIVICION").GetValue(item, null);
                    Data.TIENDA = (string)tipo.GetProperty("TIENDA").GetValue(item, null);
                    Data.PAQUETE = (string)tipo.GetProperty("PAQUETE").GetValue(item, null);
                    Data.OBSERVACIONES = (string)tipo.GetProperty("OBSERVACIONES").GetValue(item, null);
                    Data.RESPUESTA_TELMEX = (string)tipo.GetProperty("RESPUESTA_TELMEX").GetValue(item, null);
                    Data.MOTIVO_RECHAZO = (string)tipo.GetProperty("MOTIVO_RECHAZO").GetValue(item, null);
                    Data.TELEFONO_ASIGNADO = (string)tipo.GetProperty("TELEFONO_ASIGNADO").GetValue(item, null);
                    Data.TELEFONO_PORTADO = (string)tipo.GetProperty("TELEFONO_PORTADO").GetValue(item, null);
                    Data.OS_ALTA_LINEA_MULTIORDEN = (string)tipo.GetProperty("OS_ALTA_LINEA_MULTIORDEN").GetValue(item, null);
                    Data.FECHA_OS_ALTA_LINEA_MULTIORDEN = (string)tipo.GetProperty("FECHA_OS_ALTA_LINEA_MULTIORDEN").GetValue(item, null);
                    Data.ORDEN_SERVICIO_TV = (string)tipo.GetProperty("ORDEN_SERVICIO_TV").GetValue(item, null);
                    Data.FECHA_OS_TV = (string)tipo.GetProperty("FECHA_OS_TV").GetValue(item, null);
                    Data.CAMPANA = (string)tipo.GetProperty("CAMPANA").GetValue(item, null);
                    Data.ESTATUS_ATENCION_MULTIORDEN = (string)tipo.GetProperty("ESTATUS_ATENCION_MULTIORDEN").GetValue(item, null);
                    Data.ESTATUS_PISA_MULTIORDEN = (string)tipo.GetProperty("ESTATUS_PISA_MULTIORDEN").GetValue(item, null);
                    Data.PISA_OS_FECHA_POSTEO_MULTIORDEN = (string)tipo.GetProperty("PISA_OS_FECHA_POSTEO_MULTIORDEN").GetValue(item, null);
                    Data.ESTATUS_PISA_TV = (string)tipo.GetProperty("ESTATUS_PISA_TV").GetValue(item, null);
                    Data.PISA_OS_FECHA_POSTEO_TV = (string)tipo.GetProperty("PISA_OS_FECHA_POSTEO_TV").GetValue(item, null);
                    Data.FECHA_CAMBIO_ESTATUS_SIAC = (string)tipo.GetProperty("FECHA_CAMBIO_ESTATUS_SIAC").GetValue(item, null);
                    Data.CLAVE_EMPRESA = (string)tipo.GetProperty("CLAVE_EMPRESA").GetValue(item, null);
                    Data.NOMBRE_EMPRESA = (string)tipo.GetProperty("NOMBRE_EMPRESA").GetValue(item, null);
                    Data.SERVICIO_FACTURACION_TERCEROS = (string)tipo.GetProperty("SERVICIO_FACTURACION_TERCEROS").GetValue(item, null);
                    Data.ETAPA_PISA_MULTIORDEN = (string)tipo.GetProperty("ETAPA_PISA_MULTIORDEN").GetValue(item, null);
                    Data.ETAPA_PISA_TV = (string)tipo.GetProperty("ETAPA_PISA_TV").GetValue(item, null);
                    Data.ETIQUETA_TRAFICO_VOZ = (string)tipo.GetProperty("ETIQUETA_TRAFICO_VOZ").GetValue(item, null);
                    Data.TRAFICO_VOZ_ENTRANTE = (string)tipo.GetProperty("TRAFICO_VOZ_ENTRANTE").GetValue(item, null);
                    Data.TRAFICO_VOZ_SALIENTE = (string)tipo.GetProperty("TRAFICO_VOZ_SALIENTE").GetValue(item, null);
                    Data.FECHA_TRAFICO_VOZ = (string)tipo.GetProperty("FECHA_TRAFICO_VOZ").GetValue(item, null);
                    Data.TRAFICO_DATOS = (string)tipo.GetProperty("TRAFICO_DATOS").GetValue(item, null);
                    Data.FECHA_TRAFICO_DATOS = (string)tipo.GetProperty("FECHA_TRAFICO_DATOS").GetValue(item, null);
                    Data.FECHA_FACTURCION = (string)tipo.GetProperty("FECHA_FACTURCION").GetValue(item, null);
                    Data.DESCRIPCION_VALIDA_ADEUDO = (string)tipo.GetProperty("DESCRIPCION_VALIDA_ADEUDO").GetValue(item, null);
                    Data.CORREO_ELECTRONICO = (string)tipo.GetProperty("CORREO_ELECTRONICO").GetValue(item, null);
                    Data.FECHA_NACIMIENTO = (string)tipo.GetProperty("FECHA_NACIMIENTO").GetValue(item, null);
                    Data.ID = (string)tipo.GetProperty("ID").GetValue(item, null);
                    Data.TERMINAL = (string)tipo.GetProperty("TERMINAL").GetValue(item, null);
                    Data.DISTRITO = (string)tipo.GetProperty("DISTRITO").GetValue(item, null);
                    Data.TECELULAR = (string)tipo.GetProperty("TECELULAR").GetValue(item, null);
                    Data.FECHA_CREACION = Convert.ToDateTime(tipo.GetProperty("FECHA_CREACION").GetValue(item, null));
                    Data.FECHA_CALCULO_COMISION = Convert.ToDateTime(tipo.GetProperty("FECHA_CALCULO_COMISION").GetValue(item, null));
                    Data.ESTATUS_PAGADO = (bool)tipo.GetProperty("ESTATUS_PAGADO").GetValue(item, null);

                    ListaDataFolios.Add(Data);
                }
            }
            return ListaDataFolios;
        }

        /// <summary>
        /// Método que obtiene el registro de un folio en especifico
        /// </summary>
        /// <param name="Datos"></param>
        /// <returns></returns>
        public static List<ModeloFolios> GetInfoFolios(ModeloFolios Datos)
        {
            SO_FOLIO servicio = new SO_FOLIO();

            List<ModeloFolios> ListaDataFolios = new List<ModeloFolios>();

            IList Obj = servicio.GetInfoFolios(Datos.FOLIO_SIAC);

            if (Obj != null)
            {
                foreach (var item in Obj)
                {
                    System.Type tipo = item.GetType();

                    ModeloFolios Data = new ModeloFolios();

                    Data.FOLIO_SIAC = (string)tipo.GetProperty("FOLIO_SIAC").GetValue(item, null);
                    Data.FECHA_CAPTURA = (DateTime)tipo.GetProperty("FECHA_CAPTURA").GetValue(item, null);
                    Data.ESTRATEGIA = (string)tipo.GetProperty("ESTRATEGIA").GetValue(item, null);
                    Data.PROMOTOR = (string)tipo.GetProperty("PROMOTOR").GetValue(item, null);
                    Data.ESTATUS_SIAC = (string)tipo.GetProperty("ESTATUS_SIAC").GetValue(item, null);
                    Data.TIPO_LINEA = (string)tipo.GetProperty("TIPO_LINEA").GetValue(item, null);
                    Data.LINEA_CONTRATADA = (string)tipo.GetProperty("LINEA_CONTRATADA").GetValue(item, null);
                    Data.AREA = (string)tipo.GetProperty("AREA").GetValue(item, null);
                    Data.DIVICION = (string)tipo.GetProperty("DIVICION").GetValue(item, null);
                    Data.TIENDA = (string)tipo.GetProperty("TIENDA").GetValue(item, null);
                    Data.PAQUETE = (string)tipo.GetProperty("PAQUETE").GetValue(item, null);
                    Data.OBSERVACIONES = (string)tipo.GetProperty("OBSERVACIONES").GetValue(item, null);
                    Data.RESPUESTA_TELMEX = (string)tipo.GetProperty("RESPUESTA_TELMEX").GetValue(item, null);
                    Data.MOTIVO_RECHAZO = (string)tipo.GetProperty("MOTIVO_RECHAZO").GetValue(item, null);
                    Data.TELEFONO_ASIGNADO = (string)tipo.GetProperty("TELEFONO_ASIGNADO").GetValue(item, null);
                    Data.TELEFONO_PORTADO = (string)tipo.GetProperty("TELEFONO_PORTADO").GetValue(item, null);
                    Data.OS_ALTA_LINEA_MULTIORDEN = (string)tipo.GetProperty("OS_ALTA_LINEA_MULTIORDEN").GetValue(item, null);
                    Data.FECHA_OS_ALTA_LINEA_MULTIORDEN = (string)tipo.GetProperty("FECHA_OS_ALTA_LINEA_MULTIORDEN").GetValue(item, null);
                    Data.ORDEN_SERVICIO_TV = (string)tipo.GetProperty("ORDEN_SERVICIO_TV").GetValue(item, null);
                    Data.FECHA_OS_TV = (string)tipo.GetProperty("FECHA_OS_TV").GetValue(item, null);
                    Data.CAMPANA = (string)tipo.GetProperty("CAMPANA").GetValue(item, null);
                    Data.ESTATUS_ATENCION_MULTIORDEN = (string)tipo.GetProperty("ESTATUS_ATENCION_MULTIORDEN").GetValue(item, null);
                    Data.ESTATUS_PISA_MULTIORDEN = (string)tipo.GetProperty("ESTATUS_PISA_MULTIORDEN").GetValue(item, null);
                    Data.PISA_OS_FECHA_POSTEO_MULTIORDEN = (string)tipo.GetProperty("PISA_OS_FECHA_POSTEO_MULTIORDEN").GetValue(item, null);
                    Data.ESTATUS_PISA_TV = (string)tipo.GetProperty("ESTATUS_PISA_TV").GetValue(item, null);
                    Data.PISA_OS_FECHA_POSTEO_TV = (string)tipo.GetProperty("PISA_OS_FECHA_POSTEO_TV").GetValue(item, null);
                    Data.FECHA_CAMBIO_ESTATUS_SIAC = (string)tipo.GetProperty("FECHA_CAMBIO_ESTATUS_SIAC").GetValue(item, null);
                    Data.CLAVE_EMPRESA = (string)tipo.GetProperty("CLAVE_EMPRESA").GetValue(item, null);
                    Data.NOMBRE_EMPRESA = (string)tipo.GetProperty("NOMBRE_EMPRESA").GetValue(item, null);
                    Data.SERVICIO_FACTURACION_TERCEROS = (string)tipo.GetProperty("SERVICIO_FACTURACION_TERCEROS").GetValue(item, null);
                    Data.ETAPA_PISA_MULTIORDEN = (string)tipo.GetProperty("ETAPA_PISA_MULTIORDEN").GetValue(item, null);
                    Data.ETAPA_PISA_TV = (string)tipo.GetProperty("ETAPA_PISA_TV").GetValue(item, null);
                    Data.ETIQUETA_TRAFICO_VOZ = (string)tipo.GetProperty("ETIQUETA_TRAFICO_VOZ").GetValue(item, null);
                    Data.TRAFICO_VOZ_ENTRANTE = (string)tipo.GetProperty("TRAFICO_VOZ_ENTRANTE").GetValue(item, null);
                    Data.TRAFICO_VOZ_SALIENTE = (string)tipo.GetProperty("TRAFICO_VOZ_SALIENTE").GetValue(item, null);
                    Data.FECHA_TRAFICO_VOZ = (string)tipo.GetProperty("FECHA_TRAFICO_VOZ").GetValue(item, null);
                    Data.TRAFICO_DATOS = (string)tipo.GetProperty("TRAFICO_DATOS").GetValue(item, null);
                    Data.FECHA_TRAFICO_DATOS = (string)tipo.GetProperty("FECHA_TRAFICO_DATOS").GetValue(item, null);
                    Data.FECHA_FACTURCION = (string)tipo.GetProperty("FECHA_FACTURCION").GetValue(item, null);
                    Data.DESCRIPCION_VALIDA_ADEUDO = (string)tipo.GetProperty("DESCRIPCION_VALIDA_ADEUDO").GetValue(item, null);
                    Data.CORREO_ELECTRONICO = (string)tipo.GetProperty("CORREO_ELECTRONICO").GetValue(item, null);
                    Data.FECHA_NACIMIENTO = (string)tipo.GetProperty("FECHA_NACIMIENTO").GetValue(item, null);
                    Data.ID = (string)tipo.GetProperty("ID").GetValue(item, null);
                    Data.TERMINAL = (string)tipo.GetProperty("TERMINAL").GetValue(item, null);
                    Data.DISTRITO = (string)tipo.GetProperty("DISTRITO").GetValue(item, null);
                    Data.TECELULAR = (string)tipo.GetProperty("TECELULAR").GetValue(item, null);
                    Data.FECHA_CREACION = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    Data.FECHA_CALCULO_COMISION = (DateTime)tipo.GetProperty("FECHA_CALCULO_COMISION").GetValue(item, null);
                    Data.ESTATUS_PAGADO = (bool)tipo.GetProperty("ESTATUS_PAGADO").GetValue(item, null);

                    ListaDataFolios.Add(Data);
                }
            }
            return ListaDataFolios;
        }

        /// <summary>
        /// Método que inserta o actualiza un folio
        /// primero se verifica si existe el folio
        /// </summary>
        /// <returns></returns>
        public static Task<int> SetORUpdateFolio(ModeloFolios Datos)
        {
            SO_FOLIO servicios = new SO_FOLIO();

            //Verificamos la existencia del folio, si no existe se retorna un un "0"
            string FolioExistente = servicios.VerificarExistenciaFolio(Datos.FOLIO_SIAC);

            return servicios.SetORUpdateFolio(
                Datos.FOLIO_SIAC,
                Datos.FECHA_CAPTURA,
                Datos.ESTRATEGIA,
                Datos.PROMOTOR,
                Datos.ESTATUS_SIAC,
                Datos.TIPO_LINEA,
                Datos.LINEA_CONTRATADA,
                Datos.AREA,
                Datos.DIVICION,
                Datos.TIENDA,
                Datos.PAQUETE,
                Datos.OBSERVACIONES,
                Datos.RESPUESTA_TELMEX,
                Datos.MOTIVO_RECHAZO,
                Datos.TELEFONO_ASIGNADO,
                Datos.TELEFONO_PORTADO,
                Datos.OS_ALTA_LINEA_MULTIORDEN,
                Datos.FECHA_OS_ALTA_LINEA_MULTIORDEN,
                Datos.ORDEN_SERVICIO_TV,
                Datos.FECHA_OS_TV,
                Datos.CAMPANA,
                Datos.ESTATUS_ATENCION_MULTIORDEN,
                Datos.ESTATUS_PISA_MULTIORDEN,
                Datos.PISA_OS_FECHA_POSTEO_MULTIORDEN,
                Datos.ESTATUS_PISA_TV,
                Datos.PISA_OS_FECHA_POSTEO_TV,
                Datos.FECHA_CAMBIO_ESTATUS_SIAC,
                Datos.CLAVE_EMPRESA,
                Datos.NOMBRE_EMPRESA,
                Datos.SERVICIO_FACTURACION_TERCEROS,
                Datos.ETAPA_PISA_MULTIORDEN,
                Datos.ETAPA_PISA_TV,
                Datos.ETIQUETA_TRAFICO_VOZ,
                Datos.TRAFICO_VOZ_ENTRANTE,
                Datos.TRAFICO_VOZ_SALIENTE,
                Datos.FECHA_TRAFICO_VOZ,
                Datos.TRAFICO_DATOS,
                Datos.FECHA_TRAFICO_DATOS,
                Datos.FECHA_FACTURCION,
                Datos.DESCRIPCION_VALIDA_ADEUDO,
                Datos.CORREO_ELECTRONICO,
                Datos.FECHA_NACIMIENTO,
                Datos.ID,
                Datos.TERMINAL,
                Datos.DISTRITO,
                Datos.TECELULAR,
                Datos.FECHA_CREACION,
                Datos.FECHA_CALCULO_COMISION,
                Datos.ESTATUS_PAGADO,
                FolioExistente);
        }

        #endregion

        #region Pagos
        public static List<DO_Pago> GetPagoIngresoGeneral()
        {
            List<DO_Pago> ListaPago = new List<DO_Pago>();

            SO_Pagos sO_Pagos = new SO_Pagos();

            DataSet dataSet = sO_Pagos.GetPagoIngreso();

            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in dataSet.Tables[0].Rows)
                    {
                        DO_Pago dO_Pago = new DO_Pago();

                        dO_Pago.NOMBRE_CLIENTE = item["NOMBRE_CLIENTE"].ToString();
                        dO_Pago.SERVICIO = item["SERVICIO"].ToString();
                        dO_Pago.TIPO_LINEA = item["TIPO_LINEA"].ToString();
                        dO_Pago.PAQUETE = item["PAQUETE"].ToString();
                        dO_Pago.NOMBRE_PROMOTOR = item["NOMBRE_COMPLETO"].ToString();
                        dO_Pago.FOLIO_SIAC = item["FOLIO_SIAC"].ToString();
                        dO_Pago.INGRESO = Convert.ToDouble(item["INGRESO"].ToString());
                        dO_Pago.CONCEPTO = item["CONCEPTO"].ToString();
                        dO_Pago.AREA = item["AREA"].ToString();
                        ListaPago.Add(dO_Pago);
                    }
                }
            }

            return ListaPago;
        }

        public static List<DO_Pago> GetPagoSupervisores()
        {
            List<DO_Pago> ListaPago = new List<DO_Pago>();

            SO_Pagos sO_Pagos = new SO_Pagos();

            DataSet dataSet = sO_Pagos.GetPagoSupervisores();

            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in dataSet.Tables[0].Rows)
                    {
                        DO_Pago dO_Pago = new DO_Pago();

                        dO_Pago.NOMBRE_CLIENTE = item["NOMBRE_CLIENTE"].ToString();
                        dO_Pago.SERVICIO = item["SERVICIO"].ToString();
                        dO_Pago.TIPO_LINEA = item["TIPO_LINEA"].ToString();
                        dO_Pago.PAQUETE = item["PAQUETE"].ToString();
                        dO_Pago.NOMBRE_PROMOTOR = item["NOMBRE_COMPLETO"].ToString();
                        dO_Pago.FOLIO_SIAC = item["FOLIO_SIAC"].ToString();
                        dO_Pago.INGRESO = Convert.ToDouble(item["INGRESO"].ToString());
                        dO_Pago.CONCEPTO = item["CONCEPTO"].ToString();
                        dO_Pago.AREA = item["AREA"].ToString();
                        ListaPago.Add(dO_Pago);
                    }
                }
            }

            return ListaPago;
        }

        public static List<DO_Pago> GetPagoSupervisor(int idUsuario, string nombreCompleto)
        {
            List<DO_Pago> ListaPago = new List<DO_Pago>();

            SO_Pagos sO_Pagos = new SO_Pagos();

            DataSet dataSet = sO_Pagos.GetPagoSupervisor(idUsuario,nombreCompleto);

            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in dataSet.Tables[0].Rows)
                    {
                        DO_Pago dO_Pago = new DO_Pago();

                        dO_Pago.NOMBRE_CLIENTE = item["NOMBRE_CLIENTE"].ToString();
                        dO_Pago.SERVICIO = item["SERVICIO"].ToString();
                        dO_Pago.TIPO_LINEA = item["TIPO_LINEA"].ToString();
                        dO_Pago.PAQUETE = item["PAQUETE"].ToString();
                        dO_Pago.NOMBRE_PROMOTOR = item["NOMBRE_COMPLETO"].ToString();
                        dO_Pago.FOLIO_SIAC = item["FOLIO_SIAC"].ToString();
                        dO_Pago.INGRESO = Convert.ToDouble(item["INGRESO"].ToString());
                        dO_Pago.CONCEPTO = item["CONCEPTO"].ToString();
                        dO_Pago.AREA = item["AREA"].ToString();
                        ListaPago.Add(dO_Pago);
                    }
                }
            }

            return ListaPago;
        }

        public static List<DO_Pago> GetPagoIngresoPromotor(string promotor)
        {
            List<DO_Pago> ListaPago = new List<DO_Pago>();

            SO_Pagos sO_Pagos = new SO_Pagos();

            DataSet dataSet = sO_Pagos.GetPagoIngresoPromotor(promotor);

            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in dataSet.Tables[0].Rows)
                    {
                        DO_Pago dO_Pago = new DO_Pago();

                        dO_Pago.NOMBRE_CLIENTE = item["NOMBRE_CLIENTE"].ToString();
                        dO_Pago.SERVICIO = item["SERVICIO"].ToString();
                        dO_Pago.TIPO_LINEA = item["TIPO_LINEA"].ToString();
                        dO_Pago.PAQUETE = item["PAQUETE"].ToString();
                        dO_Pago.NOMBRE_PROMOTOR = item["NOMBRE_COMPLETO"].ToString();
                        dO_Pago.FOLIO_SIAC = item["FOLIO_SIAC"].ToString();
                        dO_Pago.INGRESO = Convert.ToDouble(item["INGRESO"].ToString());
                        dO_Pago.CONCEPTO = item["CONCEPTO"].ToString();
                        ListaPago.Add(dO_Pago);
                    }
                }
            }

            return ListaPago;
        }

        public static int SetPagoPromotor(out List<DO_Pago> pagos)
        {
            pagos = new List<DO_Pago>();
            //PROMOTOR
            List<DO_Pago> pagosPromotor = GetPagoIngresoGeneral();
            List<DO_Pago> pagosIngresoPromotor = new List<DO_Pago>();
            List<DO_Pago> pagosPosteoPromotor = new List<DO_Pago>();

            foreach (DO_Pago pago in pagosPromotor)
            {
                if (pago.CONCEPTO == "INGRESO")
                    pagosIngresoPromotor.Add(pago);
                else
                    pagosPosteoPromotor.Add(pago);
            }

            SO_Pagos sO_Pagos = new SO_Pagos();

            foreach (DO_Pago pago in pagosIngresoPromotor)
            {
                sO_Pagos.UpdatePagoIngreso(pago.FOLIO_SIAC, true);
            }

            foreach (DO_Pago pago in pagosPosteoPromotor)
            {
                sO_Pagos.UpdatePagoPosteo(pago.FOLIO_SIAC, true);
            }

            //SUPERVISOR
            List<DO_Pago> pagosSupervisor = GetPagoSupervisores();
            List<DO_Pago> pagosSupervisorIngreso = pagosSupervisor.Where(x => x.CONCEPTO == "INGRESO").ToList();
            List<DO_Pago> pagosSupervisorPosteo = pagosSupervisor.Where(x => x.CONCEPTO == "POSTEO").ToList();

            foreach (DO_Pago pago in pagosSupervisorIngreso)
            {
                sO_Pagos.UpdatePagoIngresoSupervisor(pago.FOLIO_SIAC, true);
            }

            foreach (DO_Pago pago in pagosSupervisorPosteo)
            {
                sO_Pagos.UpdatePagoPosteoSupervisor(pago.FOLIO_SIAC, true);
            }

            foreach (DO_Pago pago in pagosPromotor)
            {
                pagos.Add(pago);
            }

            foreach (var pago in pagosSupervisor)
            {
                pagos.Add(pago);
            }

            return 1;
        }

        public static List<SelectListItem> GetAllServicios()
        {
            List<SelectListItem> ListaResultante = new List<SelectListItem>();

            SO_Pagos sO_Pagos = new SO_Pagos();

            IList informacionBD = sO_Pagos.GetAllServicios();

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    Type tipo = item.GetType();

                    SelectListItem selectListItem = new SelectListItem();

                    string servicio = (string)tipo.GetProperty("SERVICIO").GetValue(item, null);

                    selectListItem.Text = servicio;
                    selectListItem.Value = servicio;

                    ListaResultante.Add(selectListItem);
                }
            }

            return ListaResultante;
        }

        public static List<SelectListItem> GetAllTipo()
        {
            List<SelectListItem> ListaResultante = new List<SelectListItem>();

            SO_Pagos sO_Pagos = new SO_Pagos();

            IList informacionBD = sO_Pagos.GetAllTipo();

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    Type tipo = item.GetType();

                    SelectListItem selectListItem = new SelectListItem();

                    string _tipo = (string)tipo.GetProperty("TIPO").GetValue(item, null);

                    selectListItem.Text = _tipo;
                    selectListItem.Value = _tipo;

                    ListaResultante.Add(selectListItem);
                }
            }

            return ListaResultante;
        }

        public static List<SelectListItem> GetAllPaquetes()
        {
            List<SelectListItem> ListaResultante = new List<SelectListItem>();

            SO_Pagos sO_Pagos = new SO_Pagos();

            IList informacionBD = sO_Pagos.GetAllPaqutes();

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    Type tipo = item.GetType();

                    SelectListItem selectListItem = new SelectListItem();

                    string paquete = (string)tipo.GetProperty("PAQUETE").GetValue(item, null);

                    selectListItem.Text = paquete;
                    selectListItem.Value = paquete;

                    ListaResultante.Add(selectListItem);
                }
            }

            return ListaResultante;
        }

        #endregion

        #region ARCHIVO GLOBAL
        public static List<DO_Archivo> GetFiles(int idGlobal)
        {
            SO_ArchivoGlobal sO_ArchivoGlobal = new SO_ArchivoGlobal();

            IList informacionBD = sO_ArchivoGlobal.GetFiles(idGlobal);

            List<DO_Archivo> archivos = new List<DO_Archivo>();

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    DO_Archivo archivo = new DO_Archivo();

                    Type tipo = item.GetType();

                    archivo.ID_ARCHIVO = (int)tipo.GetProperty("ID_ARCHIVO_GLOBAL").GetValue(item, null);
                    archivo.ID_GLOBAL = (int)tipo.GetProperty("ID_GLOBAL").GetValue(item, null);
                    archivo.NOMBRE_ARCHIVO = (string)tipo.GetProperty("NOMBRE_ARCHIVO").GetValue(item, null);
                    archivo.EXT = (string)tipo.GetProperty("EXT").GetValue(item, null);
                    archivo.RUTA = (string)tipo.GetProperty("RUTA").GetValue(item, null);

                    archivos.Add(archivo);
                }
            }

            return archivos;
        }

        public static int InsertArchivoGlobal(string ext, int idGlobal, string nombreArchivo, string path)
        {
            SO_ArchivoGlobal sO_ArchivoGlobal = new SO_ArchivoGlobal();

            return sO_ArchivoGlobal.Insert(ext, idGlobal, nombreArchivo, path);
        }
        #endregion

        #region LOG
        public static int InsertLog(string usuario, string descripcion)
        {
            SO_LOG sO_LOG = new SO_LOG();

            return sO_LOG.Insert(usuario, descripcion);
        }
        #endregion

        #region Factura
        public static List<DO_Factura> GetFactura()
        {
            List<DO_Factura> listaFactura = new List<DO_Factura>();

            SO_Factura serviceFactura = new SO_Factura();

            IList informacionBD = serviceFactura.Get();

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    Type type = item.GetType();

                    DO_Factura factura = new DO_Factura();

                    factura.Quincena = Convert.ToDateTime(type.GetProperty("QUINCENA").GetValue(item, null));
                    factura.Folio = Convert.ToString(type.GetProperty("FOLIO_SIAC").GetValue(item, null));
                    factura.ImporteBase = Convert.ToDouble(type.GetProperty("IMPORTE_BASE").GetValue(item, null));
                    factura.ImportePosteo = Convert.ToDouble(type.GetProperty("IMPORTE_POSTEO").GetValue(item, null));
                    factura.PisaE = Convert.ToString(type.GetProperty("PISA_E").GetValue(item, null));
                    factura.Procede2 = Convert.ToString(type.GetProperty("PROCEDE2").GetValue(item, null));
                    factura.Pago2 = Convert.ToString(type.GetProperty("PAGO2").GetValue(item, null));
                    factura.Fecha = Convert.ToString(type.GetProperty("FECHA").GetValue(item, null));
                    factura.OSPago = Convert.ToString(type.GetProperty("OS_PAGO").GetValue(item, null));
                    factura.Estatus = Convert.ToString(type.GetProperty("ESTATUS").GetValue(item, null));
                    factura.LineaContratada = Convert.ToString(type.GetProperty("LINEA_CONTRATADA").GetValue(item, null));
                    factura.Paquete = Convert.ToString(type.GetProperty("PAQUETE").GetValue(item, null));

                    listaFactura.Add(factura);
                }
            }

            return listaFactura;
        }

        public static List<DO_Log> GetLog()
        {
            SO_LOG ServiceLog = new SO_LOG();

            List<DO_Log> listaLog = new List<DO_Log>();

            IList informacionBD = ServiceLog.Get();

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    Type type = item.GetType();

                    DO_Log log = new DO_Log();

                    log.ID = Convert.ToInt32(type.GetProperty("ID_LOG").GetValue(item, null));
                    log.Fecha = Convert.ToDateTime(type.GetProperty("FECHA").GetValue(item, null));
                    log.Usuario = Convert.ToString(type.GetProperty("USUARIO").GetValue(item, null));
                    log.Descripcion = Convert.ToString(type.GetProperty("DESCRIPCION").GetValue(item, null));

                    listaLog.Add(log);
                }
            }

            return listaLog;
        }
        #endregion

        #endregion
    }
}