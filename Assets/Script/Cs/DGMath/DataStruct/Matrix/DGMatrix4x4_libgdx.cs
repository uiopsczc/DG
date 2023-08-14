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


/** Encapsulates a <a href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order">column major</a> 4 by 4 matrix. Like
 * the {@link Vector3} class it allows the chaining of methods by returning a reference to itself. For example:
 * 
 * <pre>
 * Matrix4 mat = new Matrix4().trn(position).mul(camera.combined);
 * </pre>
 * 
 * @author badlogicgames@gmail.com */
public partial struct DGMatrix4x4
{
	/** XX: Typically the unrotated X component for scaling, also the cosine of the angle when rotated on the Y and/or Z axis. On
	 * Vector3 multiplication this value is multiplied with the source X component and added to the target X component. */
	public const int M00Index = 0;

	/** XY: Typically the negative sine of the angle when rotated on the Z axis. On Vector3 multiplication this value is multiplied
	 * with the source Y component and added to the target X component. */
	public const int M10Index = 1;


	/** XZ: Typically the sine of the angle when rotated on the Y axis. On Vector3 multiplication this value is multiplied with the
	 * source Z component and added to the target X component. */
	public const int M20Index = 2;


	/** XW: Typically the translation of the X component. On Vector3 multiplication this value is added to the target X
	 * component. */
	public const int M30Index = 3;
	

	/** YX: Typically the sine of the angle when rotated on the Z axis. On Vector3 multiplication this value is multiplied with the
	 * source X component and added to the target Y component. */
	public const int M01Index = 4;

	/** YY: Typically the unrotated Y component for scaling, also the cosine of the angle when rotated on the X and/or Z axis. On
	 * Vector3 multiplication this value is multiplied with the source Y component and added to the target Y component. */
	public const int M11Index = 5;

	/** YZ: Typically the negative sine of the angle when rotated on the X axis. On Vector3 multiplication this value is multiplied
	 * with the source Z component and added to the target Y component. */
	public const int M21Index = 6;


	/** YW: Typically the translation of the Y component. On Vector3 multiplication this value is added to the target Y
	 * component. */
	public const int M31Index = 7;
	

	/** ZX: Typically the negative sine of the angle when rotated on the Y axis. On Vector3 multiplication this value is multiplied
	 * with the source X component and added to the target Z component. */
	public const int M02Index = 8;

	/** ZY: Typical the sine of the angle when rotated on the X axis. On Vector3 multiplication this value is multiplied with the
	 * source Y component and added to the target Z component. */
	public const int M12Index = 9;

	/** ZZ: Typically the unrotated Z component for scaling, also the cosine of the angle when rotated on the X and/or Y axis. On
	 * Vector3 multiplication this value is multiplied with the source Z component and added to the target Z component. */
	public const int M22Index = 10;

	/** ZW: Typically the translation of the Z component. On Vector3 multiplication this value is added to the target Z
	 * component. */
	public const int M32Index = 11;
	

	/** WX: Typically the value zero. On Vector3 multiplication this value is ignored. */
	public const int M03Index = 12;

	/** WY: Typically the value zero. On Vector3 multiplication this value is ignored. */
	public const int M13Index = 13;

	/** WZ: Typically the value zero. On Vector3 multiplication this value is ignored. */
	public const int M23Index = 14;

	/** WW: Typically the value one. On Vector3 multiplication this value is ignored. */
	public const int M33Index = 15;

	private const int Count = 16;
	static DGQuaternion quat = DGQuaternion.default2;
	static DGQuaternion quat2 = DGQuaternion.default2;
	static DGVector3 l_vez = new DGVector3();
	static DGVector3 l_vex = new DGVector3();
	static DGVector3 l_vey = new DGVector3();
	static DGVector3 tmpVec = new DGVector3();
	static DGMatrix4x4 tmpMat = DGMatrix4x4.default2;
	static DGVector3 right = new DGVector3();
	static DGVector3 tmpForward = new DGVector3();
	static DGVector3 tmpUp = new DGVector3();

	public DGFixedPoint m00;
	public DGFixedPoint m10;
	public DGFixedPoint m20;
	public DGFixedPoint m30;

	public DGFixedPoint m01;
	public DGFixedPoint m11;
	public DGFixedPoint m21;
	public DGFixedPoint m31;

	public DGFixedPoint m02;
	public DGFixedPoint m12;
	public DGFixedPoint m22;
	public DGFixedPoint m32;

	public DGFixedPoint m03;
	public DGFixedPoint m13;
	public DGFixedPoint m23;
	public DGFixedPoint m33;

	public DGFixedPoint[] val
	{
		get
		{
			var result = new DGFixedPoint[Count];
			result[M00Index] = m00;
			result[M01Index] = m01;
			result[M02Index] = m02;
			result[M03Index] = m03;

			result[M10Index] = m10;
			result[M11Index] = m11;
			result[M12Index] = m12;
			result[M13Index] = m13;

			result[M20Index] = m20;
			result[M21Index] = m21;
			result[M22Index] = m22;
			result[M23Index] = m23;

			result[M30Index] = m30;
			result[M31Index] = m31;
			result[M32Index] = m32;
			result[M33Index] = m33;

			return result;
		}
	}

	/** Constructs an identity matrix */
	public static DGMatrix4x4 default2
	{
		get
		{
			DGMatrix4x4 result = default;
			result.m00 = (DGFixedPoint)1f;
			result.m11 = (DGFixedPoint)1f;
			result.m22 = (DGFixedPoint)1f;
			result.m33 = (DGFixedPoint)1f;
			return result;
		}
	}

	/** Constructs a matrix from the given matrix.
	 * @param matrix The matrix to copy. (This matrix is not modified) */
	public DGMatrix4x4(DGMatrix4x4 matrix)
	{
	this.m00=matrix.m00;
	this.m10=matrix.m10;
	this.m20=matrix.m20;
	this.m30=matrix.m30;

	this.m01=matrix.m01;
	this.m11=matrix.m11;
	this.m21=matrix.m21;
	this.m31=matrix.m31;

	this.m02=matrix.m02;
	this.m12=matrix.m12;
	this.m22=matrix.m22;
	this.m32=matrix.m32;

	this.m03=matrix.m03;
	this.m13=matrix.m13;
	this.m23=matrix.m23;
	this.m33=matrix.m33;
}

	/** Constructs a matrix from the given float array. The array must have at least 16 elements; the first 16 will be copied.
	 * @param values The float array to copy. Remember that this matrix is in
	 *           <a href="http://en.wikipedia.org/wiki/Row-major_order">column major</a> order. (The float array is not
	 *           modified) */
	public DGMatrix4x4(DGFixedPoint[] values)
	{
		this.m00 = values[M00Index];
		this.m10 = values[M10Index];
		this.m20 = values[M20Index];
		this.m30 = values[M30Index];

		this.m01 = values[M01Index];
		this.m11 = values[M11Index];
		this.m21 = values[M21Index];
		this.m31 = values[M31Index];

		this.m02 = values[M02Index];
		this.m12 = values[M12Index];
		this.m22 = values[M22Index];
		this.m32 = values[M32Index];

		this.m03 = values[M03Index];
		this.m13 = values[M13Index];
		this.m23 = values[M23Index];
		this.m33 = values[M33Index];
	}

	/** Constructs a rotation matrix from the given {@link Quaternion}.
	 * @param quaternion The quaternion to be copied. (The quaternion is not modified) */
	public DGMatrix4x4(DGQuaternion quaternion)
	{
		DGFixedPoint translationX = (DGFixedPoint) 0;
		DGFixedPoint translationY = (DGFixedPoint) 0;
		DGFixedPoint translationZ = (DGFixedPoint) 0;
		DGFixedPoint quaternionX = quaternion.x;
		DGFixedPoint quaternionY = quaternion.y;
		DGFixedPoint quaternionZ = quaternion.z;
		DGFixedPoint quaternionW = quaternion.w;

		DGFixedPoint xs = quaternionX * (DGFixedPoint) 2f, ys = quaternionY * (DGFixedPoint) 2f, zs = quaternionZ * (DGFixedPoint) 2f;
		DGFixedPoint wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
		DGFixedPoint xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
		DGFixedPoint yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

		this.m00 = (DGFixedPoint) 1f - (yy + zz);
		this.m01 = xy - wz;
		this.m02 = xz + wy;
		this.m03 = translationX;

		this.m10 = xy + wz;
		this.m11 = (DGFixedPoint) 1f - (xx + zz);
		this.m12 = yz - wx;
		this.m13 = translationY;

		this.m20 = xz - wy;
		this.m21 = yz + wx;
		this.m22 = (DGFixedPoint) 1f - (xx + yy);
		this.m23 = translationZ;

		this.m30 = (DGFixedPoint) 0f;
		this.m31 = (DGFixedPoint) 0f;
		this.m32 = (DGFixedPoint) 0f;
		this.m33 = (DGFixedPoint) 1f;
	}

	/** Construct a matrix from the given translation, rotation and scale.
	 * @param position The translation
	 * @param rotation The rotation, must be normalized
	 * @param scale The scale */
	public DGMatrix4x4(DGVector3 position, DGQuaternion rotation, DGVector3 scale)
	{
		DGFixedPoint translationX = position.x;
		DGFixedPoint translationY = position.y;
		DGFixedPoint translationZ = position.z;
		DGFixedPoint quaternionX = rotation.x;
		DGFixedPoint quaternionY = rotation.y;
		DGFixedPoint quaternionZ = rotation.z;
		DGFixedPoint quaternionW = rotation.w;
		DGFixedPoint scaleX = scale.x;
		DGFixedPoint scaleY = scale.y;
		DGFixedPoint scaleZ = scale.z;

		DGFixedPoint xs = quaternionX * (DGFixedPoint) 2f, ys = quaternionY * (DGFixedPoint) 2f, zs = quaternionZ * (DGFixedPoint) 2f;
		DGFixedPoint wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
		DGFixedPoint xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
		DGFixedPoint yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

		this.m00 = scaleX * ((DGFixedPoint) 1.0f - (yy + zz));
		this.m01 = scaleY * (xy - wz);
		this.m02 = scaleZ * (xz + wy);
		this.m03 = translationX;

		this.m10 = scaleX * (xy + wz);
		this.m11 = scaleY * ((DGFixedPoint) 1.0f - (xx + zz));
		this.m12 = scaleZ * (yz - wx);
		this.m13 = translationY;

		this.m20 = scaleX * (xz - wy);
		this.m21 = scaleY * (yz + wx);
		this.m22 = scaleZ * ((DGFixedPoint) 1.0f - (xx + yy));
		this.m23 = translationZ;

		this.m30 = (DGFixedPoint) 0f;
		this.m31 = (DGFixedPoint) 0f;
		this.m32 = (DGFixedPoint) 0f;
		this.m33 = (DGFixedPoint) 1f;
	}

	/** Sets the matrix to the given matrix.
	 * @param matrix The matrix that is to be copied. (The given matrix is not modified)
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 set(DGMatrix4x4 matrix)
	{
		return set(matrix.val);
	}

	/** Sets the matrix to the given matrix as a float array. The float array must have at least 16 elements; the first 16 will be
	 * copied.
	 * 
	 * @param values The matrix, in float form, that is to be copied. Remember that this matrix is in
	 *           <a href="http://en.wikipedia.org/wiki/Row-major_order">column major</a> order.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 set(DGFixedPoint[] values)
	{
		this.m00 = values[M00Index];
		this.m10 = values[M10Index];
		this.m20 = values[M20Index];
		this.m30 = values[M30Index];

		this.m01 = values[M01Index];
		this.m11 = values[M11Index];
		this.m21 = values[M21Index];
		this.m31 = values[M31Index];

		this.m02 = values[M02Index];
		this.m12 = values[M12Index];
		this.m22 = values[M22Index];
		this.m32 = values[M32Index];

		this.m03 = values[M03Index];
		this.m13 = values[M13Index];
		this.m23 = values[M23Index];
		this.m33 = values[M33Index];
		return this;
	}

	/** Sets the matrix to a rotation matrix representing the quaternion.
	 * @param quaternion The quaternion that is to be used to set this matrix.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 set(DGQuaternion quaternion)
	{
		return set(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
	}

	/** Sets the matrix to a rotation matrix representing the quaternion.
	 * 
	 * @param quaternionX The X component of the quaternion that is to be used to set this matrix.
	 * @param quaternionY The Y component of the quaternion that is to be used to set this matrix.
	 * @param quaternionZ The Z component of the quaternion that is to be used to set this matrix.
	 * @param quaternionW The W component of the quaternion that is to be used to set this matrix.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 set(DGFixedPoint quaternionX, DGFixedPoint quaternionY, DGFixedPoint quaternionZ, DGFixedPoint quaternionW)
	{
		return set((DGFixedPoint) 0f, (DGFixedPoint) 0f, (DGFixedPoint) 0f, quaternionX, quaternionY, quaternionZ, quaternionW);
	}

	/** Set this matrix to the specified translation and rotation.
	 * @param position The translation
	 * @param orientation The rotation, must be normalized
	 * @return This matrix for chaining */
	public DGMatrix4x4 set(DGVector3 position, DGQuaternion orientation)
	{
		return set(position.x, position.y, position.z, orientation.x, orientation.y, orientation.z, orientation.w);
	}

