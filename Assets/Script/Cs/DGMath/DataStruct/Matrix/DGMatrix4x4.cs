/*************************************************************************************
 * ��    ��:  
 * �� �� ��:  czq
 * ����ʱ��:  2023/5/12
 * ======================================
 * ��ʷ���¼�¼
 * �汾:V          �޸�ʱ��:         �޸���:
 * �޸�����:
 * ======================================
*************************************************************************************/

using FP = DGFixedPoint;
using FPVector3 = DGVector3;
using FPVector4 = DGVector4;
using FPMatrix4x8 = DGMatrix4x8;
using FPQuaternion = DGQuaternion;

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

	/// <summary>
	/// Creates a matrix representing the given axis and angle rotation.
	/// </summary>
	/// <param name="axis">Axis around which to rotate.</param>
	/// <param name="angle">Angle to rotate around the axis.</param>
	/// <param name="result">Matrix created from the axis and angle.</param>
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
	/// Creates a right handed orthographic projection.
	/// </summary>
	/// <param name="left">Leftmost coordinate of the projected area.</param>
	/// <param name="right">Rightmost coordinate of the projected area.</param>
	/// <param name="bottom">Bottom coordinate of the projected area.</param>
	/// <param name="top">Top coordinate of the projected area.</param>
	/// <param name="zNear">Near plane of the projection.</param>
	/// <param name="zFar">Far plane of the projection.</param>
	/// <param name="projection">The resulting orthographic projection matrix.</param>
	public static DGMatrix4x4 CreateOrthographicRH(FP left, FP right, FP bottom, FP top, FP zNear, FP zFar)
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
	/// Creates a right-handed perspective matrix.
	/// </summary>
	/// <param name="fieldOfView">Field of view of the perspective in radians.</param>
	/// <param name="aspectRatio">Width of the viewport over the height of the viewport.</param>
	/// <param name="nearClip">Near clip plane of the perspective.</param>
	/// <param name="farClip">Far clip plane of the perspective.</param>
	/// <param name="perspective">Resulting perspective matrix.</param>
	public static DGMatrix4x4 CreatePerspectiveFieldOfViewRH(FP fieldOfView, FP aspectRatio, FP nearClip, FP farClip)
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
	/// Creates a view matrix pointing from a position to a target with the given up vector.
	/// </summary>
	/// <param name="position">Position of the camera.</param>
	/// <param name="target">Target of the camera.</param>
	/// <param name="upVector">Up vector of the camera.</param>
	/// <param name="viewMatrix">Look at matrix.</param>
	public static DGMatrix4x4 CreateLookAtRH(FPVector3 position, FPVector3 target, FPVector3 upVector)
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
	public static DGMatrix4x4 CreateWorldRH(FPVector3 position, FPVector3 forward, FPVector3 upVector)
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

	/// <summary>
	/// Creates a string representation of the matrix.
	/// </summary>
	/// <returns>A string representation of the matrix.</returns>
	public override string ToString()
	{
		return "{" + M11 + ", " + M12 + ", " + M13 + ", " + M14 + "} " +
		       "{" + M21 + ", " + M22 + ", " + M23 + ", " + M24 + "} " +
		       "{" + M31 + ", " + M32 + ", " + M33 + ", " + M34 + "} " +
		       "{" + M41 + ", " + M42 + ", " + M43 + ", " + M44 + "}";
	}
}