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

namespace DG
{
	/// <summary>
	/// github:Bepu
	/// 3 row, 3 column matrix.
	/// </summary>
	public partial struct FPMatrix3x3
	{
		/// <summary>
		/// Value at row 1, column 1 of the matrix.
		/// </summary>
		public FP sm11
		{
			get => m00;
			set => m00 = value;
		}

		/// <summary>
		/// Value at row 1, column 2 of the matrix.
		/// </summary>
		public FP sm12
		{
			get => m01;
			set => m01 = value;
		}

		/// <summary>
		/// Value at row 1, column 3 of the matrix.
		/// </summary>
		public FP sm13
		{
			get => m02;
			set => m02 = value;
		}

		/// <summary>
		/// Value at row 2, column 1 of the matrix.
		/// </summary>
		public FP sm21
		{
			get => m10;
			set => m10 = value;
		}

		/// <summary>
		/// Value at row 2, column 2 of the matrix.
		/// </summary>
		public FP sm22
		{
			get => m11;
			set => m11 = value;
		}

		/// <summary>
		/// Value at row 2, column 3 of the matrix.
		/// </summary>
		public FP sm23
		{
			get => m12;
			set => m12 = value;
		}

		/// <summary>
		/// Value at row 3, column 1 of the matrix.
		/// </summary>
		public FP sm31
		{
			get => m20;
			set => m20 = value;
		}

		/// <summary>
		/// Value at row 3, column 2 of the matrix.
		/// </summary>
		public FP sm32
		{
			get => m21;
			set => m21 = value;
		}

		/// <summary>
		/// Value at row 3, column 3 of the matrix.
		/// </summary>
		public FP sm33
		{
			get => m22;
			set => m22 = value;
		}

		/// <summary>
		/// Gets the 3x3 identity matrix.
		/// </summary>
		public static FPMatrix3x3 Identity =>
			new(1, 0, 0, 0, 1, 0, 0, 0, 1);


		/// <summary>
		/// Gets or sets the backward vector of the matrix.
		/// </summary>
		public FPVector3 Backward
		{
			get
			{
				var x = -sm13;
				var y = -sm23;
				var z = -sm33;
				return new FPVector3(x, y, z);
			}
			set
			{
				sm13 = -value.x;
				sm23 = -value.y;
				sm33 = -value.z;
			}
		}

		/// <summary>
		/// Gets or sets the down vector of the matrix.
		/// </summary>
		public FPVector3 Down
		{
			get
			{
				var x = -sm12;
				var y = -sm22;
				var z = -sm32;
				return new FPVector3(x, y, z);
			}
			set
			{
				sm12 = -value.x;
				sm22 = -value.y;
				sm32 = -value.z;
			}
		}

		/// <summary>
		/// Gets or sets the forward vector of the matrix.
		/// </summary>
		public FPVector3 Forward
		{
			get
			{
				var x = sm13;
				var y = sm23;
				var z = sm33;
				return new FPVector3(x, y, z);
			}
			set
			{
				sm13 = value.x;
				sm23 = value.y;
				sm33 = value.z;
			}
		}

		/// <summary>
		/// Gets or sets the left vector of the matrix.
		/// </summary>
		public FPVector3 Left
		{
			get
			{
				var x = -sm11;
				var y = -sm21;
				var z = -sm31;
				return new FPVector3(x, y, z);
			}
			set
			{
				sm11 = -value.x;
				sm21 = -value.y;
				sm31 = -value.z;
			}
		}

		/// <summary>
		/// Gets or sets the right vector of the matrix.
		/// </summary>
		public FPVector3 Right
		{
			get
			{
				var x = sm11;
				var y = sm21;
				var z = sm31;
				return new FPVector3(x, y, z);
			}
			set
			{
				sm11 = value.x;
				sm21 = value.y;
				sm31 = value.z;
			}
		}

