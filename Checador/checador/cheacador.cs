﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.IO;

namespace Checador
{
    public partial class cheacador : Checador.formularios_padres.formpadre
    {
        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE CHECADOR
        ClaseChecador Clase_Checador = new ClaseChecador();
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();
        formularios_padres.mensaje_error frm_error = new formularios_padres.mensaje_error();
        validacion validar = new validacion();
        public bool respuesta = false;
        public bool sincronizar = false;
        int contador;

        //VARIABLE GLOBAL DE NOMBRE DE ARCHIVO PARA NOMINA
        string FileName;

        //OBJETO DE LA CLASSE CKEM (SDK) PARA PODER ACCEDER A METODOS Y ATRIBUTOS
        public zkemkeeper.CZKEM Checador = new zkemkeeper.CZKEM();

        //VARIABLE DE CONEXION DEL CHECADOR
        bool bConn;

        //CHECKBOX DE MARCAR TODOS EN EL DATAGRID DEL CHECADOR
        CheckBox HeaderCheckBox = new CheckBox();

        public cheacador()
        {
            InitializeComponent();
            //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
            //CheckForIllegalCrossThreadCalls = false;
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
            rb_mod_activo.Checked = true;
            groupBox4.Visible = true;
            btn_modificar.Enabled = true;
            btn_modificar.Visible = true;
            btn_registrar.Visible = false;
            tabControlBase.SelectedTab = tabPage2;
            txt_id_mod.Text = "";
        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {

            txt_id.Enabled = true;
            rb_mod_activo.Checked = true;
            groupBox4.Visible = false;
            btn_modificar.Enabled = false;
            btn_modificar.Visible = false;
            btn_registrar.Visible = true;
            tabControlBase.SelectedTab = tabPage1;
            Limpiar();

        }

        private void responder(object sender, EventArgs e)
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                Enabled = true;
                respuesta = confirmacion.respuesta;

                if (respuesta == true)
                {
                    Clase_Checador.Modificar_Checador();
                    //FUNCION PAR RECARGAR EL DATAGRID
                    this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                    Limpiar();
                    tabControlBase.SelectedTab = tabPage2;
                    txt_id_mod.Focus();
                }
                confirmacion = null;
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;

        }

