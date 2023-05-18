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
using FPVector3 = DGVector3;
#if UNITY_5_3_OR_NEWER
using UnityEngine;

#endif
public struct DGVector4 : IEquatable<DGVector4>
{
	public static readonly FP kEpsilon = (FP) 0.00001F;
	public static readonly FP kEpsilonNormalSqrt = (FP) 1e-15F;

	public static DGVector4 zero => new DGVector4(0, 0, 0, 0);
	public static DGVector4 one => new DGVector4(1, 1, 1, 1);
	public static DGVector4 max => new DGVector4(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);
	public static DGVector4 min => new DGVector4(float.MinValue, float.MinValue, float.MinValue, float.MinValue);

	public FP x;
	public FP y;
	public FP z;
	public FP w;


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
				case 3:
					return w;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
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
				case 3:
					w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
			}
		}
	}

	public long[] scaledValue => new[] {x.scaledValue, y.scaledValue, z.scaledValue, w.scaledValue};
	public DGVector4 abs => Abs(this);

	/// <summary>
	/// 返回当前向量长度的平方
	/// </summary>
	public FP sqrMagnitude => x * x + y * y + z * z + w * w;

	public FP magnitude => DGMath.Sqrt(sqrMagnitude);

	/// <summary>
	/// 返回该向量的单位向量
	/// </summary>
	public DGVector4 normalized => Normalize(this);

	public DGVector4(FP x, FP y, FP z, FP w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}

	public DGVector4(float x, float y, float z, float w)
	{
		this.x = (FP) x;
		this.y = (FP) y;
		this.z = (FP) z;
		this.w = (FP) w;
	}

	public DGVector4(int x, int y, int z, int w)
	{
		this.x = (FP) x;
		this.y = (FP) y;
		this.z = (FP) z;
		this.w = (FP) w;
	}

#if UNITY_5_3_OR_NEWER
	public DGVector4(Vector4 vector)
	{
		this.x = (FP) vector.x;
		this.y = (FP) vector.y;
		this.z = (FP) vector.z;
		this.w = (FP) vector.w;
	}
#endif
	/*************************************************************************************
	* 模块描述:Equals ToString
	*************************************************************************************/
	public override bool Equals(object obj)
	{
		if (obj == null)
			return false;
		var other = (DGVector4) obj;
		return Equals(other);
	}

	public bool Equals(DGVector4 other)
	{
		return other.x == x && other.y == y && other.z == z && other.w == w;
	}

	public override int GetHashCode()
	{
		return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
	}

	public override string ToString()
	{
		return string.Format("x:{0},y:{1},z:{2},w{3}", x, y, z, w);
	}

	/*************************************************************************************
	* 模块描述:转换
	*************************************************************************************/
	public static implicit operator DGVector4(FPVector2 v)
	{
		return new DGVector4(v.x, v.y, (FP) 0, (FP) 0);
	}

	public static implicit operator DGVector4(FPVector3 v)
	{
		return new DGVector4(v.x, v.y, v.z, (FP) 0);
	}
#if UNITY_5_3_OR_NEWER
	//转换为Unity的Vector4
	public Vector4 ToVector4()
	{
		return new Vector4((float) x, (float) y, (float) z, (float) w);
	}
