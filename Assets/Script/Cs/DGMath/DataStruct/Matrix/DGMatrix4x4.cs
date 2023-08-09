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
using FPVector4 = DGVector4;
using FPMatrix4x8 = DGMatrix4x8;
using FPQuaternion = DGQuaternion;
using FPPlane = DGPlane;
#if UNITY_5_3_OR_NEWER
using UnityEngine;

#endif

/// <summary>
/// github:Bepu
/// </summary>
public partial struct DGMatrix4x4
{
	/// <summary>
	/// Value at row 1, column 1 of the matrix.
	/// </summary>
	public FP SM11
	{
		get => val[M00];
		set => val[M00] = value;
	}

	/// <summary>
	/// Value at row 1, column 2 of the matrix.
	/// </summary>
	public FP SM12
	{
		get => val[M01];
		set => val[M01] = value;
	}

	/// <summary>
	/// Value at row 1, column 3 of the matrix.
	/// </summary>
	public FP SM13
	{
		get => val[M02];
		set => val[M02] = value;
	}

	/// <summary>
	/// Value at row 1, column 4 of the matrix.
	/// </summary>
	public FP SM14
	{
		get => val[M03];
		set => val[M03] = value;
	}

	/// <summary>
	/// Value at row 2, column 1 of the matrix.
	/// </summary>
	public FP SM21
	{
		get => val[M10];
		set => val[M10] = value;
	}

	/// <summary>
	/// Value at row 2, column 2 of the matrix.
	/// </summary>
	public FP SM22
	{
		get => val[M11];
		set => val[M11] = value;
	}

	/// <summary>
	/// Value at row 2, column 3 of the matrix.
	/// </summary>
	public FP SM23
	{
		get => val[M12];
		set => val[M12] = value;
	}

	/// <summary>
	/// Value at row 2, column 4 of the matrix.
	/// </summary>
	public FP SM24
	{
		get => val[M13];
		set => val[M13] = value;
	}

	/// <summary>
	/// Value at row 3, column 1 of the matrix.
	/// </summary>
	public FP SM31
	{
		get => val[M20];
		set => val[M20] = value;
	}

	/// <summary>
	/// Value at row 3, column 2 of the matrix.
	/// </summary>
	public FP SM32
	{
		get => val[M21];
		set => val[M21] = value;
	}

	/// <summary>
	/// Value at row 3, column 3 of the matrix.
	/// </summary>
	public FP SM33
	{
		get => val[M22];
		set => val[M22] = value;
	}

	/// <summary>
	/// Value at row 3, column 4 of the matrix.
	/// </summary>
	public FP SM34
	{
		get => val[M23];
		set => val[M23] = value;
	}

	/// <summary>
	/// Value at row 4, column 1 of the matrix.
	/// </summary>
	public FP SM41
	{
		get => val[M30];
		set => val[M30] = value;
	}

	/// <summary>
	/// Value at row 4, column 2 of the matrix.
	/// </summary>
	public FP SM42
	{
		get => val[M31];
		set => val[M31] = value;
	}

	/// <summary>
	/// Value at row 4, column 3 of the matrix.
	/// </summary>
	public FP SM43
	{
		get => val[M32];
		set => val[M32] = value;
	}

	/// <summary>
	/// Value at row 4, column 4 of the matrix.
	/// </summary>
	public FP SM44
	{
		get => val[M33];
		set => val[M33] = value;
	}


	/// <summary>
	/// Gets the 4x4 identity matrix.
	/// </summary>
	public static DGMatrix4x4 Identity
	{
		get
		{
			DGMatrix4x4 toReturn = new DGMatrix4x4(false);
			toReturn.SM11 = (FP) 1;
			toReturn.SM12 = (FP) 0;
			toReturn.SM13 = (FP) 0;
			toReturn.SM14 = (FP) 0;

			toReturn.SM21 = (FP) 0;
			toReturn.SM22 = (FP) 1;
			toReturn.SM23 = (FP) 0;
			toReturn.SM24 = (FP) 0;

			toReturn.SM31 = (FP) 0;
			toReturn.SM32 = (FP) 0;
			toReturn.SM33 = (FP) 1;
			toReturn.SM34 = (FP) 0;

			toReturn.SM41 = (FP) 0;
			toReturn.SM42 = (FP) 0;
			toReturn.SM43 = (FP) 0;
			toReturn.SM44 = (FP) 1;
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
			x = SM14,
			y = SM24,
			z = SM34
		};
		set
		{
			SM14 = value.x;
			SM24 = value.y;
			SM34 = value.z;
		}
	}


	/// <summary>
	/// Gets or sets the backward vector of the matrix.
	/// </summary>
	public FPVector3 Backward
	{
		get
		{
			var x = -SM13;
			var y = -SM23;
			var z = -SM33;
			return new FPVector3(x, y, z);
		}
		set
		{
			SM13 = -value.x;
			SM23 = -value.y;
			SM33 = -value.z;
		}
	}

	/// <summary>
	/// Gets or sets the down vector of the matrix.
	/// </summary>
	public FPVector3 Down
	{
		get
		{
			var x = -SM12;
			var y = -SM22;
			var z = -SM32;
			return new FPVector3(x, y, z);
		}
		set
		{
			SM12 = -value.x;
			SM22 = -value.y;
			SM32 = -value.z;
		}
	}

	/// <summary>
	/// Gets or sets the forward vector of the matrix.
	/// </summary>
	public FPVector3 Forward
	{
		get
		{
			var x = SM13;
			var y = SM23;
			var z = SM33;
			return new FPVector3(x, y, z);
		}
		set
		{
			SM13 = value.x;
			SM23 = value.y;
			SM33 = value.z;
		}
	}

	/// <summary>
	/// Gets or sets the left vector of the matrix.
	/// </summary>
	public FPVector3 Left
	{
		get
		{
			var x = -SM11;
			var y = -SM21;
			var z = -SM31;
			return new FPVector3(x, y, z);
		}
		set
		{
			SM11 = -value.x;
			SM21 = -value.y;
			SM31 = -value.z;
		}
	}

