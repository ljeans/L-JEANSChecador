﻿using System;
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
        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE SUCURSAL
        ClaseSucursal Empleado = new ClaseSucursal();

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
    }
}
