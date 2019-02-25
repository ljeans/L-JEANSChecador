using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Checador
{
    class Conexion
    {
        //PARA ESTE SE COPIA TODO; ESTE ES EL WENO; RESPALDO DONDE ESTA EL ESTABLE
        //public string cadenaConexion = "Data Source = 20.20.1.126,1433; Initial Catalog =sistema_checador; user= SA; password=123456;";
        //public string cadenaConexion = "Data Source = DESKTOP-JBSAC7M,1433; Initial Catalog =sistema_checador; user= SA; password=123456;";
        public string cadenaConexion = Program.cadena_conexion;
    }
}
