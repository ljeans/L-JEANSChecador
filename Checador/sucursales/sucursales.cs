using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
namespace Checador
{
    public partial class sucursales : Checador.formularios_padres.formpadre
    {
        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE SUCURSAL
        ClaseSucursal Sucursal = new ClaseSucursal();
        ClaseHorario horario = new ClaseHorario();
        int idhorario;
        public sucursales()
        {
            InitializeComponent();
        }


        private void panel_barra_sup_Paint(object sender, PaintEventArgs e)
        {

        }


        private void sucursales_Load(object sender, EventArgs e)
        {
           
            // TODO: This line of code loads data into the 'dataSet_Checador.horarios' table. You can move, or remove it, as needed.
            this.horariosTableAdapter.Fill(this.dataSet_Checador.horarios);
           
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {
            txt_id.Enabled = true;
            rb_mod_activo.Checked = true;
            groupBox4.Visible = false;
            tabControlBase.SelectedTab = tabPage1;
            btn_modificar.Enabled = false;
            btn_modificar.Visible = false;
            btn_registrar.Visible = true;
        }

        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
            rb_mod_activo.Checked = true;
            groupBox4.Visible = true;
            btn_modificar.Enabled = true;
            btn_modificar.Visible = true;
            btn_registrar.Visible = false;
            txt_id_mod.Text = "";
            tabControlBase.SelectedTab = tabPage3;
            
        }

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Sucursal' table. You can move, or remove it, as needed.
            this.vista_SucursalTableAdapter.Fill(this.dataSet_Checador.Vista_Sucursal);
            tabControlBase.SelectedTab = tabPage2;
        }


        //REGISTRAR///////////////////////////////////////////////////////////////////////////
        //CLICK AL BOTON REGISTRAR
        //FUNCION PARA REGITAR SUCURSAL EN LA BASE DE DATOS
        public void cargarIDHorario()
        {
            //MOSTRAR EL ID DEL HORARIO AL CARGAR LA PAGINA
            try
            {
                idhorario = (Sucursal.obtenerId(horario.horario));
            }
            catch (Exception ex)
            {
                txt_id.Text = "1";
            }
        }

