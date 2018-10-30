using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checador.incidentes
{
    public partial class incidentes : Checador.formularios_padres.formpadre
    {
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
            // TODO: This line of code loads data into the 'dataSet_Checador.vista_registros' table. You can move, or remove it, as needed.
            this.vista_registrosTableAdapter.Fill(this.dataSet_Checador.vista_registros);
            // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.horarios' Puede moverla o quitarla según sea necesario.
            this.horariosTableAdapter.Fill(this.dataSet_Checador.horarios);
            // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_Empleados' Puede moverla o quitarla según sea necesario.
            this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

////////////////FILTRAR EL BUSCAR//////////////////////////
        private void txt_idbuscar_TextChanged(object sender, EventArgs e)
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

        private void txt_nombrebuscar_TextChanged(object sender, EventArgs e)
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
<<<<<<< HEAD

        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage2;

        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage1;

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txt_buscar_TextChanged(object sender, EventArgs e)
        {

        }
        //////////////////////////////////////////////////////////////////////
=======
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        



>>>>>>> 138bc1ea1dc8f8f5a9f67852bd6d00a48864b536
    }
}
