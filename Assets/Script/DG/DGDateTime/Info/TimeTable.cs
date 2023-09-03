using System.Text;

namespace DG
{
	public struct TimeTable
	{
		public int day;
		public int hour;
		public int minute;
		public int second;


		public TimeTable(int day, int hour, int minute, int second)
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
			if (this.day != 0)
				stringBuilder.Append(this.day + dayUnit);
			if (stringBuilder.Length != 0 || this.hour != 0)
				stringBuilder.Append(this.hour + hourUnit);
			if (stringBuilder.Length != 0 || this.minute != 0)
				stringBuilder.Append(this.minute + minuteUnit);
			if (stringBuilder.Length != 0 || this.second != 0)
				stringBuilder.Append(this.second + secondUnit);
			var result = stringBuilder.ToString();
			return result;
		}
	}
}