using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Checador.incidentes
{
    public partial class incidentes : Checador.formularios_padres.formpadre
    {
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();

        public incidentes()
        {
            InitializeComponent();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void incidentes_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'dataSet_Checador.vista_registros' table. You can move, or remove it, as needed.
                this.vista_registrosTableAdapter.Fill(this.dataSet_Checador.vista_registros);
                // TODO: This line of code loads data into the 'dataSet_Checador1.vista_registros' table. You can move, or remove it, as needed.
                this.vista_registrosTableAdapter.Fill(this.dataSet_Checador1.vista_registros);
                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.horarios' Puede moverla o quitarla según sea necesario.
                this.horariosTableAdapter.Fill(this.dataSet_Checador.horarios);
                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_Empleados' Puede moverla o quitarla según sea necesario.
                this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);

                //FILTRAR COMBOBOX DE EMPLEADOS EN ASIGNAR RECALCULAR HORAS DEPENDIENDO LA SUCURSAL
                if (Program.rol != "ENCARGADA DE TIENDA")
                {
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                    vistaEmpleadosBindingSource.Filter = "";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "sucursal ='" + Program.sucursal + "'";
                }

                //FILTRAR COMBOBOX DE EMPLEADOS EN REGISTRAR CHEQUEO DEPENDIENDO LA SUCURSAL
                if (Program.rol != "ENCARGADA DE TIENDA")
                {
                    this.vista_registrosTableAdapter.Fill(this.dataSet_Checador.vista_registros);
                    vistaregistrosBindingSource.Filter = "";
                }
                else
                {
                    vistaregistrosBindingSource.Filter = "sucursal ='" + Program.sucursal + "'";
                }

                //FLITRAR CONSULTA HORARIO DEPENDIENDO EL ROL
                if (Program.rol != "ENCARGADA DE TIENDA")
                {
                    this.horariosTableAdapter.Fill(this.dataSet_Checador.horarios);
                    horariosBindingSource.Filter = "";
                }
                else
                {
                    horariosBindingSource.Filter = "lunes ='" + Program.id_empleado + "' or [id_horario] = 0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

////////////////FILTRAR EL BUSCAR//////////////////////////
        private void txt_idbuscar_TextChanged(object sender, EventArgs e)
        {
            if (Program.rol != "ENCARGADA DE TIENDA")
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                    vistaEmpleadosBindingSource.Filter = "";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "CONVERT([id_empleado], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre_completo] LIKE '*" + txt_nombrebuscar.Text + "*'";
                }
            }
            else
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                    vistaEmpleadosBindingSource.Filter = "sucursal ='" + Program.sucursal + "'";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "CONVERT([id_empleado], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre_completo] LIKE '*" + txt_nombrebuscar.Text + "*' and sucursal ='" + Program.sucursal + "'";
                }
            }
        }

        private void txt_nombrebuscar_TextChanged(object sender, EventArgs e)
        {
            if (Program.rol != "ENCARGADA DE TIENDA")
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                    vistaEmpleadosBindingSource.Filter = "";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "CONVERT([id_empleado], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre_completo] LIKE '*" + txt_nombrebuscar.Text + "*'";
                }
            }
            else
            {
                if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
                {
                    this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                    vistaEmpleadosBindingSource.Filter = "sucursal ='" + Program.sucursal + "'";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "CONVERT([id_empleado], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre_completo] LIKE '*" + txt_nombrebuscar.Text + "*' and sucursal ='" + Program.sucursal + "'";
                }
            }
        }

        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
            this.vista_registrosTableAdapter.Fill(this.dataSet_Checador1.vista_registros);
            tabControlBase.SelectedTab = tabPage2;

        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {
            this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
            tabControlBase.SelectedTab = tabPage1;

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            if (Program.rol != "ENCARGADA DE TIENDA")
            {
                if (txt_buscar.Text == "")
                {
                    this.vista_registrosTableAdapter.Fill(this.dataSet_Checador.vista_registros);
                    vistaregistrosBindingSource.Filter = "";
                }
                else
                {
                    vistaregistrosBindingSource.Filter = "[nombre_completo] LIKE '*" + txt_buscar.Text + "*'";
                }
            }
            else
            {
                if (txt_buscar.Text == "")
                {
                    this.vista_registrosTableAdapter.Fill(this.dataSet_Checador.vista_registros);
                    vistaregistrosBindingSource.Filter = "Sucursal ='" + Program.sucursal + "'";
                }
                else
                {
                    vistaregistrosBindingSource.Filter = "[nombre_completo] LIKE '*" + txt_buscar.Text + "*' and Sucursal ='" + Program.sucursal + "'";
                }
            }
        }
        //////////////////////////////////////////////////////////////////////

        //FUNCION PARA RECALCULAS LAS HORAS TRABAJADAS Y LOS RETARDOS
        private void btn_recalcular_Click(object sender, EventArgs e)
        {
            try
            {
                ClaseChecador Clase_Checador = new ClaseChecador();
                ClaseSucursal Sucursal = new ClaseSucursal();
                ClaseHorario Horario = new ClaseHorario();
                ClaseAsignar_Horario AsignarHorario = new ClaseAsignar_Horario();

                var row = dgv_empleados_recalcular.CurrentRow;
                DateTime fecha_inicio = new DateTime(dtp_fechaInicial.Value.Year, dtp_fechaInicial.Value.Month, dtp_fechaInicial.Value.Day);
                DateTime fecha_final = new DateTime(dtp_fechaFinal.Value.Year, dtp_fechaFinal.Value.Month, dtp_fechaFinal.Value.Day);

                for (DateTime fecha_evento= fecha_inicio; fecha_evento <= fecha_final; fecha_evento = fecha_evento.AddDays(1))
                {
                    // OBTENER EL ID DE LA SUCURSAL
                    Sucursal.obtenerIdSucursal(row.Cells[2].Value.ToString());

                    //OBTENER ID CHECADOR
                    Clase_Checador.obtenerIdChecador(Sucursal.id);

                    //CARGAR LOS DATOS DEL HORARIO PERTENECIENTE A UN EMPLEADO
                    AsignarHorario.verificar_existencia(Convert.ToInt32(row.Cells[0].Value.ToString())); //SE MANDA EL ID DEL EMPLEADO COMO PARAMETRO
                    DateTime dia = new DateTime(fecha_evento.Year, fecha_evento.Month, fecha_evento.Day);

                    if (dia.DayOfWeek.ToString() == "Monday")
                    {
                        Horario.verificar_existencia(AsignarHorario.lunes);
                    }
                    else if (dia.DayOfWeek.ToString() == "Tuesday")
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

                    Clase_Checador.Recalcular_HorasTrabajadas(Clase_Checador.id, Convert.ToInt32(row.Cells[0].Value), Sucursal.id, fecha_evento, Horario.hr_entrada, Horario.hr_salida, Horario.hora_entrada_descanso, Horario.hora_salida_descanso, Horario.tolerancia, Horario.horario);
                }
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
                MessageBox.Show(ex.ToString());
            }
        }

        //FUNCION PARA REGISTRAR UN CHEQUEO MANUALMENTE
        private void btn_llenar_null_Click(object sender, EventArgs e)
        {
            try
            {
                ClaseChecador Clase_Checador = new ClaseChecador();
                ClaseSucursal Sucursal = new ClaseSucursal();
                ClaseHorario Horario = new ClaseHorario();
                ClaseAsignar_Horario AsignarHorario = new ClaseAsignar_Horario();
                ClaseEmpleado Empleado = new ClaseEmpleado();

                var row = dgv_rellenar.CurrentRow;
                TimeSpan hora_evento = new TimeSpan(dtp_hora.Value.Hour, dtp_hora.Value.Minute, dtp_hora.Value.Second);
                DateTime fecha_referencia, fecha_evento;
                int tipo_evento;

                //SACAR LA FECHA DEL DATA GRID Y LA FECHA DEL EVENTO CON LA HORA SELECCIONADA POR EL USUARIO
                if (row.Cells[4].Value.ToString() != "")
                {
                    //LO QUE FALTA ES LA SALIDA POR ESO SE PONE 1
                    tipo_evento = 1;
                    fecha_referencia = Convert.ToDateTime(row.Cells[4].Value);
                    fecha_evento = new DateTime(fecha_referencia.Year, fecha_referencia.Month,fecha_referencia.Day, dtp_hora.Value.Hour, dtp_hora.Value.Minute, dtp_hora.Value.Second);
                }
                else
                {
                    //LO QUE FALTA ES LA ENTRADA POR ESO SE PONE 0
                    tipo_evento = 0;
                    fecha_referencia = Convert.ToDateTime(row.Cells[5].Value);
                    fecha_evento = new DateTime(fecha_referencia.Year, fecha_referencia.Month, fecha_referencia.Day, dtp_hora.Value.Hour, dtp_hora.Value.Minute, dtp_hora.Value.Second);
                }

                // OBTENER EL ID DE LA SUCURSAL
                Sucursal.obtenerIdSucursal(row.Cells[3].Value.ToString());

                //OBTENER ID CHECADOR
                Clase_Checador.obtenerIdChecador(Sucursal.id);

                //CARGAR LOS DATOS DEL HORARIO PERTENECIENTE A UN EMPLEADO
                AsignarHorario.verificar_existencia(Convert.ToInt32(row.Cells[0].Value.ToString())); //SE MANDA EL ID DEL EMPLEADO COMO PARAMETRO
                DateTime dia = new DateTime(fecha_evento.Year, fecha_evento.Month, fecha_evento.Day);

                if (dia.DayOfWeek.ToString() == "Monday")
                {
                    Horario.verificar_existencia(AsignarHorario.lunes);
                }
                else if (dia.DayOfWeek.ToString() == "Tuesday")
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

                if (Clase_Checador.RegistrarChequeo(Clase_Checador.id, Convert.ToInt32(row.Cells[0].Value), Sucursal.id, fecha_evento, fecha_referencia, Horario.hr_entrada, Horario.hr_salida, Horario.hora_entrada_descanso, Horario.hora_salida_descanso, Horario.tolerancia, tipo_evento, Horario.horario))
                {
                    //FUNCION PAR RECARGAR EL DATAGRID
                    // TODO: This line of code loads data into the 'dataSet_Checador1.vista_registros' table. You can move, or remove it, as needed.
                    this.vista_registrosTableAdapter.Fill(this.dataSet_Checador.vista_registros);
                }
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
                MessageBox.Show(ex.ToString());
            }
        }

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;

        }
    }
}
