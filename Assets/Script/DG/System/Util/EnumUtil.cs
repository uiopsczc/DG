using System;
using System.Collections.Generic;

namespace DG
{
    public class EnumUtil
    {
        public static int GetCount<T>()
        {
            return Enum.GetValues(typeof(T)).Length;
        }

        public static string[] GetNames<T>()
        {
            return Enum.GetNames(typeof(T));
        }

        public static string GetName<T>(int i)
        {
            return GetNames<T>()[i];
        }

        public static T[] GetValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static T GetValue<T>(int i)
        {
            return GetValues<T>()[i];
        }

        public static List<int> GetInts<T>()
        {
            var es = GetValues<T>();
            var result = new List<int>(es.Length);
            for (var i = 0; i < es.Length; i++)
            {
                var e = es[i];
                result.Add(Convert.ToInt32(e));
            }

            return result;
        }


        public static bool IsEnum<T>()
        {
            var enumType = typeof(T);
            return enumType.IsEnum;
        }

        public static T ToEnum<T>(Enum enumValue)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be enum Type");
            var value = enumValue.ToInt();
            return value.ToEnum<T>();
        }

        public static int ToInt(Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static bool Contains(Enum value, Enum toBeContained)
        {
            int containerInt = value.ToInt();
            int toBeContainedInt = toBeContained.ToInt();
            //只要包含，一定有一位为1，只要不包含，一定全部位都是0
            return (containerInt & toBeContainedInt) > 0;
        }
    }
}