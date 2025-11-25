using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Base
{
    namespace GestionAcademica.Entities
    {
        public abstract class Persona
        {
            public int Id { get; set; } // Para coincidir con la BD
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Email { get; set; }
            public string Telefono { get; set; }

            // Propiedad de ayuda para mostrar nombre completo
            public string NombreCompleto => $"{Nombre} {Apellido}";
        }
    }
}
