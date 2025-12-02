using CapaNegocio.Base.GestionAcademica.Entities;
using CapaNegocio.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Base
{
    public partial class Estudiante : Persona, ICalificable, IComparable<Estudiante>
    {
        // Propiedades que coinciden con los campos de la tabla Estudiantes
        public int IdEstudiante { get; set; }
        public string Matricula { get; set; } // Clave principal de negocio
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Carrera { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int SemestreActual { get; set; }

        public string NombreCompleto
        {
            get { return $"{Nombre} {Apellido}"; }
        }

        // Lista de materias que ha inscrito este estudiante
        public List<Inscripcion> MateriasCursadas { get; set; }

        // Constructor para inicializar la lista y evitar errores
        public Estudiante()
        {
            MateriasCursadas = new List<Inscripcion>();
        }

        // --- LÓGICA DEL ÍNDICE ACADÉMICO (Requisito BLL) ---
        // Implementación de ICalificable
        public double CalcularIndice()
        {
            if (MateriasCursadas == null || MateriasCursadas.Count == 0) return 0;

            // Filtramos solo las materias que ya tienen nota (no son null)
            var materiasConNota = MateriasCursadas.Where(i => i.Calificacion != null).ToList();

            if (materiasConNota.Count == 0) return 0;

            double totalPuntos = materiasConNota.Sum(i => i.PuntosHonor);
            int totalCreditos = materiasConNota.Sum(i => i.Materia.Creditos);

            if (totalCreditos == 0) return 0;

            return Math.Round(totalPuntos / totalCreditos, 2);
        }

        // Propiedad de solo lectura para acceder fácil al índice
        public double IndiceAcademico => CalcularIndice();

        // --- COMPARACIÓN (Requisito IComparable) ---
        // Permite ordenar una lista de estudiantes automáticamente (por matrícula)
        public int CompareTo(Estudiante otro)
        {
            if (otro == null) return 1;
            return this.Matricula.CompareTo(otro.Matricula);
        }
    }
}