	/** Sets the matrix to a rotation matrix representing the translation and quaternion.
	 * @param translationX The X component of the translation that is to be used to set this matrix.
	 * @param translationY The Y component of the translation that is to be used to set this matrix.
	 * @param translationZ The Z component of the translation that is to be used to set this matrix.
	 * @param quaternionX The X component of the quaternion that is to be used to set this matrix.
	 * @param quaternionY The Y component of the quaternion that is to be used to set this matrix.
	 * @param quaternionZ The Z component of the quaternion that is to be used to set this matrix.
	 * @param quaternionW The W component of the quaternion that is to be used to set this matrix.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 set(DGFixedPoint translationX, DGFixedPoint translationY, DGFixedPoint translationZ, DGFixedPoint quaternionX, DGFixedPoint quaternionY,
		DGFixedPoint quaternionZ, DGFixedPoint quaternionW)
	{
		DGFixedPoint xs = quaternionX * (DGFixedPoint) 2f, ys = quaternionY * (DGFixedPoint) 2f, zs = quaternionZ * (DGFixedPoint) 2f;
		DGFixedPoint wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
		DGFixedPoint xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
		DGFixedPoint yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

		this.m00 = (DGFixedPoint) 1f - (yy + zz);
		this.m01 = xy - wz;
		this.m02 = xz + wy;
		this.m03 = translationX;

		this.m10 = xy + wz;
		this.m11 = (DGFixedPoint) 1f - (xx + zz);
		this.m12 = yz - wx;
		this.m13 = translationY;

		this.m20 = xz - wy;
		this.m21 = yz + wx;
		this.m22 = (DGFixedPoint) 1f - (xx + yy);
		this.m23 = translationZ;

		this.m30 = (DGFixedPoint) 0f;
		this.m31 = (DGFixedPoint) 0f;
		this.m32 = (DGFixedPoint) 0f;
		this.m33 = (DGFixedPoint) 1f;
		return this;
	}

	/** Set this matrix to the specified translation, rotation and scale.
	 * @param position The translation
	 * @param orientation The rotation, must be normalized
	 * @param scale The scale
	 * @return This matrix for chaining */
	public DGMatrix4x4 set(DGVector3 position, DGQuaternion orientation, DGVector3 scale)
	{
		return set(position.x, position.y, position.z, orientation.x, orientation.y, orientation.z, orientation.w,
			scale.x, scale.y,
			scale.z);
	}

	/** Sets the matrix to a rotation matrix representing the translation and quaternion.
	 * @param translationX The X component of the translation that is to be used to set this matrix.
	 * @param translationY The Y component of the translation that is to be used to set this matrix.
	 * @param translationZ The Z component of the translation that is to be used to set this matrix.
	 * @param quaternionX The X component of the quaternion that is to be used to set this matrix.
	 * @param quaternionY The Y component of the quaternion that is to be used to set this matrix.
	 * @param quaternionZ The Z component of the quaternion that is to be used to set this matrix.
	 * @param quaternionW The W component of the quaternion that is to be used to set this matrix.
	 * @param scaleX The X component of the scaling that is to be used to set this matrix.
	 * @param scaleY The Y component of the scaling that is to be used to set this matrix.
	 * @param scaleZ The Z component of the scaling that is to be used to set this matrix.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 set(DGFixedPoint translationX, DGFixedPoint translationY, DGFixedPoint translationZ, DGFixedPoint quaternionX, DGFixedPoint quaternionY,
		DGFixedPoint quaternionZ, DGFixedPoint quaternionW, DGFixedPoint scaleX, DGFixedPoint scaleY, DGFixedPoint scaleZ)
	{
		DGFixedPoint xs = quaternionX * (DGFixedPoint) 2f, ys = quaternionY * (DGFixedPoint) 2f, zs = quaternionZ * (DGFixedPoint) 2f;
		DGFixedPoint wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
		DGFixedPoint xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
		DGFixedPoint yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

		this.m00 = scaleX * ((DGFixedPoint) 1.0f - (yy + zz));
		this.m01 = scaleY * (xy - wz);
		this.m02 = scaleZ * (xz + wy);
		this.m03 = translationX;

		this.m10 = scaleX * (xy + wz);
		this.m11 = scaleY * ((DGFixedPoint) 1.0f - (xx + zz));
		this.m12 = scaleZ * (yz - wx);
		this.m13 = translationY;

		this.m20 = scaleX * (xz - wy);
		this.m21 = scaleY * (yz + wx);
		this.m22 = scaleZ * ((DGFixedPoint) 1.0f - (xx + yy));
		this.m23 = translationZ;

		this.m30 = (DGFixedPoint) 0f;
		this.m31 = (DGFixedPoint) 0f;
		this.m32 = (DGFixedPoint) 0f;
		this.m33 = (DGFixedPoint) 1f;
		return this;
	}

	
	/** Sets the four columns of the matrix which correspond to the x-, y- and z-axis of the vector space this matrix creates as
	 * well as the 4th column representing the translation of any point that is multiplied by this matrix.
	 * @param xAxis The x-axis.
	 * @param yAxis The y-axis.
	 * @param zAxis The z-axis.
	 * @param pos The translation vector. */
	public DGMatrix4x4 set(DGVector3 xAxis, DGVector3 yAxis, DGVector3 zAxis, DGVector3 pos)
	{
		this.m00 = xAxis.x;
		this.m10 = xAxis.y;
		this.m20 = xAxis.z;
		this.m01 = yAxis.x;
		this.m11 = yAxis.y;
		this.m21 = yAxis.z;
		this.m02 = zAxis.x;
		this.m12 = zAxis.y;
		this.m22 = zAxis.z;
		this.m03 = pos.x;
		this.m13 = pos.y;
		this.m23 = pos.z;
		this.m30 = (DGFixedPoint) 0f;
		this.m31 = (DGFixedPoint) 0f;
		this.m32 = (DGFixedPoint) 0f;
		this.m33 = (DGFixedPoint) 1f;
		return this;
	}

	/** @return a copy of this matrix */
	public DGMatrix4x4 cpy()
	{
		return new DGMatrix4x4(this);
	}

	/** Adds a translational component to the matrix in the 4th column. The other columns are untouched.
	 * @param vector The translation vector to add to the current matrix. (This vector is not modified)
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 trn(DGVector3 vector)
	{
		this.m03 += vector.x;
		this.m13 += vector.y;
		this.m23 += vector.z;
		return this;
	}

	/** Adds a translational component to the matrix in the 4th column. The other columns are untouched.
	 * @param x The x-component of the translation vector.
	 * @param y The y-component of the translation vector.
	 * @param z The z-component of the translation vector.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 trn(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		this.m03 += x;
		this.m13 += y;
		this.m23 += z;
		return this;
	}

	/** @return the backing float array */
	public DGFixedPoint[] getValues()
	{
		return val;
	}

	/** Postmultiplies this matrix with the given matrix, storing the result in this matrix. For example:
	 * 
	 * <pre>
	 * A.mul(B) results in A := AB.
	 * </pre>
	 * 
	 * @param matrix The other matrix to multiply by.
	 * @return This matrix for the purpose of chaining operations together. */
	public DGMatrix4x4 mul(DGMatrix4x4 matrix)
	{
		mul(val, matrix.val);
		return this;
	}

	/** Premultiplies this matrix with the given matrix, storing the result in this matrix. For example:
	 * 
	 * <pre>
	 * A.mulLeft(B) results in A := BA.
	 * </pre>
	 * 
	 * @param matrix The other matrix to multiply by.
	 * @return This matrix for the purpose of chaining operations together. */
	public DGMatrix4x4 mulLeft(DGMatrix4x4 matrix)
	{
		tmpMat.set(matrix);
		mul(tmpMat.val, val);
		return set(tmpMat);
	}

