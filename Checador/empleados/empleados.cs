using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//LIBRERIA PARA HILOS
using System.Threading;

namespace Checador.empleados
{
    public partial class empleados : Checador.formularios_padres.formpadre
    {
        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE EMPLEADO
        ClaseEmpleado Empleado = new ClaseEmpleado();
        huella huella = new huella();

        //SE CREA LA INSTANCIA DE LA CLASE CHECADOR
        ClaseChecador clase_checador = new ClaseChecador();

        //OBJETO DE LA CLASSE CKEM (SDK) PARA PODER ACCEDER A METODOS Y ATRIBUTOS
        public zkemkeeper.CZKEM Checador = new zkemkeeper.CZKEM();

        public empleados()
        {
            InitializeComponent();
        }

        private void empleados_Load(object sender, EventArgs e)
        {
            //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
            CheckForIllegalCrossThreadCalls = false;
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.cargarID));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
            //cargarID();
            groupBox4.Visible = false;
            groupBox4.Enabled = false;
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
            tabControlBase.SelectedTab = tabPage1;
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
                //Empleado.id_privilegio = cbx_privilegio.SelectedValue.ToString();
                Empleado.id_privilegio = 0;
                //Empleado.id_sucursal = cbx_sucursal.SelectedValue.ToString();
                Empleado.id_sucursal = 13;
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
                Empleado.id_horario = 1;
                Empleado.tipo_salario = txt_tipo_salario.Text;
                Empleado.password = txt_contra.Text;
                Empleado.guardarEmpleado();
                Empleado.guardarEmpleado_Sucursal();

                //SE OBTIENEN LOS DATOS DEL CHECADOR
                clase_checador.getChecador_Sucursal(Empleado.id_sucursal);
                Conectar_Checador();

                Crear_Usuario_Checador(clase_checador.id, Convert.ToString(Empleado.id), Empleado.nombre, Empleado.password, Empleado.id_privilegio);

                if (MessageBox.Show("Desea registrar huella al empleado?", "registrar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    tabControlBase.SelectedTab = tabPage3;
                    cbx_huella.SelectedIndex = 6;
                }
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

            //ATENCION SE NECESITA MODIFICAR EL PRIMER PARAMETRO DE LA FUNCION POR EL ID DEL CHECADOR CORRESPONDIENTE
            if (Checador.SSR_SetUserInfo(id_checador, id_empleado, nombre, contra, privilegio, true))
            {
                MessageBox.Show("Registrado en el checador");
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
                bool bConn = Checador.Connect_Net(clase_checador.ip, Convert.ToInt32(clase_checador.puerto));

                if (bConn == true)
                {
                    //SE ACTIVA EL DISPOSITIVO. PARAMETRO EL NUM. DE MAQUINA Y UNA BANDERA
                    Checador.EnableDevice(clase_checador.id, true);
                }
                else
                {
                    //ATENCION CAMBIAR ESTE MENSAJE A LA CONSOLA PARA MAYOR COMODIDAD
                    MessageBox.Show("Dispositivo no conectado");
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
            txt_dias_aguinaldo.Text = "";
            txt_dias_vacaciones.Text = "";
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
            txt_sueldo_diario.Text = "";
            txt_sueldo_integrado.Text = "";
            txt_sueldo_quincenal.Text = "";
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
            cbx_horario.Enabled = false;
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
            cbx_horario.Enabled = true;
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
            Enabled = false;
            huella = new huella();
            huella.FormClosed += new FormClosedEventHandler(Desbloquear_empleados);
            huella.Show();
        }

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage4;
        }

        private void rb_modificar_CheckedChanged_1(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage5;
            groupBox4.Visible = true;
            groupBox4.Enabled = true;

        }
        //*************************** MODIFICAR EMPLEADOS ***********************************************
        private void btn_modificar_Click_1(object sender, EventArgs e)
        {
            txt_id.Enabled = false;
            Empleado.id = Convert.ToInt32(txt_id_a_modificar.Text);
            Empleado.verificar_existencia(Empleado.id);

            tabControlBase.SelectedTab = tabPage1;
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
            if (Empleado.id_privilegio == 0)
            {
                cbx_privilegio.SelectedIndex = 0;
            }
            else if (Empleado.id_privilegio == 3) {
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

        private void Checador_OnEnrollFinger(int EnrollNumber, int FingerIndex, int ActionResult, int TemplateLenght)
        {
            //pic_huella_mod.Image = Image.FromFile("..\\..\\Resources\\huella1.png");
            MessageBox.Show("Huella registrada con exito");
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

        //FUNCION PARA ACTUALIZAR LOS DATOS DE UN EMPLEADO
        private void btn_modificar_Click_3(object sender, EventArgs e)
        {
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
                //Empleado.id_privilegio = cbx_privilegio.SelectedValue.ToString();
                Empleado.id_privilegio = 0;
                //Empleado.id_sucursal = cbx_sucursal.SelectedValue.ToString();
                Empleado.id_sucursal = 13;
         
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
                Empleado.id_horario = 1;
                Empleado.tipo_salario = txt_tipo_salario.Text;
                Empleado.password = txt_contra.Text;
                Empleado.Modificar_Empleado();
                if
                (MessageBox.Show("Desea cambiar huella al empleado?", "cambiar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    tabControlBase.SelectedTab = tabPage3;
                    cbx_huella.SelectedIndex = 6;
                }
                else
                {
                    tabControlBase.SelectedTab = tabPage5;
                    txt_id_a_modificar.Clear();
                }

                //SE OBTIENEN LOS DATOS DEL CHECADOR
                /*   clase_checador.getChecador_Sucursal(Empleado.id_sucursal);
                   Conectar_Checador();

                   Crear_Usuario_Checador(clase_checador.id, Convert.ToString(Empleado.id), Empleado.nombre, Empleado.password, Empleado.id_privilegio);

                   if (MessageBox.Show("Desea registrar huella al empleado?", "registrar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                   {
                       tabControlBase.SelectedTab = tabPage3;
                       cbx_huella.SelectedIndex = 6;
                   }
                */
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
    }
}
