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
using FPMatrix4x4 = DGMatrix4x4;
#if UNITY_5_3_OR_NEWER
using UnityEngine;

#endif

//https://github.com/sungiant/abacus/blob/master/source/abacus/gen/main/Quaternion.t4
public struct DGQuaternion : IEquatable<DGQuaternion>
{
	public static readonly FP kEpsilon = (FP) 0.000001F;
	public static DGQuaternion identity = new DGQuaternion(0, 0, 0, 1);

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


	public DGQuaternion normalized => Normalize(this);
	public FP sqrMagnitude => SqrMagnitude();
	public FP magnitude => Magnitude();


	public DGQuaternion(FP x, FP y, FP z, FP w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}

	public DGQuaternion(float x, float y, float z, float w)
	{
		this.x = (FP) x;
		this.y = (FP) y;
		this.z = (FP) z;
		this.w = (FP) w;
	}

	public DGQuaternion(int x, int y, int z, int w)
	{
		this.x = (FP) x;
		this.y = (FP) y;
		this.z = (FP) z;
		this.w = (FP) w;
	}


#if UNITY_5_3_OR_NEWER
	public DGQuaternion(Quaternion quaternion)
	{
		this.x = (FP) quaternion.x;
		this.y = (FP) quaternion.y;
		this.z = (FP) quaternion.z;
		this.w = (FP) quaternion.w;
	}
#endif
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

