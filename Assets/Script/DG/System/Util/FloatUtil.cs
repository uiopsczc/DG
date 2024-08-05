using System;

namespace DG
{
    public static class FloatUtil
    {
        /// <summary>
        ///   比较两个float是否相等，两者相差FloatConst.EPSILON则判断为相等，否则判断为不相等
        /// </summary>
        /// <param name="v"></param>
        /// <param name="float2"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool EqualsEpsilon(float v, float float2, float epsilon = float.Epsilon)
        {
            return Math.Abs(v - float2) < epsilon;
        }

        /// <summary>
        ///   转为bytes
        /// </summary>
        /// <param name="v"></param>
        /// <param name="isNetOrder">是否是网络顺序，网络顺序是相反的</param>
        /// <returns></returns>
        public static byte[] ToBytes(float v, bool isNetOrder = false)
        {
            var data = BitConverter.GetBytes(v);
            if (isNetOrder)
                Array.Reverse(data);
            return data;
        }


        //是否是defalut, 默认是与float.MaxValue比较
        public static bool IsDefault(float v, bool isMin = false)
        {
            return isMin ? v == float.MinValue : v == float.MaxValue;
        }

        //得到百分比
        public static float GetPercent(float value, float minValue, float maxValue, bool isClamp = true)
        {
            if (isClamp)
            {
                if (value < minValue)
                    value = minValue;
                else if (value > maxValue)
                    value = maxValue;
            }

            float offset = value - minValue;
            return offset / (maxValue - minValue);
        }

        public static bool IsInRange(float value, float minValue, float maxValue, bool isMinValueInclude = false,
            bool isMaxValueInclude = false)
        {
            return !(value < minValue) && !(value > maxValue) &&
                   ((value != minValue || isMinValueInclude) && (value != maxValue || isMaxValueInclude));
        }

        /// <summary>
        /// 百分比  输入0.1,输出10%
        /// </summary>
        /// <param name="pct"></param>
        /// <returns></returns>
        public static string ToPctString(float pct)
        {
            return string.Format(StringConst.STRING_FORMAT_PCT, pct * 100);
        }

        //将v Round四舍五入snap_soze的倍数的值
        //Rounds value to the closest multiple of snap_size.
        public static float Snap(float self, float snapSize)
        {
            return (float)(Math.Round(self / snapSize) * snapSize);
        }

        public static float Snap2(float self, float snapSize)
        {
            return (float)(Math.Round(self * snapSize) / snapSize);
        }

        public static float Minimum(float v, float minimum)
        {
            return Math.Max(v, minimum);
        }

        public static float Maximum(float v, float maximum)
        {
            return Math.Min(v, maximum);
        }
    }
}