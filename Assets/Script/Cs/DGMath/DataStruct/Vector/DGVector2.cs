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
using FP = DGFixedPoint;
using FPVector3 = DGVector3;
using FPVector4 = DGVector4;
#if UNITY_5_3_OR_NEWER
using UnityEngine;

#endif

public struct DGVector2 : IEquatable<DGVector2>
{
	public static readonly FP kEpsilon = (FP) 0.00001F;
	public static readonly FP kEpsilonNormalSqrt = (FP) 1e-15f;

	public static DGVector2 zero => new DGVector2(0, 0);
	public static DGVector2 one => new DGVector2(1, 1);
	public static DGVector2 left => new DGVector2(-1, 0);
	public static DGVector2 right => new DGVector2(1, 0);
	public static DGVector2 up => new DGVector2(0, 1);
	public static DGVector2 down => new DGVector2(0, -1);
	public static DGVector2 max => new DGVector2(float.MaxValue, float.MaxValue);
	public static DGVector2 min => new DGVector2(float.MinValue, float.MinValue);

	public FP x;
	public FP y;

	public FP this[int index]
	{
		get
		{
			switch (index)
			{
				case 0:
					return x;
				case 1:
					return y;
				default:
					throw new IndexOutOfRangeException("Invalid Vector2 index!");
			}
		}
		set
		{
			switch (index)
			{
				case 0:
					x = value;
					break;
				case 1:
					y = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector2 index!");
			}
		}
	}

	public long[] scaledValue => new long[] {x.scaledValue, y.scaledValue};

	/// <summary>
	/// 返回当前向量长度的平方
	/// </summary>
	public FP sqrMagnitude => x * x + y * y;

	public FP magnitude => DGMath.Sqrt(sqrMagnitude);

	/// <summary>
	/// 返回该向量的单位向量
	/// </summary>
	public DGVector2 normalized => Normalize(this);

	public DGVector2(FP x, FP y)
	{
		this.x = x;
		this.y = y;
	}

	public DGVector2(float x, float y)
	{
		this.x = (FP) x;
		this.y = (FP) y;
	}

	public DGVector2(int x, int y)
	{
		this.x = (FP) x;
		this.y = (FP) y;
	}


#if UNITY_5_3_OR_NEWER
	public DGVector2(Vector2 vector)
	{
		this.x = (FP) vector.x;
		this.y = (FP) vector.y;
	}
#endif
	/*************************************************************************************
	* 模块描述:Equals ToString
	*************************************************************************************/
	public override bool Equals(object obj)
	{
		if (obj == null)
			return false;
		var other = (DGVector2) obj;
		return Equals(other);
	}

	public bool Equals(DGVector2 other)
	{
		return x == other.x && y == other.y;
	}

	public override int GetHashCode()
	{
		return x.GetHashCode() ^ (y.GetHashCode() << 2);
	}

	public override string ToString()
	{
		return string.Format("x:{0},y:{1}", x, y);
	}

	/*************************************************************************************
	* 模块描述:转换
	*************************************************************************************/
	public static implicit operator DGVector2(FPVector3 v)
	{
		return new DGVector2(v.x, v.y);
	}

	public static implicit operator DGVector2(FPVector4 v)
	{
		return new DGVector2(v.x, v.y);
	}

#if UNITY_5_3_OR_NEWER
	//转换为Unity的Vector2
	public Vector2 ToVector2()
	{
		return new Vector2((float) x, (float) y);
	}
#endif
	/*************************************************************************************
	* 模块描述:关系运算符
	*************************************************************************************/
	public static bool operator ==(DGVector2 value1, DGVector2 value2)
	{
		// Returns false in the presence of NaN values.
		FP diffX = value1.x - value2.x;
		FP diffY = value1.y - value2.y;
		return (diffX * diffX + diffY * diffY) < kEpsilon * kEpsilon;
	}

	public static bool operator !=(DGVector2 value1, DGVector2 value2)
	{
		return !(value1 == value2);
	}

