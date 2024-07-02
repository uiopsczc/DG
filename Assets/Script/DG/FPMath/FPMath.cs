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



#if UNITY_STANDALONE
using UnityEngine;
#endif

namespace DG
{
	public static partial class FPMath
	{
		public static readonly FP ONE = FP.ONE;
		public static readonly FP E = FP.E;
		public static readonly FP PI = FP.PI;
		public static readonly FP TWO_PI = FP.TWO_PI;
		public static readonly FP HALF_PI = FP.HALF_PI;
		public static readonly FP DEG2RAD = FP.DEG2RAD;
		public static readonly FP Rad2Deg = FP.RAD2DEG;
		public static readonly FP EPSILION = FP.EPSILION;
		public static readonly FP HALF = FP.HALF;
		public static readonly FP QUARTER = FP.QUARTER;
		public static readonly FP HALF_SQRT2 = 0.7071067811865475244008443621048490f;
		public static readonly FP MAX_VALUE = FP.MAX_VALUE;
		public static readonly FP MIN_VALUE = FP.MIN_VALUE;


		public static bool IsApproximatelyZero(FP value)
		{
			return Abs(value) < EPSILION;
		}


		public static FP Abs(FP value)
		{
			return FP.Abs(value);
		}

		public static FP Min(FP value1, FP value2)
		{
			return value1.scaledValue < value2.scaledValue ? value1 : value2;
		}

		public static FP Min(FP[] values)
		{
			int length = values.Length;
			if (length == 0)
				return FP.ZERO;
			var minValue = values[0];
			for (int index = 1; index < length; ++index)
			{
				var value = values[index];
				if (value < minValue)
					minValue = value;
			}

			return minValue;
		}

		public static FP Max(FP value1, FP value2)
		{
			return value1.scaledValue > value2.scaledValue ? value1 : value2;
		}

		public static FP Max(FP[] values)
		{
			int length = values.Length;
			if (length == 0)
				return FP.ZERO;
			var maxValue = values[0];
			for (int index = 1; index < length; ++index)
			{
				var value = values[index];
				if (value > maxValue)
					maxValue = value;
			}

			return maxValue;
		}

		public static FP Sqrt(FP value)
		{
			return FP.Sqrt(value);
		}

		public static FP Pow(FP value, FP power)
		{
			return FP.Pow(value, power);
		}

		public static FP Exp(FP power)
		{
			return Pow(E, power);
		}

		public static FP Ceiling(FP value)
		{
			return FP.Ceiling(value);
		}

		public static FP Floor(FP value)
		{
			return FP.Floor(value);
		}

		public static FP Round(FP value)
		{
			return FP.Round(value);
		}

		public static FP Truncate(FP value)
		{
			return FP.Truncate(value);
		}

		public static int Sign(FP value)
		{
			return FP.Sign(value);
		}

		public static FP CopySign(FP x, FP y)
		{
			return FP.CopySign(x, y);
		}

		public static FP Clamp(FP value, FP min, FP max)
		{
			if (value < min)
				return min;
			if (value > max)
				return max;
			return value;
		}

		public static FP Clamp01(FP value)
		{
			return Clamp(value, FP.ZERO, FP.ONE);
		}

		public static FP Lerp(FP a, FP b, FP t)
		{
			return a + (b - a) * Clamp01(t);
		}

		public static FP LerpUnclamped(FP a, FP b, FP t)
		{
			return a + (b - a) * t;
		}

		public static FP LerpAngle(FP a, FP b, FP t)
		{
			FP num = Repeat(b - a, FP.CACHE[360]);
			if (num > FP.CACHE[180])
				num -= FP.CACHE[360];
			return a + num * Clamp01(t);
		}

		public static FP MoveTowards(FP current, FP target, FP maxDelta)
		{
			if (Abs(target - current) <= maxDelta)
				return target;
			return current + Sign(target - current) * maxDelta;
		}

		public static FP MoveTowardsAngle(FP current, FP target, FP maxDelta)
		{
			FP num = DeltaAngle(current, target);
			if (-maxDelta < num && num < maxDelta)
				return target;
			target = current + num;
			return MoveTowards(current, target, maxDelta);
		}

