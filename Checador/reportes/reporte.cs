using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;

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

        }

        private void btn_gnerar_Click(object sender, EventArgs e)
        {
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

            crystalReportViewer1.ReportSource = crystalrpt;
            crystalReportViewer1.Refresh();
        }

        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage2;
        }

        private void btn_retardo_Click(object sender, EventArgs e)
        {
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

            crystalReportViewer2.ReportSource = crystalrpt;
            crystalReportViewer2.Refresh();
        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {
            tabControlBase.SelectedTab = tabPage1;
        }

        private void btn_asistencia_Click(object sender, EventArgs e)
        {
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

            crystalReportViewer3.ReportSource = crystalrpt;
            crystalReportViewer3.Refresh();
        }

        private void btn_empleado_retardo_Click(object sender, EventArgs e)
        {
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

            crystalReportViewer4.ReportSource = crystalrpt;
            crystalReportViewer4.Refresh();
        }
    }
}
