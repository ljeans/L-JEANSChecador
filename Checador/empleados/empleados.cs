using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

//LIBRERIA PARA HILOS
using System.Threading;

namespace Checador.empleados
{
    public partial class empleados : Checador.formularios_padres.formpadre
    {
        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE EMPLEADO
        ClaseEmpleado Empleado = new ClaseEmpleado();
        ClaseHorario horario = new ClaseHorario();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();
        formularios_padres.Mensajes confirmacion2 = new formularios_padres.Mensajes();
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();

        //SE CREA LA INSTANCIA DE LA CLASE CHECADOR
        ClaseChecador clase_checador = new ClaseChecador();

        //VARIABLE DE CONEXION DEL CHECADOR
        bool bConn;

        //OBJETO DE LA CLASSE CKEM (SDK) PARA PODER ACCEDER A METODOS Y ATRIBUTOS
        public zkemkeeper.CZKEM Checador = new zkemkeeper.CZKEM();
        public int sucurzal, verificador=0;
        public string valor_datagrid;
        public bool respuesta = false;

        public empleados()
        {
            InitializeComponent();
        }

        private void empleados_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.horarios' Puede moverla o quitarla según sea necesario.
            this.horariosTableAdapter.Fill(this.dataSet_Checador.horarios);
            // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_Empleados' Puede moverla o quitarla según sea necesario.
            this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
            // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador1.sucursal' Puede moverla o quitarla según sea necesario.
            this.sucursalTableAdapter.Fill(this.dataSet_Checador1.sucursal);

           
            //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
            CheckForIllegalCrossThreadCalls = false;
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.cargarID));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
            //cargarID();
            groupBox4.Visible = false;
            groupBox4.Enabled = false;

            cbx_privilegio.SelectedIndex = 0;
        
        }            

        public void cargarID()
        {
            //MOSTRAR EL ID DEL EMPLEADO AL CARGAR LA PAGINA
            try
            {
                txt_id.Text = (Empleado.obtenerIdMaximo() + 1).ToString();
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
            txt_curp.Focus();
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
                Empleado.id = Convert.ToInt32(txt_id.Text);
                Empleado.apellido_mat = txt_apellido_materno.Text;
                Empleado.apellido_pat = txt_apellido_paterno.Text;
                Empleado.banco = txt_banco.Text;
                Empleado.calle = txt_domicilio_calle.Text;
                Empleado.clave_edenred = txt_edenred.Text;
                Empleado.codigo_postal = txt_domicilio_cp.Text;
                Empleado.colonia = txt_domicilio_colonia.Text;
                Empleado.cuenta_bancaria = txt_cuenta.Text;
                Empleado.CURP = txt_curp.Text;
                Empleado.departamento = txt_departamento.Text;
                Empleado.dias_aguinaldo = Convert.ToInt32(txt_dias_aguinaldo.Text);
                Empleado.dias_vacaciones = Convert.ToInt32(txt_dias_vacaciones.Text);
                Empleado.email = txt_email.Text;
                Empleado.estado = txt_domicilio_estado.Text;
                Empleado.estatus = "A";
                //Empleado.fecha_alta = Convert.ToDateTime(dtp_fec_alt.Value.Year.ToString() + "-" + dtp_fec_alt.Value.Month.ToString() + "-" + dtp_fec_alt.Value.Day.ToString());
                Empleado.fecha_alta = Convert.ToDateTime(dtp_fec_alt.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                if (cbx_privilegio.Text=="Usuario")
                {
                    Empleado.id_privilegio = 0;
                }
                else
                {
                    Empleado.id_privilegio = 3;
                }
                
                Empleado.id_sucursal = Convert.ToInt32(cbx_sucursal.SelectedValue.ToString());
                Empleado.id_horario = Convert.ToInt32(cbx_horario.SelectedValue.ToString());
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
                Empleado.guardarEmpleado();
                Empleado.guardarEmpleado_Sucursal();

                //SE OBTIENEN LOS DATOS DEL CHECADOR
                clase_checador.getChecador_Sucursal(Empleado.id_sucursal);
                Conectar_Checador();

                if (bConn)
                {
                    Crear_Usuario_Checador(clase_checador.id, Convert.ToString(Empleado.id), Empleado.nombre, Empleado.password, Empleado.id_privilegio);
                }
                
                confirmacion2 = new formularios_padres.Mensajes();
                confirmacion2.lbl_mensaje.Text = "Desea registrar huella al empleado?";
                confirmacion2.FormClosed += new FormClosedEventHandler(mod_huella);
                confirmacion2.Show();
                Enabled = false;
                Limpiar();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //FUNCION PARA REGISTRAR NUEVO USUARIO EN EL CHECADOR
        public void Crear_Usuario_Checador(int id_checador, string id_empleado, string nombre, string contra, int privilegio)
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

        public void Conectar_Checador()
        {
            try
            {
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
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

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
            txt_departamento.Text = "";
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
            txt_id.Enabled = false;
            Empleado.id = Convert.ToInt32(txt_id_a_modificar.Text);
            if (Empleado.verificar_existencia(Empleado.id))
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
                txt_departamento.Text = Empleado.departamento;
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
            else
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Empleado no registrado.";
                mensaje.lbl_info2.Text = "Intente de nuevo.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
        }


        private void Checador_OnEnrollFinger(int EnrollNumber, int FingerIndex, int ActionResult, int TemplateLenght)
        {
            verificador = verificador + 1;
            if (verificador == 1)
            {
                pic_huella_mod.Image = Image.FromFile("..\\..\\Resources\\huellaregistred.png");
            }
           
        }

        private void btn_modificar_Click_2(object sender, EventArgs e)
        {
            if
          (MessageBox.Show("Desea cambiar huella al empleado?", "cambiar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                tabControlBase.SelectedTab = tabPage3;
                cbx_huella.SelectedIndex = 6;
            }
        }
        //****************** NECESARIOS PARA MOSTRAR MENSAJES *****************
        private void responder(object sender, EventArgs e)
        {
           
            Enabled = true;
            respuesta = confirmacion.respuesta;
           
            if (respuesta == true)
            {
                Conectar_Checador();
                if (bConn)
                {
                    Empleado.Modificar_Empleado();
                   if (sucurzal != Convert.ToInt32(cbx_sucursal.SelectedValue.ToString()))
                    {
                        Crear_Usuario_Checador(clase_checador.id, Convert.ToString(Empleado.id), Empleado.nombre, Empleado.password, Empleado.id_privilegio);
                        Empleado.guardarEmpleado_Sucursal();
                        //SE OBTIENEN LOS DATOS DEL CHECADOR
                        clase_checador.getChecador_Sucursal(Empleado.id_sucursal);
                    
                    }
                    else
                    {

                    }
                }
                else
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Checador no conectado, marquele a Mirsa.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.Show();
                }

               
                confirmacion2 = new formularios_padres.Mensajes();
                confirmacion2.lbl_mensaje.Text = "Desea cambiar huella al empleado?";
                confirmacion2.FormClosed += new FormClosedEventHandler(mod_huella);
                confirmacion2.Show();
                Enabled = false;
                
            }
            else
            {

            }
          
          

        }
        private void mod_huella(object sender, EventArgs e)
        {
            //**************
         
            //****************

            Enabled = true;
            respuesta = confirmacion2.respuesta;
            if (respuesta == true)
            {
                tabControlBase.SelectedTab = tabPage3;
                cbx_huella.SelectedIndex = 6;
            }
            else
            {
                tabControlBase.SelectedTab = tabPage5;
                txt_id_a_modificar.Clear();
            }
            confirmacion2 = null;
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
                Empleado.apellido_mat = txt_apellido_materno.Text;
                Empleado.apellido_pat = txt_apellido_paterno.Text;
                Empleado.banco = txt_banco.Text;
                Empleado.calle = txt_domicilio_calle.Text;
                Empleado.clave_edenred = txt_edenred.Text;
                Empleado.codigo_postal = txt_domicilio_cp.Text;
                Empleado.colonia = txt_domicilio_colonia.Text;
                Empleado.cuenta_bancaria = txt_cuenta.Text;
                Empleado.CURP = txt_curp.Text;
                Empleado.departamento = txt_departamento.Text;
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
                Empleado.id_horario = Convert.ToInt32(cbx_horario.SelectedValue.ToString());
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

                //SE OBTIENEN LOS DATOS DEL CHECADOR
                clase_checador.getChecador_Sucursal(Empleado.id_sucursal);
                Conectar_Checador();

                if (bConn)
                {
                    Modificar_Usuario_Checador(clase_checador.id, Convert.ToString(Empleado.id), Empleado.nombre, Empleado.password, Empleado.id_privilegio);
                }

                confirmacion = new formularios_padres.Mensajes();
                confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea modificar el empleado?";
                confirmacion.FormClosed += new FormClosedEventHandler(responder);
                confirmacion.Show();
                Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //FUNCION PARA MODIFICAR USUARIO EN EL CHECADOR
        public void Modificar_Usuario_Checador(int id_checador, string id_empleado, string nombre, string contra, int privilegio)
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

        private void tabPage2_Click(object sender, EventArgs e)

        {
            int dedo = cbx_huella.SelectedIndex;
            mensaje = new formularios_padres.mensaje_info();
            mensaje.lbl_info.Text = "Coloque su dedo 3 veces en el checador.";
            mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
            mensaje.Show();
            //CODIGO PARA LA INTERFAZ DE REGISTRO DE NUEVA HUELLA
            int flag = 0;
            Checador.StartEnrollEx(Empleado.id.ToString(), dedo, flag);
            if (Checador.RegEvent(clase_checador.id, 65535))
            {
                Checador.OnEnrollFinger += new zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(Checador_OnEnrollFinger);
            }
        }

        private void btn_capturar_Click(object sender, EventArgs e)
        {
            int dedo = cbx_huella.SelectedIndex;
            MessageBox.Show(dedo.ToString());
            //CODIGO PARA LA INTERFAZ DE REGISTRO DE NUEVA HUELLA
            int flag = 0;
            Checador.StartEnrollEx(Empleado.id.ToString(), dedo, flag);
            if (Checador.RegEvent(clase_checador.id, 65535))
            {
                Checador.OnEnrollFinger += new zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(Checador_OnEnrollFinger);
            }
        }

        private void tabPage2_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_guardar_huella_Click(object sender, EventArgs e)
        {
            mensaje = new formularios_padres.mensaje_info();
            mensaje.lbl_info.Text = "La huella ha sido guardada";
            mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
            mensaje.Show();
            Limpiar();
            tabControlBase.SelectedTab = tabPage5;
            txt_curp.Focus();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void txt_dias_aguinaldo_TextChanged(object sender, EventArgs e)
        {

        }
        
////////////////FILTRAR EL BUSCAR//////////////////////////
        private void txt_idbuscar_TextChanged(object sender, EventArgs e)
        {
            if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text=="")
            {
                this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                vistaEmpleadosBindingSource.Filter = "";
            }
            else
            {
                vistaEmpleadosBindingSource.Filter = "CONVERT([id_empleado], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre_completo] LIKE '*"+ txt_nombrebuscar.Text +"*'";
            }
        }

        private void txt_nombrebuscar_TextChanged(object sender, EventArgs e)
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
//////////////////////////////////////////////////////////////////////

        private void btn_dar_baja_Click(object sender, EventArgs e)
        {
            var row = dgv_empleadobuscar.CurrentRow;
            Empleado.id = Convert.ToInt32(row.Cells[0].Value);
            Empleado.estatus = "I";
            Empleado.Eliminar_Empleado();
            
        }
    }
}
