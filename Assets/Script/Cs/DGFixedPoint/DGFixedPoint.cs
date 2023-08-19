/*************************************************************************************
 * 描    述:  抄自https://github.com/asik/FixedMath.Net，里面还有fix16，fix8的实现
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

public partial struct DGFixedPoint : IEquatable<DGFixedPoint>, IComparable<DGFixedPoint>
{
	//不能修改Null的值
	public static DGFixedPoint Null = new DGFixedPoint(DGFixedPointConstInternal.MAX_VALUE);
	public static readonly decimal Precision = (decimal) new DGFixedPoint(DGFixedPointConstInternal.PRECISION);
	public static readonly DGFixedPoint Epsilon = new DGFixedPoint(DGFixedPointConstInternal.SCALED_EPSILON);
	public static readonly DGFixedPoint MaxValue = new DGFixedPoint(DGFixedPointConstInternal.MAX_VALUE);
	public static readonly DGFixedPoint MinValue = new DGFixedPoint(DGFixedPointConstInternal.MIN_VALUE);
	public static readonly DGFixedPoint One = new DGFixedPoint(DGFixedPointConstInternal.SCALED_ONE);
	public static readonly DGFixedPoint NegativeOne = new DGFixedPoint(DGFixedPointConstInternal.SCALED_NEGATIVE_ONE);
	public static readonly DGFixedPoint Half = new DGFixedPoint((long)DGFixedPointConstInternal.SCALED_HALF_ONE);
	public static readonly DGFixedPoint Quarter = new DGFixedPoint((long)DGFixedPointConstInternal.SCALED_HALF_QUARTER);
	public static readonly DGFixedPoint Zero = new DGFixedPoint();
	public static readonly DGFixedPoint Pi = new DGFixedPoint(DGFixedPointConstInternal.SCALED_PI);
	public static readonly DGFixedPoint HalfPi = new DGFixedPoint(DGFixedPointConstInternal.SCALED_HALF_PI);
	public static readonly DGFixedPoint TwoPi = new DGFixedPoint(DGFixedPointConstInternal.SCALED_TWO_PI);
	public static readonly DGFixedPoint FourPiDiv3 = Pi*(DGFixedPoint)4/(DGFixedPoint)3;
	public static readonly DGFixedPoint QuarterPi = new DGFixedPoint(DGFixedPointConstInternal.SCALED_QUARTER_PI);
	public static readonly DGFixedPoint Ln2 = new DGFixedPoint(DGFixedPointConstInternal.SCALED_LN2);//以E为底的2的对数
	public static readonly DGFixedPoint Log2Max = new DGFixedPoint(DGFixedPointConstInternal.SCALED_LOG2MAX);
	public static readonly DGFixedPoint Log2Min = new DGFixedPoint(DGFixedPointConstInternal.SCALED_LOG2MIN);
	public static readonly DGFixedPoint E = new DGFixedPoint(DGFixedPointConstInternal.SCALED_E);
	public static readonly DGFixedPoint Deg2Rad = new DGFixedPoint(DGFixedPointConstInternal.SCALED_DEG2RAD);
	public static readonly DGFixedPoint Rad2Deg = new DGFixedPoint(DGFixedPointConstInternal.SCALED_RAD2DEG);

	static readonly DGFixedPoint LutInterval = (DGFixedPoint) (DGFixedPointConstInternal.LUT_SIZE - 1) / HalfPi;

	public static readonly Dictionary<int, DGFixedPoint> Cache = new Dictionary<int, DGFixedPoint>
	{
		{0, Zero},
		{1, One},
		{2, new DGFixedPoint(2)},
		{10, new DGFixedPoint(10)},
		{360, new DGFixedPoint(360)},
		{180, new DGFixedPoint(180)},

		{-1, new DGFixedPoint(-1)},
	};

	public long scaledValue { get; }

	DGFixedPoint(long rawValue)
	{
		scaledValue = rawValue;
	}

	public DGFixedPoint(int value)
	{
		scaledValue = value * DGFixedPointConstInternal.SCALED_ONE;
	}

	public static DGFixedPoint CreateByScaledValue(long scaledValue)
	{
		return new DGFixedPoint(scaledValue);
	}

	/*************************************************************************************
	* 模块描述:Equals ToString
	*************************************************************************************/
	public override bool Equals(object obj)
	{
		return obj is DGFixedPoint fixedPoint && fixedPoint.scaledValue == scaledValue;
	}

	public bool Equals(DGFixedPoint other)
	{
		return scaledValue == other.scaledValue;
	}

	public override int GetHashCode()
	{
		return scaledValue.GetHashCode();
	}

	public int CompareTo(DGFixedPoint other)
	{
		return scaledValue.CompareTo(other.scaledValue);
	}

	public override string ToString()
	{
		return ((decimal) this).ToString("0.##########");
	}
	/*************************************************************************************
	* 模块描述:转换
	*************************************************************************************/
	public static explicit operator DGFixedPoint(long value)
	{
		return new DGFixedPoint(value << DGFixedPointConstInternal.MOVE_BIT_COUNT);
	}

	public static explicit operator long(DGFixedPoint value)
	{
		return value.scaledValue >> DGFixedPointConstInternal.MOVE_BIT_COUNT;
	}

	public static explicit operator DGFixedPoint(float value)
	{
		return new DGFixedPoint((long)(value * DGFixedPointConstInternal.SCALED_ONE));
	}

	public static explicit operator float(DGFixedPoint value)
	{
		return (float)value.scaledValue / DGFixedPointConstInternal.SCALED_ONE;
	}

	public static explicit operator DGFixedPoint(double value)
	{
		return new DGFixedPoint((long)(value * DGFixedPointConstInternal.SCALED_ONE));
	}

	public static explicit operator double(DGFixedPoint value)
	{
		return (double)value.scaledValue / DGFixedPointConstInternal.SCALED_ONE;
	}

	public static explicit operator DGFixedPoint(decimal value)
	{
		return new DGFixedPoint((long)(value * DGFixedPointConstInternal.SCALED_ONE));
	}

	public static explicit operator decimal(DGFixedPoint value)
	{
		return (decimal)value.scaledValue / DGFixedPointConstInternal.SCALED_ONE;
	}
	/*************************************************************************************
	* 模块描述:关系运算符
	*************************************************************************************/
	public static bool operator ==(DGFixedPoint value1, DGFixedPoint value2)
	{
		return value1.scaledValue == value2.scaledValue;
	}

	public static bool operator !=(DGFixedPoint value1, DGFixedPoint value2)
	{
		return value1.scaledValue != value2.scaledValue;
	}

	public static bool operator >(DGFixedPoint value1, DGFixedPoint value2)
	{
		return value1.scaledValue > value2.scaledValue;
	}

	public static bool operator <(DGFixedPoint value1, DGFixedPoint value2)
	{
		return value1.scaledValue < value2.scaledValue;
	}

	public static bool operator >=(DGFixedPoint value1, DGFixedPoint value2)
	{
		return value1.scaledValue >= value2.scaledValue;
	}

	public static bool operator <=(DGFixedPoint value1, DGFixedPoint value2)
	{
		return value1.scaledValue <= value2.scaledValue;
	}
	/*************************************************************************************
	* 模块描述:算术运算符
	*************************************************************************************/
	public static DGFixedPoint operator +(DGFixedPoint value1, DGFixedPoint value2)
	{
		var scaledValue1 = value1.scaledValue;
		var scaledValue2 = value2.scaledValue;
		var scaledValueSum = scaledValue1 + scaledValue2;
		//如果v1和v2相同sign，但sum和x的sign不同，则sum的值溢出
		if (((~(scaledValue1 ^ scaledValue2) & (scaledValue1 ^ scaledValueSum)) &
		     DGFixedPointConstInternal.MIN_VALUE) != 0)
			scaledValueSum = scaledValue1 > 0
				? DGFixedPointConstInternal.MAX_VALUE
				: DGFixedPointConstInternal.MIN_VALUE;
		return new DGFixedPoint(scaledValueSum);
	}

	public static DGFixedPoint operator -(DGFixedPoint value1, DGFixedPoint value2)
	{
		var scaledValue1 = value1.scaledValue;
		var scaleValue2 = value2.scaledValue;
		var scaledDiff = scaledValue1 - scaleValue2;
		//如果v1和v2不同sign，但diff和x的sign不同，则diff的值溢出
		if ((((scaledValue1 ^ scaleValue2) & (scaledValue1 ^ scaledDiff)) & DGFixedPointConstInternal.MIN_VALUE) != 0)
			scaledDiff = scaledValue1 < 0 ? DGFixedPointConstInternal.MIN_VALUE : DGFixedPointConstInternal.MAX_VALUE;
		return new DGFixedPoint(scaledDiff);
	}

	public static DGFixedPoint operator *(DGFixedPoint value1, DGFixedPoint value2)
	{
		var scaledValue1 = value1.scaledValue;
		var scaledValue2 = value2.scaledValue;

		var low1 = (ulong) (scaledValue1 & DGFixedPointConstInternal.SCALED_FRACTIONAL_PART_MASK);
		var high1 = scaledValue1 >> DGFixedPointConstInternal.MOVE_BIT_COUNT;
		var low2 = (ulong) (scaledValue2 & DGFixedPointConstInternal.SCALED_FRACTIONAL_PART_MASK);
		var high2 = scaledValue2 >> DGFixedPointConstInternal.MOVE_BIT_COUNT;

		var low1Low2 = low1 * low2;
		var low1High2 = (long) low1 * high2;
		var high1Low2 = high1 * (long) low2;
		var high1High2 = high1 * high2;

		var scaledLowResult = low1Low2 >> DGFixedPointConstInternal.MOVE_BIT_COUNT;
		var scaledMidResult1 = low1High2;
		var scaledMidResult2 = high1Low2;
		var scaledHighResult = high1High2 << DGFixedPointConstInternal.MOVE_BIT_COUNT;

		bool overflow = false;
		var scaledSum = _AddOverflowHelper((long) scaledLowResult, scaledMidResult1, ref overflow);
		scaledSum = _AddOverflowHelper(scaledSum, scaledMidResult2, ref overflow);
		scaledSum = _AddOverflowHelper(scaledSum, scaledHighResult, ref overflow);

		bool opSignsEqual = ((scaledValue1 ^ scaledValue2) & DGFixedPointConstInternal.MIN_VALUE) == 0;

		// if signs of operands are equal and sign of result is negative,
		// then multiplication overflowed positively
		// the reverse is also true
		if (opSignsEqual)
		{
			if (scaledSum < 0 || (overflow && scaledValue1 > 0))
				return MaxValue;
		}
		else
		{
			if (scaledSum > 0)
				return MinValue;
		}

		// if the top 32 bits of hihi (unused in the result) are neither all 0s or 1s,
		// then this means the result overflowed.
		var topCarry = high1High2 >> DGFixedPointConstInternal.MOVE_BIT_COUNT;
		if (topCarry != DGFixedPointConstInternal.INTEGRAL_PART_ALL_ZERO &&
		    topCarry != DGFixedPointConstInternal.INTEGRAL_PART_ALL_ONE /*&& xl != -17 && yl != -17*/)
			return opSignsEqual ? MaxValue : MinValue;

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

			if (scaledSum > negOp && negOp < -DGFixedPointConstInternal.SCALED_ONE &&
			    posOp > DGFixedPointConstInternal.SCALED_ONE)
				return MinValue;
		}

		return new DGFixedPoint(scaledSum);
	}

	public static DGFixedPoint operator /(DGFixedPoint value1, DGFixedPoint value2)
	{
		var scaledValue1 = value1.scaledValue;
		var scaledValue2 = value2.scaledValue;

		if (scaledValue2 == 0)
			throw new DivideByZeroException();

		var remainder = (ulong) (scaledValue1 >= 0 ? scaledValue1 : -scaledValue1);
		var divider = (ulong) (scaledValue2 >= 0 ? scaledValue2 : -scaledValue2);
		var quotient = 0UL;
		var bitPos = DGFixedPointConstInternal.NUM_BIT_COUNT / 2 + 1;


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
			if ((div & ~(DGFixedPointConstInternal.ALL_ONE >> bitPos)) != 0)
				return ((scaledValue1 ^ scaledValue2) & DGFixedPointConstInternal.MIN_VALUE) == 0 ? MaxValue : MinValue;

			remainder <<= 1;
			--bitPos;
		}

		// rounding
		++quotient;
		var result = (long) (quotient >> 1);
		if (((scaledValue1 ^ scaledValue2) & DGFixedPointConstInternal.MIN_VALUE) != 0)
			result = -result;

		return new DGFixedPoint(result);
	}

	public static DGFixedPoint operator %(DGFixedPoint value1, DGFixedPoint value2)
	{
		return new DGFixedPoint(
			value1.scaledValue == DGFixedPointConstInternal.MIN_VALUE & value2.scaledValue == -1
				? 0
				: value1.scaledValue % value2.scaledValue);
	}

	public static DGFixedPoint operator -(DGFixedPoint value)
	{
		return value.scaledValue == DGFixedPointConstInternal.MIN_VALUE
			? MaxValue
			: new DGFixedPoint(-value.scaledValue);
	}
	/*************************************************************************************
	* 模块描述:Fast 运算(但不安全)
	*************************************************************************************/
	public static DGFixedPoint FastAdd(DGFixedPoint value1, DGFixedPoint value2)
	{
		return new DGFixedPoint(value1.scaledValue + value2.scaledValue);
	}

	public static DGFixedPoint FastSub(DGFixedPoint value1, DGFixedPoint value2)
	{
		long x = 0;
		long y = 1;
		long sum = x + y;
		var t = (x ^ y ^ sum);
		return new DGFixedPoint(value1.scaledValue - value2.scaledValue);
	}

	public static DGFixedPoint FastMul(DGFixedPoint value1, DGFixedPoint value2)
	{
		var scaleValue1 = value1.scaledValue;
		var scaleValue2 = value2.scaledValue;

		var low1 = (ulong) (scaleValue1 & DGFixedPointConstInternal.SCALED_FRACTIONAL_PART_MASK);
		var high1 = scaleValue1 >> DGFixedPointConstInternal.MOVE_BIT_COUNT;
		var low2 = (ulong) (scaleValue2 & DGFixedPointConstInternal.SCALED_FRACTIONAL_PART_MASK);
		var high2 = scaleValue2 >> DGFixedPointConstInternal.MOVE_BIT_COUNT;

		var low1Low2 = low1 * low2;
		var low1High2 = (long) low1 * high2;
		var high1Low2 = high1 * (long) low2;
		var high1High2 = high1 * high2;

		var scaleLowResult = low1Low2 >> DGFixedPointConstInternal.MOVE_BIT_COUNT;
		var scaledMidResult1 = low1High2;
		var scaledMidResult2 = high1Low2;
		var scaledHighResult = high1High2 << DGFixedPointConstInternal.MOVE_BIT_COUNT;

		var scaledSum = (long) scaleLowResult + scaledMidResult1 + scaledMidResult2 + scaledHighResult;
		return new DGFixedPoint(scaledSum);
	}

	public static DGFixedPoint FastMod(DGFixedPoint value1, DGFixedPoint value2)
	{
		return new DGFixedPoint(value1.scaledValue % value2.scaledValue);
	}

	public static DGFixedPoint FastAbs(DGFixedPoint value)
	{
		// branchless implementation, see http://www.strchr.com/optimized_abs_function
		var mask = value.scaledValue >> (DGFixedPointConstInternal.NUM_BIT_COUNT - 1);
		return new DGFixedPoint((value.scaledValue + mask) ^ mask);
	}

	public static DGFixedPoint FastSin(DGFixedPoint value)
	{
		var clampedScaledSinValue = _ClampSinValue(value.scaledValue, out bool flipHorizontal, out bool flipVertical);

		// Here we use the fact that the SinLut table has a number of entries
		// equal to (PI_OVER_2 >> 15) to use the angle to index directly into it
		var rawIndex = (uint) (clampedScaledSinValue >> 15);
		if (rawIndex >= DGFixedPointConstInternal.LUT_SIZE)
			rawIndex = DGFixedPointConstInternal.LUT_SIZE - 1;
		var nearestValue =
			DGFixedPointSinLut.SinLut[
				flipHorizontal ? DGFixedPointSinLut.SinLut.Length - 1 - (int) rawIndex : (int) rawIndex];
		return new DGFixedPoint(flipVertical ? -nearestValue : nearestValue);
	}

	public static DGFixedPoint FastCos(DGFixedPoint value)
	{
		var scaledValue = value.scaledValue;
		var scaledAngle = scaledValue + (scaledValue > 0
			                  ? -DGFixedPointConstInternal.SCALED_PI - DGFixedPointConstInternal.SCALED_HALF_PI
			                  : DGFixedPointConstInternal.SCALED_HALF_PI);
		return FastSin(new DGFixedPoint(scaledAngle));
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	public static int Sign(DGFixedPoint value)
	{
		return
			value.scaledValue < 0 ? -1 :
			value.scaledValue > 0 ? 1 :
			0;
	}

	public static DGFixedPoint CopySign(DGFixedPoint x, DGFixedPoint y)
	{
		if ((x.scaledValue >= 0 && y.scaledValue >= 0) 
		    || (x.scaledValue <= 0 && y.scaledValue <= 0))
			return x;
		return -x;
	}

	

	public static DGFixedPoint Abs(DGFixedPoint value)
	{
		if (value.scaledValue == DGFixedPointConstInternal.MIN_VALUE)
			return MaxValue;

		// branchless implementation, see http://www.strchr.com/optimized_abs_function
		var mask = value.scaledValue >> (DGFixedPointConstInternal.NUM_BIT_COUNT - 1);
		return new DGFixedPoint((value.scaledValue + mask) ^ mask);
	}

	public static DGFixedPoint Truncate(DGFixedPoint value)
	{
		var scaledValue = value.scaledValue;
		if (scaledValue < 0)
			return CreateByScaledValue(-(-scaledValue >> DGFixedPointConstInternal.MOVE_BIT_COUNT << DGFixedPointConstInternal.MOVE_BIT_COUNT));
		return CreateByScaledValue(scaledValue >> DGFixedPointConstInternal.MOVE_BIT_COUNT << DGFixedPointConstInternal.MOVE_BIT_COUNT);
	}

	public static DGFixedPoint Floor(DGFixedPoint value)
	{
		return new DGFixedPoint(
			(long) ((ulong) value.scaledValue & DGFixedPointConstInternal.SCALED_INTEGRAL_PART_MASK));
	}

	public static DGFixedPoint Ceiling(DGFixedPoint value)
	{
		var hasFractionalPart = (value.scaledValue & DGFixedPointConstInternal.SCALED_FRACTIONAL_PART_MASK) != 0;
		return hasFractionalPart ? Floor(value) + One : value;
	}

	public static DGFixedPoint Round(DGFixedPoint value)
	{
		var fractionalPart = value.scaledValue & DGFixedPointConstInternal.SCALED_FRACTIONAL_PART_MASK;
		var integralPart = Floor(value);
		if (fractionalPart < DGFixedPointConstInternal.SCALED_ROUND_FRACTIONAL_PART_MASK)
			return integralPart;
		if (fractionalPart > DGFixedPointConstInternal.SCALED_ROUND_FRACTIONAL_PART_MASK)
			return integralPart + One;
		// 小数部分等于0.5时， System.Math.Round()的处理方式是四舍五入到最近的偶整数
		return (integralPart.scaledValue & DGFixedPointConstInternal.SCALED_ONE) == 0
			? integralPart
			: integralPart + One;
	}

	static long _AddOverflowHelper(long scaledValue1, long scaledValue2, ref bool overflow)
	{
		var sum = scaledValue1 + scaledValue2;
		// x + y overflows if sign(x) ^ sign(y) != sign(sum)
		overflow |= ((scaledValue1 ^ scaledValue2 ^ sum) & DGFixedPointConstInternal.MIN_VALUE) != 0;
		return sum;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static int _CountLeadingZeroes(ulong x)
	{
		int result = 0;
		while ((x & DGFixedPointConstInternal.COUNT_LEADING_ZERO_ROUGH_Mask) == 0)
		{
			result += 4;
			x <<= 4;
		}

		while ((x & DGFixedPointConstInternal.COUNT_LEADING_ZERO_MASK) == 0)
		{
			result += 1;
			x <<= 1;
		}

		return result;
	}

	internal static DGFixedPoint Pow2(DGFixedPoint power)
	{
		if (power.scaledValue == 0)
			return One;

		// Avoid negative arguments by exploiting that exp(-x) = 1/exp(x).
		bool neg = power.scaledValue < 0;
		if (neg)
			power = -power;

		if (power == One)
			return neg ? One / (DGFixedPoint) 2 : (DGFixedPoint) 2;
		if (power >= Log2Max)
			return neg ? One / MaxValue : MaxValue;
		if (power <= Log2Min)
			return neg ? MaxValue : Zero;

		/* The algorithm is based on the power series for exp(x):
		 * http://en.wikipedia.org/wiki/Exponential_function#Formal_definition
		 * 
		 * From term n, we get term n+1 by multiplying with x/n.
		 * When the sum term drops to zero, we can stop summing.
		 */

		int integerPart = (int) Floor(power);
		// Take fractional part of exponent
		power = new DGFixedPoint(power.scaledValue & DGFixedPointConstInternal.SCALED_FRACTIONAL_PART_MASK);

		var result = One;
		var term = One;
		int i = 1;
		while (term.scaledValue != 0)
		{
			term = FastMul(FastMul(power, term), Ln2) / (DGFixedPoint) i;
			result += term;
			i++;
		}

		result = CreateByScaledValue(result.scaledValue << integerPart);
		if (neg)
			result = One / result;

		return result;
	}

	internal static DGFixedPoint Log2(DGFixedPoint value)
	{
		if (value.scaledValue <= 0)
			throw new ArgumentOutOfRangeException("Non-positive value passed to Ln", "x");

		// This implementation is based on Clay. S. Turner's fast binary logarithm
		// algorithm (C. S. Turner,  "A Fast Binary Logarithm Algorithm", IEEE Signal
		//     Processing Mag., pp. 124,140, Sep. 2010.)

		long b = 1U << (DGFixedPointConstInternal.MOVE_BIT_COUNT - 1);
		long y = 0;

		long scaledValue = value.scaledValue;
		while (scaledValue < DGFixedPointConstInternal.SCALED_ONE)
		{
			scaledValue <<= 1;
			y -= DGFixedPointConstInternal.SCALED_ONE;
		}

		while (scaledValue >= (DGFixedPointConstInternal.SCALED_ONE << 1))
		{
			scaledValue >>= 1;
			y += DGFixedPointConstInternal.SCALED_ONE;
		}

		var z = new DGFixedPoint(scaledValue);

		for (int i = 0; i < DGFixedPointConstInternal.MOVE_BIT_COUNT; i++)
		{
			z = FastMul(z, z);
			if (z.scaledValue >= (DGFixedPointConstInternal.SCALED_ONE << 1))
			{
				z = new DGFixedPoint(z.scaledValue >> 1);
				y += b;
			}

			b >>= 1;
		}

		return new DGFixedPoint(y);
	}

	public static DGFixedPoint Ln(DGFixedPoint x)
	{
		return FastMul(Log2(x), Ln2);
	}

	public static DGFixedPoint Pow(DGFixedPoint num, DGFixedPoint power)
	{
		if (num == One)
			return One;
		if (power.scaledValue == 0)
			return One;
		if (num.scaledValue == 0)
		{
			if (power.scaledValue < 0)
				throw new DivideByZeroException();
			return Zero;
		}

		DGFixedPoint log2 = Log2(num);
		return Pow2(power * log2);
	}

	public static DGFixedPoint Sqrt(DGFixedPoint value)
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
		var bit = 1UL << (DGFixedPointConstInternal.NUM_BIT_COUNT - 2);

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
				if (num > (1UL << (DGFixedPointConstInternal.NUM_BIT_COUNT / 2)) - 1)
				{
					// The remainder 'num' is too large to be shifted left
					// by 32, so we have to add 1 to result manually and
					// adjust 'num' accordingly.
					// num = a - (result + 0.5)^2
					//       = num + result^2 - (result + 0.5)^2
					//       = num - result - 0.5
					num -= result;
					num = (num << (DGFixedPointConstInternal.NUM_BIT_COUNT / 2)) -
					      DGFixedPointConstInternal.SCALED_HALF_ONE;
					result = (result << (DGFixedPointConstInternal.NUM_BIT_COUNT / 2)) +
					         DGFixedPointConstInternal.SCALED_HALF_ONE;
				}
				else
				{
					num <<= (DGFixedPointConstInternal.NUM_BIT_COUNT / 2);
					result <<= (DGFixedPointConstInternal.NUM_BIT_COUNT / 2);
				}

				bit = 1UL << (DGFixedPointConstInternal.NUM_BIT_COUNT / 2 - 2);
			}
		}

		// Finally, if next bit would have been 1, round the result upwards.
		if (num > result)
			++result;
		return new DGFixedPoint((long) result);
	}

	/// <summary>
	/// Returns the Sine of x.
	/// The relative error is less than 1E-10 for x in [-2PI, 2PI], and less than 1E-7 in the worst case.
	/// </summary>
	public static DGFixedPoint Sin(DGFixedPoint value)
	{
		var clampedScaledSinValue = _ClampSinValue(value.scaledValue, out var flipHorizontal, out var flipVertical);
		var clamped = new DGFixedPoint(clampedScaledSinValue);

		// Find the two closest values in the LUT and perform linear interpolation
		// This is what kills the performance of this function on x86 - x64 is fine though
		var rawIndex = FastMul(clamped, LutInterval);
		var roundedIndex = Round(rawIndex);
		var indexError = FastSub(rawIndex, roundedIndex);

		var nearestValue = new DGFixedPoint(DGFixedPointSinLut.SinLut[
			flipHorizontal ? DGFixedPointSinLut.SinLut.Length - 1 - (int) roundedIndex : (int) roundedIndex]);
		var secondNearestValue = new DGFixedPoint(DGFixedPointSinLut.SinLut[
			flipHorizontal
				? DGFixedPointSinLut.SinLut.Length - 1 - (int) roundedIndex - Sign(indexError)
				: (int) roundedIndex + Sign(indexError)]);

		var delta = FastMul(indexError, FastAbs(FastSub(nearestValue, secondNearestValue))).scaledValue;
		var interpolatedValue = nearestValue.scaledValue + (flipHorizontal ? -delta : delta);
		var finalValue = flipVertical ? -interpolatedValue : interpolatedValue;
		return new DGFixedPoint(finalValue);
	}

	static long _ClampSinValue(long angle, out bool flipHorizontal, out bool flipVertical)
	{
		// SCALED_LARGE_PI
		// This is (2^29)*PI, where 29 is the largest N such that (2^N)*PI < MaxValue.
		// The idea is that this number contains way more precision than PI_TIMES_2,
		// and (((x % (2^29*PI)) % (2^28*PI)) % ... (2^1*PI) = x % (2 * PI)
		// In practice this gives us an error of about 1,25e-9 in the worst case scenario (Sin(MaxValue))
		// Whereas simply doing x % PI_TIMES_2 is the 2e-3 range.
		var largePI = DGFixedPointConstInternal.SCALED_LARGE_PI;

		var clamped2Pi = angle;
		for (int i = 0; i < 29; ++i)
			clamped2Pi %= (largePI >> i);

		if (angle < 0)
			clamped2Pi += DGFixedPointConstInternal.SCALED_TWO_PI;

		// The LUT contains values for 0 - PiOver2; every other value must be obtained by
		// vertical or horizontal mirroring
		flipVertical = clamped2Pi >= DGFixedPointConstInternal.SCALED_PI;
		// obtain (angle % PI) from (angle % 2PI) - much faster than doing another modulo
		var clampedPi = clamped2Pi;
		while (clampedPi >= DGFixedPointConstInternal.SCALED_PI)
			clampedPi -= DGFixedPointConstInternal.SCALED_PI;

		flipHorizontal = clampedPi >= DGFixedPointConstInternal.SCALED_HALF_PI;
		// obtain (angle % PI_OVER_2) from (angle % PI) - much faster than doing another modulo
		var clampedPiOver2 = clampedPi;
		if (clampedPiOver2 >= DGFixedPointConstInternal.SCALED_HALF_PI)
			clampedPiOver2 -= DGFixedPointConstInternal.SCALED_HALF_PI;
		return clampedPiOver2;
	}

	public static DGFixedPoint Cos(DGFixedPoint value)
	{
		var scaledValue = value.scaledValue;
		var scaledAngle = scaledValue + (scaledValue > 0
			                  ? -DGFixedPointConstInternal.SCALED_PI - DGFixedPointConstInternal.SCALED_HALF_PI
			                  : DGFixedPointConstInternal.SCALED_HALF_PI);
		return Sin(new DGFixedPoint(scaledAngle));
	}

	public static DGFixedPoint Tan(DGFixedPoint value)
	{
		var clampedScaledPI = value.scaledValue % DGFixedPointConstInternal.SCALED_PI;
		var flip = false;
		if (clampedScaledPI < 0)
		{
			clampedScaledPI = -clampedScaledPI;
			flip = true;
		}

		if (clampedScaledPI > DGFixedPointConstInternal.SCALED_HALF_PI)
		{
			flip = !flip;
			clampedScaledPI = DGFixedPointConstInternal.SCALED_HALF_PI -
			                  (clampedScaledPI - DGFixedPointConstInternal.SCALED_HALF_PI);
		}

		var clamped = new DGFixedPoint(clampedScaledPI);

		// Find the two closest values in the LUT and perform linear interpolation
		var rawIndex = FastMul(clamped, LutInterval);
		var roundedIndex = Round(rawIndex);
		var indexError = FastSub(rawIndex, roundedIndex);

		var nearestValue = new DGFixedPoint(DGFixedPointTanLut.TanLut[(int) roundedIndex]);
		var secondNearestValue = new DGFixedPoint(DGFixedPointTanLut.TanLut[(int) roundedIndex + Sign(indexError)]);

		var delta = FastMul(indexError, FastAbs(FastSub(nearestValue, secondNearestValue))).scaledValue;
		var interpolatedValue = nearestValue.scaledValue + delta;
		var finalValue = flip ? -interpolatedValue : interpolatedValue;
		return new DGFixedPoint(finalValue);
	}

	public static DGFixedPoint Asin(DGFixedPoint value)
	{
		return FastSub(HalfPi, Acos(value));
	}

	public static DGFixedPoint Acos(DGFixedPoint value)
	{
				if (value < -One || value > One)
					throw new ArgumentOutOfRangeException(nameof(value));
		
				if (value.scaledValue == 0) return HalfPi;
		
				var result = Atan(Sqrt(One - value * value) / value);
				return value.scaledValue < 0 ? result + Pi : result;
	}

	public static DGFixedPoint Atan(DGFixedPoint z)
	{
		if (z.scaledValue == 0) return Zero;

		// Force positive values for argument
		// Atan(-z) = -Atan(z).
		var neg = z.scaledValue < 0;
		if (neg)
			z = -z;

		var two = (DGFixedPoint) 2;
		var three = (DGFixedPoint) 3;

		bool invert = z > One;
		if (invert) z = One / z;

		var result = One;
		var term = One;

		var zSq = z * z;
		var zSq2 = zSq * two;
		var zSqPlusOne = zSq + One;
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
			result = HalfPi - result;

		if (neg)
			result = -result;
		return result;
	}

	public static DGFixedPoint Atan2(DGFixedPoint y, DGFixedPoint x)
	{
		var scaledY = y.scaledValue;
		var scaledX = x.scaledValue;
		if (scaledX == 0)
		{
			if (scaledY > 0)
				return HalfPi;
			if (scaledY == 0)
				return Zero;
			return -HalfPi;
		}

		DGFixedPoint atan;
		var z = y / x;

		// Deal with overflow
		if (One + (DGFixedPoint) 0.28M * z * z == MaxValue)
			return y < Zero ? -HalfPi : HalfPi;

		if (Abs(z) < One)
		{
			atan = z / (One + (DGFixedPoint) 0.28M * z * z);
			if (scaledX < 0)
			{
				if (scaledY < 0)
					return atan - Pi;
				return atan + Pi;
			}
		}
		else
		{
			atan = HalfPi - z / (z * z + (DGFixedPoint) 0.28M);
			if (scaledY < 0)
				return atan - Pi;
		}

		return atan;
	}
}