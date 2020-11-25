using System;

namespace HRInPocket.DAL.Service
{
    internal static class IDisposableExtensions
    {
        public static void DisposeAfter<T>(this T element, Action<T> action) where T : IDisposable
        {
            using (element)
                action(element);
        }

        public static TResult DisposeAfter<T, TResult>(this T element, Func<T, TResult> function) where T : IDisposable
        {
            using (element)
                return function(element);
        }
    }
}
