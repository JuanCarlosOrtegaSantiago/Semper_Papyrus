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
                    Settings.NumAleatorio = rnd.Next(10, 30);
                    Settings.CountParaNumAleatorio = "1";

                    var test = CrossMTAdmob.Current.IsInterstitialLoaded().ToString();
                    CrossMTAdmob.Current.ShowInterstitial();
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-2336879831564913/8704842488");
                }
            }
            else
            {
                Settings.CountParaNumAleatorio = "1";
                Settings.NumAleatorio = 1;
            }

        }

        public void MostrarAnuncioPantallaParaTitulos()
        {

            if (Settings.CountParaNumAleatorioTitulos != "" && Settings.NumAleatorioTitulos != 0)
            {

                if (int.Parse(Settings.CountParaNumAleatorioTitulos) != Settings.NumAleatorioTitulos)
                {
                    int num = int.Parse(Settings.CountParaNumAleatorioTitulos) + 1;
                    Settings.CountParaNumAleatorioTitulos = num.ToString();
                }
                else
                {
                    Random rnd = new Random();
                    Settings.NumAleatorioTitulos = rnd.Next(1, 4);
                    Settings.CountParaNumAleatorioTitulos = "1";

                    var test = CrossMTAdmob.Current.IsInterstitialLoaded().ToString();
                    
                    CrossMTAdmob.Current.ShowInterstitial();
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-2336879831564913/8704842488");

                    ////var test2 = CrossMTAdmob.Current.IsRewardedVideoLoaded().ToString();
                    ////CrossMTAdmob.Current.ShowRewardedVideo();
                    ////CrossMTAdmob.Current.LoadRewardedVideo("ca-app-pub-3940256099942544/1044960115");
                }
            }
            else
            {
                Settings.CountParaNumAleatorioTitulos = "1";
                Settings.NumAleatorioTitulos = 1;
            }

        }

    }

    
}
