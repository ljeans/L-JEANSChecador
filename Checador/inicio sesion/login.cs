﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checador.inicio_sesion
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void login_Load(object sender, EventArgs e)
        {
            txt_contraseña.UseSystemPasswordChar = true;
            txt_usuario.Focus();
        }
        private void ver_pass()
        {
            pictureBox1.BackgroundImage = Properties.Resources.ocultar_pass;
            txt_contraseña.UseSystemPasswordChar = false;
        }
        private void ocultar_pass()
        {
            pictureBox1.BackgroundImage = Properties.Resources.ver_pass;
            txt_contraseña.UseSystemPasswordChar = true;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            ver_pass();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
                ocultar_pass();   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}