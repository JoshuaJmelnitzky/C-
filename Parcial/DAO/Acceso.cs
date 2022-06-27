using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial.DAO
{
    public class Acceso
    {
        public static DataTable ObtenerCategorias()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = "SELECT * FROM Categorias";

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


        public static DataTable ObtenerPosiciones()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = "SELECT * FROM Posiciones";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText= consulta;

                cn.Open();
                cmd.Connection= cn;

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

        public static int ObtenerUltimoIdEquipo()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = "SELECT (MAX) Id FROM Equipos";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
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


        public static DataTable BuscarJugadorPorId(int IDjugador)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);   

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM Jugadores WHERE Id = @IDjugador";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IDjugador", IDjugador);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText= consulta;

                cn.Open();
                cmd.Connection= cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
       
                da.Fill(tabla);
                return tabla;   
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }


        public static bool AltaJugadoresXEquipo(int nroEquipo, string nombreEquipo, List<int> listajugadores)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlTransaction objTransaction = null;
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = "INSERT INTO Equipos VALUES(@nombreEquipo, @fechaCreacion)";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombreEquipo", nombreEquipo);
                cmd.Parameters.AddWithValue("@fechaCreacion", DateTime.Now);

                cmd.CommandType= CommandType.Text;  
                cmd.CommandText = consulta;

                cn.Open();
                objTransaction = cn.BeginTransaction("AltaEquipo");
                cmd.Transaction = objTransaction;
                cmd.Connection = cn;

                cmd.ExecuteNonQuery();

                foreach (var jugador in listajugadores)
                {
                    string consultaEquipoJugador = "INSERT INTO JugadoresPorEquipos VALUES(@IdJugador, @IdEquipo, @FechaAsignacion)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdJugador", jugador);
                    cmd.Parameters.AddWithValue("IdEquipo", nroEquipo);
                    cmd.Parameters.AddWithValue("@FechaAsignacion", DateTime.Now);

                    cmd.CommandText = consultaEquipoJugador;
                    cmd.ExecuteNonQuery();
                }

                objTransaction.Commit();
                return true;

            }
            catch (Exception ex)
            {
                objTransaction.Rollback();
                return false;
            }
            finally
            {
                cn.Close();
            }

            return false;
        }

        public static DataTable ObtenerJugadores()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = "SELECT * FROM Jugadores";

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
        }
    }
}
