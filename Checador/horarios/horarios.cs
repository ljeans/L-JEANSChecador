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
        ClaseHorario Horario = new ClaseHorario();
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();



        public bool respuesta = false;

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
                txt_id.Text = (Horario.obtenerIdMaximo() + 1).ToString();
            }
            catch (Exception ex)
            {
                txt_id.Text = "1";
            }
        }
        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;

        }


        private void Limpiar()
        {
 
            txt_id_a_modificar.Text = "";
            txt_nombre.Text = "";
            txt_horas_diarias.Text = "8";
            txt_horas_totales.Text = "96";
            dtp_hora_entrada.Text = "";
            dtp_hora_entrada_desc.Text = "";
            dtp_hora_salida.Text = "";
            dtp_hora_salida_desc.Text = "";
            txt_tolerancia.Text = "10";

            cb_lunes.Checked = true;
            cb_martes.Checked = true;
            cb_miercoles.Checked = true;
            cb_jueves.Checked = true;
            cb_viernes.Checked = true;
            cb_sabado.Checked = true;
            cb_domingo.Checked = true;
            cargarID();

        }

        private void responder(object sender, EventArgs e)
        {
            Enabled = true;
            respuesta = confirmacion.respuesta;

            if (respuesta == true)
            {
                Horario.Modificar_Horario();
                Limpiar();
                tabControlBase.SelectedTab = tabPage2;
            }
            else
            {

            }
            confirmacion = null;

        }

        // MODIFICAR///////////////////////////////////////////////////////////////////////////////////
        private void btn_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                Horario.id = Convert.ToInt32(txt_id.Text);
                Horario.horario = txt_nombre.Text;
                Horario.horas_diarias = Convert.ToInt32(txt_horas_diarias.Text);
                Horario.horas_totales_quincenales = Convert.ToInt32(txt_horas_totales.Text);
                Horario.hora_entrada_descanso = dtp_hora_entrada_desc.Value.TimeOfDay;
                Horario.hora_salida_descanso = dtp_hora_salida_desc.Value.TimeOfDay;
                Horario.hr_entrada = dtp_hora_entrada.Value.TimeOfDay;
                Horario.hr_salida = dtp_hora_salida.Value.TimeOfDay;
                Horario.tolerancia = Convert.ToInt32(txt_tolerancia.Text);

                if (cb_lunes.Checked == true)
                    Horario.lunes = 1;
                else
                {
                    Horario.lunes = 0;
                }

                if (cb_martes.Checked == true)
                    Horario.martes = 1;
                else
                {
                    Horario.martes = 0;
                }

                if (cb_miercoles.Checked == true)
                    Horario.miercoles = 1;
                else
                {
                    Horario.miercoles = 0;
                }

                if (cb_jueves.Checked == true)
                    Horario.jueves = 1;
                else
                {
                    Horario.jueves = 0;
                }

                if (cb_viernes.Checked == true)
                    Horario.viernes = 1;
                else
                {
                    Horario.viernes = 0;
                }

                if (cb_sabado.Checked == true)
                    Horario.sabado = 1;
                else
                {
                    Horario.sabado = 0;
                }
                if (cb_domingo.Checked == true)
                    Horario.domingo = 1;
                else
                {
                    Horario.domingo = 0;
                }


                confirmacion = new formularios_padres.Mensajes();
                confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea modificar el horario?";
                confirmacion.FormClosed += new FormClosedEventHandler(responder);
                confirmacion.Show();
                Enabled = false;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            Limpiar();

            //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
            CheckForIllegalCrossThreadCalls = false;
            Thread hilo_secundario = new Thread(new ThreadStart(this.cargarID));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
        }

        //FUNCION PARA CARGAR LOS DATOS DEL HORARIO EN EL FORMULARIO
        private void btn_ir_modificar_Click(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage1;
            txt_id.Enabled = false;
            Horario.id = Convert.ToInt32(txt_id_a_modificar.Text);
            if (Horario.verificar_existencia(Horario.id))
            {
                txt_id.Text = Horario.id.ToString();
                txt_nombre.Text = Horario.horario;
                txt_horas_diarias.Value = Horario.horas_diarias;
                txt_horas_totales.Value = Horario.horas_totales_quincenales;
                txt_tolerancia.Value = Horario.tolerancia;
                dtp_hora_entrada.Value = Convert.ToDateTime(Horario.hr_entrada.ToString());

                //VALIDACION PARA NULL EN HORAS DE DESCANSO
                TimeSpan hora_fija = new TimeSpan(00,00,00);
                if(Horario.hora_entrada_descanso != hora_fija)
                {
                    dtp_hora_entrada_desc.Value = Convert.ToDateTime(Horario.hora_entrada_descanso.ToString());
                }
                else
                {
                    cb_descanso.Checked = false;
                    dtp_hora_entrada_desc.Enabled = false;
                }

                if (Horario.hora_entrada_descanso != hora_fija)
                {
                    dtp_hora_salida_desc.Value = Convert.ToDateTime(Horario.hora_salida_descanso.ToString());
                }
                else
                {
                    cb_descanso.Checked = false;
                    dtp_hora_salida_desc.Enabled = false;
                }

               
                dtp_hora_salida.Value = Convert.ToDateTime(Horario.hr_salida.ToString());
                if (Horario.lunes == 1)
                    cb_lunes.Checked = true;
                else
                {
                    cb_lunes.Checked = false;
                }

                if (Horario.martes == 1)
                    cb_martes.Checked = true;
                else
                {
                    cb_martes.Checked = false;
                }

                if (Horario.miercoles == 1)
                    cb_miercoles.Checked = true;
                else
                {
                    cb_miercoles.Checked = false;
                }

                if (Horario.jueves == 1)
                    cb_jueves.Checked = true;
                else
                {
                    cb_jueves.Checked = false;
                }

                if (Horario.viernes == 1)
                    cb_viernes.Checked = true;
                else
                {
                    cb_viernes.Checked = true;
                }

                if (Horario.sabado == 1)
                    cb_sabado.Checked = true;
                else
                {
                    cb_sabado.Checked = false;
                }
                if (Horario.domingo == 1)
                    cb_domingo.Checked = true;
                else
                {
                    cb_domingo.Checked = false;
                }

                btn_registrar.Visible = false;
                btn_modificar.Visible = true;
            }else
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Horario no registrado. Por favor intente de nuevo.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
               
            }
        }

        private void horarios_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Horario' table. You can move, or remove it, as needed.
            this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
            //CAMBIAR LA LETRA AL DATAGRIDVIEW
            dgv_horarios.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgv_horarios.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);



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

