using System.Reflection;
using UnityEngine;
using Object = System.Object;

namespace DG
{
    public class ColorUtil
    {
        public static void SetColorR(Object obj, float v, string memberName = StringConst.STRING_COLOR)
        {
            SetColor(obj, memberName, EColorMode.R, v);
        }

        public static void SetColorG(Object obj, float v, string memberName = StringConst.STRING_COLOR)
        {
            SetColor(obj, memberName, EColorMode.G, v);
        }

        public static void SetColorB(Object obj, float v, string memberName = StringConst.STRING_COLOR)
        {
            SetColor(obj, memberName, EColorMode.B, v);
        }

        public static void SetColorA(Object obj, float v, string memberName = StringConst.STRING_COLOR)
        {
            SetColor(obj, memberName, EColorMode.A, v);
        }

        public static void SetColor(Object obj, EColorMode rgbaMode, params float[] rgba)
        {
            SetColor(obj, StringConst.STRING_COLOR, rgbaMode, rgba);
        }

        public static void SetColor(Object obj, string memberName, EColorMode rgbaMode, params float[] rgba)
        {
            FieldInfo fieldInfo = obj.GetType().GetFieldInfo(memberName);
            if (fieldInfo != null)
            {
                Color oldColor = (Color)fieldInfo.GetValue(obj);
                Color newColor = Set(oldColor, rgbaMode, rgba);
                fieldInfo.SetValue(obj, newColor);
                return;
            }

            PropertyInfo propertyInfo = obj.GetType().GetPropertyInfo(memberName);
            if (propertyInfo != null)
            {
                Color oldColor = (Color)propertyInfo.GetValue(obj, null);
                Color newColor = Set(oldColor, rgbaMode, rgba);
                propertyInfo.SetValue(obj, newColor, null);
            }
        }

        /// <summary>
        /// 修改rgba中的值，rgbaEnum任意组合
        /// </summary>
        /// <param name="color">源color</param>
        /// <param name="rgbaMode">有RGBA</param>
        /// <param name="rgba">对应设置的值，按照rgba的顺序来设置</param>
        /// <returns></returns>
        public static Color Set(Color color, EColorMode rgbaMode, params float[] rgba)
        {
            float r = color.r;
            float g = color.g;
            float b = color.b;
            float a = color.a;
            var colorModes = EnumUtil.GetValues<EColorMode>();
            for (var i = 0; i < colorModes.Length; i++)
            {
                var colorMode = colorModes[i];
                if (!rgbaMode.Contains(colorMode)) continue;
                switch (colorMode)
                {
                    case EColorMode.R:
                        r = rgba[i];
                        break;
                    case EColorMode.G:
                        g = rgba[i];
                        break;
                    case EColorMode.B:
                        b = rgba[i];
                        break;
                    case EColorMode.A:
                        a = rgba[i];
                        break;
                }
            }

            return new Color(r, g, b, a);
        }


        /// <summary>
        /// 反向值
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color Inverted(Color color)
        {
            var result = Color.white - color;
            result.a = color.a;
            return result;
        }


        /// <summary>
        /// 转为HtmlStringRGB
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHtmlStringRGB(Color color)
        {
            return ColorUtility.ToHtmlStringRGB(color);
        }

        /// <summary>
        /// 转为HtmlStringRGB
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHtmlStringRGBA(Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }


        public static Color ToGray(Color color)
        {
            float lum = color.r * .3f + color.g * .59f + color.b * .11f;
            Color result = new Color(lum, lum, lum, color.a);
            return result;
        }

        public static string ToHtmToHtmlStringRGBOrDefault(Color color, string toDefaultValue = null,
            Color defaultColor = default)
        {
            return ObjectUtil.Equals(color, defaultColor) ? toDefaultValue : color.ToHtmlStringRGB();
        }

        public static string ToHtmlStringRGBAOrDefault(Color color, string toDefaultValue = null,
            Color defaultColor = default)
        {
            return ObjectUtil.Equals(color, defaultColor) ? toDefaultValue : color.ToHtmlStringRGBA();
        }
    }
}