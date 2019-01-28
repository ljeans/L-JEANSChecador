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

namespace Checador
{
    public partial class principal : Form
    {
        empleados.empleados modulo_empleados = new empleados.empleados();
        Checador.cheacador modulo_checador = new Checador.cheacador();
        sucursales modulo_sucursal = new sucursales();
        horarios modulo_horarios = new horarios();
        reportes.reporte modulo_reportes = new reportes.reporte();
        inicio_sesion.login login = new inicio_sesion.login();
        incidentes.incidentes modulo_incidente = new incidentes.incidentes();
        inicio_sesion.usuario crearusuario = new inicio_sesion.usuario();
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();
        formularios_padres.mensaje_error frm_error = new formularios_padres.mensaje_error();

        //VARIABLE PARA CARGAR LOS CHECADORES
        DataTable dtChecadores = null;

        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE CHECADOR
        ClaseChecador Clase_Checador = new ClaseChecador();

        private void Desbloquear_Principal(object sender, EventArgs e)
        {
            this.Activate();
            lbl_usuario.Text = Program.nombre_usuario;
            if (Program.rol== "ADMINISTRADOR")
            {
                Enabled = true;
                btn_checador.Enabled = true;
                btn_sucursal.Enabled = true;
                btn_empleados.Enabled = true;
                btn_horarios.Enabled = true;
                btn_reportes.Enabled = true;
                btn_incidente.Enabled = true;
                btn_cerrar.Enabled = true;
                btn_iniciar.Enabled = true;
                button1.Enabled = true;
                //cerrar
                button2.Enabled = true;
                //minimizar
                btn_minimizar.Enabled = true;
                btn_cerrar.Visible = true;
                btn_iniciar.Visible = false;
            }
           else if(Program.rol == "SUPERVISOR DE PERSONAL")
            {
                Enabled = true;
                btn_checador.Enabled = true;
                btn_sucursal.Enabled = false;
                btn_empleados.Enabled = false;
                btn_horarios.Enabled = true;
                 btn_reportes.Enabled = true;
                btn_incidente.Enabled = true;
                //cerrar
                button2.Enabled = true;
                //minimizar
                btn_minimizar.Enabled = true;
                btn_cerrar.Visible = true;
                btn_iniciar.Visible = false;
            }
            else if(Program.rol == "ENCARGADA DE TIENDA")
            {
                Enabled = true;
                btn_checador.Enabled = true;
                btn_sucursal.Enabled = false;
                btn_empleados.Enabled = false;
                btn_horarios.Enabled = true;
                btn_reportes.Enabled = true;
                btn_incidente.Enabled = true;
                //cerrar
                button2.Enabled = true;
                //minimizar
                btn_minimizar.Enabled = true;
                btn_cerrar.Enabled = true;
                btn_cerrar.Visible = true;
                btn_iniciar.Visible = false;
            }
            else
            {
                Desbloquear_inicio(sender, e);
            }
          
        }

        private void Desbloquear_inicio(object sender, EventArgs e)
        {
            Enabled = true;
            btn_checador.Enabled = false;
            btn_sucursal.Enabled = false;
            btn_empleados.Enabled = false;
            btn_horarios.Enabled = false;
            btn_reportes.Enabled = false;
            btn_incidente.Enabled = false;
            btn_cerrar.Visible = false;
            btn_iniciar.Enabled = true;
            button1.Enabled = false;
            //cerrar
            button2.Enabled = true;
            //minimizar
            btn_minimizar.Enabled = true;
           
        }

        //OBJETO DE LA CLASSE CKEM (SDK) PARA PODER ACCEDER A METODOS Y ATRIBUTOS
        public zkemkeeper.CZKEM Checador = new zkemkeeper.CZKEM();
        
        //VARIABLES UTILIZADAS A LO LARGO DEL PROGRAMA
        public string Nombre = string.Empty, Contra = string.Empty, EnrollNumber = string.Empty;
        public int Privilegio = 0;
        public bool Estado = false;

        public principal()
        {
            InitializeComponent();
        }

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;
        }

