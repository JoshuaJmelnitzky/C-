using practica.AccesoADatos;
using practica.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practica
{
    public partial class AltaPersona : Form
    {
        public AltaPersona()
        {
            InitializeComponent();
        }

        private void AltaPersona_Load(object sender, EventArgs e)
        {
            limpiarCampos();

            CargarComboDocumento();
            CargarComboCarreras();

            CargarGrilla();

            txtCantHijos.Visible = false;
            lblCantHijos.Visible = false;
            btnActualizar.Enabled = false;
        }


        private void CargarComboDocumento()
        {
            try
            {     
                cmbTipoDoc.DataSource = AD_Varios.ObtenerTiposDeDocumento();
                cmbTipoDoc.DisplayMember = "nombre";
                cmbTipoDoc.ValueMember = "id";
                cmbTipoDoc.SelectedIndex = -1;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al cargar combo tipo de documentos");
            }
        }

        private void CargarComboCarreras()
        {
            try
            {          
                cmbCarrera.DataSource = AD_Varios.ObtenerCarreras();
                cmbCarrera.DisplayMember = "nombre";
                cmbCarrera.ValueMember = "id";
                cmbCarrera.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar combo carreras");
            }
        }

        private Persona ObtenerDatosPersona()
        {
            Persona p = new Persona();
            p.NombrePersona = txtNombre.Text.Trim();
            p.ApellidoPersona = txtApellido.Text.Trim();
            p.FechaNacimientoPersona = DateTime.Parse(txtFechaNacimiento.Text);

            if (rbMasculino.Checked)
            {
                p.SexoPersona = 1;
            }
            else if (rbFemenino.Checked)
            {
                p.SexoPersona = 2;
            }
            else
            {
                p.SexoPersona = 3;
            }

            p.TipoDocumentoPersona = (int)cmbTipoDoc.SelectedValue;
            p.DocumentoPersona = txtNumDoc.Text.Trim();
            p.CallePersona = txtCalle.Text.Trim();
            p.NroCasaPersona = txtNumCasa.Text.Trim();

            if (chkSoltero.Checked)
            {
                p.SolteroPersona = true;
            }
            else
            {
                p.SolteroPersona = false;
            }

            if (chkCasado.Checked)
            {
                p.CasadoPersona = true;
            }
            else
            {
                p.CasadoPersona = false;
            }

            if (chkHijos.Checked)
            {
                p.HijosPersona = true;
            }
            else
            {
                p.HijosPersona = false;
            }

            if (txtCantHijos.Text.Equals(""))
            {
                p.CantidadHijosPersona = 0;
            }
            else
            {
                p.CantidadHijosPersona = int.Parse(txtCantHijos.Text);
            }

            p.CarreraPersona = (int)cmbCarrera.SelectedValue;

            return p;
        }


        private void btnGuardarPersona_Click(object sender, EventArgs e)
        {
            Persona p = ObtenerDatosPersona();
            bool resultado = AD_Varios.AgregarPersonaABD(p);
            if (resultado)
            {
                MessageBox.Show("Persona agregada con exito");
                limpiarCampos();
                CargarComboCarreras();
                CargarComboDocumento();
                CargarGrilla();
            }
            else
            {
                MessageBox.Show("Error al agregar persona");
            }
        }



        private void chkHijos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHijos.Checked)
            {
                txtCantHijos.Visible = true;
                lblCantHijos.Visible = true;
            }
            else
            {
                txtCantHijos.Visible = false;
                lblCantHijos.Visible = false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void limpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtFechaNacimiento.Text = "";
            txtNumDoc.Text = "";
            chkCasado.Checked = false;
            chkHijos.Checked = false;
            chkSoltero.Checked = false;
            txtCalle.Text = "";
            txtNumCasa.Text = "";
            rbMasculino.Checked = true;
   
        }


        private void CargarGrilla()
        {
            try
            {
                gdrPersonas.DataSource = AD_Personas.ObtenerListadoPersonasReducido();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al obtener personas");
            }
        }

        // LEER PARA VERIFICAR SI HAY PERSONA EXISTENTE
        private bool ExisteGrilla(string criterioABuscar)
        {
            bool resultado = false;

            for (int i = 0; i < gdrPersonas.Rows.Count ; i++)
            {
                if (gdrPersonas.Rows[i].Cells["Documento"].Value.Equals(criterioABuscar))
                {
                    resultado = true;
                    break;
                }
            }

            return resultado;
        }

        private void gdrPersonas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            btnActualizar.Enabled = true;
            DataGridViewRow filaSeleccionada = gdrPersonas.Rows[indice];

            string documento = filaSeleccionada.Cells["Documento"].Value.ToString();

            Persona p = ObtenerPersona(documento);
            limpiarCampos();
            CargarCampos(p);


        }

            
        private void CargarCampos(Persona p)
        {
            txtNombre.Text = p.NombrePersona;
            txtApellido.Text = p.ApellidoPersona;
            txtFechaNacimiento.Text = p.FechaNacimientoPersona.Date.ToShortDateString();

            if (p.SexoPersona == 1)
            {
                rbMasculino.Checked = true;
            }

            else if (p.SexoPersona == 2)
            {
                rbFemenino.Checked = true;
            }

            else
            {
                rbOtros.Checked = true;
            }

            cmbTipoDoc.SelectedValue = p.TipoDocumentoPersona;
            txtNumDoc.Text = p.DocumentoPersona;
            txtCalle.Text = p.NroCasaPersona.ToString();

            if (p.CasadoPersona)
            {
                chkCasado.Checked = true;
            }
            else
            {
                chkCasado.Checked = false;
            }


            if (p.SolteroPersona)
            {
                chkSoltero.Checked = true;
            }
            else
            {
                chkSoltero.Checked = false;
            }

            if (p.HijosPersona)
            {
                chkHijos.Checked = true;
            }
            else
            {
                chkHijos.Checked = false;
            }


            if (p.CantidadHijosPersona > 0)
            {
                txtCantHijos.Text = p.CantidadHijosPersona.ToString();
            }
            else
            {
                txtCantHijos.Text = "";
            }


            cmbCarrera.SelectedValue = p.CarreraPersona;

        }


        private Persona ObtenerPersona(string documento)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);

            Persona p = new Persona();

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM Personas WHERE nroDoc LIKE @documento";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@documento", documento);
         


                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd .ExecuteReader();

                if (dr != null && dr.Read())
                {
                    p.NombrePersona = dr["nombre"].ToString();
                    p.ApellidoPersona = dr["apellido"].ToString();
                    p.FechaNacimientoPersona = DateTime.Parse(dr["fechaNacimiento"].ToString());
                    p.SexoPersona = int.Parse(dr["sexo"].ToString());
                    p.TipoDocumentoPersona = int.Parse(dr["tipo_doc"].ToString());
                    p.DocumentoPersona = dr["nroDoc"].ToString();
                    p.CallePersona = dr["calle"].ToString();
                    p.NroCasaPersona = dr["nroCasa"].ToString();
                    p.SolteroPersona = bool.Parse(dr["soltero"].ToString());
                    p.CasadoPersona = bool.Parse(dr["casado"].ToString());
                    p.HijosPersona = bool.Parse(dr["hijos"].ToString());
                    p.CantidadHijosPersona = int.Parse(dr["cantHijos"].ToString());
                    p.CarreraPersona = int.Parse(dr["carrera"].ToString());

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }


            return p;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Persona p = ObtenerDatosPersona();
            bool resultado = ActualizarPersonaABD(p);

            if (resultado)
            {
                MessageBox.Show("Persona actualizada con exito");
                limpiarCampos();
                CargarGrilla();
                CargarComboCarreras();
                CargarComboDocumento();
            }
            else
            {
                MessageBox.Show("Error al actualizar persona");
            }

        }

        private bool ActualizarPersonaABD(Persona per)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);

            bool resultado = false;

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE Personas SET nombre = @nombre, apellido = @apellido, fechaNacimiento = @fechaNacimiento, sexo = @sexo, " +
                    "tipo_doc = @tipo_doc, nroDoc = @nroDoc, calle = @calle, nroCasa = @nroCasa, soltero = @soltero, " +
                    "casado = @casado, hijos = @hijos, cantHijos = @cantHijos, carrera = @carrera " +
                    "WHERE nroDoc LIKE @nroDoc";

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
    }
}
