﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

//LIBRERIA PARA HILOS
using System.Threading;

namespace Checador.empleados
{
    public partial class empleados : Checador.formularios_padres.formpadre
    {
        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE EMPLEADO
        ClaseEmpleado Empleado = new ClaseEmpleado();
        ClaseHorario horario = new ClaseHorario();
        ClaseDepartamento departamento = new ClaseDepartamento();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();
        formularios_padres.Mensajes confirmacion2 = new formularios_padres.Mensajes();
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.mensaje_error frm_error = new formularios_padres.mensaje_error();
        validacion validar = new validacion();
        //SE CREA LA INSTANCIA DE LA CLASE CHECADOR
        ClaseChecador clase_checador = new ClaseChecador();

        //VARIABLE DE CONEXION DEL CHECADOR
        bool bConn;

        //VARIABLE PARA SABER SI HIZO REGISTRO O MODIFICACION DE LA HUELLA DEL EMPLEADO PARA EL LOG DE HUELLAS
        string tipo_evento;

        //OBJETO DE LA CLASSE CKEM (SDK) PARA PODER ACCEDER A METODOS Y ATRIBUTOS
        public zkemkeeper.CZKEM Checador = new zkemkeeper.CZKEM();
        public int sucurzal, verificador=0, contador;
        public string valor_datagrid;
        public bool respuesta = false;
        public int id_checador_viejo;

        public empleados()
        {
            InitializeComponent();
        }

        private void empleados_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.horarios' Puede moverla o quitarla según sea necesario.
                this.horariosTableAdapter.Fill(this.dataSet_Checador.horarios);
                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_Empleados' Puede moverla o quitarla según sea necesario.
                this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador1.sucursal' Puede moverla o quitarla según sea necesario.
                this.sucursalTableAdapter.Fill(this.dataSet_Checador1.sucursal);
                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_Departamento' Puede moverla o quitarla según sea necesario.
                this.vista_DepartamentoTableAdapter.Fill(this.dataSet_Checador.Vista_Departamento);

                //FILTRAR POR SUCURSALES ACTIVAS EL COMBOBOX
                sucursalBindingSource.Filter = "estatus='A'";

                //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
                CheckForIllegalCrossThreadCalls = false;
                //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
                Thread hilo_secundario = new Thread(new ThreadStart(this.cargarID));
                hilo_secundario.IsBackground = true;
                hilo_secundario.Start();
                groupBox4.Visible = false;
                groupBox4.Enabled = false;
                cbx_privilegio.SelectedIndex = 0;
                btn_registrar_dep.Visible = true;
                btn_actualizar_dep.Visible = false;

                //*************   HACER FORMULARIO RESPONSIVO   *****************
                double porcentaje_ancho = (Convert.ToDouble(Width) / 1362);
                double porcentaje_alto = (Convert.ToDouble(Height) / 741);

                if (Program.rol == "SUPERVISOR DE PERSONAL")
                {
                    rb_registrar.Enabled = false;
                    rb_modificar.Enabled = false;
                    rb_departamento.Enabled = false;
                    rb_buscar.Checked = true;
                }
                else if (Program.rol == "ENCARGADA DE TIENDA")
                {
                    rb_registrar.Enabled = false;
                    rb_modificar.Enabled = false;
                    rb_departamento.Enabled = false;
                    rb_buscar.Checked = true;
                    btn_dar_baja.Visible = false;
                    vistaEmpleadosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and estatus = 'A'";
                }
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
                                            if (w is TextBox | w is Label | w is Button | w is MaskedTextBox | w is DateTimePicker | w is ComboBox | w is RadioButton | w is PictureBox | w is GroupBox)
                                            {
                                                if (w is Label & porcentaje_ancho <= 0.8)
                                                {
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

                                        if ((z is Label & porcentaje_ancho <= 0.8) | (z is Button & porcentaje_ancho <= 0.8) | (z is ComboBox & porcentaje_ancho <= 0.8))
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

        public void cargarID()
        {
            //MOSTRAR EL ID DEL EMPLEADO AL CARGAR LA PAGINA
            try
            {
                txt_id.Text = (Empleado.obtenerIdMaximo() + 1).ToString();
                //CONDICION PARA INVOCAR EL DATAGRID DESDE OTRO HILO
                if (InvokeRequired)
                {
                    Invoke(new Action(() => txt_id.Focus()));
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
                txt_id.Text = "1";
            }
        }



        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {
            Limpiar();
            tabControlBase.SelectedTab = tabPage1;
            txt_id.Focus();
            groupBox4.Visible = false;
            groupBox4.Enabled = false;
            btn_modificar.Enabled = false;
            btn_modificar.Visible = false;
            btn_registrar.Visible = true;
            btn_registrar.Enabled = true;
        }

        private void btn_home_Click(object sender, EventArgs e)
        {     
            this.Close();
        }

        private void rb_vertodos_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage4;
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage5;
        }

        private void btn_siguiente_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage2;
        }


        private void btn_atras_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage1;
            txt_curp.Focus();
        }

