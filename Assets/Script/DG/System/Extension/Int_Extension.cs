using System.Collections.Generic;

namespace DG
{
	public static class Int_Extension
	{
		public static int RightShift3(this int self, int shiftAmount)
		{
			return IntUtil.RightShift3(self, shiftAmount);
		}

		public static T ToEnum<T>(this int self)
		{
			return IntUtil.ToEnum<T>(self);
		}

		#region 编码

		/// <summary>
		///   10进制转为16进制字符串
		/// </summary>
		public static string ToHexString(this int self)
		{
			return IntUtil.ToHexString(self);
		}

		#endregion

		#region bytes

		/// <summary>
		///   将数字转化为bytes
		/// </summary>
		public static byte[] ToBytes(this int self, bool isNetOrder = false)
		{
			return IntUtil.ToBytes(self, isNetOrder);
		}

		#endregion

				/// <summary>
				///   随机一个total以内的队列（队列里面的元素不会重复）
				/// </summary>
				/// <param name="self"></param>
				/// <param name="isIncludeTotal">是否包括total</param>
				/// <param name="isZeroBase">是否从0开始</param>
				/// <returns></returns>
				public static List<int> Random(this int self, float outCount, bool isUnique, RandomManager randomManager, bool isIncludeTotal = false,
					bool isZeroBase = true)
				{
					var result = new List<int>();
					var toRandomList = new List<int>(); //要被随机的List
		
					for (var i = isZeroBase ? 0 : 1; i < (isIncludeTotal ? self + 1 : self); i++)
						toRandomList.Add(i);
		
					for (var i = 0; i < outCount; i++)
					{
						var index = randomManager.RandomInt(0, toRandomList.Count);
						result.Add(isUnique ? toRandomList.RemoveAt2(index) : toRandomList[index]);
					}
		
					return result;
				}

		//是否是defalut, 默认是与float.MaxValue比较
		public static bool IsDefault(this int self, bool isMin = false)
		{
			return IntUtil.IsDefault(self, isMin);
		}

		public static bool IsInRange(this int self, int minValue, int maxValue, bool isMinValueIncluded = false,
			bool isMaxValueIncluded = false)
		{
			return IntUtil.IsInRange(self, minValue, maxValue, isMinValueIncluded, isMaxValueIncluded);
		}

		public static int Minimum(this int self, int minimum)
		{
			return IntUtil.Minimum(self, minimum);
		}

		public static int Maximum(this int self, int maximum)
		{
			return IntUtil.Maximum(self, maximum);
		}

		public static string ToStringWithComma(this int self)
		{
			return IntUtil.ToStringWithComma(self);
		}
	}
}