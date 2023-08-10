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

/** A specialized 3x3 matrix that can represent sequences of 2D translations, scales, flips, rotations, and shears.
 * <a href="http://en.wikipedia.org/wiki/Affine_transformation">Affine transformations</a> preserve straight lines, and parallel
 * lines remain parallel after the transformation. Operations on affine matrices are faster because the last row can always be
 * assumed (0, 0, 1).
 *
 * @author vmilea */
public partial struct DGAffine2
{
	public DGFixedPoint m00;
	public DGFixedPoint m01;
	public DGFixedPoint m02;
	public DGFixedPoint m10;
	public DGFixedPoint m11;
	public DGFixedPoint m12;

	// constant: m21 = 0, m21 = 1, m22 = 1

	/** Constructs an identity matrix. */
	public DGAffine2(bool isNotLibgdx = false)
	{
		m00 = (DGFixedPoint) 1;
		m01 = (DGFixedPoint) 0;
		m02 = (DGFixedPoint) 0;
		m10 = (DGFixedPoint) 0;
		m11 = (DGFixedPoint) 1;
		m12 = (DGFixedPoint) 0;
	}

	public DGAffine2(DGFixedPoint m00, DGFixedPoint m01, DGFixedPoint m02, DGFixedPoint m10, DGFixedPoint m11, DGFixedPoint m12)
	{
		this.m00 = m00;
		this.m01 = m01;
		this.m02 = m02;
		this.m10 = m10;
		this.m11 = m11;
		this.m12 = m12;
	}

	/** Constructs a matrix from the given affine matrix.
	 *
	 * @param other The affine matrix to copy. This matrix will not be modified. */
	public DGAffine2(DGAffine2 other)
	{
		m00 = other.m00;
		m01 = other.m01;
		m02 = other.m02;
		m10 = other.m10;
		m11 = other.m11;
		m12 = other.m12;
	}

	/** Sets this matrix to the identity matrix
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 idt()
	{
		m00 = (DGFixedPoint) 1;
		m01 = (DGFixedPoint) 0;
		m02 = (DGFixedPoint) 0;
		m10 = (DGFixedPoint) 0;
		m11 = (DGFixedPoint) 1;
		m12 = (DGFixedPoint) 0;
		return this;
	}

	/** Copies the values from the provided affine matrix to this matrix.
	 * @param other The affine matrix to copy.
	 * @return This matrix for the purposes of chaining. */
	public DGAffine2 set(DGAffine2 other)
	{
		m00 = other.m00;
		m01 = other.m01;
		m02 = other.m02;
		m10 = other.m10;
		m11 = other.m11;
		m12 = other.m12;
		return this;
	}

	/** Copies the values from the provided matrix to this matrix.
	 * @param matrix The matrix to copy, assumed to be an affine transformation.
	 * @return This matrix for the purposes of chaining. */
	public DGAffine2 set(DGMatrix3x3 matrix)
	{
		DGFixedPoint[] other = matrix.val;

		m00 = other[DGMatrix3x3.M00];
		m01 = other[DGMatrix3x3.M01];
		m02 = other[DGMatrix3x3.M02];
		m10 = other[DGMatrix3x3.M10];
		m11 = other[DGMatrix3x3.M11];
		m12 = other[DGMatrix3x3.M12];
		return this;
	}

