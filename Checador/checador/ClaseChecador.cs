﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.IO;

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

        //obtienes el Id Checador por Sucursal
        public void obtenerIdChecador(int id_sucursal)
        {
            string consulta = "Select id_checador From checador where id_sucursal=@id_sucursal";
            Conexion con = new Conexion();
            SqlConnection conexion = new SqlConnection(con.cadenaConexion);
            SqlCommand comand = new SqlCommand(consulta, conexion);
            comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
            conexion.Open();
            SqlDataReader lector = comand.ExecuteReader();//Ejecuta el comadno
            if (lector.HasRows)//Revisa si hay resultados
            {
                lector.Read();//Lee una linea de los resultados
                              //this.id = ;//Asignacion a atributos
                              //get ordinal regresa el indice de la fila
                              //el Nombre especificado en el parametro 
                id = lector.GetInt32(lector.GetOrdinal("id_checador"));
                conexion.Close();
            }
            else
            {
                conexion.Close();
            }
        }


        //FUNCION PARA REGISTRAR UN DISPOSITIVO CHECADOR
        public bool guardarChecador()
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
                mensaje.ShowDialog();
                return true;
            }
            catch (Exception e)
            {
                throw;
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
                //MessageBox.Show("Ocurrió un problema");
                /*mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Upss.. Ocurrió un error,";
                mensaje.lbl_info2.Text = "por favor vuelva a intentarlo.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.ShowDialog();
                return false;*/
                throw;
            }
        }

        public void guardarEvento(int id_checador, int id_empleado, int id_sucursal, DateTime fecha_evento, TimeSpan hora_entrada, TimeSpan hora_salida, TimeSpan hora_entrada_descanso, TimeSpan hora_salida_descanso, int tolerancia, int tipo_evento, int id_horario)
        {
            try
            {
                Conexion conexion = new Conexion();
                SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                DateTime? fecha_entrada, fecha_salida;
                double minutos_retardo, minutos_retardo2;
                int retardo=1;

                TimeSpan fecha_event = new TimeSpan(fecha_evento.Hour, fecha_evento.Minute, fecha_evento.Second);

                TimeSpan fecha_retardo = TimeSpan.FromMinutes(hora_entrada.TotalMinutes + Convert.ToDouble(tolerancia));

                TimeSpan fecha_retardo2 = TimeSpan.FromMinutes(hora_entrada_descanso.TotalMinutes + Convert.ToDouble(tolerancia));
                
                //ENTRADA
                if (tipo_evento == 0)
                {
                    //GUARDAR RETARDO
                    if (fecha_event.TotalMinutes > fecha_retardo2.TotalMinutes && hora_entrada_descanso != new TimeSpan(00, 00, 00))
                    {
                        minutos_retardo2 = Math.Abs(fecha_event.TotalMinutes - fecha_retardo2.TotalMinutes);

                        //SACAR EL TIPO DE RETARDO
                        int id_tipo_retardo=0;
                        if (minutos_retardo2 < 16)
                        {
                            id_tipo_retardo = 1;
                        }
                        else if (minutos_retardo2 >= 16 && minutos_retardo2 <= 30)
                        {
                            id_tipo_retardo = 2;
                        }
                        else if (minutos_retardo2 > 30)
                        {
                            id_tipo_retardo = 3;
                        }
                        else
                        {
                            id_tipo_retardo = 1;
                        }
              
                        //GUARDAR NUEVO EVENTO
                        string consulta = "INSERT INTO registros WITH (TABLOCK) VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida, @horas_trabajadas, @retardos, @total_min_retardo, @id_tipo_retardo, @id_horario)";
                        con.Open();
                        SqlCommand comand = new SqlCommand(consulta, con);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", fecha_evento);
                        comand.Parameters.AddWithValue("@fecha_salida", DBNull.Value);
                        comand.Parameters.AddWithValue("@horas_trabajadas", DBNull.Value);
                        comand.Parameters.AddWithValue("@retardos", retardo);
                        comand.Parameters.AddWithValue("@total_min_retardo", minutos_retardo2);
                        comand.Parameters.AddWithValue("@id_tipo_retardo", id_tipo_retardo);
                        comand.Parameters.AddWithValue("@id_horario", id_horario);
                        comand.ExecuteNonQuery();
                        con.Close();
                    }
                    else if (fecha_event.TotalMinutes > fecha_retardo.TotalMinutes && fecha_event.TotalMinutes < hora_salida_descanso.TotalMinutes)
                    {
                        minutos_retardo = fecha_event.TotalMinutes - fecha_retardo.TotalMinutes;

                        //SACAR EL TIPO DE RETARDO
                        int id_tipo_retardo=0;
                        if (minutos_retardo < 16)
                        {
                            id_tipo_retardo = 1;
                        }
                        else if (minutos_retardo >= 16 && minutos_retardo <= 30)
                        {
                            id_tipo_retardo = 2;
                        }
                        else if (minutos_retardo > 30)
                        {
                            id_tipo_retardo = 3;
                        }
                        else
                        {
                            id_tipo_retardo = 1;
                        }

                        //GUARDAR NUEVO EVENTO
                        string consulta = "INSERT INTO registros WITH (TABLOCK) VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida, @horas_trabajadas, @retardos, @total_min_retardo, @id_tipo_retardo, @id_horario)";
                        con.Open();
                        SqlCommand comand = new SqlCommand(consulta, con);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", fecha_evento);
                        comand.Parameters.AddWithValue("@fecha_salida", DBNull.Value);
                        comand.Parameters.AddWithValue("@horas_trabajadas", DBNull.Value);
                        comand.Parameters.AddWithValue("@retardos", retardo);
                        comand.Parameters.AddWithValue("@total_min_retardo", minutos_retardo);
                        comand.Parameters.AddWithValue("@id_tipo_retardo", id_tipo_retardo);
                        comand.Parameters.AddWithValue("@id_horario", id_horario);
                        comand.ExecuteNonQuery();
                        con.Close();
                    }
                    else if (fecha_event.TotalMinutes > fecha_retardo.TotalMinutes && hora_salida_descanso  == new TimeSpan(00,00,00))
                    {
                        minutos_retardo = fecha_event.TotalMinutes - fecha_retardo.TotalMinutes;

                        //SACAR EL TIPO DE RETARDO
                        int id_tipo_retardo;
                        if (minutos_retardo < 16)
                        {
                            id_tipo_retardo = 1;
                        }
                        else if (minutos_retardo >= 16 && minutos_retardo <= 30)
                        {
                            id_tipo_retardo = 2;
                        }
                        else if (minutos_retardo > 30)
                        {
                            id_tipo_retardo = 3;
                        }
                        else
                        {
                            id_tipo_retardo = 1;
                        }

                        //GUARDAR NUEVO EVENTO
                        string consulta = "INSERT INTO registros WITH (TABLOCK) VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida, @horas_trabajadas, @retardos, @total_min_retardo, @id_tipo_retardo, @id_horario)";
                        con.Open();
                        SqlCommand comand = new SqlCommand(consulta, con);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", fecha_evento);
                        comand.Parameters.AddWithValue("@fecha_salida", DBNull.Value);
                        comand.Parameters.AddWithValue("@horas_trabajadas", DBNull.Value);
                        comand.Parameters.AddWithValue("@retardos", retardo);
                        comand.Parameters.AddWithValue("@total_min_retardo", minutos_retardo);
                        comand.Parameters.AddWithValue("@id_tipo_retardo", id_tipo_retardo);
                        comand.Parameters.AddWithValue("@id_horario", id_horario);
                        comand.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        //GUARDAR NUEVO EVENTO
                        string consulta = "INSERT INTO registros WITH (TABLOCK) VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida, @horas_trabajadas, @retardos, @total_min_retardo, @id_tipo_retardo, @id_horario)";
                        con.Open();
                        SqlCommand comand = new SqlCommand(consulta, con);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", fecha_evento);
                        comand.Parameters.AddWithValue("@fecha_salida", DBNull.Value);
                        comand.Parameters.AddWithValue("@horas_trabajadas", DBNull.Value);
                        comand.Parameters.AddWithValue("@retardos", DBNull.Value);
                        comand.Parameters.AddWithValue("@total_min_retardo", DBNull.Value);
                        comand.Parameters.AddWithValue("@id_tipo_retardo", DBNull.Value);
                        comand.Parameters.AddWithValue("@id_horario", id_horario);
                        comand.ExecuteNonQuery();
                        con.Close();
                    }
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
                            string consulta = "INSERT INTO registros VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida, @horas_trabajadas, @retardos,@total_min_retardo, @id_tipo_retardo, @id_horario)";
                            Conexion con2 = new Conexion();
                            SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                            conexion2.Open();
                            SqlCommand comand = new SqlCommand(consulta, conexion2);
                            comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                            comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                            comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                            comand.Parameters.AddWithValue("@fecha_entrada", DBNull.Value);
                            comand.Parameters.AddWithValue("@fecha_salida", fecha_evento);
                            comand.Parameters.AddWithValue("@horas_trabajadas", 0);
                            comand.Parameters.AddWithValue("@retardos", DBNull.Value);
                            comand.Parameters.AddWithValue("@total_min_retardo", DBNull.Value);
                            comand.Parameters.AddWithValue("@id_tipo_retardo", DBNull.Value);
                            comand.Parameters.AddWithValue("@id_horario", id_horario);
                            comand.ExecuteNonQuery();
                            conexion2.Close();
                        }
                        else if(fecha_entrada != null && fecha_salida == null)
                        {
                            TimeSpan hr_entrada = new TimeSpan(fecha_entrada.Value.Hour, fecha_entrada.Value.Minute - tolerancia, fecha_entrada.Value.Second);
                            TimeSpan hr_salida = new TimeSpan(fecha_evento.Hour, fecha_evento.Minute + tolerancia, fecha_evento.Second);
                            //TimeSpan hr_evento = new TimeSpan(fecha_evento.Hour, fecha_evento.Minute + tolerancia, fecha_evento.Second);
                            int horas_trabajadas = 0;

                            //VALIDACION PARA HORAS EXTRAS A BODEGA, ESTA FIJO POR EL ID DE LA SUCURSAL DE BODEGA
                            if (id_sucursal != 101)
                            {
                                /*TimeSpan hr_entrada = new TimeSpan(fecha_entrada.Value.Hour, fecha_entrada.Value.Minute - tolerancia, fecha_entrada.Value.Second);
                                TimeSpan hr_salida = new TimeSpan(fecha_evento.Hour, fecha_evento.Minute + tolerancia, fecha_evento.Second);
                                //TimeSpan hr_evento = new TimeSpan(fecha_evento.Hour, fecha_evento.Minute + tolerancia, fecha_evento.Second);
                                int horas_trabajadas = 0;*/

                                //VALIDACION PARA SABER COMO SE CALCULAN LAS HORAS TRABAJADAS
                                if (Math.Abs(hr_salida.TotalMinutes - hora_salida.TotalMinutes) > Math.Abs(hr_salida.TotalMinutes - hora_salida_descanso.TotalMinutes))
                                {
                                    //SALIDA DE DESCANSO
                                    //VALIDAR LOS MINUTOS ANTES DE LA ENTADA Y DESPUES DE LA HORA DE SALIDA DEL HORARIO
                                    if ((hr_salida <= hora_salida_descanso) && (hr_entrada >= hora_entrada))
                                    {
                                        horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes-tolerancia) - (hr_entrada.TotalMinutes+ tolerancia));
                                    }
                                    else if ((hr_salida > hora_salida_descanso) && (hr_entrada >= hora_entrada))
                                    {
                                        horas_trabajadas = Convert.ToInt32(hora_salida_descanso.TotalMinutes - (hr_entrada.TotalMinutes + tolerancia));
                                    }
                                    else if ((hr_salida <= hora_salida_descanso) && (hr_entrada < hora_entrada))
                                    {
                                        horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - hora_entrada.TotalMinutes);
                                    }
                                    else if ((hr_salida > hora_salida_descanso) && (hr_entrada < hora_entrada))
                                    {
                                        horas_trabajadas = Convert.ToInt32(hora_salida_descanso.TotalMinutes - hora_entrada.TotalMinutes);
                                    }

                                }
                                else if (Math.Abs(hr_salida.TotalMinutes - hora_salida.TotalMinutes) < Math.Abs(hr_salida.TotalMinutes - hora_salida_descanso.TotalMinutes))
                                {
                                    //SALIDA DE TURNO
                                    if (hora_entrada_descanso != new TimeSpan(00, 00, 00))
                                    {
                                        //ENTRADA DESCANSO
                                        if (hr_entrada > hora_salida)
                                        {
                                            horas_trabajadas = 0;
                                        }
                                        else if ((hr_salida <= hora_salida) && (hr_entrada >= hora_entrada_descanso))
                                        {
                                            horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - (hr_entrada.TotalMinutes + tolerancia));
                                        }
                                        else if ((hr_salida > hora_salida) && (hr_entrada >= hora_entrada_descanso))
                                        {
                                            horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - (hr_entrada.TotalMinutes + tolerancia));
                                        }
                                        else if ((hr_salida <= hora_salida) && (hr_entrada < hora_entrada_descanso))
                                        {
                                            horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - hora_entrada_descanso.TotalMinutes);
                                        }
                                        else if ((hr_salida > hora_salida) && (hr_entrada < hora_entrada_descanso))
                                        {
                                            horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - hora_entrada_descanso.TotalMinutes);
                                        }
                                    }
                                    else
                                    {
                                        //ENTRADA DE TURNO
                                        if (hr_entrada > hora_salida_descanso && hora_salida_descanso != new TimeSpan(00, 00, 00))
                                        {
                                            horas_trabajadas = 0;
                                        }
                                        else if ((hr_salida <= hora_salida) && (hr_entrada >= hora_entrada))
                                        {
                                            horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - (hr_entrada.TotalMinutes + tolerancia));
                                        }
                                        else if ((hr_salida > hora_salida) && (hr_entrada >= hora_entrada))
                                        {
                                            horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - (hr_entrada.TotalMinutes + tolerancia));
                                        }
                                        else if ((hr_salida <= hora_salida) && (hr_entrada < hora_entrada))
                                        {
                                            horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - hora_entrada.TotalMinutes);
                                        }
                                        else if ((hr_salida > hora_salida) && (hr_entrada < hora_entrada))
                                        {
                                            horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - hora_entrada.TotalMinutes);
                                        }
                                    }

                                }
                            }
                            else
                            {
                                horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - (hr_entrada.TotalMinutes + tolerancia));
                            }

                            con.Close();
                            //HACER UPDATE PARA INSERTAR LA FECHA DE SALIDA
                            string consulta = "UPDATE registros WITH (TABLOCK) SET fecha_salida = @fecha_evento, horas_trabajadas = @horas_trabajadas WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada < '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and fecha_salida is Null and fecha_entrada = (SELECT MAX(fecha_entrada) FROM registros WHERE id_checador=@checador and id_empleado = @empleado and id_sucursal = @sucursal); ";
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
                            comand.Parameters.AddWithValue("@horas_trabajadas", horas_trabajadas);
                            comand.ExecuteNonQuery();
                            conexion2.Close();
                        }
                    }
                    else
                    {
                        con.Close();
                        //GUARDAR NUEVO EVENTO CON PURA SALIDA
                        string consulta = "INSERT INTO registros VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida, @horas_trabajadas, @retardos,@total_min_retardo, @id_tipo_retardo, @id_horario)";
                        Conexion con2 = new Conexion();
                        SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                        conexion2.Open();
                        SqlCommand comand = new SqlCommand(consulta, conexion2);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", DBNull.Value);
                        comand.Parameters.AddWithValue("@fecha_salida", fecha_evento);
                        comand.Parameters.AddWithValue("@horas_trabajadas", 0);
                        comand.Parameters.AddWithValue("@retardos", DBNull.Value);
                        comand.Parameters.AddWithValue("@total_min_retardo", DBNull.Value);
                        comand.Parameters.AddWithValue("@id_tipo_retardo", DBNull.Value);
                        comand.Parameters.AddWithValue("@id_horario", id_horario);
                        comand.ExecuteNonQuery();
                        conexion2.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //FUNCION PARA GUARDAR EL DESCANSO
        public void guardarDescanso(int id_checador, int id_empleado, int id_sucursal, int id_horario, DateTime fecha_inicial, DateTime fecha_final)
        {
            try
            {
                Conexion conexion = new Conexion();
                SqlConnection con = new SqlConnection(conexion.cadenaConexion);

                //VERIFICAR SI TIENE CHEQUEO O DESCANSO EL DIA QUE TIENE DESCANSO/VACACIONES
                string select2 = "SELECT * FROM registros WHERE id_empleado=@id and (fecha_entrada>=@fecha_inicial or fecha_salida>=@fecha_inicial) and (fecha_entrada<=@fecha_final or fecha_salida<=@fecha_final)";//Consulta
                SqlCommand comando2 = new SqlCommand(select2, con);//Nuevo objeto sqlcommand
                comando2.Parameters.AddWithValue("@id", id_empleado);//Agregamos parametros a la consulta
                comando2.Parameters.AddWithValue("@fecha_inicial", fecha_inicial);
                comando2.Parameters.AddWithValue("@fecha_final", fecha_final);
                con.Open();//abre la conexion
                SqlDataReader lector = comando2.ExecuteReader();//Ejecuta el comadno
                if (!lector.HasRows)//Revisa si hay resultados
                {
                    //GUARDAR NUEVO EVENTO CON DESCANSO
                    string consulta = "INSERT INTO registros VALUES (@id_checador,@id_empleado,@id_sucursal,@fecha_entrada, @fecha_salida, @horas_trabajadas, @retardos,@total_min_retardo, @id_tipo_retardo, @id_horario)";
                    Conexion con2 = new Conexion();
                    SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                    conexion2.Open();
                    SqlCommand comand = new SqlCommand(consulta, conexion2);
                    comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                    comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                    comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                    comand.Parameters.AddWithValue("@fecha_entrada", fecha_inicial);
                    comand.Parameters.AddWithValue("@fecha_salida", fecha_inicial);
                    comand.Parameters.AddWithValue("@horas_trabajadas", 480);
                    comand.Parameters.AddWithValue("@retardos", DBNull.Value);
                    comand.Parameters.AddWithValue("@total_min_retardo", DBNull.Value);
                    comand.Parameters.AddWithValue("@id_tipo_retardo", DBNull.Value);
                    comand.Parameters.AddWithValue("@id_horario", id_horario);
                    comand.ExecuteNonQuery();
                    conexion2.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //FUNCION PARA RECALCULAR HORAS TRABAJADAS Y RETARDOS
        public void Recalcular_HorasTrabajadas(int id_checador, int id_empleado, int id_sucursal, DateTime fecha_evento, TimeSpan hora_entrada, TimeSpan hora_salida, TimeSpan hora_entrada_descanso, TimeSpan hora_salida_descanso, int tolerancia, string horario)
        {
            try
            {
                Conexion conexion = new Conexion();
                SqlConnection con = new SqlConnection(conexion.cadenaConexion);

                DateTime fecha_salida, fecha_entrada;

                string select = "SELECT * FROM registros WHERE id_checador =@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_entrada < '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000' and fecha_salida > '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T00:00:00.000' and fecha_salida < '" + fecha_evento.Year + "-" + fecha_evento.Month.ToString("d2") + "-" + fecha_evento.Day.ToString("d2") + "T23:59:59.000';";//Consulta
                SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                comando.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                comando.Parameters.AddWithValue("@id_empleado", id_empleado);
                comando.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                con.Open();//abre la conexion
                SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                if (lector.HasRows)//Revisa si hay resultados
                {
                    //OBTENER LOS CHEQUEOS DEL RANGO DE FECHAS 
                    while (lector.Read())
                    {
                        fecha_entrada = lector.GetDateTime(lector.GetOrdinal("fecha_entrada"));
                        fecha_salida = lector.GetDateTime(lector.GetOrdinal("fecha_salida"));

                        //RECALCULAR LOS RETRDOS
                        TimeSpan fecha_event = new TimeSpan(fecha_entrada.Hour, fecha_entrada.Minute, fecha_entrada.Second);
                        TimeSpan fecha_retardo = TimeSpan.FromMinutes(hora_entrada.TotalMinutes + Convert.ToDouble(tolerancia));
                        TimeSpan fecha_retardo2 = TimeSpan.FromMinutes(hora_entrada_descanso.TotalMinutes + Convert.ToDouble(tolerancia));
                        double minutos_retardo, minutos_retardo2;
                        int retardo = 1;

                        //RECALCULAR LAS HORAS TRABAJADAS
                        TimeSpan hr_entrada = new TimeSpan(fecha_entrada.Hour, fecha_entrada.Minute - tolerancia, fecha_entrada.Second);
                        TimeSpan hr_salida = new TimeSpan(fecha_salida.Hour, fecha_salida.Minute + tolerancia, fecha_salida.Second);
                        int horas_trabajadas = 0;

                        //VALIDACION PARA SABER COMO SE CALCULAN LAS HORAS TRABAJADAS
                        if (Math.Abs(hr_salida.TotalMinutes - hora_salida.TotalMinutes) > Math.Abs(hr_salida.TotalMinutes - hora_salida_descanso.TotalMinutes))
                        {
                            //SALIDA DE DESCANSO
                            //VALIDAR LOS MINUTOS ANTES DE LA ENTADA Y DESPUES DE LA HORA DE SALIDA DEL HORARIO
                            if ((hr_salida <= hora_salida_descanso) && (hr_entrada >= hora_entrada))
                            {
                                horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - (hr_entrada.TotalMinutes + tolerancia));
                            }
                            else if ((hr_salida > hora_salida_descanso) && (hr_entrada >= hora_entrada))
                            {
                                horas_trabajadas = Convert.ToInt32(hora_salida_descanso.TotalMinutes - (hr_entrada.TotalMinutes + tolerancia));
                            }
                            else if ((hr_salida <= hora_salida_descanso) && (hr_entrada < hora_entrada))
                            {
                                horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - hora_entrada.TotalMinutes);
                            }
                            else if ((hr_salida > hora_salida_descanso) && (hr_entrada < hora_entrada))
                            {
                                horas_trabajadas = Convert.ToInt32(hora_salida_descanso.TotalMinutes - hora_entrada.TotalMinutes);
                            }

                        }
                        else if (Math.Abs(hr_salida.TotalMinutes - hora_salida.TotalMinutes) < Math.Abs(hr_salida.TotalMinutes - hora_salida_descanso.TotalMinutes))
                        {
                            //SALIDA DE TURNO
                            if (hora_entrada_descanso != new TimeSpan(00, 00, 00))
                            {
                                //ENTRADA DESCANSO
                                if (hr_entrada > hora_salida)
                                {
                                    horas_trabajadas = 0;
                                }
                                else if ((hr_salida <= hora_salida) && (hr_entrada >= hora_entrada_descanso))
                                {
                                    horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - (hr_entrada.TotalMinutes + tolerancia));
                                }
                                else if ((hr_salida > hora_salida) && (hr_entrada >= hora_entrada_descanso))
                                {
                                    horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - (hr_entrada.TotalMinutes + tolerancia));
                                }
                                else if ((hr_salida <= hora_salida) && (hr_entrada < hora_entrada_descanso))
                                {
                                    horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - hora_entrada_descanso.TotalMinutes);
                                }
                                else if ((hr_salida > hora_salida) && (hr_entrada < hora_entrada_descanso))
                                {
                                    horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - hora_entrada_descanso.TotalMinutes);
                                }
                            }
                            else
                            {
                                //ENTRADA DE TURNO
                                if (hr_entrada > hora_salida_descanso && hora_salida_descanso != new TimeSpan(00, 00, 00))
                                {
                                    horas_trabajadas = 0;
                                }
                                else if ((hr_salida <= hora_salida) && (hr_entrada >= hora_entrada))
                                {
                                    horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - (hr_entrada.TotalMinutes + tolerancia));
                                }
                                else if ((hr_salida > hora_salida) && (hr_entrada >= hora_entrada))
                                {
                                    horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - (hr_entrada.TotalMinutes + tolerancia));
                                }
                                else if ((hr_salida <= hora_salida) && (hr_entrada < hora_entrada))
                                {
                                    horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - hora_entrada.TotalMinutes);
                                }
                                else if ((hr_salida > hora_salida) && (hr_entrada < hora_entrada))
                                {
                                    horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - hora_entrada.TotalMinutes);
                                }
                            }
                        }

                        //GUARDAR RETARDO
                        if (fecha_event.TotalMinutes > fecha_retardo2.TotalMinutes && hora_entrada_descanso != new TimeSpan(00, 00, 00))
                        {
                            minutos_retardo2 = Math.Abs(fecha_event.TotalMinutes - fecha_retardo2.TotalMinutes);

                            //HACER UPDATE PARA INSERTAR LOS RETARDOS Y LAS HORAS TRABAJADAS RECALCULADAS
                            string consulta = "UPDATE registros SET horas_trabajadas = @horas_trabajadas, retardos = @retardos, total_min_retardo=@total_min_retardo WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada = @fecha_entrada and fecha_salida = @fecha_salida ; ";
                            Conexion con2 = new Conexion();
                            SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                            conexion2.Open();
                            SqlCommand comand = new SqlCommand(consulta, conexion2);
                            comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                            comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                            comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                            comand.Parameters.AddWithValue("@horas_trabajadas", horas_trabajadas);
                            comand.Parameters.AddWithValue("@retardos", retardo);
                            comand.Parameters.AddWithValue("@total_min_retardo", minutos_retardo2);
                            comand.Parameters.AddWithValue("@fecha_entrada", fecha_entrada);
                            comand.Parameters.AddWithValue("@fecha_salida", fecha_salida);
                            comand.ExecuteNonQuery();
                            conexion2.Close();
                        }
                        else if (fecha_event.TotalMinutes > fecha_retardo.TotalMinutes && fecha_event.TotalMinutes < hora_salida_descanso.TotalMinutes)
                        {
                            minutos_retardo = fecha_event.TotalMinutes - fecha_retardo.TotalMinutes;

                            //HACER UPDATE PARA INSERTAR LOS RETARDOS Y LAS HORAS TRABAJADAS RECALCULADAS
                            string consulta = "UPDATE registros SET horas_trabajadas = @horas_trabajadas, retardos = @retardos, total_min_retardo=@total_min_retardo WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada = @fecha_entrada and fecha_salida = @fecha_salida ; ";
                            Conexion con2 = new Conexion();
                            SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                            conexion2.Open();
                            SqlCommand comand = new SqlCommand(consulta, conexion2);
                            comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                            comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                            comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                            comand.Parameters.AddWithValue("@horas_trabajadas", horas_trabajadas);
                            comand.Parameters.AddWithValue("@retardos", retardo);
                            comand.Parameters.AddWithValue("@total_min_retardo", minutos_retardo);
                            comand.Parameters.AddWithValue("@fecha_entrada", fecha_entrada);
                            comand.Parameters.AddWithValue("@fecha_salida", fecha_salida);
                            comand.ExecuteNonQuery();
                            conexion2.Close();
                        }
                        else
                        {
                            //POR SI NO HAY RETARTOS

                            //HACER UPDATE PARA INSERTAR LOS RETARDOS Y LAS HORAS TRABAJADAS RECALCULADAS
                            string consulta = "UPDATE registros SET horas_trabajadas = @horas_trabajadas, retardos = @retardos, total_min_retardo=@total_min_retardo WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada = @fecha_entrada and fecha_salida = @fecha_salida ; ";
                            Conexion con2 = new Conexion();
                            SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                            conexion2.Open();
                            SqlCommand comand = new SqlCommand(consulta, conexion2);
                            comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                            comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                            comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                            comand.Parameters.AddWithValue("@horas_trabajadas", horas_trabajadas);
                            comand.Parameters.AddWithValue("@retardos", DBNull.Value);
                            comand.Parameters.AddWithValue("@total_min_retardo", DBNull.Value);
                            comand.Parameters.AddWithValue("@fecha_entrada", fecha_entrada);
                            comand.Parameters.AddWithValue("@fecha_salida", fecha_salida);
                            comand.ExecuteNonQuery();
                            conexion2.Close();
                        }
                    }
                    con.Close();                    
                }
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Horas recalculadas con éxito!";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //FUNCION PARA REGISTRAR UN CHEQUEO EN LA BD POR SI SE LE OLVIDO CHECAR
        public bool RegistrarChequeo(int id_checador, int id_empleado, int id_sucursal, DateTime fecha_evento, DateTime fecha_referencia, TimeSpan hora_entrada, TimeSpan hora_salida, TimeSpan hora_entrada_descanso, TimeSpan hora_salida_descanso, int tolerancia, int tipo_evento, int id_horario)
        {
            try
            {
                Conexion conexion = new Conexion();
                SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                DateTime? fecha_entrada, fecha_salida;
                double minutos_retardo, minutos_retardo2;
                int retardo = 1;

                TimeSpan fecha_event = new TimeSpan(fecha_evento.Hour, fecha_evento.Minute, fecha_evento.Second);

                TimeSpan fecha_retardo = TimeSpan.FromMinutes(hora_entrada.TotalMinutes + Convert.ToDouble(tolerancia));

                TimeSpan fecha_retardo2 = TimeSpan.FromMinutes(hora_entrada_descanso.TotalMinutes + Convert.ToDouble(tolerancia));

                //ENTRADA
                if (tipo_evento == 0)
                {                    
                    fecha_entrada = fecha_evento;
                    fecha_salida = fecha_referencia;
                    //GUARDAR RETARDO
                    if (fecha_event.TotalMinutes > fecha_retardo2.TotalMinutes && hora_entrada_descanso != new TimeSpan(00, 00, 00))
                    {
                        minutos_retardo2 = Math.Abs(fecha_event.TotalMinutes - fecha_retardo2.TotalMinutes);

                        //SACAR EL TIPO DE RETARDO
                        int id_tipo_retardo = 0;
                        if (minutos_retardo2 < 16)
                        {
                            id_tipo_retardo = 1;
                        }
                        else if (minutos_retardo2 >= 16 && minutos_retardo2 <= 30)
                        {
                            id_tipo_retardo = 2;
                        }
                        else if (minutos_retardo2 > 30)
                        {
                            id_tipo_retardo = 3;
                        }
                        else
                        {
                            id_tipo_retardo = 1;
                        }

                        //ACTUALIZAR ENTRADA DEL EVENTO
                        string consulta = "UPDATE registros SET fecha_entrada = @fecha_entrada, retardos = @retardos, total_min_retardo = @total_min_retardo, id_tipo_retardo = @id_tipo_retardo WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_salida=@fecha_salida";
                        con.Open();
                        SqlCommand comand = new SqlCommand(consulta, con);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", fecha_evento);
                        comand.Parameters.AddWithValue("@retardos", retardo);
                        comand.Parameters.AddWithValue("@total_min_retardo", minutos_retardo2);
                        comand.Parameters.AddWithValue("@fecha_salida", fecha_referencia);
                        comand.Parameters.AddWithValue("@id_tipo_retardo", id_tipo_retardo);
                        comand.ExecuteNonQuery();
                        con.Close();
                    }
                    else if (fecha_event.TotalMinutes > fecha_retardo.TotalMinutes && fecha_event.TotalMinutes < hora_salida_descanso.TotalMinutes)
                    {
                        minutos_retardo = fecha_event.TotalMinutes - fecha_retardo.TotalMinutes;

                        //SACAR EL TIPO DE RETARDO
                        int id_tipo_retardo = 0;
                        if (minutos_retardo < 16)
                        {
                            id_tipo_retardo = 1;
                        }
                        else if (minutos_retardo >= 16 && minutos_retardo <= 30)
                        {
                            id_tipo_retardo = 2;
                        }
                        else if (minutos_retardo > 30)
                        {
                            id_tipo_retardo = 3;
                        }
                        else
                        {
                            id_tipo_retardo = 1;
                        }

                        //ACTUALIZAR ENTRADA DEL EVENTO
                        string consulta = "UPDATE registros SET fecha_entrada = @fecha_entrada, retardos = @retardos, total_min_retardo = @total_min_retardo, id_tipo_retardo=@id_tipo_retardo WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_salida=@fecha_salida";
                        con.Open();
                        SqlCommand comand = new SqlCommand(consulta, con);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", fecha_evento);
                        comand.Parameters.AddWithValue("@retardos", retardo);
                        comand.Parameters.AddWithValue("@total_min_retardo", minutos_retardo);
                        comand.Parameters.AddWithValue("@id_tipo_retardo", id_tipo_retardo);
                        comand.Parameters.AddWithValue("@fecha_salida", fecha_referencia);
                        comand.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        //ACTUALIZAR ENTRADA DEL EVENTO
                        string consulta = "UPDATE registros SET fecha_entrada = @fecha_entrada, retardos = @retardos, total_min_retardo = @total_min_retardo, id_tipo_retardo=@id_tipo_retardo WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_salida=@fecha_salida";
                        con.Open();
                        SqlCommand comand = new SqlCommand(consulta, con);
                        comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                        comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                        comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                        comand.Parameters.AddWithValue("@fecha_entrada", fecha_evento);
                        comand.Parameters.AddWithValue("@retardos", DBNull.Value);
                        comand.Parameters.AddWithValue("@total_min_retardo", DBNull.Value);
                        comand.Parameters.AddWithValue("@id_tipo_retardo", DBNull.Value);
                        comand.Parameters.AddWithValue("@fecha_salida", fecha_referencia);
                        comand.ExecuteNonQuery();
                        con.Close();
                    }
                }
                //SALIDA
                else
                {
                    fecha_entrada = fecha_referencia;
                    fecha_salida = fecha_evento;
                    //HACER UPDATE PARA INSERTAR LA FECHA DE SALIDA
                    string consulta = "UPDATE registros SET fecha_salida = @fecha_evento WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada=@fecha_entrada";
                    Conexion con2 = new Conexion();
                    SqlConnection conexion2 = new SqlConnection(con2.cadenaConexion);
                    conexion2.Open();
                    SqlCommand comand = new SqlCommand(consulta, conexion2);
                    comand.Parameters.AddWithValue("@fecha_evento", fecha_evento);
                    comand.Parameters.AddWithValue("@fecha_entrada", fecha_entrada);
                    comand.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                    comand.Parameters.AddWithValue("@id_empleado", id_empleado);
                    comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                    comand.ExecuteNonQuery();
                    conexion2.Close();
                }

                //OBTENER HORAS TRABAJADAS
                TimeSpan hr_entrada = new TimeSpan(fecha_entrada.Value.Hour, fecha_entrada.Value.Minute - tolerancia, fecha_entrada.Value.Second);
                TimeSpan hr_salida = new TimeSpan(fecha_salida.Value.Hour, fecha_salida.Value.Minute + tolerancia, fecha_salida.Value.Second);
                int horas_trabajadas = 0;

                //VALIDACION PARA HORAS EXTRAS A BODEGA, ESTA FIJO POR EL ID DE LA SUCURSAL DE BODEGA
                if (id_sucursal != 101)
                {
                    /*TimeSpan hr_entrada = new TimeSpan(fecha_entrada.Value.Hour, fecha_entrada.Value.Minute - tolerancia, fecha_entrada.Value.Second);
                    TimeSpan hr_salida = new TimeSpan(fecha_evento.Hour, fecha_evento.Minute + tolerancia, fecha_evento.Second);
                    //TimeSpan hr_evento = new TimeSpan(fecha_evento.Hour, fecha_evento.Minute + tolerancia, fecha_evento.Second);
                    int horas_trabajadas = 0;*/

                    //VALIDACION PARA SABER COMO SE CALCULAN LAS HORAS TRABAJADAS
                    if (Math.Abs(hr_salida.TotalMinutes - hora_salida.TotalMinutes) > Math.Abs(hr_salida.TotalMinutes - hora_salida_descanso.TotalMinutes))
                    {
                        //SALIDA DE DESCANSO
                        //VALIDAR LOS MINUTOS ANTES DE LA ENTADA Y DESPUES DE LA HORA DE SALIDA DEL HORARIO
                        if ((hr_salida <= hora_salida_descanso) && (hr_entrada >= hora_entrada))
                        {
                            horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - (hr_entrada.TotalMinutes + tolerancia));
                        }
                        else if ((hr_salida > hora_salida_descanso) && (hr_entrada >= hora_entrada))
                        {
                            horas_trabajadas = Convert.ToInt32(hora_salida_descanso.TotalMinutes - (hr_entrada.TotalMinutes + tolerancia));
                        }
                        else if ((hr_salida <= hora_salida_descanso) && (hr_entrada < hora_entrada))
                        {
                            horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - hora_entrada.TotalMinutes);
                        }
                        else if ((hr_salida > hora_salida_descanso) && (hr_entrada < hora_entrada))
                        {
                            horas_trabajadas = Convert.ToInt32(hora_salida_descanso.TotalMinutes - hora_entrada.TotalMinutes);
                        }

                    }
                    else if (Math.Abs(hr_salida.TotalMinutes - hora_salida.TotalMinutes) < Math.Abs(hr_salida.TotalMinutes - hora_salida_descanso.TotalMinutes))
                    {
                        //SALIDA DE TURNO
                        if (hora_entrada_descanso != new TimeSpan(00, 00, 00))
                        {
                            //ENTRADA DESCANSO
                            if (hr_entrada > hora_salida)
                            {
                                horas_trabajadas = 0;
                            }
                            else if ((hr_salida <= hora_salida) && (hr_entrada >= hora_entrada_descanso))
                            {
                                horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - (hr_entrada.TotalMinutes + tolerancia));
                            }
                            else if ((hr_salida > hora_salida) && (hr_entrada >= hora_entrada_descanso))
                            {
                                horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - (hr_entrada.TotalMinutes + tolerancia));
                            }
                            else if ((hr_salida <= hora_salida) && (hr_entrada < hora_entrada_descanso))
                            {
                                horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - hora_entrada_descanso.TotalMinutes);
                            }
                            else if ((hr_salida > hora_salida) && (hr_entrada < hora_entrada_descanso))
                            {
                                horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - hora_entrada_descanso.TotalMinutes);
                            }
                        }
                        else
                        {
                            //ENTRADA DE TURNO
                            if (hr_entrada > hora_salida_descanso && hora_salida_descanso != new TimeSpan(00, 00, 00))
                            {
                                horas_trabajadas = 0;
                            }
                            else if ((hr_salida <= hora_salida) && (hr_entrada >= hora_entrada))
                            {
                                horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - (hr_entrada.TotalMinutes + tolerancia));
                            }
                            else if ((hr_salida > hora_salida) && (hr_entrada >= hora_entrada))
                            {
                                horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - (hr_entrada.TotalMinutes + tolerancia));
                            }
                            else if ((hr_salida <= hora_salida) && (hr_entrada < hora_entrada))
                            {
                                horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - hora_entrada.TotalMinutes);
                            }
                            else if ((hr_salida > hora_salida) && (hr_entrada < hora_entrada))
                            {
                                horas_trabajadas = Convert.ToInt32(hora_salida.TotalMinutes - hora_entrada.TotalMinutes);
                            }
                        }

                    }
                }
                else
                {
                    horas_trabajadas = Convert.ToInt32((hr_salida.TotalMinutes - tolerancia) - (hr_entrada.TotalMinutes + tolerancia));
                }

                //AGREGAR HORAS TABAJADAS AL REGISTRO
                //HACER UPDATE PARA INSERTAR LA FECHA DE SALIDA
                string consulta3 = "UPDATE registros SET horas_trabajadas=@horas_trabajadas WHERE id_checador=@id_checador and id_empleado = @id_empleado and id_sucursal = @id_sucursal and fecha_entrada=@fecha_entrada and fecha_salida= @fecha_salida";
                Conexion con3 = new Conexion();
                SqlConnection conexion3 = new SqlConnection(con3.cadenaConexion);
                conexion3.Open();
                SqlCommand comando = new SqlCommand(consulta3, conexion3);
                comando.Parameters.AddWithValue("@id_checador", id_checador);//Agregamos parametros a la consulta
                comando.Parameters.AddWithValue("@id_empleado", id_empleado);
                comando.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                comando.Parameters.AddWithValue("@horas_trabajadas", horas_trabajadas);
                comando.Parameters.AddWithValue("@fecha_entrada", fecha_entrada);
                comando.Parameters.AddWithValue("@fecha_salida", fecha_salida);
                comando.ExecuteNonQuery();
                conexion3.Close();
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Chequeo registrado con éxito";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

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

        //INSERTAR EN LA TABLA RESUMEN SEMANAL DE CHEQUEOS
        public void guardarResumen(DateTime fecha_inicial, DateTime fecha_final, string path)
        {
            //SACAR DIAS TRABAJADOS
            TimeSpan dias_maximos = fecha_final.AddDays(1.0).Subtract(fecha_inicial);

            double dias_trabajados;
            string departamento;
            int id_empleado=0, domingos, sabados, retardos_chicos, retardos_medianos, retardos_grandes, id_tipo_retardo, descansos, dias_vacaciones, faltas;
            double horas_trabajadas = 0;
            DateTime dia, hora;
            DateTime fecha_entrada, fecha_salida;
            int cont_borrado=0;
            Conexion conexion = new Conexion();
            using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
            {
                string select = "SELECT * FROM empleado WHERE Estatus = 'A'";//Consulta
                SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                con.Open();//abre la conexion
                SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                if (lector.HasRows)//Revisa si hay resultados
                {
                    ClaseAsignar_Horario AsignarHorario = new ClaseAsignar_Horario();
                    
                    //CICLO PARA TODOS LOS EMPLEADOS ACTIVOS PARA GENERAR EL RESUMEN
                    while (lector.Read())
                    {
                        id_empleado = lector.GetInt32(lector.GetOrdinal("id_empleado"));
                        departamento = lector.GetString(lector.GetOrdinal("departamento"));

                        if (AsignarHorario.verificar_existencia(id_empleado))
                        {
                            using (SqlConnection con2 = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                            {
                                dias_trabajados = 0;
                                horas_trabajadas = 0;
                                retardos_chicos = 0;
                                retardos_medianos = 0;
                                retardos_grandes = 0;
                                domingos = 0;
                                sabados = 0;
                                descansos = 0;
                                dias_vacaciones = 0;
                                faltas = 0;

                                //VARIABLE QUE SIRVE PARA REALIZAR LA CONSULTA Y QUE ABARQUE EL DIA
                                DateTime fecha_final_2 = fecha_final.AddDays(1.0);

                                string select2 = "SELECT * FROM registros WHERE id_empleado=@id and (fecha_entrada>@fecha_inicial or fecha_salida>@fecha_inicial) and (fecha_entrada<@fecha_final or fecha_salida<@fecha_final)";//Consulta
                                SqlCommand comando2 = new SqlCommand(select2, con2);//Nuevo objeto sqlcommand
                                comando2.Parameters.AddWithValue("@id", id_empleado);//Agregamos parametros a la consulta
                                comando2.Parameters.AddWithValue("@fecha_inicial", fecha_inicial);
                                comando2.Parameters.AddWithValue("@fecha_final", fecha_final_2);
                                con2.Open();//abre la conexion
                                SqlDataReader lector2 = comando2.ExecuteReader();//Ejecuta el comadno
                                if (lector2.HasRows)//Revisa si hay resultados
                                {
                                    while (lector2.Read())//Lee una linea de los resultados
                                    {

                                        //SACAR MINUTOS TRABAJADOS
                                        if (!lector2.IsDBNull(lector2.GetOrdinal("horas_trabajadas")))
                                        {
                                            horas_trabajadas = horas_trabajadas + lector2.GetInt32(lector2.GetOrdinal("horas_trabajadas"));
                                        }

                                        //SACAR DOMINGOS
                                        if (!lector2.IsDBNull(lector2.GetOrdinal("fecha_entrada")))
                                        {
                                            fecha_entrada = lector2.GetDateTime(lector2.GetOrdinal("fecha_entrada"));
                                            dia = new DateTime(fecha_entrada.Year, fecha_entrada.Month, fecha_entrada.Day);
                                            hora = fecha_entrada;
                                            if (dia.DayOfWeek.ToString() == "Sunday" && hora.Subtract(dia) != new TimeSpan(0, 0, 0))
                                            {
                                                domingos += 1;
                                            }
                                        }
                                        else
                                        {
                                            fecha_salida = lector2.GetDateTime(lector2.GetOrdinal("fecha_salida"));
                                            dia = new DateTime(fecha_salida.Year, fecha_salida.Month, fecha_salida.Day);
                                            hora = fecha_salida;
                                            if (dia.DayOfWeek.ToString() == "Sunday" && hora.Subtract(dia) != new TimeSpan(0, 0, 0))
                                            {
                                                domingos += 1;
                                            }
                                        }

                                        //SACAR SABADOS
                                        if (!lector2.IsDBNull(lector2.GetOrdinal("fecha_entrada")))
                                        {
                                            fecha_entrada = lector2.GetDateTime(lector2.GetOrdinal("fecha_entrada"));
                                            dia = new DateTime(fecha_entrada.Year, fecha_entrada.Month, fecha_entrada.Day);
                                            hora = fecha_entrada;
                                            if (dia.DayOfWeek.ToString() == "Saturday" && hora.Subtract(dia) != new TimeSpan(0, 0, 0))
                                            {
                                                sabados += 1;
                                            }
                                        }
                                        else
                                        {
                                            fecha_salida = lector2.GetDateTime(lector2.GetOrdinal("fecha_salida"));
                                            dia = new DateTime(fecha_salida.Year, fecha_salida.Month, fecha_salida.Day);
                                            hora = fecha_salida;
                                            if (dia.DayOfWeek.ToString() == "Saturday" && hora.Subtract(dia) != new TimeSpan(0, 0, 0))
                                            {
                                                sabados += 1;
                                            }
                                        }

                                        //SACAR DESCANSOS
                                        if (!lector2.IsDBNull(lector2.GetOrdinal("id_horario")) && lector2.GetInt32(lector2.GetOrdinal("id_horario")) == 0)
                                        {
                                            descansos += 1;
                                        }

                                        //SACAR VACACIONES
                                        if (!lector2.IsDBNull(lector2.GetOrdinal("id_horario")) && lector2.GetInt32(lector2.GetOrdinal("id_horario")) == 1)
                                        {
                                            dias_vacaciones += 1;
                                        }

                                        //SACAR CONTEO DE RETARDOS POR TIPO
                                        if (!lector2.IsDBNull(lector2.GetOrdinal("id_tipo_retardo")))
                                        {
                                            id_tipo_retardo = lector2.GetInt32(lector2.GetOrdinal("id_tipo_retardo"));
                                            if (id_tipo_retardo == 1)
                                            {
                                                retardos_chicos += 1;
                                            }
                                            else if (id_tipo_retardo == 2)
                                            {
                                                retardos_medianos += 1;
                                            }
                                            else if (id_tipo_retardo == 3)
                                            {
                                                retardos_grandes += 1;
                                            }
                                        }
                                    }

                                    con2.Close();
                                }
                                else
                                {
                                    con2.Close();
                                }
                            }

                            //SUMAR LAS HORAS DE LOS SABADOS FALTANTES A LOS GODINEZ
                            if (departamento == "019" || departamento == "726" || departamento == "056" || departamento == "044" || departamento == "053" || departamento == "048" || departamento == "055" || departamento == "047" || departamento == "046" || departamento == "045" || departamento == "725"  )
                            {
                                horas_trabajadas += (sabados * 180);
                            }

                            //SACAR DIAS TRABAJADOS POR CADA 8 HORAS
                            dias_trabajados = horas_trabajadas / 480;


                            //REDONDEAR DIAS
                            if ((dias_trabajados - Math.Floor(dias_trabajados)) < .5)
                            {
                                dias_trabajados = Math.Floor(dias_trabajados);
                            }
                            else
                            {
                                dias_trabajados = Math.Ceiling(dias_trabajados);
                            }

                            for (DateTime fecha = fecha_inicial; fecha <= fecha_final; fecha = fecha.AddDays(1.0))
                            {
                                SqlConnection con3 = new SqlConnection(conexion.cadenaConexion);

                                //VERIFICAR SI TIENE CHEQUEO EL DIA
                                string select2 = "SELECT * FROM registros WHERE id_empleado=@id and (fecha_entrada>=@fecha_inicial or fecha_salida>=@fecha_inicial) and (fecha_entrada<@fecha_final or fecha_salida<@fecha_final)";//Consulta
                                SqlCommand comando2 = new SqlCommand(select2, con3);//Nuevo objeto sqlcommand
                                comando2.Parameters.AddWithValue("@id", id_empleado);//Agregamos parametros a la consulta
                                comando2.Parameters.AddWithValue("@fecha_inicial", fecha);
                                comando2.Parameters.AddWithValue("@fecha_final", fecha.AddDays(1.0));
                                con3.Open();//abre la conexion
                                SqlDataReader lector2 = comando2.ExecuteReader();//Ejecuta el comadno
                                if (!lector2.HasRows)//Revisa si hay resultados
                                {
                                    faltas += 1;
                                }
                                con3.Close();
                            }

                            //SACAR LOS DIAS MAXIMOS QUE PUEDEN TRABAJAR
                            if (dias_trabajados > (dias_maximos.Days-faltas))
                            {
                                dias_trabajados = (dias_maximos.Days - faltas);
                            }

                            //VERIFICAR SI DIAS TRABAJADOS JUNTO CON FALTAS SON MAYORES A LOS DIAS MAXIMOS (POSIBLES)
                            /*f ((dias_trabajados + faltas) > dias_maximos.Days)
                            {
                                //SE LE RESTAN LAS FALTAS A LOS DIAS TRABAJADOS
                                dias_trabajados = dias_trabajados - faltas;
                            }*/

                            //GUARDAR EL ARCHIVO
                            if (!File.Exists(path))
                            {
                                File.Create(path).Dispose();
                                File.AppendAllText(path, id_empleado + "," + dias_trabajados + "," + faltas + "," + retardos_chicos + "," + retardos_medianos + "," + retardos_grandes + "," + domingos + "\n" + Environment.NewLine);
                            }
                            else if (File.Exists(path))
                            {
                                if (cont_borrado == 0)
                                {
                                    File.Delete(path);
                                    cont_borrado = 1;
                                }
                                else
                                {
                                    File.AppendAllText(path, id_empleado + "," + dias_trabajados + "," + faltas + "," + retardos_chicos + "," + retardos_medianos + "," + retardos_grandes + "," + domingos + "\n" + Environment.NewLine);
                                }
                            }
                        }
                        
                    }
                    con.Close();
                }
                else
                {
                    con.Close();
                }
            }
        }
    }
}
