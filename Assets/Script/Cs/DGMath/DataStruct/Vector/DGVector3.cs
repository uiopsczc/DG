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
#if UNITY_STANDALONE
using UnityEngine;

#endif
public partial struct DGVector3
{
	public static readonly DGFixedPoint kEpsilon = (DGFixedPoint) 0.00001F;
	public static readonly DGFixedPoint kEpsilonNormalSqrt = (DGFixedPoint) 1e-15F;

	public static DGVector3 zero => new DGVector3(0, 0, 0);
	public static DGVector3 one => new DGVector3(1, 1, 1);
	public static DGVector3 forward => new DGVector3(0, 0, 1);
	public static DGVector3 back => new DGVector3(0, 0, -1);
	public static DGVector3 left => new DGVector3(-1, 0, 0);
	public static DGVector3 right => new DGVector3(1, 0, 0);
	public static DGVector3 up => new DGVector3(0, 1, 0);
	public static DGVector3 down => new DGVector3(0, -1, 0);
	public static DGVector3 max => new DGVector3(float.MaxValue, float.MaxValue, float.MaxValue);
	public static DGVector3 min => new DGVector3(float.MinValue, float.MinValue, float.MinValue);


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
				case 2:
					return z;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
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
				case 2:
					z = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
			}
		}
	}

	public long[] scaledValue => new[] {x.scaledValue, y.scaledValue, z.scaledValue};
	public DGVector3 abs => Abs(this);

	/// <summary>
	/// 返回当前向量长度的平方
	/// </summary>
	public DGFixedPoint sqrMagnitude => len2();

	public DGFixedPoint magnitude => len();

	/// <summary>
	/// 返回该向量的单位向量
	/// </summary>
	public DGVector3 normalized => this.nor();


	public DGVector3(float x, float y, float z)
	{
		this.x = (DGFixedPoint) x;
		this.y = (DGFixedPoint) y;
		this.z = (DGFixedPoint) z;
	}

	public DGVector3(int x, int y, int z)
	{
		this.x = (DGFixedPoint) x;
		this.y = (DGFixedPoint) y;
		this.z = (DGFixedPoint) z;
	}


#if UNITY_STANDALONE
	public DGVector3(Vector3 vector)
	{
		this.x = (DGFixedPoint) vector.x;
		this.y = (DGFixedPoint) vector.y;
		this.z = (DGFixedPoint) vector.z;
	}
#endif
	public DGVector3(System.Numerics.Vector3 vector)
	{
		this.x = (DGFixedPoint) vector.X;
		this.y = (DGFixedPoint) vector.Y;
		this.z = (DGFixedPoint) vector.Z;
	}

	/*************************************************************************************
	* 模块描述:Equals ToString
	*************************************************************************************/
	public override bool Equals(object obj)
	{
		if (obj == null)
			return false;
		var other = (DGVector3) obj;
		return Equals(other);
	}

	public bool Equals(DGVector3 other)
	{
		return other.x == x && other.y == y && other.z == z;
	}

	public override int GetHashCode()
	{
		return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
	}


	/*************************************************************************************
	* 模块描述:转换
	*************************************************************************************/
	public static implicit operator DGVector3(DGVector2 v)
	{
		return new DGVector3(v.x, v.y, (DGFixedPoint) 0);
	}

	public static implicit operator DGVector3(DGVector4 v)
	{
		return new DGVector3(v.x, v.y, v.z);
	}
#if UNITY_STANDALONE
	//转换为Unity的Vector3
	public Vector3 ToVector3()
	{
		return new Vector3((float) x, (float) y, (float) z);
	}