        private void btn_atras2_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage2;
        }
        //***************************** REGISTRO EMPLEADOS EN BD Y CHECADOR *************************************
        //CLICK AL BOTON REGISTRAR
        //FUNCION PARA REGITAR SUCURSAL EN LA BASE DE DATOS
        private void btn_registrar_Click(object sender, EventArgs e)
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                Empleado.id = Convert.ToInt32(txt_id.Text);
                Empleado.apellido_mat = txt_apellido_materno.Text.ToUpper();
                Empleado.apellido_pat = txt_apellido_paterno.Text.ToUpper();
                Empleado.banco = txt_banco.Text.ToUpper();
                Empleado.calle = txt_domicilio_calle.Text.ToUpper();
                Empleado.clave_edenred = txt_edenred.Text.ToUpper();
                Empleado.codigo_postal = txt_domicilio_cp.Text.ToUpper();
                Empleado.colonia = txt_domicilio_colonia.Text.ToUpper();
                Empleado.cuenta_bancaria = txt_cuenta.Text.ToUpper();
                Empleado.CURP = txt_curp.Text.ToUpper();
                Empleado.departamento = cbx_departamento.SelectedValue.ToString();
                Empleado.dias_aguinaldo = Convert.ToInt32(txt_dias_aguinaldo.Text);
                Empleado.dias_vacaciones = Convert.ToInt32(txt_dias_vacaciones.Text);
                Empleado.email = txt_email.Text;
                Empleado.estado = txt_domicilio_estado.Text.ToUpper();
                Empleado.estatus = "A";
                //Empleado.fecha_alta = Convert.ToDateTime(dtp_fec_alt.Value.Year.ToString() + "-" + dtp_fec_alt.Value.Month.ToString() + "-" + dtp_fec_alt.Value.Day.ToString());
                Empleado.fecha_alta = Convert.ToDateTime(dtp_fec_alt.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                if (cbx_privilegio.Text == "Usuario")
                {
                    Empleado.id_privilegio = 0;
                }
                else
                {
                    Empleado.id_privilegio = 3;
                }

                Empleado.id_sucursal = Convert.ToInt32(cbx_sucursal.SelectedValue.ToString());
                Empleado.id_horario = 0;
                Empleado.municipio = txt_domicilio_municipio.Text.ToUpper();
                Empleado.nombre = txt_nombre.Text.ToUpper();
                Empleado.NSS = txt_nss.Text;
                Empleado.num_ext = txt_domicilio_num_ext.Text;
                Empleado.num_int = txt_domicilio_num_int.Text;
                Empleado.observaciones = txt_observaciones.Text.ToUpper();
                Empleado.pais = txt_domicilio_pais.Text.ToUpper();
                Empleado.periodicidad_pago = txt_periodicidad_pago.Text.ToUpper();
                Empleado.poblacion = txt_domicilio_pob.Text.ToUpper();
                Empleado.puesto = txt_puesto.Text.ToUpper();
                Empleado.RFC = txt_rfc.Text.ToUpper();
                Empleado.riesgo_puesto = txt_riesgo_puesto.Text.ToUpper();
                Empleado.sueldo_base_quincenal = Convert.ToDecimal(txt_sueldo_quincenal.Text);
                Empleado.sueldo_diario = Convert.ToDecimal(txt_sueldo_diario.Text);
                Empleado.sueldo_diario_integrado = Convert.ToDecimal(txt_sueldo_integrado.Text);
                Empleado.tarjeta_despensa = txt_despensa.Text.ToUpper();
                Empleado.telefono = txt_telefono.Text;
                Empleado.tipo_contrato = txt_tipo_contrato.Text.ToUpper();
                //Empleado.tipo_horario = cbx_horario.SelectedValue.ToString();

                Empleado.tipo_salario = txt_tipo_salario.Text.ToUpper();
                Empleado.password = txt_contra.Text;
                if (Empleado.id.ToString() == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el numero de empleado.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    tabControlBase.SelectedTab = tabPage1;
                    txt_id.Focus();
                }
                else if (Empleado.nombre == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el nombre del empleado.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    tabControlBase.SelectedTab = tabPage1;
                    txt_nombre.Focus();
                }
                else if (Empleado.apellido_pat == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado primer apellido.";
                    mensaje.lbl_info2.Text = "del empleado.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    tabControlBase.SelectedTab = tabPage1;
                    txt_apellido_paterno.Focus();
                }
                else if (Empleado.id_sucursal.ToString() == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha seleccionado una sucursal.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    tabControlBase.SelectedTab = tabPage1;
                    cbx_sucursal.Focus();
                }
                else
                {
                    Empleado.guardarEmpleado();
                    Empleado.guardarEmpleado_Sucursal();
                    //SE OBTIENEN LOS DATOS DEL CHECADOR
                    clase_checador.getChecador_Sucursal(Empleado.id_sucursal);
                    Conectar_Checador();

                    if (bConn)
                    {
                        Crear_Usuario_Checador(clase_checador.id, Convert.ToString(Empleado.id), Empleado.nombre, Empleado.password, Empleado.id_privilegio, 0);
                    }
                    confirmacion2 = new formularios_padres.Mensajes();
                    //FUNCION PAR RECARGAR EL DATAGRID
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);

                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    confirmacion2 = new formularios_padres.Mensajes();
                    confirmacion2.lbl_mensaje.Text = "Desea registrar huella al empleado?";
                    confirmacion2.FormClosed += new FormClosedEventHandler(reg_huella);
                    confirmacion2.ShowDialog();
                }
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

