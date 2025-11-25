using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Excepciones
{
    public class MatriculaDuplicadaException : Exception
    {
        public MatriculaDuplicadaException(string matricula)
            : base($"La matrícula '{matricula}' ya está registrada en el sistema.") { }
    }
}
