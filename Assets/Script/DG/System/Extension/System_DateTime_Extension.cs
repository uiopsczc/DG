using System;

namespace DG
{
    public static class System_DateTime_Extension
    {
        /// <summary>
        /// 当前的时间是否是月的最后一天
        /// </summary>
        public static bool IsLastDayOfMonth(this DateTime self)
        {
            return DateTimeUtil.IsLastDayOfMonth(self);
        }

        /// <summary>
        /// 当前的时间是否是月的第一天
        /// </summary>
        public static bool IsFirstDayOfMonth(this DateTime self)
        {
            return DateTimeUtil.IsFirstDayOfMonth(self);
        }

        /// <summary>
        /// 获取指定时刻的DateTime
        /// </summary>
        /// <param name="self"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime GetTime(this DateTime self, int hour = 0, int minute = 0, int second = 0)
        {
            return DateTimeUtil.GetTime(self, hour, minute, second);
        }
    }
}