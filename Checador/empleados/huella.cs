using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checador.empleados
{
    public partial class huella : Form
    {
        public huella()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_huella.Text == "1")
            {
                pictureBox1.Image = new System.Drawing.Bitmap(@"C:\Users\manue\Desktop\L-JEANSChecador\Checador\Resources\huella1.png");
            }
            else if (cbx_huella.Text == "0")
            {
                pictureBox1.Image = new System.Drawing.Bitmap(@"C:\Users\manue\Desktop\L-JEANSChecador\Checador\Resources\huella0.png");
            }
            else if (cbx_huella.Text == "2")
            {
                pictureBox1.Image = new System.Drawing.Bitmap(@"C:\Users\manue\Desktop\L-JEANSChecador\Checador\Resources\huella2.png");
            }
            else if (cbx_huella.Text == "3")
            {
                pictureBox1.Image = new System.Drawing.Bitmap(@"C:\Users\manue\Desktop\L-JEANSChecador\Checador\Resources\huella3.png");
            }
            else if (cbx_huella.Text == "4")
            {
                pictureBox1.Image = new System.Drawing.Bitmap(@"C:\Users\manue\Desktop\L-JEANSChecador\Checador\Resources\huella4.png");
            }
            else if (cbx_huella.Text == "5")
            {
                pictureBox1.Image = new System.Drawing.Bitmap(@"C:\Users\manue\Desktop\L-JEANSChecador\Checador\Resources\huella5.png");
            }
            else if (cbx_huella.Text == "6")
            {
                pictureBox1.Image = new System.Drawing.Bitmap(@"C:\Users\manue\Desktop\L-JEANSChecador\Checador\Resources\huella6.png");
            }
            else if (cbx_huella.Text == "7")
            {
                pictureBox1.Image = new System.Drawing.Bitmap(@"C:\Users\manue\Desktop\L-JEANSChecador\Checador\Resources\huella7.png");
            }
            else if (cbx_huella.Text == "8")
            {
                pictureBox1.Image = new System.Drawing.Bitmap(@"C:\Users\manue\Desktop\L-JEANSChecador\Checador\Resources\huella8.png");
            }
            else if (cbx_huella.Text == "9")
            {
                pictureBox1.Image = new System.Drawing.Bitmap(@"C:\Users\manue\Desktop\L-JEANSChecador\Checador\Resources\huella9.png");
            }


        }

        private void btn_siguiente2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
