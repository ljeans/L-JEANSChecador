using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Data.SqlClient;

namespace Checador
{
    public partial class sucursales : Checador.formularios_padres.formpadre
    {
        //SE CREA LA INSTANCIA AL OBJETO DE LA CLASE SUCURSAL
        ClaseSucursal Sucursal = new ClaseSucursal();
        ClaseHorario horario = new ClaseHorario();
        formularios_padres.mensaje_info mensaje = new formularios_padres.mensaje_info();
        formularios_padres.Mensajes confirmacion = new formularios_padres.Mensajes();
        validacion validar = new validacion();
        int idhorario, contador;
        public bool respuesta = false;

        public sucursales()
        {
            InitializeComponent();
        }

        private void sucursales_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
           
            // TODO: This line of code loads data into the 'dataSet_Checador.horarios' table. You can move, or remove it, as needed.
            this.horariosTableAdapter.Fill(this.dataSet_Checador.horarios);
            txt_id.Focus();
            //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
            CheckForIllegalCrossThreadCalls = false;

            //*************   HACER FORMULARIO RESPONSIVO   *****************
            double porcentaje_ancho = (Convert.ToDouble(Width) / 1362);
            double porcentaje_alto = (Convert.ToDouble(Height) / 741);
=======
            try
            {
                // TODO: This line of code loads data into the 'dataSet_Checador.horarios' table. You can move, or remove it, as needed.
                this.horariosTableAdapter.Fill(this.dataSet_Checador.horarios);
                txt_id.Focus();
                //INSTRUCCION PARA QUE NO HAYA PROBLEMAS CON LOS HILOS
                CheckForIllegalCrossThreadCalls = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
>>>>>>> dd17f2229b68e772f8ae207c964dafd87e3af09f

            foreach (Control x in this.Controls)
            {
                if (x.HasChildren)
                {
                    foreach (Control y in x.Controls)
                    {
                        if (y.HasChildren)
                        {
                            foreach (Control z in y.Controls)
                            {
                                if (z.HasChildren)
                                {
                                    foreach (Control w in z.Controls)
                                    {
                                        if (w is TextBox | w is Label | w is Button | w is MaskedTextBox | w is DateTimePicker | w is ComboBox | w is RadioButton | w is PictureBox | w is GroupBox | w is DataGridView | w is NumericUpDown)
                                        {
                                            if ((w is Label & porcentaje_ancho <= 0.8) | (w is DateTimePicker & porcentaje_ancho <= 0.8) | (w is NumericUpDown & porcentaje_ancho <= 0.8) | (w is Button & porcentaje_ancho <= 0.8) | (w is ComboBox & porcentaje_ancho <= 0.8) | (w is RadioButton & porcentaje_ancho <= 0.8))
                                            {
                                                if (w is Label)
                                                {
                                                    w.Font = new Font("Microsoft Sans Serif", 10);
                                                }
                                                w.Font = new Font("Microsoft Sans Serif", 11);
                                            }
                                            double posicionx = Convert.ToDouble(w.Location.X) * porcentaje_ancho;
                                            double posiciony = Convert.ToDouble(w.Location.Y) * porcentaje_alto;
                                            double ancho = w.Width * porcentaje_ancho;
                                            double alto = w.Height * porcentaje_alto;

                                            w.Left = Convert.ToInt32(posicionx);
                                            w.Top = Convert.ToInt32(posiciony);
                                            w.Width = Convert.ToInt32(ancho);
                                            w.Height = Convert.ToInt32(alto);
                                        }
                                    }
                                }
                                if (z is TextBox | z is Label | z is Button | z is MaskedTextBox | z is DateTimePicker | z is ComboBox | z is RadioButton | z is PictureBox | z is GroupBox | z is DataGridView)
                                {

                                    if ((z is Label & porcentaje_ancho <= 0.8) | (z is Button & porcentaje_ancho <= 0.8) | (z is ComboBox & porcentaje_ancho <= 0.8) | (z is RadioButton & porcentaje_ancho <= 0.8))
                                    {
                                        z.Font = new Font("Microsoft Sans Serif", 11);
                                    }

                                    double posicionx = Convert.ToDouble(z.Location.X) * porcentaje_ancho;
                                    double posiciony = Convert.ToDouble(z.Location.Y) * porcentaje_alto;
                                    double ancho = z.Width * porcentaje_ancho;
                                    double alto = z.Height * porcentaje_alto;

                                    z.Left = Convert.ToInt32(posicionx);
                                    z.Top = Convert.ToInt32(posiciony);
                                    z.Width = Convert.ToInt32(ancho);
                                    z.Height = Convert.ToInt32(alto);
                                }
                            }
                        }
                        if (y is TextBox | y is Label | y is Button | y is MaskedTextBox | y is DateTimePicker | y is ComboBox | y is RadioButton | y is Panel | y is TabControl)
                        {
                            if ((y is Button & porcentaje_ancho <= 0.8) | (y is RadioButton & porcentaje_ancho <= 0.8))
                            {
                                y.Font = new Font("Microsoft Sans Serif", 12);
                            }

                            double posicionx = Convert.ToDouble(y.Location.X) * porcentaje_ancho;
                            double posiciony = Convert.ToDouble(y.Location.Y) * porcentaje_alto;
                            double ancho = y.Width * porcentaje_ancho;
                            double alto = y.Height * porcentaje_alto;

                            y.Left = Convert.ToInt32(posicionx);
                            y.Top = Convert.ToInt32(posiciony);
                            y.Width = Convert.ToInt32(ancho);
                            y.Height = Convert.ToInt32(alto);
                        }
                    }
                }
                if (x is TextBox | x is Label | x is Button | x is MaskedTextBox | x is DateTimePicker | x is ComboBox | x is RadioButton | x is Panel | x is TabControl)
                {
                    double posicionx = Convert.ToDouble(x.Location.X) * porcentaje_ancho;
                    double posiciony = Convert.ToDouble(x.Location.Y) * porcentaje_alto;
                    double ancho = x.Width * porcentaje_ancho;
                    double alto = x.Height * porcentaje_alto;

                    x.Left = Convert.ToInt32(posicionx);
                    x.Top = Convert.ToInt32(posiciony);
                    x.Width = Convert.ToInt32(ancho);
                    x.Height = Convert.ToInt32(alto);
                }
            }
            //********************************************************************************************

        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rb_registrar_CheckedChanged(object sender, EventArgs e)
        {
            txt_id.Enabled = true;
            rb_mod_activo.Checked = true;
            groupBox4.Visible = false;
            tabControlBase.SelectedTab = tabPage1;
            btn_modificar.Enabled = false;
            btn_modificar.Visible = false;
            btn_registrar.Visible = true;
        }

        private void rb_modificar_CheckedChanged(object sender, EventArgs e)
        {
            rb_mod_activo.Checked = true;
            groupBox4.Visible = true;
            btn_modificar.Enabled = true;
            btn_modificar.Visible = true;
            btn_registrar.Visible = false;
            txt_id_mod.Text = "";
            tabControlBase.SelectedTab = tabPage3;

        }

        private void rb_buscar_CheckedChanged(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Sucursal' table. You can move, or remove it, as needed.
            this.vista_SucursalTableAdapter.Fill(this.dataSet_Checador.Vista_Sucursal);
            tabControlBase.SelectedTab = tabPage2;
        }


        //REGISTRAR///////////////////////////////////////////////////////////////////////////
        //CLICK AL BOTON REGISTRAR
        //FUNCION PARA REGITAR SUCURSAL EN LA BASE DE DATOS

        private void responder(object sender, EventArgs e)
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                Enabled = true;
                respuesta = confirmacion.respuesta;

                if (respuesta == true)
                {
                    Sucursal.Modificar_Sucursal();
                    //FUNCION PAR RECARGAR EL DATAGRID
                    // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Sucursal' table. You can move, or remove it, as needed.
                    this.vista_SucursalTableAdapter.Fill(this.dataSet_Checador.Vista_Sucursal);
                    Limpiar();
                    tabControlBase.SelectedTab = tabPage3;
                    txt_id_mod.Focus();
                }
                else
                {

                }
                confirmacion = null;
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                MessageBox.Show(ex.ToString());
            }
            
        }

