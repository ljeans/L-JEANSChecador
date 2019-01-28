using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace Checador
{
    public partial class horarios : Checador.formularios_padres.formpadre
    {
        ClaseHorario Horario = new ClaseHorario();
        ClaseAsignar_Horario AsignarHorario = new ClaseAsignar_Horario();
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();
        formularios_padres.mensaje_error frm_error = new formularios_padres.mensaje_error();
        double horas_descanso, horas_diarias;
        validacion validar = new validacion();

        public bool respuesta = false;
        public bool descansoFlag;

        public horarios()
        {
            InitializeComponent();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void cargarID()
        {
            //MOSTRAR EL ID DEL EMPLEADO AL CARGAR LA PAGINA
            try
            {
                txt_id.Text = (Horario.obtenerIdMaximo() + 1).ToString();
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
                txt_id.Text = "1";
            }
        }

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;
        }
        
        private void Limpiar()
        {
 
            txt_id_a_modificar.Text = "";
            txt_nombre.Text = "";
            txt_horas_diarias.Text = "8";
            txt_horas_totales.Text = "96";
            dtp_hora_entrada.Text = "09:00";
            dtp_hora_entrada_desc.Text = "15:00";
            dtp_hora_salida.Text = "19:00";
            dtp_hora_salida_desc.Text = "13:00";
            txt_tolerancia.Text = "10";
            
            cargarID();

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
                    Horario.Modificar_Horario(descansoFlag);
                    //FUNCION PAR RECARGAR EL DATAGRID
                    // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Horario' table. You can move, or remove it, as needed.
                    this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
                    Limpiar();
                    tabControlBase.SelectedTab = tabPage2;
                    txt_id_a_modificar.Focus();
                }
                else
                {

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

        // MODIFICAR///////////////////////////////////////////////////////////////////////////////////
        private void btn_modificar_Click(object sender, EventArgs e)
        {
            descansoFlag = false;
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                Horario.id = Convert.ToInt32(txt_id.Text);
                Horario.horario = txt_nombre.Text;
                Horario.horas_diarias = Convert.ToInt32(txt_horas_diarias.Text);
                Horario.horas_totales_quincenales = Convert.ToInt32(txt_horas_totales.Text);

                if (cb_descanso.Checked == true)
                {
                    descansoFlag = true;
                    Horario.hora_entrada_descanso = dtp_hora_entrada_desc.Value.TimeOfDay;  //izi
                    Horario.hora_salida_descanso = dtp_hora_salida_desc.Value.TimeOfDay;    //pizi
                }
                else
                {
                    descansoFlag = false;
                }
                Horario.hr_entrada = dtp_hora_entrada.Value.TimeOfDay;
                Horario.hr_salida = dtp_hora_salida.Value.TimeOfDay;
                Horario.tolerancia = Convert.ToInt32(txt_tolerancia.Text);
                Horario.id_empleado = Program.id_empleado;

                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                confirmacion = new formularios_padres.Mensajes();
                confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea modificar el horario?";
                confirmacion.FormClosed += new FormClosedEventHandler(responder);
                confirmacion.Show();
                Enabled = false;
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

        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage2;
        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                tabControlBase.SelectedTab = tabPage1;
                btn_modificar.Visible = false;
                btn_registrar.Visible = true;
                Limpiar();

                //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
                CheckForIllegalCrossThreadCalls = false;
                Thread hilo_secundario = new Thread(new ThreadStart(this.cargarID));
                hilo_secundario.IsBackground = true;
                hilo_secundario.Start();
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

        //FUNCION PARA CARGAR LOS DATOS DEL HORARIO EN EL FORMULARIO
        private void btn_ir_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                txt_id.Enabled = false;
                Horario.id = Convert.ToInt32(txt_id_a_modificar.Text);
                if (Horario.verificar_existencia(Horario.id))
                {
                    tabControlBase.SelectedTab = tabPage1;
                    txt_id.Text = Horario.id.ToString();
                    txt_nombre.Text = Horario.horario;
                    txt_horas_diarias.Value = Horario.horas_diarias;
                    txt_horas_totales.Value = Horario.horas_totales_quincenales;
                    txt_tolerancia.Value = Horario.tolerancia;
                    dtp_hora_entrada.Value = Convert.ToDateTime(Horario.hr_entrada.ToString());

                    //VALIDACION PARA NULL EN HORAS DE DESCANSO
                    TimeSpan hora_fija = new TimeSpan(00, 00, 00);
                    if (Horario.hora_entrada_descanso != hora_fija)
                    {
                        dtp_hora_entrada_desc.Value = Convert.ToDateTime(Horario.hora_entrada_descanso.ToString());
                    }
                    else
                    {
                        cb_descanso.Checked = false;
                        dtp_hora_entrada_desc.Enabled = false;
                    }

                    if (Horario.hora_entrada_descanso != hora_fija)
                    {
                        dtp_hora_salida_desc.Value = Convert.ToDateTime(Horario.hora_salida_descanso.ToString());
                    }
                    else
                    {
                        cb_descanso.Checked = false;
                        dtp_hora_salida_desc.Enabled = false;
                    }

                    dtp_hora_salida.Value = Convert.ToDateTime(Horario.hr_salida.ToString());

                    btn_registrar.Visible = false;
                    btn_modificar.Visible = true;
                }
                else
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Horario no registrado. Por favor intente de nuevo.";
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

        private void horarios_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.horarios' Puede moverla o quitarla según sea necesario.
                this.horariosTableAdapter.Fill(this.dataSet_Checador.horarios);
                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_Empleados' Puede moverla o quitarla según sea necesario.
                this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Horario' table. You can move, or remove it, as needed.
                this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
                //CAMBIAR LA LETRA AL DATAGRIDVIEW
                dgv_horarios.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
                dgv_horarios.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

                //CARGAR LOS HORARIOS EN ASIGNAR HORARIOS
                cbx_empleado_SelectedIndexChanged(cbx_empleado, EventArgs.Empty);

                cbx_lunes_SelectedIndexChanged(cbx_lunes, EventArgs.Empty);
                cbx_martes_SelectedIndexChanged(cbx_martes, EventArgs.Empty);
                cbx_miercoles_SelectedIndexChanged(cbx_miercoles, EventArgs.Empty);
                cbx_jueves_SelectedIndexChanged(cbx_jueves, EventArgs.Empty);
                cbx_viernes_SelectedIndexChanged(cbx_viernes, EventArgs.Empty);
                cbx_sabado_SelectedIndexChanged(cbx_sabado, EventArgs.Empty);
                cbx_domingo_SelectedIndexChanged(cbx_domingo, EventArgs.Empty);

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
                                            if (w is TextBox | w is Label | w is Button | w is MaskedTextBox | w is DateTimePicker | w is ComboBox | w is RadioButton | w is PictureBox | w is GroupBox | w is DataGridView | w is NumericUpDown)
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
                
                //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
                CheckForIllegalCrossThreadCalls = false;
                Thread hilo_secundario = new Thread(new ThreadStart(this.cargarID));
                hilo_secundario.IsBackground = true;
                hilo_secundario.Start();
                txt_nombre.Focus();

                //FLITRAR CONSULTA HORARIO DEPENDIENDO EL ROL
                if (Program.rol != "ENCARGADA DE TIENDA")
                {
                    this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
                    vistaHorarioBindingSource.Filter = "";
                }
                else
                {
                    vistaHorarioBindingSource.Filter = "lunes ='" + Program.id_empleado + "' or [id_horario] = 0";
                    vistaHorarioBindingSource1.Filter = "lunes ='" + Program.id_empleado + "' or [id_horario] = 0";
                    vistaHorarioBindingSource2.Filter = "lunes ='" + Program.id_empleado + "' or [id_horario] = 0";
                    vistaHorarioBindingSource3.Filter = "lunes ='" + Program.id_empleado + "' or [id_horario] = 0";
                    vistaHorarioBindingSource4.Filter = "lunes ='" + Program.id_empleado + "' or [id_horario] = 0";
                    vistaHorarioBindingSource5.Filter = "lunes ='" + Program.id_empleado + "' or [id_horario] = 0";
                    vistaHorarioBindingSource6.Filter = "lunes ='" + Program.id_empleado + "' or [id_horario] = 0";
                }

                //FILTRAR COMBOBOX DE EMPLEADOS EN ASIGNAR HORARIO DEPENDIENDO EL ROL
                if (Program.rol != "ENCARGADA DE TIENDA")
                {
                    //FILTAR LOS EMPLEADOS POR EL ESTATUS, MOSTAR SOLO LOS ACTIVOS
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                    vistaEmpleadosBindingSource.Filter = "estatus='A'";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and estatus = 'A'";
                }

                //DESACTIVAR LOS DESCANSOS AL INICIAR
                cb_descanso.Checked = false;
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

        ///REGISTRAR///////////////////////////////////////////////////////////////////////////////////
        private void btn_registrar_Click(object sender, EventArgs e)
        {
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.Registrar));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
        }

        public void Registrar()
        {
            descansoFlag = false;
            try
            {
                //CAMBIAR EL CURSOR
               
                Horario.id = Convert.ToInt32(txt_id.Text);
                Horario.horario = txt_nombre.Text;
                Horario.horas_diarias = Convert.ToInt32(txt_horas_diarias.Text);
                Horario.horas_totales_quincenales = Convert.ToInt32(txt_horas_totales.Text);
                Horario.id_empleado = Program.id_empleado;

                if (cb_descanso.Checked == true)
                {
                    descansoFlag = true;
                    Horario.hora_entrada_descanso = dtp_hora_entrada_desc.Value.TimeOfDay;  //izi
                    Horario.hora_salida_descanso = dtp_hora_salida_desc.Value.TimeOfDay;    //pizi
                }
                else
                {
                    descansoFlag = false;
                }

                Horario.hr_entrada = new TimeSpan(dtp_hora_entrada.Value.TimeOfDay.Hours, dtp_hora_entrada.Value.Minute, 00);
                Horario.hr_salida = new TimeSpan(dtp_hora_salida.Value.TimeOfDay.Hours, dtp_hora_salida.Value.Minute, 00);
                Horario.tolerancia = Convert.ToInt32(txt_tolerancia.Text);

                if (Horario.id.ToString() == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el id del horario.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_id.Focus();
                }
                else if (Horario.horario == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el nombre del horario.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_nombre.Focus();
                }
                else
                {
                    this.UseWaitCursor = true;
                    Horario.guardarHorario(descansoFlag);
                    //FUNCION PAR RECARGAR EL DATAGRID
                    // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Horario' table. You can move, or remove it, as needed.
                    this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
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

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rb_buscar.Checked==true)
                {
                    this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
                    tabControlBase.SelectedTab = tabPage3;
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

        private void cb_descanso_CheckedChanged(object sender, EventArgs e)
        {
            //FUNCION PARA CALCULAR HORAS TRABAJADAS CUANDO SE HABILITEN O NO LAS HORAS DE DESCANSO
            try
            {
                DateTime hora_e = Convert.ToDateTime(dtp_hora_entrada.Text);
                DateTime hora_s = Convert.ToDateTime(dtp_hora_salida.Text);
                if (cb_descanso.Checked == true)
                {
                    dtp_hora_entrada_desc.Enabled = true;
                    dtp_hora_salida_desc.Enabled = true;
                    DateTime salida_eat = Convert.ToDateTime(dtp_hora_salida_desc.Text);
                    DateTime regreso_eat = Convert.ToDateTime(dtp_hora_entrada_desc.Text);

                    TimeSpan salida_comer = new TimeSpan(salida_eat.Hour, salida_eat.Minute, salida_eat.Second);
                    TimeSpan regreso_comer = new TimeSpan(regreso_eat.Hour, regreso_eat.Minute, regreso_eat.Second);
                    horas_descanso = regreso_comer.TotalMinutes - salida_comer.TotalMinutes;
                    TimeSpan entrada = new TimeSpan(hora_e.Hour, hora_e.Minute, hora_e.Second);
                    TimeSpan salida = new TimeSpan(hora_s.Hour, hora_s.Minute, hora_s.Second);
                    horas_diarias = salida.TotalMinutes - entrada.TotalMinutes;
                    txt_horas_diarias.Value = Math.Truncate((Convert.ToDecimal(horas_diarias) - Convert.ToDecimal(horas_descanso)) / 60);


                }
                else if (cb_descanso.Checked == false)
                {
                    dtp_hora_entrada_desc.Enabled = false;
                    dtp_hora_salida_desc.Enabled = false;

                    TimeSpan entrada = new TimeSpan(hora_e.Hour, hora_e.Minute, hora_e.Second);
                    TimeSpan salida = new TimeSpan(hora_s.Hour, hora_s.Minute, hora_s.Second);
                    horas_diarias = salida.TotalMinutes - entrada.TotalMinutes;
                    txt_horas_diarias.Value = Math.Truncate(Convert.ToDecimal(horas_diarias) / 60);
                }
            }
            catch (Exception ex)
            {
                int code = ex.HResult;
                if (code == -2146233086)
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = ("Las horas trabajadas no pueden ser iguales o menores a 0. Verifique las horas ingresadas anteriormente y seleccione un horario válido.");
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
                else
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = (code + " " + ex.Message.ToString());
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
            }
        }

        private void tabControlBase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

////////////////FILTRAR EL BUSCAR//////////////////////////
        private void txt_idbuscar_TextChanged(object sender, EventArgs e)
        {
            if (Program.rol != "ENCARGADA DE TIENDA")
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
                    vistaHorarioBindingSource.Filter = "";
                }
                else
                {
                    vistaHorarioBindingSource.Filter = "CONVERT([id_horario], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [horario] LIKE '*" + txt_nombrebuscar.Text + "*'";
                }
            }
            else
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
                    vistaHorarioBindingSource.Filter = "lunes ='" + Program.id_empleado + "'";
                }
                else
                {
                    vistaHorarioBindingSource.Filter = "CONVERT([id_horario], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [horario] LIKE '*" + txt_nombrebuscar.Text + "*' and [lunes] = '" + Program.id_empleado + "'";
                }
            }
        }

        private void txt_nombrebuscar_TextChanged(object sender, EventArgs e)
        {
            if (Program.rol != "ENCARGADA DE TIENDA")
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
                    vistaHorarioBindingSource.Filter = "";
                }
                else
                {
                    vistaHorarioBindingSource.Filter = "CONVERT([id_horario], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [horario] LIKE '*" + txt_nombrebuscar.Text + "*'";
                }
            }
            else
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
                    vistaHorarioBindingSource.Filter = "lunes ='" + Program.id_empleado + "'";
                }
                else
                {
                    vistaHorarioBindingSource.Filter = "CONVERT([id_horario], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [horario] LIKE '*" + txt_nombrebuscar.Text + "*' and [lunes] = '" + Program.id_empleado + "'";
                }
            }
        }

        private void rb_asignar_horarios_CheckedChanged(object sender, EventArgs e)
        {
            cargarHorarioEmpleado();
            tabControlBase.SelectedTab = tabPage4;
        }

        private void cbx_lunes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Horario.verificar_horario(cbx_lunes.SelectedValue.ToString());
                if (Horario.hr_entrada != new TimeSpan(00, 01, 00))
                {
                    label20.Text = Horario.hr_entrada.ToString();
                    label21.Text = Horario.hr_salida.ToString();
                    if (Horario.hora_salida_descanso.ToString() == "00:00:00")
                    {
                        label22.Text = "-";
                    }
                    else
                    {
                        label22.Text = Horario.hora_salida_descanso.ToString();
                    }
                    if (Horario.hora_entrada_descanso.ToString() == "00:00:00")
                    {
                        label23.Text = "-";
                    }
                    else
                    {
                        label23.Text = Horario.hora_entrada_descanso.ToString();
                    }
                }else
                {
                    label20.Text = "-";
                    label21.Text = "-";
                    label22.Text = "-";
                    label23.Text = "-";
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void cbx_martes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Horario.verificar_horario(cbx_martes.SelectedValue.ToString());
                if (Horario.hr_entrada != new TimeSpan(00, 01, 00))
                {
                    lbl_martes_1.Text = Horario.hr_entrada.ToString();
                    lbl_martes_4.Text = Horario.hr_salida.ToString();
                    if (Horario.hora_salida_descanso.ToString() == "00:00:00")
                    {
                        lbl_martes_2.Text = "-";
                    }
                    else
                    {
                        lbl_martes_2.Text = Horario.hora_salida_descanso.ToString();
                    }
                    if (Horario.hora_entrada_descanso.ToString() == "00:00:00")
                    {
                        lbl_martes_3.Text = "-";
                    }
                    else
                    {
                        lbl_martes_3.Text = Horario.hora_entrada_descanso.ToString();
                    }
                }
                else
                {
                    lbl_martes_1.Text = "-";
                    lbl_martes_2.Text = "-";
                    lbl_martes_3.Text = "-";
                    lbl_martes_4.Text = "-";
                }
            }
            catch (Exception)
            {
                lbl_martes_2.Text = "-";
                lbl_martes_3.Text = "-";
            }
        }

        private void cbx_miercoles_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                Horario.verificar_horario(cbx_miercoles.SelectedValue.ToString());
                if (Horario.hr_entrada != new TimeSpan(00, 01, 00))
                {
                    lbl_miercoles_1.Text = Horario.hr_entrada.ToString();
                    lbl_miercoles_4.Text = Horario.hr_salida.ToString();
                    if (Horario.hora_salida_descanso.ToString() == "00:00:00")
                    {
                        lbl_miercoles_2.Text = "-";
                    }
                    else
                    {
                        lbl_miercoles_2.Text = Horario.hora_salida_descanso.ToString();
                    }
                    if (Horario.hora_entrada_descanso.ToString() == "00:00:00")
                    {
                        lbl_miercoles_3.Text = "-";
                    }
                    else
                    {
                        lbl_miercoles_3.Text = Horario.hora_entrada_descanso.ToString();
                    }
                }
                else
                {
                    lbl_miercoles_1.Text = "-";
                    lbl_miercoles_2.Text = "-";
                    lbl_miercoles_3.Text = "-";
                    lbl_miercoles_4.Text = "-";
                }
            }
            catch (Exception)
            {
                lbl_miercoles_3.Text = "-";
                lbl_miercoles_2.Text = "-";
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void cbx_jueves_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Horario.verificar_horario(cbx_jueves.SelectedValue.ToString());
                if (Horario.hr_entrada != new TimeSpan(00, 01, 00))
                {
                    lbl_jueves_1.Text = Horario.hr_entrada.ToString();
                    lbl_jueves_4.Text = Horario.hr_salida.ToString();
                    if (Horario.hora_salida_descanso.ToString() == "00:00:00")
                    {
                        lbl_jueves_2.Text = "-";
                    }
                    else
                    {
                        lbl_jueves_2.Text = Horario.hora_salida_descanso.ToString();
                    }
                    if (Horario.hora_entrada_descanso.ToString() == "00:00:00")
                    {
                        lbl_jueves_3.Text = "-";
                    }
                    else
                    {
                        lbl_jueves_3.Text = Horario.hora_entrada_descanso.ToString();
                    }
                }
                else
                {
                    lbl_jueves_1.Text = "-";
                    lbl_jueves_2.Text = "-";
                    lbl_jueves_3.Text = "-";
                    lbl_jueves_4.Text = "-";
                }
            }
            catch (Exception)
            {
                lbl_jueves_3.Text = "-";
                lbl_jueves_2.Text = "-";
            }
        }

        private void cbx_viernes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Horario.verificar_horario(cbx_viernes.SelectedValue.ToString());
                if (Horario.hr_entrada != new TimeSpan(00, 01, 00))
                {
                    lbl_viernes_1.Text = Horario.hr_entrada.ToString();
                    lbl_viernes_4.Text = Horario.hr_salida.ToString();
                    if (Horario.hora_salida_descanso.ToString() == "00:00:00")
                    {
                        lbl_viernes_2.Text = "-";
                    }
                    else
                    {
                        lbl_viernes_2.Text = Horario.hora_salida_descanso.ToString();
                    }
                    if (Horario.hora_entrada_descanso.ToString() == "00:00:00")
                    {
                        lbl_viernes_3.Text = "-";
                    }
                    else
                    {
                        lbl_viernes_3.Text = Horario.hora_entrada_descanso.ToString();
                    }
                }
                else
                {
                    lbl_viernes_1.Text = "-";
                    lbl_viernes_2.Text = "-";
                    lbl_viernes_3.Text = "-";
                    lbl_viernes_4.Text = "-";
                }

            }
            catch (Exception)
            {
                lbl_viernes_3.Text = "-";
                lbl_viernes_2.Text = "-";
            }
        }

        private void cbx_sabado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Horario.verificar_horario(cbx_sabado.SelectedValue.ToString());
                if (Horario.hr_entrada != new TimeSpan(00, 01, 00))
                {
                    lbl_sabado_1.Text = Horario.hr_entrada.ToString();
                    lbl_sabado_4.Text = Horario.hr_salida.ToString();
                    if (Horario.hora_salida_descanso.ToString() == "00:00:00")
                    {
                        lbl_sabado_2.Text = "-";
                    }
                    else
                    {
                        lbl_sabado_2.Text = Horario.hora_salida_descanso.ToString();
                    }
                    if (Horario.hora_entrada_descanso.ToString() == "00:00:00")
                    {
                        lbl_sabado_3.Text = "-";
                    }
                    else
                    {
                        lbl_sabado_3.Text = Horario.hora_entrada_descanso.ToString();
                    }
                }
                else
                {
                    lbl_sabado_1.Text = "-";
                    lbl_sabado_2.Text = "-";
                    lbl_sabado_3.Text = "-";
                    lbl_sabado_4.Text = "-";
                }
            }
            catch (Exception)
            {
                lbl_sabado_3.Text = "-";
                lbl_sabado_2.Text = "-";
            }
        }

        private void cbx_domingo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Horario.verificar_horario(cbx_domingo.SelectedValue.ToString());
                if (Horario.hr_entrada != new TimeSpan(00,01,00))
                {
                    lbl_domingo_1.Text = Horario.hr_entrada.ToString();
                    lbl_domingo_4.Text = Horario.hr_salida.ToString();
               
                    if(Horario.hora_salida_descanso.ToString()== "00:00:00")
                    {
                        lbl_domingo_2.Text = "-";
                    }
                    else
                    {
                        lbl_domingo_2.Text = Horario.hora_salida_descanso.ToString();
                    }
                    if (Horario.hora_entrada_descanso.ToString() == "00:00:00")
                    {
                        lbl_domingo_3.Text = "-";
                    }
                    else
                    {
                        lbl_domingo_3.Text = Horario.hora_entrada_descanso.ToString();
                    }
                }
                else
                {
                    lbl_domingo_1.Text = "-";
                    lbl_domingo_2.Text = "-";
                    lbl_domingo_3.Text = "-";
                    lbl_domingo_4.Text = "-";
                }
            }
            catch (Exception)
            {
                lbl_domingo_3.Text = "-";
                lbl_domingo_2.Text = "-";
            }
        }
        ///////////////////////////////////////////////////////////////

        //GURADAR LA ASIGNACION DE UN HORARIO A UN EMPLEADO
        private void btn_siguiente_Click(object sender, EventArgs e)
        {
            try
            {
                AsignarHorario.id_empleado = Convert.ToInt32(cbx_empleado.SelectedValue.ToString());
                AsignarHorario.lunes = Convert.ToInt32(cbx_lunes.SelectedValue.ToString());
                AsignarHorario.martes = Convert.ToInt32(cbx_martes.SelectedValue.ToString());
                AsignarHorario.miercoles = Convert.ToInt32(cbx_miercoles.SelectedValue.ToString());
                AsignarHorario.jueves = Convert.ToInt32(cbx_jueves.SelectedValue.ToString());
                AsignarHorario.viernes = Convert.ToInt32(cbx_viernes.SelectedValue.ToString());
                AsignarHorario.sabado = Convert.ToInt32(cbx_sabado.SelectedValue.ToString());
                AsignarHorario.domingo = Convert.ToInt32(cbx_domingo.SelectedValue.ToString());
                AsignarHorario.asignarHorario();

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

        //FUNCION PARA VERIFICAR SI UN EMPLEADO YA TIENE UN HORARIO ASIGNADO Y CARGARLO EN LOS COMBOBOX
        private void cargarHorarioEmpleado()
        {
            try
            {
                AsignarHorario.id_empleado = Convert.ToInt32(cbx_empleado.SelectedValue.ToString());
                if (AsignarHorario.verificar_existencia(AsignarHorario.id_empleado))
                {
                    cbx_lunes.SelectedValue = AsignarHorario.lunes;
                    cbx_martes.SelectedValue = AsignarHorario.martes;
                    cbx_miercoles.SelectedValue = AsignarHorario.miercoles;
                    cbx_jueves.SelectedValue = AsignarHorario.jueves;
                    cbx_viernes.SelectedValue = AsignarHorario.viernes;
                    cbx_sabado.SelectedValue = AsignarHorario.sabado;
                    cbx_domingo.SelectedValue = AsignarHorario.domingo;
                }
                else
                {
                    cbx_lunes.SelectedValue = 0;
                    cbx_martes.SelectedValue = 0;
                    cbx_miercoles.SelectedValue = 0;
                    cbx_jueves.SelectedValue = 0;
                    cbx_viernes.SelectedValue = 0;
                    cbx_sabado.SelectedValue = 0;
                    cbx_domingo.SelectedValue = 0;
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

            }
        }

        private void cbx_empleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarHorarioEmpleado();
        }

        //FUNCION PARA CARGAR LOS DATOS DESDE LA PESTAÑA CONSULTAR PARA MODIFICAR
        private void btn_b_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                // CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                var row = dgv_horarios.CurrentRow;
                Horario.id = Convert.ToInt32(row.Cells[0].Value);
                rb_modificar.Checked = true;
                txt_id_a_modificar.Text = Convert.ToString(Horario.id);
                tabControlBase.SelectedTab = tabPage2;
                btn_ir_modificar.PerformClick();
                // CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
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

        private void txt_id_Leave(object sender, EventArgs e)
        {
            if (txt_id.Text != "")
            {
                Horario.id = Convert.ToInt32(txt_id.Text);
                if (Horario.verificar_existencia(Horario.id))
                {
                    MessageBox.Show("El ID del horario " + Horario.id + " ya existe. Ingrese otro ID");
                    txt_id.Text = "";
                    txt_id.Focus();
                }
            }
        }

        //FUNCION PARA ELIMINAR UN HORARIO PERMANENTEMENTE DE LA BD
        private void btn_dar_baja_Click(object sender, EventArgs e)
        {
            try
            {
                confirmacion = new formularios_padres.Mensajes();
                confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea eliminar el horario?";
                confirmacion.FormClosed += new FormClosedEventHandler(eliminar);
                confirmacion.Show();
                Enabled = false;
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

        public void eliminar(object sender, EventArgs e)
        {
            try
            {
                Enabled = true;
                respuesta = confirmacion.respuesta;
                if (respuesta) {
                    var row = dgv_horarios.CurrentRow;
                    Horario.id = Convert.ToInt32(row.Cells[0].Value);
                    Horario.eliminarHorario();
                    //FUNCION PAR RECARGAR EL DATAGRID
                    // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Horario' table. You can move, or remove it, as needed.
                    this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
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
            if (string.IsNullOrEmpty(txt_id.Text))
            {

                errorProvider1.SetError(txt_id, "No ha ingresado el ID del horario");

            }
            else
            {
                errorProvider1.SetError(txt_id, null);
            }
        }

        private void txt_nombre_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_nombre.Text))
            {

                errorProvider1.SetError(txt_nombre, "No ha ingresado el nombre del horario");

            }
            else
            {
                errorProvider1.SetError(txt_nombre, null);
            }
        }

        private void txt_nombre_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_nombre.Text))
            {

                errorProvider1.SetError(txt_nombre, "No ha ingresado el nombre del horario.");

            }
            else
            {
                errorProvider1.SetError(txt_nombre, null);
               
            }
        }

        private void dtp_hora_salida_ValueChanged(object sender, EventArgs e)
        {
            //CALCULAR HORAS AUTOMATICAMENTE DEPENDIENDO DE LAS HORAS ESTABLECIDAD DE ENTRADA-SALIDA Y DESCANSOS
            try
            {
                DateTime hora_e = Convert.ToDateTime(dtp_hora_entrada.Text);
                DateTime hora_s = Convert.ToDateTime(dtp_hora_salida.Text);
                horas_descanso = 0;
                if (cb_descanso.Checked == true)
                {
                    DateTime salida_eat = Convert.ToDateTime(dtp_hora_salida_desc.Text);
                    DateTime regreso_eat = Convert.ToDateTime(dtp_hora_entrada_desc.Text);
                    TimeSpan salida_comer = new TimeSpan(salida_eat.Hour, salida_eat.Minute, salida_eat.Second);
                    TimeSpan regreso_comer = new TimeSpan(regreso_eat.Hour, regreso_eat.Minute, regreso_eat.Second);
                    horas_descanso = regreso_comer.TotalMinutes - salida_comer.TotalMinutes;

                }
                TimeSpan entrada = new TimeSpan(hora_e.Hour, hora_e.Minute, hora_e.Second);
                TimeSpan salida = new TimeSpan(hora_s.Hour, hora_s.Minute, hora_s.Second);
                horas_diarias = salida.TotalMinutes - entrada.TotalMinutes;
                txt_horas_diarias.Value = Math.Truncate((Convert.ToDecimal(horas_diarias) - Convert.ToDecimal(horas_descanso)) / 60);

            }
            catch (Exception ex)
            {
                int code = ex.HResult;
                if (code == -2146233086)
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = ("Las horas trabajadas no pueden ser iguales o menores a 0. Verifique las horas ingresadas anteriormente y seleccione un horario válido.");
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
                else
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = (code + " " + ex.Message.ToString());
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
            }
        }

        private void dtp_hora_salida_desc_ValueChanged(object sender, EventArgs e)
        {
            //CALCULAR HORAS AUTOMATICAMENTE DEPENDIENDO DE LAS HORAS ESTABLECIDAD DE ENTRADA-SALIDA Y DESCANSOS
            try
            {
                DateTime hora_e = Convert.ToDateTime(dtp_hora_entrada.Text);
                DateTime hora_s = Convert.ToDateTime(dtp_hora_salida.Text);
                DateTime salida_eat = Convert.ToDateTime(dtp_hora_salida_desc.Text);
                DateTime regreso_eat = Convert.ToDateTime(dtp_hora_entrada_desc.Text);

                TimeSpan entrada = new TimeSpan(hora_e.Hour, hora_e.Minute, hora_e.Second);
                TimeSpan salida = new TimeSpan(hora_s.Hour, hora_s.Minute, hora_s.Second);
                horas_diarias = salida.TotalMinutes - entrada.TotalMinutes;

                TimeSpan salida_comer = new TimeSpan(salida_eat.Hour, salida_eat.Minute, salida_eat.Second);
                TimeSpan regreso_comer = new TimeSpan(regreso_eat.Hour, regreso_eat.Minute, regreso_eat.Second);
                horas_descanso = regreso_comer.TotalMinutes - salida_comer.TotalMinutes;

                txt_horas_diarias.Value = Math.Truncate((Convert.ToDecimal(horas_diarias) - Convert.ToDecimal(horas_descanso)) / 60);

            }
            catch (Exception ex)
            {
                int code = ex.HResult;
                if (code == -2146233086)
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = ("Las horas trabajadas no pueden ser iguales o menores a 0. Verifique las horas ingresadas anteriormente y seleccione un horario válido.");
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
                else
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = (code + " " + ex.Message.ToString());
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
            }
        }

        private void dtp_hora_entrada_desc_ValueChanged(object sender, EventArgs e)
        {
            //CALCULAR HORAS AUTOMATICAMENTE DEPENDIENDO DE LAS HORAS ESTABLECIDAD DE ENTRADA-SALIDA Y DESCANSOS
            try
            {
                DateTime hora_e = Convert.ToDateTime(dtp_hora_entrada.Text);
                DateTime hora_s = Convert.ToDateTime(dtp_hora_salida.Text);
                DateTime salida_eat = Convert.ToDateTime(dtp_hora_salida_desc.Text);
                DateTime regreso_eat = Convert.ToDateTime(dtp_hora_entrada_desc.Text);

                TimeSpan entrada = new TimeSpan(hora_e.Hour, hora_e.Minute, hora_e.Second);
                TimeSpan salida = new TimeSpan(hora_s.Hour, hora_s.Minute, hora_s.Second);
                horas_diarias = salida.TotalMinutes - entrada.TotalMinutes;

                TimeSpan salida_comer = new TimeSpan(salida_eat.Hour, salida_eat.Minute, salida_eat.Second);
                TimeSpan regreso_comer = new TimeSpan(regreso_eat.Hour, regreso_eat.Minute, regreso_eat.Second);
                horas_descanso = regreso_comer.TotalMinutes - salida_comer.TotalMinutes;

                txt_horas_diarias.Value = Math.Truncate((Convert.ToDecimal(horas_diarias) - Convert.ToDecimal(horas_descanso)) / 60);

            }
            catch (Exception ex)
            {
                int code = ex.HResult;
                if (code == -2146233086)
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = ("Las horas trabajadas no pueden ser iguales o menores a 0. Verifique las horas ingresadas anteriormente y seleccione un horario válido.");
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
                else
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = (code + " " + ex.Message.ToString());
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
            }
        }

        private void txt_horas_diarias_ValueChanged(object sender, EventArgs e)
        {
            //CAMBIAR HORAS QUINCENALES CUANDO SE CAMBIEN LAS HORAS DIARIAS
            try
            {
                int horas_diarias = Convert.ToInt32(txt_horas_diarias.Value);
                txt_horas_totales.Value = horas_diarias * 12;
            }
            catch (Exception ex)
            {
                int code = ex.HResult;
                if (code == -2146233086)
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = ("Las horas trabajadas no pueden ser iguales o menores a 0. Verifique las horas ingresadas anteriormente y seleccione un horario válido.");
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
                else
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = (code + " " + ex.Message.ToString());
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
            }
        }

        private void txt_id_a_modificar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //EJECUTAR MODIFICAR CON ENTER
            if (e.KeyChar == 13)
            {
                btn_ir_modificar.PerformClick();
            }
            validar.solonumeros(e);
        }

        private void txt_idbuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void dtp_hora_entrada_ValueChanged(object sender, EventArgs e)
        {
            //CALCULAR HORAS AUTOMATICAMENTE DEPENDIENDO DE LAS HORAS ESTABLECIDAD DE ENTRADA-SALIDA Y DESCANSOS
            try
            {
                DateTime hora_e = Convert.ToDateTime(dtp_hora_entrada.Text);
                DateTime hora_s = Convert.ToDateTime(dtp_hora_salida.Text);
                horas_descanso = 0;
                if (cb_descanso.Checked == true)
                {
                    DateTime salida_eat = Convert.ToDateTime(dtp_hora_salida_desc.Text);
                    DateTime regreso_eat = Convert.ToDateTime(dtp_hora_entrada_desc.Text);
                    TimeSpan salida_comer = new TimeSpan(salida_eat.Hour, salida_eat.Minute, salida_eat.Second);
                    TimeSpan regreso_comer = new TimeSpan(regreso_eat.Hour, regreso_eat.Minute, regreso_eat.Second);
                    horas_descanso = regreso_comer.TotalMinutes - salida_comer.TotalMinutes;

                }
                TimeSpan entrada = new TimeSpan(hora_e.Hour, hora_e.Minute, hora_e.Second);
                TimeSpan salida = new TimeSpan(hora_s.Hour, hora_s.Minute, hora_s.Second);
                horas_diarias = salida.TotalMinutes - entrada.TotalMinutes;
                txt_horas_diarias.Value = Math.Truncate((Convert.ToDecimal(horas_diarias) - Convert.ToDecimal(horas_descanso)) / 60);

            }
            catch (Exception ex)
            {
                int code = ex.HResult;
                if (code == -2146233086)
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = ("Las horas trabajadas no pueden ser iguales o menores a 0. Verifique las horas ingresadas anteriormente y seleccione un horario válido.");
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
                else
                {
                    frm_error = new formularios_padres.mensaje_error();
                    frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                    frm_error.txt_error.Text = (code + " " + ex.Message.ToString());
                    frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    frm_error.ShowDialog();
                }
            }
        }
    }
}
