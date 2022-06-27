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
    public partial class ParcialPAV : Form
    {
        public ParcialPAV()
        {
            InitializeComponent();
        }

        private void altaDeEquipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            altaEquipo ventana = new altaEquipo();
            ventana.ShowDialog();
        }

        private void reporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listadoJugadores vetana = new listadoJugadores();
            vetana.ShowDialog();
        }
    }
}