        private void principal_Load(object sender, EventArgs e)
        {
            try
            {
                //************ACOMODAR FORMULARIO************

                double porcentaje_ancho = (Convert.ToDouble(Width) / 1370);
                double porcentaje_alto = (Convert.ToDouble(Height) / 766);


                double proporcion_barrar_superior = Convert.ToDouble(Height) / Convert.ToDouble(panel_barra_superior.Height);
                double proporcion_menu = Convert.ToDouble(Height) / Convert.ToDouble(Height);
                panel_barra_superior.Height = Convert.ToInt32(Math.Round(Height / proporcion_barrar_superior, 0));

                btn_minimizar.Top = 0;
                button2.Top = 0;
                button2.Left = this.Width - 41;
                btn_minimizar.Left = this.Width - 83;
                btn_iniciar_sesion.Left = Width - 246;
                btn_cerrar_sesion.Left = Width - 261;
                btn_iniciar_sesion.Top = 18;
                btn_cerrar_sesion.Top = 18;

                double posicionx = Convert.ToDouble(btn_checador.Location.X) * porcentaje_ancho;
                double posiciony = Convert.ToDouble(btn_checador.Location.Y) * porcentaje_alto;
                double ancho = btn_checador.Width * porcentaje_ancho;
                double alto = btn_checador.Height * porcentaje_alto;

                btn_checador.Left = Convert.ToInt32(posicionx);
                btn_checador.Top = Convert.ToInt32(posiciony);

                posicionx = Convert.ToDouble(btn_sucursal.Location.X) * porcentaje_ancho;
                posiciony = Convert.ToDouble(btn_sucursal.Location.Y) * porcentaje_alto;
                btn_sucursal.Left = Convert.ToInt32(posicionx);
                btn_sucursal.Top = Convert.ToInt32(posiciony);

                posicionx = Convert.ToDouble(btn_empleados.Location.X) * porcentaje_ancho;
                posiciony = Convert.ToDouble(btn_empleados.Location.Y) * porcentaje_alto;
                btn_empleados.Left = Convert.ToInt32(posicionx);
                btn_empleados.Top = Convert.ToInt32(posiciony);


                posicionx = 193 * porcentaje_ancho;
                posiciony = 437 * porcentaje_alto;
                btn_horarios.Left = Convert.ToInt32(posicionx);
                btn_horarios.Top = Convert.ToInt32(posiciony);


                posicionx = 585 * porcentaje_ancho;
                posiciony = 437 * porcentaje_alto;
                btn_reportes.Left = Convert.ToInt32(posicionx);
                btn_reportes.Top = Convert.ToInt32(posiciony);


                posicionx = 978 * porcentaje_ancho;
                posiciony = 437 * porcentaje_alto;
                btn_incidente.Left = Convert.ToInt32(posicionx);
                btn_incidente.Top = Convert.ToInt32(posiciony);



                //*********************************************


                //OBTIENE TODOS LOS CHECADORES ACTIVOS Y LOS CONECTA
                dtChecadores = Clase_Checador.Obtener_Checadores_Activos();
                //DataRow row = dtChecadores.Rows[0];
                //MessageBox.Show(Convert.ToString(row["ip"]));

                /* for (int pos = 0; pos < dtChecadores.Rows.Count; pos++)
                 {
                     DataRow row = dtChecadores.Rows[pos];
                     ipChecador = Convert.ToString(row["ip"]);
                     id_checador = Convert.ToInt32(row["id_checador"]);
                     puerto = Convert.ToInt32(row["puerto"]);

                     try
                     {
                         zkemkeeper.CZKEM Checador = new zkemkeeper.CZKEM();
                         //SE CREA UNA VARIABLE CON EL METODO CONECTAR DEL OBJETO CHECADOR.
                         //SE ENVIAN COMO PARAMETROS LA IP DEL CHECADOR Y EL PUERTO
                         bool bConn = Checador.Connect_Net(ipChecador, puerto);

                         //
                         if (bConn == true)
                         {
                             //SE ACTIVA EL DISPOSITIVO. PARAMETRO EL NUM. DE MAQUINA Y UNA BANDERA
                             Checador.EnableDevice(id_checador, true);
                             MessageBox.Show("Dispositivo conectado + ID: " + id_checador);

                             //FUNCION PARA REGISTRAR TODOS LOS EVENTOS DEL CHECADOR EN TIEMPO REAL
                             if (Checador.RegEvent(id_checador, 65535))
                             {
                                 //Checador.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(Checador_OnEnroll);
                                 Checador.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(Checador_OnAttTransactionEx);
                                 Checador.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(Checador_OnNewUser);
                                 //Checador.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(Checador_OnFinger);
                             }
                         }
                         else
                         {
                             MessageBox.Show("Dispositivo no conectado");
                         }

                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.ToString());
                     }
                 }*/
                Desbloquear_inicio(sender, e);
                Enabled = false;
                login = new inicio_sesion.login();
                login.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
                login.Show();
                btn_cerrar.Visible = true;
                lbl_usuario.Text = Program.nombre_usuario;
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


        //FUNCION QUE SE EJECUTA EN EL EVENTO DE TRANSACCION. CACHA LOS PARAMETROS QUE ESTAN EN LOS ARGUMENTOS
        private void Checador_OnAttTransactionEx(string EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
        {
            MessageBox.Show(EnrollNumber);
            MessageBox.Show(Year + " - " + Month + " - " + Day + "  " + Hour + ":" + Minute + ":" +Second);

            //FUNCION PARA OBTENER LA INFO DE UN USUARIO MEDIANTE SU ID Y EL NUMERO DE CHECADOR
            //Checador.SSR_GetUserInfo(1, EnrollNumber,out Nombre,out Contra,out Privilegio, out Estado);
            //MessageBox.Show(Nombre);

            //FUNCION PARA BORRAR EL CACHE
            Checador.ClearSLog(1);
            //Checador.ClearGLog(1);
        }

        //FUNCION QUE SE EJECUTA EN EL EVENTO NUEVO USUARIO. ACTUALIZA LOS DATOS DEL CHECADOR
        private void Checador_OnNewUser(int id)
        {
            if (Checador.RefreshData(1))
            {
                MessageBox.Show("Usuario nuevo registraro. ID = " + id.ToString());
            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel_menu_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btn_reportes_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                modulo_reportes = new reportes.reporte();
                modulo_reportes.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
                modulo_reportes.Show();
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

        private void btn_horarios_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                modulo_horarios = new horarios();
                modulo_horarios.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
                modulo_horarios.Show();
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

        private void btn_checador_Click(object sender, EventArgs e)
        {
            try { 
                Enabled = false;
                modulo_checador = new Checador.cheacador();
                modulo_checador.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
                modulo_checador.Show();
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

        private void btn_sucursal_Click(object sender, EventArgs e)
        {
            try
            { 
                Enabled = false;
                modulo_sucursal = new sucursales();
                modulo_sucursal.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
                modulo_sucursal.Show();
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

        private void btn_empleados_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                modulo_empleados = new empleados.empleados();
                modulo_empleados.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
                modulo_empleados.Show();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                modulo_empleados = new empleados.empleados();
                modulo_empleados.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
                modulo_empleados.Show();
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //MINIMIZAR EL PROGRAMA
                this.WindowState = FormWindowState.Minimized;
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                login = new inicio_sesion.login();
                login.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
                login.Show();
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

        private void btn_incidente_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                modulo_incidente = new incidentes.incidentes();
                modulo_incidente.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
                modulo_incidente.Show();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                crearusuario = new inicio_sesion.usuario();
                crearusuario.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
                crearusuario.Show();
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

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            //CERRAR SESIÓN
            try
            {
                Desbloquear_inicio(sender, e);
                Program.nombre_usuario = "";
                Program.rol = "";
                Program.id_empleado = 0;
                Program.id_sucursal = 0;
                lbl_usuario.Text = "";
                btn_iniciar.Visible = true;

                //LLAMAR INICIAR SESION AL MOMENTO DE CERRAR SESION
                login = new inicio_sesion.login();
                login.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
                login.Show();
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

        private void Checador_OnFinger()
        {
            
            /*if (Checador.CaptureImage(Imagen, ancho, alto, image, imagefile))
            {
                MessageBox.Show(imagefile);
            }
            else
            {
                MessageBox.Show(Checador.CaptureImage(Imagen, ancho, alto, image, imagefile).ToString());
            }*/
            /*if (Checador.RegEvent(1, 65535))
            {
                Checador.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(Checador_OnAttTransactionEx);
                //Checador.OnEnrollFinger += new zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(Checador_OnEnrollFinger);

            }
            Checador.ClearGLog(1);*/
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*int flag = 0;
            Checador.StartEnrollEx("22",4,flag);
            
            if (Checador.GetPhotoCount(1, out Privilegio, flag))
            {
                MessageBox.Show(Privilegio.ToString());
            }*/
        }
    }
}
