
namespace DG
{
	public partial struct FPVector2
	{
		public static FPVector2 X = new(1, 0);
		public static FPVector2 Y = new(0, 1);
		public static FPVector2 Zero = new(0, 0);

		/** the x-component of this vector **/
		public FP x;

		/** the y-component of this vector **/
		public FP y;

		/** Constructs a vector with the given components
		 * @param x The x-component
		 * @param y The y-component */
		public FPVector2(FP x, FP y)
		{
			this.x = x;
			this.y = y;
		}

		/** Constructs a vector from the given vector
		 * @param v The vector */
		public FPVector2(FPVector2 v)
		{
			x = v.x;
			y = v.y;
		}

		public FPVector2 cpy()
		{
			return new FPVector2(this);
		}

		public static FP len(FP x, FP y)
		{
			return FPMath.Sqrt(x * x + y * y);
		}

		public FP len()
		{
			return FPMath.Sqrt(x * x + y * y);
		}

		public static FP len2(FP x, FP y)
		{
			return x * x + y * y;
		}

		public FP len2()
		{
			return x * x + y * y;
		}

		public FPVector2 set(FPVector2 v)
		{
			x = v.x;
			y = v.y;
			return this;
		}

		/** Sets the components of this vector
		 * @param x The x-component
		 * @param y The y-component
		 * @return This vector for chaining */
		public FPVector2 set(FP x, FP y)
		{
			this.x = x;
			this.y = y;
			return this;
		}

		public FPVector2 sub(FPVector2 v)
		{
			x -= v.x;
			y -= v.y;
			return this;
		}

		/** Substracts the other vector from this vector.
		 * @param x The x-component of the other vector
		 * @param y The y-component of the other vector
		 * @return This vector for chaining */
		public FPVector2 sub(FP x, FP y)
		{
			this.x -= x;
			this.y -= y;
			return this;
		}

		public FPVector2 nor()
		{
			FP len = this.len();
			if (len != 0)
			{
				x /= len;
				y /= len;
			}

			return this;
		}

		public FPVector2 add(FPVector2 v)
		{
			x += v.x;
			y += v.y;
			return this;
		}

		/** Adds the given components to this vector
		 * @param x The x-component
		 * @param y The y-component
		 * @return This vector for chaining */
		public FPVector2 add(FP x, FP y)
		{
			this.x += x;
			this.y += y;
			return this;
		}

		public static FP dot(FP x1, FP y1, FP x2, FP y2)
		{
			return x1 * x2 + y1 * y2;
		}

		public FP dot(FPVector2 v)
		{
			return x * v.x + y * v.y;
		}

		public FP dot(FP ox, FP oy)
		{
			return x * ox + y * oy;
		}

		public FPVector2 scl(FP scalar)
		{
			x *= scalar;
			y *= scalar;
			return this;
		}

		/** Multiplies this vector by a scalar
		 * @return This vector for chaining */
		public FPVector2 scl(FP x, FP y)
		{
			this.x *= x;
			this.y *= y;
			return this;
		}

		public FPVector2 scl(FPVector2 v)
		{
			x *= v.x;
			y *= v.y;
			return this;
		}

		public FPVector2 mulAdd(FPVector2 vec, FP scalar)
		{
			x += vec.x * scalar;
			y += vec.y * scalar;
			return this;
		}

		public FPVector2 mulAdd(FPVector2 vec, FPVector2 mulVec)
		{
			x += vec.x * mulVec.x;
			y += vec.y * mulVec.y;
			return this;
		}

		public static FP dst(FP x1, FP y1, FP x2, FP y2)
		{
			FP x_d = x2 - x1;
			FP y_d = y2 - y1;
			return FPMath.Sqrt(x_d * x_d + y_d * y_d);
		}

		public FP dst(FPVector2 v)
		{
			FP x_d = v.x - x;
			FP y_d = v.y - y;
			return FPMath.Sqrt(x_d * x_d + y_d * y_d);
		}

