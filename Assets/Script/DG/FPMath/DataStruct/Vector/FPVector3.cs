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
	public partial struct FPVector3
	{
		public static readonly FP kEpsilon = 0.00001F;
		public static readonly FP kEpsilonNormalSqrt = 1e-15F;

		//不能修改Null的值
		public static FPVector3 Null = max.cpy();
		public static FPVector3 zero => new(0, 0, 0);
		public static FPVector3 one => new(1, 1, 1);
		public static FPVector3 forward => new(0, 0, 1);
		public static FPVector3 back => new(0, 0, -1);
		public static FPVector3 left => new(-1, 0, 0);
		public static FPVector3 right => new(1, 0, 0);
		public static FPVector3 up => new(0, 1, 0);
		public static FPVector3 down => new(0, -1, 0);
		public static FPVector3 max => new(float.MaxValue, float.MaxValue, float.MaxValue);
		public static FPVector3 min => new(float.MinValue, float.MinValue, float.MinValue);


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

		public long[] scaledValue => new[] { x.scaledValue, y.scaledValue, z.scaledValue };
		public FPVector3 abs => Abs(this);

		/// <summary>
		/// 返回当前向量长度的平方
		/// </summary>
		public FP sqrMagnitude => len2();

		public FP magnitude => len();

		/// <summary>
		/// 返回该向量的单位向量
		/// </summary>
		public FPVector3 normalized => nor();


		public FPVector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public FPVector3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}


#if UNITY_STANDALONE
		public FPVector3(Vector3 vector)
		{
			x = vector.x;
			y = vector.y;
			z = vector.z;
		}
#endif
		public FPVector3(System.Numerics.Vector3 vector)
		{
			x = vector.X;
			y = vector.Y;
			z = vector.Z;
		}

		/*************************************************************************************
		* 模块描述:Equals ToString
		*************************************************************************************/
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			var other = (FPVector3)obj;
			return Equals(other);
		}

		public bool Equals(FPVector3 other)
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
		public static implicit operator FPVector3(FPVector2 v)
		{
			return new FPVector3(v.x, v.y, 0);
		}

		public static implicit operator FPVector3(FPVector4 v)
		{
			return new FPVector3(v.x, v.y, v.z);
		}
#if UNITY_STANDALONE
		//转换为Unity的Vector3
		public static implicit operator Vector3(FPVector3 value)
		{
			return new Vector3(value.x, value.y, value.z);
		}

		public static implicit operator FPVector3(Vector3 value)
		{
			return new FPVector3(value.x, value.y, value.z);
		}
