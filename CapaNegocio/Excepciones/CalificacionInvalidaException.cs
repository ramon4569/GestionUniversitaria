using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Excepciones
{
    // Excepciones/CalificacionInvalidaException.cs

    using System;
    using System.Runtime.Serialization;

    namespace CapaNegocio.Excepciones
    {
        // Asegúrate de que herede de System.Exception (o ApplicationException)
        public class CalificacionInvalidaException : Exception
        {
            // 1. Constructor básico
            public CalificacionInvalidaException()
            {
            }

            // 2. Constructor que acepta el mensaje (el que estás usando)
            public CalificacionInvalidaException(string message)
                : base(message)
            {
            }

            // 3. Constructor para encadenar excepciones
            public CalificacionInvalidaException(string message, Exception innerException)
                : base(message, innerException)
            {
            }

            // 4. Constructor para serialización (opcional pero recomendado)
            protected CalificacionInvalidaException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
    }
}
