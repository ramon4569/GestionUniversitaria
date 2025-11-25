using CapaNegocio.Base;

using CapaNegocio.ServiciosCD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Servicios
{
    public class ConsultasService
    {
        // Necesitamos traer los datos completos para procesarlos con LINQ
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
        // 2. BUSCADOR (Probablemente también te falte este)
        // -----------------------------------------------------------
        public object BuscarEstudiante(string texto)
        {
            List<Estudiante> lista = _estudianteDAL.ObtenerTodosCompletos();

            if (string.IsNullOrEmpty(texto)) return ObtenerListadoGeneral();

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

        // 1. Estudiantes en Riesgo (Indice < 2.0)
        public object ObtenerEstudiantesEnRiesgo()
        {
            List<Estudiante> lista = _estudianteDAL.ObtenerTodosCompletos(); // Trae estudiantes + materias

            return lista
                .Where(e => e.IndiceAcademico < 2.0 && e.MateriasCursadas.Any())
                .OrderBy(e => e.IndiceAcademico)
                .Select(e => new {
                    Matricula = e.Matricula,
                    Nombre = $"{e.Nombre} {e.Apellido}",
                    Indice = e.IndiceAcademico
                }).ToList();
        }

        // 2. Promedio por Carrera (GroupBy)
        public object ObtenerPromedioPorCarrera()
        {
            List<Estudiante> lista = _estudianteDAL.ObtenerTodosCompletos();

            return lista
                .GroupBy(e => e.Carrera)
                .Select(grupo => new {
                    Carrera = grupo.Key,
                    Cantidad = grupo.Count(),
                    Promedio = Math.Round(grupo.Average(e => e.IndiceAcademico), 2)
                }).ToList();
        }

        // Método para filtrar específicamente por Carrera
        public object ObtenerPorCarrera(string carreraSeleccionada)
        {
            // Traemos todos los datos
            var todos = _estudianteDAL.ObtenerTodosCompletos();

            // Filtramos con LINQ
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

        // 3. Materias más Reprobadas (SelectMany + Count)
        public object ObtenerMateriasReprobadas()
        {
            List<Estudiante> lista = _estudianteDAL.ObtenerTodosCompletos();

            // Aplanamos todas las inscripciones de todos los estudiantes
            var todasLasInscripciones = lista.SelectMany(e => e.MateriasCursadas);

            return todasLasInscripciones
                .Where(i => i.Calificacion != null && i.Calificacion < 70) // Reprobados
                .GroupBy(i => i.Materia.Codigo)
                .Select(grupo => new {
                    Materia = grupo.First().Materia.Nombre,
                    Reprobados = grupo.Count()
                })
                .OrderByDescending(x => x.Reprobados)
                .ToList();
        }

        public object ObtenerTopEstudiantes()
        {
            List<Estudiante> lista = _estudianteDAL.ObtenerTodosCompletos();

            return lista
                .OrderByDescending(e => e.IndiceAcademico)
                .Take(10)
                .Select(e => new {
                    Matricula = e.Matricula,
                    Nombre = $"{e.Nombre} {e.Apellido}",
                    Indice = e.IndiceAcademico,
                    Honor = e.IndiceAcademico >= 3.8 ? "Summa Cum Laude" :
                            e.IndiceAcademico >= 3.5 ? "Magna Cum Laude" : "Cum Laude"
                }).ToList();
        }
    }
}



