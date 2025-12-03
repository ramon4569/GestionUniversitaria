using CapaDatos.Conexion;        // Necesario para usar la clase Conexion
using CapaNegocio.Base;          // Necesario para usar Estudiante, Inscripcion, Materia
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // Necesario para DBNull y Convert

namespace CapaNegocio.ServiciosCD
{
    public class EstudianteDAL
    {
        // 1. MÉTODO COMPLEJO (Para reportes y LINQ)
        public List<Estudiante> ObtenerTodosCompletos()
        {
            var diccionarioEstudiantes = new Dictionary<string, Estudiante>();

            // SOLUCIÓN AMBIGÜEDAD: Instanciar la clase Conexion para obtener la conexión SQL
            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();

                string query = @"
                                SELECT
                                e.IdEstudiante, e.Matricula, e.Nombre, e.Apellido, e.Carrera, e.Email, e.Telefono, 
                                i.IdMatricula, i.CalificacionFinal, 
                                m.IdMateria, m.Codigo AS CodigoMateria, m.Nombre AS NombreMateria, m.Creditos,
                                s.CodigoSeccion 
                            FROM Estudiantes e
                            LEFT JOIN Matriculas i ON e.IdEstudiante = i.IdEstudiante
                            LEFT JOIN Secciones s ON i.IdSeccion = s.IdSeccion
                            LEFT JOIN Materias m ON s.IdMateria = m.IdMateria
                         ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string matricula = reader["Matricula"].ToString();

                            if (!diccionarioEstudiantes.ContainsKey(matricula))
                            {
                                // Asumimos que Estudiante tiene un constructor vacío para la inicialización
                                var nuevoEstudiante = new Estudiante(matricula, reader["Nombre"].ToString(), reader["Apellido"].ToString(), reader["Carrera"].ToString())
                                {
                                    IdEstudiante = Convert.ToInt32(reader["IdEstudiante"]),
                                    // *** CORRECCIÓN CRÍTICA: Manejo de DBNull para Email y Telefono ***
                                    Email = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString(),
                                    Telefono = reader["Telefono"] == DBNull.Value ? string.Empty : reader["Telefono"].ToString(),
                                    // ***************************************************************
                                    MateriasCursadas = new List<Inscripcion>()
                                };
                                // En este punto, necesitas calcular el Índice Académico si la entidad Estudiante lo requiere:
                                // nuevoEstudiante.IndiceAcademico = cnCalificaciones.CalcularIndiceAcademico(nuevoEstudiante.MateriasCursadas);

                                diccionarioEstudiantes.Add(matricula, nuevoEstudiante);
                            }

                            Estudiante estudianteActual = diccionarioEstudiantes[matricula];

                            if (reader["IdMatricula"] != DBNull.Value) // Solo crear inscripción si hay datos de matrícula
                            {
                                var inscripcion = new Inscripcion
                                {
                                    IdMatricula = Convert.ToInt32(reader["IdMatricula"]),
                                    CodigoSeccion = reader["CodigoSeccion"].ToString(),

                                    // CalificacionFinal puede ser NULL en la BD, se lee como double? (nullable double)
                                    CalificacionFinal = reader["CalificacionFinal"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["CalificacionFinal"]),

                                    Materia = new Materia
                                    {
                                        // Asumimos que IdMateria está disponible en la consulta
                                        IdMateria = Convert.ToInt32(reader["IdMateria"]),
                                        Codigo = reader["CodigoMateria"].ToString(),
                                        Nombre = reader["NombreMateria"].ToString(),
                                        Creditos = Convert.ToInt32(reader["Creditos"])
                                    }
                                };
                                estudianteActual.MateriasCursadas.Add(inscripcion);
                            }
                        }
                    }
                }
            }

            // Una vez cargados los datos, calculamos el índice para cada estudiante
            // Necesitas una instancia de CN_Calificaciones (o mover el método de cálculo aquí)
            CN_Calificaciones cnCalificaciones = new CN_Calificaciones();
            foreach (var est in diccionarioEstudiantes.Values)
            {
                // La propiedad IndiceAcademico del Estudiante debe ser actualizada
                if (est.MateriasCursadas != null)
                {
                    // Convertir el string del índice a decimal para el objeto Estudiante
                    if (decimal.TryParse(cnCalificaciones.CalcularIndiceAcademico(est.MateriasCursadas), out decimal indice))
                    {
                        est.IndiceAcademico = indice;
                    }
                }
            }

            return new List<Estudiante>(diccionarioEstudiantes.Values);
        }

        // --------------------------------------------------------------------------------------
        // 2. EXISTE MATRÍCULA (Validación) - Se corrige la forma de obtener la conexión
        // --------------------------------------------------------------------------------------
        public bool ExisteMatricula(string matricula)
        {
            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Estudiantes WHERE Matricula = @Matricula";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Matricula", matricula);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // --------------------------------------------------------------------------------------
        // 3. INSERTAR (Guardar) - Se corrige la forma de obtener la conexión
        // --------------------------------------------------------------------------------------
        public void Insertar(Estudiante est)
        {
            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();
                // Nota: Se agrega SemestreActual a la query, asumiendo que Estudiante lo tiene y que es 1 para el nuevo.
                string query = @"INSERT INTO Estudiantes (Matricula, Nombre, Apellido, Carrera, Email, Telefono, SemestreActual) 
                                 VALUES (@Matricula, @Nombre, @Apellido, @Carrera, @Email, @Telefono, @SemestreActual)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Matricula", est.Matricula);
                    cmd.Parameters.AddWithValue("@Nombre", est.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", est.Apellido);
                    cmd.Parameters.AddWithValue("@Carrera", est.Carrera);

                    // Manejo de NULL para Email y Telefono
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(est.Email) ? (object)DBNull.Value : est.Email);
                    cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(est.Telefono) ? (object)DBNull.Value : est.Telefono);
                    cmd.Parameters.AddWithValue("@SemestreActual", 1); // Asignar 1 por defecto al registrar

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --------------------------------------------------------------------------------------
        // 4. OBTENER TODOS (Simple) - Se corrige la forma de obtener la conexión y lectura de campos
        // --------------------------------------------------------------------------------------
        public List<Estudiante> ObtenerTodos()
        {
            List<Estudiante> lista = new List<Estudiante>();

            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT * FROM Estudiantes";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Estudiante est = new Estudiante(
                                reader["Matricula"].ToString(),
                                reader["Nombre"].ToString(),
                                reader["Apellido"].ToString(),
                                reader["Carrera"].ToString()
                            )
                            {
                                // Asignar campos opcionales después de la inicialización
                                Email = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString(),
                                Telefono = reader["Telefono"] == DBNull.Value ? string.Empty : reader["Telefono"].ToString(),
                                MateriasCursadas = new List<Inscripcion>()
                            };
                            lista.Add(est);
                        }
                    }
                }
            }
            return lista;
        }

        // --------------------------------------------------------------------------------------
        // 5. BUSCAR POR MATRICULA (Simple) - Se corrige la forma de obtener la conexión y lectura de campos
        // --------------------------------------------------------------------------------------
        public Estudiante BuscarPorMatricula(string matricula)
        {
            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT * FROM Estudiantes WHERE Matricula = @Matricula";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Matricula", matricula);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Estudiante(
                                reader["Matricula"].ToString(),
                                reader["Nombre"].ToString(),
                                reader["Apellido"].ToString(),
                                reader["Carrera"].ToString()
                            )
                            {
                                // Asignar campos opcionales
                                Email = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString(),
                                Telefono = reader["Telefono"] == DBNull.Value ? string.Empty : reader["Telefono"].ToString(),
                                MateriasCursadas = new List<Inscripcion>()
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}