	/** Transposes the matrix.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 tra()
	{
		DGFixedPoint m01 = this.m01;
		DGFixedPoint m02 = this.m02;
		DGFixedPoint m03 = this.m03;
		DGFixedPoint m12 = this.m12;
		DGFixedPoint m13 = this.m13;
		DGFixedPoint m23 = this.m23;
		this.m01 = this.m10;
		this.m02 = this.m20;
		this.m03 = this.m30;
		this.m10 = m01;
		this.m12 = this.m21;
		this.m13 = this.m31;
		this.m20 = m02;
		this.m21 = m12;
		this.m23 = this.m32;
		this.m30 = m03;
		this.m31 = m13;
		this.m32 = m23;
		return this;
	}

	/** Sets the matrix to an identity matrix.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 idt()
	{
		this.m00 = (DGFixedPoint) 1f;
		this.m01 = (DGFixedPoint) 0f;
		this.m02 = (DGFixedPoint) 0f;
		this.m03 = (DGFixedPoint) 0f;
		this.m10 = (DGFixedPoint) 0f;
		this.m11 = (DGFixedPoint) 1f;
		this.m12 = (DGFixedPoint) 0f;
		this.m13 = (DGFixedPoint) 0f;
		this.m20 = (DGFixedPoint) 0f;
		this.m21 = (DGFixedPoint) 0f;
		this.m22 = (DGFixedPoint) 1f;
		this.m23 = (DGFixedPoint) 0f;
		this.m30 = (DGFixedPoint) 0f;
		this.m31 = (DGFixedPoint) 0f;
		this.m32 = (DGFixedPoint) 0f;
		this.m33 = (DGFixedPoint) 1f;
		return this;
	}

	/** Inverts the matrix. Stores the result in this matrix.
	 * @return This matrix for the purpose of chaining methods together.
	 * @throws RuntimeException if the matrix is singular (not invertible) */
	public DGMatrix4x4 inv()
	{
		DGFixedPoint l_det = this.m30 * this.m21 * this.m12 * this.m03 - this.m20 * this.m31 * this.m12 * this.m03
		                                                     - this.m30 * this.m11 * this.m22 * this.m03 + this.m10 *
		                                                                                                 this.m31 * this.
			                                                                                                 m22 * this.
			                                                                                                 m03
		                                                                                                 + this.m20 *
		                                                                                                 this.m11 *
		                                                                                                 this.m32 * this.m03 - this.m10 *
			                                                                                                      this.m21 *
			                                                                                                      this.m32 *
			                                                                                                      this.m03
			                                                                                                      - this.m30 *
			                                                                                                      this.m21 *
			                                                                                                      this.m02 *
			                                                                                                      this.m13 + this.m20 *
				                                                                                                           this.m31 *
				                                                                                                           this.m02 *
				                                                                                                           this.m13
				                                                                                                           + this.m30 *
				                                                                                                           this.m01 *
				                                                                                                           this.m22 *
				                                                                                                           this.m13 - this.m00 *
					                                                                                                                 this.m31 *
					                                                                                                                 this.m22 *
					                                                                                                                 this.m13
					                                                                                                                 - this.m20 *
					                                                                                                                 this.m01 *
					                                                                                                                 this.m32 *
					                                                                                                                 this.m13 + this.m00 *
						                                                                                                                       this.m21 *
						                                                                                                                       this.m32 *
						                                                                                                                       this.m13
						                                                                                                                       + this.m30 *
						                                                                                                                       this.m11 *
						                                                                                                                       this.m02 *
						                                                                                                                       this.m23 - this.m10 *
							                                                                                                                             this.m31 *
							                                                                                                                             this.m02 *
							                                                                                                                             this.m23
							                                                                                                                             - this.m30 *
							                                                                                                                             this.m01 *
							                                                                                                                             this.m12 *
							                                                                                                                             this.m23 + this.m00 *
								                                                                                                                                   this.m31 *
								                                                                                                                                   this.m12 *
								                                                                                                                                   this.m23
								                                                                                                                                   + this.m10 *
								                                                                                                                                   this.m01 *
								                                                                                                                                   this.m32 *
								                                                                                                                                   this.m23 - this.m00 *
									                                                                                                                                         this.m11 *
									                                                                                                                                         this.m32 *
									                                                                                                                                         this.m23
									                                                                                                                                         - this.m20 *
									                                                                                                                                         this.m11 *
									                                                                                                                                         this.m02 *
									                                                                                                                                         this.m33 + this.m10 *
										                                                                                                                                               this.m21 *
										                                                                                                                                               this.m02 *
										                                                                                                                                               this.m33
										                                                                                                                                               +
										                                                                                                                                               this.m20 *
										                                                                                                                                               this.m01 *
										                                                                                                                                               this.m12 *
										                                                                                                                                               this.m33 - this.m00 *
											                                                                                                                                                     this.m21 *
											                                                                                                                                                     this.m12 *
											                                                                                                                                                     this.m33
											                                                                                                                                                     -
											                                                                                                                                                     this.m10 *
											                                                                                                                                                     this.m01 *
											                                                                                                                                                     this.m22 *
											                                                                                                                                                     this.m33 +
		           this.m00 * this.m11 * this.m22 * this.m33;
		if (l_det == (DGFixedPoint) 0f) throw new Exception("non-invertible matrix");
		DGFixedPoint m00 = this.m12 * this.m23 * this.m31 - this.m13 * this.m22 * this.m31 + this.m13 * this.m21 * this.m32
		         - this.m11 * this.m23 * this.m32 - this.m12 * this.m21 * this.m33 + this.m11 * this.m22 * this.m33;
		DGFixedPoint m01 = this.m03 * this.m22 * this.m31 - this.m02 * this.m23 * this.m31 - this.m03 * this.m21 * this.m32
		         + this.m01 * this.m23 * this.m32 + this.m02 * this.m21 * this.m33 - this.m01 * this.m22 * this.m33;
		DGFixedPoint m02 = this.m02 * this.m13 * this.m31 - this.m03 * this.m12 * this.m31 + this.m03 * this.m11 * this.m32
		         - this.m01 * this.m13 * this.m32 - this.m02 * this.m11 * this.m33 + this.m01 * this.m12 * this.m33;
		DGFixedPoint m03 = this.m03 * this.m12 * this.m21 - this.m02 * this.m13 * this.m21 - this.m03 * this.m11 * this.m22
		         + this.m01 * this.m13 * this.m22 + this.m02 * this.m11 * this.m23 - this.m01 * this.m12 * this.m23;
		DGFixedPoint m10 = this.m13 * this.m22 * this.m30 - this.m12 * this.m23 * this.m30 - this.m13 * this.m20 * this.m32
		         + this.m10 * this.m23 * this.m32 + this.m12 * this.m20 * this.m33 - this.m10 * this.m22 * this.m33;
		DGFixedPoint m11 = this.m02 * this.m23 * this.m30 - this.m03 * this.m22 * this.m30 + this.m03 * this.m20 * this.m32
		         - this.m00 * this.m23 * this.m32 - this.m02 * this.m20 * this.m33 + this.m00 * this.m22 * this.m33;
		DGFixedPoint m12 = this.m03 * this.m12 * this.m30 - this.m02 * this.m13 * this.m30 - this.m03 * this.m10 * this.m32
		         + this.m00 * this.m13 * this.m32 + this.m02 * this.m10 * this.m33 - this.m00 * this.m12 * this.m33;
		DGFixedPoint m13 = this.m02 * this.m13 * this.m20 - this.m03 * this.m12 * this.m20 + this.m03 * this.m10 * this.m22
		         - this.m00 * this.m13 * this.m22 - this.m02 * this.m10 * this.m23 + this.m00 * this.m12 * this.m23;
		DGFixedPoint m20 = this.m11 * this.m23 * this.m30 - this.m13 * this.m21 * this.m30 + this.m13 * this.m20 * this.m31
		         - this.m10 * this.m23 * this.m31 - this.m11 * this.m20 * this.m33 + this.m10 * this.m21 * this.m33;
		DGFixedPoint m21 = this.m03 * this.m21 * this.m30 - this.m01 * this.m23 * this.m30 - this.m03 * this.m20 * this.m31
		         + this.m00 * this.m23 * this.m31 + this.m01 * this.m20 * this.m33 - this.m00 * this.m21 * this.m33;
		DGFixedPoint m22 = this.m01 * this.m13 * this.m30 - this.m03 * this.m11 * this.m30 + this.m03 * this.m10 * this.m31
		         - this.m00 * this.m13 * this.m31 - this.m01 * this.m10 * this.m33 + this.m00 * this.m11 * this.m33;
		DGFixedPoint m23 = this.m03 * this.m11 * this.m20 - this.m01 * this.m13 * this.m20 - this.m03 * this.m10 * this.m21
		         + this.m00 * this.m13 * this.m21 + this.m01 * this.m10 * this.m23 - this.m00 * this.m11 * this.m23;
		DGFixedPoint m30 = this.m12 * this.m21 * this.m30 - this.m11 * this.m22 * this.m30 - this.m12 * this.m20 * this.m31
		         + this.m10 * this.m22 * this.m31 + this.m11 * this.m20 * this.m32 - this.m10 * this.m21 * this.m32;
		DGFixedPoint m31 = this.m01 * this.m22 * this.m30 - this.m02 * this.m21 * this.m30 + this.m02 * this.m20 * this.m31
		         - this.m00 * this.m22 * this.m31 - this.m01 * this.m20 * this.m32 + this.m00 * this.m21 * this.m32;
		DGFixedPoint m32 = this.m02 * this.m11 * this.m30 - this.m01 * this.m12 * this.m30 - this.m02 * this.m10 * this.m31
		         + this.m00 * this.m12 * this.m31 + this.m01 * this.m10 * this.m32 - this.m00 * this.m11 * this.m32;
		DGFixedPoint m33 = this.m01 * this.m12 * this.m20 - this.m02 * this.m11 * this.m20 + this.m02 * this.m10 * this.m21
		         - this.m00 * this.m12 * this.m21 - this.m01 * this.m10 * this.m22 + this.m00 * this.m11 * this.m22;
		DGFixedPoint inv_det = (DGFixedPoint) 1.0f / l_det;
		this.m00 = m00 * inv_det;
		this.m10 = m10 * inv_det;
		this.m20 = m20 * inv_det;
		this.m30 = m30 * inv_det;
		this.m01 = m01 * inv_det;
		this.m11 = m11 * inv_det;
		this.m21 = m21 * inv_det;
		this.m31 = m31 * inv_det;
		this.m02 = m02 * inv_det;
		this.m12 = m12 * inv_det;
		this.m22 = m22 * inv_det;
		this.m32 = m32 * inv_det;
		this.m03 = m03 * inv_det;
		this.m13 = m13 * inv_det;
		this.m23 = m23 * inv_det;
		this.m33 = m33 * inv_det;
		return this;
	}

	/** @return The determinant of this matrix */
	public DGFixedPoint det()
	{
		return this.m30 * this.m21 * this.m12 * this.m03 - this.m20 * this.m31 * this.m12 * this.m03
		                                                 - this.m30 * this.m11 * this.m22 * this.m03 + this.m10 *
		                                                                                             this.m31 *
		                                                                                             this.m22 * this.m03
		                                                                                             + this.m20 *
		                                                                                             this.m11 *
		                                                                                             this.m32 *
		                                                                                             this.m03 - this.m10 *
			                                                                                                  this.m21 *
			                                                                                                  this.m32 *
			                                                                                                  this.m03
			                                                                                                  - this.m30 *
			                                                                                                  this.m21 *
			                                                                                                  this.m02 *
		                                                                                                      this.m13 + this.m20 *
		       this.m31 *
		       this.m02 *
																													   this.m13
																													   + this.m30 *
		                                                                                                               this.m01 *
		                                                                                                               this.m22 *
		                                                                                                               this.m13 - this.m00 *
		       this.m31 *
		       this.m22 *
																																 this.m13
																																 - this.m20 *
		                                                                                                                        this.m01 *
		                                                                                                                        this.m32 *
		                                                                                                                        this.m13 + this.m00 *
		       this.m21 *
		       this.m32 *
																																		   this.m13
																																		   + this.m30 *
		                                                                                                                                 this.m11 *
		                                                                                                                                 this.m02 *
		                                                                                                                                 this.m23 - this.m10 *
		       this.m31 *
		       this.m02 *
																																					 this.m23
																																					 - this.m30 *
		                                                                                                                                          this.m01 *
		                                                                                                                                          this.m12 *
		                                                                                                                                          this.m23 + this.m00 *
		       this.m31 *
		       this.m12 *
																																							   this.m23
																																							   + this.m10 *
		                                                                                                                                                   this.m01 *
		                                                                                                                                                   this.m32 *
		                                                                                                                                                   this.m23 - this.m00 *
		       this.m11 *
		       this.m32 *
																																										 this.m23
																																										 - this.m20 *
		                                                                                                                                                            this.m11 *
		                                                                                                                                                            this.m02 *
		                                                                                                                                                            this.m33 + this.m10 *
		       this.m21 *
		       this.m02 *
																																												   this.m33
																																												   +
		                                                                                                                                                                     this.m20 *
		                                                                                                                                                                     this.m01 *
		                                                                                                                                                                     this.m12 *
		                                                                                                                                                                     this.m33 - this.m00 *
		       this.m21 *
		       this.m12 *
																																															 this.m33
																																															 -
		                                                                                                                                                                              this.m10 *
		                                                                                                                                                                              this.m01 *
		                                                                                                                                                                              this.m22 *
		                                                                                                                                                                              this.m33 +
		       this.m00 * this.m11 * this.m22 * this.m33;
	}

	/** @return The determinant of the 3x3 upper left matrix */
	public DGFixedPoint det3x3()
	{
		return this.m00 * this.m11 * this.m22 + this.m01 * this.m12 * this.m20 + this.m02 * this.m10 * this.m21
		       - this.m00 * this.m12 * this.m21 - this.m01 * this.m10 * this.m22 - this.m02 * this.m11 * this.m20;
	}

