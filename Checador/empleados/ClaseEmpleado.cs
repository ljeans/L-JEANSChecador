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
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido_pat { get; set; }
        public string apellido_mat { get; set; }
        public string departamento { get; set; }
        //public int id_privilegio { get; set; }
        public string id_privilegio { get; set; }
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
        public double sueldo_diario { get; set; }
        public double sueldo_diario_integrado { get; set; }
        public double sueldo_base_quincenal { get; set; }
        public string tipo_salario { get; set; }
        public int dias_aguinaldo { get; set; }
        public int dias_vacaciones { get; set; }
        public string observaciones { get; set; }
        //public int id_horario { get; set; }
        public string tipo_horario { get; set; }
        public string tipo_contrato { get; set; }
        public string riesgo_puesto { get; set; }
        public string periodicidad_pago { get; set; }
        public string banco { get; set; }
        public string cuenta_bancaria { get; set; }
        public string email { get; set; }
        public string tarjeta_despensa { get; set; }
        public string clave_edenred { get; set; }

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
                string consulta = "INSERT INTO sucursal  VALUES (@id,@nombre, @apellido_pat,@apellido_mat, @departamento,@id_privilegio, @telefono, @calle,@colonia, @num_ext,@num_int, @codigo_postal,@poblacion,@municipio, @estado, @pais,@puesto,@NSS, @RFC, @CURP, @estatus, @fecha_alta, @fecha_baja, @sueldo_diario, @sueldo_diario_integrado, @sueldo_base_quincenal, @tipo_salario, @dias_aguinaldo, @dias_vacaciones, @observaciones,@tipo_jornada, @tipo_contrato, @riesgo_puesto, @periodicidad_pago, @banco, @cuenta_bancaria, @email, @tarjeta_despensa, @clave_edenred)";
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
                comand.Parameters.AddWithValue("@fecha_baja", fecha_baja);
                comand.Parameters.AddWithValue("@sueldo_diario", sueldo_diario);
                comand.Parameters.AddWithValue("@sueldo_diario_integrado", sueldo_diario_integrado);
                comand.Parameters.AddWithValue("@sueldo_base_quincenal", sueldo_base_quincenal);
                comand.Parameters.AddWithValue("@tipo_salario", tipo_salario);
                comand.Parameters.AddWithValue("@dias_aguinaldo", dias_aguinaldo);
                comand.Parameters.AddWithValue("@dias_vacaciones", dias_vacaciones);
                comand.Parameters.AddWithValue("@observaciones", observaciones);
                comand.Parameters.AddWithValue("@tipo_jornada", tipo_horario);
                comand.Parameters.AddWithValue("@tipo_contrato", tipo_contrato);
                comand.Parameters.AddWithValue("@riesgo_puesto", riesgo_puesto);
                comand.Parameters.AddWithValue("@periodicidad_pago", periodicidad_pago);
                comand.Parameters.AddWithValue("@banco", banco);
                comand.Parameters.AddWithValue("@cuenta_bancaria", cuenta_bancaria);
                comand.Parameters.AddWithValue("@email", email);
                comand.Parameters.AddWithValue("@tarjeta_despensa", tarjeta_despensa);
                comand.Parameters.AddWithValue("@clave_edenred", clave_edenred);

                comand.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Empleado registrado con éxito. ID= " + id.ToString());

            }
            catch (Exception e)
            {
                MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

        //FUNCION PARA ACTUALIZAR LOS DATOS DE UN EMPLEADO
        public void Modificar_Empleado()
        {
            try
            {
                string consulta = "UPDATE empleado SET nombre = @nombre, apellido_pat = @apellido_pat, apellido_mat=@apellido_mat, departamento=@departamento,id_privilegio=@id_privilegio, telefono=@telefono, calle = @calle, colonia = @colonia, num_ext = @num_ext, num_int=@num_int, codigo_postal=@codigo_postal, poblacion=@poblacion, municipio=@municipio, estado=@estado, pais=@pais, puesto=@puesto, NSS=@NSS, RFC=@RFC, CURP=@CURP, estatus=@estatus, fecha_alta=@fecha_alta, fecha_baja=@fecha_baja, sueldo_diario=@sueldo_diario, sueldo_diario_integrado=@sueldo_diario_integrado, sueldo_base_quincenal=@sueldo_base_quincenal, tipo_salario=@tipo_salario, dias_aguinaldo=@dias_aguinaldo, dias_vacaciones=@dias_vacaciones, observaciones=@observaciones,tipo_jornada=@tipo_jornada, tipo_contrato=@tipo_contrato, riesgo_puesto=@riesgo_puesto, periodicidad_pago=@periodicidad_pago, banco=@banco, cuenta_bancaria=@cuenta_bancaria, email=@email, tarjeta_despensa=@tarjeta_despensa, clave_edenred=@clave_edenred WHERE id_empleado = @id";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
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
                comand.Parameters.AddWithValue("@fecha_baja", fecha_baja);
                comand.Parameters.AddWithValue("@sueldo_diario", sueldo_diario);
                comand.Parameters.AddWithValue("@sueldo_diario_integrado", sueldo_diario_integrado);
                comand.Parameters.AddWithValue("@sueldo_base_quincenal", sueldo_base_quincenal);
                comand.Parameters.AddWithValue("@tipo_salario", tipo_salario);
                comand.Parameters.AddWithValue("@dias_aguinaldo", dias_aguinaldo);
                comand.Parameters.AddWithValue("@dias_vacaciones", dias_vacaciones);
                comand.Parameters.AddWithValue("@observaciones", observaciones);
                comand.Parameters.AddWithValue("@tipo_jornada", tipo_horario);
                comand.Parameters.AddWithValue("@tipo_contrato", tipo_contrato);
                comand.Parameters.AddWithValue("@riesgo_puesto", riesgo_puesto);
                comand.Parameters.AddWithValue("@periodicidad_pago", periodicidad_pago);
                comand.Parameters.AddWithValue("@banco", banco);
                comand.Parameters.AddWithValue("@cuenta_bancaria", cuenta_bancaria);
                comand.Parameters.AddWithValue("@email", email);
                comand.Parameters.AddWithValue("@tarjeta_despensa", tarjeta_despensa);
                comand.Parameters.AddWithValue("@clave_edenred", clave_edenred);
                comand.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Empleado modificado con éxito. ID= " + id.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
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
                MessageBox.Show("Empleado dado de baja con éxito. ID= " + id.ToString());

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
                        apellido_mat = lector.GetString(lector.GetOrdinal("apelldio_mat"));
                        departamento = lector.GetString(lector.GetOrdinal("departamento"));
                        //id_privilegio = lector.GetInt32(lector.GetOrdinal("id_privilegio"));
                        id_privilegio = lector.GetString(lector.GetOrdinal("id_privilegio"));
                        telefono = lector.GetString(lector.GetOrdinal("telefono"));
                        calle = lector.GetString(lector.GetOrdinal("calle"));
                        colonia = lector.GetString(lector.GetOrdinal("colonia"));
                        num_ext = lector.GetString(lector.GetOrdinal("num_ext"));
                        num_int = lector.GetString(lector.GetOrdinal("num_int"));
                        codigo_postal = lector.GetString(lector.GetOrdinal("codigo_postal"));
                        poblacion = lector.GetString(lector.GetOrdinal("poblcion"));
                        municipio = lector.GetString(lector.GetOrdinal("municipio"));
                        estado = lector.GetString(lector.GetOrdinal("estado"));
                        pais = lector.GetString(lector.GetOrdinal("pais"));
                        puesto = lector.GetString(lector.GetOrdinal("puesto"));
                        NSS = lector.GetString(lector.GetOrdinal("NSS"));
                        RFC = lector.GetString(lector.GetOrdinal("RFC"));
                        CURP = lector.GetString(lector.GetOrdinal("CURP"));
                        estatus = lector.GetString(lector.GetOrdinal("estatus"));
                        fecha_alta = lector.GetDateTime(lector.GetOrdinal("fecha_alta"));
                        fecha_baja = lector.GetDateTime(lector.GetOrdinal("fecha_baja"));
                        sueldo_diario = lector.GetDouble(lector.GetOrdinal("sueldo_diario"));
                        sueldo_diario_integrado = lector.GetDouble(lector.GetOrdinal("sueldo_diario_integrado"));
                        sueldo_base_quincenal = lector.GetDouble(lector.GetOrdinal("sueldo_base_quincenal"));
                        tipo_salario = lector.GetString(lector.GetOrdinal("tipo_salario"));
                        dias_aguinaldo = lector.GetInt32(lector.GetOrdinal("dias_aguinaldo"));
                        dias_vacaciones = lector.GetInt32(lector.GetOrdinal("dias_vacaciones"));
                        observaciones = lector.GetString(lector.GetOrdinal("observaciones"));
                        tipo_horario = lector.GetString(lector.GetOrdinal("tipo_jornada"));
                        tipo_contrato = lector.GetString(lector.GetOrdinal("tipo_contrato"));
                        riesgo_puesto = lector.GetString(lector.GetOrdinal("riesgo_puesto"));
                        periodicidad_pago = lector.GetString(lector.GetOrdinal("periodicidad_pago"));
                        banco = lector.GetString(lector.GetOrdinal("banco"));
                        cuenta_bancaria = lector.GetString(lector.GetOrdinal("cuenta_bancaria"));
                        email = lector.GetString(lector.GetOrdinal("email"));
                        tarjeta_despensa = lector.GetString(lector.GetOrdinal("tarjeta_despensa"));
                        clave_edenred = lector.GetString(lector.GetOrdinal("clave_edenred"));
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
                MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
                return false;
            }
        }



    }
}
