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
    }
}