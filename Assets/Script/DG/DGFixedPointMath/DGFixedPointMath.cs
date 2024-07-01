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
	public static partial class DGFixedPointMath
	{
		public static readonly DGFixedPoint One = DGFixedPoint.One;
		public static readonly DGFixedPoint E = DGFixedPoint.E;
		public static readonly DGFixedPoint PI = DGFixedPoint.Pi;
		public static readonly DGFixedPoint TwoPI = DGFixedPoint.TwoPi;
		public static readonly DGFixedPoint HalfPi = DGFixedPoint.HalfPi;
		public static readonly DGFixedPoint Deg2Rad = DGFixedPoint.Deg2Rad;
		public static readonly DGFixedPoint Rad2Deg = DGFixedPoint.Rad2Deg;
		public static readonly DGFixedPoint Epsilon = DGFixedPoint.Epsilon;
		public static readonly DGFixedPoint Half = DGFixedPoint.Half;
		public static readonly DGFixedPoint Quarter = DGFixedPoint.Quarter;
		public static readonly DGFixedPoint HalfSqrt2 = (DGFixedPoint)0.7071067811865475244008443621048490f;
		public static readonly DGFixedPoint MaxValue = DGFixedPoint.MaxValue;
		public static readonly DGFixedPoint MinValue = DGFixedPoint.MinValue;


		public static bool IsApproximatelyZero(DGFixedPoint value)
		{
			return Abs(value) < Epsilon;
		}


		public static DGFixedPoint Abs(DGFixedPoint value)
		{
			return DGFixedPoint.Abs(value);
		}

		public static DGFixedPoint Min(DGFixedPoint value1, DGFixedPoint value2)
		{
			return value1.scaledValue < value2.scaledValue ? value1 : value2;
		}

		public static DGFixedPoint Min(DGFixedPoint[] values)
		{
			int length = values.Length;
			if (length == 0)
				return DGFixedPoint.Zero;
			var minValue = values[0];
			for (int index = 1; index < length; ++index)
			{
				var value = values[index];
				if (value < minValue)
					minValue = value;
			}

			return minValue;
		}

		public static DGFixedPoint Max(DGFixedPoint value1, DGFixedPoint value2)
		{
			return value1.scaledValue > value2.scaledValue ? value1 : value2;
		}

		public static DGFixedPoint Max(DGFixedPoint[] values)
		{
			int length = values.Length;
			if (length == 0)
				return DGFixedPoint.Zero;
			var maxValue = values[0];
			for (int index = 1; index < length; ++index)
			{
				var value = values[index];
				if (value > maxValue)
					maxValue = value;
			}

			return maxValue;
		}

		public static DGFixedPoint Sqrt(DGFixedPoint value)
		{
			return DGFixedPoint.Sqrt(value);
		}

		public static DGFixedPoint Pow(DGFixedPoint value, DGFixedPoint power)
		{
			return DGFixedPoint.Pow(value, power);
		}

		public static DGFixedPoint Exp(DGFixedPoint power)
		{
			return Pow(E, power);
		}

		public static DGFixedPoint Ceiling(DGFixedPoint value)
		{
			return DGFixedPoint.Ceiling(value);
		}

		public static DGFixedPoint Floor(DGFixedPoint value)
		{
			return DGFixedPoint.Floor(value);
		}

		public static DGFixedPoint Round(DGFixedPoint value)
		{
			return DGFixedPoint.Round(value);
		}

		public static DGFixedPoint Truncate(DGFixedPoint value)
		{
			return DGFixedPoint.Truncate(value);
		}

		public static int Sign(DGFixedPoint value)
		{
			return DGFixedPoint.Sign(value);
		}

		public static DGFixedPoint CopySign(DGFixedPoint x, DGFixedPoint y)
		{
			return DGFixedPoint.CopySign(x, y);
		}

		public static DGFixedPoint Clamp(DGFixedPoint value, DGFixedPoint min, DGFixedPoint max)
		{
			if (value < min)
				return min;
			if (value > max)
				return max;
			return value;
		}

		public static DGFixedPoint Clamp01(DGFixedPoint value)
		{
			return Clamp(value, DGFixedPoint.Zero, DGFixedPoint.One);
		}

		public static DGFixedPoint Lerp(DGFixedPoint a, DGFixedPoint b, DGFixedPoint t)
		{
			return a + (b - a) * Clamp01(t);
		}

		public static DGFixedPoint LerpUnclamped(DGFixedPoint a, DGFixedPoint b, DGFixedPoint t)
		{
			return a + (b - a) * t;
		}

		public static DGFixedPoint LerpAngle(DGFixedPoint a, DGFixedPoint b, DGFixedPoint t)
		{
			DGFixedPoint num = Repeat(b - a, DGFixedPoint.Cache[360]);
			if (num > DGFixedPoint.Cache[180])
				num -= DGFixedPoint.Cache[360];
			return a + num * Clamp01(t);
		}

		public static DGFixedPoint MoveTowards(DGFixedPoint current, DGFixedPoint target, DGFixedPoint maxDelta)
		{
			if (Abs(target - current) <= maxDelta)
				return target;
			return current + (DGFixedPoint)Sign(target - current) * maxDelta;
		}

		public static DGFixedPoint MoveTowardsAngle(DGFixedPoint current, DGFixedPoint target, DGFixedPoint maxDelta)
		{
			DGFixedPoint num = DeltaAngle(current, target);
			if (-maxDelta < num && num < maxDelta)
				return target;
			target = current + num;
			return MoveTowards(current, target, maxDelta);
		}

		public static DGFixedPoint SmoothStep(DGFixedPoint from, DGFixedPoint to, DGFixedPoint t)
		{
			t = Clamp01(t);
			t = (DGFixedPoint)(-2) * t * t * t + (DGFixedPoint)(3) * t * t;
			return to * t + from * ((DGFixedPoint)1 - t);
		}

		public static DGFixedPoint Gamma(DGFixedPoint value, DGFixedPoint absmax, DGFixedPoint gamma)
		{
			bool negative = value < (DGFixedPoint)0;
			DGFixedPoint absval = Abs(value);
			if (absval > absmax)
				return negative ? -absval : absval;
			DGFixedPoint result = Pow(absval / absmax, gamma) * absmax;
			return negative ? -result : result;
		}

#if UNITY_STANDALONE
		public static DGFixedPoint SmoothDamp(DGFixedPoint current, DGFixedPoint target, ref DGFixedPoint currentVelocity,
			DGFixedPoint smoothTime, DGFixedPoint maxSpeed)
		{
			DGFixedPoint deltaTime = (DGFixedPoint)Time.deltaTime;
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}
#endif

		public static DGFixedPoint SmoothDamp(DGFixedPoint current, DGFixedPoint target, ref DGFixedPoint currentVelocity,
			DGFixedPoint smoothTime, DGFixedPoint maxSpeed, DGFixedPoint deltaTime)
		{
			// Based on Game Programming Gems 4 Chapter 1.10
			smoothTime = Max((DGFixedPoint)0.0001F, smoothTime);
			DGFixedPoint omega = (DGFixedPoint)2F / smoothTime;

			DGFixedPoint x = omega * deltaTime;
			DGFixedPoint exp = (DGFixedPoint)1F / ((DGFixedPoint)1F + x + (DGFixedPoint)0.48F * x * x + (DGFixedPoint)0.235F * x * x * x);
			DGFixedPoint change = current - target;
			DGFixedPoint originalTo = target;

			// Clamp maximum speed
			DGFixedPoint maxChange = maxSpeed * smoothTime;
			change = Clamp(change, -maxChange, maxChange);
			target = current - change;

			DGFixedPoint temp = (currentVelocity + omega * change) * deltaTime;
			currentVelocity = (currentVelocity - omega * temp) * exp;
			DGFixedPoint output = target + (change + temp) * exp;

			// Prevent overshooting
			if (originalTo - current > (DGFixedPoint)0.0F == output > originalTo)
			{
				output = originalTo;
				currentVelocity = (output - originalTo) / deltaTime;
			}

			return output;
		}
#if UNITY_STANDALONE
		public static DGFixedPoint SmoothDampAngle(DGFixedPoint current, DGFixedPoint target,
			ref DGFixedPoint currentVelocity, DGFixedPoint smoothTime, DGFixedPoint maxSpeed)
		{
			DGFixedPoint deltaTime = (DGFixedPoint)Time.deltaTime;
			return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static DGFixedPoint SmoothDampAngle(DGFixedPoint current, DGFixedPoint target,
			ref DGFixedPoint currentVelocity, DGFixedPoint smoothTime)
		{
			DGFixedPoint deltaTime = (DGFixedPoint)Time.deltaTime;
			DGFixedPoint maxSpeed = DGFixedPoint.MaxValue;
			return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}
#endif

		public static DGFixedPoint SmoothDampAngle(DGFixedPoint current, DGFixedPoint target,
			ref DGFixedPoint currentVelocity, DGFixedPoint smoothTime, DGFixedPoint maxSpeed, DGFixedPoint deltaTime)
		{
			target = current + DeltaAngle(current, target);
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static DGFixedPoint Repeat(DGFixedPoint t, DGFixedPoint length)
		{
			return Clamp(t - Floor(t / length) * length, DGFixedPoint.Zero, length);
		}

		public static DGFixedPoint PingPong(DGFixedPoint t, DGFixedPoint length)
		{
			t = Repeat(t, (DGFixedPoint)2 * length);
			return length - Abs(t - length);
		}

		public static DGFixedPoint InverseLerp(DGFixedPoint a, DGFixedPoint b, DGFixedPoint value)
		{
			if (a != b)
				return Clamp01((value - a) / (b - a));
			return DGFixedPoint.Zero;
		}

		public static DGFixedPoint DeltaAngle(DGFixedPoint current, DGFixedPoint target)
		{
			DGFixedPoint num = Repeat(target - current, DGFixedPoint.Cache[360]);
			if (num > DGFixedPoint.Cache[180])
				num -= DGFixedPoint.Cache[360];
			return num;
		}

		public static DGFixedPoint Asin(DGFixedPoint value)
		{
			return DGFixedPoint.Asin(value);
		}

		/// <summary>
		/// 反余弦值（即求指定余弦值对应的弧度）
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DGFixedPoint Acos(DGFixedPoint value)
		{
			return DGFixedPoint.Acos(value);
		}

		public static DGFixedPoint Atan(DGFixedPoint value)
		{
			return DGFixedPoint.Atan(value);
		}

		public static DGFixedPoint Atan2(DGFixedPoint x, DGFixedPoint y)
		{
			return DGFixedPoint.Atan2(x, y);
		}


		public static DGFixedPoint Sin(DGFixedPoint value)
		{
			return DGFixedPoint.Sin(value);
		}

		public static DGFixedPoint Cos(DGFixedPoint value)
		{
			return DGFixedPoint.Cos(value);
		}

		public static DGFixedPoint Tan(DGFixedPoint value)
		{
			return DGFixedPoint.Tan(value);
		}

		public static DGFixedPoint Log(DGFixedPoint value)
		{
			return DGFixedPoint.Ln(value);
		}

		public static DGFixedPoint Log(DGFixedPoint value, DGFixedPoint newBase)
		{
			return Log(value) / Log(newBase);
		}

		public static DGFixedPoint Log10(DGFixedPoint value)
		{
			return Log(value) / Log((DGFixedPoint)(10));
		}

		// Infinite Line Intersection (line1 is p1-p2 and line2 is p3-p4)
		public static bool LineIntersection(DGVector2 p1, DGVector2 p2, DGVector2 p3, DGVector2 p4, ref DGVector2 result)
		{
			DGFixedPoint bx = p2.x - p1.x;
			DGFixedPoint by = p2.y - p1.y;
			DGFixedPoint dx = p4.x - p3.x;
			DGFixedPoint dy = p4.y - p3.y;
			DGFixedPoint bDotDPerp = bx * dy - by * dx;
			if (bDotDPerp == (DGFixedPoint)0)
				return false;
			DGFixedPoint cx = p3.x - p1.x;
			DGFixedPoint cy = p3.y - p1.y;
			DGFixedPoint t = (cx * dy - cy * dx) / bDotDPerp;

			result.x = p1.x + t * bx;
			result.y = p1.y + t * by;
			return true;
		}

		// Line Segment Intersection (line1 is p1-p2 and line2 is p3-p4)
		public static bool LineSegmentIntersection(DGVector2 p1, DGVector2 p2, DGVector2 p3, DGVector2 p4, ref DGVector2 result)
		{
			DGFixedPoint bx = p2.x - p1.x;
			DGFixedPoint by = p2.y - p1.y;
			DGFixedPoint dx = p4.x - p3.x;
			DGFixedPoint dy = p4.y - p3.y;
			DGFixedPoint bDotDPerp = bx * dy - by * dx;
			if (bDotDPerp == (DGFixedPoint)0)
				return false;
			DGFixedPoint cx = p3.x - p1.x;
			DGFixedPoint cy = p3.y - p1.y;
			DGFixedPoint t = (cx * dy - cy * dx) / bDotDPerp;
			if (t < (DGFixedPoint)0 || t > (DGFixedPoint)1)
				return false;
			DGFixedPoint u = (cx * by - cy * bx) / bDotDPerp;
			if (u < (DGFixedPoint)0 || u > (DGFixedPoint)1)
				return false;

			result.x = p1.x + t * bx;
			result.y = p1.y + t * by;
			return true;
		}

		public static DGFixedPoint ClampedMove(DGFixedPoint lhs, DGFixedPoint rhs, DGFixedPoint clampedDelta)
		{
			var delta = rhs - lhs;
			if (delta > (DGFixedPoint)0)
				return lhs + Min(delta, clampedDelta);
			return lhs - Min(-delta, clampedDelta);
		}


		public static DGFixedPoint HorizontalAngle(DGVector2 dir)
		{
			var v = Atan2(dir.x, dir.y);
			return Rad2Deg * v;
		}

		public static DGFixedPoint IEEERemainder(DGFixedPoint x, DGFixedPoint y)
		{
			//		if (Double.IsNaN(x))
			//		{
			//			return x; // IEEE 754-2008: NaN payload must be preserved
			//		}
			//		if (Double.IsNaN(y))
			//		{
			//			return y; // IEEE 754-2008: NaN payload must be preserved
			//		}

			DGFixedPoint regularMod = x % y;
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
			DGFixedPoint alternativeResult = regularMod - (Abs(y) * (DGFixedPoint)Sign(x));
			if (Abs(alternativeResult) == Abs(regularMod))
			{
				DGFixedPoint divisionResult = x / y;
				DGFixedPoint roundedResult = Round(divisionResult);
				if (Abs(roundedResult) > Abs(divisionResult))
					return alternativeResult;
				return regularMod;
			}

			if (Abs(alternativeResult) < Abs(regularMod))
				return alternativeResult;
			return regularMod;
		}

		//https://stackoverflow.com/questions/23402414/implementing-an-accurate-cbrt-function-without-extra-precision
		public static DGFixedPoint Cbrt(DGFixedPoint x)
		{
			if (x == (DGFixedPoint)0) return (DGFixedPoint)0;
			if (x < (DGFixedPoint)0) return -Cbrt(-x);

			var r = x;
			var ex = (DGFixedPoint)0;

			while (r < (DGFixedPoint)0.125) { r *= (DGFixedPoint)8; ex = ex - (DGFixedPoint)1; }
			while (r > (DGFixedPoint)1.0) { r *= (DGFixedPoint)0.125; ex = ex + (DGFixedPoint)1; }

			r = ((DGFixedPoint)(-0.46946116) * r + (DGFixedPoint)1.072302) * r + (DGFixedPoint)0.3812513;

			while (ex < (DGFixedPoint)0) { r *= (DGFixedPoint)0.5; ex = ex + (DGFixedPoint)1; }
			while (ex > (DGFixedPoint)0) { r *= (DGFixedPoint)2; ex = ex - (DGFixedPoint)1; }

			var a = (DGFixedPoint)0.66666666666667;// 2/3
			var b = (DGFixedPoint)0.33333333333333;// 1/3

			r = a * r + b * x / (r * r);
			r = a * r + b * x / (r * r);
			r = a * r + b * x / (r * r);
			r = a * r + b * x / (r * r);

			return r;
		}
	}
}

