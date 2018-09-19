using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Checador
{
    public partial class horarios : Checador.formularios_padres.formpadre
    {
        ClaseHorario horario = new ClaseHorario();


        public horarios()
        {
            InitializeComponent();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void cargarID()
        {
            //MOSTRAR EL ID DEL EMPLEADO AL CARGAR LA PAGINA
            try
            {
                txt_id.Text = (horario.obtenerIdMaximo() + 1).ToString();
            }
            catch (Exception ex)
            {
                txt_id.Text = "1";
            }
        }


        private void Limpiar()
        {
            txt_id.Text = "";
            txt_id_a_modificar.Text = "";
            txt_nombre.Text = "";
            txt_horas_diarias.Text = "";
            txt_horas_totales.Text = "";
            dtp_hora_entrada.Text = "";
            dtp_hora_entrada_desc.Text = "";
            dtp_hora_salida.Text = "";
            dtp_hora_salida_desc.Text = "";
            dtp_tolerancia.Text = "";

            cb_lunes.Checked = false;
            cb_martes.Checked = false;
            cb_miercoles.Checked = false;
            cb_jueves.Checked = false;
            cb_viernes.Checked = false;
            cb_sabado.Checked = false;
            cb_domingo.Checked = false;


        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {

        }

        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
           
            tabControlBase.SelectedTab = tabPage2;
         

        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage1;
            btn_modificar.Visible = false;
            btn_registrar.Visible = true;
            cb_lunes.Checked = true;
            cb_martes.Checked = true;
            cb_miercoles.Checked = true;
            cb_jueves.Checked = true;
            cb_viernes.Checked = true;
            cb_sabado.Checked = true;
            cb_domingo.Checked = true;
        }

        private void btn_ir_modificar_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage1;
            btn_registrar.Visible = false;
            btn_modificar.Visible = true;
        }

        private void horarios_Load(object sender, EventArgs e)
        {
            //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
            CheckForIllegalCrossThreadCalls = false;
            Thread hilo_secundario = new Thread(new ThreadStart(this.cargarID));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
            cb_lunes.Checked = true;
            cb_martes.Checked = true;
            cb_miercoles.Checked = true;
            cb_jueves.Checked = true;
            cb_viernes.Checked = true;
            cb_sabado.Checked = true;
            cb_domingo.Checked = true;
        }

        private void btn_registrar_Click(object sender, EventArgs e)
        {
            try
            {
                horario.id = Convert.ToInt32(txt_id.Text);
                horario.horario = txt_nombre.Text;
                horario.horas_diarias = Convert.ToInt32(txt_horas_diarias.Text);
                horario.horas_totales_quincenales = Convert.ToInt32(txt_horas_totales.Text);
                horario.hora_entrada_descanso = dtp_hora_entrada_desc.Value.TimeOfDay;  //izi
                horario.hora_salida_descanso = dtp_hora_salida_desc.Value.TimeOfDay;    //pizi
                horario.hr_entrada = dtp_hora_entrada.Value.TimeOfDay;
                horario.hr_salida = dtp_hora_salida.Value.TimeOfDay;


                if (cb_lunes.Checked == true)
                    horario.lunes = 1;
                else
                {
                    horario.lunes = 0;
                }

                if (cb_martes.Checked == true)
                    horario.martes = 1;
                else
                {
                    horario.martes = 0;
                }

                if (cb_miercoles.Checked == true)
                    horario.miercoles = 1;
                else
                {
                    horario.miercoles = 0;
                }

                if (cb_jueves.Checked == true)
                    horario.jueves = 1;
                else
                {
                    horario.jueves = 0;
                }

                if (cb_viernes.Checked == true)
                    horario.viernes = 1;
                else
                {
                    horario.viernes = 0;
                }

                if (cb_sabado.Checked == true)
                    horario.sabado = 1;
                else
                {
                    horario.sabado = 0;
                }
                if (cb_domingo.Checked == true)
                    horario.domingo = 1;
                else
                {
                    horario.domingo = 0;
                }

                horario.guardarHorario();
                Limpiar();
            }
            catch (Exception ex)
            {

            }
        }
      
    }
}
