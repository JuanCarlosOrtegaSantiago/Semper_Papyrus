using Android.Widget;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageInicioDeSesion : ContentPage
    {
        int Intentos = 0;

        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        IManejadorDeLeyes manejadorDeLeyes;
        Usuarios _User;
        bool Recuerdame = false;
        List<Leyes> LeyesParaActualizar=new List<Leyes>();

        public PageInicioDeSesion()
        {
            InitializeComponent();
            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());

            DatosAIniciar();
        }

        private void DatosAIniciar()
        {
            if (Settings.Recuerdame)
            {
                if (Settings.Email != "")
                    EntryCorreo.Text = Settings.Email;
                if (Settings.Contrasenia != "")
                    EntryPasswor.Text = Settings.Contrasenia;
                CheckRecuerdame.IsChecked = Settings.Recuerdame;
            }


        }

        private async void BtnCanselar_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {
                await Navigation.PopAsync();
                await Navigation.PushAsync(new MainPage(), true);
                Intentos = 0;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {
                LblNoRecuerdoMiContrasenia.TextColor = Color.CadetBlue;
                Navigation.PushAsync(new PageRecuperarCuenta(), false);
                LblNoRecuerdoMiContrasenia.TextColor = Color.White;

                Intentos = 0;
            }
        }

        private async void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {

                if (!string.IsNullOrWhiteSpace(EntryPasswor.Text) && !string.IsNullOrWhiteSpace(EntryCorreo.Text))
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.None)
                    {
                        await DisplayAlert("Error", "Sin conexión a internet", "Aceptar");
                        return;
                    }

                    _User = manejadorDeUsuarioAplicacion.EncontrarUsuario(EntryCorreo.Text, int.Parse(EntryPasswor.Text));
                    if (_User != null)
                    {
                        if (Recuerdame)
                        {
                            Settings.Email = _User.Correo;
                            Settings.Contrasenia = _User.Contrasenia.ToString();
                        }

                            Settings.Recuerdame = Recuerdame;
                        Settings.NumUsuario = _User.IdApp.ToString();

                        if (HayActualizacion())
                        {
                            Actualizaeyes();
                        }
                        await Navigation.PushAsync(new FirtsView(_User), false);

                        //Toast.MakeText(context,3,  ToastLength.Long).Show();

                    }
                    else
                    {
                        await DisplayAlert("Error de usuario", "Por favor verifica los datos ingresados", "OK");
                    }
                }
            }
            Intentos = 0;
        }

        private void Actualizaeyes()
        {
            
        }

        private void CheckRecuerdame_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            Recuerdame = CheckRecuerdame.IsChecked ? true : false;
            //Settings.Recuerdame = CheckRecuerdame.IsChecked ? true : false;

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            CheckRecuerdame.IsChecked = CheckRecuerdame.IsChecked ? false : true;
            Recuerdame = CheckRecuerdame.IsChecked ? true : false;
        }

        public bool HayActualizacion()
        {
            List<Leyes> LeyesActualizadas = manejadorDeLeyes.Listar.Where(x => x.EsModificacion == true).ToList();
            if (LeyesActualizadas.Count == 0)
                return false;

            foreach (var Ley in LeyesActualizadas)
            {
                foreach (var MiLey in _User.MisLeyes)
                {
                    if (MiLey.id == Ley.id)
                    {
                        LeyesParaActualizar.Add(Ley);
                    }
                }
            }
            return true;
        }
    }
}