		public static FP SmoothStep(FP from, FP to, FP t)
		{
			t = Clamp01(t);
			t = -2 * t * t * t + 3 * t * t;
			return to * t + from * (1 - t);
		}

		public static FP Gamma(FP value, FP absmax, FP gamma)
		{
			bool negative = value < 0;
			FP absval = Abs(value);
			if (absval > absmax)
				return negative ? -absval : absval;
			FP result = Pow(absval / absmax, gamma) * absmax;
			return negative ? -result : result;
		}

#if UNITY_STANDALONE
		public static FP SmoothDamp(FP current, FP target, ref FP currentVelocity,
			FP smoothTime, FP maxSpeed)
		{
			FP deltaTime = Time.deltaTime;
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}
#endif

		public static FP SmoothDamp(FP current, FP target, ref FP currentVelocity,
			FP smoothTime, FP maxSpeed, FP deltaTime)
		{
			// Based on Game Programming Gems 4 Chapter 1.10
			smoothTime = Max(0.0001F, smoothTime);
			FP omega = 2F / smoothTime;

			FP x = omega * deltaTime;
			FP exp = 1F / (1F + x + 0.48F * x * x + 0.235F * x * x * x);
			FP change = current - target;
			FP originalTo = target;

			// Clamp maximum speed
			FP maxChange = maxSpeed * smoothTime;
			change = Clamp(change, -maxChange, maxChange);
			target = current - change;

			FP temp = (currentVelocity + omega * change) * deltaTime;
			currentVelocity = (currentVelocity - omega * temp) * exp;
			FP output = target + (change + temp) * exp;

			// Prevent overshooting
			if (originalTo - current > 0.0F == output > originalTo)
			{
				output = originalTo;
				currentVelocity = (output - originalTo) / deltaTime;
			}

			return output;
		}
#if UNITY_STANDALONE
		public static FP SmoothDampAngle(FP current, FP target,
			ref FP currentVelocity, FP smoothTime, FP maxSpeed)
		{
			FP deltaTime = Time.deltaTime;
			return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static FP SmoothDampAngle(FP current, FP target,
			ref FP currentVelocity, FP smoothTime)
		{
			FP deltaTime = Time.deltaTime;
			FP maxSpeed = FP.MAX_VALUE;
			return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}
#endif

		public static FP SmoothDampAngle(FP current, FP target,
			ref FP currentVelocity, FP smoothTime, FP maxSpeed, FP deltaTime)
		{
			target = current + DeltaAngle(current, target);
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static FP Repeat(FP t, FP length)
		{
			return Clamp(t - Floor(t / length) * length, FP.ZERO, length);
		}

		public static FP PingPong(FP t, FP length)
		{
			t = Repeat(t, 2 * length);
			return length - Abs(t - length);
		}

		public static FP InverseLerp(FP a, FP b, FP value)
		{
			if (a != b)
				return Clamp01((value - a) / (b - a));
			return FP.ZERO;
		}

		public static FP DeltaAngle(FP current, FP target)
		{
			FP num = Repeat(target - current, FP.CACHE[360]);
			if (num > FP.CACHE[180])
				num -= FP.CACHE[360];
			return num;
		}

		public static FP Asin(FP value)
		{
			return FP.Asin(value);
		}

		/// <summary>
		/// 反余弦值（即求指定余弦值对应的弧度）
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static FP Acos(FP value)
		{
			return FP.Acos(value);
		}

		public static FP Atan(FP value)
		{
			return FP.Atan(value);
		}

		public static FP Atan2(FP x, FP y)
		{
			return FP.Atan2(x, y);
		}


		public static FP Sin(FP value)
		{
			return FP.Sin(value);
		}

		public static FP Cos(FP value)
		{
			return FP.Cos(value);
		}

		public static FP Tan(FP value)
		{
			return FP.Tan(value);
		}

		public static FP Log(FP value)
		{
			return FP.Ln(value);
		}

		public static FP Log(FP value, FP newBase)
		{
			return Log(value) / Log(newBase);
		}

		public static FP Log10(FP value)
		{
			return Log(value) / Log(10);
		}

		// Infinite Line Intersection (line1 is p1-p2 and line2 is p3-p4)
		public static bool LineIntersection(FPVector2 p1, FPVector2 p2, FPVector2 p3, FPVector2 p4, ref FPVector2 result)
		{
			FP bx = p2.x - p1.x;
			FP by = p2.y - p1.y;
			FP dx = p4.x - p3.x;
			FP dy = p4.y - p3.y;
			FP bDotDPerp = bx * dy - by * dx;
			if (bDotDPerp == 0)
				return false;
			FP cx = p3.x - p1.x;
			FP cy = p3.y - p1.y;
			FP t = (cx * dy - cy * dx) / bDotDPerp;

			result.x = p1.x + t * bx;
			result.y = p1.y + t * by;
			return true;
		}

		// Line Segment Intersection (line1 is p1-p2 and line2 is p3-p4)
		public static bool LineSegmentIntersection(FPVector2 p1, FPVector2 p2, FPVector2 p3, FPVector2 p4, ref FPVector2 result)
		{
			FP bx = p2.x - p1.x;
			FP by = p2.y - p1.y;
			FP dx = p4.x - p3.x;
			FP dy = p4.y - p3.y;
			FP bDotDPerp = bx * dy - by * dx;
			if (bDotDPerp == 0)
				return false;
			FP cx = p3.x - p1.x;
			FP cy = p3.y - p1.y;
			FP t = (cx * dy - cy * dx) / bDotDPerp;
			if (t < 0 || t > 1)
				return false;
			FP u = (cx * by - cy * bx) / bDotDPerp;
			if (u < 0 || u > 1)
				return false;

			result.x = p1.x + t * bx;
			result.y = p1.y + t * by;
			return true;
		}

		public static FP ClampedMove(FP lhs, FP rhs, FP clampedDelta)
		{
			var delta = rhs - lhs;
			if (delta > 0)
				return lhs + Min(delta, clampedDelta);
			return lhs - Min(-delta, clampedDelta);
		}


		public static FP HorizontalAngle(FPVector2 dir)
		{
			var v = Atan2(dir.x, dir.y);
			return Rad2Deg * v;
		}

		public static FP IEEERemainder(FP x, FP y)
		{
			//		if (Double.IsNaN(x))
			//		{
			//			return x; // IEEE 754-2008: NaN payload must be preserved
			//		}
			//		if (Double.IsNaN(y))
			//		{
			//			return y; // IEEE 754-2008: NaN payload must be preserved
			//		}

			FP regularMod = x % y;
			//		if (Double.IsNaN(regularMod))
			//		{
			//			return Double.NaN;
			//		}
			//		if (regularMod == 0)
			//		{
			//			if (Double.IsNegative(x))
			//			{
			//				return Double.NegativeZero;
			//			}
			//		}
			FP alternativeResult = regularMod - (Abs(y) * Sign(x));
			if (Abs(alternativeResult) == Abs(regularMod))
			{
				FP divisionResult = x / y;
				FP roundedResult = Round(divisionResult);
				if (Abs(roundedResult) > Abs(divisionResult))
					return alternativeResult;
				return regularMod;
			}

			if (Abs(alternativeResult) < Abs(regularMod))
				return alternativeResult;
			return regularMod;
		}

		//https://stackoverflow.com/questions/23402414/implementing-an-accurate-cbrt-function-without-extra-precision
		public static FP Cbrt(FP x)
		{
			if (x == 0) return 0;
			if (x < 0) return -Cbrt(-x);

			var r = x;
			FP ex = 0;

			while (r < 0.125) { r *= 8; ex = ex - 1; }
			while (r > 1.0) { r *= 0.125; ex = ex + 1; }

			r = (-0.46946116 * r + 1.072302) * r + 0.3812513;

			while (ex < 0) { r *= 0.5; ex = ex + 1; }
			while (ex > 0) { r *= 2; ex = ex - 1; }

			FP a = 0.66666666666667;// 2/3
			FP b = 0.33333333333333;// 1/3

			r = a * r + b * x / (r * r);
			r = a * r + b * x / (r * r);
			r = a * r + b * x / (r * r);
			r = a * r + b * x / (r * r);

			return r;
		}
	}
}

