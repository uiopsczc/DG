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


/// <summary>
/// github:Bepu
/// 3 row, 3 column matrix.
/// </summary>
public partial struct DGMatrix3x3
{
	/// <summary>
	/// Value at row 1, column 1 of the matrix.
	/// </summary>
	public DGFixedPoint SM11
	{
		get => val[M00];
		set => val[M00] = value;
	}

	/// <summary>
	/// Value at row 1, column 2 of the matrix.
	/// </summary>
	public DGFixedPoint SM12
	{
		get => val[M01];
		set => val[M01] = value;
	}

	/// <summary>
	/// Value at row 1, column 3 of the matrix.
	/// </summary>
	public DGFixedPoint SM13
	{
		get => val[M02];
		set => val[M02] = value;
	}

	/// <summary>
	/// Value at row 2, column 1 of the matrix.
	/// </summary>
	public DGFixedPoint SM21
	{
		get => val[M10];
		set => val[M10] = value;
	}

	/// <summary>
	/// Value at row 2, column 2 of the matrix.
	/// </summary>
	public DGFixedPoint SM22
	{
		get => val[M11];
		set => val[M11] = value;
	}

	/// <summary>
	/// Value at row 2, column 3 of the matrix.
	/// </summary>
	public DGFixedPoint SM23
	{
		get => val[M12];
		set => val[M12] = value;
	}

	/// <summary>
	/// Value at row 3, column 1 of the matrix.
	/// </summary>
	public DGFixedPoint SM31
	{
		get => val[M20];
		set => val[M20] = value;
	}

	/// <summary>
	/// Value at row 3, column 2 of the matrix.
	/// </summary>
	public DGFixedPoint SM32
	{
		get => val[M21];
		set => val[M21] = value;
	}

	/// <summary>
	/// Value at row 3, column 3 of the matrix.
	/// </summary>
	public DGFixedPoint SM33
	{
		get => val[M22];
		set => val[M22] = value;
	}

	/// <summary>
	/// Gets the 3x3 identity matrix.
	/// </summary>
	public static DGMatrix3x3 Identity =>
		new DGMatrix3x3((DGFixedPoint) 1, (DGFixedPoint) 0, (DGFixedPoint) 0, (DGFixedPoint) 0, (DGFixedPoint) 1, (DGFixedPoint) 0, (DGFixedPoint) 0, (DGFixedPoint) 0, (DGFixedPoint) 1);


