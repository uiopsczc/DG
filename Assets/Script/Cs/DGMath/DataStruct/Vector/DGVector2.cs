/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/5/12
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************c************************************************/

using System;
#if UNITY_STANDALONE
using UnityEngine;

#endif

public partial struct DGVector2 : IEquatable<DGVector2>
{
	public static readonly DGFixedPoint kEpsilon = (DGFixedPoint) 0.00001F;
	public static readonly DGFixedPoint kEpsilonNormalSqrt = (DGFixedPoint) 1e-15f;

	public static DGVector2 zero => new DGVector2(0, 0);
	public static DGVector2 one => new DGVector2(1, 1);
	public static DGVector2 left => new DGVector2(-1, 0);
	public static DGVector2 right => new DGVector2(1, 0);
	public static DGVector2 up => new DGVector2(0, 1);
	public static DGVector2 down => new DGVector2(0, -1);
	public static DGVector2 max => new DGVector2(float.MaxValue, float.MaxValue);
	public static DGVector2 min => new DGVector2(float.MinValue, float.MinValue);


	public DGFixedPoint this[int index]
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

	/// <summary>
	/// 返回当前向量长度的平方
	/// </summary>
	public DGFixedPoint sqrMagnitude => len2();

	public DGFixedPoint magnitude => len();

	/// <summary>
	/// 返回该向量的单位向量
	/// </summary>
	public DGVector2 normalized => nor();


	public DGVector2(float x, float y)
	{
		this.x = (DGFixedPoint) x;
		this.y = (DGFixedPoint) y;
	}

	public DGVector2(int x, int y)
	{
		this.x = (DGFixedPoint) x;
		this.y = (DGFixedPoint) y;
	}


#if UNITY_STANDALONE
	public DGVector2(Vector2 vector)
	{
		this.x = (DGFixedPoint) vector.x;
		this.y = (DGFixedPoint) vector.y;
	}
#endif
	public DGVector2(System.Numerics.Vector2 vector)
	{
		this.x = (DGFixedPoint) vector.X;
		this.y = (DGFixedPoint) vector.Y;
	}

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

	/*************************************************************************************
	* 模块描述:转换
	*************************************************************************************/
	public static implicit operator DGVector2(DGVector3 v)
	{
		return new DGVector2(v.x, v.y);
	}