        void vaciar_instancia_mensaje(Object sender, EventArgs e)
        {
            mensaje = null;
            Enabled = true;
        }

        private void btn_registrar_Click(object sender, EventArgs e)
        {
            // SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.Registrar));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();        
        }

        public void Registrar()
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                Sucursal.calle = txt_domicilio_calle.Text.ToUpper();
                Sucursal.codigo_postal = txt_domicilio_cp.Text.ToUpper();
                Sucursal.colonia = txt_domicilio_colonia.Text.ToUpper();
                Sucursal.estado = txt_domicilio_estado.Text.ToUpper();
                Sucursal.estatus = "A";
                Sucursal.id = Convert.ToInt32(txt_id.Text);
                Sucursal.municipio = txt_domicilio_municipio.Text.ToUpper();
                Sucursal.nombre = txt_nombre.Text.ToUpper();
                Sucursal.num_ext = txt_domicilio_num_ext.Text;
                Sucursal.num_int = txt_domicilio_num_int.Text;
                Sucursal.pais = txt_domicilio_pais.Text.ToUpper();
                Sucursal.poblacion = txt_domicilio_pob.Text.ToUpper();
                Sucursal.telefono = txt_telefono.Text;

                if (Sucursal.id.ToString() == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el id de la sucursal.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_id.Focus();
                }
                else if (Sucursal.nombre == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el nombre de la sucursal.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_nombre.Focus();
                }
                else if (Sucursal.telefono == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el numero de telefono de";
                    mensaje.lbl_info2.Text = "la sucursal.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_telefono.Focus();
                }
                else if (Sucursal.calle == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado la calle donde se encuetra";
                    mensaje.lbl_info2.Text = "la sucursal.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_domicilio_calle.Focus();
                }
                else if (Sucursal.num_ext == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el num. exterior de la";
                    mensaje.lbl_info2.Text = "sucursal.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_domicilio_num_ext.Focus();
                }
               
