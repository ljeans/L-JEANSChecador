using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Checador
{
    class validacion
    {
        //METODO PARA VALIDAR CORREO ELECTRONICO
        //******************************************
        public bool validaremail(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                { return true; }
                else
                { return false; }
            }
            else
            { return false; }
        }

        //METODO PARA NO ACEPTAR ESPACIOS EN TXT
        //******************************************
        public void sinespacios(KeyPressEventArgs e)
        {
            try
            {
                if (char.IsSeparator(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

            }

        }

        //METODO PARA ACEPTAR SOLO LETRAS
        //*****************************************
        public void sololetras(KeyPressEventArgs e)
        {
            try
            {
                if (char.IsLetter(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

            }

        }

        //METODO PARA ACEPTAR SOLO NUMEROS
        //*********************************
        public void solonumeros(KeyPressEventArgs e)
        {
            try
            {
                if (char.IsNumber(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;

                }

                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

            }

        }

        //METODO PARA ACEPTAR SOLO IMPORTES(CANTIDADES EN PESOS).
        //************************************************************++
        public void soloimportes(KeyPressEventArgs e)
        {
            try
            {
                if (char.IsNumber(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;

                }
                else if (char.IsPunctuation(e.KeyChar))
                {
                    int i = 0;
                    if (i == 0)
                    {
                        e.Handled = false;
                        i = i + 1;
                    }

                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}
