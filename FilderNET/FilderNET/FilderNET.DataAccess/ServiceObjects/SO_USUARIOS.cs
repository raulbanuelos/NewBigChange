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
    public class SO_USUARIOS
    {
        private string SP_NET_GET_LOGIN = "SP_NET_GET_LOGIN";

        /// <summary>
        /// Método para obtener todos los registros de la BD, y tambien obtitne un usuario en especifico
        /// </summary>
        /// <returns></returns>
        public IList GetAllUsuarios()
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var lista = (from a in conexion.TBL_USUARIO                                 
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
        /// Método que obtiene todo el registro de un usuario en especifico
        /// </summary>
        /// <param name="ID_USUARIO_BUSCAR"></param>
        /// <returns></returns>
        public IList GetAllUsuarioFiltrado(string USUARIO_BUSCAR)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    var lista = (from a in conexion.TBL_USUARIO
                                 where a.USUARIO.Contains(USUARIO_BUSCAR) || a.NOMBRE.Contains(USUARIO_BUSCAR) || a.APELLIDO_PATERNO.Contains(USUARIO_BUSCAR) || a.APELLIDO_MATERNO.Contains(USUARIO_BUSCAR)
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
        /// Método para agregar un nuevo usuario
        /// </summary>
        /// <returns></returns>
        public int SetNewUsuario(int ID_JERARQUIA, int ID_JEFE, string USUARIO, string CONTRASENA, string AMATERNO, 
            string APATERNO, string NOMBRE, DateTime FECHA_NACIMIENTO, string CURP, string RFC, string TELEFONO, 
            string EMAIL, string FOTO, bool ACTIVO, DateTime FECHA_CREACION, DateTime FECHA_ACTUALIZACION)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    TBL_USUARIO Obj = new TBL_USUARIO();

                    Obj.ID_JERARQUIA = ID_JERARQUIA;
                    Obj.ID_JEFE = ID_JEFE;
                    Obj.USUARIO = USUARIO;
                    Obj.CONTRASENA = CONTRASENA;
                    Obj.APELLIDO_MATERNO = AMATERNO;
                    Obj.APELLIDO_PATERNO = APATERNO;
                    Obj.NOMBRE = NOMBRE;
                    Obj.FECHA_NACIMIENTO = FECHA_NACIMIENTO;
                    Obj.CURP = CURP;
                    Obj.RFC = RFC;
                    Obj.TELEFONO = TELEFONO;
                    Obj.EMAIL = EMAIL;
                    Obj.FOTO = FOTO;
                    Obj.ACTIVO = ACTIVO;
                    Obj.FECHA_CREACION = DateTime.Now;
                    Obj.FECHA_ACTUALIZACION = DateTime.Now;

                    conexion.TBL_USUARIO.Add(Obj);

                    return conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar los datos de un usuario
        /// </summary>
        /// <returns></returns>
        public int UpdateUsuario(int ID_USUARIO, int ID_JERARQUIA, int ID_JEFE, string USUARIO, string CONTRASENA, string AMATERNO,
            string APATERNO, string NOMBRE, DateTime FECHA_NACIMIENTO, string CURP, string RFC, string TELEFONO,
            string EMAIL, string FOTO, bool ACTIVO, DateTime FECHA_CREACION, DateTime FECHA_ACTUALIZACION)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    TBL_USUARIO Obj = conexion.TBL_USUARIO.Where(x => x.ID_USUARIO == ID_USUARIO).FirstOrDefault();

                    Obj.ID_JERARQUIA = ID_JERARQUIA;
                    Obj.ID_JEFE = ID_JEFE;
                    Obj.USUARIO = USUARIO;
                    Obj.APELLIDO_MATERNO = AMATERNO;
                    Obj.APELLIDO_PATERNO = APATERNO;
                    Obj.NOMBRE = NOMBRE;
                    Obj.FECHA_NACIMIENTO = FECHA_NACIMIENTO;
                    Obj.CURP = CURP;
                    Obj.RFC = RFC;
                    Obj.TELEFONO = TELEFONO;
                    Obj.EMAIL = EMAIL;
                    Obj.FOTO = FOTO;
                    Obj.ACTIVO = ACTIVO;
                    Obj.FECHA_ACTUALIZACION = DateTime.Now;

                    conexion.Entry(Obj).State = EntityState.Modified;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro de un usario
        /// </summary>
        /// <returns></returns>
        public int DeleteUsuario(int ID_USUARIO)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    TBL_USUARIO Obj = conexion.TBL_USUARIO.Where(x => x.ID_USUARIO == ID_USUARIO).FirstOrDefault();

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
        /// Método que ejecuta el procedimiento almacenado que hace el Login del sistema
        /// </summary>
        /// <param name="USUARIO"></param>
        /// <param name="CONTRASENA"></param>
        /// <returns></returns>
        public DataSet GetLogin(string USUARIO, string CONTRASENA)
        {
            try
            {
                DataSet Data = null;

                Desing_SQL Conexion = new Desing_SQL();

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("usuario", USUARIO);
                parametros.Add("contrasena", CONTRASENA);

                Data = Conexion.EjecutarStoredProcedure(SP_NET_GET_LOGIN, parametros);

                return Data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que retorna el usuario a partir de un ID.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public IList GetUsuario(int idUsuario)
        {
            try
            {
                using (var Conexion = new EntitiesFilder())
                {
                    var lista = (from a in Conexion.TBL_USUARIO
                                 where a.ID_USUARIO == idUsuario
                                 select a).ToList();

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