        private void btn_registrar_Click(object sender, EventArgs e)
        {
            try
            {
                Sucursal.calle = txt_domicilio_calle.Text;
                Sucursal.codigo_postal = txt_domicilio_cp.Text;
                Sucursal.colonia = txt_domicilio_colonia.Text;
                Sucursal.estado = txt_domicilio_estado.Text;
                Sucursal.estatus = "A";
                Sucursal.id = Convert.ToInt32(txt_id.Text);
                Sucursal.municipio = txt_domicilio_municipio.Text;
                Sucursal.nombre = txt_nombre.Text;
                Sucursal.num_ext = txt_domicilio_num_ext.Text;
                Sucursal.num_int = txt_domicilio_num_int.Text;
                Sucursal.pais = txt_domicilio_pais.Text;
                Sucursal.poblacion = txt_domicilio_pob.Text;
                Sucursal.telefono = txt_telefono.Text;
                Sucursal.id_horario = Convert.ToInt32(cbx_horario.SelectedValue.ToString());
                Sucursal.guardarSucursal();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        //FUNCION PARA LIMPIAR LOS COMPONENTES DEL FORMULARIO DESPUES DE HACER UN REGISTRO
        public void Limpiar()
        {
            txt_domicilio_calle.Text = "";
            txt_domicilio_colonia.Text = "";
            txt_domicilio_cp.Text = "";
            txt_domicilio_estado.Text = "";
            txt_domicilio_municipio.Text = "";
            txt_domicilio_num_ext.Text = "";
            txt_domicilio_num_int.Text = "";
            txt_domicilio_pais.Text = "";
            txt_domicilio_pob.Text = "";
            txt_id.Text = "";
            txt_nombre.Text = "";
            txt_telefono.Text = "";
            cbx_horario.SelectedIndex = 0;
            txt_id.Focus();
            //Deshabilitar_Componentes();
        }

        //FUNCION PARA DESHABILITAR LOS COMPONENTES DEL FORMULARIO
        public void Deshabilitar_Componentes()
        {
            txt_domicilio_calle.Enabled = false;
            txt_domicilio_colonia.Enabled = false;
            txt_domicilio_cp.Enabled = false;
            txt_domicilio_estado.Enabled = false;
            txt_domicilio_municipio.Enabled = false;
            txt_domicilio_num_ext.Enabled = false;
            txt_domicilio_num_int.Enabled = false;
            txt_domicilio_pais.Enabled = false;
            txt_domicilio_pob.Enabled = false;
            txt_nombre.Enabled = false;
            txt_telefono.Enabled = false;
            btn_registrar.Enabled = false;
        }

        //FUNCION PARA HABILITAR LOS COMPONENTES DEL FORMULARIO
        public void Habilitar_Componentes()
        {
            txt_domicilio_calle.Enabled = true;
            txt_domicilio_colonia.Enabled = true;
            txt_domicilio_cp.Enabled = true;
            txt_domicilio_estado.Enabled = true;
            txt_domicilio_municipio.Enabled = true;
            txt_domicilio_num_ext.Enabled = true;
            txt_domicilio_num_int.Enabled = true;
            txt_domicilio_pais.Enabled = true;
            txt_domicilio_pob.Enabled = true;
            txt_nombre.Enabled = true;
            txt_telefono.Enabled = true;
            btn_registrar.Enabled = true;
        }

//MODIFICAR/////////////////////////////////////////////////////////////////////////////////
        private void btn_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                horario.horario = cbx_horario.Text;
                idhorario = Sucursal.obtenerId(horario.horario);
                //Sucursal.id = Convert.ToInt32(txt_id_mod.Text);
                Sucursal.calle = txt_domicilio_calle.Text;
                Sucursal.codigo_postal = txt_domicilio_cp.Text;
                Sucursal.colonia = txt_domicilio_colonia.Text;
                Sucursal.estado = txt_domicilio_estado.Text;
                if (rb_mod_activo.Checked==true)
                {
                    Sucursal.estatus = "A";
                }
                else
                {
                    Sucursal.estatus = "I";
                }
                Sucursal.municipio = txt_domicilio_municipio.Text;
                Sucursal.nombre = txt_nombre.Text;
                Sucursal.num_ext = txt_domicilio_num_ext.Text;
                Sucursal.num_int = txt_domicilio_num_int.Text;
                Sucursal.pais = txt_domicilio_pais.Text;
                Sucursal.poblacion = txt_domicilio_pob.Text;
                Sucursal.telefono = txt_telefono.Text;
                Sucursal.id_horario = Convert.ToInt32(cbx_horario.SelectedValue.ToString());
                Sucursal.Modificar_Sucursal();
                tabControlBase.SelectedTab = tabPage3;
                txt_id_mod.Text = "";
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //CARGAR DATOS AL FORMULARIO
        private void btn_mod_Click(object sender, EventArgs e)
        {
            txt_id.Enabled = false;
            Sucursal.id = Convert.ToInt32(txt_id_mod.Text);
            if (Sucursal.verificar_existencia(Sucursal.id))
            {
                tabControlBase.SelectedTab = tabPage1;
                txt_id.Text = Sucursal.id.ToString();
                txt_nombre.Text = Sucursal.nombre;
                txt_domicilio_calle.Text = Sucursal.calle;
                txt_domicilio_num_ext.Text = Sucursal.num_ext;
                txt_domicilio_num_int.Text = Sucursal.num_int;
                txt_domicilio_colonia.Text = Sucursal.colonia;
                txt_domicilio_cp.Text = Sucursal.codigo_postal;
                txt_domicilio_pob.Text = Sucursal.poblacion;
                txt_domicilio_municipio.Text = Sucursal.municipio;
                txt_domicilio_estado.Text = Sucursal.estado;
                txt_domicilio_pais.Text = Sucursal.pais;
                txt_telefono.Text = Sucursal.telefono;
                cbx_horario.SelectedValue = Sucursal.id_horario;
                if (Sucursal.estatus.ToString() == "A")
                {
                    rb_mod_activo.Checked = true;
                }
                else if (Sucursal.estatus.ToString() == "I")
                {
                    rb_mod_inactivo.Checked = true;
                }
            }
            else
            {
                MessageBox.Show("Sucursal no registrada. Por favor intente de nuevo.");
            }

        }

        private void txt_domicilio_calle_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_dar_baja_Click(object sender, EventArgs e)
        {
            var row = dgv_sucursal.CurrentRow;
            Sucursal.id = Convert.ToInt32(row.Cells[0].Value);
            Sucursal.estatus = "I";
            Sucursal.Eliminar_Sucursal();
        }
    }
}
