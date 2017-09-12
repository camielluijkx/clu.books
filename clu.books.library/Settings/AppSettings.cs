using System.ComponentModel;
using System.Configuration;

namespace clu.books.library.settings
{
    public static class AppSettings
    {
        public static T Get<T>(string key)
        {
            string appSetting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting))
            {
                throw new AppSettingNotFoundException(key);
            }

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromInvariantString(appSetting);
        }
    }
}
