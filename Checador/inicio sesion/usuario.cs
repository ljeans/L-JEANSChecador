using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Checador.inicio_sesion
{
    public partial class usuario : Form
    {
        public usuario()
        {
            InitializeComponent();
        }

        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.mensaje_error frm_error = new formularios_padres.mensaje_error();
        ClaseUsuario usuarios = new ClaseUsuario();
        validacion validar = new validacion();

        private void usuario_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.rol' Puede moverla o quitarla según sea necesario.
                this.rolTableAdapter.Fill(this.dataSet_Checador.rol);
                txt_contraseña.UseSystemPasswordChar = true;
                txt_comfirmar.UseSystemPasswordChar = true;
                txt_usuario.Focus();
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }


        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;
        }

        private void btn_entrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_contraseña.Text != txt_comfirmar.Text)
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Las contraseñas no coinciden";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.Show();
                    txt_comfirmar.Focus();
                }
                usuarios.usuario = txt_usuario.Text;
                usuarios.contraseña = txt_contraseña.Text;
                usuarios.id_rol = Convert.ToInt32(cbx_rol.SelectedValue.ToString());
                usuarios.id_empleado = Convert.ToInt32(txt_empleado.Text);
                usuarios.guardarUsuario();
                this.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        public void limpiar()
        {
            txt_usuario.Clear();
            cbx_rol.SelectedIndex = 1;
            txt_contraseña.Text = "";
            txt_comfirmar.Text = "";

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

        private void txt_empleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }
    }
}
