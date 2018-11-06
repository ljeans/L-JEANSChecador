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

        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();



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


        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;
           
        }

        //FUNCION PARA REGISTRAR UN HORARIO
        public void guardarHorario(bool flag)
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
                comand.Parameters.AddWithValue("@lunes", 0);
                comand.Parameters.AddWithValue("@martes", 0);
                comand.Parameters.AddWithValue("@miercoles", 0);
                comand.Parameters.AddWithValue("@jueves", 0);
                comand.Parameters.AddWithValue("@viernes", 0);
                comand.Parameters.AddWithValue("@sabado", 0);
                comand.Parameters.AddWithValue("@domingo", 0);
                comand.Parameters.AddWithValue("@horas_diarias", horas_diarias);
                comand.Parameters.AddWithValue("@horas_totales_quincenales", horas_totales_quincenales);
                if (flag)
                {
                    comand.Parameters.AddWithValue("@hora_salida_descanso", hora_salida_descanso);
                    comand.Parameters.AddWithValue("@hora_entrada_descanso", hora_entrada_descanso);
                }
                else
                {
                    comand.Parameters.AddWithValue("@hora_salida_descanso", DBNull.Value);
                    comand.Parameters.AddWithValue("@hora_entrada_descanso", DBNull.Value);
                }
                
                comand.Parameters.AddWithValue("@tolerancia", tolerancia);

                comand.ExecuteNonQuery();
                conexion.Close();

                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Horario Registrado con exito. ID= " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
                

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
                comand.Parameters.AddWithValue("@lunes", 0);
                comand.Parameters.AddWithValue("@martes", 0);
                comand.Parameters.AddWithValue("@miercoles", 0);
                comand.Parameters.AddWithValue("@jueves", 0);
                comand.Parameters.AddWithValue("@viernes", 0);
                comand.Parameters.AddWithValue("@sabado", 0);
                comand.Parameters.AddWithValue("@domingo", 0);
                comand.Parameters.AddWithValue("@horas_diarias", horas_diarias);
                comand.Parameters.AddWithValue("@horas_totales_quincenales", horas_totales_quincenales);
                comand.Parameters.AddWithValue("@hora_salida_descanso", hora_salida_descanso);
                comand.Parameters.AddWithValue("@hora_entrada_descanso", hora_entrada_descanso);
                comand.Parameters.AddWithValue("@tolerancia", tolerancia);
                comand.ExecuteNonQuery();
                conexion.Close();

                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Horario Modificado con exito. ID= " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

        public bool verificar_horario(string horario)//Funcion que hace el select retorna false si no hay resultados
        {
            try
            {
                Conexion conexion = new Conexion();
                //SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    
                    string select = "SELECT hr_entrada, hr_salida, hr_salida_descanso, hr_entrada_descanso FROM horarios WHERE id_horario=@horario";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@horario", horario);//Agregamos parametros a la consulta
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        lector.Read();//Lee una linea de los resultados
                        //this.id = ;//Asignacion a atributos
                        //get ordinal regresa el indice de la fila-
                        //el Nombre especificado en el parametro
                        hr_entrada = lector.GetTimeSpan(lector.GetOrdinal("hr_entrada"));
                        hr_salida = lector.GetTimeSpan(lector.GetOrdinal("hr_salida"));

                        try
                        {
                            hora_salida_descanso = lector.GetTimeSpan(lector.GetOrdinal("hr_salida_descanso"));
                        }
                        catch (Exception ex)
                        {
                            hora_salida_descanso = new TimeSpan(00, 00, 00);
                        }

                        try
                        {
                            hora_entrada_descanso = lector.GetTimeSpan(lector.GetOrdinal("hr_entrada_descanso"));
                        }
                        catch (Exception ex)
                        {
                            hora_entrada_descanso = new TimeSpan(00, 00, 00);
                        }
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
                MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
                return false;
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
                TimeSpan? hora_salida_descanso_null, hora_entrada_descanso_null;
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

                        try
                        {
                            hora_salida_descanso = lector.GetTimeSpan(lector.GetOrdinal("hr_salida_descanso"));
                        }
                        catch (Exception ex)
                        {
                            hora_salida_descanso = new TimeSpan(00, 00, 00);
                        }

                        try
                        {
                            hora_entrada_descanso = lector.GetTimeSpan(lector.GetOrdinal("hr_entrada_descanso"));
                        }
                        catch (Exception ex)
                        {
                            hora_entrada_descanso = new TimeSpan(00, 00, 00);
                        }

                        
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

        //FUNCION PARA ELIMINAR UN HORARIO EN LA BASE DE DATOS UN HORARIO
        public void eliminarHorario()
        {
            try
            {
                //ELIMINAR HORARIO
                string consulta = "DELETE from horarios WHERE id_horario = @id";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);

                comand.ExecuteNonQuery();
                conexion.Close();

                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Horario eliminado con exito. ID= " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

    }
}
