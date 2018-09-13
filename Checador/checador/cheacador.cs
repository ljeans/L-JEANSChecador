using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checador
{
    public partial class cheacador : Checador.formularios_padres.formpadre
    {
        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE CHECADOR
        ClaseChecador Clase_Checador = new ClaseChecador();

        public cheacador()
        {
            InitializeComponent();
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

//MODIFICAR///////////////////////////////////////////////////////////////////////
        //FUNCION PARA ACTUALIZAR LOS DATOS EN LA BD DEL CHECADOR
        private void btn_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                Clase_Checador.ip = txt_ip.Text;
                Clase_Checador.puerto = txt_puerto.Text;
                //SE TIENE QUE MODIFICAR ESTO!!!!!!!!!!!!!!!!!!!!!!
                //Clase_Checador.id_sucursal = cbx_sucursal.SelectedValue.ToString();
                Clase_Checador.id_sucursal = 13;
                if (rb_mod_activo.Checked == true)
                {
                    Clase_Checador.estatus = "A";
                }
                else
                {
                    Clase_Checador.estatus = "I";
                }
                Clase_Checador.Modificar_Checador();
                tabControlBase.SelectedTab = tabPage2;
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //FUNCION PARA CARGAR LOS DATOS DEL CHECADOR EN EL FORMULARIO
        private void btn_ir_modificar_Click(object sender, EventArgs e)
        {
            txt_id.Enabled = false;
            Clase_Checador.id = Convert.ToInt32(txt_id_mod.Text);
            Clase_Checador.verificar_existencia(Clase_Checador.id);
            tabControlBase.SelectedTab = tabPage1;
            MessageBox.Show(Clase_Checador.id.ToString());
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

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage3;
        }

//REGISTRAR//////////////////////////////////////////////////////////////////////////
        //CLICK AL BOTON REGISTRAR
        //FUNCION PARA REGITAR CHECADOR EN LA BASE DE DATOS
        private void btn_registrar_Click(object sender, EventArgs e)
        {
            try
            {
                Clase_Checador.estatus = "A";
                Clase_Checador.id = Convert.ToInt32(txt_id.Text);
                //Clase_Checador.id_sucursal = cbx_sucursal.SelectedValue.ToString();
                Clase_Checador.id_sucursal = 13;
                Clase_Checador.ip = txt_ip.Text;
                Clase_Checador.puerto = txt_puerto.Text;
                Clase_Checador.guardarChecador();
                Limpiar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //FUNCION PARA LIMPIAR LOS COMPONENTES DEL FORMULARIO DESPUES DE HACER UN REGISTRO
        public void Limpiar()
        {
            txt_id.Text = "";
            txt_ip.Text = "";
            txt_puerto.Text = "";
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

    }
}
