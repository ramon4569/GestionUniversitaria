using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaNegocio.Validaciones
{
    public static class Validador
    {
        // Expresión regular para Emails
        public static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            // Patrón estándar de email
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patron);
        }

        // Expresión regular para Teléfonos (Ej: 809-555-5555 o solo números)
        public static bool EsTelefonoValido(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono)) return false;
            // Acepta números, guiones y espacios. Min 10 caracteres.
            string patron = @"^[\d\-\s]{10,}$";
            return Regex.IsMatch(telefono, patron);
        }
    }
}