	/// <summary>
	/// Gets or sets the right vector of the matrix.
	/// </summary>
	public FPVector3 Right
	{
		get
		{
			var x = SM11;
			var y = SM21;
			var z = SM31;
			return new FPVector3(x, y, z);
		}
		set
		{
			SM11 = value.x;
			SM21 = value.y;
			SM31 = value.z;
		}
	}

	/// <summary>
	/// Gets or sets the up vector of the matrix.
	/// </summary>
	public FPVector3 Up
	{
		get
		{
			var x = SM12;
			var y = SM22;
			var z = SM32;
			return new FPVector3(x, y, z);
		}
		set
		{
			SM12 = value.x;
			SM22 = value.y;
			SM32 = value.z;
		}
	}


	public DGMatrix4x4 inverse => Invert(this);
	public FP determinant => this.Determinant();

	/// <summary>
	/// Constructs a new 4 row, 4 column matrix.
	/// </summary>
	/// <param name="sm11">Value at row 1, column 1 of the matrix.</param>
	/// <param name="sm12">Value at row 1, column 2 of the matrix.</param>
	/// <param name="sm13">Value at row 1, column 3 of the matrix.</param>
	/// <param name="sm14">Value at row 1, column 4 of the matrix.</param>
	/// <param name="sm21">Value at row 2, column 1 of the matrix.</param>
	/// <param name="sm22">Value at row 2, column 2 of the matrix.</param>
	/// <param name="sm23">Value at row 2, column 3 of the matrix.</param>
	/// <param name="sm24">Value at row 2, column 4 of the matrix.</param>
	/// <param name="sm31">Value at row 3, column 1 of the matrix.</param>
	/// <param name="sm32">Value at row 3, column 2 of the matrix.</param>
	/// <param name="sm33">Value at row 3, column 3 of the matrix.</param>
	/// <param name="sm34">Value at row 3, column 4 of the matrix.</param>
	/// <param name="sm41">Value at row 4, column 1 of the matrix.</param>
	/// <param name="sm42">Value at row 4, column 2 of the matrix.</param>
	/// <param name="sm43">Value at row 4, column 3 of the matrix.</param>
	/// <param name="sm44">Value at row 4, column 4 of the matrix.</param>
	public DGMatrix4x4(FP sm11, FP sm12, FP sm13, FP sm14,
		FP sm21, FP sm22, FP sm23, FP sm24,
		FP sm31, FP sm32, FP sm33, FP sm34,
		FP sm41, FP sm42, FP sm43, FP sm44)
	{
		val = new FP[Count];

		this.SM11 = sm11;
		this.SM12 = sm12;
		this.SM13 = sm13;
		this.SM14 = sm14;


		this.SM21 = sm21;
		this.SM22 = sm22;
		this.SM23 = sm23;
		this.SM24 = sm24;

		this.SM31 = sm31;
		this.SM32 = sm32;
		this.SM33 = sm33;
		this.SM34 = sm34;

		this.SM41 = sm41;
		this.SM42 = sm42;
		this.SM43 = sm43;
		this.SM44 = sm44;
	}

#if UNITY_5_3_OR_NEWER
	public DGMatrix4x4(Matrix4x4 matrix)
	{
		val = new FP[Count];
		this.SM11 = (FP) matrix.m00;
		this.SM12 = (FP) matrix.m01;
		this.SM13 = (FP) matrix.m02;
		this.SM14 = (FP) matrix.m03;


		this.SM21 = (FP) matrix.m10;
		this.SM22 = (FP) matrix.m11;
		this.SM23 = (FP) matrix.m12;
		this.SM24 = (FP) matrix.m13;

		this.SM31 = (FP) matrix.m20;
		this.SM32 = (FP) matrix.m21;
		this.SM33 = (FP) matrix.m22;
		this.SM34 = (FP) matrix.m23;

		this.SM41 = (FP) matrix.m30;
		this.SM42 = (FP) matrix.m31;
		this.SM43 = (FP) matrix.m32;
		this.SM44 = (FP) matrix.m33;
	}
#endif

