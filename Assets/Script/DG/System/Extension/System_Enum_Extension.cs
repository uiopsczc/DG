using System;

namespace DG
{
    public static class System_Enum_Extension
    {
        /// <summary>
        /// 用于每个枚举都是左移的枚举类型
        /// </summary>
        /// <param name="self"></param>
        /// <param name="toBeContained"></param>
        /// <returns></returns>
        public static bool Contains(this Enum self, Enum toBeContained)
        {
            return EnumUtil.Contains(self, toBeContained);
        }

        public static int ToInt(this Enum self)
        {
            return EnumUtil.ToInt(self);
        }

        //转为不同的enum
        public static T ToEnum<T>(this Enum self)
        {
            return EnumUtil.ToEnum<T>(self);
        }
    }
}