	//http://gsteph.blogspot.com/2012/05/world-view-and-projection-matrix.html
	/** Sets the matrix to a projection matrix with a near- and far plane, a field of view in degrees and an aspect ratio. Note
	 * that the field of view specified is the angle in degrees for the height, the field of view for the width will be calculated
	 * according to the aspect ratio.
	 * @param near The near plane
	 * @param far The far plane
	 * @param fovy The field of view of the height in degrees
	 * @param aspectRatio The "width over height" aspect ratio
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToProjection(DGFixedPoint near, DGFixedPoint far, DGFixedPoint fovy, DGFixedPoint aspectRatio)
	{
		idt();
		DGFixedPoint l_fd = (DGFixedPoint) 1.0 / DGMath.Tan((fovy * (DGMath.PI / (DGFixedPoint) 180)) / (DGFixedPoint) 2.0);
		DGFixedPoint l_a1 = (far + near) / (near - far);
		DGFixedPoint l_a2 = ((DGFixedPoint) 2 * far * near) / (near - far);
		this.m00 = l_fd / aspectRatio;
		this.m10 = (DGFixedPoint) 0;
		this.m20 = (DGFixedPoint) 0;
		this.m30 = (DGFixedPoint) 0;
		this.m01 = (DGFixedPoint) 0;
		this.m11 = l_fd;
		this.m21 = (DGFixedPoint) 0;
		this.m31 = (DGFixedPoint) 0;
		this.m02 = (DGFixedPoint) 0;
		this.m12 = (DGFixedPoint) 0;
		this.m22 = l_a1;
		this.m32 = (DGFixedPoint) (-1);
		this.m03 = (DGFixedPoint) 0;
		this.m13 = (DGFixedPoint) 0;
		this.m23 = l_a2;
		this.m33 = (DGFixedPoint) 0;
		return this;
	}

	/** Sets the matrix to a projection matrix with a near/far plane, and left, bottom, right and top specifying the points on the
	 * near plane that are mapped to the lower left and upper right corners of the viewport. This allows to create projection
	 * matrix with off-center vanishing point.
	 * @param left
	 * @param right
	 * @param bottom
	 * @param top
	 * @param near The near plane
	 * @param far The far plane
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToProjection(DGFixedPoint left, DGFixedPoint right, DGFixedPoint bottom, DGFixedPoint top, DGFixedPoint near, DGFixedPoint far)
	{
		DGFixedPoint x = (DGFixedPoint) 2.0f * near / (right - left);
		DGFixedPoint y = (DGFixedPoint) 2.0f * near / (top - bottom);
		DGFixedPoint a = (right + left) / (right - left);
		DGFixedPoint b = (top + bottom) / (top - bottom);
		DGFixedPoint l_a1 = (far + near) / (near - far);
		DGFixedPoint l_a2 = ((DGFixedPoint) 2 * far * near) / (near - far);
		this.m00 = x;
		this.m10 = (DGFixedPoint) 0;
		this.m20 = (DGFixedPoint) 0;
		this.m30 = (DGFixedPoint) 0;
		this.m01 = (DGFixedPoint) 0;
		this.m11 = y;
		this.m21 = (DGFixedPoint) 0;
		this.m31 = (DGFixedPoint) 0;
		this.m02 = a;
		this.m12 = b;
		this.m22 = l_a1;
		this.m32 = (DGFixedPoint) (-1);
		this.m03 = (DGFixedPoint) 0;
		this.m13 = (DGFixedPoint) 0;
		this.m23 = l_a2;
		this.m33 = (DGFixedPoint) 0;
		return this;
	}

	/** Sets this matrix to an orthographic projection matrix with the origin at (x,y) extending by width and height. The near
	 * plane is set to 0, the far plane is set to 1.
	 * @param x The x-coordinate of the origin
	 * @param y The y-coordinate of the origin
	 * @param width The width
	 * @param height The height
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToOrtho2D(DGFixedPoint x, DGFixedPoint y, DGFixedPoint width, DGFixedPoint height)
	{
		setToOrtho(x, x + width, y, y + height, (DGFixedPoint) 0, (DGFixedPoint) 1);
		return this;
	}

	/** Sets this matrix to an orthographic projection matrix with the origin at (x,y) extending by width and height, having a near
	 * and far plane.
	 * @param x The x-coordinate of the origin
	 * @param y The y-coordinate of the origin
	 * @param width The width
	 * @param height The height
	 * @param near The near plane
	 * @param far The far plane
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToOrtho2D(DGFixedPoint x, DGFixedPoint y, DGFixedPoint width, DGFixedPoint height, DGFixedPoint near, DGFixedPoint far)
	{
		setToOrtho(x, x + width, y, y + height, near, far);
		return this;
	}

	/** Sets the matrix to an orthographic projection like glOrtho (http://www.opengl.org/sdk/docs/man/xhtml/glOrtho.xml) following
	 * the OpenGL equivalent
	 * @param left The left clipping plane
	 * @param right The right clipping plane
	 * @param bottom The bottom clipping plane
	 * @param top The top clipping plane
	 * @param near The near clipping plane
	 * @param far The far clipping plane
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToOrtho(DGFixedPoint left, DGFixedPoint right, DGFixedPoint bottom, DGFixedPoint top, DGFixedPoint near, DGFixedPoint far)
	{
		DGFixedPoint x_orth = (DGFixedPoint) 2 / (right - left);
		DGFixedPoint y_orth = (DGFixedPoint) 2 / (top - bottom);
		DGFixedPoint z_orth = (DGFixedPoint) (-2) / (far - near);

		DGFixedPoint tx = -(right + left) / (right - left);
		DGFixedPoint ty = -(top + bottom) / (top - bottom);
		DGFixedPoint tz = -(far + near) / (far - near);

		this.m00 = x_orth;
		this.m10 = (DGFixedPoint) 0;
		this.m20 = (DGFixedPoint) 0;
		this.m30 = (DGFixedPoint) 0;
		this.m01 = (DGFixedPoint) 0;
		this.m11 = y_orth;
		this.m21 = (DGFixedPoint) 0;
		this.m31 = (DGFixedPoint) 0;
		this.m02 = (DGFixedPoint) 0;
		this.m12 = (DGFixedPoint) 0;
		this.m22 = z_orth;
		this.m32 = (DGFixedPoint) 0;
		this.m03 = tx;
		this.m13 = ty;
		this.m23 = tz;
		this.m33 = (DGFixedPoint) 1;
		return this;
	}

	/** Sets the 4th column to the translation vector.
	 * @param vector The translation vector
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setTranslation(DGVector3 vector)
	{
		this.m03 = vector.x;
		this.m13 = vector.y;
		this.m23 = vector.z;
		return this;
	}

	/** Sets the 4th column to the translation vector.
	 * @param x The X coordinate of the translation vector
	 * @param y The Y coordinate of the translation vector
	 * @param z The Z coordinate of the translation vector
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setTranslation(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		this.m03 = x;
		this.m13 = y;
		this.m23 = z;
		return this;
	}

	/** Sets this matrix to a translation matrix, overwriting it first by an identity matrix and then setting the 4th column to the
	 * translation vector.
	 * @param vector The translation vector
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToTranslation(DGVector3 vector)
	{
		idt();
		this.m03 = vector.x;
		this.m13 = vector.y;
		this.m23 = vector.z;
		return this;
	}

	/** Sets this matrix to a translation matrix, overwriting it first by an identity matrix and then setting the 4th column to the
	 * translation vector.
	 * @param x The x-component of the translation vector.
	 * @param y The y-component of the translation vector.
	 * @param z The z-component of the translation vector.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToTranslation(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		idt();
		this.m03 = x;
		this.m13 = y;
		this.m23 = z;
		return this;
	}

	/** Sets this matrix to a translation and scaling matrix by first overwriting it with an identity and then setting the
	 * translation vector in the 4th column and the scaling vector in the diagonal.
	 * @param translation The translation vector
	 * @param scaling The scaling vector
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToTranslationAndScaling(DGVector3 translation, DGVector3 scaling)
	{
		idt();
		this.m03 = translation.x;
		this.m13 = translation.y;
		this.m23 = translation.z;
		this.m00 = scaling.x;
		this.m11 = scaling.y;
		this.m22 = scaling.z;
		return this;
	}

	/** Sets this matrix to a translation and scaling matrix by first overwriting it with an identity and then setting the
	 * translation vector in the 4th column and the scaling vector in the diagonal.
	 * @param translationX The x-component of the translation vector
	 * @param translationY The y-component of the translation vector
	 * @param translationZ The z-component of the translation vector
	 * @param scalingX The x-component of the scaling vector
	 * @param scalingY The x-component of the scaling vector
	 * @param scalingZ The x-component of the scaling vector
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToTranslationAndScaling(DGFixedPoint translationX, DGFixedPoint translationY, DGFixedPoint translationZ, DGFixedPoint scalingX,
		DGFixedPoint scalingY, DGFixedPoint scalingZ)
	{
		idt();
		this.m03 = translationX;
		this.m13 = translationY;
		this.m23 = translationZ;
		this.m00 = scalingX;
		this.m11 = scalingY;
		this.m22 = scalingZ;
		return this;
	}

	/** Sets the matrix to a rotation matrix around the given axis.
	 * @param axis The axis
	 * @param degrees The angle in degrees
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToRotation(DGVector3 axis, DGFixedPoint degrees)
	{
		if (degrees == (DGFixedPoint) 0)
		{
			idt();
			return this;
		}

		return set(quat.set(axis, degrees));
	}

	/** Sets the matrix to a rotation matrix around the given axis.
	 * @param axis The axis
	 * @param radians The angle in radians
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToRotationRad(DGVector3 axis, DGFixedPoint radians)
	{
		if (radians == (DGFixedPoint) 0)
		{
			idt();
			return this;
		}

		return set(quat.setFromAxisRad(axis, radians));
	}

	/** Sets the matrix to a rotation matrix around the given axis.
	 * @param axisX The x-component of the axis
	 * @param axisY The y-component of the axis
	 * @param axisZ The z-component of the axis
	 * @param degrees The angle in degrees
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToRotation(DGFixedPoint axisX, DGFixedPoint axisY, DGFixedPoint axisZ, DGFixedPoint degrees)
	{
		if (degrees == (DGFixedPoint) 0)
		{
			idt();
			return this;
		}

		return set(quat.setFromAxis(axisX, axisY, axisZ, degrees));
	}

	/** Sets the matrix to a rotation matrix around the given axis.
	 * @param axisX The x-component of the axis
	 * @param axisY The y-component of the axis
	 * @param axisZ The z-component of the axis
	 * @param radians The angle in radians
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToRotationRad(DGFixedPoint axisX, DGFixedPoint axisY, DGFixedPoint axisZ, DGFixedPoint radians)
	{
		if (radians == (DGFixedPoint) 0)
		{
			idt();
			return this;
		}

		return set(quat.setFromAxisRad(axisX, axisY, axisZ, radians));
	}

	/** Set the matrix to a rotation matrix between two vectors.
	 * @param v1 The base vector
	 * @param v2 The target vector
	 * @return This matrix for the purpose of chaining methods together */
	public DGMatrix4x4 setToRotation(DGVector3 v1, DGVector3 v2)
	{
		return set(quat.setFromCross(v1, v2));
	}

	/** Set the matrix to a rotation matrix between two vectors.
	 * @param x1 The base vectors x value
	 * @param y1 The base vectors y value
	 * @param z1 The base vectors z value
	 * @param x2 The target vector x value
	 * @param y2 The target vector y value
	 * @param z2 The target vector z value
	 * @return This matrix for the purpose of chaining methods together */
	public DGMatrix4x4 setToRotation(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint z1, DGFixedPoint x2, DGFixedPoint y2, DGFixedPoint z2)
	{
		return set(quat.setFromCross(x1, y1, z1, x2, y2, z2));
	}

	/** Sets this matrix to a rotation matrix from the given euler angles.
	 * @param yaw the yaw in degrees
	 * @param pitch the pitch in degrees
	 * @param roll the roll in degrees
	 * @return This matrix */
	public DGMatrix4x4 setFromEulerAngles(DGFixedPoint yaw, DGFixedPoint pitch, DGFixedPoint roll)
	{
		quat.setEulerAngles(yaw, pitch, roll);
		return set(quat);
	}

	/** Sets this matrix to a rotation matrix from the given euler angles.
	 * @param yaw the yaw in radians
	 * @param pitch the pitch in radians
	 * @param roll the roll in radians
	 * @return This matrix */
	public DGMatrix4x4 setFromEulerAnglesRad(DGFixedPoint yaw, DGFixedPoint pitch, DGFixedPoint roll)
	{
		quat.setEulerAnglesRad(yaw, pitch, roll);
		return set(quat);
	}

	/** Sets this matrix to a scaling matrix
	 * @param vector The scaling vector
	 * @return This matrix for chaining. */
	public DGMatrix4x4 setToScaling(DGVector3 vector)
	{
		idt();
		this.m00 = vector.x;
		this.m11 = vector.y;
		this.m22 = vector.z;
		return this;
	}

	/** Sets this matrix to a scaling matrix
	 * @param x The x-component of the scaling vector
	 * @param y The y-component of the scaling vector
	 * @param z The z-component of the scaling vector
	 * @return This matrix for chaining. */
	public DGMatrix4x4 setToScaling(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		idt();
		this.m00 = x;
		this.m11 = y;
		this.m22 = z;
		return this;
	}

	/** Sets the matrix to a look at matrix with a direction and an up vector. Multiply with a translation matrix to get a camera
	 * model view matrix.
	 * @param direction The direction vector
	 * @param up The up vector
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToLookAt(DGVector3 direction, DGVector3 up)
	{
		l_vez = l_vez.set(direction).nor();
		l_vex = -l_vex.set(l_vez).crs(up).nor();
		l_vey = -l_vey.set(l_vex).crs(l_vez).nor();
		idt();
		this.m00 = l_vex.x;
		this.m10 = l_vex.y;
		this.m20 = l_vex.z;
		this.m01 = l_vey.x;
		this.m11 = l_vey.y;
		this.m21 = l_vey.z;
		this.m02 = l_vez.x;
		this.m12 = l_vez.y;
		this.m22 = l_vez.z;
		return this;
	}

	/** Sets this matrix to a look at matrix with the given position, target and up vector.
	 * @param position the position
	 * @param target the target
	 * @param up the up vector
	 * @return This matrix */
	public DGMatrix4x4 setToLookAt(DGVector3 position, DGVector3 target, DGVector3 up)
	{
		tmpVec = tmpVec.set(target).sub(position);
		setToLookAt(tmpVec, up);
		mulLeft(tmpMat.setToTranslation(position.x, position.y, position.z));
		return this;
	}

	//http://gsteph.blogspot.com/2012/05/world-view-and-projection-matrix.html
	public DGMatrix4x4 setToWorld(DGVector3 position, DGVector3 forward, DGVector3 up)
	{
//		UnityEngine.Debug.LogWarning($"forword:{forward.x}  {forward.y}  {forward.z} position:{position.x} {position.y} {position.z}");
		tmpForward = tmpForward.set(forward).nor();
//		UnityEngine.Debug.LogWarning(tmpForward);
		right = -right.set(tmpForward).crs(up).nor();
//		UnityEngine.Debug.LogWarning(right);
		tmpUp = -tmpUp.set(right).crs(tmpForward).nor();
		//		UnityEngine.Debug.LogWarning($"right:{right.x}  {right.y}  {right.z} tmpUp:{tmpUp.x}  {tmpUp.y}  {tmpUp.z} tmpForward:{tmpForward.x}  {tmpForward.y}  {tmpForward.z}");
		//		set(right, tmpUp, tmpForward.scl(-(DGFixedPoint) 1), position);
		set(right, tmpUp, tmpForward, position);
		return this;
	}