#endif
	/*************************************************************************************
	* 模块描述:关系运算符
	*************************************************************************************/
	public static bool operator ==(DGVector3 value1, DGVector3 value2)
	{
		// Returns false in the presence of NaN values.
		DGFixedPoint diffX = value1.x - value2.x;
		DGFixedPoint diffY = value1.y - value2.y;
		DGFixedPoint diffZ = value1.z - value2.z;
		DGFixedPoint sqrmag = diffX * diffX + diffY * diffY + diffZ * diffZ;
		return sqrmag <= kEpsilon * kEpsilon;
	}

	public static bool operator !=(DGVector3 value1, DGVector3 value2)
	{
		return !(value1 == value2);
	}

	/*************************************************************************************
	* 模块描述:操作运算
	*************************************************************************************/
	public static DGVector3 operator +(DGVector3 value1, DGVector3 value2)
	{
		DGFixedPoint x = value1.x + value2.x;
		DGFixedPoint y = value1.y + value2.y;
		DGFixedPoint z = value1.z + value2.z;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator -(DGVector3 value1, DGVector3 value2)
	{
		DGFixedPoint x = value1.x - value2.x;
		DGFixedPoint y = value1.y - value2.y;
		DGFixedPoint z = value1.z - value2.z;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator -(DGVector3 value)
	{
		DGFixedPoint x = -value.x;
		DGFixedPoint y = -value.y;
		DGFixedPoint z = -value.z;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator *(DGVector3 value1, DGVector3 value2)
	{
		DGFixedPoint x = value1.x * value2.x;
		DGFixedPoint y = value1.y * value2.y;
		DGFixedPoint z = value1.z * value2.z;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator *(DGVector3 value, DGFixedPoint multiply)
	{
		DGFixedPoint x = value.x * multiply;
		DGFixedPoint y = value.y * multiply;
		DGFixedPoint z = value.z * multiply;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator /(DGVector3 value1, DGVector3 value2)
	{
		DGFixedPoint x = value1.x / value2.x;
		DGFixedPoint y = value1.y / value2.y;
		DGFixedPoint z = value1.z / value2.z;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator /(DGVector3 value1, DGFixedPoint div)
	{
		DGFixedPoint x = value1.x / div;
		DGFixedPoint y = value1.y / div;
		DGFixedPoint z = value1.z / div;
		return new DGVector3(x, y, z);
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/

	/// <summary>
	/// 返回当前向量长度的平方
	/// </summary>
	public static DGFixedPoint SqrMagnitude(DGVector3 v)
	{
		return v.len2();
	}

	public static DGFixedPoint Magnitude(DGVector3 vector)
	{
		return vector.len();
	}

	public static DGVector3 ClampMagnitude(DGVector3 vector, DGFixedPoint maxLength)
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
		DGFixedPoint normalizedZ = vector.z / mag;
		return new DGVector3(normalizedX * maxLength, normalizedY * maxLength, normalizedZ * maxLength);
	}

	public static DGFixedPoint Distance(DGVector3 a, DGVector3 b)
	{
		return a.dst(b);
	}

	public static (DGVector3, DGVector3) OrthoNormalize(DGVector3 va, DGVector3 vb)
	{
		va.Normalize();
		vb = vb - Project(vb, va);
		vb.Normalize();
		return (va, vb);
	}

	public static (DGVector3, DGVector3, DGVector3) OrthoNormalize(DGVector3 va, DGVector3 vb, DGVector3 vc)
	{
		va.Normalize();
		vb = vb - Project(vb, va);
		vb.Normalize();
		vc = vc - Project(vc, va);
		vc = vc - Project(vc, vb);
		vc.Normalize();
		return (va, vb, vc);
	}

	public static DGVector3 OrthoNormalVector(DGVector3 vec)
	{
		DGFixedPoint a = default;
		DGFixedPoint k = default;
		DGFixedPoint x = default;
		DGFixedPoint y = default;
		DGFixedPoint z = default;
		if (DGMath.Abs(vec.z) > DGMath.HalfSqrt2)
		{
			a = vec.y * vec.y + vec.z * vec.z;
			k = (DGFixedPoint) 1 / DGMath.Sqrt(a);
			x = (DGFixedPoint) 0;
			y = -vec.z * k;
			z = vec.y * k;
			return new DGVector3(x, y, z);
		}

		a = vec.x * vec.x + vec.y * vec.y;
		k = (DGFixedPoint) 1 / DGMath.Sqrt(a);
		x = -vec.y * k;
		y = vec.x * k;
		z = (DGFixedPoint) 0;


		return new DGVector3(x, y, z);
	}


	public static DGVector3 RotateTowards(DGVector3 current, DGVector3 target, DGFixedPoint maxRadiansDelta, DGFixedPoint maxMagnitudeDelta)
	{
		var len1 = current.magnitude;
		var len2 = target.magnitude;

		if (len1 > DGMath.Epsilon && len2 > DGMath.Epsilon)
		{
			var from = current / len1;
			var to = target / len2;
			var cosom = Dot(from, to);
			if (cosom > (DGFixedPoint) 1 - DGMath.Epsilon)
				return MoveTowards(current, target, maxMagnitudeDelta);
			if (cosom < (DGFixedPoint) (-1) + DGMath.Epsilon)
			{
				var axis = OrthoNormalVector(@from);
				var q = DGQuaternion.AngleAxis(maxRadiansDelta * DGMath.Rad2Deg, axis);
				var rotated = q * @from;
				var delta = DGMath.ClampedMove(len1, len2, maxMagnitudeDelta);
				rotated = rotated * delta;
				return rotated;
			}

			{
				var angle = DGMath.Acos(cosom);
				var axis = Cross(@from, to);
				axis.Normalize();
				var q = DGQuaternion.AngleAxis(DGMath.Min(maxRadiansDelta, angle) * DGMath.Rad2Deg, axis);
				var rotated = q * @from;
				var delta = DGMath.ClampedMove(len1, len2, maxMagnitudeDelta);
				rotated = rotated * delta;
				return rotated;
			}
		}

		return MoveTowards(current, target, maxMagnitudeDelta);
	}

	public static DGVector3 Lerp(DGVector3 a, DGVector3 b, DGFixedPoint t)
	{
		t = DGMath.Clamp01(t);
		return new DGVector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
	}

	public static DGVector3 LerpUnclamped(DGVector3 a, DGVector3 b, DGFixedPoint t)
	{
		return new DGVector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
	}

	public static DGVector3 MoveTowards(DGVector3 current, DGVector3 target, DGFixedPoint maxDistanceDelta)
	{
		DGFixedPoint toVectorX = target.x - current.x;
		DGFixedPoint toVectorY = target.y - current.y;
		DGFixedPoint toVectorZ = target.z - current.z;
		DGFixedPoint sqrtDistance = toVectorX * toVectorX + toVectorY * toVectorY + toVectorZ * toVectorZ;
		if (sqrtDistance == DGFixedPoint.Zero ||
		    maxDistanceDelta >= DGFixedPoint.Zero && sqrtDistance <= maxDistanceDelta * maxDistanceDelta)
			return target;
		DGFixedPoint distance = DGMath.Sqrt(sqrtDistance);
		return new DGVector3(current.x + toVectorX / distance * maxDistanceDelta,
			current.y + toVectorY / distance * maxDistanceDelta, current.z + toVectorZ / distance * maxDistanceDelta);
	}

#if UNITY_STANDALONE
	public static DGVector3 SmoothDamp(DGVector3 current, DGVector3 target, ref DGVector3 currentVelocity,
		DGFixedPoint smoothTime, DGFixedPoint maxSpeed)
	{
		DGFixedPoint deltaTime = (DGFixedPoint) Time.deltaTime;
		return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}

	public static DGVector3 SmoothDamp(DGVector3 current, DGVector3 target, ref DGVector3 currentVelocity,
		DGFixedPoint smoothTime)
	{
		DGFixedPoint deltaTime = (DGFixedPoint) Time.deltaTime;
		DGFixedPoint maxSpeed = DGFixedPoint.MaxValue;
		return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}
#endif

	public static DGVector3 SmoothDamp(DGVector3 current, DGVector3 target, ref DGVector3 currentVelocity,
		DGFixedPoint smoothTime, DGFixedPoint maxSpeed, DGFixedPoint deltaTime)
	{
		DGFixedPoint outputX = (DGFixedPoint) 0;
		DGFixedPoint outputY = (DGFixedPoint) 0;
		DGFixedPoint outputZ = (DGFixedPoint) 0;

		// Based on Game Programming Gems 4 Chapter 1.10
		smoothTime = DGMath.Max((DGFixedPoint) 0.0001F, smoothTime);
		DGFixedPoint omega = (DGFixedPoint) 2 / smoothTime;

		DGFixedPoint x = omega * deltaTime;
		DGFixedPoint exp = (DGFixedPoint) 1 / ((DGFixedPoint) 1 + x + (DGFixedPoint) 0.48f * x * x + (DGFixedPoint) 0.235f * x * x * x);

		DGFixedPoint changeX = current.x - target.x;
		DGFixedPoint changeY = current.y - target.y;
		DGFixedPoint changeZ = current.z - target.z;
		DGVector3 originalTo = target;

		// Clamp maximum speed
		DGFixedPoint maxChange = maxSpeed * smoothTime;

		DGFixedPoint maxChangeSq = maxChange * maxChange;
		DGFixedPoint sqrmag = changeX * changeX + changeY * changeY + changeZ * changeZ;
		if (sqrmag > maxChangeSq)
		{
			var mag = DGMath.Sqrt(sqrmag);
			changeX = changeX / mag * maxChange;
			changeY = changeY / mag * maxChange;
			changeZ = changeZ / mag * maxChange;
		}

		target.x = current.x - changeX;
		target.y = current.y - changeY;
		target.z = current.z - changeZ;

		DGFixedPoint tempX = (currentVelocity.x + omega * changeX) * deltaTime;
		DGFixedPoint tempY = (currentVelocity.y + omega * changeY) * deltaTime;
		DGFixedPoint tempZ = (currentVelocity.z + omega * changeZ) * deltaTime;

		currentVelocity.x = (currentVelocity.x - omega * tempX) * exp;
		currentVelocity.y = (currentVelocity.y - omega * tempY) * exp;
		currentVelocity.z = (currentVelocity.z - omega * tempZ) * exp;

		outputX = target.x + (changeX + tempX) * exp;
		outputY = target.y + (changeY + tempY) * exp;
		outputZ = target.z + (changeZ + tempZ) * exp;

		// Prevent overshooting
		DGFixedPoint origMinusCurrentX = originalTo.x - current.x;
		DGFixedPoint origMinusCurrentY = originalTo.y - current.y;
		DGFixedPoint origMinusCurrentZ = originalTo.z - current.z;
		DGFixedPoint outMinusOrigX = outputX - originalTo.x;
		DGFixedPoint outMinusOrigY = outputY - originalTo.y;
		DGFixedPoint outMinusOrigZ = outputZ - originalTo.z;

		if (origMinusCurrentX * outMinusOrigX + origMinusCurrentY * outMinusOrigY + origMinusCurrentZ * outMinusOrigZ >
		    (DGFixedPoint) 0)
		{
			outputX = originalTo.x;
			outputY = originalTo.y;
			outputZ = originalTo.z;

			currentVelocity.x = (outputX - originalTo.x) / deltaTime;
			currentVelocity.y = (outputY - originalTo.y) / deltaTime;
			currentVelocity.z = (outputZ - originalTo.z) / deltaTime;
		}

		return new DGVector3(outputX, outputY, outputZ);
	}

	public static DGVector3 Scale(DGVector3 a, DGVector3 b)
	{
		return new DGVector3(a.x * b.x, a.y * b.y, a.z * b.z);
	}

	public static DGVector3 Reflect(DGVector3 inDirection, DGVector3 inNormal)
	{
		DGFixedPoint factor = (DGFixedPoint) (-2) * Dot(inNormal, inDirection);
		return new DGVector3(factor * inNormal.x + inDirection.x, factor * inNormal.y + inDirection.y,
			factor * inNormal.z + inDirection.z);
	}

	/// <summary>
	/// 单位化该向量
	/// </summary>
	/// <param name="v"></param>
	public static DGVector3 Normalize(DGVector3 value)
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
	public static DGFixedPoint Dot(DGVector3 v1, DGVector3 v2)
	{
		return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
	}

	/// <summary>
	/// 叉乘
	/// </summary>
	/// <param name="v1"></param>
	/// <param name="v2"></param>
	/// <returns></returns>GetHashCode
	public static DGVector3 Cross(DGVector3 v1, DGVector3 v2)
	{
		return new DGVector3(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
	}

	public static DGVector3 Project(DGVector3 vector, DGVector3 onNormal)
	{
		DGFixedPoint sqrMag = Dot(onNormal, onNormal);
		if (sqrMag < DGMath.Epsilon)
			return zero;
		DGFixedPoint dot = Dot(vector, onNormal);
		return new DGVector3(onNormal.x * dot / sqrMag, onNormal.y * dot / sqrMag, onNormal.z * dot / sqrMag);
	}

	public static DGVector3 ProjectOnPlane(DGVector3 vector, DGVector3 planeNormal)
	{
		DGFixedPoint sqrMag = Dot(planeNormal, planeNormal);
		if (sqrMag < DGMath.Epsilon)
			return vector;
		DGFixedPoint dot = Dot(vector, planeNormal);
		return new DGVector3(vector.x - planeNormal.x * dot / sqrMag, vector.y - planeNormal.y * dot / sqrMag,
			vector.z - planeNormal.z * dot / sqrMag);
	}

	/// <summary>
	/// 求两个向量的夹角，没有正负区分
	/// </summary>
	/// <param name="fromAngle"></param>
	/// <param name="toAngle"></param>
	/// <returns></returns>
	public static DGFixedPoint Angle(DGVector3 fromAngle, DGVector3 toAngle)
	{
		// sqrt(a) * sqrt(b) = sqrt(a * b) -- valid for real numbers
		DGFixedPoint denominator = fromAngle.magnitude * toAngle.magnitude;
		if (denominator <= kEpsilonNormalSqrt)
			return DGFixedPoint.Zero;
		DGFixedPoint dot = DGMath.Clamp(Dot(fromAngle, toAngle) / denominator, DGFixedPoint.NegativeOne, DGFixedPoint.One);
		return DGMath.Acos(dot) * DGMath.Rad2Deg;
	}

	public static DGFixedPoint SignedAngle(DGVector3 from, DGVector3 to, DGVector3 axis)
	{
		DGFixedPoint unsignedAngle = Angle(from, to);

		DGFixedPoint crossX = from.y * to.z - from.z * to.y;
		DGFixedPoint crossY = from.z * to.x - from.x * to.z;
		DGFixedPoint crossZ = from.x * to.y - from.y * to.x;
		DGFixedPoint sign = (DGFixedPoint) DGMath.Sign(axis.x * crossX + axis.y * crossY + axis.z * crossZ);
		return unsignedAngle * sign;
	}

	public static DGVector3 Min(DGVector3 value1, DGVector3 value2)
	{
		return new DGVector3(DGMath.Min(value1.x, value2.x), DGMath.Min(value1.y, value2.y),
			DGMath.Min(value1.z, value2.z));
	}

	public static DGVector3 Max(DGVector3 value1, DGVector3 value2)
	{
		return new DGVector3(DGMath.Max(value1.x, value2.x), DGMath.Max(value1.y, value2.y),
			DGMath.Max(value1.z, value2.z));
	}

	public static DGVector3 Abs(DGVector3 value)
	{
		return new DGVector3(DGMath.Abs(value.x), DGMath.Abs(value.y), DGMath.Abs(value.z));
	}

	public static DGVector3 SmoothStep(DGVector3 v1, DGVector3 v2, DGFixedPoint amount)
	{
		amount = DGMath.Clamp01(amount);
		amount = (amount * amount) * ((DGFixedPoint) 3 - ((DGFixedPoint) 2 * amount));
		var x = v1.x + ((v2.x - v1.x) * amount);
		var y = v1.y + ((v2.y - v1.y) * amount);
		var z = v1.z + ((v2.z - v1.z) * amount);
		return new DGVector3(x, y, z);
	}

	public static DGVector3 CatmullRom(DGVector3 v1, DGVector3 v2, DGVector3 v3, DGVector3 v4, DGFixedPoint amount)
	{
		amount = DGMath.Clamp01(amount);
		DGFixedPoint squared = amount * amount;
		DGFixedPoint cubed = amount * squared;
		DGVector3 r = default;
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
		r.z = (DGFixedPoint) 2 * v2.z;
		r.z += (v3.z - v1.z) * amount;
		r.z += (((DGFixedPoint) 2 * v1.z) + ((DGFixedPoint) 4 * v3.z) - ((DGFixedPoint) 5 * v2.z) - (v4.z)) * squared;
		r.z += (((DGFixedPoint) 3 * v2.z) + (v4.z) - (v1.z) - ((DGFixedPoint) 3 * v3.z)) * cubed;
		r.z *= DGMath.Half;
		return r;
	}


	/// <summary>
	/// Computes an intermediate location using hermite interpolation.
	/// </summary>
	/// <param name="value1">First position.</param>
	/// <param name="tangent1">Tangent associated with the first position.</param>
	/// <param name="value2">Second position.</param>
	/// <param name="tangent2">Tangent associated with the second position.</param>
	/// <param name="interpolationAmount">Amount of the second point to use.</param>
	/// <param name="result">Interpolated intermediate state.</param>
	public static DGVector3 Hermite(DGVector3 value1, DGVector3 tangent1, DGVector3 value2, DGVector3 tangent2,
		DGFixedPoint interpolationAmount)
	{
		DGFixedPoint weightSquared = interpolationAmount * interpolationAmount;
		DGFixedPoint weightCubed = interpolationAmount * weightSquared;
		DGFixedPoint value1Blend = (DGFixedPoint) 2 * weightCubed - (DGFixedPoint) 3 * weightSquared + (DGFixedPoint) 1;
		DGFixedPoint tangent1Blend = weightCubed - (DGFixedPoint) 2 * weightSquared + interpolationAmount;
		DGFixedPoint value2Blend = (DGFixedPoint) (-2) * weightCubed + (DGFixedPoint) 3 * weightSquared;
		DGFixedPoint tangent2Blend = weightCubed - weightSquared;
		DGFixedPoint x = value1.x * value1Blend + value2.x * value2Blend + tangent1.x * tangent1Blend +
		       tangent2.x * tangent2Blend;
		DGFixedPoint y = value1.y * value1Blend + value2.y * value2Blend + tangent1.y * tangent1Blend +
		       tangent2.y * tangent2Blend;
		DGFixedPoint z = value1.z * value1Blend + value2.z * value2Blend + tangent1.z * tangent1Blend +
		       tangent2.z * tangent2Blend;
		return new DGVector3(x, y, z);
	}

	public static DGFixedPoint AngleAroundAxis(DGVector3 from, DGVector3 to, DGVector3 axis)
	{
		from = from - Project(from, axis);
		to = to - Project(to, axis);
		var angle = Angle(from, to);
		return angle * (Dot(axis, Cross(from, to)) < (DGFixedPoint) 0 ? (DGFixedPoint) (-1) : (DGFixedPoint) 1);
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
			x = (DGFixedPoint) 0;
			y = (DGFixedPoint) 0;
			z = (DGFixedPoint) 0;
			return;
		}

		DGFixedPoint rate = DGFixedPoint.One / magnitude;
		x *= rate;
		y *= rate;
		z *= rate;
	}

	public void Scale(DGVector3 scale)
	{
		this.x *= scale.x;
		this.y *= scale.y;
		this.z *= scale.z;
	}

	public void Abs()
	{
		var abs = Abs(this);
		x = abs.x;
		y = abs.y;
		z = abs.z;
	}
}