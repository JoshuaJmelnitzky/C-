namespace Parcial
{
    partial class listadoJugadores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reporteJugadores = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "ReportViewer";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // reporteJugadores
            // 
            this.reporteJugadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reporteJugadores.LocalReport.ReportEmbeddedResource = "Parcial.listadoJugadores.rdlc";
            this.reporteJugadores.Location = new System.Drawing.Point(0, 0);
            this.reporteJugadores.Name = "reporteJugadores";
            this.reporteJugadores.ServerReport.BearerToken = null;
            this.reporteJugadores.Size = new System.Drawing.Size(643, 450);
            this.reporteJugadores.TabIndex = 0;
            this.reporteJugadores.Load += new System.EventHandler(this.reporteJugadores_Load);
            // 
            // listadoJugadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 450);
            this.Controls.Add(this.reporteJugadores);
            this.Name = "listadoJugadores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "listadoJugadores";
            this.Load += new System.EventHandler(this.listadoJugadores_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Microsoft.Reporting.WinForms.ReportViewer reporteJugadores;
    }
}