		/// <summary>
		/// Gets or sets the up vector of the matrix.
		/// </summary>
		public FPVector3 Up
		{
			get
			{
				var x = sm12;
				var y = sm22;
				var z = sm32;
				return new FPVector3(x, y, z);
			}
			set
			{
				sm12 = value.x;
				sm22 = value.y;
				sm32 = value.z;
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
		public FPMatrix3x3(FP sm11, FP sm12, FP sm13, FP sm21, FP sm22, FP sm23, FP sm31, FP sm32, FP sm33)
		{
			m00 = sm11;
			m01 = sm12;
			m02 = sm13;
			m10 = sm21;
			m11 = sm22;
			m12 = sm23;
			m20 = sm31;
			m21 = sm32;
			m22 = sm33;
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
		public static FPMatrix3x3 operator *(FPMatrix3x3 a, FPMatrix3x3 b)
		{
			FPMatrix3x3 result = Multiply(a, b);
			return result;
		}

		/// <summary>
		/// Scales all components of the matrix by the given value.
		/// </summary>
		/// <param name="m">First matrix to multiply.</param>
		/// <param name="f">Scaling value to apply to all components of the matrix.</param>
		/// <returns>Product of the multiplication.</returns>
		public static FPMatrix3x3 operator *(FPMatrix3x3 m, FP f)
		{
			FPMatrix3x3 result = Multiply(m, f);
			return result;
		}

		/// <summary>
		/// Scales all components of the matrix by the given value.
		/// </summary>
		/// <param name="m">First matrix to multiply.</param>
		/// <param name="f">Scaling value to apply to all components of the matrix.</param>
		/// <returns>Product of the multiplication.</returns>
		public static FPMatrix3x3 operator *(FP f, FPMatrix3x3 m)
		{
			FPMatrix3x3 result = Multiply(m, f);
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
		public static FPMatrix3x3 Add(FPMatrix3x3 a, FPMatrix3x3 b)
		{
			FPMatrix3x3 result = default2;
			FP m11 = a.sm11 + b.sm11;
			FP m12 = a.sm12 + b.sm12;
			FP m13 = a.sm13 + b.sm13;

			FP m21 = a.sm21 + b.sm21;
			FP m22 = a.sm22 + b.sm22;
			FP m23 = a.sm23 + b.sm23;

			FP m31 = a.sm31 + b.sm31;
			FP m32 = a.sm32 + b.sm32;
			FP m33 = a.sm33 + b.sm33;

			result.sm11 = m11;
			result.sm12 = m12;
			result.sm13 = m13;

			result.sm21 = m21;
			result.sm22 = m22;
			result.sm23 = m23;

			result.sm31 = m31;
			result.sm32 = m32;
			result.sm33 = m33;

			return result;
		}

		/// <summary>
		/// Adds the two matrices together on a per-element basis.
		/// </summary>
		/// <param name="a">First matrix to add.</param>
		/// <param name="b">Second matrix to add.</param>
		/// <param name="result">Sum of the two matrices.</param>
		public static FPMatrix3x3 Add(FPMatrix4x4 a, FPMatrix3x3 b)
		{
			FPMatrix3x3 result = default2;
			FP m11 = a.sm11 + b.sm11;
			FP m12 = a.sm12 + b.sm12;
			FP m13 = a.sm13 + b.sm13;

			FP m21 = a.sm21 + b.sm21;
			FP m22 = a.sm22 + b.sm22;
			FP m23 = a.sm23 + b.sm23;

			FP m31 = a.sm31 + b.sm31;
			FP m32 = a.sm32 + b.sm32;
			FP m33 = a.sm33 + b.sm33;

			result.sm11 = m11;
			result.sm12 = m12;
			result.sm13 = m13;

			result.sm21 = m21;
			result.sm22 = m22;
			result.sm23 = m23;

			result.sm31 = m31;
			result.sm32 = m32;
			result.sm33 = m33;

			return result;
		}

		/// <summary>
		/// Adds the two matrices together on a per-element basis.
		/// </summary>
		/// <param name="a">First matrix to add.</param>
		/// <param name="b">Second matrix to add.</param>
		/// <param name="result">Sum of the two matrices.</param>
		public static FPMatrix3x3 Add(FPMatrix3x3 a, FPMatrix4x4 b)
		{
			FPMatrix3x3 result = default2;
			FP m11 = a.sm11 + b.sm11;
			FP m12 = a.sm12 + b.sm12;
			FP m13 = a.sm13 + b.sm13;

			FP m21 = a.sm21 + b.sm21;
			FP m22 = a.sm22 + b.sm22;
			FP m23 = a.sm23 + b.sm23;

			FP m31 = a.sm31 + b.sm31;
			FP m32 = a.sm32 + b.sm32;
			FP m33 = a.sm33 + b.sm33;

			result.sm11 = m11;
			result.sm12 = m12;
			result.sm13 = m13;

			result.sm21 = m21;
			result.sm22 = m22;
			result.sm23 = m23;

			result.sm31 = m31;
			result.sm32 = m32;
			result.sm33 = m33;

			return result;
		}

		/// <summary>
		/// Adds the two matrices together on a per-element basis.
		/// </summary>
		/// <param name="a">First matrix to add.</param>
		/// <param name="b">Second matrix to add.</param>
		/// <param name="result">Sum of the two matrices.</param>
		public static FPMatrix3x3 Add(FPMatrix4x4 a, FPMatrix4x4 b)
		{
			FPMatrix3x3 result = default2;
			FP m11 = a.sm11 + b.sm11;
			FP m12 = a.sm12 + b.sm12;
			FP m13 = a.sm13 + b.sm13;

			FP m21 = a.sm21 + b.sm21;
			FP m22 = a.sm22 + b.sm22;
			FP m23 = a.sm23 + b.sm23;

			FP m31 = a.sm31 + b.sm31;
			FP m32 = a.sm32 + b.sm32;
			FP m33 = a.sm33 + b.sm33;

			result.sm11 = m11;
			result.sm12 = m12;
			result.sm13 = m13;

			result.sm21 = m21;
			result.sm22 = m22;
			result.sm23 = m23;

			result.sm31 = m31;
			result.sm32 = m32;
			result.sm33 = m33;

			return result;
		}

		/// <summary>
		/// Creates a skew symmetric matrix M from vector A such that M * B for some other vector B is equivalent to the cross product of A and B.
		/// </summary>
		/// <param name="v">Vector to base the matrix on.</param>
		/// <param name="result">Skew-symmetric matrix result.</param>
		public static FPMatrix3x3 CreateCrossProduct(FPVector3 v)
		{
			FPMatrix3x3 result = default2;
			result.sm11 = 0;
			result.sm12 = -v.z;
			result.sm13 = v.y;
			result.sm21 = v.z;
			result.sm22 = 0;
			result.sm23 = -v.x;
			result.sm31 = -v.y;
			result.sm32 = v.x;
			result.sm33 = 0;

			return result;
		}

		/// <summary>
		/// Creates a 3x3 matrix from an XNA 4x4 matrix.
		/// </summary>
		/// <param name="matrix4x4">Matrix to extract a 3x3 matrix from.</param>
		/// <param name="matrix3X3">Upper 3x3 matrix extracted from the XNA matrix.</param>
		public static FPMatrix3x3 CreateFromMatrix(FPMatrix4x4 matrix4x4)
		{
			FPMatrix3x3 matrix3X3 = default2;
			matrix3X3.sm11 = matrix4x4.sm11;
			matrix3X3.sm12 = matrix4x4.sm12;
			matrix3X3.sm13 = matrix4x4.sm13;

			matrix3X3.sm21 = matrix4x4.sm21;
			matrix3X3.sm22 = matrix4x4.sm22;
			matrix3X3.sm23 = matrix4x4.sm23;

			matrix3X3.sm31 = matrix4x4.sm31;
			matrix3X3.sm32 = matrix4x4.sm32;
			matrix3X3.sm33 = matrix4x4.sm33;

			return matrix3X3;
		}

		/// <summary>
		/// Constructs a uniform scaling matrix.
		/// </summary>
		/// <param name="scale">Value to use in the diagonal.</param>
		/// <param name="matrix">Scaling matrix.</param>
		public static FPMatrix3x3 CreateScale(FP scale)
		{
			FPMatrix3x3 matrix = new FPMatrix3x3 { sm11 = scale, sm22 = scale, sm33 = scale };
			return matrix;
		}


		/// <summary>
		/// Constructs a non-uniform scaling matrix.
		/// </summary>
		/// <param name="scale">Values defining the axis scales.</param>
		/// <param name="matrix">Scaling matrix.</param>
		public static FPMatrix3x3 CreateScale(FPVector3 scale)
		{
			FPMatrix3x3 matrix = new FPMatrix3x3 { sm11 = scale.x, sm22 = scale.y, sm33 = scale.z };
			return matrix;
		}


		/// <summary>
		/// Constructs a non-uniform scaling matrix.
		/// </summary>
		/// <param name="x">Scaling along the x axis.</param>
		/// <param name="y">Scaling along the y axis.</param>
		/// <param name="z">Scaling along the z axis.</param>
		/// <param name="matrix">Scaling matrix.</param>
		public static FPMatrix3x3 CreateScale(FP x, FP y, FP z)
		{
			FPMatrix3x3 matrix = new FPMatrix3x3 { sm11 = x, sm22 = y, sm33 = z };
			return matrix;
		}

		/// <summary>
		/// Inverts the given matix.
		/// </summary>
		/// <param name="matrix">Matrix to be inverted.</param>
		/// <param name="result">Inverted matrix.</param>
		/// <returns>false if matrix is singular, true otherwise</returns>
		public static bool Invert(FPMatrix3x3 matrix, out FPMatrix3x3 result)
		{
			return FPMatrix3x6.Invert(matrix, out result);
		}

		/// <summary>
		/// Inverts the given matix.
		/// </summary>
		/// <param name="matrix">Matrix to be inverted.</param>
		/// <returns>Inverted matrix.</returns>
		public static FPMatrix3x3 Invert(FPMatrix3x3 matrix)
		{
			FPMatrix3x3 toReturn = default2;
			Invert(matrix, out toReturn);
			return toReturn;
		}

		/// <summary>
		/// Inverts the largest nonsingular submatrix in the matrix, excluding 2x2's that involve M13 or M31, and excluding 1x1's that include nondiagonal elements.
		/// </summary>
		/// <param name="matrix">Matrix to be inverted.</param>
		/// <param name="result">Inverted matrix.</param>
		public static FPMatrix3x3 AdaptiveInvert(FPMatrix3x3 matrix)
		{
			FPMatrix3x3 result = default2;
			// Perform full Gauss-invert and return if successful
			if (Invert(matrix, out result))
				return result;

			int submatrix;
			FP determinantInverse = 1 / matrix.AdaptiveDeterminant(out submatrix);
			FP m11, m12, m13, m21, m22, m23, m31, m32, m33;
			switch (submatrix)
			{
				case 1: //Upper left matrix, m11, m12, m21, m22.
					m11 = matrix.sm22 * determinantInverse;
					m12 = -matrix.sm12 * determinantInverse;
					m13 = 0;

					m21 = -matrix.sm21 * determinantInverse;
					m22 = matrix.sm11 * determinantInverse;
					m23 = 0;

					m31 = 0;
					m32 = 0;
					m33 = 0;
					break;
				case 2: //Lower right matrix, m22, m23, m32, m33.
					m11 = 0;
					m12 = 0;
					m13 = 0;

					m21 = 0;
					m22 = matrix.sm33 * determinantInverse;
					m23 = -matrix.sm23 * determinantInverse;

					m31 = 0;
					m32 = -matrix.sm32 * determinantInverse;
					m33 = matrix.sm22 * determinantInverse;
					break;
				case 3: //Corners, m11, m31, m13, m33.
					m11 = matrix.sm33 * determinantInverse;
					m12 = 0;
					m13 = -matrix.sm13 * determinantInverse;

					m21 = 0;
					m22 = 0;
					m23 = 0;

					m31 = -matrix.sm31 * determinantInverse;
					m32 = 0;
					m33 = matrix.sm11 * determinantInverse;
					break;
				case 4: //M11
					m11 = 1 / matrix.sm11;
					m12 = 0;
					m13 = 0;

					m21 = 0;
					m22 = 0;
					m23 = 0;

					m31 = 0;
					m32 = 0;
					m33 = 0;
					break;
				case 5: //M22
					m11 = 0;
					m12 = 0;
					m13 = 0;

					m21 = 0;
					m22 = 1 / matrix.sm22;
					m23 = 0;

					m31 = 0;
					m32 = 0;
					m33 = 0;
					break;
				case 6: //M33
					m11 = 0;
					m12 = 0;
					m13 = 0;

					m21 = 0;
					m22 = 0;
					m23 = 0;

					m31 = 0;
					m32 = 0;
					m33 = 1 / matrix.sm33;
					break;
				default: //Completely singular.
					m11 = 0;
					;
					m12 = 0;
					;
					m13 = 0;
					;
					m21 = 0;
					;
					m22 = 0;
					;
					m23 = 0;
					;
					m31 = 0;
					;
					m32 = 0;
					;
					m33 = 0;
					;
					break;
			}

			result.sm11 = m11;
			result.sm12 = m12;
			result.sm13 = m13;

			result.sm21 = m21;
			result.sm22 = m22;
			result.sm23 = m23;

			result.sm31 = m31;
			result.sm32 = m32;
			result.sm33 = m33;

			return result;
		}

		/// <summary>
		/// <para>Computes the adjugate transpose of a matrix.</para>
		/// <para>The adjugate transpose A of matrix M is: det(M) * transpose(invert(M))</para>
		/// <para>This is necessary when transforming normals (bivectors) with general linear transformations.</para>
		/// </summary>
		/// <param name="matrix">Matrix to compute the adjugate transpose of.</param>
		/// <param name="result">Adjugate transpose of the input matrix.</param>
		public static FPMatrix3x3 AdjugateTranspose(FPMatrix3x3 matrix)
		{
			//Despite the relative obscurity of the operation, this is a fairly straightforward operation which is actually faster than a true invert (by virtue of cancellation).
			//Conceptually, this is implemented as transpose(det(M) * invert(M)), but that's perfectly acceptable:
			//1) transpose(invert(M)) == invert(transpose(M)), and
			//2) det(M) == det(transpose(M))
			//This organization makes it clearer that the invert's usual division by determinant drops out.
			FPMatrix3x3 result = default2;
			FP m11 = (matrix.sm22 * matrix.sm33 - matrix.sm23 * matrix.sm32);
			FP m12 = (matrix.sm13 * matrix.sm32 - matrix.sm33 * matrix.sm12);
			FP m13 = (matrix.sm12 * matrix.sm23 - matrix.sm22 * matrix.sm13);

			FP m21 = (matrix.sm23 * matrix.sm31 - matrix.sm21 * matrix.sm33);
			FP m22 = (matrix.sm11 * matrix.sm33 - matrix.sm13 * matrix.sm31);
			FP m23 = (matrix.sm13 * matrix.sm21 - matrix.sm11 * matrix.sm23);

			FP m31 = (matrix.sm21 * matrix.sm32 - matrix.sm22 * matrix.sm31);
			FP m32 = (matrix.sm12 * matrix.sm31 - matrix.sm11 * matrix.sm32);
			FP m33 = (matrix.sm11 * matrix.sm22 - matrix.sm12 * matrix.sm21);

			//Note transposition.
			result.sm11 = m11;
			result.sm12 = m21;
			result.sm13 = m31;

			result.sm21 = m12;
			result.sm22 = m22;
			result.sm23 = m32;

			result.sm31 = m13;
			result.sm32 = m23;
			result.sm33 = m33;

			return result;
		}


		/// <summary>
		/// Multiplies the two matrices.
		/// </summary>
		/// <param name="a">First matrix to multiply.</param>
		/// <param name="b">Second matrix to multiply.</param>
		/// <param name="result">Product of the multiplication.</param>
		public static FPMatrix3x3 Multiply(FPMatrix3x3 a, FPMatrix3x3 b)
		{
			FP resultM11 = a.sm11 * b.sm11 + a.sm12 * b.sm21 + a.sm13 * b.sm31;
			FP resultM12 = a.sm11 * b.sm12 + a.sm12 * b.sm22 + a.sm13 * b.sm32;
			FP resultM13 = a.sm11 * b.sm13 + a.sm12 * b.sm23 + a.sm13 * b.sm33;

			FP resultM21 = a.sm21 * b.sm11 + a.sm22 * b.sm21 + a.sm23 * b.sm31;
			FP resultM22 = a.sm21 * b.sm12 + a.sm22 * b.sm22 + a.sm23 * b.sm32;
			FP resultM23 = a.sm21 * b.sm13 + a.sm22 * b.sm23 + a.sm23 * b.sm33;

			FP resultM31 = a.sm31 * b.sm11 + a.sm32 * b.sm21 + a.sm33 * b.sm31;
			FP resultM32 = a.sm31 * b.sm12 + a.sm32 * b.sm22 + a.sm33 * b.sm32;
			FP resultM33 = a.sm31 * b.sm13 + a.sm32 * b.sm23 + a.sm33 * b.sm33;

			FPMatrix3x3 result = default2;
			result.sm11 = resultM11;
			result.sm12 = resultM12;
			result.sm13 = resultM13;

			result.sm21 = resultM21;
			result.sm22 = resultM22;
			result.sm23 = resultM23;

			result.sm31 = resultM31;
			result.sm32 = resultM32;
			result.sm33 = resultM33;

			return result;
		}

		/// <summary>
		/// Multiplies the two matrices.
		/// </summary>
		/// <param name="a">First matrix to multiply.</param>
		/// <param name="b">Second matrix to multiply.</param>
		/// <param name="result">Product of the multiplication.</param>
		public static FPMatrix3x3 Multiply(FPMatrix3x3 a, FPMatrix4x4 b)
		{
			FP resultM11 = a.sm11 * b.sm11 + a.sm12 * b.sm21 + a.sm13 * b.sm31;
			FP resultM12 = a.sm11 * b.sm12 + a.sm12 * b.sm22 + a.sm13 * b.sm32;
			FP resultM13 = a.sm11 * b.sm13 + a.sm12 * b.sm23 + a.sm13 * b.sm33;

			FP resultM21 = a.sm21 * b.sm11 + a.sm22 * b.sm21 + a.sm23 * b.sm31;
			FP resultM22 = a.sm21 * b.sm12 + a.sm22 * b.sm22 + a.sm23 * b.sm32;
			FP resultM23 = a.sm21 * b.sm13 + a.sm22 * b.sm23 + a.sm23 * b.sm33;

			FP resultM31 = a.sm31 * b.sm11 + a.sm32 * b.sm21 + a.sm33 * b.sm31;
			FP resultM32 = a.sm31 * b.sm12 + a.sm32 * b.sm22 + a.sm33 * b.sm32;
			FP resultM33 = a.sm31 * b.sm13 + a.sm32 * b.sm23 + a.sm33 * b.sm33;

			FPMatrix3x3 result = default2;
			result.sm11 = resultM11;
			result.sm12 = resultM12;
			result.sm13 = resultM13;

			result.sm21 = resultM21;
			result.sm22 = resultM22;
			result.sm23 = resultM23;

			result.sm31 = resultM31;
			result.sm32 = resultM32;
			result.sm33 = resultM33;

			return result;
		}

		/// <summary>
		/// Multiplies the two matrices.
		/// </summary>
		/// <param name="a">First matrix to multiply.</param>
		/// <param name="b">Second matrix to multiply.</param>
		/// <param name="result">Product of the multiplication.</param>
		public static FPMatrix3x3 Multiply(FPMatrix4x4 a, FPMatrix3x3 b)
		{
			FPMatrix3x3 result = default2;
			FP resultM11 = a.sm11 * b.sm11 + a.sm12 * b.sm21 + a.sm13 * b.sm31;
			FP resultM12 = a.sm11 * b.sm12 + a.sm12 * b.sm22 + a.sm13 * b.sm32;
			FP resultM13 = a.sm11 * b.sm13 + a.sm12 * b.sm23 + a.sm13 * b.sm33;

			FP resultM21 = a.sm21 * b.sm11 + a.sm22 * b.sm21 + a.sm23 * b.sm31;
			FP resultM22 = a.sm21 * b.sm12 + a.sm22 * b.sm22 + a.sm23 * b.sm32;
			FP resultM23 = a.sm21 * b.sm13 + a.sm22 * b.sm23 + a.sm23 * b.sm33;

			FP resultM31 = a.sm31 * b.sm11 + a.sm32 * b.sm21 + a.sm33 * b.sm31;
			FP resultM32 = a.sm31 * b.sm12 + a.sm32 * b.sm22 + a.sm33 * b.sm32;
			FP resultM33 = a.sm31 * b.sm13 + a.sm32 * b.sm23 + a.sm33 * b.sm33;

			result.sm11 = resultM11;
			result.sm12 = resultM12;
			result.sm13 = resultM13;

			result.sm21 = resultM21;
			result.sm22 = resultM22;
			result.sm23 = resultM23;

			result.sm31 = resultM31;
			result.sm32 = resultM32;
			result.sm33 = resultM33;

			return result;
		}


		/// <summary>
		/// Multiplies a transposed matrix with another matrix.
		/// </summary>
		/// <param name="matrix">Matrix to be multiplied.</param>
		/// <param name="transpose">Matrix to be transposed and multiplied.</param>
		/// <param name="result">Product of the multiplication.</param>
		public static FPMatrix3x3 MultiplyTransposed(FPMatrix3x3 transpose, FPMatrix3x3 matrix)
		{
			FPMatrix3x3 result = default2;
			FP resultM11 = transpose.sm11 * matrix.sm11 + transpose.sm21 * matrix.sm21 + transpose.sm31 * matrix.sm31;
			FP resultM12 = transpose.sm11 * matrix.sm12 + transpose.sm21 * matrix.sm22 + transpose.sm31 * matrix.sm32;
			FP resultM13 = transpose.sm11 * matrix.sm13 + transpose.sm21 * matrix.sm23 + transpose.sm31 * matrix.sm33;

			FP resultM21 = transpose.sm12 * matrix.sm11 + transpose.sm22 * matrix.sm21 + transpose.sm32 * matrix.sm31;
			FP resultM22 = transpose.sm12 * matrix.sm12 + transpose.sm22 * matrix.sm22 + transpose.sm32 * matrix.sm32;
			FP resultM23 = transpose.sm12 * matrix.sm13 + transpose.sm22 * matrix.sm23 + transpose.sm32 * matrix.sm33;

			FP resultM31 = transpose.sm13 * matrix.sm11 + transpose.sm23 * matrix.sm21 + transpose.sm33 * matrix.sm31;
			FP resultM32 = transpose.sm13 * matrix.sm12 + transpose.sm23 * matrix.sm22 + transpose.sm33 * matrix.sm32;
			FP resultM33 = transpose.sm13 * matrix.sm13 + transpose.sm23 * matrix.sm23 + transpose.sm33 * matrix.sm33;

			result.sm11 = resultM11;
			result.sm12 = resultM12;
			result.sm13 = resultM13;

			result.sm21 = resultM21;
			result.sm22 = resultM22;
			result.sm23 = resultM23;

			result.sm31 = resultM31;
			result.sm32 = resultM32;
			result.sm33 = resultM33;

			return result;
		}

		/// <summary>
		/// Multiplies a matrix with a transposed matrix.
		/// </summary>
		/// <param name="matrix">Matrix to be multiplied.</param>
		/// <param name="transpose">Matrix to be transposed and multiplied.</param>
		/// <param name="result">Product of the multiplication.</param>
		public static FPMatrix3x3 MultiplyByTransposed(FPMatrix3x3 matrix, FPMatrix3x3 transpose)
		{
			FPMatrix3x3 result = default2;
			FP resultM11 = matrix.sm11 * transpose.sm11 + matrix.sm12 * transpose.sm12 + matrix.sm13 * transpose.sm13;
			FP resultM12 = matrix.sm11 * transpose.sm21 + matrix.sm12 * transpose.sm22 + matrix.sm13 * transpose.sm23;
			FP resultM13 = matrix.sm11 * transpose.sm31 + matrix.sm12 * transpose.sm32 + matrix.sm13 * transpose.sm33;

			FP resultM21 = matrix.sm21 * transpose.sm11 + matrix.sm22 * transpose.sm12 + matrix.sm23 * transpose.sm13;
			FP resultM22 = matrix.sm21 * transpose.sm21 + matrix.sm22 * transpose.sm22 + matrix.sm23 * transpose.sm23;
			FP resultM23 = matrix.sm21 * transpose.sm31 + matrix.sm22 * transpose.sm32 + matrix.sm23 * transpose.sm33;

			FP resultM31 = matrix.sm31 * transpose.sm11 + matrix.sm32 * transpose.sm12 + matrix.sm33 * transpose.sm13;
			FP resultM32 = matrix.sm31 * transpose.sm21 + matrix.sm32 * transpose.sm22 + matrix.sm33 * transpose.sm23;
			FP resultM33 = matrix.sm31 * transpose.sm31 + matrix.sm32 * transpose.sm32 + matrix.sm33 * transpose.sm33;

			result.sm11 = resultM11;
			result.sm12 = resultM12;
			result.sm13 = resultM13;

			result.sm21 = resultM21;
			result.sm22 = resultM22;
			result.sm23 = resultM23;

			result.sm31 = resultM31;
			result.sm32 = resultM32;
			result.sm33 = resultM33;

			return result;
		}

		/// <summary>
		/// Scales all components of the matrix.
		/// </summary>
		/// <param name="matrix">Matrix to scale.</param>
		/// <param name="scale">Amount to scale.</param>
		/// <param name="result">Scaled matrix.</param>
		public static FPMatrix3x3 Multiply(FPMatrix3x3 matrix, FP scale)
		{
			FPMatrix3x3 result = default2;
			result.sm11 = matrix.sm11 * scale;
			result.sm12 = matrix.sm12 * scale;
			result.sm13 = matrix.sm13 * scale;

			result.sm21 = matrix.sm21 * scale;
			result.sm22 = matrix.sm22 * scale;
			result.sm23 = matrix.sm23 * scale;

			result.sm31 = matrix.sm31 * scale;
			result.sm32 = matrix.sm32 * scale;
			result.sm33 = matrix.sm33 * scale;
			return result;
		}

		/// <summary>
		/// Negates every element in the matrix.
		/// </summary>
		/// <param name="matrix">Matrix to negate.</param>
		/// <param name="result">Negated matrix.</param>
		public static FPMatrix3x3 Negate(FPMatrix3x3 matrix)
		{
			FPMatrix3x3 result = default2;
			result.sm11 = -matrix.sm11;
			result.sm12 = -matrix.sm12;
			result.sm13 = -matrix.sm13;

			result.sm21 = -matrix.sm21;
			result.sm22 = -matrix.sm22;
			result.sm23 = -matrix.sm23;

			result.sm31 = -matrix.sm31;
			result.sm32 = -matrix.sm32;
			result.sm33 = -matrix.sm33;

			return result;
		}

		/// <summary>
		/// Subtracts the two matrices from each other on a per-element basis.
		/// </summary>
		/// <param name="a">First matrix to subtract.</param>
		/// <param name="b">Second matrix to subtract.</param>
		/// <param name="result">Difference of the two matrices.</param>
		public static FPMatrix3x3 Subtract(FPMatrix3x3 a, FPMatrix3x3 b)
		{
			FPMatrix3x3 result = default2;
			FP m11 = a.sm11 - b.sm11;
			FP m12 = a.sm12 - b.sm12;
			FP m13 = a.sm13 - b.sm13;

			FP m21 = a.sm21 - b.sm21;
			FP m22 = a.sm22 - b.sm22;
			FP m23 = a.sm23 - b.sm23;

			FP m31 = a.sm31 - b.sm31;
			FP m32 = a.sm32 - b.sm32;
			FP m33 = a.sm33 - b.sm33;

			result.sm11 = m11;
			result.sm12 = m12;
			result.sm13 = m13;

			result.sm21 = m21;
			result.sm22 = m22;
			result.sm23 = m23;

			result.sm31 = m31;
			result.sm32 = m32;
			result.sm33 = m33;

			return result;
		}

		/// <summary>
		/// Creates a 4x4 matrix from a 3x3 matrix.
		/// </summary>
		/// <param name="a">3x3 matrix.</param>
		/// <param name="b">Created 4x4 matrix.</param>
		public static FPMatrix4x4 ToMatrix4X4(FPMatrix3x3 a)
		{
			FPMatrix4x4 b = FPMatrix4x4.default2;
			b.sm11 = a.sm11;
			b.sm12 = a.sm12;
			b.sm13 = a.sm13;

			b.sm21 = a.sm21;
			b.sm22 = a.sm22;
			b.sm23 = a.sm23;

			b.sm31 = a.sm31;
			b.sm32 = a.sm32;
			b.sm33 = a.sm33;

			b.sm44 = 1;
			b.sm14 = 0;
			b.sm24 = 0;
			b.sm34 = 0;
			b.sm41 = 0;
			b.sm42 = 0;
			b.sm43 = 0;

			return b;
		}

		/// <summary>
		/// Transforms the vector by the matrix.
		/// </summary>
		/// <param name="v">Vector3 to transform.</param>
		/// <param name="matrix">Matrix to use as the transformation.</param>
		/// <param name="result">Product of the transformation.</param>
		public static FPVector3 Transform(FPVector3 v, FPMatrix3x3 matrix)
		{
			FPVector3 result = default;
			FP vX = v.x;
			FP vY = v.y;
			FP vZ = v.z;
			result.x = vX * matrix.sm11 + vY * matrix.sm21 + vZ * matrix.sm31;
			result.y = vX * matrix.sm12 + vY * matrix.sm22 + vZ * matrix.sm32;
			result.z = vX * matrix.sm13 + vY * matrix.sm23 + vZ * matrix.sm33;
			return result;
		}


		/// <summary>
		/// Transforms the vector by the matrix's transpose.
		/// </summary>
		/// <param name="v">Vector3 to transform.</param>
		/// <param name="matrix">Matrix to use as the transformation transpose.</param>
		/// <param name="result">Product of the transformation.</param>
		public static FPVector3 TransformTranspose(FPVector3 v, FPMatrix3x3 matrix)
		{
			FPVector3 result = default;
			FP vX = v.x;
			FP vY = v.y;
			FP vZ = v.z;
			result.x = vX * matrix.sm11 + vY * matrix.sm12 + vZ * matrix.sm13;
			result.y = vX * matrix.sm21 + vY * matrix.sm22 + vZ * matrix.sm23;
			result.z = vX * matrix.sm31 + vY * matrix.sm32 + vZ * matrix.sm33;

			return result;
		}


		/// <summary>
		/// Computes the transposed matrix of a matrix.
		/// </summary>
		/// <param name="matrix">Matrix to transpose.</param>
		/// <param name="result">Transposed matrix.</param>
		public static FPMatrix3x3 Transpose(FPMatrix3x3 matrix)
		{
			FPMatrix3x3 result = default2;
			FP m21 = matrix.sm12;
			FP m31 = matrix.sm13;
			FP m12 = matrix.sm21;
			FP m32 = matrix.sm23;
			FP m13 = matrix.sm31;
			FP m23 = matrix.sm32;

			result.sm11 = matrix.sm11;
			result.sm12 = m12;
			result.sm13 = m13;
			result.sm21 = m21;
			result.sm22 = matrix.sm22;
			result.sm23 = m23;
			result.sm31 = m31;
			result.sm32 = m32;
			result.sm33 = matrix.sm33;

			return result;
		}

		/// <summary>
		/// Creates a 3x3 matrix representing the orientation stored in the quaternion.
		/// </summary>
		/// <param name="quaternion">Quaternion to use to create a matrix.</param>
		/// <param name="result">Matrix representing the quaternion's orientation.</param>
		public static FPMatrix3x3 CreateFromQuaternion(FPQuaternion quaternion)
		{
			FPMatrix3x3 result = default2;
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

			result.sm11 = 1 - YY - ZZ;
			result.sm21 = XY - ZW;
			result.sm31 = XZ + YW;

			result.sm12 = XY + ZW;
			result.sm22 = 1 - XX - ZZ;
			result.sm32 = YZ - XW;

			result.sm13 = XZ - YW;
			result.sm23 = YZ + XW;
			result.sm33 = 1 - XX - YY;

			return result;
		}


		/// <summary>
		/// Computes the outer product of the given vectors.
		/// </summary>
		/// <param name="a">First vector.</param>
		/// <param name="b">Second vector.</param>
		/// <param name="result">Outer product result.</param>
		public static FPMatrix3x3 CreateOuterProduct(FPVector3 a, FPVector3 b)
		{
			FPMatrix3x3 result = default2;
			result.sm11 = a.x * b.x;
			result.sm12 = a.x * b.y;
			result.sm13 = a.x * b.z;

			result.sm21 = a.y * b.x;
			result.sm22 = a.y * b.y;
			result.sm23 = a.y * b.z;

			result.sm31 = a.z * b.x;
			result.sm32 = a.z * b.y;
			result.sm33 = a.z * b.z;

			return result;
		}

		/// <summary>
		/// Creates a matrix representing a rotation of a given angle around a given axis.
		/// </summary>
		/// <param name="axis">Axis around which to rotate. need nomalized</param>
		/// <param name="angle">Amount to rotate.</param>
		/// <param name="result">Matrix representing the rotation.</param>
		public static FPMatrix3x3 CreateFromAxisAngle(FPVector3 axis, FP angle)
		{
			return CreateFromAxisAngleRad(axis, angle * FPMath.DEG2RAD);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="axis">Axis around which to rotate. need nomalized</param>
		/// <param name="radians"></param>
		/// <returns></returns>
		public static FPMatrix3x3 CreateFromAxisAngleRad(FPVector3 axis, FP radians)
		{
			FPMatrix3x3 result = default2;
			FP xx = axis.x * axis.x;
			FP yy = axis.y * axis.y;
			FP zz = axis.z * axis.z;
			FP xy = axis.x * axis.y;
			FP xz = axis.x * axis.z;
			FP yz = axis.y * axis.z;

			FP sin = FPMath.Sin(radians);
			FP cos = FPMath.Cos(radians);

			FP oc = 1 - cos;

			result.sm11 = oc * xx + cos;
			result.sm21 = oc * xy + axis.z * sin;
			result.sm31 = oc * xz - axis.y * sin;

			result.sm12 = oc * xy - axis.z * sin;
			result.sm22 = oc * yy + cos;
			result.sm32 = oc * yz + axis.x * sin;

			result.sm13 = oc * xz + axis.y * sin;
			result.sm23 = oc * yz - axis.x * sin;
			result.sm33 = oc * zz + cos;

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
			FP intermediate = sm12;
			sm12 = sm21;
			sm21 = intermediate;

			intermediate = sm13;
			sm13 = sm31;
			sm31 = intermediate;

			intermediate = sm23;
			sm23 = sm32;
			sm32 = intermediate;
		}


		/// <summary>
		/// Calculates the determinant of largest nonsingular submatrix, excluding 2x2's that involve M13 or M31, and excluding all 1x1's that involve nondiagonal elements.
		/// </summary>
		/// <param name="subMatrixCode">Represents the submatrix that was used to compute the determinant.
		/// 0 is the full 3x3.  1 is the upper left 2x2.  2 is the lower right 2x2.  3 is the four corners.
		/// 4 is M11.  5 is M22.  6 is M33.</param>
		/// <returns>The matrix's determinant.</returns>
		internal FP AdaptiveDeterminant(out int subMatrixCode)
		{
			// We do not try the full matrix. This is handled by the AdaptiveInverse.

			// We'll play it fast and loose here and assume the following won't overflow
			//Try m11, m12, m21, m22.
			FP determinant = sm11 * sm22 - sm12 * sm21;
			if (determinant != 0)
			{
				subMatrixCode = 1;
				return determinant;
			}

			//Try m22, m23, m32, m33.
			determinant = sm22 * sm33 - sm23 * sm32;
			if (determinant != 0)
			{
				subMatrixCode = 2;
				return determinant;
			}

			//Try m11, m13, m31, m33.
			determinant = sm11 * sm33 - sm13 * sm12;
			if (determinant != 0)
			{
				subMatrixCode = 3;
				return determinant;
			}

			//Try m11.
			if (sm11 != 0)
			{
				subMatrixCode = 4;
				return sm11;
			}

			//Try m22.
			if (sm22 != 0)
			{
				subMatrixCode = 5;
				return sm22;
			}

			//Try m33.
			if (sm33 != 0)
			{
				subMatrixCode = 6;
				return sm33;
			}

			//It's completely singular!
			subMatrixCode = -1;
			return 0;
		}
	}
}
