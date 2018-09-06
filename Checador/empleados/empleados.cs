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
      

        public empleados()
        {
            InitializeComponent();
        }

        private void empleados_Load(object sender, EventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

    
        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage8;
        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage2;
        }

        private void btn_registrar_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage3;
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage2;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {

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

        private void panel_barra_sup_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
