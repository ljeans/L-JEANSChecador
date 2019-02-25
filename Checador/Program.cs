using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checador
{
    
    static class Program
    {
        public static string rol;
        public static string nombre_usuario, sucursal;
        public static int id_empleado, id_sucursal;
        public static string cadena_conexion;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new principal());
        }
    }
}
