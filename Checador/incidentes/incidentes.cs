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
//////////////////////////////////////////////////////////////////////
    }
}
