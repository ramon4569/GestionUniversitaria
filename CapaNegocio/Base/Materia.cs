using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Base
{
    public class Materia
    {
        public int Id { get; set; }
        public string Codigo { get; set; } // Ej: ISW-122
        public string Nombre { get; set; }
        public int Creditos { get; set; }

        public override string ToString()
        {
            return $"{Codigo} - {Nombre}";
        }
    }
}
