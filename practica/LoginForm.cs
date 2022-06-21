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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
       
            if (txtUsuario.Text.Equals("") || txtContraseña.Text.Equals(""))
            {
                MessageBox.Show("Ingrese nombre de usuario y contraseña");
            }
            else
            {
                string nombreUsuario = txtUsuario.Text;
                string password = txtContraseña.Text;
                bool resultado = false;

                try
                {
                    resultado = AD_Usuarios.ValidarUsuario(nombreUsuario, password);

                    if (resultado)
                    {
                        Usuario usu = new Usuario(nombreUsuario, password);
                        PrincipalForm ventana = new PrincipalForm(usu);
                        ventana.Show();
                        this.Hide();
                    }

                    else
                    {
                        MessageBox.Show("El usuario ingresado no existe");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al consultar usuario");
                }
                                   
            }
        }


    }
}
