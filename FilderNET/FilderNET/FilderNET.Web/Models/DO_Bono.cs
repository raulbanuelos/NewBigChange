using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilderNET.Web.Models
{
    public class DO_Bono
    {
        public int ID { get; set; }
        public DateTime Quincena { get; set; }
        public string Rubro { get; set; }
        public string Folio { get; set; }
        public double ImporteEfectividad { get; set; }
        public double ImporteComercio { get; set; }
        public double ImporteCalidad { get; set; }
    }
}