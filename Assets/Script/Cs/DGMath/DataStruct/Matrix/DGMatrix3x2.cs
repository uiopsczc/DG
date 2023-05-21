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

using System.Numerics;
using FP = DGFixedPoint;
using FPVector3 = DGVector3;
using FPMatrix4x4 = DGMatrix4x4;
using FPMatrix3x3 = DGMatrix3x3;
using FPMatrix2x3 = DGMatrix2x3;
using FPVector2 = DGVector2;

/// <summary>
/// 3 row, 2 column matrix.
/// </summary>
public struct DGMatrix3x2
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
	/// Value at row 2, column 1 of the matrix.
	/// </summary>
	public FP M21;

	/// <summary>
	/// Value at row 2, column 2 of the matrix.
	/// </summary>
	public FP M22;

	/// <summary>
	/// Value at row 3, column 1 of the matrix.
	/// </summary>
	public FP M31;

	/// <summary>
	/// Value at row 3, column 2 of the matrix.
	/// </summary>
	public FP M32;

	private static readonly DGMatrix3x2 _identity = new DGMatrix3x2
	(
		(FP) 1f, (FP) 0f,
		(FP) 0f, (FP) 1f,
		(FP) 0f, (FP) 0f
	);

	/// <summary>
	/// Returns the multiplicative identity matrix.
	/// </summary>
	public static DGMatrix3x2 identity => _identity;

	/// <summary>
	/// Returns whether the matrix is the identity matrix.
	/// </summary>
	public bool isIdentity => M11 == (FP) 1f && M22 == (FP) 1f && // Check diagonal element first for early out.
	                          M12 == (FP) 0f &&
	                          M21 == (FP) 0f &&
	                          M31 == (FP) 0f && M32 == (FP) 0f;

	/// <summary>
	/// Gets or sets the translation component of this matrix.
	/// </summary>
	public FPVector2 translation
	{
		get => new FPVector2(M31, M32);

		set
		{
			M31 = value.x;
			M32 = value.y;
		}
	}


	/// <summary>
	/// Constructs a new 3 row, 2 column matrix.
	/// </summary>
	/// <param name="m11">Value at row 1, column 1 of the matrix.</param>
	/// <param name="m12">Value at row 1, column 2 of the matrix.</param>
	/// <param name="m21">Value at row 2, column 1 of the matrix.</param>
	/// <param name="m22">Value at row 2, column 2 of the matrix.</param>
	/// <param name="m31">Value at row 2, column 1 of the matrix.</param>
	/// <param name="m32">Value at row 2, column 2 of the matrix.</param>
	public DGMatrix3x2(FP m11, FP m12, FP m21, FP m22, FP m31, FP m32)
	{
		M11 = m11;
		M12 = m12;
		M21 = m21;
		M22 = m22;
		M31 = m31;
		M32 = m32;
	}

	public DGMatrix3x2(Matrix3x2 matrix)
	{
		M11 = (FP)matrix.M11;
		M12 = (FP)matrix.M12;
		M21 = (FP)matrix.M21;
		M22 = (FP)matrix.M22;
		M31 = (FP)matrix.M31;
		M32 = (FP)matrix.M32;
	}

	/*************************************************************************************
	* 模块描述:Equal ToString
	*************************************************************************************/
	/// <summary>
	/// Returns a boolean indicating whether the matrix is equal to the other given matrix.
	/// </summary>
	/// <param name="other">The other matrix to test equality against.</param>
	/// <returns>True if this matrix is equal to other; False otherwise.</returns>
	public bool Equals(DGMatrix3x2 other)
	{
		return (M11 == other.M11 && M22 == other.M22 && // Check diagonal element first for early out.
		        M12 == other.M12 &&
		        M21 == other.M21 &&
		        M31 == other.M31 && M32 == other.M32);
	}

	/// <summary>
	/// Returns a boolean indicating whether the given Object is equal to this matrix instance.
	/// </summary>
	/// <param name="obj">The Object to compare against.</param>
	/// <returns>True if the Object is equal to this matrix; False otherwise.</returns>
	public override bool Equals(object obj)
	{
		if (obj is DGMatrix3x2 x2)
			return Equals(x2);

		return false;
	}

	/// <summary>
	/// Returns the hash code for this instance.
	/// </summary>
	/// <returns>The hash code.</returns>
	public override int GetHashCode()
	{
		return M11.GetHashCode() + M12.GetHashCode() +
		       M21.GetHashCode() + M22.GetHashCode() +
		       M31.GetHashCode() + M32.GetHashCode();
	}

	/// <summary>
	/// Creates a string representation of the matrix.
	/// </summary>
	/// <returns>A string representation of the matrix.</returns>
	public override string ToString()
	{
		return "{" + M11 + ", " + M12 + "} \n" +
		       "{" + M21 + ", " + M22 + "} \n" +
		       "{" + M31 + ", " + M32 + "}\n";
	}

	/*************************************************************************************
	* 模块描述:关系运算符
	*************************************************************************************/
	/// <summary>
	/// Returns a boolean indicating whether the given matrices are equal.
	/// </summary>
	/// <param name="value1">The first source matrix.</param>
	/// <param name="value2">The second source matrix.</param>
	/// <returns>True if the matrices are equal; False otherwise.</returns>
	public static bool operator ==(DGMatrix3x2 value1, DGMatrix3x2 value2)
	{
		return (value1.M11 == value2.M11 && value1.M22 == value2.M22 && // Check diagonal element first for early out.
		        value1.M12 == value2.M12 &&
		        value1.M21 == value2.M21 &&
		        value1.M31 == value2.M31 && value1.M32 == value2.M32);
	}

	/// <summary>
	/// Returns a boolean indicating whether the given matrices are not equal.
	/// </summary>
	/// <param name="value1">The first source matrix.</param>
	/// <param name="value2">The second source matrix.</param>
	/// <returns>True if the matrices are not equal; False if they are equal.</returns>
	public static bool operator !=(DGMatrix3x2 value1, DGMatrix3x2 value2)
	{
		return (value1.M11 != value2.M11 || value1.M12 != value2.M12 ||
		        value1.M21 != value2.M21 || value1.M22 != value2.M22 ||
		        value1.M31 != value2.M31 || value1.M32 != value2.M32);
	}

	/*************************************************************************************
	* 模块描述:操作运算
	*************************************************************************************/
	/// <summary>
	/// Negates the given matrix by multiplying all values by -1.
	/// </summary>
	/// <param name="value">The source matrix.</param>
	/// <returns>The negated matrix.</returns>
	public static DGMatrix3x2 operator -(DGMatrix3x2 value)
	{
		DGMatrix3x2 m;

		m.M11 = -value.M11;
		m.M12 = -value.M12;
		m.M21 = -value.M21;
		m.M22 = -value.M22;
		m.M31 = -value.M31;
		m.M32 = -value.M32;

		return m;
	}

	/// <summary>
	/// Adds each matrix element in value1 with its corresponding element in value2.
	/// </summary>
	/// <param name="value1">The first source matrix.</param>
	/// <param name="value2">The second source matrix.</param>
	/// <returns>The matrix containing the summed values.</returns>
	public static DGMatrix3x2 operator +(DGMatrix3x2 value1, DGMatrix3x2 value2)
	{
		DGMatrix3x2 m;

		m.M11 = value1.M11 + value2.M11;
		m.M12 = value1.M12 + value2.M12;
		m.M21 = value1.M21 + value2.M21;
		m.M22 = value1.M22 + value2.M22;
		m.M31 = value1.M31 + value2.M31;
		m.M32 = value1.M32 + value2.M32;

		return m;
	}

	/// <summary>
	/// Subtracts each matrix element in value2 from its corresponding element in value1.
	/// </summary>
	/// <param name="value1">The first source matrix.</param>
	/// <param name="value2">The second source matrix.</param>
	/// <returns>The matrix containing the resulting values.</returns>
	public static DGMatrix3x2 operator -(DGMatrix3x2 value1, DGMatrix3x2 value2)
	{
		DGMatrix3x2 m;

		m.M11 = value1.M11 - value2.M11;
		m.M12 = value1.M12 - value2.M12;
		m.M21 = value1.M21 - value2.M21;
		m.M22 = value1.M22 - value2.M22;
		m.M31 = value1.M31 - value2.M31;
		m.M32 = value1.M32 - value2.M32;

		return m;
	}

	/// <summary>
	/// Multiplies two matrices together and returns the resulting matrix.
	/// </summary>
	/// <param name="value1">The first source matrix.</param>
	/// <param name="value2">The second source matrix.</param>
	/// <returns>The product matrix.</returns>
	public static DGMatrix3x2 operator *(DGMatrix3x2 value1, DGMatrix3x2 value2)
	{
		DGMatrix3x2 m;

		// First row
		m.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21;
		m.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22;

		// Second row
		m.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21;
		m.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22;

		// Third row
		m.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value2.M31;
		m.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value2.M32;

		return m;
	}

	/// <summary>
	/// Scales all elements in a matrix by the given scalar factor.
	/// </summary>
	/// <param name="value1">The source matrix.</param>
	/// <param name="value2">The scaling value to use.</param>
	/// <returns>The resulting matrix.</returns>
	public static DGMatrix3x2 operator *(DGMatrix3x2 value1, FP value2)
	{
		DGMatrix3x2 m;

		m.M11 = value1.M11 * value2;
		m.M12 = value1.M12 * value2;
		m.M21 = value1.M21 * value2;
		m.M22 = value1.M22 * value2;
		m.M31 = value1.M31 * value2;
		m.M32 = value1.M32 * value2;

		return m;
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	/// <summary>
	/// Creates a translation matrix from the given vector.
	/// </summary>
	/// <param name="position">The translation position.</param>
	/// <returns>A translation matrix.</returns>
	public static DGMatrix3x2 CreateTranslation(FPVector2 position)
	{
		DGMatrix3x2 result;

		result.M11 = (FP) 1.0f;
		result.M12 = (FP) 0.0f;
		result.M21 = (FP) 0.0f;
		result.M22 = (FP) 1.0f;

		result.M31 = position.x;
		result.M32 = position.y;

		return result;
	}

	/// <summary>
	/// Creates a translation matrix from the given X and Y components.
	/// </summary>
	/// <param name="xPosition">The X position.</param>
	/// <param name="yPosition">The Y position.</param>
	/// <returns>A translation matrix.</returns>
	public static DGMatrix3x2 CreateTranslation(FP xPosition, FP yPosition)
	{
		DGMatrix3x2 result;

		result.M11 = (FP) 1.0f;
		result.M12 = (FP) 0.0f;
		result.M21 = (FP) 0.0f;
		result.M22 = (FP) 1.0f;

		result.M31 = xPosition;
		result.M32 = yPosition;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix from the given X and Y components.
	/// </summary>
	/// <param name="xScale">Value to scale by on the X-axis.</param>
	/// <param name="yScale">Value to scale by on the Y-axis.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(FP xScale, FP yScale)
	{
		DGMatrix3x2 result;

		result.M11 = xScale;
		result.M12 = (FP) 0.0f;
		result.M21 = (FP) 0.0f;
		result.M22 = (FP) yScale;
		result.M31 = (FP) 0.0f;
		result.M32 = (FP) 0.0f;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix that is offset by a given center point.
	/// </summary>
	/// <param name="xScale">Value to scale by on the X-axis.</param>
	/// <param name="yScale">Value to scale by on the Y-axis.</param>
	/// <param name="centerPoint">The center point.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(FP xScale, FP yScale, FPVector2 centerPoint)
	{
		DGMatrix3x2 result;

		FP tx = centerPoint.x * ((FP) 1 - xScale);
		FP ty = centerPoint.y * ((FP) 1 - yScale);

		result.M11 = xScale;
		result.M12 = (FP) 0.0f;
		result.M21 = (FP) 0.0f;
		result.M22 = yScale;
		result.M31 = tx;
		result.M32 = ty;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix from the given vector scale.
	/// </summary>
	/// <param name="scales">The scale to use.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(FPVector2 scales)
	{
		DGMatrix3x2 result;

		result.M11 = (FP) scales.x;
		result.M12 = (FP) 0.0f;
		result.M21 = (FP) 0.0f;
		result.M22 = (FP) scales.y;
		result.M31 = (FP) 0.0f;
		result.M32 = (FP) 0.0f;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix from the given vector scale with an offset from the given center point.
	/// </summary>
	/// <param name="scales">The scale to use.</param>
	/// <param name="centerPoint">The center offset.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(FPVector2 scales, FPVector2 centerPoint)
	{
		DGMatrix3x2 result;

		FP tx = centerPoint.x * ((FP) 1 - scales.x);
		FP ty = centerPoint.y * ((FP) 1 - scales.y);

		result.M11 = (FP) scales.x;
		result.M12 = (FP) 0.0f;
		result.M21 = (FP) 0.0f;
		result.M22 = (FP) scales.y;
		result.M31 = (FP) tx;
		result.M32 = (FP) ty;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix that scales uniformly with the given scale.
	/// </summary>
	/// <param name="scale">The uniform scale to use.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(FP scale)
	{
		DGMatrix3x2 result;

		result.M11 = (FP) scale;
		result.M12 = (FP) 0.0f;
		result.M21 = (FP) 0.0f;
		result.M22 = (FP) scale;
		result.M31 = (FP) 0.0f;
		result.M32 = (FP) 0.0f;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix that scales uniformly with the given scale with an offset from the given center.
	/// </summary>
	/// <param name="scale">The uniform scale to use.</param>
	/// <param name="centerPoint">The center offset.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(FP scale, FPVector2 centerPoint)
	{
		DGMatrix3x2 result;

		FP tx = centerPoint.x * ((FP) 1 - scale);
		FP ty = centerPoint.y * ((FP) 1 - scale);

		result.M11 = (FP) scale;
		result.M12 = (FP) 0.0f;
		result.M21 = (FP) 0.0f;
		result.M22 = (FP) scale;
		result.M31 = (FP) tx;
		result.M32 = (FP) ty;

		return result;
	}

	/// <summary>
	/// Creates a skew matrix from the given angles in radians.
	/// </summary>
	/// <param name="radiansX">The X angle, in radians.</param>
	/// <param name="radiansY">The Y angle, in radians.</param>
	/// <returns>A skew matrix.</returns>
	public static DGMatrix3x2 CreateSkew(FP radiansX, FP radiansY)
	{
		DGMatrix3x2 result;

		FP xTan = DGMath.Tan(radiansX);
		FP yTan = DGMath.Tan(radiansY);

		result.M11 = (FP) 1.0f;
		result.M12 = (FP) yTan;
		result.M21 = (FP) xTan;
		result.M22 = (FP) 1.0f;
		result.M31 = (FP) 0.0f;
		result.M32 = (FP) 0.0f;

		return result;
	}

	/// <summary>
	/// Creates a skew matrix from the given angles in radians and a center point.
	/// </summary>
	/// <param name="radiansX">The X angle, in radians.</param>
	/// <param name="radiansY">The Y angle, in radians.</param>
	/// <param name="centerPoint">The center point.</param>
	/// <returns>A skew matrix.</returns>
	public static DGMatrix3x2 CreateSkew(FP radiansX, FP radiansY, FPVector2 centerPoint)
	{
		DGMatrix3x2 result;

		FP xTan = DGMath.Tan(radiansX);
		FP yTan = DGMath.Tan(radiansY);

		FP tx = -centerPoint.y * xTan;
		FP ty = -centerPoint.x * yTan;

		result.M11 = (FP) 1.0f;
		result.M12 = (FP) yTan;
		result.M21 = (FP) xTan;
		result.M22 = (FP) 1.0f;
		result.M31 = (FP) tx;
		result.M32 = (FP) ty;

		return result;
	}

	/// <summary>
	/// Creates a rotation matrix using the given rotation in radians.
	/// </summary>
	/// <param name="radians">The amount of rotation, in radians.</param>
	/// <returns>A rotation matrix.</returns>
	public static DGMatrix3x2 CreateRotation(FP radians)
	{
		DGMatrix3x2 result;

		radians = DGMath.IEEERemainder(radians, DGMath.PI * (FP) 2);

		FP c, s;

		FP epsilon = (FP) 0.001f * DGMath.PI / (FP) 180f; // 0.1% of a degree

		if (radians > -epsilon && radians < epsilon)
		{
			// Exact case for zero rotation.
			c = (FP) 1;
			s = (FP) 0;
		}
		else if (radians > DGMath.PI / (FP) 2 - epsilon && radians < DGMath.PI / (FP) 2 + epsilon)
		{
			// Exact case for 90 degree rotation.
			c = (FP) 0;
			s = (FP) 1;
		}
		else if (radians < -DGMath.PI + epsilon || radians > DGMath.PI - epsilon)
		{
			// Exact case for 180 degree rotation.
			c = (FP) (-1);
			s = (FP) 0;
		}
		else if (radians > -DGMath.PI / (FP) 2 - epsilon && radians < -DGMath.PI / (FP) 2 + epsilon)
		{
			// Exact case for 270 degree rotation.
			c = (FP) 0;
			s = (FP) (-1);
		}
		else
		{
			// Arbitrary rotation.
			c = DGMath.Cos(radians);
			s = DGMath.Sin(radians);
		}

		// [  c  s ]
		// [ -s  c ]
		// [  0  0 ]
		result.M11 = c;
		result.M12 = s;
		result.M21 = -s;
		result.M22 = c;
		result.M31 = (FP) 0.0f;
		result.M32 = (FP) 0.0f;

		return result;
	}

	/// <summary>
	/// Creates a rotation matrix using the given rotation in radians and a center point.
	/// </summary>
	/// <param name="radians">The amount of rotation, in radians.</param>
	/// <param name="centerPoint">The center point.</param>
	/// <returns>A rotation matrix.</returns>
	public static DGMatrix3x2 CreateRotation(FP radians, FPVector2 centerPoint)
	{
		DGMatrix3x2 result;

		radians = DGMath.IEEERemainder(radians, DGMath.PI * (FP) 2);

		FP c, s;

		FP epsilon = (FP) 0.001f * DGMath.PI / (FP) 180f; // 0.1% of a degree

		if (radians > -epsilon && radians < epsilon)
		{
			// Exact case for zero rotation.
			c = (FP) 1;
			s = (FP) 0;
		}
		else if (radians > DGMath.PI / (FP) 2 - epsilon && radians < DGMath.PI / (FP) 2 + epsilon)
		{
			// Exact case for 90 degree rotation.
			c = (FP) 0;
			s = (FP) 1;
		}
		else if (radians < -DGMath.PI + epsilon || radians > DGMath.PI - epsilon)
		{
			// Exact case for 180 degree rotation.
			c = (FP) (-1);
			s = (FP) 0;
		}
		else if (radians > -DGMath.PI / (FP) 2 - epsilon && radians < -DGMath.PI / (FP) 2 + epsilon)
		{
			// Exact case for 270 degree rotation.
			c = (FP) 0;
			s = (FP) (-1);
		}
		else
		{
			// Arbitrary rotation.
			c = DGMath.Cos(radians);
			s = DGMath.Sin(radians);
		}

		FP x = centerPoint.x * ((FP) 1 - c) + centerPoint.y * s;
		FP y = centerPoint.y * ((FP) 1 - c) - centerPoint.x * s;

		// [  c  s ]
		// [ -s  c ]
		// [  x  y ]
		result.M11 = c;
		result.M12 = s;
		result.M21 = -s;
		result.M22 = c;
		result.M31 = x;
		result.M32 = y;

		return result;
	}


	/// <summary>
	/// Attempts to invert the given matrix. If the operation succeeds, the inverted matrix is stored in the result parameter.
	/// </summary>
	/// <param name="matrix">The source matrix.</param>
	/// <param name="result">The output matrix.</param>
	/// <returns>True if the operation succeeded, False otherwise.</returns>
	public static bool Invert(DGMatrix3x2 matrix, out DGMatrix3x2 result)
	{
		FP det = (matrix.M11 * matrix.M22) - (matrix.M21 * matrix.M12);

		if (DGMath.Abs(det) < DGMath.Epsilon)
		{
			result = default;
			return false;
		}

		FP invDet = (FP) 1.0f / det;

		result.M11 = matrix.M22 * invDet;
		result.M12 = -matrix.M12 * invDet;
		result.M21 = -matrix.M21 * invDet;
		result.M22 = matrix.M11 * invDet;
		result.M31 = (matrix.M21 * matrix.M32 - matrix.M31 * matrix.M22) * invDet;
		result.M32 = (matrix.M31 * matrix.M12 - matrix.M11 * matrix.M32) * invDet;

		return true;
	}

	/// <summary>
	/// Linearly interpolates from matrix1 to matrix2, based on the third parameter.
	/// </summary>
	/// <param name="matrix1">The first source matrix.</param>
	/// <param name="matrix2">The second source matrix.</param>
	/// <param name="amount">The relative weighting of matrix2.</param>
	/// <returns>The interpolated matrix.</returns>
	public static DGMatrix3x2 Lerp(DGMatrix3x2 matrix1, DGMatrix3x2 matrix2, FP amount)
	{
		DGMatrix3x2 result;

		// First row
		result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
		result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;

		// Second row
		result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
		result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;

		// Third row
		result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
		result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;

		return result;
	}


	/// <summary>
	/// Multiplies two matrices together and returns the resulting matrix.
	/// </summary>
	/// <param name="value1">The first source matrix.</param>
	/// <param name="value2">The second source matrix.</param>
	/// <returns>The product matrix.</returns>
	public static DGMatrix3x2 Multiply(DGMatrix3x2 value1, DGMatrix3x2 value2)
	{
		DGMatrix3x2 result;

		// First row
		result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21;
		result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22;

		// Second row
		result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21;
		result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22;

		// Third row
		result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value2.M31;
		result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value2.M32;

		return result;
	}

	/// <summary>
	/// Scales all elements in a matrix by the given scalar factor.
	/// </summary>
	/// <param name="value1">The source matrix.</param>
	/// <param name="value2">The scaling value to use.</param>
	/// <returns>The resulting matrix.</returns>
	public static DGMatrix3x2 Multiply(DGMatrix3x2 value1, FP value2)
	{
		DGMatrix3x2 result;

		result.M11 = value1.M11 * value2;
		result.M12 = value1.M12 * value2;
		result.M21 = value1.M21 * value2;
		result.M22 = value1.M22 * value2;
		result.M31 = value1.M31 * value2;
		result.M32 = value1.M32 * value2;

		return result;
	}


	/// <summary>
	/// Adds the two matrices together on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to add.</param>
	/// <param name="b">Second matrix to add.</param>
	/// <param name="result">Sum of the two matrices.</param>
	public static DGMatrix3x2 Add(DGMatrix3x2 a, DGMatrix3x2 b)
	{
		DGMatrix3x2 result = default;
		FP m11 = a.M11 + b.M11;
		FP m12 = a.M12 + b.M12;

		FP m21 = a.M21 + b.M21;
		FP m22 = a.M22 + b.M22;

		FP m31 = a.M31 + b.M31;
		FP m32 = a.M32 + b.M32;

		result.M11 = m11;
		result.M12 = m12;

		result.M21 = m21;
		result.M22 = m22;

		result.M31 = m31;
		result.M32 = m32;

		return result;
	}

	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix3x2 Multiply(FPMatrix3x3 a, DGMatrix3x2 b)
	{
		DGMatrix3x2 result = default;
		FP resultM11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31;
		FP resultM12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32;

		FP resultM21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31;
		FP resultM22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32;

		FP resultM31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31;
		FP resultM32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32;

		result.M11 = resultM11;
		result.M12 = resultM12;

		result.M21 = resultM21;
		result.M22 = resultM22;

		result.M31 = resultM31;
		result.M32 = resultM32;

		return result;
	}

	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix3x2 Multiply(FPMatrix4x4 a, ref DGMatrix3x2 b)
	{
		DGMatrix3x2 result = default;
		FP resultM11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31;
		FP resultM12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32;

		FP resultM21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31;
		FP resultM22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32;

		FP resultM31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31;
		FP resultM32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32;

		result.M11 = resultM11;
		result.M12 = resultM12;

		result.M21 = resultM21;
		result.M22 = resultM22;

		result.M31 = resultM31;
		result.M32 = resultM32;

		return result;
	}

	/// <summary>
	/// Negates every element in the matrix.
	/// </summary>
	/// <param name="matrix">Matrix to negate.</param>
	/// <param name="result">Negated matrix.</param>
	public static DGMatrix3x2 Negate(DGMatrix3x2 matrix)
	{
		DGMatrix3x2 result = default;
		FP m11 = -matrix.M11;
		FP m12 = -matrix.M12;

		FP m21 = -matrix.M21;
		FP m22 = -matrix.M22;

		FP m31 = -matrix.M31;
		FP m32 = -matrix.M32;

		result.M11 = m11;
		result.M12 = m12;

		result.M21 = m21;
		result.M22 = m22;

		result.M31 = m31;
		result.M32 = m32;

		return result;
	}

	/// <summary>
	/// Subtracts the two matrices from each other on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to subtract.</param>
	/// <param name="b">Second matrix to subtract.</param>
	/// <param name="result">Difference of the two matrices.</param>
	public static DGMatrix3x2 Subtract(DGMatrix3x2 a, DGMatrix3x2 b)
	{
		DGMatrix3x2 result = default;
		FP m11 = a.M11 - b.M11;
		FP m12 = a.M12 - b.M12;

		FP m21 = a.M21 - b.M21;
		FP m22 = a.M22 - b.M22;

		FP m31 = a.M31 - b.M31;
		FP m32 = a.M32 - b.M32;

		result.M11 = m11;
		result.M12 = m12;

		result.M21 = m21;
		result.M22 = m22;

		result.M31 = m31;
		result.M32 = m32;

		return result;
	}

	/// <summary>
	/// Transforms the vector by the matrix.
	/// </summary>
	/// <param name="v">Vector2 to transform.  Considered to be a column vector for purposes of multiplication.</param>
	/// <param name="matrix">Matrix to use as the transformation.</param>
	/// <param name="result">Column vector product of the transformation.</param>
	public static FPVector3 Transform(FPVector2 v, DGMatrix3x2 matrix)
	{
		FPVector3 result = default;
		result.x = matrix.M11 * v.x + matrix.M12 * v.y;
		result.y = matrix.M21 * v.x + matrix.M22 * v.y;
		result.z = matrix.M31 * v.x + matrix.M32 * v.y;
		return result;
	}

	/// <summary>
	/// Transforms the vector by the matrix.
	/// </summary>
	/// <param name="v">Vector2 to transform.  Considered to be a row vector for purposes of multiplication.</param>
	/// <param name="matrix">Matrix to use as the transformation.</param>
	/// <param name="result">Row vector product of the transformation.</param>
	public static FPVector2 Transform(FPVector3 v, DGMatrix3x2 matrix)
	{
		FPVector2 result = default;
		result.x = v.x * matrix.M11 + v.y * matrix.M21 + v.z * matrix.M31;
		result.y = v.x * matrix.M12 + v.y * matrix.M22 + v.z * matrix.M32;
		return result;
	}


	/// <summary>
	/// Computes the transposed matrix of a matrix.
	/// </summary>
	/// <param name="matrix">Matrix to transpose.</param>
	/// <param name="result">Transposed matrix.</param>
	public static FPMatrix2x3 Transpose(DGMatrix3x2 matrix)
	{
		FPMatrix2x3 result = default;
		result.M11 = matrix.M11;
		result.M12 = matrix.M21;
		result.M13 = matrix.M31;

		result.M21 = matrix.M12;
		result.M22 = matrix.M22;
		result.M23 = matrix.M32;
		return result;
	}

	/*************************************************************************************
	* 模块描述:Member Util
	*************************************************************************************/
	/// <summary>
	/// Calculates the determinant for this matrix. 
	/// The determinant is calculated by expanding the matrix with a third column whose values are (0,0,1).
	/// </summary>
	/// <returns>The determinant.</returns>
	public FP GetDeterminant()
	{
		// There isn't actually any such thing as a determinant for a non-square matrix,
		// but this 3x2 type is really just an optimization of a 3x3 where we happen to
		// know the rightmost column is always (0, 0, 1). So we expand to 3x3 format:
		//
		//  [ M11, M12, 0 ]
		//  [ M21, M22, 0 ]
		//  [ M31, M32, 1 ]
		//
		// Sum the diagonal products:
		//  (M11 * M22 * 1) + (M12 * 0 * M31) + (0 * M21 * M32)
		//
		// Subtract the opposite diagonal products:
		//  (M31 * M22 * 0) + (M32 * 0 * M11) + (1 * M21 * M12)
		//
		// Collapse out the constants and oh look, this is just a 2x2 determinant!

		return (M11 * M22) - (M21 * M12);
	}
}