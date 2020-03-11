using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilderNET.Web.Models
{
    public class ModeloUsuarios
    {
        public int ID_USUARIO { get; set; }
        public int ID_JERARQUIA { get; set; }
        public int ID_JEFE { get; set; }
        public string USUARIO { get; set; }
        public string CONTRASENA { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRE { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
        public string FOTO { get; set; }
        public bool ACTIVO { get; set; }
        public DateTime FECHA_CREACION { get; set; }
        public DateTime FECHA_ACTUALIZACION { get; set; }


        public string SUPERVISOR_SELECTED { get; set; }
        public string JERARQUIA_SELECTED { get; set; }

        public string NOMBRE_COMPLETO
        {
            get
            {
                return NOMBRE + " " + APELLIDO_PATERNO + " " + APELLIDO_MATERNO;
            }
        }

        public ModeloUsuarios()
        {
            FOTO = string.Empty;
        }
    }
}