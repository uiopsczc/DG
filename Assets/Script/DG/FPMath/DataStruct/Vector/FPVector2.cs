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

namespace DG
{
	public partial struct FPVector2 : IEquatable<FPVector2>
	{
		public static readonly FP kEpsilon = 0.00001F;
		public static readonly FP kEpsilonNormalSqrt = 1e-15f;

		//不能修改Null的值
		public static FPVector2 Null = FPVector2.max.cpy();
		public static FPVector2 zero => new FPVector2(0, 0);
		public static FPVector2 one => new FPVector2(1, 1);
		public static FPVector2 left => new FPVector2(-1, 0);
		public static FPVector2 right => new FPVector2(1, 0);
		public static FPVector2 up => new FPVector2(0, 1);
		public static FPVector2 down => new FPVector2(0, -1);
		public static FPVector2 max => new FPVector2(float.MaxValue, float.MaxValue);
		public static FPVector2 min => new FPVector2(float.MinValue, float.MinValue);


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

		/// <summary>
		/// 返回当前向量长度的平方
		/// </summary>
		public FP sqrMagnitude => len2();

		public FP magnitude => len();

		/// <summary>
		/// 返回该向量的单位向量
		/// </summary>
		public FPVector2 normalized => nor();


		public FPVector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public FPVector2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}


#if UNITY_STANDALONE
		public FPVector2(Vector2 vector)
		{
			this.x = vector.x;
			this.y = vector.y;
		}
#endif
		public FPVector2(System.Numerics.Vector2 vector)
		{
			this.x = vector.X;
			this.y = vector.Y;
		}

		/*************************************************************************************
		* 模块描述:Equals ToString
		*************************************************************************************/
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			var other = (FPVector2)obj;
			return Equals(other);
		}

		public bool Equals(FPVector2 other)
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
		public static implicit operator FPVector2(FPVector3 v)
		{
			return new FPVector2(v.x, v.y);
		}

		public static implicit operator FPVector2(FPVector4 v)
		{
			return new FPVector2(v.x, v.y);
		}

#if UNITY_STANDALONE
		//转换为Unity的Vector2
		public Vector2 ToVector2()
		{
			return new Vector2((float)x, (float)y);
		}
