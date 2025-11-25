using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Excepciones
{
    public class CreditosInsuficientesException : Exception
    {
        public CreditosInsuficientesException()
            : base("El estudiante no tiene créditos suficientes para esta acción.") { }
    }
}
