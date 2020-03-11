using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilderNET.Web.Models
{
    public class DO_Pago
    {
        public string NOMBRE_CLIENTE { get; set; }
        public string SERVICIO { get; set; }
        public string TIPO_LINEA { get; set; }
        public string PAQUETE { get; set; }
        public string NOMBRE_PROMOTOR { get; set; }
        public string FOLIO_SIAC { get; set; }
        public double INGRESO { get; set; }
        public string CONCEPTO { get; set; }
        public string AREA { get; set; }

    }
}