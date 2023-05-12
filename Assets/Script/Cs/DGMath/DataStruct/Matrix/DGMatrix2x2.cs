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
using FPMatrix4x4 = DGMatrix4x4;
using FPMatrix3x2 = DGMatrix3x2;
using FPMatrix2x3 = DGMatrix2x3;
using FPVector2 = DGVector2;

/// <summary>
/// 2 row, 2 column matrix.
/// </summary>
public struct DGMatrix2x2
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
	/// Constructs a new 2 row, 2 column matrix.
	/// </summary>
	/// <param name="m11">Value at row 1, column 1 of the matrix.</param>
	/// <param name="m12">Value at row 1, column 2 of the matrix.</param>
	/// <param name="m21">Value at row 2, column 1 of the matrix.</param>
	/// <param name="m22">Value at row 2, column 2 of the matrix.</param>
	public DGMatrix2x2(FP m11, FP m12, FP m21, FP m22)
	{
		M11 = m11;
		M12 = m12;
		M21 = m21;
		M22 = m22;
	}

	/// <summary>
	/// Gets the 2x2 identity matrix.
	/// </summary>
	public static DGMatrix2x2 Identity => new DGMatrix2x2((FP) 1, (FP) 0, (FP) 0, (FP) 1);

	/// <summary>
	/// Adds the two matrices together on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to add.</param>
	/// <param name="b">Second matrix to add.</param>
	/// <param name="result">Sum of the two matrices.</param>
	public static DGMatrix2x2 Add(DGMatrix2x2 a, DGMatrix2x2 b)
	{
		DGMatrix2x2 result = default;
		FP m11 = a.M11 + b.M11;
		FP m12 = a.M12 + b.M12;

		FP m21 = a.M21 + b.M21;
		FP m22 = a.M22 + b.M22;

		result.M11 = m11;
		result.M12 = m12;

		result.M21 = m21;
		result.M22 = m22;

		return result;
	}

	/// <summary>
	/// Adds the two matrices together on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to add.</param>
	/// <param name="b">Second matrix to add.</param>
	/// <param name="result">Sum of the two matrices.</param>
	public static DGMatrix2x2 Add(FPMatrix4x4 a, DGMatrix2x2 b)
	{
		DGMatrix2x2 result = default;
		FP m11 = a.M11 + b.M11;
		FP m12 = a.M12 + b.M12;

		FP m21 = a.M21 + b.M21;
		FP m22 = a.M22 + b.M22;

		result.M11 = m11;
		result.M12 = m12;

		result.M21 = m21;
		result.M22 = m22;

		return result;
	}

	/// <summary>
	/// Adds the two matrices together on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to add.</param>
	/// <param name="b">Second matrix to add.</param>
	/// <param name="result">Sum of the two matrices.</param>
	public static DGMatrix2x2 Add(DGMatrix2x2 a, FPMatrix4x4 b)
	{
		DGMatrix2x2 result = default;
		FP m11 = a.M11 + b.M11;
		FP m12 = a.M12 + b.M12;

		FP m21 = a.M21 + b.M21;
		FP m22 = a.M22 + b.M22;

		result.M11 = m11;
		result.M12 = m12;

		result.M21 = m21;
		result.M22 = m22;

		return result;
	}

	/// <summary>
	/// Adds the two matrices together on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to add.</param>
	/// <param name="b">Second matrix to add.</param>
	/// <param name="result">Sum of the two matrices.</param>
	public static DGMatrix2x2 Add(FPMatrix4x4 a, FPMatrix4x4 b)
	{
		DGMatrix2x2 result = default;
		FP m11 = a.M11 + b.M11;
		FP m12 = a.M12 + b.M12;

		FP m21 = a.M21 + b.M21;
		FP m22 = a.M22 + b.M22;

		result.M11 = m11;
		result.M12 = m12;

		result.M21 = m21;
		result.M22 = m22;

		return result;
	}

	/// <summary>
	/// Constructs a uniform scaling matrix.
	/// </summary>
	/// <param name="scale">Value to use in the diagonal.</param>
	/// <param name="matrix">Scaling matrix.</param>
	public static DGMatrix2x2 CreateScale(FP scale)
	{
		DGMatrix2x2 matrix = default;
		matrix.M11 = scale;
		matrix.M22 = scale;

		matrix.M12 = (FP) 0;
		matrix.M21 = (FP) 0;

		return matrix;
	}


	/// <summary>
	/// Inverts the given matix.
	/// </summary>
	/// <param name="matrix">Matrix to be inverted.</param>
	/// <param name="result">Inverted matrix.</param>
	public static DGMatrix2x2 Invert(DGMatrix2x2 matrix)
	{
		DGMatrix2x2 result = default;
		FP determinantInverse = (FP) 1 / (matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21);
		FP m11 = matrix.M22 * determinantInverse;
		FP m12 = -matrix.M12 * determinantInverse;

		FP m21 = -matrix.M21 * determinantInverse;
		FP m22 = matrix.M11 * determinantInverse;

		result.M11 = m11;
		result.M12 = m12;

		result.M21 = m21;
		result.M22 = m22;

		return result;
	}

	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix2x2 Multiply(DGMatrix2x2 a, DGMatrix2x2 b)
	{
		DGMatrix2x2 result = default;
		FP resultM11 = a.M11 * b.M11 + a.M12 * b.M21;
		FP resultM12 = a.M11 * b.M12 + a.M12 * b.M22;

		FP resultM21 = a.M21 * b.M11 + a.M22 * b.M21;
		FP resultM22 = a.M21 * b.M12 + a.M22 * b.M22;

		result.M11 = resultM11;
		result.M12 = resultM12;

		result.M21 = resultM21;
		result.M22 = resultM22;

		return result;
	}

	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix2x2 Multiply(DGMatrix2x2 a, FPMatrix4x4 b)
	{
		DGMatrix2x2 result = default;
		FP resultM11 = a.M11 * b.M11 + a.M12 * b.M21;
		FP resultM12 = a.M11 * b.M12 + a.M12 * b.M22;

		FP resultM21 = a.M21 * b.M11 + a.M22 * b.M21;
		FP resultM22 = a.M21 * b.M12 + a.M22 * b.M22;

		result.M11 = resultM11;
		result.M12 = resultM12;

		result.M21 = resultM21;
		result.M22 = resultM22;

		return result;
	}

	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix2x2 Multiply(FPMatrix4x4 a, DGMatrix2x2 b)
	{
		DGMatrix2x2 result = default;
		FP resultM11 = a.M11 * b.M11 + a.M12 * b.M21;
		FP resultM12 = a.M11 * b.M12 + a.M12 * b.M22;

		FP resultM21 = a.M21 * b.M11 + a.M22 * b.M21;
		FP resultM22 = a.M21 * b.M12 + a.M22 * b.M22;

		result.M11 = resultM11;
		result.M12 = resultM12;

		result.M21 = resultM21;
		result.M22 = resultM22;

		return result;
	}

	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix2x2 Multiply(FPMatrix2x3 a, FPMatrix3x2 b)
	{
		DGMatrix2x2 result;
		result.M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31;
		result.M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32;

		result.M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31;
		result.M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32;
		return result;
	}

	/// <summary>
	/// Negates every element in the matrix.
	/// </summary>
	/// <param name="matrix">Matrix to negate.</param>
	/// <param name="result">Negated matrix.</param>
	public static DGMatrix2x2 Negate(DGMatrix2x2 matrix)
	{
		DGMatrix2x2 result = default;
		FP m11 = -matrix.M11;
		FP m12 = -matrix.M12;

		FP m21 = -matrix.M21;
		FP m22 = -matrix.M22;


		result.M11 = m11;
		result.M12 = m12;

		result.M21 = m21;
		result.M22 = m22;

		return result;
	}

	/// <summary>
	/// Subtracts the two matrices from each other on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to subtract.</param>
	/// <param name="b">Second matrix to subtract.</param>
	/// <param name="result">Difference of the two matrices.</param>
	public static DGMatrix2x2 Subtract(DGMatrix2x2 a, DGMatrix2x2 b)
	{
		DGMatrix2x2 result = default;
		FP m11 = a.M11 - b.M11;
		FP m12 = a.M12 - b.M12;

		FP m21 = a.M21 - b.M21;
		FP m22 = a.M22 - b.M22;

		result.M11 = m11;
		result.M12 = m12;

		result.M21 = m21;
		result.M22 = m22;

		return result;
	}

	/// <summary>
	/// Transforms the vector by the matrix.
	/// </summary>
	/// <param name="v">Vector2 to transform.</param>
	/// <param name="matrix">Matrix to use as the transformation.</param>
	/// <param name="result">Product of the transformation.</param>
	public static FPVector2 Transform(FPVector2 v, DGMatrix2x2 matrix)
	{
		FPVector2 result = default;
		FP vX = v.x;
		FP vY = v.y;
		result.x = vX * matrix.M11 + vY * matrix.M21;
		result.y = vX * matrix.M12 + vY * matrix.M22;

		return result;
	}

	/// <summary>
	/// Computes the transposed matrix of a matrix.
	/// </summary>
	/// <param name="matrix">Matrix to transpose.</param>
	/// <param name="result">Transposed matrix.</param>
	public static DGMatrix2x2 Transpose(DGMatrix2x2 matrix)
	{
		DGMatrix2x2 result = default;
		FP m21 = matrix.M12;

		result.M11 = matrix.M11;
		result.M12 = matrix.M21;

		result.M21 = m21;
		result.M22 = matrix.M22;

		return result;
	}

	/// <summary>
	/// Transposes the matrix in-place.
	/// </summary>
	public void Transpose()
	{
		FP m21 = M21;
		M21 = M12;
		M12 = m21;
	}

	/// <summary>
	/// Creates a string representation of the matrix.
	/// </summary>
	/// <returns>A string representation of the matrix.</returns>
	public override string ToString()
	{
		return "{" + M11 + ", " + M12 + "} " +
		       "{" + M21 + ", " + M22 + "}";
	}

	/// <summary>
	/// Calculates the determinant of the matrix.
	/// </summary>
	/// <returns>The matrix's determinant.</returns>
	public FP Determinant()
	{
		return M11 * M22 - M12 * M21;
	}
}