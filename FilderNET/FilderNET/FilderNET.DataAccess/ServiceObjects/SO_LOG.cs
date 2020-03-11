using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilderNET.DataAccess.ServiceObjects
{
    public class SO_LOG
    {
        public int Insert(string usuario, string descripcion)
        {
            try
            {
                using (var conexion = new EntitiesFilder())
                {
                    TBL_LOG tBL_LOG = new TBL_LOG();

                    tBL_LOG.FECHA = DateTime.Now;
                    tBL_LOG.USUARIO = usuario;
                    tBL_LOG.DESCRIPCION = descripcion;

                    conexion.TBL_LOG.Add(tBL_LOG);

                    return conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {
                return 0;
            }
        }

        public IList Get()
        {
            try
            {
                using (var Conexion = new EntitiesFilder())
                {
                    var lista = (from a in Conexion.TBL_LOG
                                 orderby a.FECHA descending
                                 select a).ToList().Take(500).ToList();
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
