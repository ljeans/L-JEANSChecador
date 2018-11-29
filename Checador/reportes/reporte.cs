using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Threading;
using System.Data.SqlClient;

namespace Checador.reportes
{

    #region NewUsing
    using CrystalDecisions.CrystalReports.Engine;
    using CrystalDecisions.Shared;
    #endregion

    public partial class reporte : Checador.formularios_padres.formpadre
    {

        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();

        public reporte()
        {
            InitializeComponent();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;
        }

        private void reporte_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.Vista_Empleados' Puede moverla o quitarla según sea necesario.
                this.vista_EmpleadosTableAdapter.Fill(this.dataSet_Checador.Vista_Empleados);
                // TODO: esta línea de código carga datos en la tabla 'dataSet_Checador.sucursal' Puede moverla o quitarla según sea necesario.
                this.sucursalTableAdapter.Fill(this.dataSet_Checador.sucursal);

                CheckForIllegalCrossThreadCalls = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           
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

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage6;
        }
    }
}
