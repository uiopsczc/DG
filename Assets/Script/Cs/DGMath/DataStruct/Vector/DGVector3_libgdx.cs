using FP = DGFixedPoint;
using FPVector2 = DGVector2;
using FPQuaternion = DGQuaternion;
using FPMatrix4x4 = DGMatrix4x4;
using FPMatrix3x3 = DGMatrix3x3;
using FPInterpolation = DGInterpolation;

/** Encapsulates a 3D vector. Allows chaining operations by returning a reference to itself in all modification methods.
 * @author badlogicgames@gmail.com */
public partial struct DGVector3
{
	/** the x-component of this vector **/
	public FP x;

	/** the y-component of this vector **/
	public FP y;

	/** the z-component of this vector **/
	public FP z;

	public static DGVector3 X = new DGVector3((FP) 1, (FP) 0, (FP) 0);
	public static DGVector3 Y = new DGVector3((FP) 0, (FP) 1, (FP) 0);
	public static DGVector3 Z = new DGVector3((FP) 0, (FP) 0, (FP) 1);
	public static DGVector3 Zero = new DGVector3((FP) 0, (FP) 0, (FP) 0);

	private static FPMatrix4x4 tmpMat = new FPMatrix4x4(false);


	/** Creates a vector with the given components
	 * @param x The x-component
	 * @param y The y-component
	 * @param z The z-component */
	public DGVector3(FP x, FP y, FP z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	/** Creates a vector from the given vector
	 * @param vector The vector */
	public DGVector3(DGVector3 vector)
	{
		this.x = vector.x;
		this.y = vector.y;
		this.z = vector.z;
	}

	/** Creates a vector from the given array. The array must have at least 3 elements.
	 *
	 * @param values The array */
	public DGVector3(FP[] values)
	{
		this.x = values[0];
		this.y = values[1];
		this.z = values[2];
	}

	/** Creates a vector from the given vector and z-component
	 *
	 * @param vector The vector
	 * @param z The z-component */
	public DGVector3(FPVector2 vector, FP z)
	{
		this.x = vector.x;
		this.y = vector.y;
		this.z = z;
	}

	/** Sets the vector to the given components
	 *
	 * @param x The x-component
	 * @param y The y-component
	 * @param z The z-component
	 * @return this vector for chaining */
	public DGVector3 set(FP x, FP y, FP z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		return this;
	}

	public DGVector3 set(DGVector3 vector)
	{
		return this.set(vector.x, vector.y, vector.z);
	}

	/** Sets the components from the array. The array must have at least 3 elements
	 *
	 * @param values The array
	 * @return this vector for chaining */
	public DGVector3 set(FP[] values)
	{
		return this.set(values[0], values[1], values[2]);
	}

	/** Sets the components of the given vector and z-component
	 *
	 * @param vector The vector
	 * @param z The z-component
	 * @return This vector for chaining */
	public DGVector3 set(FPVector2 vector, FP z)
	{
		return this.set(vector.x, vector.y, z);
	}

	/** Sets the components from the given spherical coordinate
	 * @param azimuthalAngle The angle between x-axis in radians [0, 2pi]
	 * @param polarAngle The angle between z-axis in radians [0, pi]
	 * @return This vector for chaining */
	public DGVector3 setFromSpherical(FP azimuthalAngle, FP polarAngle)
	{
		FP cosPolar = DGMath.Cos(polarAngle);
		FP sinPolar = DGMath.Sin(polarAngle);

		FP cosAzim = DGMath.Cos(azimuthalAngle);
		FP sinAzim = DGMath.Sin(azimuthalAngle);

		return this.set(cosAzim * sinPolar, sinAzim * sinPolar, cosPolar);
	}


	public DGVector3 cpy()
	{
		return new DGVector3(this);
	}

	public DGVector3 add(DGVector3 vector)
	{
		return this.add(vector.x, vector.y, vector.z);
	}

	/** Adds the given vector to this component
	 * @param x The x-component of the other vector
	 * @param y The y-component of the other vector
	 * @param z The z-component of the other vector
	 * @return This vector for chaining. */
	public DGVector3 add(FP x, FP y, FP z)
	{
		return this.set(this.x + x, this.y + y, this.z + z);
	}

	/** Adds the given value to all three components of the vector.
	 *
	 * @param values The value
	 * @return This vector for chaining */
	public DGVector3 add(FP values)
	{
		return this.set(this.x + values, this.y + values, this.z + values);
	}

	public DGVector3 sub(DGVector3 a_vec)
	{
		return this.sub(a_vec.x, a_vec.y, a_vec.z);
	}

	/** Subtracts the other vector from this vector.
	 *
	 * @param x The x-component of the other vector
	 * @param y The y-component of the other vector
	 * @param z The z-component of the other vector
	 * @return This vector for chaining */
	public DGVector3 sub(FP x, FP y, FP z)
	{
		return this.set(this.x - x, this.y - y, this.z - z);
	}

	/** Subtracts the given value from all components of this vector
	 *
	 * @param value The value
	 * @return This vector for chaining */
	public DGVector3 sub(FP value)
	{
		return this.set(this.x - value, this.y - value, this.z - value);
	}

	public DGVector3 scl(FP scalar)
	{
		return this.set(this.x * scalar, this.y * scalar, this.z * scalar);
	}

	public DGVector3 scl(DGVector3 other)
	{
		return this.set(x * other.x, y * other.y, z * other.z);
	}

	/** Scales this vector by the given values
	 * @param vx X value
	 * @param vy Y value
	 * @param vz Z value
	 * @return This vector for chaining */
	public DGVector3 scl(FP vx, FP vy, FP vz)
	{
		return this.set(this.x * vx, this.y * vy, this.z * vz);
	}

	public DGVector3 mulAdd(DGVector3 vec, FP scalar)
	{
		this.x += vec.x * scalar;
		this.y += vec.y * scalar;
		this.z += vec.z * scalar;
		return this;
	}

	public DGVector3 mulAdd(DGVector3 vec, DGVector3 mulVec)
	{
		this.x += vec.x * mulVec.x;
		this.y += vec.y * mulVec.y;
		this.z += vec.z * mulVec.z;
		return this;
	}

	/** @return The euclidean length */
	public static FP len(FP x, FP y, FP z)
	{
		return DGMath.Sqrt(x * x + y * y + z * z);
	}

	public FP len()
	{
		return DGMath.Sqrt(x * x + y * y + z * z);
	}

	/** @return The squared euclidean length */
	public static FP len2(FP x, FP y, FP z)
	{
		return x * x + y * y + z * z;
	}

	public FP len2()
	{
		return x * x + y * y + z * z;
	}

	/** @param vector The other vector
	 * @return Whether this and the other vector are equal */
	public bool idt(DGVector3 vector)
	{
		return x == vector.x && y == vector.y && z == vector.z;
	}

	/** @return The euclidean distance between the two specified vectors */
	public static FP dst(FP x1, FP y1, FP z1, FP x2, FP y2, FP z2)
	{
		FP a = x2 - x1;
		FP b = y2 - y1;
		FP c = z2 - z1;
		return DGMath.Sqrt(a * a + b * b + c * c);
	}

	public FP dst(DGVector3 vector)
	{
		FP a = vector.x - x;
		FP b = vector.y - y;
		FP c = vector.z - z;
		return DGMath.Sqrt(a * a + b * b + c * c);
	}

	/** @return the distance between this point and the given point */
	public FP dst(FP x, FP y, FP z)
	{
		FP a = x - this.x;
		FP b = y - this.y;
		FP c = z - this.z;
		return DGMath.Sqrt(a * a + b * b + c * c);
	}

	/** @return the squared distance between the given points */
	public static FP dst2(FP x1, FP y1, FP z1, FP x2, FP y2, FP z2)
	{
		FP a = x2 - x1;
		FP b = y2 - y1;
		FP c = z2 - z1;
		return a * a + b * b + c * c;
	}

	public FP dst2(DGVector3 point)
	{
		FP a = point.x - x;
		FP b = point.y - y;
		FP c = point.z - z;
		return a * a + b * b + c * c;
	}

	/** Returns the squared distance between this point and the given point
	 * @param x The x-component of the other point
	 * @param y The y-component of the other point
	 * @param z The z-component of the other point
	 * @return The squared distance */
	public FP dst2(FP x, FP y, FP z)
	{
		FP a = x - this.x;
		FP b = y - this.y;
		FP c = z - this.z;
		return a * a + b * b + c * c;
	}

	public DGVector3 nor()
	{
		FP len2 = this.len2();
		if (len2 == (FP) 0f || len2 == (FP) 1f) return this;
		return this.scl((FP) 1f / DGMath.Sqrt(len2));
	}

	/** @return The dot product between the two vectors */
	public static FP dot(FP x1, FP y1, FP z1, FP x2, FP y2, FP z2)
	{
		return x1 * x2 + y1 * y2 + z1 * z2;
	}

	public FP dot(DGVector3 vector)
	{
		return x * vector.x + y * vector.y + z * vector.z;
	}

	/** Returns the dot product between this and the given vector.
	 * @param x The x-component of the other vector
	 * @param y The y-component of the other vector
	 * @param z The z-component of the other vector
	 * @return The dot product */
	public FP dot(FP x, FP y, FP z)
	{
		return this.x * x + this.y * y + this.z * z;
	}

	/** Sets this vector to the cross product between it and the other vector.
	 * @param vector The other vector
	 * @return This vector for chaining */
	public DGVector3 crs(DGVector3 vector)
	{
		return this.set(y * vector.z - z * vector.y, z * vector.x - x * vector.z, x * vector.y - y * vector.x);
	}

	/** Sets this vector to the cross product between it and the other vector.
	 * @param x The x-component of the other vector
	 * @param y The y-component of the other vector
	 * @param z The z-component of the other vector
	 * @return This vector for chaining */
	public DGVector3 crs(FP x, FP y, FP z)
	{
		return this.set(this.y * z - this.z * y, this.z * x - this.x * z, this.x * y - this.y * x);
	}

	/** Left-multiplies the vector by the given 4x3 column major matrix. The matrix should be composed by a 3x3 matrix representing
	 * rotation and scale plus a 1x3 matrix representing the translation.
	 * @param matrix The matrix
	 * @return This vector for chaining */
	public DGVector3 mul4x3(FP[] matrix)
	{
		return set(x * matrix[0] + y * matrix[3] + z * matrix[6] + matrix[9],
			x * matrix[1] + y * matrix[4] + z * matrix[7] + matrix[10],
			x * matrix[2] + y * matrix[5] + z * matrix[8] + matrix[11]);
	}

	/** Left-multiplies the vector by the given matrix, assuming the fourth (w) component of the vector is 1.
	 * @param matrix The matrix
	 * @return This vector for chaining */
	public DGVector3 mul(FPMatrix4x4 matrix)
	{
		FP[] l_mat = matrix.val;
		return this.set(
			x * l_mat[FPMatrix4x4.M00] + y * l_mat[FPMatrix4x4.M01] + z * l_mat[FPMatrix4x4.M02] +
			l_mat[FPMatrix4x4.M03],
			x * l_mat[FPMatrix4x4.M10] + y * l_mat[FPMatrix4x4.M11] + z * l_mat[FPMatrix4x4.M12] +
			l_mat[FPMatrix4x4.M13],
			x * l_mat[FPMatrix4x4.M20] + y * l_mat[FPMatrix4x4.M21] + z * l_mat[FPMatrix4x4.M22] +
			l_mat[FPMatrix4x4.M23]);
	}

	/** Multiplies the vector by the transpose of the given matrix, assuming the fourth (w) component of the vector is 1.
	 * @param matrix The matrix
	 * @return This vector for chaining */
	public DGVector3 traMul(FPMatrix4x4 matrix)
	{
		FP[] l_mat = matrix.val;
		return this.set(
			x * l_mat[FPMatrix4x4.M00] + y * l_mat[FPMatrix4x4.M10] + z * l_mat[FPMatrix4x4.M20] +
			l_mat[FPMatrix4x4.M30],
			x * l_mat[FPMatrix4x4.M01] + y * l_mat[FPMatrix4x4.M11] + z * l_mat[FPMatrix4x4.M21] +
			l_mat[FPMatrix4x4.M31],
			x * l_mat[FPMatrix4x4.M02] + y * l_mat[FPMatrix4x4.M12] + z * l_mat[FPMatrix4x4.M22] +
			l_mat[FPMatrix4x4.M32]);
	}

	/** Left-multiplies the vector by the given matrix.
	 * @param matrix The matrix
	 * @return This vector for chaining */
	public DGVector3 mul(FPMatrix3x3 matrix)
	{
		FP[] l_mat = matrix.val;
		return set(x * l_mat[FPMatrix3x3.M00] + y * l_mat[FPMatrix3x3.M01] + z * l_mat[FPMatrix3x3.M02],
			x * l_mat[FPMatrix3x3.M10] + y * l_mat[FPMatrix3x3.M11] + z * l_mat[FPMatrix3x3.M12],
			x * l_mat[FPMatrix3x3.M20] + y * l_mat[FPMatrix3x3.M21] + z * l_mat[FPMatrix3x3.M22]);
	}

	/** Multiplies the vector by the transpose of the given matrix.
	 * @param matrix The matrix
	 * @return This vector for chaining */
	public DGVector3 traMul(FPMatrix3x3 matrix)
	{
		FP[] l_mat = matrix.val;
		return set(x * l_mat[FPMatrix3x3.M00] + y * l_mat[FPMatrix3x3.M10] + z * l_mat[FPMatrix3x3.M20],
			x * l_mat[FPMatrix3x3.M01] + y * l_mat[FPMatrix3x3.M11] + z * l_mat[FPMatrix3x3.M21],
			x * l_mat[FPMatrix3x3.M02] + y * l_mat[FPMatrix3x3.M12] + z * l_mat[FPMatrix3x3.M22]);
	}

	/** Multiplies the vector by the given {@link Quaternion}.
	 * @return This vector for chaining */
	public DGVector3 mul(FPQuaternion quat)
	{
		return quat.transform(this);
	}

	/** Multiplies this vector by the given matrix dividing by w, assuming the fourth (w) component of the vector is 1. This is
	 * mostly used to project/unproject vectors via a perspective projection matrix.
	 *
	 * @param matrix The matrix.
	 * @return This vector for chaining */
	public DGVector3 prj(FPMatrix4x4 matrix)
	{
		FP[] l_mat = matrix.val;
		FP l_w = (FP) 1f / (x * l_mat[FPMatrix4x4.M30] + y * l_mat[FPMatrix4x4.M31] + z * l_mat[FPMatrix4x4.M32] +
		                    l_mat[FPMatrix4x4.M33]);
		return this.set(
			(x * l_mat[FPMatrix4x4.M00] + y * l_mat[FPMatrix4x4.M01] + z * l_mat[FPMatrix4x4.M02] +
			 l_mat[FPMatrix4x4.M03]) * l_w,
			(x * l_mat[FPMatrix4x4.M10] + y * l_mat[FPMatrix4x4.M11] + z * l_mat[FPMatrix4x4.M12] +
			 l_mat[FPMatrix4x4.M13]) * l_w,
			(x * l_mat[FPMatrix4x4.M20] + y * l_mat[FPMatrix4x4.M21] + z * l_mat[FPMatrix4x4.M22] +
			 l_mat[FPMatrix4x4.M23]) * l_w);
	}

	/** Multiplies this vector by the first three columns of the matrix, essentially only applying rotation and scaling.
	 *
	 * @param matrix The matrix
	 * @return This vector for chaining */
	public DGVector3 rot(FPMatrix4x4 matrix)
	{
		FP[] l_mat = matrix.val;
		return this.set(x * l_mat[FPMatrix4x4.M00] + y * l_mat[FPMatrix4x4.M01] + z * l_mat[FPMatrix4x4.M02],
			x * l_mat[FPMatrix4x4.M10] + y * l_mat[FPMatrix4x4.M11] + z * l_mat[FPMatrix4x4.M12],
			x * l_mat[FPMatrix4x4.M20] + y * l_mat[FPMatrix4x4.M21] + z * l_mat[FPMatrix4x4.M22]);
	}

	/** Multiplies this vector by the transpose of the first three columns of the matrix. Note: only works for translation and
	 * rotation, does not work for scaling. For those, use {@link #rot(Matrix4)} with {@link Matrix4#inv()}.
	 * @param matrix The transformation matrix
	 * @return The vector for chaining */
	public DGVector3 unrotate(FPMatrix4x4 matrix)
	{
		FP[] l_mat = matrix.val;
		return this.set(x * l_mat[FPMatrix4x4.M00] + y * l_mat[FPMatrix4x4.M10] + z * l_mat[FPMatrix4x4.M20],
			x * l_mat[FPMatrix4x4.M01] + y * l_mat[FPMatrix4x4.M11] + z * l_mat[FPMatrix4x4.M21],
			x * l_mat[FPMatrix4x4.M02] + y * l_mat[FPMatrix4x4.M12] + z * l_mat[FPMatrix4x4.M22]);
	}

	/** Translates this vector in the direction opposite to the translation of the matrix and the multiplies this vector by the
	 * transpose of the first three columns of the matrix. Note: only works for translation and rotation, does not work for
	 * scaling. For those, use {@link #mul(Matrix4)} with {@link Matrix4#inv()}.
	 * @param matrix The transformation matrix
	 * @return The vector for chaining */
	public DGVector3 untransform(FPMatrix4x4 matrix)
	{
		FP[] l_mat = matrix.val;
		x -= l_mat[FPMatrix4x4.M03];
		y -= l_mat[FPMatrix4x4.M03];
		z -= l_mat[FPMatrix4x4.M03];
		return this.set(x * l_mat[FPMatrix4x4.M00] + y * l_mat[FPMatrix4x4.M10] + z * l_mat[FPMatrix4x4.M20],
			x * l_mat[FPMatrix4x4.M01] + y * l_mat[FPMatrix4x4.M11] + z * l_mat[FPMatrix4x4.M21],
			x * l_mat[FPMatrix4x4.M02] + y * l_mat[FPMatrix4x4.M12] + z * l_mat[FPMatrix4x4.M22]);
	}

	/** Rotates this vector by the given angle in degrees around the given axis.
	 *
	 * @param degrees the angle in degrees
	 * @param axisX the x-component of the axis
	 * @param axisY the y-component of the axis
	 * @param axisZ the z-component of the axis
	 * @return This vector for chaining */
	public DGVector3 rotate(FP degrees, FP axisX, FP axisY, FP axisZ)
	{
		return this.mul(tmpMat.setToRotation(axisX, axisY, axisZ, degrees));
	}

	/** Rotates this vector by the given angle in radians around the given axis.
	 *
	 * @param radians the angle in radians
	 * @param axisX the x-component of the axis
	 * @param axisY the y-component of the axis
	 * @param axisZ the z-component of the axis
	 * @return This vector for chaining */
	public DGVector3 rotateRad(FP radians, FP axisX, FP axisY, FP axisZ)
	{
		return this.mul(tmpMat.setToRotationRad(axisX, axisY, axisZ, radians));
	}

	/** Rotates this vector by the given angle in degrees around the given axis.
	 *
	 * @param axis the axis
	 * @param degrees the angle in degrees
	 * @return This vector for chaining */
	public DGVector3 rotate(DGVector3 axis, FP degrees)
	{
		tmpMat.setToRotation(axis, degrees);
		return this.mul(tmpMat);
	}

	/** Rotates this vector by the given angle in radians around the given axis.
	 *
	 * @param axis the axis
	 * @param radians the angle in radians
	 * @return This vector for chaining */
	public DGVector3 rotateRad(DGVector3 axis, FP radians)
	{
		tmpMat.setToRotationRad(axis, radians);
		return this.mul(tmpMat);
	}

	public bool isUnit()
	{
		return isUnit((FP) 0.000000001f);
	}

	public bool isUnit(FP margin)
	{
		return DGMath.Abs(len2() - (FP) 1f) < margin;
	}

	public bool isZero()
	{
		return x == (FP) 0 && y == (FP) 0 && z == (FP) 0;
	}

	public bool isZero(FP margin)
	{
		return len2() < margin;
	}

	public bool isOnLine(DGVector3 other, FP epsilon)
	{
		return len2(y * other.z - z * other.y, z * other.x - x * other.z, x * other.y - y * other.x) <= epsilon;
	}

	public bool isOnLine(DGVector3 other)
	{
		return len2(y * other.z - z * other.y, z * other.x - x * other.z,
			       x * other.y - y * other.x) <= DGMath.Epsilon;
	}

	public bool isCollinear(DGVector3 other, FP epsilon)
	{
		return isOnLine(other, epsilon) && hasSameDirection(other);
	}

	public bool isCollinear(DGVector3 other)
	{
		return isOnLine(other) && hasSameDirection(other);
	}

	public bool isCollinearOpposite(DGVector3 other, FP epsilon)
	{
		return isOnLine(other, epsilon) && hasOppositeDirection(other);
	}

	public bool isCollinearOpposite(DGVector3 other)
	{
		return isOnLine(other) && hasOppositeDirection(other);
	}

	public bool isPerpendicular(DGVector3 vector)
	{
		return DGMath.IsZero(dot(vector));
	}

	public bool isPerpendicular(DGVector3 vector, FP epsilon)
	{
		return DGMath.IsZero(dot(vector), epsilon);
	}

	public bool hasSameDirection(DGVector3 vector)
	{
		return dot(vector) > (FP) 0;
	}

	public bool hasOppositeDirection(DGVector3 vector)
	{
		return dot(vector) < (FP) 0;
	}

	public DGVector3 lerp(DGVector3 target, FP alpha)
	{
		x += alpha * (target.x - x);
		y += alpha * (target.y - y);
		z += alpha * (target.z - z);
		return this;
	}

	public DGVector3 interpolate(DGVector3 target, FP alpha, FPInterpolation interpolator)
	{
		return lerp(target, interpolator.Apply((FP) 0f, (FP) 1f, alpha));
	}

	/** Spherically interpolates between this vector and the target vector by alpha which is in the range [0,1]. The result is
	 * stored in this vector.
	 *
	 * @param target The target vector
	 * @param alpha The interpolation coefficient
	 * @return This vector for chaining. */
	public DGVector3 slerp(DGVector3 target, FP alpha)
	{
		FP dot = this.dot(target);
		// If the inputs are too close for comfort, simply linearly interpolate.
		if (dot > (FP) 0.9995 || dot < (FP) (-0.9995)) return lerp(target, alpha);

		// theta0 = angle between input vectors
		FP theta0 = DGMath.Acos(dot);
		// theta = angle between this vector and result
		FP theta = theta0 * alpha;

		FP st = DGMath.Sin(theta);
		FP tx = target.x - x * dot;
		FP ty = target.y - y * dot;
		FP tz = target.z - z * dot;
		FP l2 = tx * tx + ty * ty + tz * tz;
		FP dl = st * ((l2 < (FP) 0.0001f) ? (FP) 1f : (FP) 1f / DGMath.Sqrt(l2));

		return scl(DGMath.Cos(theta)).add(tx * dl, ty * dl, tz * dl).nor();
	}

	/** Converts this {@code Vector3} to a string in the format {@code (x,y,z)}.
	 * @return a string representation of this object. */
	public override string ToString()
	{
		return "(" + x + "," + y + "," + z + ")";
	}


	public DGVector3 limit(FP limit)
	{
		return limit2(limit * limit);
	}

	public DGVector3 limit2(FP limit2)
	{
		FP len2 = this.len2();
		if (len2 > limit2)
		{
			scl(DGMath.Sqrt(limit2 / len2));
		}

		return this;
	}

	public DGVector3 setLength(FP len)
	{
		return setLength2(len * len);
	}

	public DGVector3 setLength2(FP len2)
	{
		FP oldLen2 = this.len2();
		return (oldLen2 == (FP) 0 || oldLen2 == len2) ? this : scl(DGMath.Sqrt(len2 / oldLen2));
	}

	public DGVector3 clamp(FP min, FP max)
	{
		FP len2 = this.len2();
		if (len2 == (FP) 0f) return this;
		FP max2 = max * max;
		if (len2 > max2) return scl(DGMath.Sqrt(max2 / len2));
		FP min2 = min * min;
		if (len2 < min2) return scl(DGMath.Sqrt(min2 / len2));
		return this;
	}


	public bool epsilonEquals(DGVector3 other, FP epsilon)
	{
		if (DGMath.Abs(other.x - x) > epsilon) return false;
		if (DGMath.Abs(other.y - y) > epsilon) return false;
		if (DGMath.Abs(other.z - z) > epsilon) return false;
		return true;
	}

	/** Compares this vector with the other vector, using the supplied epsilon for fuzzy equality testing.
	 * @return whether the vectors are the same. */
	public bool epsilonEquals(FP x, FP y, FP z, FP epsilon)
	{
		if (DGMath.Abs(x - this.x) > epsilon) return false;
		if (DGMath.Abs(y - this.y) > epsilon) return false;
		if (DGMath.Abs(z - this.z) > epsilon) return false;
		return true;
	}

	/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
	 *
	 * @param other other vector to compare
	 * @return true if vector are equal, otherwise false */
	public bool epsilonEquals(DGVector3 other)
	{
		return epsilonEquals(other, DGMath.Epsilon);
	}

	/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
	 *
	 * @param x x component of the other vector to compare
	 * @param y y component of the other vector to compare
	 * @param z z component of the other vector to compare
	 * @return true if vector are equal, otherwise false */
	public bool epsilonEquals(FP x, FP y, FP z)
	{
		return epsilonEquals(x, y, z, DGMath.Epsilon);
	}

	public DGVector3 setZero()
	{
		this.x = (FP) 0;
		this.y = (FP) 0;
		this.z = (FP) 0;
		return this;
	}
}