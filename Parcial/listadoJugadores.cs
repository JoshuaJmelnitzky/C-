using Microsoft.Reporting.WinForms;
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
    public partial class listadoJugadores : Form
    {
        public listadoJugadores()
        {
            InitializeComponent();
        }

        private void listadoJugadores_Load(object sender, EventArgs e)
        {

            this.reporteJugadores.RefreshReport();
        }

        private void reporteJugadores_Load(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            tabla = DAO.Acceso.ObtenerJugadores();


            ReportDataSource ds = new ReportDataSource("DataSet", tabla);
            reporteJugadores.LocalReport.DataSources.Clear();
            reporteJugadores.LocalReport.DataSources.Add(ds);
            reporteJugadores.LocalReport.Refresh();
        }
    }
}
