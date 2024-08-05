namespace DG
{
    public static class CharConst
    {
        public const char CHAR_PLUS = '+';
        public const char CHAR_MUTIPLY = 'x';
        public const char CHAR_SPACE = ' ';
        public const char CHAR_MINUS = '-';
        public const char CHAR_UNDERLINE = '_';
        public const char CHAR_DIV = '/';
        public const char CHAR_SLASH = '/';
        public const char CHAR_SLASH_N = '\n';
        public const char CHAR_SLASH_R = '\r';
        public const char CHAR_COLON = ':';
        public const char CHAR_SEMICOLON = ';';
        public const char CHAR_COMMA = ',';
        public const char CHAR_DOT = '.';
        public const char CHAR_DOUBLE_QUOTES = '\"';
        public const char CHAR_QUOTES = '\'';
        public const char CHAR_LEFT_ROUND_BRACKETS = '(';
        public const char CHAR_RIGHT_ROUND_BRACKETS = ')';
        public const char CHAR_LEFT_SQUARE_BRACKETS = '[';
        public const char CHAR_RIGHT_SQUARE_BRACKETS = ']';
        public const char CHAR_LEFT_CURLY_BRACKETS = '{';
        public const char CHAR_RIGHT_CURLY_BRACKETS = '}';
        public const char CHAR_LEFT_ANGLE_BRACKETS = '<';
        public const char CHAR_RIGHT_ANGLE_BRACKETS = '>';
        public const char CHAR_VERTICAL = '|';
        public const char CHAR_TILDE = '~';
        public const char CHAR_TAB = '\t';
        public const char CHAR_NUMBER_SIGN = '#';
        public const char CHAR_BACK_SLASH = '\\';

        public const char CHAR_0 = '0';
        public const char CHAR_1 = '1';
        public const char CHAR_2 = '2';
        public const char CHAR_3 = '3';
        public const char CHAR_4 = '4';
        public const char CHAR_5 = '5';
        public const char CHAR_6 = '6';
        public const char CHAR_7 = '7';
        public const char CHAR_8 = '8';
        public const char CHAR_9 = '9';

        public const char CHAR_A = 'A';
        public const char CHAR_X = 'X';
        public const char CHAR_Z = 'Z';

        public const char CHAR_a = 'a';
        public const char CHAR_c = 'c';
        public const char CHAR_x = 'x';
        public const char CHAR_y = 'y';
        public const char CHAR_z = 'z';

        public static readonly char[] DIGITS =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        public static readonly char[] CHARS_BIG =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        public static readonly char[] CHARS_SMALL =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
            'x', 'y', 'z'
        };

        private static char[] _CHARS_ALL;
        public static char[] CHARS_ALL => _CHARS_ALL ?? (_CHARS_ALL = CharUtil.GetCharsAll());
        private static char[] _DIGITS_AND_CHARS_BIG;

        public static char[] DIGITS_AND_CHARS_BIG =>
            _DIGITS_AND_CHARS_BIG ?? (_DIGITS_AND_CHARS_BIG = CharUtil.GetDigitsAndCharsBig());

        private static char[] _DIGITS_AND_CHARS_SMALL;

        public static char[] DIGITS_AND_CHARS_SMALL =>
            _DIGITS_AND_CHARS_SMALL ?? (_DIGITS_AND_CHARS_SMALL = CharUtil.GetDigitsAndCharsSmall());

        private static char[] _DIGITS_AND_CHARS_ALL;

        public static char[] DIGITS_AND_CHARS_ALL =>
            _DIGITS_AND_CHARS_ALL ?? (_DIGITS_AND_CHARS_ALL = CharUtil.GetDigitsAndCharsAll());
    }
}