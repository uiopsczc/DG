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
using System.Runtime.CompilerServices;
using FP = DGFixedPoint;
using FPVector3 = DGVector3;
using FPVector4 = DGVector4;
using FPMatrix4x8 = DGMatrix4x8;
using FPQuaternion = DGQuaternion;
using FPPlane = DGPlane;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

public struct DGMatrix4x4
{
	/// <summary>
	/// Value at row 1, column 1 of the matrix.
	/// </summary>
	public FP M11;

	/// <summary>
	/// Value at row 1, column 2 of the matrix.
	/// </summary>
	public FP M12;

	/// <summary>
	/// Value at row 1, column 3 of the matrix.
	/// </summary>
	public FP M13;

	/// <summary>
	/// Value at row 1, column 4 of the matrix.
	/// </summary>
	public FP M14;

	/// <summary>
	/// Value at row 2, column 1 of the matrix.
	/// </summary>
	public FP M21;

	/// <summary>
	/// Value at row 2, column 2 of the matrix.
	/// </summary>
	public FP M22;

	/// <summary>
	/// Value at row 2, column 3 of the matrix.
	/// </summary>
	public FP M23;

	/// <summary>
	/// Value at row 2, column 4 of the matrix.
	/// </summary>
	public FP M24;

	/// <summary>
	/// Value at row 3, column 1 of the matrix.
	/// </summary>
	public FP M31;

	/// <summary>
	/// Value at row 3, column 2 of the matrix.
	/// </summary>
	public FP M32;

	/// <summary>
	/// Value at row 3, column 3 of the matrix.
	/// </summary>
	public FP M33;

	/// <summary>
	/// Value at row 3, column 4 of the matrix.
	/// </summary>
	public FP M34;

	/// <summary>
	/// Value at row 4, column 1 of the matrix.
	/// </summary>
	public FP M41;

	/// <summary>
	/// Value at row 4, column 2 of the matrix.
	/// </summary>
	public FP M42;

	/// <summary>
	/// Value at row 4, column 3 of the matrix.
	/// </summary>
	public FP M43;

	/// <summary>
	/// Value at row 4, column 4 of the matrix.
	/// </summary>
	public FP M44;

	/// <summary>
	/// Gets the 4x4 identity matrix.
	/// </summary>
	public static DGMatrix4x4 Identity
	{
		get
		{
			DGMatrix4x4 toReturn = default;
			toReturn.M11 = (FP) 1;
			toReturn.M12 = (FP) 0;
			toReturn.M13 = (FP) 0;
			toReturn.M14 = (FP) 0;

			toReturn.M21 = (FP) 0;
			toReturn.M22 = (FP) 1;
			toReturn.M23 = (FP) 0;
			toReturn.M24 = (FP) 0;

			toReturn.M31 = (FP) 0;
			toReturn.M32 = (FP) 0;
			toReturn.M33 = (FP) 1;
			toReturn.M34 = (FP) 0;

			toReturn.M41 = (FP) 0;
			toReturn.M42 = (FP) 0;
			toReturn.M43 = (FP) 0;
			toReturn.M44 = (FP) 1;
			return toReturn;
		}
	}

	/// <summary>
	/// Gets or sets the translation component of the transform.
	/// </summary>
	public FPVector3 Translation
	{
		get => new FPVector3()
		{
			x = M41,
			y = M42,
			z = M43
		};
		set
		{
			M41 = value.x;
			M42 = value.y;
			M43 = value.z;
		}
	}

	/// <summary>
	/// Gets or sets the backward vector of the matrix.
	/// </summary>
	public FPVector3 Backward
	{
		get
		{
			var x = M31;
			var y = M32;
			var z = M33;
			return new FPVector3(x, y, z);
		}
		set
		{
			M31 = value.x;
			M32 = value.y;
			M33 = value.z;
		}
	}

	/// <summary>
	/// Gets or sets the down vector of the matrix.
	/// </summary>
	public FPVector3 Down
	{
		get
		{
			var x = -M21;
			var y = -M22;
			var z = -M23;
			return new FPVector3(x, y, z);
		}
		set
		{
			M21 = -value.x;
			M22 = -value.y;
			M23 = -value.z;
		}
	}

	/// <summary>
	/// Gets or sets the forward vector of the matrix.
	/// </summary>
	public FPVector3 Forward
	{
		get
		{
			var x = -M31;
			var y = -M32;
			var z = -M33;
			return new FPVector3(x, y, z);
		}
		set
		{
			M31 = -value.x;
			M32 = -value.y;
			M33 = -value.z;
		}
	}

	/// <summary>
	/// Gets or sets the left vector of the matrix.
	/// </summary>
	public FPVector3 Left
	{
		get
		{
			var x = -M11;
			var y = -M12;
			var z = -M13;
			return new FPVector3(x, y, z);
		}
		set
		{
			M11 = -value.x;
			M12 = -value.y;
			M13 = -value.z;
		}
	}

	/// <summary>
	/// Gets or sets the right vector of the matrix.
	/// </summary>
	public FPVector3 Right
	{
		get
		{
			var x = M11;
			var y = M12;
			var z = M13;
			return new FPVector3(x, y, z);
		}
		set
		{
			M11 = value.x;
			M12 = value.y;
			M13 = value.z;
		}
	}

	/// <summary>
	/// Gets or sets the up vector of the matrix.
	/// </summary>
	public FPVector3 Up
	{
		get
		{
			var x = M21;
			var y = M22;
			var z = M23;
			return new FPVector3(x, y, z);
		}
		set
		{
			M21 = value.x;
			M22 = value.y;
			M23 = value.z;
		}
	}

	public DGMatrix4x4 inverse => Invert(this);
	public FP determinant => this.Determinant();

	/// <summary>
	/// Constructs a new 4 row, 4 column matrix.
	/// </summary>
	/// <param name="m11">Value at row 1, column 1 of the matrix.</param>
	/// <param name="m12">Value at row 1, column 2 of the matrix.</param>
	/// <param name="m13">Value at row 1, column 3 of the matrix.</param>
	/// <param name="m14">Value at row 1, column 4 of the matrix.</param>
	/// <param name="m21">Value at row 2, column 1 of the matrix.</param>
	/// <param name="m22">Value at row 2, column 2 of the matrix.</param>
	/// <param name="m23">Value at row 2, column 3 of the matrix.</param>
	/// <param name="m24">Value at row 2, column 4 of the matrix.</param>
	/// <param name="m31">Value at row 3, column 1 of the matrix.</param>
	/// <param name="m32">Value at row 3, column 2 of the matrix.</param>
	/// <param name="m33">Value at row 3, column 3 of the matrix.</param>
	/// <param name="m34">Value at row 3, column 4 of the matrix.</param>
	/// <param name="m41">Value at row 4, column 1 of the matrix.</param>
	/// <param name="m42">Value at row 4, column 2 of the matrix.</param>
	/// <param name="m43">Value at row 4, column 3 of the matrix.</param>
	/// <param name="m44">Value at row 4, column 4 of the matrix.</param>
	public DGMatrix4x4(FP m11, FP m12, FP m13, FP m14,
		FP m21, FP m22, FP m23, FP m24,
		FP m31, FP m32, FP m33, FP m34,
		FP m41, FP m42, FP m43, FP m44)
	{
		this.M11 = m11;
		this.M12 = m12;
		this.M13 = m13;
		this.M14 = m14;

		this.M21 = m21;
		this.M22 = m22;
		this.M23 = m23;
		this.M24 = m24;

		this.M31 = m31;
		this.M32 = m32;
		this.M33 = m33;
		this.M34 = m34;

		this.M41 = m41;
		this.M42 = m42;
		this.M43 = m43;
		this.M44 = m44;
	}

#if UNITY_5_3_OR_NEWER
	public DGMatrix4x4(Matrix4x4 matrix)
	{
		this.M11 = (FP)matrix.m00;
		this.M12 = (FP)matrix.m01;
		this.M13 = (FP)matrix.m02;
		this.M14 = (FP)matrix.m03;

		this.M21 = (FP)matrix.m10;
		this.M22 = (FP)matrix.m11;
		this.M23 = (FP)matrix.m12;
		this.M24 = (FP)matrix.m13;

		this.M31 = (FP)matrix.m20;
		this.M32 = (FP)matrix.m21;
		this.M33 = (FP)matrix.m22;
		this.M34 = (FP)matrix.m23;

		this.M41 = (FP)matrix.m30;
		this.M42 = (FP)matrix.m31;
		this.M43 = (FP)matrix.m32;
		this.M44 = (FP)matrix.m33;
	}
#endif

