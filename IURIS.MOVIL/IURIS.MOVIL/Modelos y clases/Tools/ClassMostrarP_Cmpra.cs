using System;
using IURIS.MOVIL.Utils;
using System.Collections.Generic;
using System.Text;

namespace IURIS.MOVIL.Modelos_y_clases
{
    public class ClassMostrarP_Cmpra
    {

        public bool MostrarPantalla()
        {

            if (Settings.CountParaNumAleatorio != "" && Settings.NumAleatorio != 0)
            {

                if (int.Parse(Settings.CountParaNumAleatorio) != Settings.NumAleatorio)
                {
                    int num = int.Parse(Settings.CountParaNumAleatorio) + 1;
                    Settings.CountParaNumAleatorio = num.ToString();
                    return false;
                }
                else
                {
                    Random rnd = new Random();
                    Settings.NumAleatorio = rnd.Next(5, 100);
                    Settings.CountParaNumAleatorio = "1";

                    return true;
                }
            }
            else
            {
                Settings.CountParaNumAleatorio = "1";
                Settings.NumAleatorio = 1;
                return false;
            }

        }

    }

    
}
