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

        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;

        }

        //FUNCION PARA ASIGNAR HORARIO RECIBE COMO PARAMETRO EL ID DEL EMPLEADO
        public void asignarHorario()
        {
            try
            {
                Conexion conexion = new Conexion();
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "SELECT * FROM horario_empleados WHERE id_empleado=@id_empleado";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@id_empleado", id_empleado);//Agregamos parametros a la consulta
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
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Horario asignado al empleado = " + id_empleado;
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();


                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

        //FUNCION PARA VERIFICAR SI EL EMPLEADO YA TIENE ASIGNADO UN HORARIO PREVIAMENTE EN LA BASE DE DATOS
        //RETORNA UN VALOR BOOL DEPENDIENDO SI ES V O F
        //ADEMAS CARGA LOS ATRIBUTOS DE LA CLASE CON LOS DATOS GUARDADOS DEL DISPOSITIVO CHECADOR
        //REFERENTE AL ID RECIBIDO COMO PARAMETRO
        public bool verificar_existencia(int id)//Funcion que hace el select retorna false si no hay resultados
        {
            try
            {
                Conexion conexion = new Conexion();
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "SELECT * FROM horario_empleados WHERE id_empleado=@id_empleado";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@id_empleado", id);//Agregamos parametros a la consulta
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        lector.Read();//Lee una linea de los resultados
                        //this.id = ;//Asignacion a atributos
                        //get ordinal regresa el indice de la fila
                        //el Nombre especificado en el parametro 
                        id_empleado = lector.GetInt32(lector.GetOrdinal("id_empleado"));
                        lunes = lector.GetInt32(lector.GetOrdinal("lunes"));
                        martes = lector.GetInt32(lector.GetOrdinal("martes"));
                        miercoles = lector.GetInt32(lector.GetOrdinal("miercoles"));
                        jueves = lector.GetInt32(lector.GetOrdinal("jueves"));
                        viernes = lector.GetInt32(lector.GetOrdinal("viernes"));
                        sabado = lector.GetInt32(lector.GetOrdinal("sabado"));
                        domingo = lector.GetInt32(lector.GetOrdinal("domingo"));
                        con.Close();
                        return true;
                    }
                    else
                    {
                        con.Close();
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                /*mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Upss.. Ocurrió un error, por favor vuelva a intentarlo.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();*/
                MessageBox.Show(e.ToString());
                return false;
            }
        }
    }
}
