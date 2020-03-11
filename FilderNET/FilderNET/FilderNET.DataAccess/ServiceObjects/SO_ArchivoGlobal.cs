using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilderNET.DataAccess.ServiceObjects
{
    public class SO_ArchivoGlobal
    {
        public IList GetFiles(int idGlobal)
        {
            try
            {
                using (var Conexion = new EntitiesFilder())
                {
                    var fileList = (from a in Conexion.TBL_ARCHIVO_GLOBAL
                                    where a.ID_GLOBAL == idGlobal
                                    select a).ToList();

                    return fileList;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int Insert(string ext, int idGlobal,string nombreArchivo,string path)
        {
            try
            {
                using (var Conexion = new EntitiesFilder())
                {
                    TBL_ARCHIVO_GLOBAL tBL_ARCHIVO = new TBL_ARCHIVO_GLOBAL();

                    tBL_ARCHIVO.EXT = ext;
                    tBL_ARCHIVO.ID_GLOBAL = idGlobal;
                    tBL_ARCHIVO.NOMBRE_ARCHIVO = nombreArchivo;
                    tBL_ARCHIVO.RUTA = path;

                    Conexion.TBL_ARCHIVO_GLOBAL.Add(tBL_ARCHIVO);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(int idGlobal)
        {
            try
            {
                using (var Conexion = new EntitiesFilder())
                {
                    List<TBL_ARCHIVO_GLOBAL> tBLs = Conexion.TBL_ARCHIVO_GLOBAL.Where(x => x.ID_GLOBAL == idGlobal).ToList();

                    foreach (TBL_ARCHIVO_GLOBAL archivo in tBLs)
                    {
                        Conexion.Entry(archivo).State = EntityState.Deleted;
                    }

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
