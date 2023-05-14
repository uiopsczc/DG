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

	/*************************************************************************************
	* 模块描述:ToString
	*************************************************************************************/
	/// <summary>
	/// Creates a string representation of the matrix.
	/// </summary>
	/// <returns>A string representation of the matrix.</returns>
	public override string ToString()
	{
		return "{" + M11 + ", " + M12 + "} " +
		       "{" + M21 + ", " + M22 + "} " +
		       "{" + M31 + ", " + M32 + "}";
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
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
}