	/** Linearly interpolates between this matrix and the given matrix mixing by alpha
	 * @param matrix the matrix
	 * @param alpha the alpha value in the range [0,1]
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 lerp(DGMatrix4x4 matrix, DGFixedPoint alpha)
	{
		this.m00 = this.m00 * ((DGFixedPoint) 1 - alpha) + matrix.m00 * alpha;
		this.m10 = this.m10 * ((DGFixedPoint) 1 - alpha) + matrix.m10 * alpha;
		this.m20 = this.m20 * ((DGFixedPoint) 1 - alpha) + matrix.m20 * alpha;
		this.m30 = this.m30 * ((DGFixedPoint) 1 - alpha) + matrix.m30 * alpha;

		this.m01 = this.m01 * ((DGFixedPoint) 1 - alpha) + matrix.m01 * alpha;
		this.m11 = this.m11 * ((DGFixedPoint) 1 - alpha) + matrix.m11 * alpha;
		this.m21 = this.m21 * ((DGFixedPoint) 1 - alpha) + matrix.m21 * alpha;
		this.m31 = this.m31 * ((DGFixedPoint) 1 - alpha) + matrix.m31 * alpha;

		this.m02 = this.m02 * ((DGFixedPoint) 1 - alpha) + matrix.m02 * alpha;
		this.m12 = this.m12 * ((DGFixedPoint) 1 - alpha) + matrix.m12 * alpha;
		this.m22 = this.m22 * ((DGFixedPoint) 1 - alpha) + matrix.m22 * alpha;
		this.m32 = this.m32 * ((DGFixedPoint) 1 - alpha) + matrix.m32 * alpha;

		this.m03 = this.m03 * ((DGFixedPoint) 1 - alpha) + matrix.m03 * alpha;
		this.m13 = this.m13 * ((DGFixedPoint) 1 - alpha) + matrix.m13 * alpha;
		this.m23 = this.m23 * ((DGFixedPoint) 1 - alpha) + matrix.m23 * alpha;
		this.m33 = this.m33 * ((DGFixedPoint)1 - alpha) + matrix.m33 * alpha;

		return this;
	}

	/** Averages the given transform with this one and stores the result in this matrix. Translations and scales are lerped while
	 * rotations are slerped.
	 * @param other The other transform
	 * @param w Weight of this transform; weight of the other transform is (1 - w)
	 * @return This matrix for chaining */
	public DGMatrix4x4 avg(DGMatrix4x4 other, DGFixedPoint w)
	{
		getScale(ref tmpVec);
		other.getScale(ref tmpForward);

		getRotation(ref quat);
		other.getRotation(ref quat2);

		getTranslation(ref tmpUp);
		other.getTranslation(ref right);

		setToScaling(tmpVec.scl(w).add(tmpForward.scl((DGFixedPoint) 1 - w)));
		rotate(quat.slerp(quat2, (DGFixedPoint) 1 - w));
		setTranslation(tmpUp.scl(w).add(right.scl((DGFixedPoint) 1 - w)));
		return this;
	}

	/** Averages the given transforms and stores the result in this matrix. Translations and scales are lerped while rotations are
	 * slerped. Does not destroy the data contained in t.
	 * @param t List of transforms
	 * @return This matrix for chaining */
	public DGMatrix4x4 avg(DGMatrix4x4[] t)
	{
		DGFixedPoint w = (DGFixedPoint) 1.0f / (DGFixedPoint) t.Length;

		tmpVec.set(t[0].getScale(ref tmpUp).scl(w));
		quat.set(t[0].getRotation(ref quat2).exp(w));
		tmpForward.set(t[0].getTranslation(ref tmpUp).scl(w));

		for (int i = 1; i < t.Length; i++)
		{
			tmpVec.add(t[i].getScale(ref tmpUp).scl(w));
			quat.mul(t[i].getRotation(ref quat2).exp(w));
			tmpForward.add(t[i].getTranslation(ref tmpUp).scl(w));
		}

		quat.nor();

		setToScaling(tmpVec);
		rotate(quat);
		setTranslation(tmpForward);
		return this;
	}

	/** Averages the given transforms with the given weights and stores the result in this matrix. Translations and scales are
	 * lerped while rotations are slerped. Does not destroy the data contained in t or w; Sum of w_i must be equal to 1, or
	 * unexpected results will occur.
	 * @param t List of transforms
	 * @param w List of weights
	 * @return This matrix for chaining */
	public DGMatrix4x4 avg(DGMatrix4x4[] t, DGFixedPoint[] w)
	{
		tmpVec.set(t[0].getScale(ref tmpUp).scl(w[0]));
		quat.set(t[0].getRotation(ref quat2).exp(w[0]));
		tmpForward.set(t[0].getTranslation(ref tmpUp).scl(w[0]));

		for (int i = 1; i < t.Length; i++)
		{
			tmpVec.add(t[i].getScale(ref tmpUp).scl(w[i]));
			quat.mul(t[i].getRotation(ref quat2).exp(w[i]));
			tmpForward.add(t[i].getTranslation(ref tmpUp).scl(w[i]));
		}

		quat.nor();

		setToScaling(tmpVec);
		rotate(quat);
		setTranslation(tmpForward);
		return this;
	}

	/** Sets this matrix to the given 3x3 matrix. The third column of this matrix is set to (0,0,1,0).
	 * @param mat the matrix */
	public DGMatrix4x4 set(DGMatrix3x3 mat)
	{
		val[0] = mat.val[0];
		val[1] = mat.val[1];
		val[2] = mat.val[2];
		val[3] = (DGFixedPoint) 0;
		val[4] = mat.val[3];
		val[5] = mat.val[4];
		val[6] = mat.val[5];
		val[7] = (DGFixedPoint) 0;
		val[8] = (DGFixedPoint) 0;
		val[9] = (DGFixedPoint) 0;
		val[10] = (DGFixedPoint) 1;
		val[11] = (DGFixedPoint) 0;
		val[12] = mat.val[6];
		val[13] = mat.val[7];
		val[14] = (DGFixedPoint) 0;
		val[15] = mat.val[8];
		return this;
	}

	/** Sets this matrix to the given affine matrix. The values are mapped as follows:
	 *
	 * <pre>
	 *      [  M00  M01   0   M02  ]
	 *      [  M10  M11   0   M12  ]
	 *      [   0    0    1    0   ]
	 *      [   0    0    0    1   ]
	 * </pre>
	 * 
	 * @param affine the affine matrix
	 * @return This matrix for chaining */
	public DGMatrix4x4 set(DGAffine2 affine)
	{
		this.m00 = affine.m00;
		this.m10 = affine.m10;
		this.m20 = (DGFixedPoint) 0;
		this.m30 = (DGFixedPoint) 0;
		this.m01 = affine.m01;
		this.m11 = affine.m11;
		this.m21 = (DGFixedPoint) 0;
		this.m31 = (DGFixedPoint) 0;
		this.m02 = (DGFixedPoint) 0;
		this.m12 = (DGFixedPoint) 0;
		this.m22 = (DGFixedPoint) 1;
		this.m32 = (DGFixedPoint) 0;
		this.m03 = affine.m02;
		this.m13 = affine.m12;
		this.m23 = (DGFixedPoint) 0;
		this.m33 = (DGFixedPoint) 1;
		return this;
	}

	/** Assumes that this matrix is a 2D affine transformation, copying only the relevant components. The values are mapped as
	 * follows:
	 *
	 * <pre>
	 *      [  M00  M01   _   M02  ]
	 *      [  M10  M11   _   M12  ]
	 *      [   _    _    _    _   ]
	 *      [   _    _    _    _   ]
	 * </pre>
	 * 
	 * @param affine the source matrix
	 * @return This matrix for chaining */
	public DGMatrix4x4 setAsAffine(DGAffine2 affine)
	{
		this.m00 = affine.m00;
		this.m10 = affine.m10;
		this.m01 = affine.m01;
		this.m11 = affine.m11;
		this.m03 = affine.m02;
		this.m13 = affine.m12;
		return this;
	}

	/** Assumes that both matrices are 2D affine transformations, copying only the relevant components. The copied values are:
	 *
	 * <pre>
	 *      [  M00  M01   _   M03  ]
	 *      [  M10  M11   _   M13  ]
	 *      [   _    _    _    _   ]
	 *      [   _    _    _    _   ]
	 * </pre>
	 * 
	 * @param mat the source matrix
	 * @return This matrix for chaining */
	public DGMatrix4x4 setAsAffine(DGMatrix4x4 mat)
	{
		this.m00 = mat.m00;
		this.m10 = mat.m10;
		this.m01 = mat.m01;
		this.m11 = mat.m11;
		this.m03 = mat.m03;
		this.m13 = mat.m13;
		return this;
	}

	public DGMatrix4x4 scl(DGVector3 scale)
	{
		this.m00 *= scale.x;
		this.m11 *= scale.y;
		this.m22 *= scale.z;
		return this;
	}

	public DGMatrix4x4 scl(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		this.m00 *= x;
		this.m11 *= y;
		this.m22 *= z;
		return this;
	}

	public DGMatrix4x4 scl(DGFixedPoint scale)
	{
		this.m00 *= scale;
		this.m11 *= scale;
		this.m22 *= scale;
		return this;
	}

	public DGVector3 getTranslation(ref DGVector3 position)
	{
		position.x = this.m03;
		position.y = this.m13;
		position.z = this.m23;
		return position;
	}

	/** Gets the rotation of this matrix.
	 * @param rotation The {@link Quaternion} to receive the rotation
	 * @param normalizeAxes True to normalize the axes, necessary when the matrix might also include scaling.
	 * @return The provided {@link Quaternion} for chaining. */
	public DGQuaternion getRotation(ref DGQuaternion rotation, bool normalizeAxes)
	{
		return rotation.setFromMatrix(normalizeAxes, this);
	}

	/** Gets the rotation of this matrix.
	 * @param rotation The {@link Quaternion} to receive the rotation
	 * @return The provided {@link Quaternion} for chaining. */
	public DGQuaternion getRotation(ref DGQuaternion rotation)
	{
		return rotation.setFromMatrix(this);
	}

	/** @return the squared scale factor on the X axis */
	public DGFixedPoint getScaleXSquared()
	{
		return this.m00 * this.m00 +this.m10 *this.m10 +this.m20 *this.m20;
	}

	/** @return the squared scale factor on the Y axis */
	public DGFixedPoint getScaleYSquared()
	{
		return this.m01 *this.m01 +this.m11 *this.m11 +this.m21 *this.m21;
	}

	/** @return the squared scale factor on the Z axis */
	public DGFixedPoint getScaleZSquared()
	{
		return this.m02 *this.m02 +this.m12 *this.m12 +this.m22 *this.m22;
	}

	/** @return the scale factor on the X axis (non-negative) */
	public DGFixedPoint getScaleX()
	{
//		UnityEngine.Debug.LogWarning((DGMath.IsZero(val[M01]) && DGMath.IsZero(val[M02]))
//			? DGMath.Abs(val[M00])
//			: DGMath.Sqrt(getScaleXSquared()));
		return (DGMath.IsZero(this.m10) && DGMath.IsZero(this.m20))
			? DGMath.Abs(this.m00)
			: DGMath.Sqrt(getScaleXSquared());
	}

	/** @return the scale factor on the Y axis (non-negative) */
	public DGFixedPoint getScaleY()
	{
		return (DGMath.IsZero(this.m01) && DGMath.IsZero(this.m21))
			? DGMath.Abs(this.m11)
			: DGMath.Sqrt(getScaleYSquared());
	}

	/** @return the scale factor on the X axis (non-negative) */
	public DGFixedPoint getScaleZ()
	{
		return (DGMath.IsZero(this.m02) && DGMath.IsZero(this.m12))
			? DGMath.Abs(this.m22)
			: DGMath.Sqrt(getScaleZSquared());
	}

	/** @param scale The vector which will receive the (non-negative) scale components on each axis.
	 * @return The provided vector for chaining. */
	public DGVector3 getScale(ref DGVector3 scale)
	{
		return scale.set(getScaleX(), getScaleY(), getScaleZ());
	}

	/** removes the translational part and transposes the matrix. */
	public DGMatrix4x4 toNormalMatrix()
	{
		this.m03 = (DGFixedPoint) 0;
		this.m13 = (DGFixedPoint) 0;
		this.m23 = (DGFixedPoint) 0;
		return inv().tra();
	}

	public override string ToString()
	{
		return "[" + this.m00 + "|" + this.m01 + "|" + this.m02 + "|" + this.m03 + "]\n" //
		       + "[" + this.m10 + "|" + this.m11 + "|" + this.m12 + "|" + this.m13 + "]\n" //
		       + "[" + this.m20 + "|" + this.m21 + "|" + this.m22 + "|" + this.m23 + "]\n" //
		       + "[" + this.m30 + "|" + this.m31 + "|" + this.m32 + "|" + this.m33 + "]\n";
	}

