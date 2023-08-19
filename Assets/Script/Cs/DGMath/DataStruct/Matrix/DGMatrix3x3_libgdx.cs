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

/// <summary>
/// 3 row, 3 column matrix.
/// </summary>
public partial struct DGMatrix3x3
{
	public const int M00Index = 0;
	public const int M01Index = 3;
	public const int M02Index = 6;
	public const int M10Index = 1;
	public const int M11Index = 4;
	public const int M12Index = 7;
	public const int M20Index = 2;
	public const int M21Index = 5;
	public const int M22Index = 8;
	private const int Count = 9;


	public DGFixedPoint m00;
	public DGFixedPoint m01;
	public DGFixedPoint m02;
	public DGFixedPoint m10;
	public DGFixedPoint m11;
	public DGFixedPoint m12;
	public DGFixedPoint m20;
	public DGFixedPoint m21;
	public DGFixedPoint m22;


	public static DGMatrix3x3 default2
	{
		get
		{
			DGMatrix3x3 result = default;
			result.m00 = (DGFixedPoint)1f;
			result.m11 = (DGFixedPoint)1f;
			result.m22 = (DGFixedPoint)1f;
			return result;
		}
	}

	public DGFixedPoint[] val
	{
		get
		{
			var result = new DGFixedPoint[Count];
			result[M00Index] = m00;
			result[M01Index] = m01;
			result[M02Index] = m02;
			result[M10Index] = m10;
			result[M11Index] = m11;
			result[M12Index] = m12;
			result[M20Index] = m20;
			result[M21Index] = m21;
			result[M22Index] = m22;
			return result;
		}
	}


	private DGFixedPoint[] _NewTmp()
	{
		var result = new DGFixedPoint[Count];
		result[M22Index] = (DGFixedPoint) 1;
		return result;
	}


	/** Constructs a matrix from the given float array. The array must have at least 9 elements; the first 9 will be copied.
	 * @param values The float array to copy. Remember that this matrix is in
	 *           <a href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order">column major</a> order. (The float array
	 *           is not modified.) */
	public DGMatrix3x3(DGFixedPoint[] values)
	{
	this.m00=values[M00Index];
	this.m01=values[M01Index];
	this.m02=values[M02Index];
	this.m10=values[M10Index];
	this.m11=values[M11Index];
	this.m12=values[M12Index];
	this.m20=values[M20Index];
	this.m21=values[M21Index];
	this.m22=values[M22Index];
	}

/** Sets this matrix to the identity matrix
 * @return This matrix for the purpose of chaining operations. */
	public DGMatrix3x3 idt()
	{
		this.m00 = (DGFixedPoint) 1;
		this.m10 = (DGFixedPoint) 0;
		this.m20 = (DGFixedPoint) 0;
		this.m01 = (DGFixedPoint) 0;
		this.m11 = (DGFixedPoint) 1;
		this.m21 = (DGFixedPoint) 0;
		this.m02 = (DGFixedPoint) 0;
		this.m12 = (DGFixedPoint) 0;
		this.m22 = (DGFixedPoint) 1;
		return this;
	}

	/** Postmultiplies this matrix with the provided matrix and stores the result in this matrix. For example:
	 * 
	 * <pre>
	 * A.mul(B) results in A := AB
	 * </pre>
	 * 
	 * @param m Matrix to multiply by.
	 * @return This matrix for the purpose of chaining operations together. */
	public DGMatrix3x3 mul(DGMatrix3x3 m)
	{
		DGFixedPoint v00 = this.m00 * m.m00 + this.m01 * m.m10 + this.m02 * m.m20;
		DGFixedPoint v01 = this.m00 * m.m01 + this.m01 * m.m11 + this.m02 * m.m21;
		DGFixedPoint v02 = this.m00 * m.m02 + this.m01 * m.m12 + this.m02 * m.m22;

		DGFixedPoint v10 = this.m10 * m.m00 + this.m11 * m.m10 + this.m12 * m.m20;
		DGFixedPoint v11 = this.m10 * m.m01 + this.m11 * m.m11 + this.m12 * m.m21;
		DGFixedPoint v12 = this.m10 * m.m02 + this.m11 * m.m12 + this.m12 * m.m22;

		DGFixedPoint v20 = this.m20 * m.m00 + this.m21 * m.m10 + this.m22 * m.m20;
		DGFixedPoint v21 = this.m20 * m.m01 + this.m21 * m.m11 + this.m22 * m.m21;
		DGFixedPoint v22 = this.m20 * m.m02 + this.m21 * m.m12 + this.m22 * m.m22;

		this.m00 = v00;
		this.m10 = v10;
		this.m20 = v20;
		this.m01 = v01;
		this.m11 = v11;
		this.m21 = v21;
		this.m02 = v02;
		this.m12 = v12;
		this.m22 = v22;

		return this;
	}

