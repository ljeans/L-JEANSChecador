using System;
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
    public partial class usuario : Form
    {
        public usuario()
        {
            InitializeComponent();
        }

        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();

        private void usuario_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.rol' Puede moverla o quitarla según sea necesario.
            this.rolTableAdapter.Fill(this.dataSet_Checador.rol);
            txt_contraseña.UseSystemPasswordChar = true;
            txt_comfirmar.UseSystemPasswordChar = true;
            txt_usuario.Focus();

        }


        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;

        }

        private void btn_entrar_Click(object sender, EventArgs e)
        {
            if (txt_contraseña.Text != txt_comfirmar.Text)
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Las contraseñas no coinciden";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
            txt_comfirmar.Focus();
        }

        private void ver_pass()
        {
            txt_contraseña.UseSystemPasswordChar = false;
        }

        private void ocultar_pass()
        {
            txt_contraseña.UseSystemPasswordChar = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_comfirmar_Leave(object sender, EventArgs e)
        {
           
        }

        private void txt_comfirmar_Leave_1(object sender, EventArgs e)
        {

            if (txt_contraseña.Text == txt_comfirmar.Text)
            {
                pictureBox1.Image = Image.FromFile("..\\..\\Resources\\crearusuario_correcto.png");
            }
            else if (txt_comfirmar.Text != txt_contraseña.Text)
            {
                pictureBox1.Image = Image.FromFile("..\\..\\Resources\\crearusuario_incorrecto.png");
            }
            else if (txt_contraseña.Text == "")
            {
                pictureBox1.Image = Image.FromFile("..\\..\\Resources\\crearusuario.png");
            }
        }

        private void txt_contraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            ver_pass();
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            ocultar_pass();
        }
    }
}
