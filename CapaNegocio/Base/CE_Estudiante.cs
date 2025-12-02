using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos.Conexion;
using Microsoft.Data.SqlClient;


namespace CapaNegocio.Base
{
    public class CE_Estudiante
    {
        // Se añade IdEstudiante, que es IDENTITY y Primary Key en la base de datos
        public int IdEstudiante { get; set; } = 0; // 0 indica un nuevo registro

        // Campos de la tabla ESTUDIANTES
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Carrera { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int SemestreActual { get; set; } // Aunque no esté en el form, es un campo de la tabla

        // Campos adicionales de solo lectura que podríamos necesitar
        public string NombreCompleto => $"{Nombre} {Apellido}";



    }
} 
