using CapaDatos.Conexion;
using CapaNegocio.Base;
using CapaNegocio.Servicios;
using CapaNegocio.ServiciosCD;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Acceso
{
    public class UsuarioService
    {
        // Instancia de la capa de datos
        private UsuarioDAL _usuarioDAL = new UsuarioDAL();

        public Usuario ValidarAcceso(string usuario, string clave)
        {
            // 1. Validaciones básicas antes de ir a la BD
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
            {
                throw new Exception("Debe ingresar usuario y contraseña.");
            }

            // 2. Llamamos al DAL
            Usuario usuarioEncontrado = _usuarioDAL.Login(usuario, clave);

            // 3. Verificamos si encontró algo
            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuario o contraseña incorrectos.");
            }

            // 4. Devolvemos el usuario completo al formulario
            return usuarioEncontrado;
        }
    }
}