		/** @param x The x-component of the other vector
		 * @param y The y-component of the other vector
		 * @return the distance between this and the other vector */
		public FP dst(FP x, FP y)
		{
			FP x_d = x - this.x;
			FP y_d = y - this.y;
			return FPMath.Sqrt(x_d * x_d + y_d * y_d);
		}

		public static FP dst2(FP x1, FP y1, FP x2, FP y2)
		{
			FP x_d = x2 - x1;
			FP y_d = y2 - y1;
			return x_d * x_d + y_d * y_d;
		}

		public FP dst2(FPVector2 v)
		{
			FP x_d = v.x - x;
			FP y_d = v.y - y;
			return x_d * x_d + y_d * y_d;
		}

		/** @param x The x-component of the other vector
		 * @param y The y-component of the other vector
		 * @return the squared distance between this and the other vector */
		public FP dst2(FP x, FP y)
		{
			FP x_d = x - this.x;
			FP y_d = y - this.y;
			return x_d * x_d + y_d * y_d;
		}


		public FPVector2 limit(FP limit)
		{
			return limit2(limit * limit);
		}


		public FPVector2 limit2(FP limit2)
		{
			FP len2 = this.len2();
			if (len2 > limit2)
			{
				return scl(FPMath.Sqrt(limit2 / len2));
			}

			return this;
		}

		public FPVector2 clamp(FP min, FP max)
		{
			FP len2 = this.len2();
			if (len2 == 0f) return this;
			FP max2 = max * max;
			if (len2 > max2) return scl(FPMath.Sqrt(max2 / len2));
			FP min2 = min * min;
			if (len2 < min2) return scl(FPMath.Sqrt(min2 / len2));
			return this;
		}

		public FPVector2 setLength(FP len)
		{
			return setLength2(len * len);
		}

		public FPVector2 setLength2(FP len2)
		{
			FP oldLen2 = this.len2();
			return (oldLen2 == 0 || oldLen2 == len2) ? this : scl(FPMath.Sqrt(len2 / oldLen2));
		}

		/** Converts this {@code Vector2} to a string in the format {@code (x,y)}.
		 * @return a string representation of this object. */
		public override string ToString()
		{
			return "(" + x + "," + y + ")";
		}


		/** Left-multiplies this vector by the given matrix
		 * @param mat the matrix
		 * @return this vector */
		public FPVector2 mul(FPMatrix3x3 mat)
		{
			FP x = this.x * mat.m00 + this.y * mat.m01 + mat.m02;
			FP y = this.x * mat.m10 + this.y * mat.m11 + mat.m12;
			this.x = x;
			this.y = y;
			return this;
		}

		/** Calculates the 2D cross product between this and the given vector.
		 * @param v the other vector
		 * @return the cross product */
		public FP crs(FPVector2 v)
		{
			return x * v.y - y * v.x;
		}

		/** Calculates the 2D cross product between this and the given vector.
		 * @param x the x-coordinate of the other vector
		 * @param y the y-coordinate of the other vector
		 * @return the cross product */
		public FP crs(FP x, FP y)
		{
			return this.x * y - this.y * x;
		}

		/** @return the angle in degrees of this vector (point) relative to the x-axis. Angles are towards the positive y-axis
		 *         (typically counter-clockwise) and between 0 and 360.
		 * @deprecated use {@link #angleDeg()} instead. */
		public FP angle()
		{
			FP angle = FPMath.Atan2(y, x) * FPMath.Rad2Deg;
			if (angle < 0) angle += 360;
			return angle;
		}

		/** @return the angle in degrees of this vector (point) relative to the given vector. Angles are towards the negative y-axis
		 *         (typically clockwise) between -180 and +180
		 * @deprecated use {@link #angleDeg(Vector2)} instead. Be ware of the changes in returned angle to counter-clockwise and the
		 *             range. */
		public FP angle(FPVector2 reference)
		{
			return FPMath.Atan2(crs(reference), dot(reference)) * FPMath.Rad2Deg;
		}

