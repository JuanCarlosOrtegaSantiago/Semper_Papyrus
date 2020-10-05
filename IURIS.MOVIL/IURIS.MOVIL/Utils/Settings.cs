using IURIS.COMMON.Entidades.Ley;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.MOVIL.Utils
{
    public static class Settings
    {
        private static ISettings Appsettings { get { return CrossSettings.Current; } }

        #region Settings Constants
        private const string NumUserSettingsKey = "Last_Num_key";
        private const string CorreoUserSettingsKey = "Last_Correo_key";
        private const string ContraseniaUserSettingsKey = "Last_Cotrasenia_key";
        private const string LeyCodigoUserSettingsKey = "Last_LeyCodigo_key";

        private const bool RecuerdameUserSettingsKey = false;
        private static readonly string SettingsDefault = string.Empty;
        private static readonly string SettingsDefaultCodigo = string.Empty;
        private static readonly bool SettingsDefaultiBool = default;
        #endregion

        public static string NumUsuario
        {
            get
            {
                return Appsettings.GetValueOrDefault(NumUserSettingsKey, SettingsDefault);
            }
            set
            {
                Appsettings.AddOrUpdateValue(NumUserSettingsKey, value);
            }
        }

        public static string Email
        {
            get
            {
                return Appsettings.GetValueOrDefault(CorreoUserSettingsKey, SettingsDefault);
            }
            set
            {

                Appsettings.AddOrUpdateValue(CorreoUserSettingsKey, value);

            }
        }

        public static string Contrasenia
        {
            get
            {
                return Appsettings.GetValueOrDefault(ContraseniaUserSettingsKey, SettingsDefault);
            }
            set
            {

                Appsettings.AddOrUpdateValue(ContraseniaUserSettingsKey, value);

            }
        }

        public static bool Recuerdame
        {
            get
            {
                return Appsettings.GetValueOrDefault(RecuerdameUserSettingsKey.ToString(), SettingsDefaultiBool);
            }
            set
            {

                Appsettings.AddOrUpdateValue(RecuerdameUserSettingsKey.ToString(), value);

            }
        }

        //public static string CodigoDeLeyCargada
        //{
        //    get
        //    {
        //        return Appsettings.GetValueOrDefault(LeyCodigoUserSettingsKey, SettingsDefaultCodigo);
        //    }
        //    set
        //    {
        //        Appsettings.AddOrUpdateValue(LeyCodigoUserSettingsKey, value);
        //    }

        //}

    }
}
