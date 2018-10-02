using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Checador
{
    class ClaseSucursal
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string calle { get; set; }
        public string colonia { get; set; }
        public string num_ext { get; set; }
        public string num_int { get; set; }
        public string codigo_postal { get; set; }
        public string poblacion { get; set; }
        public string municipio { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
        public string telefono { get; set; }
        public string estatus { get; set; }
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();


        //FUNCION PARA OBTENER EL ID MAXIMO DE LA SUCURSAL POR SI ES AUTOINCREMENTABLE EL ID
        public int obtenerIdMaximo()
        {
            string consulta = "Select Max(id_sucursal) From sucursal";
            Conexion con = new Conexion();
            SqlConnection conexion = new SqlConnection(con.cadenaConexion);
            SqlCommand comand = new SqlCommand(consulta, conexion);
            conexion.Open();
            int idMaximo = Convert.ToInt32(comand.ExecuteScalar());
            conexion.Close();
            return idMaximo;
        }

<<<<<<< HEAD
       
=======
        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;

        }
>>>>>>> 86f3f8e2d6179768149f3614bc15fb6b8c888182

        //obtienes el Id sucursal por Nombre
        public void obtenerIdSucursal(string nombre)
        {
            string consulta = "Select id_sucursal From sucursal where nombre=@nombre";
            Conexion con = new Conexion();
            SqlConnection conexion = new SqlConnection(con.cadenaConexion);
            SqlCommand comand = new SqlCommand(consulta, conexion);
            comand.Parameters.AddWithValue("@nombre", nombre);
            conexion.Open();
<<<<<<< HEAD
            /*int id = Convert.ToInt32(comand.ExecuteScalar());
         
            conexion.Close();
            return id;*/
=======
>>>>>>> 86f3f8e2d6179768149f3614bc15fb6b8c888182

            SqlDataReader lector = comand.ExecuteReader();//Ejecuta el comadno
            if (lector.HasRows)//Revisa si hay resultados
            {
                lector.Read();//Lee una linea de los resultados
                              //this.id = ;//Asignacion a atributos
                              //get ordinal regresa el indice de la fila
                              //el Nombre especificado en el parametro 
                id = lector.GetInt32(lector.GetOrdinal("id_sucursal"));
                conexion.Close();
            }
            else
            {
                conexion.Close();
            }
        }


        //FUNCION PARA REGISTRAR UNA SUCURSAL
        public void guardarSucursal()
        {

            try
            {
                //Registrar SUCURSAL
                string consulta = "INSERT INTO sucursal  VALUES (@id,@nombre, @calle,@colonia, @num_ext,@num_int, @codigo_postal,@poblacion,@municipio, @estado, @pais, @telefono, @estatus)";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);
                comand.Parameters.AddWithValue("@nombre", nombre);
                comand.Parameters.AddWithValue("@calle", calle);
                comand.Parameters.AddWithValue("@colonia", colonia);
                comand.Parameters.AddWithValue("@num_ext", num_ext);
                comand.Parameters.AddWithValue("@num_int", num_int);
                comand.Parameters.AddWithValue("@codigo_postal", codigo_postal);
                comand.Parameters.AddWithValue("@poblacion", poblacion);
                comand.Parameters.AddWithValue("@municipio", municipio);
                comand.Parameters.AddWithValue("@estado", estado);
                comand.Parameters.AddWithValue("@pais", pais);
                comand.Parameters.AddWithValue("@telefono", telefono);
                comand.Parameters.AddWithValue("@estatus", estatus);


                comand.ExecuteNonQuery();
                conexion.Close();
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Sucursal registrada con éxito. ID= " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();

            }
            catch (Exception e)
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Upss.. Ocurrió un error, por favor vuelva a intentarlo.";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
                //MessageBox.Show(e.ToString());
            }
        }

        //FUNCION PARA ACTUALIZAR LOS DATOS DE UNA SUCURSAL
        public void Modificar_Sucursal()
        {
            try
            {
       
                string consulta = "UPDATE sucursal SET nombre = @nombre, calle = @calle, colonia = @colonia, num_ext = @num_ext, num_int=@num_int, codigo_postal=@codigo_postal, poblacion=@poblacion, municipio=@municipio, estado=@estado, pais=@pais, telefono=@telefono, estatus=@estatus WHERE id_sucursal = @id";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);
                comand.Parameters.AddWithValue("@nombre", nombre);
                comand.Parameters.AddWithValue("@calle", calle);
                comand.Parameters.AddWithValue("@colonia", colonia);
                comand.Parameters.AddWithValue("@num_ext", num_ext);
                comand.Parameters.AddWithValue("@num_int", num_int);
                comand.Parameters.AddWithValue("@codigo_postal", codigo_postal);
                comand.Parameters.AddWithValue("@poblacion", poblacion);
                comand.Parameters.AddWithValue("@municipio", municipio);
                comand.Parameters.AddWithValue("@estado", estado);
                comand.Parameters.AddWithValue("@pais", pais);
                comand.Parameters.AddWithValue("@telefono", telefono);
                comand.Parameters.AddWithValue("@estatus", estatus);

                comand.ExecuteNonQuery();
                conexion.Close();

                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Sucursal modificada con éxito. ID= " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
               // MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

        //FUNCION PARA DAR DE BAJA UNA SUCURSAL CAMBIANDO EL ESTATUS
        public void Eliminar_Sucursal()
        {
            try
            {
                string eliminar = "Update sucursal SET estatus=@estatus where id_sucursal=@id";

                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(eliminar, conexion);
                comand.Parameters.AddWithValue("@estatus", estatus);
                comand.Parameters.AddWithValue("@id", id);
                comand.ExecuteNonQuery();

                conexion.Close();

                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Sucursal dada de baja con éxito. ID= " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
          

            }
            catch (Exception e)
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Uuups... ocurrio un error, por favor vuelva a intentarlo";
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
        }

        //FUNCION PARA VERIFICAR SI LA SUCRUSAL YA EXISTE PREVIAMENTE EN LA BASE DE DATOS
        //RETORNA UN VALOR BOOL DEPENDIENDO SI ES V O F
        //ADEMAS CARGA LOS ATRIBUTOS DE LA CLASE CON LOS DATOS GUARDADOS DE LA SUCURSAL
        //REFERENTE AL ID RECIBIDO COMO PARAMETRO
        public bool verificar_existencia(int id)//Funcion que hace el select retorna false si no hay resultados
        {
            try
            {
                Conexion conexion = new Conexion();
                //SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "SELECT * FROM sucursal WHERE id_sucursal=@id";//Consulta
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
                        nombre = lector.GetString(lector.GetOrdinal("nombre"));
                        calle = lector.GetString(lector.GetOrdinal("calle"));
                        colonia = lector.GetString(lector.GetOrdinal("colonia"));
                        num_ext = lector.GetString(lector.GetOrdinal("num_ext"));
                        num_int = lector.GetString(lector.GetOrdinal("num_int"));
                        codigo_postal = lector.GetString(lector.GetOrdinal("codigo_postal"));
                        poblacion = lector.GetString(lector.GetOrdinal("poblacion"));
                        municipio = lector.GetString(lector.GetOrdinal("municipio"));
                        estado = lector.GetString(lector.GetOrdinal("estado"));
                        pais = lector.GetString(lector.GetOrdinal("pais"));
                        telefono = lector.GetString(lector.GetOrdinal("telefono"));
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
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
                MessageBox.Show(e.ToString());
                return false;
            }
        }


    }
}
