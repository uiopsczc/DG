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

/// <summary>
/// 3 row, 2 column matrix.
/// </summary>
public struct DGMatrix3x2
{
	/// <summary>
	/// Value at row 1, column 1 of the matrix.
	/// </summary>
	public DGFixedPoint SM11;

	/// <summary>
	/// Value at row 1, column 2 of the matrix.
	/// </summary>
	public DGFixedPoint SM12;

	/// <summary>
	/// Value at row 2, column 1 of the matrix.
	/// </summary>
	public DGFixedPoint SM21;

	/// <summary>
	/// Value at row 2, column 2 of the matrix.
	/// </summary>
	public DGFixedPoint SM22;

	/// <summary>
	/// Value at row 3, column 1 of the matrix.
	/// </summary>
	public DGFixedPoint SM31;

	/// <summary>
	/// Value at row 3, column 2 of the matrix.
	/// </summary>
	public DGFixedPoint SM32;

	private static readonly DGMatrix3x2 _identity = new DGMatrix3x2
	(
		(DGFixedPoint) 1f, (DGFixedPoint) 0f,
		(DGFixedPoint) 0f, (DGFixedPoint) 1f,
		(DGFixedPoint) 0f, (DGFixedPoint) 0f
	);

	/// <summary>
	/// Returns the multiplicative identity matrix.
	/// </summary>
	public static DGMatrix3x2 identity => _identity;

	/// <summary>
	/// Returns whether the matrix is the identity matrix.
	/// </summary>
	public bool isIdentity => SM11 == (DGFixedPoint) 1f && SM22 == (DGFixedPoint) 1f && // Check diagonal element first for early out.
	                          SM12 == (DGFixedPoint) 0f &&
	                          SM21 == (DGFixedPoint) 0f &&
	                          SM31 == (DGFixedPoint) 0f && SM32 == (DGFixedPoint) 0f;

	/// <summary>
	/// Gets or sets the translation component of this matrix.
	/// </summary>
	public DGVector2 translation
	{
		get => new DGVector2(SM31, SM32);

		set
		{
			SM31 = value.x;
			SM32 = value.y;
		}
	}


	/// <summary>
	/// Constructs a new 3 row, 2 column matrix.
	/// </summary>
	/// <param name="sm11">Value at row 1, column 1 of the matrix.</param>
	/// <param name="sm12">Value at row 1, column 2 of the matrix.</param>
	/// <param name="sm21">Value at row 2, column 1 of the matrix.</param>
	/// <param name="sm22">Value at row 2, column 2 of the matrix.</param>
	/// <param name="sm31">Value at row 2, column 1 of the matrix.</param>
	/// <param name="sm32">Value at row 2, column 2 of the matrix.</param>
	public DGMatrix3x2(DGFixedPoint sm11, DGFixedPoint sm12, DGFixedPoint sm21, DGFixedPoint sm22, DGFixedPoint sm31, DGFixedPoint sm32)
	{
		SM11 = sm11;
		SM12 = sm12;
		SM21 = sm21;
		SM22 = sm22;
		SM31 = sm31;
		SM32 = sm32;
	}

	public DGMatrix3x2(Matrix3x2 matrix)
	{
		SM11 = (DGFixedPoint) matrix.M11;
		SM12 = (DGFixedPoint) matrix.M12;
		SM21 = (DGFixedPoint) matrix.M21;
		SM22 = (DGFixedPoint) matrix.M22;
		SM31 = (DGFixedPoint) matrix.M31;
		SM32 = (DGFixedPoint) matrix.M32;
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
		return (SM11 == other.SM11 && SM22 == other.SM22 && // Check diagonal element first for early out.
		        SM12 == other.SM12 &&
		        SM21 == other.SM21 &&
		        SM31 == other.SM31 && SM32 == other.SM32);
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
		return SM11.GetHashCode() + SM12.GetHashCode() +
		       SM21.GetHashCode() + SM22.GetHashCode() +
		       SM31.GetHashCode() + SM32.GetHashCode();
	}