	/// <summary>
	/// Gets or sets the backward vector of the matrix.
	/// </summary>
	public DGVector3 Backward
	{
		get
		{
			var x = -SM13;
			var y = -SM23;
			var z = -SM33;
			return new DGVector3(x, y, z);
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
	public DGVector3 Down
	{
		get
		{
			var x = -SM12;
			var y = -SM22;
			var z = -SM32;
			return new DGVector3(x, y, z);
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
	public DGVector3 Forward
	{
		get
		{
			var x = SM13;
			var y = SM23;
			var z = SM33;
			return new DGVector3(x, y, z);
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
	public DGVector3 Left
	{
		get
		{
			var x = -SM11;
			var y = -SM21;
			var z = -SM31;
			return new DGVector3(x, y, z);
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
	public DGVector3 Right
	{
		get
		{
			var x = SM11;
			var y = SM21;
			var z = SM31;
			return new DGVector3(x, y, z);
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
	public DGVector3 Up
	{
		get
		{
			var x = SM12;
			var y = SM22;
			var z = SM32;
			return new DGVector3(x, y, z);
		}
		set
		{
			SM12 = value.x;
			SM22 = value.y;
			SM32 = value.z;
		}
	}

	/// <summary>
	/// Constructs a new 3 row, 3 column matrix.
	/// </summary>
	/// <param name="sm11">Value at row 1, column 1 of the matrix.</param>
	/// <param name="sm12">Value at row 1, column 2 of the matrix.</param>
	/// <param name="sm13">Value at row 1, column 3 of the matrix.</param>
	/// <param name="sm21">Value at row 2, column 1 of the matrix.</param>
	/// <param name="sm22">Value at row 2, column 2 of the matrix.</param>
	/// <param name="sm23">Value at row 2, column 3 of the matrix.</param>
	/// <param name="sm31">Value at row 3, column 1 of the matrix.</param>
	/// <param name="sm32">Value at row 3, column 2 of the matrix.</param>
	/// <param name="sm33">Value at row 3, column 3 of the matrix.</param>
	public DGMatrix3x3(DGFixedPoint sm11, DGFixedPoint sm12, DGFixedPoint sm13, DGFixedPoint sm21, DGFixedPoint sm22, DGFixedPoint sm23, DGFixedPoint sm31, DGFixedPoint sm32, DGFixedPoint sm33)
	{
		val = new DGFixedPoint[Count];
		SM11 = sm11;
		SM12 = sm12;
		SM13 = sm13;
		SM21 = sm21;
		SM22 = sm22;
		SM23 = sm23;
		SM31 = sm31;
		SM32 = sm32;
		SM33 = sm33;
	}

	/*************************************************************************************
	* 模块描述:算数操作符
	*************************************************************************************/
	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <returns>Product of the multiplication.</returns>
	public static DGMatrix3x3 operator *(DGMatrix3x3 a, DGMatrix3x3 b)
	{
		DGMatrix3x3 result = Multiply(a, b);
		return result;
	}

	/// <summary>
	/// Scales all components of the matrix by the given value.
	/// </summary>
	/// <param name="m">First matrix to multiply.</param>
	/// <param name="f">Scaling value to apply to all components of the matrix.</param>
	/// <returns>Product of the multiplication.</returns>
	public static DGMatrix3x3 operator *(DGMatrix3x3 m, DGFixedPoint f)
	{
		DGMatrix3x3 result = Multiply(m, f);
		return result;
	}

	/// <summary>
	/// Scales all components of the matrix by the given value.
	/// </summary>
	/// <param name="m">First matrix to multiply.</param>
	/// <param name="f">Scaling value to apply to all components of the matrix.</param>
	/// <returns>Product of the multiplication.</returns>
	public static DGMatrix3x3 operator *(DGFixedPoint f, DGMatrix3x3 m)
	{
		DGMatrix3x3 result = Multiply(m, f);
		return result;
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
	public static DGMatrix3x3 Add(DGMatrix3x3 a, DGMatrix3x3 b)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint m11 = a.SM11 + b.SM11;
		DGFixedPoint m12 = a.SM12 + b.SM12;
		DGFixedPoint m13 = a.SM13 + b.SM13;

		DGFixedPoint m21 = a.SM21 + b.SM21;
		DGFixedPoint m22 = a.SM22 + b.SM22;
		DGFixedPoint m23 = a.SM23 + b.SM23;

		DGFixedPoint m31 = a.SM31 + b.SM31;
		DGFixedPoint m32 = a.SM32 + b.SM32;
		DGFixedPoint m33 = a.SM33 + b.SM33;

		result.SM11 = m11;
		result.SM12 = m12;
		result.SM13 = m13;

		result.SM21 = m21;
		result.SM22 = m22;
		result.SM23 = m23;

		result.SM31 = m31;
		result.SM32 = m32;
		result.SM33 = m33;

		return result;
	}

	/// <summary>
	/// Adds the two matrices together on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to add.</param>
	/// <param name="b">Second matrix to add.</param>
	/// <param name="result">Sum of the two matrices.</param>
	public static DGMatrix3x3 Add(DGMatrix4x4 a, DGMatrix3x3 b)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint m11 = a.SM11 + b.SM11;
		DGFixedPoint m12 = a.SM12 + b.SM12;
		DGFixedPoint m13 = a.SM13 + b.SM13;

		DGFixedPoint m21 = a.SM21 + b.SM21;
		DGFixedPoint m22 = a.SM22 + b.SM22;
		DGFixedPoint m23 = a.SM23 + b.SM23;

		DGFixedPoint m31 = a.SM31 + b.SM31;
		DGFixedPoint m32 = a.SM32 + b.SM32;
		DGFixedPoint m33 = a.SM33 + b.SM33;

		result.SM11 = m11;
		result.SM12 = m12;
		result.SM13 = m13;

		result.SM21 = m21;
		result.SM22 = m22;
		result.SM23 = m23;

		result.SM31 = m31;
		result.SM32 = m32;
		result.SM33 = m33;

		return result;
	}

	/// <summary>
	/// Adds the two matrices together on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to add.</param>
	/// <param name="b">Second matrix to add.</param>
	/// <param name="result">Sum of the two matrices.</param>
	public static DGMatrix3x3 Add(DGMatrix3x3 a, DGMatrix4x4 b)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint m11 = a.SM11 + b.SM11;
		DGFixedPoint m12 = a.SM12 + b.SM12;
		DGFixedPoint m13 = a.SM13 + b.SM13;

		DGFixedPoint m21 = a.SM21 + b.SM21;
		DGFixedPoint m22 = a.SM22 + b.SM22;
		DGFixedPoint m23 = a.SM23 + b.SM23;

		DGFixedPoint m31 = a.SM31 + b.SM31;
		DGFixedPoint m32 = a.SM32 + b.SM32;
		DGFixedPoint m33 = a.SM33 + b.SM33;

		result.SM11 = m11;
		result.SM12 = m12;
		result.SM13 = m13;

		result.SM21 = m21;
		result.SM22 = m22;
		result.SM23 = m23;

		result.SM31 = m31;
		result.SM32 = m32;
		result.SM33 = m33;

		return result;
	}

	/// <summary>
	/// Adds the two matrices together on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to add.</param>
	/// <param name="b">Second matrix to add.</param>
	/// <param name="result">Sum of the two matrices.</param>
	public static DGMatrix3x3 Add(DGMatrix4x4 a, DGMatrix4x4 b)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint m11 = a.SM11 + b.SM11;
		DGFixedPoint m12 = a.SM12 + b.SM12;
		DGFixedPoint m13 = a.SM13 + b.SM13;

		DGFixedPoint m21 = a.SM21 + b.SM21;
		DGFixedPoint m22 = a.SM22 + b.SM22;
		DGFixedPoint m23 = a.SM23 + b.SM23;

		DGFixedPoint m31 = a.SM31 + b.SM31;
		DGFixedPoint m32 = a.SM32 + b.SM32;
		DGFixedPoint m33 = a.SM33 + b.SM33;

		result.SM11 = m11;
		result.SM12 = m12;
		result.SM13 = m13;

		result.SM21 = m21;
		result.SM22 = m22;
		result.SM23 = m23;

		result.SM31 = m31;
		result.SM32 = m32;
		result.SM33 = m33;

		return result;
	}

	/// <summary>
	/// Creates a skew symmetric matrix M from vector A such that M * B for some other vector B is equivalent to the cross product of A and B.
	/// </summary>
	/// <param name="v">Vector to base the matrix on.</param>
	/// <param name="result">Skew-symmetric matrix result.</param>
	public static DGMatrix3x3 CreateCrossProduct(DGVector3 v)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		result.SM11 = (DGFixedPoint) 0;
		result.SM12 = -v.z;
		result.SM13 = v.y;
		result.SM21 = v.z;
		result.SM22 = (DGFixedPoint) 0;
		result.SM23 = -v.x;
		result.SM31 = -v.y;
		result.SM32 = v.x;
		result.SM33 = (DGFixedPoint) 0;

		return result;
	}

	/// <summary>
	/// Creates a 3x3 matrix from an XNA 4x4 matrix.
	/// </summary>
	/// <param name="matrix4x4">Matrix to extract a 3x3 matrix from.</param>
	/// <param name="matrix3X3">Upper 3x3 matrix extracted from the XNA matrix.</param>
	public static DGMatrix3x3 CreateFromMatrix(DGMatrix4x4 matrix4x4)
	{
		DGMatrix3x3 matrix3X3 = DGMatrix3x3.default2;
		matrix3X3.SM11 = matrix4x4.SM11;
		matrix3X3.SM12 = matrix4x4.SM12;
		matrix3X3.SM13 = matrix4x4.SM13;

		matrix3X3.SM21 = matrix4x4.SM21;
		matrix3X3.SM22 = matrix4x4.SM22;
		matrix3X3.SM23 = matrix4x4.SM23;

		matrix3X3.SM31 = matrix4x4.SM31;
		matrix3X3.SM32 = matrix4x4.SM32;
		matrix3X3.SM33 = matrix4x4.SM33;

		return matrix3X3;
	}

	/// <summary>
	/// Constructs a uniform scaling matrix.
	/// </summary>
	/// <param name="scale">Value to use in the diagonal.</param>
	/// <param name="matrix">Scaling matrix.</param>
	public static DGMatrix3x3 CreateScale(DGFixedPoint scale)
	{
		DGMatrix3x3 matrix = new DGMatrix3x3 {SM11 = scale, SM22 = scale, SM33 = scale};
		return matrix;
	}


	/// <summary>
	/// Constructs a non-uniform scaling matrix.
	/// </summary>
	/// <param name="scale">Values defining the axis scales.</param>
	/// <param name="matrix">Scaling matrix.</param>
	public static DGMatrix3x3 CreateScale(DGVector3 scale)
	{
		DGMatrix3x3 matrix = new DGMatrix3x3 {SM11 = scale.x, SM22 = scale.y, SM33 = scale.z};
		return matrix;
	}


	/// <summary>
	/// Constructs a non-uniform scaling matrix.
	/// </summary>
	/// <param name="x">Scaling along the x axis.</param>
	/// <param name="y">Scaling along the y axis.</param>
	/// <param name="z">Scaling along the z axis.</param>
	/// <param name="matrix">Scaling matrix.</param>
	public static DGMatrix3x3 CreateScale(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		DGMatrix3x3 matrix = new DGMatrix3x3 {SM11 = x, SM22 = y, SM33 = z};
		return matrix;
	}

	/// <summary>
	/// Inverts the given matix.
	/// </summary>
	/// <param name="matrix">Matrix to be inverted.</param>
	/// <param name="result">Inverted matrix.</param>
	/// <returns>false if matrix is singular, true otherwise</returns>
	public static bool Invert(DGMatrix3x3 matrix, out DGMatrix3x3 result)
	{
		return Matrix3x6.Invert(matrix, out result);
	}

	/// <summary>
	/// Inverts the given matix.
	/// </summary>
	/// <param name="matrix">Matrix to be inverted.</param>
	/// <returns>Inverted matrix.</returns>
	public static DGMatrix3x3 Invert(DGMatrix3x3 matrix)
	{
		DGMatrix3x3 toReturn = DGMatrix3x3.default2;
		Invert(matrix, out toReturn);
		return toReturn;
	}

	/// <summary>
	/// Inverts the largest nonsingular submatrix in the matrix, excluding 2x2's that involve M13 or M31, and excluding 1x1's that include nondiagonal elements.
	/// </summary>
	/// <param name="matrix">Matrix to be inverted.</param>
	/// <param name="result">Inverted matrix.</param>
	public static DGMatrix3x3 AdaptiveInvert(DGMatrix3x3 matrix)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		// Perform full Gauss-invert and return if successful
		if (Invert(matrix, out result))
			return result;

		int submatrix;
		DGFixedPoint determinantInverse = (DGFixedPoint) 1 / matrix.AdaptiveDeterminant(out submatrix);
		DGFixedPoint m11, m12, m13, m21, m22, m23, m31, m32, m33;
		switch (submatrix)
		{
			case 1: //Upper left matrix, m11, m12, m21, m22.
				m11 = matrix.SM22 * determinantInverse;
				m12 = -matrix.SM12 * determinantInverse;
				m13 = (DGFixedPoint) 0;

				m21 = -matrix.SM21 * determinantInverse;
				m22 = matrix.SM11 * determinantInverse;
				m23 = (DGFixedPoint) 0;

				m31 = (DGFixedPoint) 0;
				m32 = (DGFixedPoint) 0;
				m33 = (DGFixedPoint) 0;
				break;
			case 2: //Lower right matrix, m22, m23, m32, m33.
				m11 = (DGFixedPoint) 0;
				m12 = (DGFixedPoint) 0;
				m13 = (DGFixedPoint) 0;

				m21 = (DGFixedPoint) 0;
				m22 = matrix.SM33 * determinantInverse;
				m23 = -matrix.SM23 * determinantInverse;

				m31 = (DGFixedPoint) 0;
				m32 = -matrix.SM32 * determinantInverse;
				m33 = matrix.SM22 * determinantInverse;
				break;
			case 3: //Corners, m11, m31, m13, m33.
				m11 = matrix.SM33 * determinantInverse;
				m12 = (DGFixedPoint) 0;
				m13 = -matrix.SM13 * determinantInverse;

				m21 = (DGFixedPoint) 0;
				m22 = (DGFixedPoint) 0;
				m23 = (DGFixedPoint) 0;

				m31 = -matrix.SM31 * determinantInverse;
				m32 = (DGFixedPoint) 0;
				m33 = matrix.SM11 * determinantInverse;
				break;
			case 4: //M11
				m11 = (DGFixedPoint) 1 / matrix.SM11;
				m12 = (DGFixedPoint) 0;
				m13 = (DGFixedPoint) 0;

				m21 = (DGFixedPoint) 0;
				m22 = (DGFixedPoint) 0;
				m23 = (DGFixedPoint) 0;

				m31 = (DGFixedPoint) 0;
				m32 = (DGFixedPoint) 0;
				m33 = (DGFixedPoint) 0;
				break;
			case 5: //M22
				m11 = (DGFixedPoint) 0;
				m12 = (DGFixedPoint) 0;
				m13 = (DGFixedPoint) 0;

				m21 = (DGFixedPoint) 0;
				m22 = (DGFixedPoint) 1 / matrix.SM22;
				m23 = (DGFixedPoint) 0;

				m31 = (DGFixedPoint) 0;
				m32 = (DGFixedPoint) 0;
				m33 = (DGFixedPoint) 0;
				break;
			case 6: //M33
				m11 = (DGFixedPoint) 0;
				m12 = (DGFixedPoint) 0;
				m13 = (DGFixedPoint) 0;

				m21 = (DGFixedPoint) 0;
				m22 = (DGFixedPoint) 0;
				m23 = (DGFixedPoint) 0;

				m31 = (DGFixedPoint) 0;
				m32 = (DGFixedPoint) 0;
				m33 = (DGFixedPoint) 1 / matrix.SM33;
				break;
			default: //Completely singular.
				m11 = (DGFixedPoint) 0;
				;
				m12 = (DGFixedPoint) 0;
				;
				m13 = (DGFixedPoint) 0;
				;
				m21 = (DGFixedPoint) 0;
				;
				m22 = (DGFixedPoint) 0;
				;
				m23 = (DGFixedPoint) 0;
				;
				m31 = (DGFixedPoint) 0;
				;
				m32 = (DGFixedPoint) 0;
				;
				m33 = (DGFixedPoint) 0;
				;
				break;
		}

		result.SM11 = m11;
		result.SM12 = m12;
		result.SM13 = m13;

		result.SM21 = m21;
		result.SM22 = m22;
		result.SM23 = m23;

		result.SM31 = m31;
		result.SM32 = m32;
		result.SM33 = m33;

		return result;
	}

	/// <summary>
	/// <para>Computes the adjugate transpose of a matrix.</para>
	/// <para>The adjugate transpose A of matrix M is: det(M) * transpose(invert(M))</para>
	/// <para>This is necessary when transforming normals (bivectors) with general linear transformations.</para>
	/// </summary>
	/// <param name="matrix">Matrix to compute the adjugate transpose of.</param>
	/// <param name="result">Adjugate transpose of the input matrix.</param>
	public static DGMatrix3x3 AdjugateTranspose(DGMatrix3x3 matrix)
	{
		//Despite the relative obscurity of the operation, this is a fairly straightforward operation which is actually faster than a true invert (by virtue of cancellation).
		//Conceptually, this is implemented as transpose(det(M) * invert(M)), but that's perfectly acceptable:
		//1) transpose(invert(M)) == invert(transpose(M)), and
		//2) det(M) == det(transpose(M))
		//This organization makes it clearer that the invert's usual division by determinant drops out.
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint m11 = (matrix.SM22 * matrix.SM33 - matrix.SM23 * matrix.SM32);
		DGFixedPoint m12 = (matrix.SM13 * matrix.SM32 - matrix.SM33 * matrix.SM12);
		DGFixedPoint m13 = (matrix.SM12 * matrix.SM23 - matrix.SM22 * matrix.SM13);

		DGFixedPoint m21 = (matrix.SM23 * matrix.SM31 - matrix.SM21 * matrix.SM33);
		DGFixedPoint m22 = (matrix.SM11 * matrix.SM33 - matrix.SM13 * matrix.SM31);
		DGFixedPoint m23 = (matrix.SM13 * matrix.SM21 - matrix.SM11 * matrix.SM23);

		DGFixedPoint m31 = (matrix.SM21 * matrix.SM32 - matrix.SM22 * matrix.SM31);
		DGFixedPoint m32 = (matrix.SM12 * matrix.SM31 - matrix.SM11 * matrix.SM32);
		DGFixedPoint m33 = (matrix.SM11 * matrix.SM22 - matrix.SM12 * matrix.SM21);

		//Note transposition.
		result.SM11 = m11;
		result.SM12 = m21;
		result.SM13 = m31;

		result.SM21 = m12;
		result.SM22 = m22;
		result.SM23 = m32;

		result.SM31 = m13;
		result.SM32 = m23;
		result.SM33 = m33;

		return result;
	}


	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix3x3 Multiply(DGMatrix3x3 a, DGMatrix3x3 b)
	{
		DGFixedPoint resultM11 = a.SM11 * b.SM11 + a.SM12 * b.SM21 + a.SM13 * b.SM31;
		DGFixedPoint resultM12 = a.SM11 * b.SM12 + a.SM12 * b.SM22 + a.SM13 * b.SM32;
		DGFixedPoint resultM13 = a.SM11 * b.SM13 + a.SM12 * b.SM23 + a.SM13 * b.SM33;

		DGFixedPoint resultM21 = a.SM21 * b.SM11 + a.SM22 * b.SM21 + a.SM23 * b.SM31;
		DGFixedPoint resultM22 = a.SM21 * b.SM12 + a.SM22 * b.SM22 + a.SM23 * b.SM32;
		DGFixedPoint resultM23 = a.SM21 * b.SM13 + a.SM22 * b.SM23 + a.SM23 * b.SM33;

		DGFixedPoint resultM31 = a.SM31 * b.SM11 + a.SM32 * b.SM21 + a.SM33 * b.SM31;
		DGFixedPoint resultM32 = a.SM31 * b.SM12 + a.SM32 * b.SM22 + a.SM33 * b.SM32;
		DGFixedPoint resultM33 = a.SM31 * b.SM13 + a.SM32 * b.SM23 + a.SM33 * b.SM33;

		DGMatrix3x3 result = DGMatrix3x3.default2;
		result.SM11 = resultM11;
		result.SM12 = resultM12;
		result.SM13 = resultM13;

		result.SM21 = resultM21;
		result.SM22 = resultM22;
		result.SM23 = resultM23;

		result.SM31 = resultM31;
		result.SM32 = resultM32;
		result.SM33 = resultM33;

		return result;
	}

	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix3x3 Multiply(DGMatrix3x3 a, DGMatrix4x4 b)
	{
		DGFixedPoint resultM11 = a.SM11 * b.SM11 + a.SM12 * b.SM21 + a.SM13 * b.SM31;
		DGFixedPoint resultM12 = a.SM11 * b.SM12 + a.SM12 * b.SM22 + a.SM13 * b.SM32;
		DGFixedPoint resultM13 = a.SM11 * b.SM13 + a.SM12 * b.SM23 + a.SM13 * b.SM33;

		DGFixedPoint resultM21 = a.SM21 * b.SM11 + a.SM22 * b.SM21 + a.SM23 * b.SM31;
		DGFixedPoint resultM22 = a.SM21 * b.SM12 + a.SM22 * b.SM22 + a.SM23 * b.SM32;
		DGFixedPoint resultM23 = a.SM21 * b.SM13 + a.SM22 * b.SM23 + a.SM23 * b.SM33;

		DGFixedPoint resultM31 = a.SM31 * b.SM11 + a.SM32 * b.SM21 + a.SM33 * b.SM31;
		DGFixedPoint resultM32 = a.SM31 * b.SM12 + a.SM32 * b.SM22 + a.SM33 * b.SM32;
		DGFixedPoint resultM33 = a.SM31 * b.SM13 + a.SM32 * b.SM23 + a.SM33 * b.SM33;

		DGMatrix3x3 result = DGMatrix3x3.default2;
		result.SM11 = resultM11;
		result.SM12 = resultM12;
		result.SM13 = resultM13;

		result.SM21 = resultM21;
		result.SM22 = resultM22;
		result.SM23 = resultM23;

		result.SM31 = resultM31;
		result.SM32 = resultM32;
		result.SM33 = resultM33;

		return result;
	}

	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix3x3 Multiply(DGMatrix4x4 a, DGMatrix3x3 b)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint resultM11 = a.SM11 * b.SM11 + a.SM12 * b.SM21 + a.SM13 * b.SM31;
		DGFixedPoint resultM12 = a.SM11 * b.SM12 + a.SM12 * b.SM22 + a.SM13 * b.SM32;
		DGFixedPoint resultM13 = a.SM11 * b.SM13 + a.SM12 * b.SM23 + a.SM13 * b.SM33;

		DGFixedPoint resultM21 = a.SM21 * b.SM11 + a.SM22 * b.SM21 + a.SM23 * b.SM31;
		DGFixedPoint resultM22 = a.SM21 * b.SM12 + a.SM22 * b.SM22 + a.SM23 * b.SM32;
		DGFixedPoint resultM23 = a.SM21 * b.SM13 + a.SM22 * b.SM23 + a.SM23 * b.SM33;

		DGFixedPoint resultM31 = a.SM31 * b.SM11 + a.SM32 * b.SM21 + a.SM33 * b.SM31;
		DGFixedPoint resultM32 = a.SM31 * b.SM12 + a.SM32 * b.SM22 + a.SM33 * b.SM32;
		DGFixedPoint resultM33 = a.SM31 * b.SM13 + a.SM32 * b.SM23 + a.SM33 * b.SM33;

		result.SM11 = resultM11;
		result.SM12 = resultM12;
		result.SM13 = resultM13;

		result.SM21 = resultM21;
		result.SM22 = resultM22;
		result.SM23 = resultM23;

		result.SM31 = resultM31;
		result.SM32 = resultM32;
		result.SM33 = resultM33;

		return result;
	}


	/// <summary>
	/// Multiplies a transposed matrix with another matrix.
	/// </summary>
	/// <param name="matrix">Matrix to be multiplied.</param>
	/// <param name="transpose">Matrix to be transposed and multiplied.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix3x3 MultiplyTransposed(DGMatrix3x3 transpose, DGMatrix3x3 matrix)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint resultM11 = transpose.SM11 * matrix.SM11 + transpose.SM21 * matrix.SM21 + transpose.SM31 * matrix.SM31;
		DGFixedPoint resultM12 = transpose.SM11 * matrix.SM12 + transpose.SM21 * matrix.SM22 + transpose.SM31 * matrix.SM32;
		DGFixedPoint resultM13 = transpose.SM11 * matrix.SM13 + transpose.SM21 * matrix.SM23 + transpose.SM31 * matrix.SM33;

		DGFixedPoint resultM21 = transpose.SM12 * matrix.SM11 + transpose.SM22 * matrix.SM21 + transpose.SM32 * matrix.SM31;
		DGFixedPoint resultM22 = transpose.SM12 * matrix.SM12 + transpose.SM22 * matrix.SM22 + transpose.SM32 * matrix.SM32;
		DGFixedPoint resultM23 = transpose.SM12 * matrix.SM13 + transpose.SM22 * matrix.SM23 + transpose.SM32 * matrix.SM33;

		DGFixedPoint resultM31 = transpose.SM13 * matrix.SM11 + transpose.SM23 * matrix.SM21 + transpose.SM33 * matrix.SM31;
		DGFixedPoint resultM32 = transpose.SM13 * matrix.SM12 + transpose.SM23 * matrix.SM22 + transpose.SM33 * matrix.SM32;
		DGFixedPoint resultM33 = transpose.SM13 * matrix.SM13 + transpose.SM23 * matrix.SM23 + transpose.SM33 * matrix.SM33;

		result.SM11 = resultM11;
		result.SM12 = resultM12;
		result.SM13 = resultM13;

		result.SM21 = resultM21;
		result.SM22 = resultM22;
		result.SM23 = resultM23;

		result.SM31 = resultM31;
		result.SM32 = resultM32;
		result.SM33 = resultM33;

		return result;
	}

	/// <summary>
	/// Multiplies a matrix with a transposed matrix.
	/// </summary>
	/// <param name="matrix">Matrix to be multiplied.</param>
	/// <param name="transpose">Matrix to be transposed and multiplied.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix3x3 MultiplyByTransposed(DGMatrix3x3 matrix, DGMatrix3x3 transpose)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint resultM11 = matrix.SM11 * transpose.SM11 + matrix.SM12 * transpose.SM12 + matrix.SM13 * transpose.SM13;
		DGFixedPoint resultM12 = matrix.SM11 * transpose.SM21 + matrix.SM12 * transpose.SM22 + matrix.SM13 * transpose.SM23;
		DGFixedPoint resultM13 = matrix.SM11 * transpose.SM31 + matrix.SM12 * transpose.SM32 + matrix.SM13 * transpose.SM33;

		DGFixedPoint resultM21 = matrix.SM21 * transpose.SM11 + matrix.SM22 * transpose.SM12 + matrix.SM23 * transpose.SM13;
		DGFixedPoint resultM22 = matrix.SM21 * transpose.SM21 + matrix.SM22 * transpose.SM22 + matrix.SM23 * transpose.SM23;
		DGFixedPoint resultM23 = matrix.SM21 * transpose.SM31 + matrix.SM22 * transpose.SM32 + matrix.SM23 * transpose.SM33;

		DGFixedPoint resultM31 = matrix.SM31 * transpose.SM11 + matrix.SM32 * transpose.SM12 + matrix.SM33 * transpose.SM13;
		DGFixedPoint resultM32 = matrix.SM31 * transpose.SM21 + matrix.SM32 * transpose.SM22 + matrix.SM33 * transpose.SM23;
		DGFixedPoint resultM33 = matrix.SM31 * transpose.SM31 + matrix.SM32 * transpose.SM32 + matrix.SM33 * transpose.SM33;

		result.SM11 = resultM11;
		result.SM12 = resultM12;
		result.SM13 = resultM13;

		result.SM21 = resultM21;
		result.SM22 = resultM22;
		result.SM23 = resultM23;

		result.SM31 = resultM31;
		result.SM32 = resultM32;
		result.SM33 = resultM33;

		return result;
	}

	/// <summary>
	/// Scales all components of the matrix.
	/// </summary>
	/// <param name="matrix">Matrix to scale.</param>
	/// <param name="scale">Amount to scale.</param>
	/// <param name="result">Scaled matrix.</param>
	public static DGMatrix3x3 Multiply(DGMatrix3x3 matrix, DGFixedPoint scale)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		result.SM11 = matrix.SM11 * scale;
		result.SM12 = matrix.SM12 * scale;
		result.SM13 = matrix.SM13 * scale;

		result.SM21 = matrix.SM21 * scale;
		result.SM22 = matrix.SM22 * scale;
		result.SM23 = matrix.SM23 * scale;

		result.SM31 = matrix.SM31 * scale;
		result.SM32 = matrix.SM32 * scale;
		result.SM33 = matrix.SM33 * scale;
		return result;
	}

	/// <summary>
	/// Negates every element in the matrix.
	/// </summary>
	/// <param name="matrix">Matrix to negate.</param>
	/// <param name="result">Negated matrix.</param>
	public static DGMatrix3x3 Negate(DGMatrix3x3 matrix)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		result.SM11 = -matrix.SM11;
		result.SM12 = -matrix.SM12;
		result.SM13 = -matrix.SM13;

		result.SM21 = -matrix.SM21;
		result.SM22 = -matrix.SM22;
		result.SM23 = -matrix.SM23;

		result.SM31 = -matrix.SM31;
		result.SM32 = -matrix.SM32;
		result.SM33 = -matrix.SM33;

		return result;
	}

