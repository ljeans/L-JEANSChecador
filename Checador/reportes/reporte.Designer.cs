namespace Checador.reportes
{
    partial class reporte
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_sucursal = new System.Windows.Forms.ComboBox();
            this.btn_generar = new System.Windows.Forms.Button();
            this.dataSet_Checador = new Checador.DataSet_Checador();
            this.sucursalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sucursalTableAdapter = new Checador.DataSet_ChecadorTableAdapters.sucursalTableAdapter();
            this.tabControlBase.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel_barra_sup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_home)).BeginInit();
            this.panel_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_Checador)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sucursalBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rb_4
            // 
            this.rb_4.FlatAppearance.BorderSize = 0;
            this.rb_4.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.rb_4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.rb_4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            // 
            // rb_3
            // 
            this.rb_3.FlatAppearance.BorderSize = 0;
            this.rb_3.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkBlue;
            this.rb_3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
            this.rb_3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            // 
            // rb_2
            // 
            this.rb_2.FlatAppearance.BorderSize = 0;
            this.rb_2.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkBlue;
            this.rb_2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
            this.rb_2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            // 
            // rb_1
            // 
            this.rb_1.FlatAppearance.BorderSize = 0;
            this.rb_1.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkBlue;
            this.rb_1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
            this.rb_1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            // 
            // tabControlBase
            // 
            this.tabControlBase.Location = new System.Drawing.Point(309, 57);
            this.tabControlBase.Size = new System.Drawing.Size(1020, 670);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_generar);
            this.tabPage1.Controls.Add(this.cbx_sucursal);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.crystalReportViewer1);
            this.tabPage1.Size = new System.Drawing.Size(1012, 644);
            // 
            // tabPage2
            // 
            this.tabPage2.Size = new System.Drawing.Size(1012, 644);
            // 
            // tabPage3
            // 
            this.tabPage3.Size = new System.Drawing.Size(1012, 644);
            // 
            // tabPage4
            // 
            this.tabPage4.Size = new System.Drawing.Size(1012, 644);
            // 
            // tabPage5
            // 
            this.tabPage5.Size = new System.Drawing.Size(1012, 644);
            // 
            // btn_home
            // 
            this.btn_home.Click += new System.EventHandler(this.btn_home_Click);
            // 
            // rb_buscar
            // 
            this.rb_buscar.FlatAppearance.BorderSize = 0;
            this.rb_buscar.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkBlue;
            this.rb_buscar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
            this.rb_buscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.rb_buscar.Text = "";
            // 
            // rb_modificar
            // 
            this.rb_modificar.FlatAppearance.BorderSize = 0;
            this.rb_modificar.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkBlue;
            this.rb_modificar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
            this.rb_modificar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.rb_modificar.Text = "";
            // 
            // rb_registrar
            // 
            this.rb_registrar.FlatAppearance.BorderSize = 0;
            this.rb_registrar.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.rb_registrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.rb_registrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.rb_registrar.Text = "Reporte por sucursal";
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 51);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1006, 590);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sucursal:";
            // 
            // cbx_sucursal
            // 
            this.cbx_sucursal.DataSource = this.sucursalBindingSource;
            this.cbx_sucursal.DisplayMember = "nombre";
            this.cbx_sucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_sucursal.FormattingEnabled = true;
            this.cbx_sucursal.Location = new System.Drawing.Point(157, 20);
            this.cbx_sucursal.Name = "cbx_sucursal";
            this.cbx_sucursal.Size = new System.Drawing.Size(121, 21);
            this.cbx_sucursal.TabIndex = 2;
            this.cbx_sucursal.ValueMember = "id_sucursal";
            // 
            // btn_generar
            // 
            this.btn_generar.Location = new System.Drawing.Point(308, 18);
            this.btn_generar.Name = "btn_generar";
            this.btn_generar.Size = new System.Drawing.Size(75, 23);
            this.btn_generar.TabIndex = 3;
            this.btn_generar.Text = "Generar";
            this.btn_generar.UseVisualStyleBackColor = true;
            this.btn_generar.Click += new System.EventHandler(this.btn_generar_Click);
            // 
            // dataSet_Checador
            // 
            this.dataSet_Checador.DataSetName = "DataSet_Checador";
            this.dataSet_Checador.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sucursalBindingSource
            // 
            this.sucursalBindingSource.DataMember = "sucursal";
            this.sucursalBindingSource.DataSource = this.dataSet_Checador;
            // 
            // sucursalTableAdapter
            // 
            this.sucursalTableAdapter.ClearBeforeFill = true;
            // 
            // reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1362, 741);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "reporte";
            this.Load += new System.EventHandler(this.reporte_Load);
            this.tabControlBase.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel_barra_sup.ResumeLayout(false);
            this.panel_barra_sup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_home)).EndInit();
            this.panel_menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_Checador)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sucursalBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Button btn_generar;
        private System.Windows.Forms.ComboBox cbx_sucursal;
        private System.Windows.Forms.Label label1;
        private DataSet_Checador dataSet_Checador;
        private System.Windows.Forms.BindingSource sucursalBindingSource;
        private DataSet_ChecadorTableAdapters.sucursalTableAdapter sucursalTableAdapter;
    }
}
