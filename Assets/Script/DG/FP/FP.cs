/*************************************************************************************
 * 描    述:  定点数，抄自https://github.com/asik/FixedMath.Net，里面还有fix16，fix8的实现
 * 创 建 者:  czq
 * 创建时间:  2023/5/8
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DG
{
	public partial struct FP : IEquatable<FP>, IComparable<FP>
	{
		//不能修改Null的值
		public static readonly FP NULL = FPConstInternal.MAX_VALUE;
		public static readonly decimal PRECISION = FPConstInternal.PRECISION;
		public static readonly FP EPSILION = FPConstInternal.SCALED_EPSILON;
		public static readonly FP MAX_VALUE = FPConstInternal.MAX_VALUE;
		public static readonly FP MIN_VALUE = FPConstInternal.MIN_VALUE;
		public static readonly FP ONE = FPConstInternal.SCALED_ONE;
		public static readonly FP NEGATIVE_ONE = FPConstInternal.SCALED_NEGATIVE_ONE;
		public static readonly FP HALF = (long) FPConstInternal.SCALED_HALF_ONE;
		public static readonly FP QUARTER = (long) FPConstInternal.SCALED_HALF_QUARTER;
		public static readonly FP ZERO = 0;
		public static readonly FP PI = FPConstInternal.SCALED_PI;
		public static readonly FP HALF_PI = FPConstInternal.SCALED_HALF_PI;
		public static readonly FP TWO_PI = FPConstInternal.SCALED_TWO_PI;
		public static readonly FP FOUR_PI_DIV_3 = PI * 4 / 3;
		public static readonly FP QUARTER_PI = FPConstInternal.SCALED_QUARTER_PI;
		public static readonly FP LN2 = FPConstInternal.SCALED_LN2; //以E为底的2的对数
		public static readonly FP LOG2MAX = FPConstInternal.SCALED_LOG2MAX;
		public static readonly FP LOG2MIN = FPConstInternal.SCALED_LOG2MIN;
		public static readonly FP E = FPConstInternal.SCALED_E;
		public static readonly FP DEG2RAD = FPConstInternal.SCALED_DEG2RAD;
		public static readonly FP RAD2DEG = FPConstInternal.SCALED_RAD2DEG;

		static readonly FP LUT_INTERVAL = (FPConstInternal.LUT_SIZE - 1) / HALF_PI;

		public static readonly Dictionary<int, FP> CACHE = new Dictionary<int, FP>
		{
			{0, ZERO},
			{1, ONE},
			{2, 2},
			{10, 10},
			{360, 360},
			{180, 180},
			{-1, -1},
		};

		public long scaledValue { get; }

		FP(long rawValue)
		{
			scaledValue = rawValue;
		}

		public FP(int value)
		{
			scaledValue = value * FPConstInternal.SCALED_ONE;
		}

		public static FP CreateByScaledValue(long scaledValue)
		{
			return scaledValue;
		}

		#region Equals ToString

		public override bool Equals(object obj)
		{
			return obj is FP fixedPoint && fixedPoint.scaledValue == scaledValue;
		}

		public bool Equals(FP other)
		{
			return scaledValue == other.scaledValue;
		}

		public override int GetHashCode()
		{
			return scaledValue.GetHashCode();
		}

		public int CompareTo(FP other)
		{
			return scaledValue.CompareTo(other.scaledValue);
		}

		public override string ToString()
		{
			return ((decimal) this).ToString("0.##########");
		}

		#endregion

		#region 转换

		public static implicit operator FP(long value)
		{
			return value << FPConstInternal.MOVE_BIT_COUNT;
		}

		public static implicit operator long(FP value)
		{
			return value.scaledValue >> FPConstInternal.MOVE_BIT_COUNT;
		}

		public static implicit operator FP(float value)
		{
			return (long) (value * FPConstInternal.SCALED_ONE);
		}

		public static implicit operator float(FP value)
		{
			return (float) value.scaledValue / FPConstInternal.SCALED_ONE;
		}

		public static implicit operator FP(double value)
		{
			return (long) (value * FPConstInternal.SCALED_ONE);
		}

		public static implicit operator double(FP value)
		{
			return (double) value.scaledValue / FPConstInternal.SCALED_ONE;
		}

		public static implicit operator FP(decimal value)
		{
			return (long) (value * FPConstInternal.SCALED_ONE);
		}

		public static implicit operator decimal(FP value)
		{
			return (decimal) value.scaledValue / FPConstInternal.SCALED_ONE;
		}

		#endregion

		#region 关系运算符

		public static bool operator ==(FP value1, FP value2)
		{
			return value1.scaledValue == value2.scaledValue;
		}

		public static bool operator !=(FP value1, FP value2)
		{
			return value1.scaledValue != value2.scaledValue;
		}

		public static bool operator >(FP value1, FP value2)
		{
			return value1.scaledValue > value2.scaledValue;
		}

		public static bool operator <(FP value1, FP value2)
		{
			return value1.scaledValue < value2.scaledValue;
		}

		public static bool operator >=(FP value1, FP value2)
		{
			return value1.scaledValue >= value2.scaledValue;
		}

		public static bool operator <=(FP value1, FP value2)
		{
			return value1.scaledValue <= value2.scaledValue;
		}

		#endregion

		#region 算术运算符

		public static FP operator +(FP value1, FP value2)
		{
			var scaledValue1 = value1.scaledValue;
			var scaledValue2 = value2.scaledValue;
			var scaledValueSum = scaledValue1 + scaledValue2;
			//如果v1和v2相同sign，但sum和x的sign不同，则sum的值溢出
			if (((~(scaledValue1 ^ scaledValue2) & (scaledValue1 ^ scaledValueSum)) &
			     FPConstInternal.MIN_VALUE) != 0)
				scaledValueSum = scaledValue1 > 0
					? FPConstInternal.MAX_VALUE
					: FPConstInternal.MIN_VALUE;
			return scaledValueSum;
		}

		public static FP operator -(FP value1, FP value2)
		{
			var scaledValue1 = value1.scaledValue;
			var scaleValue2 = value2.scaledValue;
			var scaledDiff = scaledValue1 - scaleValue2;
			//如果v1和v2不同sign，但diff和x的sign不同，则diff的值溢出
			if ((((scaledValue1 ^ scaleValue2) & (scaledValue1 ^ scaledDiff)) & FPConstInternal.MIN_VALUE) !=
			    0)
				scaledDiff = scaledValue1 < 0
					? FPConstInternal.MIN_VALUE
					: FPConstInternal.MAX_VALUE;
			return scaledDiff;
		}

		public static FP operator *(FP value1, FP value2)
		{
			var scaledValue1 = value1.scaledValue;
			var scaledValue2 = value2.scaledValue;

			var low1 = (ulong) (scaledValue1 & FPConstInternal.SCALED_FRACTIONAL_PART_MASK);
			var high1 = scaledValue1 >> FPConstInternal.MOVE_BIT_COUNT;
			var low2 = (ulong) (scaledValue2 & FPConstInternal.SCALED_FRACTIONAL_PART_MASK);
			var high2 = scaledValue2 >> FPConstInternal.MOVE_BIT_COUNT;

			var low1Low2 = low1 * low2;
			var low1High2 = (long) low1 * high2;
			var high1Low2 = high1 * (long) low2;
			var high1High2 = high1 * high2;

			var scaledLowResult = low1Low2 >> FPConstInternal.MOVE_BIT_COUNT;
			var scaledMidResult1 = low1High2;
			var scaledMidResult2 = high1Low2;
			var scaledHighResult = high1High2 << FPConstInternal.MOVE_BIT_COUNT;

			bool overflow = false;
			var scaledSum = _AddOverflowHelper((long) scaledLowResult, scaledMidResult1, ref overflow);
			scaledSum = _AddOverflowHelper(scaledSum, scaledMidResult2, ref overflow);
			scaledSum = _AddOverflowHelper(scaledSum, scaledHighResult, ref overflow);

			bool opSignsEqual = ((scaledValue1 ^ scaledValue2) & FPConstInternal.MIN_VALUE) == 0;

			// if signs of operands are equal and sign of result is negative,
			// then multiplication overflowed positively
			// the reverse is also true
			if (opSignsEqual)
			{
				if (scaledSum < 0 || (overflow && scaledValue1 > 0))
					return MAX_VALUE;
			}
			else
			{
				if (scaledSum > 0)
					return MIN_VALUE;
			}

			// if the top 32 bits of hihi (unused in the result) are neither all 0s or 1s,
			// then this means the result overflowed.
			var topCarry = high1High2 >> FPConstInternal.MOVE_BIT_COUNT;
			if (topCarry != FPConstInternal.INTEGRAL_PART_ALL_ZERO &&
			    topCarry != FPConstInternal.INTEGRAL_PART_ALL_ONE /*&& xl != -17 && yl != -17*/)
				return opSignsEqual ? MAX_VALUE : MIN_VALUE;

			// If signs differ, both operands' magnitudes are greater than 1,
			// and the result is greater than the negative operand, then there was negative overflow.
			if (!opSignsEqual)
			{
				long posOp, negOp;
				if (scaledValue1 > scaledValue2)
				{
					posOp = scaledValue1;
					negOp = scaledValue2;
				}
				else
				{
					posOp = scaledValue2;
					negOp = scaledValue1;
				}

				if (scaledSum > negOp && negOp < -FPConstInternal.SCALED_ONE &&
				    posOp > FPConstInternal.SCALED_ONE)
					return MIN_VALUE;
			}

			return scaledSum;
		}

		public static FP operator /(FP value1, FP value2)
		{
			var scaledValue1 = value1.scaledValue;
			var scaledValue2 = value2.scaledValue;

			if (scaledValue2 == 0)
				throw new DivideByZeroException();

			var remainder = (ulong) (scaledValue1 >= 0 ? scaledValue1 : -scaledValue1);
			var divider = (ulong) (scaledValue2 >= 0 ? scaledValue2 : -scaledValue2);
			var quotient = 0UL;
			var bitPos = FPConstInternal.NUM_BIT_COUNT / 2 + 1;


			// If the divider is divisible by 2^n, take advantage of it.
			while ((divider & 0xF) == 0 && bitPos >= 4)
			{
				divider >>= 4;
				bitPos -= 4;
			}

			while (remainder != 0 && bitPos >= 0)
			{
				int shift = _CountLeadingZeroes(remainder);
				if (shift > bitPos)
					shift = bitPos;
				remainder <<= shift;
				bitPos -= shift;

				var div = remainder / divider;
				remainder = remainder % divider;
				quotient += div << bitPos;

				// Detect overflow
				if ((div & ~(FPConstInternal.ALL_ONE >> bitPos)) != 0)
					return ((scaledValue1 ^ scaledValue2) & FPConstInternal.MIN_VALUE) == 0
						? MAX_VALUE
						: MIN_VALUE;

				remainder <<= 1;
				--bitPos;
			}

			// rounding
			++quotient;
			var result = (long) (quotient >> 1);
			if (((scaledValue1 ^ scaledValue2) & FPConstInternal.MIN_VALUE) != 0)
				result = -result;

			return result;
		}

		public static FP operator %(FP value1, FP value2)
		{
			return
				value1.scaledValue == FPConstInternal.MIN_VALUE & value2.scaledValue == -1
					? 0
					: value1.scaledValue % value2.scaledValue;
		}

		public static FP operator -(FP value)
		{
			return value.scaledValue == FPConstInternal.MIN_VALUE
				? MAX_VALUE
				: new FP(-value.scaledValue);
		}

		#endregion

		#region Fast 运算(但不安全)

		public static FP FastAdd(FP value1, FP value2)
		{
			return new FP(value1.scaledValue + value2.scaledValue);
		}

		public static FP FastSub(FP value1, FP value2)
		{
			long x = 0;
			long y = 1;
			long sum = x + y;
			var t = (x ^ y ^ sum);
			return new FP(value1.scaledValue - value2.scaledValue);
		}

		public static FP FastMul(FP value1, FP value2)
		{
			var scaleValue1 = value1.scaledValue;
			var scaleValue2 = value2.scaledValue;

			var low1 = (ulong) (scaleValue1 & FPConstInternal.SCALED_FRACTIONAL_PART_MASK);
			var high1 = scaleValue1 >> FPConstInternal.MOVE_BIT_COUNT;
			var low2 = (ulong) (scaleValue2 & FPConstInternal.SCALED_FRACTIONAL_PART_MASK);
			var high2 = scaleValue2 >> FPConstInternal.MOVE_BIT_COUNT;

			var low1Low2 = low1 * low2;
			var low1High2 = (long) low1 * high2;
			var high1Low2 = high1 * (long) low2;
			var high1High2 = high1 * high2;

			var scaleLowResult = low1Low2 >> FPConstInternal.MOVE_BIT_COUNT;
			var scaledMidResult1 = low1High2;
			var scaledMidResult2 = high1Low2;
			var scaledHighResult = high1High2 << FPConstInternal.MOVE_BIT_COUNT;

			var scaledSum = (long) scaleLowResult + scaledMidResult1 + scaledMidResult2 + scaledHighResult;
			return scaledSum;
		}

		public static FP FastMod(FP value1, FP value2)
		{
			return value1.scaledValue % value2.scaledValue;
		}

		public static FP FastAbs(FP value)
		{
			// branchless implementation, see http://www.strchr.com/optimized_abs_function
			var mask = value.scaledValue >> (FPConstInternal.NUM_BIT_COUNT - 1);
			return (value.scaledValue + mask) ^ mask;
		}

		public static FP FastSin(FP value)
		{
			var clampedScaledSinValue =
				_ClampSinValue(value.scaledValue, out bool flipHorizontal, out bool flipVertical);

			// Here we use the fact that the SinLut table has a number of entries
			// equal to (PI_OVER_2 >> 15) to use the angle to index directly into it
			var rawIndex = (uint) (clampedScaledSinValue >> 15);
			if (rawIndex >= FPConstInternal.LUT_SIZE)
				rawIndex = FPConstInternal.LUT_SIZE - 1;
			var nearestValue =
				FPSinLut.SinLut[
					flipHorizontal ? FPSinLut.SinLut.Length - 1 - (int) rawIndex : (int) rawIndex];
			return flipVertical ? -nearestValue : nearestValue;
		}

		public static FP FastCos(FP value)
		{
			var scaledValue = value.scaledValue;
			var scaledAngle = scaledValue + (scaledValue > 0
				                  ? -FPConstInternal.SCALED_PI - FPConstInternal.SCALED_HALF_PI
				                  : FPConstInternal.SCALED_HALF_PI);
			return FastSin(scaledAngle);
		}

		#endregion

		#region StaticUtil

		public static int Sign(FP value)
		{
			return
				value.scaledValue < 0 ? -1 :
				value.scaledValue > 0 ? 1 :
				0;
		}

		public static FP CopySign(FP x, FP y)
		{
			if ((x.scaledValue >= 0 && y.scaledValue >= 0)
			    || (x.scaledValue <= 0 && y.scaledValue <= 0))
				return x;
			return -x;
		}


		public static FP Abs(FP value)
		{
			if (value.scaledValue == FPConstInternal.MIN_VALUE)
				return MAX_VALUE;

			// branchless implementation, see http://www.strchr.com/optimized_abs_function
			var mask = value.scaledValue >> (FPConstInternal.NUM_BIT_COUNT - 1);
			return (value.scaledValue + mask) ^ mask;
		}

		public static FP Truncate(FP value)
		{
			var scaledValue = value.scaledValue;
			if (scaledValue < 0)
				return CreateByScaledValue(-(-scaledValue >> FPConstInternal.MOVE_BIT_COUNT <<
				                             FPConstInternal.MOVE_BIT_COUNT));
			return CreateByScaledValue(scaledValue >> FPConstInternal.MOVE_BIT_COUNT <<
			                           FPConstInternal.MOVE_BIT_COUNT);
		}

		public static FP Floor(FP value)
		{
			return
				(long) ((ulong) value.scaledValue & FPConstInternal.SCALED_INTEGRAL_PART_MASK);
		}

		public static FP Ceiling(FP value)
		{
			var hasFractionalPart = (value.scaledValue & FPConstInternal.SCALED_FRACTIONAL_PART_MASK) != 0;
			return hasFractionalPart ? Floor(value) + ONE : value;
		}

		public static FP Round(FP value)
		{
			var fractionalPart = value.scaledValue & FPConstInternal.SCALED_FRACTIONAL_PART_MASK;
			var integralPart = Floor(value);
			if (fractionalPart < FPConstInternal.SCALED_ROUND_FRACTIONAL_PART_MASK)
				return integralPart;
			if (fractionalPart > FPConstInternal.SCALED_ROUND_FRACTIONAL_PART_MASK)
				return integralPart + ONE;
			// 小数部分等于0.5时， System.Math.Round()的处理方式是四舍五入到最近的偶整数
			return (integralPart.scaledValue & FPConstInternal.SCALED_ONE) == 0
				? integralPart
				: integralPart + ONE;
		}

		static long _AddOverflowHelper(long scaledValue1, long scaledValue2, ref bool overflow)
		{
			var sum = scaledValue1 + scaledValue2;
			// x + y overflows if sign(x) ^ sign(y) != sign(sum)
			overflow |= ((scaledValue1 ^ scaledValue2 ^ sum) & FPConstInternal.MIN_VALUE) != 0;
			return sum;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static int _CountLeadingZeroes(ulong x)
		{
			int result = 0;
			while ((x & FPConstInternal.COUNT_LEADING_ZERO_ROUGH_Mask) == 0)
			{
				result += 4;
				x <<= 4;
			}

			while ((x & FPConstInternal.COUNT_LEADING_ZERO_MASK) == 0)
			{
				result += 1;
				x <<= 1;
			}

			return result;
		}

		internal static FP Pow2(FP power)
		{
			if (power.scaledValue == 0)
				return ONE;

			// Avoid negative arguments by exploiting that exp(-x) = 1/exp(x).
			bool neg = power.scaledValue < 0;
			if (neg)
				power = -power;

			if (power == ONE)
				return neg ? ONE /  2 :  2;
			if (power >= LOG2MAX)
				return neg ? ONE / MAX_VALUE : MAX_VALUE;
			if (power <= LOG2MIN)
				return neg ? MAX_VALUE : ZERO;

			/* The algorithm is based on the power series for exp(x):
			 * http://en.wikipedia.org/wiki/Exponential_function#Formal_definition
			 * 
			 * From term n, we get term n+1 by multiplying with x/n.
			 * When the sum term drops to zero, we can stop summing.
			 */

			int integerPart = (int) Floor(power);
			// Take fractional part of exponent
			power = power.scaledValue & FPConstInternal.SCALED_FRACTIONAL_PART_MASK;

			var result = ONE;
			var term = ONE;
			int i = 1;
			while (term.scaledValue != 0)
			{
				term = FastMul(FastMul(power, term), LN2) / i;
				result += term;
				i++;
			}

			result = CreateByScaledValue(result.scaledValue << integerPart);
			if (neg)
				result = ONE / result;

			return result;
		}

		internal static FP Log2(FP value)
		{
			if (value.scaledValue <= 0)
				throw new ArgumentOutOfRangeException("Non-positive value passed to Ln", "x");

			// This implementation is based on Clay. S. Turner's fast binary logarithm
			// algorithm (C. S. Turner,  "A Fast Binary Logarithm Algorithm", IEEE Signal
			//     Processing Mag., pp. 124,140, Sep. 2010.)

			long b = 1U << (FPConstInternal.MOVE_BIT_COUNT - 1);
			long y = 0;

			long scaledValue = value.scaledValue;
			while (scaledValue < FPConstInternal.SCALED_ONE)
			{
				scaledValue <<= 1;
				y -= FPConstInternal.SCALED_ONE;
			}

			while (scaledValue >= (FPConstInternal.SCALED_ONE << 1))
			{
				scaledValue >>= 1;
				y += FPConstInternal.SCALED_ONE;
			}

			FP z = scaledValue;

			for (int i = 0; i < FPConstInternal.MOVE_BIT_COUNT; i++)
			{
				z = FastMul(z, z);
				if (z.scaledValue >= (FPConstInternal.SCALED_ONE << 1))
				{
					z = z.scaledValue >> 1;
					y += b;
				}

				b >>= 1;
			}

			return y;
		}

		public static FP Ln(FP x)
		{
			return FastMul(Log2(x), LN2);
		}

		public static FP Pow(FP num, FP power)
		{
			if (num == ONE)
				return ONE;
			if (power.scaledValue == 0)
				return ONE;
			if (num.scaledValue == 0)
			{
				if (power.scaledValue < 0)
					throw new DivideByZeroException();
				return ZERO;
			}

			FP log2 = Log2(num);
			return Pow2(power * log2);
		}

		public static FP Sqrt(FP value)
		{
			var scaledValue1 = value.scaledValue;
			if (scaledValue1 < 0)
			{
				// We cannot represent infinities like Single and Double, and Sqrt is
				// mathematically undefined for x < 0. So we just throw an exception.
				throw new ArgumentOutOfRangeException("Negative value passed to Sqrt", "x");
			}

			var num = (ulong) scaledValue1;
			var result = 0UL;

			// second-to-top bit
			var bit = 1UL << (FPConstInternal.NUM_BIT_COUNT - 2);

			while (bit > num)
				bit >>= 2;

			// The main part is executed twice, in order to avoid
			// using 128 bit values in computations.
			for (var i = 0; i < 2; ++i)
			{
				// First we get the top 48 bits of the answer.
				while (bit != 0)
				{
					if (num >= result + bit)
					{
						num -= result + bit;
						result = (result >> 1) + bit;
					}
					else
						result = result >> 1;

					bit >>= 2;
				}

				if (i == 0)
				{
					// Then process it again to get the lowest 16 bits.
					if (num > (1UL << (FPConstInternal.NUM_BIT_COUNT / 2)) - 1)
					{
						// The remainder 'num' is too large to be shifted left
						// by 32, so we have to add 1 to result manually and
						// adjust 'num' accordingly.
						// num = a - (result + 0.5)^2
						//       = num + result^2 - (result + 0.5)^2
						//       = num - result - 0.5
						num -= result;
						num = (num << (FPConstInternal.NUM_BIT_COUNT / 2)) -
						      FPConstInternal.SCALED_HALF_ONE;
						result = (result << (FPConstInternal.NUM_BIT_COUNT / 2)) +
						         FPConstInternal.SCALED_HALF_ONE;
					}
					else
					{
						num <<= (FPConstInternal.NUM_BIT_COUNT / 2);
						result <<= (FPConstInternal.NUM_BIT_COUNT / 2);
					}

					bit = 1UL << (FPConstInternal.NUM_BIT_COUNT / 2 - 2);
				}
			}

			// Finally, if next bit would have been 1, round the result upwards.
			if (num > result)
				++result;
			return (long) result;
		}

		/// <summary>
		/// Returns the Sine of x.
		/// The relative error is less than 1E-10 for x in [-2PI, 2PI], and less than 1E-7 in the worst case.
		/// </summary>
		public static FP Sin(FP value)
		{
			var clampedScaledSinValue = _ClampSinValue(value.scaledValue, out var flipHorizontal, out var flipVertical);
			FP clamped = clampedScaledSinValue;

			// Find the two closest values in the LUT and perform linear interpolation
			// This is what kills the performance of this function on x86 - x64 is fine though
			var rawIndex = FastMul(clamped, LUT_INTERVAL);
			var roundedIndex = Round(rawIndex);
			var indexError = FastSub(rawIndex, roundedIndex);

			FP nearestValue = FPSinLut.SinLut[
				flipHorizontal ? FPSinLut.SinLut.Length - 1 - (int) roundedIndex : (int) roundedIndex];
			FP secondNearestValue = FPSinLut.SinLut[
				flipHorizontal
					? FPSinLut.SinLut.Length - 1 - (int) roundedIndex - Sign(indexError)
					: (int) roundedIndex + Sign(indexError)];

			var delta = FastMul(indexError, FastAbs(FastSub(nearestValue, secondNearestValue))).scaledValue;
			var interpolatedValue = nearestValue.scaledValue + (flipHorizontal ? -delta : delta);
			var finalValue = flipVertical ? -interpolatedValue : interpolatedValue;
			return finalValue;
		}

		static long _ClampSinValue(long angle, out bool flipHorizontal, out bool flipVertical)
		{
			// SCALED_LARGE_PI
			// This is (2^29)*PI, where 29 is the largest N such that (2^N)*PI < MaxValue.
			// The idea is that this number contains way more precision than PI_TIMES_2,
			// and (((x % (2^29*PI)) % (2^28*PI)) % ... (2^1*PI) = x % (2 * PI)
			// In practice this gives us an error of about 1,25e-9 in the worst case scenario (Sin(MaxValue))
			// Whereas simply doing x % PI_TIMES_2 is the 2e-3 range.
			var largePI = FPConstInternal.SCALED_LARGE_PI;

			var clamped2Pi = angle;
			for (int i = 0; i < 29; ++i)
				clamped2Pi %= (largePI >> i);

			if (angle < 0)
				clamped2Pi += FPConstInternal.SCALED_TWO_PI;

			// The LUT contains values for 0 - PiOver2; every other value must be obtained by
			// vertical or horizontal mirroring
			flipVertical = clamped2Pi >= FPConstInternal.SCALED_PI;
			// obtain (angle % PI) from (angle % 2PI) - much faster than doing another modulo
			var clampedPi = clamped2Pi;
			while (clampedPi >= FPConstInternal.SCALED_PI)
				clampedPi -= FPConstInternal.SCALED_PI;

			flipHorizontal = clampedPi >= FPConstInternal.SCALED_HALF_PI;
			// obtain (angle % PI_OVER_2) from (angle % PI) - much faster than doing another modulo
			var clampedPiOver2 = clampedPi;
			if (clampedPiOver2 >= FPConstInternal.SCALED_HALF_PI)
				clampedPiOver2 -= FPConstInternal.SCALED_HALF_PI;
			return clampedPiOver2;
		}

		public static FP Cos(FP value)
		{
			var scaledValue = value.scaledValue;
			var scaledAngle = scaledValue + (scaledValue > 0
				                  ? -FPConstInternal.SCALED_PI - FPConstInternal.SCALED_HALF_PI
				                  : FPConstInternal.SCALED_HALF_PI);
			return Sin(scaledAngle);
		}

		public static FP Tan(FP value)
		{
			var clampedScaledPI = value.scaledValue % FPConstInternal.SCALED_PI;
			var flip = false;
			if (clampedScaledPI < 0)
			{
				clampedScaledPI = -clampedScaledPI;
				flip = true;
			}

			if (clampedScaledPI > FPConstInternal.SCALED_HALF_PI)
			{
				flip = !flip;
				clampedScaledPI = FPConstInternal.SCALED_HALF_PI -
				                  (clampedScaledPI - FPConstInternal.SCALED_HALF_PI);
			}

			FP clamped = new FP(clampedScaledPI);

			// Find the two closest values in the LUT and perform linear interpolation
			var rawIndex = FastMul(clamped, LUT_INTERVAL);
			var roundedIndex = Round(rawIndex);
			var indexError = FastSub(rawIndex, roundedIndex);

			FP nearestValue = FPTanLut.TanLut[(int) roundedIndex];
			var secondNearestValue = FPTanLut.TanLut[(int) roundedIndex + Sign(indexError)];

			var delta = FastMul(indexError, FastAbs(FastSub(nearestValue, secondNearestValue))).scaledValue;
			var interpolatedValue = nearestValue.scaledValue + delta;
			var finalValue = flip ? -interpolatedValue : interpolatedValue;
			return finalValue;
		}

		public static FP Asin(FP value)
		{
			return FastSub(HALF_PI, Acos(value));
		}

		public static FP Acos(FP value)
		{
			if (value < -ONE || value > ONE)
				throw new ArgumentOutOfRangeException(nameof(value));

			if (value.scaledValue == 0) return HALF_PI;

			var result = Atan(Sqrt(ONE - value * value) / value);
			return value.scaledValue < 0 ? result + PI : result;
		}

		public static FP Atan(FP z)
		{
			if (z.scaledValue == 0) return ZERO;

			// Force positive values for argument
			// Atan(-z) = -Atan(z).
			var neg = z.scaledValue < 0;
			if (neg)
				z = -z;

			FP two =  2;
			FP three = 3;

			bool invert = z > ONE;
			if (invert) z = ONE / z;

			var result = ONE;
			var term = ONE;

			var zSq = z * z;
			var zSq2 = zSq * two;
			var zSqPlusOne = zSq + ONE;
			var zSq12 = zSqPlusOne * two;
			var dividend = zSq2;
			var divisor = zSqPlusOne * three;

			for (var i = 2; i < 30; ++i)
			{
				term *= dividend / divisor;
				result += term;

				dividend += zSq2;
				divisor += zSq12;

				if (term.scaledValue == 0) break;
			}

			result = result * z / zSqPlusOne;

			if (invert)
				result = HALF_PI - result;

			if (neg)
				result = -result;
			return result;
		}

		public static FP Atan2(FP y, FP x)
		{
			var scaledY = y.scaledValue;
			var scaledX = x.scaledValue;
			if (scaledX == 0)
			{
				if (scaledY > 0)
					return HALF_PI;
				if (scaledY == 0)
					return ZERO;
				return -HALF_PI;
			}

			FP atan;
			var z = y / x;

			// Deal with overflow
			if (ONE +0.28M * z * z == MAX_VALUE)
				return y < ZERO ? -HALF_PI : HALF_PI;

			if (Abs(z) < ONE)
			{
				atan = z / (ONE + 0.28M * z * z);
				if (scaledX < 0)
				{
					if (scaledY < 0)
						return atan - PI;
					return atan + PI;
				}
			}
			else
			{
				atan = HALF_PI - z / (z * z + 0.28M);
				if (scaledY < 0)
					return atan - PI;
			}

			return atan;
		}

		#endregion
	}
}