using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaNegocio.Excepciones;
using CapaNegocio.Excepciones.CapaNegocio.Excepciones;
using CapaNegocio.Interfaz;

namespace CapaNegocio.Base
{
    public class Inscripcion : ICalificable
    {
        public int Id { get; set; }
        public string MatriculaEstudiante { get; set; }

        // Relación: Una inscripción tiene UNA materia
        public Materia Materia { get; set; }

        public double? Calificacion { get; set; } // Puede ser null
        public DateTime FechaInscripcion { get; set; }


        // Calculamos los puntos de honor para el índice (Nota * Créditos)
        public double PuntosHonor => (Calificacion ?? 0) * (Materia?.Creditos ?? 0);

        public int IdMatricula { get; set; }
        public int IdEstudiante { get; set; }
        public int IdSeccion { get; set; }
        public double AsistenciaPorc { get; set; } = 0.00;

        // Propiedades de navegación (necesarias para los reportes y el índice)
        public string CodigoSeccion { get; set; }

        // Implementación de ICalificable
        public double? CalificacionFinal { get; set; }

        public int Creditos => Materia?.Creditos ?? 0; // Accede a los créditos de la materia

        public Inscripcion() { }

        // Método para registrar la calificación con validación
        public void RegistrarCalificacion(double nota)
        {
            if (nota < 0 || nota > 100)
            {
                // Lanza la excepción requerida
                throw new CalificacionInvalidaException("La calificación debe estar entre 0 y 100.");
            }
            this.CalificacionFinal = nota;
        }

    }
}
