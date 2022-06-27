using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial
{
    public partial class altaEquipo : Form
    {
        public altaEquipo()
        {
            InitializeComponent();
        }

        private void altaEquipo_Load(object sender, EventArgs e)
        {
            // Carga de combo categorias
            cargarComboCategorias();

            // Carga de combo posiciones
            cargarComboPosiciones();

            // Carga de fecha
            txtFecha.Text = DateTime.Now.ToShortDateString();

            // Carga de número de equipo
            int id = DAO.Acceso.ObtenerUltimoIdEquipo();
            txtNroNuevoEquipo.Text = (id + 1).ToString();
        }


        private void cargarComboCategorias()
        {
            cmbCategorias.DataSource = DAO.Acceso.ObtenerCategorias();
            cmbCategorias.DisplayMember = "Nombre";
            cmbCategorias.SelectedIndex = -1;
            cmbCategorias.ValueMember = "Id";
        }

        private void cargarComboPosiciones()
        {
            cmbPosicion.DataSource = DAO.Acceso.ObtenerPosiciones();
            cmbPosicion.DisplayMember = "Nombre";
            cmbPosicion.SelectedIndex = -1; 
            cmbPosicion.ValueMember = "Id";
        }

        private void btnBuscarJugador_Click(object sender, EventArgs e)
        {
            DataTable tablaResultado = DAO.Acceso.BuscarJugadorPorId(int.Parse(txtNroJugador.Text.Trim()));

            if (tablaResultado.Rows.Count > 0)
            {
                txtNombreJugador.Text = tablaResultado.Rows[0][1].ToString();
            }
            else
            {
                MessageBox.Show("Jugador no encontrado");
            }
        }

        private void btnAgregarJugador_Click(object sender, EventArgs e)
        {
            if (VerificarDatosJugadorCompletos())
            {
                if(!verificarJugador())
                {
                    grillaEquipo.Rows.Add(txtNroJugador.Text, txtNombreJugador.Text, (int)cmbPosicion.SelectedValue);
                }
                else
                {
                    MessageBox.Show("Jugador ya agregado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool verificarJugador()
        {
            foreach (DataGridViewRow fila in grillaEquipo.Rows)
            {
                if (fila.Cells["NroJugador"].Value.ToString().Equals(txtNroJugador.Text.ToString()))
                {
                    return true;
                }
            }
            return false;
        }


        private bool VerificarDatosJugadorCompletos()
        {
            if(txtNombreJugador.Text.Equals("") || cmbPosicion.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Complete los datos faltantes", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }


        private bool VerificarDatosEquipoCompletos()
        {
            if (txtNombreDeEquipo.Text.Equals("") || cmbCategorias.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Complete los datos faltantes", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }


        private void btnConfirmarEquipo_Click(object sender, EventArgs e)
        {
            if(grillaEquipo.Rows.Count > 0)
            {

                List<int> listajugadores = new List<int>();

                for (int i = 0; i < grillaEquipo.Rows.Count; i++)
                {
                    listajugadores.Add(int.Parse(grillaEquipo.Rows[i].Cells[0].Value.ToString()));
                }

                bool resultado = DAO.Acceso.AltaJugadoresXEquipo(int.Parse(txtNroNuevoEquipo.Text), txtNombreDeEquipo.Text.Trim(), listajugadores);


                if (VerificarDatosEquipoCompletos())
                {
                    if (resultado)
                    {
                        MessageBox.Show("Equipo dado de alta con éxito");
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Error al dar de alta nuevo equipo");
                    }
                }            
            }

            else
            {
                MessageBox.Show("No hay jugadores cargados en la grilla", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }      

        }


        private void LimpiarCampos()
        {
            cmbCategorias.SelectedIndex = -1;
            cmbPosicion.SelectedIndex = -1;
            txtNombreDeEquipo.Text = "";
            txtNombreDeEquipo.Focus();  
            txtNombreJugador.Text = "";
            txtFecha.Text = DateTime.Now.ToShortDateString();
            int id = DAO.Acceso.ObtenerUltimoIdEquipo();
            txtNroNuevoEquipo.Text = (id + 1).ToString();
            txtNroJugador.Text = "";
            grillaEquipo.Rows.Clear();      
        }
    }
}
