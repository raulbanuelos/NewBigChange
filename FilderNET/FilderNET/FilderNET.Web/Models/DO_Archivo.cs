using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilderNET.Web.Models
{
    public class DO_Archivo
    {
        public int ID_ARCHIVO { get; set; }
        public int ID_GLOBAL { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public string EXT { get; set; }
        public string RUTA { get; set; }
    }
}