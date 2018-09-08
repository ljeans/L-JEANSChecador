using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checador.empleados
{
    public partial class empleados : Checador.formularios_padres.formpadre
    {
        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE EMPLEADO
        ClaseEmpleado Empleado = new ClaseEmpleado();
        huella huella = new huella();

        public empleados()
        {
            InitializeComponent();
        }

        private void empleados_Load(object sender, EventArgs e)
        {
            //MOSTRAR EL ID DEL EMPLEADO AL CARGAR LA PAGINA
            try
            {
                txt_id.Text = (Empleado.obtenerIdMaximo()+1).ToString();
            }
            catch (Exception ex)
            {
                txt_id.Text ="1";
            }
            
        }

    
        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage8;
        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage1;
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_mod_siguiente_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage6;
        }

        private void btn_mod_siguiente2_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage7;
        }

        private void btn_mod_atras_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage5;
        }

        private void btn_mod_atras2_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage6;
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

        private void btn_siguiente2_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage3;
        }

        private void btn_atras_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage1;
        }

        private void btn_atras2_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage2;
        }

        //CLICK AL BOTON REGISTRAR
        private void btn_registrar_emp_Click(object sender, EventArgs e)
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
            Empleado.sueldo_base_quincenal = Convert.ToDouble( txt_sueldo_quincenal.Text);
            Empleado.sueldo_diario = Convert.ToDouble(txt_sueldo_diario.Text);
            Empleado.sueldo_diario_integrado = Convert.ToDouble(txt_sueldo_integrado.Text);
            Empleado.tarjeta_despensa= txt_despensa.Text;
            Empleado.telefono = txt_telefono.Text;
            Empleado.tipo_contrato = txt_tipo_contrato.Text;
            //Empleado.tipo_horario = cbx_horario.SelectedValue.ToString();
            Empleado.id_horario = 1;
            Empleado.tipo_salario = txt_tipo_salario.Text;
            Empleado.guardarEmpleado();
        }
        private void Desbloquear_empleados(object sender, EventArgs e)
        {
           
            Enabled = true;
        }

        private void btn_capturar_Click(object sender, EventArgs e)
        {
            Enabled = false;
            huella = new huella();
            huella.FormClosed += new FormClosedEventHandler(Desbloquear_empleados);
            huella.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_capturar_mod_Click(object sender, EventArgs e)
        {
            Enabled = false;
            huella = new huella();
            huella.FormClosed += new FormClosedEventHandler(Desbloquear_empleados);
            huella.Show();
        }
    }
}