                else if (Sucursal.colonia == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado la colonia donde se ";
                    mensaje.lbl_info2.Text = " encuentra la sucursal.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_domicilio_colonia.Focus();
                }
                else if (Sucursal.codigo_postal == "")
                {
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "No ha ingresado el codigo postal donde ";
                    mensaje.lbl_info2.Text = "se encuentra la sucursal.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                    txt_domicilio_cp.Focus();
                }
                else
                {
                    Sucursal.guardarSucursal();

                    //FUNCION PAR RECARGAR EL DATAGRID
                    // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Sucursal' table. You can move, or remove it, as needed.
                    this.vista_SucursalTableAdapter.Fill(this.dataSet_Checador.Vista_Sucursal);

                    Limpiar();
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                }
                   
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                MessageBox.Show(ex.ToString());
            }
        }

        //FUNCION PARA LIMPIAR LOS COMPONENTES DEL FORMULARIO DESPUES DE HACER UN REGISTRO
        public void Limpiar()
        {
            txt_domicilio_calle.Text = "";
            txt_domicilio_colonia.Text = "";
            txt_domicilio_cp.Text = "";
            txt_domicilio_estado.Text = "";
            txt_domicilio_municipio.Text = "";
            txt_domicilio_num_ext.Text = "";
            txt_domicilio_num_int.Text = "";
            txt_domicilio_pais.Text = "";
            txt_domicilio_pob.Text = "";
            txt_id.Text = "";
            txt_nombre.Text = "";
            txt_telefono.Text = "";
        
            txt_id.Focus();
            //Deshabilitar_Componentes();
        }

        //FUNCION PARA DESHABILITAR LOS COMPONENTES DEL FORMULARIO
        public void Deshabilitar_Componentes()
        {
            txt_domicilio_calle.Enabled = false;
            txt_domicilio_colonia.Enabled = false;
            txt_domicilio_cp.Enabled = false;
            txt_domicilio_estado.Enabled = false;
            txt_domicilio_municipio.Enabled = false;
            txt_domicilio_num_ext.Enabled = false;
            txt_domicilio_num_int.Enabled = false;
            txt_domicilio_pais.Enabled = false;
            txt_domicilio_pob.Enabled = false;
            txt_nombre.Enabled = false;
            txt_telefono.Enabled = false;
            btn_registrar.Enabled = false;
        }

        //FUNCION PARA HABILITAR LOS COMPONENTES DEL FORMULARIO
        public void Habilitar_Componentes()
        {
            txt_domicilio_calle.Enabled = true;
            txt_domicilio_colonia.Enabled = true;
            txt_domicilio_cp.Enabled = true;
            txt_domicilio_estado.Enabled = true;
            txt_domicilio_municipio.Enabled = true;
            txt_domicilio_num_ext.Enabled = true;
            txt_domicilio_num_int.Enabled = true;
            txt_domicilio_pais.Enabled = true;
            txt_domicilio_pob.Enabled = true;
            txt_nombre.Enabled = true;
            txt_telefono.Enabled = true;
            btn_registrar.Enabled = true;
        }

