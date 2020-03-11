using FilderNET.DataAccess.SQLServer;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilderNET.DataAccess.ServiceObjects
{
    public class SO_GLOBAL
    {

        private string SP_NET_GET_GLOBAL_GENERAL = "SP_NET_GET_GLOBAL_ADMIN";
        private string SP_NET_GET_GLOBAL_GENERAL_SUPERVISOR = "SP_NET_GET_GLOBAL_GENERAL_SUPERVISOR";

        /// <summary>
        /// Método para insertar un nuevo registro global
        /// </summary>
        /// <returns></returns>
        public int SetNewDataGlobal(int ID_PROMOTOR, DateTime FECHA_ELABORACION, string FILDER,
            DateTime FECHA_CAPTURA, string MESA_CONTROL, string NOMBRE_CLIENTE, string TEL_CONTACTO, string SERVICIO,
            string TIPO, string PAQUETE, string FOLIO_SIAC, string PAGO_A_PROMOTOR, bool ESTATUS_PAGO_INGRESO,
            bool ESTATUS_PAGO_POSTEO, string OBSERVACIONES)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    TBL_GLOBAL data = new TBL_GLOBAL();
                    
                    data.ID_PROMOTOR = ID_PROMOTOR;
                    data.FECHA_ELABORACION = FECHA_ELABORACION;
                    data.FILDER = FILDER;
                    data.FECHA_CAPTURA = FECHA_CAPTURA;
                    data.MESA_CONTROL = MESA_CONTROL;
                    data.NOMBRE_CLIENTE = NOMBRE_CLIENTE;
                    data.TEL_CONTACTO = TEL_CONTACTO;
                    data.SERVICIO = SERVICIO;
                    data.TIPO = TIPO;
                    data.PAQUETE = PAQUETE;
                    data.FOLIO_SIAC = FOLIO_SIAC;
                    data.PAGO_A_PROMOTOR = PAGO_A_PROMOTOR;
                    data.ESTATUS_PAGO_INGRESO = ESTATUS_PAGO_INGRESO;
                    data.ESTATUS_PAGO_POSTEO = ESTATUS_PAGO_POSTEO;
                    data.OBSERVACIONES = OBSERVACIONES;

                    conexion.TBL_GLOBAL.Add(data);

                    return conexion.SaveChanges();

                }
            }
            catch (Exception er)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro en base a su ID
        /// </summary>
        /// <param name="ID_GLOBAL"></param>
        /// <returns></returns>
        public int DeleteDataGlobal(int ID_GLOBAL)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    TBL_GLOBAL Obj = conexion.TBL_GLOBAL.Where(x => x.ID_GLOBAL == ID_GLOBAL).FirstOrDefault();

                    conexion.Entry(Obj).State = EntityState.Deleted;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método para modificar los datos de un registro en base a su ID
        /// </summary>
        /// <returns></returns>
        public int UpdateDataGlobal(int ID_GLOBAL, int ID_PROMOTOR, DateTime FECHA_ELABORACION, string FILDER,
            DateTime FECHA_CAPTURA, string MESA_CONTROL, string NOMBRE_CLIENTE, string TEL_CONTACTO, string SERVICIO,
            string TIPO, string PAQUETE, string FOLIO_SIAC, string PAGO_A_PROMOTOR, bool ESTATUS_PAGO_INGRESO,
            bool ESTATUS_PAGO_POSTEO, string OBSERVACIONES)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    TBL_GLOBAL Obj = conexion.TBL_GLOBAL.Where(x => x.ID_GLOBAL == ID_GLOBAL).FirstOrDefault();

                    Obj.ID_PROMOTOR = ID_PROMOTOR;
                    Obj.FECHA_ELABORACION = FECHA_ELABORACION;
                    Obj.FILDER = FILDER;
                    Obj.FECHA_CAPTURA = FECHA_CAPTURA;
                    Obj.MESA_CONTROL = MESA_CONTROL;
                    Obj.NOMBRE_CLIENTE = NOMBRE_CLIENTE;
                    Obj.TEL_CONTACTO = TEL_CONTACTO;
                    Obj.SERVICIO = SERVICIO;
                    Obj.TIPO = TIPO;
                    Obj.PAQUETE = PAQUETE;
                    Obj.FOLIO_SIAC = FOLIO_SIAC;
                    Obj.PAGO_A_PROMOTOR = PAGO_A_PROMOTOR;
                    Obj.ESTATUS_PAGO_INGRESO = ESTATUS_PAGO_INGRESO;
                    Obj.ESTATUS_PAGO_POSTEO = ESTATUS_PAGO_POSTEO;
                    Obj.OBSERVACIONES = OBSERVACIONES;

                    //Se cambia el estado de registro a modificado.
                    conexion.Entry(Obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene los datos filtrados por ID_PROMOTOR ordenas por la fecha de captura de forma descendiente
        /// </summary>
        /// <param name="ID_PROMOTOR"></param>
        /// <returns></returns>
        public IList GetDataGlobalPromotor(int ID_PROMOTOR)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var Lista = (from a in conexion.TBL_GLOBAL
                                 join b in conexion.TBL_USUARIO on a.ID_PROMOTOR equals b.ID_USUARIO
                                 where a.ID_PROMOTOR == ID_PROMOTOR
                                 orderby a.FECHA_CAPTURA descending
                                 select new
                                 {
                                     a.ID_GLOBAL,
                                     a.ID_PROMOTOR,
                                     a.FECHA_ELABORACION,
                                     a.FILDER,
                                     a.FECHA_CAPTURA,
                                     a.MESA_CONTROL,
                                     a.NOMBRE_CLIENTE,
                                     a.TEL_CONTACTO,
                                     a.SERVICIO,
                                     a.TIPO,
                                     a.PAQUETE,
                                     a.FOLIO_SIAC,
                                     a.PAGO_A_PROMOTOR,
                                     a.ESTATUS_PAGO_INGRESO,
                                     a.FECHA_PAGO_INGRESO,
                                     a.ESTATUS_PAGO_POSTEO,
                                     a.FECHA_PAGO_POSTEO,
                                     b.NOMBRE,
                                     b.APELLIDO_PATERNO,
                                     b.APELLIDO_MATERNO,
                                 }
                                 ).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene los datos filtrados por ID_PROMOTOR y por ESTATUS_PAGO ordenados por FECHA_CAPTURA descendiente
        /// </summary>
        /// <param name="ID_PROMOTOR"></param>
        /// <param name="ESTATUS_PAGO"></param>
        /// <returns></returns>
        public IList GetDataGlobalPromotorEstatusPago(int ID_PROMOTOR, bool ESTATUS_PAGO_INGRESO)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var Lista = (from a in conexion.TBL_GLOBAL
                                 join b in conexion.TBL_USUARIO on a.ID_PROMOTOR equals b.ID_USUARIO
                                 where a.ID_PROMOTOR == ID_PROMOTOR && a.ESTATUS_PAGO_INGRESO == ESTATUS_PAGO_INGRESO
                                 orderby a.FECHA_CAPTURA descending
                                 select new
                                 {
                                     a.ID_GLOBAL,
                                     a.ID_PROMOTOR,
                                     a.FECHA_ELABORACION,
                                     a.FILDER,
                                     a.FECHA_CAPTURA,
                                     a.MESA_CONTROL,
                                     a.NOMBRE_CLIENTE,
                                     a.TEL_CONTACTO,
                                     a.SERVICIO,
                                     a.TIPO,
                                     a.PAQUETE,
                                     a.FOLIO_SIAC,
                                     a.PAGO_A_PROMOTOR,
                                     a.ESTATUS_PAGO_INGRESO,
                                     a.FECHA_PAGO_INGRESO,
                                     a.ESTATUS_PAGO_POSTEO,
                                     a.FECHA_PAGO_POSTEO,
                                     b.NOMBRE,
                                     b.APELLIDO_PATERNO,
                                     b.APELLIDO_MATERNO,
                                 }
                                 ).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataSet GetAllDatGlobal()
        {
            try
            {
                DataSet Lista = null;

                Desing_SQL conexion = new Desing_SQL();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                
                Lista = conexion.EjecutarStoredProcedure(SP_NET_GET_GLOBAL_GENERAL, parametros);

                return Lista;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla global ordenamos por la fecha de captura ordenados descendientemente
        /// </summary>
        /// <returns></returns>
        //public IList GetAllDatGlobal()
        //{
        //    try
        //    {
        //        using (var conexion = new EntitiesFilder())
        //        {
        //            var Lista = (from a in conexion.TBL_GLOBAL
        //                         join b in conexion.TBL_USUARIO on a.ID_PROMOTOR equals b.ID_USUARIO
        //                         join c in conexion.TBL_FOLIOS on a.FOLIO_SIAC equals c.FOLIO_SIAC
        //                         orderby a.FECHA_CAPTURA descending
        //                         select new {
        //                             NOMBRE_COMPLETO_PROMOTOR = b.NOMBRE + " " + b.APELLIDO_PATERNO + " " + b.APELLIDO_MATERNO,
        //                             a.ID_PROMOTOR,
        //                             a.ID_GLOBAL,
        //                             a.FECHA_ELABORACION,
        //                             a.FILDER,
        //                             a.FECHA_CAPTURA,
        //                             a.MESA_CONTROL,
        //                             a.NOMBRE_CLIENTE,
        //                             a.TEL_CONTACTO,
        //                             a.SERVICIO,
        //                             a.TIPO,
        //                             a.PAQUETE,
        //                             a.FOLIO_SIAC,
        //                             a.PAGO_A_PROMOTOR,
        //                             a.ESTATUS_PAGO_INGRESO,
        //                             a.FECHA_PAGO_INGRESO,
        //                             a.ESTATUS_PAGO_POSTEO,
        //                             a.FECHA_PAGO_POSTEO,
        //                             c.ESTATUS_SIAC,
        //                             c.ESTATUS_PISA_MULTIORDEN
        //                         }).ToList();

        //            return Lista;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public IList GetAllDataGlobalByPromotor(string promotor)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var Lista = (from a in conexion.TBL_GLOBAL
                                 join b in conexion.TBL_USUARIO on a.ID_PROMOTOR equals b.ID_USUARIO
                                 join c in conexion.TBL_FOLIOS on a.FOLIO_SIAC equals c.FOLIO_SIAC
                                 where b.USUARIO == promotor
                                 orderby a.FECHA_CAPTURA descending
                                 select new
                                 {
                                     NOMBRE_COMPLETO_PROMOTOR = b.NOMBRE + " " + b.APELLIDO_PATERNO + " " + b.APELLIDO_MATERNO,
                                     a.ID_PROMOTOR,
                                     a.ID_GLOBAL,
                                     a.FECHA_ELABORACION,
                                     a.FILDER,
                                     a.FECHA_CAPTURA,
                                     a.MESA_CONTROL,
                                     a.NOMBRE_CLIENTE,
                                     a.TEL_CONTACTO,
                                     a.SERVICIO,
                                     a.TIPO,
                                     a.PAQUETE,
                                     a.FOLIO_SIAC,
                                     a.PAGO_A_PROMOTOR,
                                     a.ESTATUS_PAGO_INGRESO,
                                     a.FECHA_PAGO_INGRESO,
                                     a.ESTATUS_PAGO_POSTEO,
                                     a.FECHA_PAGO_POSTEO,
                                     c.ESTATUS_SIAC,
                                     c.ESTATUS_PISA_MULTIORDEN,
                                     OBSERVACIONES = a.OBSERVACIONES
                                 }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla filtrados por el FOLIO_SIAC
        /// </summary>
        /// <param name="FOLIO_SIAC"></param>
        /// <returns></returns>
        public IList GetDataGlobalFolioSIAC(int idGlobal)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var lista = (from a in conexion.TBL_GLOBAL
                                 join b in conexion.TBL_USUARIO on a.ID_PROMOTOR equals b.ID_USUARIO
                                 where a.ID_GLOBAL == idGlobal
                                 select new
                                 {
                                     a.ESTATUS_PAGO_INGRESO,
                                     a.ESTATUS_PAGO_POSTEO,
                                     a.FECHA_CAPTURA,
                                     a.FECHA_ELABORACION,
                                     a.FECHA_PAGO_INGRESO,
                                     a.FECHA_PAGO_POSTEO,
                                     a.FILDER,
                                     a.FOLIO_SIAC,
                                     a.ID_GLOBAL,
                                     a.ID_PROMOTOR,
                                     a.MESA_CONTROL,
                                     a.NOMBRE_CLIENTE,
                                     a.PAGO_A_PROMOTOR,
                                     a.PAQUETE,
                                     a.SERVICIO,
                                     a.TEL_CONTACTO,
                                     a.TIPO,
                                     NOMBRE_PROMOTOR = b.NOMBRE + " " + b.APELLIDO_PATERNO + " " + b.APELLIDO_MATERNO,
                                     OBSERVACIONES = a.OBSERVACIONES
                                 }).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla filtrados por el FOLIO_SIAC
        /// </summary>
        /// <param name="FOLIO_SIAC"></param>
        /// <returns></returns>
        public IList GetDataGlobalFolioSIAC(string FOLIO_SIAC)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var lista = (from a in conexion.TBL_GLOBAL
                                 join b in conexion.TBL_USUARIO on a.ID_PROMOTOR equals b.ID_USUARIO
                                 where a.FOLIO_SIAC == FOLIO_SIAC
                                 select new
                                 {
                                     a.ESTATUS_PAGO_INGRESO,
                                     a.ESTATUS_PAGO_POSTEO,
                                     a.FECHA_CAPTURA,
                                     a.FECHA_ELABORACION,
                                     a.FECHA_PAGO_INGRESO,
                                     a.FECHA_PAGO_POSTEO,
                                     a.FILDER,
                                     a.FOLIO_SIAC,
                                     a.ID_GLOBAL,
                                     a.ID_PROMOTOR,
                                     a.MESA_CONTROL,
                                     a.NOMBRE_CLIENTE,
                                     a.PAGO_A_PROMOTOR,
                                     a.PAQUETE,
                                     a.SERVICIO,
                                     a.TEL_CONTACTO,
                                     a.TIPO,
                                     NOMBRE_PROMOTOR = b.NOMBRE + " " + b.APELLIDO_PATERNO + " " + b.APELLIDO_MATERNO,
                                     OBSERVACIONES = a.OBSERVACIONES
                                 }).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la lista de los ESTATUS_PAGO = O con un Procedimiento Almacenado
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllDataGlobalEstatusPago()
        {
            try
            {
                DataSet Lista = null;

                Desing_SQL conexion = new Desing_SQL();
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("ESTATUS_PAGO", "0");

                Lista = conexion.EjecutarStoredProcedure("SP_GetDataModeloGlobal", parametros);

                return Lista;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList GetFolioSIAC(string folioSIAC)
        {
            try
            {
                using (var Conexion = new EntitiesFilder())
                {
                    var list = (from a in Conexion.TBL_GLOBAL
                                where a.FOLIO_SIAC == folioSIAC
                                select a).ToList();
                    return list;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataSet GetAllDataGlobalSupervisor(int idSupervisor)
        {
            try
            {
                DataSet dataSet = null;

                Desing_SQL conexion = new Desing_SQL();

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("idSupervisor", idSupervisor);

                dataSet = conexion.EjecutarStoredProcedure(SP_NET_GET_GLOBAL_GENERAL_SUPERVISOR, parametros);

                return dataSet;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
