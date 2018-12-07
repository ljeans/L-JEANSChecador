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
    public partial class login : Form
    {
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();
        formularios_padres.mensaje_error frm_error = new formularios_padres.mensaje_error();

        public login()
        {
            InitializeComponent();
        }

        ClaseUsuario usuario = new ClaseUsuario();
        ClaseEmpleado empleados = new ClaseEmpleado();
        ClaseSucursal sucursal = new ClaseSucursal();

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;
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
            Enabled = true;
            this.Close();

        }
        private void btn_entrar_Click(object sender, EventArgs e)
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                usuario.usuario = txt_usuario.Text;
                usuario.contraseña = txt_contraseña.Text;
                if (usuario.Entrar())
                {
                    usuario.obtener_rol(usuario.id_rol);
                    Program.rol = usuario.nombre_rol;
                    Program.id_empleado = usuario.id_empleado;
                    empleados.verificar_existencia(usuario.id_empleado);
                    sucursal.verificar_existencia(empleados.id_sucursal);
                    Program.sucursal = sucursal.nombre;
                    Program.nombre_usuario = empleados.nombre + " " + empleados.apellido_pat;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Bienvenido(a) a L-JEANS:";
                    mensaje.lbl_info2.Text = empleados.nombre + " " + empleados.apellido_pat;
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos..");
                }
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;

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

        private void txt_contraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_contraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_entrar.PerformClick();
            }
        }
    }
}
