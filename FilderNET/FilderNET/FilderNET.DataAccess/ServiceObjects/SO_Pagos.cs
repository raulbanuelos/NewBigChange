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
    public class SO_Pagos
    {
        private string SP_NET_GET_PAGOINGRESO = "SP_NET_GET_PAGOINGRESO";
        private string SP_NET_GET_PAGOINGRESO_PROMOTOR = "SP_NET_GET_PAGOINGRESO_PROMOTOR";
        private string SP_NET_GET_PAGO_SUPERVISORES = "SP_NET_GET_PAGO_SUPERVISORES";
        private string SP_NET_GET_PAGO_SUPERVISOR = "SP_NET_GET_PAGO_SUPERVISOR";

        public DataSet GetPagoIngreso()
        {
            try
            {
                DataSet data = new DataSet();

                Desing_SQL desing_SQL = new Desing_SQL();

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                data = desing_SQL.EjecutarStoredProcedure(SP_NET_GET_PAGOINGRESO, parametros);

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataSet GetPagoSupervisor(int idUsuario, string nombreCompleto)
        {
            try
            {
                DataSet data = new DataSet();

                Desing_SQL desing_SQL = new Desing_SQL();

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("idUsuario", idUsuario);
                parametros.Add("nombreLider", nombreCompleto);

                data = desing_SQL.EjecutarStoredProcedure(SP_NET_GET_PAGO_SUPERVISOR, parametros);

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataSet GetPagoSupervisores()
        {
            try
            {
                DataSet data = new DataSet();

                Desing_SQL desing_SQL = new Desing_SQL();

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                data = desing_SQL.EjecutarStoredProcedure(SP_NET_GET_PAGO_SUPERVISORES, parametros);

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataSet GetPagoIngresoPromotor(string promotor)
        {
            try
            {
                DataSet data = new DataSet();

                Desing_SQL desing_SQL = new Desing_SQL();

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("promotor", promotor);

                data = desing_SQL.EjecutarStoredProcedure(SP_NET_GET_PAGOINGRESO_PROMOTOR, parametros);

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int UpdatePagoIngreso(string folioSIAC, bool banPago)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    TBL_GLOBAL tBL_GLOBAL = conexion.TBL_GLOBAL.Where(x => x.FOLIO_SIAC == folioSIAC).FirstOrDefault();

                    tBL_GLOBAL.ESTATUS_PAGO_INGRESO = banPago;
                    tBL_GLOBAL.FECHA_PAGO_INGRESO = DateTime.Now;

                    conexion.Entry(tBL_GLOBAL).State = EntityState.Modified;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdatePagoPosteo(string folioSIAC, bool banPago)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    TBL_GLOBAL tBL_GLOBAL = conexion.TBL_GLOBAL.Where(x => x.FOLIO_SIAC == folioSIAC).FirstOrDefault();

                    tBL_GLOBAL.ESTATUS_PAGO_POSTEO = banPago;
                    tBL_GLOBAL.FECHA_PAGO_POSTEO = DateTime.Now;

                    conexion.Entry(tBL_GLOBAL).State = EntityState.Modified;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdatePagoIngresoSupervisor(string folioSIAC, bool banPago)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    TBL_GLOBAL tBL_GLOBAL = conexion.TBL_GLOBAL.Where(x => x.FOLIO_SIAC == folioSIAC).FirstOrDefault();

                    tBL_GLOBAL.ESTATUS_PAGO_INGRESO_LIDER = banPago;
                    tBL_GLOBAL.FECHA_PAGO_INGRESO_LIDER = DateTime.Now;

                    conexion.Entry(tBL_GLOBAL).State = EntityState.Modified;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdatePagoPosteoSupervisor(string folioSIAC, bool banPago)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    TBL_GLOBAL tBL_GLOBAL = conexion.TBL_GLOBAL.Where(x => x.FOLIO_SIAC == folioSIAC).FirstOrDefault();

                    tBL_GLOBAL.ESTATUS_PAGO_POSTEO_LIDER = banPago;
                    tBL_GLOBAL.FECHA_PAGO_POSTEO_LIDER = DateTime.Now;

                    conexion.Entry(tBL_GLOBAL).State = EntityState.Modified;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList GetAllServicios()
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var lista = (from a in conexion.TBL_COMISION_PROMOTOR
                                 group a.SERVICIO by a.SERVICIO into serviceGroup
                                 orderby serviceGroup.Key
                                 select new {
                                     SERVICIO = serviceGroup.Key
                                 }
                                 ).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList GetAllTipo()
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var lista = (from a in conexion.TBL_COMISION_PROMOTOR
                                 group a.TIPO by a.TIPO into tipoGroup
                                 orderby tipoGroup.Key
                                 select new
                                 {
                                     TIPO = tipoGroup.Key
                                 }
                                 ).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList GetAllPaqutes()
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var lista = (from a in conexion.TBL_COMISION_PROMOTOR
                                 group a.PAQUETE by a.PAQUETE into paqueteGroup
                                 orderby paqueteGroup.Key
                                 select new
                                 {
                                     PAQUETE = paqueteGroup.Key
                                 }
                                 ).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
