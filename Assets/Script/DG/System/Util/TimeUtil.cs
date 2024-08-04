using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace DG
{
	public static class TimeUtil
	{
		public static long SystemTicksToSecond(long tick)
		{
			return tick / (10000000);
		}

		public static long SecondToSystemTicks(double second)
		{
			return (long)(second * 10000000);
		}

		public static TimeInfo SecondToTimeTable(long seconds)
		{
			int day = (int)Mathf.Floor(seconds / (3600 * 24f));
			int hour = (int)Mathf.Floor((seconds % (3600 * 24)) / 3600f);
			int min = (int)Mathf.Floor((seconds % 3600) / 60f);
			int sec = (int)Mathf.Floor(seconds % 60f);

			return new TimeInfo(day, hour, min, sec);
		}

		/// <summary>
		/// 将seconds转为hh:mm:ss
		/// 倒计时经常使用
		/// </summary>
		/// <param name="seconds"></param>
		/// <param name="hCount">小时那位需要至少保留多少位，即1的时候显不显示为01</param>
		/// <param name="isZeroIgnore">是否小时或分钟或秒为0的时候忽视该位，高位有的话还是会保留地位的，即使低位为0</param>
		/// <returns></returns>
		public static string SecondToStringHHmmss(long seconds, int hCount = 2, bool isZeroIgnore = false)
		{
			var stringBuilder = new StringBuilder();
			long HH = seconds / 3600;
			isZeroIgnore = isZeroIgnore && HH == 0;
			if (!isZeroIgnore)
				stringBuilder.Append(HH.ToString().FillHead(hCount, CharConst.CHAR_0) +
				                           StringConst.STRING_COLON);

			long mm = (seconds % 3600) / 60;
			isZeroIgnore = isZeroIgnore && mm == 0;
			if (isZeroIgnore)
				stringBuilder.Append(mm.ToString().FillHead(2, CharConst.CHAR_0) + StringConst.STRING_COLON);


			long ss = seconds % 60;
			isZeroIgnore = isZeroIgnore && ss == 0;
			if (isZeroIgnore)
				stringBuilder.Append(ss.ToString().FillHead(2, CharConst.CHAR_0));

			return stringBuilder.ToString();
		}

		/// <summary>
		/// 以tick为单位的两个东西的相差多少秒
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <returns></returns>
		public static float DiffBySeconds(long t1, long t2)
		{
			float diff = (t1 - t2) / 10000000f; //
			return diff;
		}

		public static long GetNowTimestamp()
		{
			return Stopwatch.GetTimestamp();
		}
	}
}

