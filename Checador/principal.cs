using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checador
{
    public partial class principal : Form
    {
        
        empleados.empleados modulo_empleados = new empleados.empleados();
        Checador.cheacador modulo_checador = new Checador.cheacador();
        sucursales modulo_sucursal = new sucursales();

        horarios modulo_horarios = new horarios();
       

        //VARIABLE PARA CARGAR LOS CHECADORES
        DataTable dtChecadores = null;

        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE CHECADOR
        ClaseChecador Clase_Checador = new ClaseChecador();

        private void Desbloquear_Principal(object sender, EventArgs e)
        {
            if (modulo_empleados != null) { modulo_empleados = null; }
            else if (modulo_checador != null) { modulo_checador = null; }
            Enabled = true;
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

        private void principal_Load(object sender, EventArgs e)
        {
            string ipChecador = "20.20.0.15";
            int id_checador = 1;
            int puerto = 4370;

            //OBTIENE TODOS LOS CHECADORES ACTIVOS Y LOS CONECTA
            dtChecadores = Clase_Checador.Obtener_Checadores_Activos();
            //DataRow row = dtChecadores.Rows[0];
            //MessageBox.Show(Convert.ToString(row["ip"]));

            /*for (int pos = 0; pos < dtChecadores.Rows.Count; pos++)
            {
                DataRow row = dtChecadores.Rows[pos];
                ipChecador = Convert.ToString(row["ip"]);
                id_checador = Convert.ToInt32(row["id_checador"]);
                puerto = Convert.ToInt32(row["puerto"]);
            }*/

            

            try
            {
                //SE CREA UNA VARIABLE CON EL METODO CONECTAR DEL OBJETO CHECADOR.
                //SE ENVIAN COMO PARAMETROS LA IP DEL CHECADOR Y EL PUERTO
                bool bConn = Checador.Connect_Net(ipChecador, puerto);
                
                //
                if (bConn == true)
                {
                    //SE ACTIVA EL DISPOSITIVO. PARAMETRO EL NUM. DE MAQUINA Y UNA BANDERA
                    Checador.EnableDevice(id_checador, true);
                    MessageBox.Show("Dispositivo conectado");

                    //FUNCION PARA REGISTRAR TODOS LOS EVENTOS DEL CHECADOR EN TIEMPO REAL
                    /*if (Checador.RegEvent(1, 65535))
                    {
                        //Checador.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(Checador_OnEnroll);
                        Checador.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(Checador_OnAttTransactionEx);
                        Checador.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(Checador_OnNewUser);
                        //Checador.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(Checador_OnFinger);
                    }*/
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
        }


        //FUNCION QUE SE EJECUTA EN EL EVENTO DE TRANSACCION. CACHA LOS PARAMETROS QUE ESTAN EN LOS ARGUMENTOS
        private void Checador_OnAttTransactionEx(string EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
        {
            MessageBox.Show(EnrollNumber);

            //FUNCION PARA OBTENER LA INFO DE UN USUARIO MEDIANTE SU ID Y EL NUMERO DE CHECADOR
            Checador.SSR_GetUserInfo(1, EnrollNumber,out Nombre,out Contra,out Privilegio, out Estado);
            MessageBox.Show(Nombre);

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

        private void btn_empleados_Click(object sender, EventArgs e)
        {
            Enabled = false;
            modulo_empleados = new empleados.empleados();
            modulo_empleados.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
            modulo_empleados.Show();
         
        }

        private void btn_sucursal_Click(object sender, EventArgs e)
        {
            Enabled = false;
            modulo_sucursal = new sucursales();
            modulo_sucursal.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
            modulo_sucursal.Show();
        }

        private void btn_checador_Click(object sender, EventArgs e)
        {
            Enabled = false;
            modulo_checador = new Checador.cheacador();
            modulo_checador.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
            modulo_checador.Show();
        }

        private void btn_horarios_Click(object sender, EventArgs e)
        {
            Enabled = false;
            modulo_horarios = new horarios();
            modulo_horarios.FormClosed += new FormClosedEventHandler(Desbloquear_Principal);
            modulo_horarios.Show();
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
            int flag = 0;
            Checador.StartEnrollEx("22",4,flag);
            
            if (Checador.GetPhotoCount(1, out Privilegio, flag))
            {
                MessageBox.Show(Privilegio.ToString());
            }
        }
    }
}
