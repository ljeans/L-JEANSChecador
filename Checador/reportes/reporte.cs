using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Threading;

namespace Checador.reportes
{

    #region NewUsing
    using CrystalDecisions.CrystalReports.Engine;
    using CrystalDecisions.Shared;
    #endregion

    public partial class reporte : Checador.formularios_padres.formpadre
    {
        public reporte()
        {
            InitializeComponent();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reporte_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_Empleados' Puede moverla o quitarla según sea necesario.
            this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
            // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.sucursal' Puede moverla o quitarla según sea necesario.
            this.sucursalTableAdapter.Fill(this.dataSet_Checador.sucursal);

            CheckForIllegalCrossThreadCalls = false;

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

        }

        private void btn_gnerar_Click(object sender, EventArgs e)
        {
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.Por_Sucursal));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
        }

        public void Por_Sucursal()
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                ReportDocument crystalrpt = new ReportDocument();
                crystalrpt.Load(@"..\\..\\reportes\Sucursal-Empleados.rpt");

                ParameterFieldDefinitions parameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValue = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                //COLOCAR USUARIO Y CONTRASEÑA PARA CRYSTAL REPORTS
                string username = "sa"; // database user name
                string password = "123456"; //database password
                crystalrpt.SetDatabaseLogon(username, password); //here usaer name and password for crystel report

                //parametro
                crParameterDiscreteValue.Value = cbx_sucursal.SelectedValue;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["id_sucursal"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                //parametros fechas
                crParameterDiscreteValue.Value = dtp_fecha1.Value.ToString("yyyy-MM-dd 00:00:00");
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["fecha1"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                crParameterDiscreteValue.Value = dtp_fecha2.Value.ToString("yyyy-MM-dd 23:59:59"); ;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["fecha2"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                if (InvokeRequired)
                {
                    Invoke(new Action(() => crystalReportViewer1.ReportSource = crystalrpt));
                }
                
                crystalReportViewer1.Refresh();
                

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

        private void btn_retardo_Click(object sender, EventArgs e)
        {
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.Por_Retardos));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
        }

        public void Por_Retardos()
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                ReportDocument crystalrpt = new ReportDocument();
                crystalrpt.Load(@"..\\..\\reportes\retardos.rpt");

                ParameterFieldDefinitions parameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValue = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                //COLOCAR USUARIO Y CONTRASEÑA PARA CRYSTAL REPORTS
                string username = "sa"; // database user name
                string password = "123456"; //database password
                crystalrpt.SetDatabaseLogon(username, password); //here usaer name and password for crystel report

                //parametro
                crParameterDiscreteValue.Value = cbx_sucursal_retardo.SelectedValue;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["id_sucursal"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                //parametros fechas
                crParameterDiscreteValue.Value = dtp_fecha1_retardo.Value.ToString("yyyy-MM-dd"); ;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["fecha1"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                crParameterDiscreteValue.Value = dtp_fecha2_retardo.Value.ToString("yyyy-MM-dd"); ;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["fecha2"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                if (InvokeRequired)
                {
                    Invoke(new Action(() => crystalReportViewer2.ReportSource = crystalrpt));
                }

                crystalReportViewer2.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage1;
        }

        private void btn_asistencia_Click(object sender, EventArgs e)
        {
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.Por_Asistencia));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
        }

        public void Por_Asistencia()
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                ReportDocument crystalrpt = new ReportDocument();
                crystalrpt.Load(@"..\\..\\reportes\asistenciaxempleados.rpt");

                ParameterFieldDefinitions parameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValue = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                //COLOCAR USUARIO Y CONTRASEÑA PARA CRYSTAL REPORTS
                string username = "sa"; // database user name
                string password = "123456"; //database password
                crystalrpt.SetDatabaseLogon(username, password); //here usaer name and password for crystel report

                //parametro
                crParameterDiscreteValue.Value = cbx_empleado.SelectedValue;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["id_empleado"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                //parametros fechas
                crParameterDiscreteValue.Value = dtp_asistencia1.Value.ToString("yyyy-MM-dd"); ;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["fecha1"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                crParameterDiscreteValue.Value = dtp_asistencia2.Value.ToString("yyyy-MM-dd"); ;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["fecha2"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                if (InvokeRequired)
                {
                    Invoke(new Action(() => crystalReportViewer3.ReportSource = crystalrpt));
                }
                crystalReportViewer3.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_empleado_retardo_Click(object sender, EventArgs e)
        {
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.Retardo_Empleado));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
        }

        public void Retardo_Empleado()
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                ReportDocument crystalrpt = new ReportDocument();
                crystalrpt.Load(@"..\\..\\reportes\retardoempleados.rpt");

                ParameterFieldDefinitions parameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValue = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                //COLOCAR USUARIO Y CONTRASEÑA PARA CRYSTAL REPORTS
                string username = "sa"; // database user name
                string password = "123456"; //database password
                crystalrpt.SetDatabaseLogon(username, password); //here usaer name and password for crystel report

                //parametro
                crParameterDiscreteValue.Value = cbx_empleado_retardo.SelectedValue;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["id_empleado"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                //parametros fechas
                crParameterDiscreteValue.Value = dtp_empleado_retardo1.Value.ToString("yyyy-MM-dd"); ;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["fecha1"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                crParameterDiscreteValue.Value = dtp_empleado_retardo2.Value.ToString("yyyy-MM-dd"); ;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["fecha2"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                if (InvokeRequired)
                {
                    Invoke(new Action(() => crystalReportViewer4.ReportSource = crystalrpt));
                }
                crystalReportViewer4.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dtp_empleado_retardo2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtp_empleado_retardo1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbx_empleado_retardo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage3;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage4;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage5;
        }

        private void btn_departamento_Click(object sender, EventArgs e)
        {
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.Por_Departamento));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
        }

        public void Por_Departamento()
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                ReportDocument crystalrpt = new ReportDocument();
                crystalrpt.Load(@"..\\..\\reportes\empleados-departamento.rpt");

                ParameterFieldDefinitions parameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValue = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                //COLOCAR USUARIO Y CONTRASEÑA PARA CRYSTAL REPORTS
                string username = "sa"; // database user name
                string password = "123456"; //database password
                crystalrpt.SetDatabaseLogon(username, password); //here usaer name and password for crystel report

                //parametro
                crParameterDiscreteValue.Value = txt_departamento.Text;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["departamento"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                //parametros fechas
                crParameterDiscreteValue.Value = dtp_departamento1.Value.ToString("yyyy-MM-dd"); ;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["fecha1"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                crParameterDiscreteValue.Value = dtp_departamento2.Value.ToString("yyyy-MM-dd"); ;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["fecha2"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                if (InvokeRequired)
                {
                    Invoke(new Action(() => crystalReportViewer5.ReportSource = crystalrpt));
                }
                crystalReportViewer5.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_empleados_Click(object sender, EventArgs e)
        {
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.Empleados_Checador));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
        }

        public void Empleados_Checador()
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                ReportDocument crystalrpt = new ReportDocument();
                crystalrpt.Load(@"..\\..\\reportes\Empleados-checador.rpt");

                ParameterFieldDefinitions parameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValue = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                //COLOCAR USUARIO Y CONTRASEÑA PARA CRYSTAL REPORTS
                string username = "sa"; // database user name
                string password = "123456"; //database password
                crystalrpt.SetDatabaseLogon(username, password); //here usaer name and password for crystel report

                //parametro
                crParameterDiscreteValue.Value = cbx_sucursal_checando.SelectedValue;
                parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = parameterFieldDefinitions["id_sucursal"];
                crParameterValue = crParameterFieldDefinition.CurrentValues;

                crParameterValue.Clear();
                crParameterValue.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                if (InvokeRequired)
                {
                    Invoke(new Action(() => crystalReportViewer6.ReportSource = crystalrpt));
                }
                crystalReportViewer6.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage6;
        }
    }
}
