using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Checador
{
    class ClaseAsignar_Horario
    {
        public int id_empleado { get; set; }
        public int lunes { get; set; }
        public int martes { get; set; }
        public int miercoles { get; set; }
        public int jueves { get; set; }
        public int viernes { get; set; }
        public int sabado { get; set; }
        public int domingo { get; set; }

        //FUNCION PARA ASIGNAR HORARIO RECIBE COMO PARAMETRO EL ID DEL EMPLEADO
        public void asignarHorario(int id)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "SELECT * FROM horario_empleados WHERE id_empleado=@id_empleado";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@id", id);//Agregamos parametros a la consulta
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        string consulta = "UPDATE horario_empleados SET lunes=@lunes, martes=@martes, miercoles= @miercoles, jueves=@jueves, viernes=@viernes, sabado=@sabado, domingo=@domingo WHERE id_empleado = @id_empleado";
                        SqlConnection con2 = new SqlConnection(conexion.cadenaConexion);
                        con2.Open();
                        SqlCommand comand = new SqlCommand(consulta, con2);
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@lunes", lunes);
                        comand.Parameters.AddWithValue("@martes", martes);
                        comand.Parameters.AddWithValue("@miercoles", miercoles);
                        comand.Parameters.AddWithValue("@jueves", jueves);
                        comand.Parameters.AddWithValue("@viernes", viernes);
                        comand.Parameters.AddWithValue("@sabado", sabado);
                        comand.Parameters.AddWithValue("@domingo", domingo);
                        comand.ExecuteNonQuery();
                        con2.Close();
                    }
                    else
                    {
                        //Registrar HORARIO
                        string consulta = "INSERT INTO horario_empleados VALUES (@id_empleado,@lunes, @martes, @miercoles, @jueves, @viernes, @sabado, @domingo)";
                        SqlConnection con2 = new SqlConnection(conexion.cadenaConexion);
                        con2.Open();
                        SqlCommand comand = new SqlCommand(consulta, con2);
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@lunes", lunes);
                        comand.Parameters.AddWithValue("@martes", martes);
                        comand.Parameters.AddWithValue("@miercoles", miercoles);
                        comand.Parameters.AddWithValue("@jueves", jueves);
                        comand.Parameters.AddWithValue("@viernes", viernes);
                        comand.Parameters.AddWithValue("@sabado", sabado);
                        comand.Parameters.AddWithValue("@domingo", domingo);

                        comand.ExecuteNonQuery();
                        con2.Close();
                    }

                    MessageBox.Show("Horario asignado al empleado = " + id_empleado);


                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

    }
}
