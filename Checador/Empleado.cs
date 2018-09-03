using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Checador
{
    public class Empleado
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido_pat { get; set; }
        public string apellido_mat { get; set; }
        public string departamento { get; set; }
        public string calle { get; set; }
        public string num_ext { get; set; }
        public string num_int { get; set; }
        public string colonia { get; set; }
        public string codigo_postal { get; set; }
        public string poblacion { get; set; }
        public string municipio { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
        public string puesto { get; set; }
        public string NSS { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string estatus { get; set; }
        public DateTime fecha_alta { get; set; }
        public DateTime fecha_baja { get; set; }
        public double sueldo_diario { get; set; }
        public double sueldo_diario_integrado { get; set; }
        public double sueldo_base_quincenal { get; set; }
        public string tipo_salario { get; set; }
        public int dias_aguinaldo { get; set; }
        public int dias_vacaciones { get; set; }
        public string observaciones { get; set; }
        public string tipo_jornada { get; set; }
        public string tipo_contrato { get; set; }
        public string riesgo_puesto { get; set; }
        public string periodicidad_pago { get; set; }
        public string banco { get; set; }
        public string cuenta_bancaria { get; set; }
        public string email { get; set; }
        public string sindicalizado { get; set; }
        public string tarjeta_despensa { get; set; }
        public string clave_edenred { get; set; }

        public int obtenerIdMaximo()
        {
            string consulta = "Select Max(id_empleado) From empleado";
            Conexion con = new Conexion();
            SqlConnection conexion = new SqlConnection(con.cadenaConexion);
            SqlCommand comand = new SqlCommand(consulta, conexion);
            conexion.Open();
            int idMaximo = Convert.ToInt32(comand.ExecuteScalar());
            conexion.Close();
            return idMaximo;
        }
    }
}
