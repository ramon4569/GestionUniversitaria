using CapaNegocio.Base.GestionAcademica.Entities;
using CapaNegocio.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Base
{
    public partial class Estudiante : Persona, IComparable<Estudiante>
    {
        // Propiedades que coinciden con los campos de la tabla Estudiantes
        public int IdEstudiante { get; set; }
        public  string Matricula { get; set; } // Clave principal de negocio
        public  string Nombre { get; set; }
        public  string Apellido { get; set; }
        public  string Carrera { get; set; }
        public  string Email { get; set; }
        public  string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int SemestreActual { get; set; }
        public decimal IndiceAcademico { get; set; } = 0.00m; // Se calcula aparte o se carga.

        public string NombreCompleto
        {
            get { return $"{Nombre} {Apellido}"; }
        }

        // Lista de materias que ha inscrito este estudiante
        public List<Inscripcion> MateriasCursadas { get; set; }

        // Constructor para inicializar la lista y evitar errores
        public Estudiante(string matricula, string nombre, string apellido, string carrera)
                 : base(nombre, apellido)
        {
            Matricula = matricula;
            Carrera = carrera;
            MateriasCursadas = new List<Inscripcion>();
        }

        public Estudiante() : base(string.Empty, string.Empty)
        {
            // Inicializa la clase base Persona para evitar el error de compilación
        }
        public int CompareTo(Estudiante other)
        {
            if (other == null) return 1;
            // Ordenar de mayor a menor índice académico
            return other.IndiceAcademico.CompareTo(this.IndiceAcademico);
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

      

 
    }
}
    