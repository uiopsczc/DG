using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
	public static class NumberUnitUtil
	{
		private const int MAX_INTEGER_COUNT = 3; // 有单位时最多显示多少为整数
		private const int MAX_DECIMALS_COUNT = 1; // 最多显示多少位小数
		private const int INIT_MAX_INTEGER_COUNT = 3; // 不使用单位时最多显示多少位

		//根据num和number_unit获取数量
		public static long GetNumber(float num, string numberUnit = null,
			Dictionary<string, NumberUnitInfo> numberUnitDict = null)
		{
			numberUnitDict ??= NumberUnitConst.NumberUnitDict;
			var zhiShu = 0; // 指数
			if (!numberUnit.IsNullOrWhiteSpace())
			{
				var numberUnitInfo = numberUnitDict[numberUnit];
				zhiShu = numberUnitInfo.zhiShu;
			}

			return (long)(num * (Math.Pow(10, zhiShu)));
		}

		//获取zhi_shu指数对应的单位
		public static string GetNumberUnitInfoByZhiShu(int zhiShu, List<NumberUnitInfo> numberUnitList = null)
		{
			numberUnitList ??= NumberUnitConst.NumberUnitList;
			for (var i = 0; i < numberUnitList.Count; i++)
			{
				var numberUnitInfo = numberUnitList[i];
				if (numberUnitInfo.zhiShu == zhiShu)
					return numberUnitInfo.numberUnit;
			}

			throw new Exception(string.Format("没有该指数的单位信息 指数:{0}", zhiShu)); //指数
		}

		//when_show_unit传入的是大于多少开始显示单位
		public static string GetString(long num, int? maxDecimalsCount, long? whenShowUnit,
			List<NumberUnitInfo> numberUnitList = null)
		{
			if (whenShowUnit.HasValue && num >= whenShowUnit)
			{
				int maxDecimalsCountValue = maxDecimalsCount.GetValueOrDefault(MAX_DECIMALS_COUNT);
				var isFuShu = num < 0; // 是否是负数
				num = Math.Abs(num);
				var zhiShu = 0; // 指数
				num = (long)Mathf.Floor(num);
				var getNum = num;
				while (true)
				{
					if (getNum < 10)
						break;
					getNum = (long)Mathf.Floor(getNum / 10f);
					zhiShu += 1;
				}

				float showNum;
				string showUnit;
				if ((zhiShu + 1) <= INIT_MAX_INTEGER_COUNT)
				{
					showNum = num;
					showUnit = StringConst.STRING_EMPTY;
				}
				else
				{
					var outZhiShu = zhiShu - INIT_MAX_INTEGER_COUNT;
					var showWeiShu = outZhiShu % MAX_INTEGER_COUNT;
					showNum = Mathf.Floor(num / (Mathf.Pow(10, (zhiShu - showWeiShu - maxDecimalsCountValue - 1))));
					showNum = Mathf.Floor((showNum + 5) / 10);
					showNum /= (Mathf.Pow(10, maxDecimalsCountValue));
					showUnit = GetNumberUnitInfoByZhiShu((int)(Mathf.Floor(zhiShu / 3f) * 3),
						numberUnitList);
				}

				var result = string.Format("{0}{1}", showNum, showUnit);
				if (isFuShu) // 如果是负数
					result = string.Format("-{0}", result);
				return result;
			}

			return ((long)Mathf.Floor(num)).ToString();
		}
	}
}