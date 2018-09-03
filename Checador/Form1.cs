﻿using System;
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
    public partial class Form1 : Form
    {
        //OBJETO DE LA CLASSE CKEM (SDK) PARA PODER ACCEDER A METODOS Y ATRIBUTOS
        public zkemkeeper.CZKEM Checador = new zkemkeeper.CZKEM();
        
        //VARIABLES UTILIZADAS A LO LARGO DEL PROGRAMA
        public string Nombre = string.Empty, Contra = string.Empty, EnrollNumber = string.Empty;
        public int Privilegio = 0;
        public bool Estado = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //SE CREA UNA VARIABLE CON EL METODO CONECTAR DEL OBJETO CHECADOR.
                //SE ENVIAN COMO PARAMETROS LA IP DEL CHECADOR Y EL PUERTO
                bool bConn = Checador.Connect_Net("20.20.1.50", 4370);
                
                //
                if (bConn == true)
                {
                    //SE ACTIVA EL DISPOSITIVO. PARAMETRO EL NUM. DE MAQUINA Y UNA BANDERA
                    Checador.EnableDevice(1, true);
                    MessageBox.Show("Si conecta esta madre");

                    //FUNCION PARA REGISTRAR TODOS LOS EVENTOS DEL CHECADOR EN TIEMPO REAL
                    if (Checador.RegEvent(1, 65535))
                    {
                        //Checador.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(Checador_OnEnroll);
                        Checador.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(Checador_OnAttTransactionEx);
                        Checador.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(Checador_OnNewUser);
                        //Checador.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(Checador_OnFinger);
                    }
                }
                else
                {
                    MessageBox.Show("No conecta esta madre");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se murio el programa.");
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
