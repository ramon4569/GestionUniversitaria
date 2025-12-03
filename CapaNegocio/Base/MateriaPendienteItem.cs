using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Base
{
    public class MateriaPendienteItem
    {
        public int IdMatricula { get; set; }
        public string CodigoSeccion { get; set; }
        public string Display { get; set; }

        public MateriaPendienteItem(int idMatricula, string codigoSeccion, string display)
        {
            IdMatricula = idMatricula;
            CodigoSeccion = codigoSeccion;
            Display = display;
        }
    }
}
