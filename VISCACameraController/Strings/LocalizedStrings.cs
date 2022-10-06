using Windows.ApplicationModel.Resources;

namespace VISCACameraController.Strings
{

    public class LocalizedStrings
    {
        #region Static Fields

        private static ResourceLoader resourceLoader;

        #endregion

        #region Public Methods

        public static string GetString(string key)
        {
            if (resourceLoader == null)
            {
                resourceLoader = ResourceLoader.GetForViewIndependentUse("Resources");
            }

            return resourceLoader.GetString(key);
        }

        #endregion
    }
}
