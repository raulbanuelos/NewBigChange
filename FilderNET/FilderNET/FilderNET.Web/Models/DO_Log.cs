using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilderNET.Web.Models
{
    public class DO_Log
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
    }
}