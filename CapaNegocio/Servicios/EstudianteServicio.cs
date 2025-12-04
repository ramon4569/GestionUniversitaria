using CapaNegocio.Excepciones;
using CapaNegocio.ServiciosCD;
using CapaNegocio.Validaciones;
using CapaNegocio.Base;
using CapaNegocio.ServiciosCD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Servicios
{
    public class EstudianteServicio
    {
        // Instancia de la capa de datos (La crearemos luego)
        private EstudianteDAL _estudianteDAL = new EstudianteDAL();


        public void RegistrarEstudiante(Estudiante est)
        {
            // 1. Validaciones de Formato (Regex)
            if (!Validador.EsEmailValido(est.Email))
                throw new ArgumentException("El formato del correo electrónico no es válido.");

            if (!Validador.EsTelefonoValido(est.Telefono))
                throw new ArgumentException("El formato del teléfono no es válido.");

            if (string.IsNullOrWhiteSpace(est.Matricula))
                throw new ArgumentException("La matrícula es obligatoria.");

            // 2. Validación de Negocio: Duplicados
            // Consultamos a la BD si ya existe
            if (_estudianteDAL.ExisteMatricula(est.Matricula))
            {
                throw new MatriculaDuplicadaException(est.Matricula);
            }

            // 3. Si todo está bien, llamamos al DAL para guardar
            _estudianteDAL.Insertar(est);
        }

        public List<Estudiante> ObtenerTodos()
        {
            
            return _estudianteDAL.ObtenerTodosCompletos();
        }

        public Estudiante BuscarPorMatricula(string matricula)
        {
            return _estudianteDAL.BuscarPorMatricula(matricula);
        }

        // Método para el buscador dinámico
        public List<Estudiante> BuscarDinamico(string texto)
        {
            var todos = _estudianteDAL.ObtenerTodosCompletos(); // <--- CAMBIO AQUÍ

            if (string.IsNullOrEmpty(texto)) return todos;

            return todos.Where(e => (e.Nombre != null && e.Nombre.ToLower().Contains(texto.ToLower())) ||
                                    (e.Apellido != null && e.Apellido.ToLower().Contains(texto.ToLower())) ||
                                    (e.Matricula != null && e.Matricula.Contains(texto))).ToList();
        }


    }
}
