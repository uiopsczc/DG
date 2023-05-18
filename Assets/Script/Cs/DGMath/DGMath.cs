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


using System;
using UnityEngine;
using FP = DGFixedPoint;
using FPVector2 = DGVector2;

public static class DGMath
{
	public static readonly FP E = FP.E;
	public static readonly FP PI = FP.Pi;
	public static readonly FP HalfPi = FP.HalfPi;
	public static readonly FP Deg2Rad = FP.Deg2Rad;
	public static readonly FP Rad2Deg = FP.Rad2Deg;
	public static readonly FP Epsilon = FP.Epsilon;
	public static readonly FP Half = FP.Half;
	public static readonly FP Quarter = FP.Quarter;
	public static readonly FP HalfSqrt2 = (FP)0.7071067811865475244008443621048490f;


	public static bool IsApproximatelyZero(FP value)
	{
		return Abs(value)< Epsilon;
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
			return FP.Zero;
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
			return FP.Zero;
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
		return Clamp(value, FP.Zero, FP.One);
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
		FP num = Repeat(b - a, FP.Cache[360]);
		if (num > FP.Cache[180])
			num -= FP.Cache[360];
		return a + num * Clamp01(t);
	}

	public static FP MoveTowards(FP current, FP target, FP maxDelta)
	{
		if (Abs(target - current) <= maxDelta)
			return target;
		return current + (FP)Sign(target - current) * maxDelta;
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
		t = (FP)(-2) * t * t * t + (FP)(3) * t * t;
		return to * t + from * ((FP)1 - t);
	}

	public static FP Gamma(FP value, FP absmax, FP gamma)
	{
		bool negative = value < (FP)0;
		FP absval = Abs(value);
		if (absval > absmax)
			return negative ? -absval : absval;
		FP result = Pow(absval / absmax, gamma) * absmax;
		return negative ? -result : result;
	}

#if UNITY_5_3_OR_NEWER
	public static FP SmoothDamp(FP current, FP target, ref FP currentVelocity,
		FP smoothTime, FP maxSpeed)
	{
		FP deltaTime = (FP)Time.deltaTime;
		return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}
#endif

	public static FP SmoothDamp(FP current, FP target, ref FP currentVelocity,
		FP smoothTime, FP maxSpeed, FP deltaTime)
	{
		// Based on Game Programming Gems 4 Chapter 1.10
		smoothTime = Max((FP)0.0001F, smoothTime);
		FP omega = (FP)2F / smoothTime;

		FP x = omega * deltaTime;
		FP exp = (FP)1F / ((FP)1F + x + (FP)0.48F * x * x + (FP)0.235F * x * x * x);
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
		if (originalTo - current >  (FP)0.0F == output > originalTo)
		{
			output = originalTo;
			currentVelocity = (output - originalTo) / deltaTime;
		}

		return output;
	}

	public static FP SmoothDampAngle(FP current, FP target,
		ref FP currentVelocity, FP smoothTime, FP maxSpeed)
	{
		FP deltaTime = (FP)Time.deltaTime;
		return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}

	public static FP SmoothDampAngle(FP current, FP target,
		ref FP currentVelocity, FP smoothTime)
	{
		FP deltaTime = (FP)Time.deltaTime;
		FP maxSpeed = FP.MaxValue;
		return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}

	public static FP SmoothDampAngle(FP current, FP target,
		ref FP currentVelocity, FP smoothTime, FP maxSpeed, FP deltaTime)
	{
		target = current + DeltaAngle(current, target);
		return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}

	public static FP Repeat(FP t, FP length)
	{
		return Clamp(t - Floor(t / length) * length, FP.Zero, length);
	}

	public static FP PingPong(FP t, FP length)
	{
		t = Repeat(t, (FP)2 * length);
		return length - Abs(t - length);
	}

	public static FP InverseLerp(FP a, FP b, FP value)
	{
		if (a != b)
			return Clamp01((value - a) / (b - a));
		return FP.Zero;
	}

	public static FP DeltaAngle(FP current, FP target)
	{
		FP num = Repeat(target - current, FP.Cache[360]);
		if (num > FP.Cache[180])
			num -= FP.Cache[360];
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
		return FP.Atan2(x,y);
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
		return Log(value)/Log(newBase);
	}

	public static FP Log10(FP value)
	{
		return Log(value) / Log((FP)(10));
	}

	// Infinite Line Intersection (line1 is p1-p2 and line2 is p3-p4)
	public static bool LineIntersection(FPVector2 p1, FPVector2 p2, FPVector2 p3, FPVector2 p4, ref FPVector2 result)
	{
		FP bx = p2.x - p1.x;
		FP by = p2.y - p1.y;
		FP dx = p4.x - p3.x;
		FP dy = p4.y - p3.y;
		FP bDotDPerp = bx * dy - by * dx;
		if (bDotDPerp == (FP)0)
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
		if (bDotDPerp == (FP)0)
			return false;
		FP cx = p3.x - p1.x;
		FP cy = p3.y - p1.y;
		FP t = (cx * dy - cy * dx) / bDotDPerp;
		if (t < (FP)0 || t > (FP)1)
			return false;
		FP u = (cx * by - cy * bx) / bDotDPerp;
		if (u < (FP)0 || u > (FP)1)
			return false;

		result.x = p1.x + t * bx;
		result.y = p1.y + t * by;
		return true;
	}

	public static FP ClampedMove(FP lhs, FP rhs, FP clampedDelta)
	{
		var delta = rhs - lhs;
		if (delta > (FP)0)
			return lhs + Min(delta, clampedDelta);
		return lhs - Min(-delta, clampedDelta);
	}


	public static FP HorizontalAngle(FPVector2 dir)
	{
		var v = Atan2(dir.x, dir.y);
		return Rad2Deg * v;
	}
}
