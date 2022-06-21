using practica.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica.AccesoADatos
{
    public class AD_Cursos
    {

        public static int ObtenerUltimoIdCurso()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = "SELECT MAX(id) FROM Cursos";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                int resultado = (int)cmd.ExecuteScalar();
                return resultado;

            }
            catch (Exception ex)
            {

                return 0;
            }
            finally
            {
                cn.Close();
            }
        }

        public static bool AltaNuevoCurso(int idCurso, string nombreCurso, string descripcion, int idCarrera, List<int> listaIdAlumnos)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlTransaction objTransaccion = null;
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = "INSERT INTO Cursos VALUES (@nombre, @descripcion, @idCarrera)";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", nombreCurso);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@idCarrera", idCarrera);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                objTransaccion = cn.BeginTransaction("Alta de Curso");

                cmd.Transaction = objTransaccion;

                cmd.Connection = cn;

                cmd.ExecuteNonQuery();

                foreach (var idAlumno in listaIdAlumnos)
                {
                    string consultaCursoAlumnos = "INSERT INTO Personas_por_cursos VALUES (@idPersona, @idCurso, @fechaAsociacion)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idPersona", idAlumno);
                    cmd.Parameters.AddWithValue("@idCurso", idCurso);
                    cmd.Parameters.AddWithValue("@fechaAsociacion", DateTime.Now);

                    cmd.CommandText = consultaCursoAlumnos;
                    cmd.ExecuteNonQuery();
                }        

                objTransaccion.Commit();
                return true;

            }
            catch (Exception ex)
            {
                objTransaccion.Rollback();
                return false;
            }
            finally
            {
                cn.Close();
            }
        }


        public static DataTable ObtenerCarreras()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = "GetCarreras";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(tabla);

                return tabla;

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public static bool AgregarPersonaABD(Persona per)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);

            bool resultado = false;

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "INSERT INTO Personas (nombre, apellido, fechaNacimiento, sexo, tipo_doc, nroDoc, calle, nroCasa, soltero, casado, hijos, cantHijos, carrera) VALUES(@nombre, @apellido, @fechaNacimiento, @sexo, @tipo_doc, @nroDoc, @calle, @nroCasa, @soltero, @casado, @hijos, @cantHijos, @carrera)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", per.NombrePersona);
                cmd.Parameters.AddWithValue("@apellido", per.ApellidoPersona);
                cmd.Parameters.AddWithValue("@fechaNacimiento", per.FechaNacimientoPersona);
                cmd.Parameters.AddWithValue("@sexo", per.SexoPersona);
                cmd.Parameters.AddWithValue("@tipo_doc", per.TipoDocumentoPersona);
                cmd.Parameters.AddWithValue("@nroDoc", per.DocumentoPersona);
                cmd.Parameters.AddWithValue("@calle", per.CallePersona);
                cmd.Parameters.AddWithValue("@nroCasa", per.NroCasaPersona);
                cmd.Parameters.AddWithValue("@soltero", per.SolteroPersona);
                cmd.Parameters.AddWithValue("@casado", per.CasadoPersona);
                cmd.Parameters.AddWithValue("@hijos", per.HijosPersona);
                cmd.Parameters.AddWithValue("@cantHijos", per.CantidadHijosPersona);
                cmd.Parameters.AddWithValue("@carrera", per.CarreraPersona);


                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }


            return resultado;
        }

        public static DataTable ObtenerListadoDeCursos()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = "SELECT * FROM Cursos";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(tabla);

                return tabla;

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
