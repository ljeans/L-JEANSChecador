using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checador
{
    public partial class sucursales : Checador.formularios_padres.formpadre
    {
        public sucursales()
        {
            InitializeComponent();
        }


        private void panel_barra_sup_Paint(object sender, PaintEventArgs e)
        {

        }


        private void sucursales_Load(object sender, EventArgs e)
        {

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
            rb_mod_activo.Checked = true;
            groupBox4.Visible = false;
            tabControlBase.SelectedTab = tabPage1;
        }

        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
            rb_mod_activo.Checked = true;
            groupBox4.Visible = true;
            tabControlBase.SelectedTab = tabPage1;
        }

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage2;
        }

  
    }
}
