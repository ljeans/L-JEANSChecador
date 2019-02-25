using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Checador.configuracion
{
    public partial class configuracion : Form
    {
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();
        formularios_padres.mensaje_error frm_error = new formularios_padres.mensaje_error();

        public configuracion()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            //GUARDAR CONFIGURACION DE RED
            try
            {
                if (rb_publica.Checked == true)
                {
                    //ESCRIBIR EN EL ARCHIVO DE CONFIGURACION
                    string[] lines = { "Data Source=ljeansinv.ddns.net,14333;Initial Catalog=sistema_checador;Persist Security Info=True;User ID=SA;Password=123456" };
                    File.WriteAllLines(@"..\\..\\Archivos\\configuracion.txt", lines);
                }
                else if (rb_vpn.Checked == true)
                {
                    //ESCRIBIR EN EL ARCHIVO DE CONFIGURACION
                    string[] lines = { "Data Source=20.20.1.126;Initial Catalog=sistema_checador;Persist Security Info=True;User ID=SA;Password=123456" };
                    File.WriteAllLines(@"..\\..\\Archivos\\configuracion.txt", lines);
                }
                MessageBox.Show("Configuración guardada con éxito. Reinicie el sistema para aplicar los cambios");
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

        private void configuracion_Load(object sender, EventArgs e)
        {
            try
            {
                //VERIFICAR SI EL ARCHIVO EXISTE
                if (System.IO.File.Exists(@"..\\..\\Archivos\\configuracion.txt"))
                {
                    //LEER EL ARCHIVO DE CONFIGURACION
                    string cadena_conexion = File.ReadAllLines(@"..\\..\\Archivos\\configuracion.txt")[0];
                    Program.cadena_conexion = cadena_conexion;

                    if(cadena_conexion == "Data Source=ljeansinv.ddns.net,14333;Initial Catalog=sistema_checador;Persist Security Info=True;User ID=SA;Password=123456")
                    {
                        rb_publica.Checked = true;
                    }
                    else
                    {
                        rb_vpn.Checked = true;
                    }
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
    }
}