        //FUNCION PARA REGISTRAR NUEVO USUARIO EN EL CHECADOR
        public void Crear_Usuario_Checador(int id_checador, string id_empleado, string nombre, string contra, int privilegio, int id_checador_viejo)
        {
            try
            {
                int error = 0;
                int dedo;
                int tFlag = 0, tTemplateLength = 0;
                string huella = string.Empty;

                //VALIDACION PARA SABER SI ESTA REGISTANDO O MODIFICANDO
                if (id_checador_viejo != 0)
                {
                    for (dedo = 0; dedo < 10; dedo++)
                    {
                        clase_checador.getChecador_Sucursal(sucurzal);
                        Conectar_Checador();
                        //OBTENER HUELLA
                        if (Checador.GetUserTmpExStr(id_checador_viejo, id_empleado.ToString(), dedo, out tFlag, out huella, out tTemplateLength))
                        {
                            clase_checador.getChecador_Sucursal(Empleado.id_sucursal);
                            Conectar_Checador();

                            //CREAR USUARIO EN EL NUEVO CHECADOR
                            if (Checador.SSR_SetUserInfo(id_checador, id_empleado, nombre, contra, privilegio, true))
                            {
                                //SETEAR LA HUELLA
                                Checador.SetUserTmpExStr(id_checador, id_empleado, dedo, tFlag, huella);
                            }
                            else
                            {
                                Checador.GetLastError(ref error);
                                MessageBox.Show(error.ToString());
                            }
                        }
                    }
                }
                else
                {
                    //CREAR USUARIO EN EL NUEVO CHECADOR
                    if (Checador.SSR_SetUserInfo(id_checador, id_empleado, nombre, contra, privilegio, true))
                    {
                    }
                    else
                    {
                        Checador.GetLastError(ref error);
                        MessageBox.Show(error.ToString());
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

        //ESTO ESTA MODIFICADO PARA MANDARLO A LA LAP DE MIRSA!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public void Conectar_Checador()
        {
            try
            {
                //Codigo para hacer el ping
                Ping HacerPing = new Ping();
                int iTiempoEspera = 5000;
                PingReply RespuestaPing;
                string sDireccion = clase_checador.ip;
                RespuestaPing = HacerPing.Send(sDireccion, iTiempoEspera);
                if (RespuestaPing.Status == IPStatus.Success)
                {
                    clase_checador.ip = RespuestaPing.Address.ToString();
                    //SE CREA UNA VARIABLE CON EL METODO CONECTAR DEL OBJETO CHECADOR.
                    //SE ENVIAN COMO PARAMETROS LA IP DEL CHECADOR Y EL PUERTO
                    bConn = Checador.Connect_Net(clase_checador.ip, Convert.ToInt32(clase_checador.puerto));

                    if (bConn == true)
                    {
                        //SE ACTIVA EL DISPOSITIVO. PARAMETRO EL NUM. DE MAQUINA Y UNA BANDERA
                        Checador.EnableDevice(clase_checador.id, true);
                    }
                    else
                    {
                        //ATENCION CAMBIAR ESTE MENSAJE A LA CONSOLA PARA MAYOR COMODIDAD
                        mensaje = new formularios_padres.mensaje_info();
                        mensaje.lbl_info.Text = "Dispositivo no conectado";
                        mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                        mensaje.Show();
                    }
                }
                else
                {
                    //ATENCION CAMBIAR ESTE MENSAJE A LA CONSOLA PARA MAYOR COMODIDAD
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Dispositivo no conectado";
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

            /*try
            {
                //SE CREA UNA VARIABLE CON EL METODO CONECTAR DEL OBJETO CHECADOR.
                //SE ENVIAN COMO PARAMETROS LA IP DEL CHECADOR Y EL PUERTO
                bConn = Checador.Connect_Net(clase_checador.ip, Convert.ToInt32(clase_checador.puerto));

                if (bConn == true)
                {
                    //SE ACTIVA EL DISPOSITIVO. PARAMETRO EL NUM. DE MAQUINA Y UNA BANDERA
                    Checador.EnableDevice(clase_checador.id, true);
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
            }*/
        }
        ///********************************************************************************************

        //************************************************************************************************************
        //FUNCION PARA LIMPIAR LOS COMPONENTES DEL FORMULARIO DESPUES DE HACER UN REGISTRO
        private void Limpiar()
        {
            txt_apellido_materno.Text = "";
            txt_apellido_paterno.Text = "";
            txt_banco.Text = "";
            txt_contra.Text = "";
            txt_cuenta.Text = "";
            txt_curp.Text = "";
            txt_despensa.Text = "";
            txt_dias_aguinaldo.Text = "0";
            txt_dias_vacaciones.Text = "0";
            txt_domicilio_calle.Text = "";
            txt_domicilio_colonia.Text = "";
            txt_domicilio_cp.Text = "";
            txt_domicilio_estado.Text = "";
            txt_domicilio_municipio.Text = "";
            txt_domicilio_num_ext.Text = "";
            txt_domicilio_num_int.Text = "";
            txt_domicilio_pais.Text = "";
            txt_domicilio_pob.Text = "";
            txt_edenred.Text = "";
            txt_email.Text = "";
            txt_nombre.Text = "";
            txt_nss.Text = "";
            txt_observaciones.Text = "";
            txt_periodicidad_pago.Text = "";
            txt_puesto.Text = "";
            txt_rfc.Text = "";
            txt_riesgo_puesto.Text = "";
            txt_sueldo_diario.Text = "0";
            txt_sueldo_integrado.Text = "0";
            txt_sueldo_quincenal.Text = "0";
            txt_telefono.Text = "";
            txt_tipo_contrato.Text = "";
            txt_tipo_salario.Text = "";
            cargarID();
            txt_id.Focus();
            //Deshabilitar_Componentes();
        }

        //FUNCION PARA DESHABILITAR LOS COMPONENTES DEL FORMULARIO
        public void Deshabilitar_Componentes()
        {
            txt_apellido_materno.Enabled = false;
            txt_apellido_paterno.Enabled = false;
            txt_nombre.Enabled = false;
            cbx_sucursal.Enabled = false;
            dtp_fec_alt.Enabled = false;
            txt_contra.Enabled = false;
            cbx_privilegio.Enabled = false;
            btn_siguiente.Enabled = false;
        }

        //FUNCION PARA HABILITAR LOS COMPONENTES DEL FORMULARIO
        public void Habilitar_Componentes()
        {
            txt_apellido_materno.Enabled = true;
            txt_apellido_paterno.Enabled = true;
            txt_nombre.Enabled = true;
            cbx_sucursal.Enabled = true;
            dtp_fec_alt.Enabled = true;
            txt_contra.Enabled = true;
            cbx_privilegio.Enabled = true;
            btn_siguiente.Enabled = true;
        }
        //CARGA EL DEDO SELECCIONADO PARA REGISTRARLO EN EL CHECADOR
        private void cbx_huella_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbx_huella.Text == "1 (anular izquierdo)")
                {
                    pictureBox1.Image = Image.FromFile("..\\..\\Resources\\huella1.png");
                }
                else if (cbx_huella.Text == "0 (meñique izquierdo)")
                {
                    pictureBox1.Image = Image.FromFile("..\\..\\Resources\\huella0.png");
                }
                else if (cbx_huella.Text == "2 (medio izquierdo)")
                {
                    pictureBox1.Image = Image.FromFile("..\\..\\Resources\\huella2.png");
                }
                else if (cbx_huella.Text == "3 (indice izquierdo)")
                {
                    pictureBox1.Image = Image.FromFile("..\\..\\Resources\\huella3.png");
                }
                else if (cbx_huella.Text == "4 (pulgar izquierdo)")
                {
                    pictureBox1.Image = Image.FromFile("..\\..\\Resources\\huella4.png");
                }
                else if (cbx_huella.Text == "5 (pulgar derecho)")
                {
                    pictureBox1.Image = Image.FromFile("..\\..\\Resources\\huella5.png");
                }
                else if (cbx_huella.Text == "6 (indice  derecho)")
                {
                    pictureBox1.Image = Image.FromFile("..\\..\\Resources\\huella6.png");
                }
                else if (cbx_huella.Text == "7 (medio derecho)")
                {
                    pictureBox1.Image = Image.FromFile("..\\..\\Resources\\huella7.png");
                }
                else if (cbx_huella.Text == "8 (anular derecho)")
                {
                    pictureBox1.Image = Image.FromFile("..\\..\\Resources\\huella8.png");
                }
                else if (cbx_huella.Text == "9 (meñique derecho)")
                {
                    pictureBox1.Image = Image.FromFile("..\\..\\Resources\\huella9.png");
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

        private void Desbloquear_empleados(object sender, EventArgs e)
        {
            Enabled = true;
        }

        private void btn_capturar_mod_Click(object sender, EventArgs e)
        {
           
        }

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
            tabControlBase.SelectedTab = tabPage4;
        }

        private void rb_modificar_CheckedChanged_1(object sender, EventArgs e)
        {
            txt_id_a_modificar.Text = "";
            tabControlBase.SelectedTab = tabPage5;
            groupBox4.Visible = true;
            groupBox4.Enabled = true;

        }

        //*************************** MODIFICAR EMPLEADOS ***********************************************
        private void btn_modificar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txt_id_a_modificar.Text != "")
                {
                    txt_id.Enabled = false;
                    Empleado.id = Convert.ToInt32(txt_id_a_modificar.Text);
                    if (Empleado.verificar_existencia(Empleado.id))
                    {
                        if (Program.rol == "ENCARGADA DE TIENDA")
                        {
                            tabControlBase.SelectedTab = tabPage3;
                            cbx_huella.SelectedIndex = 6;
                            tipo_evento = "Modificacion";
                            //SE OBTIENEN LOS DATOS DEL CHECADOR
                            clase_checador.getChecador_Sucursal(Empleado.id_sucursal);
                            Conectar_Checador();
                        }
                        else
                        {
                            tabControlBase.SelectedTab = tabPage1;
                            txt_curp.Focus();
                            btn_modificar.Enabled = true;
                            btn_modificar.Visible = true;
                            btn_registrar.Visible = false;
                            btn_registrar.Enabled = false;
                            txt_id.Text = Empleado.id.ToString();
                            txt_nombre.Text = Empleado.nombre;
                            txt_apellido_materno.Text = Empleado.apellido_mat;
                            txt_apellido_paterno.Text = Empleado.apellido_pat;
                            txt_banco.Text = Empleado.banco;
                            txt_contra.Text = Empleado.password;
                            txt_cuenta.Text = Empleado.cuenta_bancaria;
                            txt_curp.Text = Empleado.CURP;
                            txt_despensa.Text = Empleado.tarjeta_despensa;
                            txt_dias_aguinaldo.Text = Empleado.dias_aguinaldo.ToString();
                            txt_dias_vacaciones.Text = Empleado.dias_vacaciones.ToString();
                            txt_domicilio_calle.Text = Empleado.calle;
                            txt_domicilio_colonia.Text = Empleado.colonia;
                            txt_domicilio_cp.Text = Empleado.codigo_postal;
                            txt_domicilio_estado.Text = Empleado.estado;
                            txt_domicilio_municipio.Text = Empleado.municipio;
                            txt_domicilio_num_ext.Text = Empleado.num_ext;
                            txt_domicilio_num_int.Text = Empleado.num_int;
                            txt_domicilio_municipio.Text = Empleado.municipio;
                            txt_domicilio_pais.Text = Empleado.pais;
                            txt_domicilio_pob.Text = Empleado.poblacion;
                            txt_edenred.Text = Empleado.clave_edenred;
                            txt_email.Text = Empleado.email;
                            txt_nombre.Text = Empleado.nombre;
                            txt_nss.Text = Empleado.NSS;
                            txt_observaciones.Text = Empleado.observaciones;
                            txt_periodicidad_pago.Text = Empleado.periodicidad_pago;
                            txt_puesto.Text = Empleado.puesto;
                            txt_rfc.Text = Empleado.RFC;
                            txt_riesgo_puesto.Text = Empleado.riesgo_puesto;
                            txt_sueldo_diario.Text = Empleado.sueldo_diario.ToString();
                            txt_sueldo_integrado.Text = Empleado.sueldo_diario_integrado.ToString();
                            txt_sueldo_quincenal.Text = Empleado.sueldo_base_quincenal.ToString();
                            txt_telefono.Text = Empleado.telefono.ToString();
                            txt_tipo_contrato.Text = Empleado.tipo_contrato;
                            txt_tipo_salario.Text = Empleado.tipo_salario;
                            dtp_fec_alt.Text = Empleado.fecha_alta.ToString();
                            cbx_sucursal.SelectedValue = Empleado.id_sucursal;
                            cbx_departamento.SelectedValue = Empleado.departamento;
                            sucurzal = Empleado.id_sucursal;

                            if (Empleado.id_privilegio == 0)
                            {
                                cbx_privilegio.SelectedIndex = 0;
                            }
                            else if (Empleado.id_privilegio == 3)
                            {
                                cbx_privilegio.SelectedIndex = 1;
                            }

                            if (Empleado.estatus == "A")
                            {
                                rb_mod_activo.Checked = true;
                            }
                            else
                            {
                                rb_mod_inactivo.Checked = true;
                            }
                        }
                    }
                    else
                    {
                        mensaje = new formularios_padres.mensaje_info();
                        mensaje.lbl_info.Text = "Empleado no registrado.";
                        mensaje.lbl_info2.Text = "Intente de nuevo.";
                        mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                        mensaje.ShowDialog();
                    }
                }
                else
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el identificador.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
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


        private void Checador_OnEnrollFinger(int EnrollNumber, int FingerIndex, int ActionResult, int TemplateLenght)
        {
            try
            {
                verificador = verificador + 1;
                if (verificador == 1)
                {
                    pic_huella_mod.Image = Image.FromFile("..\\..\\Resources\\huellaregistred.png");
                    Empleado.registrarLogHuella(Program.id_empleado, DateTime.Now, tipo_evento,Empleado.id);
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

        
        //****************** NECESARIOS PARA MOSTRAR MENSAJES *****************
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
                    Empleado.Modificar_Empleado();
                    //FUNCION PAR RECARGAR EL DATAGRID
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);

                    Conectar_Checador();
                    if (bConn)
                    {
                        //Empleado.Modificar_Empleado();

                        if (sucurzal != Convert.ToInt32(cbx_sucursal.SelectedValue.ToString()))
                        {
                            Crear_Usuario_Checador(clase_checador.id, Convert.ToString(Empleado.id), Empleado.nombre, Empleado.password, Empleado.id_privilegio, id_checador_viejo);
                            Empleado.guardarEmpleado_Sucursal();
                            //SE OBTIENEN LOS DATOS DEL CHECADOR
                            clase_checador.getChecador_Sucursal(Empleado.id_sucursal);

                        }
                    }
                    else
                    {
                        //CAMBIAR EL CURSOR
                        this.UseWaitCursor = false;
                        mensaje = new formularios_padres.mensaje_info();
                        mensaje.lbl_info.Text = "Checador no conectado, marquele a Mirsa.";
                        mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                        mensaje.ShowDialog();
                    }

                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    confirmacion2 = new formularios_padres.Mensajes();
                    confirmacion2.lbl_mensaje.Text = "Desea cambiar huella al empleado?";
                    confirmacion2.FormClosed += new FormClosedEventHandler(mod_huella);
                    confirmacion2.Show();
                    Enabled = false;

                }
                txt_id.Enabled = true;
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

        private void reg_huella(object sender, EventArgs e)
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                Enabled = true;
                respuesta = confirmacion2.respuesta;
                if (respuesta == true)
                {
                    tabControlBase.SelectedTab = tabPage3;
                    cbx_huella.SelectedIndex = 6;
                    tipo_evento = "Registro";
                }
                else
                {
                    tabControlBase.SelectedTab = tabPage1;
                    txt_id.Focus();
                    Limpiar();
                }
                confirmacion2 = null;
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

        private void mod_huella(object sender, EventArgs e)
        {
            //**************

            //****************
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                Enabled = true;
                respuesta = confirmacion2.respuesta;
                if (respuesta == true)
                {
                    tabControlBase.SelectedTab = tabPage3;
                    cbx_huella.SelectedIndex = 6;
                    tipo_evento = "Modificacion";
                }
                else
                {
                    tabControlBase.SelectedTab = tabPage5;
                    txt_id_a_modificar.Clear();
                }
                confirmacion2 = null;
                //CAMBIAR EL CURSOR
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

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;
            Enabled = true;
        }
        //**********************************************************************
        //FUNCION PARA ACTUALIZAR LOS DATOS DE UN EMPLEADO
        private void btn_modificar_Click_3(object sender, EventArgs e)
        {
            Enabled = false;

            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                Empleado.apellido_mat = txt_apellido_materno.Text;
                Empleado.apellido_pat = txt_apellido_paterno.Text;
                Empleado.banco = txt_banco.Text;
                Empleado.calle = txt_domicilio_calle.Text;
                Empleado.clave_edenred = txt_edenred.Text;
                Empleado.codigo_postal = txt_domicilio_cp.Text;
                Empleado.colonia = txt_domicilio_colonia.Text;
                Empleado.cuenta_bancaria = txt_cuenta.Text;
                Empleado.CURP = txt_curp.Text;
                Empleado.departamento = cbx_departamento.SelectedValue.ToString();
                Empleado.dias_aguinaldo = Convert.ToInt32(txt_dias_aguinaldo.Text);
                Empleado.dias_vacaciones = Convert.ToInt32(txt_dias_vacaciones.Text);
                Empleado.email = txt_email.Text;
                Empleado.estado = txt_domicilio_estado.Text;
                if (rb_mod_activo.Checked == true)
                {
                    Empleado.estatus = "A";
                }
                else
                {
                    Empleado.estatus = "I";
                }
                //Empleado.fecha_alta = Convert.ToDateTime(dtp_fec_alt.Value.Year.ToString() + "-" + dtp_fec_alt.Value.Month.ToString() + "-" + dtp_fec_alt.Value.Day.ToString());
                Empleado.fecha_alta = Convert.ToDateTime(dtp_fec_alt.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                if (cbx_privilegio.Text == "Usuario")
                {
                    Empleado.id_privilegio = 0;
                }
                else
                {
                    Empleado.id_privilegio = 3;
                }
                Empleado.id_sucursal = Convert.ToInt32(cbx_sucursal.SelectedValue.ToString());
                Empleado.id_horario = 0;
                Empleado.municipio = txt_domicilio_municipio.Text;
                Empleado.nombre = txt_nombre.Text;
                Empleado.NSS = txt_nss.Text;
                Empleado.num_ext = txt_domicilio_num_ext.Text;
                Empleado.num_int = txt_domicilio_num_int.Text;
                Empleado.observaciones = txt_observaciones.Text;
                Empleado.pais = txt_domicilio_pais.Text;
                Empleado.periodicidad_pago = txt_periodicidad_pago.Text;
                Empleado.poblacion = txt_domicilio_pob.Text;
                Empleado.puesto = txt_puesto.Text;
                Empleado.RFC = txt_rfc.Text;
                Empleado.riesgo_puesto = txt_riesgo_puesto.Text;
                Empleado.sueldo_base_quincenal = Convert.ToDecimal(txt_sueldo_quincenal.Text);
                Empleado.sueldo_diario = Convert.ToDecimal(txt_sueldo_diario.Text);
                Empleado.sueldo_diario_integrado = Convert.ToDecimal(txt_sueldo_integrado.Text);
                Empleado.tarjeta_despensa = txt_despensa.Text;
                Empleado.telefono = txt_telefono.Text;
                Empleado.tipo_contrato = txt_tipo_contrato.Text;
                //Empleado.tipo_horario = cbx_horario.SelectedValue.ToString();
               
                Empleado.tipo_salario = txt_tipo_salario.Text;
                Empleado.password = txt_contra.Text;

                //SE OBTIENEN LOS DATOS DEL CHECADOR VIEJO EN CASO DE SER NECESARIO POR CAMBIO DE SUCURSAL
                clase_checador.getChecador_Sucursal(sucurzal);
                id_checador_viejo = clase_checador.id;

                //SE OBTIENEN LOS DATOS DEL CHECADOR
                clase_checador.getChecador_Sucursal(Empleado.id_sucursal);
                Conectar_Checador();

                if (bConn)
                {
                    Modificar_Usuario_Checador(clase_checador.id, Convert.ToString(Empleado.id), Empleado.nombre, Empleado.password, Empleado.id_privilegio);
                }

                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                confirmacion = new formularios_padres.Mensajes();
                confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea actualizar el empleado?";
                confirmacion.FormClosed += new FormClosedEventHandler(responder);
                confirmacion.ShowDialog();

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

        //FUNCION PARA MODIFICAR USUARIO EN EL CHECADOR
        public void Modificar_Usuario_Checador(int id_checador, string id_empleado, string nombre, string contra, int privilegio)
        {
            try
            {
                int error = 0;

                if (Checador.SSR_SetUserInfo(id_checador, id_empleado, nombre, contra, privilegio, true))
                {

                }
                else
                {
                    Checador.GetLastError(ref error);
                    MessageBox.Show(error.ToString());
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

        private void btn_capturar_Click(object sender, EventArgs e)
        {
            try
            {
                int dedo = cbx_huella.SelectedIndex;
                //CODIGO PARA LA INTERFAZ DE REGISTRO DE NUEVA HUELLA
                int flag = 0;
                Checador.StartEnrollEx(Empleado.id.ToString(), dedo, flag);
                if (Checador.RegEvent(clase_checador.id, 65535))
                {
                    Checador.OnEnrollFinger += new zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(Checador_OnEnrollFinger);
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

        private void btn_guardar_huella_Click(object sender, EventArgs e)
        {
            try
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "La huella ha sido guardada";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
                Limpiar();
                tabControlBase.SelectedTab = tabPage4;
                btn_registrar.Visible = true;
                btn_modificar.Visible = false;
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
        private void txt_idbuscar_TextChanged(object sender, EventArgs e)
        {
            if (Program.rol != "ENCARGADA DE TIENDA")
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                    vistaEmpleadosBindingSource.Filter = "";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "CONVERT([id_empleado], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre_completo] LIKE '*" + txt_nombrebuscar.Text + "*'";
                }
            }
            else
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                    vistaEmpleadosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and estatus = 'A'";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "CONVERT([id_empleado], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre_completo] LIKE '*" + txt_nombrebuscar.Text + "*' and sucursal ='" + Program.sucursal + "' and estatus = 'A'";
                }
            }
        }

        private void txt_nombrebuscar_TextChanged(object sender, EventArgs e)
        {
            if (Program.rol != "ENCARGADA DE TIENDA")
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                    vistaEmpleadosBindingSource.Filter = "";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "CONVERT([id_empleado], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre_completo] LIKE '*" + txt_nombrebuscar.Text + "*'";
                }
            }
            else
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                    vistaEmpleadosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and estatus = 'A'";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "CONVERT([id_empleado], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre_completo] LIKE '*" + txt_nombrebuscar.Text + "*' and sucursal ='" + Program.sucursal + "' and estatus = 'A'";
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btn_b_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                var row = dgv_empleadobuscar.CurrentRow;
                Empleado.id = Convert.ToInt32(row.Cells[0].Value);
                rb_modificar.Checked = true;
                txt_id_a_modificar.Text = Convert.ToString(Empleado.id);
                btn_ir_modificar.PerformClick();
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

        private void panel_barra_sup_Paint(object sender, PaintEventArgs e)
        {

        }
        //******************************   VALIDACION DE LOS CAMPOS    *******************************************

        private void txt_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void txt_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.sololetras(e);
        }

        private void txt_apellido_paterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.sololetras(e);
        }

        private void txt_apellido_materno_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.sololetras(e);
        }

        private void txt_domicilio_num_ext_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void txt_domicilio_num_int_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void txt_domicilio_cp_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void txt_nss_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void txt_email_Leave(object sender, EventArgs e)
        {
            if (validar.validaremail(txt_email.Text))
            {

            }
            else
            {
                txt_email.SelectAll();
                if (string.IsNullOrEmpty(txt_email.Text))
                {

                    errorProvider1.SetError(txt_email, "El correo electronico proporcionado no tiene el formato correcto.");

                }
                else
                {
                    errorProvider1.SetError(txt_email, null);
                    contador = contador + 1;
                }
                txt_email.Focus();
            }
        }

        private void txt_telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void txt_sueldo_diario_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.soloimportes(e);
        }

        private void txt_sueldo_integrado_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.soloimportes(e);
        }

        private void txt_sueldo_quincenal_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.soloimportes(e);
        }

        private void txt_dias_aguinaldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void txt_dias_vacaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void txt_cuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void txt_nombre_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_nombre.Text))
            {

                errorProvider1.SetError(txt_nombre, "No ha ingresado el nombre del empleado.");

            }
            else
            {
                errorProvider1.SetError(txt_nombre, null);
                contador = contador + 1;
            }
        }

