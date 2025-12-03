using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Interfaz
{
    public interface ICalificable
    {
        double? CalificacionFinal { get; set; }
        int Creditos { get; }

        // Método para registrar la calificación, podría lanzar CalificacionInvalidaException
        void RegistrarCalificacion(double nota);
    }
}