	// @off
	/*JNI
	#include <memory.h>
	#include <stdio.h>
	#include <string.h>
	
	#define M00 0
	#define M01 4
	#define M02 8
	#define M03 12
	#define M10 1
	#define M11 5
	#define M12 9
	#define M13 13
	#define M20 2
	#define M21 6
	#define M22 10
	#define M23 14
	#define M30 3
	#define M31 7
	#define M32 11
	#define M33 15
	
	static inline void matrix4_mul(float* mata, float* matb) {
		float tmp[16];
		tmp[M00] = mata[M00] * matb[M00] + mata[M01] * matb[M10] + mata[M02] * matb[M20] + mata[M03] * matb[M30];
		tmp[M01] = mata[M00] * matb[M01] + mata[M01] * matb[M11] + mata[M02] * matb[M21] + mata[M03] * matb[M31];
		tmp[M02] = mata[M00] * matb[M02] + mata[M01] * matb[M12] + mata[M02] * matb[M22] + mata[M03] * matb[M32];
		tmp[M03] = mata[M00] * matb[M03] + mata[M01] * matb[M13] + mata[M02] * matb[M23] + mata[M03] * matb[M33];
		tmp[M10] = mata[M10] * matb[M00] + mata[M11] * matb[M10] + mata[M12] * matb[M20] + mata[M13] * matb[M30];
		tmp[M11] = mata[M10] * matb[M01] + mata[M11] * matb[M11] + mata[M12] * matb[M21] + mata[M13] * matb[M31];
		tmp[M12] = mata[M10] * matb[M02] + mata[M11] * matb[M12] + mata[M12] * matb[M22] + mata[M13] * matb[M32];
		tmp[M13] = mata[M10] * matb[M03] + mata[M11] * matb[M13] + mata[M12] * matb[M23] + mata[M13] * matb[M33];
		tmp[M20] = mata[M20] * matb[M00] + mata[M21] * matb[M10] + mata[M22] * matb[M20] + mata[M23] * matb[M30];
		tmp[M21] = mata[M20] * matb[M01] + mata[M21] * matb[M11] + mata[M22] * matb[M21] + mata[M23] * matb[M31];
		tmp[M22] = mata[M20] * matb[M02] + mata[M21] * matb[M12] + mata[M22] * matb[M22] + mata[M23] * matb[M32];
		tmp[M23] = mata[M20] * matb[M03] + mata[M21] * matb[M13] + mata[M22] * matb[M23] + mata[M23] * matb[M33];
		tmp[M30] = mata[M30] * matb[M00] + mata[M31] * matb[M10] + mata[M32] * matb[M20] + mata[M33] * matb[M30];
		tmp[M31] = mata[M30] * matb[M01] + mata[M31] * matb[M11] + mata[M32] * matb[M21] + mata[M33] * matb[M31];
		tmp[M32] = mata[M30] * matb[M02] + mata[M31] * matb[M12] + mata[M32] * matb[M22] + mata[M33] * matb[M32];
		tmp[M33] = mata[M30] * matb[M03] + mata[M31] * matb[M13] + mata[M32] * matb[M23] + mata[M33] * matb[M33];
		memcpy(mata, tmp, sizeof(float) *  16);
	}
	
	static inline void matrix4_mulVec(float* mat, float* vec) {
		float x = vec[0] * mat[M00] + vec[1] * mat[M01] + vec[2] * mat[M02] + mat[M03];
		float y = vec[0] * mat[M10] + vec[1] * mat[M11] + vec[2] * mat[M12] + mat[M13];
		float z = vec[0] * mat[M20] + vec[1] * mat[M21] + vec[2] * mat[M22] + mat[M23];
		vec[0] = x;
		vec[1] = y;
		vec[2] = z;
	}
	
	static inline void matrix4_proj(float* mat, float* vec) {
		float inv_w = 1.0f / (vec[0] * mat[M30] + vec[1] * mat[M31] + vec[2] * mat[M32] + mat[M33]);
		float x = (vec[0] * mat[M00] + vec[1] * mat[M01] + vec[2] * mat[M02] + mat[M03]) * inv_w;
		float y = (vec[0] * mat[M10] + vec[1] * mat[M11] + vec[2] * mat[M12] + mat[M13]) * inv_w; 
		float z = (vec[0] * mat[M20] + vec[1] * mat[M21] + vec[2] * mat[M22] + mat[M23]) * inv_w;
		vec[0] = x;
		vec[1] = y;
		vec[2] = z;
	}
	
	static inline void matrix4_rot(float* mat, float* vec) {
		float x = vec[0] * mat[M00] + vec[1] * mat[M01] + vec[2] * mat[M02];
		float y = vec[0] * mat[M10] + vec[1] * mat[M11] + vec[2] * mat[M12];
		float z = vec[0] * mat[M20] + vec[1] * mat[M21] + vec[2] * mat[M22];
		vec[0] = x;
		vec[1] = y;
		vec[2] = z;
	}
	 */

	/** Multiplies the vectors with the given matrix. The matrix array is assumed to hold a 4x4 column major matrix as you can get
	 * from {@link Matrix4#val}. The vectors array is assumed to hold 3-component vectors. Offset specifies the offset into the
	 * array where the x-component of the first vector is located. The numVecs parameter specifies the number of vectors stored in
	 * the vectors array. The stride parameter specifies the number of floats between subsequent vectors and must be >= 3. This is
	 * the same as {@link Vector3#mul(Matrix4)} applied to multiple vectors.
	 * @param mat the matrix
	 * @param vecs the vectors
	 * @param offset the offset into the vectors array
	 * @param numVecs the number of vectors
	 * @param stride the stride between vectors in floats */
//	public static native void mulVec(float[] mat, float[] vecs, int offset, int numVecs, int stride) 
	/*-{ }-*/ /*
		float* vecPtr = vecs + offset;
		for(int i = 0; i < numVecs; i++) {
			matrix4_mulVec(mat, vecPtr);
			vecPtr += stride;
		}
	*/

	/** Multiplies the vectors with the given matrix, , performing a division by w. The matrix array is assumed to hold a 4x4 column
	 * major matrix as you can get from {@link Matrix4#val}. The vectors array is assumed to hold 3-component vectors. Offset
	 * specifies the offset into the array where the x-component of the first vector is located. The numVecs parameter specifies
	 * the number of vectors stored in the vectors array. The stride parameter specifies the number of floats between subsequent
	 * vectors and must be >= 3. This is the same as {@link Vector3#prj(Matrix4)} applied to multiple vectors.
	 * @param mat the matrix
	 * @param vecs the vectors
	 * @param offset the offset into the vectors array
	 * @param numVecs the number of vectors
	 * @param stride the stride between vectors in floats */
//	public static native void prj(float[] mat, float[] vecs, int offset, int numVecs, int stride) 
	/*-{ }-*/ /*
		float* vecPtr = vecs + offset;
		for(int i = 0; i < numVecs; i++) {
			matrix4_proj(mat, vecPtr);
			vecPtr += stride;
		}
	*/

	/** Multiplies the vectors with the top most 3x3 sub-matrix of the given matrix. The matrix array is assumed to hold a 4x4
	 * column major matrix as you can get from {@link Matrix4#val}. The vectors array is assumed to hold 3-component vectors.
	 * Offset specifies the offset into the array where the x-component of the first vector is located. The numVecs parameter
	 * specifies the number of vectors stored in the vectors array. The stride parameter specifies the number of floats between
	 * subsequent vectors and must be >= 3. This is the same as {@link Vector3#rot(Matrix4)} applied to multiple vectors.
	 * @param mat the matrix
	 * @param vecs the vectors
	 * @param offset the offset into the vectors array
	 * @param numVecs the number of vectors
	 * @param stride the stride between vectors in floats */
//	public static native void rot(float[] mat, float[] vecs, int offset, int numVecs, int stride) 
	/*-{ }-*/ /*
		float* vecPtr = vecs + offset;
		for(int i = 0; i < numVecs; i++) {
			matrix4_rot(mat, vecPtr);
			vecPtr += stride;
		}
	*/
	// @on

	/** Multiplies the matrix mata with matrix matb, storing the result in mata. The arrays are assumed to hold 4x4 column major
	 * matrices as you can get from {@link Matrix4#val}. This is the same as {@link Matrix4#mul(Matrix4)}.
	 *
	 * @param mata the first matrix.
	 * @param matb the second matrix. */
	public static void mul(DGFixedPoint[] mata, DGFixedPoint[] matb)
	{
		DGFixedPoint m00 = mata[M00Index] * matb[M00Index] + mata[M01Index] * matb[M10Index] + mata[M02Index] * matb[M20Index] + mata[M03Index] * matb[M30Index];
		DGFixedPoint m01 = mata[M00Index] * matb[M01Index] + mata[M01Index] * matb[M11Index] + mata[M02Index] * matb[M21Index] + mata[M03Index] * matb[M31Index];
		DGFixedPoint m02 = mata[M00Index] * matb[M02Index] + mata[M01Index] * matb[M12Index] + mata[M02Index] * matb[M22Index] + mata[M03Index] * matb[M32Index];
		DGFixedPoint m03 = mata[M00Index] * matb[M03Index] + mata[M01Index] * matb[M13Index] + mata[M02Index] * matb[M23Index] + mata[M03Index] * matb[M33Index];
		DGFixedPoint m10 = mata[M10Index] * matb[M00Index] + mata[M11Index] * matb[M10Index] + mata[M12Index] * matb[M20Index] + mata[M13Index] * matb[M30Index];
		DGFixedPoint m11 = mata[M10Index] * matb[M01Index] + mata[M11Index] * matb[M11Index] + mata[M12Index] * matb[M21Index] + mata[M13Index] * matb[M31Index];
		DGFixedPoint m12 = mata[M10Index] * matb[M02Index] + mata[M11Index] * matb[M12Index] + mata[M12Index] * matb[M22Index] + mata[M13Index] * matb[M32Index];
		DGFixedPoint m13 = mata[M10Index] * matb[M03Index] + mata[M11Index] * matb[M13Index] + mata[M12Index] * matb[M23Index] + mata[M13Index] * matb[M33Index];
		DGFixedPoint m20 = mata[M20Index] * matb[M00Index] + mata[M21Index] * matb[M10Index] + mata[M22Index] * matb[M20Index] + mata[M23Index] * matb[M30Index];
		DGFixedPoint m21 = mata[M20Index] * matb[M01Index] + mata[M21Index] * matb[M11Index] + mata[M22Index] * matb[M21Index] + mata[M23Index] * matb[M31Index];
		DGFixedPoint m22 = mata[M20Index] * matb[M02Index] + mata[M21Index] * matb[M12Index] + mata[M22Index] * matb[M22Index] + mata[M23Index] * matb[M32Index];
		DGFixedPoint m23 = mata[M20Index] * matb[M03Index] + mata[M21Index] * matb[M13Index] + mata[M22Index] * matb[M23Index] + mata[M23Index] * matb[M33Index];
		DGFixedPoint m30 = mata[M30Index] * matb[M00Index] + mata[M31Index] * matb[M10Index] + mata[M32Index] * matb[M20Index] + mata[M33Index] * matb[M30Index];
		DGFixedPoint m31 = mata[M30Index] * matb[M01Index] + mata[M31Index] * matb[M11Index] + mata[M32Index] * matb[M21Index] + mata[M33Index] * matb[M31Index];
		DGFixedPoint m32 = mata[M30Index] * matb[M02Index] + mata[M31Index] * matb[M12Index] + mata[M32Index] * matb[M22Index] + mata[M33Index] * matb[M32Index];
		DGFixedPoint m33 = mata[M30Index] * matb[M03Index] + mata[M31Index] * matb[M13Index] + mata[M32Index] * matb[M23Index] + mata[M33Index] * matb[M33Index];
		mata[M00Index] = m00;
		mata[M10Index] = m10;
		mata[M20Index] = m20;
		mata[M30Index] = m30;
		mata[M01Index] = m01;
		mata[M11Index] = m11;
		mata[M21Index] = m21;
		mata[M31Index] = m31;
		mata[M02Index] = m02;
		mata[M12Index] = m12;
		mata[M22Index] = m22;
		mata[M32Index] = m32;
		mata[M03Index] = m03;
		mata[M13Index] = m13;
		mata[M23Index] = m23;
		mata[M33Index] = m33;
	}

	/** Multiplies the vector with the given matrix. The matrix array is assumed to hold a 4x4 column major matrix as you can get
	 * from {@link Matrix4#val}. The vector array is assumed to hold a 3-component vector, with x being the first element, y being
	 * the second and z being the last component. The result is stored in the vector array. This is the same as
	 * {@link Vector3#mul(Matrix4)}.
	 * @param mat the matrix
	 * @param vec the vector. */
	public static void mulVec(DGFixedPoint[] mat, DGFixedPoint[] vec)
	{
		DGFixedPoint x = vec[0] * mat[M00Index] + vec[1] * mat[M01Index] + vec[2] * mat[M02Index] + mat[M03Index];
		DGFixedPoint y = vec[0] * mat[M10Index] + vec[1] * mat[M11Index] + vec[2] * mat[M12Index] + mat[M13Index];
		DGFixedPoint z = vec[0] * mat[M20Index] + vec[1] * mat[M21Index] + vec[2] * mat[M22Index] + mat[M23Index];
		vec[0] = x;
		vec[1] = y;
		vec[2] = z;
	}

	/** Multiplies the vector with the given matrix, performing a division by w. The matrix array is assumed to hold a 4x4 column
	 * major matrix as you can get from {@link Matrix4#val}. The vector array is assumed to hold a 3-component vector, with x being
	 * the first element, y being the second and z being the last component. The result is stored in the vector array. This is the
	 * same as {@link Vector3#prj(Matrix4)}.
	 * @param mat the matrix
	 * @param vec the vector. */
	public static void prj(DGFixedPoint[] mat, DGFixedPoint[] vec)
	{
		DGFixedPoint inv_w = (DGFixedPoint) 1.0f / (vec[0] * mat[M30Index] + vec[1] * mat[M31Index] + vec[2] * mat[M32Index] + mat[M33Index]);
		DGFixedPoint x = (vec[0] * mat[M00Index] + vec[1] * mat[M01Index] + vec[2] * mat[M02Index] + mat[M03Index]) * inv_w;
		DGFixedPoint y = (vec[0] * mat[M10Index] + vec[1] * mat[M11Index] + vec[2] * mat[M12Index] + mat[M13Index]) * inv_w;
		DGFixedPoint z = (vec[0] * mat[M20Index] + vec[1] * mat[M21Index] + vec[2] * mat[M22Index] + mat[M23Index]) * inv_w;
		vec[0] = x;
		vec[1] = y;
		vec[2] = z;
	}

