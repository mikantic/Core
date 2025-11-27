using System;

namespace Core.Tools
{
    public static class Utils
    {
        #region Swapping
        /// <summary>
        /// swaps to values in place
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="other"></param>
        public static void SwapWith<T>(ref T value, ref T other)
        {
            T placeholder = other;
            other = value;
            value = placeholder;
        }
        #endregion

        #region Lerping

        /// <summary>
        /// returns percentage of value between min and max
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double InverseLerp(this double value, double min, double max)
        {
            return (value - min) / (max - min);
        }

        #endregion

        #region Clamping

        /// <summary>
        /// floors double to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Floor(this double value) => (int)Math.Floor(value);

        #endregion
    }
}