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

//https://github.com/sungiant/abacus/blob/master/source/abacus/gen/main/Quaternion.t4
public partial struct DGQuaternion 
{
	public static readonly DGFixedPoint kEpsilon = (DGFixedPoint) 0.000001F;
	public static DGQuaternion identity = new DGQuaternion(0, 0, 0, 1);



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
				case 3:
					return w;
				default:
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
			}
		}
	}

	public DGVector3 xyz
	{
		set
		{
			x = value.x;
			y = value.y;
			z = value.z;
		}
		get => new DGVector3(x, y, z);
	}

	public DGVector3 eulerAngles
	{
		get => Internal_ToEulerRad(this);
		set => this = Internal_FromEulerRad(value * DGMath.Rad2Deg);
	}

	public DGQuaternion normalized => Normalize(this);
	public DGFixedPoint sqrMagnitude => SqrMagnitude();
	public DGFixedPoint magnitude => Magnitude();


	

	public DGQuaternion(int x, int y, int z, int w)
	{
		this.x = (DGFixedPoint) x;
		this.y = (DGFixedPoint) y;
		this.z = (DGFixedPoint) z;
		this.w = (DGFixedPoint) w;
	}


#if UNITY_STANDALONE
	public DGQuaternion(Quaternion quaternion)
	{
		this.x = (DGFixedPoint) quaternion.x;
		this.y = (DGFixedPoint) quaternion.y;
		this.z = (DGFixedPoint) quaternion.z;
		this.w = (DGFixedPoint) quaternion.w;
	}
#endif

	public DGQuaternion(System.Numerics.Quaternion quaternion)
	{
		this.x = (DGFixedPoint)quaternion.X;
		this.y = (DGFixedPoint)quaternion.Y;
		this.z = (DGFixedPoint)quaternion.Z;
		this.w = (DGFixedPoint)quaternion.W;
	}
	/*************************************************************************************
	* 模块描述:Equals ToString
	*************************************************************************************/
	public override bool Equals(object obj)
	{
		if (obj == null)
			return false;
		var other = (DGQuaternion) obj;
		return Equals(other);
	}

	public bool Equals(DGQuaternion other)
	{
		return other.x == x && other.y == y && other.z == z && other.w == w;
	}

	public override int GetHashCode()
	{
		return x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2 ^ w.GetHashCode() >> 1;
	}
	/*************************************************************************************
	* 模块描述:转换
	*************************************************************************************/
#if UNITY_STANDALONE
	//转换为Unity的Quaternion
	public Quaternion ToQuaternion()
	{
		return new Quaternion((float) x, (float) y, (float) z, (float) w);
	}