	/** Multiplies the vector with the top most 3x3 sub-matrix of the given matrix. The matrix array is assumed to hold a 4x4
	 * column major matrix as you can get from {@link Matrix4#val}. The vector array is assumed to hold a 3-component vector, with
	 * x being the first element, y being the second and z being the last component. The result is stored in the vector array. This
	 * is the same as {@link Vector3#rot(Matrix4)}.
	 * @param mat the matrix
	 * @param vec the vector. */
	public static void rot(DGFixedPoint[] mat, DGFixedPoint[] vec)
	{
		DGFixedPoint x = vec[0] * mat[M00Index] + vec[1] * mat[M01Index] + vec[2] * mat[M02Index];
		DGFixedPoint y = vec[0] * mat[M10Index] + vec[1] * mat[M11Index] + vec[2] * mat[M12Index];
		DGFixedPoint z = vec[0] * mat[M20Index] + vec[1] * mat[M21Index] + vec[2] * mat[M22Index];
		vec[0] = x;
		vec[1] = y;
		vec[2] = z;
	}

	/** Computes the inverse of the given matrix. The matrix array is assumed to hold a 4x4 column major matrix as you can get from
	 * {@link Matrix4#val}.
	 * @param values the matrix values.
	 * @return false in case the inverse could not be calculated, true otherwise. */
	public static bool inv(DGFixedPoint[] values)
	{
		DGFixedPoint l_det = det(values);
		if (l_det == (DGFixedPoint) 0) return false;
		DGFixedPoint m00 = values[M12Index] * values[M23Index] * values[M31Index] - values[M13Index] * values[M22Index] * values[M31Index]
		         + values[M13Index] * values[M21Index] * values[M32Index] - values[M11Index] * values[M23Index] * values[M32Index]
		                                                   - values[M12Index] * values[M21Index] * values[M33Index] +
		         values[M11Index] * values[M22Index] * values[M33Index];
		DGFixedPoint m01 = values[M03Index] * values[M22Index] * values[M31Index] - values[M02Index] * values[M23Index] * values[M31Index]
		                                                 - values[M03Index] * values[M21Index] * values[M32Index] + values[M01Index] *
		                                                                                           values[M23Index] *
		                                                                                           values[M32Index]
		                                                                                           + values[M02Index] *
		                                                                                           values[M21Index] *
		                                                                                           values[M33Index] -
		         values[M01Index] * values[M22Index] * values[M33Index];
		DGFixedPoint m02 = values[M02Index] * values[M13Index] * values[M31Index] - values[M03Index] * values[M12Index] * values[M31Index]
		         + values[M03Index] * values[M11Index] * values[M32Index] - values[M01Index] * values[M13Index] * values[M32Index]
		                                                   - values[M02Index] * values[M11Index] * values[M33Index] +
		         values[M01Index] * values[M12Index] * values[M33Index];
		DGFixedPoint m03 = values[M03Index] * values[M12Index] * values[M21Index] - values[M02Index] * values[M13Index] * values[M21Index]
		                                                 - values[M03Index] * values[M11Index] * values[M22Index] + values[M01Index] *
		                                                                                           values[M13Index] *
		                                                                                           values[M22Index]
		                                                                                           + values[M02Index] *
		                                                                                           values[M11Index] *
		                                                                                           values[M23Index] -
		         values[M01Index] * values[M12Index] * values[M23Index];
		DGFixedPoint m10 = values[M13Index] * values[M22Index] * values[M30Index] - values[M12Index] * values[M23Index] * values[M30Index]
		                                                 - values[M13Index] * values[M20Index] * values[M32Index] + values[M10Index] *
		                                                                                           values[M23Index] *
		                                                                                           values[M32Index]
		                                                                                           + values[M12Index] *
		                                                                                           values[M20Index] *
		                                                                                           values[M33Index] -
		         values[M10Index] * values[M22Index] * values[M33Index];
		DGFixedPoint m11 = values[M02Index] * values[M23Index] * values[M30Index] - values[M03Index] * values[M22Index] * values[M30Index]
		         + values[M03Index] * values[M20Index] * values[M32Index] - values[M00Index] * values[M23Index] * values[M32Index]
		                                                   - values[M02Index] * values[M20Index] * values[M33Index] +
		         values[M00Index] * values[M22Index] * values[M33Index];
		DGFixedPoint m12 = values[M03Index] * values[M12Index] * values[M30Index] - values[M02Index] * values[M13Index] * values[M30Index]
		                                                 - values[M03Index] * values[M10Index] * values[M32Index] + values[M00Index] *
		                                                                                           values[M13Index] *
		                                                                                           values[M32Index]
		                                                                                           + values[M02Index] *
		                                                                                           values[M10Index] *
		                                                                                           values[M33Index] -
		         values[M00Index] * values[M12Index] * values[M33Index];
		DGFixedPoint m13 = values[M02Index] * values[M13Index] * values[M20Index] - values[M03Index] * values[M12Index] * values[M20Index]
		         + values[M03Index] * values[M10Index] * values[M22Index] - values[M00Index] * values[M13Index] * values[M22Index]
		                                                   - values[M02Index] * values[M10Index] * values[M23Index] +
		         values[M00Index] * values[M12Index] * values[M23Index];
		DGFixedPoint m20 = values[M11Index] * values[M23Index] * values[M30Index] - values[M13Index] * values[M21Index] * values[M30Index]
		         + values[M13Index] * values[M20Index] * values[M31Index] - values[M10Index] * values[M23Index] * values[M31Index]
		                                                   - values[M11Index] * values[M20Index] * values[M33Index] +
		         values[M10Index] * values[M21Index] * values[M33Index];
		DGFixedPoint m21 = values[M03Index] * values[M21Index] * values[M30Index] - values[M01Index] * values[M23Index] * values[M30Index]
		                                                 - values[M03Index] * values[M20Index] * values[M31Index] + values[M00Index] *
		                                                                                           values[M23Index] *
		                                                                                           values[M31Index]
		                                                                                           + values[M01Index] *
		                                                                                           values[M20Index] *
		                                                                                           values[M33Index] -
		         values[M00Index] * values[M21Index] * values[M33Index];
		DGFixedPoint m22 = values[M01Index] * values[M13Index] * values[M30Index] - values[M03Index] * values[M11Index] * values[M30Index]
		         + values[M03Index] * values[M10Index] * values[M31Index] - values[M00Index] * values[M13Index] * values[M31Index]
		                                                   - values[M01Index] * values[M10Index] * values[M33Index] +
		         values[M00Index] * values[M11Index] * values[M33Index];
		DGFixedPoint m23 = values[M03Index] * values[M11Index] * values[M20Index] - values[M01Index] * values[M13Index] * values[M20Index]
		                                                 - values[M03Index] * values[M10Index] * values[M21Index] + values[M00Index] *
		                                                                                           values[M13Index] *
		                                                                                           values[M21Index]
		                                                                                           + values[M01Index] *
		                                                                                           values[M10Index] *
		                                                                                           values[M23Index] -
		         values[M00Index] * values[M11Index] * values[M23Index];
		DGFixedPoint m30 = values[M12Index] * values[M21Index] * values[M30Index] - values[M11Index] * values[M22Index] * values[M30Index]
		                                                 - values[M12Index] * values[M20Index] * values[M31Index] + values[M10Index] *
		                                                                                           values[M22Index] *
		                                                                                           values[M31Index]
		                                                                                           + values[M11Index] *
		                                                                                           values[M20Index] *
		                                                                                           values[M32Index] -
		         values[M10Index] * values[M21Index] * values[M32Index];
		DGFixedPoint m31 = values[M01Index] * values[M22Index] * values[M30Index] - values[M02Index] * values[M21Index] * values[M30Index]
		         + values[M02Index] * values[M20Index] * values[M31Index] - values[M00Index] * values[M22Index] * values[M31Index]
		                                                   - values[M01Index] * values[M20Index] * values[M32Index] +
		         values[M00Index] * values[M21Index] * values[M32Index];
		DGFixedPoint m32 = values[M02Index] * values[M11Index] * values[M30Index] - values[M01Index] * values[M12Index] * values[M30Index]
		                                                 - values[M02Index] * values[M10Index] * values[M31Index] + values[M00Index] *
		                                                                                           values[M12Index] *
		                                                                                           values[M31Index]
		                                                                                           + values[M01Index] *
		                                                                                           values[M10Index] *
		                                                                                           values[M32Index] -
		         values[M00Index] * values[M11Index] * values[M32Index];
		DGFixedPoint m33 = values[M01Index] * values[M12Index] * values[M20Index] - values[M02Index] * values[M11Index] * values[M20Index]
		         + values[M02Index] * values[M10Index] * values[M21Index] - values[M00Index] * values[M12Index] * values[M21Index]
		                                                   - values[M01Index] * values[M10Index] * values[M22Index] +
		         values[M00Index] * values[M11Index] * values[M22Index];
		DGFixedPoint inv_det = (DGFixedPoint) 1.0f / l_det;
		values[M00Index] = m00 * inv_det;
		values[M10Index] = m10 * inv_det;
		values[M20Index] = m20 * inv_det;
		values[M30Index] = m30 * inv_det;
		values[M01Index] = m01 * inv_det;
		values[M11Index] = m11 * inv_det;
		values[M21Index] = m21 * inv_det;
		values[M31Index] = m31 * inv_det;
		values[M02Index] = m02 * inv_det;
		values[M12Index] = m12 * inv_det;
		values[M22Index] = m22 * inv_det;
		values[M32Index] = m32 * inv_det;
		values[M03Index] = m03 * inv_det;
		values[M13Index] = m13 * inv_det;
		values[M23Index] = m23 * inv_det;
		values[M33Index] = m33 * inv_det;
		return true;
	}

	/** Computes the determinante of the given matrix. The matrix array is assumed to hold a 4x4 column major matrix as you can get
	 * from {@link Matrix4#val}.
	 * @param values the matrix values.
	 * @return the determinante. */
	public static DGFixedPoint det(DGFixedPoint[] values)
	{
		return values[M30Index] * values[M21Index] * values[M12Index] * values[M03Index] - values[M20Index] * values[M31Index] * values[M12Index] *
		                                                             values[M03Index]
		                                                             - values[M30Index] * values[M11Index] * values[M22Index] *
		                                                             values[M03Index] + values[M10Index] * values[M31Index] *
		                                                                         values[M22Index] * values[M03Index]
		                                                                         + values[M20Index] * values[M11Index] *
		                                                                         values[M32Index] *
		                                                                         values[M03Index] - values[M10Index] *
		                                                                                     values[M21Index] * values[M32Index] *
		                                                                                     values[M03Index]
		                                                                                     - values[M30Index] *
		                                                                                     values[M21Index] * values[M02Index] *
		                                                                                     values[M13Index] + values[M20Index] *
		                                                                                                 values[M31Index] *
		                                                                                                 values[M02Index] *
		                                                                                                 values[M13Index]
		                                                                                                 + values[M30Index] *
		                                                                                                 values[M01Index] *
		                                                                                                 values[M22Index] *
		                                                                                                 values
			                                                                                                 [M13Index] - values
			                                                                                                       [
				                                                                                                       M00Index] *
			                                                                                                       values
				                                                                                                       [M31Index] *
			                                                                                                       values
				                                                                                                       [M22Index] *
			                                                                                                       values
				                                                                                                       [M13Index]
			                                                                                                       - values
				                                                                                                       [M20Index] *
			                                                                                                       values
				                                                                                                       [M01Index] *
			                                                                                                       values
				                                                                                                       [M32Index] *
			                                                                                                       values
				                                                                                                       [M13Index] + values
				                                                                                                             [
					                                                                                                             M00Index] *
				                                                                                                             values
					                                                                                                             [M21Index] *
				                                                                                                             values
					                                                                                                             [M32Index] *
				                                                                                                             values
					                                                                                                             [M13Index]
				                                                                                                             + values
					                                                                                                             [M30Index] *
				                                                                                                             values
					                                                                                                             [M11Index] *
				                                                                                                             values
					                                                                                                             [M02Index] *
				                                                                                                             values
					                                                                                                             [M23Index] - values
					                                                                                                                   [
						                                                                                                                   M10Index] *
					                                                                                                                   values
						                                                                                                                   [M31Index] *
					                                                                                                                   values
						                                                                                                                   [M02Index] *
					                                                                                                                   values
						                                                                                                                   [M23Index]
					                                                                                                                   - values
						                                                                                                                   [M30Index] *
					                                                                                                                   values
						                                                                                                                   [M01Index] *
					                                                                                                                   values
						                                                                                                                   [M12Index] *
					                                                                                                                   values
						                                                                                                                   [M23Index] + values
						                                                                                                                         [
							                                                                                                                         M00Index] *
						                                                                                                                         values
							                                                                                                                         [M31Index] *
						                                                                                                                         values
							                                                                                                                         [M12Index] *
						                                                                                                                         values
							                                                                                                                         [M23Index]
						                                                                                                                         + values
							                                                                                                                         [M10Index] *
						                                                                                                                         values
							                                                                                                                         [M01Index] *
						                                                                                                                         values
							                                                                                                                         [M32Index] *
						                                                                                                                         values
							                                                                                                                         [M23Index] - values
							                                                                                                                               [
								                                                                                                                               M00Index] *
							                                                                                                                               values
								                                                                                                                               [M11Index] *
							                                                                                                                               values
								                                                                                                                               [M32Index] *
							                                                                                                                               values
								                                                                                                                               [M23Index]
							                                                                                                                               - values
								                                                                                                                               [M20Index] *
							                                                                                                                               values
								                                                                                                                               [M11Index] *
							                                                                                                                               values
								                                                                                                                               [M02Index] *
							                                                                                                                               values
								                                                                                                                               [M33Index] + values
								                                                                                                                                     [
									                                                                                                                                     M10Index] *
								                                                                                                                                     values
									                                                                                                                                     [M21Index] *
								                                                                                                                                     values
									                                                                                                                                     [M02Index] *
								                                                                                                                                     values
									                                                                                                                                     [M33Index]
								                                                                                                                                     + values
									                                                                                                                                     [M20Index] *
								                                                                                                                                     values
									                                                                                                                                     [M01Index] *
								                                                                                                                                     values
									                                                                                                                                     [M12Index] *
								                                                                                                                                     values
									                                                                                                                                     [M33Index] - values
									                                                                                                                                           [
										                                                                                                                                           M00Index] *
									                                                                                                                                           values
										                                                                                                                                           [M21Index] *
									                                                                                                                                           values
										                                                                                                                                           [M12Index] *
									                                                                                                                                           values
										                                                                                                                                           [M33Index]
									                                                                                                                                           -
									                                                                                                                                           values
										                                                                                                                                           [M10Index] *
									                                                                                                                                           values
										                                                                                                                                           [M01Index] *
									                                                                                                                                           values
										                                                                                                                                           [M22Index] *
									                                                                                                                                           values
										                                                                                                                                           [M33Index] +
		       values[M00Index] * values[M11Index] * values[M22Index] * values[M33Index];
	}

