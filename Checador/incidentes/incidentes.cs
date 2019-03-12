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
        formularios_padres.mensaje_error frm_error = new formularios_padres.mensaje_error();
        validacion validar = new validacion();

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
                    vistaEmpleadosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and estatus = 'A'";
                }

                //*************   HACER FORMULARIO RESPONSIVO   *****************
                double porcentaje_ancho = (Convert.ToDouble(Width) / 1362);
                double porcentaje_alto = (Convert.ToDouble(Height) / 741);

                foreach (Control x in this.Controls)
                {
                    if (x.HasChildren)
                    {
                        foreach (Control y in x.Controls)
                        {
                            if (y.HasChildren)
                            {
                                foreach (Control z in y.Controls)
                                {
                                    if (z.HasChildren)
                                    {
                                        foreach (Control w in z.Controls)
                                        {
                                            if (w is TextBox | w is Label | w is Button | w is MaskedTextBox | w is DateTimePicker | w is ComboBox | w is RadioButton | w is PictureBox | w is GroupBox | w is DataGridView | w is NumericUpDown)
                                            {
                                                if ((w is Label & porcentaje_ancho <= 0.8) | (w is DateTimePicker & porcentaje_ancho <= 0.8) | (w is NumericUpDown & porcentaje_ancho <= 0.8) | (w is Button & porcentaje_ancho <= 0.8) | (w is ComboBox & porcentaje_ancho <= 0.8) | (w is RadioButton & porcentaje_ancho <= 0.8))
                                                {
                                                    if (w is Label)
                                                    {
                                                        w.Font = new Font("Microsoft Sans Serif", 10);
                                                    }
                                                    w.Font = new Font("Microsoft Sans Serif", 11);
                                                }
                                                double posicionx = Convert.ToDouble(w.Location.X) * porcentaje_ancho;
                                                double posiciony = Convert.ToDouble(w.Location.Y) * porcentaje_alto;
                                                double ancho = w.Width * porcentaje_ancho;
                                                double alto = w.Height * porcentaje_alto;

                                                w.Left = Convert.ToInt32(posicionx);
                                                w.Top = Convert.ToInt32(posiciony);
                                                w.Width = Convert.ToInt32(ancho);
                                                w.Height = Convert.ToInt32(alto);
                                            }
                                        }
                                    }
                                    if (z is TextBox | z is Label | z is Button | z is MaskedTextBox | z is DateTimePicker | z is ComboBox | z is RadioButton | z is PictureBox | z is GroupBox | z is DataGridView)
                                    {

                                        if ((z is Label & porcentaje_ancho <= 0.8) | (z is Button & porcentaje_ancho <= 0.8) | (z is ComboBox & porcentaje_ancho <= 0.8) | (z is RadioButton & porcentaje_ancho <= 0.8))
                                        {
                                            z.Font = new Font("Microsoft Sans Serif", 11);
                                        }

                                        double posicionx = Convert.ToDouble(z.Location.X) * porcentaje_ancho;
                                        double posiciony = Convert.ToDouble(z.Location.Y) * porcentaje_alto;
                                        double ancho = z.Width * porcentaje_ancho;
                                        double alto = z.Height * porcentaje_alto;

                                        z.Left = Convert.ToInt32(posicionx);
                                        z.Top = Convert.ToInt32(posiciony);
                                        z.Width = Convert.ToInt32(ancho);
                                        z.Height = Convert.ToInt32(alto);
                                    }
                                }
                            }
                            if (y is TextBox | y is Label | y is Button | y is MaskedTextBox | y is DateTimePicker | y is ComboBox | y is RadioButton | y is Panel | y is TabControl)
                            {
                                if ((y is Button & porcentaje_ancho <= 0.8) | (y is RadioButton & porcentaje_ancho <= 0.8))
                                {
                                    y.Font = new Font("Microsoft Sans Serif", 12);
                                }

                                double posicionx = Convert.ToDouble(y.Location.X) * porcentaje_ancho;
                                double posiciony = Convert.ToDouble(y.Location.Y) * porcentaje_alto;
                                double ancho = y.Width * porcentaje_ancho;
                                double alto = y.Height * porcentaje_alto;

                                y.Left = Convert.ToInt32(posicionx);
                                y.Top = Convert.ToInt32(posiciony);
                                y.Width = Convert.ToInt32(ancho);
                                y.Height = Convert.ToInt32(alto);
                            }
                        }
                    }
                    if (x is TextBox | x is Label | x is Button | x is MaskedTextBox | x is DateTimePicker | x is ComboBox | x is RadioButton | x is Panel | x is TabControl)
                    {
                        double posicionx = Convert.ToDouble(x.Location.X) * porcentaje_ancho;
                        double posiciony = Convert.ToDouble(x.Location.Y) * porcentaje_alto;
                        double ancho = x.Width * porcentaje_ancho;
                        double alto = x.Height * porcentaje_alto;

                        x.Left = Convert.ToInt32(posicionx);
                        x.Top = Convert.ToInt32(posiciony);
                        x.Width = Convert.ToInt32(ancho);
                        x.Height = Convert.ToInt32(alto);
                    }
                }
                //********************************************************************************************

                DateTime fecha_actual = DateTime.Now;
                //FILTRAR COMBOBOX DE EMPLEADOS EN REGISTRAR CHEQUEO DEPENDIENDO LA SUCURSAL
                if (Program.rol != "ENCARGADA DE TIENDA")
                {
                    this.vista_registrosTableAdapter.Fill(this.dataSet_Checador.vista_registros);
                    //FILTRAR DATAGRID POR MES EN REGISTRAR CHEQUEO

                    if (fecha_actual.Day >= 11 & fecha_actual.Day <= 25)
                    {
                        vistaregistrosBindingSource.Filter = "(CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-11', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-25', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-11', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-25', 'System.DateTime'))";
                    }
                    else
                    {
                        if (fecha_actual.AddMonths(-1).Month == 12)
                        {
                            if (fecha_actual.Day < 10)
                            {
                                vistaregistrosBindingSource.Filter = "(CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.AddYears(-1).Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.AddYears(-1).Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime'))";
                            }
                            else
                            {
                                vistaregistrosBindingSource.Filter = "(CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.AddYears(-1).Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.AddYears(-1).Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime'))";
                            }
                        }
                        else
                        {
                            if (fecha_actual.Day < 10)
                            {
                                vistaregistrosBindingSource.Filter = "(CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime'))";
                            }
                            else
                            {
                                vistaregistrosBindingSource.Filter = "(CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime'))";
                            }
                        }
                    }
                }
                else
                {
                    //FILTRAR DATAGRID POR MES EN REGISTRAR CHEQUEO
                    if (fecha_actual.Day >= 11 & fecha_actual.Day <= 25)
                    {
                        vistaregistrosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and ((CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-11', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-25', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-11', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-25', 'System.DateTime')))";
                    }
                    else
                    {
                        if (fecha_actual.AddMonths(-1).Month == 12)
                        {
                            if (fecha_actual.Day < 10)
                            {
                                vistaregistrosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and ((CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.AddYears(-1).Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.AddYears(-1).Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')))";
                            }
                            else
                            {
                                vistaregistrosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and ((CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.AddYears(-1).Year + "-" + fecha_actual.Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(1).Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.AddYears(-1).Year + "-" + fecha_actual.Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(1).Month + "-09', 'System.DateTime')))";
                            }
                        }
                        else
                        {
                            if (fecha_actual.Day < 10)
                            {
                                vistaregistrosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and ((CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')))";
                            }
                            else
                            {
                                vistaregistrosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and ((CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(1).Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(1).Month + "-09', 'System.DateTime')))";
                            }
                        }
                    }
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
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
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
                    vistaEmpleadosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and estatus = 'A'";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "CONVERT([id_empleado], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre_completo] LIKE '*" + txt_nombrebuscar.Text + "*' and sucursal ='" + Program.sucursal + "' and estatus = 'A'";
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
                    vistaEmpleadosBindingSource.Filter = "sucursal ='" + Program.sucursal + "' and estatus = 'A'";
                }
                else
                {
                    vistaEmpleadosBindingSource.Filter = "CONVERT([id_empleado], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre_completo] LIKE '*" + txt_nombrebuscar.Text + "*' and sucursal ='" + Program.sucursal + "' and estatus = 'A'";
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
            DateTime fecha_actual = DateTime.Now;
            //FILTRAR COMBOBOX DE EMPLEADOS EN REGISTRAR CHEQUEO DEPENDIENDO LA SUCURSAL
            if (Program.rol != "ENCARGADA DE TIENDA")
            {
                this.vista_registrosTableAdapter.Fill(this.dataSet_Checador.vista_registros);
                //FILTRAR DATAGRID POR MES EN REGISTRAR CHEQUEO
                if (fecha_actual.Day >= 11 & fecha_actual.Day <= 25)
                {
                    vistaregistrosBindingSource.Filter = "[nombre_completo] LIKE '*" + txt_buscar.Text + "*' and ((CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-11', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-25', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-11', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-25', 'System.DateTime')))";
                }
                else
                {
                    if (fecha_actual.Day < 10)
                    {
                        vistaregistrosBindingSource.Filter = "[nombre_completo] LIKE '*" + txt_buscar.Text + "*' and ((CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')))";
                    }
                    else
                    {
                        vistaregistrosBindingSource.Filter = "[nombre_completo] LIKE '*" + txt_buscar.Text + "*' and ((CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(1).Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(1).Month + "-09', 'System.DateTime')))";
                    }
                }
            }
            else
            {
                //FILTRAR DATAGRID POR MES EN REGISTRAR CHEQUEO
                if (fecha_actual.Day >= 11 & fecha_actual.Day <= 25)
                {
                    vistaregistrosBindingSource.Filter = "[nombre_completo] LIKE '*" + txt_buscar.Text + "*' and sucursal ='" + Program.sucursal + "' and ((CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-11', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-25', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-11', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-25', 'System.DateTime')))";
                }
                else
                {
                    if (fecha_actual.Day < 10)
                    {
                        vistaregistrosBindingSource.Filter = "[nombre_completo] LIKE '*" + txt_buscar.Text + "*' and sucursal ='" + Program.sucursal + "' and ((CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(-1).Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-09', 'System.DateTime')))";
                    }
                    else
                    {
                        vistaregistrosBindingSource.Filter = "[nombre_completo] LIKE '*" + txt_buscar.Text + "*' and sucursal ='" + Program.sucursal + "' and ((CONVERT([fecha_entrada], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-26', 'System.DateTime') and CONVERT([fecha_entrada], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(1).Month + "-09', 'System.DateTime')) or (CONVERT([fecha_salida], 'System.DateTime') >= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.Month + "-26', 'System.DateTime') and CONVERT([fecha_salida], 'System.DateTime') <= CONVERT('" + fecha_actual.Year + "-" + fecha_actual.AddMonths(1).Month + "-09', 'System.DateTime')))";
                    }
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
                    
                    Horario.verificar_existencia(Convert.ToInt32(cbx_horario.SelectedValue));

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
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
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

                //CARGAR LOS DATOS DEL HORARIO PERTENECIENTE SEGUN EL EVENTO DE REFERENCIA
                AsignarHorario.verificarHorario_Evento(Convert.ToInt32(row.Cells[0].Value.ToString()), fecha_referencia); //SE MANDA EL ID DEL EMPLEADO COMO PARAMETRO

                //EN HORARIO.LUNES SE GUARDA EL ID DEL HORARIO DEL DIA
                Horario.verificar_existencia(AsignarHorario.lunes);

                if (Clase_Checador.RegistrarChequeo(Clase_Checador.id, Convert.ToInt32(row.Cells[0].Value), Sucursal.id, fecha_evento, fecha_referencia, Horario.hr_entrada, Horario.hr_salida, Horario.hora_entrada_descanso, Horario.hora_salida_descanso, Horario.tolerancia, tipo_evento, Horario.id))
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
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                frm_error = new formularios_padres.mensaje_error();
                frm_error.lbl_info.Text = "Upps.. Ocurrió un error";
                frm_error.txt_error.Text = (ex.Message.ToString());
                frm_error.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                frm_error.ShowDialog();
            }
        }

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;

        }

        private void txt_idbuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }
    }
}
