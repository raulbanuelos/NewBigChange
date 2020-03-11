﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilderNET.DataAccess.ServiceObjects
{
    public class SO_Factura
    {
        public IList Get()
        {
            try
            {
                using (var Conexion = new EntitiesFilder())
                {
                    var lista = (from a in Conexion.TBL_NET_FACTURA
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
