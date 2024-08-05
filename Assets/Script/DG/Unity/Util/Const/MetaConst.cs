using System.Text.RegularExpressions;

namespace DG
{
    public static class MetaConst
    {
        public static Regex GUID_REGEX =
            new("(?<=(guid: ))[\\s\\S]*?(?=(,))", RegexOptions.Multiline | RegexOptions.Singleline);

        public static Regex FILE_ID_REGEX =
            new("(?<=(fileID: ))[\\s\\S]*?(?=(,))", RegexOptions.Multiline | RegexOptions.Singleline);

        public static Regex FONT_REGEX = new("(?<=(m_Font: {))[\\s\\S]*?(?=(}))",
            RegexOptions.Multiline | RegexOptions.Singleline);

        public static Regex SPRITE_REGEX = new("(?<=(m_Sprite: {))[\\s\\S]*?(?=(}))",
            RegexOptions.Multiline | RegexOptions.Singleline);
    }
}