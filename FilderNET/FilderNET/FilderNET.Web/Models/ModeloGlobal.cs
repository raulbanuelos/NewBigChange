using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilderNET.Web.Models
{
    public class ModeloGlobal
    {
        public int ID_GLOBAL { get; set; }
        public int ID_PROMOTOR { get; set; }
        public DateTime FECHA_ELABORACION { get; set; }
        public string FILDER { get; set; }
        public DateTime FECHA_CAPTURA { get; set; }
        public string MESA_CONTROL { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string TEL_CONTACTO { get; set; }
        public string SERVICIO { get; set; }
        public string TIPO { get; set; }
        public string PAQUETE { get; set; }
        public string FOLIO_SIAC { get; set; }
        public string PAGO_A_PROMOTOR { get; set; }
        public string OBSERVACIONES { get; set; }
        public List<DO_Archivo> ARCHIVOS { get; set; }
        public string AREA { get; set; }

        public DateTime FECHA_PAGO_INGRESO { get; set; }
        public bool ESTATUS_PAGO_POSTEO { get; set; }
        public DateTime FECHA_PAGO_POSTEO { get; set; }
        public bool ESTATUS_PAGO_INGRESO { get; set; }
        public string ESTATUS_SIAC { get; set; }
        public string ESTATUS_PISA_MULTIORDEN { get; set; }

        public string NOMBRE_PROMOTOR { get; set; }

        public string PROMOTOR_SELECTED { get; set; }
        public string SERVICIO_SELECTED { get; set; }
        public string TIPO_SELECTED { get; internal set; }
        public string PAQUETE_SELECTED { get; internal set; }

        public double FACTURA_INGRESO { get; set; }
        public double FACTURA_POSTEO { get; set; }
        public double FACTURA_BONO_COMERCIO { get; set; }
        public double FACTURA_BONO_EFECTIVIDAD { get; set; }
        public double FACTURA_BONO_CALIDAD { get; set; }

    }
}