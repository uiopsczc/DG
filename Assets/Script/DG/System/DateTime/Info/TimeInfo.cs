using System.Text;

namespace DG
{
    public struct TimeInfo
    {
        public int day;
        public int hour;
        public int minute;
        public int second;


        public TimeInfo(int day, int hour, int minute, int second)
        {
            this.day = day;
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public string GetFormatString(string dayUnit, string hourUnit, string minuteUnit,
            string secondUnit)
        {
            var stringBuilder = new StringBuilder(20);
            if (day != 0)
                stringBuilder.Append(day + dayUnit);
            if (stringBuilder.Length != 0 || hour != 0)
                stringBuilder.Append(hour + hourUnit);
            if (stringBuilder.Length != 0 || minute != 0)
                stringBuilder.Append(minute + minuteUnit);
            if (stringBuilder.Length != 0 || second != 0)
                stringBuilder.Append(second + secondUnit);
            var result = stringBuilder.ToString();
            return result;
        }
    }
}