#endif
		/*************************************************************************************
		* 模块描述:关系运算符
		*************************************************************************************/
		public static bool operator ==(FPVector3 value1, FPVector3 value2)
		{
			// Returns false in the presence of NaN values.
			FP diffX = value1.x - value2.x;
			FP diffY = value1.y - value2.y;
			FP diffZ = value1.z - value2.z;
			FP sqrmag = diffX * diffX + diffY * diffY + diffZ * diffZ;
			return sqrmag <= kEpsilon * kEpsilon;
		}

		public static bool operator !=(FPVector3 value1, FPVector3 value2)
		{
			return !(value1 == value2);
		}

		/*************************************************************************************
		* 模块描述:操作运算
		*************************************************************************************/
		public static FPVector3 operator +(FPVector3 value1, FPVector3 value2)
		{
			FP x = value1.x + value2.x;
			FP y = value1.y + value2.y;
			FP z = value1.z + value2.z;
			return new FPVector3(x, y, z);
		}

		public static FPVector3 operator -(FPVector3 value1, FPVector3 value2)
		{
			FP x = value1.x - value2.x;
			FP y = value1.y - value2.y;
			FP z = value1.z - value2.z;
			return new FPVector3(x, y, z);
		}

		public static FPVector3 operator -(FPVector3 value)
		{
			FP x = -value.x;
			FP y = -value.y;
			FP z = -value.z;
			return new FPVector3(x, y, z);
		}

		public static FPVector3 operator *(FPVector3 value1, FPVector3 value2)
		{
			FP x = value1.x * value2.x;
			FP y = value1.y * value2.y;
			FP z = value1.z * value2.z;
			return new FPVector3(x, y, z);
		}

		public static FPVector3 operator *(FPVector3 value, FP multiply)
		{
			FP x = value.x * multiply;
			FP y = value.y * multiply;
			FP z = value.z * multiply;
			return new FPVector3(x, y, z);
		}

		public static FPVector3 operator /(FPVector3 value1, FPVector3 value2)
		{
			FP x = value1.x / value2.x;
			FP y = value1.y / value2.y;
			FP z = value1.z / value2.z;
			return new FPVector3(x, y, z);
		}

		public static FPVector3 operator /(FPVector3 value1, FP div)
		{
			FP x = value1.x / div;
			FP y = value1.y / div;
			FP z = value1.z / div;
			return new FPVector3(x, y, z);
		}

		/*************************************************************************************
		* 模块描述:StaticUtil
		*************************************************************************************/

		/// <summary>
		/// 返回当前向量长度的平方
		/// </summary>
		public static FP SqrMagnitude(FPVector3 v)
		{
			return v.len2();
		}

		public static FP Magnitude(FPVector3 vector)
		{
			return vector.len();
		}

		public static FPVector3 ClampMagnitude(FPVector3 vector, FP maxLength)
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
			return new FPVector3(normalizedX * maxLength, normalizedY * maxLength, normalizedZ * maxLength);
		}

		public static FP Distance(FPVector3 a, FPVector3 b)
		{
			return a.dst(b);
		}

		public static (FPVector3, FPVector3) OrthoNormalize(FPVector3 va, FPVector3 vb)
		{
			va.Normalize();
			vb -= Project(vb, va);
			vb.Normalize();
			return (va, vb);
		}

		public static (FPVector3, FPVector3, FPVector3) OrthoNormalize(FPVector3 va, FPVector3 vb, FPVector3 vc)
		{
			va.Normalize();
			vb -= Project(vb, va);
			vb.Normalize();
			vc -= Project(vc, va);
			vc -= Project(vc, vb);
			vc.Normalize();
			return (va, vb, vc);
		}

		public static FPVector3 OrthoNormalVector(FPVector3 vec)
		{
			FP a = default;
			FP k = default;
			FP x = default;
			FP y = default;
			FP z = default;
			if (FPMath.Abs(vec.z) > FPMath.HALF_SQRT2)
			{
				a = vec.y * vec.y + vec.z * vec.z;
				k = 1 / FPMath.Sqrt(a);
				x = 0;
				y = -vec.z * k;
				z = vec.y * k;
				return new FPVector3(x, y, z);
			}

			a = vec.x * vec.x + vec.y * vec.y;
			k = 1 / FPMath.Sqrt(a);
			x = -vec.y * k;
			y = vec.x * k;
			z = 0;


			return new FPVector3(x, y, z);
		}


		public static FPVector3 RotateTowards(FPVector3 current, FPVector3 target, FP maxRadiansDelta, FP maxMagnitudeDelta)
		{
			var len1 = current.magnitude;
			var len2 = target.magnitude;

			if (len1 > FPMath.EPSILION && len2 > FPMath.EPSILION)
			{
				var from = current / len1;
				var to = target / len2;
				var cosom = Dot(from, to);
				if (cosom > 1 - FPMath.EPSILION)
					return MoveTowards(current, target, maxMagnitudeDelta);
				if (cosom < (-1) + FPMath.EPSILION)
				{
					var axis = OrthoNormalVector(from);
					var q = FPQuaternion.AngleAxis(maxRadiansDelta * FPMath.Rad2Deg, axis);
					var rotated = q * from;
					var delta = FPMath.ClampedMove(len1, len2, maxMagnitudeDelta);
					rotated *= delta;
					return rotated;
				}

				{
					var angle = FPMath.Acos(cosom);
					var axis = Cross(from, to);
					axis.Normalize();
					var q = FPQuaternion.AngleAxis(FPMath.Min(maxRadiansDelta, angle) * FPMath.Rad2Deg, axis);
					var rotated = q * from;
					var delta = FPMath.ClampedMove(len1, len2, maxMagnitudeDelta);
					rotated *= delta;
					return rotated;
				}
			}

			return MoveTowards(current, target, maxMagnitudeDelta);
		}

		public static FPVector3 Lerp(FPVector3 a, FPVector3 b, FP t)
		{
			t = FPMath.Clamp01(t);
			return new FPVector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		public static FPVector3 LerpUnclamped(FPVector3 a, FPVector3 b, FP t)
		{
			return new FPVector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		public static FPVector3 MoveTowards(FPVector3 current, FPVector3 target, FP maxDistanceDelta)
		{
			FP toVectorX = target.x - current.x;
			FP toVectorY = target.y - current.y;
			FP toVectorZ = target.z - current.z;
			FP sqrtDistance = toVectorX * toVectorX + toVectorY * toVectorY + toVectorZ * toVectorZ;
			if (sqrtDistance == FP.ZERO ||
				maxDistanceDelta >= FP.ZERO && sqrtDistance <= maxDistanceDelta * maxDistanceDelta)
				return target;
			FP distance = FPMath.Sqrt(sqrtDistance);
			return new FPVector3(current.x + toVectorX / distance * maxDistanceDelta,
				current.y + toVectorY / distance * maxDistanceDelta, current.z + toVectorZ / distance * maxDistanceDelta);
		}

#if UNITY_STANDALONE
		public static FPVector3 SmoothDamp(FPVector3 current, FPVector3 target, ref FPVector3 currentVelocity,
			FP smoothTime, FP maxSpeed)
		{
			FP deltaTime = Time.deltaTime;
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static FPVector3 SmoothDamp(FPVector3 current, FPVector3 target, ref FPVector3 currentVelocity,
			FP smoothTime)
		{
			FP deltaTime = Time.deltaTime;
			FP maxSpeed = FP.MAX_VALUE;
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}
#endif

		public static FPVector3 SmoothDamp(FPVector3 current, FPVector3 target, ref FPVector3 currentVelocity,
			FP smoothTime, FP maxSpeed, FP deltaTime)
		{
			FP outputX = 0;
			FP outputY = 0;
			FP outputZ = 0;

			// Based on Game Programming Gems 4 Chapter 1.10
			smoothTime = FPMath.Max(0.0001F, smoothTime);
			FP omega = 2 / smoothTime;

			FP x = omega * deltaTime;
			FP exp = 1 / (1 + x + 0.48f * x * x + 0.235f * x * x * x);

			FP changeX = current.x - target.x;
			FP changeY = current.y - target.y;
			FP changeZ = current.z - target.z;
			FPVector3 originalTo = target;

			// Clamp maximum speed
			FP maxChange = maxSpeed * smoothTime;

			FP maxChangeSq = maxChange * maxChange;
			FP sqrmag = changeX * changeX + changeY * changeY + changeZ * changeZ;
			if (sqrmag > maxChangeSq)
			{
				var mag = FPMath.Sqrt(sqrmag);
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
				0)
			{
				outputX = originalTo.x;
				outputY = originalTo.y;
				outputZ = originalTo.z;

				currentVelocity.x = (outputX - originalTo.x) / deltaTime;
				currentVelocity.y = (outputY - originalTo.y) / deltaTime;
				currentVelocity.z = (outputZ - originalTo.z) / deltaTime;
			}

			return new FPVector3(outputX, outputY, outputZ);
		}

		public static FPVector3 Scale(FPVector3 a, FPVector3 b)
		{
			return new FPVector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public static FPVector3 Reflect(FPVector3 inDirection, FPVector3 inNormal)
		{
			FP factor = (-2) * Dot(inNormal, inDirection);
			return new FPVector3(factor * inNormal.x + inDirection.x, factor * inNormal.y + inDirection.y,
				factor * inNormal.z + inDirection.z);
		}

		/// <summary>
		/// 单位化该向量
		/// </summary>
		/// <param name="v"></param>
		public static FPVector3 Normalize(FPVector3 value)
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
		public static FP Dot(FPVector3 v1, FPVector3 v2)
		{
			return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
		}

		/// <summary>
		/// 叉乘
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>GetHashCode
		public static FPVector3 Cross(FPVector3 v1, FPVector3 v2)
		{
			return new FPVector3(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
		}

		public static FPVector3 Project(FPVector3 vector, FPVector3 onNormal)
		{
			FP sqrMag = Dot(onNormal, onNormal);
			if (sqrMag < FPMath.EPSILION)
				return zero;
			FP dot = Dot(vector, onNormal);
			return new FPVector3(onNormal.x * dot / sqrMag, onNormal.y * dot / sqrMag, onNormal.z * dot / sqrMag);
		}

		public static FPVector3 ProjectOnPlane(FPVector3 vector, FPVector3 planeNormal)
		{
			FP sqrMag = Dot(planeNormal, planeNormal);
			if (sqrMag < FPMath.EPSILION)
				return vector;
			FP dot = Dot(vector, planeNormal);
			return new FPVector3(vector.x - planeNormal.x * dot / sqrMag, vector.y - planeNormal.y * dot / sqrMag,
				vector.z - planeNormal.z * dot / sqrMag);
		}

		/// <summary>
		/// 求两个向量的夹角，没有正负区分
		/// </summary>
		/// <param name="fromAngle"></param>
		/// <param name="toAngle"></param>
		/// <returns></returns>
		public static FP Angle(FPVector3 fromAngle, FPVector3 toAngle)
		{
			// sqrt(a) * sqrt(b) = sqrt(a * b) -- valid for real numbers
			FP denominator = fromAngle.magnitude * toAngle.magnitude;
			if (denominator <= kEpsilonNormalSqrt)
				return FP.ZERO;
			FP dot = FPMath.Clamp(Dot(fromAngle, toAngle) / denominator, FP.NEGATIVE_ONE, FP.ONE);
			return FPMath.Acos(dot) * FPMath.Rad2Deg;
		}

		public static FP SignedAngle(FPVector3 from, FPVector3 to, FPVector3 axis)
		{
			FP unsignedAngle = Angle(from, to);

			FP crossX = from.y * to.z - from.z * to.y;
			FP crossY = from.z * to.x - from.x * to.z;
			FP crossZ = from.x * to.y - from.y * to.x;
			FP sign = FPMath.Sign(axis.x * crossX + axis.y * crossY + axis.z * crossZ);
			return unsignedAngle * sign;
		}

		public static FPVector3 Min(FPVector3 value1, FPVector3 value2)
		{
			return new FPVector3(FPMath.Min(value1.x, value2.x), FPMath.Min(value1.y, value2.y),
				FPMath.Min(value1.z, value2.z));
		}

		public static FPVector3 Max(FPVector3 value1, FPVector3 value2)
		{
			return new FPVector3(FPMath.Max(value1.x, value2.x), FPMath.Max(value1.y, value2.y),
				FPMath.Max(value1.z, value2.z));
		}

		public static FPVector3 Abs(FPVector3 value)
		{
			return new FPVector3(FPMath.Abs(value.x), FPMath.Abs(value.y), FPMath.Abs(value.z));
		}

		public static FPVector3 SmoothStep(FPVector3 v1, FPVector3 v2, FP amount)
		{
			amount = FPMath.Clamp01(amount);
			amount = (amount * amount) * (3 - (2 * amount));
			var x = v1.x + ((v2.x - v1.x) * amount);
			var y = v1.y + ((v2.y - v1.y) * amount);
			var z = v1.z + ((v2.z - v1.z) * amount);
			return new FPVector3(x, y, z);
		}

		public static FPVector3 CatmullRom(FPVector3 v1, FPVector3 v2, FPVector3 v3, FPVector3 v4, FP amount)
		{
			amount = FPMath.Clamp01(amount);
			FP squared = amount * amount;
			FP cubed = amount * squared;
			FPVector3 r = default;
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
		public static FPVector3 Hermite(FPVector3 value1, FPVector3 tangent1, FPVector3 value2, FPVector3 tangent2,
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
			return new FPVector3(x, y, z);
		}

		public static FP AngleAroundAxis(FPVector3 from, FPVector3 to, FPVector3 axis)
		{
			from -= Project(from, axis);
			to -= Project(to, axis);
			var angle = Angle(from, to);
			return angle * (Dot(axis, Cross(from, to)) < 0 ? (-1) : 1);
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
				return;
			}

			FP rate = FP.ONE / magnitude;
			x *= rate;
			y *= rate;
			z *= rate;
		}

		public void Scale(FPVector3 scale)
		{
			x *= scale.x;
			y *= scale.y;
			z *= scale.z;
		}

		public void Abs()
		{
			var abs = Abs(this);
			x = abs.x;
			y = abs.y;
			z = abs.z;
		}
	}
}
