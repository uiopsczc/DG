using UnityEngine;

namespace DG
{
    public static class UnityEngine_Color_Extension
    {
        public static Color SetR(this Color self, float v)
        {
            return ColorUtil.Set(self, EColorMode.R, v);
        }

        public static Color SetG(this Color self, float v)
        {
            return ColorUtil.Set(self, EColorMode.G, v);
        }

        public static Color SetB(this Color self, float v)
        {
            return ColorUtil.Set(self, EColorMode.B, v);
        }

        public static Color SetA(this Color self, float v)
        {
            return ColorUtil.Set(self, EColorMode.A, v);
        }


        /// <summary>
        /// 修改rgba中的值，rgbaEnum任意组合
        /// </summary>
        /// <param name="self">源color</param>
        /// <param name="rgbaMode">有RGBA</param>
        /// <param name="rgba">对应设置的值，按照rgba的顺序来设置</param>
        /// <returns></returns>
        public static Color Set(this Color self, EColorMode rgbaMode, params float[] rgba)
        {
            return ColorUtil.Set(self, rgbaMode, rgba);
        }


        /// <summary>
        /// 反向值
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Color Inverted(this Color self)
        {
            return ColorUtil.Inverted(self);
        }


        /// <summary>
        /// 转为HtmlStringRGB
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToHtmlStringRGB(this Color self)
        {
            return ColorUtil.ToHtmlStringRGB(self);
        }

        public static string ToHtmToHtmlStringRGBOrDefault(this Color self, string toDefaultValue = null,
            Color defaultColor = default)
        {
            return ColorUtil.ToHtmToHtmlStringRGBOrDefault(self, toDefaultValue, defaultColor);
        }

        /// <summary>
        /// 转为HtmlStringRGB
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToHtmlStringRGBA(this Color self)
        {
            return ColorUtil.ToHtmlStringRGBA(self);
        }

        public static string ToHtmlStringRGBAOrDefault(this Color self, string toDefaultValue = null,
            Color defaultColor = default)
        {
            return ColorUtil.ToHtmlStringRGBAOrDefault(self, toDefaultValue, defaultColor);
        }

        public static Color ToGray(this Color self)
        {
            return ColorUtil.ToGray(self);
        }
    }
}