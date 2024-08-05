using System;
using System.Collections.Generic;

namespace DG
{
    public static class IntUtil
    {
        // >>>无符号右移
        public static int RightShift3(int value, int shiftAmount)
        {
            //移动 0 位时直接返回原值
            if (shiftAmount != 0)
            {
                // int.MaxValue = 0x7FFFFFFF 整数最大值
                int mask = int.MaxValue;
                //无符号整数最高位不表示正负但操作数还是有符号的，有符号数右移1位，正数时高位补0，负数时高位补1
                value >>= 1;
                //和整数最大值进行逻辑与运算，运算后的结果为忽略表示正负值的最高位
                value &= mask;
                //逻辑运算后的值无符号，对无符号的值直接做右移运算，计算剩下的位
                value >>= shiftAmount - 1;
            }

            return value;
        }

        public static T ToEnum<T>(int value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }


        #region 编码

        /// <summary>
        ///   10进制转为16进制字符串
        /// </summary>
        public static string ToHexString(int v)
        {
            return v.ToString(StringConst.STRING_X);
        }

        #endregion

        #region bytes

        /// <summary>
        ///   将数字转化为bytes
        /// </summary>
        public static byte[] ToBytes(int v, bool isNetOrder = false)
        {
            return ByteUtil.ToBytes(v & 0xFFFFFFFF, 4, isNetOrder);
        }

        #endregion

        /// <summary>
        ///   随机一个total以内的队列（队列里面的元素不会重复）
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isIncludeTotal">是否包括total</param>
        /// <param name="isZeroBase">是否从0开始</param>
        /// <returns></returns>
        public static List<int> Random(this int self, float outCount, bool isUnique, RandomManager randomManager,
            bool isIncludeTotal = false,
            bool isZeroBase = true)
        {
            var result = new List<int>();
            var toRandomList = new List<int>(); //要被随机的List

            for (var i = isZeroBase ? 0 : 1; i < (isIncludeTotal ? self + 1 : self); i++)
                toRandomList.Add(i);

            for (var i = 0; i < outCount; i++)
            {
                var index = randomManager.RandomInt(0, toRandomList.Count);
                result.Add(isUnique ? toRandomList.RemoveAt2(index) : toRandomList[index]);
            }

            return result;
        }

        //是否是defalut, 默认是与float.MaxValue比较
        public static bool IsDefault(int v, bool isMin = false)
        {
            return isMin ? v == int.MinValue : v == int.MaxValue;
        }

        public static bool IsInRange(int v, int minValue, int maxValue, bool isMinValueIncluded = false,
            bool isMaxValueIncluded = false)
        {
            return v >= minValue && v <= maxValue &&
                   ((v != minValue || isMinValueIncluded) && (v != maxValue || isMaxValueIncluded));
        }

        public static int Minimum(int v, int minimum)
        {
            return Math.Max(v, minimum);
        }

        public static int Maximum(int v, int maximum)
        {
            return Math.Min(v, maximum);
        }

        public static string ToStringWithComma(int v)
        {
            return string.Format(StringConst.STRING_FORMAT_NUMBER_WITH_COMMA, v);
        }
    }
}