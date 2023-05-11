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

public static class DGFixedPointMath
{
	public static readonly FP E = (FP)DGFixedPoint.E;
	public static readonly FP PI = (FP)DGFixedPoint.Pi;
	public static readonly FP Deg2Rad = (FP)DGFixedPoint.Deg2Rad;
	public static readonly FP Rad2Deg = (FP)DGFixedPoint.Rad2Deg;

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

	public static DGFixedPoint MoveTowardsAngle(FP current, FP target, FP maxDelta)
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
		bool flag = value < (FP)0;
		FP num1 = Abs(value);
		if (num1 > absmax)
			return flag ? -num1 : num1;
		FP num2 = Pow(num1 / absmax, gamma) * absmax;
		return flag ? -num2 : num2;
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
		smoothTime = Max((FP)0.0001f, smoothTime);
		FP num1 = (FP)2f / smoothTime;
		FP num2 = num1 * deltaTime;
		FP num3 = ((FP)1 / ((FP)1 + num2 + (FP)0.479999989271164 * num2 * num2 +
								  (FP)0.234999999403954 * num2 * num2 * num2));
		FP num4 = current - target;
		FP num5 = target;
		FP max = maxSpeed * smoothTime;
		FP num6 = Clamp(num4, -max, max);
		target = current - num6;
		FP num7 = (currentVelocity + num1 * num6) * deltaTime;
		currentVelocity = (currentVelocity - num1 * num7) * num3;
		FP num8 = target + (num6 + num7) * num3;
		if (num5 - current > FP.Zero == num8 > num5)
		{
			num8 = num5;
			currentVelocity = (num8 - num5) / deltaTime;
		}

		return num8;
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
}
