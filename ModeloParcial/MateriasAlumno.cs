using ModeloParcial.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModeloParcial
{
    public partial class formAltaMateriasAlumno : Form
    {
        public formAltaMateriasAlumno()
        {
            InitializeComponent();
        }

        private void formAltaMateriasAlumno_Load(object sender, EventArgs e)
        {
            CargarComboDocumento();
        }

        private void CargarComboDocumento()
        {
            try
            {
                cmbMaterias.DataSource = AD_Varios.ObtenerMaterias();
                cmbMaterias.DisplayMember = "nombre";
                cmbMaterias.ValueMember = "id_materia";
                cmbMaterias.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar combo de materias");
            }
        }

        private void btnLegajo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tablaresultado = AD_Alumno.ObtenerAlumnoPorLegajo(int.Parse(txtLegajo.Text.Trim()));

                if (tablaresultado.Rows.Count > 0)
                {
                    txtIdAlumno.Text = tablaresultado.Rows[0][0].ToString();
                    txtNombres.Text = tablaresultado.Rows[0][2].ToString();
                    txtApellido.Text = tablaresultado.Rows[0][3].ToString();
                    CargarGrilla(int.Parse(txtIdAlumno.Text));

                }
                else
                {
                    MessageBox.Show("Alumno inexistente");
                    txtLegajo.Focus();
                    txtLegajo.Text = "";
                    txtApellido.Text = "";
                    txtNombres.Text = "";
                    txtIdAlumno.Text = "";
                    tablaresultado.Rows.Clear();
                    grillaMaterias.DataSource = tablaresultado;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al obtener el alumno");
            }
        }

        private void CargarGrilla(int idAlumno)
        {
            try
            {
                grillaMaterias.DataSource = AD_Alumno.ObtenerMateriasAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener materias del alumno");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            DataTable tablaresultado = grillaMaterias.DataSource as DataTable;
            if (tablaresultado != null)
            {
                tablaresultado.Rows.Clear();
            }

            txtNombres.Text = "";
            txtApellido.Text = "";
            txtLegajo.Text = "";
            txtIdAlumno.Text = "";
            txtLegajo.Focus();

            cmbMaterias.SelectedIndex = -1;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                bool resultado = AD_Materias_x_alumno.InsertarMateriaAlumno(int.Parse(txtIdAlumno.Text), (int)cmbMaterias.SelectedValue);

                if (!VerificarAlumnoMateria())
                {
                    if (resultado)
                    {
                        MessageBox.Show("Materia guardada con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        CargarGrilla(int.Parse(txtIdAlumno.Text));
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar materia");
                    }
                }
                else
                {
                    MessageBox.Show("Materia sido cargada previamente");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ingresar nueva materia");
            }
        }

        private bool VerificarAlumnoMateria()
        {
            foreach (DataGridViewRow fila in grillaMaterias.Rows)
            {
                if (fila.Cells["id_materia"].Value.ToString().Equals(cmbMaterias.SelectedValue.ToString()))
                {
                    return true;
                }

            }
            return false;
        }

    }
}
