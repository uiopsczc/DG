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
	public static partial class FPMath
	{
		/** Returns the next power of two. Returns the specified value if the value is already a power of two. */
		public static FP NextPowerOfTwo(FP value)
		{
			var v = (int)value;
			if (v == 0) return 1;
			v--;
			v |= v >> 1;
			v |= v >> 2;
			v |= v >> 4;
			v |= v >> 8;
			v |= v >> 16;
			return v + 1;
		}

		public static bool IsPowerOfTwo(FP value)
		{
			var v = (int)value;
			return v != 0 && (v & v - 1) == 0;
		}

		/** Linearly normalizes value from a range. Range must not be empty. This is the inverse of {@link #lerp(float, float, float)}.
		 * @param rangeStart Range start normalized to 0
		 * @param rangeEnd Range end normalized to 1
		 * @param value Value to normalize
		 * @return Normalized value. Values outside of the range are not clamped to 0 and 1 */
		public static FP Norm(FP rangeStart, FP rangeEnd, FP value)
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
		public static FP Map(FP inRangeStart, FP inRangeEnd, FP outRangeStart, FP outRangeEnd, FP value)
		{
			return outRangeStart + (value - inRangeStart) * (outRangeEnd - outRangeStart) / (inRangeEnd - inRangeStart);
		}

		public static FP SinDeg(FP degrees)
		{
			return FP.Sin(degrees * DEG2RAD);
		}

		public static FP CosDeg(FP degrees)
		{
			return FP.Cos(degrees * DEG2RAD);
		}

		public static FP TanDeg(FP degrees)
		{
			return FP.Tan(degrees * DEG2RAD);
		}


		/** Returns true if the value is zero (using the default tolerance as upper bound) */
		public static bool IsZero(FP value)
		{
			return Abs(value) <= EPSILION;
		}

		/** Returns true if the value is zero.
		 * @param tolerance represent an upper bound below which the value is considered zero. */
		public static bool IsZero(FP value, FP tolerance)
		{
			return Abs(value) <= tolerance;
		}

		/** Returns true if a is nearly equal to b. The function uses the default floating error tolerance.
		 * @param a the first value.
		 * @param b the second value. */
		public static bool IsEqual(FP a, FP b)
		{
			return Abs(a - b) <= EPSILION;
		}

		/** Returns true if a is nearly equal to b.
		 * @param a the first value.
		 * @param b the second value.
		 * @param tolerance represent an upper bound below which the two values are considered equal. */
		public static bool IsEqual(FP a, FP b, FP tolerance)
		{
			return Abs(a - b) <= tolerance;
		}
	}
}