#endif
	/*************************************************************************************
	* 模块描述:关系运算符运算
	*************************************************************************************/
	public static bool operator ==(DGQuaternion value1, DGQuaternion value2)
	{
		return _IsEqualUsingDot(Dot(value1, value2));
	}

	public static bool operator !=(DGQuaternion value1, DGQuaternion value2)
	{
		return !(value1 == value2);
	}

	/*************************************************************************************
	* 模块描述:操作运算
	*************************************************************************************/
	public static DGQuaternion operator +(DGQuaternion value1, DGQuaternion value2)
	{
		DGFixedPoint x = value1.x + value2.x;
		DGFixedPoint y = value1.y + value2.y;
		DGFixedPoint z = value1.z + value2.z;
		DGFixedPoint w = value1.w + value2.w;
		return new DGQuaternion(x, y, z, w);
	}

	public static DGQuaternion operator -(DGQuaternion value1, DGQuaternion value2)
	{
		DGFixedPoint x = value1.x - value2.x;
		DGFixedPoint y = value1.y - value2.y;
		DGFixedPoint z = value1.z - value2.z;
		DGFixedPoint w = value1.w - value2.w;
		return new DGQuaternion(x, y, z, w);
	}

	public static DGQuaternion operator -(DGQuaternion value)
	{
		DGFixedPoint x = -value.x;
		DGFixedPoint y = -value.y;
		DGFixedPoint z = -value.z;
		DGFixedPoint w = -value.w;
		return new DGQuaternion(x, y, z, w);
	}

	public static DGQuaternion operator *(DGQuaternion lhs, DGQuaternion rhs)
	{
		return new DGQuaternion(
			lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
			lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
			lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
			lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
	}

	// Rotates the point /point/ with /rotation/.
	public static DGVector3 operator *(DGQuaternion rotation, DGVector3 point)
	{
		DGFixedPoint x = rotation.x * (DGFixedPoint) 2F;
		DGFixedPoint y = rotation.y * (DGFixedPoint) 2F;
		DGFixedPoint z = rotation.z * (DGFixedPoint) 2F;
		DGFixedPoint xx = rotation.x * x;
		DGFixedPoint yy = rotation.y * y;
		DGFixedPoint zz = rotation.z * z;
		DGFixedPoint xy = rotation.x * y;
		DGFixedPoint xz = rotation.x * z;
		DGFixedPoint yz = rotation.y * z;
		DGFixedPoint wx = rotation.w * x;
		DGFixedPoint wy = rotation.w * y;
		DGFixedPoint wz = rotation.w * z;

		DGVector3 res;
		res.x = ((DGFixedPoint) 1 - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
		res.y = (xy + wz) * point.x + ((DGFixedPoint) 1 - (xx + zz)) * point.y + (yz - wx) * point.z;
		res.z = (xz - wy) * point.x + (yz + wx) * point.y + ((DGFixedPoint) 1 - (xx + yy)) * point.z;
		return res;
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	private static bool _IsEqualUsingDot(DGFixedPoint dot)
	{
		// Returns false in the presence of NaN values.
		return dot > (DGFixedPoint) 1 - kEpsilon;
	}

	public static bool IsUnit(DGQuaternion q)
	{
		return DGMath.IsApproximatelyZero((DGFixedPoint) 1 - q.w * q.w - q.x * q.x - q.y * q.y - q.z * q.z);
	}

	public static DGFixedPoint Dot(DGQuaternion value1, DGQuaternion value2)
	{
		return value1.x * value2.x + value1.y * value2.y + value1.z * value2.z + value1.w * value2.w;
	}

	public static DGQuaternion Cross(DGQuaternion value1, DGQuaternion value2)
	{
		var a = (value1.z * value2.y) - (value1.y * value2.z);
		var b = (value1.x * value2.z) - (value1.z * value2.x);
		var c = (value1.y * value2.x) - (value1.x * value2.y);
		var d = (value1.x * value2.x) - (value1.y * value2.y);
		var x = (value1.w * value2.x) + (value1.x * value2.w) + a;
		var y = (value1.w * value2.y) + (value1.y * value2.w) + b;
		var z = (value1.w * value2.z) + (value1.z * value2.w) + c;
		var w = (value1.w * value2.w) - (value1.z * value2.z) - d;
		return new DGQuaternion(x, y, z, w);
	}

	public static DGVector3 Transform(DGQuaternion value, DGVector3 vector)
	{
		var i = value.x;
		var j = value.y;
		var k = value.z;
		var u = value.w;
		var ii = i * i;
		var jj = j * j;
		var kk = k * k;
		var ui = u * i;
		var uj = u * j;
		var uk = u * k;
		var ij = i * j;
		var ik = i * k;
		var jk = j * k;
		var x = vector.x - ((DGFixedPoint) 2 * vector.x * (jj + kk)) + ((DGFixedPoint) 2 * vector.y * (ij - uk)) +
		        ((DGFixedPoint) 2 * vector.z * (ik + uj));
		var y = vector.y + ((DGFixedPoint) 2 * vector.x * (ij + uk)) - ((DGFixedPoint) 2 * vector.y * (ii + kk)) +
		        ((DGFixedPoint) 2 * vector.z * (jk - ui));
		var z = vector.z + ((DGFixedPoint) 2 * vector.x * (ik - uj)) + ((DGFixedPoint) 2 * vector.y * (jk + ui)) -
		        ((DGFixedPoint) 2 * vector.z * (ii + jj));
		return new DGVector3(x, y, z);
	}

	/// <summary>
	/// Transforms a vector using a quaternion. Specialized for x,0,0 vectors.
	/// </summary>
	/// <param name="x">X component of the vector to transform.</param>
	/// <param name="rotation">Rotation to apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static DGVector3 TransformX(DGFixedPoint x, DGQuaternion rotation)
	{
		//This operation is an optimized-down version of v' = q * v * q^-1.
		//The expanded form would be to treat v as an 'axis only' quaternion
		//and perform standard quaternion multiplication.  Assuming q is normalized,
		//q^-1 can be replaced by a conjugation.
		DGFixedPoint y2 = rotation.y + rotation.y;
		DGFixedPoint z2 = rotation.z + rotation.z;
		DGFixedPoint xy2 = rotation.x * y2;
		DGFixedPoint xz2 = rotation.x * z2;
		DGFixedPoint yy2 = rotation.y * y2;
		DGFixedPoint zz2 = rotation.z * z2;
		DGFixedPoint wy2 = rotation.w * y2;
		DGFixedPoint wz2 = rotation.w * z2;
		//Defer the component setting since they're used in computation.
		DGFixedPoint transformedX = x * (DGFixedPoint.One - yy2 - zz2);
		DGFixedPoint transformedY = x * (xy2 + wz2);
		DGFixedPoint transformedZ = x * (xz2 - wy2);
		return new DGVector3(transformedX, transformedY, transformedZ);
	}

	/// <summary>
	/// Transforms a vector using a quaternion. Specialized for 0,y,0 vectors.
	/// </summary>
	/// <param name="y">Y component of the vector to transform.</param>
	/// <param name="rotation">Rotation to apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static DGVector3 TransformY(DGFixedPoint y, DGQuaternion rotation)
	{
		//This operation is an optimized-down version of v' = q * v * q^-1.
		//The expanded form would be to treat v as an 'axis only' quaternion
		//and perform standard quaternion multiplication.  Assuming q is normalized,
		//q^-1 can be replaced by a conjugation.
		DGFixedPoint x2 = rotation.x + rotation.x;
		DGFixedPoint y2 = rotation.y + rotation.y;
		DGFixedPoint z2 = rotation.z + rotation.z;
		DGFixedPoint xx2 = rotation.x * x2;
		DGFixedPoint xy2 = rotation.x * y2;
		DGFixedPoint yz2 = rotation.y * z2;
		DGFixedPoint zz2 = rotation.z * z2;
		DGFixedPoint wx2 = rotation.w * x2;
		DGFixedPoint wz2 = rotation.w * z2;
		//Defer the component setting since they're used in computation.
		DGFixedPoint transformedX = y * (xy2 - wz2);
		DGFixedPoint transformedY = y * (DGFixedPoint.One - xx2 - zz2);
		DGFixedPoint transformedZ = y * (yz2 + wx2);
		return new DGVector3(transformedX, transformedY, transformedZ);
	}

	/// <summary>
	/// Transforms a vector using a quaternion. Specialized for 0,0,z vectors.
	/// </summary>
	/// <param name="z">Z component of the vector to transform.</param>
	/// <param name="rotation">Rotation to apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static DGVector3 TransformZ(DGFixedPoint z, DGQuaternion rotation)
	{
		//This operation is an optimized-down version of v' = q * v * q^-1.
		//The expanded form would be to treat v as an 'axis only' quaternion
		//and perform standard quaternion multiplication.  Assuming q is normalized,
		//q^-1 can be replaced by a conjugation.
		DGFixedPoint x2 = rotation.x + rotation.x;
		DGFixedPoint y2 = rotation.y + rotation.y;
		DGFixedPoint z2 = rotation.z + rotation.z;
		DGFixedPoint xx2 = rotation.x * x2;
		DGFixedPoint xz2 = rotation.x * z2;
		DGFixedPoint yy2 = rotation.y * y2;
		DGFixedPoint yz2 = rotation.y * z2;
		DGFixedPoint wx2 = rotation.w * x2;
		DGFixedPoint wy2 = rotation.w * y2;
		//Defer the component setting since they're used in computation.
		DGFixedPoint transformedX = z * (xz2 + wy2);
		DGFixedPoint transformedY = z * (yz2 - wx2);
		DGFixedPoint transformedZ = z * (DGFixedPoint.One - xx2 - yy2);
		return new DGVector3(transformedX, transformedY, transformedZ);
	}

	// Angle of rotation, in radians. Angles are measured anti-clockwise when viewed from the rotation axis (positive side) toward the origin.
	public static DGVector3 ToYawPitchRoll(DGQuaternion value, DGVector3 vector)
	{
		var sinrCosp = (DGFixedPoint) 2 * (value.w * value.z + value.x * value.y);
		var cosrCosp = (DGFixedPoint) 1 - (DGFixedPoint) 2 * (value.z * value.z + value.x * value.y);
		var z = DGMath.Atan2(sinrCosp, cosrCosp);
		// pitch (y-axis rotation)
		var sinp = (DGFixedPoint) 2 * (value.w * value.x - value.y * value.z);
		DGVector3 result = DGVector3.zero;
		if (DGMath.Abs(sinp) >= DGFixedPoint.One)
			result.y = DGMath.CopySign(DGMath.HalfPi, sinp);
		else
			result.y = DGMath.Asin(sinp);
		// yaw (z-axis rotation)
		var sinYcosp = (DGFixedPoint) 2 * (value.w * value.y + value.z + value.x);
		var cosYcosp = (DGFixedPoint) 1 - (DGFixedPoint) 2 * (value.x * value.x + value.y * value.y);
		result.x = DGMath.Atan2(sinYcosp, cosYcosp);
		return result;
	}

	public static DGQuaternion Normalize(DGQuaternion q)
	{
		DGFixedPoint num = DGMath.Sqrt(Dot(q, q));
		if (num < DGMath.Epsilon)
			return identity;
		return new DGQuaternion(q.x / num, q.y / num, q.z / num, q.w / num);
	}

	public static DGFixedPoint Angle(DGQuaternion a, DGQuaternion b)
	{
		DGFixedPoint dot = DGMath.Min(DGMath.Abs(Dot(a, b)), (DGFixedPoint) 1);
		return _IsEqualUsingDot(dot) ? DGFixedPoint.Zero : DGMath.Acos(dot) * (DGFixedPoint) 2 * DGMath.Rad2Deg;
	}

	// Makes euler angles positive 0/360 with 0.0001 hacked to support old behaviour of QuaternionToEuler
	private static DGVector3 Internal_MakePositive(DGVector3 euler)
	{
		DGFixedPoint negativeFlip = (DGFixedPoint) (-0.0001f) * DGMath.Rad2Deg;
		DGFixedPoint positiveFlip = (DGFixedPoint) 360 + negativeFlip;

		if (euler.x < negativeFlip)
			euler.x += (DGFixedPoint) 360;
		else if (euler.x > positiveFlip)
			euler.x -= (DGFixedPoint) 360;

		if (euler.y < negativeFlip)
			euler.y += (DGFixedPoint) 360;
		else if (euler.y > positiveFlip)
			euler.y -= (DGFixedPoint) 360;

		if (euler.z < negativeFlip)
			euler.z += (DGFixedPoint) 360;
		else if (euler.z > positiveFlip)
			euler.z -= (DGFixedPoint) 360;

		return euler;
	}

	public static DGQuaternion RotateTowards(DGQuaternion from, DGQuaternion to,
		DGFixedPoint maxDegreesDelta)
	{
		DGFixedPoint angle = Angle(from, to);
		if (angle == DGFixedPoint.Zero)
			return to;
		return SlerpUnclamped(from, to, DGMath.Min((DGFixedPoint) 1, maxDegreesDelta / angle));
	}

	public static DGQuaternion CreateFromAxisAngle(DGVector3 axis, DGFixedPoint angle)
	{
		return CreateFromAxisAngleRad(axis, angle * DGMath.Deg2Rad);
	}

	public static DGQuaternion CreateFromAxisAngleRad(DGVector3 axis, DGFixedPoint radians)
	{
		var theta = radians * DGMath.Half;
		var sin = DGMath.Sin(theta);
		var cos = DGMath.Cos(theta);
		var x = axis.x * sin;
		var y = axis.y * sin;
		var z = axis.z * sin;
		var w = cos;
		return new DGQuaternion(x, y, z, w);
	}

	/// <summary>
	///   <para>Creates a rotation which rotates /angle/ degrees around /axis/.</para>
	/// </summary>
	/// <param name="angle"></param>
	/// <param name="axis"></param>
	public static DGQuaternion AngleAxis(DGFixedPoint angle, DGVector3 axis)
	{
		return INTERNAL_CALL_AngleAxis(angle, ref axis);
	}

	private static DGQuaternion INTERNAL_CALL_AngleAxis(DGFixedPoint degress, ref DGVector3 axis)
	{
		if (axis.sqrMagnitude == (DGFixedPoint) 0.0f)
			return identity;

		DGQuaternion result = identity;
		var radians = degress * DGMath.Deg2Rad;
		radians *= (DGFixedPoint) 0.5f;
		axis.Normalize();
		axis = axis * DGMath.Sin(radians);
		result.x = axis.x;
		result.y = axis.y;
		result.z = axis.z;
		result.w = DGMath.Cos(radians);

		return Normalize(result);
	}


	/// <summary>
	///   <para>Creates a rotation which rotates from /fromDirection/ to /toDirection/.</para>
	/// </summary>
	/// <param name="fromDirection"></param>
	/// <param name="toDirection"></param>
	public static DGQuaternion FromToRotation(DGVector3 fromDirection, DGVector3 toDirection)
	{
		DGVector3 axis = DGVector3.Cross(fromDirection, toDirection);
		DGFixedPoint angle = DGVector3.Angle(fromDirection, toDirection);
		return AngleAxis(angle, axis.normalized);
	}


	/// <summary>
	///   <para>Creates a rotation with the specified /forward/ and /upwards/ directions.</para>
	/// </summary>
	/// <param name="forward">The direction to look in.</param>
	/// <param name="upwards">The vector that defines in which direction up is.</param>
	public static DGQuaternion LookRotation(DGVector3 forward, DGVector3 upwards)
	{
		return INTERNAL_CALL_LookRotation(ref forward, ref upwards);
	}

	public static DGQuaternion LookRotation(DGVector3 forward)
	{
		DGVector3 up = DGVector3.up;
		return INTERNAL_CALL_LookRotation(ref forward, ref up);
	}

	// from http://answers.unity3d.com/questions/467614/what-is-the-source-code-of-quaternionlookrotation.html
	private static DGQuaternion INTERNAL_CALL_LookRotation(ref DGVector3 forward, ref DGVector3 up)
	{
		forward = DGVector3.Normalize(forward);
		DGVector3 right = DGVector3.Normalize(DGVector3.Cross(up, forward));
		up = DGVector3.Cross(forward, right);
		var m00 = right.x;
		var m01 = right.y;
		var m02 = right.z;
		var m10 = up.x;
		var m11 = up.y;
		var m12 = up.z;
		var m20 = forward.x;
		var m21 = forward.y;
		var m22 = forward.z;


		DGFixedPoint num8 = (m00 + m11) + m22;
		var quaternion = new DGQuaternion(false);
		if (num8 > (DGFixedPoint) 0f)
		{
			var num = DGMath.Sqrt(num8 + (DGFixedPoint) 1f);
			quaternion.w = num * (DGFixedPoint) 0.5f;
			num = (DGFixedPoint) 0.5f / num;
			quaternion.x = (m12 - m21) * num;
			quaternion.y = (m20 - m02) * num;
			quaternion.z = (m01 - m10) * num;
			return quaternion;
		}

		if ((m00 >= m11) && (m00 >= m22))
		{
			var num7 = DGMath.Sqrt((((DGFixedPoint) 1f + m00) - m11) - m22);
			var num4 = (DGFixedPoint) 0.5f / num7;
			quaternion.x = (DGFixedPoint) 0.5f * num7;
			quaternion.y = (m01 + m10) * num4;
			quaternion.z = (m02 + m20) * num4;
			quaternion.w = (m12 - m21) * num4;
			return quaternion;
		}

		if (m11 > m22)
		{
			var num6 = DGMath.Sqrt((((DGFixedPoint) 1f + m11) - m00) - m22);
			var num3 = (DGFixedPoint) 0.5f / num6;
			quaternion.x = (m10 + m01) * num3;
			quaternion.y = (DGFixedPoint) 0.5f * num6;
			quaternion.z = (m21 + m12) * num3;
			quaternion.w = (m20 - m02) * num3;
			return quaternion;
		}

		var num5 = DGMath.Sqrt((((DGFixedPoint) 1f + m22) - m00) - m11);
		var num2 = (DGFixedPoint) 0.5f / num5;
		quaternion.x = (m20 + m02) * num2;
		quaternion.y = (m21 + m12) * num2;
		quaternion.z = (DGFixedPoint) 0.5f * num5;
		quaternion.w = (m01 - m10) * num2;
		return quaternion;
	}


	public static DGQuaternion CreateFromYawPitchRoll(DGFixedPoint yaw, DGFixedPoint pitch, DGFixedPoint roll)
	{
		var hr = roll * DGMath.Half;
		var hp = pitch * DGMath.Half;
		var hy = yaw * DGMath.Half;
		var shr = DGMath.Sin(hr);
		var chr = DGMath.Cos(hr);
		var shp = DGMath.Sin(hp);
		var chp = DGMath.Cos(hp);
		var shy = DGMath.Sin(hy);
		var chy = DGMath.Cos(hy);
		var x = (chy * shp * chr) + (shy * chp * shr);
		var y = (shy * chp * chr) - (chy * shp * shr);
		var z = (chy * chp * shr) - (shy * shp * chr);
		var w = (chy * chp * chr) + (shy * shp * shr);
		return new DGQuaternion(x, y, z, w);
	}

	

	/// <summary>
	/// Computes the axis angle representation of a normalized quaternion.
	/// </summary>
	/// <param name="q">Quaternion to be converted.</param>
	/// <param name="axis">Axis represented by the quaternion.GetAxisAngleFromQu</param>
	/// <param name="angle">Angle around the axis represented by the quaternion.</param>
	public static DGFixedPoint GetAxisAngleFromQuaternion(DGQuaternion q, DGVector3 axis)
	{
		DGFixedPoint qw = q.w;
		if (qw > DGFixedPoint.Zero)
		{
			axis.x = q.x;
			axis.y = q.y;
			axis.z = q.z;
		}
		else
		{
			axis.x = -q.x;
			axis.y = -q.y;
			axis.z = -q.z;
			qw = -qw;
		}

		DGFixedPoint lengthSquared = axis.sqrMagnitude;
		DGFixedPoint angle;
		if (lengthSquared > (DGFixedPoint) 1e-14m)
		{
			axis = axis / DGMath.Sqrt(lengthSquared);
			angle = (DGFixedPoint) 2 * DGMath.Acos(DGMath.Clamp(qw, (DGFixedPoint) (-1), DGFixedPoint.One));
		}
		else
		{
			axis = DGVector3.up;
			angle = DGFixedPoint.Zero;
		}

		return angle;
	}

	/// <summary>
	/// Computes the quaternion rotation between two normalized vectors.
	/// </summary>
	/// <param name="v1">First unit-length vector.</param>
	/// <param name="v2">Second unit-length vector.</param>
	/// <param name="q">Quaternion representing the rotation from v1 to v2.</param>
	public static DGQuaternion GetQuaternionBetweenNormalizedVectors(DGVector3 v1, DGVector3 v2)
	{
		DGFixedPoint dot = DGVector3.Dot(v1, v2);
		DGQuaternion q;
		//For non-normal vectors, the multiplying the axes length squared would be necessary:
		//Fix64 w = dot + (Fix64)Math.Sqrt(v1.LengthSquared() * v2.LengthSquared());
		if (dot < (DGFixedPoint) (-0.9999m)) //parallel, opposing direction
		{
			//If this occurs, the rotation required is ~180 degrees.
			//The problem is that we could choose any perpendicular axis for the rotation. It's not uniquely defined.
			//The solution is to pick an arbitrary perpendicular axis.
			//Project onto the plane which has the lowest component magnitude.
			//On that 2d plane, perform a 90 degree rotation.
			DGFixedPoint absX = DGFixedPoint.Abs(v1.x);
			DGFixedPoint absY = DGFixedPoint.Abs(v1.y);
			DGFixedPoint absZ = DGFixedPoint.Abs(v1.z);
			if (absX < absY && absX < absZ)
				q = new DGQuaternion(DGFixedPoint.Zero, -v1.z, v1.y, DGFixedPoint.Zero);
			else if (absY < absZ)
				q = new DGQuaternion(-v1.z, DGFixedPoint.Zero, v1.x, DGFixedPoint.Zero);
			else
				q = new DGQuaternion(-v1.y, v1.x, DGFixedPoint.Zero, DGFixedPoint.Zero);
		}
		else
		{
			DGVector3 axis = DGVector3.Cross(v1, v2);
			q = new DGQuaternion(axis.x, axis.y, axis.z, dot + DGFixedPoint.One);
		}

		q.Normalize();
		return q;
	}

	//The following two functions are highly similar, but it's a bit of a brain teaser to phrase one in terms of the other.
	//Providing both simplifies things.

	/// <summary>
	/// Computes the rotation from the start orientation to the end orientation such that end = Quaternion.Concatenate(start, relative).
	/// </summary>
	/// <param name="start">Starting orientation.</param>
	/// <param name="end">Ending orientation.</param>
	/// <param name="relative">Relative rotation from the start to the end orientation.</param>
	public static DGQuaternion GetRelativeRotation(DGQuaternion start, DGQuaternion end)
	{
		DGQuaternion startInverse = Conjugate(start);
		DGQuaternion relative = Concatenate(startInverse, end);
		return relative;
	}


	/// <summary>
	/// Transforms the rotation into the local space of the target basis such that rotation = Quaternion.Concatenate(localRotation, targetBasis)
	/// </summary>
	/// <param name="rotation">Rotation in the original frame of reference.</param>
	/// <param name="targetBasis">Basis in the original frame of reference to transform the rotation into.</param>
	/// <param name="localRotation">Rotation in the local space of the target basis.</param>
	public static DGQuaternion GetLocalRotation(DGQuaternion rotation, DGQuaternion targetBasis)
	{
		DGQuaternion basisInverse = Conjugate(targetBasis);
		DGQuaternion localRotation = Concatenate(rotation, basisInverse);
		return localRotation;
	}


	public static DGQuaternion Slerp(DGQuaternion start, DGQuaternion end, DGFixedPoint pct)
	{
		pct = DGMath.Clamp01(pct);
		return SlerpUnclamped(start, end, pct);
	}

	public static DGQuaternion SlerpUnclamped(DGQuaternion start, DGQuaternion end, DGFixedPoint pct)
	{
		var dot = start.x * end.x + start.y * end.y + start.z * end.z + start.w * end.w;


		if (dot < (DGFixedPoint)0)
		{
			dot = -dot;
			end = new DGQuaternion(-end.x, -end.y, -end.z, -end.w);
		}


		if (dot < (DGFixedPoint)0.95)
		{
			var angle = DGMath.Acos(dot);

			var invSinAngle = (DGFixedPoint)1 / DGMath.Sin(angle);

			var t1 = DGMath.Sin(((DGFixedPoint)1 - pct) * angle) * invSinAngle;

			var t2 = DGMath.Sin(pct * angle) * invSinAngle;

			return new DGQuaternion(start.x * t1 + end.x * t2, start.y * t1 + end.y * t2, start.z * t1 + end.z * t2,
				start.w * t1 + end.w * t2);
			;
		}

		var x = start.x + pct * (end.x - start.x);
		var y = start.y + pct * (end.y - start.y);
		var z = start.z + pct * (end.z - start.z);
		var w = start.w + pct * (end.w - start.w);
		return new DGQuaternion(x, y, z, w).normalized;
	}

	public static DGFixedPoint SqrMagnitude(DGQuaternion value)
	{
		return value.x * value.x + value.y * value.y + value.z * value.z + value.w + value.w;
	}

	public static DGFixedPoint Magnitude(DGQuaternion value)
	{
		return DGMath.Sqrt(SqrMagnitude(value));
	}

	//共轭，转置
	public static DGQuaternion Conjugate(DGQuaternion value)
	{
		var x = -value.x;
		var y = -value.y;
		var z = -value.z;
		var w = value.w;
		return new DGQuaternion(x, y, z, w);
	}

	public static DGQuaternion Inverse(DGQuaternion value)
	{
		var a = (value.x * value.x) + (value.y * value.y) + (value.z * value.z) + (value.w * value.w);
		var b = (DGFixedPoint) 1 / a;
		var x = -value.x * b;
		var y = -value.y * b;
		var z = -value.z * b;
		var w = value.w * b;
		return new DGQuaternion(x, y, z, w);
	}

	public static DGQuaternion Concatenate(DGQuaternion q1, DGQuaternion q2)
	{
		var a = (q1.z * q2.y) - (q1.y * q2.z);
		var b = (q1.x * q2.z) - (q1.z * q2.x);
		var c = (q1.y * q2.x) - (q1.x * q2.y);
		var d = (q1.x * q2.x) - (q1.y * q2.y);

		var x = (q1.w * q2.x) + (q1.x * q2.w) + a;
		var y = (q1.w * q2.y) + (q1.y * q2.w) + b;
		var z = (q1.w * q2.z) + (q1.z * q2.w) + c;
		var w = (q1.w * q2.w) - (q1.z * q2.z) - d;

		return new DGQuaternion(x, y, z, w);
	}

	public static DGQuaternion Euler(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		return Euler(new DGVector3(x, y, z));
	}

	public static DGQuaternion Euler(DGVector3 v)
	{
		return Internal_FromEulerRad(v * DGMath.Deg2Rad);
	}

	// from http://stackoverflow.com/questions/12088610/conversion-between-euler-quaternion-like-in-unity3d-engine
	private static DGVector3 Internal_ToEulerRad(DGQuaternion rotation)
	{
		DGFixedPoint sqw = rotation.w * rotation.w;
		DGFixedPoint sqx = rotation.x * rotation.x;
		DGFixedPoint sqy = rotation.y * rotation.y;
		DGFixedPoint sqz = rotation.z * rotation.z;
		DGFixedPoint unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
		DGFixedPoint test = rotation.x * rotation.w - rotation.y * rotation.z;
		DGVector3 v;

		if (test > (DGFixedPoint) 0.4995f * unit)
		{
			// singularity at north pole
			v.y = (DGFixedPoint) 2f * DGMath.Atan2(rotation.y, rotation.x);
			v.x = DGMath.HalfPi;
			v.z = (DGFixedPoint) 0;
			return NormalizeAngles(v * DGMath.Rad2Deg);
		}

		if (test < (DGFixedPoint) (-0.4995f) * unit)
		{
			// singularity at south pole
			v.y = (DGFixedPoint) (-2f) * DGMath.Atan2(rotation.y, rotation.x);
			v.x = -DGMath.HalfPi;
			v.z = (DGFixedPoint) 0;
			return NormalizeAngles(v * DGMath.Rad2Deg);
		}

		DGQuaternion q = new DGQuaternion(rotation.w, rotation.z, rotation.x, rotation.y);
		v.y = DGMath.Atan2((DGFixedPoint) 2f * q.x * q.w + (DGFixedPoint) 2f * q.y * q.z,
			(DGFixedPoint) 1 - (DGFixedPoint) 2f * (q.z * q.z + q.w * q.w)); // Yaw
		v.x = DGMath.Asin((DGFixedPoint) 2f * (q.x * q.z - q.w * q.y)); // Pitch
		v.z = DGMath.Atan2((DGFixedPoint) 2f * q.x * q.y + (DGFixedPoint) 2f * q.z * q.w,
			(DGFixedPoint) 1 - (DGFixedPoint) 2f * (q.y * q.y + q.z * q.z)); // Roll
		return NormalizeAngles(v * DGMath.Rad2Deg);
	}

	private static DGVector3 NormalizeAngles(DGVector3 angles)
	{
		angles.x = NormalizeAngle(angles.x);
		angles.y = NormalizeAngle(angles.y);
		angles.z = NormalizeAngle(angles.z);
		return angles;
	}

	private static DGFixedPoint NormalizeAngle(DGFixedPoint angle)
	{
		DGFixedPoint modAngle = angle % (DGFixedPoint) 360.0f;

		if (modAngle < (DGFixedPoint) 0.0f)
			return modAngle + (DGFixedPoint) 360.0f;
		return modAngle;
	}


	// from http://stackoverflow.com/questions/11492299/quaternion-to-euler-angles-algorithm-how-to-convert-to-y-up-and-between-ha
	private static DGQuaternion Internal_FromEulerRad(DGVector3 euler)
	{
		var yaw = euler.y;
		var pitch = euler.x;
		var roll = euler.z;
		DGFixedPoint rollOver2 = roll * (DGFixedPoint) 0.5f;
		DGFixedPoint sinRollOver2 = DGMath.Sin(rollOver2);
		DGFixedPoint cosRollOver2 = DGMath.Cos(rollOver2);
		DGFixedPoint pitchOver2 = pitch * (DGFixedPoint) 0.5f;
		DGFixedPoint sinPitchOver2 = DGMath.Sin(pitchOver2);
		DGFixedPoint cosPitchOver2 = DGMath.Cos(pitchOver2);
		DGFixedPoint yawOver2 = yaw * (DGFixedPoint) 0.5f;
		DGFixedPoint sinYawOver2 = DGMath.Sin(yawOver2);
		DGFixedPoint cosYawOver2 = DGMath.Cos(yawOver2);
		DGQuaternion result;
		result.x = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
		result.y = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
		result.z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
		result.w = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
		return result;
	}

	private static void Internal_ToAxisAngleRad(DGQuaternion q, out DGVector3 axis, out DGFixedPoint angle)
	{
		if (DGMath.Abs(q.w) > (DGFixedPoint) 1.0f)
			q.Normalize();


		angle = (DGFixedPoint) 2.0f * DGMath.Acos(q.w); // angle
		DGFixedPoint den = DGMath.Sqrt((DGFixedPoint) 1.0 - q.w * q.w);
		if (den > (DGFixedPoint) 0.0001f)
			axis = q.xyz / den;
		else
			// This occurs when the angle is zero. 
			// Not a problem: just set an arbitrary normalized axis.
			axis = new DGVector3(1, 0, 0);
	}


	/*************************************************************************************
	* 模块描述:Util
	*************************************************************************************/
	public void Normalize()
	{
		this = Normalize(this);
	}

	public DGFixedPoint SqrMagnitude()
	{
		return SqrMagnitude(this);
	}

	public DGFixedPoint Magnitude()
	{
		return Magnitude(this);
	}

	public void ToAngleAxis(out DGFixedPoint angle, out DGVector3 axis)
	{
		Internal_ToAxisAngleRad(this, out axis, out angle);
		angle *= DGMath.Rad2Deg;
	}

	/// <summary>
	///   <para>Creates a rotation which rotates from /fromDirection/ to /toDirection/.</para>
	/// </summary>
	/// <param name="fromDirection"></param>
	/// <param name="toDirection"></param>
	public void SetFromToRotation(DGVector3 fromDirection, DGVector3 toDirection)
	{
		this = FromToRotation(fromDirection, toDirection);
	}

	public void SetLookRotation(DGVector3 view)
	{
		DGVector3 up = DGVector3.up;
		this.SetLookRotation(view, up);
	}

	/// <summary>
	///   <para>Creates a rotation with the specified /forward/ and /upwards/ directions.</para>
	/// </summary>
	/// <param name="view">The direction to look in.</param>
	/// <param name="up">The vector that defines in which direction up is.</param>
	public void SetLookRotation(DGVector3 view, DGVector3 up)
	{
		this = LookRotation(view, up);
	}

	public void SetIdentity()
	{
		this.x = (DGFixedPoint) 0;
		this.y = (DGFixedPoint) 0;
		this.z = (DGFixedPoint) 0;
		this.w = (DGFixedPoint) 1;
	}
}