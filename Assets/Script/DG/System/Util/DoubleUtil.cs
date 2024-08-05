using System;

namespace DG
{
    public static class DoubleUtil
    {
        public static byte[] ToBytes(double v, bool isNetOrder = false)
        {
            byte[] data = BitConverter.GetBytes(v);
            if (isNetOrder)
                Array.Reverse(data);
            return data;
        }


        //是否是defalut, 默认是与float.MaxValue比较
        public static bool IsDefault(double v, bool isMin = false)
        {
            return isMin ? v == double.MinValue : v == double.MaxValue;
        }

        //得到百分比
        public static double GetPercent(double v, double minValue, double maxValue, bool isClamp = true)
        {
            if (isClamp)
            {
                if (v < minValue)
                    v = minValue;
                else if (v > maxValue)
                    v = maxValue;
            }

            double offset = v - minValue;
            return offset / (maxValue - minValue);
        }

        public static bool IsInRange(double v, double minValue, double maxValue,
            bool isMinValueIncluded = false,
            bool isMaxValueIncluded = false)
        {
            return !(v < minValue) && !(v > maxValue) &&
                   ((v != minValue || isMinValueIncluded) && (v != maxValue || isMaxValueIncluded));
        }

        //将v Round四舍五入snap_soze的倍数的值
        //Rounds value to the closest multiple of snap_soze.
        public static double Snap(double v, double snapSize)
        {
            return Math.Round(v / snapSize) * snapSize;
        }

        public static double Snap2(double v, double snapSize)
        {
            return Math.Round(v * snapSize) / snapSize;
        }

        public static double Minimum(double v, double minimum)
        {
            return Math.Max(v, minimum);
        }

        public static double Maximum(double v, double maximum)
        {
            return Math.Min(v, maximum);
        }
    }
}