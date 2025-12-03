using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Excepciones
{
    public class MatriculaDuplicadaException : Exception
    {
        public MatriculaDuplicadaException() : base("La matrícula ya está registrada en el sistema.") { }
        public MatriculaDuplicadaException(string message) : base(message) { }
        public MatriculaDuplicadaException(string message, Exception innerException) : base(message, innerException) { }
        protected MatriculaDuplicadaException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
