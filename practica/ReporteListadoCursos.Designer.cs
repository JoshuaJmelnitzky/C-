﻿namespace practica
{
    partial class ReporteListadoCursos
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
            this.reportViewerCursos = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerCursos
            // 
            this.reportViewerCursos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerCursos.LocalReport.ReportEmbeddedResource = "practica.ListadoDeCursos.rdlc";
            this.reportViewerCursos.Location = new System.Drawing.Point(0, 0);
            this.reportViewerCursos.Name = "reportViewerCursos";
            this.reportViewerCursos.ServerReport.BearerToken = null;
            this.reportViewerCursos.Size = new System.Drawing.Size(800, 450);
            this.reportViewerCursos.TabIndex = 0;
            this.reportViewerCursos.Load += new System.EventHandler(this.reportViewerCursos_Load);
            // 
            // ReporteListadoCursos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerCursos);
            this.Name = "ReporteListadoCursos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listado de Cursos";
            this.Load += new System.EventHandler(this.ReporteListadoCursos_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerCursos;
    }
}