	/** Copies the 2D transformation components from the provided 4x4 matrix. The values are mapped as follows:
	 *
	 * <pre>
	 *      [  M00  M01  M03  ]
	 *      [  M10  M11  M13  ]
	 *      [   0    0    1   ]
	 * </pre>
	 * 
	 * @param matrix The source matrix, assumed to be an affine transformation within XY plane. This matrix will not be modified.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 set(DGMatrix4x4 matrix)
	{
		DGFixedPoint[] other = matrix.val;

		m00 = other[DGMatrix4x4.M00];
		m01 = other[DGMatrix4x4.M01];
		m02 = other[DGMatrix4x4.M03];
		m10 = other[DGMatrix4x4.M10];
		m11 = other[DGMatrix4x4.M11];
		m12 = other[DGMatrix4x4.M13];
		return this;
	}

	/** Sets this matrix to a translation matrix.
	 * @param x The translation in x
	 * @param y The translation in y
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToTranslation(DGFixedPoint x, DGFixedPoint y)
	{
		m00 = (DGFixedPoint) 1;
		m01 = (DGFixedPoint) 0;
		m02 = x;
		m10 = (DGFixedPoint) 0;
		m11 = (DGFixedPoint) 1;
		m12 = y;
		return this;
	}

	/** Sets this matrix to a translation matrix.
	 * @param trn The translation vector.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToTranslation(DGVector2 trn)
	{
		return setToTranslation(trn.x, trn.y);
	}

	/** Sets this matrix to a scaling matrix.
	 * @param scaleX The scale in x.
	 * @param scaleY The scale in y.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToScaling(DGFixedPoint scaleX, DGFixedPoint scaleY)
	{
		m00 = scaleX;
		m01 = (DGFixedPoint) 0;
		m02 = (DGFixedPoint) 0;
		m10 = (DGFixedPoint) 0;
		m11 = scaleY;
		m12 = (DGFixedPoint) 0;
		return this;
	}

	/** Sets this matrix to a scaling matrix.
	 * @param scale The scale vector.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToScaling(DGVector2 scale)
	{
		return setToScaling(scale.x, scale.y);
	}

	/** Sets this matrix to a rotation matrix that will rotate any vector in counter-clockwise direction around the z-axis.
	 * @param degrees The angle in degrees.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToRotation(DGFixedPoint degrees)
	{
		DGFixedPoint cos = DGMath.CosDeg(degrees);
		DGFixedPoint sin = DGMath.SinDeg(degrees);

		m00 = cos;
		m01 = -sin;
		m02 = (DGFixedPoint) 0;
		m10 = sin;
		m11 = cos;
		m12 = (DGFixedPoint) 0;
		return this;
	}

	/** Sets this matrix to a rotation matrix that will rotate any vector in counter-clockwise direction around the z-axis.
	 * @param radians The angle in radians.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToRotationRad(DGFixedPoint radians)
	{
		DGFixedPoint cos = DGMath.Cos(radians);
		DGFixedPoint sin = DGMath.Sin(radians);

		m00 = cos;
		m01 = -sin;
		m02 = (DGFixedPoint) 0;
		m10 = sin;
		m11 = cos;
		m12 = (DGFixedPoint) 0;
		return this;
	}

	/** Sets this matrix to a rotation matrix that will rotate any vector in counter-clockwise direction around the z-axis.
	 * @param cos The angle cosine.
	 * @param sin The angle sine.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToRotation(DGFixedPoint cos, DGFixedPoint sin)
	{
		m00 = cos;
		m01 = -sin;
		m02 = (DGFixedPoint) 0;
		m10 = sin;
		m11 = cos;
		m12 = (DGFixedPoint) 0;
		return this;
	}

	/** Sets this matrix to a shearing matrix.
	 * @param shearX The shear in x direction.
	 * @param shearY The shear in y direction.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToShearing(DGFixedPoint shearX, DGFixedPoint shearY)
	{
		m00 = (DGFixedPoint) 1;
		m01 = shearX;
		m02 = (DGFixedPoint) 0;
		m10 = shearY;
		m11 = (DGFixedPoint) 1;
		m12 = (DGFixedPoint) 0;
		return this;
	}

	/** Sets this matrix to a shearing matrix.
	 * @param shear The shear vector.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToShearing(DGVector2 shear)
	{
		return setToShearing(shear.x, shear.y);
	}

	/** Sets this matrix to a concatenation of translation, rotation and scale. It is a more efficient form for:
	 * <code>idt().translate(x, y).rotate(degrees).scale(scaleX, scaleY)</code>
	 * @param x The translation in x.
	 * @param y The translation in y.
	 * @param degrees The angle in degrees.
	 * @param scaleX The scale in y.
	 * @param scaleY The scale in x.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToTrnRotScl(DGFixedPoint x, DGFixedPoint y, DGFixedPoint degrees, DGFixedPoint scaleX, DGFixedPoint scaleY)
	{
		m02 = x;
		m12 = y;

		if (degrees == (DGFixedPoint) 0)
		{
			m00 = scaleX;
			m01 = (DGFixedPoint) 0;
			m10 = (DGFixedPoint) 0;
			m11 = scaleY;
		}
		else
		{
			DGFixedPoint sin = DGMath.SinDeg(degrees);
			DGFixedPoint cos = DGMath.CosDeg(degrees);

			m00 = cos * scaleX;
			m01 = -sin * scaleY;
			m10 = sin * scaleX;
			m11 = cos * scaleY;
		}

		return this;
	}

	/** Sets this matrix to a concatenation of translation, rotation and scale. It is a more efficient form for:
	 * <code>idt().translate(trn).rotate(degrees).scale(scale)</code>
	 * @param trn The translation vector.
	 * @param degrees The angle in degrees.
	 * @param scale The scale vector.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToTrnRotScl(DGVector2 trn, DGFixedPoint degrees, DGVector2 scale)
	{
		return setToTrnRotScl(trn.x, trn.y, degrees, scale.x, scale.y);
	}

	/** Sets this matrix to a concatenation of translation, rotation and scale. It is a more efficient form for:
	 * <code>idt().translate(x, y).rotateRad(radians).scale(scaleX, scaleY)</code>
	 * @param x The translation in x.
	 * @param y The translation in y.
	 * @param radians The angle in radians.
	 * @param scaleX The scale in y.
	 * @param scaleY The scale in x.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToTrnRotRadScl(DGFixedPoint x, DGFixedPoint y, DGFixedPoint radians, DGFixedPoint scaleX, DGFixedPoint scaleY)
	{
		m02 = x;
		m12 = y;

		if (radians == (DGFixedPoint) 0)
		{
			m00 = scaleX;
			m01 = (DGFixedPoint) 0;
			m10 = (DGFixedPoint) 0;
			m11 = scaleY;
		}
		else
		{
			DGFixedPoint sin = DGMath.Sin(radians);
			DGFixedPoint cos = DGMath.Cos(radians);

			m00 = cos * scaleX;
			m01 = -sin * scaleY;
			m10 = sin * scaleX;
			m11 = cos * scaleY;
		}

		return this;
	}

	/** Sets this matrix to a concatenation of translation, rotation and scale. It is a more efficient form for:
	 * <code>idt().translate(trn).rotateRad(radians).scale(scale)</code>
	 * @param trn The translation vector.
	 * @param radians The angle in radians.
	 * @param scale The scale vector.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToTrnRotRadScl(DGVector2 trn, DGFixedPoint radians, DGVector2 scale)
	{
		return setToTrnRotRadScl(trn.x, trn.y, radians, scale.x, scale.y);
	}

	/** Sets this matrix to a concatenation of translation and scale. It is a more efficient form for:
	 * <code>idt().translate(x, y).scale(scaleX, scaleY)</code>
	 * @param x The translation in x.
	 * @param y The translation in y.
	 * @param scaleX The scale in y.
	 * @param scaleY The scale in x.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToTrnScl(DGFixedPoint x, DGFixedPoint y, DGFixedPoint scaleX, DGFixedPoint scaleY)
	{
		m00 = scaleX;
		m01 = (DGFixedPoint) 0;
		m02 = x;
		m10 = (DGFixedPoint) 0;
		m11 = scaleY;
		m12 = y;
		return this;
	}

	/** Sets this matrix to a concatenation of translation and scale. It is a more efficient form for:
	 * <code>idt().translate(trn).scale(scale)</code>
	 * @param trn The translation vector.
	 * @param scale The scale vector.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToTrnScl(DGVector2 trn, DGVector2 scale)
	{
		return setToTrnScl(trn.x, trn.y, scale.x, scale.y);
	}

	/** Sets this matrix to the product of two matrices.
	 * @param l Left matrix.
	 * @param r Right matrix.
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 setToProduct(DGAffine2 l, DGAffine2 r)
	{
		m00 = l.m00 * r.m00 + l.m01 * r.m10;
		m01 = l.m00 * r.m01 + l.m01 * r.m11;
		m02 = l.m00 * r.m02 + l.m01 * r.m12 + l.m02;
		m10 = l.m10 * r.m00 + l.m11 * r.m10;
		m11 = l.m10 * r.m01 + l.m11 * r.m11;
		m12 = l.m10 * r.m02 + l.m11 * r.m12 + l.m12;
		return this;
	}

	/** Inverts this matrix given that the determinant is != 0.
	 * @return This matrix for the purpose of chaining operations.
	 * @throws GdxRuntimeException if the matrix is singular (not invertible) */
	public DGAffine2 inv()
	{
		DGFixedPoint det = this.det();
		if (det == (DGFixedPoint) 0) throw new Exception("Can't invert a singular affine matrix");

		DGFixedPoint invDet = (DGFixedPoint) 1.0f / det;

		DGFixedPoint tmp00 = m11;
		DGFixedPoint tmp01 = -m01;
		DGFixedPoint tmp02 = m01 * m12 - m11 * m02;
		DGFixedPoint tmp10 = -m10;
		DGFixedPoint tmp11 = m00;
		DGFixedPoint tmp12 = m10 * m02 - m00 * m12;

		m00 = invDet * tmp00;
		m01 = invDet * tmp01;
		m02 = invDet * tmp02;
		m10 = invDet * tmp10;
		m11 = invDet * tmp11;
		m12 = invDet * tmp12;
		return this;
	}