	/// <summary>
	/// Subtracts the two matrices from each other on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to subtract.</param>
	/// <param name="b">Second matrix to subtract.</param>
	/// <param name="result">Difference of the two matrices.</param>
	public static DGMatrix3x3 Subtract(DGMatrix3x3 a, DGMatrix3x3 b)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint m11 = a.SM11 - b.SM11;
		DGFixedPoint m12 = a.SM12 - b.SM12;
		DGFixedPoint m13 = a.SM13 - b.SM13;

		DGFixedPoint m21 = a.SM21 - b.SM21;
		DGFixedPoint m22 = a.SM22 - b.SM22;
		DGFixedPoint m23 = a.SM23 - b.SM23;

		DGFixedPoint m31 = a.SM31 - b.SM31;
		DGFixedPoint m32 = a.SM32 - b.SM32;
		DGFixedPoint m33 = a.SM33 - b.SM33;

		result.SM11 = m11;
		result.SM12 = m12;
		result.SM13 = m13;

		result.SM21 = m21;
		result.SM22 = m22;
		result.SM23 = m23;

		result.SM31 = m31;
		result.SM32 = m32;
		result.SM33 = m33;

		return result;
	}

	/// <summary>
	/// Creates a 4x4 matrix from a 3x3 matrix.
	/// </summary>
	/// <param name="a">3x3 matrix.</param>
	/// <param name="b">Created 4x4 matrix.</param>
	public static DGMatrix4x4 ToMatrix4X4(DGMatrix3x3 a)
	{
		DGMatrix4x4 b = DGMatrix4x4.default2;
		b.SM11 = a.SM11;
		b.SM12 = a.SM12;
		b.SM13 = a.SM13;

		b.SM21 = a.SM21;
		b.SM22 = a.SM22;
		b.SM23 = a.SM23;

		b.SM31 = a.SM31;
		b.SM32 = a.SM32;
		b.SM33 = a.SM33;

		b.SM44 = (DGFixedPoint) 1;
		b.SM14 = (DGFixedPoint) 0;
		b.SM24 = (DGFixedPoint) 0;
		b.SM34 = (DGFixedPoint) 0;
		b.SM41 = (DGFixedPoint) 0;
		b.SM42 = (DGFixedPoint) 0;
		b.SM43 = (DGFixedPoint) 0;

		return b;
	}

	/// <summary>
	/// Transforms the vector by the matrix.
	/// </summary>
	/// <param name="v">Vector3 to transform.</param>
	/// <param name="matrix">Matrix to use as the transformation.</param>
	/// <param name="result">Product of the transformation.</param>
	public static DGVector3 Transform(DGVector3 v, DGMatrix3x3 matrix)
	{
		DGVector3 result = default;
		DGFixedPoint vX = v.x;
		DGFixedPoint vY = v.y;
		DGFixedPoint vZ = v.z;
		result.x = vX * matrix.SM11 + vY * matrix.SM21 + vZ * matrix.SM31;
		result.y = vX * matrix.SM12 + vY * matrix.SM22 + vZ * matrix.SM32;
		result.z = vX * matrix.SM13 + vY * matrix.SM23 + vZ * matrix.SM33;
		return result;
	}


	/// <summary>
	/// Transforms the vector by the matrix's transpose.
	/// </summary>
	/// <param name="v">Vector3 to transform.</param>
	/// <param name="matrix">Matrix to use as the transformation transpose.</param>
	/// <param name="result">Product of the transformation.</param>
	public static DGVector3 TransformTranspose(DGVector3 v, DGMatrix3x3 matrix)
	{
		DGVector3 result = default;
		DGFixedPoint vX = v.x;
		DGFixedPoint vY = v.y;
		DGFixedPoint vZ = v.z;
		result.x = vX * matrix.SM11 + vY * matrix.SM12 + vZ * matrix.SM13;
		result.y = vX * matrix.SM21 + vY * matrix.SM22 + vZ * matrix.SM23;
		result.z = vX * matrix.SM31 + vY * matrix.SM32 + vZ * matrix.SM33;

		return result;
	}


	/// <summary>
	/// Computes the transposed matrix of a matrix.
	/// </summary>
	/// <param name="matrix">Matrix to transpose.</param>
	/// <param name="result">Transposed matrix.</param>
	public static DGMatrix3x3 Transpose(DGMatrix3x3 matrix)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint m21 = matrix.SM12;
		DGFixedPoint m31 = matrix.SM13;
		DGFixedPoint m12 = matrix.SM21;
		DGFixedPoint m32 = matrix.SM23;
		DGFixedPoint m13 = matrix.SM31;
		DGFixedPoint m23 = matrix.SM32;

		result.SM11 = matrix.SM11;
		result.SM12 = m12;
		result.SM13 = m13;
		result.SM21 = m21;
		result.SM22 = matrix.SM22;
		result.SM23 = m23;
		result.SM31 = m31;
		result.SM32 = m32;
		result.SM33 = matrix.SM33;

		return result;
	}

	/// <summary>
	/// Creates a 3x3 matrix representing the orientation stored in the quaternion.
	/// </summary>
	/// <param name="quaternion">Quaternion to use to create a matrix.</param>
	/// <param name="result">Matrix representing the quaternion's orientation.</param>
	public static DGMatrix3x3 CreateFromQuaternion(DGQuaternion quaternion)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint qX2 = quaternion.x + quaternion.x;
		DGFixedPoint qY2 = quaternion.y + quaternion.y;
		DGFixedPoint qZ2 = quaternion.z + quaternion.z;
		DGFixedPoint XX = qX2 * quaternion.x;
		DGFixedPoint YY = qY2 * quaternion.y;
		DGFixedPoint ZZ = qZ2 * quaternion.z;
		DGFixedPoint XY = qX2 * quaternion.y;
		DGFixedPoint XZ = qX2 * quaternion.z;
		DGFixedPoint XW = qX2 * quaternion.w;
		DGFixedPoint YZ = qY2 * quaternion.z;
		DGFixedPoint YW = qY2 * quaternion.w;
		DGFixedPoint ZW = qZ2 * quaternion.w;

		result.SM11 = (DGFixedPoint) 1 - YY - ZZ;
		result.SM21 = XY - ZW;
		result.SM31 = XZ + YW;

		result.SM12 = XY + ZW;
		result.SM22 = (DGFixedPoint) 1 - XX - ZZ;
		result.SM32 = YZ - XW;

		result.SM13 = XZ - YW;
		result.SM23 = YZ + XW;
		result.SM33 = (DGFixedPoint) 1 - XX - YY;

		return result;
	}


	/// <summary>
	/// Computes the outer product of the given vectors.
	/// </summary>
	/// <param name="a">First vector.</param>
	/// <param name="b">Second vector.</param>
	/// <param name="result">Outer product result.</param>
	public static DGMatrix3x3 CreateOuterProduct(DGVector3 a, DGVector3 b)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		result.SM11 = a.x * b.x;
		result.SM12 = a.x * b.y;
		result.SM13 = a.x * b.z;

		result.SM21 = a.y * b.x;
		result.SM22 = a.y * b.y;
		result.SM23 = a.y * b.z;

		result.SM31 = a.z * b.x;
		result.SM32 = a.z * b.y;
		result.SM33 = a.z * b.z;

		return result;
	}

	/// <summary>
	/// Creates a matrix representing a rotation of a given angle around a given axis.
	/// </summary>
	/// <param name="axis">Axis around which to rotate. need nomalized</param>
	/// <param name="angle">Amount to rotate.</param>
	/// <param name="result">Matrix representing the rotation.</param>
	public static DGMatrix3x3 CreateFromAxisAngle(DGVector3 axis, DGFixedPoint angle)
	{
		return CreateFromAxisAngleRad(axis, angle * DGMath.Deg2Rad);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="axis">Axis around which to rotate. need nomalized</param>
	/// <param name="radians"></param>
	/// <returns></returns>
	public static DGMatrix3x3 CreateFromAxisAngleRad(DGVector3 axis, DGFixedPoint radians)
	{
		DGMatrix3x3 result = DGMatrix3x3.default2;
		DGFixedPoint xx = axis.x * axis.x;
		DGFixedPoint yy = axis.y * axis.y;
		DGFixedPoint zz = axis.z * axis.z;
		DGFixedPoint xy = axis.x * axis.y;
		DGFixedPoint xz = axis.x * axis.z;
		DGFixedPoint yz = axis.y * axis.z;

		DGFixedPoint sin = DGMath.Sin(radians);
		DGFixedPoint cos = DGMath.Cos(radians);

		DGFixedPoint oc = (DGFixedPoint)1 - cos;

		result.SM11 = oc * xx + cos;
		result.SM21 = oc * xy + axis.z * sin;
		result.SM31 = oc * xz - axis.y * sin;

		result.SM12 = oc * xy - axis.z * sin;
		result.SM22 = oc * yy + cos;
		result.SM32 = oc * yz + axis.x * sin;

		result.SM13 = oc * xz + axis.y * sin;
		result.SM23 = oc * yz - axis.x * sin;
		result.SM33 = oc * zz + cos;

		return result;
	}


	/*************************************************************************************
	* 模块描述:Util
	*************************************************************************************/


	/// <summary>
	/// Transposes the matrix in-place.
	/// </summary>
	public void Transpose()
	{
		DGFixedPoint intermediate = SM12;
		SM12 = SM21;
		SM21 = intermediate;

		intermediate = SM13;
		SM13 = SM31;
		SM31 = intermediate;

		intermediate = SM23;
		SM23 = SM32;
		SM32 = intermediate;
	}


	/// <summary>
	/// Calculates the determinant of largest nonsingular submatrix, excluding 2x2's that involve M13 or M31, and excluding all 1x1's that involve nondiagonal elements.
	/// </summary>
	/// <param name="subMatrixCode">Represents the submatrix that was used to compute the determinant.
	/// 0 is the full 3x3.  1 is the upper left 2x2.  2 is the lower right 2x2.  3 is the four corners.
	/// 4 is M11.  5 is M22.  6 is M33.</param>
	/// <returns>The matrix's determinant.</returns>
	internal DGFixedPoint AdaptiveDeterminant(out int subMatrixCode)
	{
		// We do not try the full matrix. This is handled by the AdaptiveInverse.

		// We'll play it fast and loose here and assume the following won't overflow
		//Try m11, m12, m21, m22.
		DGFixedPoint determinant = SM11 * SM22 - SM12 * SM21;
		if (determinant != (DGFixedPoint) 0)
		{
			subMatrixCode = 1;
			return determinant;
		}

		//Try m22, m23, m32, m33.
		determinant = SM22 * SM33 - SM23 * SM32;
		if (determinant != (DGFixedPoint) 0)
		{
			subMatrixCode = 2;
			return determinant;
		}

		//Try m11, m13, m31, m33.
		determinant = SM11 * SM33 - SM13 * SM12;
		if (determinant != (DGFixedPoint) 0)
		{
			subMatrixCode = 3;
			return determinant;
		}

		//Try m11.
		if (SM11 != (DGFixedPoint) 0)
		{
			subMatrixCode = 4;
			return SM11;
		}

		//Try m22.
		if (SM22 != (DGFixedPoint) 0)
		{
			subMatrixCode = 5;
			return SM22;
		}

		//Try m33.
		if (SM33 != (DGFixedPoint) 0)
		{
			subMatrixCode = 6;
			return SM33;
		}

		//It's completely singular!
		subMatrixCode = -1;
		return (DGFixedPoint) 0;
	}
}