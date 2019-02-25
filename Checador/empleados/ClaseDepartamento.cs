using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Checador.empleados
{
    class ClaseDepartamento
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string locacion { get; set; }

        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();

        //FUNCION PARA GUARDAR NUEVO DEPARTAMENTO
        public void guardarDepartamento()
        {
            try
            {
                //Registrar DEPARTAMENTO
                string consulta = "INSERT INTO departamento VALUES (@id_departamento,@nombre, @locacion)";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id_departamento", id);
                comand.Parameters.AddWithValue("@nombre", nombre);
                comand.Parameters.AddWithValue("@locacion", locacion);

                comand.ExecuteNonQuery();
                conexion.Close();

                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Departamento registrado con exito. ID= " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.ShowDialog();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;
        }

        //FUNCION PARA VERIFICAR LA EXISTENCIA DEL DEPARTAMENTO
        public bool verificar_departamento(string id)//Funcion que hace el select retorna false si no hay resultados
        {
            try
            {
                Conexion conexion = new Conexion();
                //SqlConnection con = new SqlConnection(conexion.cadenaConexion);
                using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))//utilizamos la clase conexion
                {
                    string select = "SELECT * FROM departamento WHERE id_departamento=@id";//Consulta
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
                        id = lector.GetString(lector.GetOrdinal("id_departamento"));
                        nombre = lector.GetString(lector.GetOrdinal("nombre"));
                        locacion = lector.GetString(lector.GetOrdinal("locacion"));
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
                throw;
            }
        }

        //FUNCION PARA ACTUALIZAR LOS DATOS DE UN DEPARTAMENTO
        public void Modificar_Departamento()
        {
            try
            {
                string consulta = "UPDATE departamento SET nombre = @nombre, locacion = @locacion WHERE id_departamento = @id";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);
                comand.Parameters.AddWithValue("@nombre", nombre);
                comand.Parameters.AddWithValue("@locacion", locacion);
                comand.ExecuteNonQuery();
                conexion.Close();
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Departamento modificado con éxito.ID = " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.Show();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //FUNCION PARA ELIMINAR UN DEPARTAMENTO EN LA BASE DE DATOS
        public void eliminarDepartamento()
        {
            try
            {
                //ELIMINAR DEPARTAMENTO
                string consulta = "DELETE from departamento WHERE id_departamento = @id";
                Conexion con = new Conexion();
                SqlConnection conexion = new SqlConnection(con.cadenaConexion);
                conexion.Open();
                SqlCommand comand = new SqlCommand(consulta, conexion);
                comand.Parameters.AddWithValue("@id", id);

                comand.ExecuteNonQuery();
                conexion.Close();

                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "Departamento eliminado con exito. ID= " + id.ToString();
                mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                mensaje.ShowDialog();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Upss.. Ocurrió un error, por favor vuelva a intentarlo.");
            }
        }

    }
}
