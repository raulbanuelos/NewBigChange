//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FilderNET.DataAccess.ServiceObjects
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_NET_BONO_HISTORICO
    {
        public int ID_BONO_HISTORICO { get; set; }
        public Nullable<System.DateTime> QUINCENA { get; set; }
        public string RUBRO { get; set; }
        public string FOLIO { get; set; }
        public Nullable<double> IMPORTE_EFECTIVIDAD { get; set; }
        public Nullable<double> IMPORTE_COMERCIO { get; set; }
        public Nullable<double> IMPORTE_CALIDAD { get; set; }
    }
}
