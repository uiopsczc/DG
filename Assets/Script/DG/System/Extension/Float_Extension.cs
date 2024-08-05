namespace DG
{
    public static class Float_Extension
    {
        /// <summary>
        ///   比较两个float是否相等，两者相差FloatConst.EPSILON则判断为相等，否则判断为不相等
        /// </summary>
        /// <param name="self"></param>
        /// <param name="float2"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool EqualsEpsilon(this float self, float float2, float epsilon = float.Epsilon)
        {
            return FloatUtil.EqualsEpsilon(self, float2, epsilon);
        }

        /// <summary>
        ///   转为bytes
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isNetOrder">是否是网络顺序，网络顺序是相反的</param>
        /// <returns></returns>
        public static byte[] ToBytes(this float self, bool isNetOrder = false)
        {
            return FloatUtil.ToBytes(self, isNetOrder);
        }


        //是否是defalut, 默认是与float.MaxValue比较
        public static bool IsDefault(this float value, bool isMin = false)
        {
            return FloatUtil.IsDefault(value, isMin);
        }

        //得到百分比
        public static float GetPercent(this float value, float minValue, float maxValue, bool isClamp = true)
        {
            return FloatUtil.GetPercent(value, minValue, maxValue, isClamp);
        }

        public static bool IsInRange(this float value, float minValue, float maxValue, bool isMinValueInclude = false,
            bool isMaxValueInclude = false)
        {
            return FloatUtil.IsInRange(value, minValue, maxValue, isMinValueInclude, isMaxValueInclude);
        }

        /// <summary>
        /// 百分比  输入0.1,输出10%
        /// </summary>
        /// <param name="pct"></param>
        /// <returns></returns>
        public static string ToPctString(this float pct)
        {
            return FloatUtil.ToPctString(pct);
        }

        //将v Round四舍五入snap_soze的倍数的值
        //Rounds value to the closest multiple of snap_size.
        public static float Snap(this float self, float snapSize)
        {
            return FloatUtil.Snap(self, snapSize);
        }

        public static float Snap2(this float self, float snapSize)
        {
            return FloatUtil.Snap2(self, snapSize);
        }

        public static float Minimum(this float self, float minimum)
        {
            return FloatUtil.Minimum(self, minimum);
        }

        public static float Maximum(this float self, float maximum)
        {
            return FloatUtil.Maximum(self, maximum);
        }
    }
}