//using Xamarin.Forms.PlatformConfiguration.Android.Widget;
using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario.DatosCriticos;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.DB_Local;
using IURIS.MOVIL.Modelos_y_clases.Tools;
using IURIS.MOVIL.Utils;
using MarcTron.Plugin.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        List<Leyes> LeyesActualizadas;
        Leyes _leyGuardada;
        public PageInicioDeSesion()
        {
            InitializeComponent();
            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

            //manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());
            //MainThread.BeginInvokeOnMainThread(async () => {
            //    await Task.Delay(5000);
            //    //LeyesActualizadas = manejadorDeLeyes.Listar.Where(x => x.EsModificacion == true).ToList();
            //    if (Settings.LastCode != "Last_LeyCodigo_key")
            //    {
            //        _leyGuardada = manejadorDeLeyes.Listar.Find(x=>x.CodigoLey==Settings.LastCode);
            //        Debug.WriteLine("[ID]--Ley guardad {}" + _leyGuardada.id.ToString());


            //    }
            //});

            DatosAIniciar();
        }

        private void DatosAIniciar()
        {
            if (!Settings.Recuerdame) return;

            EntryCorreo.Text = Settings.Email != "" ? Settings.Email : null;
            EntryPasswor.Text = Settings.Contrasenia != "" ? Settings.Contrasenia : null;

            CheckRecuerdame.IsChecked = Settings.Recuerdame;

        }

        private async void BtnCanselar_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos != 1) return;

            await Navigation.PopAsync();
            Intentos = 0;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos != 1) return;

                LblNoRecuerdoMiContrasenia.TextColor = Color.CadetBlue;
                Navigation.PushAsync(new PageRecuperarCuenta(), false);
                LblNoRecuerdoMiContrasenia.TextColor = Color.White;

                Intentos = 0;
        }

        private void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                UserDialogs.Instance.ShowLoading("Iniciando sesión", MaskType.None);
                //await Task.Delay(200);

                Intentos++;
                if (Intentos != 1) return;
                try
                {
                    if (string.IsNullOrWhiteSpace(EntryPasswor.Text) || string.IsNullOrWhiteSpace(EntryCorreo.Text)) return;

                    string CorreoSinEspacios;
                    CorreoSinEspacios = EntryCorreo.Text.TrimStart();
                    CorreoSinEspacios = CorreoSinEspacios.TrimEnd();

                    var users = await App.Database.GetPeopleAsync();
                    if (users.Count > 0)
                    {
                        var _User = users.Find(r => r.Correo == CorreoSinEspacios && r.Contrasenia == int.Parse(EntryPasswor.Text));

                        if (_User != null)
                        {
                            App.MyUser = _User;
                            Intentos = 0;
                            await Task.Delay(1000);
                            UserDialogs.Instance.HideLoading();
                            await Navigation.PushAsync(new FirtsView(this._User), false);

                            return;
                        }

                    }


                    _User = manejadorDeUsuarioAplicacion.EncontrarUsuario(CorreoSinEspacios, int.Parse(EntryPasswor.Text));

                    if (_User != null)
                    {
                        Settings.Recuerdame = Recuerdame;
                        if (Settings.Recuerdame)
                        {
                            Settings.Email = _User.Correo;
                            Settings.Contrasenia = _User.Contrasenia.ToString();
                        }
                        Settings.NumUsuario = _User.IdApp.ToString();
                        Settings.LastCode = _User.MiUltimaLeyCargada;

                        //HerramientasGenerales herramientasGenerales = new HerramientasGenerales(_User);
                        //herramientasGenerales.TipoDeAlmacenamiento();

                        LocalSaveUser localSaveUser = new LocalSaveUser(_User);
                        if (!await localSaveUser.ExisteUsuario()) localSaveUser.Save();

                        //UserDialogs.Instance.HideLoading();
                        //UserDialogs.Instance.ShowLoading("Obteniendo leyes");
                        //await Task.Delay(300);
                        //if (HayActualizacion()) Actualizaeyes();

                        await Navigation.PushAsync(new FirtsView(_User), false);
                    }
                    else
                    {
                        await DisplayAlert("Error de usuario", "Por favor verifica los datos ingresados", "OK");

                    }
                    Intentos = 0;
                }
                catch (TimeoutException)
                {
                    Intentos = 0;
                    await Task.Delay(200);
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "No tienes conexión a internet", "ok");

                    return;
                }

                catch (Exception ex)
                {
                    Intentos = 0;
                    await Task.Delay(200);
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Error:" + ex.Message, "ok");
                    return;
                }
                await Task.Delay(200);
                UserDialogs.Instance.HideLoading();
            });
        }

        private async void Actualizaeyes()
        {
            foreach (var LeyParaActualizar in LeyesParaActualizar)
            {
                foreach (var MiLey in _User.MisLeyes)
                {
                    if (MiLey.id == LeyParaActualizar.id)
                    {

                        if (_User.MiUltimaLeyCargada == MiLey.CodigoLey)
                            _User.MiUltimaLeyCargada = MiLey.CodigoLey != LeyParaActualizar.CodigoLey ? LeyParaActualizar.CodigoLey : _User.MiUltimaLeyCargada;

                        MiLey.EsModificacion = MiLey.EsModificacion != LeyParaActualizar.EsModificacion ? LeyParaActualizar.EsModificacion : MiLey.EsModificacion;
                        //MiLey.EsModificacion = false;
                        MiLey.NombreLey = MiLey.NombreLey != LeyParaActualizar.NombreLey ? LeyParaActualizar.NombreLey : MiLey.NombreLey;
                        MiLey.UltimaFechaDeModificacion = MiLey.UltimaFechaDeModificacion != LeyParaActualizar.UltimaFechaDeModificacion ? LeyParaActualizar.UltimaFechaDeModificacion : MiLey.UltimaFechaDeModificacion;
                        MiLey.CodigoLey = MiLey.CodigoLey != LeyParaActualizar.CodigoLey ? LeyParaActualizar.CodigoLey : MiLey.CodigoLey;
                        

                        //MiLey.ListaDeTitulos[0] = null;
                        foreach (var Titulo in MiLey.ListaDeTitulos)
                        {
                            foreach (var TituloAActualizar in LeyParaActualizar.ListaDeTitulos)
                            {

                                if (Titulo.id == TituloAActualizar.id)
                                {

                                    Titulo.NumTitulo = Titulo.NumTitulo != TituloAActualizar.NumTitulo ? TituloAActualizar.NumTitulo : Titulo.NumTitulo;
                                    Titulo.NombreTitulo = Titulo.NombreTitulo != TituloAActualizar.NombreTitulo ? TituloAActualizar.NombreTitulo : Titulo.NombreTitulo;

                                    foreach (var _Capitulo in Titulo.ListaCapitulos)
                                    {
                                        foreach (var _CapituloAActualizar in TituloAActualizar.ListaCapitulos)
                                        {
                                            if (_Capitulo.id == _CapituloAActualizar.id)
                                            {

                                                _Capitulo.NumCapitulo = _Capitulo.NumCapitulo != _CapituloAActualizar.NumCapitulo ? _CapituloAActualizar.NumCapitulo : _Capitulo.NumCapitulo;
                                                _Capitulo.NombreCapitulo = _Capitulo.NombreCapitulo != _CapituloAActualizar.NombreCapitulo ? _CapituloAActualizar.NombreCapitulo : _Capitulo.NombreCapitulo;

                                                foreach (var _Articulo in _Capitulo.ListaArticulos)
                                                {
                                                    foreach (var _ArticuloAModificar in _CapituloAActualizar.ListaArticulos)
                                                    {

                                                        if (_Articulo.id == _ArticuloAModificar.id)
                                                        {

                                                            _Articulo.Contenido = _Articulo.Contenido != _ArticuloAModificar.Contenido ? _ArticuloAModificar.Contenido : _Articulo.Contenido;
                                                            _Articulo.NombreArticulo = _Articulo.NombreArticulo != _ArticuloAModificar.NombreArticulo ? _ArticuloAModificar.NombreArticulo : _Articulo.NombreArticulo;
                                                            _Articulo.NumArticulo = _Articulo.NumArticulo != _ArticuloAModificar.NumArticulo ? _ArticuloAModificar.NumArticulo : _Articulo.NumArticulo;
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }


                            }
                        }

                    }
                }
            }
         
            try
            {
                if (_User.DatosSobreUsuario == null)
                {
                    _User.DatosSobreUsuario = new DatosSobreUsuarioParaLey();
                    _User.DatosSobreUsuario.NumLeyesPermitidas = 4;

                }

                if (manejadorDeUsuarioAplicacion.Modificar(_User))
                    await DisplayAlert("","la actualizacion fue exitosa", "OK");

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CheckRecuerdame_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Recuerdame = CheckRecuerdame.IsChecked;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            CheckRecuerdame.IsChecked = CheckRecuerdame.IsChecked ? false : true;
            Recuerdame = CheckRecuerdame.IsChecked ? true : false;
        }

        public bool HayActualizacion()
        {
            
            if (LeyesActualizadas.Count == 0)
                return false;

            foreach (var Ley in LeyesActualizadas)
            {
                foreach (var MiLey in _User.MisLeyes)
                {
                    if (MiLey.id == Ley.id && Ley.UltimaFechaDeModificacion > MiLey.UltimaFechaDeModificacion) //&& Ley.UltimaFechaDeModificacion > MiLey.UltimaFechaDeModificacion //Poner que obtenga la hora y la fecha exactas
                    {
                            LeyesParaActualizar.Add(Ley);
                    }
                }
            }


            return LeyesParaActualizar.Count == 0;
        }
    }
}