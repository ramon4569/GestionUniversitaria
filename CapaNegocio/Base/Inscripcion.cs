using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Base
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public string MatriculaEstudiante { get; set; }

        // Relación: Una inscripción tiene UNA materia
        public Materia Materia { get; set; }

        public double? Calificacion { get; set; } // Puede ser null
        public DateTime FechaInscripcion { get; set; }

        // Calculamos los puntos de honor para el índice (Nota * Créditos)
        public double PuntosHonor => (Calificacion ?? 0) * (Materia?.Creditos ?? 0);
    }
}
