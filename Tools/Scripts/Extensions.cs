using System;
using Unity.VisualScripting;

namespace Core.Tools
{
    /// <summary>
    /// action that takes a ref of a generic type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    public delegate void Ref<T>(ref T value);

    public static class Extensions
    {
        #region Comparable
        /// <summary>
        /// returns if value is greater than another
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="comparable"></param>
        /// <returns></returns>
        public static bool GreaterThan<T>(this T value, T comparable) where T : IComparable
        {
            return value.CompareTo(comparable) > 0;
        }

        /// <summary>
        /// returns if value is less than another
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="comparable"></param>
        /// <returns></returns>
        public static bool LessThan<T>(this T value, T comparable) where T : IComparable
        {
            return value.CompareTo(comparable) < 0;
        }
        #endregion
    }
}