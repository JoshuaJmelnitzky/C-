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
            if (!verificarJugador())
            {
                grillaEquipo.Rows.Add(txtNroJugador.Text, txtNombreJugador.Text, (int)cmbPosicion.SelectedValue);
            }
            else
            {
                MessageBox.Show("Jugador ya agregado");
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

        private void btnConfirmarEquipo_Click(object sender, EventArgs e)
        {
            List<int> listajugadores = new List<int>();

            for (int i = 0; i < grillaEquipo.Rows.Count; i++)
            {
                listajugadores.Add(int.Parse(grillaEquipo.Rows[i].Cells[0].Value.ToString()));
            }

            bool resultado = DAO.Acceso.AltaJugadoresXEquipo(int.Parse(txtNroNuevoEquipo.Text) ,txtNombreDeEquipo.Text.Trim(), listajugadores);

            if (resultado)
            {
                MessageBox.Show("Equipo dado de alta con éxito");
            }
            else
            {
                MessageBox.Show("Error al dar de alta nuevo equipo");
            }

        }
    }
}