	/*************************************************************************************
	* 模块描述:算术运算符
	*************************************************************************************/
	public static DGVector2 operator +(DGVector2 value1, DGVector2 value2)
	{
		FP x = value1.x + value2.x;
		FP y = value1.y + value2.y;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator -(DGVector2 value1, DGVector2 value2)
	{
		FP x = value1.x - value2.x;
		FP y = value1.y - value2.y;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator -(DGVector2 value)
	{
		FP x = -value.x;
		FP y = -value.y;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator *(DGVector2 value1, DGVector2 value2)
	{
		FP x = value1.x * value2.x;
		FP y = value1.y * value2.y;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator *(DGVector2 value, FP multiply)
	{
		FP x = value.x * multiply;
		FP y = value.y * multiply;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator /(DGVector2 value1, DGVector2 value2)
	{
		FP x = value1.x / value2.x;
		FP y = value1.y / value2.y;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator /(DGVector2 value1, FP div)
	{
		FP x = value1.x / div;
		FP y = value1.y / div;
		return new DGVector2(x, y);
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	public static bool IsUnit(DGVector2 vector)
	{
		return DGMath.IsApproximatelyZero((FP)1 - vector.x * vector.x - vector.y * vector.y);
	}

	/// <summary>
	/// 返回当前向量长度的平方
	/// </summary>
	public static FP SqrMagnitude(DGVector2 v)
	{
		return v.sqrMagnitude;
	}

	public static DGVector2 Lerp(DGVector2 a, DGVector2 b, FP t)
	{
		t = DGMath.Clamp01(t);
		return new DGVector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
	}

	public static DGVector2 LerpUnclamped(DGVector2 a, DGVector2 b, FP t)
	{
		return new DGVector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
	}

	public static DGVector2 MoveTowards(DGVector2 current, DGVector2 target, FP maxDistanceDelta)
	{
		FP toVectorX = target.x - current.x;
		FP toVectorY = target.y - current.y;
		FP sqrtDistance = toVectorX * toVectorX + toVectorY * toVectorY;
		if (sqrtDistance == FP.Zero ||
		    maxDistanceDelta >= FP.Zero && sqrtDistance <= maxDistanceDelta * maxDistanceDelta)
			return target;
		FP num4 = DGMath.Sqrt(sqrtDistance);
		return new DGVector2(current.x + toVectorX / num4 * maxDistanceDelta,
			current.y + toVectorY / num4 * maxDistanceDelta);
	}

#if UNITY_5_3_OR_NEWER
	public static DGVector2 SmoothDamp(DGVector2 current, DGVector2 target, ref DGVector2 currentVelocity,
		FP smoothTime, FP maxSpeed)
	{
		FP deltaTime = (FP) Time.deltaTime;
		return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}

	public static DGVector2 SmoothDamp(DGVector2 current, DGVector2 target, ref DGVector2 currentVelocity,
		FP smoothTime)
	{
		FP deltaTime = (FP) Time.deltaTime;
		FP maxSpeed = FP.MaxValue;
		return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}
#endif

	public static DGVector2 SmoothDamp(DGVector2 current, DGVector2 target, ref DGVector2 currentVelocity,
		FP smoothTime, FP maxSpeed, FP deltaTime)
	{
		// Based on Game Programming Gems 4 Chapter 1.10
		smoothTime = DGMath.Max((FP) 0.0001F, smoothTime);
		FP omega = (FP) 2 / smoothTime;

		FP x = omega * deltaTime;
		FP exp = (FP) 1 / ((FP) 1 + x + (FP) 0.48F * x * x + (FP) 0.235F * x * x * x);

		FP changeX = current.x - target.x;
		FP changeY = current.y - target.y;
		DGVector2 originalTo = target;

		// Clamp maximum speed
		FP maxChange = maxSpeed * smoothTime;

		FP maxChangeSq = maxChange * maxChange;
		FP sqDist = changeX * changeX + changeY * changeY;
		if (sqDist > maxChangeSq)
		{
			var mag = DGMath.Sqrt(sqDist);
			changeX = changeX / mag * maxChange;
			changeY = changeY / mag * maxChange;
		}

		target.x = current.x - changeX;
		target.y = current.y - changeY;

		FP tempX = (currentVelocity.x + omega * changeX) * deltaTime;
		FP tempY = (currentVelocity.y + omega * changeY) * deltaTime;

		currentVelocity.x = (currentVelocity.x - omega * tempX) * exp;
		currentVelocity.y = (currentVelocity.y - omega * tempY) * exp;

		FP outputX = target.x + (changeX + tempX) * exp;
		FP outputY = target.y + (changeY + tempY) * exp;

		// Prevent overshooting
		FP origMinusCurrentX = originalTo.x - current.x;
		FP origMinusCurrentY = originalTo.y - current.y;
		FP outMinusOrigX = outputX - originalTo.x;
		FP outMinusOrigY = outputY - originalTo.y;

		if (origMinusCurrentX * outMinusOrigX + origMinusCurrentY * outMinusOrigY > (FP) 0)
		{
			outputX = originalTo.x;
			outputY = originalTo.y;

			currentVelocity.x = (outputX - originalTo.x) / deltaTime;
			currentVelocity.y = (outputY - originalTo.y) / deltaTime;
		}

		return new DGVector2(outputX, outputY);
	}

	public static DGVector2 Scale(DGVector2 a, DGVector2 b)
	{
		return new DGVector2(a.x * b.x, a.y * b.y);
	}

	public static DGVector2 Reflect(DGVector2 inDirection, DGVector2 inNormal)
	{
		FP factor = (FP) (-2) * Dot(inNormal, inDirection);
		return new DGVector2(factor * inNormal.x + inDirection.x, factor * inNormal.y + inDirection.y);
	}

	public static DGVector2 Perpendicular(DGVector2 inDirection)
	{
		return new DGVector2(-inDirection.y, inDirection.x);
	}

	/// <summary>
	/// 单位化该向量
	/// </summary>
	/// <param name="v"></param>
	public static DGVector2 Normalize(DGVector2 value)
	{
		var magnitude = value.magnitude;
		if (magnitude <= kEpsilon)
			return zero;
		FP rate = FP.One / magnitude;
		return value * rate;
	}

	/// <summary>
	/// 点乘
	/// </summary>
	/// <param name="v"></param>
	/// <returns></returns>
	public static FP Dot(DGVector2 v1, DGVector2 v2)
	{
		return v1.x * v2.x + v1.y * v2.y;
	}

	/// <summary>
	/// 叉乘
	/// </summary>
	/// <param name="v1"></param>
	/// <param name="v2"></param>
	/// <returns></returns>GetHashCode
	public static DGVector2 Cross(DGVector2 v1, DGVector2 v2)
	{
		return new DGVector2(v1.x * v2.y - v1.y * v2.x, v1.y * v2.x - v1.x * v2.y);
	}

	/// <summary>
	/// 求两个向量的夹角，没有正负区分
	/// </summary>
	/// <param name="fromAngle"></param>
	/// <param name="toAngle"></param>
	/// <returns></returns>
	public static FP Angle(DGVector2 fromAngle, DGVector2 toAngle)
	{
		// sqrt(a) * sqrt(b) = sqrt(a * b) -- valid for real numbers
		FP denominator = DGMath.Sqrt(fromAngle.sqrMagnitude * toAngle.sqrMagnitude);
		if (denominator < kEpsilonNormalSqrt)
			return FP.Zero;
		FP dot = DGMath.Clamp(Dot(fromAngle, toAngle) / denominator, FP.NegativeOne, FP.One);
		return DGMath.Acos(dot) * DGMath.Rad2Deg;
	}

	public static FP SignedAngle(DGVector2 from, DGVector2 to)
	{
		FP unsignedAngle = Angle(from, to);
		FP sign = (FP) DGMath.Sign(from.x * to.y - from.y * to.x);
		return unsignedAngle * sign;
	}

	public static FP Distance(DGVector2 a, DGVector2 b)
	{
		FP diffX = a.x - b.x;
		FP diffY = a.y - b.y;
		return DGMath.Sqrt(diffX * diffX + diffY * diffY);
	}

	public static DGVector2 ClampMagnitude(DGVector2 vector, FP maxLength)
	{
		FP sqrMagnitude = vector.sqrMagnitude;
		if (sqrMagnitude <= maxLength * maxLength)
			return vector;
		//these intermediate variables force the intermediate result to be
		//of float precision. without this, the intermediate result can be of higher
		//precision, which changes behavior.
		FP mag = DGMath.Sqrt(sqrMagnitude);
		FP normalizedX = vector.x / mag;
		FP normalizedY = vector.y / mag;
		return new DGVector2(normalizedX * maxLength, normalizedY * maxLength);
	}

	public static FP Magnitude(DGVector2 vector)
	{
		return DGMath.Sqrt(vector.x * vector.x + vector.y * vector.y);
	}

	public static DGVector2 Min(DGVector2 value1, DGVector2 value2)
	{
		return new DGVector2(DGMath.Min(value1.x, value2.x), DGMath.Min(value1.y, value2.y));
	}

	public static DGVector2 Max(DGVector2 value1, DGVector2 value2)
	{
		return new DGVector2(DGMath.Max(value1.x, value2.x), DGMath.Max(value1.y, value2.y));
	}

	public static DGVector2 SmoothStep(DGVector2 v1, DGVector2 v2, FP amount)
	{
		amount = DGMath.Clamp01(amount);
		amount = (amount * amount) * ((FP) 3 - ((FP) 2 * amount));
		var x = v1.x + ((v2.x - v1.x) * amount);
		var y = v1.y + ((v2.y - v1.y) * amount);
		return new DGVector2(x, y);
	}

	public static DGVector2 CatmullRom(DGVector2 v1, DGVector2 v2, DGVector2 v3, DGVector2 v4, FP amount)
	{
		amount = DGMath.Clamp01(amount);
		FP squared = amount * amount;
		FP cubed = amount * squared;
		DGVector2 r;
		r.x = (FP) 2 * v2.x;
		r.x += (v3.x - v1.x) * amount;
		r.x += (((FP) 2 * v1.x) + ((FP) 4 * v3.x) - ((FP) 5 * v2.x) - (v4.x)) * squared;
		r.x += (((FP) 3 * v2.x) + (v4.x) - (v1.x) - ((FP) 3 * v3.x)) * cubed;
		r.x *= DGMath.Half;
		r.y = (FP) 2 * v2.y;
		r.y += (v3.y - v1.y) * amount;
		r.y += (((FP) 2 * v1.y) + ((FP) 4 * v3.y) - ((FP) 5 * v2.y) - (v4.y)) * squared;
		r.y += (((FP) 3 * v2.y) + (v4.y) - (v1.y) - ((FP) 3 * v3.y)) * cubed;
		r.y *= DGMath.Half;
		return r;
	}

	public static DGVector2 Hermite(DGVector2 v1, DGVector2 tangent1, DGVector2 v2, DGVector2 tangent2, FP amount)
	{
		amount = DGMath.Clamp01(amount);
		bool tangent1IsUnit = IsUnit(tangent1);
		bool tangent2IsUnit = IsUnit(tangent2);
		FP squared = amount * amount;
		FP cubed = amount * squared;
		FP a = ((cubed * (FP) 2) - (squared * (FP) 3)) + (FP) 1;
		FP b = (-cubed * (FP) 2) + (squared * (FP) 3);
		FP c = (cubed - (squared * (FP) 2)) + amount;
		FP d = cubed - squared;
		var x = (v1.x * a) + (v2.x * b) + (tangent1.x * c) + (tangent2.x * d);
		var y = (v1.y * a) + (v2.y * b) + (tangent1.y * c) + (tangent2.y * d);
		return new DGVector2(x, y);
	}

/*************************************************************************************
* 模块描述:Util
*************************************************************************************/
	/// <summary>
	/// 单位化该向量
	/// </summary>
	/// <param name="v"></param>
	public void Normalize()
	{
		if (magnitude <= kEpsilon)
		{
			x = (FP) 0;
			y = (FP) 0;
			return;
		}

		FP rate = FP.One / magnitude;
		x *= rate;
		y *= rate;
	}

	public void Scale(DGVector2 scale)
	{
		this.x *= scale.x;
		this.y *= scale.y;
	}

	public bool IsUnit()
	{
		return IsUnit(this);
	}
}