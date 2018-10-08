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
    class ClaseChecador
    {
        public int id { get; set; }
        public string ip { get; set; }
        public string puerto { get; set; }
        public int id_sucursal { get; set; }
        public string estatus { get; set; }
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();


        //FUNCION PARA OBTENER EL ID MAXIMO DEL CHECADOR POR SI ES AUTOINCREMENTABLE EL ID
        public int obtenerIdMaximo()
        {
            string consulta = "Select Max(id_checador) From checador";
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


        //FUNCION PARA REGISTRAR UN DISPOSITIVO CHECADOR
        public void guardarChecador()
        {
            try
            {
                //Registrar CHECADOR
                string consulta = "INSERT INTO checador VALUES (@id,@ip, @puerto,@id_sucursal,@estatus)";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);
                comand.Parameters.AddWithValue("@ip", ip);
                comand.Parameters.AddWithValue("@puerto", puerto);
                comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                comand.Parameters.AddWithValue("@estatus", estatus);

                comand.ExecuteNonQuery();
                conexion.Close();
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Checador registrado con exito. ID= " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

        //FUNCION PARA ACTUALIZAR LOS DATOS DE UN DISPOSITIVO CHECADOR
        public void Modificar_Checador()
        {
            try
            {
                string consulta = "UPDATE checador SET ip = @ip, puerto = @puerto, id_sucursal = @id_sucursal, estatus=@estatus WHERE id_checador = @id";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);
                comand.Parameters.AddWithValue("@ip", ip);
                comand.Parameters.AddWithValue("@puerto", puerto);
                comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                comand.Parameters.AddWithValue("@estatus", estatus);
                comand.ExecuteNonQuery();
                conexion.Close();
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Checador modificado con éxito.ID = " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();

               
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Upss.. Ocurrió un error, por favor vuelva a intentarlo.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
        }

        //FUNCION PARA DAR DE BAJA UN DISPOSITIVO CHECADOR CAMBIANDO EL ESTATUS
        public void Eliminar_Checador()
        {
            try
            {
                string eliminar = "Update checador SET estatus=@estatus where id_checador=@id";

                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(eliminar, conexion);
                comand.Parameters.AddWithValue("@estatus", estatus);
                comand.Parameters.AddWithValue("@id", id);
                comand.ExecuteNonQuery();

                conexion.Close();
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Checador dado de baja con éxito. ID = " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();

            }
            catch (Exception e)
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Upss.. Ocurrió un error, por favor vuelva a intentarlo.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
        }

        //OBTIENE LOS DATOS DE UN CHECADOR POR SUCURSAL
        public void getChecador_Sucursal(int id_sucursal)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string get = "Select * FROM checador WHERE id_sucursal = @id_sucursal";
                    SqlCommand comando = new SqlCommand(get, con);
                    comando.Parameters.AddWithValue("@id_sucursal", id_sucursal);//Agregamos parametros a la consulta
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        lector.Read();//Lee una linea de los resultados
                        //this.id = ;//Asignacion a atributos
                        //get ordinal regresa el indice de la fila
                        //el Nombre especificado en el parametro 
                        id = lector.GetInt32(lector.GetOrdinal("id_checador"));
                        ip = lector.GetString(lector.GetOrdinal("ip"));
                        puerto = lector.GetString(lector.GetOrdinal("puerto"));
                        estatus = lector.GetString(lector.GetOrdinal("estatus"));
                        con.Close();
                    }
                    else
                    {
                        mensaje = new formularios_padres.mensaje_info();
                        mensaje.lbl_info.Text = "Checador inactivo o no existe.";
                        mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                        mensaje.Show();
                        con.Close();
                    }

                }

            }
            catch (Exception e)
            {
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
                MessageBox.Show(e.ToString());
            }
        }

        //FUNCION PARA OBTENER LOS CHECADORES ACTIVOS
        public DataTable Obtener_Checadores_Activos()
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion conexion = new Conexion();
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string consulta = "SELECT * FROM checador WHERE estatus = 'A'";
                    con.Open();//abre la conexion
                    SqlDataAdapter da = new SqlDataAdapter(consulta, con);
                    da.Fill(dt);
                    con.Close();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
                MessageBox.Show(e.ToString());
            }
            return dt;


        }

        //FUNCION PARA VERIFICAR SI EL DISPOSITIVO CHECADOR YA EXISTE PREVIAMENTE EN LA BASE DE DATOS
        //RETORNA UN VALOR BOOL DEPENDIENDO SI ES V O F
        //ADEMAS CARGA LOS ATRIBUTOS DE LA CLASE CON LOS DATOS GUARDADOS DEL DISPOSITIVO CHECADOR
        //REFERENTE AL ID RECIBIDO COMO PARAMETRO
        public bool verificar_existencia(int id)//Funcion que hace el select retorna false si no hay resultados
        {
            try
            {
                Conexion conexion = new Conexion();
                //SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "SELECT * FROM checador WHERE id_checador=@id";//Consulta
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
                        id = lector.GetInt32(lector.GetOrdinal("id_sucursal"));
                        ip = lector.GetString(lector.GetOrdinal("ip"));
                        puerto = lector.GetString(lector.GetOrdinal("puerto"));
                        id_sucursal = lector.GetInt32(lector.GetOrdinal("id_sucursal"));
                        estatus = lector.GetString(lector.GetOrdinal("estatus"));
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
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Upss.. Ocurrió un error, por favor vuelva a intentarlo.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
                return false;
            }
        }

        public void guardarEvento(int id_checador, int id_empleado, int id_sucursal, DateTime fecha_evento, TimeSpan hora_entrada, TimeSpan hora_salida, TimeSpan hora_entrada_descanso, TimeSpan hora_salida_descanso, int tolerancia, int tipo_evento, string horario)
        {
            try
            {
                Conexion conexion = new Conexion();
                SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                DateTime? fecha_entrada, fecha_salida;
                int Ordinal;
                double minutos_retardo, minutos_retardo2;
                int retardo=1;

                TimeSpan fecha_event = new TimeSpan(fecha_evento.Hour, fecha_evento.Minute, fecha_evento.Second);

                TimeSpan fecha_retardo = TimeSpan.FromMinutes(hora_entrada.TotalMinutes + Convert.ToDouble(tolerancia));

                TimeSpan fecha_retardo2 = TimeSpan.FromMinutes(hora_entrada_descanso.TotalMinutes + Convert.ToDouble(tolerancia));
                MessageBox.Show(horario);
                //ENTRADA
                if (tipo_evento == 0)
                {
                    if (horario != "ABIERTO")
                    {
                        if (fecha_event.TotalMinutes > fecha_retardo.TotalMinutes)
                        {
                            minutos_retardo = fecha_event.TotalMinutes - fecha_retardo.TotalMinutes;
                            minutos_retardo2 = Math.Abs(fecha_event.TotalMinutes - fecha_retardo2.TotalMinutes);
                            if (minutos_retardo > minutos_retardo2)
                            {
                                minutos_retardo = minutos_retardo2;
                            }

                            string consulta2 = "UPDATE empleado SET retardos = retardos+@retardo, total_min_retardo = total_min_retardo + @minutos_retardo WHERE  id_empleado = @id_empleado";
                            con.Open();
                            SqlCommand comand2 = new SqlCommand(consulta2, con);
                            comand2.Parameters.AddWithValue("@retardo", retardo);
                            comand2.Parameters.AddWithValue("@minutos_retardo", minutos_retardo);
                            comand2.Parameters.AddWithValue("@id_empleado", id_empleado);
                            comand2.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                  
                    //GUARDAR NUEVO EVENTO
                        string consulta = "INSERT INTO registros VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida)";
                        con.Open();
                        SqlCommand comand = new SqlCommand(consulta, con);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", fecha_evento);
                        comand.Parameters.AddWithValue("@fecha_salida", DBNull.Value);
                        comand.ExecuteNonQuery();
                        con.Close();
                }
                //SALIDA
                else if(tipo_evento == 1)
                {
                    string select = "SELECT top 1 * FROM registros WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada < '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' ORDER BY fecha_entrada DESC;";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                    comando.Parameters.AddWithValue("@id_empleado", id_empleado);
                    comando.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        lector.Read();//Lee una linea de los resultados
                        fecha_entrada = lector.GetDateTime(lector.GetOrdinal("fecha_entrada"));
                        try
                        {
                            fecha_salida = lector.GetDateTime(lector.GetOrdinal("fecha_salida"));
                        }
                        catch (Exception ex)
                        {
                            fecha_salida = null;
                        }


                        if (fecha_entrada != null && fecha_salida != null)
                        {
                            con.Close();
                            //GUARDAR NUEVO EVENTO CON PURA SALIDA
                            string consulta = "INSERT INTO registros VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida)";
                            Conexion con2 = new Conexion();
                            SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                            conexion2.Open();
                            SqlCommand comand = new SqlCommand(consulta, conexion2);
                            comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                            comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                            comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                            comand.Parameters.AddWithValue("@fecha_entrada", DBNull.Value);
                            comand.Parameters.AddWithValue("@fecha_salida", fecha_evento);
                            comand.ExecuteNonQuery();
                            conexion2.Close();
                        }
                        else if(fecha_entrada != null && fecha_salida == null)
                        {
                            con.Close();
                            //HACER UPDATE PARA INSERTAR LA FECHA DE SALIDA
                            string consulta = "UPDATE registros SET fecha_salida = @fecha_evento WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada < '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and fecha_salida is Null and fecha_entrada = (SELECT MAX(fecha_entrada) FROM registros WHERE id_checador=@checador and id_empleado = @empleado and id_sucursal = @sucursal); ";
                            Conexion con2 = new Conexion();
                            SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                            conexion2.Open();
                            SqlCommand comand = new SqlCommand(consulta, conexion2);
                            comand.Parameters.AddWithValue("@fecha_evento", fecha_evento);
                            comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                            comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                            comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                            comand.Parameters.AddWithValue("@checador", id_checador);//Agregamos parametros a la consulta
                            comand.Parameters.AddWithValue("@empleado", id_empleado);
                            comand.Parameters.AddWithValue("@sucursal", id_sucursal);
                            comand.ExecuteNonQuery();
                            conexion2.Close();
                        }
                    }

                    /*string select = "SELECT * FROM registros WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada < '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and fecha_salida is Null;";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                    comando.Parameters.AddWithValue("@id_empleado", id_empleado);
                    comando.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        con.Close();
                        //HACER UPDATE PARA INSERTAR LA FECHA DE SALIDA
                        string consulta = "UPDATE registros SET fecha_salida = @fecha_evento WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada < '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and fecha_salida is Null and fecha_entrada = (SELECT MAX(fecha_entrada) FROM registros); ";
                        Conexion con2 = new Conexion();
                        SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                        conexion2.Open();
                        SqlCommand comand = new SqlCommand(consulta, conexion2);
                        comand.Parameters.AddWithValue("@fecha_evento", fecha_evento);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.ExecuteNonQuery();
                        conexion2.Close();
                    }*/
                    /*else
                    {
                        MessageBox.Show("ENTRO");
                        //GUARDAR NUEVO EVENTO CON PURA SALIDA
                        string consulta = "INSERT INTO registros VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida)";
                        Conexion con2 = new Conexion();
                        SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                        conexion2.Open();
                        SqlCommand comand = new SqlCommand(consulta, conexion2);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", DBNull.Value);
                        comand.Parameters.AddWithValue("@fecha_salida", fecha_evento);
                        comand.ExecuteNonQuery();
                        conexion2.Close();
                    } */
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //FUNCION PARA REGISTRAR EVENTO DEL CHECADOR EN LA BD
        /*public void guardarEvento(int id_checador, int id_empleado, int id_sucursal, DateTime fecha_evento, TimeSpan hora_entrada, TimeSpan hora_salida, TimeSpan hora_entrada_descanso, TimeSpan hora_salida_descanso)
        {
            try
            {
                TimeSpan fecha_evento_hora = new TimeSpan (fecha_evento.Hour, fecha_evento.Minute, fecha_evento.Second);
                //MessageBox.Show(fecha_evento_hora.ToString());
                //SACAR INTERVALOS DE TIEMPO PARA VALIDACIONES DE CHECKEOS
                TimeSpan promedio_entrada, promedio_salida, promedio_descanso;
                promedio_entrada = (hora_salida_descanso - hora_entrada);
                double pivote = promedio_entrada.TotalMinutes / 2;
                promedio_entrada = TimeSpan.FromMinutes(hora_entrada.TotalMinutes + pivote);

                promedio_salida = (hora_salida - hora_entrada_descanso);
                pivote = promedio_salida.TotalMinutes / 2;
                promedio_salida = TimeSpan.FromMinutes(hora_entrada_descanso.TotalMinutes + pivote);

                promedio_descanso = (hora_entrada_descanso - hora_salida_descanso);
                pivote = promedio_descanso.TotalMinutes / 2;
                promedio_descanso = TimeSpan.FromMinutes(hora_salida_descanso.TotalMinutes + pivote);
                //////////////////////////

                Conexion conexion = new Conexion();
                //SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    //GUARDAR ENTRADA
                    if (fecha_evento_hora <= promedio_entrada)
                    {
                        //GUARDAR NUEVO EVENTO
                        string consulta = "INSERT INTO registros VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida)";
                        Conexion con2 = new Conexion();
                        SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                        conexion2.Open();
                        SqlCommand comand = new SqlCommand(consulta, conexion2);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", fecha_evento);
                        comand.Parameters.AddWithValue("@fecha_salida", DBNull.Value);
                        comand.ExecuteNonQuery();
                        conexion2.Close();
                        //MessageBox.Show("Entrada registrada");
                    }

                    //VALIDACION DE EVENTO DE SALIDA A COMER
                    else if (fecha_evento_hora >= promedio_entrada && fecha_evento_hora <= promedio_descanso)
                    {
                        string select = "SELECT * FROM registros WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada < '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and cast(fecha_entrada as time) <= '"+ promedio_entrada.Hours.ToString("d2") + ":"+promedio_entrada.Minutes.ToString("d2") + ":00.000' and fecha_salida is Null;";//Consulta
                        SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                        comando.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comando.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comando.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        con.Open();//abre la conexion
                        SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                        if (lector.HasRows)//Revisa si hay resultados
                        {
                            con.Close();
                            //HACER UPDATE PARA INSERTAR LA FECHA DE SALIDA
                            string consulta = "UPDATE registros SET fecha_salida = @fecha_evento WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada < '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and cast(fecha_entrada as time) <= '" + promedio_entrada.Hours.ToString("d2") + ":" + promedio_entrada.Minutes.ToString("d2") + ":00.000' and fecha_salida is Null;";
                            Conexion con2 = new Conexion();
                            SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                            conexion2.Open();
                            SqlCommand comand = new SqlCommand(consulta, conexion2);
                            comand.Parameters.AddWithValue("@fecha_evento", fecha_evento);
                            comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                            comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                            comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                            comand.ExecuteNonQuery();
                            conexion2.Close();
                        }
                        else
                        {
                            //GUARDAR NUEVO EVENTO
                            string consulta = "INSERT INTO registros VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida)";
                            Conexion con2 = new Conexion();
                            SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                            conexion2.Open();
                            SqlCommand comand = new SqlCommand(consulta, conexion2);
                            comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                            comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                            comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                            comand.Parameters.AddWithValue("@fecha_entrada", DBNull.Value);
                            comand.Parameters.AddWithValue("@fecha_salida", fecha_evento);
                            comand.ExecuteNonQuery();
                            conexion2.Close();
                        }
                    }

                    //VALIDACION DE EVENTOS REGRESO DE COMER
                    else if (fecha_evento_hora >= promedio_descanso && fecha_evento_hora <= promedio_salida)
                    {
                        //GUARDAR NUEVO EVENTO
                        string consulta = "INSERT INTO registros VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida)";
                        Conexion con2 = new Conexion();
                        SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                        conexion2.Open();
                        SqlCommand comand = new SqlCommand(consulta, conexion2);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", fecha_evento);
                        comand.Parameters.AddWithValue("@fecha_salida", DBNull.Value);
                        comand.ExecuteNonQuery();
                        conexion2.Close();
                        //MessageBox.Show("Entrada a comer");
                    }

                    //VALIDACION SALIDA
                    else if (fecha_evento_hora >= promedio_salida)
                    {
                        //string select = "SELECT * FROM registros WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada >= '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T" + promedio_descanso.Hours.ToString("d2") + ":" + promedio_descanso.Minutes.ToString("d2") + ":00.000' and fecha_entrada <= '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T" + promedio_salida.Hours + ":" + promedio_salida.Minutes + ":00.000' and fecha_salida is Null;";//Consulta
                        string select = "SELECT * FROM registros WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada < '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and cast(fecha_entrada as time) >= '" + promedio_descanso.Hours.ToString("d2") + ":" + promedio_descanso.Minutes.ToString("d2") + ":00.000' and cast(fecha_entrada as time) <= '" + promedio_salida.Hours.ToString("d2") + ":" + promedio_salida.Minutes.ToString("d2") + ":00.000' and fecha_salida is Null;";//Consulta
                        SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                        comando.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comando.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comando.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        con.Open();//abre la conexion
                        SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                        if (lector.HasRows)//Revisa si hay resultados
                        {
                            con.Close();
                            //HACER UPDATE PARA INSERTAR LA FECHA DE SALIDA
                            string consulta = "UPDATE registros SET fecha_salida = @fecha_evento WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada < '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and cast(fecha_entrada as time) >= '" + promedio_descanso.Hours.ToString("d2") + ":" + promedio_descanso.Minutes.ToString("d2") + ":00.000' and cast(fecha_entrada as time) <= '" + promedio_salida.Hours.ToString("d2") + ":" + promedio_salida.Minutes.ToString("d2") + ":00.000' and fecha_salida is Null;";
                            Conexion con2 = new Conexion();
                            SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                            conexion2.Open();
                            SqlCommand comand = new SqlCommand(consulta, conexion2);
                            comand.Parameters.AddWithValue("@fecha_evento", fecha_evento);
                            comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                            comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                            comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                            comand.ExecuteNonQuery();
                            conexion2.Close();
                        }
                        else
                        {
                            //GUARDAR NUEVO EVENTO
                            string consulta = "INSERT INTO registros VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida)";
                            Conexion con2 = new Conexion();
                            SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                            conexion2.Open();
                            SqlCommand comand = new SqlCommand(consulta, conexion2);
                            comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                            comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                            comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                            comand.Parameters.AddWithValue("@fecha_entrada", DBNull.Value);
                            comand.Parameters.AddWithValue("@fecha_salida", fecha_evento);
                            comand.ExecuteNonQuery();
                            conexion2.Close();
                        }
                        //MessageBox.Show("Salida");
                    }
                    /*string select = "SELECT * FROM registros WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada	< '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and fecha_salida is Null;";//Consulta
                    string select = "SELECT * FROM registros WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada	< '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and fecha_salida is Null;";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                    comando.Parameters.AddWithValue("@id_empleado", id_empleado);
                    comando.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {

                        con.Close();
                        //HACER UPDATE PARA INSERTAR LA FECHA DE SALIDA
                        string consulta = "UPDATE registros SET fecha_salida = @fecha_evento WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada	< '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and fecha_salida is Null;";
                        Conexion con2 = new Conexion();
                        SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                        conexion2.Open();
                        SqlCommand comand = new SqlCommand(consulta, conexion2);
                        comand.Parameters.AddWithValue("@fecha_evento", fecha_evento);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.ExecuteNonQuery();
                        conexion2.Close();
                        //MessageBox.Show("Evento modificado con exito");

                    }
                    else
                    {
                        con.Close();

                        //GUARDAR NUEVO EVENTO
                        string consulta = "INSERT INTO registros VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida)";
                        Conexion con2 = new Conexion();
                        SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                        conexion2.Open();
                        SqlCommand comand = new SqlCommand(consulta, conexion2);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", fecha_evento);
                        comand.Parameters.AddWithValue("@fecha_salida", DBNull.Value);
                        comand.ExecuteNonQuery();
                        conexion2.Close();
                        //MessageBox.Show("Evento registrado con exito");

                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }*/

        //FUNCION PARA VERIFICAR EL ULTIMO EVENTO REGISTRADO
        public DateTime verificarEvento(int id_checador)
        {
            try
            {
                DateTime fecha_entrada, fecha_salida;
                DateTime? fecha_entrada2, fecha_salida2;
                Conexion conexion = new Conexion();
                //SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "select Max(fecha_entrada) as fecha_entrada, Max(fecha_salida) as fecha_salida from registros WHERE id_checador = @id_checador;";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        lector.Read();
                        if (lector.GetDateTime(lector.GetOrdinal("fecha_entrada")) != null || lector.GetDateTime(lector.GetOrdinal("fecha_salida")) != null)
                        {
                            try
                            {
                                fecha_entrada2 = lector.GetDateTime(lector.GetOrdinal("fecha_entrada"));
                            }
                            catch (Exception ex)
                            {
                                fecha_entrada2 = null;
                            }

                            try
                            {
                                fecha_salida2 = lector.GetDateTime(lector.GetOrdinal("fecha_salida"));
                            }
                            catch (Exception ex)
                            {
                                fecha_salida2 = null;
                            }

                            if (fecha_salida2 > fecha_entrada2 || fecha_entrada2 == null)
                            {
                                fecha_salida = Convert.ToDateTime(fecha_salida2);
                                return fecha_salida;
                            }
                            else if (fecha_salida2 < fecha_entrada2 || fecha_salida2 == null)
                            {
                                fecha_entrada = Convert.ToDateTime(fecha_entrada2);
                                return fecha_entrada;
                            }
                            else
                            {
                                fecha_entrada = Convert.ToDateTime(fecha_entrada2);
                                return fecha_entrada;
                            }
                        }
                        else
                        {
                            return Convert.ToDateTime("1995-12-12 00:00:00");
                        }

                    }
                    else
                    {
                        return Convert.ToDateTime("1995-12-12 00:00:00");
                    }

                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                return Convert.ToDateTime("1995-12-12 00:00:00");
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }


    }
}
