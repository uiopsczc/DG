namespace DG
{
    public static class Double_Extension
    {
        public static byte[] ToBytes(this double self, bool isNetOrder = false)
        {
            return DoubleUtil.ToBytes(self, isNetOrder);
        }


        //是否是defalut, 默认是与float.MaxValue比较
        public static bool IsDefault(this double self, bool isMin = false)
        {
            return DoubleUtil.IsDefault(self, isMin);
        }

        //得到百分比
        public static double GetPercent(this double self, double minValue, double maxValue, bool isClamp = true)
        {
            return DoubleUtil.GetPercent(self, minValue, maxValue, isClamp);
        }

        public static bool IsInRange(this double self, double minValue, double maxValue,
            bool isMinValueIncluded = false,
            bool isMaxValueIncluded = false)
        {
            return DoubleUtil.IsInRange(self, minValue, maxValue, isMinValueIncluded, isMaxValueIncluded);
        }

        //将v Round四舍五入snap_soze的倍数的值
        //Rounds value to the closest multiple of snap_soze.
        public static double Snap(this double self, double snapSize)
        {
            return DoubleUtil.Snap(self, snapSize);
        }

        public static double Snap2(this double self, double snapSize)
        {
            return DoubleUtil.Snap2(self, snapSize);
        }

        public static double Minimum(this double self, double minimum)
        {
            return DoubleUtil.Minimum(self, minimum);
        }

        public static double Maximum(this double self, double maximum)
        {
            return DoubleUtil.Maximum(self, maximum);
        }
    }
}