	public DGMatrix4x4(System.Numerics.Matrix4x4 matrix)
	{
		this.M11 = (FP)matrix.M11;
		this.M12 = (FP)matrix.M12;
		this.M13 = (FP)matrix.M13;
		this.M14 = (FP)matrix.M14;

		this.M21 = (FP)matrix.M21;
		this.M22 = (FP)matrix.M22;
		this.M23 = (FP)matrix.M23;
		this.M24 = (FP)matrix.M24;

		this.M31 = (FP)matrix.M31;
		this.M32 = (FP)matrix.M32;
		this.M33 = (FP)matrix.M33;
		this.M34 = (FP)matrix.M34;

		this.M41 = (FP)matrix.M41;
		this.M42 = (FP)matrix.M42;
		this.M43 = (FP)matrix.M43;
		this.M44 = (FP)matrix.M44;
	}

	/*************************************************************************************
	* 模块描述:ToString
	*************************************************************************************/
	/// <summary>
	/// Creates a string representation of the matrix.
	/// </summary>
	/// <returns>A string representation of the matrix.</returns>
	public override string ToString()
	{
		return "{" + M11 + ", " + M12 + ", " + M13 + ", " + M14 + "} \n" +
		       "{" + M21 + ", " + M22 + ", " + M23 + ", " + M24 + "} \n" +
		       "{" + M31 + ", " + M32 + ", " + M33 + ", " + M34 + "} \n" +
		       "{" + M41 + ", " + M42 + ", " + M43 + ", " + M44 + "} \n";
	}

	/*************************************************************************************
	* 模块描述:算数运算符
	*************************************************************************************/
	/// <summary>
	/// Multiplies two matrices together.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <returns>Combined transformation.</returns>
	public static DGMatrix4x4 operator *(DGMatrix4x4 a, DGMatrix4x4 b)
	{
		return Multiply(a, b);
	}

	/// <summary>
	/// Scales all components of the matrix by the given value.
	/// </summary>
	/// <param name="m">First matrix to multiply.</param>
	/// <param name="f">Scaling value to apply to all components of the matrix.</param>
	/// <returns>Product of the multiplication.</returns>
	public static DGMatrix4x4 operator *(DGMatrix4x4 m, FP f)
	{
		return Multiply(m, f);
	}

	/// <summary>
	/// Scales all components of the matrix by the given value.
	/// </summary>
	/// <param name="m">First matrix to multiply.</param>
	/// <param name="f">Scaling value to apply to all components of the matrix.</param>
	/// <returns>Product of the multiplication.</returns>
	public static DGMatrix4x4 operator *(FP f, DGMatrix4x4 m)
	{
		return Multiply(m, f);
	}

