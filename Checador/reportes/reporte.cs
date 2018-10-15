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
            crParameterDiscreteValue.Value = dtp_fecha1.Value.ToString("yyyy-MM-dd"); ;
            parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = parameterFieldDefinitions["fecha1"];
            crParameterValue = crParameterFieldDefinition.CurrentValues;

            crParameterValue.Clear();
            crParameterValue.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

            crParameterDiscreteValue.Value = dtp_fecha2.Value.ToString("yyyy-MM-dd"); ;
            parameterFieldDefinitions = crystalrpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = parameterFieldDefinitions["fecha2"];
            crParameterValue = crParameterFieldDefinition.CurrentValues;

            crParameterValue.Clear();
            crParameterValue.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValue);

            crystalReportViewer1.ReportSource = crystalrpt;
            crystalReportViewer1.Refresh();
        }
    }
}