	/** Postmultiplies this matrix with the provided matrix and stores the result in this matrix. For example:
	 *
	 * <pre>
	 * A.mul(B) results in A := AB
	 * </pre>
	 * 
	 * @param other Matrix to multiply by.
	 * @return This matrix for the purpose of chaining operations together. */
	public DGAffine2 mul(DGAffine2 other)
	{
		DGFixedPoint tmp00 = m00 * other.m00 + m01 * other.m10;
		DGFixedPoint tmp01 = m00 * other.m01 + m01 * other.m11;
		DGFixedPoint tmp02 = m00 * other.m02 + m01 * other.m12 + m02;
		DGFixedPoint tmp10 = m10 * other.m00 + m11 * other.m10;
		DGFixedPoint tmp11 = m10 * other.m01 + m11 * other.m11;
		DGFixedPoint tmp12 = m10 * other.m02 + m11 * other.m12 + m12;

		m00 = tmp00;
		m01 = tmp01;
		m02 = tmp02;
		m10 = tmp10;
		m11 = tmp11;
		m12 = tmp12;
		return this;
	}

	/** Premultiplies this matrix with the provided matrix and stores the result in this matrix. For example:
	 *
	 * <pre>
	 * A.preMul(B) results in A := BA
	 * </pre>
	 * 
	 * @param other The other Matrix to multiply by
	 * @return This matrix for the purpose of chaining operations. */
	public DGAffine2 preMul(DGAffine2 other)
	{
		DGFixedPoint tmp00 = other.m00 * m00 + other.m01 * m10;
		DGFixedPoint tmp01 = other.m00 * m01 + other.m01 * m11;
		DGFixedPoint tmp02 = other.m00 * m02 + other.m01 * m12 + other.m02;
		DGFixedPoint tmp10 = other.m10 * m00 + other.m11 * m10;
		DGFixedPoint tmp11 = other.m10 * m01 + other.m11 * m11;
		DGFixedPoint tmp12 = other.m10 * m02 + other.m11 * m12 + other.m12;

		m00 = tmp00;
		m01 = tmp01;
		m02 = tmp02;
		m10 = tmp10;
		m11 = tmp11;
		m12 = tmp12;
		return this;
	}

