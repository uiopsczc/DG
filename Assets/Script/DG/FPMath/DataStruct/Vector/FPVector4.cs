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

namespace DG
{
	public struct FPVector4
	{
		public static readonly FP kEpsilon = 0.00001F;
		public static readonly FP kEpsilonNormalSqrt = 1e-15F;

		public static FPVector4 zero => new FPVector4(0, 0, 0, 0);
		public static FPVector4 one => new FPVector4(1, 1, 1, 1);
		public static FPVector4 max => new FPVector4(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);
		public static FPVector4 min => new FPVector4(float.MinValue, float.MinValue, float.MinValue, float.MinValue);

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

		public long[] scaledValue => new[] { x.scaledValue, y.scaledValue, z.scaledValue, w.scaledValue };
		public FPVector4 abs => Abs(this);

		/// <summary>
		/// 返回当前向量长度的平方
		/// </summary>
		public FP sqrMagnitude => x * x + y * y + z * z + w * w;

		public FP magnitude => FPMath.Sqrt(sqrMagnitude);

		/// <summary>
		/// 返回该向量的单位向量
		/// </summary>
		public FPVector4 normalized => Normalize(this);

		public FPVector4(FP x, FP y, FP z, FP w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public FPVector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public FPVector4(int x, int y, int z, int w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

#if UNITY_STANDALONE
		public FPVector4(Vector4 vector)
		{
			this.x = vector.x;
			this.y = vector.y;
			this.z = vector.z;
			this.w = vector.w;
		}
#endif

		/*************************************************************************************
		* 模块描述:Equals ToString
		*************************************************************************************/
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			var other = (FPVector4)obj;
			return Equals(other);
		}

		public bool Equals(FPVector4 other)
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
		public static implicit operator FPVector4(FPVector2 v)
		{
			return new FPVector4(v.x, v.y, 0, 0);
		}

		public static implicit operator FPVector4(FPVector3 v)
		{
			return new FPVector4(v.x, v.y, v.z, 0);
		}
#if UNITY_STANDALONE
		//转换为Unity的Vector4
		public Vector4 ToVector4()
		{
			return new Vector4((float)x, (float)y, (float)z, (float)w);
		}
#endif

		/*************************************************************************************
		* 模块描述:关系运算符
		*************************************************************************************/
		public static bool operator ==(FPVector4 value1, FPVector4 value2)
		{
			// Returns false in the presence of NaN values.
			FP diffX = value1.x - value2.x;
			FP diffY = value1.y - value2.y;
			FP diffZ = value1.z - value2.z;
			FP diffW = value1.w - value2.w;
			FP sqrmag = diffX * diffX + diffY * diffY + diffZ * diffZ + diffW * diffW;
			return sqrmag <= kEpsilon * kEpsilon;
		}

		public static bool operator !=(FPVector4 value1, FPVector4 value2)
		{
			return !(value1 == value2);
		}

		/*************************************************************************************
		* 模块描述:操作运算
		*************************************************************************************/
		public static FPVector4 operator +(FPVector4 value1, FPVector4 value2)
		{
			FP x = value1.x + value2.x;
			FP y = value1.y + value2.y;
			FP z = value1.z + value2.z;
			FP w = value1.w + value2.w;
			return new FPVector4(x, y, z, w);
		}

		public static FPVector4 operator -(FPVector4 value1, FPVector4 value2)
		{
			FP x = value1.x - value2.x;
			FP y = value1.y - value2.y;
			FP z = value1.z - value2.z;
			FP w = value1.w - value2.w;
			return new FPVector4(x, y, z, w);
		}

		public static FPVector4 operator -(FPVector4 value)
		{
			FP x = -value.x;
			FP y = -value.y;
			FP z = -value.z;
			FP w = -value.w;
			return new FPVector4(x, y, z, w);
		}

		public static FPVector4 operator *(FPVector4 value1, FPVector4 value2)
		{
			FP x = value1.x * value2.x;
			FP y = value1.y * value2.y;
			FP z = value1.z * value2.z;
			FP w = value1.w * value2.w;
			return new FPVector4(x, y, z, w);
		}

		public static FPVector4 operator *(FPVector4 value, FP multiply)
		{
			FP x = value.x * multiply;
			FP y = value.y * multiply;
			FP z = value.z * multiply;
			FP w = value.w * multiply;
			return new FPVector4(x, y, z, w);
		}

		public static FPVector4 operator /(FPVector4 value1, FPVector4 value2)
		{
			FP x = value1.x / value2.x;
			FP y = value1.y / value2.y;
			FP z = value1.z / value2.z;
			FP w = value1.w / value2.w;
			return new FPVector4(x, y, z, w);
		}

		public static FPVector4 operator /(FPVector4 value1, FP div)
		{
			FP x = value1.x / div;
			FP y = value1.y / div;
			FP z = value1.z / div;
			FP w = value1.w / div;
			return new FPVector4(x, y, z, w);
		}

		/*************************************************************************************
		* 模块描述:StaticUtil
		*************************************************************************************/
		public static bool IsUnit(FPVector4 vector)
		{
			return FPMath.IsApproximatelyZero(1 - vector.x * vector.x - vector.y * vector.y - vector.z * vector.z -
											  vector.w * vector.w);
		}

		/// <summary>
		/// 返回当前向量长度的平方
		/// </summary>
		public static FP SqrMagnitude(FPVector4 v)
		{
			return v.sqrMagnitude;
		}

		public static FP Magnitude(FPVector4 vector)
		{
			return FPMath.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z + vector.w * vector.w);
		}

