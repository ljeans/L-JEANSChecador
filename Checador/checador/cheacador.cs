using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Checador
{
    public partial class cheacador : Checador.formularios_padres.formpadre
    {
        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE CHECADOR
        ClaseChecador Clase_Checador = new ClaseChecador();
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();
        validacion validar = new validacion();
        public bool respuesta = false;

        //OBJETO DE LA CLASSE CKEM (SDK) PARA PODER ACCEDER A METODOS Y ATRIBUTOS
        public zkemkeeper.CZKEM Checador = new zkemkeeper.CZKEM();

        //VARIABLE DE CONEXION DEL CHECADOR
        bool bConn;

        //CHECKBOX DE MARCAR TODOS EN EL DATAGRID DEL CHECADOR
        CheckBox HeaderCheckBox = new CheckBox();

        public cheacador()
        {
            InitializeComponent();
            //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
            //CheckForIllegalCrossThreadCalls = false;
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

        private void responder(object sender, EventArgs e)
        {
            Enabled = true;
            respuesta = confirmacion.respuesta;

            if (respuesta == true)
            {
                Clase_Checador.Modificar_Checador();
                Limpiar();
                tabControlBase.SelectedTab = tabPage2;
               
            }
            else
            {

            }
            confirmacion = null;

        }

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;

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
                confirmacion = new formularios_padres.Mensajes();
                confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea modificar el checador?";
                confirmacion.FormClosed += new FormClosedEventHandler(responder);
                confirmacion.Show();
                Enabled = false;
             
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
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Checador no registrado. Por favor intente de nuevo.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();

               
            }

        }

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
            tabControlBase.SelectedTab = tabPage3;
            HeaderCheckBox.Checked = false;
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
            catch (Exception ex)
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

            //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
            //CheckForIllegalCrossThreadCalls = false;

            //CAMBIAR LA LETRA AL DATAGRIDVIEW
            dgv_checador.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgv_checador.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            //AGREGAR EL CHECKBOX MARCAR TODOS DEL DATAGRID
            AgregarCheckBox();
            HeaderCheckBox.MouseClick += new MouseEventHandler(CheckBox_MarcarTodos_Click);

            DateTime dateValue = new DateTime(2018, 10, 22);
            //MessageBox.Show(dateValue.DayOfWeek.ToString());
        }

        //AGREGAR CHECHBOX DE MARCAR TODOS
        private void AgregarCheckBox()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 0;
            toolTip.ReshowDelay = 0;
            toolTip.ShowAlways = true;
            
            HeaderCheckBox.Size = new Size(15, 15);
            HeaderCheckBox.Location = new Point(15, 5);
            toolTip.SetToolTip(HeaderCheckBox, "Marcar todos");
            this.dgv_checadorbuscar.Controls.Add(HeaderCheckBox);

        }

        private void btn_scr_fecha_Click(object sender, EventArgs e)
        {
            //PARA PROGRESS BAR
            using (formularios_padres.ProgressBar frm = new formularios_padres.ProgressBar(sincronizar_fechaHora))
            {
                frm.lbl_mensaje.Text = "Sincronizando Fecha/Hora..";
                frm.ShowDialog(this);
            }
        }

        //FUNCION PARA RECORRER TODAS LAS FILAS DEL DATAGRID Y SABER CUALES ESTÁN MARCADAS
        /*private void prueba_check()
        {
            int x = 1;
            foreach (DataGridViewRow dataGridRow in dgv_checadorbuscar.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridRow.Cells[0];
                chk.Value = true;

                if (dataGridRow.Cells["Check"].Value != null)
                {
                    MessageBox.Show("Columna " + x +" checada");
                }
                x = x + 1;
            }
        }*/

        private void sincronizar_fechaHora()
        {
            try
            {
                //CODIGO PARA APLICAR LA FUNCION A MULTIPLES CHECADORES
                foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
                {
                    //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                    if (row.Cells["Check"].Value != null)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                        {
                            //FUNCION PARA SINCRONIZAR LA FECHA Y HORA DEL CHECADOR CON LA DEL SERVIDOR
                            //var row = dgv_checadorbuscar.CurrentRow;
                            Conectar_Checador(Convert.ToInt32(row.Cells[1].Value), row.Cells[3].Value.ToString(), Convert.ToInt32(row.Cells[4].Value));
                            if (bConn)
                            {
                                Checador.SetDeviceTime(Convert.ToInt32(row.Cells[0].Value));
                                MessageBox.Show("Sincronizado. Checador: " + Convert.ToInt32(row.Cells[1].Value));
                            }
                            //Checador.SetDeviceTime(Convert.ToInt32(row.Cells[0].Value));
                        }
                    }
                }
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
                bConn = Checador.Connect_Net(IP, Puerto);

                if (bConn == true)
                {
                    //SE ACTIVA EL DISPOSITIVO. PARAMETRO EL NUM. DE MAQUINA Y UNA BANDERA
                    Checador.EnableDevice(ID, true);
                }
                else
                {
                    //ATENCION CAMBIAR ESTE MENSAJE A LA CONSOLA PARA MAYOR COMODIDAD
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Dispositivo no conectado";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.Show();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_fecha_manual_Click(object sender, EventArgs e)
        {
            //PARA PROGRESS BAR
            using (formularios_padres.ProgressBar frm = new formularios_padres.ProgressBar(aplicar_fecha_manual))
            {
                frm.lbl_mensaje.Text = "Aplicando Fecha/Hora..";
                frm.ShowDialog(this);
            }
        }

        private void aplicar_fecha_manual()
        {

            try
            {
                // CODIGO PARA APLICAR LA FUNCION A MULTIPLES CHECADORES
                foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
                {
                    //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                    if (row.Cells["Check"].Value != null)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                        {
                            //FUNCION PARA SINCRONIZAR LA FECHA Y HORA DEL CHECADOR MANUALMENTE
                            Conectar_Checador(Convert.ToInt32(row.Cells[1].Value), row.Cells[3].Value.ToString(), Convert.ToInt32(row.Cells[4].Value));
                            if (bConn)
                            {
                                Checador.SetDeviceTime2(Convert.ToInt32(row.Cells[1].Value), dtp_fecha.Value.Year, dtp_fecha.Value.Month, dtp_fecha.Value.Day, dtp_hora.Value.Hour, dtp_hora.Value.Minute, dtp_hora.Value.Second);
                                MessageBox.Show("Sincronizado. Checador: " + Convert.ToInt32(row.Cells[1].Value));
                            }
                        }
                    }
                }
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
                //PARA PROGRESS BAR
                using (formularios_padres.ProgressBar frm = new formularios_padres.ProgressBar(sincronizar_eventos))
                {
                    frm.lbl_mensaje.Text = "Sincronizando eventos..";
                    frm.ShowDialog(this);
                }

                //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
                /*Thread hilo_secundario = new Thread(new ThreadStart(this.progressbar));
                hilo_secundario.IsBackground = true;
                hilo_secundario.Start();*/
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ocurrió un error al borrar los eventos del checador. Intenta de nuevo.");
                MessageBox.Show(ex.ToString());
            }

        }

        private void sincronizar_eventos()
        {
            //VARIABLE PARA SABER SI SE SINCRONIZARON EVENTOS DE ALGUN CHECADOR
            bool sincronizar = false;

            //FUNCION PARA SINCRONIZAR EVENTOS DE MULTIPLES CHECADORES
            foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
            {
                //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                if (row.Cells["Check"].Value != null)
                {
                    if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                    {
                        try
                        {
                            //FUNCION PARA SINCRONIZAR LOS EVENTOS DEL CHECADOR A LA BASE DE DATOS
                            //var row = dgv_checadorbuscar.CurrentRow;
                            Conectar_Checador(Convert.ToInt32(row.Cells[1].Value), row.Cells[3].Value.ToString(), Convert.ToInt32(row.Cells[4].Value));
                            if (bConn)
                            {
                                string id = string.Empty;
                                int verifyMode = 0, inOutMode = 0, workCode = 0, id_horario;
                                int Year = 0, Month = 0, Day = 0, Hour = 0, Minute = 0, Second = 0;
                                DateTime fecha_max;
                                ClaseSucursal Sucursal = new ClaseSucursal();
                                ClaseEmpleado Empleado = new ClaseEmpleado();
                                ClaseHorario Horario = new ClaseHorario();
                                ClaseAsignar_Horario AsignarHorario = new ClaseAsignar_Horario();

                                if (Checador.ReadGeneralLogData(Convert.ToInt32(row.Cells[1].Value)))//read all the attendance records to the memory
                                {
                                    fecha_max = Clase_Checador.verificarEvento(Convert.ToInt32(row.Cells[1].Value));
                                    while (Checador.SSR_GetGeneralLogData(Convert.ToInt32(row.Cells[1].Value), out id, out verifyMode,
                                               out inOutMode, out Year, out Month, out Day, out Hour, out Minute, out Second, ref workCode))//get records from the memory
                                    {
                                        //VALIDACION PARA SABER DESDE DONDE VAMOS A JALAR LOS EVENTOS DEL CHECADOR [SE BORRARA DESPUES!!]

                                        if (fecha_max < Convert.ToDateTime(Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString()))
                                        {
                                            //CARGAR LOS DATOS DEL HORARIO PERTENECIENTE A UN EMPLEADO
                                            Sucursal.obtenerIdSucursal(row.Cells[2].Value.ToString());
                                            //Empleado.obtenerIdHorario(Convert.ToInt32(id));
                                            
                                            AsignarHorario.verificar_existencia(Convert.ToInt32(id));
                                            DateTime dia = new DateTime(Year, Month, Day);

                                            if (dia.DayOfWeek.ToString() == "Monday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.lunes);
                                            }
                                            else if(dia.DayOfWeek.ToString() == "Tuesday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.martes);
                                            }
                                            else if (dia.DayOfWeek.ToString() == "Wednesday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.miercoles);
                                            }
                                            else if (dia.DayOfWeek.ToString() == "Thursday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.jueves);
                                            }
                                            else if (dia.DayOfWeek.ToString() == "Friday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.viernes);
                                            }
                                            else if (dia.DayOfWeek.ToString() == "Saturday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.sabado);
                                            }
                                            else if (dia.DayOfWeek.ToString() == "Sunday")
                                            {
                                                Horario.verificar_existencia(AsignarHorario.domingo);
                                            }

                                            //Horario.verificar_existencia(Empleado.id_horario);
                                            Clase_Checador.guardarEvento(Convert.ToInt32(row.Cells[1].Value), Convert.ToInt32(id), Sucursal.id, Convert.ToDateTime(Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString()), Horario.hr_entrada, Horario.hr_salida, Horario.hora_entrada_descanso, Horario.hora_salida_descanso, Horario.tolerancia, inOutMode, Horario.horario);

                                            //Agregar fila en DataGrid de datos sincronizados
                                            agregarFila(Convert.ToString(row.Cells[1].Value), Sucursal.id.ToString(), Convert.ToString(id), Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString(), inOutMode);
                                        }
                                        else if (fecha_max == Convert.ToDateTime("1995-12-12 00:00:00"))
                                        {
                                            //CARGAR LOS DATOS DEL HORARIO PERTENECIENTE A UN EMPLEADO
                                            Sucursal.obtenerIdSucursal(row.Cells[2].Value.ToString());
                                            Empleado.obtenerIdHorario(Convert.ToInt32(id));
                                            Horario.verificar_existencia(Empleado.id_horario);
                                            Clase_Checador.guardarEvento(Convert.ToInt32(row.Cells[1].Value), Convert.ToInt32(id), Sucursal.id, Convert.ToDateTime(Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString()), Horario.hr_entrada, Horario.hr_salida, Horario.hora_entrada_descanso, Horario.hora_salida_descanso, Horario.tolerancia, inOutMode, Horario.horario);
                                            agregarFila(Convert.ToString(row.Cells[1].Value), Sucursal.id.ToString(), Convert.ToString(id), Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString() + "  " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString(), inOutMode);
                                        }
                                    }
                                    MessageBox.Show("Eventos Sincronizados con exito. Checador: " + Convert.ToInt32(row.Cells[1].Value));
                                    sincronizar = true;

                                }
                                else
                                {
                                    /*Checador.GetLastError(ref Error);
                                    MessageBox.Show(Error.ToString());*/
                                }
                                /*mensaje = new formularios_padres.mensaje_info();
                                mensaje = new formularios_padres.mensaje_info();
                                mensaje.lbl_info.Text = "Eventos sincronizados con exito.";
                                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                                mensaje.Show();*/

                            }
                            else
                            {
                                /*Checador.GetLastError(ref Error);
                                MessageBox.Show(Error.ToString());*/
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

            if (sincronizar)
            {
                //CONDICION PARA INVOCAR EL DATAGRID DESDE OTRO HILO
                if (InvokeRequired)
                {
                    Invoke(new Action(() => tabControlBase.SelectedTab = tabPage4));
                }
            }
        }

        //Funcion para agregar filas al DATAGRID de eventos sincronizados
        private void agregarFila(string id_checador, string id_sucursal, string id_empleado, string fecha_evento, int tipo_evento)
        {
            DataGridViewRow fila = new DataGridViewRow();
            fila.CreateCells(dgv_eventos_sincronizados);
            fila.Cells[0].Value = id_checador;
            fila.Cells[1].Value = id_empleado;
            fila.Cells[2].Value = id_sucursal;
            fila.Cells[3].Value = fecha_evento;
            if (tipo_evento == 0)
            {
                fila.Cells[4].Value = "ENTRADA";
            }
            else if(tipo_evento == 1)
            {
                fila.Cells[4].Value = "SALIDA";
            }

            //CONDICION PARA INVOCAR EL DATAGRID DESDE OTRO HILO
            if (InvokeRequired)
            {
                Invoke(new Action(()=> dgv_eventos_sincronizados.Rows.Add(fila)));
            }
        }

        private void btn_borrar_eventos_Click(object sender, EventArgs e)
        {
            //PARA PROGRESS BAR
            using (formularios_padres.ProgressBar frm = new formularios_padres.ProgressBar(borrar_eventos))
            {
                frm.lbl_mensaje.Text = "Borrando eventos..";
                frm.ShowDialog(this);
            }
        }

        private void borrar_eventos()
        {
            try
            {
                //CODIGO PARA APLICAR LA FUNCION A MULTIPLES CHECADORES
                foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
                {
                    //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                    if (row.Cells["Check"].Value != null)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                        {
                            //FUNCION PARA BORRAR TODOS LOS EVENTOS DE UN CHECADOR                
                            Conectar_Checador(Convert.ToInt32(row.Cells[1].Value), row.Cells[3].Value.ToString(), Convert.ToInt32(row.Cells[4].Value));
                            if (bConn)
                            {
                                Checador.ClearGLog(Convert.ToInt32(row.Cells[1].Value));
                                MessageBox.Show("Eventos eliminados con exito. Checador: " + Convert.ToInt32(row.Cells[1].Value));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ocurrió un error al borrar los eventos del checador. Intenta de nuevo.");
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_borrar_usuarios_Click(object sender, EventArgs e)
        {
            //PARA PROGRESS BAR
            using (formularios_padres.ProgressBar frm = new formularios_padres.ProgressBar(borrar_eventos))
            {
                frm.lbl_mensaje.Text = "Borrando usuarios..";
                frm.ShowDialog(this);
            }
        }

        private void borrar_usuarios()
        {
            try
            {
                //CODIGO PARA APLICAR LA FUNCION A MULTIPLES CHECADORES
                foreach (DataGridViewRow row in dgv_checadorbuscar.Rows)
                {
                    //VALIDAR LAS FILAS MARCADAS EN EL DATAGRID
                    if (row.Cells["Check"].Value != null)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) != false)
                        {
                            //FUNCION PARA BORRAR TODOS LOS USUARIOS DE UN CHECADOR
                            int id_checador = Convert.ToInt32(row.Cells[1].Value);

                            Conectar_Checador(id_checador, row.Cells[3].Value.ToString(), Convert.ToInt32(row.Cells[4].Value));

                            if (bConn)
                            {
                                if (Checador.ClearData(id_checador, 5))
                                {
                                    MessageBox.Show("Usuarios eliminados con exito. Checador: " + Convert.ToInt32(row.Cells[1].Value));
                                }
                                else
                                {
                                    MessageBox.Show("No existen usuarios en el Checador: " + Convert.ToInt32(row.Cells[1].Value));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ocurrió un error al borrar los usuarios del checador. Intenta de nuevo.");
                MessageBox.Show(ex.ToString());
            }
        }

////////////////FILTRAR EL BUSCAR//////////////////////////
        private void txt_nombrebuscar_TextChanged(object sender, EventArgs e)
        {
            if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'A'";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'I'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'A'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'I'";
            }
            else
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*'";
            }
        }        

        

        private void cb_buscarActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'A'";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'I'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'A'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'I'";
            }
            else
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*'";
            }
        }

        private void cb_buscarInactivo_CheckedChanged(object sender, EventArgs e)
        {
            if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'A'";
            }
            else if (txt_buscar.Text == "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                this.vista_ChecadorTableAdapter.Fill(this.dataSet_Checador.Vista_Checador);
                vistaChecadorBindingSource.Filter = "[estatus] = 'I'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == true && cb_buscarInactivo.Checked == false)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'A'";
            }
            else if (txt_buscar.Text != "" && cb_buscarActivo.Checked == false && cb_buscarInactivo.Checked == true)
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*' and [estatus] = 'I'";
            }
            else
            {
                vistaChecadorBindingSource.Filter = "CONVERT([id_checador], 'System.String') LIKE " + "'" + txt_buscar.Text + "*'";
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////

        //EVENTO CLICK EN MARCAR TODOS
        private void CheckBox_MarcarTodos_Click(object sender, MouseEventArgs e)
        {
            if (HeaderCheckBox.Checked == true)
            {
                foreach (DataGridViewRow dataGridRow in dgv_checadorbuscar.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridRow.Cells[0];
                    chk.Value = true;
                    dgv_checadorbuscar.RefreshEdit();
                }
            }
            else
            {
                foreach (DataGridViewRow dataGridRow in dgv_checadorbuscar.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridRow.Cells[0];
                    chk.Value = false;
                    dgv_checadorbuscar.RefreshEdit();
                }
            }
        }

        private void Regresar_Click(object sender, EventArgs e)
        {
            //LIMPIAR DATOS DEL DATAGRID
            dgv_eventos_sincronizados.Rows.Clear();
            dgv_eventos_sincronizados.Refresh();
            tabControlBase.SelectedTab = tabPage3;
        }

        private void txt_ip_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.soloimportes(e);
        }
    }
}
