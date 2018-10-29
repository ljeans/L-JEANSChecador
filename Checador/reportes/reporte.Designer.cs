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
            this.sucursalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet_Checador = new Checador.DataSet_Checador();
            this.sucursalTableAdapter = new Checador.DataSet_ChecadorTableAdapters.sucursalTableAdapter();
            this.dtp_fecha1 = new System.Windows.Forms.DateTimePicker();
            this.dtp_fecha2 = new System.Windows.Forms.DateTimePicker();
            this.btn_gnerar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_retardo = new System.Windows.Forms.Button();
            this.dtp_fecha2_retardo = new System.Windows.Forms.DateTimePicker();
            this.dtp_fecha1_retardo = new System.Windows.Forms.DateTimePicker();
            this.cbx_sucursal_retardo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.crystalReportViewer2 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tabControlBase.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel_barra_sup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_home)).BeginInit();
            this.panel_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sucursalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_Checador)).BeginInit();
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
            this.tabControlBase.Location = new System.Drawing.Point(309, 68);
            this.tabControlBase.Size = new System.Drawing.Size(1020, 670);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btn_gnerar);
            this.tabPage1.Controls.Add(this.dtp_fecha2);
            this.tabPage1.Controls.Add(this.dtp_fecha1);
            this.tabPage1.Controls.Add(this.cbx_sucursal);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.crystalReportViewer1);
            this.tabPage1.Size = new System.Drawing.Size(1012, 644);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.crystalReportViewer2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.btn_retardo);
            this.tabPage2.Controls.Add(this.dtp_fecha2_retardo);
            this.tabPage2.Controls.Add(this.dtp_fecha1_retardo);
            this.tabPage2.Controls.Add(this.cbx_sucursal_retardo);
            this.tabPage2.Controls.Add(this.label4);
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
            this.rb_modificar.Text = "Reporte Retardos";
            this.rb_modificar.CheckedChanged += new System.EventHandler(this.rb_modificar_CheckedChanged);
            // 
            // rb_registrar
            // 
            this.rb_registrar.FlatAppearance.BorderSize = 0;
            this.rb_registrar.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.rb_registrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.rb_registrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.rb_registrar.Text = "Reporte por sucursal";
            this.rb_registrar.CheckedChanged += new System.EventHandler(this.rb_registrar_CheckedChanged);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 51);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowCloseButton = false;
            this.crystalReportViewer1.ShowParameterPanelButton = false;
            this.crystalReportViewer1.ShowRefreshButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(1006, 590);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sucursal:";
            // 
            // cbx_sucursal
            // 
            this.cbx_sucursal.DataSource = this.sucursalBindingSource;
            this.cbx_sucursal.DisplayMember = "nombre";
            this.cbx_sucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_sucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx_sucursal.FormattingEnabled = true;
            this.cbx_sucursal.Location = new System.Drawing.Point(112, 9);
            this.cbx_sucursal.Name = "cbx_sucursal";
            this.cbx_sucursal.Size = new System.Drawing.Size(200, 32);
            this.cbx_sucursal.TabIndex = 2;
            this.cbx_sucursal.ValueMember = "id_sucursal";
            // 
            // sucursalBindingSource
            // 
            this.sucursalBindingSource.DataMember = "sucursal";
            this.sucursalBindingSource.DataSource = this.dataSet_Checador;
            // 
            // dataSet_Checador
            // 
            this.dataSet_Checador.DataSetName = "DataSet_Checador";
            this.dataSet_Checador.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sucursalTableAdapter
            // 
            this.sucursalTableAdapter.ClearBeforeFill = true;
            // 
            // dtp_fecha1
            // 
            this.dtp_fecha1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_fecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_fecha1.Location = new System.Drawing.Point(435, 12);
            this.dtp_fecha1.Name = "dtp_fecha1";
            this.dtp_fecha1.Size = new System.Drawing.Size(200, 29);
            this.dtp_fecha1.TabIndex = 4;
            // 
            // dtp_fecha2
            // 
            this.dtp_fecha2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_fecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_fecha2.Location = new System.Drawing.Point(641, 12);
            this.dtp_fecha2.Name = "dtp_fecha2";
            this.dtp_fecha2.Size = new System.Drawing.Size(200, 29);
            this.dtp_fecha2.TabIndex = 5;
            // 
            // btn_gnerar
            // 
            this.btn_gnerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.btn_gnerar.FlatAppearance.BorderSize = 0;
            this.btn_gnerar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_gnerar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_gnerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gnerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_gnerar.ForeColor = System.Drawing.Color.White;
            this.btn_gnerar.Location = new System.Drawing.Point(856, 9);
            this.btn_gnerar.Name = "btn_gnerar";
            this.btn_gnerar.Size = new System.Drawing.Size(150, 32);
            this.btn_gnerar.TabIndex = 19;
            this.btn_gnerar.Text = "Generar";
            this.btn_gnerar.UseVisualStyleBackColor = false;
            this.btn_gnerar.Click += new System.EventHandler(this.btn_gnerar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(351, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 24);
            this.label2.TabIndex = 20;
            this.label2.Text = "Fechas:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(347, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 24);
            this.label3.TabIndex = 26;
            this.label3.Text = "Fechas:";
            // 
            // btn_retardo
            // 
            this.btn_retardo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.btn_retardo.FlatAppearance.BorderSize = 0;
            this.btn_retardo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_retardo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_retardo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_retardo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_retardo.ForeColor = System.Drawing.Color.White;
            this.btn_retardo.Location = new System.Drawing.Point(852, 15);
            this.btn_retardo.Name = "btn_retardo";
            this.btn_retardo.Size = new System.Drawing.Size(150, 32);
            this.btn_retardo.TabIndex = 25;
            this.btn_retardo.Text = "Generar";
            this.btn_retardo.UseVisualStyleBackColor = false;
            this.btn_retardo.Click += new System.EventHandler(this.btn_retardo_Click);
            // 
            // dtp_fecha2_retardo
            // 
            this.dtp_fecha2_retardo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_fecha2_retardo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_fecha2_retardo.Location = new System.Drawing.Point(637, 18);
            this.dtp_fecha2_retardo.Name = "dtp_fecha2_retardo";
            this.dtp_fecha2_retardo.Size = new System.Drawing.Size(200, 29);
            this.dtp_fecha2_retardo.TabIndex = 24;
            // 
            // dtp_fecha1_retardo
            // 
            this.dtp_fecha1_retardo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_fecha1_retardo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_fecha1_retardo.Location = new System.Drawing.Point(431, 18);
            this.dtp_fecha1_retardo.Name = "dtp_fecha1_retardo";
            this.dtp_fecha1_retardo.Size = new System.Drawing.Size(200, 29);
            this.dtp_fecha1_retardo.TabIndex = 23;
            // 
            // cbx_sucursal_retardo
            // 
            this.cbx_sucursal_retardo.DataSource = this.sucursalBindingSource;
            this.cbx_sucursal_retardo.DisplayMember = "nombre";
            this.cbx_sucursal_retardo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_sucursal_retardo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx_sucursal_retardo.FormattingEnabled = true;
            this.cbx_sucursal_retardo.Location = new System.Drawing.Point(108, 15);
            this.cbx_sucursal_retardo.Name = "cbx_sucursal_retardo";
            this.cbx_sucursal_retardo.Size = new System.Drawing.Size(200, 32);
            this.cbx_sucursal_retardo.TabIndex = 22;
            this.cbx_sucursal_retardo.ValueMember = "id_sucursal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 24);
            this.label4.TabIndex = 21;
            this.label4.Text = "Sucursal:";
            // 
            // crystalReportViewer2
            // 
            this.crystalReportViewer2.ActiveViewIndex = -1;
            this.crystalReportViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.crystalReportViewer2.Location = new System.Drawing.Point(3, 51);
            this.crystalReportViewer2.Name = "crystalReportViewer2";
            this.crystalReportViewer2.ShowCloseButton = false;
            this.crystalReportViewer2.ShowParameterPanelButton = false;
            this.crystalReportViewer2.ShowRefreshButton = false;
            this.crystalReportViewer2.Size = new System.Drawing.Size(1006, 590);
            this.crystalReportViewer2.TabIndex = 27;
            this.crystalReportViewer2.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
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
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel_barra_sup.ResumeLayout(false);
            this.panel_barra_sup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_home)).EndInit();
            this.panel_menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sucursalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_Checador)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.ComboBox cbx_sucursal;
        private System.Windows.Forms.Label label1;
        private DataSet_Checador dataSet_Checador;
        private System.Windows.Forms.BindingSource sucursalBindingSource;
        private DataSet_ChecadorTableAdapters.sucursalTableAdapter sucursalTableAdapter;
        private System.Windows.Forms.DateTimePicker dtp_fecha2;
        private System.Windows.Forms.DateTimePicker dtp_fecha1;
        private System.Windows.Forms.Button btn_gnerar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_retardo;
        private System.Windows.Forms.DateTimePicker dtp_fecha2_retardo;
        private System.Windows.Forms.DateTimePicker dtp_fecha1_retardo;
        private System.Windows.Forms.ComboBox cbx_sucursal_retardo;
        private System.Windows.Forms.Label label4;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer2;
    }
}