        //MODIFICAR///////////////////////////////////////////////////////////////////////
        //FUNCION PARA ACTUALIZAR LOS DATOS EN LA BD DEL CHECADOR
        private void btn_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                Clase_Checador.ip = txt_ip.Text;
                Clase_Checador.puerto = txt_puerto.Text;
                Clase_Checador.id_sucursal = Convert.ToInt32(cbx_sucursal.SelectedValue.ToString());
                if (rb_mod_activo.Checked == true)
                {
                    Clase_Checador.estatus = "A";
                }
                else
                {
                    Clase_Checador.estatus = "I";
                }
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                confirmacion = new formularios_padres.Mensajes();
                confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea modificar el checador?";
                confirmacion.FormClosed += new FormClosedEventHandler(responder);
                confirmacion.ShowDialog();
                //Enabled = false;

            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS DATOS DEL CHECADOR EN EL FORMULARIO
        private void btn_ir_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                txt_id.Enabled = false;
                Clase_Checador.id = Convert.ToInt32(txt_id_mod.Text);
                if (Clase_Checador.verificar_existencia(Clase_Checador.id))
                {
                    tabControlBase.SelectedTab = tabPage1;
                    txt_id.Text = Clase_Checador.id.ToString();
                    txt_ip.Text = Clase_Checador.ip;
                    txt_puerto.Text = Clase_Checador.puerto;
                    cbx_sucursal.SelectedValue = Clase_Checador.id_sucursal;

                    if (Clase_Checador.estatus.ToString() == "A")
                    {
                        rb_mod_activo.Checked = true;
                    }
                    else if (Clase_Checador.estatus.ToString() == "I")
                    {
                        rb_mod_inactivo.Checked = true;
                    }

                    btn_modificar.Enabled = true;
                    btn_modificar.Visible = true;
                    btn_registrar.Visible = false;
                }
                else
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Checador no registrado. Por favor intente de nuevo.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.Show();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rb_buscar.Checked == true)
                {
                    this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                    tabControlBase.SelectedTab = tabPage3;
                    HeaderCheckBox.Checked = false;
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }

        }

        //REGISTRAR//////////////////////////////////////////////////////////////////////////
        //CLICK AL BOTON REGISTRAR
        //FUNCION PARA REGITAR CHECADOR EN LA BASE DE DATOS
        private void btn_registrar_Click(object sender, EventArgs e)
        {
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.Registrar));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
        }

        public void Registrar()
        {
            try
            {
                Clase_Checador.estatus = "A";
                Clase_Checador.id = Convert.ToInt32(txt_id.Text);
                Clase_Checador.id_sucursal = Convert.ToInt32(cbx_sucursal.SelectedValue.ToString());
                Clase_Checador.ip = txt_ip.Text;
                Clase_Checador.puerto = txt_puerto.Text;
                if (Clase_Checador.id.ToString() == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el ID del checador";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_id.Focus();
                }
                else if (Clase_Checador.ip == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado la IP del checador";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_ip.Focus();
                }
                else if (Clase_Checador.puerto == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el puerto del checador";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_puerto.Focus();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = true;
                    if (Clase_Checador.guardarChecador())
                    {
                        //FUNCION PAR RECARGAR EL DATAGRID
                        this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                    }

                    Limpiar();
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                }
               
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString()); 
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR LOS COMPONENTES DEL FORMULARIO DESPUES DE HACER UN REGISTRO
        public void Limpiar()
        {
            txt_id.Text = "";
            txt_ip.Text = "";
            txt_puerto.Text = "4370";
            txt_id.Focus();
            //Deshabilitar_Componentes();
        }

        //FUNCION PARA DESHABILITAR LOS COMPONENTES DEL FORMULARIO
        public void Deshabilitar_Componentes()
        {
            txt_puerto.Enabled = false;
            txt_ip.Enabled = false;
            cbx_sucursal.Enabled = false;
            btn_registrar.Enabled = false;
        }

        //FUNCION PARA HABILITAR LOS COMPONENTES DEL FORMULARIO
        public void Habilitar_Componentes()
        {
            txt_puerto.Enabled = true;
            txt_ip.Enabled = true;
            cbx_sucursal.Enabled = true;
            btn_registrar.Enabled = true;
        }

        private void cheacador_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Checador' table. You can move, or remove it, as needed.
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                // TODO: This line of code loads data into the 'dataSet_Checador.sucursal' table. You can move, or remove it, as needed.
                this.sucursalTableAdapter.Fill(this.dataSet_Checador.sucursal);
                //FILTRAR POR SUCURSALES ACTIVAS EL COMBOBOX
                sucursalBindingSource.Filter = "estatus='A'";

                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_HuellaLog' Puede moverla o quitarla según sea necesario.
                this.vista_HuellaLogTableAdapter.Fill(this.dataSet_Checador.Vista_HuellaLog);

                //FILTRAR EL HISTORIAL DE HUELLAS EN LOS ULTIMOS 30 DIAS
                DateTime fechaActual = DateTime.Now;
                fechaActual = fechaActual.AddDays(-30);
                this.vistaHuellaLogBindingSource.Filter = "(CONVERT([fecha], 'System.DateTime') >= CONVERT('" + fechaActual.Year + "-" + fechaActual.Month + "-" + fechaActual.Day + "', 'System.DateTime'))"; 

                //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
                CheckForIllegalCrossThreadCalls = false;

                //CAMBIAR LA LETRA AL DATAGRIDVIEW
                dgv_checador.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
                dgv_checador.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

                //AGREGAR EL CHECKBOX MARCAR TODOS DEL DATAGRID
                AgregarCheckBox();
                HeaderCheckBox.MouseClick += new MouseEventHandler(CheckBox_MarcarTodos_Click);

                DateTime dateValue = new DateTime(2018, 10, 22);
                txt_id.Focus();
                //MessageBox.Show(dateValue.DayOfWeek.ToString());

                //*************   HACER FORMULARIO RESPONSIVO   *****************
                double porcentaje_ancho = (Convert.ToDouble(Width) / 1362);
                double porcentaje_alto = (Convert.ToDouble(Height) / 741);

                foreach (Control x in this.Controls)
                {
                    if (x.HasChildren)
                    {
                        foreach (Control y in x.Controls)
                        {
                            if (y.HasChildren)
                            {
                                foreach (Control z in y.Controls)
                                {
                                    if (z.HasChildren)
                                    {
                                        foreach (Control w in z.Controls)
                                        {
                                            if (w is TextBox | w is Label | w is Button | w is CheckBox | w is MaskedTextBox | w is DateTimePicker | w is ComboBox | w is RadioButton | w is PictureBox | w is GroupBox | w is DataGridView | w is NumericUpDown)
                                            {
                                                if ((w is Label & porcentaje_ancho <= 0.8) | (w is DateTimePicker & porcentaje_ancho <= 0.8) | (w is NumericUpDown & porcentaje_ancho <= 0.8) | (w is Button & porcentaje_ancho <= 0.8) | (w is ComboBox & porcentaje_ancho <= 0.8) | (w is RadioButton & porcentaje_ancho <= 0.8))
                                                {
                                                    if (w is Label)
                                                    {
                                                        w.Font = new Font("Microsoft Sans Serif", 10);
                                                    }
                                                    w.Font = new Font("Microsoft Sans Serif", 11);
                                                }
                                                double posicionx = Convert.ToDouble(w.Location.X) * porcentaje_ancho;
                                                double posiciony = Convert.ToDouble(w.Location.Y) * porcentaje_alto;
                                                double ancho = w.Width * porcentaje_ancho;
                                                double alto = w.Height * porcentaje_alto;

                                                w.Left = Convert.ToInt32(posicionx);
                                                w.Top = Convert.ToInt32(posiciony);
                                                w.Width = Convert.ToInt32(ancho);
                                                w.Height = Convert.ToInt32(alto);
                                            }
                                        }
                                    }
                                    if (z is TextBox | z is Label | z is Button | z is MaskedTextBox | z is DateTimePicker | z is ComboBox | z is RadioButton | z is PictureBox | z is GroupBox | z is DataGridView)
                                    {

                                        if ((z is Label & porcentaje_ancho <= 0.8) | (z is Button & porcentaje_ancho <= 0.8) | (z is ComboBox & porcentaje_ancho <= 0.8) | (z is RadioButton & porcentaje_ancho <= 0.8))
                                        {
                                            z.Font = new Font("Microsoft Sans Serif", 11);
                                        }

                                        double posicionx = Convert.ToDouble(z.Location.X) * porcentaje_ancho;
                                        double posiciony = Convert.ToDouble(z.Location.Y) * porcentaje_alto;
                                        double ancho = z.Width * porcentaje_ancho;
                                        double alto = z.Height * porcentaje_alto;

                                        z.Left = Convert.ToInt32(posicionx);
                                        z.Top = Convert.ToInt32(posiciony);
                                        z.Width = Convert.ToInt32(ancho);
                                        z.Height = Convert.ToInt32(alto);
                                    }
                                }
                            }
                            if (y is TextBox | y is Label | y is Button | y is MaskedTextBox | y is DateTimePicker | y is ComboBox | y is RadioButton | y is Panel | y is TabControl)
                            {
                                if ((y is Button & porcentaje_ancho <= 0.8) | (y is RadioButton & porcentaje_ancho <= 0.8))
                                {
                                    y.Font = new Font("Microsoft Sans Serif", 12);
                                }

                                double posicionx = Convert.ToDouble(y.Location.X) * porcentaje_ancho;
                                double posiciony = Convert.ToDouble(y.Location.Y) * porcentaje_alto;
                                double ancho = y.Width * porcentaje_ancho;
                                double alto = y.Height * porcentaje_alto;

                                y.Left = Convert.ToInt32(posicionx);
                                y.Top = Convert.ToInt32(posiciony);
                                y.Width = Convert.ToInt32(ancho);
                                y.Height = Convert.ToInt32(alto);
                            }
                        }
                    }
                    if (x is TextBox | x is Label | x is Button | x is MaskedTextBox | x is DateTimePicker | x is ComboBox | x is RadioButton | x is Panel | x is TabControl)
                    {
                        double posicionx = Convert.ToDouble(x.Location.X) * porcentaje_ancho;
                        double posiciony = Convert.ToDouble(x.Location.Y) * porcentaje_alto;
                        double ancho = x.Width * porcentaje_ancho;
                        double alto = x.Height * porcentaje_alto;

                        x.Left = Convert.ToInt32(posicionx);
                        x.Top = Convert.ToInt32(posiciony);
                        x.Width = Convert.ToInt32(ancho);
                        x.Height = Convert.ToInt32(alto);
                    }
                }
                //********************************************************************************************

                //***********   PERMISOS PARA SUPERVISOR   **************
                if (Program.rol == "SUPERVISOR DE PERSONAL")
                {
                    rb_registrar.Enabled = false;
                    rb_modificar.Enabled = false;
                    btn_borrar_usuarios.Enabled = false;
                    btn_borrar_eventos.Enabled = false;
                    btn_fecha_manual.Enabled = false;
                    dtp_fecha.Enabled = false;
                    dtp_hora.Enabled = false;
                    btn_b_modificar.Enabled = false;
                    btn_dar_baja.Enabled = false;
                    rb_buscar.Checked = true;
                    rb_historial.Enabled = true;
                    radioButton1.Enabled = false;
                }
                else if(Program.rol == "ENCARGADA DE TIENDA")
                {
                    vistaChecadorBindingSource.Filter = "sucursal ='" + Program.sucursal + "'";
                    rb_registrar.Enabled = false;
                    rb_modificar.Enabled = false;
                    btn_borrar_usuarios.Enabled = false;
                    btn_borrar_eventos.Enabled = false;
                    btn_fecha_manual.Enabled = false;
                    dtp_fecha.Enabled = false;
                    dtp_hora.Enabled = false;
                    btn_b_modificar.Enabled = false;
                    btn_dar_baja.Enabled = false;
                    rb_buscar.Checked = true;
                    btn_scr_fecha.Enabled = false;
                    radioButton1.Enabled = false;
                    rb_historial.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        //AGREGAR CHECHBOX DE MARCAR TODOS
        private void AgregarCheckBox()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 0;
            toolTip.ReshowDelay = 0;
            toolTip.ShowAlways = true;

            HeaderCheckBox.Size = new Size(15, 15);
            HeaderCheckBox.Location = new Point(15, 5);
            toolTip.SetToolTip(HeaderCheckBox, "Marcar todos");
            this.dgv_checadorbuscar.Controls.Add(HeaderCheckBox);

        }

        private void btn_scr_fecha_Click(object sender, EventArgs e)
        {
            //PARA PROGRESS BAR
            using (formularios_padres.ProgressBar frm = new formularios_padres.ProgressBar(sincronizar_fechaHora))
            {
                frm.lbl_mensaje.Text = "Sincronizando Fecha/Hora..";
                frm.ShowDialog(this);
            }
            if (sincronizar == true)
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Fecha Y hora sincronizada.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
            else
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "ERROR: Checador no conectado.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
        }

        //FUNCION PARA RECORRER TODAS LAS FILAS DEL DATAGRID Y SABER CUALES ESTÁN MARCADAS
        /*private void prueba_check()
        {
            int x = 1;
            foreach (DataGridViewRow dataGridRow in dgv_checadorbuscar.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridRow.Cells[0];
                chk.Value = true;

                if (dataGridRow.Cells["Check"].Value != null)
                {
                    MessageBox.Show("Columna " + x +" checada");
                }
                x = x + 1;
            }
        }*/

        private void sincronizar_fechaHora()
        {
            sincronizar = false;
            try
            {
                //CODIGO PARA APLICAR LA FUNCION A MULTIPLES CHECADORES
                foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
                {
                    //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                    if (row.Cells["Check"].Value != null)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                        {
                            //FUNCION PARA SINCRONIZAR LA FECHA Y HORA DEL CHECADOR CON LA DEL SERVIDOR
                            //var row = dgv_checadorbuscar.CurrentRow;
                            Conectar_Checador(Convert.ToInt32(row.Cells[1].Value), row.Cells[3].Value.ToString(), Convert.ToInt32(row.Cells[4].Value));
                            if (bConn)
                            {
                                Checador.SetDeviceTime(Convert.ToInt32(row.Cells[0].Value));
                                sincronizar = true;
                            }
                            //Checador.SetDeviceTime(Convert.ToInt32(row.Cells[0].Value));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        //ESTO ESTA MODIFICADO PARA MANDARLO A LA LAP DE MIRSA!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public void Conectar_Checador(int ID, string IP, int Puerto)
        {
            try
            {
                //Codigo para hacer el ping
                Ping HacerPing = new Ping();
                int iTiempoEspera = 5000;
                PingReply RespuestaPing;
                string sDireccion = IP;
                RespuestaPing = HacerPing.Send(sDireccion, iTiempoEspera);
                if (RespuestaPing.Status == IPStatus.Success)
                {
                    IP = RespuestaPing.Address.ToString();

                    //SE CREA UNA VARIABLE CON EL METODO CONECTAR DEL OBJETO CHECADOR.
                    //SE ENVIAN COMO PARAMETROS LA IP DEL CHECADOR Y EL PUERTO
                    bConn = Checador.Connect_Net(IP, Puerto);

                    if (bConn == true)
                    {
                        //SE ACTIVA EL DISPOSITIVO. PARAMETRO EL NUM. DE MAQUINA Y UNA BANDERA
                        Checador.EnableDevice(ID, true);
                    }
                    else
                    {
                        //ATENCION CAMBIAR ESTE MENSAJE A LA CONSOLA PARA MAYOR COMODIDAD
                        /*mensaje = new formularios_padres.mensaje_info();
                        mensaje.lbl_info.Text = "Dispositivo no conectado";
                        mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                        mensaje.Show();*/
                    }
                }
                else
                {
                    bConn = false;
                    //ATENCION CAMBIAR ESTE MENSAJE A LA CONSOLA PARA MAYOR COMODIDAD
                    /*mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Dispositivo no conectado";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.Show();*/
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }
        //**********************************************************************************************

        private void btn_fecha_manual_Click(object sender, EventArgs e)
        {
            //PARA PROGRESS BAR
            using (formularios_padres.ProgressBar frm = new formularios_padres.ProgressBar(aplicar_fecha_manual))
            {
                frm.lbl_mensaje.Text = "Aplicando Fecha/Hora..";
                frm.ShowDialog(this);
            }
            if (sincronizar == true)
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Fecha Y hora sincronizada.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
            else
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "ERROR: Checador no conectado.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
        }

        private void aplicar_fecha_manual()
        {

            try
            {
                sincronizar = false;
                // CODIGO PARA APLICAR LA FUNCION A MULTIPLES CHECADORES
                foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
                {
                    //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                    if (row.Cells["Check"].Value != null)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                        {
                            //FUNCION PARA SINCRONIZAR LA FECHA Y HORA DEL CHECADOR MANUALMENTE
                            Conectar_Checador(Convert.ToInt32(row.Cells[1].Value), row.Cells[3].Value.ToString(), Convert.ToInt32(row.Cells[4].Value));
                            if (bConn)
                            {
                                Checador.SetDeviceTime2(Convert.ToInt32(row.Cells[1].Value), dtp_fecha.Value.Year, dtp_fecha.Value.Month, dtp_fecha.Value.Day, dtp_hora.Value.Hour, dtp_hora.Value.Minute, dtp_hora.Value.Second);
                                sincronizar = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        private void btn_scr_eventos_Click(object sender, EventArgs e)
        {
            try
            {
                //PARA PROGRESS BAR
                using (formularios_padres.ProgressBar frm = new formularios_padres.ProgressBar(sincronizar_eventos))
                {
                    frm.lbl_mensaje.Text = "Sincronizando eventos..";
                    frm.ShowDialog(this);
                }

                //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
                /*Thread hilo_secundario = new Thread(new ThreadStart(this.progressbar));
                hilo_secundario.IsBackground = true;
                hilo_secundario.Start();*/

                if (sincronizar == true)
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Eventos sincronizados con exito.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.Show();
                }
                else
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "ERROR: Checador no conectado.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.Show();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        private void sincronizar_eventos()
        {
            //VARIABLE PARA SABER SI SE SINCRONIZARON EVENTOS DE ALGUN CHECADOR
            sincronizar = false;

            //FUNCION PARA SINCRONIZAR EVENTOS DE MULTIPLES CHECADORES
            foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
            {
                //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                if (row.Cells["Check"].Value != null)
                {
                    if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                    {
                        try
                        {
                            //FUNCION PARA SINCRONIZAR LOS EVENTOS DEL CHECADOR A LA BASE DE DATOS
                            //var row = dgv_checadorbuscar.CurrentRow;
                            Conectar_Checador(Convert.ToInt32(row.Cells[1].Value), row.Cells[3].Value.ToString(), Convert.ToInt32(row.Cells[4].Value));
                            if (bConn)
                            {
                                //LISTA PARA OBTENER LOS EMPLEADOS A LOS QUE SE LE APLICO LA SINCRONIZACION DE EVENTOS
                                List<int> id_empleados_eventos = new List<int>();
                                DateTime fecha_final = new DateTime();

                                string id = string.Empty;
                                int verifyMode = 0, inOutMode = 0, workCode = 0;
                                int Year = 0, Month = 0, Day = 0, Hour = 0, Minute = 0, Second = 0;
                                DateTime fecha_max;
                                ClaseSucursal Sucursal = new ClaseSucursal();
                                ClaseEmpleado Empleado = new ClaseEmpleado();

                                if (Checador.ReadGeneralLogData(Convert.ToInt32(row.Cells[1].Value)))//read all the attendance records to the memory
                                {
                                    ClaseAsignar_Horario AsignarHorario = new ClaseAsignar_Horario();
                                    fecha_max = Clase_Checador.verificarEvento(Convert.ToInt32(row.Cells[1].Value));
                                    while (Checador.SSR_GetGeneralLogData(Convert.ToInt32(row.Cells[1].Value), out id, out verifyMode,
                                               out inOutMode, out Year, out Month, out Day, out Hour, out Minute, out Second, ref workCode))//get records from the memory
                                    {

                                        //VALIDACION PARA SABER DESDE DONDE VAMOS A JALAR LOS EVENTOS DEL CHECADOR [SE BORRARA DESPUES!!]
                                        ClaseHorario Horario = new ClaseHorario();
                                        //ClaseAsignar_Horario AsignarHorario = new ClaseAsignar_Horario();
                                        if (fecha_max < Convert.ToDateTime(Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString()))
                                        {
                                            //AGREGAR LOS EMPLEADOS A LOS QUE SE LE APLICO LA SINCRONIZACION PARA PODER AGREGARLES LOS DESCANSOS
                                            if (!id_empleados_eventos.Contains(Convert.ToInt32(id)))
                                            {
                                                id_empleados_eventos.Add(Convert.ToInt32(id));
                                            }

                                            //SACAR LA ULTIMA FECHA DE LA SINCRONIZACION
                                            fecha_final = Convert.ToDateTime(Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString());

                                            //CARGAR LOS DATOS DEL HORARIO PERTENECIENTE A UN EMPLEADO
                                            Sucursal.obtenerIdSucursal(row.Cells[2].Value.ToString());
                                            //Empleado.obtenerIdHorario(Convert.ToInt32(id));

                                            AsignarHorario.verificar_existencia(Convert.ToInt32(id));
                                            DateTime dia = new DateTime(Year, Month, Day);

                                            if (dia.DayOfWeek.ToString() == "Monday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.lunes);
                                                Horario.id = AsignarHorario.lunes;
                                            }
                                            else if (dia.DayOfWeek.ToString() == "Tuesday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.martes);
                                                Horario.id = AsignarHorario.martes;
                                            }
                                            else if (dia.DayOfWeek.ToString() == "Wednesday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.miercoles);
                                                Horario.id = AsignarHorario.miercoles;
                                            }
                                            else if (dia.DayOfWeek.ToString() == "Thursday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.jueves);
                                                Horario.id = AsignarHorario.jueves;
                                            }
                                            else if (dia.DayOfWeek.ToString() == "Friday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.viernes);
                                                Horario.id = AsignarHorario.viernes;
                                            }
                                            else if (dia.DayOfWeek.ToString() == "Saturday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.sabado);
                                                Horario.id = AsignarHorario.sabado;
                                            }
                                            else if (dia.DayOfWeek.ToString() == "Sunday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.domingo);
                                                Horario.id = AsignarHorario.domingo;
                                            }
                                            
                                            //Horario.verificar_existencia(Empleado.id_horario);
                                            Clase_Checador.guardarEvento(Convert.ToInt32(row.Cells[1].Value), Convert.ToInt32(id), Sucursal.id, Convert.ToDateTime(Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString()), Horario.hr_entrada, Horario.hr_salida, Horario.hora_entrada_descanso, Horario.hora_salida_descanso, Horario.tolerancia, inOutMode, Horario.id);

                                            //OBTENER EL NOMBRE DEL EMPLEADO PARA MOSTRARLO EN EL DATAGRID
                                            Empleado.verificar_existencia(Convert.ToInt32(id));
                                            agregarFila(Convert.ToString(row.Cells[1].Value), Sucursal.id.ToString(), Convert.ToString(id), Empleado.nombre + " " + Empleado.apellido_pat + " " + Empleado.apellido_mat, Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString(), inOutMode);
                                        }
                                        else if (fecha_max == Convert.ToDateTime("1995-12-12 00:00:00"))
                                        {
                                            //CARGAR LOS DATOS DEL HORARIO PERTENECIENTE A UN EMPLEADO
                                            Sucursal.obtenerIdSucursal(row.Cells[2].Value.ToString());
                                            Empleado.obtenerIdHorario(Convert.ToInt32(id));
                                            Horario.verificar_existencia(Empleado.id_horario);
                                            Clase_Checador.guardarEvento(Convert.ToInt32(row.Cells[1].Value), Convert.ToInt32(id), Sucursal.id, Convert.ToDateTime(Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString()), Horario.hr_entrada, Horario.hr_salida, Horario.hora_entrada_descanso, Horario.hora_salida_descanso, Horario.tolerancia, inOutMode, Horario.id);
                                            //OBTENER EL NOMBRE DEL EMPLEADO PARA MOSTRARLO EN EL DATAGRID
                                            Empleado.verificar_existencia(Convert.ToInt32(id));
                                            //Agregar fila en DataGrid de datos sincronizados
                                            agregarFila(Convert.ToString(row.Cells[1].Value), Sucursal.id.ToString(), Convert.ToString(id), Empleado.nombre + " " + Empleado.apellido_pat +" "+ Empleado.apellido_mat,Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString(), inOutMode);
                                        }
                                    }

                                    //CICLO PARA CADA UNO DE LOS EMPLEADOS OBTENIDOS DE LOS EVENTOS
                                    foreach (var id_empleado in id_empleados_eventos)
                                    {
                                        int id_horario = -1;
                                        //OBTENER DESCANSOS DE LOS HORARIOS E INSERTAR EVENTOS
                                        //OBTENER LA PLANEACION SEMANAL DE HORARIOS DEL EMPLEADO Y VALIDACION PARA QUE EXISTA
                                        if (AsignarHorario.verificar_existencia(id_empleado))
                                        {
                                            DateTime fecha_inicial = fecha_max.AddDays(1.0);
                                            for (DateTime fecha = new DateTime(fecha_inicial.Year, fecha_inicial.Month, fecha_inicial.Day); fecha <= fecha_final; fecha = fecha.AddDays(1.0))
                                            {
                                                id_horario = -1;
                                                //SACAR LOS DESCANSOS, VACACIONES Y FALTAS
                                                if (fecha.DayOfWeek.ToString() == "Monday")
                                                {
                                                    if (AsignarHorario.lunes == 0)
                                                    {
                                                        id_horario = 0;
                                                    }
                                                    else if (AsignarHorario.lunes == 1)
                                                    {
                                                        id_horario = 1;
                                                    }
                                                }
                                                else if (fecha.DayOfWeek.ToString() == "Tuesday")
                                                {
                                                    if (AsignarHorario.martes == 0)
                                                    {
                                                        id_horario = 0;
                                                    }
                                                    else if (AsignarHorario.martes == 1)
                                                    {
                                                        id_horario = 1;
                                                    }
                                                }
                                                else if (fecha.DayOfWeek.ToString() == "Wednesday")
                                                {
                                                    if (AsignarHorario.miercoles == 0)
                                                    {
                                                        id_horario = 0;
                                                    }
                                                    else if (AsignarHorario.miercoles == 1)
                                                    {
                                                        id_horario = 1;
                                                    }
                                                }
                                                else if (fecha.DayOfWeek.ToString() == "Thursday")
                                                {
                                                    if (AsignarHorario.jueves == 0)
                                                    {
                                                        id_horario = 0;
                                                    }
                                                    else if (AsignarHorario.jueves == 1)
                                                    {
                                                        id_horario = 1;
                                                    }
                                                }
                                                else if (fecha.DayOfWeek.ToString() == "Friday")
                                                {
                                                    if (AsignarHorario.viernes == 0)
                                                    {
                                                        id_horario = 0;
                                                    }
                                                    else if (AsignarHorario.viernes == 1)
                                                    {
                                                        id_horario = 1;
                                                    }
                                                }
                                                else if (fecha.DayOfWeek.ToString() == "Saturday")
                                                {
                                                    if (AsignarHorario.sabado == 0)
                                                    {
                                                        id_horario = 0;
                                                    }
                                                    else if (AsignarHorario.sabado == 1)
                                                    {
                                                        id_horario = 1;
                                                    }
                                                }
                                                else if (fecha.DayOfWeek.ToString() == "Sunday")
                                                {
                                                    if (AsignarHorario.domingo == 0)
                                                    {
                                                        id_horario = 0;
                                                    }
                                                    else if (AsignarHorario.domingo == 1)
                                                    {
                                                        id_horario = 1;
                                                    }
                                                }

                                                if (id_horario == 0 || id_horario == 1)
                                                {
                                                    //SUMARLE LAS HORAS TRABAJADAS DE LOS DESCANSOS Y VACACIONES
                                                    Sucursal.obtenerIdSucursal(row.Cells[2].Value.ToString());
                                                    //GUARDAR EL DESCANSO O VACACIONES
                                                    Clase_Checador.guardarDescanso(Convert.ToInt32(row.Cells[1].Value), id_empleado, Sucursal.id, id_horario, fecha, fecha.AddDays(1.0));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //CODIGO PARA CUANDO EL EMPLEADO NO TENGA ASIGNADO UN HORARIO
                                            //MessageBox.Show("Empleado sin horario asignado");
                                        }
                                    }
                                    sincronizar = true;
                                }
                                else
                                {
                                    /*Checador.GetLastError(ref Error);
                                    MessageBox.Show(Error.ToString());*/
                                }
                                /*mensaje = new formularios_padres.mensaje_info();
                                mensaje = new formularios_padres.mensaje_info();
                                mensaje.lbl_info.Text = "Eventos sincronizados con exito.";
                                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                                mensaje.Show();*/

                            }
                            else
                            {
                                /*Checador.GetLastError(ref Error);
                                MessageBox.Show(Error.ToString());*/
                            }

                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 121 || ex.Number == 1232)
                            {
                                //CAMBIAR EL CURSOR
                                this.UseWaitCursor = false;
                                mensaje = new formularios_padres.mensaje_info();
                                mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                                mensaje.lbl_info2.Text = "Verifique la conexión.";
                                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                                mensaje.ShowDialog();
                            }
                            else
                            {
                                //CAMBIAR EL CURSOR
                                this.UseWaitCursor = false;
                                mensaje = new formularios_padres.mensaje_info();
                                mensaje.lbl_info.Text = "Error referente a la base de datos";
                                mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                                mensaje.ShowDialog();
                            }
                        }
                        catch (Exception ex)
                        {
                            //CAMBIAR EL CURSOR
                            this.UseWaitCursor = false;
                            frm_error = new formularios_padres.mensaje_error();
                            frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                            frm_error.txt_error.Text = (ex.Message.ToString());
                            frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                            frm_error.ShowDialog();
                        }
                    }
                }
            }

            if (sincronizar)
            {
                //CONDICION PARA INVOCAR EL DATAGRID DESDE OTRO HILO
                if (InvokeRequired)
                {
                    Invoke(new Action(() => tabControlBase.SelectedTab = tabPage4));
                }
            }
        }

        //Funcion para agregar filas al DATAGRID de eventos sincronizados
        private void agregarFila(string id_checador, string id_sucursal, string id_empleado, string nombre_empleado, string fecha_evento, int tipo_evento)
        {
            try
            {
                DataGridViewRow fila = new DataGridViewRow();
                fila.CreateCells(dgv_eventos_sincronizados);
                fila.Cells[0].Value = id_checador;
                fila.Cells[1].Value = id_empleado;
                fila.Cells[2].Value = nombre_empleado;
                fila.Cells[3].Value = id_sucursal;
                fila.Cells[4].Value = fecha_evento;
                if (tipo_evento == 0)
                {
                    fila.Cells[5].Value = "ENTRADA";
                }
                else if (tipo_evento == 1)
                {
                    fila.Cells[5].Value = "SALIDA";
                }

                //CONDICION PARA INVOCAR EL DATAGRID DESDE OTRO HILO
                if (InvokeRequired)
                {
                    Invoke(new Action(() => dgv_eventos_sincronizados.Rows.Add(fila)));
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        private void responder2(object sender, EventArgs e)
        {
            try
            {
                Enabled = true;
                respuesta = confirmacion.respuesta;

                if (respuesta == true)
                {
                    using (formularios_padres.ProgressBar frm = new formularios_padres.ProgressBar(borrar_eventos))
                    {
                        frm.lbl_mensaje.Text = "Borrando eventos..";
                        frm.ShowDialog(this);
                    }
                    //REALIZA ACCION SEGUN LA BANDERA RETORNADA
                    if (sincronizar == true)
                    {
                        mensaje = new formularios_padres.mensaje_info();
                        mensaje.lbl_info.Text = "Los eventos han sido borrados.";
                        mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                        mensaje.Show();
                    }
                    else
                    {
                        mensaje = new formularios_padres.mensaje_info();
                        mensaje.lbl_info.Text = "ERROR: Checador no conectado.";
                        mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                        mensaje.Show();
                    }
                }
                else
                {

                }
                confirmacion = null;
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        private void responder3(object sender, EventArgs e)
        {
            try
            {
                Enabled = true;
                respuesta = confirmacion.respuesta;

                if (respuesta == true)
                {
                    //PARA PROGRESS BAR
                    using (formularios_padres.ProgressBar frm = new formularios_padres.ProgressBar(borrar_usuarios))
                    {
                        frm.lbl_mensaje.Text = "Borrando usuarios..";
                        frm.ShowDialog(this);
                    }
                    //REALIZA ACCION SEGUN LA BANDERA RETORNADA
                    if (sincronizar == true)
                    {
                        mensaje = new formularios_padres.mensaje_info();
                        mensaje.lbl_info.Text = "Los usuarios han sido borrados con exito.";
                        mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                        mensaje.Show();
                    }
                    else
                    {
                        mensaje = new formularios_padres.mensaje_info();
                        mensaje.lbl_info.Text = "No se encontraron usuarios en el checador..";
                        mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                        mensaje.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }


        private void btn_borrar_eventos_Click(object sender, EventArgs e)
        {
            //PARA PROGRESS BAR
            confirmacion = new formularios_padres.Mensajes();
            confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea borrar los eventos";
            confirmacion.lbl_mensaje2.Text = "de este checador?";
            confirmacion.FormClosed += new FormClosedEventHandler(responder2);
            confirmacion.Show();
            Enabled = false;
        }

        private void borrar_eventos()
        {
            try
            {
                sincronizar = false;
                //CODIGO PARA APLICAR LA FUNCION A MULTIPLES CHECADORES
                foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
                {
                    //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                    if (row.Cells["Check"].Value != null)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                        {
                            //FUNCION PARA BORRAR TODOS LOS EVENTOS DE UN CHECADOR                
                            Conectar_Checador(Convert.ToInt32(row.Cells[1].Value), row.Cells[3].Value.ToString(), Convert.ToInt32(row.Cells[4].Value));
                            if (bConn)
                            {
                                Checador.ClearGLog(Convert.ToInt32(row.Cells[1].Value));
                                sincronizar = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        private void btn_borrar_usuarios_Click(object sender, EventArgs e)
        {
            confirmacion = new formularios_padres.Mensajes();
            confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea borrar todos los";
            confirmacion.lbl_mensaje2.Text = "usuarios de este checador?";
            confirmacion.FormClosed += new FormClosedEventHandler(responder3);
            confirmacion.Show();
            Enabled = false;

        }

        private void borrar_usuarios()
        {
            try
            {
                sincronizar = false;
                //CODIGO PARA APLICAR LA FUNCION A MULTIPLES CHECADORES
                foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
                {
                    //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                    if (row.Cells["Check"].Value != null)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                        {
                            //FUNCION PARA BORRAR TODOS LOS USUARIOS DE UN CHECADOR
                            int id_checador = Convert.ToInt32(row.Cells[1].Value);

                            Conectar_Checador(id_checador, row.Cells[3].Value.ToString(), Convert.ToInt32(row.Cells[4].Value));

                            //BANDERA SI BORRO O NO.
                            if (bConn)
                            {
                                if (Checador.ClearData(id_checador, 5))
                                {
                                    sincronizar = true;
                                }
                                else
                                {
                                    sincronizar = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

////////////////FILTRAR EL BUSCAR//////////////////////////
        private void txt_nombrebuscar_TextChanged(object sender, EventArgs e)
        {
            if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'A'";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'I'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'A'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'I'";
            }
            else
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*'";
            }
        }        

        

        private void cb_buscarActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'A'";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'I'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'A'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'I'";
            }
            else
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*'";
            }
        }

        private void cb_buscarInactivo_CheckedChanged(object sender, EventArgs e)
        {
            if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'A'";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'I'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'A'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'I'";
            }
            else
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*'";
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////

        //EVENTO CLICK EN MARCAR TODOS
        private void CheckBox_MarcarTodos_Click(object sender, MouseEventArgs e)
        {
            if (HeaderCheckBox.Checked == true)
            {
                foreach (DataGridViewRow dataGridRow in dgv_checadorbuscar.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridRow.Cells[0];
                    chk.Value = true;
                    dgv_checadorbuscar.RefreshEdit();
                }
            }
            else
            {
                foreach (DataGridViewRow dataGridRow in dgv_checadorbuscar.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridRow.Cells[0];
                    chk.Value = false;
                    dgv_checadorbuscar.RefreshEdit();
                }
            }
        }

        private void Regresar_Click(object sender, EventArgs e)
        {
            //LIMPIAR DATOS DEL DATAGRID
            dgv_eventos_sincronizados.Rows.Clear();
            dgv_eventos_sincronizados.Refresh();
            tabControlBase.SelectedTab = tabPage3;
        }

        private void txt_ip_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        //FUNCION PARA CUANDO DEJE EL CAMPO DE TEXTO ID BUSQUE SI EXISTE EL CHECADOR
        private void txt_id_Leave(object sender, EventArgs e)
        {
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.verificarExistencia));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
        }

        public void verificarExistencia()
        {
            try
            {
                if (txt_id.Text != "")
                {
                    Clase_Checador.id = Convert.ToInt32(txt_id.Text);
                    if (Clase_Checador.verificar_existencia(Clase_Checador.id))
                    {
                        MessageBox.Show("El ID del checador " + Clase_Checador.id + " ya existe. Ingrese otro ID");
                        
                        txt_id.Text = "";
                        //CONDICION PARA INVOCAR EL TXT DESDE OTRO HILO
                        if (InvokeRequired)
                        {
                            Invoke(new Action(() => txt_id.Focus()));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        private void txt_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void txt_puerto_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void txt_ip_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ip.Text))
            {

                errorProvider1.SetError(txt_ip, "No ha ingresado la IP del checador.");

            }
            else
            {
                errorProvider1.SetError(txt_ip, null);
                contador = contador + 1;
            }
        }

        private void txt_puerto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_puerto.Text))
            {

                errorProvider1.SetError(txt_puerto, "No ha ingresado el puerto del checador.");

            }
            else
            {
                errorProvider1.SetError(txt_puerto, null);
                contador = contador + 1;
            }
        }
        //FUNCION PARA IR A MODIFICAR DESDE CONSULTAR CHECADOR
        private void btn_b_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                // CAMBIAR EL CURSOR
                //this.UseWaitCursor = true;
                //CODIGO PARA APLICAR LA FUNCION A MULTIPLES CHECADORES
                foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
                {
                    //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                    if (row.Cells["Check"].Value != null)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                        {
                            Clase_Checador.id = Convert.ToInt32(row.Cells[1].Value);
                            rb_modificar.Checked = true;
                            txt_id_mod.Text = Convert.ToString(Clase_Checador.id);
                            tabControlBase.SelectedTab = tabPage2;
                            btn_ir_modificar.PerformClick();
                        }
                    }
                }
                // CAMBIAR EL CURSOR
                //this.UseWaitCursor = false;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        //FUNCION PARA DAR DE BAJA UN CHECADOR
        private void btn_dar_baja_Click(object sender, EventArgs e)
        {
            try
            {
                //CODIGO PARA APLICAR LA FUNCION A MULTIPLES CHECADORES
                foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
                {
                    //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                    if (row.Cells["Check"].Value != null)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                        {
                            Clase_Checador.id = Convert.ToInt32(row.Cells[1].Value);
                            Clase_Checador.estatus = "I";
                            Clase_Checador.Eliminar_Checador();
                            //FUNCION PAR RECARGAR EL DATAGRID
                            // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Sucursal' table. You can move, or remove it, as needed.
                            this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        private void txt_id_Validating(object sender, CancelEventArgs e)
        {
            //PARA LA COMPU DE MIRSAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAaa
            try
            {
                if (string.IsNullOrEmpty(txt_id.Text))
                {
                    errorProvider1.SetError(txt_id, "No ha ingresado el ID del checador.");
                }
                else
                {
                    errorProvider1.SetError(txt_id, null);
                    contador = contador + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
        }

        private void txt_ip_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_id.Text))
            {

                errorProvider1.SetError(txt_id, "No ha ingresado la IP del checador.");

            }
            else
            {
                errorProvider1.SetError(txt_id, null);
                contador = contador + 1;
            }
        }

        private void txt_puerto_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_id.Text))
            {

                errorProvider1.SetError(txt_id, "No ha ingresado el puerto del checador.");

            }
            else
            {
                errorProvider1.SetError(txt_id, null);
                contador = contador + 1;
            }
        }

        private void txt_id_mod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //EJECUTAR MODIFICAR CON ENTER
            if (e.KeyChar == 13)
            {
                btn_ir_modificar.PerformClick();
            }
            validar.solonumeros(e);
        }

        private void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage5;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FileName = "";
                if (Convert.ToDateTime(dtp_fecha_inicial.Value.ToString("yyyy-MM-dd 00:00:00")) > Convert.ToDateTime(dtp_fecha_final.Value.ToString("yyyy-MM-dd 00:00:00")))
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "La fecha inicial es menor que la fecha final.";
                    mensaje.lbl_info2.Text = "Verifique las fechas.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    DateTime fecha_inicial, fecha_final;
                    fecha_inicial = new DateTime(dtp_fecha_inicial.Value.Year, dtp_fecha_inicial.Value.Month, dtp_fecha_inicial.Value.Day);
                    fecha_final = new DateTime(dtp_fecha_final.Value.Year, dtp_fecha_final.Value.Month, dtp_fecha_final.Value.Day);

                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                    saveFileDialog1.FileName = "Resumen(" + fecha_inicial.ToString("dd-MM-yyyy") + "_" + fecha_final.ToString("dd-MM-yyyy") + ").txt";
                    saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.RestoreDirectory = true;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        FileName = saveFileDialog1.FileName;

                        //PARA PROGRESS BAR
                        using (formularios_padres.ProgressBar frm = new formularios_padres.ProgressBar(guardarResumen))
                        {
                            frm.lbl_mensaje.Text = "Creando archivo..";
                            frm.ShowDialog(this);
                        }
                        if (sincronizar == true)
                        {
                            mensaje = new formularios_padres.mensaje_info();
                            mensaje.lbl_info.Text = "Archivo creado con éxito.";
                            mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                            mensaje.Show();
                        }

                        //CAMBIAR EL CURSOR
                        //this.UseWaitCursor = true;

                        //Clase_Checador.guardarResumen(fecha_inicial, fecha_final, saveFileDialog1.FileName);
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        private void guardarResumen()
        {
            try
            {
                //VARIABLE PARA SABER QUE TODO SALIO BIEN
                sincronizar = false;

                DateTime fecha_inicial, fecha_final;
                fecha_inicial = new DateTime(dtp_fecha_inicial.Value.Year, dtp_fecha_inicial.Value.Month, dtp_fecha_inicial.Value.Day);
                fecha_final = new DateTime(dtp_fecha_final.Value.Year, dtp_fecha_final.Value.Month, dtp_fecha_final.Value.Day);

                Clase_Checador.guardarResumen(fecha_inicial, fecha_final, FileName);

                sincronizar = true;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage6;
        }
    }
}
