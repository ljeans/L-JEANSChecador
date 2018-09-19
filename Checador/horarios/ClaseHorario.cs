using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Checador
{
    class ClaseHorario
    {
        public int id { get; set; }
        public string horario { get; set; }
        public TimeSpan hr_entrada { get; set; }
        public TimeSpan hr_salida { get; set; }
        public int horas_diarias { get; set; }
        public int lunes { get; set; }
        public int martes { get; set; }
        public int miercoles { get; set; }
        public int jueves { get; set; }
        public int viernes { get; set; }
        public int sabado { get; set; }
        public int domingo { get; set; }
        public int horas_totales_quincenales { get; set; }
        public TimeSpan hora_salida_descanso { get; set; }
        public TimeSpan hora_entrada_descanso { get; set; }
        public int tolerancia { get; set; }

        //FUNCION PARA OBTENER EL ID MAXIMO DEL HORARIO POR SI ES AUTOINCREMENTABLE EL ID
        public int obtenerIdMaximo()
        {
            string consulta = "Select Max(id_horario) From horarios";
            Conexion con = new Conexion();
            SqlConnection conexion = new SqlConnection(con.cadenaConexion);
            SqlCommand comand = new SqlCommand(consulta, conexion);
            conexion.Open();
            int idMaximo = Convert.ToInt32(comand.ExecuteScalar());
            conexion.Close();
            return idMaximo;
        }

        //FUNCION PARA REGISTRAR UN HORARIO
        public void guardarHorario()
        {
            try
            {
                //Registrar HORARIO
                string consulta = "INSERT INTO horarios VALUES (@id,@horario,@hora_entrada, @hora_salida,@horas_diarias,@lunes, @martes, @miercoles, @jueves, @viernes, @sabado, @domingo, @horas_totales_quincenales, @hora_salida_descanso, @hora_entrada_descanso, @tolerancia)";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);
                comand.Parameters.AddWithValue("@horario", horario);
                comand.Parameters.AddWithValue("@hora_entrada", hr_entrada);
                comand.Parameters.AddWithValue("@hora_salida", hr_salida);
                comand.Parameters.AddWithValue("@lunes", lunes);
                comand.Parameters.AddWithValue("@martes", martes);
                comand.Parameters.AddWithValue("@miercoles", miercoles);
                comand.Parameters.AddWithValue("@jueves", jueves);
                comand.Parameters.AddWithValue("@viernes", viernes);
                comand.Parameters.AddWithValue("@sabado", sabado);
                comand.Parameters.AddWithValue("@domingo", domingo);
                comand.Parameters.AddWithValue("@horas_diarias", horas_diarias);
                comand.Parameters.AddWithValue("@horas_totales_quincenales", horas_totales_quincenales);
                comand.Parameters.AddWithValue("@hora_salida_descanso", hora_salida_descanso);
                comand.Parameters.AddWithValue("@hora_entrada_descanso", hora_entrada_descanso);
                comand.Parameters.AddWithValue("@tolerancia", tolerancia);

                comand.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Horario registrado con éxito. ID= " + id.ToString());

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

        //FUNCION PARA ACTUALIZAR LOS DATOS DE UN HORARIO
        public void Modificar_Horario()
        {
            try
            {
                string consulta = "UPDATE horarios SET horario = @horario, hr_entrada =@hora_entrada, hr_salida =@hora_salida,horas_diarias= @horas_diarias,lunes=@lunes, martes=@martes, miercoles= @miercoles, jueves=@jueves, viernes=@viernes, sabado=@sabado, domingo=@domingo, horas_totales_quincenales=@horas_totales_quincenales, hr_salida_descanso= @hora_salida_descanso, hr_entrada_descanso= @hora_entrada_descanso, tolerancia=@tolerancia WHERE id_horario = @id";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);
                comand.Parameters.AddWithValue("@horario", horario);
                comand.Parameters.AddWithValue("@hora_entrada", hr_entrada);
                comand.Parameters.AddWithValue("@hora_salida", hr_salida);
                comand.Parameters.AddWithValue("@lunes", lunes);
                comand.Parameters.AddWithValue("@martes", martes);
                comand.Parameters.AddWithValue("@miercoles", miercoles);
                comand.Parameters.AddWithValue("@jueves", jueves);
                comand.Parameters.AddWithValue("@viernes", viernes);
                comand.Parameters.AddWithValue("@sabado", sabado);
                comand.Parameters.AddWithValue("@domingo", domingo);
                comand.Parameters.AddWithValue("@horas_diarias", horas_diarias);
                comand.Parameters.AddWithValue("@horas_totales_quincenales", horas_totales_quincenales);
                comand.Parameters.AddWithValue("@hora_salida_descanso", hora_salida_descanso);
                comand.Parameters.AddWithValue("@hora_entrada_descanso", hora_entrada_descanso);
                comand.Parameters.AddWithValue("@tolerancia", tolerancia);
                comand.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Horario modificado con éxito. ID= " + id.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

        //FUNCION PARA VERIFICAR SI EL HORARIO YA EXISTE PREVIAMENTE EN LA BASE DE DATOS
        //RETORNA UN VALOR BOOL DEPENDIENDO SI ES V O F
        //ADEMAS CARGA LOS ATRIBUTOS DE LA CLASE CON LOS DATOS GUARDADOS DEL HORARIO
        //REFERENTE AL ID RECIBIDO COMO PARAMETRO
        public bool verificar_existencia(int id)//Funcion que hace el select retorna false si no hay resultados
        {
            try
            {
                Conexion conexion = new Conexion();
                //SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "SELECT * FROM horarios WHERE id_horario=@id";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@id", id);//Agregamos parametros a la consulta
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        lector.Read();//Lee una linea de los resultados
                        //this.id = ;//Asignacion a atributos
                        //get ordinal regresa el indice de la fila
                        //el Nombre especificado en el parametro 
                        id = lector.GetInt32(lector.GetOrdinal("id_horario"));
                        horario = lector.GetString(lector.GetOrdinal("horario"));
                        hr_entrada = lector.GetTimeSpan(lector.GetOrdinal("hr_entrada"));
                        hr_salida = lector.GetTimeSpan(lector.GetOrdinal("hr_salida"));
                        horas_diarias = lector.GetInt32(lector.GetOrdinal("horas_diarias"));
                        lunes = lector.GetInt32(lector.GetOrdinal("lunes"));
                        martes = lector.GetInt32(lector.GetOrdinal("martes"));
                        miercoles = lector.GetInt32(lector.GetOrdinal("miercoles"));
                        jueves = lector.GetInt32(lector.GetOrdinal("jueves"));
                        viernes = lector.GetInt32(lector.GetOrdinal("viernes"));
                        sabado = lector.GetInt32(lector.GetOrdinal("sabado"));
                        domingo = lector.GetInt32(lector.GetOrdinal("domingo"));
                        horas_totales_quincenales = lector.GetInt32(lector.GetOrdinal("horas_totales_quincenales"));
                        hora_salida_descanso = lector.GetTimeSpan(lector.GetOrdinal("hr_salida_descanso"));
                        hora_entrada_descanso = lector.GetTimeSpan(lector.GetOrdinal("hr_entrada_descanso"));
                        tolerancia = lector.GetInt32(lector.GetOrdinal("tolerancia"));
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
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
                return false;
            }
        }

    }
}
