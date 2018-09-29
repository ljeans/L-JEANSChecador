using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checador.formularios_padres
{
    public partial class Mensajes : Form
    {
        public Mensajes()
        {
            InitializeComponent();
        }

        public bool respuesta = false;

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            respuesta = true;
            Close();
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            respuesta = false;
            Close();
        }

        private void Mensajes_Load(object sender, EventArgs e)
        {

        }
    }
}
