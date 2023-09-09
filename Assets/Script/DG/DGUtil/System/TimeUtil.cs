using System;
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

		public static TimeTable SecondToTimeTable(long seconds)
		{
			int day = (int)Mathf.Floor(seconds / (3600 * 24f));
			int hour = (int)Mathf.Floor((seconds % (3600 * 24)) / 3600f);
			int min = (int)Mathf.Floor((seconds % 3600) / 60f);
			int sec = (int)Mathf.Floor(seconds % 60f);

			return new TimeTable(day, hour, min, sec);
		}

		/// <summary>
		/// ��secondsתΪhh:mm:ss
		/// ����ʱ����ʹ��
		/// </summary>
		/// <param name="seconds"></param>
		/// <param name="hCount">Сʱ��λ��Ҫ���ٱ�������λ����1��ʱ���Բ���ʾΪ01</param>
		/// <param name="isZeroIgnore">�Ƿ�Сʱ����ӻ���Ϊ0��ʱ����Ӹ�λ����λ�еĻ����ǻᱣ����λ�ģ���ʹ��λΪ0</param>
		/// <returns></returns>
		public static string SecondToStringHHmmss(long seconds, int hCount = 2, bool isZeroIgnore = false)
		{
			var stringBuilder = new StringBuilder();
			long HH = seconds / 3600;
			isZeroIgnore = isZeroIgnore && HH == 0;
			if (!isZeroIgnore)
				stringBuilder.Append(HH.ToString().FillHead(hCount, CharConst.Char_0) +
				                           StringConst.String_Colon);

			long mm = (seconds % 3600) / 60;
			isZeroIgnore = isZeroIgnore && mm == 0;
			if (isZeroIgnore)
				stringBuilder.Append(mm.ToString().FillHead(2, CharConst.Char_0) + StringConst.String_Colon);


			long ss = seconds % 60;
			isZeroIgnore = isZeroIgnore && ss == 0;
			if (isZeroIgnore)
				stringBuilder.Append(ss.ToString().FillHead(2, CharConst.Char_0));

			return stringBuilder.ToString();
		}

		/// <summary>
		/// ��tickΪ��λ��������������������
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