	/** Postmultiplies this matrix by a translation matrix.
	 * @param x The x-component of the translation vector.
	 * @param y The y-component of the translation vector.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 translate(DGFixedPoint x, DGFixedPoint y)
	{
		m02 += m00 * x + m01 * y;
		m12 += m10 * x + m11 * y;
		return this;
	}

	/** Postmultiplies this matrix by a translation matrix.
	 * @param trn The translation vector.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 translate(DGVector2 trn)
	{
		return translate(trn.x, trn.y);
	}

	/** Premultiplies this matrix by a translation matrix.
	 * @param x The x-component of the translation vector.
	 * @param y The y-component of the translation vector.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 preTranslate(DGFixedPoint x, DGFixedPoint y)
	{
		m02 += x;
		m12 += y;
		return this;
	}

	/** Premultiplies this matrix by a translation matrix.
	 * @param trn The translation vector.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 preTranslate(DGVector2 trn)
	{
		return preTranslate(trn.x, trn.y);
	}

	/** Postmultiplies this matrix with a scale matrix.
	 * @param scaleX The scale in the x-axis.
	 * @param scaleY The scale in the y-axis.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 scale(DGFixedPoint scaleX, DGFixedPoint scaleY)
	{
		m00 *= scaleX;
		m01 *= scaleY;
		m10 *= scaleX;
		m11 *= scaleY;
		return this;
	}

	/** Postmultiplies this matrix with a scale matrix.
	 * @param scale The scale vector.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 scale(DGVector2 scale)
	{
		return this.scale(scale.x, scale.y);
	}

	/** Premultiplies this matrix with a scale matrix.
	 * @param scaleX The scale in the x-axis.
	 * @param scaleY The scale in the y-axis.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 preScale(DGFixedPoint scaleX, DGFixedPoint scaleY)
	{
		m00 *= scaleX;
		m01 *= scaleX;
		m02 *= scaleX;
		m10 *= scaleY;
		m11 *= scaleY;
		m12 *= scaleY;
		return this;
	}

	/** Premultiplies this matrix with a scale matrix.
	 * @param scale The scale vector.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 preScale(DGVector2 scale)
	{
		return preScale(scale.x, scale.y);
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix.
	 * @param degrees The angle in degrees
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 rotate(DGFixedPoint degrees)
	{
		if (degrees == (DGFixedPoint) 0) return this;

		DGFixedPoint cos = DGMath.CosDeg(degrees);
		DGFixedPoint sin = DGMath.SinDeg(degrees);

		DGFixedPoint tmp00 = m00 * cos + m01 * sin;
		DGFixedPoint tmp01 = m00 * -sin + m01 * cos;
		DGFixedPoint tmp10 = m10 * cos + m11 * sin;
		DGFixedPoint tmp11 = m10 * -sin + m11 * cos;

		m00 = tmp00;
		m01 = tmp01;
		m10 = tmp10;
		m11 = tmp11;
		return this;
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix.
	 * @param radians The angle in radians
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 rotateRad(DGFixedPoint radians)
	{
		if (radians == (DGFixedPoint) 0) return this;

		DGFixedPoint cos = DGMath.Cos(radians);
		DGFixedPoint sin = DGMath.Sin(radians);

		DGFixedPoint tmp00 = m00 * cos + m01 * sin;
		DGFixedPoint tmp01 = m00 * -sin + m01 * cos;
		DGFixedPoint tmp10 = m10 * cos + m11 * sin;
		DGFixedPoint tmp11 = m10 * -sin + m11 * cos;

		m00 = tmp00;
		m01 = tmp01;
		m10 = tmp10;
		m11 = tmp11;
		return this;
	}

	/** Premultiplies this matrix with a (counter-clockwise) rotation matrix.
	 * @param degrees The angle in degrees
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 preRotate(DGFixedPoint degrees)
	{
		if (degrees == (DGFixedPoint) 0) return this;

		DGFixedPoint cos = DGMath.CosDeg(degrees);
		DGFixedPoint sin = DGMath.SinDeg(degrees);

		DGFixedPoint tmp00 = cos * m00 - sin * m10;
		DGFixedPoint tmp01 = cos * m01 - sin * m11;
		DGFixedPoint tmp02 = cos * m02 - sin * m12;
		DGFixedPoint tmp10 = sin * m00 + cos * m10;
		DGFixedPoint tmp11 = sin * m01 + cos * m11;
		DGFixedPoint tmp12 = sin * m02 + cos * m12;

		m00 = tmp00;
		m01 = tmp01;
		m02 = tmp02;
		m10 = tmp10;
		m11 = tmp11;
		m12 = tmp12;
		return this;
	}

	/** Premultiplies this matrix with a (counter-clockwise) rotation matrix.
	 * @param radians The angle in radians
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 preRotateRad(DGFixedPoint radians)
	{
		if (radians == (DGFixedPoint) 0) return this;

		DGFixedPoint cos = DGMath.Cos(radians);
		DGFixedPoint sin = DGMath.Sin(radians);

		DGFixedPoint tmp00 = cos * m00 - sin * m10;
		DGFixedPoint tmp01 = cos * m01 - sin * m11;
		DGFixedPoint tmp02 = cos * m02 - sin * m12;
		DGFixedPoint tmp10 = sin * m00 + cos * m10;
		DGFixedPoint tmp11 = sin * m01 + cos * m11;
		DGFixedPoint tmp12 = sin * m02 + cos * m12;

		m00 = tmp00;
		m01 = tmp01;
		m02 = tmp02;
		m10 = tmp10;
		m11 = tmp11;
		m12 = tmp12;
		return this;
	}

	/** Postmultiplies this matrix by a shear matrix.
	 * @param shearX The shear in x direction.
	 * @param shearY The shear in y direction.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 shear(DGFixedPoint shearX, DGFixedPoint shearY)
	{
		DGFixedPoint tmp0 = m00 + shearY * m01;
		DGFixedPoint tmp1 = m01 + shearX * m00;
		m00 = tmp0;
		m01 = tmp1;

		tmp0 = m10 + shearY * m11;
		tmp1 = m11 + shearX * m10;
		m10 = tmp0;
		m11 = tmp1;
		return this;
	}

	/** Postmultiplies this matrix by a shear matrix.
	 * @param shear The shear vector.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 shear(DGVector2 shear)
	{
		return this.shear(shear.x, shear.y);
	}

	/** Premultiplies this matrix by a shear matrix.
	 * @param shearX The shear in x direction.
	 * @param shearY The shear in y direction.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 preShear(DGFixedPoint shearX, DGFixedPoint shearY)
	{
		DGFixedPoint tmp00 = m00 + shearX * m10;
		DGFixedPoint tmp01 = m01 + shearX * m11;
		DGFixedPoint tmp02 = m02 + shearX * m12;
		DGFixedPoint tmp10 = m10 + shearY * m00;
		DGFixedPoint tmp11 = m11 + shearY * m01;
		DGFixedPoint tmp12 = m12 + shearY * m02;

		m00 = tmp00;
		m01 = tmp01;
		m02 = tmp02;
		m10 = tmp10;
		m11 = tmp11;
		m12 = tmp12;
		return this;
	}

	/** Premultiplies this matrix by a shear matrix.
	 * @param shear The shear vector.
	 * @return This matrix for the purpose of chaining. */
	public DGAffine2 preShear(DGVector2 shear)
	{
		return preShear(shear.x, shear.y);
	}

