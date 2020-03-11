using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilderNET.Web.Models
{
    public class DO_Factura
    {
        public DateTime Quincena { get; set; }
        public string Folio { get; set; }
        public double ImporteBase { get; set; }
        public double ImportePosteo { get; set; }
        public string PisaE { get; set; }
        public string Procede2 { get; set; }
        public string Pago2 { get; set; }
        public string Fecha { get; set; }
        public string OSPago { get; set; }
        public string Estatus { get; set; }
        public string LineaContratada { get; set; }
        public string Paquete { get; set; }

    }
}