﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checador
{
    public partial class cheacador : Checador.formularios_padres.formpadre
    {
        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE CHECADOR
        ClaseChecador Clase_Checador = new ClaseChecador();

        //OBJETO DE LA CLASSE CKEM (SDK) PARA PODER ACCEDER A METODOS Y ATRIBUTOS
        public zkemkeeper.CZKEM Checador = new zkemkeeper.CZKEM();

        public cheacador()
        {
            InitializeComponent();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
            rb_mod_activo.Checked = true;
            groupBox4.Visible = true;
            btn_modificar.Enabled = true;
            btn_modificar.Visible = true;
            btn_registrar.Visible = false;
            tabControlBase.SelectedTab = tabPage2;
            txt_id_mod.Text = "";
        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {

            txt_id.Enabled = true;
            rb_mod_activo.Checked = true;
            groupBox4.Visible = false;
            btn_modificar.Enabled = false;
            btn_modificar.Visible = false;
            btn_registrar.Visible = true;
            tabControlBase.SelectedTab = tabPage1;
            Limpiar();

        }

//MODIFICAR///////////////////////////////////////////////////////////////////////
        //FUNCION PARA ACTUALIZAR LOS DATOS EN LA BD DEL CHECADOR
        private void btn_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                Clase_Checador.ip = txt_ip.Text;
                Clase_Checador.puerto = txt_puerto.Text;
                Clase_Checador.id_sucursal = Convert.ToInt32(cbx_sucursal.SelectedValue.ToString());
                if (rb_mod_activo.Checked == true)
                {
                    Clase_Checador.estatus = "A";
                }
                else
                {
                    Clase_Checador.estatus = "I";
                }
                Clase_Checador.Modificar_Checador();
                tabControlBase.SelectedTab = tabPage2;
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //FUNCION PARA CARGAR LOS DATOS DEL CHECADOR EN EL FORMULARIO
        private void btn_ir_modificar_Click(object sender, EventArgs e)
        {
            txt_id.Enabled = false;
            Clase_Checador.id = Convert.ToInt32(txt_id_mod.Text);
            if (Clase_Checador.verificar_existencia(Clase_Checador.id))
            {
                tabControlBase.SelectedTab = tabPage1;
                txt_id.Text = Clase_Checador.id.ToString();
                txt_ip.Text = Clase_Checador.ip;
                txt_puerto.Text = Clase_Checador.puerto;
                cbx_sucursal.SelectedValue = Clase_Checador.id_sucursal;

                if (Clase_Checador.estatus.ToString() == "A")
                {
                    rb_mod_activo.Checked = true;
                }
                else if (Clase_Checador.estatus.ToString() == "I")
                {
                    rb_mod_inactivo.Checked = true;
                }

                btn_modificar.Enabled = true;
                btn_modificar.Visible = true;
                btn_registrar.Visible = false;
            }
            else
            {
                MessageBox.Show("Checador no registrado. Por favor intente de nuevo.");
            }
            
        }

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage3;
        }

//REGISTRAR//////////////////////////////////////////////////////////////////////////
        //CLICK AL BOTON REGISTRAR
        //FUNCION PARA REGITAR CHECADOR EN LA BASE DE DATOS
        private void btn_registrar_Click(object sender, EventArgs e)
        {
            try
            {
                Clase_Checador.estatus = "A";
                Clase_Checador.id = Convert.ToInt32(txt_id.Text);
                Clase_Checador.id_sucursal = Convert.ToInt32(cbx_sucursal.SelectedValue.ToString());
                Clase_Checador.ip = txt_ip.Text;
                Clase_Checador.puerto = txt_puerto.Text;
                Clase_Checador.guardarChecador();
                Limpiar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //FUNCION PARA LIMPIAR LOS COMPONENTES DEL FORMULARIO DESPUES DE HACER UN REGISTRO
        public void Limpiar()
        {
            txt_id.Text = "";
            txt_ip.Text = "";
            txt_puerto.Text = "";
            txt_id.Focus();
            //Deshabilitar_Componentes();
        }

        //FUNCION PARA DESHABILITAR LOS COMPONENTES DEL FORMULARIO
        public void Deshabilitar_Componentes()
        {
            txt_puerto.Enabled = false;
            txt_ip.Enabled = false;
            cbx_sucursal.Enabled = false;
            btn_registrar.Enabled = false;
        }

        //FUNCION PARA HABILITAR LOS COMPONENTES DEL FORMULARIO
        public void Habilitar_Componentes()
        {
            txt_puerto.Enabled = true;
            txt_ip.Enabled = true;
            cbx_sucursal.Enabled = true;
            btn_registrar.Enabled = true;
        }

        private void cheacador_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Checador' table. You can move, or remove it, as needed.
            this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
            // TODO: This line of code loads data into the 'dataSet_Checador.sucursal' table. You can move, or remove it, as needed.
            this.sucursalTableAdapter.Fill(this.dataSet_Checador.sucursal);

            //CAMBIAR LA LETRA AL DATAGRIDVIEW
            dgv_checador.DefaultCellStyle.Font = new Font ("Microsoft Sans Serif", 12);
            dgv_checador.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
        }

        private void btn_scr_fecha_Click(object sender, EventArgs e)
        {
            try
            {
                //FUNCION PARA SINCRONIZAR LA FECHA Y HORA DEL CHECADOR CON LA DEL SERVIDOR
                var row = dgv_checadorbuscar.CurrentRow;
                Conectar_Checador(Convert.ToInt32(row.Cells[0].Value), row.Cells[2].Value.ToString(), Convert.ToInt32(row.Cells[3].Value));
                Checador.SetDeviceTime(Convert.ToInt32(row.Cells[0].Value));
                MessageBox.Show("Sincronizado");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ocurrió un error al sincronizar la fecha y hora del checador. Intenta de nuevo.");
                MessageBox.Show(ex.ToString());
            }
        }

        public void Conectar_Checador(int ID, string IP, int Puerto)
        {
            try
            {
                //SE CREA UNA VARIABLE CON EL METODO CONECTAR DEL OBJETO CHECADOR.
                //SE ENVIAN COMO PARAMETROS LA IP DEL CHECADOR Y EL PUERTO
                bool bConn = Checador.Connect_Net(IP, Puerto);

                if (bConn == true)
                {
                    //SE ACTIVA EL DISPOSITIVO. PARAMETRO EL NUM. DE MAQUINA Y UNA BANDERA
                    Checador.EnableDevice(ID, true);
                }
                else
                {
                    //ATENCION CAMBIAR ESTE MENSAJE A LA CONSOLA PARA MAYOR COMODIDAD
                    MessageBox.Show("Dispositivo no conectado");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_fecha_manual_Click(object sender, EventArgs e)
        {
            try
            {
                //FUNCION PARA SINCRONIZAR LA FECHA Y HORA DEL CHECADOR MANUALMENTE
                var row = dgv_checadorbuscar.CurrentRow;
                Conectar_Checador(Convert.ToInt32(row.Cells[0].Value), row.Cells[2].Value.ToString(), Convert.ToInt32(row.Cells[3].Value));
                Checador.SetDeviceTime2(Convert.ToInt32(row.Cells[0].Value), dtp_fecha.Value.Year, dtp_fecha.Value.Month, dtp_fecha.Value.Day, dtp_hora.Value.Hour, dtp_hora.Value.Minute, dtp_hora.Value.Second);
                MessageBox.Show("Sincronizado");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ocurrió un error al sincronizar la fecha y hora del checador. Intenta de nuevo.");
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_scr_eventos_Click(object sender, EventArgs e)
        {
            try
            {
                //FUNCION PARA SINCRONIZAR LOS EVENTOS DEL CHECADOR A LA BASE DE DATOS
                var row = dgv_checadorbuscar.CurrentRow;
                Conectar_Checador(Convert.ToInt32(row.Cells[0].Value), row.Cells[2].Value.ToString(), Convert.ToInt32(row.Cells[3].Value));
                string id = string.Empty;
                int verifyMode = 0, inOutMode = 0, workCode = 0, Error = 0;
                int Year = 0, Month = 0, Day = 0, Hour = 0, Minute = 0, Second = 0;

                if (Checador.ReadGeneralLogData(Convert.ToInt32(row.Cells[0].Value)))//read all the attendance records to the memory
                {
                    while (Checador.SSR_GetGeneralLogData(Convert.ToInt32(row.Cells[0].Value), out id, out verifyMode,
                               out inOutMode, out Year, out Month, out Day, out Hour, out Minute, out Second, ref workCode))//get records from the memory
                    {
                        //MessageBox.Show(verifyMode.ToString());
                        MessageBox.Show(id + "-" + verifyMode.ToString() + "-" + inOutMode.ToString() + "-" + Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + "-" + Minute.ToString() + "-" + Second.ToString());
                    }
                }
                else
                {
                    Checador.GetLastError(ref Error);
                    MessageBox.Show(Error.ToString());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ocurrió un error al sincronizar la fecha y hora del checador. Intenta de nuevo.");
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