	public static FPVector4 operator *(DGMatrix4x4 lhs, FPVector4 vector)
	{
		FPVector4 res;
		res.x = lhs.M11 * vector.x + lhs.M12 * vector.y + lhs.M13 * vector.z + lhs.M14 * vector.w;
		res.y = lhs.M21 * vector.x + lhs.M22 * vector.y + lhs.M23 * vector.z + lhs.M24 * vector.w;
		res.z = lhs.M31 * vector.x + lhs.M32 * vector.y + lhs.M33 * vector.z + lhs.M34 * vector.w;
		res.w = lhs.M41 * vector.x + lhs.M42 * vector.y + lhs.M43 * vector.z + lhs.M44 * vector.w;
		return res;
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	public static DGMatrix4x4 TRS(FPVector3 pos, FPQuaternion rotation, FPVector3 scale)
	{
		return Translate(pos) * Rotate(rotation) * Scale(scale);
	}

	/// <summary>
		/// Creates a matrix representing the given axis and angle rotation.
		/// </summary>
		/// <param name="axis">Axis around which to rotate.</param>
		/// <param name="angle">Angle to rotate around the axis.</param>
		/// <param name="result">Matrix created from the axis and angle.</param>MultiplyPoint3x4
		public static DGMatrix4x4 CreateFromAxisAngle(FPVector3 axis, FP angle)
	{
		FP xx = axis.x * axis.x;
		FP yy = axis.y * axis.y;
		FP zz = axis.z * axis.z;
		FP xy = axis.x * axis.y;
		FP xz = axis.x * axis.z;
		FP yz = axis.y * axis.z;

		FP sinAngle = DGMath.Sin(angle);
		FP oneMinusCosAngle = (FP) 1 - DGMath.Cos(angle);

		DGMatrix4x4 result = default;
		result.M11 = (FP) 1 + oneMinusCosAngle * (xx - (FP) 1);
		result.M21 = -axis.z * sinAngle + oneMinusCosAngle * xy;
		result.M31 = axis.y * sinAngle + oneMinusCosAngle * xz;
		result.M41 = (FP) 0;

		result.M12 = axis.z * sinAngle + oneMinusCosAngle * xy;
		result.M22 = (FP) 1 + oneMinusCosAngle * (yy - (FP) 1);
		result.M32 = -axis.x * sinAngle + oneMinusCosAngle * yz;
		result.M42 = (FP) 0;

		result.M13 = -axis.y * sinAngle + oneMinusCosAngle * xz;
		result.M23 = axis.x * sinAngle + oneMinusCosAngle * yz;
		result.M33 = (FP) 1 + oneMinusCosAngle * (zz - (FP) 1);
		result.M43 = (FP) 0;

		result.M14 = (FP) 0;
		result.M24 = (FP) 0;
		result.M34 = (FP) 0;
		result.M44 = (FP) 1;

		return result;
	}

	/// <summary>
	/// Creates a rotation matrix from a quaternion.
	/// </summary>
	/// <param name="quaternion">Quaternion to convert.</param>
	/// <param name="result">Rotation matrix created from the quaternion.</param>
	public static DGMatrix4x4 CreateFromQuaternion(FPQuaternion quaternion)
	{
		FP qX2 = quaternion.x + quaternion.x;
		FP qY2 = quaternion.y + quaternion.y;
		FP qZ2 = quaternion.z + quaternion.z;
		FP XX = qX2 * quaternion.x;
		FP YY = qY2 * quaternion.y;
		FP ZZ = qZ2 * quaternion.z;
		FP XY = qX2 * quaternion.y;
		FP XZ = qX2 * quaternion.z;
		FP XW = qX2 * quaternion.w;
		FP YZ = qY2 * quaternion.z;
		FP YW = qY2 * quaternion.w;
		FP ZW = qZ2 * quaternion.w;

		DGMatrix4x4 result = default;

		result.M11 = (FP) 1 - YY - ZZ;
		result.M21 = XY - ZW;
		result.M31 = XZ + YW;
		result.M41 = (FP) 0;

		result.M12 = XY + ZW;
		result.M22 = (FP) 1 - XX - ZZ;
		result.M32 = YZ - XW;
		result.M42 = (FP) 0;

		result.M13 = XZ - YW;
		result.M23 = YZ + XW;
		result.M33 = (FP) 1 - XX - YY;
		result.M43 = (FP) 0;

		result.M14 = (FP) 0;
		result.M24 = (FP) 0;
		result.M34 = (FP) 0;
		result.M44 = (FP) 1;

		return result;
	}

	/// <summary>
	/// Multiplies two matrices together.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Combined transformation.</param>
	public static DGMatrix4x4 Multiply(DGMatrix4x4 a, DGMatrix4x4 b)
	{
		FP resultM11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41;
		FP resultM12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42;
		FP resultM13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43;
		FP resultM14 = a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44;

		FP resultM21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41;
		FP resultM22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42;
		FP resultM23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43;
		FP resultM24 = a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44;

		FP resultM31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41;
		FP resultM32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42;
		FP resultM33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43;
		FP resultM34 = a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44;

		FP resultM41 = a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41;
		FP resultM42 = a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42;
		FP resultM43 = a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43;
		FP resultM44 = a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44;

		DGMatrix4x4 result = default;
		result.M11 = resultM11;
		result.M12 = resultM12;
		result.M13 = resultM13;
		result.M14 = resultM14;

		result.M21 = resultM21;
		result.M22 = resultM22;
		result.M23 = resultM23;
		result.M24 = resultM24;

		result.M31 = resultM31;
		result.M32 = resultM32;
		result.M33 = resultM33;
		result.M34 = resultM34;

		result.M41 = resultM41;
		result.M42 = resultM42;
		result.M43 = resultM43;
		result.M44 = resultM44;
		return result;
	}


	/// <summary>
	/// Scales all components of the matrix.
	/// </summary>
	/// <param name="matrix">Matrix to scale.</param>
	/// <param name="scale">Amount to scale.</param>
	/// <param name="result">Scaled matrix.</param>
	public static DGMatrix4x4 Multiply(DGMatrix4x4 matrix, FP scale)
	{
		DGMatrix4x4 result = default;

		result.M11 = matrix.M11 * scale;
		result.M12 = matrix.M12 * scale;
		result.M13 = matrix.M13 * scale;
		result.M14 = matrix.M14 * scale;

		result.M21 = matrix.M21 * scale;
		result.M22 = matrix.M22 * scale;
		result.M23 = matrix.M23 * scale;
		result.M24 = matrix.M24 * scale;

		result.M31 = matrix.M31 * scale;
		result.M32 = matrix.M32 * scale;
		result.M33 = matrix.M33 * scale;
		result.M34 = matrix.M34 * scale;

		result.M41 = matrix.M41 * scale;
		result.M42 = matrix.M42 * scale;
		result.M43 = matrix.M43 * scale;
		result.M44 = matrix.M44 * scale;

		return result;
	}


	/// <summary>
	/// Transforms a vector using a matrix.
	/// </summary>
	/// <param name="v">Vector to transform.</param>
	/// <param name="matrix">Transform to apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static FPVector4 Transform(FPVector4 v, ref DGMatrix4x4 matrix)
	{
		FP vX = v.x;
		FP vY = v.y;
		FP vZ = v.z;
		FP vW = v.w;
		FPVector4 result = default;
		result.x = vX * matrix.M11 + vY * matrix.M21 + vZ * matrix.M31 + vW * matrix.M41;
		result.y = vX * matrix.M12 + vY * matrix.M22 + vZ * matrix.M32 + vW * matrix.M42;
		result.z = vX * matrix.M13 + vY * matrix.M23 + vZ * matrix.M33 + vW * matrix.M43;
		result.w = vX * matrix.M14 + vY * matrix.M24 + vZ * matrix.M34 + vW * matrix.M44;
		return result;
	}

	/// <summary>
	/// Transforms a vector using the transpose of a matrix.
	/// </summary>
	/// <param name="v">Vector to transform.</param>
	/// <param name="matrix">Transform to tranpose and apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static FPVector4 TransformTranspose(FPVector4 v, DGMatrix4x4 matrix)
	{
		FP vX = v.x;
		FP vY = v.y;
		FP vZ = v.z;
		FP vW = v.w;
		FPVector4 result = default;
		result.x = vX * matrix.M11 + vY * matrix.M12 + vZ * matrix.M13 + vW * matrix.M14;
		result.y = vX * matrix.M21 + vY * matrix.M22 + vZ * matrix.M23 + vW * matrix.M24;
		result.z = vX * matrix.M31 + vY * matrix.M32 + vZ * matrix.M33 + vW * matrix.M34;
		result.w = vX * matrix.M41 + vY * matrix.M42 + vZ * matrix.M43 + vW * matrix.M44;
		return result;
	}

	/// <summary>
	/// Transforms a vector using a matrix.
	/// </summary>
	/// <param name="v">Vector to transform.</param>
	/// <param name="matrix">Transform to apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static FPVector4 Transform(FPVector3 v, DGMatrix4x4 matrix)
	{
		FPVector4 result = default;
		result.x = v.x * matrix.M11 + v.y * matrix.M21 + v.z * matrix.M31 + matrix.M41;
		result.y = v.x * matrix.M12 + v.y * matrix.M22 + v.z * matrix.M32 + matrix.M42;
		result.z = v.x * matrix.M13 + v.y * matrix.M23 + v.z * matrix.M33 + matrix.M43;
		result.w = v.x * matrix.M14 + v.y * matrix.M24 + v.z * matrix.M34 + matrix.M44;
		return result;
	}


	/// <summary>
	/// Transforms a vector using the transpose of a matrix.
	/// </summary>
	/// <param name="v">Vector to transform.</param>
	/// <param name="matrix">Transform to tranpose and apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static FPVector3 TransformTranspose(FPVector3 v, DGMatrix4x4 matrix)
	{
		FP vX = v.x;
		FP vY = v.y;
		FP vZ = v.z;
		FPVector3 result = default;
		result.x = vX * matrix.M11 + vY * matrix.M12 + vZ * matrix.M13 + matrix.M14;
		result.y = vX * matrix.M21 + vY * matrix.M22 + vZ * matrix.M23 + matrix.M24;
		result.z = vX * matrix.M31 + vY * matrix.M32 + vZ * matrix.M33 + matrix.M34;
		return result;
	}

	/// <summary>
	/// Transforms a vector using a matrix.
	/// </summary>
	/// <param name="v">Vector to transform.</param>
	/// <param name="matrix">Transform to apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static FPVector3 TransformNormal(FPVector3 v, DGMatrix4x4 matrix)
	{
		FP vX = v.x;
		FP vY = v.y;
		FP vZ = v.z;
		FPVector3 result = default;
		result.x = vX * matrix.M11 + vY * matrix.M21 + vZ * matrix.M31;
		result.y = vX * matrix.M12 + vY * matrix.M22 + vZ * matrix.M32;
		result.z = vX * matrix.M13 + vY * matrix.M23 + vZ * matrix.M33;

		return result;
	}


	/// <summary>
	/// Transforms a vector using the transpose of a matrix.
	/// </summary>
	/// <param name="v">Vector to transform.</param>
	/// <param name="matrix">Transform to tranpose and apply to the vector.</param>
	/// <param name="result">Transformed vector.</param>
	public static FPVector3 TransformNormalTranspose(FPVector3 v, DGMatrix4x4 matrix)
	{
		FP vX = v.x;
		FP vY = v.y;
		FP vZ = v.z;
		FPVector3 result = default;
		result.x = vX * matrix.M11 + vY * matrix.M12 + vZ * matrix.M13;
		result.y = vX * matrix.M21 + vY * matrix.M22 + vZ * matrix.M23;
		result.z = vX * matrix.M31 + vY * matrix.M32 + vZ * matrix.M33;
		return result;
	}


	/// <summary>
	/// Transposes the matrix.
	/// </summary>
	/// <param name="m">Matrix to transpose.</param>
	/// <param name="transposed">Matrix to transpose.</param>
	public static DGMatrix4x4 Transpose(DGMatrix4x4 m)
	{
		FP intermediate = m.M12;
		DGMatrix4x4 transposed = default;
		transposed.M12 = m.M21;
		transposed.M21 = intermediate;

		intermediate = m.M13;
		transposed.M13 = m.M31;
		transposed.M31 = intermediate;

		intermediate = m.M14;
		transposed.M14 = m.M41;
		transposed.M41 = intermediate;

		intermediate = m.M23;
		transposed.M23 = m.M32;
		transposed.M32 = intermediate;

		intermediate = m.M24;
		transposed.M24 = m.M42;
		transposed.M42 = intermediate;

		intermediate = m.M34;
		transposed.M34 = m.M43;
		transposed.M43 = intermediate;

		transposed.M11 = m.M11;
		transposed.M22 = m.M22;
		transposed.M33 = m.M33;
		transposed.M44 = m.M44;

		return transposed;
	}

	/// <summary>
	/// Inverts the matrix.
	/// </summary>
	/// <param name="m">Matrix to invert.</param>
	/// <param name="inverted">Inverted version of the matrix.</param>
	public static DGMatrix4x4 Invert(DGMatrix4x4 m)
	{
		DGMatrix4x4 inverted = default;
		FPMatrix4x8.Invert(m, out inverted);
		return inverted;
	}


	/// <summary>
	/// Inverts the matrix using a process that only works for rigid transforms.
	/// </summary>
	/// <param name="m">Matrix to invert.</param>
	/// <param name="inverted">Inverted version of the matrix.</param>
	public static DGMatrix4x4 InvertRigid(DGMatrix4x4 m)
	{
		DGMatrix4x4 inverted = default;
		//Invert (transpose) the upper left 3x3 rotation.
		FP intermediate = m.M12;
		inverted.M12 = m.M21;
		inverted.M21 = intermediate;

		intermediate = m.M13;
		inverted.M13 = m.M31;
		inverted.M31 = intermediate;

		intermediate = m.M23;
		inverted.M23 = m.M32;
		inverted.M32 = intermediate;

		inverted.M11 = m.M11;
		inverted.M22 = m.M22;
		inverted.M33 = m.M33;

		//Translation component
		var vX = m.M41;
		var vY = m.M42;
		var vZ = m.M43;
		inverted.M41 = -(vX * inverted.M11 + vY * inverted.M21 + vZ * inverted.M31);
		inverted.M42 = -(vX * inverted.M12 + vY * inverted.M22 + vZ * inverted.M32);
		inverted.M43 = -(vX * inverted.M13 + vY * inverted.M23 + vZ * inverted.M33);

		//Last chunk.
		inverted.M14 = (FP) 0;
		inverted.M24 = (FP) 0;
		inverted.M34 = (FP) 0;
		inverted.M44 = (FP) 1;

		return inverted;
	}

	public static DGMatrix4x4 CreateRotationX(FP radians)
	{
		DGMatrix4x4 r = default;
		FP cos = DGMath.Cos(radians);
		FP sin = DGMath.Sin(radians);
		r.M11 = (FP) 1;
		r.M12 = (FP) 0;
		r.M13 = (FP) 0;
		r.M14 = (FP) 0;
		r.M21 = (FP) 0;
		r.M22 = cos;
		r.M23 = sin;
		r.M24 = (FP) 0;
		r.M31 = (FP) 0;
		r.M32 = -sin;
		r.M33 = cos;
		r.M34 = (FP) 0;
		r.M41 = (FP) 0;
		r.M42 = (FP) 0;
		r.M43 = (FP) 0;
		r.M44 = (FP) 1;
		return r;
	}

	public static DGMatrix4x4 CreateRotationY(FP radians)
	{
		DGMatrix4x4 r = default;
		FP cos = DGMath.Cos(radians);
		FP sin = DGMath.Sin(radians);
		r.M11 = cos;
		r.M12 = (FP) 0;
		r.M13 = -sin;
		r.M14 = (FP) 0;
		r.M21 = (FP) 0;
		r.M22 = (FP) 1;
		r.M23 = (FP) 0;
		r.M24 = (FP) 0;
		r.M31 = sin;
		r.M32 = (FP) 0;
		r.M33 = cos;
		r.M34 = (FP) 0;
		r.M41 = (FP) 0;
		r.M42 = (FP) 0;
		r.M43 = (FP) 0;
		r.M44 = (FP) 1;
		return r;
	}

	public static DGMatrix4x4 CreateRotationZ(FP radians)
	{
		DGMatrix4x4 r = default;
		FP cos = DGMath.Cos(radians);
		FP sin = DGMath.Sin(radians);
		r.M11 = cos;
		r.M12 = sin;
		r.M13 = (FP) 0;
		r.M14 = (FP) 0;
		r.M21 = -sin;
		r.M22 = cos;
		r.M23 = (FP) 0;
		r.M24 = (FP) 0;
		r.M31 = (FP) 0;
		r.M32 = (FP) 0;
		r.M33 = (FP) 1;
		r.M34 = (FP) 0;
		r.M41 = (FP) 0;
		r.M42 = (FP) 0;
		r.M43 = (FP) 0;
		r.M44 = (FP) 1;
		return r;
	}

	// Angle of rotation, in radians. Angles are measured anti-clockwise when viewed from the rotation axis (positive side) toward the origin.
	public static DGMatrix4x4 CreateFromYawPitchRoll(FP yaw, FP pitch, FP roll)
	{
		FPQuaternion quaternion = FPQuaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
		DGMatrix4x4 r = CreateFromQuaternion(quaternion);
		return r;
	}

	// Axes must be pair-wise perpendicular and have unit length.
	public static DGMatrix4x4 CreateFromCartesianAxes(FPVector3 right, FPVector3 up, FPVector3 backward)
	{
		DGMatrix4x4 r = default;
		r.M11 = right.x;
		r.M12 = right.y;
		r.M13 = right.z;
		r.M14 = (FP) 0;
		r.M21 = up.x;
		r.M22 = up.y;
		r.M23 = up.z;
		r.M24 = (FP) 0;
		r.M31 = backward.x;
		r.M32 = backward.y;
		r.M33 = backward.z;
		r.M34 = (FP) 0;
		r.M41 = (FP) 0;
		r.M42 = (FP) 0;
		r.M43 = (FP) 0;
		r.M44 = (FP) 1;
		return r;
	}

	/// <summary>
	/// Creates an orthographic perspective matrix from the given view volume dimensions.
	/// </summary>
	/// <param name="width">Width of the view volume.</param>
	/// <param name="height">Height of the view volume.</param>
	/// <param name="zNearPlane">Minimum Z-value of the view volume.</param>
	/// <param name="zFarPlane">Maximum Z-value of the view volume.</param>
	/// <returns>The orthographic projection matrix.</returns>
	public static DGMatrix4x4 CreateOrthographic(FP width, FP height, FP zNearPlane, FP zFarPlane)
	{
		DGMatrix4x4 result;

		result.M11 = (FP)2.0f / width;
		result.M12 = result.M13 = result.M14 = (FP)0.0f;

		result.M22 = (FP)2.0f / height;
		result.M21 = result.M23 = result.M24 = (FP)0.0f;

		result.M33 = (FP)1.0f / (zNearPlane - zFarPlane);
		result.M31 = result.M32 = result.M34 = (FP)0.0f;

		result.M41 = result.M42 = (FP)0.0f;
		result.M43 = zNearPlane / (zNearPlane - zFarPlane);
		result.M44 = (FP)1.0f;

		return result;
	}

	/// <summary>
	/// Creates a right handed orthographic projection.
	/// </summary>
	/// <param name="left">Leftmost coordinate of the projected area.</param>
	/// <param name="right">Rightmost coordinate of the projected area.</param>
	/// <param name="bottom">Bottom coordinate of the projected area.</param>
	/// <param name="top">Top coordinate of the projected area.</param>
	/// <param name="zNear">Near plane of the projection.</param>
	/// <param name="zFar">Far plane of the projection.</param>
	/// <param name="projection">The resulting orthographic projection matrix.</param>
	public static DGMatrix4x4 CreateOrthographicOffCenter(FP left, FP right, FP bottom, FP top, FP zNear, FP zFar)
	{
		DGMatrix4x4 projection = default;
		FP width = right - left;
		FP height = top - bottom;
		FP depth = zFar - zNear;
		projection.M11 = (FP) 2 / width;
		projection.M12 = (FP) 0;
		projection.M13 = (FP) 0;
		projection.M14 = (FP) 0;

		projection.M21 = (FP) 0;
		projection.M22 = (FP) 2 / height;
		projection.M23 = (FP) 0;
		projection.M24 = (FP) 0;

		projection.M31 = (FP) 0;
		projection.M32 = (FP) 0;
		projection.M33 = (FP) (-1) / depth;
		projection.M34 = (FP) 0;

		projection.M41 = (left + right) / -width;
		projection.M42 = (top + bottom) / -height;
		projection.M43 = zNear / -depth;
		projection.M44 = (FP) 1;

		return projection;
	}

	/// <summary>
	/// Creates a perspective projection matrix from the given view volume dimensions.
	/// </summary>
	/// <param name="width">Width of the view volume at the near view plane.</param>
	/// <param name="height">Height of the view volume at the near view plane.</param>
	/// <param name="nearPlaneDistance">Distance to the near view plane.</param>
	/// <param name="farPlaneDistance">Distance to the far view plane.</param>
	/// <returns>The perspective projection matrix.</returns>
	public static DGMatrix4x4 CreatePerspective(FP width, FP height, FP nearPlaneDistance, FP farPlaneDistance)
	{
		if (nearPlaneDistance <= (FP)0.0f)
			throw new ArgumentOutOfRangeException("nearPlaneDistance");

		if (farPlaneDistance <= (FP)0.0f)
			throw new ArgumentOutOfRangeException("farPlaneDistance");

		if (nearPlaneDistance >= farPlaneDistance)
			throw new ArgumentOutOfRangeException("nearPlaneDistance");

		DGMatrix4x4 result;

		result.M11 = (FP)2.0f * nearPlaneDistance / width;
		result.M12 = result.M13 = result.M14 = (FP)0.0f;

		result.M22 = (FP)2.0f * nearPlaneDistance / height;
		result.M21 = result.M23 = result.M24 = (FP)0.0f;

		result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
		result.M31 = result.M32 = (FP)0.0f;
		result.M34 = (FP) (- 1.0f);

		result.M41 = result.M42 = result.M44 = (FP)0.0f;
		result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);

		return result;
	}

