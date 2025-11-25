using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Acceso
{
    
    public class AutenticacionService
    {
        // Este método debería llamar a la DAL para verificar las credenciales en la DB o lista.
        public bool ValidarCredenciales(string usuario, string contrasena)
        {
            // **Lógica de Autenticación temporal o de prueba:**
            // Reemplaza esto con una llamada a tu DAL (Repositorio)
            return usuario == "admin" && contrasena == "uce123";
        }
    }
}
