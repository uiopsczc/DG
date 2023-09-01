/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/5/12
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/


namespace DG
{
	public static partial class DGMath
	{
		/** Returns the next power of two. Returns the specified value if the value is already a power of two. */
		public static DGFixedPoint NextPowerOfTwo(DGFixedPoint value)
		{
			var v = (int)value;
			if (v == 0) return (DGFixedPoint)1;
			v--;
			v |= v >> 1;
			v |= v >> 2;
			v |= v >> 4;
			v |= v >> 8;
			v |= v >> 16;
			return (DGFixedPoint)(v + 1);
		}

		public static bool IsPowerOfTwo(DGFixedPoint value)
		{
			var v = (int)value;
			return v != 0 && (v & v - 1) == 0;
		}

		/** Linearly normalizes value from a range. Range must not be empty. This is the inverse of {@link #lerp(float, float, float)}.
		 * @param rangeStart Range start normalized to 0
		 * @param rangeEnd Range end normalized to 1
		 * @param value Value to normalize
		 * @return Normalized value. Values outside of the range are not clamped to 0 and 1 */
		public static DGFixedPoint Norm(DGFixedPoint rangeStart, DGFixedPoint rangeEnd, DGFixedPoint value)
		{
			return (value - rangeStart) / (rangeEnd - rangeStart);
		}

		/** Linearly map a value from one range to another. Input range must not be empty. This is the same as chaining
		 * {@link #norm(float, float, float)} from input range and {@link #lerp(float, float, float)} to output range.
		 * @param inRangeStart Input range start
		 * @param inRangeEnd Input range end
		 * @param outRangeStart Output range start
		 * @param outRangeEnd Output range end
		 * @param value Value to map
		 * @return Mapped value. Values outside of the input range are not clamped to output range */
		public static DGFixedPoint Map(DGFixedPoint inRangeStart, DGFixedPoint inRangeEnd, DGFixedPoint outRangeStart, DGFixedPoint outRangeEnd, DGFixedPoint value)
		{
			return outRangeStart + (value - inRangeStart) * (outRangeEnd - outRangeStart) / (inRangeEnd - inRangeStart);
		}

		public static DGFixedPoint SinDeg(DGFixedPoint degrees)
		{
			return DGFixedPoint.Sin(degrees * Deg2Rad);
		}

		public static DGFixedPoint CosDeg(DGFixedPoint degrees)
		{
			return DGFixedPoint.Cos(degrees * Deg2Rad);
		}

		public static DGFixedPoint TanDeg(DGFixedPoint degrees)
		{
			return DGFixedPoint.Tan(degrees * Deg2Rad);
		}


		/** Returns true if the value is zero (using the default tolerance as upper bound) */
		public static bool IsZero(DGFixedPoint value)
		{
			return Abs(value) <= Epsilon;
		}

		/** Returns true if the value is zero.
		 * @param tolerance represent an upper bound below which the value is considered zero. */
		public static bool IsZero(DGFixedPoint value, DGFixedPoint tolerance)
		{
			return Abs(value) <= tolerance;
		}

		/** Returns true if a is nearly equal to b. The function uses the default floating error tolerance.
		 * @param a the first value.
		 * @param b the second value. */
		public static bool IsEqual(DGFixedPoint a, DGFixedPoint b)
		{
			return Abs(a - b) <= Epsilon;
		}

		/** Returns true if a is nearly equal to b.
		 * @param a the first value.
		 * @param b the second value.
		 * @param tolerance represent an upper bound below which the two values are considered equal. */
		public static bool IsEqual(DGFixedPoint a, DGFixedPoint b, DGFixedPoint tolerance)
		{
			return Abs(a - b) <= tolerance;
		}
	}
}