	/// <summary>
	/// Creates a right-handed perspective matrix.
	/// </summary>
	/// <param name="fieldOfView">Field of view of the perspective in radians.</param>
	/// <param name="aspectRatio">Width of the viewport over the height of the viewport.</param>
	/// <param name="nearClip">Near clip plane of the perspective.</param>
	/// <param name="farClip">Far clip plane of the perspective.</param>
	/// <param name="perspective">Resulting perspective matrix.</param>
	public static DGMatrix4x4 CreatePerspectiveFieldOfView(FP fieldOfView, FP aspectRatio, FP nearClip, FP farClip)
	{
		DGMatrix4x4 perspective = default;
		FP h = (FP) 1 / DGMath.Tan(fieldOfView / (FP) 2);
		FP w = h / aspectRatio;
		perspective.M11 = w;
		perspective.M12 = (FP) 0;
		perspective.M13 = (FP) 0;
		perspective.M14 = (FP) 0;

		perspective.M21 = (FP) 0;
		perspective.M22 = h;
		perspective.M23 = (FP) 0;
		perspective.M24 = (FP) 0;

		perspective.M31 = (FP) 0;
		perspective.M32 = (FP) 0;
		perspective.M33 = farClip / (nearClip - farClip);
		perspective.M34 = (FP) (-1);

		perspective.M41 = (FP) 0;
		perspective.M42 = (FP) 0;
		perspective.M44 = (FP) 0;
		perspective.M43 = nearClip * perspective.M33;

		return perspective;
	}