	/** Calculates the determinant of the matrix.
	 * @return The determinant of this matrix. */
	public DGFixedPoint det()
	{
		return m00 * m11 - m01 * m10;
	}

	/** Get the x-y translation component of the matrix.
	 * @param position Output vector.
	 * @return Filled position. */
	public DGVector2 getTranslation(DGVector2 position)
	{
		position.x = m02;
		position.y = m12;
		return position;
	}

	/** Check if the this is a plain translation matrix.
	 * @return True if scale is 1 and rotation is 0. */
	public bool isTranslation()
	{
		return (m00 == (DGFixedPoint) 1 && m11 == (DGFixedPoint) 1 && m01 == (DGFixedPoint) 0 && m10 == (DGFixedPoint) 0);
	}

	/** Check if this is an indentity matrix.
	 * @return True if scale is 1 and rotation is 0. */
	public bool isIdt()
	{
		return (m00 == (DGFixedPoint) 1 && m02 == (DGFixedPoint) 0 && m12 == (DGFixedPoint) 0 && m11 == (DGFixedPoint) 1 && m01 == (DGFixedPoint) 0 && m10 == (DGFixedPoint) 0);
	}

	/** Applies the affine transformation on a vector. */
	public void applyTo(ref DGVector2 point)
	{
		DGFixedPoint x = point.x;
		DGFixedPoint y = point.y;
		point.x = m00 * x + m01 * y + m02;
		point.y = m10 * x + m11 * y + m12;
	}

	public override string ToString()
	{
		return "[" + m00 + "|" + m01 + "|" + m02 + "]\n[" + m10 + "|" + m11 + "|" + m12 + "]\n[0|0|1]";
	}
}