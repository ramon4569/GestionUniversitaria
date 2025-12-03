using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Excepciones
{
    public class CreditosInsuficientesException : Exception
    {
        public CreditosInsuficientesException() : base("El estudiante no cumple con los prerrequisitos necesarios para inscribir esta materia.") { }
        public CreditosInsuficientesException(string message) : base(message) { }
        public CreditosInsuficientesException(string message, Exception innerException) : base(message, innerException) { }
        protected CreditosInsuficientesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
