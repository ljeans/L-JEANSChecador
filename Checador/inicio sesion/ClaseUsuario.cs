using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Checador.inicio_sesion
{
    class ClaseUsuario
    {
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public int id_rol { get; set; }
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;

        }

        //FUNCION PARA ENTRAR EN EL LOGIN
        public bool Entrar()
        {
            try
            {
                Conexion conexion = new Conexion();
                //SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "select * from usuario where usuario=@usuario and password=@contraseña";//Consulta
                    SqlCommand comando = new SqlCommand(select, con);//Nuevo objeto sqlcommand
                    comando.Parameters.AddWithValue("@usuario", usuario);//Agregamos parametros a la consulta
                    comando.Parameters.AddWithValue("@contraseña", contraseña);
                    con.Open();//abre la conexion
                    SqlDataReader lector = comando.ExecuteReader();//Ejecuta el comadno
                    if (lector.HasRows)//Revisa si hay resultados
                    {
                        lector.Read();//Lee una linea de los resultados
                        //this.id = ;//Asignacion a atributos
                        //get ordinal regresa el indice de la fila
                        //el nombre especificado en el parametro 
                        usuario = lector.GetString(lector.GetOrdinal("usuario"));
                        contraseña = lector.GetString(lector.GetOrdinal("password"));
                        id_rol = lector.GetInt32(lector.GetOrdinal("id_rol"));
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
                return false;
            }

        }

        //FUNCION PARA REGISTRAR UN USUARIO
        public void guardarUsuario()
        {

            try
            {
                //Registrar USUARIO
                string consulta = "INSERT INTO usuario VALUES (@usuario,@password, @id_rol)";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@usuario", usuario);
                comand.Parameters.AddWithValue("@password", contraseña);
                comand.Parameters.AddWithValue("@id_rol", id_rol);

                comand.ExecuteNonQuery();
                conexion.Close();
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Usuario registrado con éxito: " + usuario;
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

    }
}