	/** Postmultiplies this matrix by a translation matrix. Postmultiplication is also used by OpenGL ES'
	 * glTranslate/glRotate/glScale
	 * @param translation
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 translate(DGVector3 translation)
	{
		return translate(translation.x, translation.y, translation.z);
	}

	/** Postmultiplies this matrix by a translation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param x Translation in the x-axis.
	 * @param y Translation in the y-axis.
	 * @param z Translation in the z-axis.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 translate(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		this.m03 += this.m00 * x + this.m01 * y + this.m02 * z;
		this.m13 += this.m10 * x + this.m11 * y + this.m12 * z;
		this.m23 += this.m20 * x + this.m21 * y + this.m22 * z;
		this.m33 += this.m30 * x + this.m31 * y + this.m32 * z;
		return this;
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param axis The vector axis to rotate around.
	 * @param degrees The angle in degrees.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 rotate(DGVector3 axis, DGFixedPoint degrees)
	{
		if (degrees == (DGFixedPoint) 0) return this;
		quat.set(axis, degrees);
		return rotate(quat);
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param axis The vector axis to rotate around.rotateTowardDirection
	 * @param radians The angle in radians.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 rotateRad(DGVector3 axis, DGFixedPoint radians)
	{
		if (radians == (DGFixedPoint) 0) return this;
		quat.setFromAxisRad(axis, radians);
		return rotate(quat);
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale
	 * @param axisX The x-axis component of the vector to rotate around.
	 * @param axisY The y-axis component of the vector to rotate around.
	 * @param axisZ The z-axis component of the vector to rotate around.
	 * @param degrees The angle in degrees
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 rotate(DGFixedPoint axisX, DGFixedPoint axisY, DGFixedPoint axisZ, DGFixedPoint degrees)
	{
		if (degrees == (DGFixedPoint) 0) return this;
		quat.setFromAxis(axisX, axisY, axisZ, degrees);
		return rotate(quat);
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale
	 * @param axisX The x-axis component of the vector to rotate around.
	 * @param axisY The y-axis component of the vector to rotate around.
	 * @param axisZ The z-axis component of the vector to rotate around.
	 * @param radians The angle in radians
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 rotateRad(DGFixedPoint axisX, DGFixedPoint axisY, DGFixedPoint axisZ, DGFixedPoint radians)
	{
		if (radians == (DGFixedPoint) 0) return this;
		quat.setFromAxisRad(axisX, axisY, axisZ, radians);
		return rotate(quat);
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param rotation
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 rotate(DGQuaternion rotation)
	{
		DGFixedPoint x = rotation.x;
		DGFixedPoint y = rotation.y;
		DGFixedPoint z = rotation.z;
		DGFixedPoint w = rotation.w;
		DGFixedPoint xx = x * x;
		DGFixedPoint xy = x * y;
		DGFixedPoint xz = x * z;
		DGFixedPoint xw = x * w;
		DGFixedPoint yy = y * y;
		DGFixedPoint yz = y * z;
		DGFixedPoint yw = y * w;
		DGFixedPoint zz = z * z;
		DGFixedPoint zw = z * w;
		// Set matrix from quaternion
		DGFixedPoint r00 = (DGFixedPoint) 1 - (DGFixedPoint) 2 * (yy + zz);
		DGFixedPoint r01 = (DGFixedPoint) 2 * (xy - zw);
		DGFixedPoint r02 = (DGFixedPoint) 2 * (xz + yw);
		DGFixedPoint r10 = (DGFixedPoint) 2 * (xy + zw);
		DGFixedPoint r11 = (DGFixedPoint) 1 - (DGFixedPoint) 2 * (xx + zz);
		DGFixedPoint r12 = (DGFixedPoint) 2 * (yz - xw);
		DGFixedPoint r20 = (DGFixedPoint) 2 * (xz - yw);
		DGFixedPoint r21 = (DGFixedPoint) 2 * (yz + xw);
		DGFixedPoint r22 = (DGFixedPoint) 1 - (DGFixedPoint) 2 * (xx + yy);
		DGFixedPoint m00 = this.m00 * r00 + this.m01 * r10 + this.m02 * r20;
		DGFixedPoint m01 = this.m00 * r01 + this.m01 * r11 + this.m02 * r21;
		DGFixedPoint m02 = this.m00 * r02 + this.m01 * r12 + this.m02 * r22;
		DGFixedPoint m10 = this.m10 * r00 + this.m11 * r10 + this.m12 * r20;
		DGFixedPoint m11 = this.m10 * r01 + this.m11 * r11 + this.m12 * r21;
		DGFixedPoint m12 = this.m10 * r02 + this.m11 * r12 + this.m12 * r22;
		DGFixedPoint m20 = this.m20 * r00 + this.m21 * r10 + this.m22 * r20;
		DGFixedPoint m21 = this.m20 * r01 + this.m21 * r11 + this.m22 * r21;
		DGFixedPoint m22 = this.m20 * r02 + this.m21 * r12 + this.m22 * r22;
		DGFixedPoint m30 = this.m30 * r00 + this.m31 * r10 + this.m32 * r20;
		DGFixedPoint m31 = this.m30 * r01 + this.m31 * r11 + this.m32 * r21;
		DGFixedPoint m32 = this.m30 * r02 + this.m31 * r12 + this.m32 * r22;
		this.m00 = m00;
		this.m10 = m10;
		this.m20 = m20;
		this.m30 = m30;
		this.m01 = m01;
		this.m11 = m11;
		this.m21 = m21;
		this.m31 = m31;
		this.m02 = m02;
		this.m12 = m12;
		this.m22 = m22;
		this.m32 = m32;
		return this;
	}

	/** Postmultiplies this matrix by the rotation between two vectors.
	 * @param v1 The base vector
	 * @param v2 The target vector
	 * @return This matrix for the purpose of chaining methods together */
	public DGMatrix4x4 rotate(DGVector3 v1, DGVector3 v2)
	{
		return rotate(quat.setFromCross(v1, v2));
	}

	/** Post-multiplies this matrix by a rotation toward a direction.
	 * @param direction direction to rotate toward
	 * @param up up vector
	 * @return This matrix for chaining */
	public DGMatrix4x4 rotateTowardDirection(DGVector3 direction, DGVector3 up)
	{
		l_vez = l_vez.set(direction).nor();
		l_vex = -l_vex.set(l_vez).crs(up).nor();
		l_vey = -l_vey.set(l_vex).crs(l_vez).nor();

		DGFixedPoint m00 = this.m00 * l_vex.x + this.m01 * l_vex.y + this.m02 * l_vex.z;
		DGFixedPoint m01 = this.m00 * l_vey.x + this.m01 * l_vey.y + this.m02 * l_vey.z;
		DGFixedPoint m02 = this.m00 * l_vez.x + this.m01 * l_vez.y + this.m02 * l_vez.z;
		DGFixedPoint m10 = this.m10 * l_vex.x + this.m11 * l_vex.y + this.m12 * l_vex.z;
		DGFixedPoint m11 = this.m10 * l_vey.x + this.m11 * l_vey.y + this.m12 * l_vey.z;
		DGFixedPoint m12 = this.m10 * l_vez.x + this.m11 * l_vez.y + this.m12 * l_vez.z;
		DGFixedPoint m20 = this.m20 * l_vex.x + this.m21 * l_vex.y + this.m22 * l_vex.z;
		DGFixedPoint m21 = this.m20 * l_vey.x + this.m21 * l_vey.y + this.m22 * l_vey.z;
		DGFixedPoint m22 = this.m20 * l_vez.x + this.m21 * l_vez.y + this.m22 * l_vez.z;
		DGFixedPoint m30 = this.m30 * l_vex.x + this.m31 * l_vex.y + this.m32 * l_vex.z;
		DGFixedPoint m31 = this.m30 * l_vey.x + this.m31 * l_vey.y + this.m32 * l_vey.z;
		DGFixedPoint m32 = this.m30 * l_vez.x + this.m31 * l_vez.y + this.m32 * l_vez.z;
		this.m00 = m00;
		this.m10 = m10;
		this.m20 = m20;
		this.m30 = m30;
		this.m01 = m01;
		this.m11 = m11;
		this.m21 = m21;
		this.m31 = m31;
		this.m02 = m02;
		this.m12 = m12;
		this.m22 = m22;
		this.m32 = m32;
		return this;
	}

	/** Post-multiplies this matrix by a rotation toward a target.
	 * @param target the target to rotate to
	 * @param up the up vector
	 * @return This matrix for chaining */
	public DGMatrix4x4 rotateTowardTarget(DGVector3 target, DGVector3 up)
	{
		tmpVec.set(target.x - this.m03, target.y - this.m13, target.z - this.m23);
		return rotateTowardDirection(tmpVec, up);
	}

	/** Postmultiplies this matrix with a scale matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param scaleX The scale in the x-axis.
	 * @param scaleY The scale in the y-axis.
	 * @param scaleZ The scale in the z-axis.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 scale(DGFixedPoint scaleX, DGFixedPoint scaleY, DGFixedPoint scaleZ)
	{
		this.m00 *= scaleX;
		this.m01 *= scaleY;
		this.m02 *= scaleZ;
		this.m10 *= scaleX;
		this.m11 *= scaleY;
		this.m12 *= scaleZ;
		this.m20 *= scaleX;
		this.m21 *= scaleY;
		this.m22 *= scaleZ;
		this.m30 *= scaleX;
		this.m31 *= scaleY;
		this.m32 *= scaleZ;
		return this;
	}

	/** Copies the 4x3 upper-left sub-matrix into float array. The destination array is supposed to be a column major matrix.
	 * @param dst the destination matrix */
	public void extract4x3Matrix(DGFixedPoint[] dst)
	{
		dst[0] = this.m00;
		dst[1] = this.m10;
		dst[2] = this.m20;
		dst[3] = this.m01;
		dst[4] = this.m11;
		dst[5] = this.m21;
		dst[6] = this.m02;
		dst[7] = this.m12;
		dst[8] = this.m22;
		dst[9] = this.m03;
		dst[10] = this.m13;
		dst[11] = this.m23;
	}

	/** @return True if this matrix has any rotation or scaling, false otherwise */
	public bool hasRotationOrScaling()
	{
		return !(DGMath.IsEqual(this.m00, (DGFixedPoint) 1) && DGMath.IsEqual(this.m11, (DGFixedPoint) 1) &&
		         DGMath.IsEqual(this.m22, (DGFixedPoint) 1)
		         && DGMath.IsZero(this.m01) && DGMath.IsZero(this.m02) && DGMath.IsZero(this.m10) &&
		         DGMath.IsZero(this.m12)
		         && DGMath.IsZero(this.m20) && DGMath.IsZero(this.m21));
	}
}