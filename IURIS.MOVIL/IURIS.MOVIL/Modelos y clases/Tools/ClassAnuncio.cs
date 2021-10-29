using System;
using IURIS.MOVIL.Utils;
using System.Collections.Generic;
using System.Text;
using Rg.Plugins.Popup.Services;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using MarcTron.Plugin;

namespace IURIS.MOVIL.Modelos_y_clases
{
    public class ClassAnuncio
    {

        public void MostrarAnuncioPantalla()
        {

            if (Settings.CountParaNumAleatorio != "" && Settings.NumAleatorio != 0)
            {

                if (int.Parse(Settings.CountParaNumAleatorio) != Settings.NumAleatorio)
                {
                    int num = int.Parse(Settings.CountParaNumAleatorio) + 1;
                    Settings.CountParaNumAleatorio = num.ToString();
                }
                else
                {
                    Random rnd = new Random();
                    Settings.NumAleatorio = rnd.Next(15, 100);
                    Settings.CountParaNumAleatorio = "1";

                    var test = CrossMTAdmob.Current.IsInterstitialLoaded().ToString();
                    CrossMTAdmob.Current.ShowInterstitial();
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");
                }
            }
            else
            {
                Settings.CountParaNumAleatorio = "1";
                Settings.NumAleatorio = 1;
            }

        }

    }

    
}