	/// <summary>
	/// Creates a string representation of the matrix.
	/// </summary>
	/// <returns>A string representation of the matrix.</returns>
	public override string ToString()
	{
		return "[" + SM11 + ", " + SM12 + "] \n" +
		       "[" + SM21 + ", " + SM22 + "] \n" +
		       "[" + SM31 + ", " + SM32 + "]\n";
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
		return (value1.SM11 == value2.SM11 &&
		        value1.SM22 == value2.SM22 && // Check diagonal element first for early out.
		        value1.SM12 == value2.SM12 &&
		        value1.SM21 == value2.SM21 &&
		        value1.SM31 == value2.SM31 && value1.SM32 == value2.SM32);
	}

	/// <summary>
	/// Returns a boolean indicating whether the given matrices are not equal.
	/// </summary>
	/// <param name="value1">The first source matrix.</param>
	/// <param name="value2">The second source matrix.</param>
	/// <returns>True if the matrices are not equal; False if they are equal.</returns>
	public static bool operator !=(DGMatrix3x2 value1, DGMatrix3x2 value2)
	{
		return (value1.SM11 != value2.SM11 || value1.SM12 != value2.SM12 ||
		        value1.SM21 != value2.SM21 || value1.SM22 != value2.SM22 ||
		        value1.SM31 != value2.SM31 || value1.SM32 != value2.SM32);
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

		m.SM11 = -value.SM11;
		m.SM12 = -value.SM12;
		m.SM21 = -value.SM21;
		m.SM22 = -value.SM22;
		m.SM31 = -value.SM31;
		m.SM32 = -value.SM32;

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

		m.SM11 = value1.SM11 + value2.SM11;
		m.SM12 = value1.SM12 + value2.SM12;
		m.SM21 = value1.SM21 + value2.SM21;
		m.SM22 = value1.SM22 + value2.SM22;
		m.SM31 = value1.SM31 + value2.SM31;
		m.SM32 = value1.SM32 + value2.SM32;

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

		m.SM11 = value1.SM11 - value2.SM11;
		m.SM12 = value1.SM12 - value2.SM12;
		m.SM21 = value1.SM21 - value2.SM21;
		m.SM22 = value1.SM22 - value2.SM22;
		m.SM31 = value1.SM31 - value2.SM31;
		m.SM32 = value1.SM32 - value2.SM32;

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
		m.SM11 = value1.SM11 * value2.SM11 + value1.SM12 * value2.SM21;
		m.SM12 = value1.SM11 * value2.SM12 + value1.SM12 * value2.SM22;

		// Second row
		m.SM21 = value1.SM21 * value2.SM11 + value1.SM22 * value2.SM21;
		m.SM22 = value1.SM21 * value2.SM12 + value1.SM22 * value2.SM22;

		// Third row
		m.SM31 = value1.SM31 * value2.SM11 + value1.SM32 * value2.SM21 + value2.SM31;
		m.SM32 = value1.SM31 * value2.SM12 + value1.SM32 * value2.SM22 + value2.SM32;

		return m;
	}