///REGISTRAR///////////////////////////////////////////////////////////////////////////////////
        private void btn_registrar_Click(object sender, EventArgs e)
        {
            bool descansoFlag=false;
            try
            {
                Horario.id = Convert.ToInt32(txt_id.Text);
                Horario.horario = txt_nombre.Text;
                Horario.horas_diarias = Convert.ToInt32(txt_horas_diarias.Text);
                Horario.horas_totales_quincenales = Convert.ToInt32(txt_horas_totales.Text);

                if (cb_descanso.Checked == true)
                {
                    descansoFlag = true;
                    Horario.hora_entrada_descanso = dtp_hora_entrada_desc.Value.TimeOfDay;  //izi
                    Horario.hora_salida_descanso = dtp_hora_salida_desc.Value.TimeOfDay;    //pizi
                }
                else
                {
                    descansoFlag = false;
                }

                Horario.hr_entrada = dtp_hora_entrada.Value.TimeOfDay;
                Horario.hr_salida = dtp_hora_salida.Value.TimeOfDay;
                Horario.tolerancia = Convert.ToInt32(txt_tolerancia.Text);


                if (cb_lunes.Checked == true)
                    Horario.lunes = 1;
                else
                {
                    Horario.lunes = 0;
                }

                if (cb_martes.Checked == true)
                    Horario.martes = 1;
                else
                {
                    Horario.martes = 0;
                }

                if (cb_miercoles.Checked == true)
                    Horario.miercoles = 1;
                else
                {
                    Horario.miercoles = 0;
                }

                if (cb_jueves.Checked == true)
                    Horario.jueves = 1;
                else
                {
                    Horario.jueves = 0;
                }

                if (cb_viernes.Checked == true)
                    Horario.viernes = 1;
                else
                {
                    Horario.viernes = 0;
                }

                if (cb_sabado.Checked == true)
                    Horario.sabado = 1;
                else
                {
                    Horario.sabado = 0;
                }
                if (cb_domingo.Checked == true)
                    Horario.domingo = 1;
                else
                {
                    Horario.domingo = 0;
                }

                Horario.guardarHorario(descansoFlag);
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
            tabControlBase.SelectedTab = tabPage3;
        }

        private void cb_descanso_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_descanso.Checked == true)
            {
                dtp_hora_entrada_desc.Enabled = true;
                dtp_hora_salida_desc.Enabled = true;
              
            }
            else if (cb_descanso.Checked == false)
            {
                dtp_hora_entrada_desc.Enabled = false;
                dtp_hora_salida_desc.Enabled = false;
            }
        }

        private void tabControlBase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

////////////////FILTRAR EL BUSCAR//////////////////////////
        private void txt_idbuscar_TextChanged(object sender, EventArgs e)
        {
            if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
            {
                this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
                vistaHorarioBindingSource.Filter = "";
            }
            else
            {
                vistaHorarioBindingSource.Filter = "CONVERT([id_horario], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [horario] LIKE '*" + txt_nombrebuscar.Text + "*'";
            }
        }

        private void txt_nombrebuscar_TextChanged(object sender, EventArgs e)
        {
            if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
            {
                this.vista_HorarioTableAdapter.Fill(this.dataSet_Checador.Vista_Horario);
                vistaHorarioBindingSource.Filter = "";
            }
            else
            {
                vistaHorarioBindingSource.Filter = "CONVERT([id_horario], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [horario] LIKE '*" + txt_nombrebuscar.Text + "*'";
            }
        }
///////////////////////////////////////////////////////////////

    }
}
