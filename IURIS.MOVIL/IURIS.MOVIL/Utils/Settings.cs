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
        private static readonly string SettingsDefault = string.Empty;
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

    }
}
