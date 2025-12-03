using CapaNegocio.Base.GestionAcademica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Base
{
    public class Profesor : Persona
    {
        public int IdProfesor { get; set; }
        public string NumeroEmpleado { get; set; } // Identificador único del profesor

        // Propiedades de la persona base: Nombre, Apellido, Email, Telefono

        public Profesor(string numeroEmpleado, string nombre, string apellido)
            : base(nombre, apellido)
        {
            NumeroEmpleado = numeroEmpleado;
        }

        // Puedes añadir aquí métodos específicos de negocio para el Profesor,
        // como AsignarCalificacion (que luego se llamaría a CN_Calificaciones)
    }
}