#endif
		/*************************************************************************************
		* 模块描述:关系运算符
		*************************************************************************************/
		public static bool operator ==(FPVector2 value1, FPVector2 value2)
		{
			return value1.x == value2.x && value1.y == value2.y;
		}

		public static bool operator !=(FPVector2 value1, FPVector2 value2)
		{
			return !(value1 == value2);
		}

		/*************************************************************************************
		* 模块描述:算术运算符
		*************************************************************************************/
		public static FPVector2 operator +(FPVector2 value1, FPVector2 value2)
		{
			FP x = value1.x + value2.x;
			FP y = value1.y + value2.y;
			return new FPVector2(x, y);
		}

		public static FPVector2 operator -(FPVector2 value1, FPVector2 value2)
		{
			FP x = value1.x - value2.x;
			FP y = value1.y - value2.y;
			return new FPVector2(x, y);
		}

		public static FPVector2 operator -(FPVector2 value)
		{
			FP x = -value.x;
			FP y = -value.y;
			return new FPVector2(x, y);
		}

		public static FPVector2 operator *(FPVector2 value1, FPVector2 value2)
		{
			FP x = value1.x * value2.x;
			FP y = value1.y * value2.y;
			return new FPVector2(x, y);
		}

		public static FPVector2 operator *(FPVector2 value, FP multiply)
		{
			FP x = value.x * multiply;
			FP y = value.y * multiply;
			return new FPVector2(x, y);
		}

		public static FPVector2 operator /(FPVector2 value1, FPVector2 value2)
		{
			FP x = value1.x / value2.x;
			FP y = value1.y / value2.y;
			return new FPVector2(x, y);
		}

		public static FPVector2 operator /(FPVector2 value1, FP div)
		{
			FP x = value1.x / div;
			FP y = value1.y / div;
			return new FPVector2(x, y);
		}


		/*************************************************************************************
		* 模块描述:StaticUtil
		*************************************************************************************/
		/// <summary>
		/// 返回当前向量长度的平方
		/// </summary>
		public static FP SqrMagnitude(FPVector2 v)
		{
			return v.len2();
		}

		public static FPVector2 Lerp(FPVector2 a, FPVector2 b, FP t)
		{
			t = FPMath.Clamp01(t);
			return new FPVector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		public static FPVector2 LerpUnclamped(FPVector2 a, FPVector2 b, FP t)
		{
			return new FPVector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		public static FPVector2 MoveTowards(FPVector2 current, FPVector2 target, FP maxDistanceDelta)
		{
			FP toVectorX = target.x - current.x;
			FP toVectorY = target.y - current.y;
			FP sqrtDistance = toVectorX * toVectorX + toVectorY * toVectorY;
			if (sqrtDistance == FP.ZERO ||
				maxDistanceDelta >= FP.ZERO && sqrtDistance <= maxDistanceDelta * maxDistanceDelta)
				return target;
			FP num4 = FPMath.Sqrt(sqrtDistance);
			return new FPVector2(current.x + toVectorX / num4 * maxDistanceDelta,
				current.y + toVectorY / num4 * maxDistanceDelta);
		}

#if UNITY_STANDALONE
		public static FPVector2 SmoothDamp(FPVector2 current, FPVector2 target, ref FPVector2 currentVelocity,
			FP smoothTime, FP maxSpeed)
		{
			FP deltaTime = Time.deltaTime;
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static FPVector2 SmoothDamp(FPVector2 current, FPVector2 target, ref FPVector2 currentVelocity,
			FP smoothTime)
		{
			FP deltaTime = Time.deltaTime;
			FP maxSpeed = FP.MAX_VALUE;
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}
#endif

		public static FPVector2 SmoothDamp(FPVector2 current, FPVector2 target, ref FPVector2 currentVelocity,
			FP smoothTime, FP maxSpeed, FP deltaTime)
		{
			// Based on Game Programming Gems 4 Chapter 1.10
			smoothTime = FPMath.Max(0.0001F, smoothTime);
			FP omega = 2 / smoothTime;

			FP x = omega * deltaTime;
			FP exp = 1 / (1 + x + 0.48F * x * x + 0.235F * x * x * x);

			FP changeX = current.x - target.x;
			FP changeY = current.y - target.y;
			FPVector2 originalTo = target;

			// Clamp maximum speed
			FP maxChange = maxSpeed * smoothTime;

			FP maxChangeSq = maxChange * maxChange;
			FP sqDist = changeX * changeX + changeY * changeY;
			if (sqDist > maxChangeSq)
			{
				var mag = FPMath.Sqrt(sqDist);
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

			if (origMinusCurrentX * outMinusOrigX + origMinusCurrentY * outMinusOrigY > 0)
			{
				outputX = originalTo.x;
				outputY = originalTo.y;

				currentVelocity.x = (outputX - originalTo.x) / deltaTime;
				currentVelocity.y = (outputY - originalTo.y) / deltaTime;
			}

			return new FPVector2(outputX, outputY);
		}

		public static FPVector2 Scale(FPVector2 a, FPVector2 b)
		{
			return new FPVector2(a.x * b.x, a.y * b.y);
		}

		public static FPVector2 Reflect(FPVector2 inDirection, FPVector2 inNormal)
		{
			FP factor = (-2) * Dot(inNormal, inDirection);
			return new FPVector2(factor * inNormal.x + inDirection.x, factor * inNormal.y + inDirection.y);
		}

		public static FPVector2 Perpendicular(FPVector2 inDirection)
		{
			return new FPVector2(-inDirection.y, inDirection.x);
		}

		/// <summary>
		/// 单位化该向量
		/// </summary>
		/// <param name="v"></param>
		public static FPVector2 Normalize(FPVector2 value)
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
		public static FP Dot(FPVector2 v1, FPVector2 v2)
		{
			return v1.x * v2.x + v1.y * v2.y;
		}

		/// <summary>
		/// 叉乘
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>GetHashCode
		public static FPVector2 Cross(FPVector2 v1, FPVector2 v2)
		{
			return new FPVector2(v1.x * v2.y - v1.y * v2.x, v1.y * v2.x - v1.x * v2.y);
		}

		/// <summary>
		/// 求两个向量的夹角，没有正负区分
		/// </summary>
		/// <param name="fromAngle"></param>
		/// <param name="toAngle"></param>
		/// <returns></returns>
		public static FP Angle(FPVector2 fromAngle, FPVector2 toAngle)
		{
			// sqrt(a) * sqrt(b) = sqrt(a * b) -- valid for real numbers
			FP denominator = fromAngle.magnitude * toAngle.magnitude;
			if (denominator < kEpsilonNormalSqrt)
				return FP.ZERO;
			FP dot = FPMath.Clamp(Dot(fromAngle, toAngle) / denominator, FP.NEGATIVE_ONE, FP.ONE);
			return FPMath.Acos(dot) * FPMath.Rad2Deg;
		}

		public static FP SignedAngle(FPVector2 from, FPVector2 to)
		{
			FP unsignedAngle = Angle(from, to);
			FP sign = FPMath.Sign(from.x * to.y - from.y * to.x);
			return unsignedAngle * sign;
		}

		public static FP Distance(FPVector2 a, FPVector2 b)
		{
			FP diffX = a.x - b.x;
			FP diffY = a.y - b.y;
			return FPMath.Sqrt(diffX * diffX + diffY * diffY);
		}

		public static FPVector2 ClampMagnitude(FPVector2 vector, FP maxLength)
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
			return new FPVector2(normalizedX * maxLength, normalizedY * maxLength);
		}

		public static FP Magnitude(FPVector2 vector)
		{
			return FPMath.Sqrt(vector.x * vector.x + vector.y * vector.y);
		}

		public static FPVector2 Min(FPVector2 value1, FPVector2 value2)
		{
			return new FPVector2(FPMath.Min(value1.x, value2.x), FPMath.Min(value1.y, value2.y));
		}

		public static FPVector2 Max(FPVector2 value1, FPVector2 value2)
		{
			return new FPVector2(FPMath.Max(value1.x, value2.x), FPMath.Max(value1.y, value2.y));
		}

		public static FPVector2 SmoothStep(FPVector2 v1, FPVector2 v2, FP amount)
		{
			amount = FPMath.Clamp01(amount);
			amount = (amount * amount) * (3 - (2 * amount));
			var x = v1.x + ((v2.x - v1.x) * amount);
			var y = v1.y + ((v2.y - v1.y) * amount);
			return new FPVector2(x, y);
		}

		public static FPVector2 CatmullRom(FPVector2 v1, FPVector2 v2, FPVector2 v3, FPVector2 v4, FP amount)
		{
			amount = FPMath.Clamp01(amount);
			FP squared = amount * amount;
			FP cubed = amount * squared;
			FPVector2 r;
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
			return r;
		}

		public static FPVector2 Hermite(FPVector2 v1, FPVector2 tangent1, FPVector2 v2, FPVector2 tangent2, FP amount)
		{
			amount = FPMath.Clamp01(amount);
			FP squared = amount * amount;
			FP cubed = amount * squared;
			FP a = ((cubed * 2) - (squared * 3)) + 1;
			FP b = (-cubed * 2) + (squared * 3);
			FP c = (cubed - (squared * 2)) + amount;
			FP d = cubed - squared;
			var x = (v1.x * a) + (v2.x * b) + (tangent1.x * c) + (tangent2.x * d);
			var y = (v1.y * a) + (v2.y * b) + (tangent1.y * c) + (tangent2.y * d);
			return new FPVector2(x, y);
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

		public void Scale(FPVector2 scale)
		{
			scl(scale);
		}
	}
}
