using System;
using System.Collections;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using FilderNET.DataAccess.SQLServer;
using System.Collections.Generic;

namespace FilderNET.DataAccess.ServiceObjects
{
    public class SO_FOLIO
    {

        /// <summary>
        /// Método que obtiene todos los datos de los folios
        /// </summary>
        /// <param name="FolioBuscar"></param>
        /// <returns></returns>
        public IList GetAllFolios()
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var lista = (from a in conexion.TBL_FOLIOS
                                 select a).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que busca un folio en especifico por FOLIO SIAC
        /// </summary>
        /// <param name="FolioBuscar"></param>
        /// <returns></returns>
        public IList GetInfoFolios(string FolioBuscar)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var lista = (from a in conexion.TBL_FOLIOS
                                 where a.FOLIO_SIAC.Contains(FolioBuscar)
                                 select a).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método para eliminar un folio de la BD.
        /// </summary>
        /// <param name="IdFolio"></param>
        /// <returns></returns>
        public int DeleteFolio(string FolioSIAC)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {

                    TBL_FOLIOS Obj = conexion.TBL_FOLIOS.Where(x => x.FOLIO_SIAC == FolioSIAC).FirstOrDefault();

                    conexion.Entry(Obj).State = EntityState.Deleted;

                    return conexion.SaveChanges();

                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que verifica si un folio ya existe
        /// </summary>
        /// <param name="FOLIO_SIAC"></param>
        /// <returns></returns>
        public string VerificarExistenciaFolio(string FOLIO_SIAC)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var lista = (from a in conexion.TBL_FOLIOS
                                 where a.FOLIO_SIAC == FOLIO_SIAC
                                 select a.FOLIO_SIAC).ToList().FirstOrDefault();

                    return lista;
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        /// <summary>
        /// Método para insertar un nuevo folio. se inserta con un procedimiento almacenado
        /// </summary>
        /// <returns></returns>
        public Task<int> SetORUpdateFolio(string FOLIO_SIAC, Nullable<System.DateTime> FECHA_CAPTURA, string ESTRATEGIA, string PROMOTOR, string ESTATUS_SIAC,
            string TIPO_LINEA, string LINEA_CONTRATADA, string AREA, string DIVICION, string TIENDA, string PAQUETE, string OBSERVACIONES,
            string RESPUESTA_TELMEX, string MOTIVO_RECHAZO, string TELEFONO_ASIGNADO, string TELEFONO_PORTADO, string OS_ALTA_LINEA_MULTIORDEN,
            string FECHA_OS_ALTA_LINEA_MULTIORDEN, string ORDEN_SERVICIO_TV, string FECHA_OS_TV, string CAMPANA, string ESTATUS_ATENCION_MULTIORDEN,
            string ESTATUS_PISA_MULTIORDEN, string PISA_OS_FECHA_POSTEO_MULTIORDEN, string ESTATUS_PISA_TV, string PISA_OS_FECHA_POSTEO_TV,
            string FECHA_CAMBIO_ESTATUS_SIAC, string CLAVE_EMPRESA, string NOMBRE_EMPRESA, string SERVICIO_FACTURACION_TERCEROS,
            string ETAPA_PISA_MULTIORDEN, string ETAPA_PISA_TV, string ETIQUETA_TRAFICO_VOZ, string TRAFICO_VOZ_ENTRANTE,
            string TRAFICO_VOZ_SALIENTE, string FECHA_TRAFICO_VOZ, string TRAFICO_DATOS, string FECHA_TRAFICO_DATOS, string FECHA_FACTURACION,
            string DESCRIPCION_VALIDA_ADEUDO, string CORREO_ELECTRONICO, string FECHA_NACIMIENTO, string ID, string TERMINAL, string DISTRITO,
            string TELCELULAR, Nullable<System.DateTime> FECHA_CREACION, Nullable<System.DateTime> FECHA_CALCULO_COMISION, Nullable<bool> ESTATUS_PAGADO,
            string FolioExistente)
        {
            return Task.Run(() =>
            {
                try
                {
                    DataSet datos = null;
                    //Se crea conexion a la BD.
                    Desing_SQL conexion = new Desing_SQL();

                    //Se inicializa un dictionario que contiene propiedades de tipo string y un objeto.
                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    //se agregan el nombre y el objeto de los parámetros.

                    parametros.Add("FOLIO_SIAC", FOLIO_SIAC);
                    parametros.Add("FECHA_CAPTURA", FECHA_CAPTURA);
                    parametros.Add("ESTRATEGIA", ESTRATEGIA);
                    parametros.Add("PROMOTOR", PROMOTOR);
                    parametros.Add("ESTATUS_SIAC", ESTATUS_SIAC);
                    parametros.Add("TIPO_LINEA", TIPO_LINEA);
                    parametros.Add("LINEA_CONTRATADA", LINEA_CONTRATADA);
                    parametros.Add("AREA", AREA);
                    parametros.Add("DIVICION", DIVICION);
                    parametros.Add("TIENDA", TIENDA);
                    parametros.Add("PAQUETE", PAQUETE);
                    parametros.Add("OBSERVACIONES", OBSERVACIONES);
                    parametros.Add("RESPUESTA_TELMEX", RESPUESTA_TELMEX);
                    parametros.Add("MOTIVO_RECHAZO", MOTIVO_RECHAZO);
                    parametros.Add("TELEFONO_ASIGNADO", TELEFONO_ASIGNADO);
                    parametros.Add("TELEFONO_PORTADO", TELEFONO_PORTADO);
                    parametros.Add("OS_ALTA_LINEA_MULTIORDEN", OS_ALTA_LINEA_MULTIORDEN);
                    parametros.Add("FECHA_OS_ALTA_LINEA_MULTIORDEN", FECHA_OS_ALTA_LINEA_MULTIORDEN);
                    parametros.Add("ORDEN_SERVICIO_TV", ORDEN_SERVICIO_TV);
                    parametros.Add("FECHA_OS_TV", FECHA_OS_TV);
                    parametros.Add("CAMPANA", CAMPANA);
                    parametros.Add("ESTATUS_ATENCION_MULTIORDEN", ESTATUS_ATENCION_MULTIORDEN);
                    parametros.Add("ESTATUS_PISA_MULTIORDEN", ESTATUS_PISA_MULTIORDEN);
                    parametros.Add("PISA_OS_FECHA_POSTEO_MULTIORDEN", PISA_OS_FECHA_POSTEO_MULTIORDEN);
                    parametros.Add("ESTATUS_PISA_TV", ESTATUS_PISA_TV);
                    parametros.Add("PISA_OS_FECHA_POSTEO_TV", PISA_OS_FECHA_POSTEO_TV);
                    parametros.Add("FECHA_CAMBIO_ESTATUS_SIAC", FECHA_CAMBIO_ESTATUS_SIAC);
                    parametros.Add("CLAVE_EMPRESA", CLAVE_EMPRESA);
                    parametros.Add("NOMBRE_EMPRESA", NOMBRE_EMPRESA);
                    parametros.Add("SERVICIO_FACTURACION_TERCEROS", SERVICIO_FACTURACION_TERCEROS);
                    parametros.Add("ETAPA_PISA_MULTIORDEN", ETAPA_PISA_MULTIORDEN);
                    parametros.Add("ETAPA_PISA_TV", ETAPA_PISA_TV);
                    parametros.Add("ETIQUETA_TRAFICO_VOZ", ETIQUETA_TRAFICO_VOZ);
                    parametros.Add("TRAFICO_VOZ_ENTRANTE", TRAFICO_VOZ_ENTRANTE);
                    parametros.Add("TRAFICO_VOZ_SALIENTE", TRAFICO_VOZ_SALIENTE);
                    parametros.Add("FECHA_TRAFICO_VOZ", FECHA_TRAFICO_VOZ);
                    parametros.Add("TRAFICO_DATOS", TRAFICO_DATOS);
                    parametros.Add("FECHA_TRAFICO_DATOS", FECHA_TRAFICO_DATOS);
                    parametros.Add("FECHA_FACTURACION", FECHA_FACTURACION);
                    parametros.Add("DESCRIPCION_VALIDA_ADEUDO", DESCRIPCION_VALIDA_ADEUDO);
                    parametros.Add("CORREO_ELECTRONICO", CORREO_ELECTRONICO);
                    parametros.Add("FECHA_NACIMIENTO", FECHA_NACIMIENTO);
                    parametros.Add("ID", ID);
                    parametros.Add("TERMINAL", TERMINAL);
                    parametros.Add("DISTRITO", DISTRITO);
                    parametros.Add("TECELULAR", TELCELULAR);
                    parametros.Add("FECHA_CREACION", FECHA_CREACION);
                    parametros.Add("FECHA_CALCULO_COMISION", FECHA_CALCULO_COMISION);
                    parametros.Add("ESTATUS_PAGADO", ESTATUS_PAGADO);

                    //si no existe se agrega un el nuevo registro
                    if (FolioExistente != "0")
                    {
                        //se ejecuta el procedimiento y se mandan los parámetros añadidos anteriormente.
                        datos = conexion.EjecutarStoredProcedure("SP_Set_NuevoFolio", parametros);

                    }
                    else //si existe solo se actualizan los registros
                    {
                        //se ejecuta el procedimiento y se mandan los parámetros añadidos anteriormente.
                        datos = conexion.EjecutarStoredProcedure("SP_UpdateFolio", parametros);
                    }


                    //Retorna el número de elementos en la tabla.
                    //return datos.Tables.Count;
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            });
        }
    }
}
