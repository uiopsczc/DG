namespace DG
{
    public static class Short_Extension
    {
        #region bytes

        /// <summary>
        ///   将数字转化为bytes
        /// </summary>
        public static byte[] ToBytes(this short self, bool isNetOrder = false)
        {
            return ShortUtil.ToBytes(self, isNetOrder);
        }

        #endregion

        public static short Minimum(this short self, short minimum)
        {
            return ShortUtil.Minimum(self, minimum);
        }

        public static short Maximum(this short self, short maximum)
        {
            return ShortUtil.Maximum(self, maximum);
        }
    }
}