	/// <summary>
	/// Linearly interpolates between the corresponding values of two matrices.
	/// </summary>
	/// <param name="matrix1">The first source matrix.</param>
	/// <param name="matrix2">The second source matrix.</param>
	/// <param name="amount">The relative weight of the second source matrix.</param>
	/// <returns>The interpolated matrix.</returns>
	public static DGMatrix4x4 Lerp(DGMatrix4x4 matrix1, DGMatrix4x4 matrix2, FP amount)
	{
		DGMatrix4x4 result;

		// First row
		result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
		result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
		result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
		result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;

		// Second row
		result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
		result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
		result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
		result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;

		// Third row
		result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
		result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
		result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
		result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;

		// Fourth row
		result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
		result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
		result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
		result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;

		return result;
	}

	/// <summary>
	/// Creates a customized, perspective projection matrix.
	/// </summary>
	/// <param name="left">Minimum x-value of the view volume at the near view plane.</param>
	/// <param name="right">Maximum x-value of the view volume at the near view plane.</param>
	/// <param name="bottom">Minimum y-value of the view volume at the near view plane.</param>
	/// <param name="top">Maximum y-value of the view volume at the near view plane.</param>
	/// <param name="nearPlaneDistance">Distance to the near view plane.</param>
	/// <param name="farPlaneDistance">Distance to of the far view plane.</param>
	/// <returns>The perspective projection matrix.</returns>
	public static DGMatrix4x4 CreatePerspectiveOffCenter(FP left, FP right, FP bottom, FP top, FP nearPlaneDistance, FP farPlaneDistance)
	{
		if (nearPlaneDistance <= (FP)0.0f)
			throw new ArgumentOutOfRangeException("nearPlaneDistance");

		if (farPlaneDistance <= (FP)0.0f)
			throw new ArgumentOutOfRangeException("farPlaneDistance");

		if (nearPlaneDistance >= (FP)farPlaneDistance)
			throw new ArgumentOutOfRangeException("nearPlaneDistance");

		DGMatrix4x4 result;

		result.M11 = (FP)2.0f * nearPlaneDistance / (right - left);
		result.M12 = result.M13 = result.M14 = (FP)0.0f;

		result.M22 = (FP)2.0f * nearPlaneDistance / (top - bottom);
		result.M21 = result.M23 = result.M24 = (FP)0.0f;

		result.M31 = (left + right) / (right - left);
		result.M32 = (top + bottom) / (top - bottom);
		result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
		result.M34 = (FP) (- 1.0f);

		result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
		result.M41 = result.M42 = result.M44 = (FP)0.0f;

		return result;
	}