	/** Premultiplies this matrix with the provided matrix and stores the result in this matrix. For example:
	 * 
	 * <pre>
	 * A.mulLeft(B) results in A := BA
	 * </pre>
	 * 
	 * @param m The other Matrix to multiply by
	 * @return This matrix for the purpose of chaining operations. */
	public DGMatrix3x3 mulLeft(DGMatrix3x3 m)
	{
		DGFixedPoint v00 = m.m00 * this.m00 + m.m01 * this.m10 + m.m02 * this.m20;
		DGFixedPoint v01 = m.m00 * this.m01 + m.m01 * this.m11 + m.m02 * this.m21;
		DGFixedPoint v02 = m.m00 * this.m02 + m.m01 * this.m12 + m.m02 * this.m22;

		DGFixedPoint v10 = m.m10 * this.m00 + m.m11 * this.m10 + m.m12 * this.m20;
		DGFixedPoint v11 = m.m10 * this.m01 + m.m11 * this.m11 + m.m12 * this.m21;
		DGFixedPoint v12 = m.m10 * this.m02 + m.m11 * this.m12 + m.m12 * this.m22;

		DGFixedPoint v20 = m.m20 * this.m00 + m.m21 * this.m10 + m.m22 * this.m20;
		DGFixedPoint v21 = m.m20 * this.m01 + m.m21 * this.m11 + m.m22 * this.m21;
		DGFixedPoint v22 = m.m20 * this.m02 + m.m21 * this.m12 + m.m22 * this.m22;

		this.m00 = v00;
		this.m10 = v10;
		this.m20 = v20;
		this.m01 = v01;
		this.m11 = v11;
		this.m21 = v21;
		this.m02 = v02;
		this.m12 = v12;
		this.m22 = v22;

		return this;
	}

	/** Sets this matrix to a rotation matrix that will rotate any vector in counter-clockwise direction around the z-axis.
	 * @param degrees the angle in degrees.
	 * @return This matrix for the purpose of chaining operations. */
	public DGMatrix3x3 setToRotation(DGFixedPoint degrees)
	{
		return setToRotationRad(DGMath.Deg2Rad * degrees);
	}

