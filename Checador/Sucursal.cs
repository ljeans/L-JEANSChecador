using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Checador
{
    class Sucursal
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string calle { get; set; }
        public string colonia { get; set; }
        public string num_ext { get; set; }
        public string num_int { get; set; }
        public string codigo_postal { get; set; }
        public string poblacion { get; set; }
        public string municipio { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
        public string telefono { get; set;}

        public int obtenerIdMaximo()
        {
            string consulta = "Select Max(id_sucursal) From sucursal";
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