	/// <summary>
	/// Scales all elements in a matrix by the given scalar factor.
	/// </summary>
	/// <param name="value1">The source matrix.</param>
	/// <param name="value2">The scaling value to use.</param>
	/// <returns>The resulting matrix.</returns>
	public static DGMatrix3x2 operator *(DGMatrix3x2 value1, DGFixedPoint value2)
	{
		DGMatrix3x2 m;

		m.SM11 = value1.SM11 * value2;
		m.SM12 = value1.SM12 * value2;
		m.SM21 = value1.SM21 * value2;
		m.SM22 = value1.SM22 * value2;
		m.SM31 = value1.SM31 * value2;
		m.SM32 = value1.SM32 * value2;

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
	public static DGMatrix3x2 CreateTranslation(DGVector2 position)
	{
		DGMatrix3x2 result;

		result.SM11 = (DGFixedPoint) 1.0f;
		result.SM12 = (DGFixedPoint) 0.0f;
		result.SM21 = (DGFixedPoint) 0.0f;
		result.SM22 = (DGFixedPoint) 1.0f;

		result.SM31 = position.x;
		result.SM32 = position.y;

		return result;
	}

	/// <summary>
	/// Creates a translation matrix from the given X and Y components.
	/// </summary>
	/// <param name="xPosition">The X position.</param>
	/// <param name="yPosition">The Y position.</param>
	/// <returns>A translation matrix.</returns>
	public static DGMatrix3x2 CreateTranslation(DGFixedPoint xPosition, DGFixedPoint yPosition)
	{
		DGMatrix3x2 result;

		result.SM11 = (DGFixedPoint) 1.0f;
		result.SM12 = (DGFixedPoint) 0.0f;
		result.SM21 = (DGFixedPoint) 0.0f;
		result.SM22 = (DGFixedPoint) 1.0f;

		result.SM31 = xPosition;
		result.SM32 = yPosition;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix from the given X and Y components.
	/// </summary>
	/// <param name="xScale">Value to scale by on the X-axis.</param>
	/// <param name="yScale">Value to scale by on the Y-axis.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(DGFixedPoint xScale, DGFixedPoint yScale)
	{
		DGMatrix3x2 result;

		result.SM11 = xScale;
		result.SM12 = (DGFixedPoint) 0.0f;
		result.SM21 = (DGFixedPoint) 0.0f;
		result.SM22 = (DGFixedPoint) yScale;
		result.SM31 = (DGFixedPoint) 0.0f;
		result.SM32 = (DGFixedPoint) 0.0f;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix that is offset by a given center point.
	/// </summary>
	/// <param name="xScale">Value to scale by on the X-axis.</param>
	/// <param name="yScale">Value to scale by on the Y-axis.</param>
	/// <param name="centerPoint">The center point.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(DGFixedPoint xScale, DGFixedPoint yScale, DGVector2 centerPoint)
	{
		DGMatrix3x2 result;

		DGFixedPoint tx = centerPoint.x * ((DGFixedPoint) 1 - xScale);
		DGFixedPoint ty = centerPoint.y * ((DGFixedPoint) 1 - yScale);

		result.SM11 = xScale;
		result.SM12 = (DGFixedPoint) 0.0f;
		result.SM21 = (DGFixedPoint) 0.0f;
		result.SM22 = yScale;
		result.SM31 = tx;
		result.SM32 = ty;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix from the given vector scale.
	/// </summary>
	/// <param name="scales">The scale to use.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(DGVector2 scales)
	{
		DGMatrix3x2 result;

		result.SM11 = (DGFixedPoint) scales.x;
		result.SM12 = (DGFixedPoint) 0.0f;
		result.SM21 = (DGFixedPoint) 0.0f;
		result.SM22 = (DGFixedPoint) scales.y;
		result.SM31 = (DGFixedPoint) 0.0f;
		result.SM32 = (DGFixedPoint) 0.0f;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix from the given vector scale with an offset from the given center point.
	/// </summary>
	/// <param name="scales">The scale to use.</param>
	/// <param name="centerPoint">The center offset.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(DGVector2 scales, DGVector2 centerPoint)
	{
		DGMatrix3x2 result;

		DGFixedPoint tx = centerPoint.x * ((DGFixedPoint) 1 - scales.x);
		DGFixedPoint ty = centerPoint.y * ((DGFixedPoint) 1 - scales.y);

		result.SM11 = (DGFixedPoint) scales.x;
		result.SM12 = (DGFixedPoint) 0.0f;
		result.SM21 = (DGFixedPoint) 0.0f;
		result.SM22 = (DGFixedPoint) scales.y;
		result.SM31 = (DGFixedPoint) tx;
		result.SM32 = (DGFixedPoint) ty;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix that scales uniformly with the given scale.
	/// </summary>
	/// <param name="scale">The uniform scale to use.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(DGFixedPoint scale)
	{
		DGMatrix3x2 result;

		result.SM11 = (DGFixedPoint) scale;
		result.SM12 = (DGFixedPoint) 0.0f;
		result.SM21 = (DGFixedPoint) 0.0f;
		result.SM22 = (DGFixedPoint) scale;
		result.SM31 = (DGFixedPoint) 0.0f;
		result.SM32 = (DGFixedPoint) 0.0f;

		return result;
	}

	/// <summary>
	/// Creates a scale matrix that scales uniformly with the given scale with an offset from the given center.
	/// </summary>
	/// <param name="scale">The uniform scale to use.</param>
	/// <param name="centerPoint">The center offset.</param>
	/// <returns>A scaling matrix.</returns>
	public static DGMatrix3x2 CreateScale(DGFixedPoint scale, DGVector2 centerPoint)
	{
		DGMatrix3x2 result;

		DGFixedPoint tx = centerPoint.x * ((DGFixedPoint) 1 - scale);
		DGFixedPoint ty = centerPoint.y * ((DGFixedPoint) 1 - scale);

		result.SM11 = (DGFixedPoint) scale;
		result.SM12 = (DGFixedPoint) 0.0f;
		result.SM21 = (DGFixedPoint) 0.0f;
		result.SM22 = (DGFixedPoint) scale;
		result.SM31 = (DGFixedPoint) tx;
		result.SM32 = (DGFixedPoint) ty;

		return result;
	}

	/// <summary>
	/// Creates a skew matrix from the given angles in radians.
	/// </summary>
	/// <param name="radiansX">The X angle, in radians.</param>
	/// <param name="radiansY">The Y angle, in radians.</param>
	/// <returns>A skew matrix.</returns>
	public static DGMatrix3x2 CreateSkew(DGFixedPoint radiansX, DGFixedPoint radiansY)
	{
		DGMatrix3x2 result;

		DGFixedPoint xTan = DGMath.Tan(radiansX);
		DGFixedPoint yTan = DGMath.Tan(radiansY);

		result.SM11 = (DGFixedPoint) 1.0f;
		result.SM12 = (DGFixedPoint) yTan;
		result.SM21 = (DGFixedPoint) xTan;
		result.SM22 = (DGFixedPoint) 1.0f;
		result.SM31 = (DGFixedPoint) 0.0f;
		result.SM32 = (DGFixedPoint) 0.0f;

		return result;
	}

	/// <summary>
	/// Creates a skew matrix from the given angles in radians and a center point.
	/// </summary>
	/// <param name="radiansX">The X angle, in radians.</param>
	/// <param name="radiansY">The Y angle, in radians.</param>
	/// <param name="centerPoint">The center point.</param>
	/// <returns>A skew matrix.</returns>
	public static DGMatrix3x2 CreateSkew(DGFixedPoint radiansX, DGFixedPoint radiansY, DGVector2 centerPoint)
	{
		DGMatrix3x2 result;

		DGFixedPoint xTan = DGMath.Tan(radiansX);
		DGFixedPoint yTan = DGMath.Tan(radiansY);

		DGFixedPoint tx = -centerPoint.y * xTan;
		DGFixedPoint ty = -centerPoint.x * yTan;

		result.SM11 = (DGFixedPoint) 1.0f;
		result.SM12 = (DGFixedPoint) yTan;
		result.SM21 = (DGFixedPoint) xTan;
		result.SM22 = (DGFixedPoint) 1.0f;
		result.SM31 = (DGFixedPoint) tx;
		result.SM32 = (DGFixedPoint) ty;

		return result;
	}

	/// <summary>
	/// Creates a rotation matrix using the given rotation in radians.
	/// </summary>
	/// <param name="radians">The amount of rotation, in radians.</param>
	/// <returns>A rotation matrix.</returns>
	public static DGMatrix3x2 CreateRotation(DGFixedPoint radians)
	{
		DGMatrix3x2 result;

		radians = DGMath.IEEERemainder(radians, DGMath.PI * (DGFixedPoint) 2);

		DGFixedPoint c, s;

		DGFixedPoint epsilon = (DGFixedPoint) 0.001f * DGMath.PI / (DGFixedPoint) 180f; // 0.1% of a degree

		if (radians > -epsilon && radians < epsilon)
		{
			// Exact case for zero rotation.
			c = (DGFixedPoint) 1;
			s = (DGFixedPoint) 0;
		}
		else if (radians > DGMath.PI / (DGFixedPoint) 2 - epsilon && radians < DGMath.PI / (DGFixedPoint) 2 + epsilon)
		{
			// Exact case for 90 degree rotation.
			c = (DGFixedPoint) 0;
			s = (DGFixedPoint) 1;
		}
		else if (radians < -DGMath.PI + epsilon || radians > DGMath.PI - epsilon)
		{
			// Exact case for 180 degree rotation.
			c = (DGFixedPoint) (-1);
			s = (DGFixedPoint) 0;
		}
		else if (radians > -DGMath.PI / (DGFixedPoint) 2 - epsilon && radians < -DGMath.PI / (DGFixedPoint) 2 + epsilon)
		{
			// Exact case for 270 degree rotation.
			c = (DGFixedPoint) 0;
			s = (DGFixedPoint) (-1);
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
		result.SM11 = c;
		result.SM12 = s;
		result.SM21 = -s;
		result.SM22 = c;
		result.SM31 = (DGFixedPoint) 0.0f;
		result.SM32 = (DGFixedPoint) 0.0f;

		return result;
	}

	/// <summary>
	/// Creates a rotation matrix using the given rotation in radians and a center point.
	/// </summary>
	/// <param name="radians">The amount of rotation, in radians.</param>
	/// <param name="centerPoint">The center point.</param>
	/// <returns>A rotation matrix.</returns>
	public static DGMatrix3x2 CreateRotation(DGFixedPoint radians, DGVector2 centerPoint)
	{
		DGMatrix3x2 result;

		radians = DGMath.IEEERemainder(radians, DGMath.PI * (DGFixedPoint) 2);

		DGFixedPoint c, s;

		DGFixedPoint epsilon = (DGFixedPoint) 0.001f * DGMath.PI / (DGFixedPoint) 180f; // 0.1% of a degree

		if (radians > -epsilon && radians < epsilon)
		{
			// Exact case for zero rotation.
			c = (DGFixedPoint) 1;
			s = (DGFixedPoint) 0;
		}
		else if (radians > DGMath.PI / (DGFixedPoint) 2 - epsilon && radians < DGMath.PI / (DGFixedPoint) 2 + epsilon)
		{
			// Exact case for 90 degree rotation.
			c = (DGFixedPoint) 0;
			s = (DGFixedPoint) 1;
		}
		else if (radians < -DGMath.PI + epsilon || radians > DGMath.PI - epsilon)
		{
			// Exact case for 180 degree rotation.
			c = (DGFixedPoint) (-1);
			s = (DGFixedPoint) 0;
		}
		else if (radians > -DGMath.PI / (DGFixedPoint) 2 - epsilon && radians < -DGMath.PI / (DGFixedPoint) 2 + epsilon)
		{
			// Exact case for 270 degree rotation.
			c = (DGFixedPoint) 0;
			s = (DGFixedPoint) (-1);
		}
		else
		{
			// Arbitrary rotation.
			c = DGMath.Cos(radians);
			s = DGMath.Sin(radians);
		}

		DGFixedPoint x = centerPoint.x * ((DGFixedPoint) 1 - c) + centerPoint.y * s;
		DGFixedPoint y = centerPoint.y * ((DGFixedPoint) 1 - c) - centerPoint.x * s;

		// [  c  s ]
		// [ -s  c ]
		// [  x  y ]
		result.SM11 = c;
		result.SM12 = s;
		result.SM21 = -s;
		result.SM22 = c;
		result.SM31 = x;
		result.SM32 = y;

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
		DGFixedPoint det = (matrix.SM11 * matrix.SM22) - (matrix.SM21 * matrix.SM12);

		if (DGMath.Abs(det) < DGMath.Epsilon)
		{
			result = default;
			return false;
		}

		DGFixedPoint invDet = (DGFixedPoint) 1.0f / det;

		result.SM11 = matrix.SM22 * invDet;
		result.SM12 = -matrix.SM12 * invDet;
		result.SM21 = -matrix.SM21 * invDet;
		result.SM22 = matrix.SM11 * invDet;
		result.SM31 = (matrix.SM21 * matrix.SM32 - matrix.SM31 * matrix.SM22) * invDet;
		result.SM32 = (matrix.SM31 * matrix.SM12 - matrix.SM11 * matrix.SM32) * invDet;

		return true;
	}

	/// <summary>
	/// Linearly interpolates from matrix1 to matrix2, based on the third parameter.
	/// </summary>
	/// <param name="matrix1">The first source matrix.</param>
	/// <param name="matrix2">The second source matrix.</param>
	/// <param name="amount">The relative weighting of matrix2.</param>
	/// <returns>The interpolated matrix.</returns>
	public static DGMatrix3x2 Lerp(DGMatrix3x2 matrix1, DGMatrix3x2 matrix2, DGFixedPoint amount)
	{
		DGMatrix3x2 result;

		// First row
		result.SM11 = matrix1.SM11 + (matrix2.SM11 - matrix1.SM11) * amount;
		result.SM12 = matrix1.SM12 + (matrix2.SM12 - matrix1.SM12) * amount;

		// Second row
		result.SM21 = matrix1.SM21 + (matrix2.SM21 - matrix1.SM21) * amount;
		result.SM22 = matrix1.SM22 + (matrix2.SM22 - matrix1.SM22) * amount;

		// Third row
		result.SM31 = matrix1.SM31 + (matrix2.SM31 - matrix1.SM31) * amount;
		result.SM32 = matrix1.SM32 + (matrix2.SM32 - matrix1.SM32) * amount;

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
		result.SM11 = value1.SM11 * value2.SM11 + value1.SM12 * value2.SM21;
		result.SM12 = value1.SM11 * value2.SM12 + value1.SM12 * value2.SM22;

		// Second row
		result.SM21 = value1.SM21 * value2.SM11 + value1.SM22 * value2.SM21;
		result.SM22 = value1.SM21 * value2.SM12 + value1.SM22 * value2.SM22;

		// Third row
		result.SM31 = value1.SM31 * value2.SM11 + value1.SM32 * value2.SM21 + value2.SM31;
		result.SM32 = value1.SM31 * value2.SM12 + value1.SM32 * value2.SM22 + value2.SM32;

		return result;
	}

	/// <summary>
	/// Scales all elements in a matrix by the given scalar factor.
	/// </summary>
	/// <param name="value1">The source matrix.</param>
	/// <param name="value2">The scaling value to use.</param>
	/// <returns>The resulting matrix.</returns>
	public static DGMatrix3x2 Multiply(DGMatrix3x2 value1, DGFixedPoint value2)
	{
		DGMatrix3x2 result;

		result.SM11 = value1.SM11 * value2;
		result.SM12 = value1.SM12 * value2;
		result.SM21 = value1.SM21 * value2;
		result.SM22 = value1.SM22 * value2;
		result.SM31 = value1.SM31 * value2;
		result.SM32 = value1.SM32 * value2;

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
		DGFixedPoint m11 = a.SM11 + b.SM11;
		DGFixedPoint m12 = a.SM12 + b.SM12;

		DGFixedPoint m21 = a.SM21 + b.SM21;
		DGFixedPoint m22 = a.SM22 + b.SM22;

		DGFixedPoint m31 = a.SM31 + b.SM31;
		DGFixedPoint m32 = a.SM32 + b.SM32;

		result.SM11 = m11;
		result.SM12 = m12;

		result.SM21 = m21;
		result.SM22 = m22;

		result.SM31 = m31;
		result.SM32 = m32;

		return result;
	}

	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix3x2 Multiply(DGMatrix3x3 a, DGMatrix3x2 b)
	{
		DGMatrix3x2 result = default;
		DGFixedPoint resultM11 = a.SM11 * b.SM11 + a.SM12 * b.SM21 + a.SM13 * b.SM31;
		DGFixedPoint resultM12 = a.SM11 * b.SM12 + a.SM12 * b.SM22 + a.SM13 * b.SM32;

		DGFixedPoint resultM21 = a.SM21 * b.SM11 + a.SM22 * b.SM21 + a.SM23 * b.SM31;
		DGFixedPoint resultM22 = a.SM21 * b.SM12 + a.SM22 * b.SM22 + a.SM23 * b.SM32;

		DGFixedPoint resultM31 = a.SM31 * b.SM11 + a.SM32 * b.SM21 + a.SM33 * b.SM31;
		DGFixedPoint resultM32 = a.SM31 * b.SM12 + a.SM32 * b.SM22 + a.SM33 * b.SM32;

		result.SM11 = resultM11;
		result.SM12 = resultM12;

		result.SM21 = resultM21;
		result.SM22 = resultM22;

		result.SM31 = resultM31;
		result.SM32 = resultM32;

		return result;
	}

	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix3x2 Multiply(DGMatrix4x4 a, ref DGMatrix3x2 b)
	{
		DGMatrix3x2 result = default;
		DGFixedPoint resultM11 = a.SM11 * b.SM11 + a.SM12 * b.SM21 + a.SM13 * b.SM31;
		DGFixedPoint resultM12 = a.SM11 * b.SM12 + a.SM12 * b.SM22 + a.SM13 * b.SM32;

		DGFixedPoint resultM21 = a.SM21 * b.SM11 + a.SM22 * b.SM21 + a.SM23 * b.SM31;
		DGFixedPoint resultM22 = a.SM21 * b.SM12 + a.SM22 * b.SM22 + a.SM23 * b.SM32;

		DGFixedPoint resultM31 = a.SM31 * b.SM11 + a.SM32 * b.SM21 + a.SM33 * b.SM31;
		DGFixedPoint resultM32 = a.SM31 * b.SM12 + a.SM32 * b.SM22 + a.SM33 * b.SM32;

		result.SM11 = resultM11;
		result.SM12 = resultM12;

		result.SM21 = resultM21;
		result.SM22 = resultM22;

		result.SM31 = resultM31;
		result.SM32 = resultM32;

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
		DGFixedPoint m11 = -matrix.SM11;
		DGFixedPoint m12 = -matrix.SM12;

		DGFixedPoint m21 = -matrix.SM21;
		DGFixedPoint m22 = -matrix.SM22;

		DGFixedPoint m31 = -matrix.SM31;
		DGFixedPoint m32 = -matrix.SM32;

		result.SM11 = m11;
		result.SM12 = m12;

		result.SM21 = m21;
		result.SM22 = m22;

		result.SM31 = m31;
		result.SM32 = m32;

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
		DGFixedPoint m11 = a.SM11 - b.SM11;
		DGFixedPoint m12 = a.SM12 - b.SM12;

		DGFixedPoint m21 = a.SM21 - b.SM21;
		DGFixedPoint m22 = a.SM22 - b.SM22;

		DGFixedPoint m31 = a.SM31 - b.SM31;
		DGFixedPoint m32 = a.SM32 - b.SM32;

		result.SM11 = m11;
		result.SM12 = m12;

		result.SM21 = m21;
		result.SM22 = m22;

		result.SM31 = m31;
		result.SM32 = m32;

		return result;
	}

	/// <summary>
	/// Transforms the vector by the matrix.
	/// </summary>
	/// <param name="v">Vector2 to transform.  Considered to be a column vector for purposes of multiplication.</param>
	/// <param name="matrix">Matrix to use as the transformation.</param>
	/// <param name="result">Column vector product of the transformation.</param>
	public static DGVector3 Transform(DGVector2 v, DGMatrix3x2 matrix)
	{
		DGVector3 result = default;
		result.x = matrix.SM11 * v.x + matrix.SM12 * v.y;
		result.y = matrix.SM21 * v.x + matrix.SM22 * v.y;
		result.z = matrix.SM31 * v.x + matrix.SM32 * v.y;
		return result;
	}

	/// <summary>
	/// Transforms the vector by the matrix.
	/// </summary>
	/// <param name="v">Vector2 to transform.  Considered to be a row vector for purposes of multiplication.</param>
	/// <param name="matrix">Matrix to use as the transformation.</param>
	/// <param name="result">Row vector product of the transformation.</param>
	public static DGVector2 Transform(DGVector3 v, DGMatrix3x2 matrix)
	{
		DGVector2 result = default;
		result.x = v.x * matrix.SM11 + v.y * matrix.SM21 + v.z * matrix.SM31;
		result.y = v.x * matrix.SM12 + v.y * matrix.SM22 + v.z * matrix.SM32;
		return result;
	}


	/// <summary>
	/// Computes the transposed matrix of a matrix.
	/// </summary>
	/// <param name="matrix">Matrix to transpose.</param>
	/// <param name="result">Transposed matrix.</param>
	public static DGMatrix2x3 Transpose(DGMatrix3x2 matrix)
	{
		DGMatrix2x3 result = default;
		result.SM11 = matrix.SM11;
		result.SM12 = matrix.SM21;
		result.SM13 = matrix.SM31;

		result.SM21 = matrix.SM12;
		result.SM22 = matrix.SM22;
		result.SM23 = matrix.SM32;
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
	public DGFixedPoint GetDeterminant()
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

		return (SM11 * SM22) - (SM21 * SM12);
	}
}