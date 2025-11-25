using CapaNegocio.Excepciones;
using CapaNegocio.ServiciosCD;
using CapaNegocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Servicios
{
    public class InscripcionService
    {
        private InscripcionDAL _inscripcionDAL = new InscripcionDAL();

        // Opción 2 del Menú: Inscribir
        public void InscribirMateria(string matricula, string codigoMateria)
        {
            // Validar que no esté inscrito ya (Lógica podría ir en DAL o aquí)
            if (_inscripcionDAL.ExisteInscripcion(matricula, codigoMateria))
            {
                throw new Exception("El estudiante ya tiene inscrita esta materia.");
            }

            _inscripcionDAL.Inscribir(matricula, codigoMateria);
        }

        // Opción 3 del Menú: Calificar
        // En InscripcionService.cs

        public void RegistrarCalificacion(string matricula, string codigoMateria, double nota)
        {
            // 1. Validación de Rango (0-100)
            if (nota < 0 || nota > 100)
            {
                throw new CalificacionInvalidaException(nota);
            }

            // 2. Guardar (Ahora sí coinciden los tipos de datos con el DAL)
            _inscripcionDAL.ActualizarNota(matricula, codigoMateria, nota);
        }
    }
}
