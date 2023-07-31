using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using IURIS.MOVIL.Utils;
using Plugin.Clipboard;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsVentanasEmergentes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WindowOfMenuAccion : PopupPage
    {
        readonly Titulo _Titulo;
        readonly Articulo _Articulo;
        MyLey _MyLey;
        readonly Capitulo _Capitulo;
        
        public static CancellationTokenSource cts;
        public static bool _ReproduceArticulo { get; set; }

        public WindowOfMenuAccion(Titulo titulo, Articulo articulo, MyLey ley, Capitulo capitulo)
        {
            InitializeComponent();
            _Articulo = articulo;
            _Titulo = titulo;
            _MyLey = ley;
            _Capitulo = capitulo;
            if (_ReproduceArticulo)
                a.Text = "Parar reproducción";
            
        }

        private async void LblCrearNota(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(false);
            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyCrearNota(_Titulo, _Articulo, _MyLey, _Capitulo), false);
        }

        private async Task showAlert(string mensaje)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new WindowAlert(mensaje), false);
                await Task.Delay(2000);
                await PopupNavigation.Instance.PopAsync(false);
                return;
            }
            catch (Exception)
            {
                return;

            }
        }

        private async void LblClasificacionPersonalizada(object sender, EventArgs e)
        {
            //await Task.Delay(2000);
            //await PopupNavigation.Instance.PopAsync(false);
            if (_MyLey.Clasificaciones.Count() > 0)
            {
            await PopupNavigation.Instance.PushAsync(new WindowListadoDeClasificaciones(_MyLey,_Articulo), false);
                //List<String> name = new List<String>();
                //_MyLey.Clasificaciones.ForEach(d => name.Add(d.Nombre));
                //string[] Opciones = new[] { "Cancelar", "Crear nueva clasificación" };

                //var Clasificaicon = await UserDialogs.Instance.ActionSheetAsync("Elige una clasificación", Opciones[0], Opciones[1], CancellationToken.None, name.ToArray());

                //if (!string.IsNullOrEmpty(Clasificaicon)&& !Clasificaicon.Equals(Opciones[0]))
                //{

                //    if (Clasificaicon.Equals(Opciones[1]))
                //    {
                        
                            
                //            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyNuevaClasificacion(_MyLey, _Articulo));
                //        //await Navigation.PushAsync(new ViewsMisClasificaciones(_Articulo, _MyLey), false);
                //        //await PopupNavigation.Instance.PopAllAsync(IsAnimating);
                        
                //    }
                //    else
                //    {
                //        _MyLey.Clasificaciones.Find(clasi => clasi.Nombre.Equals(Clasificaicon)).MisArticulos.Add(_Articulo);

                //        string[] datos = await App.Database.UpdateUserAsync(App.MyUser) ? new[] { "Ok", "Artículo agregado" } : new[] { "Error", "Intente más tarde" };

                //        UserDialogs.Instance.Alert(datos[0], datos[1]);
                //        await PopupNavigation.Instance.PopAsync(false);
                //    }
                //}
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(100);
                    
                    await Navigation.PushAsync(new ViewsMisClasificaciones(_Articulo, _MyLey), false);
                    await PopupNavigation.Instance.PopAsync(false);
                });
            }

        }

        private void CrearNuevaClasificaicon()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(100);
                await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyNuevaClasificacion(_MyLey, _Articulo));
                //await Navigation.PushAsync(new ViewsMisClasificaciones(_Articulo, _MyLey), false);
                await PopupNavigation.Instance.PopAsync(false);
            });
        }

        private async void GuardarSeleccion(object sender, EventArgs e)
        {
            try
            {

                await PopupNavigation.Instance.PopAsync(false);
                await showAlert("Preciona el boton de +,\nselecciona el texto y da en copiar");
            }
            catch (Exception)
            {

                return;
            }
        }
        bool isBusy = false;
        private async void ReproducirArticulo(object sender, EventArgs e)
        {
            try
            {

                await PopupNavigation.Instance.PopAsync(false);
                
                if (_ReproduceArticulo)
                {
                    if (cts?.IsCancellationRequested ?? true)
                        return;

                    cts.Cancel();
                    _ReproduceArticulo = false;
                }
                else
                {
                    cts = new CancellationTokenSource();
                    var locales = await TextToSpeech.GetLocalesAsync();
                    isBusy = true;
                    var settings = new SpeechOptions()
                    {
                        Volume = 1.0f,
                        Pitch = .69f, 
                    };

                    int init=_Capitulo.ListaArticulos.FindIndex(pre=> pre.id.Equals(_Articulo.id));


                    var x= _Capitulo.ListaArticulos.GetRange(init, _Capitulo.ListaArticulos.Count - init);
                    //Task.Run(async () =>
                    //{

                    //    await TextToSpeech.SpeakAsync(_Articulo.NumArticulo + ".\n" + _Articulo.Contenido, settings, cancelToken: cts.Token);

                    //});

                    _ReproduceArticulo = true;
                    
                        Task.Run(async () =>
                        {
                            foreach (var item in x)
                    {

                                await TextToSpeech.SpeakAsync(item.NumArticulo + ".\n" + item.Contenido, settings, cancelToken: cts.Token);
                            }
                            isBusy = false;
                        });

                }
            }
            catch (Exception)
            {

                return;
            }
        }

    }
}