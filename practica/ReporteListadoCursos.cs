using Microsoft.Reporting.WinForms;
using practica.AccesoADatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practica
{
    public partial class ReporteListadoCursos : Form
    {
        public ReporteListadoCursos()
        {
            InitializeComponent();
        }

        private void ReporteListadoCursos_Load(object sender, EventArgs e)
        {

            this.reportViewerCursos.RefreshReport();
        }

        private void reportViewerCursos_Load(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            tabla = AD_Cursos.ObtenerListadoDeCursos();


            ReportDataSource ds = new ReportDataSource("DatosCursos", tabla);
            reportViewerCursos.LocalReport.DataSources.Clear();
            reportViewerCursos.LocalReport.DataSources.Add(ds);
            reportViewerCursos.LocalReport.Refresh();
        }
    }
}
