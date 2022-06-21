using practica.AccesoADatos;
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
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            try
            {
                grillaUsuarios.DataSource = AD_Usuarios.ObtenerListadoUsuarios();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al obtener listado de usuarios");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
          LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtNombreUsuario.Text = "";
            txtContraseña.Text = "";
            txtRepetirContraseña.Text = "";
        }

        private void btnAltaUsuario_Click(object sender, EventArgs e)
        {
            if (txtNombreUsuario.Text.Equals(""))
            {
                MessageBox.Show("Ingrese nombre de usuario");
            }
            else
            {
                if (txtContraseña.Text.Equals(txtRepetirContraseña.Text))
                {
                    try
                    {
                        bool resultado = AD_Usuarios.InsertarUsuario(txtNombreUsuario.Text, txtContraseña.Text);
                        if (resultado)
                        {
                            MessageBox.Show("Usuario dado de alta con exito");
                            CargarGrilla();
                            LimpiarCampos();
                            txtNombreUsuario.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Error al ingresar nuevo usuario");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al ingresar nuevo usuario");
                        txtNombreUsuario.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseñas no coinciden");
                }
            }
        }

    }
}
