// CapaNegocio/Servicios/ConsultasService.cs (Corregido y Listo para LINQ)

using CapaNegocio.Base;
// *** Asegúrate de que este namespace exista y contenga EstudianteDAL o similar ***
using CapaNegocio.ServiciosCD;
using System;
using System.Collections.Generic;
using System.Linq; // CRUCIAL para todas las consultas LINQ
using System.Text;
using System.Threading.Tasks;

// Nota: Asegúrate de que el archivo esté en la carpeta 'Servicios'
namespace CapaNegocio.Servicios
{
    public class ConsultasService
    {
        // Instancia de Acceso a Datos (DAL / CD) para obtener la data completa
        // Asegúrate de que EstudianteDAL tenga el método ObtenerTodosCompletos() 
        // y que devuelva List<Estudiante>
        private EstudianteDAL _estudianteDAL = new EstudianteDAL();

        public object ObtenerListadoGeneral()
        {
            List<Estudiante> lista = _estudianteDAL.ObtenerTodosCompletos();

            // Proyectamos los datos para que se vean bonitos en el Grid
            return lista.Select(e => new
            {
                Matricula = e.Matricula,
                Nombre = $"{e.Nombre} {e.Apellido}",
                Carrera = e.Carrera,
                Email = e.Email,
                Indice = e.IndiceAcademico // Propiedad calculada
            }).ToList();
        }

        // -----------------------------------------------------------
        // 2. BUSCADOR (Where)
        // -----------------------------------------------------------
        public object BuscarEstudiante(string texto)
        {
            List<Estudiante> lista = _estudianteDAL.ObtenerTodosCompletos();

            if (string.IsNullOrEmpty(texto)) return ObtenerListadoGeneral();

            // Aplicación de Where() para el filtro
            return lista
                .Where(e => e.Nombre.ToLower().Contains(texto.ToLower()) ||
                            e.Apellido.ToLower().Contains(texto.ToLower()) ||
                            e.Matricula.Contains(texto))
                .Select(e => new
                {
                    Matricula = e.Matricula,
                    Nombre = $"{e.Nombre} {e.Apellido}",
                    Carrera = e.Carrera,
                    Indice = e.IndiceAcademico
                }).ToList();
        }

        // 1. Estudiantes en Riesgo (Where, OrderBy)
        public object ObtenerEstudiantesEnRiesgo()
        {
            List<Estudiante> lista = _estudianteDAL.ObtenerTodosCompletos();

            // Aplicación de Where() e OrderBy()
            return lista
                .Where(e => e.IndiceAcademico < 2.0m && e.MateriasCursadas.Any()) // Usar 'm' para decimal si IndiceAcademico es decimal
                .OrderBy(e => e.IndiceAcademico)
                .Select(e => new {
                    Matricula = e.Matricula,
                    Nombre = $"{e.Nombre} {e.Apellido}",
                    Indice = e.IndiceAcademico
                }).ToList();
        }

        // 2. Promedio por Carrera (GroupBy, Average, Count)
        public object ObtenerPromedioPorCarrera()
        {
            List<Estudiante> lista = _estudianteDAL.ObtenerTodosCompletos();

            // Aplicación de GroupBy(), Count() y Average()
            return lista
                .GroupBy(e => e.Carrera)
                .Select(grupo => new {
                    Carrera = grupo.Key,
                    Cantidad = grupo.Count(),
                    // Promedio requiere que IndiceAcademico sea numérico (decimal o double)
                    Promedio = Math.Round(grupo.Average(e => (double)e.IndiceAcademico), 2)
                }).ToList();
        }

        // Método para filtrar específicamente por Carrera (Where)
        public object ObtenerPorCarrera(string carreraSeleccionada)
        {
            var todos = _estudianteDAL.ObtenerTodosCompletos();

            // Aplicación de Where()
            return todos
                .Where(e => e.Carrera.Equals(carreraSeleccionada, StringComparison.OrdinalIgnoreCase))
                .Select(e => new {
                    Matricula = e.Matricula,
                    Nombre = $"{e.Nombre} {e.Apellido}",
                    Carrera = e.Carrera,
                    Email = e.Email,
                    Indice = e.IndiceAcademico
                }).ToList();
        }

        // 3. Materias más Reprobadas (SelectMany, Where, GroupBy, Count)
        public object ObtenerMateriasReprobadas()
        {
            List<Estudiante> lista = _estudianteDAL.ObtenerTodosCompletos();

            // SelectMany() aplana la lista de listas (todas las inscripciones de todos los estudiantes)
            var todasLasInscripciones = lista.SelectMany(e => e.MateriasCursadas);

            // Aplicación de SelectMany(), Where(), GroupBy(), Count() y OrderByDescending()
            return todasLasInscripciones
                // Nota: Usar CalificacionFinal que está en Inscripcion.
                // Asumo que 'Calificacion' es un alias de CalificacionFinal en la Entidad Inscripcion.
                .Where(i => i.CalificacionFinal.HasValue && i.CalificacionFinal.Value < 70) // Reprobados
                .GroupBy(i => i.Materia.Codigo)
                .Select(grupo => new {
                    Materia = grupo.First().Materia.Nombre,
                    Reprobados = grupo.Count()
                })
                .OrderByDescending(x => x.Reprobados)
                .ToList();
        }

        // Top Estudiantes (OrderByDescending, Take, Select)
        public object ObtenerTopEstudiantes()
        {
            List<Estudiante> lista = _estudianteDAL.ObtenerTodosCompletos();

            // Aplicación de OrderByDescending(), Take() y Select()
            return lista
                .OrderByDescending(e => e.IndiceAcademico)
                .Take(10)
                .Select(e => new {
                    Matricula = e.Matricula,
                    Nombre = $"{e.Nombre} {e.Apellido}",
                    Indice = e.IndiceAcademico,
                    // Lógica de Honores
                    Honor = e.IndiceAcademico >= 3.8m ? "Summa Cum Laude" :
                            e.IndiceAcademico >= 3.5m ? "Magna Cum Laude" : "Cum Laude"
                }).ToList();
        }
    }
}