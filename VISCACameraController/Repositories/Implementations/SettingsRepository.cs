using System;
using System.Diagnostics;
using VISCACameraController.Repositories.Interfaces;
using Windows.Storage;

namespace VISCACameraController.Repositories.Implementations
{
    public class SettingsRepository : ISettingsRepository
    {
        #region Publics Properties
        
        public string DefaultComPort
        {
            get => TryGetLocalValue(nameof(DefaultComPort), string.Empty);
            set => TrySetLocalValue(nameof(DefaultComPort), value);
        }

        #endregion Publics Properties

        #region Private Methods

        private T TryGetLocalValue<T>(string key, T defaultValue)
        {
            try
            {
                object tValue;

                if (!ApplicationData.Current.LocalSettings.Values.TryGetValue(key, out tValue))
                {
                    return defaultValue;
                }

                return (T)tValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        private void TrySetLocalValue<T>(string key, T value)
        {
            try
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
                {
                    ApplicationData.Current.LocalSettings.Values[key] = value;
                }
                else
                {
                    ApplicationData.Current.LocalSettings.Values.Add(key, value);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion Private Methods
    }
}