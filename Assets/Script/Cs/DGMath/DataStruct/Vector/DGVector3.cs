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
using FPVector2 = DGVector2;
using FPVector4 = DGVector4;
using FPQuaternion = DGQuaternion;
#if UNITY_5_3_OR_NEWER
using UnityEngine;

#endif
public partial struct DGVector3
{
	public static readonly FP kEpsilon = (FP) 0.00001F;
	public static readonly FP kEpsilonNormalSqrt = (FP) 1e-15F;

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
	public FP sqrMagnitude => len2();

	public FP magnitude => len();

	/// <summary>
	/// 返回该向量的单位向量
	/// </summary>
	public DGVector3 normalized => this.nor();


	public DGVector3(float x, float y, float z)
	{
		this.x = (FP) x;
		this.y = (FP) y;
		this.z = (FP) z;
	}

	public DGVector3(int x, int y, int z)
	{
		this.x = (FP) x;
		this.y = (FP) y;
		this.z = (FP) z;
	}


#if UNITY_5_3_OR_NEWER
	public DGVector3(Vector3 vector)
	{
		this.x = (FP) vector.x;
		this.y = (FP) vector.y;
		this.z = (FP) vector.z;
	}
#endif
	public DGVector3(System.Numerics.Vector3 vector)
	{
		this.x = (FP) vector.X;
		this.y = (FP) vector.Y;
		this.z = (FP) vector.Z;
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
	public static implicit operator DGVector3(FPVector2 v)
	{
		return new DGVector3(v.x, v.y, (FP) 0);
	}

	public static implicit operator DGVector3(FPVector4 v)
	{
		return new DGVector3(v.x, v.y, v.z);
	}
#if UNITY_5_3_OR_NEWER
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
		FP diffX = value1.x - value2.x;
		FP diffY = value1.y - value2.y;
		FP diffZ = value1.z - value2.z;
		FP sqrmag = diffX * diffX + diffY * diffY + diffZ * diffZ;
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
		FP x = value1.x + value2.x;
		FP y = value1.y + value2.y;
		FP z = value1.z + value2.z;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator -(DGVector3 value1, DGVector3 value2)
	{
		FP x = value1.x - value2.x;
		FP y = value1.y - value2.y;
		FP z = value1.z - value2.z;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator -(DGVector3 value)
	{
		FP x = -value.x;
		FP y = -value.y;
		FP z = -value.z;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator *(DGVector3 value1, DGVector3 value2)
	{
		FP x = value1.x * value2.x;
		FP y = value1.y * value2.y;
		FP z = value1.z * value2.z;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator *(DGVector3 value, FP multiply)
	{
		FP x = value.x * multiply;
		FP y = value.y * multiply;
		FP z = value.z * multiply;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator /(DGVector3 value1, DGVector3 value2)
	{
		FP x = value1.x / value2.x;
		FP y = value1.y / value2.y;
		FP z = value1.z / value2.z;
		return new DGVector3(x, y, z);
	}

	public static DGVector3 operator /(DGVector3 value1, FP div)
	{
		FP x = value1.x / div;
		FP y = value1.y / div;
		FP z = value1.z / div;
		return new DGVector3(x, y, z);
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/

	/// <summary>
	/// 返回当前向量长度的平方
	/// </summary>
	public static FP SqrMagnitude(DGVector3 v)
	{
		return v.len2();
	}

	public static FP Magnitude(DGVector3 vector)
	{
		return vector.len();
	}

	public static DGVector3 ClampMagnitude(DGVector3 vector, FP maxLength)
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
		FP normalizedZ = vector.z / mag;
		return new DGVector3(normalizedX * maxLength, normalizedY * maxLength, normalizedZ * maxLength);
	}

	public static FP Distance(DGVector3 a, DGVector3 b)
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
		FP a = default;
		FP k = default;
		FP x = default;
		FP y = default;
		FP z = default;
		if (DGMath.Abs(vec.z) > DGMath.HalfSqrt2)
		{
			a = vec.y * vec.y + vec.z * vec.z;
			k = (FP) 1 / DGMath.Sqrt(a);
			x = (FP) 0;
			y = -vec.z * k;
			z = vec.y * k;
			return new DGVector3(x, y, z);
		}

		a = vec.x * vec.x + vec.y * vec.y;
		k = (FP) 1 / DGMath.Sqrt(a);
		x = -vec.y * k;
		y = vec.x * k;
		z = (FP) 0;


		return new DGVector3(x, y, z);
	}


	public static DGVector3 RotateTowards(DGVector3 current, DGVector3 target, FP maxRadiansDelta, FP maxMagnitudeDelta)
	{
		var len1 = current.magnitude;
		var len2 = target.magnitude;

		if (len1 > DGMath.Epsilon && len2 > DGMath.Epsilon)
		{
			var from = current / len1;
			var to = target / len2;
			var cosom = Dot(from, to);
			if (cosom > (FP) 1 - DGMath.Epsilon)
				return MoveTowards(current, target, maxMagnitudeDelta);
			if (cosom < (FP) (-1) + DGMath.Epsilon)
			{
				var axis = OrthoNormalVector(@from);
				var q = FPQuaternion.AngleAxis(maxRadiansDelta * DGMath.Rad2Deg, axis);
				var rotated = q * @from;
				var delta = DGMath.ClampedMove(len1, len2, maxMagnitudeDelta);
				rotated = rotated * delta;
				return rotated;
			}

			{
				var angle = DGMath.Acos(cosom);
				var axis = Cross(@from, to);
				axis.Normalize();
				var q = FPQuaternion.AngleAxis(DGMath.Min(maxRadiansDelta, angle) * DGMath.Rad2Deg, axis);
				var rotated = q * @from;
				var delta = DGMath.ClampedMove(len1, len2, maxMagnitudeDelta);
				rotated = rotated * delta;
				return rotated;
			}
		}

		return MoveTowards(current, target, maxMagnitudeDelta);
	}

	public static DGVector3 Lerp(DGVector3 a, DGVector3 b, FP t)
	{
		t = DGMath.Clamp01(t);
		return new DGVector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
	}

	public static DGVector3 LerpUnclamped(DGVector3 a, DGVector3 b, FP t)
	{
		return new DGVector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
	}

	public static DGVector3 MoveTowards(DGVector3 current, DGVector3 target, FP maxDistanceDelta)
	{
		FP toVectorX = target.x - current.x;
		FP toVectorY = target.y - current.y;
		FP toVectorZ = target.z - current.z;
		FP sqrtDistance = toVectorX * toVectorX + toVectorY * toVectorY + toVectorZ * toVectorZ;
		if (sqrtDistance == FP.Zero ||
		    maxDistanceDelta >= FP.Zero && sqrtDistance <= maxDistanceDelta * maxDistanceDelta)
			return target;
		FP distance = DGMath.Sqrt(sqrtDistance);
		return new DGVector3(current.x + toVectorX / distance * maxDistanceDelta,
			current.y + toVectorY / distance * maxDistanceDelta, current.z + toVectorZ / distance * maxDistanceDelta);
	}

#if UNITY_5_3_OR_NEWER
	public static DGVector3 SmoothDamp(DGVector3 current, DGVector3 target, ref DGVector3 currentVelocity,
		FP smoothTime, FP maxSpeed)
	{
		FP deltaTime = (FP) Time.deltaTime;
		return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}

	public static DGVector3 SmoothDamp(DGVector3 current, DGVector3 target, ref DGVector3 currentVelocity,
		FP smoothTime)
	{
		FP deltaTime = (FP) Time.deltaTime;
		FP maxSpeed = FP.MaxValue;
		return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}
#endif

	public static DGVector3 SmoothDamp(DGVector3 current, DGVector3 target, ref DGVector3 currentVelocity,
		FP smoothTime, FP maxSpeed, FP deltaTime)
	{
		FP outputX = (FP) 0;
		FP outputY = (FP) 0;
		FP outputZ = (FP) 0;

		// Based on Game Programming Gems 4 Chapter 1.10
		smoothTime = DGMath.Max((FP) 0.0001F, smoothTime);
		FP omega = (FP) 2 / smoothTime;

		FP x = omega * deltaTime;
		FP exp = (FP) 1 / ((FP) 1 + x + (FP) 0.48f * x * x + (FP) 0.235f * x * x * x);

		FP changeX = current.x - target.x;
		FP changeY = current.y - target.y;
		FP changeZ = current.z - target.z;
		DGVector3 originalTo = target;

		// Clamp maximum speed
		FP maxChange = maxSpeed * smoothTime;

		FP maxChangeSq = maxChange * maxChange;
		FP sqrmag = changeX * changeX + changeY * changeY + changeZ * changeZ;
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

		FP tempX = (currentVelocity.x + omega * changeX) * deltaTime;
		FP tempY = (currentVelocity.y + omega * changeY) * deltaTime;
		FP tempZ = (currentVelocity.z + omega * changeZ) * deltaTime;

		currentVelocity.x = (currentVelocity.x - omega * tempX) * exp;
		currentVelocity.y = (currentVelocity.y - omega * tempY) * exp;
		currentVelocity.z = (currentVelocity.z - omega * tempZ) * exp;

		outputX = target.x + (changeX + tempX) * exp;
		outputY = target.y + (changeY + tempY) * exp;
		outputZ = target.z + (changeZ + tempZ) * exp;

		// Prevent overshooting
		FP origMinusCurrentX = originalTo.x - current.x;
		FP origMinusCurrentY = originalTo.y - current.y;
		FP origMinusCurrentZ = originalTo.z - current.z;
		FP outMinusOrigX = outputX - originalTo.x;
		FP outMinusOrigY = outputY - originalTo.y;
		FP outMinusOrigZ = outputZ - originalTo.z;

		if (origMinusCurrentX * outMinusOrigX + origMinusCurrentY * outMinusOrigY + origMinusCurrentZ * outMinusOrigZ >
		    (FP) 0)
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
		FP factor = (FP) (-2) * Dot(inNormal, inDirection);
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

		FP rate = FP.One / magnitude;
		return value * rate;
	}

	/// <summary>
	/// 点乘
	/// </summary>
	/// <param name="v"></param>
	/// <returns></returns>
	public static FP Dot(DGVector3 v1, DGVector3 v2)
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
		FP sqrMag = Dot(onNormal, onNormal);
		if (sqrMag < DGMath.Epsilon)
			return zero;
		FP dot = Dot(vector, onNormal);
		return new DGVector3(onNormal.x * dot / sqrMag, onNormal.y * dot / sqrMag, onNormal.z * dot / sqrMag);
	}

	public static DGVector3 ProjectOnPlane(DGVector3 vector, DGVector3 planeNormal)
	{
		FP sqrMag = Dot(planeNormal, planeNormal);
		if (sqrMag < DGMath.Epsilon)
			return vector;
		FP dot = Dot(vector, planeNormal);
		return new DGVector3(vector.x - planeNormal.x * dot / sqrMag, vector.y - planeNormal.y * dot / sqrMag,
			vector.z - planeNormal.z * dot / sqrMag);
	}

	/// <summary>
	/// 求两个向量的夹角，没有正负区分
	/// </summary>
	/// <param name="fromAngle"></param>
	/// <param name="toAngle"></param>
	/// <returns></returns>
	public static FP Angle(DGVector3 fromAngle, DGVector3 toAngle)
	{
		// sqrt(a) * sqrt(b) = sqrt(a * b) -- valid for real numbers
		FP denominator = DGMath.Sqrt(fromAngle.sqrMagnitude * toAngle.sqrMagnitude);
		if (denominator <= kEpsilonNormalSqrt)
			return FP.Zero;

		FP dot = DGMath.Clamp(Dot(fromAngle, toAngle) / denominator, FP.NegativeOne, FP.One);
		return DGMath.Acos(dot) * DGMath.Rad2Deg;
	}

	public static FP SignedAngle(DGVector3 from, DGVector3 to, DGVector3 axis)
	{
		FP unsignedAngle = Angle(from, to);

		FP crossX = from.y * to.z - from.z * to.y;
		FP crossY = from.z * to.x - from.x * to.z;
		FP crossZ = from.x * to.y - from.y * to.x;
		FP sign = (FP) DGMath.Sign(axis.x * crossX + axis.y * crossY + axis.z * crossZ);
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

	public static DGVector3 SmoothStep(DGVector3 v1, DGVector3 v2, FP amount)
	{
		amount = DGMath.Clamp01(amount);
		amount = (amount * amount) * ((FP) 3 - ((FP) 2 * amount));
		var x = v1.x + ((v2.x - v1.x) * amount);
		var y = v1.y + ((v2.y - v1.y) * amount);
		var z = v1.z + ((v2.z - v1.z) * amount);
		return new DGVector3(x, y, z);
	}

	public static DGVector3 CatmullRom(DGVector3 v1, DGVector3 v2, DGVector3 v3, DGVector3 v4, FP amount)
	{
		amount = DGMath.Clamp01(amount);
		FP squared = amount * amount;
		FP cubed = amount * squared;
		DGVector3 r = default;
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
		r.z = (FP) 2 * v2.z;
		r.z += (v3.z - v1.z) * amount;
		r.z += (((FP) 2 * v1.z) + ((FP) 4 * v3.z) - ((FP) 5 * v2.z) - (v4.z)) * squared;
		r.z += (((FP) 3 * v2.z) + (v4.z) - (v1.z) - ((FP) 3 * v3.z)) * cubed;
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
		FP interpolationAmount)
	{
		FP weightSquared = interpolationAmount * interpolationAmount;
		FP weightCubed = interpolationAmount * weightSquared;
		FP value1Blend = (FP) 2 * weightCubed - (FP) 3 * weightSquared + (FP) 1;
		FP tangent1Blend = weightCubed - (FP) 2 * weightSquared + interpolationAmount;
		FP value2Blend = (FP) (-2) * weightCubed + (FP) 3 * weightSquared;
		FP tangent2Blend = weightCubed - weightSquared;
		FP x = value1.x * value1Blend + value2.x * value2Blend + tangent1.x * tangent1Blend +
		       tangent2.x * tangent2Blend;
		FP y = value1.y * value1Blend + value2.y * value2Blend + tangent1.y * tangent1Blend +
		       tangent2.y * tangent2Blend;
		FP z = value1.z * value1Blend + value2.z * value2Blend + tangent1.z * tangent1Blend +
		       tangent2.z * tangent2Blend;
		return new DGVector3(x, y, z);
	}

	public static FP AngleAroundAxis(DGVector3 from, DGVector3 to, DGVector3 axis)
	{
		from = from - Project(from, axis);
		to = to - Project(to, axis);
		var angle = Angle(from, to);
		return angle * (Dot(axis, Cross(from, to)) < (FP) 0 ? (FP) (-1) : (FP) 1);
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
			z = (FP) 0;
			return;
		}

		FP rate = FP.One / magnitude;
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