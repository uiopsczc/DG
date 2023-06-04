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
using FPMatrix3x3 = DGMatrix3x3;
using FPVector3 = DGVector3;
using FPAffine2 = DGAffine2;
using FPQuaternion = DGQuaternion;


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
	public const int M00 = 0;

	/** XY: Typically the negative sine of the angle when rotated on the Z axis. On Vector3 multiplication this value is multiplied
	 * with the source Y component and added to the target X component. */
	public const int M01 = 4;

	/** XZ: Typically the sine of the angle when rotated on the Y axis. On Vector3 multiplication this value is multiplied with the
	 * source Z component and added to the target X component. */
	public const int M02 = 8;

	/** XW: Typically the translation of the X component. On Vector3 multiplication this value is added to the target X
	 * component. */
	public const int M03 = 12;

	/** YX: Typically the sine of the angle when rotated on the Z axis. On Vector3 multiplication this value is multiplied with the
	 * source X component and added to the target Y component. */
	public const int M10 = 1;

	/** YY: Typically the unrotated Y component for scaling, also the cosine of the angle when rotated on the X and/or Z axis. On
	 * Vector3 multiplication this value is multiplied with the source Y component and added to the target Y component. */
	public const int M11 = 5;

	/** YZ: Typically the negative sine of the angle when rotated on the X axis. On Vector3 multiplication this value is multiplied
	 * with the source Z component and added to the target Y component. */
	public const int M12 = 9;

	/** YW: Typically the translation of the Y component. On Vector3 multiplication this value is added to the target Y
	 * component. */
	public const int M13 = 13;

	/** ZX: Typically the negative sine of the angle when rotated on the Y axis. On Vector3 multiplication this value is multiplied
	 * with the source X component and added to the target Z component. */
	public const int M20 = 2;

	/** ZY: Typical the sine of the angle when rotated on the X axis. On Vector3 multiplication this value is multiplied with the
	 * source Y component and added to the target Z component. */
	public const int M21 = 6;

	/** ZZ: Typically the unrotated Z component for scaling, also the cosine of the angle when rotated on the X and/or Y axis. On
	 * Vector3 multiplication this value is multiplied with the source Z component and added to the target Z component. */
	public const int M22 = 10;

	/** ZW: Typically the translation of the Z component. On Vector3 multiplication this value is added to the target Z
	 * component. */
	public const int M23 = 14;

	/** WX: Typically the value zero. On Vector3 multiplication this value is ignored. */
	public const int M30 = 3;

	/** WY: Typically the value zero. On Vector3 multiplication this value is ignored. */
	public const int M31 = 7;

	/** WZ: Typically the value zero. On Vector3 multiplication this value is ignored. */
	public const int M32 = 11;

	/** WW: Typically the value one. On Vector3 multiplication this value is ignored. */
	public const int M33 = 15;

	private const int Count = 16;
	static FPQuaternion quat = new FPQuaternion(false);
	static FPQuaternion quat2 = new FPQuaternion(false);
	static FPVector3 l_vez = new FPVector3();
	static FPVector3 l_vex = new FPVector3();
	static FPVector3 l_vey = new FPVector3();
	static FPVector3 tmpVec = new FPVector3();
	static DGMatrix4x4 tmpMat = new DGMatrix4x4(false);
	static FPVector3 right = new FPVector3();
	static FPVector3 tmpForward = new FPVector3();
	static FPVector3 tmpUp = new FPVector3();

	public FP[] val;

	/** Constructs an identity matrix */
	public DGMatrix4x4(bool isNotLibgdx = false)
	{
		val = new FP[Count];
		val[M00] = (FP) 1f;
		val[M11] = (FP) 1f;
		val[M22] = (FP) 1f;
		val[M33] = (FP) 1f;
	}

	/** Constructs a matrix from the given matrix.
	 * @param matrix The matrix to copy. (This matrix is not modified) */
	public DGMatrix4x4(DGMatrix4x4 matrix)
	{
		val = new FP[Count];
		for (int i = 0; i < Count; i++)
			val[i] = matrix.val[i];
	}

	/** Constructs a matrix from the given float array. The array must have at least 16 elements; the first 16 will be copied.
	 * @param values The float array to copy. Remember that this matrix is in
	 *           <a href="http://en.wikipedia.org/wiki/Row-major_order">column major</a> order. (The float array is not
	 *           modified) */
	public DGMatrix4x4(FP[] values)
	{
		val = new FP[Count];
		for (int i = 0; i < Count; i++)
			val[i] = values[i];
	}

	/** Constructs a rotation matrix from the given {@link Quaternion}.
	 * @param quaternion The quaternion to be copied. (The quaternion is not modified) */
	public DGMatrix4x4(FPQuaternion quaternion)
	{
		val = new FP[Count];
		FP translationX = (FP) 0;
		FP translationY = (FP) 0;
		FP translationZ = (FP) 0;
		FP quaternionX = quaternion.x;
		FP quaternionY = quaternion.y;
		FP quaternionZ = quaternion.z;
		FP quaternionW = quaternion.w;

		FP xs = quaternionX * (FP) 2f, ys = quaternionY * (FP) 2f, zs = quaternionZ * (FP) 2f;
		FP wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
		FP xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
		FP yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

		val[M00] = (FP) 1f - (yy + zz);
		val[M01] = xy - wz;
		val[M02] = xz + wy;
		val[M03] = translationX;

		val[M10] = xy + wz;
		val[M11] = (FP) 1f - (xx + zz);
		val[M12] = yz - wx;
		val[M13] = translationY;

		val[M20] = xz - wy;
		val[M21] = yz + wx;
		val[M22] = (FP) 1f - (xx + yy);
		val[M23] = translationZ;

		val[M30] = (FP) 0f;
		val[M31] = (FP) 0f;
		val[M32] = (FP) 0f;
		val[M33] = (FP) 1f;
	}

	/** Construct a matrix from the given translation, rotation and scale.
	 * @param position The translation
	 * @param rotation The rotation, must be normalized
	 * @param scale The scale */
	public DGMatrix4x4(FPVector3 position, FPQuaternion rotation, FPVector3 scale)
	{
		val = new FP[Count];
		FP translationX = position.x;
		FP translationY = position.y;
		FP translationZ = position.z;
		FP quaternionX = rotation.x;
		FP quaternionY = rotation.y;
		FP quaternionZ = rotation.z;
		FP quaternionW = rotation.w;
		FP scaleX = scale.x;
		FP scaleY = scale.y;
		FP scaleZ = scale.z;

		FP xs = quaternionX * (FP) 2f, ys = quaternionY * (FP) 2f, zs = quaternionZ * (FP) 2f;
		FP wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
		FP xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
		FP yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

		val[M00] = scaleX * ((FP) 1.0f - (yy + zz));
		val[M01] = scaleY * (xy - wz);
		val[M02] = scaleZ * (xz + wy);
		val[M03] = translationX;

		val[M10] = scaleX * (xy + wz);
		val[M11] = scaleY * ((FP) 1.0f - (xx + zz));
		val[M12] = scaleZ * (yz - wx);
		val[M13] = translationY;

		val[M20] = scaleX * (xz - wy);
		val[M21] = scaleY * (yz + wx);
		val[M22] = scaleZ * ((FP) 1.0f - (xx + yy));
		val[M23] = translationZ;

		val[M30] = (FP) 0f;
		val[M31] = (FP) 0f;
		val[M32] = (FP) 0f;
		val[M33] = (FP) 1f;
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
	public DGMatrix4x4 set(FP[] values)
	{
		Array.Copy(values, 0, val, 0, val.Length);
		return this;
	}

	/** Sets the matrix to a rotation matrix representing the quaternion.
	 * @param quaternion The quaternion that is to be used to set this matrix.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 set(FPQuaternion quaternion)
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
	public DGMatrix4x4 set(FP quaternionX, FP quaternionY, FP quaternionZ, FP quaternionW)
	{
		return set((FP) 0f, (FP) 0f, (FP) 0f, quaternionX, quaternionY, quaternionZ, quaternionW);
	}

	/** Set this matrix to the specified translation and rotation.
	 * @param position The translation
	 * @param orientation The rotation, must be normalized
	 * @return This matrix for chaining */
	public DGMatrix4x4 set(FPVector3 position, FPQuaternion orientation)
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
	public DGMatrix4x4 set(FP translationX, FP translationY, FP translationZ, FP quaternionX, FP quaternionY,
		FP quaternionZ, FP quaternionW)
	{
		FP xs = quaternionX * (FP) 2f, ys = quaternionY * (FP) 2f, zs = quaternionZ * (FP) 2f;
		FP wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
		FP xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
		FP yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

		val[M00] = (FP) 1f - (yy + zz);
		val[M01] = xy - wz;
		val[M02] = xz + wy;
		val[M03] = translationX;

		val[M10] = xy + wz;
		val[M11] = (FP) 1f - (xx + zz);
		val[M12] = yz - wx;
		val[M13] = translationY;

		val[M20] = xz - wy;
		val[M21] = yz + wx;
		val[M22] = (FP) 1f - (xx + yy);
		val[M23] = translationZ;

		val[M30] = (FP) 0f;
		val[M31] = (FP) 0f;
		val[M32] = (FP) 0f;
		val[M33] = (FP) 1f;
		return this;
	}

	/** Set this matrix to the specified translation, rotation and scale.
	 * @param position The translation
	 * @param orientation The rotation, must be normalized
	 * @param scale The scale
	 * @return This matrix for chaining */
	public DGMatrix4x4 set(FPVector3 position, FPQuaternion orientation, FPVector3 scale)
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
	public DGMatrix4x4 set(FP translationX, FP translationY, FP translationZ, FP quaternionX, FP quaternionY,
		FP quaternionZ, FP quaternionW, FP scaleX, FP scaleY, FP scaleZ)
	{
		FP xs = quaternionX * (FP) 2f, ys = quaternionY * (FP) 2f, zs = quaternionZ * (FP) 2f;
		FP wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
		FP xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
		FP yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

		val[M00] = scaleX * ((FP) 1.0f - (yy + zz));
		val[M01] = scaleY * (xy - wz);
		val[M02] = scaleZ * (xz + wy);
		val[M03] = translationX;

		val[M10] = scaleX * (xy + wz);
		val[M11] = scaleY * ((FP) 1.0f - (xx + zz));
		val[M12] = scaleZ * (yz - wx);
		val[M13] = translationY;

		val[M20] = scaleX * (xz - wy);
		val[M21] = scaleY * (yz + wx);
		val[M22] = scaleZ * ((FP) 1.0f - (xx + yy));
		val[M23] = translationZ;

		val[M30] = (FP) 0f;
		val[M31] = (FP) 0f;
		val[M32] = (FP) 0f;
		val[M33] = (FP) 1f;
		return this;
	}

	/** Sets the four columns of the matrix which correspond to the x-, y- and z-axis of the vector space this matrix creates as
	 * well as the 4th column representing the translation of any point that is multiplied by this matrix.
	 * @param xAxis The x-axis.
	 * @param yAxis The y-axis.
	 * @param zAxis The z-axis.
	 * @param pos The translation vector. */
	public DGMatrix4x4 set(FPVector3 xAxis, FPVector3 yAxis, FPVector3 zAxis, FPVector3 pos)
	{
		val[M00] = xAxis.x;
		val[M01] = xAxis.y;
		val[M02] = xAxis.z;
		val[M10] = yAxis.x;
		val[M11] = yAxis.y;
		val[M12] = yAxis.z;
		val[M20] = zAxis.x;
		val[M21] = zAxis.y;
		val[M22] = zAxis.z;
		val[M03] = pos.x;
		val[M13] = pos.y;
		val[M23] = pos.z;
		val[M30] = (FP) 0f;
		val[M31] = (FP) 0f;
		val[M32] = (FP) 0f;
		val[M33] = (FP) 1f;
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
	public DGMatrix4x4 trn(FPVector3 vector)
	{
		val[M03] += vector.x;
		val[M13] += vector.y;
		val[M23] += vector.z;
		return this;
	}

	/** Adds a translational component to the matrix in the 4th column. The other columns are untouched.
	 * @param x The x-component of the translation vector.
	 * @param y The y-component of the translation vector.
	 * @param z The z-component of the translation vector.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 trn(FP x, FP y, FP z)
	{
		val[M03] += x;
		val[M13] += y;
		val[M23] += z;
		return this;
	}

	/** @return the backing float array */
	public FP[] getValues()
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
		FP m01 = val[M01];
		FP m02 = val[M02];
		FP m03 = val[M03];
		FP m12 = val[M12];
		FP m13 = val[M13];
		FP m23 = val[M23];
		val[M01] = val[M10];
		val[M02] = val[M20];
		val[M03] = val[M30];
		val[M10] = m01;
		val[M12] = val[M21];
		val[M13] = val[M31];
		val[M20] = m02;
		val[M21] = m12;
		val[M23] = val[M32];
		val[M30] = m03;
		val[M31] = m13;
		val[M32] = m23;
		return this;
	}

	/** Sets the matrix to an identity matrix.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 idt()
	{
		val[M00] = (FP) 1f;
		val[M01] = (FP) 0f;
		val[M02] = (FP) 0f;
		val[M03] = (FP) 0f;
		val[M10] = (FP) 0f;
		val[M11] = (FP) 1f;
		val[M12] = (FP) 0f;
		val[M13] = (FP) 0f;
		val[M20] = (FP) 0f;
		val[M21] = (FP) 0f;
		val[M22] = (FP) 1f;
		val[M23] = (FP) 0f;
		val[M30] = (FP) 0f;
		val[M31] = (FP) 0f;
		val[M32] = (FP) 0f;
		val[M33] = (FP) 1f;
		return this;
	}

	/** Inverts the matrix. Stores the result in this matrix.
	 * @return This matrix for the purpose of chaining methods together.
	 * @throws RuntimeException if the matrix is singular (not invertible) */
	public DGMatrix4x4 inv()
	{
		FP l_det = val[M30] * val[M21] * val[M12] * val[M03] - val[M20] * val[M31] * val[M12] * val[M03]
		                                                     - val[M30] * val[M11] * val[M22] * val[M03] + val[M10] *
		                                                                                                 val[M31] * val[
			                                                                                                 M22] * val[
			                                                                                                 M03]
		                                                                                                 + val[M20] *
		                                                                                                 val[M11] *
		                                                                                                 val[M32] * val[
			                                                                                                 M03] - val[
				                                                                                                      M10] *
			                                                                                                      val[
				                                                                                                      M21] *
			                                                                                                      val[
				                                                                                                      M32] *
			                                                                                                      val[
				                                                                                                      M03]
			                                                                                                      - val[
				                                                                                                      M30] *
			                                                                                                      val[
				                                                                                                      M21] *
			                                                                                                      val[
				                                                                                                      M02] *
			                                                                                                      val[
				                                                                                                      M13] + val
				                                                                                                           [
					                                                                                                           M20] *
				                                                                                                           val
					                                                                                                           [M31] *
				                                                                                                           val
					                                                                                                           [M02] *
				                                                                                                           val
					                                                                                                           [M13]
				                                                                                                           + val
					                                                                                                           [M30] *
				                                                                                                           val
					                                                                                                           [M01] *
				                                                                                                           val
					                                                                                                           [M22] *
				                                                                                                           val
					                                                                                                           [M13] - val
					                                                                                                                 [
						                                                                                                                 M00] *
					                                                                                                                 val
						                                                                                                                 [M31] *
					                                                                                                                 val
						                                                                                                                 [M22] *
					                                                                                                                 val
						                                                                                                                 [M13]
					                                                                                                                 - val
						                                                                                                                 [M20] *
					                                                                                                                 val
						                                                                                                                 [M01] *
					                                                                                                                 val
						                                                                                                                 [M32] *
					                                                                                                                 val
						                                                                                                                 [M13] + val
						                                                                                                                       [
							                                                                                                                       M00] *
						                                                                                                                       val
							                                                                                                                       [M21] *
						                                                                                                                       val
							                                                                                                                       [M32] *
						                                                                                                                       val
							                                                                                                                       [M13]
						                                                                                                                       + val
							                                                                                                                       [M30] *
						                                                                                                                       val
							                                                                                                                       [M11] *
						                                                                                                                       val
							                                                                                                                       [M02] *
						                                                                                                                       val
							                                                                                                                       [M23] - val
							                                                                                                                             [
								                                                                                                                             M10] *
							                                                                                                                             val
								                                                                                                                             [M31] *
							                                                                                                                             val
								                                                                                                                             [M02] *
							                                                                                                                             val
								                                                                                                                             [M23]
							                                                                                                                             - val
								                                                                                                                             [M30] *
							                                                                                                                             val
								                                                                                                                             [M01] *
							                                                                                                                             val
								                                                                                                                             [M12] *
							                                                                                                                             val
								                                                                                                                             [M23] + val
								                                                                                                                                   [
									                                                                                                                                   M00] *
								                                                                                                                                   val
									                                                                                                                                   [M31] *
								                                                                                                                                   val
									                                                                                                                                   [M12] *
								                                                                                                                                   val
									                                                                                                                                   [M23]
								                                                                                                                                   + val
									                                                                                                                                   [M10] *
								                                                                                                                                   val
									                                                                                                                                   [M01] *
								                                                                                                                                   val
									                                                                                                                                   [M32] *
								                                                                                                                                   val
									                                                                                                                                   [M23] - val
									                                                                                                                                         [
										                                                                                                                                         M00] *
									                                                                                                                                         val
										                                                                                                                                         [M11] *
									                                                                                                                                         val
										                                                                                                                                         [M32] *
									                                                                                                                                         val
										                                                                                                                                         [M23]
									                                                                                                                                         - val
										                                                                                                                                         [M20] *
									                                                                                                                                         val
										                                                                                                                                         [M11] *
									                                                                                                                                         val
										                                                                                                                                         [M02] *
									                                                                                                                                         val
										                                                                                                                                         [M33] + val
										                                                                                                                                               [
											                                                                                                                                               M10] *
										                                                                                                                                               val
											                                                                                                                                               [M21] *
										                                                                                                                                               val
											                                                                                                                                               [M02] *
										                                                                                                                                               val
											                                                                                                                                               [M33]
										                                                                                                                                               +
										                                                                                                                                               val
											                                                                                                                                               [M20] *
										                                                                                                                                               val
											                                                                                                                                               [M01] *
										                                                                                                                                               val
											                                                                                                                                               [M12] *
										                                                                                                                                               val
											                                                                                                                                               [M33] - val
											                                                                                                                                                     [
												                                                                                                                                                     M00] *
											                                                                                                                                                     val
												                                                                                                                                                     [M21] *
											                                                                                                                                                     val
												                                                                                                                                                     [M12] *
											                                                                                                                                                     val
												                                                                                                                                                     [M33]
											                                                                                                                                                     -
											                                                                                                                                                     val
												                                                                                                                                                     [M10] *
											                                                                                                                                                     val
												                                                                                                                                                     [M01] *
											                                                                                                                                                     val
												                                                                                                                                                     [M22] *
											                                                                                                                                                     val
												                                                                                                                                                     [M33] +
		           val[M00] * val[M11] * val[M22] * val[M33];
		if (l_det == (FP) 0f) throw new Exception("non-invertible matrix");
		FP m00 = val[M12] * val[M23] * val[M31] - val[M13] * val[M22] * val[M31] + val[M13] * val[M21] * val[M32]
		         - val[M11] * val[M23] * val[M32] - val[M12] * val[M21] * val[M33] + val[M11] * val[M22] * val[M33];
		FP m01 = val[M03] * val[M22] * val[M31] - val[M02] * val[M23] * val[M31] - val[M03] * val[M21] * val[M32]
		         + val[M01] * val[M23] * val[M32] + val[M02] * val[M21] * val[M33] - val[M01] * val[M22] * val[M33];
		FP m02 = val[M02] * val[M13] * val[M31] - val[M03] * val[M12] * val[M31] + val[M03] * val[M11] * val[M32]
		         - val[M01] * val[M13] * val[M32] - val[M02] * val[M11] * val[M33] + val[M01] * val[M12] * val[M33];
		FP m03 = val[M03] * val[M12] * val[M21] - val[M02] * val[M13] * val[M21] - val[M03] * val[M11] * val[M22]
		         + val[M01] * val[M13] * val[M22] + val[M02] * val[M11] * val[M23] - val[M01] * val[M12] * val[M23];
		FP m10 = val[M13] * val[M22] * val[M30] - val[M12] * val[M23] * val[M30] - val[M13] * val[M20] * val[M32]
		         + val[M10] * val[M23] * val[M32] + val[M12] * val[M20] * val[M33] - val[M10] * val[M22] * val[M33];
		FP m11 = val[M02] * val[M23] * val[M30] - val[M03] * val[M22] * val[M30] + val[M03] * val[M20] * val[M32]
		         - val[M00] * val[M23] * val[M32] - val[M02] * val[M20] * val[M33] + val[M00] * val[M22] * val[M33];
		FP m12 = val[M03] * val[M12] * val[M30] - val[M02] * val[M13] * val[M30] - val[M03] * val[M10] * val[M32]
		         + val[M00] * val[M13] * val[M32] + val[M02] * val[M10] * val[M33] - val[M00] * val[M12] * val[M33];
		FP m13 = val[M02] * val[M13] * val[M20] - val[M03] * val[M12] * val[M20] + val[M03] * val[M10] * val[M22]
		         - val[M00] * val[M13] * val[M22] - val[M02] * val[M10] * val[M23] + val[M00] * val[M12] * val[M23];
		FP m20 = val[M11] * val[M23] * val[M30] - val[M13] * val[M21] * val[M30] + val[M13] * val[M20] * val[M31]
		         - val[M10] * val[M23] * val[M31] - val[M11] * val[M20] * val[M33] + val[M10] * val[M21] * val[M33];
		FP m21 = val[M03] * val[M21] * val[M30] - val[M01] * val[M23] * val[M30] - val[M03] * val[M20] * val[M31]
		         + val[M00] * val[M23] * val[M31] + val[M01] * val[M20] * val[M33] - val[M00] * val[M21] * val[M33];
		FP m22 = val[M01] * val[M13] * val[M30] - val[M03] * val[M11] * val[M30] + val[M03] * val[M10] * val[M31]
		         - val[M00] * val[M13] * val[M31] - val[M01] * val[M10] * val[M33] + val[M00] * val[M11] * val[M33];
		FP m23 = val[M03] * val[M11] * val[M20] - val[M01] * val[M13] * val[M20] - val[M03] * val[M10] * val[M21]
		         + val[M00] * val[M13] * val[M21] + val[M01] * val[M10] * val[M23] - val[M00] * val[M11] * val[M23];
		FP m30 = val[M12] * val[M21] * val[M30] - val[M11] * val[M22] * val[M30] - val[M12] * val[M20] * val[M31]
		         + val[M10] * val[M22] * val[M31] + val[M11] * val[M20] * val[M32] - val[M10] * val[M21] * val[M32];
		FP m31 = val[M01] * val[M22] * val[M30] - val[M02] * val[M21] * val[M30] + val[M02] * val[M20] * val[M31]
		         - val[M00] * val[M22] * val[M31] - val[M01] * val[M20] * val[M32] + val[M00] * val[M21] * val[M32];
		FP m32 = val[M02] * val[M11] * val[M30] - val[M01] * val[M12] * val[M30] - val[M02] * val[M10] * val[M31]
		         + val[M00] * val[M12] * val[M31] + val[M01] * val[M10] * val[M32] - val[M00] * val[M11] * val[M32];
		FP m33 = val[M01] * val[M12] * val[M20] - val[M02] * val[M11] * val[M20] + val[M02] * val[M10] * val[M21]
		         - val[M00] * val[M12] * val[M21] - val[M01] * val[M10] * val[M22] + val[M00] * val[M11] * val[M22];
		FP inv_det = (FP) 1.0f / l_det;
		val[M00] = m00 * inv_det;
		val[M10] = m10 * inv_det;
		val[M20] = m20 * inv_det;
		val[M30] = m30 * inv_det;
		val[M01] = m01 * inv_det;
		val[M11] = m11 * inv_det;
		val[M21] = m21 * inv_det;
		val[M31] = m31 * inv_det;
		val[M02] = m02 * inv_det;
		val[M12] = m12 * inv_det;
		val[M22] = m22 * inv_det;
		val[M32] = m32 * inv_det;
		val[M03] = m03 * inv_det;
		val[M13] = m13 * inv_det;
		val[M23] = m23 * inv_det;
		val[M33] = m33 * inv_det;
		return this;
	}

	/** @return The determinant of this matrix */
	public FP det()
	{
		return val[M30] * val[M21] * val[M12] * val[M03] - val[M20] * val[M31] * val[M12] * val[M03]
		                                                 - val[M30] * val[M11] * val[M22] * val[M03] + val[M10] *
		                                                                                             val[M31] *
		                                                                                             val[M22] * val[M03]
		                                                                                             + val[M20] *
		                                                                                             val[M11] *
		                                                                                             val[M32] *
		                                                                                             val[
			                                                                                             M03] - val[
				                                                                                                  M10] *
			                                                                                                  val[M21] *
			                                                                                                  val[M32] *
			                                                                                                  val[M03]
			                                                                                                  - val[
				                                                                                                  M30] *
			                                                                                                  val[M21] *
			                                                                                                  val[M02] *
			                                                                                                  val[
				                                                                                                  M13] + val
				                                                                                                       [
					                                                                                                       M20] *
				                                                                                                       val
					                                                                                                       [M31] *
				                                                                                                       val
					                                                                                                       [M02] *
				                                                                                                       val
					                                                                                                       [M13]
				                                                                                                       + val
					                                                                                                       [M30] *
				                                                                                                       val
					                                                                                                       [M01] *
				                                                                                                       val
					                                                                                                       [M22] *
				                                                                                                       val
					                                                                                                       [M13] - val
					                                                                                                             [
						                                                                                                             M00] *
					                                                                                                             val
						                                                                                                             [M31] *
					                                                                                                             val
						                                                                                                             [M22] *
					                                                                                                             val
						                                                                                                             [M13]
					                                                                                                             - val
						                                                                                                             [M20] *
					                                                                                                             val
						                                                                                                             [M01] *
					                                                                                                             val
						                                                                                                             [M32] *
					                                                                                                             val
						                                                                                                             [M13] + val
						                                                                                                                   [
							                                                                                                                   M00] *
						                                                                                                                   val
							                                                                                                                   [M21] *
						                                                                                                                   val
							                                                                                                                   [M32] *
						                                                                                                                   val
							                                                                                                                   [M13]
						                                                                                                                   + val
							                                                                                                                   [M30] *
						                                                                                                                   val
							                                                                                                                   [M11] *
						                                                                                                                   val
							                                                                                                                   [M02] *
						                                                                                                                   val
							                                                                                                                   [M23] - val
							                                                                                                                         [
								                                                                                                                         M10] *
							                                                                                                                         val
								                                                                                                                         [M31] *
							                                                                                                                         val
								                                                                                                                         [M02] *
							                                                                                                                         val
								                                                                                                                         [M23]
							                                                                                                                         - val
								                                                                                                                         [M30] *
							                                                                                                                         val
								                                                                                                                         [M01] *
							                                                                                                                         val
								                                                                                                                         [M12] *
							                                                                                                                         val
								                                                                                                                         [M23] + val
								                                                                                                                               [
									                                                                                                                               M00] *
								                                                                                                                               val
									                                                                                                                               [M31] *
								                                                                                                                               val
									                                                                                                                               [M12] *
								                                                                                                                               val
									                                                                                                                               [M23]
								                                                                                                                               + val
									                                                                                                                               [M10] *
								                                                                                                                               val
									                                                                                                                               [M01] *
								                                                                                                                               val
									                                                                                                                               [M32] *
								                                                                                                                               val
									                                                                                                                               [M23] - val
									                                                                                                                                     [
										                                                                                                                                     M00] *
									                                                                                                                                     val
										                                                                                                                                     [M11] *
									                                                                                                                                     val
										                                                                                                                                     [M32] *
									                                                                                                                                     val
										                                                                                                                                     [M23]
									                                                                                                                                     - val
										                                                                                                                                     [M20] *
									                                                                                                                                     val
										                                                                                                                                     [M11] *
									                                                                                                                                     val
										                                                                                                                                     [M02] *
									                                                                                                                                     val
										                                                                                                                                     [M33] + val
										                                                                                                                                           [
											                                                                                                                                           M10] *
										                                                                                                                                           val
											                                                                                                                                           [M21] *
										                                                                                                                                           val
											                                                                                                                                           [M02] *
										                                                                                                                                           val
											                                                                                                                                           [M33]
										                                                                                                                                           +
										                                                                                                                                           val
											                                                                                                                                           [M20] *
										                                                                                                                                           val
											                                                                                                                                           [M01] *
										                                                                                                                                           val
											                                                                                                                                           [M12] *
										                                                                                                                                           val
											                                                                                                                                           [M33] - val
											                                                                                                                                                 [
												                                                                                                                                                 M00] *
											                                                                                                                                                 val
												                                                                                                                                                 [M21] *
											                                                                                                                                                 val
												                                                                                                                                                 [M12] *
											                                                                                                                                                 val
												                                                                                                                                                 [M33]
											                                                                                                                                                 -
											                                                                                                                                                 val
												                                                                                                                                                 [M10] *
											                                                                                                                                                 val
												                                                                                                                                                 [M01] *
											                                                                                                                                                 val
												                                                                                                                                                 [M22] *
											                                                                                                                                                 val
												                                                                                                                                                 [M33] +
		       val[M00] * val[M11] * val[M22] * val[M33];
	}

	/** @return The determinant of the 3x3 upper left matrix */
	public FP det3x3()
	{
		return val[M00] * val[M11] * val[M22] + val[M01] * val[M12] * val[M20] + val[M02] * val[M10] * val[M21]
		       - val[M00] * val[M12] * val[M21] - val[M01] * val[M10] * val[M22] - val[M02] * val[M11] * val[M20];
	}

	/** Sets the matrix to a projection matrix with a near- and far plane, a field of view in degrees and an aspect ratio. Note
	 * that the field of view specified is the angle in degrees for the height, the field of view for the width will be calculated
	 * according to the aspect ratio.
	 * @param near The near plane
	 * @param far The far plane
	 * @param fovy The field of view of the height in degrees
	 * @param aspectRatio The "width over height" aspect ratio
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToProjection(FP near, FP far, FP fovy, FP aspectRatio)
	{
		idt();
		FP l_fd = (FP) 1.0 / DGMath.Tan((fovy * (DGMath.PI / (FP) 180)) / (FP) 2.0);
		FP l_a1 = (far + near) / (near - far);
		FP l_a2 = ((FP) 2 * far * near) / (near - far);
		val[M00] = l_fd / aspectRatio;
		val[M10] = (FP) 0;
		val[M20] = (FP) 0;
		val[M30] = (FP) 0;
		val[M01] = (FP) 0;
		val[M11] = l_fd;
		val[M21] = (FP) 0;
		val[M31] = (FP) 0;
		val[M02] = (FP) 0;
		val[M12] = (FP) 0;
		val[M22] = l_a1;
		val[M32] = (FP) (-1);
		val[M03] = (FP) 0;
		val[M13] = (FP) 0;
		val[M23] = l_a2;
		val[M33] = (FP) 0;
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
	public DGMatrix4x4 setToProjection(FP left, FP right, FP bottom, FP top, FP near, FP far)
	{
		FP x = (FP) 2.0f * near / (right - left);
		FP y = (FP) 2.0f * near / (top - bottom);
		FP a = (right + left) / (right - left);
		FP b = (top + bottom) / (top - bottom);
		FP l_a1 = (far + near) / (near - far);
		FP l_a2 = ((FP) 2 * far * near) / (near - far);
		val[M00] = x;
		val[M10] = (FP) 0;
		val[M20] = (FP) 0;
		val[M30] = (FP) 0;
		val[M01] = (FP) 0;
		val[M11] = y;
		val[M21] = (FP) 0;
		val[M31] = (FP) 0;
		val[M02] = a;
		val[M12] = b;
		val[M22] = l_a1;
		val[M32] = (FP) (-1);
		val[M03] = (FP) 0;
		val[M13] = (FP) 0;
		val[M23] = l_a2;
		val[M33] = (FP) 0;
		return this;
	}

	/** Sets this matrix to an orthographic projection matrix with the origin at (x,y) extending by width and height. The near
	 * plane is set to 0, the far plane is set to 1.
	 * @param x The x-coordinate of the origin
	 * @param y The y-coordinate of the origin
	 * @param width The width
	 * @param height The height
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToOrtho2D(FP x, FP y, FP width, FP height)
	{
		setToOrtho(x, x + width, y, y + height, (FP) 0, (FP) 1);
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
	public DGMatrix4x4 setToOrtho2D(FP x, FP y, FP width, FP height, FP near, FP far)
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
	public DGMatrix4x4 setToOrtho(FP left, FP right, FP bottom, FP top, FP near, FP far)
	{
		FP x_orth = (FP) 2 / (right - left);
		FP y_orth = (FP) 2 / (top - bottom);
		FP z_orth = (FP) (-2) / (far - near);

		FP tx = -(right + left) / (right - left);
		FP ty = -(top + bottom) / (top - bottom);
		FP tz = -(far + near) / (far - near);

		val[M00] = x_orth;
		val[M10] = (FP) 0;
		val[M20] = (FP) 0;
		val[M30] = (FP) 0;
		val[M01] = (FP) 0;
		val[M11] = y_orth;
		val[M21] = (FP) 0;
		val[M31] = (FP) 0;
		val[M02] = (FP) 0;
		val[M12] = (FP) 0;
		val[M22] = z_orth;
		val[M32] = (FP) 0;
		val[M03] = tx;
		val[M13] = ty;
		val[M23] = tz;
		val[M33] = (FP) 1;
		return this;
	}

	/** Sets the 4th column to the translation vector.
	 * @param vector The translation vector
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setTranslation(FPVector3 vector)
	{
		val[M03] = vector.x;
		val[M13] = vector.y;
		val[M23] = vector.z;
		return this;
	}

	/** Sets the 4th column to the translation vector.
	 * @param x The X coordinate of the translation vector
	 * @param y The Y coordinate of the translation vector
	 * @param z The Z coordinate of the translation vector
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setTranslation(FP x, FP y, FP z)
	{
		val[M03] = x;
		val[M13] = y;
		val[M23] = z;
		return this;
	}

	/** Sets this matrix to a translation matrix, overwriting it first by an identity matrix and then setting the 4th column to the
	 * translation vector.
	 * @param vector The translation vector
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToTranslation(FPVector3 vector)
	{
		idt();
		val[M03] = vector.x;
		val[M13] = vector.y;
		val[M23] = vector.z;
		return this;
	}

	/** Sets this matrix to a translation matrix, overwriting it first by an identity matrix and then setting the 4th column to the
	 * translation vector.
	 * @param x The x-component of the translation vector.
	 * @param y The y-component of the translation vector.
	 * @param z The z-component of the translation vector.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToTranslation(FP x, FP y, FP z)
	{
		idt();
		val[M03] = x;
		val[M13] = y;
		val[M23] = z;
		return this;
	}

	/** Sets this matrix to a translation and scaling matrix by first overwriting it with an identity and then setting the
	 * translation vector in the 4th column and the scaling vector in the diagonal.
	 * @param translation The translation vector
	 * @param scaling The scaling vector
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToTranslationAndScaling(FPVector3 translation, FPVector3 scaling)
	{
		idt();
		val[M03] = translation.x;
		val[M13] = translation.y;
		val[M23] = translation.z;
		val[M00] = scaling.x;
		val[M11] = scaling.y;
		val[M22] = scaling.z;
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
	public DGMatrix4x4 setToTranslationAndScaling(FP translationX, FP translationY, FP translationZ, FP scalingX,
		FP scalingY, FP scalingZ)
	{
		idt();
		val[M03] = translationX;
		val[M13] = translationY;
		val[M23] = translationZ;
		val[M00] = scalingX;
		val[M11] = scalingY;
		val[M22] = scalingZ;
		return this;
	}

	/** Sets the matrix to a rotation matrix around the given axis.
	 * @param axis The axis
	 * @param degrees The angle in degrees
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToRotation(FPVector3 axis, FP degrees)
	{
		if (degrees == (FP) 0)
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
	public DGMatrix4x4 setToRotationRad(FPVector3 axis, FP radians)
	{
		if (radians == (FP) 0)
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
	public DGMatrix4x4 setToRotation(FP axisX, FP axisY, FP axisZ, FP degrees)
	{
		if (degrees == (FP) 0)
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
	public DGMatrix4x4 setToRotationRad(FP axisX, FP axisY, FP axisZ, FP radians)
	{
		if (radians == (FP) 0)
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
	public DGMatrix4x4 setToRotation(FPVector3 v1, FPVector3 v2)
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
	public DGMatrix4x4 setToRotation(FP x1, FP y1, FP z1, FP x2, FP y2, FP z2)
	{
		return set(quat.setFromCross(x1, y1, z1, x2, y2, z2));
	}

	/** Sets this matrix to a rotation matrix from the given euler angles.
	 * @param yaw the yaw in degrees
	 * @param pitch the pitch in degrees
	 * @param roll the roll in degrees
	 * @return This matrix */
	public DGMatrix4x4 setFromEulerAngles(FP yaw, FP pitch, FP roll)
	{
		quat.setEulerAngles(yaw, pitch, roll);
		return set(quat);
	}

	/** Sets this matrix to a rotation matrix from the given euler angles.
	 * @param yaw the yaw in radians
	 * @param pitch the pitch in radians
	 * @param roll the roll in radians
	 * @return This matrix */
	public DGMatrix4x4 setFromEulerAnglesRad(FP yaw, FP pitch, FP roll)
	{
		quat.setEulerAnglesRad(yaw, pitch, roll);
		return set(quat);
	}

	/** Sets this matrix to a scaling matrix
	 * @param vector The scaling vector
	 * @return This matrix for chaining. */
	public DGMatrix4x4 setToScaling(FPVector3 vector)
	{
		idt();
		val[M00] = vector.x;
		val[M11] = vector.y;
		val[M22] = vector.z;
		return this;
	}

	/** Sets this matrix to a scaling matrix
	 * @param x The x-component of the scaling vector
	 * @param y The y-component of the scaling vector
	 * @param z The z-component of the scaling vector
	 * @return This matrix for chaining. */
	public DGMatrix4x4 setToScaling(FP x, FP y, FP z)
	{
		idt();
		val[M00] = x;
		val[M11] = y;
		val[M22] = z;
		return this;
	}

	/** Sets the matrix to a look at matrix with a direction and an up vector. Multiply with a translation matrix to get a camera
	 * model view matrix.
	 * @param direction The direction vector
	 * @param up The up vector
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 setToLookAt(FPVector3 direction, FPVector3 up)
	{
		l_vez.set(direction).nor();
		l_vex.set(direction).crs(up).nor();
		l_vey.set(l_vex).crs(l_vez).nor();
		idt();
		val[M00] = l_vex.x;
		val[M01] = l_vex.y;
		val[M02] = l_vex.z;
		val[M10] = l_vey.x;
		val[M11] = l_vey.y;
		val[M12] = l_vey.z;
		val[M20] = -l_vez.x;
		val[M21] = -l_vez.y;
		val[M22] = -l_vez.z;
		return this;
	}

	/** Sets this matrix to a look at matrix with the given position, target and up vector.
	 * @param position the position
	 * @param target the target
	 * @param up the up vector
	 * @return This matrix */
	public DGMatrix4x4 setToLookAt(FPVector3 position, FPVector3 target, FPVector3 up)
	{
		tmpVec.set(target).sub(position);
		setToLookAt(tmpVec, up);
		mul(tmpMat.setToTranslation(-position.x, -position.y, -position.z));
		return this;
	}

	public DGMatrix4x4 setToWorld(FPVector3 position, FPVector3 forward, FPVector3 up)
	{
		tmpForward.set(forward).nor();
		right.set(tmpForward).crs(up).nor();
		tmpUp.set(right).crs(tmpForward).nor();
		set(right, tmpUp, tmpForward.scl(-(FP) 1), position);
		return this;
	}

	/** Linearly interpolates between this matrix and the given matrix mixing by alpha
	 * @param matrix the matrix
	 * @param alpha the alpha value in the range [0,1]
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 lerp(DGMatrix4x4 matrix, FP alpha)
	{
		for (int i = 0; i < 16; i++)
			val[i] = val[i] * ((FP) 1 - alpha) + matrix.val[i] * alpha;
		return this;
	}

	/** Averages the given transform with this one and stores the result in this matrix. Translations and scales are lerped while
	 * rotations are slerped.
	 * @param other The other transform
	 * @param w Weight of this transform; weight of the other transform is (1 - w)
	 * @return This matrix for chaining */
	public DGMatrix4x4 avg(DGMatrix4x4 other, FP w)
	{
		getScale(tmpVec);
		other.getScale(tmpForward);

		getRotation(quat);
		other.getRotation(quat2);

		getTranslation(tmpUp);
		other.getTranslation(right);

		setToScaling(tmpVec.scl(w).add(tmpForward.scl((FP) 1 - w)));
		rotate(quat.slerp(quat2, (FP) 1 - w));
		setTranslation(tmpUp.scl(w).add(right.scl((FP) 1 - w)));
		return this;
	}

	/** Averages the given transforms and stores the result in this matrix. Translations and scales are lerped while rotations are
	 * slerped. Does not destroy the data contained in t.
	 * @param t List of transforms
	 * @return This matrix for chaining */
	public DGMatrix4x4 avg(DGMatrix4x4[] t)
	{
		FP w = (FP) 1.0f / (FP) t.Length;

		tmpVec.set(t[0].getScale(tmpUp).scl(w));
		quat.set(t[0].getRotation(quat2).exp(w));
		tmpForward.set(t[0].getTranslation(tmpUp).scl(w));

		for (int i = 1; i < t.Length; i++)
		{
			tmpVec.add(t[i].getScale(tmpUp).scl(w));
			quat.mul(t[i].getRotation(quat2).exp(w));
			tmpForward.add(t[i].getTranslation(tmpUp).scl(w));
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
	public DGMatrix4x4 avg(DGMatrix4x4[] t, FP[] w)
	{
		tmpVec.set(t[0].getScale(tmpUp).scl(w[0]));
		quat.set(t[0].getRotation(quat2).exp(w[0]));
		tmpForward.set(t[0].getTranslation(tmpUp).scl(w[0]));

		for (int i = 1; i < t.Length; i++)
		{
			tmpVec.add(t[i].getScale(tmpUp).scl(w[i]));
			quat.mul(t[i].getRotation(quat2).exp(w[i]));
			tmpForward.add(t[i].getTranslation(tmpUp).scl(w[i]));
		}

		quat.nor();

		setToScaling(tmpVec);
		rotate(quat);
		setTranslation(tmpForward);
		return this;
	}

	/** Sets this matrix to the given 3x3 matrix. The third column of this matrix is set to (0,0,1,0).
	 * @param mat the matrix */
	public DGMatrix4x4 set(FPMatrix3x3 mat)
	{
		val[0] = mat.val[0];
		val[1] = mat.val[1];
		val[2] = mat.val[2];
		val[3] = (FP) 0;
		val[4] = mat.val[3];
		val[5] = mat.val[4];
		val[6] = mat.val[5];
		val[7] = (FP) 0;
		val[8] = (FP) 0;
		val[9] = (FP) 0;
		val[10] = (FP) 1;
		val[11] = (FP) 0;
		val[12] = mat.val[6];
		val[13] = mat.val[7];
		val[14] = (FP) 0;
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
	public DGMatrix4x4 set(FPAffine2 affine)
	{
		val[M00] = affine.m00;
		val[M10] = affine.m10;
		val[M20] = (FP) 0;
		val[M30] = (FP) 0;
		val[M01] = affine.m01;
		val[M11] = affine.m11;
		val[M21] = (FP) 0;
		val[M31] = (FP) 0;
		val[M02] = (FP) 0;
		val[M12] = (FP) 0;
		val[M22] = (FP) 1;
		val[M32] = (FP) 0;
		val[M03] = affine.m02;
		val[M13] = affine.m12;
		val[M23] = (FP) 0;
		val[M33] = (FP) 1;
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
	public DGMatrix4x4 setAsAffine(FPAffine2 affine)
	{
		val[M00] = affine.m00;
		val[M10] = affine.m10;
		val[M01] = affine.m01;
		val[M11] = affine.m11;
		val[M03] = affine.m02;
		val[M13] = affine.m12;
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
		val[M00] = mat.val[M00];
		val[M10] = mat.val[M10];
		val[M01] = mat.val[M01];
		val[M11] = mat.val[M11];
		val[M03] = mat.val[M03];
		val[M13] = mat.val[M13];
		return this;
	}

	public DGMatrix4x4 scl(FPVector3 scale)
	{
		val[M00] *= scale.x;
		val[M11] *= scale.y;
		val[M22] *= scale.z;
		return this;
	}

	public DGMatrix4x4 scl(FP x, FP y, FP z)
	{
		val[M00] *= x;
		val[M11] *= y;
		val[M22] *= z;
		return this;
	}

	public DGMatrix4x4 scl(FP scale)
	{
		val[M00] *= scale;
		val[M11] *= scale;
		val[M22] *= scale;
		return this;
	}

	public FPVector3 getTranslation(FPVector3 position)
	{
		position.x = val[M03];
		position.y = val[M13];
		position.z = val[M23];
		return position;
	}

	/** Gets the rotation of this matrix.
	 * @param rotation The {@link Quaternion} to receive the rotation
	 * @param normalizeAxes True to normalize the axes, necessary when the matrix might also include scaling.
	 * @return The provided {@link Quaternion} for chaining. */
	public FPQuaternion getRotation(FPQuaternion rotation, bool normalizeAxes)
	{
		return rotation.setFromMatrix(normalizeAxes, this);
	}

	/** Gets the rotation of this matrix.
	 * @param rotation The {@link Quaternion} to receive the rotation
	 * @return The provided {@link Quaternion} for chaining. */
	public FPQuaternion getRotation(FPQuaternion rotation)
	{
		return rotation.setFromMatrix(this);
	}

	/** @return the squared scale factor on the X axis */
	public FP getScaleXSquared()
	{
		return val[M00] * val[M00] + val[M01] * val[M01] + val[M02] * val[M02];
	}

	/** @return the squared scale factor on the Y axis */
	public FP getScaleYSquared()
	{
		return val[M10] * val[M10] + val[M11] * val[M11] + val[M12] * val[M12];
	}

	/** @return the squared scale factor on the Z axis */
	public FP getScaleZSquared()
	{
		return val[M20] * val[M20] + val[M21] * val[M21] + val[M22] * val[M22];
	}

	/** @return the scale factor on the X axis (non-negative) */
	public FP getScaleX()
	{
		return (DGMath.IsZero(val[M01]) && DGMath.IsZero(val[M02]))
			? DGMath.Abs(val[M00])
			: DGMath.Sqrt(getScaleXSquared());
	}

	/** @return the scale factor on the Y axis (non-negative) */
	public FP getScaleY()
	{
		return (DGMath.IsZero(val[M10]) && DGMath.IsZero(val[M12]))
			? DGMath.Abs(val[M11])
			: DGMath.Sqrt(getScaleYSquared());
	}

	/** @return the scale factor on the X axis (non-negative) */
	public FP getScaleZ()
	{
		return (DGMath.IsZero(val[M20]) && DGMath.IsZero(val[M21]))
			? DGMath.Abs(val[M22])
			: DGMath.Sqrt(getScaleZSquared());
	}

	/** @param scale The vector which will receive the (non-negative) scale components on each axis.
	 * @return The provided vector for chaining. */
	public FPVector3 getScale(FPVector3 scale)
	{
		return scale.set(getScaleX(), getScaleY(), getScaleZ());
	}

	/** removes the translational part and transposes the matrix. */
	public DGMatrix4x4 toNormalMatrix()
	{
		val[M03] = (FP) 0;
		val[M13] = (FP) 0;
		val[M23] = (FP) 0;
		return inv().tra();
	}

	public override string ToString()
	{
		return "[" + val[M00] + "|" + val[M01] + "|" + val[M02] + "|" + val[M03] + "]\n" //
		       + "[" + val[M10] + "|" + val[M11] + "|" + val[M12] + "|" + val[M13] + "]\n" //
		       + "[" + val[M20] + "|" + val[M21] + "|" + val[M22] + "|" + val[M23] + "]\n" //
		       + "[" + val[M30] + "|" + val[M31] + "|" + val[M32] + "|" + val[M33] + "]\n";
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
	public static void mul(FP[] mata, FP[] matb)
	{
		FP m00 = mata[M00] * matb[M00] + mata[M01] * matb[M10] + mata[M02] * matb[M20] + mata[M03] * matb[M30];
		FP m01 = mata[M00] * matb[M01] + mata[M01] * matb[M11] + mata[M02] * matb[M21] + mata[M03] * matb[M31];
		FP m02 = mata[M00] * matb[M02] + mata[M01] * matb[M12] + mata[M02] * matb[M22] + mata[M03] * matb[M32];
		FP m03 = mata[M00] * matb[M03] + mata[M01] * matb[M13] + mata[M02] * matb[M23] + mata[M03] * matb[M33];
		FP m10 = mata[M10] * matb[M00] + mata[M11] * matb[M10] + mata[M12] * matb[M20] + mata[M13] * matb[M30];
		FP m11 = mata[M10] * matb[M01] + mata[M11] * matb[M11] + mata[M12] * matb[M21] + mata[M13] * matb[M31];
		FP m12 = mata[M10] * matb[M02] + mata[M11] * matb[M12] + mata[M12] * matb[M22] + mata[M13] * matb[M32];
		FP m13 = mata[M10] * matb[M03] + mata[M11] * matb[M13] + mata[M12] * matb[M23] + mata[M13] * matb[M33];
		FP m20 = mata[M20] * matb[M00] + mata[M21] * matb[M10] + mata[M22] * matb[M20] + mata[M23] * matb[M30];
		FP m21 = mata[M20] * matb[M01] + mata[M21] * matb[M11] + mata[M22] * matb[M21] + mata[M23] * matb[M31];
		FP m22 = mata[M20] * matb[M02] + mata[M21] * matb[M12] + mata[M22] * matb[M22] + mata[M23] * matb[M32];
		FP m23 = mata[M20] * matb[M03] + mata[M21] * matb[M13] + mata[M22] * matb[M23] + mata[M23] * matb[M33];
		FP m30 = mata[M30] * matb[M00] + mata[M31] * matb[M10] + mata[M32] * matb[M20] + mata[M33] * matb[M30];
		FP m31 = mata[M30] * matb[M01] + mata[M31] * matb[M11] + mata[M32] * matb[M21] + mata[M33] * matb[M31];
		FP m32 = mata[M30] * matb[M02] + mata[M31] * matb[M12] + mata[M32] * matb[M22] + mata[M33] * matb[M32];
		FP m33 = mata[M30] * matb[M03] + mata[M31] * matb[M13] + mata[M32] * matb[M23] + mata[M33] * matb[M33];
		mata[M00] = m00;
		mata[M10] = m10;
		mata[M20] = m20;
		mata[M30] = m30;
		mata[M01] = m01;
		mata[M11] = m11;
		mata[M21] = m21;
		mata[M31] = m31;
		mata[M02] = m02;
		mata[M12] = m12;
		mata[M22] = m22;
		mata[M32] = m32;
		mata[M03] = m03;
		mata[M13] = m13;
		mata[M23] = m23;
		mata[M33] = m33;
	}

	/** Multiplies the vector with the given matrix. The matrix array is assumed to hold a 4x4 column major matrix as you can get
	 * from {@link Matrix4#val}. The vector array is assumed to hold a 3-component vector, with x being the first element, y being
	 * the second and z being the last component. The result is stored in the vector array. This is the same as
	 * {@link Vector3#mul(Matrix4)}.
	 * @param mat the matrix
	 * @param vec the vector. */
	public static void mulVec(FP[] mat, FP[] vec)
	{
		FP x = vec[0] * mat[M00] + vec[1] * mat[M01] + vec[2] * mat[M02] + mat[M03];
		FP y = vec[0] * mat[M10] + vec[1] * mat[M11] + vec[2] * mat[M12] + mat[M13];
		FP z = vec[0] * mat[M20] + vec[1] * mat[M21] + vec[2] * mat[M22] + mat[M23];
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
	public static void prj(FP[] mat, FP[] vec)
	{
		FP inv_w = (FP) 1.0f / (vec[0] * mat[M30] + vec[1] * mat[M31] + vec[2] * mat[M32] + mat[M33]);
		FP x = (vec[0] * mat[M00] + vec[1] * mat[M01] + vec[2] * mat[M02] + mat[M03]) * inv_w;
		FP y = (vec[0] * mat[M10] + vec[1] * mat[M11] + vec[2] * mat[M12] + mat[M13]) * inv_w;
		FP z = (vec[0] * mat[M20] + vec[1] * mat[M21] + vec[2] * mat[M22] + mat[M23]) * inv_w;
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
	public static void rot(FP[] mat, FP[] vec)
	{
		FP x = vec[0] * mat[M00] + vec[1] * mat[M01] + vec[2] * mat[M02];
		FP y = vec[0] * mat[M10] + vec[1] * mat[M11] + vec[2] * mat[M12];
		FP z = vec[0] * mat[M20] + vec[1] * mat[M21] + vec[2] * mat[M22];
		vec[0] = x;
		vec[1] = y;
		vec[2] = z;
	}

	/** Computes the inverse of the given matrix. The matrix array is assumed to hold a 4x4 column major matrix as you can get from
	 * {@link Matrix4#val}.
	 * @param values the matrix values.
	 * @return false in case the inverse could not be calculated, true otherwise. */
	public static bool inv(FP[] values)
	{
		FP l_det = det(values);
		if (l_det == (FP) 0) return false;
		FP m00 = values[M12] * values[M23] * values[M31] - values[M13] * values[M22] * values[M31]
		         + values[M13] * values[M21] * values[M32] - values[M11] * values[M23] * values[M32]
		                                                   - values[M12] * values[M21] * values[M33] +
		         values[M11] * values[M22] * values[M33];
		FP m01 = values[M03] * values[M22] * values[M31] - values[M02] * values[M23] * values[M31]
		                                                 - values[M03] * values[M21] * values[M32] + values[M01] *
		                                                                                           values[M23] *
		                                                                                           values[M32]
		                                                                                           + values[M02] *
		                                                                                           values[M21] *
		                                                                                           values[M33] -
		         values[M01] * values[M22] * values[M33];
		FP m02 = values[M02] * values[M13] * values[M31] - values[M03] * values[M12] * values[M31]
		         + values[M03] * values[M11] * values[M32] - values[M01] * values[M13] * values[M32]
		                                                   - values[M02] * values[M11] * values[M33] +
		         values[M01] * values[M12] * values[M33];
		FP m03 = values[M03] * values[M12] * values[M21] - values[M02] * values[M13] * values[M21]
		                                                 - values[M03] * values[M11] * values[M22] + values[M01] *
		                                                                                           values[M13] *
		                                                                                           values[M22]
		                                                                                           + values[M02] *
		                                                                                           values[M11] *
		                                                                                           values[M23] -
		         values[M01] * values[M12] * values[M23];
		FP m10 = values[M13] * values[M22] * values[M30] - values[M12] * values[M23] * values[M30]
		                                                 - values[M13] * values[M20] * values[M32] + values[M10] *
		                                                                                           values[M23] *
		                                                                                           values[M32]
		                                                                                           + values[M12] *
		                                                                                           values[M20] *
		                                                                                           values[M33] -
		         values[M10] * values[M22] * values[M33];
		FP m11 = values[M02] * values[M23] * values[M30] - values[M03] * values[M22] * values[M30]
		         + values[M03] * values[M20] * values[M32] - values[M00] * values[M23] * values[M32]
		                                                   - values[M02] * values[M20] * values[M33] +
		         values[M00] * values[M22] * values[M33];
		FP m12 = values[M03] * values[M12] * values[M30] - values[M02] * values[M13] * values[M30]
		                                                 - values[M03] * values[M10] * values[M32] + values[M00] *
		                                                                                           values[M13] *
		                                                                                           values[M32]
		                                                                                           + values[M02] *
		                                                                                           values[M10] *
		                                                                                           values[M33] -
		         values[M00] * values[M12] * values[M33];
		FP m13 = values[M02] * values[M13] * values[M20] - values[M03] * values[M12] * values[M20]
		         + values[M03] * values[M10] * values[M22] - values[M00] * values[M13] * values[M22]
		                                                   - values[M02] * values[M10] * values[M23] +
		         values[M00] * values[M12] * values[M23];
		FP m20 = values[M11] * values[M23] * values[M30] - values[M13] * values[M21] * values[M30]
		         + values[M13] * values[M20] * values[M31] - values[M10] * values[M23] * values[M31]
		                                                   - values[M11] * values[M20] * values[M33] +
		         values[M10] * values[M21] * values[M33];
		FP m21 = values[M03] * values[M21] * values[M30] - values[M01] * values[M23] * values[M30]
		                                                 - values[M03] * values[M20] * values[M31] + values[M00] *
		                                                                                           values[M23] *
		                                                                                           values[M31]
		                                                                                           + values[M01] *
		                                                                                           values[M20] *
		                                                                                           values[M33] -
		         values[M00] * values[M21] * values[M33];
		FP m22 = values[M01] * values[M13] * values[M30] - values[M03] * values[M11] * values[M30]
		         + values[M03] * values[M10] * values[M31] - values[M00] * values[M13] * values[M31]
		                                                   - values[M01] * values[M10] * values[M33] +
		         values[M00] * values[M11] * values[M33];
		FP m23 = values[M03] * values[M11] * values[M20] - values[M01] * values[M13] * values[M20]
		                                                 - values[M03] * values[M10] * values[M21] + values[M00] *
		                                                                                           values[M13] *
		                                                                                           values[M21]
		                                                                                           + values[M01] *
		                                                                                           values[M10] *
		                                                                                           values[M23] -
		         values[M00] * values[M11] * values[M23];
		FP m30 = values[M12] * values[M21] * values[M30] - values[M11] * values[M22] * values[M30]
		                                                 - values[M12] * values[M20] * values[M31] + values[M10] *
		                                                                                           values[M22] *
		                                                                                           values[M31]
		                                                                                           + values[M11] *
		                                                                                           values[M20] *
		                                                                                           values[M32] -
		         values[M10] * values[M21] * values[M32];
		FP m31 = values[M01] * values[M22] * values[M30] - values[M02] * values[M21] * values[M30]
		         + values[M02] * values[M20] * values[M31] - values[M00] * values[M22] * values[M31]
		                                                   - values[M01] * values[M20] * values[M32] +
		         values[M00] * values[M21] * values[M32];
		FP m32 = values[M02] * values[M11] * values[M30] - values[M01] * values[M12] * values[M30]
		                                                 - values[M02] * values[M10] * values[M31] + values[M00] *
		                                                                                           values[M12] *
		                                                                                           values[M31]
		                                                                                           + values[M01] *
		                                                                                           values[M10] *
		                                                                                           values[M32] -
		         values[M00] * values[M11] * values[M32];
		FP m33 = values[M01] * values[M12] * values[M20] - values[M02] * values[M11] * values[M20]
		         + values[M02] * values[M10] * values[M21] - values[M00] * values[M12] * values[M21]
		                                                   - values[M01] * values[M10] * values[M22] +
		         values[M00] * values[M11] * values[M22];
		FP inv_det = (FP) 1.0f / l_det;
		values[M00] = m00 * inv_det;
		values[M10] = m10 * inv_det;
		values[M20] = m20 * inv_det;
		values[M30] = m30 * inv_det;
		values[M01] = m01 * inv_det;
		values[M11] = m11 * inv_det;
		values[M21] = m21 * inv_det;
		values[M31] = m31 * inv_det;
		values[M02] = m02 * inv_det;
		values[M12] = m12 * inv_det;
		values[M22] = m22 * inv_det;
		values[M32] = m32 * inv_det;
		values[M03] = m03 * inv_det;
		values[M13] = m13 * inv_det;
		values[M23] = m23 * inv_det;
		values[M33] = m33 * inv_det;
		return true;
	}

	/** Computes the determinante of the given matrix. The matrix array is assumed to hold a 4x4 column major matrix as you can get
	 * from {@link Matrix4#val}.
	 * @param values the matrix values.
	 * @return the determinante. */
	public static FP det(FP[] values)
	{
		return values[M30] * values[M21] * values[M12] * values[M03] - values[M20] * values[M31] * values[M12] *
		                                                             values[M03]
		                                                             - values[M30] * values[M11] * values[M22] *
		                                                             values[M03] + values[M10] * values[M31] *
		                                                                         values[M22] * values[M03]
		                                                                         + values[M20] * values[M11] *
		                                                                         values[M32] *
		                                                                         values[M03] - values[M10] *
		                                                                                     values[M21] * values[M32] *
		                                                                                     values[M03]
		                                                                                     - values[M30] *
		                                                                                     values[M21] * values[M02] *
		                                                                                     values[M13] + values[M20] *
		                                                                                                 values[M31] *
		                                                                                                 values[M02] *
		                                                                                                 values[M13]
		                                                                                                 + values[M30] *
		                                                                                                 values[M01] *
		                                                                                                 values[M22] *
		                                                                                                 values
			                                                                                                 [M13] - values
			                                                                                                       [
				                                                                                                       M00] *
			                                                                                                       values
				                                                                                                       [M31] *
			                                                                                                       values
				                                                                                                       [M22] *
			                                                                                                       values
				                                                                                                       [M13]
			                                                                                                       - values
				                                                                                                       [M20] *
			                                                                                                       values
				                                                                                                       [M01] *
			                                                                                                       values
				                                                                                                       [M32] *
			                                                                                                       values
				                                                                                                       [M13] + values
				                                                                                                             [
					                                                                                                             M00] *
				                                                                                                             values
					                                                                                                             [M21] *
				                                                                                                             values
					                                                                                                             [M32] *
				                                                                                                             values
					                                                                                                             [M13]
				                                                                                                             + values
					                                                                                                             [M30] *
				                                                                                                             values
					                                                                                                             [M11] *
				                                                                                                             values
					                                                                                                             [M02] *
				                                                                                                             values
					                                                                                                             [M23] - values
					                                                                                                                   [
						                                                                                                                   M10] *
					                                                                                                                   values
						                                                                                                                   [M31] *
					                                                                                                                   values
						                                                                                                                   [M02] *
					                                                                                                                   values
						                                                                                                                   [M23]
					                                                                                                                   - values
						                                                                                                                   [M30] *
					                                                                                                                   values
						                                                                                                                   [M01] *
					                                                                                                                   values
						                                                                                                                   [M12] *
					                                                                                                                   values
						                                                                                                                   [M23] + values
						                                                                                                                         [
							                                                                                                                         M00] *
						                                                                                                                         values
							                                                                                                                         [M31] *
						                                                                                                                         values
							                                                                                                                         [M12] *
						                                                                                                                         values
							                                                                                                                         [M23]
						                                                                                                                         + values
							                                                                                                                         [M10] *
						                                                                                                                         values
							                                                                                                                         [M01] *
						                                                                                                                         values
							                                                                                                                         [M32] *
						                                                                                                                         values
							                                                                                                                         [M23] - values
							                                                                                                                               [
								                                                                                                                               M00] *
							                                                                                                                               values
								                                                                                                                               [M11] *
							                                                                                                                               values
								                                                                                                                               [M32] *
							                                                                                                                               values
								                                                                                                                               [M23]
							                                                                                                                               - values
								                                                                                                                               [M20] *
							                                                                                                                               values
								                                                                                                                               [M11] *
							                                                                                                                               values
								                                                                                                                               [M02] *
							                                                                                                                               values
								                                                                                                                               [M33] + values
								                                                                                                                                     [
									                                                                                                                                     M10] *
								                                                                                                                                     values
									                                                                                                                                     [M21] *
								                                                                                                                                     values
									                                                                                                                                     [M02] *
								                                                                                                                                     values
									                                                                                                                                     [M33]
								                                                                                                                                     + values
									                                                                                                                                     [M20] *
								                                                                                                                                     values
									                                                                                                                                     [M01] *
								                                                                                                                                     values
									                                                                                                                                     [M12] *
								                                                                                                                                     values
									                                                                                                                                     [M33] - values
									                                                                                                                                           [
										                                                                                                                                           M00] *
									                                                                                                                                           values
										                                                                                                                                           [M21] *
									                                                                                                                                           values
										                                                                                                                                           [M12] *
									                                                                                                                                           values
										                                                                                                                                           [M33]
									                                                                                                                                           -
									                                                                                                                                           values
										                                                                                                                                           [M10] *
									                                                                                                                                           values
										                                                                                                                                           [M01] *
									                                                                                                                                           values
										                                                                                                                                           [M22] *
									                                                                                                                                           values
										                                                                                                                                           [M33] +
		       values[M00] * values[M11] * values[M22] * values[M33];
	}

	/** Postmultiplies this matrix by a translation matrix. Postmultiplication is also used by OpenGL ES'
	 * glTranslate/glRotate/glScale
	 * @param translation
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 translate(FPVector3 translation)
	{
		return translate(translation.x, translation.y, translation.z);
	}

	/** Postmultiplies this matrix by a translation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param x Translation in the x-axis.
	 * @param y Translation in the y-axis.
	 * @param z Translation in the z-axis.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 translate(FP x, FP y, FP z)
	{
		val[M03] += val[M00] * x + val[M01] * y + val[M02] * z;
		val[M13] += val[M10] * x + val[M11] * y + val[M12] * z;
		val[M23] += val[M20] * x + val[M21] * y + val[M22] * z;
		val[M33] += val[M30] * x + val[M31] * y + val[M32] * z;
		return this;
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param axis The vector axis to rotate around.
	 * @param degrees The angle in degrees.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 rotate(FPVector3 axis, FP degrees)
	{
		if (degrees == (FP) 0) return this;
		quat.set(axis, degrees);
		return rotate(quat);
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param axis The vector axis to rotate around.
	 * @param radians The angle in radians.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 rotateRad(FPVector3 axis, FP radians)
	{
		if (radians == (FP) 0) return this;
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
	public DGMatrix4x4 rotate(FP axisX, FP axisY, FP axisZ, FP degrees)
	{
		if (degrees == (FP) 0) return this;
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
	public DGMatrix4x4 rotateRad(FP axisX, FP axisY, FP axisZ, FP radians)
	{
		if (radians == (FP) 0) return this;
		quat.setFromAxisRad(axisX, axisY, axisZ, radians);
		return rotate(quat);
	}

	/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param rotation
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 rotate(FPQuaternion rotation)
	{
		FP x = rotation.x;
		FP y = rotation.y;
		FP z = rotation.z;
		FP w = rotation.w;
		FP xx = x * x;
		FP xy = x * y;
		FP xz = x * z;
		FP xw = x * w;
		FP yy = y * y;
		FP yz = y * z;
		FP yw = y * w;
		FP zz = z * z;
		FP zw = z * w;
		// Set matrix from quaternion
		FP r00 = (FP) 1 - (FP) 2 * (yy + zz);
		FP r01 = (FP) 2 * (xy - zw);
		FP r02 = (FP) 2 * (xz + yw);
		FP r10 = (FP) 2 * (xy + zw);
		FP r11 = (FP) 1 - (FP) 2 * (xx + zz);
		FP r12 = (FP) 2 * (yz - xw);
		FP r20 = (FP) 2 * (xz - yw);
		FP r21 = (FP) 2 * (yz + xw);
		FP r22 = (FP) 1 - (FP) 2 * (xx + yy);
		FP m00 = val[M00] * r00 + val[M01] * r10 + val[M02] * r20;
		FP m01 = val[M00] * r01 + val[M01] * r11 + val[M02] * r21;
		FP m02 = val[M00] * r02 + val[M01] * r12 + val[M02] * r22;
		FP m10 = val[M10] * r00 + val[M11] * r10 + val[M12] * r20;
		FP m11 = val[M10] * r01 + val[M11] * r11 + val[M12] * r21;
		FP m12 = val[M10] * r02 + val[M11] * r12 + val[M12] * r22;
		FP m20 = val[M20] * r00 + val[M21] * r10 + val[M22] * r20;
		FP m21 = val[M20] * r01 + val[M21] * r11 + val[M22] * r21;
		FP m22 = val[M20] * r02 + val[M21] * r12 + val[M22] * r22;
		FP m30 = val[M30] * r00 + val[M31] * r10 + val[M32] * r20;
		FP m31 = val[M30] * r01 + val[M31] * r11 + val[M32] * r21;
		FP m32 = val[M30] * r02 + val[M31] * r12 + val[M32] * r22;
		val[M00] = m00;
		val[M10] = m10;
		val[M20] = m20;
		val[M30] = m30;
		val[M01] = m01;
		val[M11] = m11;
		val[M21] = m21;
		val[M31] = m31;
		val[M02] = m02;
		val[M12] = m12;
		val[M22] = m22;
		val[M32] = m32;
		return this;
	}

	/** Postmultiplies this matrix by the rotation between two vectors.
	 * @param v1 The base vector
	 * @param v2 The target vector
	 * @return This matrix for the purpose of chaining methods together */
	public DGMatrix4x4 rotate(FPVector3 v1, FPVector3 v2)
	{
		return rotate(quat.setFromCross(v1, v2));
	}

	/** Post-multiplies this matrix by a rotation toward a direction.
	 * @param direction direction to rotate toward
	 * @param up up vector
	 * @return This matrix for chaining */
	public DGMatrix4x4 rotateTowardDirection(FPVector3 direction, FPVector3 up)
	{
		l_vez.set(direction).nor();
		l_vex.set(direction).crs(up).nor();
		l_vey.set(l_vex).crs(l_vez).nor();
		FP m00 = val[M00] * l_vex.x + val[M01] * l_vex.y + val[M02] * l_vex.z;
		FP m01 = val[M00] * l_vey.x + val[M01] * l_vey.y + val[M02] * l_vey.z;
		FP m02 = val[M00] * -l_vez.x + val[M01] * -l_vez.y + val[M02] * -l_vez.z;
		FP m10 = val[M10] * l_vex.x + val[M11] * l_vex.y + val[M12] * l_vex.z;
		FP m11 = val[M10] * l_vey.x + val[M11] * l_vey.y + val[M12] * l_vey.z;
		FP m12 = val[M10] * -l_vez.x + val[M11] * -l_vez.y + val[M12] * -l_vez.z;
		FP m20 = val[M20] * l_vex.x + val[M21] * l_vex.y + val[M22] * l_vex.z;
		FP m21 = val[M20] * l_vey.x + val[M21] * l_vey.y + val[M22] * l_vey.z;
		FP m22 = val[M20] * -l_vez.x + val[M21] * -l_vez.y + val[M22] * -l_vez.z;
		FP m30 = val[M30] * l_vex.x + val[M31] * l_vex.y + val[M32] * l_vex.z;
		FP m31 = val[M30] * l_vey.x + val[M31] * l_vey.y + val[M32] * l_vey.z;
		FP m32 = val[M30] * -l_vez.x + val[M31] * -l_vez.y + val[M32] * -l_vez.z;
		val[M00] = m00;
		val[M10] = m10;
		val[M20] = m20;
		val[M30] = m30;
		val[M01] = m01;
		val[M11] = m11;
		val[M21] = m21;
		val[M31] = m31;
		val[M02] = m02;
		val[M12] = m12;
		val[M22] = m22;
		val[M32] = m32;
		return this;
	}

	/** Post-multiplies this matrix by a rotation toward a target.
	 * @param target the target to rotate to
	 * @param up the up vector
	 * @return This matrix for chaining */
	public DGMatrix4x4 rotateTowardTarget(FPVector3 target, FPVector3 up)
	{
		tmpVec.set(target.x - val[M03], target.y - val[M13], target.z - val[M23]);
		return rotateTowardDirection(tmpVec, up);
	}

	/** Postmultiplies this matrix with a scale matrix. Postmultiplication is also used by OpenGL ES' 1.x
	 * glTranslate/glRotate/glScale.
	 * @param scaleX The scale in the x-axis.
	 * @param scaleY The scale in the y-axis.
	 * @param scaleZ The scale in the z-axis.
	 * @return This matrix for the purpose of chaining methods together. */
	public DGMatrix4x4 scale(FP scaleX, FP scaleY, FP scaleZ)
	{
		val[M00] *= scaleX;
		val[M01] *= scaleY;
		val[M02] *= scaleZ;
		val[M10] *= scaleX;
		val[M11] *= scaleY;
		val[M12] *= scaleZ;
		val[M20] *= scaleX;
		val[M21] *= scaleY;
		val[M22] *= scaleZ;
		val[M30] *= scaleX;
		val[M31] *= scaleY;
		val[M32] *= scaleZ;
		return this;
	}

	/** Copies the 4x3 upper-left sub-matrix into float array. The destination array is supposed to be a column major matrix.
	 * @param dst the destination matrix */
	public void extract4x3Matrix(FP[] dst)
	{
		dst[0] = val[M00];
		dst[1] = val[M10];
		dst[2] = val[M20];
		dst[3] = val[M01];
		dst[4] = val[M11];
		dst[5] = val[M21];
		dst[6] = val[M02];
		dst[7] = val[M12];
		dst[8] = val[M22];
		dst[9] = val[M03];
		dst[10] = val[M13];
		dst[11] = val[M23];
	}

	/** @return True if this matrix has any rotation or scaling, false otherwise */
	public bool hasRotationOrScaling()
	{
		return !(DGMath.IsEqual(val[M00], (FP) 1) && DGMath.IsEqual(val[M11], (FP) 1) &&
		         DGMath.IsEqual(val[M22], (FP) 1)
		         && DGMath.IsZero(val[M01]) && DGMath.IsZero(val[M02]) && DGMath.IsZero(val[M10]) &&
		         DGMath.IsZero(val[M12])
		         && DGMath.IsZero(val[M20]) && DGMath.IsZero(val[M21]));
	}
}