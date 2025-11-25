using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Excepciones
{
    public class CalificacionInvalidaException : Exception
    {
        public CalificacionInvalidaException(double nota)
            : base($"La calificación {nota} no es válida. Debe estar entre 0 y 100.") { }
    }
}