	public override string ToString()
	{
		return string.Format("x:{0},y:{1},z:{2},w:{3}", x, y, z, w);
	}
	/*************************************************************************************
	* 模块描述:转换
	*************************************************************************************/
#if UNITY_5_3_OR_NEWER
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
		FP x = value1.x + value2.x;
		FP y = value1.y + value2.y;
		FP z = value1.z + value2.z;
		FP w = value1.w + value2.w;
		return new DGQuaternion(x, y, z, w);
	}

	public static DGQuaternion operator -(DGQuaternion value1, DGQuaternion value2)
	{
		FP x = value1.x - value2.x;
		FP y = value1.y - value2.y;
		FP z = value1.z - value2.z;
		FP w = value1.w - value2.w;
		return new DGQuaternion(x, y, z, w);
	}

	public static DGQuaternion operator -(DGQuaternion value)
	{
		FP x = -value.x;
		FP y = -value.y;
		FP z = -value.z;
		FP w = -value.w;
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
	public static FPVector3 operator *(DGQuaternion rotation, FPVector3 point)
	{
		FP x = rotation.x * (FP) 2F;
		FP y = rotation.y * (FP) 2F;
		FP z = rotation.z * (FP) 2F;
		FP xx = rotation.x * x;
		FP yy = rotation.y * y;
		FP zz = rotation.z * z;
		FP xy = rotation.x * y;
		FP xz = rotation.x * z;
		FP yz = rotation.y * z;
		FP wx = rotation.w * x;
		FP wy = rotation.w * y;
		FP wz = rotation.w * z;

		FPVector3 res;
		res.x = ((FP) 1 - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
		res.y = (xy + wz) * point.x + ((FP) 1 - (xx + zz)) * point.y + (yz - wx) * point.z;
		res.z = (xz - wy) * point.x + (yz + wx) * point.y + ((FP) 1 - (xx + yy)) * point.z;
		return res;
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	private static bool _IsEqualUsingDot(FP dot)
	{
		// Returns false in the presence of NaN values.
		return dot > (FP) 1 - kEpsilon;
	}

	public static bool IsUnit(DGQuaternion q)
	{
		return DGMath.IsApproximatelyZero((FP) 1 - q.w * q.w - q.x * q.x - q.y * q.y - q.z * q.z);
	}

	public static FP Dot(DGQuaternion value1, DGQuaternion value2)
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

	public static FPVector3 Transform(DGQuaternion value, FPVector3 vector)
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
		var x = vector.x - ((FP) 2 * vector.x * (jj + kk)) + ((FP) 2 * vector.y * (ij - uk)) +
		        ((FP) 2 * vector.z * (ik + uj));
		var y = vector.y + ((FP) 2 * vector.x * (ij + uk)) - ((FP) 2 * vector.y * (ii + kk)) +
		        ((FP) 2 * vector.z * (jk - ui));
		var z = vector.z + ((FP) 2 * vector.x * (ik - uj)) + ((FP) 2 * vector.y * (jk + ui)) -
		        ((FP) 2 * vector.z * (ii + jj));
		return new FPVector3(x, y, z);
	}

	/// <summary>
	/// Transforms a vector using a quaternion. Specialized for x,0,0 vectors.
	/// </summary>
	/// <param name="x">X component of the vector to transform.</param>
	/// <param name="rotation">Rotation to apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static FPVector3 TransformX(FP x, DGQuaternion rotation)
	{
		//This operation is an optimized-down version of v' = q * v * q^-1.
		//The expanded form would be to treat v as an 'axis only' quaternion
		//and perform standard quaternion multiplication.  Assuming q is normalized,
		//q^-1 can be replaced by a conjugation.
		FP y2 = rotation.y + rotation.y;
		FP z2 = rotation.z + rotation.z;
		FP xy2 = rotation.x * y2;
		FP xz2 = rotation.x * z2;
		FP yy2 = rotation.y * y2;
		FP zz2 = rotation.z * z2;
		FP wy2 = rotation.w * y2;
		FP wz2 = rotation.w * z2;
		//Defer the component setting since they're used in computation.
		FP transformedX = x * (FP.One - yy2 - zz2);
		FP transformedY = x * (xy2 + wz2);
		FP transformedZ = x * (xz2 - wy2);
		return new FPVector3(transformedX, transformedY, transformedZ);
	}

	/// <summary>
	/// Transforms a vector using a quaternion. Specialized for 0,y,0 vectors.
	/// </summary>
	/// <param name="y">Y component of the vector to transform.</param>
	/// <param name="rotation">Rotation to apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static FPVector3 TransformY(FP y, DGQuaternion rotation)
	{
		//This operation is an optimized-down version of v' = q * v * q^-1.
		//The expanded form would be to treat v as an 'axis only' quaternion
		//and perform standard quaternion multiplication.  Assuming q is normalized,
		//q^-1 can be replaced by a conjugation.
		FP x2 = rotation.x + rotation.x;
		FP y2 = rotation.y + rotation.y;
		FP z2 = rotation.z + rotation.z;
		FP xx2 = rotation.x * x2;
		FP xy2 = rotation.x * y2;
		FP yz2 = rotation.y * z2;
		FP zz2 = rotation.z * z2;
		FP wx2 = rotation.w * x2;
		FP wz2 = rotation.w * z2;
		//Defer the component setting since they're used in computation.
		FP transformedX = y * (xy2 - wz2);
		FP transformedY = y * (FP.One - xx2 - zz2);
		FP transformedZ = y * (yz2 + wx2);
		return new FPVector3(transformedX, transformedY, transformedZ);
	}

	/// <summary>
	/// Transforms a vector using a quaternion. Specialized for 0,0,z vectors.
	/// </summary>
	/// <param name="z">Z component of the vector to transform.</param>
	/// <param name="rotation">Rotation to apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static FPVector3 TransformZ(FP z, DGQuaternion rotation)
	{
		//This operation is an optimized-down version of v' = q * v * q^-1.
		//The expanded form would be to treat v as an 'axis only' quaternion
		//and perform standard quaternion multiplication.  Assuming q is normalized,
		//q^-1 can be replaced by a conjugation.
		FP x2 = rotation.x + rotation.x;
		FP y2 = rotation.y + rotation.y;
		FP z2 = rotation.z + rotation.z;
		FP xx2 = rotation.x * x2;
		FP xz2 = rotation.x * z2;
		FP yy2 = rotation.y * y2;
		FP yz2 = rotation.y * z2;
		FP wx2 = rotation.w * x2;
		FP wy2 = rotation.w * y2;
		//Defer the component setting since they're used in computation.
		FP transformedX = z * (xz2 + wy2);
		FP transformedY = z * (yz2 - wx2);
		FP transformedZ = z * (FP.One - xx2 - yy2);
		return new FPVector3(transformedX, transformedY, transformedZ);
	}

	// Angle of rotation, in radians. Angles are measured anti-clockwise when viewed from the rotation axis (positive side) toward the origin.
	public static FPVector3 ToYawPitchRoll(DGQuaternion value, FPVector3 vector)
	{
		var sinrCosp = (FP) 2 * (value.w * value.z + value.x * value.y);
		var cosrCosp = (FP) 1 - (FP) 2 * (value.z * value.z + value.x * value.y);
		var z = DGMath.Atan2(sinrCosp, cosrCosp);
		// pitch (y-axis rotation)
		var sinp = (FP) 2 * (value.w * value.x - value.y * value.z);
		FPVector3 result = FPVector3.zero;
		if (DGMath.Abs(sinp) >= FP.One)
			result.y = DGMath.CopySign(DGMath.HalfPi, sinp);
		else
			result.y = DGMath.Asin(sinp);
		// yaw (z-axis rotation)
		var sinYcosp = (FP) 2 * (value.w * value.y + value.z + value.x);
		var cosYcosp = (FP) 1 - (FP) 2 * (value.x * value.x + value.y * value.y);
		result.x = DGMath.Atan2(sinYcosp, cosYcosp);
		return result;
	}

	public static DGQuaternion Normalize(DGQuaternion q)
	{
		FP num = DGMath.Sqrt(Dot(q, q));
		if (num < DGMath.Epsilon)
			return identity;
		return new DGQuaternion(q.x / num, q.y / num, q.z / num, q.w / num);
	}

	public static FP Angle(DGQuaternion a, DGQuaternion b)
	{
		FP dot = DGMath.Min(DGMath.Abs(Dot(a, b)), (FP) 1);
		return _IsEqualUsingDot(dot) ? FP.Zero : DGMath.Acos(dot) * (FP) 2 * DGMath.Rad2Deg;
	}

	// Makes euler angles positive 0/360 with 0.0001 hacked to support old behaviour of QuaternionToEuler
	private static FPVector3 Internal_MakePositive(FPVector3 euler)
	{
		FP negativeFlip = (FP) (-0.0001f) * DGMath.Rad2Deg;
		FP positiveFlip = (FP) 360 + negativeFlip;

		if (euler.x < negativeFlip)
			euler.x += (FP) 360;
		else if (euler.x > positiveFlip)
			euler.x -= (FP) 360;

		if (euler.y < negativeFlip)
			euler.y += (FP) 360;
		else if (euler.y > positiveFlip)
			euler.y -= (FP) 360;

		if (euler.z < negativeFlip)
			euler.z += (FP) 360;
		else if (euler.z > positiveFlip)
			euler.z -= (FP) 360;

		return euler;
	}

	//	public static FPQuaternion RotateTowards(FPQuaternion from, FPQuaternion to,
	//		FP maxDegreesDelta)
	//	{
	//		FP angle = Angle(from, to);
	//		if (angle == FP.zero)
	//			return to;
	//		return SlerpUnclamped(from, to, DGMath.Min(1, maxDegreesDelta / angle));
	//	}

	public static DGQuaternion CreateFromAxisAngle(FPVector3 axis, FP angle)
	{
		var theta = angle * DGMath.Half;
		var sin = DGMath.Sin(theta);
		var cos = DGMath.Cos(theta);
		var x = axis.x * sin;
		var y = axis.y * sin;
		var z = axis.z * sin;
		var w = cos;
		return new DGQuaternion(x, y, z, w);
	}

	public static DGQuaternion CreateFromYawPitchRoll(FP yaw, FP pitch, FP roll)
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

	// http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/
	public static DGQuaternion CreateFromRotationMatrix(FPMatrix4x4 m)
	{
		DGQuaternion r = default;
		FP tr = m.M11 + m.M22 + m.M33; //Vector3
		if (tr > (FP) 0)
		{
			FP s = DGMath.Sqrt(tr + (FP) 1) * (FP) 2;
			r.w = DGMath.Quarter * s;
			r.x = (m.M23 - m.M32) / s;
			r.y = (m.M31 - m.M13) / s;
			r.z = (m.M12 - m.M21) / s;
		}
		else if ((m.M11 >= m.M22) && (m.M11 >= m.M33))
		{
			FP s = DGMath.Sqrt((FP) 1 + m.M11 - m.M22 - m.M33) * (FP) 2;
			r.w = (m.M23 - m.M32) / s;
			r.x = DGMath.Quarter * s;
			r.y = (m.M12 + m.M21) / s;
			r.z = (m.M13 + m.M31) / s;
		}
		else if (m.M22 > m.M33)
		{
			FP s = DGMath.Sqrt((FP) 1 + m.M22 - m.M11 - m.M33) * (FP) 2;
			r.w = (m.M31 - m.M13) / s;
			r.x = (m.M21 + m.M12) / s;
			r.y = DGMath.Quarter * s;
			r.z = (m.M32 + m.M23) / s;
		}
		else
		{
			FP s = DGMath.Sqrt((FP) 1 + m.M33 - m.M11 - m.M22) * (FP) 2;
			r.w = (m.M12 - m.M21) / s;
			r.x = (m.M31 + m.M13) / s;
			r.y = (m.M32 + m.M23) / s;
			r.z = DGMath.Quarter * s;
		}

		return r;
	}

	/// <summary>
	/// Computes the axis angle representation of a normalized quaternion.
	/// </summary>
	/// <param name="q">Quaternion to be converted.</param>
	/// <param name="axis">Axis represented by the quaternion.GetAxisAngleFromQu</param>
	/// <param name="angle">Angle around the axis represented by the quaternion.</param>
	public static FP GetAxisAngleFromQuaternion(DGQuaternion q, FPVector3 axis)
	{
		FP qw = q.w;
		if (qw > FP.Zero)
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

		FP lengthSquared = axis.sqrMagnitude;
		FP angle;
		if (lengthSquared > (FP) 1e-14m)
		{
			axis = axis / DGMath.Sqrt(lengthSquared);
			angle = (FP) 2 * DGMath.Acos(DGMath.Clamp(qw, (FP) (-1), FP.One));
		}
		else
		{
			axis = FPVector3.up;
			angle = FP.Zero;
		}

		return angle;
	}

	/// <summary>
	/// Computes the quaternion rotation between two normalized vectors.
	/// </summary>
	/// <param name="v1">First unit-length vector.</param>
	/// <param name="v2">Second unit-length vector.</param>
	/// <param name="q">Quaternion representing the rotation from v1 to v2.</param>
	public static DGQuaternion GetQuaternionBetweenNormalizedVectors(FPVector3 v1, FPVector3 v2)
	{
		FP dot = FPVector3.Dot(v1, v2);
		DGQuaternion q;
		//For non-normal vectors, the multiplying the axes length squared would be necessary:
		//Fix64 w = dot + (Fix64)Math.Sqrt(v1.LengthSquared() * v2.LengthSquared());
		if (dot < (FP) (-0.9999m)) //parallel, opposing direction
		{
			//If this occurs, the rotation required is ~180 degrees.
			//The problem is that we could choose any perpendicular axis for the rotation. It's not uniquely defined.
			//The solution is to pick an arbitrary perpendicular axis.
			//Project onto the plane which has the lowest component magnitude.
			//On that 2d plane, perform a 90 degree rotation.
			FP absX = FP.Abs(v1.x);
			FP absY = FP.Abs(v1.y);
			FP absZ = FP.Abs(v1.z);
			if (absX < absY && absX < absZ)
				q = new DGQuaternion(FP.Zero, -v1.z, v1.y, FP.Zero);
			else if (absY < absZ)
				q = new DGQuaternion(-v1.z, FP.Zero, v1.x, FP.Zero);
			else
				q = new DGQuaternion(-v1.y, v1.x, FP.Zero, FP.Zero);
		}
		else
		{
			FPVector3 axis = FPVector3.Cross(v1, v2);
			q = new DGQuaternion(axis.x, axis.y, axis.z, dot + FP.One);
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


	public static DGQuaternion Lerp(DGQuaternion from, DGQuaternion to, FP t)
	{
		t = DGMath.Clamp(t, (FP) 0, (FP) 1);
		var remaining = (FP) 1 - t;
		var x = (remaining * from.x) + (t * to.x);
		var y = (remaining * from.y) + (t * to.y);
		var z = (remaining * from.z) + (t * to.z);
		var w = (remaining * from.w) + (t * to.w);
		return new DGQuaternion(x, y, z, w);
	}

	// http://en.wikipedia.org/wiki/Slerp
	public static DGQuaternion Slerp(DGQuaternion from, DGQuaternion to, FP t)
	{
		t = DGMath.Clamp(t, (FP) 0, (FP) 1);
		var remaining = (FP) 1 - t;
		var angle = Dot(from, to);
		if (angle < (FP) 0)
		{
			from = -from;
			angle = -angle;
		}

		var theta = DGMath.Acos(angle);
		var f = remaining;
		var a = t;
		if (theta > kEpsilon)
		{
			var x = DGMath.Sin(remaining * theta);
			var y = DGMath.Sin(t * theta);
			var z = DGMath.Sin(theta);
			f = x / z;
			a = y / z;
		}

		var resultX = (f * from.x) + (a * to.x);
		var resultY = (f * from.y) + (a * to.y);
		var resultZ = (f * from.z) + (a * to.z);
		var resultW = (f * from.w) + (a * to.w);
		return new DGQuaternion(resultX, resultY, resultZ, resultW);
	}

	public static FP SqrMagnitude(DGQuaternion value)
	{
		return value.x * value.x + value.y * value.y + value.z * value.z + value.w + value.w;
	}

	public static FP Magnitude(DGQuaternion value)
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
		var b = (FP) 1 / a;
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


	/*************************************************************************************
	* 模块描述:Util
	*************************************************************************************/
	public void Normalize()
	{
		this = Normalize(this);
	}

	public FP SqrMagnitude()
	{
		return SqrMagnitude(this);
	}

	public FP Magnitude()
	{
		return Magnitude(this);
	}
}