//MODIFICAR/////////////////////////////////////////////////////////////////////////////////
        private void btn_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = true;
                //Sucursal.id = Convert.ToInt32(txt_id_mod.Text);
                Sucursal.calle = txt_domicilio_calle.Text.ToUpper();
                Sucursal.codigo_postal = txt_domicilio_cp.Text;
                Sucursal.colonia = txt_domicilio_colonia.Text.ToUpper();
                Sucursal.estado = txt_domicilio_estado.Text.ToUpper();
                if (rb_mod_activo.Checked==true)
                {
                    Sucursal.estatus = "A";
                }
                else
                {
                    Sucursal.estatus = "I";
                }
                Sucursal.municipio = txt_domicilio_municipio.Text.ToUpper();
                Sucursal.nombre = txt_nombre.Text.ToUpper();
                Sucursal.num_ext = txt_domicilio_num_ext.Text;
                Sucursal.num_int = txt_domicilio_num_int.Text;
                Sucursal.pais = txt_domicilio_pais.Text.ToUpper();
                Sucursal.poblacion = txt_domicilio_pob.Text.ToUpper();
                Sucursal.telefono = txt_telefono.Text;
                txt_id_mod.Text = "";
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                confirmacion = new formularios_padres.Mensajes();
                confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea modificar la sucursal?";
                confirmacion.FormClosed += new FormClosedEventHandler(responder);
                confirmacion.Show();
                Enabled = false;
              
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //CAMBIAR EL CURSOR
                this.UseWaitCursor = false;
                MessageBox.Show(ex.ToString());
            }
        }

        //CARGAR DATOS AL FORMULARIO
        private void btn_mod_Click(object sender, EventArgs e)
        {
            try
            {
                txt_id.Enabled = false;
                Sucursal.id = Convert.ToInt32(txt_id_mod.Text);
                if (Sucursal.verificar_existencia(Sucursal.id))
                {
                    tabControlBase.SelectedTab = tabPage1;
                    txt_id.Text = Sucursal.id.ToString();
                    txt_nombre.Text = Sucursal.nombre;
                    txt_domicilio_calle.Text = Sucursal.calle;
                    txt_domicilio_num_ext.Text = Sucursal.num_ext;
                    txt_domicilio_num_int.Text = Sucursal.num_int;
                    txt_domicilio_colonia.Text = Sucursal.colonia;
                    txt_domicilio_cp.Text = Sucursal.codigo_postal;
                    txt_domicilio_pob.Text = Sucursal.poblacion;
                    txt_domicilio_municipio.Text = Sucursal.municipio;
                    txt_domicilio_estado.Text = Sucursal.estado;
                    txt_domicilio_pais.Text = Sucursal.pais;
                    txt_telefono.Text = Sucursal.telefono;

                    if (Sucursal.estatus.ToString() == "A")
                    {
                        rb_mod_activo.Checked = true;
                    }
                    else if (Sucursal.estatus.ToString() == "I")
                    {
                        rb_mod_inactivo.Checked = true;
                    }
                }
                else
                {
                    MessageBox.Show("Sucursal no registrada. Por favor intente de nuevo.");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "No ha ingresado el identificador.";
                mensaje.FormClosed += new FormClosedEventHandler(responder);
                mensaje.Show();
                Enabled = false;
            }
        }

        private void btn_dar_baja_Click(object sender, EventArgs e)
        {
            confirmacion = new formularios_padres.Mensajes();
            confirmacion.lbl_mensaje.Text = "¿Esta seguro que desea dar de baja";
            confirmacion.lbl_mensaje2.Text = "la sucursal?";
            confirmacion.FormClosed += new FormClosedEventHandler(darbaja);
            confirmacion.Show();
            Enabled = false;
        }

        private void darbaja(object sender, EventArgs e)
        {
            try
            {
                Enabled = true;
                respuesta = confirmacion.respuesta;

                if (respuesta == true)
                {
                    var row = dgv_sucursal.CurrentRow;
                    Sucursal.id = Convert.ToInt32(row.Cells[0].Value);
                    Sucursal.estatus = "I";
                    Sucursal.Eliminar_Sucursal();
                    //FUNCION PAR RECARGAR EL DATAGRID
                    // TODO: This line of code loads data into the 'dataSet_Checador.Vista_Sucursal' table. You can move, or remove it, as needed.
                    this.vista_SucursalTableAdapter.Fill(this.dataSet_Checador.Vista_Sucursal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

            ////////////////FILTRAR EL BUSCAR//////////////////////////
        private void txt_idbuscar_TextChanged(object sender, EventArgs e)
        {
            if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
            {
                this.vista_SucursalTableAdapter.Fill(this.dataSet_Checador.Vista_Sucursal);
                vistaSucursalBindingSource.Filter = "";
            }
            else
            {
                vistaSucursalBindingSource.Filter = "CONVERT([id_sucursal], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre] LIKE '*" + txt_nombrebuscar.Text + "*'";
            }
        }

        private void txt_nombrebuscar_TextChanged(object sender, EventArgs e)
        {
            if (txt_idbuscar.Text == "" && txt_nombrebuscar.Text == "")
            {
                this.vista_SucursalTableAdapter.Fill(this.dataSet_Checador.Vista_Sucursal);
                vistaSucursalBindingSource.Filter = "";
            }
            else
            {
                vistaSucursalBindingSource.Filter = "CONVERT([id_sucursal], 'System.String') LIKE " + "'" + txt_idbuscar.Text + "*' and [nombre] LIKE '*" + txt_nombrebuscar.Text + "*'";
            }
        }

        private void btn_b_modificar_Click(object sender, EventArgs e)
        {

            var row = dgv_sucursal.CurrentRow;
            Sucursal.id= Convert.ToInt32(row.Cells[0].Value);
            rb_modificar.Checked = true;
            txt_id_mod.Text = Convert.ToString(Sucursal.id);
            tabControlBase.SelectedTab = tabPage3;
            btn_ir_modificar.PerformClick();
        }

        private void txt_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void txt_telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void txt_domicilio_num_ext_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void txt_domicilio_num_int_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void txt_domicilio_cp_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
            validar.sinespacios(e);
        }

        private void txt_domicilio_pais_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.sololetras(e);
            if (string.IsNullOrEmpty(txt_domicilio_pais.Text))
            {

                errorProvider1.SetError(txt_domicilio_pais, "No ha ingresado el nombre de la sucursal.");

            }
            else
            {
                errorProvider1.SetError(txt_domicilio_pais, null);
                contador = contador + 1;
            }
        }

        private void txt_nombre_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_nombre.Text))
            {

                errorProvider1.SetError(txt_nombre, "No ha ingresado el nombre de la sucursal.");

            }
            else
            {
                errorProvider1.SetError(txt_nombre, null);
                contador = contador + 1;
            }
        }

        private void txt_telefono_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_telefono.Text))
            {

                errorProvider1.SetError(txt_telefono, "No ha ingresado el telefono de la sucursal.");

            }
            else
            {
                errorProvider1.SetError(txt_telefono, null);
                contador = contador + 1;
            }
        }

        private void txt_domicilio_calle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_domicilio_calle.Text))
            {

                errorProvider1.SetError(txt_domicilio_calle, "No ha ingresado la calle de la sucursal.");

            }
            else
            {
                errorProvider1.SetError(txt_domicilio_calle, null);
                contador = contador + 1;
            }
        }

        private void txt_domicilio_num_ext_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_domicilio_num_ext.Text))
            {

                errorProvider1.SetError(txt_domicilio_num_ext, "No ha ingresado el numero exterior de la sucursal.");

            }
            else
            {
                errorProvider1.SetError(txt_domicilio_num_ext, null);
                contador = contador + 1;
            }
        }

        private void txt_domicilio_num_int_Validating(object sender, CancelEventArgs e)
        {
            if (txt_domicilio_num_ext.Text == "")
            {
                mensaje = new formularios_padres.mensaje_info();
                mensaje.lbl_info.Text = "No puede ingresar un num. interior si ";
                mensaje.lbl_info2.Text = "no existe un num. exterior.";
                mensaje.FormClosed += new FormClosedEventHandler(responder);
                mensaje.Show();
                Enabled = false;
                txt_domicilio_num_ext.Focus();
            }
        }

        private void txt_domicilio_colonia_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_domicilio_colonia.Text))
            {

                errorProvider1.SetError(txt_domicilio_colonia, "No ha ingresado la colonia donde se encuentra la sucursal.");

            }
            else
            {
                errorProvider1.SetError(txt_domicilio_colonia, null);
                contador = contador + 1;
            }
        }

        private void txt_domicilio_pob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_domicilio_pob.Text))
            {

                errorProvider1.SetError(txt_domicilio_pob, "No ha ingresado la poblacion donde se ecuentra la sucursal.");

            }
            else
            {
                errorProvider1.SetError(txt_domicilio_pob, null);
                contador = contador + 1;
            }
        }

        private void txt_domicilio_municipio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_domicilio_municipio.Text))
            {

                errorProvider1.SetError(txt_domicilio_municipio, "No ha ingresado el municipio donde se ecuentra la sucursal.");

            }
            else
            {
                errorProvider1.SetError(txt_domicilio_municipio, null);
                contador = contador + 1;
            }

        }

        private void txt_domicilio_estado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(string.IsNullOrEmpty(txt_domicilio_estado.Text))
            {

                errorProvider1.SetError(txt_domicilio_estado, "No ha ingresado el estado donde se ecuentra la sucursal.");

            }
            else
            {
                errorProvider1.SetError(txt_domicilio_estado, null);
                contador = contador + 1;
            }
        }

        private void txt_id_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_id.Text))
            {

                errorProvider1.SetError(txt_id, "No ha ingresado el identificador de la sucursal.");

            }
            else
            {
                errorProvider1.SetError(txt_id, null);
                contador = contador + 1;
            }
        }
        //////////////////////////////////////////////////////////////////

        //FUNCION PARA CUANDO DEJE EL CAMPO DE TEXTO ID BUSQUE SI EXISTE LA SUCURSAL
        private void txt_id_Leave(object sender, EventArgs e)
        {
            //SE CREA UN HILO, SE CARGA CON EL METODO Y SE EJECUTA
            Thread hilo_secundario = new Thread(new ThreadStart(this.verificarExistencia));
            hilo_secundario.IsBackground = true;
            hilo_secundario.Start();
        }

        private void txt_domicilio_cp_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_domicilio_cp.Text))
            {

                errorProvider1.SetError(txt_domicilio_cp, "No ha ingresado el codigo postal de la sucursal.");

            }
            else
            {
                errorProvider1.SetError(txt_domicilio_cp, null);
                contador = contador + 1;
            }
        }

        private void txt_nombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_domicilio_calle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_domicilio_num_ext_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_domicilio_num_int_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_domicilio_colonia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_domicilio_cp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_domicilio_pais_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        public void verificarExistencia()
        {
            try
            {
                if (txt_id.Text != "")
                {
                    Sucursal.id = Convert.ToInt32(txt_id.Text);
                    if (Sucursal.verificar_existencia(Sucursal.id))
                    {
                        MessageBox.Show("El ID de la sucursal " + Sucursal.id + " ya existe. Ingrese otro ID");
                        txt_id.Text = "";
                        //CONDICION PARA INVOCAR EL TXT DESDE OTRO HILO
                        if (InvokeRequired)
                        {
                            Invoke(new Action(() => txt_id.Focus()));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 121 || ex.Number == 1232)
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error al conectarse a la base de datos.";
                    mensaje.lbl_info2.Text = "Verifique la conexión.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
                else
                {
                    //CAMBIAR EL CURSOR
                    this.UseWaitCursor = false;
                    mensaje = new formularios_padres.mensaje_info();
                    mensaje.lbl_info.Text = "Error referente a la base de datos";
                    mensaje.lbl_info2.Text = "Verifique los datos ingresados.";
                    mensaje.FormClosed += new FormClosedEventHandler(vaciar_instancia_mensaje);
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