	public DGMatrix4x4(System.Numerics.Matrix4x4 matrix)
	{
		val = new FP[Count];
		this.SM11 = (FP) matrix.M11;
		this.SM12 = (FP) matrix.M12;
		this.SM13 = (FP) matrix.M13;
		this.SM14 = (FP) matrix.M14;


		this.SM21 = (FP) matrix.M21;
		this.SM22 = (FP) matrix.M22;
		this.SM23 = (FP) matrix.M23;
		this.SM24 = (FP) matrix.M24;

		this.SM31 = (FP) matrix.M31;
		this.SM32 = (FP) matrix.M32;
		this.SM33 = (FP) matrix.M33;
		this.SM34 = (FP) matrix.M34;

		this.SM41 = (FP) matrix.M41;
		this.SM42 = (FP) matrix.M42;
		this.SM43 = (FP) matrix.M43;
		this.SM44 = (FP) matrix.M44;
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
		res.x = lhs.SM11 * vector.x + lhs.SM12 * vector.y + lhs.SM13 * vector.z + lhs.SM14 * vector.w;
		res.y = lhs.SM21 * vector.x + lhs.SM22 * vector.y + lhs.SM23 * vector.z + lhs.SM24 * vector.w;
		res.z = lhs.SM31 * vector.x + lhs.SM32 * vector.y + lhs.SM33 * vector.z + lhs.SM34 * vector.w;
		res.w = lhs.SM41 * vector.x + lhs.SM42 * vector.y + lhs.SM43 * vector.z + lhs.SM44 * vector.w;
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
	/// Linearly interpolates between the corresponding values of two matrices.
	/// </summary>
	/// <param name="matrix1">The first source matrix.</param>
	/// <param name="matrix2">The second source matrix.</param>
	/// <param name="amount">The relative weight of the second source matrix.</param>
	/// <returns>The interpolated matrix.</returns>
	public static DGMatrix4x4 Lerp(DGMatrix4x4 matrix1, DGMatrix4x4 matrix2, FP amount)
	{
		DGMatrix4x4 result = new DGMatrix4x4(false);

		// First row
		result.SM11 = matrix1.SM11 + (matrix2.SM11 - matrix1.SM11) * amount;
		result.SM12 = matrix1.SM12 + (matrix2.SM12 - matrix1.SM12) * amount;
		result.SM13 = matrix1.SM13 + (matrix2.SM13 - matrix1.SM13) * amount;
		result.SM14 = matrix1.SM14 + (matrix2.SM14 - matrix1.SM14) * amount;

		// Second row
		result.SM21 = matrix1.SM21 + (matrix2.SM21 - matrix1.SM21) * amount;
		result.SM22 = matrix1.SM22 + (matrix2.SM22 - matrix1.SM22) * amount;
		result.SM23 = matrix1.SM23 + (matrix2.SM23 - matrix1.SM23) * amount;
		result.SM24 = matrix1.SM24 + (matrix2.SM24 - matrix1.SM24) * amount;

		// Third row
		result.SM31 = matrix1.SM31 + (matrix2.SM31 - matrix1.SM31) * amount;
		result.SM32 = matrix1.SM32 + (matrix2.SM32 - matrix1.SM32) * amount;
		result.SM33 = matrix1.SM33 + (matrix2.SM33 - matrix1.SM33) * amount;
		result.SM34 = matrix1.SM34 + (matrix2.SM34 - matrix1.SM34) * amount;

		// Fourth row
		result.SM41 = matrix1.SM41 + (matrix2.SM41 - matrix1.SM41) * amount;
		result.SM42 = matrix1.SM42 + (matrix2.SM42 - matrix1.SM42) * amount;
		result.SM43 = matrix1.SM43 + (matrix2.SM43 - matrix1.SM43) * amount;
		result.SM44 = matrix1.SM44 + (matrix2.SM44 - matrix1.SM44) * amount;

		return result;
	}

	/// <summary>
	/// Creates a matrix representing the given axis and angle rotation.
	/// </summary>
	/// <param name="axis">Axis around which to rotate. need nomalized</param>
	/// <param name="angle">Angle to rotate around the axis.</param>
	/// <param name="result">Matrix created from the axis and angle.</param>MultiplyPoint3x4
	public static DGMatrix4x4 CreateFromAxisAngle(FPVector3 axis, FP angle)
	{
		return CreateFromAxisAngleRad(axis, angle * DGMath.Deg2Rad);
	}

	public static DGMatrix4x4 CreateFromAxisAngleRad(FPVector3 axis, FP radians)
	{
		FP xx = axis.x * axis.x;
		FP yy = axis.y * axis.y;
		FP zz = axis.z * axis.z;
		FP xy = axis.x * axis.y;
		FP xz = axis.x * axis.z;
		FP yz = axis.y * axis.z;

		FP sin = DGMath.Sin(radians);
		FP cos = DGMath.Cos(radians);

		FP oc = (FP)1 - cos;

		DGMatrix4x4 result = new DGMatrix4x4(false);
		result.SM11 = oc * xx + cos;
		result.SM21 = oc * xy + axis.z * sin;
		result.SM31 = oc * xz - axis.y * sin;
		result.SM41 = (FP)0;

		result.SM12 = oc * xy - axis.z * sin;
		result.SM22 = oc * yy + cos;
		result.SM32 = oc * yz + axis.x * sin;
		result.SM42 = (FP)0;

		result.SM13 = oc * xz + axis.y * sin;
		result.SM23 = oc * yz - axis.x * sin;
		result.SM33 = oc * zz + cos;
		result.SM43 = (FP)0;

		result.SM14 = (FP)0;
		result.SM24 = (FP)0;
		result.SM34 = (FP)0;
		result.SM44 = (FP)1;

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

		DGMatrix4x4 result = new DGMatrix4x4(false);

		result.SM11 = (FP) 1 - YY - ZZ;
		result.SM12 = XY - ZW;
		result.SM13 = XZ + YW;
		

		result.SM21 = XY + ZW;
		result.SM22 = (FP) 1 - XX - ZZ;
		result.SM23 = YZ - XW;

		result.SM31 = XZ - YW;
		result.SM32 = YZ + XW;
		result.SM33 = (FP) 1 - XX - YY;

		result.SM14 = (FP) 0;
		result.SM24 = (FP) 0;
		result.SM34 = (FP) 0;
		result.SM44 = (FP) 1;

		result.SM41 = (FP)0;
		result.SM42 = (FP)0;
		result.SM43 = (FP)0;

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
		FP resultM11 = a.SM11 * b.SM11 + a.SM12 * b.SM21 + a.SM13 * b.SM31 + a.SM14 * b.SM41;
		FP resultM12 = a.SM11 * b.SM12 + a.SM12 * b.SM22 + a.SM13 * b.SM32 + a.SM14 * b.SM42;
		FP resultM13 = a.SM11 * b.SM13 + a.SM12 * b.SM23 + a.SM13 * b.SM33 + a.SM14 * b.SM43;
		FP resultM14 = a.SM11 * b.SM14 + a.SM12 * b.SM24 + a.SM13 * b.SM34 + a.SM14 * b.SM44;

		FP resultM21 = a.SM21 * b.SM11 + a.SM22 * b.SM21 + a.SM23 * b.SM31 + a.SM24 * b.SM41;
		FP resultM22 = a.SM21 * b.SM12 + a.SM22 * b.SM22 + a.SM23 * b.SM32 + a.SM24 * b.SM42;
		FP resultM23 = a.SM21 * b.SM13 + a.SM22 * b.SM23 + a.SM23 * b.SM33 + a.SM24 * b.SM43;
		FP resultM24 = a.SM21 * b.SM14 + a.SM22 * b.SM24 + a.SM23 * b.SM34 + a.SM24 * b.SM44;

		FP resultM31 = a.SM31 * b.SM11 + a.SM32 * b.SM21 + a.SM33 * b.SM31 + a.SM34 * b.SM41;
		FP resultM32 = a.SM31 * b.SM12 + a.SM32 * b.SM22 + a.SM33 * b.SM32 + a.SM34 * b.SM42;
		FP resultM33 = a.SM31 * b.SM13 + a.SM32 * b.SM23 + a.SM33 * b.SM33 + a.SM34 * b.SM43;
		FP resultM34 = a.SM31 * b.SM14 + a.SM32 * b.SM24 + a.SM33 * b.SM34 + a.SM34 * b.SM44;

		FP resultM41 = a.SM41 * b.SM11 + a.SM42 * b.SM21 + a.SM43 * b.SM31 + a.SM44 * b.SM41;
		FP resultM42 = a.SM41 * b.SM12 + a.SM42 * b.SM22 + a.SM43 * b.SM32 + a.SM44 * b.SM42;
		FP resultM43 = a.SM41 * b.SM13 + a.SM42 * b.SM23 + a.SM43 * b.SM33 + a.SM44 * b.SM43;
		FP resultM44 = a.SM41 * b.SM14 + a.SM42 * b.SM24 + a.SM43 * b.SM34 + a.SM44 * b.SM44;

		DGMatrix4x4 result = new DGMatrix4x4(false);
		result.SM11 = resultM11;
		result.SM12 = resultM12;
		result.SM13 = resultM13;
		result.SM14 = resultM14;

		result.SM21 = resultM21;
		result.SM22 = resultM22;
		result.SM23 = resultM23;
		result.SM24 = resultM24;

		result.SM31 = resultM31;
		result.SM32 = resultM32;
		result.SM33 = resultM33;
		result.SM34 = resultM34;

		result.SM41 = resultM41;
		result.SM42 = resultM42;
		result.SM43 = resultM43;
		result.SM44 = resultM44;
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
		DGMatrix4x4 result = new DGMatrix4x4(false);

		result.SM11 = matrix.SM11 * scale;
		result.SM12 = matrix.SM12 * scale;
		result.SM13 = matrix.SM13 * scale;
		result.SM14 = matrix.SM14 * scale;

		result.SM21 = matrix.SM21 * scale;
		result.SM22 = matrix.SM22 * scale;
		result.SM23 = matrix.SM23 * scale;
		result.SM24 = matrix.SM24 * scale;

		result.SM31 = matrix.SM31 * scale;
		result.SM32 = matrix.SM32 * scale;
		result.SM33 = matrix.SM33 * scale;
		result.SM34 = matrix.SM34 * scale;

		result.SM41 = matrix.SM41 * scale;
		result.SM42 = matrix.SM42 * scale;
		result.SM43 = matrix.SM43 * scale;
		result.SM44 = matrix.SM44 * scale;

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
		result.x = vX * matrix.SM11 + vY * matrix.SM21 + vZ * matrix.SM31 + vW * matrix.SM41;
		result.y = vX * matrix.SM12 + vY * matrix.SM22 + vZ * matrix.SM32 + vW * matrix.SM42;
		result.z = vX * matrix.SM13 + vY * matrix.SM23 + vZ * matrix.SM33 + vW * matrix.SM43;
		result.w = vX * matrix.SM14 + vY * matrix.SM24 + vZ * matrix.SM34 + vW * matrix.SM44;
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
		result.x = vX * matrix.SM11 + vY * matrix.SM12 + vZ * matrix.SM13 + vW * matrix.SM14;
		result.y = vX * matrix.SM21 + vY * matrix.SM22 + vZ * matrix.SM23 + vW * matrix.SM24;
		result.z = vX * matrix.SM31 + vY * matrix.SM32 + vZ * matrix.SM33 + vW * matrix.SM34;
		result.w = vX * matrix.SM41 + vY * matrix.SM42 + vZ * matrix.SM43 + vW * matrix.SM44;
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
		result.x = v.x * matrix.SM11 + v.y * matrix.SM21 + v.z * matrix.SM31 + matrix.SM41;
		result.y = v.x * matrix.SM12 + v.y * matrix.SM22 + v.z * matrix.SM32 + matrix.SM42;
		result.z = v.x * matrix.SM13 + v.y * matrix.SM23 + v.z * matrix.SM33 + matrix.SM43;
		result.w = v.x * matrix.SM14 + v.y * matrix.SM24 + v.z * matrix.SM34 + matrix.SM44;
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
		result.x = vX * matrix.SM11 + vY * matrix.SM12 + vZ * matrix.SM13 + matrix.SM14;
		result.y = vX * matrix.SM21 + vY * matrix.SM22 + vZ * matrix.SM23 + matrix.SM24;
		result.z = vX * matrix.SM31 + vY * matrix.SM32 + vZ * matrix.SM33 + matrix.SM34;
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
		result.x = vX * matrix.SM11 + vY * matrix.SM21 + vZ * matrix.SM31;
		result.y = vX * matrix.SM12 + vY * matrix.SM22 + vZ * matrix.SM32;
		result.z = vX * matrix.SM13 + vY * matrix.SM23 + vZ * matrix.SM33;

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
		result.x = vX * matrix.SM11 + vY * matrix.SM12 + vZ * matrix.SM13;
		result.y = vX * matrix.SM21 + vY * matrix.SM22 + vZ * matrix.SM23;
		result.z = vX * matrix.SM31 + vY * matrix.SM32 + vZ * matrix.SM33;
		return result;
	}


	/// <summary>
	/// Transposes the matrix.
	/// </summary>
	/// <param name="m">Matrix to transpose.</param>
	/// <param name="transposed">Matrix to transpose.</param>
	public static DGMatrix4x4 Transpose(DGMatrix4x4 m)
	{
		DGMatrix4x4 transposed = new DGMatrix4x4(false);
		FP m01 = m.SM12;
		FP m02 = m.SM13;
		FP m03 = m.SM14;
		FP m12 = m.SM23;
		FP m13 = m.SM24;
		FP m23 = m.SM34;

		transposed.SM11 = m.SM11;
		transposed.SM12 = m.SM21;
		transposed.SM13 = m.SM31;
		transposed.SM14 = m.SM41;

		transposed.SM21 = m01;
		transposed.SM22 = m.SM22;
		transposed.SM23 = m.SM32;
		transposed.SM24 = m.SM42;

		transposed.SM31 = m02;
		transposed.SM32 = m12;
		transposed.SM33 = m.SM33;
		transposed.SM34 = m.SM43;

		transposed.SM41 = m03;
		transposed.SM42 = m13;
		transposed.SM43 = m23;
		transposed.SM44 = m.SM44;

		return transposed;
	}

	/// <summary>
	/// Inverts the matrix.
	/// </summary>
	/// <param name="m">Matrix to invert.</param>
	/// <param name="inverted">Inverted version of the matrix.</param>
	public static DGMatrix4x4 Invert(DGMatrix4x4 m)
	{
		DGMatrix4x4 inverted = new DGMatrix4x4(false);
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
		DGMatrix4x4 inverted = new DGMatrix4x4(false);
		//Invert (transpose) the upper left 3x3 rotation.
		FP intermediate = m.SM12;
		inverted.SM12 = m.SM21;
		inverted.SM21 = intermediate;

		intermediate = m.SM13;
		inverted.SM13 = m.SM31;
		inverted.SM31 = intermediate;

		intermediate = m.SM23;
		inverted.SM23 = m.SM32;
		inverted.SM32 = intermediate;

		inverted.SM11 = m.SM11;
		inverted.SM22 = m.SM22;
		inverted.SM33 = m.SM33;

		//Translation component
		var vX = m.SM41;
		var vY = m.SM42;
		var vZ = m.SM43;
		inverted.SM41 = -(vX * inverted.SM11 + vY * inverted.SM21 + vZ * inverted.SM31);
		inverted.SM42 = -(vX * inverted.SM12 + vY * inverted.SM22 + vZ * inverted.SM32);
		inverted.SM43 = -(vX * inverted.SM13 + vY * inverted.SM23 + vZ * inverted.SM33);

		//Last chunk.
		inverted.SM14 = (FP) 0;
		inverted.SM24 = (FP) 0;
		inverted.SM34 = (FP) 0;
		inverted.SM44 = (FP) 1;

		return inverted;
	}

	public static DGMatrix4x4 CreateRotationX(FP radians)
	{
		DGMatrix4x4 r = new DGMatrix4x4(false);
		FP cos = DGMath.Cos(radians);
		FP sin = DGMath.Sin(radians);
		r.SM11 = (FP) 1;
		r.SM12 = (FP) 0;
		r.SM13 = (FP) 0;
		r.SM14 = (FP) 0;
		r.SM21 = (FP) 0;
		r.SM22 = cos;
		r.SM23 = sin;
		r.SM24 = (FP) 0;
		r.SM31 = (FP) 0;
		r.SM32 = -sin;
		r.SM33 = cos;
		r.SM34 = (FP) 0;
		r.SM41 = (FP) 0;
		r.SM42 = (FP) 0;
		r.SM43 = (FP) 0;
		r.SM44 = (FP) 1;
		return r;
	}

	public static DGMatrix4x4 CreateRotationY(FP radians)
	{
		DGMatrix4x4 r = new DGMatrix4x4(false);
		FP cos = DGMath.Cos(radians);
		FP sin = DGMath.Sin(radians);
		r.SM11 = cos;
		r.SM12 = (FP) 0;
		r.SM13 = -sin;
		r.SM14 = (FP) 0;
		r.SM21 = (FP) 0;
		r.SM22 = (FP) 1;
		r.SM23 = (FP) 0;
		r.SM24 = (FP) 0;
		r.SM31 = sin;
		r.SM32 = (FP) 0;
		r.SM33 = cos;
		r.SM34 = (FP) 0;
		r.SM41 = (FP) 0;
		r.SM42 = (FP) 0;
		r.SM43 = (FP) 0;
		r.SM44 = (FP) 1;
		return r;
	}

	public static DGMatrix4x4 CreateRotationZ(FP radians)
	{
		DGMatrix4x4 r = new DGMatrix4x4(false);
		FP cos = DGMath.Cos(radians);
		FP sin = DGMath.Sin(radians);
		r.SM11 = cos;
		r.SM12 = sin;
		r.SM13 = (FP) 0;
		r.SM14 = (FP) 0;
		r.SM21 = -sin;
		r.SM22 = cos;
		r.SM23 = (FP) 0;
		r.SM24 = (FP) 0;
		r.SM31 = (FP) 0;
		r.SM32 = (FP) 0;
		r.SM33 = (FP) 1;
		r.SM34 = (FP) 0;
		r.SM41 = (FP) 0;
		r.SM42 = (FP) 0;
		r.SM43 = (FP) 0;
		r.SM44 = (FP) 1;
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
		DGMatrix4x4 r = new DGMatrix4x4(false);
		r.SM11 = right.x;
		r.SM12 = right.y;
		r.SM13 = right.z;
		r.SM14 = (FP) 0;
		r.SM21 = up.x;
		r.SM22 = up.y;
		r.SM23 = up.z;
		r.SM24 = (FP) 0;
		r.SM31 = backward.x;
		r.SM32 = backward.y;
		r.SM33 = backward.z;
		r.SM34 = (FP) 0;
		r.SM41 = (FP) 0;
		r.SM42 = (FP) 0;
		r.SM43 = (FP) 0;
		r.SM44 = (FP) 1;
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
		var left = -width / (FP) 2;
		var right = width / (FP) 2;
		var bottom = -height / (FP) 2;
		var top = height / (FP) 2;

		return CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
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
		DGMatrix4x4 projection = new DGMatrix4x4(false);
		var xOrth = (FP) 2 / (right - left);
		var yOrth = (FP) 2 / (top - bottom);
		var zOrth = (FP) (-2) / (zFar - zNear);

		var tx = -(right + left) / (right - left);
		var ty = -(top + bottom) / (top - bottom);
		var tz = -(zFar + zNear) / (zFar - zNear);

		projection.SM11 = xOrth;
		projection.SM21 = (FP) 0;
		projection.SM31 = (FP) 0;
		projection.SM41 = (FP) 0;
		projection.SM12 = (FP) 0;
		projection.SM22 = yOrth;
		projection.SM32 = (FP) 0;
		projection.SM42 = (FP) 0;
		projection.SM13 = (FP) 0;
		projection.SM23 = (FP) 0;
		projection.SM33 = zOrth;
		projection.SM43 = (FP) 0;
		projection.SM14 = tx;
		projection.SM24 = ty;
		projection.SM34 = tz;
		projection.SM44 = (FP) 1;
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
		var left = -width / (FP) 2;
		var right = width / (FP) 2;
		var bottom = -height / (FP) 2;
		var top = height / (FP) 2;
		return CreatePerspectiveOffCenter(left, right, bottom, top, nearPlaneDistance, farPlaneDistance);
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
		DGMatrix4x4 perspective = new DGMatrix4x4(false);
		perspective.SetIdentity();
		var fd = (FP) 1.0 / DGMath.Tan(fieldOfView / (FP) 2.0);
		var a1 = (farClip + nearClip) / (nearClip - farClip);
		var a2 = ((FP) 2 * farClip * nearClip) / (nearClip - farClip);
		perspective.SM11 = fd / aspectRatio;
		perspective.SM21 = (FP) 0;
		perspective.SM31 = (FP) 0;
		perspective.SM41 = (FP) 0;
		perspective.SM12 = (FP) 0;
		perspective.SM22 = fd;
		perspective.SM32 = (FP) 0;
		perspective.SM42 = (FP) 0;
		perspective.SM13 = (FP) 0;
		perspective.SM23 = (FP) 0;
		perspective.SM33 = a1;
		perspective.SM43 = (FP) (-1);
		perspective.SM14 = (FP) 0;
		perspective.SM24 = (FP) 0;
		perspective.SM34 = a2;
		perspective.SM44 = (FP) 0;
		return perspective;
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
	public static DGMatrix4x4 CreatePerspectiveOffCenter(FP left, FP right, FP bottom, FP top, FP nearPlaneDistance,
		FP farPlaneDistance)
	{
		if (nearPlaneDistance <= (FP) 0.0f)
			throw new ArgumentOutOfRangeException("nearPlaneDistance");

		if (farPlaneDistance <= (FP) 0.0f)
			throw new ArgumentOutOfRangeException("farPlaneDistance");

		if (nearPlaneDistance >= (FP) farPlaneDistance)
			throw new ArgumentOutOfRangeException("nearPlaneDistance");

		DGMatrix4x4 result = new DGMatrix4x4(false);

		var x = (FP) 2.0f * nearPlaneDistance / (right - left);
		var y = (FP) 2.0f * nearPlaneDistance / (top - bottom);
		var a = (right + left) / (right - left);
		var b = (top + bottom) / (top - bottom);
		var a1 = (farPlaneDistance + nearPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
		var a2 = ((FP) 2 * farPlaneDistance * nearPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
		result.SM11 = x;
		result.SM21 = (FP) 0;
		result.SM31 = (FP) 0;
		result.SM41 = (FP) 0;
		result.SM12 = (FP) 0;
		result.SM22 = y;
		result.SM32 = (FP) 0;
		result.SM42 = (FP) 0;
		result.SM13 = a;
		result.SM23 = b;
		result.SM33 = a1;
		result.SM43 = (FP) (-1);
		result.SM14 = (FP) 0;
		result.SM24 = (FP) 0;
		result.SM34 = a2;
		result.SM44 = (FP) 0;
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

		FP fa = (FP) (-2.0f) * a;
		FP fb = (FP) (-2.0f) * b;
		FP fc = (FP) (-2.0f) * c;

		DGMatrix4x4 result = new DGMatrix4x4(false);

		result.SM11 = fa * a + (FP) 1.0f;
		result.SM12 = fb * a;
		result.SM13 = fc * a;
		result.SM14 = (FP) 0.0f;

		result.SM21 = fa * b;
		result.SM22 = fb * b + (FP) 1.0f;
		result.SM23 = fc * b;
		result.SM24 = (FP) 0.0f;

		result.SM31 = fa * c;
		result.SM32 = fb * c;
		result.SM33 = fc * c + (FP) 1.0f;
		result.SM34 = (FP) 0.0f;

		result.SM41 = fa * value.d;
		result.SM42 = fb * value.d;
		result.SM43 = fc * value.d;
		result.SM44 = (FP) 1.0f;

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
		FP d = -p.d;

		DGMatrix4x4 result = new DGMatrix4x4(false);

		result.SM11 = a * lightDirection.x + dot;
		result.SM21 = b * lightDirection.x;
		result.SM31 = c * lightDirection.x;
		result.SM41 = d * lightDirection.x;

		result.SM12 = a * lightDirection.y;
		result.SM22 = b * lightDirection.y + dot;
		result.SM32 = c * lightDirection.y;
		result.SM42 = d * lightDirection.y;

		result.SM13 = a * lightDirection.z;
		result.SM23 = b * lightDirection.z;
		result.SM33 = c * lightDirection.z + dot;
		result.SM43 = d * lightDirection.z;

		result.SM14 = (FP) 0.0f;
		result.SM24 = (FP) 0.0f;
		result.SM34 = (FP) 0.0f;
		result.SM44 = dot;

		return result;
	}

	public static DGMatrix4x4 LookAt(FPVector3 position, FPVector3 target, FPVector3 upVector)
	{
		return TRS(position, FPQuaternion.LookRotation(target - position, upVector), FPVector3.one);
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
		DGMatrix4x4 viewMatrix = new DGMatrix4x4(false);
		viewMatrix.SM11 = x.x;
		viewMatrix.SM12 = y.x;
		viewMatrix.SM13 = z.x;
		viewMatrix.SM14 = (FP) 0;
		viewMatrix.SM21 = x.y;
		viewMatrix.SM22 = y.y;
		viewMatrix.SM23 = z.y;
		viewMatrix.SM24 = (FP) 0;
		viewMatrix.SM31 = x.z;
		viewMatrix.SM32 = y.z;
		viewMatrix.SM33 = z.z;
		viewMatrix.SM34 = (FP) 0;
		viewMatrix.SM41 = FPVector3.Dot(x, position);
		viewMatrix.SM42 = FPVector3.Dot(y, position);
		viewMatrix.SM43 = FPVector3.Dot(z, position);
		viewMatrix.SM41 = -viewMatrix.SM41;
		viewMatrix.SM42 = -viewMatrix.SM42;
		viewMatrix.SM43 = -viewMatrix.SM43;
		viewMatrix.SM44 = (FP) 1;
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
		DGMatrix4x4 worldMatrix = new DGMatrix4x4(false);

		FP length = forward.magnitude;
		FPVector3 z = forward / -length;
		FPVector3 x = FPVector3.Cross(upVector, z);
		x.Normalize();
		FPVector3 y = FPVector3.Cross(z, x);

		worldMatrix.SM11 = x.x;
		worldMatrix.SM12 = x.y;
		worldMatrix.SM13 = x.z;
		worldMatrix.SM14 = (FP) 0;
		worldMatrix.SM21 = y.x;
		worldMatrix.SM22 = y.y;
		worldMatrix.SM23 = y.z;
		worldMatrix.SM24 = (FP) 0;
		worldMatrix.SM31 = z.x;
		worldMatrix.SM32 = z.y;
		worldMatrix.SM33 = z.z;
		worldMatrix.SM34 = (FP) 0;

		worldMatrix.SM41 = position.x;
		worldMatrix.SM42 = position.y;
		worldMatrix.SM43 = position.z;
		worldMatrix.SM44 = (FP) 1;

		return worldMatrix;
	}


	/// <summary>
	/// Creates a matrix representing a translation.
	/// </summary>
	/// <param name="translation">Translation to be represented by the matrix.</param>
	/// <param name="translationMatrix">Matrix representing the given translation.</param>
	public static DGMatrix4x4 CreateTranslation(FPVector3 translation)
	{
		DGMatrix4x4 translationMatrix = new DGMatrix4x4(false);
		translationMatrix.SM11 = (FP) 1;
		translationMatrix.SM22 = (FP) 1;
		translationMatrix.SM33 = (FP) 1;
		translationMatrix.SM44 = (FP) 1;
		translationMatrix.SM14 = translation.x;
		translationMatrix.SM24 = translation.y;
		translationMatrix.SM34 = translation.z;

		return translationMatrix;
	}


	/// <summary>
	/// Creates a matrix representing the given axis aligned scale.
	/// </summary>
	/// <param name="scale">Scale to be represented by the matrix.</param>
	/// <param name="scaleMatrix">Matrix representing the given scale.</param>
	public static DGMatrix4x4 CreateScale(FPVector3 scale)
	{
		DGMatrix4x4 scaleMatrix = new DGMatrix4x4(false);
		scaleMatrix.SM11 = scale.x;
		scaleMatrix.SM22 = scale.y;
		scaleMatrix.SM33 = scale.z;
		scaleMatrix.SM44 = (FP) 1;
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
		DGMatrix4x4 scaleMatrix = new DGMatrix4x4(false);
		scaleMatrix.SM11 = x;
		scaleMatrix.SM22 = y;
		scaleMatrix.SM33 = z;
		scaleMatrix.SM44 = (FP)1;
		return scaleMatrix;
	}

	public static DGMatrix4x4 Scale(FPVector3 scale)
	{
		return CreateScale(scale);
	}

	public static DGMatrix4x4 Translate(FPVector3 vector)
	{
		DGMatrix4x4 m = new DGMatrix4x4(false);
		m.SM11 = (FP) 1F;
		m.SM12 = (FP) 0F;
		m.SM13 = (FP) 0F;
		m.SM14 = (FP) vector.x;
		m.SM21 = (FP) 0F;
		m.SM22 = (FP) 1F;
		m.SM23 = (FP) 0F;
		m.SM24 = (FP) vector.y;
		m.SM31 = (FP) 0F;
		m.SM32 = (FP) 0F;
		m.SM33 = (FP) 1F;
		m.SM34 = (FP) vector.z;
		m.SM41 = (FP) 0F;
		m.SM42 = (FP) 0F;
		m.SM43 = (FP) 0F;
		m.SM44 = (FP) 1F;
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
		DGMatrix4x4 m = new DGMatrix4x4(false);
		m.SM11 = (FP) 1.0f - (yy + zz);
		m.SM21 = xy + wz;
		m.SM31 = xz - wy;
		m.SM41 = (FP) 0.0F;
		m.SM12 = xy - wz;
		m.SM22 = (FP) 1.0f - (xx + zz);
		m.SM32 = yz + wx;
		m.SM42 = (FP) 0.0F;
		m.SM13 = xz + wy;
		m.SM23 = yz - wx;
		m.SM33 = (FP) 1.0f - (xx + yy);
		m.SM43 = (FP) 0.0F;
		m.SM14 = (FP) 0.0F;
		m.SM24 = (FP) 0.0F;
		m.SM34 = (FP) 0.0F;
		m.SM44 = (FP) 1.0F;
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
	public static DGMatrix4x4 CreateBillboard(FPVector3 objectPosition, FPVector3 cameraPosition,
		FPVector3 cameraUpVector, FPVector3 cameraForwardVector)
	{
		FPVector3 zaxis = new FPVector3(
			objectPosition.x - cameraPosition.x,
			objectPosition.y - cameraPosition.y,
			objectPosition.z - cameraPosition.z);

		FP norm = zaxis.sqrMagnitude;

		if (norm < DGMath.Epsilon)
			zaxis = -cameraForwardVector;
		else
			zaxis = zaxis * (FP) 1.0f / DGMath.Sqrt(norm);

		FPVector3 xaxis = FPVector3.Normalize(FPVector3.Cross(cameraUpVector, zaxis));

		FPVector3 yaxis = FPVector3.Cross(zaxis, xaxis);

		DGMatrix4x4 result = new DGMatrix4x4(false);

		result.SM11 = xaxis.x;
		result.SM12 = xaxis.y;
		result.SM13 = xaxis.z;
		result.SM14 = (FP) 0.0f;
		result.SM21 = yaxis.x;
		result.SM22 = yaxis.y;
		result.SM23 = yaxis.z;
		result.SM24 = (FP) 0.0f;
		result.SM31 = zaxis.x;
		result.SM32 = zaxis.y;
		result.SM33 = zaxis.z;
		result.SM34 = (FP) 0.0f;

		result.SM41 = objectPosition.x;
		result.SM42 = objectPosition.y;
		result.SM43 = objectPosition.z;
		result.SM44 = (FP) 1.0f;

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
	public static DGMatrix4x4 CreateConstrainedBillboard(FPVector3 objectPosition, FPVector3 cameraPosition,
		FPVector3 rotateAxis, FPVector3 cameraForwardVector, FPVector3 objectForwardVector)
	{
		FP minAngle = (FP) 1.0f - ((FP) 0.1f * (DGMath.PI / (FP) 180.0f)); // 0.1 degrees

		// Treat the case when object and camera positions are too close.
		FPVector3 faceDir = new FPVector3(
			objectPosition.x - cameraPosition.x,
			objectPosition.y - cameraPosition.y,
			objectPosition.z - cameraPosition.z);

		FP norm = faceDir.sqrMagnitude;

		if (norm < DGMath.Epsilon)
			faceDir = -cameraForwardVector;
		else
			faceDir = faceDir * ((FP) 1.0f / DGMath.Sqrt(norm));

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

		DGMatrix4x4 result = new DGMatrix4x4(false);

		result.SM11 = xaxis.x;
		result.SM12 = xaxis.y;
		result.SM13 = xaxis.z;
		result.SM14 = (FP) 0.0f;
		result.SM21 = yaxis.x;
		result.SM22 = yaxis.y;
		result.SM23 = yaxis.z;
		result.SM24 = (FP) 0.0f;
		result.SM31 = zaxis.x;
		result.SM32 = zaxis.y;
		result.SM33 = zaxis.z;
		result.SM34 = (FP) 0.0f;

		result.SM41 = objectPosition.x;
		result.SM42 = objectPosition.y;
		result.SM43 = objectPosition.z;
		result.SM44 = (FP) 1.0f;

		return result;
	}

	// Transforms a position by this matrix, with a perspective divide. (generic)
	public FPVector3 MultiplyPoint(FPVector3 point)
	{
		FPVector3 res;
		FP w;
		res.x = this.SM11 * point.x + this.SM12 * point.y + this.SM13 * point.z + this.SM14;
		res.y = this.SM21 * point.x + this.SM22 * point.y + this.SM23 * point.z + this.SM24;
		res.z = this.SM31 * point.x + this.SM32 * point.y + this.SM33 * point.z + this.SM34;
		w = this.SM41 * point.x + this.SM42 * point.y + this.SM43 * point.z + this.SM44;

		w = (FP) 1F / w;
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
		FP det1 = SM33 * SM44 - SM34 * SM43;
		FP det2 = SM32 * SM44 - SM34 * SM42;
		FP det3 = SM32 * SM43 - SM33 * SM42;
		FP det4 = SM31 * SM44 - SM34 * SM41;
		FP det5 = SM31 * SM43 - SM33 * SM41;
		FP det6 = SM31 * SM42 - SM32 * SM41;
		return
			(SM11 * ((SM22 * det1 - SM23 * det2) + SM24 * det3)) -
			(SM12 * ((SM21 * det1 - SM23 * det4) + SM24 * det5)) +
			(SM13 * ((SM21 * det2 - SM22 * det4) + SM24 * det6)) -
			(SM14 * ((SM21 * det3 - SM22 * det5) + SM23 * det6));
	}

	/// <summary>
	/// Transposes the matrix in-place.
	/// </summary>
	public void Transpose()
	{
		FP intermediate = SM12;
		SM12 = SM21;
		SM21 = intermediate;

		intermediate = SM13;
		SM13 = SM31;
		SM31 = intermediate;

		intermediate = SM14;
		SM14 = SM41;
		SM41 = intermediate;

		intermediate = SM23;
		SM23 = SM32;
		SM32 = intermediate;

		intermediate = SM24;
		SM24 = SM42;
		SM42 = intermediate;

		intermediate = SM34;
		SM34 = SM43;
		SM43 = intermediate;
	}


	// Transforms a position by this matrix, without a perspective divide. (fast)
	public FPVector3 MultiplyPoint3x4(FPVector3 point)
	{
		FPVector3 res = default;
		res.x = this.SM11 * point.x + this.SM12 * point.y + this.SM13 * point.z + this.SM14;
		res.y = this.SM21 * point.x + this.SM22 * point.y + this.SM23 * point.z + this.SM24;
		res.z = this.SM31 * point.x + this.SM32 * point.y + this.SM33 * point.z + this.SM34;


		return res;
	}

	// Transforms a direction by this matrix.
	public FPVector3 MultiplyVector(FPVector3 vector)
	{
		FPVector3 res = default;
		res.x = this.SM11 * vector.x + this.SM12 * vector.y + this.SM13 * vector.z;
		res.y = this.SM21 * vector.x + this.SM22 * vector.y + this.SM23 * vector.z;
		res.z = this.SM31 * vector.x + this.SM32 * vector.y + this.SM33 * vector.z;
		return res;
	}

	public void SetIdentity()
	{
		this = Identity;
	}
}