#endif

	/*************************************************************************************
	* 模块描述:关系运算符
	*************************************************************************************/
	public static bool operator ==(DGVector4 value1, DGVector4 value2)
	{
		// Returns false in the presence of NaN values.
		FP diffX = value1.x - value2.x;
		FP diffY = value1.y - value2.y;
		FP diffZ = value1.z - value2.z;
		FP diffW = value1.w - value2.w;
		FP sqrmag = diffX * diffX + diffY * diffY + diffZ * diffZ + diffW * diffW;
		return sqrmag <= kEpsilon * kEpsilon;
	}

	public static bool operator !=(DGVector4 value1, DGVector4 value2)
	{
		return !(value1 == value2);
	}

	/*************************************************************************************
	* 模块描述:操作运算
	*************************************************************************************/
	public static DGVector4 operator +(DGVector4 value1, DGVector4 value2)
	{
		FP x = value1.x + value2.x;
		FP y = value1.y + value2.y;
		FP z = value1.z + value2.z;
		FP w = value1.w + value2.w;
		return new DGVector4(x, y, z, w);
	}

	public static DGVector4 operator -(DGVector4 value1, DGVector4 value2)
	{
		FP x = value1.x - value2.x;
		FP y = value1.y - value2.y;
		FP z = value1.z - value2.z;
		FP w = value1.w - value2.w;
		return new DGVector4(x, y, z, w);
	}

	public static DGVector4 operator -(DGVector4 value)
	{
		FP x = -value.x;
		FP y = -value.y;
		FP z = -value.z;
		FP w = -value.w;
		return new DGVector4(x, y, z, w);
	}

	public static DGVector4 operator *(DGVector4 value1, DGVector4 value2)
	{
		FP x = value1.x * value2.x;
		FP y = value1.y * value2.y;
		FP z = value1.z * value2.z;
		FP w = value1.w * value2.w;
		return new DGVector4(x, y, z, w);
	}

	public static DGVector4 operator *(DGVector4 value, FP multiply)
	{
		FP x = value.x * multiply;
		FP y = value.y * multiply;
		FP z = value.z * multiply;
		FP w = value.w * multiply;
		return new DGVector4(x, y, z, w);
	}

	public static DGVector4 operator /(DGVector4 value1, DGVector4 value2)
	{
		FP x = value1.x / value2.x;
		FP y = value1.y / value2.y;
		FP z = value1.z / value2.z;
		FP w = value1.w / value2.w;
		return new DGVector4(x, y, z, w);
	}

	public static DGVector4 operator /(DGVector4 value1, FP div)
	{
		FP x = value1.x / div;
		FP y = value1.y / div;
		FP z = value1.z / div;
		FP w = value1.w / div;
		return new DGVector4(x, y, z, w);
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	public static bool IsUnit(DGVector4 vector)
	{
		return DGMath.IsApproximatelyZero((FP) 1 - vector.x * vector.x - vector.y * vector.y - vector.z * vector.z -
		                                  vector.w * vector.w);
	}

	/// <summary>
	/// 返回当前向量长度的平方
	/// </summary>
	public static FP SqrMagnitude(DGVector4 v)
	{
		return v.sqrMagnitude;
	}

	public static FP Magnitude(DGVector4 vector)
	{
		return DGMath.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z + vector.w * vector.w);
	}

	public static DGVector4 ClampMagnitude(DGVector4 vector, FP maxLength)
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
		FP normalizedW = vector.w / mag;
		return new DGVector4(normalizedX * maxLength, normalizedY * maxLength, normalizedZ * maxLength,
			normalizedW * maxLength);
	}

	public static FP Distance(DGVector4 a, DGVector4 b)
	{
		FP diffX = a.x - b.x;
		FP diffY = a.y - b.y;
		FP diffZ = a.z - b.z;
		FP diffW = a.w - b.w;
		return DGMath.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ + diffW * diffW);
	}

	public static DGVector4 Lerp(DGVector4 a, DGVector4 b, FP t)
	{
		t = DGMath.Clamp01(t);
		return new DGVector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t,
			a.w + (b.w - a.w) * t);
	}

	public static DGVector4 LerpUnclamped(DGVector4 a, DGVector4 b, FP t)
	{
		return new DGVector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t,
			a.w + (b.w - a.w) * t);
	}

	public static DGVector4 MoveTowards(DGVector4 current, DGVector4 target, FP maxDistanceDelta)
	{
		FP toVectorX = target.x - current.x;
		FP toVectorY = target.y - current.y;
		FP toVectorZ = target.z - current.z;
		FP toVectorW = target.w - current.w;
		FP sqrtDistance = toVectorX * toVectorX + toVectorY * toVectorY + toVectorZ * toVectorZ + toVectorW * toVectorW;
		if (sqrtDistance == FP.Zero ||
		    maxDistanceDelta >= FP.Zero && sqrtDistance <= maxDistanceDelta * maxDistanceDelta)
			return target;
		FP distance = DGMath.Sqrt(sqrtDistance);
		return new DGVector4(current.x + toVectorX / distance * maxDistanceDelta,
			current.y + toVectorY / distance * maxDistanceDelta, current.z + toVectorZ / distance * maxDistanceDelta,
			current.w + toVectorW / distance * maxDistanceDelta);
	}

	public static DGVector4 Scale(DGVector4 a, DGVector4 b)
	{
		return new DGVector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
	}

	/// <summary>
	/// 单位化该向量
	/// </summary>
	/// <param name="v"></param>
	public static DGVector4 Normalize(DGVector4 value)
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
	public static FP Dot(DGVector4 v1, DGVector4 v2)
	{
		return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z + v1.w * v2.w;
	}

	public static DGVector4 Project(DGVector4 vector, DGVector4 onNormal)
	{
		return onNormal * (Dot(vector, onNormal) / Dot(onNormal, onNormal));
	}


	public static DGVector4 Min(DGVector4 value1, DGVector4 value2)
	{
		return new DGVector4(DGMath.Min(value1.x, value2.x), DGMath.Min(value1.y, value2.y),
			DGMath.Min(value1.z, value2.z), DGMath.Min(value1.w, value2.w));
	}

	public static DGVector4 Max(DGVector4 value1, DGVector4 value2)
	{
		return new DGVector4(DGMath.Max(value1.x, value2.x), DGMath.Max(value1.y, value2.y),
			DGMath.Max(value1.z, value2.z), DGMath.Max(value1.w, value2.w));
	}

	public static DGVector4 Abs(DGVector4 value)
	{
		return new DGVector4(DGMath.Abs(value.x), DGMath.Abs(value.y), DGMath.Abs(value.z), DGMath.Abs(value.w));
	}

	public static DGVector4 Hermite(DGVector4 value1, DGVector4 tangent1, DGVector4 value2, DGVector4 tangent2,
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
		FP w = value1.w * value1Blend + value2.w * value2Blend + tangent1.w * tangent1Blend +
		       tangent2.w * tangent2Blend;
		return new DGVector4(x, y, z, w);
	}

	public static DGVector4 SmoothStep(DGVector4 v1, DGVector4 v2, FP amount)
	{
		amount = DGMath.Clamp01(amount);
		amount = (amount * amount) * ((FP) 3 - ((FP) 2 * amount));
		var x = v1.x + ((v2.x - v1.x) * amount);
		var y = v1.y + ((v2.y - v1.y) * amount);
		var z = v1.z + ((v2.z - v1.z) * amount);
		var w = v1.w + ((v2.w - v1.w) * amount);
		return new DGVector4(x, y, z, w);
	}

	public static DGVector4 CatmullRom(DGVector4 v1, DGVector4 v2, DGVector4 v3, DGVector4 v4, FP amount)
	{
		amount = DGMath.Clamp01(amount);
		FP squared = amount * amount;
		FP cubed = amount * squared;
		DGVector4 r = default;
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
		r.w = (FP) 2 * v2.w;
		r.w += (v3.w - v1.w) * amount;
		r.w += (((FP) 2 * v1.w) + ((FP) 4 * v3.w) - ((FP) 5 * v2.w) - (v4.w)) * squared;
		r.w += (((FP) 3 * v2.w) + (v4.w) - (v1.w) - ((FP) 3 * v3.w)) * cubed;
		r.w *= DGMath.Half;
		return r;
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
			w = (FP) 0;
			return;
		}

		FP rate = FP.One / magnitude;
		x *= rate;
		y *= rate;
		z *= rate;
		w *= rate;
	}

	public void Scale(DGVector4 scale)
	{
		this.x *= scale.x;
		this.y *= scale.y;
		this.z *= scale.z;
		this.w *= scale.w;
	}

	public void Abs()
	{
		var abs = Abs(this);
		x = abs.x;
		y = abs.y;
		z = abs.z;
		w = abs.w;
	}

	public bool IsUnit()
	{
		return IsUnit(this);
	}
}