﻿namespace Checador
{
    partial class cheacador
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
            this.gbox_datos_checador = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rb_mod_inactivo = new System.Windows.Forms.RadioButton();
            this.rb_mod_activo = new System.Windows.Forms.RadioButton();
            this.cbx_sucursal = new System.Windows.Forms.ComboBox();
            this.sucursalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet_Checador = new Checador.DataSet_Checador();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_puerto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_registrar = new System.Windows.Forms.Button();
            this.btn_ir_modificar = new System.Windows.Forms.Button();
            this.label84 = new System.Windows.Forms.Label();
            this.txt_id_mod = new System.Windows.Forms.TextBox();
            this.btn_modificar = new System.Windows.Forms.Button();
            this.dgv_checadorbuscar = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vistaChecadorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgv_checador = new System.Windows.Forms.DataGridView();
            this.label42 = new System.Windows.Forms.Label();
            this.txt_buscar = new System.Windows.Forms.TextBox();
            this.gbox_estatus = new System.Windows.Forms.GroupBox();
            this.cb_buscar_inactivo = new System.Windows.Forms.CheckBox();
            this.cb_buscar_activo = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtp_hora = new System.Windows.Forms.DateTimePicker();
            this.btn_scr_eventos = new System.Windows.Forms.Button();
            this.dtp_fecha = new System.Windows.Forms.DateTimePicker();
            this.btn_fecha_manual = new System.Windows.Forms.Button();
            this.btn_borrar_eventos = new System.Windows.Forms.Button();
            this.btn_borrar_usuarios = new System.Windows.Forms.Button();
            this.btn_scr_fecha = new System.Windows.Forms.Button();
            this.sucursalTableAdapter = new Checador.DataSet_ChecadorTableAdapters.sucursalTableAdapter();
            this.vista_ChecadorTableAdapter = new Checador.DataSet_ChecadorTableAdapters.Vista_ChecadorTableAdapter();
            this.tabControlBase.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel_barra_sup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_home)).BeginInit();
            this.panel_menu.SuspendLayout();
            this.gbox_datos_checador.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sucursalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_Checador)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_checadorbuscar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vistaChecadorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_checador)).BeginInit();
            this.gbox_estatus.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rb_4
            // 
            this.rb_4.FlatAppearance.BorderSize = 0;
            this.rb_4.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkBlue;
            this.rb_4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
            this.rb_4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
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
            this.tabControlBase.Location = new System.Drawing.Point(309, 67);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_modificar);
            this.tabPage1.Controls.Add(this.btn_registrar);
            this.tabPage1.Controls.Add(this.gbox_datos_checador);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_ir_modificar);
            this.tabPage2.Controls.Add(this.label84);
            this.tabPage2.Controls.Add(this.txt_id_mod);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.gbox_estatus);
            this.tabPage3.Controls.Add(this.dgv_checadorbuscar);
            this.tabPage3.Controls.Add(this.dgv_checador);
            this.tabPage3.Controls.Add(this.label42);
            this.tabPage3.Controls.Add(this.txt_buscar);
            // 
            // btn_home
            // 
            this.btn_home.Click += new System.EventHandler(this.btn_home_Click);
            // 
            // lbl_titulo
            // 
            this.lbl_titulo.Size = new System.Drawing.Size(156, 37);
            this.lbl_titulo.Text = "Checador";
            // 
            // rb_buscar
            // 
            this.rb_buscar.FlatAppearance.BorderSize = 0;
            this.rb_buscar.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkBlue;
            this.rb_buscar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
            this.rb_buscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.rb_buscar.CheckedChanged += new System.EventHandler(this.rb_buscar_CheckedChanged);
            // 
            // rb_modificar
            // 
            this.rb_modificar.FlatAppearance.BorderSize = 0;
            this.rb_modificar.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkBlue;
            this.rb_modificar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
            this.rb_modificar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.rb_modificar.CheckedChanged += new System.EventHandler(this.rb_modificar_CheckedChanged);
            // 
            // rb_registrar
            // 
            this.rb_registrar.FlatAppearance.BorderSize = 0;
            this.rb_registrar.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.rb_registrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.rb_registrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.rb_registrar.CheckedChanged += new System.EventHandler(this.rb_registrar_CheckedChanged);
            // 
            // gbox_datos_checador
            // 
            this.gbox_datos_checador.Controls.Add(this.groupBox4);
            this.gbox_datos_checador.Controls.Add(this.cbx_sucursal);
            this.gbox_datos_checador.Controls.Add(this.label15);
            this.gbox_datos_checador.Controls.Add(this.txt_ip);
            this.gbox_datos_checador.Controls.Add(this.txt_id);
            this.gbox_datos_checador.Controls.Add(this.label1);
            this.gbox_datos_checador.Controls.Add(this.txt_puerto);
            this.gbox_datos_checador.Controls.Add(this.label2);
            this.gbox_datos_checador.Controls.Add(this.label9);
            this.gbox_datos_checador.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbox_datos_checador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.gbox_datos_checador.Location = new System.Drawing.Point(215, 115);
            this.gbox_datos_checador.Name = "gbox_datos_checador";
            this.gbox_datos_checador.Size = new System.Drawing.Size(597, 340);
            this.gbox_datos_checador.TabIndex = 1075;
            this.gbox_datos_checador.TabStop = false;
            this.gbox_datos_checador.Text = "Datos Checador";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rb_mod_inactivo);
            this.groupBox4.Controls.Add(this.rb_mod_activo);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(174, 246);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(320, 75);
            this.groupBox4.TabIndex = 1078;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Estatus:";
            this.groupBox4.Visible = false;
            // 
            // rb_mod_inactivo
            // 
            this.rb_mod_inactivo.AutoSize = true;
            this.rb_mod_inactivo.Location = new System.Drawing.Point(180, 28);
            this.rb_mod_inactivo.Name = "rb_mod_inactivo";
            this.rb_mod_inactivo.Size = new System.Drawing.Size(91, 28);
            this.rb_mod_inactivo.TabIndex = 6;
            this.rb_mod_inactivo.Text = "Inactivo";
            this.rb_mod_inactivo.UseVisualStyleBackColor = true;
            // 
            // rb_mod_activo
            // 
            this.rb_mod_activo.AutoSize = true;
            this.rb_mod_activo.Checked = true;
            this.rb_mod_activo.Location = new System.Drawing.Point(22, 28);
            this.rb_mod_activo.Name = "rb_mod_activo";
            this.rb_mod_activo.Size = new System.Drawing.Size(79, 28);
            this.rb_mod_activo.TabIndex = 5;
            this.rb_mod_activo.TabStop = true;
            this.rb_mod_activo.Text = "Activo";
            this.rb_mod_activo.UseVisualStyleBackColor = true;
            // 
            // cbx_sucursal
            // 
            this.cbx_sucursal.DataSource = this.sucursalBindingSource;
            this.cbx_sucursal.DisplayMember = "nombre";
            this.cbx_sucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_sucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cbx_sucursal.FormattingEnabled = true;
            this.cbx_sucursal.Location = new System.Drawing.Point(246, 194);
            this.cbx_sucursal.Name = "cbx_sucursal";
            this.cbx_sucursal.Size = new System.Drawing.Size(248, 32);
            this.cbx_sucursal.TabIndex = 4;
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
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label15.Location = new System.Drawing.Point(200, 121);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 24);
            this.label15.TabIndex = 1081;
            this.label15.Text = "IP:";
            // 
            // txt_ip
            // 
            this.txt_ip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ip.Location = new System.Drawing.Point(245, 116);
            this.txt_ip.MaxLength = 32;
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(248, 29);
            this.txt_ip.TabIndex = 2;
            // 
            // txt_id
            // 
            this.txt_id.BackColor = System.Drawing.Color.LightGray;
            this.txt_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_id.Location = new System.Drawing.Point(245, 73);
            this.txt_id.MaxLength = 32;
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(248, 29);
            this.txt_id.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(111, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 24);
            this.label1.TabIndex = 1078;
            this.label1.Text = "ID Checador:";
            // 
            // txt_puerto
            // 
            this.txt_puerto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_puerto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_puerto.Location = new System.Drawing.Point(246, 157);
            this.txt_puerto.MaxLength = 32;
            this.txt_puerto.Name = "txt_puerto";
            this.txt_puerto.Size = new System.Drawing.Size(248, 29);
            this.txt_puerto.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(161, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 24);
            this.label2.TabIndex = 1076;
            this.label2.Text = "Puerto:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(143, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 24);
            this.label9.TabIndex = 1077;
            this.label9.Text = "Sucursal:";
            // 
            // btn_registrar
            // 
            this.btn_registrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.btn_registrar.FlatAppearance.BorderSize = 0;
            this.btn_registrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_registrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_registrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_registrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_registrar.ForeColor = System.Drawing.Color.White;
            this.btn_registrar.Location = new System.Drawing.Point(388, 461);
            this.btn_registrar.Name = "btn_registrar";
            this.btn_registrar.Size = new System.Drawing.Size(250, 70);
            this.btn_registrar.TabIndex = 7;
            this.btn_registrar.Text = "Registrar";
            this.btn_registrar.UseVisualStyleBackColor = false;
            this.btn_registrar.Click += new System.EventHandler(this.btn_registrar_Click);
            // 
            // btn_ir_modificar
            // 
            this.btn_ir_modificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.btn_ir_modificar.FlatAppearance.BorderSize = 0;
            this.btn_ir_modificar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_ir_modificar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_ir_modificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ir_modificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ir_modificar.ForeColor = System.Drawing.Color.White;
            this.btn_ir_modificar.Location = new System.Drawing.Point(385, 313);
            this.btn_ir_modificar.Name = "btn_ir_modificar";
            this.btn_ir_modificar.Size = new System.Drawing.Size(270, 70);
            this.btn_ir_modificar.TabIndex = 102;
            this.btn_ir_modificar.Text = "Modificar";
            this.btn_ir_modificar.UseVisualStyleBackColor = false;
            this.btn_ir_modificar.Click += new System.EventHandler(this.btn_ir_modificar_Click);
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.Location = new System.Drawing.Point(348, 248);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(120, 24);
            this.label84.TabIndex = 100;
            this.label84.Text = "ID Checador:";
            // 
            // txt_id_mod
            // 
            this.txt_id_mod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_id_mod.Location = new System.Drawing.Point(474, 245);
            this.txt_id_mod.Name = "txt_id_mod";
            this.txt_id_mod.Size = new System.Drawing.Size(200, 29);
            this.txt_id_mod.TabIndex = 101;
            // 
            // btn_modificar
            // 
            this.btn_modificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.btn_modificar.Enabled = false;
            this.btn_modificar.FlatAppearance.BorderSize = 0;
            this.btn_modificar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_modificar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_modificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_modificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_modificar.ForeColor = System.Drawing.Color.White;
            this.btn_modificar.Location = new System.Drawing.Point(388, 477);
            this.btn_modificar.Name = "btn_modificar";
            this.btn_modificar.Size = new System.Drawing.Size(250, 70);
            this.btn_modificar.TabIndex = 7;
            this.btn_modificar.Text = "Modificar";
            this.btn_modificar.UseVisualStyleBackColor = false;
            this.btn_modificar.Visible = false;
            this.btn_modificar.Click += new System.EventHandler(this.btn_modificar_Click);
            // 
            // dgv_checadorbuscar
            // 
            this.dgv_checadorbuscar.AllowUserToAddRows = false;
            this.dgv_checadorbuscar.AllowUserToDeleteRows = false;
            this.dgv_checadorbuscar.AllowUserToResizeColumns = false;
            this.dgv_checadorbuscar.AllowUserToResizeRows = false;
            this.dgv_checadorbuscar.AutoGenerateColumns = false;
            this.dgv_checadorbuscar.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_checadorbuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_checadorbuscar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_checadorbuscar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dgv_checadorbuscar.DataSource = this.vistaChecadorBindingSource;
            this.dgv_checadorbuscar.Location = new System.Drawing.Point(23, 124);
            this.dgv_checadorbuscar.Name = "dgv_checadorbuscar";
            this.dgv_checadorbuscar.Size = new System.Drawing.Size(966, 360);
            this.dgv_checadorbuscar.TabIndex = 83;
            // 
            // check
            // 
            this.check.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.check.HeaderText = "Check";
            this.check.Name = "check";
            this.check.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.check.ToolTipText = "Marque los checadores a los que desea  aplicar la función";
            this.check.Width = 44;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id_checador";
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "sucursal";
            this.dataGridViewTextBoxColumn2.HeaderText = "Sucursal";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ip";
            this.dataGridViewTextBoxColumn3.HeaderText = "IP";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "puerto";
            this.dataGridViewTextBoxColumn4.HeaderText = "Puerto";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "estatus";
            this.dataGridViewTextBoxColumn5.HeaderText = "Estatus";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // vistaChecadorBindingSource
            // 
            this.vistaChecadorBindingSource.DataMember = "Vista_Checador";
            this.vistaChecadorBindingSource.DataSource = this.dataSet_Checador;
            // 
            // dgv_checador
            // 
            this.dgv_checador.AllowUserToAddRows = false;
            this.dgv_checador.AllowUserToDeleteRows = false;
            this.dgv_checador.AllowUserToResizeColumns = false;
            this.dgv_checador.AllowUserToResizeRows = false;
            this.dgv_checador.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_checador.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_checador.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_checador.Location = new System.Drawing.Point(23, 104);
            this.dgv_checador.MultiSelect = false;
            this.dgv_checador.Name = "dgv_checador";
            this.dgv_checador.ReadOnly = true;
            this.dgv_checador.Size = new System.Drawing.Size(966, 380);
            this.dgv_checador.TabIndex = 83;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(51, 43);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(32, 24);
            this.label42.TabIndex = 84;
            this.label42.Text = "ID:";
            // 
            // txt_buscar
            // 
            this.txt_buscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_buscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_buscar.Location = new System.Drawing.Point(89, 41);
            this.txt_buscar.Name = "txt_buscar";
            this.txt_buscar.Size = new System.Drawing.Size(248, 29);
            this.txt_buscar.TabIndex = 82;
            this.txt_buscar.TextChanged += new System.EventHandler(this.txt_nombrebuscar_TextChanged);
            // 
            // gbox_estatus
            // 
            this.gbox_estatus.Controls.Add(this.cb_buscar_inactivo);
            this.gbox_estatus.Controls.Add(this.cb_buscar_activo);
            this.gbox_estatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbox_estatus.Location = new System.Drawing.Point(405, 24);
            this.gbox_estatus.Name = "gbox_estatus";
            this.gbox_estatus.Size = new System.Drawing.Size(270, 70);
            this.gbox_estatus.TabIndex = 85;
            this.gbox_estatus.TabStop = false;
            this.gbox_estatus.Text = "Estatus";
            // 
            // cb_buscar_inactivo
            // 
            this.cb_buscar_inactivo.AutoSize = true;
            this.cb_buscar_inactivo.Checked = true;
            this.cb_buscar_inactivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_buscar_inactivo.Location = new System.Drawing.Point(144, 25);
            this.cb_buscar_inactivo.Name = "cb_buscar_inactivo";
            this.cb_buscar_inactivo.Size = new System.Drawing.Size(101, 28);
            this.cb_buscar_inactivo.TabIndex = 1;
            this.cb_buscar_inactivo.Text = "Inactivos";
            this.cb_buscar_inactivo.UseVisualStyleBackColor = true;
            this.cb_buscar_inactivo.CheckedChanged += new System.EventHandler(this.cb_buscar_inactivo_CheckedChanged);
            // 
            // cb_buscar_activo
            // 
            this.cb_buscar_activo.AutoSize = true;
            this.cb_buscar_activo.Checked = true;
            this.cb_buscar_activo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_buscar_activo.Location = new System.Drawing.Point(22, 28);
            this.cb_buscar_activo.Name = "cb_buscar_activo";
            this.cb_buscar_activo.Size = new System.Drawing.Size(89, 28);
            this.cb_buscar_activo.TabIndex = 0;
            this.cb_buscar_activo.Text = "Activos";
            this.cb_buscar_activo.UseVisualStyleBackColor = true;
            this.cb_buscar_activo.CheckedChanged += new System.EventHandler(this.cb_buscar_activo_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtp_hora);
            this.groupBox1.Controls.Add(this.btn_scr_eventos);
            this.groupBox1.Controls.Add(this.dtp_fecha);
            this.groupBox1.Controls.Add(this.btn_fecha_manual);
            this.groupBox1.Controls.Add(this.btn_borrar_eventos);
            this.groupBox1.Controls.Add(this.btn_borrar_usuarios);
            this.groupBox1.Controls.Add(this.btn_scr_fecha);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(23, 493);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(966, 130);
            this.groupBox1.TabIndex = 86;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuraciones";
            // 
            // dtp_hora
            // 
            this.dtp_hora.CustomFormat = "HH:mm";
            this.dtp_hora.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_hora.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_hora.Location = new System.Drawing.Point(528, 34);
            this.dtp_hora.Name = "dtp_hora";
            this.dtp_hora.ShowUpDown = true;
            this.dtp_hora.Size = new System.Drawing.Size(75, 29);
            this.dtp_hora.TabIndex = 97;
            this.dtp_hora.Value = new System.DateTime(2018, 9, 17, 9, 0, 0, 0);
            // 
            // btn_scr_eventos
            // 
            this.btn_scr_eventos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.btn_scr_eventos.FlatAppearance.BorderSize = 0;
            this.btn_scr_eventos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_scr_eventos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_scr_eventos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_scr_eventos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_scr_eventos.ForeColor = System.Drawing.Color.White;
            this.btn_scr_eventos.Location = new System.Drawing.Point(667, 82);
            this.btn_scr_eventos.Name = "btn_scr_eventos";
            this.btn_scr_eventos.Size = new System.Drawing.Size(250, 40);
            this.btn_scr_eventos.TabIndex = 96;
            this.btn_scr_eventos.Text = "Sincronizar eventos";
            this.btn_scr_eventos.UseVisualStyleBackColor = false;
            this.btn_scr_eventos.Click += new System.EventHandler(this.btn_scr_eventos_Click);
            // 
            // dtp_fecha
            // 
            this.dtp_fecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_fecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_fecha.Location = new System.Drawing.Point(382, 34);
            this.dtp_fecha.Name = "dtp_fecha";
            this.dtp_fecha.Size = new System.Drawing.Size(130, 29);
            this.dtp_fecha.TabIndex = 95;
            this.dtp_fecha.Value = new System.DateTime(2018, 9, 28, 0, 0, 0, 0);
            // 
            // btn_fecha_manual
            // 
            this.btn_fecha_manual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.btn_fecha_manual.FlatAppearance.BorderSize = 0;
            this.btn_fecha_manual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_fecha_manual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_fecha_manual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fecha_manual.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_fecha_manual.ForeColor = System.Drawing.Color.White;
            this.btn_fecha_manual.Location = new System.Drawing.Point(382, 69);
            this.btn_fecha_manual.Name = "btn_fecha_manual";
            this.btn_fecha_manual.Size = new System.Drawing.Size(200, 50);
            this.btn_fecha_manual.TabIndex = 92;
            this.btn_fecha_manual.Text = "Aplicar al dispositivo";
            this.btn_fecha_manual.UseVisualStyleBackColor = false;
            this.btn_fecha_manual.Click += new System.EventHandler(this.btn_fecha_manual_Click);
            // 
            // btn_borrar_eventos
            // 
            this.btn_borrar_eventos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.btn_borrar_eventos.FlatAppearance.BorderSize = 0;
            this.btn_borrar_eventos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_borrar_eventos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_borrar_eventos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_borrar_eventos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_borrar_eventos.ForeColor = System.Drawing.Color.White;
            this.btn_borrar_eventos.Location = new System.Drawing.Point(52, 34);
            this.btn_borrar_eventos.Name = "btn_borrar_eventos";
            this.btn_borrar_eventos.Size = new System.Drawing.Size(250, 40);
            this.btn_borrar_eventos.TabIndex = 94;
            this.btn_borrar_eventos.Text = "Borrar todos los eventos";
            this.btn_borrar_eventos.UseVisualStyleBackColor = false;
            this.btn_borrar_eventos.Click += new System.EventHandler(this.btn_borrar_eventos_Click);
            // 
            // btn_borrar_usuarios
            // 
            this.btn_borrar_usuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.btn_borrar_usuarios.FlatAppearance.BorderSize = 0;
            this.btn_borrar_usuarios.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_borrar_usuarios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_borrar_usuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_borrar_usuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_borrar_usuarios.ForeColor = System.Drawing.Color.White;
            this.btn_borrar_usuarios.Location = new System.Drawing.Point(52, 82);
            this.btn_borrar_usuarios.Name = "btn_borrar_usuarios";
            this.btn_borrar_usuarios.Size = new System.Drawing.Size(250, 40);
            this.btn_borrar_usuarios.TabIndex = 93;
            this.btn_borrar_usuarios.Text = "Borrar todos los usuarios";
            this.btn_borrar_usuarios.UseVisualStyleBackColor = false;
            this.btn_borrar_usuarios.Click += new System.EventHandler(this.btn_borrar_usuarios_Click);
            // 
            // btn_scr_fecha
            // 
            this.btn_scr_fecha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.btn_scr_fecha.FlatAppearance.BorderSize = 0;
            this.btn_scr_fecha.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_scr_fecha.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(0)))), ((int)(((byte)(105)))));
            this.btn_scr_fecha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_scr_fecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_scr_fecha.ForeColor = System.Drawing.Color.White;
            this.btn_scr_fecha.Location = new System.Drawing.Point(667, 34);
            this.btn_scr_fecha.Name = "btn_scr_fecha";
            this.btn_scr_fecha.Size = new System.Drawing.Size(250, 40);
            this.btn_scr_fecha.TabIndex = 91;
            this.btn_scr_fecha.Text = "Sincronizar fecha y hora";
            this.btn_scr_fecha.UseVisualStyleBackColor = false;
            this.btn_scr_fecha.Click += new System.EventHandler(this.btn_scr_fecha_Click);
            // 
            // sucursalTableAdapter
            // 
            this.sucursalTableAdapter.ClearBeforeFill = true;
            // 
            // vista_ChecadorTableAdapter
            // 
            this.vista_ChecadorTableAdapter.ClearBeforeFill = true;
            // 
            // cheacador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1362, 741);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "cheacador";
            this.Load += new System.EventHandler(this.cheacador_Load);
            this.tabControlBase.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel_barra_sup.ResumeLayout(false);
            this.panel_barra_sup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_home)).EndInit();
            this.panel_menu.ResumeLayout(false);
            this.gbox_datos_checador.ResumeLayout(false);
            this.gbox_datos_checador.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sucursalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_Checador)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_checadorbuscar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vistaChecadorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_checador)).EndInit();
            this.gbox_estatus.ResumeLayout(false);
            this.gbox_estatus.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbox_datos_checador;
        private System.Windows.Forms.ComboBox cbx_sucursal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_puerto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_registrar;
        private System.Windows.Forms.Button btn_ir_modificar;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.TextBox txt_id_mod;
        private System.Windows.Forms.Button btn_modificar;
        private System.Windows.Forms.GroupBox gbox_estatus;
        private System.Windows.Forms.DataGridView dgv_checadorbuscar;
        private System.Windows.Forms.DataGridView dgv_checador;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txt_buscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_scr_eventos;
        private System.Windows.Forms.DateTimePicker dtp_fecha;
        private System.Windows.Forms.Button btn_fecha_manual;
        private System.Windows.Forms.Button btn_borrar_eventos;
        private System.Windows.Forms.Button btn_borrar_usuarios;
        private System.Windows.Forms.Button btn_scr_fecha;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rb_mod_inactivo;
        private System.Windows.Forms.RadioButton rb_mod_activo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idchecadorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sucursalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ipDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn puertoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn estatusDataGridViewTextBoxColumn;
        private DataSet_Checador dataSet_Checador;
        private System.Windows.Forms.BindingSource sucursalBindingSource;
        private DataSet_ChecadorTableAdapters.sucursalTableAdapter sucursalTableAdapter;
        private System.Windows.Forms.BindingSource vistaChecadorBindingSource;
        private DataSet_ChecadorTableAdapters.Vista_ChecadorTableAdapter vista_ChecadorTableAdapter;
        private System.Windows.Forms.DateTimePicker dtp_hora;
        private System.Windows.Forms.CheckBox cb_buscar_inactivo;
        private System.Windows.Forms.CheckBox cb_buscar_activo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}