		/** @return the angle in degrees of this vector (point) relative to the x-axis. Angles are towards the positive y-axis
		 *         (typically counter-clockwise) and in the [0, 360) range. */
		public FP angleDeg()
		{
			FP angle = FPMath.Atan2(y, x) * FPMath.Rad2Deg;
			if (angle < 0) angle += 360;
			return angle;
		}

		/** @return the angle in degrees of this vector (point) relative to the given vector. Angles are towards the positive y-axis
		 *         (typically counter-clockwise.) in the [0, 360) range */
		public FP angleDeg(FPVector2 reference)
		{
			FP angle = FPMath.Atan2(reference.crs(this), reference.dot(this)) * FPMath.Rad2Deg;
			if (angle < 0) angle += 360;
			return angle;
		}

		/** @return the angle in radians of this vector (point) relative to the x-axis. Angles are towards the positive y-axis.
		 *         (typically counter-clockwise) */
		public FP angleRad()
		{
			return FPMath.Atan2(y, x);
		}

		/** @return the angle in radians of this vector (point) relative to the given vector. Angles are towards the positive y-axis.
		 *         (typically counter-clockwise.) */
		public FP angleRad(FPVector2 reference)
		{
			return FPMath.Atan2(reference.crs(this), reference.dot(this));
		}

		/** Sets the angle of the vector in degrees relative to the x-axis, towards the positive y-axis (typically counter-clockwise).
		 * @param degrees The angle in degrees to set.
		 * @deprecated use {@link #setAngleDeg(float)} instead. */
		public FPVector2 setAngle(FP degrees)
		{
			return setAngleRad(degrees * FPMath.DEG2RAD);
		}

		/** Sets the angle of the vector in degrees relative to the x-axis, towards the positive y-axis (typically counter-clockwise).
		 * @param degrees The angle in degrees to set. */
		public FPVector2 setAngleDeg(FP degrees)
		{
			return setAngleRad(degrees * FPMath.DEG2RAD);
		}

		/** Sets the angle of the vector in radians relative to the x-axis, towards the positive y-axis (typically counter-clockwise).
		 * @param radians The angle in radians to set. */
		public FPVector2 setAngleRad(FP radians)
		{
			set(len(), 0f);
			rotateRad(radians);

			return this;
		}

		/** Rotates the Vector2 by the given angle, counter-clockwise assuming the y-axis points up.
		 * @param degrees the angle in degrees
		 * @deprecated use {@link #rotateDeg(float)} instead. */
		public FPVector2 rotate(FP degrees)
		{
			return rotateRad(degrees * FPMath.DEG2RAD);
		}

		/** Rotates the Vector2 by the given angle around reference vector, counter-clockwise assuming the y-axis points up.
		 * @param degrees the angle in degrees
		 * @param reference center Vector2
		 * @deprecated use {@link #rotateAroundDeg(Vector2, float)} instead. */
		public FPVector2 rotateAround(FPVector2 reference, FP degrees)
		{
			return sub(reference).rotateDeg(degrees).add(reference);
		}

		/** Rotates the Vector2 by the given angle, counter-clockwise assuming the y-axis points up.
		 * @param degrees the angle in degrees */
		public FPVector2 rotateDeg(FP degrees)
		{
			return rotateRad(degrees * FPMath.DEG2RAD);
		}

		/** Rotates the Vector2 by the given angle, counter-clockwise assuming the y-axis points up.
		 * @param radians the angle in radians */
		public FPVector2 rotateRad(FP radians)
		{
			FP cos = FPMath.Cos(radians);
			FP sin = FPMath.Sin(radians);

			FP newX = x * cos - y * sin;
			FP newY = x * sin + y * cos;

			x = newX;
			y = newY;

			return this;
		}

		/** Rotates the Vector2 by the given angle around reference vector, counter-clockwise assuming the y-axis points up.
		 * @param degrees the angle in degrees
		 * @param reference center Vector2 */
		public FPVector2 rotateAroundDeg(FPVector2 reference, FP degrees)
		{
			return sub(reference).rotateDeg(degrees).add(reference);
		}