	public static implicit operator DGVector2(DGVector4 v)
	{
		return new DGVector2(v.x, v.y);
	}

#if UNITY_STANDALONE
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
		return value1.x == value2.x && value1.y == value2.y;
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
		DGFixedPoint x = value1.x + value2.x;
		DGFixedPoint y = value1.y + value2.y;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator -(DGVector2 value1, DGVector2 value2)
	{
		DGFixedPoint x = value1.x - value2.x;
		DGFixedPoint y = value1.y - value2.y;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator -(DGVector2 value)
	{
		DGFixedPoint x = -value.x;
		DGFixedPoint y = -value.y;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator *(DGVector2 value1, DGVector2 value2)
	{
		DGFixedPoint x = value1.x * value2.x;
		DGFixedPoint y = value1.y * value2.y;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator *(DGVector2 value, DGFixedPoint multiply)
	{
		DGFixedPoint x = value.x * multiply;
		DGFixedPoint y = value.y * multiply;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator /(DGVector2 value1, DGVector2 value2)
	{
		DGFixedPoint x = value1.x / value2.x;
		DGFixedPoint y = value1.y / value2.y;
		return new DGVector2(x, y);
	}

	public static DGVector2 operator /(DGVector2 value1, DGFixedPoint div)
	{
		DGFixedPoint x = value1.x / div;
		DGFixedPoint y = value1.y / div;
		return new DGVector2(x, y);
	}


	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	/// <summary>
	/// 返回当前向量长度的平方
	/// </summary>
	public static DGFixedPoint SqrMagnitude(DGVector2 v)
	{
		return v.len2();
	}

	public static DGVector2 Lerp(DGVector2 a, DGVector2 b, DGFixedPoint t)
	{
		t = DGMath.Clamp01(t);
		return new DGVector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
	}

	public static DGVector2 LerpUnclamped(DGVector2 a, DGVector2 b, DGFixedPoint t)
	{
		return new DGVector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
	}

	public static DGVector2 MoveTowards(DGVector2 current, DGVector2 target, DGFixedPoint maxDistanceDelta)
	{
		DGFixedPoint toVectorX = target.x - current.x;
		DGFixedPoint toVectorY = target.y - current.y;
		DGFixedPoint sqrtDistance = toVectorX * toVectorX + toVectorY * toVectorY;
		if (sqrtDistance == DGFixedPoint.Zero ||
		    maxDistanceDelta >= DGFixedPoint.Zero && sqrtDistance <= maxDistanceDelta * maxDistanceDelta)
			return target;
		DGFixedPoint num4 = DGMath.Sqrt(sqrtDistance);
		return new DGVector2(current.x + toVectorX / num4 * maxDistanceDelta,
			current.y + toVectorY / num4 * maxDistanceDelta);
	}

#if UNITY_STANDALONE
	public static DGVector2 SmoothDamp(DGVector2 current, DGVector2 target, ref DGVector2 currentVelocity,
		DGFixedPoint smoothTime, DGFixedPoint maxSpeed)
	{
		DGFixedPoint deltaTime = (DGFixedPoint) Time.deltaTime;
		return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}

	public static DGVector2 SmoothDamp(DGVector2 current, DGVector2 target, ref DGVector2 currentVelocity,
		DGFixedPoint smoothTime)
	{
		DGFixedPoint deltaTime = (DGFixedPoint) Time.deltaTime;
		DGFixedPoint maxSpeed = DGFixedPoint.MaxValue;
		return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}
#endif

	public static DGVector2 SmoothDamp(DGVector2 current, DGVector2 target, ref DGVector2 currentVelocity,
		DGFixedPoint smoothTime, DGFixedPoint maxSpeed, DGFixedPoint deltaTime)
	{
		// Based on Game Programming Gems 4 Chapter 1.10
		smoothTime = DGMath.Max((DGFixedPoint) 0.0001F, smoothTime);
		DGFixedPoint omega = (DGFixedPoint) 2 / smoothTime;

		DGFixedPoint x = omega * deltaTime;
		DGFixedPoint exp = (DGFixedPoint) 1 / ((DGFixedPoint) 1 + x + (DGFixedPoint) 0.48F * x * x + (DGFixedPoint) 0.235F * x * x * x);

		DGFixedPoint changeX = current.x - target.x;
		DGFixedPoint changeY = current.y - target.y;
		DGVector2 originalTo = target;

		// Clamp maximum speed
		DGFixedPoint maxChange = maxSpeed * smoothTime;

		DGFixedPoint maxChangeSq = maxChange * maxChange;
		DGFixedPoint sqDist = changeX * changeX + changeY * changeY;
		if (sqDist > maxChangeSq)
		{
			var mag = DGMath.Sqrt(sqDist);
			changeX = changeX / mag * maxChange;
			changeY = changeY / mag * maxChange;
		}

		target.x = current.x - changeX;
		target.y = current.y - changeY;

		DGFixedPoint tempX = (currentVelocity.x + omega * changeX) * deltaTime;
		DGFixedPoint tempY = (currentVelocity.y + omega * changeY) * deltaTime;

		currentVelocity.x = (currentVelocity.x - omega * tempX) * exp;
		currentVelocity.y = (currentVelocity.y - omega * tempY) * exp;

		DGFixedPoint outputX = target.x + (changeX + tempX) * exp;
		DGFixedPoint outputY = target.y + (changeY + tempY) * exp;

		// Prevent overshooting
		DGFixedPoint origMinusCurrentX = originalTo.x - current.x;
		DGFixedPoint origMinusCurrentY = originalTo.y - current.y;
		DGFixedPoint outMinusOrigX = outputX - originalTo.x;
		DGFixedPoint outMinusOrigY = outputY - originalTo.y;

		if (origMinusCurrentX * outMinusOrigX + origMinusCurrentY * outMinusOrigY > (DGFixedPoint) 0)
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
		DGFixedPoint factor = (DGFixedPoint) (-2) * Dot(inNormal, inDirection);
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
		DGFixedPoint rate = DGFixedPoint.One / magnitude;
		return value * rate;
	}

	/// <summary>
	/// 点乘
	/// </summary>
	/// <param name="v"></param>
	/// <returns></returns>
	public static DGFixedPoint Dot(DGVector2 v1, DGVector2 v2)
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
	public static DGFixedPoint Angle(DGVector2 fromAngle, DGVector2 toAngle)
	{
		// sqrt(a) * sqrt(b) = sqrt(a * b) -- valid for real numbers
		DGFixedPoint denominator = fromAngle.magnitude * toAngle.magnitude;
		if (denominator < kEpsilonNormalSqrt)
			return DGFixedPoint.Zero;
		DGFixedPoint dot = DGMath.Clamp(Dot(fromAngle, toAngle) / denominator, DGFixedPoint.NegativeOne, DGFixedPoint.One);
		return DGMath.Acos(dot) * DGMath.Rad2Deg;
	}

	public static DGFixedPoint SignedAngle(DGVector2 from, DGVector2 to)
	{
		DGFixedPoint unsignedAngle = Angle(from, to);
		DGFixedPoint sign = (DGFixedPoint) DGMath.Sign(from.x * to.y - from.y * to.x);
		return unsignedAngle * sign;
	}

	public static DGFixedPoint Distance(DGVector2 a, DGVector2 b)
	{
		DGFixedPoint diffX = a.x - b.x;
		DGFixedPoint diffY = a.y - b.y;
		return DGMath.Sqrt(diffX * diffX + diffY * diffY);
	}

	public static DGVector2 ClampMagnitude(DGVector2 vector, DGFixedPoint maxLength)
	{
		DGFixedPoint sqrMagnitude = vector.sqrMagnitude;
		if (sqrMagnitude <= maxLength * maxLength)
			return vector;
		//these intermediate variables force the intermediate result to be
		//of float precision. without this, the intermediate result can be of higher
		//precision, which changes behavior.
		DGFixedPoint mag = DGMath.Sqrt(sqrMagnitude);
		DGFixedPoint normalizedX = vector.x / mag;
		DGFixedPoint normalizedY = vector.y / mag;
		return new DGVector2(normalizedX * maxLength, normalizedY * maxLength);
	}

	public static DGFixedPoint Magnitude(DGVector2 vector)
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

	public static DGVector2 SmoothStep(DGVector2 v1, DGVector2 v2, DGFixedPoint amount)
	{
		amount = DGMath.Clamp01(amount);
		amount = (amount * amount) * ((DGFixedPoint) 3 - ((DGFixedPoint) 2 * amount));
		var x = v1.x + ((v2.x - v1.x) * amount);
		var y = v1.y + ((v2.y - v1.y) * amount);
		return new DGVector2(x, y);
	}

	public static DGVector2 CatmullRom(DGVector2 v1, DGVector2 v2, DGVector2 v3, DGVector2 v4, DGFixedPoint amount)
	{
		amount = DGMath.Clamp01(amount);
		DGFixedPoint squared = amount * amount;
		DGFixedPoint cubed = amount * squared;
		DGVector2 r;
		r.x = (DGFixedPoint) 2 * v2.x;
		r.x += (v3.x - v1.x) * amount;
		r.x += (((DGFixedPoint) 2 * v1.x) + ((DGFixedPoint) 4 * v3.x) - ((DGFixedPoint) 5 * v2.x) - (v4.x)) * squared;
		r.x += (((DGFixedPoint) 3 * v2.x) + (v4.x) - (v1.x) - ((DGFixedPoint) 3 * v3.x)) * cubed;
		r.x *= DGMath.Half;
		r.y = (DGFixedPoint) 2 * v2.y;
		r.y += (v3.y - v1.y) * amount;
		r.y += (((DGFixedPoint) 2 * v1.y) + ((DGFixedPoint) 4 * v3.y) - ((DGFixedPoint) 5 * v2.y) - (v4.y)) * squared;
		r.y += (((DGFixedPoint) 3 * v2.y) + (v4.y) - (v1.y) - ((DGFixedPoint) 3 * v3.y)) * cubed;
		r.y *= DGMath.Half;
		return r;
	}

	public static DGVector2 Hermite(DGVector2 v1, DGVector2 tangent1, DGVector2 v2, DGVector2 tangent2, DGFixedPoint amount)
	{
		amount = DGMath.Clamp01(amount);
		DGFixedPoint squared = amount * amount;
		DGFixedPoint cubed = amount * squared;
		DGFixedPoint a = ((cubed * (DGFixedPoint) 2) - (squared * (DGFixedPoint) 3)) + (DGFixedPoint) 1;
		DGFixedPoint b = (-cubed * (DGFixedPoint) 2) + (squared * (DGFixedPoint) 3);
		DGFixedPoint c = (cubed - (squared * (DGFixedPoint) 2)) + amount;
		DGFixedPoint d = cubed - squared;
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
		nor();
	}

	public void Scale(DGVector2 scale)
	{
		scl(scale);
	}
}