        private void txt_apellido_paterno_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_apellido_paterno.Text))
            {

                errorProvider1.SetError(txt_apellido_paterno, "No ha ingresado el apelido paterno del empleado.");

            }
            else
            {
                errorProvider1.SetError(txt_apellido_paterno, null);
                contador = contador + 1;
            }
        }

        private void txt_apellido_materno_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_apellido_materno.Text))
            {

                errorProvider1.SetError(txt_apellido_materno, "No ha ingresado el apelido paterno del empleado.");

            }
            else
            {
                errorProvider1.SetError(txt_apellido_materno, null);
                contador = contador + 1;
            }
        }

        //FUNCION PARA CUANDO DEJE EL CAMPO DE TEXTO ID BUSQUE SI EXISTE EL EMPLEADO
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
                    Empleado.id = Convert.ToInt32(txt_id.Text);
                    if (Empleado.verificar_existencia(Empleado.id))
                    {
                        MessageBox.Show("El ID del empleado " + Empleado.id + " ya existe. Ingrese otro ID");
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

        private void txt_id_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_id.Text))
            {

                errorProvider1.SetError(txt_id, "No ha ingresado el numero de empleado.");

            }
            else
            {
                errorProvider1.SetError(txt_id, null);
                contador = contador + 1;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                confirmacion = new formularios_padres.Mensajes();
                confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea CANCELAR?";
                confirmacion.FormClosed += new FormClosedEventHandler(cancelar);
                confirmacion.ShowDialog();
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

        private void cancelar(object sender, EventArgs e)
        {
            Enabled = true;
            respuesta = confirmacion.respuesta;

            if (respuesta == true)
            {
                tabControlBase.SelectedTab = tabPage1;
                Limpiar();
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

        private void mod_departamento(object sender, EventArgs e)
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                Enabled = true;
                respuesta = confirmacion.respuesta;

                if (respuesta == true)
                {
                    departamento.Modificar_Departamento();
                    //CAMBIAR LOS BOTONES
                    btn_actualizar_dep.Visible = false;
                    btn_registrar_dep.Visible = true;
                    txt_id_dep.Enabled = true;
                    // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_Departamento' Puede moverla o quitarla según sea necesario.
                    this.vista_DepartamentoTableAdapter.Fill(this.dataSet_Checador.Vista_Departamento);
                    Limpiar_datos_departamento();
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

        private void btn_modificar_dep_Click(object sender, EventArgs e)
        {
            //MODIFICAR DEPARTAMENTO
            try
            {
                //CAMBIAR LOS BOTONES
                btn_registrar_dep.Visible = false;
                btn_actualizar_dep.Visible = true;
                txt_id_dep.Enabled = false;

                var row = dgv_departamento.CurrentRow;
                //CARGAR LOS DATOS A LOS TEXTBOX
                departamento.id = Convert.ToString(row.Cells[0].Value);
                departamento.nombre = Convert.ToString(row.Cells[1].Value);
                departamento.locacion = Convert.ToString(row.Cells[2].Value);

                txt_id_dep.Text = departamento.id.ToString();
                txt_nom_dep.Text = departamento.nombre;
                txt_locacion_dep.Text = departamento.locacion;
                txt_nom_dep.Focus();

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

        private void btn_editar_dep_Click(object sender, EventArgs e)
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                departamento.id = txt_id_dep.Text;
                departamento.nombre = txt_nom_dep.Text;
                departamento.locacion = txt_locacion_dep.Text;

                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                confirmacion = new formularios_padres.Mensajes();
                confirmacion.lbl_mensaje.Text = "¿Desea modificar el departamento?";
                confirmacion.FormClosed += new FormClosedEventHandler(mod_departamento);
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

        private void rb_departamento_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage6;
            btn_registrar_dep.Visible = true;
            btn_actualizar_dep.Visible = false;
        }

        //Limpar los campos despues de registrado un departamento
        private void Limpiar_datos_departamento()
        {
            txt_id_dep.Text = "";
            txt_locacion_dep.Text = "";
            txt_nom_dep.Text = "";
            txt_id_dep.Focus();
        }

        private void btn_registrar_dep_Click(object sender, EventArgs e)
        {
            //REGISTRAR DEPARTAMENTO NUEVO
            try
            {
                departamento.id = txt_id_dep.Text;
                departamento.nombre = txt_nom_dep.Text;
                departamento.locacion = txt_locacion_dep.Text;

                if (departamento.id.ToString() == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el ID del departamento";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_id_dep.Focus();
                }
                else if (departamento.nombre == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el nombre del departamento";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_nom_dep.Focus();
                }
                else if (departamento.locacion == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado la locacion del departamento";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_locacion_dep.Focus();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = true;
                    if (!departamento.verificar_departamento(departamento.id))
                    {
                        departamento.guardarDepartamento();
                        //FUNCION PAR RECARGAR EL DATAGRID
                        // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_Departamento' Puede moverla o quitarla según sea necesario.
                        this.vista_DepartamentoTableAdapter.Fill(this.dataSet_Checador.Vista_Departamento);
                    }

                    Limpiar_datos_departamento();
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

        //FUNCION PARA ELIMINAR UN DEPARTAMENTO PERMANENTEMENTE DE LA BD
        private void btn_eliminar_dep_Click(object sender, EventArgs e)
        {
            try
            {
                confirmacion = new formularios_padres.Mensajes();
                confirmacion.lbl_mensaje.Text = "¿Desea eliminar el departamento?";
                confirmacion.FormClosed += new FormClosedEventHandler(eliminar_departamento);
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

        //ACCIONES REFERENTES A LA CONFIRMACION DEL DIALOGO DE ELIMINAR
        public void eliminar_departamento(object sender, EventArgs e)
        {
            try
            {
                Enabled = true;
                respuesta = confirmacion.respuesta;
                if (respuesta)
                {
                    var row = dgv_departamento.CurrentRow;
                    departamento.id = Convert.ToString(row.Cells[0].Value);
                    departamento.eliminarDepartamento();
                    //FUNCION PAR RECARGAR EL DATAGRID
                    // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_Departamento' Puede moverla o quitarla según sea necesario.
                    this.vista_DepartamentoTableAdapter.Fill(this.dataSet_Checador.Vista_Departamento);

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

        //**************************  AQUI TERMINA LA VALIDACION DE LOS CAMPOS    *******************************

        private void btn_dar_baja_Click(object sender, EventArgs e)
        {
            confirmacion = new formularios_padres.Mensajes();
            confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea dar de baja";
            confirmacion.lbl_mensaje2.Text = "al empleado?";
            confirmacion.FormClosed += new FormClosedEventHandler(darbaja);
            confirmacion.ShowDialog();
        }

        private void darbaja(object sender, EventArgs e)
        {
            try
            {
                Enabled = true;
                respuesta = confirmacion.respuesta;

                if (respuesta == true)
                {
                    var row = dgv_empleadobuscar.CurrentRow;
                    Empleado.id = Convert.ToInt32(row.Cells[0].Value);
                    Empleado.estatus = "I";
                    Empleado.Eliminar_Empleado();
                    //FUNCION PAR RECARGAR EL DATAGRID
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
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