	/// <summary>
	/// Creates a Matrix that reflects the coordinate system about a specified Plane.
	/// </summary>
	/// <param name="value">The Plane about which to create a reflection.</param>
	/// <returns>A new matrix expressing the reflection.</returns>
	public static DGMatrix4x4 CreateReflection(FPPlane value)
	{
		value = FPPlane.Normalize(value);

		FP a = value.normal.x;
		FP b = value.normal.y;
		FP c = value.normal.z;

		FP fa = (FP) (- 2.0f) * a;
		FP fb = (FP) (- 2.0f) * b;
		FP fc = (FP)(- 2.0f) * c;

		DGMatrix4x4 result;

		result.M11 = fa * a + (FP)1.0f;
		result.M12 = fb * a;
		result.M13 = fc * a;
		result.M14 = (FP)0.0f;

		result.M21 = fa * b;
		result.M22 = fb * b + (FP)1.0f;
		result.M23 = fc * b;
		result.M24 = (FP)0.0f;

		result.M31 = fa * c;
		result.M32 = fb * c;
		result.M33 = fc * c + (FP)1.0f;
		result.M34 = (FP)0.0f;

		result.M41 = fa * value.distance;
		result.M42 = fb * value.distance;
		result.M43 = fc * value.distance;
		result.M44 = (FP)1.0f;

		return result;
	}

	/// <summary>
	/// Creates a Matrix that flattens geometry into a specified Plane as if casting a shadow from a specified light source.
	/// </summary>
	/// <param name="lightDirection">The direction from which the light that will cast the shadow is coming.</param>
	/// <param name="plane">The Plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
	/// <returns>A new Matrix that can be used to flatten geometry onto the specified plane from the specified direction.</returns>
	public static DGMatrix4x4 CreateShadow(FPVector3 lightDirection, FPPlane plane)
	{
		FPPlane p = FPPlane.Normalize(plane);

		FP dot = p.normal.x * lightDirection.x + p.normal.y * lightDirection.y + p.normal.z * lightDirection.z;
		FP a = -p.normal.x;
		FP b = -p.normal.y;
		FP c = -p.normal.z;
		FP d = -p.distance;

		DGMatrix4x4 result;

		result.M11 = a * lightDirection.x + dot;
		result.M21 = b * lightDirection.x;
		result.M31 = c * lightDirection.x;
		result.M41 = d * lightDirection.x;

		result.M12 = a * lightDirection.y;
		result.M22 = b * lightDirection.y + dot;
		result.M32 = c * lightDirection.y;
		result.M42 = d * lightDirection.y;

		result.M13 = a * lightDirection.z;
		result.M23 = b * lightDirection.z;
		result.M33 = c * lightDirection.z + dot;
		result.M43 = d * lightDirection.z;

		result.M14 = (FP)0.0f;
		result.M24 = (FP)0.0f;
		result.M34 = (FP)0.0f;
		result.M44 = dot;

		return result;
	}

	



	/// <summary>
	/// Creates a view matrix pointing from a position to a target with the given up vector.
	/// </summary>
	/// <param name="position">Position of the camera.</param>
	/// <param name="target">Target of the camera.</param>
	/// <param name="upVector">Up vector of the camera.</param>
	/// <param name="viewMatrix">Look at matrix.</param>
	public static DGMatrix4x4 CreateLookAt(FPVector3 position, FPVector3 target, FPVector3 upVector)
	{
		FPVector3 forward = target - position;
		return CreateViewRH(position, forward, upVector);
	}


	/// <summary>
	/// Creates a view matrix pointing in a direction with a given up vector.
	/// </summary>
	/// <param name="position">Position of the camera.</param>
	/// <param name="forward">Forward direction of the camera.</param>
	/// <param name="upVector">Up vector of the camera.</param>
	/// <param name="viewMatrix">Look at matrix.</param>
	public static DGMatrix4x4 CreateViewRH(FPVector3 position, FPVector3 forward, FPVector3 upVector)
	{
		FP length = forward.magnitude;
		FPVector3 z = forward / -length;
		FPVector3 x = FPVector3.Cross(upVector, z);
		x.Normalize();
		FPVector3 y = FPVector3.Cross(z, x);
		DGMatrix4x4 viewMatrix = default;
		viewMatrix.M11 = x.x;
		viewMatrix.M12 = y.x;
		viewMatrix.M13 = z.x;
		viewMatrix.M14 = (FP) 0;
		viewMatrix.M21 = x.y;
		viewMatrix.M22 = y.y;
		viewMatrix.M23 = z.y;
		viewMatrix.M24 = (FP) 0;
		viewMatrix.M31 = x.z;
		viewMatrix.M32 = y.z;
		viewMatrix.M33 = z.z;
		viewMatrix.M34 = (FP) 0;
		viewMatrix.M41 = FPVector3.Dot(x, position);
		viewMatrix.M42 = FPVector3.Dot(y, position);
		viewMatrix.M43 = FPVector3.Dot(z, position);
		viewMatrix.M41 = -viewMatrix.M41;
		viewMatrix.M42 = -viewMatrix.M42;
		viewMatrix.M43 = -viewMatrix.M43;
		viewMatrix.M44 = (FP) 1;
		return viewMatrix;
	}