		public static FPVector4 ClampMagnitude(FPVector4 vector, FP maxLength)
		{
			FP sqrMagnitude = vector.sqrMagnitude;
			if (sqrMagnitude <= maxLength * maxLength)
				return vector;
			//these intermediate variables force the intermediate result to be
			//of float precision. without this, the intermediate result can be of higher
			//precision, which changes behavior.
			FP mag = FPMath.Sqrt(sqrMagnitude);
			FP normalizedX = vector.x / mag;
			FP normalizedY = vector.y / mag;
			FP normalizedZ = vector.z / mag;
			FP normalizedW = vector.w / mag;
			return new FPVector4(normalizedX * maxLength, normalizedY * maxLength, normalizedZ * maxLength,
				normalizedW * maxLength);
		}

		public static FP Distance(FPVector4 a, FPVector4 b)
		{
			FP diffX = a.x - b.x;
			FP diffY = a.y - b.y;
			FP diffZ = a.z - b.z;
			FP diffW = a.w - b.w;
			return FPMath.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ + diffW * diffW);
		}

		public static FPVector4 Lerp(FPVector4 a, FPVector4 b, FP t)
		{
			t = FPMath.Clamp01(t);
			return new FPVector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t,
				a.w + (b.w - a.w) * t);
		}

		public static FPVector4 LerpUnclamped(FPVector4 a, FPVector4 b, FP t)
		{
			return new FPVector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t,
				a.w + (b.w - a.w) * t);
		}

		public static FPVector4 MoveTowards(FPVector4 current, FPVector4 target, FP maxDistanceDelta)
		{
			FP toVectorX = target.x - current.x;
			FP toVectorY = target.y - current.y;
			FP toVectorZ = target.z - current.z;
			FP toVectorW = target.w - current.w;
			FP sqrtDistance = toVectorX * toVectorX + toVectorY * toVectorY + toVectorZ * toVectorZ + toVectorW * toVectorW;
			if (sqrtDistance == FP.ZERO ||
				maxDistanceDelta >= FP.ZERO && sqrtDistance <= maxDistanceDelta * maxDistanceDelta)
				return target;
			FP distance = FPMath.Sqrt(sqrtDistance);
			return new FPVector4(current.x + toVectorX / distance * maxDistanceDelta,
				current.y + toVectorY / distance * maxDistanceDelta, current.z + toVectorZ / distance * maxDistanceDelta,
				current.w + toVectorW / distance * maxDistanceDelta);
		}

		public static FPVector4 Scale(FPVector4 a, FPVector4 b)
		{
			return new FPVector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}

		/// <summary>
		/// 单位化该向量
		/// </summary>
		/// <param name="v"></param>
		public static FPVector4 Normalize(FPVector4 value)
		{
			var magnitude = value.magnitude;
			if (magnitude <= kEpsilon)
				return zero;

			FP rate = FP.ONE / magnitude;
			return value * rate;
		}

		/// <summary>
		/// 点乘
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static FP Dot(FPVector4 v1, FPVector4 v2)
		{
			return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z + v1.w * v2.w;
		}

		public static FPVector4 Project(FPVector4 vector, FPVector4 onNormal)
		{
			return onNormal * (Dot(vector, onNormal) / Dot(onNormal, onNormal));
		}


		public static FPVector4 Min(FPVector4 value1, FPVector4 value2)
		{
			return new FPVector4(FPMath.Min(value1.x, value2.x), FPMath.Min(value1.y, value2.y),
				FPMath.Min(value1.z, value2.z), FPMath.Min(value1.w, value2.w));
		}

		public static FPVector4 Max(FPVector4 value1, FPVector4 value2)
		{
			return new FPVector4(FPMath.Max(value1.x, value2.x), FPMath.Max(value1.y, value2.y),
				FPMath.Max(value1.z, value2.z), FPMath.Max(value1.w, value2.w));
		}

		public static FPVector4 Abs(FPVector4 value)
		{
			return new FPVector4(FPMath.Abs(value.x), FPMath.Abs(value.y), FPMath.Abs(value.z), FPMath.Abs(value.w));
		}

		public static FPVector4 Hermite(FPVector4 value1, FPVector4 tangent1, FPVector4 value2, FPVector4 tangent2,
			FP interpolationAmount)
		{
			FP weightSquared = interpolationAmount * interpolationAmount;
			FP weightCubed = interpolationAmount * weightSquared;
			FP value1Blend = 2 * weightCubed - 3 * weightSquared + 1;
			FP tangent1Blend = weightCubed - 2 * weightSquared + interpolationAmount;
			FP value2Blend = (-2) * weightCubed + 3 * weightSquared;
			FP tangent2Blend = weightCubed - weightSquared;
			FP x = value1.x * value1Blend + value2.x * value2Blend + tangent1.x * tangent1Blend +
				   tangent2.x * tangent2Blend;
			FP y = value1.y * value1Blend + value2.y * value2Blend + tangent1.y * tangent1Blend +
				   tangent2.y * tangent2Blend;
			FP z = value1.z * value1Blend + value2.z * value2Blend + tangent1.z * tangent1Blend +
				   tangent2.z * tangent2Blend;
			FP w = value1.w * value1Blend + value2.w * value2Blend + tangent1.w * tangent1Blend +
				   tangent2.w * tangent2Blend;
			return new FPVector4(x, y, z, w);
		}

		public static FPVector4 SmoothStep(FPVector4 v1, FPVector4 v2, FP amount)
		{
			amount = FPMath.Clamp01(amount);
			amount = (amount * amount) * (3 - (2 * amount));
			var x = v1.x + ((v2.x - v1.x) * amount);
			var y = v1.y + ((v2.y - v1.y) * amount);
			var z = v1.z + ((v2.z - v1.z) * amount);
			var w = v1.w + ((v2.w - v1.w) * amount);
			return new FPVector4(x, y, z, w);
		}

		public static FPVector4 CatmullRom(FPVector4 v1, FPVector4 v2, FPVector4 v3, FPVector4 v4, FP amount)
		{
			amount = FPMath.Clamp01(amount);
			FP squared = amount * amount;
			FP cubed = amount * squared;
			FPVector4 r = default;
			r.x = 2 * v2.x;
			r.x += (v3.x - v1.x) * amount;
			r.x += ((2 * v1.x) + (4 * v3.x) - (5 * v2.x) - (v4.x)) * squared;
			r.x += ((3 * v2.x) + (v4.x) - (v1.x) - (3 * v3.x)) * cubed;
			r.x *= FPMath.HALF;
			r.y = 2 * v2.y;
			r.y += (v3.y - v1.y) * amount;
			r.y += ((2 * v1.y) + (4 * v3.y) - (5 * v2.y) - (v4.y)) * squared;
			r.y += ((3 * v2.y) + (v4.y) - (v1.y) - (3 * v3.y)) * cubed;
			r.y *= FPMath.HALF;
			r.z = 2 * v2.z;
			r.z += (v3.z - v1.z) * amount;
			r.z += ((2 * v1.z) + (4 * v3.z) - (5 * v2.z) - (v4.z)) * squared;
			r.z += ((3 * v2.z) + (v4.z) - (v1.z) - (3 * v3.z)) * cubed;
			r.z *= FPMath.HALF;
			r.w = 2 * v2.w;
			r.w += (v3.w - v1.w) * amount;
			r.w += ((2 * v1.w) + (4 * v3.w) - (5 * v2.w) - (v4.w)) * squared;
			r.w += ((3 * v2.w) + (v4.w) - (v1.w) - (3 * v3.w)) * cubed;
			r.w *= FPMath.HALF;
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
				x = 0;
				y = 0;
				z = 0;
				w = 0;
				return;
			}

			FP rate = FP.ONE / magnitude;
			x *= rate;
			y *= rate;
			z *= rate;
			w *= rate;
		}

		public void Scale(FPVector4 scale)
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
}
