// InscripcionService.cs (CORREGIDO)

using CapaNegocio.Excepciones;
using CapaNegocio.ServiciosCD;
using CapaNegocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaNegocio.Excepciones.CapaNegocio.Excepciones;
// *** ELIMINAR ESTA LÍNEA O CORREGIR EL NAMESPACE DUPLICADO ***
// using CapaNegocio.Excepciones.CapaNegocio.Excepciones; 

namespace CapaNegocio.Servicios
{
    public class InscripcionService
    {
        private InscripcionDAL _inscripcionDAL = new InscripcionDAL();

        // Opción 2 del Menú: Inscribir
        public void InscribirMateria(string matricula, string codigoMateria)
        {
            if (_inscripcionDAL.ExisteInscripcion(matricula, codigoMateria))
            {
                throw new Exception("El estudiante ya tiene inscrita esta materia.");
            }

            _inscripcionDAL.Inscribir(matricula, codigoMateria);
        }

        // Opción 3 del Menú: Calificar
        public void RegistrarCalificacion(string matricula, string codigoMateria, double nota)
        {
            // 1. Validación de Rango (0-100)
            if (nota < 0 || nota > 100)
            {
                // CORRECCIÓN: Pasar un string al constructor de la excepción
                throw new CalificacionInvalidaException($"La calificación {nota} debe estar entre 0 y 100.");
            }

            // 2. Guardar
            _inscripcionDAL.ActualizarNota(matricula, codigoMateria, nota);
        }
    }
}