	/// <summary>
	/// Creates a world matrix pointing from a position to a target with the given up vector.
	/// </summary>
	/// <param name="position">Position of the transform.</param>
	/// <param name="forward">Forward direction of the transformation.</param>
	/// <param name="upVector">Up vector which is crossed against the forward vector to compute the transform's basis.</param>
	/// <param name="worldMatrix">World matrix.</param>
	public static DGMatrix4x4 CreateWorld(FPVector3 position, FPVector3 forward, FPVector3 upVector)
	{
		DGMatrix4x4 worldMatrix = default;

		FP length = forward.magnitude;
		FPVector3 z = forward / -length;
		FPVector3 x = FPVector3.Cross(upVector, z);
		x.Normalize();
		FPVector3 y = FPVector3.Cross(z, x);

		worldMatrix.M11 = x.x;
		worldMatrix.M12 = x.y;
		worldMatrix.M13 = x.z;
		worldMatrix.M14 = (FP) 0;
		worldMatrix.M21 = y.x;
		worldMatrix.M22 = y.y;
		worldMatrix.M23 = y.z;
		worldMatrix.M24 = (FP) 0;
		worldMatrix.M31 = z.x;
		worldMatrix.M32 = z.y;
		worldMatrix.M33 = z.z;
		worldMatrix.M34 = (FP) 0;

		worldMatrix.M41 = position.x;
		worldMatrix.M42 = position.y;
		worldMatrix.M43 = position.z;
		worldMatrix.M44 = (FP) 1;

		return worldMatrix;
	}


	/// <summary>
	/// Creates a matrix representing a translation.
	/// </summary>
	/// <param name="translation">Translation to be represented by the matrix.</param>
	/// <param name="translationMatrix">Matrix representing the given translation.</param>
	public static DGMatrix4x4 CreateTranslation(FPVector3 translation)
	{
		DGMatrix4x4 translationMatrix = new DGMatrix4x4
		{
			M11 = (FP) 1,
			M22 = (FP) 1,
			M33 = (FP) 1,
			M44 = (FP) 1,
			M41 = translation.x,
			M42 = translation.y,
			M43 = translation.z
		};

		return translationMatrix;
	}


	/// <summary>
	/// Creates a matrix representing the given axis aligned scale.
	/// </summary>
	/// <param name="scale">Scale to be represented by the matrix.</param>
	/// <param name="scaleMatrix">Matrix representing the given scale.</param>
	public static DGMatrix4x4 CreateScale(FPVector3 scale)
	{
		DGMatrix4x4 scaleMatrix = new DGMatrix4x4
		{
			M11 = scale.x,
			M22 = scale.y,
			M33 = scale.z,
			M44 = (FP) 1
		};
		return scaleMatrix;
	}

	/// <summary>
	/// Creates a matrix representing the given axis aligned scale.
	/// </summary>
	/// <param name="x">Scale along the x axis.</param>
	/// <param name="y">Scale along the y axis.</param>
	/// <param name="z">Scale along the z axis.</param>
	/// <param name="scaleMatrix">Matrix representing the given scale.</param>
	public static DGMatrix4x4 CreateScale(FP x, FP y, FP z)
	{
		DGMatrix4x4 scaleMatrix = new DGMatrix4x4
		{
			M11 = x,
			M22 = y,
			M33 = z,
			M44 = (FP) 1
		};
		return scaleMatrix;
	}

	public static DGMatrix4x4 Scale(FPVector3 scale)
	{
		return CreateScale(scale);
	}

	public static DGMatrix4x4 Translate(FPVector3 vector)
	{
		DGMatrix4x4 m = default;
		m.M11 = (FP) 1F;
		m.M12 = (FP) 0F;
		m.M13 = (FP) 0F;
		m.M14 = (FP) vector.x;
		m.M21 = (FP) 0F;
		m.M22 = (FP) 1F;
		m.M23 = (FP) 0F;
		m.M24 = (FP) vector.y;
		m.M31 = (FP) 0F;
		m.M32 = (FP) 0F;
		m.M33 = (FP) 1F;
		m.M34 = (FP) vector.z;
		m.M41 = (FP) 0F;
		m.M42 = (FP) 0F;
		m.M43 = (FP) 0F;
		m.M44 = (FP) 1F;
		return m;
	}


	// Creates a rotation matrix. Note: Assumes unit quaternion
	public static DGMatrix4x4 Rotate(FPQuaternion q)
	{
		// Precalculate coordinate products
		FP x = q.x * (FP) 2.0F;
		FP y = q.y * (FP) 2.0F;
		FP z = q.z * (FP) 2.0F;
		FP xx = q.x * x;
		FP yy = q.y * y;
		FP zz = q.z * z;
		FP xy = q.x * y;
		FP xz = q.x * z;
		FP yz = q.y * z;
		FP wx = q.w * x;
		FP wy = q.w * y;
		FP wz = q.w * z;

		// Calculate 3x3 matrix from orthonormal basis
		DGMatrix4x4 m = default;
		m.M11 = (FP) 1.0f - (yy + zz);
		m.M21 = xy + wz;
		m.M31 = xz - wy;
		m.M41 = (FP) 0.0F;
		m.M12 = xy - wz;
		m.M22 = (FP) 1.0f - (xx + zz);
		m.M32 = yz + wx;
		m.M42 = (FP) 0.0F;
		m.M13 = xz + wy;
		m.M23 = yz - wx;
		m.M33 = (FP) 1.0f - (xx + yy);
		m.M43 = (FP) 0.0F;
		m.M14 = (FP) 0.0F;
		m.M24 = (FP) 0.0F;
		m.M34 = (FP) 0.0F;
		m.M44 = (FP) 1.0F;
		return m;
	}

	/*************************************************************************************
	* 模块描述:Util
	*************************************************************************************/
	/// <summary>
	/// Creates a spherical billboard that rotates around a specified object position.
	/// </summary>
	/// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
	/// <param name="cameraPosition">Position of the camera.</param>
	/// <param name="cameraUpVector">The up vector of the camera.</param>
	/// <param name="cameraForwardVector">The forward vector of the camera.</param>
	/// <returns>The created billboard matrix</returns>
	public static DGMatrix4x4 CreateBillboard(FPVector3 objectPosition, FPVector3 cameraPosition, FPVector3 cameraUpVector, FPVector3 cameraForwardVector)
	{

		FPVector3 zaxis = new FPVector3(
			objectPosition.x - cameraPosition.x,
			objectPosition.y - cameraPosition.y,
			objectPosition.z - cameraPosition.z);

		FP norm = zaxis.sqrMagnitude;

		if (norm < DGMath.Epsilon)
			zaxis = -cameraForwardVector;
		else
			zaxis = zaxis * (FP)1.0f / DGMath.Sqrt(norm);

		FPVector3 xaxis = FPVector3.Normalize(FPVector3.Cross(cameraUpVector, zaxis));

		FPVector3 yaxis = FPVector3.Cross(zaxis, xaxis);

		DGMatrix4x4 result;

		result.M11 = xaxis.x;
		result.M12 = xaxis.y;
		result.M13 = xaxis.z;
		result.M14 = (FP)0.0f;
		result.M21 = yaxis.x;
		result.M22 = yaxis.y;
		result.M23 = yaxis.z;
		result.M24 = (FP)0.0f;
		result.M31 = zaxis.x;
		result.M32 = zaxis.y;
		result.M33 = zaxis.z;
		result.M34 = (FP)0.0f;

		result.M41 = objectPosition.x;
		result.M42 = objectPosition.y;
		result.M43 = objectPosition.z;
		result.M44 = (FP)1.0f;

		return result;
	}

