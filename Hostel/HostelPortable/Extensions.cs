using System.Collections.Generic;

namespace HostelPortable
{
    public static class Extensions
    {
        public static T? ToNulllable<T>(this T value) where T : struct
        {
            return EqualityComparer<T>.Default.Equals(value, default(T)) ? (T?)null : value;
        }
    }
}
