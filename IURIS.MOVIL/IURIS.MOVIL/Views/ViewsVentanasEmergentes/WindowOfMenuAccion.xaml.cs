using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
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

        public WindowOfMenuAccion(Titulo titulo, Articulo articulo, MyLey ley, Capitulo capitulo)
        {
            InitializeComponent();
            _Articulo = articulo;
            _Titulo = titulo;
            _MyLey = ley;
            _Capitulo = capitulo;
        }

        private async void LblCrearNota(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(false);
            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyCrearNota(_Titulo, _Articulo, _MyLey, _Capitulo), false);
        }

        private async void LblClasificacionPersonalizada(object sender, EventArgs e)
        {

            if (_MyLey.Clasificaciones.Count() > 0)
            {
                List<String> name = new List<String>();
                _MyLey.Clasificaciones.ForEach(d => name.Add(d.Nombre));
                string[] Opciones = new[] { "Cancelar", "Crear nueva clasificación" };

                var Clasificaicon = await UserDialogs.Instance.ActionSheetAsync("Elige una clasificación", Opciones[0], Opciones[1], CancellationToken.None, name.ToArray());

                if (!string.IsNullOrEmpty(Clasificaicon)&& !Clasificaicon.Equals(Opciones[0]))
                {

                    if (Clasificaicon.Equals(Opciones[1]))
                    {
                        
                            
                            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyNuevaClasificacion(_MyLey, _Articulo));
                        //await Navigation.PushAsync(new ViewsMisClasificaciones(_Articulo, _MyLey), false);
                        //await PopupNavigation.Instance.PopAllAsync(IsAnimating);
                        
                    }
                    else
                    {
                        _MyLey.Clasificaciones.Find(clasi => clasi.Nombre.Equals(Clasificaicon)).MisArticulos.Add(_Articulo);

                        string[] datos = await App.Database.UpdateUserAsync(App.MyUser) ? new[] { "Ok", "Artículo agregado" } : new[] { "Error", "Intente más tarde" };

                        UserDialogs.Instance.Alert(datos[0], datos[1]);
                        await PopupNavigation.Instance.PopAsync(false);
                    }
                }
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
                await PopupNavigation.Instance.PopAsync(false);
         }

    }
}