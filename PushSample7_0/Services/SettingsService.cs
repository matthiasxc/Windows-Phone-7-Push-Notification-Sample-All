using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using PushSample7_0.Model;
using System.IO.IsolatedStorage;
using Serialization;

namespace PushSample7_0.Services
{
    public class SettingsService
    {
        #region Isolated Storage Get/Save for SettingsData

        public static Settings GetSettingsData()
        {
            using (var userIsoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (userIsoStore.FileExists("settingsData.dat"))
                {
                    using (var iss = userIsoStore.OpenFile("settingsData.dat", System.IO.FileMode.Open))
                    {
                        return SilverlightSerializer.Deserialize(iss) as Settings;
                    }
                }
                else
                {
                    Settings newSettingsData = new Settings();

                    return newSettingsData;
                }
            }
        }

        public static void SaveSettingsData(Settings latestSettingsData)
        {
            using (var userIsoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (userIsoStorage.FileExists("settingsData.dat"))
                {
                    userIsoStorage.DeleteFile("settingsData.dat");
                }
                using (var iss = userIsoStorage.CreateFile("settingsData.dat"))
                {
                    SilverlightSerializer.Serialize(latestSettingsData, iss);
                }
            }
        }

        #endregion

        #region Isolated Storage Get/Save for ImplementedSettings

        public static Settings GetImplementedSettings()
        {
            using (var userIsoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (userIsoStore.FileExists("LastImplementedSettings.dat"))
                {
                    using (var iss = userIsoStore.OpenFile("LastImplementedSettings.dat", System.IO.FileMode.Open))
                    {
                        return SilverlightSerializer.Deserialize(iss) as Settings;
                    }
                }
                else
                {
                    Settings newImplementedSettings = new Settings();

                    return newImplementedSettings;
                }
            }
        }

        public static void SaveImplementedSettings(Settings latestImplementedSettings)
        {
            using (var userIsoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (userIsoStorage.FileExists("LastImplementedSettings.dat"))
                {
                    userIsoStorage.DeleteFile("LastImplementedSettings.dat");
                }
                using (var iss = userIsoStorage.CreateFile("LastImplementedSettings.dat"))
                {
                    SilverlightSerializer.Serialize(latestImplementedSettings, iss);
                }
            }
        }

        #endregion
    }
}