	/** Sets this matrix to a rotation matrix that will rotate any vector in counter-clockwise direction around the z-axis.
	 * @param radians the angle in radians.
	 * @return This matrix for the purpose of chaining operations. */
	public DGMatrix3x3 setToRotationRad(DGFixedPoint radians)
	{
		DGFixedPoint cos = DGMath.Cos(radians);
		DGFixedPoint sin = DGMath.Sin(radians);

		this.m00 = cos;
		this.m10 = sin;
		this.m20 = (DGFixedPoint) 0;

		this.m01 = -sin;
		this.m11 = cos;
		this.m21 = (DGFixedPoint) 0;

		this.m02 = (DGFixedPoint) 0;
		this.m12 = (DGFixedPoint) 0;
		this.m22 = (DGFixedPoint) 1;

		return this;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="axis">Axis around which to rotate. need nomalized</param>
	/// <param name="degrees"></param>
	/// <returns></returns>
	public DGMatrix3x3 setToRotation(DGVector3 axis, DGFixedPoint degrees)
	{
		return setToRotation(axis, DGMath.CosDeg(degrees), DGMath.SinDeg(degrees));
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="axis">Axis around which to rotate. need nomalized</param>
	/// <param name="cos"></param>
	/// <param name="sin"></param>
	/// <returns></returns>
	public DGMatrix3x3 setToRotation(DGVector3 axis, DGFixedPoint cos, DGFixedPoint sin)
	{
		DGFixedPoint oc = (DGFixedPoint) 1.0f - cos;
		this.m00 = oc * axis.x * axis.x + cos;
		this.m01 = oc * axis.x * axis.y - axis.z * sin;
		this.m02 = oc * axis.z * axis.x + axis.y * sin;
		this.m10 = oc * axis.x * axis.y + axis.z * sin;
		this.m11 = oc * axis.y * axis.y + cos;
		this.m12 = oc * axis.y * axis.z - axis.x * sin;
		this.m20 = oc * axis.z * axis.x - axis.y * sin;
		this.m21 = oc * axis.y * axis.z + axis.x * sin;
		this.m22 = oc * axis.z * axis.z + cos;
		return this;
	}

	/** Sets this matrix to a translation matrix.
	 * @param x the translation in x
	 * @param y the translation in y
	 * @return This matrix for the purpose of chaining operations. */
	public DGMatrix3x3 setToTranslation(DGFixedPoint x, DGFixedPoint y)
	{
		this.m00 = (DGFixedPoint) 1;
		this.m10 = (DGFixedPoint) 0;
		this.m20 = (DGFixedPoint) 0;

		this.m01 = (DGFixedPoint) 0;
		this.m11 = (DGFixedPoint) 1;
		this.m21 = (DGFixedPoint) 0;

		this.m02 = x;
		this.m12 = y;
		this.m22 = (DGFixedPoint) 1;

		return this;
	}

	/** Sets this matrix to a translation matrix.
	 * @param translation The translation vector.
	 * @return This matrix for the purpose of chaining operations. */
	public DGMatrix3x3 setToTranslation(DGVector2 translation)
	{
		this.m00 = (DGFixedPoint) 1;
		this.m10 = (DGFixedPoint) 0;
		this.m20 = (DGFixedPoint) 0;

		this.m01 = (DGFixedPoint) 0;
		this.m11 = (DGFixedPoint) 1;
		this.m21 = (DGFixedPoint) 0;

		this.m02 = translation.x;
		this.m12 = translation.y;
		this.m22 = (DGFixedPoint) 1;

		return this;
	}

	/** Sets this matrix to a scaling matrix.
	 * 
	 * @param scaleX the scale in x
	 * @param scaleY the scale in y
	 * @return This matrix for the purpose of chaining operations. */
	public DGMatrix3x3 setToScaling(DGFixedPoint scaleX, DGFixedPoint scaleY)
	{
		this.m00 = scaleX;
		this.m10 = (DGFixedPoint) 0;
		this.m20 = (DGFixedPoint) 0;
		this.m01 = (DGFixedPoint) 0;
		this.m11 = scaleY;
		this.m21 = (DGFixedPoint) 0;
		this.m02 = (DGFixedPoint) 0;
		this.m12 = (DGFixedPoint) 0;
		this.m22 = (DGFixedPoint) 1;
		return this;
	}

	/** Sets this matrix to a scaling matrix.
	 * @param scale The scale vector.
	 * @return This matrix for the purpose of chaining operations. */
	public DGMatrix3x3 setToScaling(DGVector2 scale)
	{
		this.m00 = scale.x;
		this.m10 = (DGFixedPoint) 0;
		this.m20 = (DGFixedPoint) 0;
		this.m01 = (DGFixedPoint) 0;
		this.m11 = scale.y;
		this.m21 = (DGFixedPoint) 0;
		this.m02 = (DGFixedPoint) 0;
		this.m12 = (DGFixedPoint) 0;
		this.m22 = (DGFixedPoint) 1;
		return this;
	}

	public override string ToString()
	{
		return "[" + this.m00 + "|" + this.m01 + "|" + this.m02 + "]\n" //
		       + "[" + this.m10 + "|" + this.m11 + "|" + this.m12 + "]\n" //
		       + "[" + this.m20 + "|" + this.m21 + "|" + this.m22 + "]";
	}

	/** @return The determinant of this matrix */
	public DGFixedPoint det()
	{
		return this.m00 * this.m11 *this.m22 +this.m01 *this.m12 *this.m20 +this.m02 *this.m10 *this.m21
		       -this.m00 * this.m12 *this.m21 - this.m01 * this.m10 * this.m22 - this.m02 * this.m11 * this.m20;
	}

	/** Inverts this matrix given that the determinant is != 0.
	 * @return This matrix for the purpose of chaining operations.
	 * @throws GdxRuntimeException if the matrix is singular (not invertible) */
	public DGMatrix3x3 inv()
	{
		DGFixedPoint det = this.det();
		if (det == (DGFixedPoint) 0) throw new Exception("Can't invert a singular matrix");

		DGFixedPoint inv_det = (DGFixedPoint) 1.0f / det;

		DGFixedPoint v00 = this.m11 * this.m22 - this.m21 * this.m12;
		DGFixedPoint v10 = this.m20 * this.m12 - this.m10 * this.m22;
		DGFixedPoint v20 = this.m10 * this.m21 - this.m20 * this.m11;
		DGFixedPoint v01 = this.m21 * this.m02 - this.m01 * this.m22;
		DGFixedPoint v11 = this.m00 * this.m22 - this.m20 * this.m02;
		DGFixedPoint v21 = this.m20 * this.m01 - this.m00 * this.m21;
		DGFixedPoint v02 = this.m01 * this.m12 - this.m11 * this.m02;
		DGFixedPoint v12 = this.m10 * this.m02 - this.m00 * this.m12;
		DGFixedPoint v22 = this.m00 *this.m11 - this.m10 * this.m01;

		this.m00 = inv_det * v00;
		this.m10 = inv_det * v10;
		this.m20 = inv_det * v20;
		this.m01 = inv_det * v01;
		this.m11 = inv_det * v11;
		this.m21 = inv_det * v21;
		this.m02 = inv_det * v02;
		this.m12 = inv_det * v12;
		this.m22 = inv_det * v22;

		return this;
	}

	/** Copies the values from the provided matrix to this matrix.
	 * @param mat The matrix to copy.
	 * @return This matrix for the purposes of chaining. */
	public DGMatrix3x3 set(DGMatrix3x3 mat)
	{
		m00 = mat.m00;
		m01 = mat.m01;
		m02 = mat.m02;
		m10 = mat.m10;
		m11 = mat.m11;
		m12 = mat.m12;
		m20 = mat.m20;
		m21 = mat.m21;
		m22 = mat.m22;
		return this;
	}

	/** Copies the values from the provided affine matrix to this matrix. The last row is set to (0, 0, 1).
	 * @param affine The affine matrix to copy.
	 * @return This matrix for the purposes of chaining. */
	public DGMatrix3x3 set(DGAffine2 affine)
	{
		this.m00 = affine.m00;
		this.m10 = affine.m10;
		this.m20 = (DGFixedPoint) 0;
		this.m01 = affine.m01;
		this.m11 = affine.m11;
		this.m21 = (DGFixedPoint) 0;
		this.m02 = affine.m02;
		this.m12 = affine.m12;
		this.m22 = (DGFixedPoint) 1;

		return this;
	}

	/** Sets this 3x3 matrix to the top left 3x3 corner of the provided 4x4 matrix.
	 * @param mat The matrix whose top left corner will be copied. This matrix will not be modified.
	 * @return This matrix for the purpose of chaining operations. */
	public DGMatrix3x3 set(DGMatrix4x4 mat)
	{
		this.m00 = mat.m00;
		this.m10 = mat.m10;
		this.m20 = mat.m20;
		this.m01 = mat.m01;
		this.m11 = mat.m11;
		this.m21 = mat.m21;
		this.m02 = mat.m02;
		this.m12 = mat.m12;
		this.m22 = mat.m22;
		return this;
	}

	/** Sets the matrix to the given matrix as a float array. The float array must have at least 9 elements; the first 9 will be
	 * copied.
	 * 
	 * @param values The matrix, in float form, that is to be copied. Remember that this matrix is in
	 *           <a href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order">column major</a> order.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix3x3 set(DGFixedPoint[] values)
	{
		m00 = values[M00Index];
		m01 = values[M01Index];
		m02 = values[M02Index];
		m10 = values[M10Index];
		m11 = values[M11Index];
		m12 = values[M12Index];
		m20 = values[M20Index];
		m21 = values[M21Index];
		m22 = values[M22Index];
		return this;
	}

	/** Adds a translational component to the matrix in the 3rd column. The other columns are untouched.
	 * @param vector The translation vector.
	 * @return This matrix for the purpose of chaining. */
	public DGMatrix3x3 trn(DGVector2 vector)
	{
		this.m02 += vector.x;
		this.m12 += vector.y;
		return this;
	}

	/** Adds a translational component to the matrix in the 3rd column. The other columns are untouched.
	 * @param x The x-component of the translation vector.
	 * @param y The y-component of the translation vector.
	 * @return This matrix for the purpose of chaining. */
	public DGMatrix3x3 trn(DGFixedPoint x, DGFixedPoint y)
	{
		this.m02 += x;
		this.m12 += y;
		return this;
	}

	/** Adds a translational component to the matrix in the 3rd column. The other columns are untouched.
	 * @param vector The translation vector. (The z-component of the vector is ignored because this is a 3x3 matrix)
	 * @return This matrix for the purpose of chaining. */
	public DGMatrix3x3 trn(DGVector3 vector)
	{
		this.m02 += vector.x;
		this.m12 += vector.y;
		return this;
	}

	/** Postmultiplies this matrix by a translation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param x The x-component of the translation vector.
	 * @param y The y-component of the translation vector.
	 * @return This matrix for the purpose of chaining. */
	public DGMatrix3x3 translate(DGFixedPoint x, DGFixedPoint y)
	{
		var tmp = DGMatrix3x3.default2;
		tmp.m02 = x;
		tmp.m12 = y;
		this.mul(tmp);
		return this;
	}

	/** Postmultiplies this matrix by a translation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param translation The translation vector.
	 * @return This matrix for the purpose of chaining. */
	public DGMatrix3x3 translate(DGVector2 translation)
	{
		var tmp = DGMatrix3x3.default2;
		tmp.m02 = translation.x;
		tmp.m12 = translation.y;
		this.mul(tmp);
		return this;
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param degrees The angle in degrees
	 * @return This matrix for the purpose of chaining. */
	public DGMatrix3x3 rotate(DGFixedPoint degrees)
	{
		return rotateRad(DGMath.Deg2Rad * degrees);
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param radians The angle in radians
	 * @return This matrix for the purpose of chaining. */
	public DGMatrix3x3 rotateRad(DGFixedPoint radians)
	{
		if (radians == (DGFixedPoint) 0) return this;
		DGFixedPoint cos = DGMath.Cos(radians);
		DGFixedPoint sin = DGMath.Sin(radians);

		var tmp = DGMatrix3x3.default2;

		tmp.m00 = cos;
		tmp.m10 = sin;
		// tmp.m20 = 0;

		tmp.m01 = -sin;
		tmp.m11 = cos;
		// tmp.m21 = 0;

		tmp.m02 = (DGFixedPoint) 0;
		tmp.m12 = (DGFixedPoint) 0;
		// tmp.m22 = 1;

		this.mul(tmp);
		return this;
	}

	/** Postmultiplies this matrix with a scale matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param scaleX The scale in the x-axis.
	 * @param scaleY The scale in the y-axis.
	 * @return This matrix for the purpose of chaining. */
	public DGMatrix3x3 scale(DGFixedPoint scaleX, DGFixedPoint scaleY)
	{
		var tmp = DGMatrix3x3.default2;
		tmp.m00 = scaleX;
		tmp.m10 = (DGFixedPoint) 0;
		// tmp.m20 = 0;

		tmp.m01 = (DGFixedPoint) 0;
		tmp.m11 = scaleY;
		// tmp[M21] = 0;

		tmp.m02 = (DGFixedPoint) 0;
		tmp.m12 = (DGFixedPoint) 0;
		// tmp[M22] = 1;

		this.mul(tmp);
		return this;
	}

	/** Postmultiplies this matrix with a scale matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param scale The vector to scale the matrix by.
	 * @return This matrix for the purpose of chaining. */
	public DGMatrix3x3 scale(DGVector2 scale)
	{
		var tmp = DGMatrix3x3.default2;

		tmp.m00 = scale.x;
		tmp.m10 = (DGFixedPoint) 0;
		// tmp.m20 = 0;

		tmp.m01 = (DGFixedPoint) 0;
		tmp.m11 = scale.y;
		// tmp.m21 = 0;

		tmp.m02 = (DGFixedPoint) 0;
		tmp.m12 = (DGFixedPoint) 0;
		// tmp.m22 = 1;

		this.mul(tmp);
		return this;
	}

	/** Get the values in this matrix.
	 * @return The float values that make up this matrix in column-major order. */
	public DGFixedPoint[] getValues()
	{
		return val;
	}

	public DGVector2 getTranslation(DGVector2 position)
	{
		position.x = this.m02;
		position.y = this.m12;
		return position;
	}

	/** @param scale The vector which will receive the (non-negative) scale components on each axis.
	 * @return The provided vector for chaining. */
	public DGVector2 getScale(DGVector2 scale)
	{
		scale.x = DGMath.Sqrt(this.m00 * this.m00 + this.m01 * this.m01);
		scale.y = DGMath.Sqrt(this.m10 * this.m10 + this.m11 * this.m11);
		return scale;
	}

	public DGFixedPoint getRotation()
	{
		return DGMath.Rad2Deg * DGMath.Atan2(this.m10, this.m00);
	}

	public DGFixedPoint getRotationRad()
	{
		return DGMath.Atan2(this.m10, this.m00);
	}

	/** Scale the matrix in the both the x and y components by the scalar value.
	 * @param scale The single value that will be used to scale both the x and y components.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix3x3 scl(DGFixedPoint scale)
	{
		this.m00 *= scale;
		this.m11 *= scale;
		return this;
	}

	/** Scale this matrix using the x and y components of the vector but leave the rest of the matrix alone.
	 * @param scale The {@link Vector3} to use to scale this matrix.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix3x3 scl(DGVector2 scale)
	{
		this.m00 *= scale.x;
		this.m11 *= scale.y;
		return this;
	}

	/** Scale this matrix using the x and y components of the vector but leave the rest of the matrix alone.
	 * @param scale The {@link Vector3} to use to scale this matrix. The z component will be ignored.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix3x3 scl(DGVector3 scale)
	{
		this.m00 *= scale.x;
		this.m11 *= scale.y;
		return this;
	}

	/** Transposes the current matrix.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix3x3 transpose()
	{
		// Where MXY you do not have to change MXX
		DGFixedPoint v01 = this.m10;
		DGFixedPoint v02 = this.m20;
		DGFixedPoint v10 = this.m01;
		DGFixedPoint v12 = this.m21;
		DGFixedPoint v20 = this.m02;
		DGFixedPoint v21 = this.m12;
		this.m01 = v01;
		this.m02 = v02;
		this.m10 = v10;
		this.m12 = v12;
		this.m20 = v20;
		this.m21 = v21;
		return this;
	}

	/** Multiplies matrix a with matrix b in the following manner:
	 * 
	 * <pre>
	 * mul(A, B) => A := AB
	 * </pre>
	 * 
	 * @param mata The float array representing the first matrix. Must have at least 9 elements.
	 * @param matb The float array representing the second matrix. Must have at least 9 elements. */
	private static void mul(DGFixedPoint[] mata, DGFixedPoint[] matb)
	{
		DGFixedPoint v00 = mata[M00Index] * matb[M00Index] + mata[M01Index] * matb[M10Index] + mata[M02Index] * matb[M20Index];
		DGFixedPoint v01 = mata[M00Index] * matb[M01Index] + mata[M01Index] * matb[M11Index] + mata[M02Index] * matb[M21Index];
		DGFixedPoint v02 = mata[M00Index] * matb[M02Index] + mata[M01Index] * matb[M12Index] + mata[M02Index] * matb[M22Index];

		DGFixedPoint v10 = mata[M10Index] * matb[M00Index] + mata[M11Index] * matb[M10Index] + mata[M12Index] * matb[M20Index];
		DGFixedPoint v11 = mata[M10Index] * matb[M01Index] + mata[M11Index] * matb[M11Index] + mata[M12Index] * matb[M21Index];
		DGFixedPoint v12 = mata[M10Index] * matb[M02Index] + mata[M11Index] * matb[M12Index] + mata[M12Index] * matb[M22Index];

		DGFixedPoint v20 = mata[M20Index] * matb[M00Index] + mata[M21Index] * matb[M10Index] + mata[M22Index] * matb[M20Index];
		DGFixedPoint v21 = mata[M20Index] * matb[M01Index] + mata[M21Index] * matb[M11Index] + mata[M22Index] * matb[M21Index];
		DGFixedPoint v22 = mata[M20Index] * matb[M02Index] + mata[M21Index] * matb[M12Index] + mata[M22Index] * matb[M22Index];

		mata[M00Index] = v00;
		mata[M10Index] = v10;
		mata[M20Index] = v20;
		mata[M01Index] = v01;
		mata[M11Index] = v11;
		mata[M21Index] = v21;
		mata[M02Index] = v02;
		mata[M12Index] = v12;
		mata[M22Index] = v22;
	}
}