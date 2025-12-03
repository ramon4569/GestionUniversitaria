// Base/Repositorio.cs (O en la carpeta Servicios)

using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaNegocio.Base
{
    // Clase Genérica Repositorio con restricción 'where T : class'
    public class Repositorio<T> where T : class
    {
        // Usamos un Dictionary para acceso rápido por clave (ej: Matrícula, ID)
        // La clave es de tipo string, pero la haríamos dinámica en una implementación real.
        // Aquí asumimos que T tiene una propiedad clave (ej. T es Estudiante con Matricula)
        private readonly Dictionary<string, T> _coleccion;

        public Repositorio()
        {
            _coleccion = new Dictionary<string, T>();
        }

        // Método para agregar un elemento. La clave debe ser extraída de T.
        // En una implementación real, tendríamos una interfaz IEntity con una propiedad Key.
        // Por simplicidad, este método requerirá la clave.
        public void Agregar(string clave, T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (_coleccion.ContainsKey(clave))
            {
                throw new InvalidOperationException($"Ya existe un elemento con la clave: {clave}");
            }
            _coleccion.Add(clave, item);
        }

        // Obtener un elemento por su clave
        public T ObtenerPorClave(string clave)
        {
            if (_coleccion.TryGetValue(clave, out T item))
            {
                return item;
            }
            return null; // O lanza una excepción
        }

        // Obtener todos los elementos (útil para consultas LINQ)
        public List<T> ObtenerTodo()
        {
            return _coleccion.Values.ToList();
        }

        // Ejemplo de uso de LINQ (Requisito)
        public List<T> Buscar(Func<T, bool> predicado)
        {
            // T.Where() con expresión lambda
            return _coleccion.Values.Where(predicado).ToList();
        }

        // Ejemplo de eliminación
        public bool Eliminar(string clave)
        {
            return _coleccion.Remove(clave);
        }
    }
}