	/// <summary>
	/// Creates a cylindrical billboard that rotates around a specified axis.
	/// </summary>
	/// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
	/// <param name="cameraPosition">Position of the camera.</param>
	/// <param name="rotateAxis">Axis to rotate the billboard around.</param>
	/// <param name="cameraForwardVector">Forward vector of the camera.</param>
	/// <param name="objectForwardVector">Forward vector of the object.</param>
	/// <returns>The created billboard matrix.</returns>
	public static DGMatrix4x4 CreateConstrainedBillboard(FPVector3 objectPosition, FPVector3 cameraPosition, FPVector3 rotateAxis, FPVector3 cameraForwardVector, FPVector3 objectForwardVector)
	{
		FP minAngle = (FP)1.0f - ((FP)0.1f * (DGMath.PI / (FP)180.0f)); // 0.1 degrees

		// Treat the case when object and camera positions are too close.
		FPVector3 faceDir = new FPVector3(
			objectPosition.x - cameraPosition.x,
			objectPosition.y - cameraPosition.y,
			objectPosition.z - cameraPosition.z);

		FP norm = faceDir.sqrMagnitude;

		if (norm < DGMath.Epsilon)
			faceDir = -cameraForwardVector;
		else
			faceDir = faceDir * ((FP)1.0f / DGMath.Sqrt(norm));

		FPVector3 yaxis = rotateAxis;
		FPVector3 xaxis;
		FPVector3 zaxis;

		// Treat the case when angle between faceDir and rotateAxis is too close to 0.
		FP dot = FPVector3.Dot(rotateAxis, faceDir);

		if (DGMath.Abs(dot) > minAngle)
		{
			zaxis = objectForwardVector;

			// Make sure passed values are useful for compute.
			dot = FPVector3.Dot(rotateAxis, zaxis);

			if (DGMath.Abs(dot) > minAngle)
			{
				zaxis = (DGMath.Abs(rotateAxis.z) > minAngle) ? new FPVector3(1, 0, 0) : new FPVector3(0, 0, -1);
			}

			xaxis = FPVector3.Normalize(FPVector3.Cross(rotateAxis, zaxis));
			zaxis = FPVector3.Normalize(FPVector3.Cross(xaxis, rotateAxis));
		}
		else
		{
			xaxis = FPVector3.Normalize(FPVector3.Cross(rotateAxis, faceDir));
			zaxis = FPVector3.Normalize(FPVector3.Cross(xaxis, yaxis));
		}

		DGMatrix4x4 result;

		result.M11 = xaxis.x;
		result.M12 = xaxis.y;
		result.M13 = xaxis.z;
		result.M14 = (FP)0.0f;
		result.M21 = yaxis.x;
		result.M22 = yaxis.y;
		result.M23 = yaxis.z;
		result.M24 = (FP)0.0f;
		result.M31 = zaxis.x;
		result.M32 = zaxis.y;
		result.M33 = zaxis.z;
		result.M34 = (FP)0.0f;

		result.M41 = objectPosition.x;
		result.M42 = objectPosition.y;
		result.M43 = objectPosition.z;
		result.M44 = (FP)1.0f;

		return result;
	}

	// Transforms a position by this matrix, with a perspective divide. (generic)
	public FPVector3 MultiplyPoint(FPVector3 point)
	{
		FPVector3 res;
		FP w;
		res.x = this.M11 * point.x + this.M12 * point.y + this.M13 * point.z + this.M14;
		res.y = this.M21 * point.x + this.M22 * point.y + this.M23 * point.z + this.M24;
		res.z = this.M31 * point.x + this.M32 * point.y + this.M33 * point.z + this.M34;
		w = this.M41 * point.x + this.M42 * point.y + this.M43 * point.z + this.M44;

		w = (FP)1F / w;
		res.x *= w;
		res.y *= w;
		res.z *= w;
		return res;
	}

	/// <summary>
	/// Computes the determinant of the matrix.
	/// </summary>
	/// <returns></returns>
	public FP Determinant()
	{
		//Compute the re-used 2x2 determinants.
		FP det1 = M33 * M44 - M34 * M43;
		FP det2 = M32 * M44 - M34 * M42;
		FP det3 = M32 * M43 - M33 * M42;
		FP det4 = M31 * M44 - M34 * M41;
		FP det5 = M31 * M43 - M33 * M41;
		FP det6 = M31 * M42 - M32 * M41;
		return
			(M11 * ((M22 * det1 - M23 * det2) + M24 * det3)) -
			(M12 * ((M21 * det1 - M23 * det4) + M24 * det5)) +
			(M13 * ((M21 * det2 - M22 * det4) + M24 * det6)) -
			(M14 * ((M21 * det3 - M22 * det5) + M23 * det6));
	}

	/// <summary>
	/// Transposes the matrix in-place.
	/// </summary>
	public void Transpose()
	{
		FP intermediate = M12;
		M12 = M21;
		M21 = intermediate;

		intermediate = M13;
		M13 = M31;
		M31 = intermediate;

		intermediate = M14;
		M14 = M41;
		M41 = intermediate;

		intermediate = M23;
		M23 = M32;
		M32 = intermediate;

		intermediate = M24;
		M24 = M42;
		M42 = intermediate;

		intermediate = M34;
		M34 = M43;
		M43 = intermediate;
	}

	public FPVector3 GetPosition()
	{
		return new FPVector3(M41, M42, M43);
	}

	

	// Transforms a position by this matrix, without a perspective divide. (fast)
	public FPVector3 MultiplyPoint3x4(FPVector3 point)
	{
		FPVector3 res;
		res.x = this.M11 * point.x + this.M12 * point.y + this.M13 * point.z + this.M14;
		res.y = this.M21 * point.x + this.M22 * point.y + this.M23 * point.z + this.M24;
		res.z = this.M31 * point.x + this.M32 * point.y + this.M33 * point.z + this.M34;
		return res;
	}

	// Transforms a direction by this matrix.
	public FPVector3 MultiplyVector(FPVector3 vector)
	{
		FPVector3 res;
		res.x = this.M11 * vector.x + this.M12 * vector.y + this.M13 * vector.z;
		res.y = this.M21 * vector.x + this.M22 * vector.y + this.M23 * vector.z;
		res.z = this.M31 * vector.x + this.M32 * vector.y + this.M33 * vector.z;
		return res;
	}
}