		/** Rotates the Vector2 by the given angle around reference vector, counter-clockwise assuming the y-axis points up.
		 * @param radians the angle in radians
		 * @param reference center Vector2 */
		public FPVector2 rotateAroundRad(FPVector2 reference, FP radians)
		{
			return sub(reference).rotateRad(radians).add(reference);
		}

		/** Rotates the Vector2 by 90 degrees in the specified direction, where >= 0 is counter-clockwise and < 0 is clockwise. */
		public FPVector2 rotate90(int dir)
		{
			FP x = this.x;
			if (dir >= 0)
			{
				this.x = -y;
				y = x;
			}
			else
			{
				this.x = y;
				y = -x;
			}

			return this;
		}

		public FPVector2 lerp(FPVector2 target, FP alpha)
		{
			FP invAlpha = 1.0f - alpha;
			x = (x * invAlpha) + (target.x * alpha);
			y = (y * invAlpha) + (target.y * alpha);
			return this;
		}

		public FPVector2 interpolate(FPVector2 target, FP alpha, FPInterpolation interpolation)
		{
			return lerp(target, interpolation.Apply(alpha));
		}


		public bool epsilonEquals(FPVector2 other, FP epsilon)
		{
			if (FPMath.Abs(other.x - x) > epsilon) return false;
			if (FPMath.Abs(other.y - y) > epsilon) return false;
			return true;
		}

		/** Compares this vector with the other vector, using the supplied epsilon for fuzzy equality testing.
		 * @return whether the vectors are the same. */
		public bool epsilonEquals(FP x, FP y, FP epsilon)
		{
			if (FPMath.Abs(x - this.x) > epsilon) return false;
			if (FPMath.Abs(y - this.y) > epsilon) return false;
			return true;
		}

		/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
		 * @param other other vector to compare
		 * @return true if vector are equal, otherwise false */
		public bool epsilonEquals(FPVector2 other)
		{
			return epsilonEquals(other, FPMath.EPSILION);
		}

		/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
		 * @param x x component of the other vector to compare
		 * @param y y component of the other vector to compare
		 * @return true if vector are equal, otherwise false */
		public bool epsilonEquals(FP x, FP y)
		{
			return epsilonEquals(x, y, FPMath.EPSILION);
		}

		public bool isUnit()
		{
			return isUnit(0.000000001f);
		}

		public bool isUnit(FP margin)
		{
			return FPMath.Abs(len2() - 1f) < margin;
		}

		public bool isZero()
		{
			return x == 0 && y == 0;
		}

		public bool isZero(FP margin)
		{
			return len2() < margin;
		}

		public bool isOnLine(FPVector2 other)
		{
			return FPMath.IsZero(x * other.y - y * other.x);
		}

		public bool isOnLine(FPVector2 other, FP epsilon)
		{
			return FPMath.IsZero(x * other.y - y * other.x, epsilon);
		}

		public bool isCollinear(FPVector2 other, FP epsilon)
		{
			return isOnLine(other, epsilon) && dot(other) > 0f;
		}

		public bool isCollinear(FPVector2 other)
		{
			return isOnLine(other) && dot(other) > 0f;
		}

		public bool isCollinearOpposite(FPVector2 other, FP epsilon)
		{
			return isOnLine(other, epsilon) && dot(other) < 0f;
		}

		public bool isCollinearOpposite(FPVector2 other)
		{
			return isOnLine(other) && dot(other) < 0f;
		}

		public bool isPerpendicular(FPVector2 vector)
		{
			return FPMath.IsZero(dot(vector));
		}

		public bool isPerpendicular(FPVector2 vector, FP epsilon)
		{
			return FPMath.IsZero(dot(vector), epsilon);
		}

		public bool hasSameDirection(FPVector2 vector)
		{
			return dot(vector) > 0;
		}

		public bool hasOppositeDirection(FPVector2 vector)
		{
			return dot(vector) < 0;
		}

		public FPVector2 setZero()
		{
			x = 0;
			y = 0;
			return this;
		}
	}
}
