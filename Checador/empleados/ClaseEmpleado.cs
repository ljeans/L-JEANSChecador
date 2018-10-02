using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Checador
{
    public class ClaseEmpleado
    {
        ClaseHorario horario = new ClaseHorario();
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        public int id { get; set; }
        public int id_sucursal { get; set; }
        public int id_horario { get; set; }
        public string nombre { get; set; }
        public string apellido_pat { get; set; }
        public string apellido_mat { get; set; }
        public string departamento { get; set; }
        //public int id_privilegio { get; set; }
        public int id_privilegio { get; set; }
        public string telefono { get; set; }
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
        public decimal sueldo_diario { get; set; }
        public decimal sueldo_diario_integrado { get; set; }
        public decimal sueldo_base_quincenal { get; set; }
        public string tipo_salario { get; set; }
        public int dias_aguinaldo { get; set; }
        public int dias_vacaciones { get; set; }
        public string observaciones { get; set; }
        //public int id_horario { get; set; }
        public string tipo_contrato { get; set; }
        public string riesgo_puesto { get; set; }
        public string periodicidad_pago { get; set; }
        public string banco { get; set; }
        public string cuenta_bancaria { get; set; }
        public string email { get; set; }
        public string tarjeta_despensa { get; set; }
        public string clave_edenred { get; set; }
        public string password { get; set; }
        public int horas_extra { get; set; }
        public TimeSpan hora_entrada { get; set; }
        public int retardos { get; set; }
        public int total_min_retardo { get; set; }

        //LUEGO VEMOS
        public TimeSpan hr_entrada { get; set; }
        public TimeSpan hr_salida { get; set; }
        public DateTime guardarsuc;

        //FUNCION PARA OBTENER EL ID MAXIMO DEL EMPLEADO POR SI ES AUTOINCREMENTABLE EL ID
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

        //obtienes el Horario del empleado por ID Empleado
        public void obtenerIdHorario(int id_empleado)
        {
            string consulta = "Select id_horario From empleado where id_empleado=@id_empleado";
            Conexion con = new Conexion();
            SqlConnection conexion = new SqlConnection(con.cadenaConexion);
            SqlCommand comand = new SqlCommand(consulta, conexion);
            comand.Parameters.AddWithValue("@id_empleado", id_empleado);
            conexion.Open();

            SqlDataReader lector = comand.ExecuteReader();//Ejecuta el comadno
            if (lector.HasRows)//Revisa si hay resultados
            {
                lector.Read();//Lee una linea de los resultados
                              //this.id = ;//Asignacion a atributos
                              //get ordinal regresa el indice de la fila
                              //el Nombre especificado en el parametro 
                id_horario = lector.GetInt32(lector.GetOrdinal("id_horario"));
                conexion.Close();
            }
            else
            {
                conexion.Close();
            }
        }



        //FUNCION PARA REGISTRAR UN EMPLEADO
        public void guardarEmpleado()
        {

            try
            {
                //Registrar EMPLEADO
                try
                {
                    id = (obtenerIdMaximo() + 1);
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message);
                    id = 1;
                }
                string consulta = "INSERT INTO empleado  VALUES (@id,@nombre, @apellido_pat,@apellido_mat, @departamento,@id_privilegio, @telefono, @calle, @num_ext,@num_int, @colonia, @codigo_postal,@poblacion,@municipio, @estado, @pais,@puesto, @RFC, @CURP, @estatus, @fecha_alta,@observaciones, @email, @fecha_baja, @NSS, @tipo_contrato, @sueldo_diario, @sueldo_diario_integrado, @sueldo_base_quincenal, @tipo_salario, @dias_aguinaldo, @dias_vacaciones, @riesgo_puesto, @periodicidad_pago, @banco, @cuenta_bancaria, @tarjeta_despensa, @clave_edenred, @password, @horas_extra, @retardos, @total_min_retardo, @id_horario)";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);
                comand.Parameters.AddWithValue("@nombre", nombre);
                comand.Parameters.AddWithValue("@apellido_pat", apellido_pat);
                comand.Parameters.AddWithValue("@apellido_mat", apellido_mat);
                comand.Parameters.AddWithValue("@departamento", departamento);
                comand.Parameters.AddWithValue("@id_privilegio", id_privilegio);
                comand.Parameters.AddWithValue("@telefono", telefono);
                comand.Parameters.AddWithValue("@calle", calle);
                comand.Parameters.AddWithValue("@num_ext", num_ext);
                comand.Parameters.AddWithValue("@num_int", num_int);
                comand.Parameters.AddWithValue("@colonia", colonia);             
                comand.Parameters.AddWithValue("@codigo_postal", codigo_postal);
                comand.Parameters.AddWithValue("@poblacion", poblacion);
                comand.Parameters.AddWithValue("@municipio", municipio);
                comand.Parameters.AddWithValue("@estado", estado);
                comand.Parameters.AddWithValue("@pais", pais);
                comand.Parameters.AddWithValue("@puesto", puesto);
                comand.Parameters.AddWithValue("@RFC", RFC);
                comand.Parameters.AddWithValue("@CURP", CURP);
                comand.Parameters.AddWithValue("@estatus", estatus);
                comand.Parameters.AddWithValue("@fecha_alta", fecha_alta);
                comand.Parameters.AddWithValue("@observaciones", observaciones);
                comand.Parameters.AddWithValue("@email", email);
                comand.Parameters.AddWithValue("@fecha_baja", DBNull.Value);
                comand.Parameters.AddWithValue("@NSS", NSS);
                comand.Parameters.AddWithValue("@tipo_contrato", tipo_contrato);
                comand.Parameters.AddWithValue("@sueldo_diario", sueldo_diario);
                comand.Parameters.AddWithValue("@sueldo_diario_integrado", sueldo_diario_integrado);
                comand.Parameters.AddWithValue("@sueldo_base_quincenal", sueldo_base_quincenal);
                comand.Parameters.AddWithValue("@tipo_salario", tipo_salario);
                comand.Parameters.AddWithValue("@dias_aguinaldo", dias_aguinaldo);
                comand.Parameters.AddWithValue("@dias_vacaciones", dias_vacaciones);
                comand.Parameters.AddWithValue("@riesgo_puesto", riesgo_puesto);
                comand.Parameters.AddWithValue("@periodicidad_pago", periodicidad_pago);
                comand.Parameters.AddWithValue("@banco", banco);
                comand.Parameters.AddWithValue("@cuenta_bancaria", cuenta_bancaria);
                comand.Parameters.AddWithValue("@tarjeta_despensa", tarjeta_despensa);
                comand.Parameters.AddWithValue("@clave_edenred", clave_edenred);
                comand.Parameters.AddWithValue("@password", password);
                comand.Parameters.AddWithValue("@horas_extra", 0);
                comand.Parameters.AddWithValue("@retardos", DBNull.Value);
                comand.Parameters.AddWithValue("@total_min_retardo", DBNull.Value);
                comand.Parameters.AddWithValue("@id_horario", id_horario);

                comand.ExecuteNonQuery();
                conexion.Close();

                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Empleado registrado con éxito. ID= " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

        //FUNCION PARA ACTUALIZAR LOS DATOS DE UN EMPLEADO
        public void Modificar_Empleado()
        {
            try
            {
                string consulta = "UPDATE empleado SET nombre = @nombre, apellido_pat = @apellido_pat, apellido_mat=@apellido_mat, departamento=@departamento,id_privilegio=@id_privilegio, telefono=@telefono, calle = @calle, colonia = @colonia, num_ext = @num_ext, num_int=@num_int, codigo_postal=@codigo_postal, poblacion=@poblacion, municipio=@municipio, estado=@estado, pais=@pais, puesto=@puesto, NSS=@NSS, RFC=@RFC, CURP=@CURP, estatus=@estatus, fecha_alta=@fecha_alta, fecha_baja=@fecha_baja, sueldo_diario=@sueldo_diario, sueldo_diario_integrado=@sueldo_diario_integrado, sueldo_base_quincenal=@sueldo_base_quincenal, tipo_salario=@tipo_salario, dias_aguinaldo=@dias_aguinaldo, dias_vacaciones=@dias_vacaciones, observaciones=@observaciones, tipo_contrato=@tipo_contrato, riesgo_puesto=@riesgo_puesto, periodicidad_pago=@periodicidad_pago, banco=@banco, cuenta_bancaria=@cuenta_bancaria, email=@email, tarjeta_despensa=@tarjeta_despensa, clave_edenred=@clave_edenred, password = @password, id_horario = @id_horario WHERE id_empleado = @id";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);
                comand.Parameters.AddWithValue("@nombre", nombre);
                comand.Parameters.AddWithValue("@apellido_pat", apellido_pat);
                comand.Parameters.AddWithValue("@apellido_mat", apellido_mat);
                comand.Parameters.AddWithValue("@departamento", departamento);
                comand.Parameters.AddWithValue("@id_privilegio", id_privilegio);
                comand.Parameters.AddWithValue("@telefono", telefono);
                comand.Parameters.AddWithValue("@calle", calle);
                comand.Parameters.AddWithValue("@colonia", colonia);
                comand.Parameters.AddWithValue("@num_ext", num_ext);
                comand.Parameters.AddWithValue("@num_int", num_int);
                comand.Parameters.AddWithValue("@codigo_postal", codigo_postal);
                comand.Parameters.AddWithValue("@poblacion", poblacion);
                comand.Parameters.AddWithValue("@municipio", municipio);
                comand.Parameters.AddWithValue("@estado", estado);
                comand.Parameters.AddWithValue("@pais", pais);
                comand.Parameters.AddWithValue("@puesto", puesto);
                comand.Parameters.AddWithValue("@NSS", NSS);
                comand.Parameters.AddWithValue("@RFC", RFC);
                comand.Parameters.AddWithValue("@CURP", CURP);
                comand.Parameters.AddWithValue("@estatus", estatus);
                comand.Parameters.AddWithValue("@fecha_alta", fecha_alta);
                comand.Parameters.AddWithValue("@fecha_baja", DBNull.Value);
                comand.Parameters.AddWithValue("@sueldo_diario", sueldo_diario);
                comand.Parameters.AddWithValue("@sueldo_diario_integrado", sueldo_diario_integrado);
                comand.Parameters.AddWithValue("@sueldo_base_quincenal", sueldo_base_quincenal);
                comand.Parameters.AddWithValue("@tipo_salario", tipo_salario);
                comand.Parameters.AddWithValue("@dias_aguinaldo", dias_aguinaldo);
                comand.Parameters.AddWithValue("@dias_vacaciones", dias_vacaciones);
                comand.Parameters.AddWithValue("@observaciones", observaciones);
                comand.Parameters.AddWithValue("@tipo_contrato", tipo_contrato);
                comand.Parameters.AddWithValue("@riesgo_puesto", riesgo_puesto);
                comand.Parameters.AddWithValue("@periodicidad_pago", periodicidad_pago);
                comand.Parameters.AddWithValue("@banco", banco);
                comand.Parameters.AddWithValue("@cuenta_bancaria", cuenta_bancaria);
                comand.Parameters.AddWithValue("@email", email);
                comand.Parameters.AddWithValue("@tarjeta_despensa", tarjeta_despensa);
                comand.Parameters.AddWithValue("@clave_edenred", clave_edenred);
                comand.Parameters.AddWithValue("@password", password);
                comand.Parameters.AddWithValue("@id_horario", id_horario);
                comand.ExecuteNonQuery();
                conexion.Close();

                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Empleado modificado con éxito. ID= " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
        }


        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;
        
        }

        //FUNCION PARA DAR DE BAJA UN EMPLEADO CAMBIANDO EL ESTATUS
        public void Eliminar_Empleado()
        {
            try
            {
                string eliminar = "Update empleado SET estatus=@estatus where id_empleado=@id";

                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(eliminar, conexion);
                comand.Parameters.AddWithValue("@estatus", estatus);
                comand.Parameters.AddWithValue("@id", id);
                comand.ExecuteNonQuery();

                conexion.Close();

                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Empleado dado de baja con éxito.ID = " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
              

            }
            catch (Exception e)
            {
                MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

        //FUNCION PARA VERIFICAR SI EL EMPLEADO YA EXISTE PREVIAMENTE EN LA BASE DE DATOS
        //RETORNA UN VALOR BOOL DEPENDIENDO SI ES V O F
        //ADEMAS CARGA LOS ATRIBUTOS DE LA CLASE CON LOS DATOS GUARDADOS DEL EMPLEADO
        //REFERENTE AL ID RECIBIDO COMO PARAMETRO
        public bool verificar_existencia(int id)//Funcion que hace el select retorna false si no hay resultados
        {
            try
            {
                Conexion conexion = new Conexion();
                //SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "SELECT * FROM empleado WHERE id_empleado=@id";//Consulta
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
                        id = lector.GetInt32(lector.GetOrdinal("id_empleado"));
                        nombre = lector.GetString(lector.GetOrdinal("nombre"));
                        apellido_pat = lector.GetString(lector.GetOrdinal("apellido_pat"));
                        apellido_mat = lector.GetString(lector.GetOrdinal("apellido_mat"));
                        departamento = lector.GetString(lector.GetOrdinal("departamento"));
                        id_privilegio = lector.GetInt32(lector.GetOrdinal("id_privilegio"));

                        //id_privilegio = lector.GetString(lector.GetOrdinal("id_privilegio"));
                        telefono = lector.GetString(lector.GetOrdinal("telefono"));
                        calle = lector.GetString(lector.GetOrdinal("calle"));
                        colonia = lector.GetString(lector.GetOrdinal("colonia"));
                        num_ext = lector.GetString(lector.GetOrdinal("num_ext"));
                        num_int = lector.GetString(lector.GetOrdinal("num_int"));
                        codigo_postal = lector.GetString(lector.GetOrdinal("codigo_postal"));
                        poblacion = lector.GetString(lector.GetOrdinal("poblacion"));
                        municipio = lector.GetString(lector.GetOrdinal("municipio"));
                        estado = lector.GetString(lector.GetOrdinal("estado"));
                        pais = lector.GetString(lector.GetOrdinal("pais"));
                        puesto = lector.GetString(lector.GetOrdinal("puesto"));
                        NSS = lector.GetString(lector.GetOrdinal("NSS"));
                        RFC = lector.GetString(lector.GetOrdinal("RFC"));
                        CURP = lector.GetString(lector.GetOrdinal("CURP"));
                        estatus = lector.GetString(lector.GetOrdinal("estatus"));
                        fecha_alta = lector.GetDateTime(lector.GetOrdinal("fecha_alta"));
                        sueldo_diario = lector.GetDecimal(lector.GetOrdinal("sueldo_diario"));
                        sueldo_diario_integrado = lector.GetDecimal(lector.GetOrdinal("sueldo_diario_integrado"));
                        sueldo_base_quincenal = lector.GetDecimal(lector.GetOrdinal("sueldo_base_quincenal"));
                        tipo_salario = lector.GetString(lector.GetOrdinal("tipo_salario"));
                        dias_aguinaldo = lector.GetInt32(lector.GetOrdinal("dias_aguinaldo"));
                        dias_vacaciones = lector.GetInt32(lector.GetOrdinal("dias_vacaciones"));
                        observaciones = lector.GetString(lector.GetOrdinal("observaciones"));
                        tipo_contrato = lector.GetString(lector.GetOrdinal("tipo_contrato"));
                        riesgo_puesto = lector.GetString(lector.GetOrdinal("riesgo_puesto"));
                        periodicidad_pago = lector.GetString(lector.GetOrdinal("periodicidad_pago"));
                        banco = lector.GetString(lector.GetOrdinal("banco"));
                        cuenta_bancaria = lector.GetString(lector.GetOrdinal("cuenta_bancaria"));
                        email = lector.GetString(lector.GetOrdinal("email"));
                        tarjeta_despensa = lector.GetString(lector.GetOrdinal("tarjeta_despensa"));
                        clave_edenred = lector.GetString(lector.GetOrdinal("clave_edenred"));
                        password = lector.GetString(lector.GetOrdinal("password"));
                        id_horario = lector.GetInt32(lector.GetOrdinal("id_horario"));
                        con.Close();
                     
                    }
                    else
                    {
                        con.Close();
                        return false;
                    }
                 
                }

                using (SqlConnection con2 = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "SELECT id_sucursal FROM empleado_sucursal WHERE id_empleado=@id";//Consulta
                    SqlCommand comando = new SqlCommand(select, con2);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@id", id);//Agregamos parametros a la consulta
                    con2.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        lector.Read();//Lee una linea de los resultados
                        //this.id = ;//Asignacion a atributos
                        //get ordinal regresa el indice de la fila
                        //el Nombre especificado en el parametro 
                        id_sucursal = lector.GetInt32(lector.GetOrdinal("id_sucursal"));
 
                        con2.Close();
                        return true;
                    }
                    else
                    {
                        con2.Close();
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

        public bool ObtenerSucursal(string suc)
        {
            try
            {
                Conexion conexion = new Conexion();
                //SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "SELECT id_sucursal FROM sucursal WHERE nombre=@suc";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@suc", suc);//Agregamos parametros a la consulta
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        lector.Read();//Lee una linea de los resultados
                        //this.id = ;//Asignacion a atributos
                        //get ordinal regresa el indice de la fila
                        //el Nombre especificado en el parametro
                        id_sucursal = lector.GetInt32(lector.GetOrdinal("id_sucursal"));
                      
                        con.Close();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("No hay sucursal");
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

        /*public bool verificar_horario(string horario)//Funcion que hace el select retorna false si no hay resultados
        {
            try
            {
                Conexion conexion = new Conexion();
                //SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "SELECT hr_entrada, hr_salida FROM horarios WHERE horario=@horario";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@horario", horario);//Agregamos parametros a la consulta
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        lector.Read();//Lee una linea de los resultados
                        //this.id = ;//Asignacion a atributos
                        //get ordinal regresa el indice de la fila
                        //el Nombre especificado en el parametro
                        hr_entrada = lector.GetTimeSpan(lector.GetOrdinal("hr_entrada"));
                        hr_salida = lector.GetTimeSpan(lector.GetOrdinal("hr_salida"));
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
        }*/

        //FUNCION PARA REGISTRAR EMPLEADO_SUCURSAL
        public void guardarEmpleado_Sucursal()
        {
            guardarsuc = fecha_alta;
            try
            {
                //Registrar SUCURSAL
                string consulta = "UPDATE empleado_sucursal SET fecha_salida=@fecha_salida Where id_empleado=@id and fecha_salida is Null;";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);
                comand.Parameters.AddWithValue("@fecha_salida", guardarsuc);
                comand.ExecuteNonQuery();
                conexion.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }

            try
            {
                //Registrar SUCURSAL
                string consulta = "INSERT INTO empleado_sucursal VALUES (@id_empleado,@id_sucursal, @fecha_entrada,@fecha_salida)";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id_empleado", id);
                comand.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                comand.Parameters.AddWithValue("@fecha_entrada", fecha_alta);
                
                comand.Parameters.AddWithValue("@fecha_salida", DBNull.Value);
                comand.ExecuteNonQuery();
                conexion.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }


        }


    }
}
