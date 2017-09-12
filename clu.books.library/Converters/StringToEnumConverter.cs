using System;

namespace clu.books.library.converters
{
    internal static class StringToEnumConverter
    {
        public static T ConvertToEnum<T>(this string value)
            where T : new()
        {
            if (!typeof(T).IsEnum)
            {
                throw new NotSupportedException("T must be of type Enum");
            }

            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
