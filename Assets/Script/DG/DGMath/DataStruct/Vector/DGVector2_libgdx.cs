
namespace DG
{
	public partial struct DGVector2
	{
		public static DGVector2 X = new DGVector2((DGFixedPoint)1, (DGFixedPoint)0);
		public static DGVector2 Y = new DGVector2((DGFixedPoint)0, (DGFixedPoint)1);
		public static DGVector2 Zero = new DGVector2((DGFixedPoint)0, (DGFixedPoint)0);

		/** the x-component of this vector **/
		public DGFixedPoint x;

		/** the y-component of this vector **/
		public DGFixedPoint y;

		/** Constructs a vector with the given components
		 * @param x The x-component
		 * @param y The y-component */
		public DGVector2(DGFixedPoint x, DGFixedPoint y)
		{
			this.x = x;
			this.y = y;
		}

		/** Constructs a vector from the given vector
		 * @param v The vector */
		public DGVector2(DGVector2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		public DGVector2 cpy()
		{
			return new DGVector2(this);
		}

		public static DGFixedPoint len(DGFixedPoint x, DGFixedPoint y)
		{
			return DGMath.Sqrt(x * x + y * y);
		}

		public DGFixedPoint len()
		{
			return DGMath.Sqrt(x * x + y * y);
		}

		public static DGFixedPoint len2(DGFixedPoint x, DGFixedPoint y)
		{
			return x * x + y * y;
		}

		public DGFixedPoint len2()
		{
			return x * x + y * y;
		}

		public DGVector2 set(DGVector2 v)
		{
			x = v.x;
			y = v.y;
			return this;
		}

		/** Sets the components of this vector
		 * @param x The x-component
		 * @param y The y-component
		 * @return This vector for chaining */
		public DGVector2 set(DGFixedPoint x, DGFixedPoint y)
		{
			this.x = x;
			this.y = y;
			return this;
		}

		public DGVector2 sub(DGVector2 v)
		{
			x -= v.x;
			y -= v.y;
			return this;
		}

		/** Substracts the other vector from this vector.
		 * @param x The x-component of the other vector
		 * @param y The y-component of the other vector
		 * @return This vector for chaining */
		public DGVector2 sub(DGFixedPoint x, DGFixedPoint y)
		{
			this.x -= x;
			this.y -= y;
			return this;
		}

		public DGVector2 nor()
		{
			DGFixedPoint len = this.len();
			if (len != (DGFixedPoint)0)
			{
				x /= len;
				y /= len;
			}

			return this;
		}

		public DGVector2 add(DGVector2 v)
		{
			x += v.x;
			y += v.y;
			return this;
		}

		/** Adds the given components to this vector
		 * @param x The x-component
		 * @param y The y-component
		 * @return This vector for chaining */
		public DGVector2 add(DGFixedPoint x, DGFixedPoint y)
		{
			this.x += x;
			this.y += y;
			return this;
		}

		public static DGFixedPoint dot(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2)
		{
			return x1 * x2 + y1 * y2;
		}

		public DGFixedPoint dot(DGVector2 v)
		{
			return x * v.x + y * v.y;
		}

		public DGFixedPoint dot(DGFixedPoint ox, DGFixedPoint oy)
		{
			return x * ox + y * oy;
		}

		public DGVector2 scl(DGFixedPoint scalar)
		{
			x *= scalar;
			y *= scalar;
			return this;
		}

		/** Multiplies this vector by a scalar
		 * @return This vector for chaining */
		public DGVector2 scl(DGFixedPoint x, DGFixedPoint y)
		{
			this.x *= x;
			this.y *= y;
			return this;
		}

		public DGVector2 scl(DGVector2 v)
		{
			this.x *= v.x;
			this.y *= v.y;
			return this;
		}

		public DGVector2 mulAdd(DGVector2 vec, DGFixedPoint scalar)
		{
			this.x += vec.x * scalar;
			this.y += vec.y * scalar;
			return this;
		}

		public DGVector2 mulAdd(DGVector2 vec, DGVector2 mulVec)
		{
			this.x += vec.x * mulVec.x;
			this.y += vec.y * mulVec.y;
			return this;
		}

		public static DGFixedPoint dst(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2)
		{
			DGFixedPoint x_d = x2 - x1;
			DGFixedPoint y_d = y2 - y1;
			return DGMath.Sqrt(x_d * x_d + y_d * y_d);
		}

		public DGFixedPoint dst(DGVector2 v)
		{
			DGFixedPoint x_d = v.x - x;
			DGFixedPoint y_d = v.y - y;
			return DGMath.Sqrt(x_d * x_d + y_d * y_d);
		}

		/** @param x The x-component of the other vector
		 * @param y The y-component of the other vector
		 * @return the distance between this and the other vector */
		public DGFixedPoint dst(DGFixedPoint x, DGFixedPoint y)
		{
			DGFixedPoint x_d = x - this.x;
			DGFixedPoint y_d = y - this.y;
			return DGMath.Sqrt(x_d * x_d + y_d * y_d);
		}

		public static DGFixedPoint dst2(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2)
		{
			DGFixedPoint x_d = x2 - x1;
			DGFixedPoint y_d = y2 - y1;
			return x_d * x_d + y_d * y_d;
		}

		public DGFixedPoint dst2(DGVector2 v)
		{
			DGFixedPoint x_d = v.x - x;
			DGFixedPoint y_d = v.y - y;
			return x_d * x_d + y_d * y_d;
		}

		/** @param x The x-component of the other vector
		 * @param y The y-component of the other vector
		 * @return the squared distance between this and the other vector */
		public DGFixedPoint dst2(DGFixedPoint x, DGFixedPoint y)
		{
			DGFixedPoint x_d = x - this.x;
			DGFixedPoint y_d = y - this.y;
			return x_d * x_d + y_d * y_d;
		}


		public DGVector2 limit(DGFixedPoint limit)
		{
			return limit2(limit * limit);
		}


		public DGVector2 limit2(DGFixedPoint limit2)
		{
			DGFixedPoint len2 = this.len2();
			if (len2 > limit2)
			{
				return scl(DGMath.Sqrt(limit2 / len2));
			}

			return this;
		}

		public DGVector2 clamp(DGFixedPoint min, DGFixedPoint max)
		{
			DGFixedPoint len2 = this.len2();
			if (len2 == (DGFixedPoint)0f) return this;
			DGFixedPoint max2 = max * max;
			if (len2 > max2) return scl(DGMath.Sqrt(max2 / len2));
			DGFixedPoint min2 = min * min;
			if (len2 < min2) return scl(DGMath.Sqrt(min2 / len2));
			return this;
		}

		public DGVector2 setLength(DGFixedPoint len)
		{
			return setLength2(len * len);
		}

		public DGVector2 setLength2(DGFixedPoint len2)
		{
			DGFixedPoint oldLen2 = this.len2();
			return (oldLen2 == (DGFixedPoint)0 || oldLen2 == len2) ? this : scl(DGMath.Sqrt(len2 / oldLen2));
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
		public DGVector2 mul(DGMatrix3x3 mat)
		{
			DGFixedPoint x = this.x * mat.m00 + this.y * mat.m01 + mat.m02;
			DGFixedPoint y = this.x * mat.m10 + this.y * mat.m11 + mat.m12;
			this.x = x;
			this.y = y;
			return this;
		}

		/** Calculates the 2D cross product between this and the given vector.
		 * @param v the other vector
		 * @return the cross product */
		public DGFixedPoint crs(DGVector2 v)
		{
			return this.x * v.y - this.y * v.x;
		}

		/** Calculates the 2D cross product between this and the given vector.
		 * @param x the x-coordinate of the other vector
		 * @param y the y-coordinate of the other vector
		 * @return the cross product */
		public DGFixedPoint crs(DGFixedPoint x, DGFixedPoint y)
		{
			return this.x * y - this.y * x;
		}

		/** @return the angle in degrees of this vector (point) relative to the x-axis. Angles are towards the positive y-axis
		 *         (typically counter-clockwise) and between 0 and 360.
		 * @deprecated use {@link #angleDeg()} instead. */
		public DGFixedPoint angle()
		{
			DGFixedPoint angle = DGMath.Atan2(y, x) * DGMath.Rad2Deg;
			if (angle < (DGFixedPoint)0) angle += (DGFixedPoint)360;
			return angle;
		}

		/** @return the angle in degrees of this vector (point) relative to the given vector. Angles are towards the negative y-axis
		 *         (typically clockwise) between -180 and +180
		 * @deprecated use {@link #angleDeg(Vector2)} instead. Be ware of the changes in returned angle to counter-clockwise and the
		 *             range. */
		public DGFixedPoint angle(DGVector2 reference)
		{
			return DGMath.Atan2(crs(reference), dot(reference)) * DGMath.Rad2Deg;
		}

		/** @return the angle in degrees of this vector (point) relative to the x-axis. Angles are towards the positive y-axis
		 *         (typically counter-clockwise) and in the [0, 360) range. */
		public DGFixedPoint angleDeg()
		{
			DGFixedPoint angle = DGMath.Atan2(y, x) * DGMath.Rad2Deg;
			if (angle < (DGFixedPoint)0) angle += (DGFixedPoint)360;
			return angle;
		}

		/** @return the angle in degrees of this vector (point) relative to the given vector. Angles are towards the positive y-axis
		 *         (typically counter-clockwise.) in the [0, 360) range */
		public DGFixedPoint angleDeg(DGVector2 reference)
		{
			DGFixedPoint angle = DGMath.Atan2(reference.crs(this), reference.dot(this)) * DGMath.Rad2Deg;
			if (angle < (DGFixedPoint)0) angle += (DGFixedPoint)360;
			return angle;
		}

		/** @return the angle in radians of this vector (point) relative to the x-axis. Angles are towards the positive y-axis.
		 *         (typically counter-clockwise) */
		public DGFixedPoint angleRad()
		{
			return DGMath.Atan2(y, x);
		}

		/** @return the angle in radians of this vector (point) relative to the given vector. Angles are towards the positive y-axis.
		 *         (typically counter-clockwise.) */
		public DGFixedPoint angleRad(DGVector2 reference)
		{
			return DGMath.Atan2(reference.crs(this), reference.dot(this));
		}

		/** Sets the angle of the vector in degrees relative to the x-axis, towards the positive y-axis (typically counter-clockwise).
		 * @param degrees The angle in degrees to set.
		 * @deprecated use {@link #setAngleDeg(float)} instead. */
		public DGVector2 setAngle(DGFixedPoint degrees)
		{
			return setAngleRad(degrees * DGMath.Deg2Rad);
		}

		/** Sets the angle of the vector in degrees relative to the x-axis, towards the positive y-axis (typically counter-clockwise).
		 * @param degrees The angle in degrees to set. */
		public DGVector2 setAngleDeg(DGFixedPoint degrees)
		{
			return setAngleRad(degrees * DGMath.Deg2Rad);
		}

		/** Sets the angle of the vector in radians relative to the x-axis, towards the positive y-axis (typically counter-clockwise).
		 * @param radians The angle in radians to set. */
		public DGVector2 setAngleRad(DGFixedPoint radians)
		{
			this.set(len(), (DGFixedPoint)0f);
			this.rotateRad(radians);

			return this;
		}

		/** Rotates the Vector2 by the given angle, counter-clockwise assuming the y-axis points up.
		 * @param degrees the angle in degrees
		 * @deprecated use {@link #rotateDeg(float)} instead. */
		public DGVector2 rotate(DGFixedPoint degrees)
		{
			return rotateRad(degrees * DGMath.Deg2Rad);
		}

		/** Rotates the Vector2 by the given angle around reference vector, counter-clockwise assuming the y-axis points up.
		 * @param degrees the angle in degrees
		 * @param reference center Vector2
		 * @deprecated use {@link #rotateAroundDeg(Vector2, float)} instead. */
		public DGVector2 rotateAround(DGVector2 reference, DGFixedPoint degrees)
		{
			return this.sub(reference).rotateDeg(degrees).add(reference);
		}

		/** Rotates the Vector2 by the given angle, counter-clockwise assuming the y-axis points up.
		 * @param degrees the angle in degrees */
		public DGVector2 rotateDeg(DGFixedPoint degrees)
		{
			return rotateRad(degrees * DGMath.Deg2Rad);
		}

		/** Rotates the Vector2 by the given angle, counter-clockwise assuming the y-axis points up.
		 * @param radians the angle in radians */
		public DGVector2 rotateRad(DGFixedPoint radians)
		{
			DGFixedPoint cos = DGMath.Cos(radians);
			DGFixedPoint sin = DGMath.Sin(radians);

			DGFixedPoint newX = this.x * cos - this.y * sin;
			DGFixedPoint newY = this.x * sin + this.y * cos;

			this.x = newX;
			this.y = newY;

			return this;
		}

		/** Rotates the Vector2 by the given angle around reference vector, counter-clockwise assuming the y-axis points up.
		 * @param degrees the angle in degrees
		 * @param reference center Vector2 */
		public DGVector2 rotateAroundDeg(DGVector2 reference, DGFixedPoint degrees)
		{
			return this.sub(reference).rotateDeg(degrees).add(reference);
		}

		/** Rotates the Vector2 by the given angle around reference vector, counter-clockwise assuming the y-axis points up.
		 * @param radians the angle in radians
		 * @param reference center Vector2 */
		public DGVector2 rotateAroundRad(DGVector2 reference, DGFixedPoint radians)
		{
			return this.sub(reference).rotateRad(radians).add(reference);
		}

		/** Rotates the Vector2 by 90 degrees in the specified direction, where >= 0 is counter-clockwise and < 0 is clockwise. */
		public DGVector2 rotate90(int dir)
		{
			DGFixedPoint x = this.x;
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

		public DGVector2 lerp(DGVector2 target, DGFixedPoint alpha)
		{
			DGFixedPoint invAlpha = (DGFixedPoint)1.0f - alpha;
			this.x = (x * invAlpha) + (target.x * alpha);
			this.y = (y * invAlpha) + (target.y * alpha);
			return this;
		}

		public DGVector2 interpolate(DGVector2 target, DGFixedPoint alpha, DGInterpolation interpolation)
		{
			return lerp(target, interpolation.Apply(alpha));
		}


		public bool epsilonEquals(DGVector2 other, DGFixedPoint epsilon)
		{
			if (DGMath.Abs(other.x - x) > epsilon) return false;
			if (DGMath.Abs(other.y - y) > epsilon) return false;
			return true;
		}

		/** Compares this vector with the other vector, using the supplied epsilon for fuzzy equality testing.
		 * @return whether the vectors are the same. */
		public bool epsilonEquals(DGFixedPoint x, DGFixedPoint y, DGFixedPoint epsilon)
		{
			if (DGMath.Abs(x - this.x) > epsilon) return false;
			if (DGMath.Abs(y - this.y) > epsilon) return false;
			return true;
		}

		/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
		 * @param other other vector to compare
		 * @return true if vector are equal, otherwise false */
		public bool epsilonEquals(DGVector2 other)
		{
			return epsilonEquals(other, DGMath.Epsilon);
		}

		/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
		 * @param x x component of the other vector to compare
		 * @param y y component of the other vector to compare
		 * @return true if vector are equal, otherwise false */
		public bool epsilonEquals(DGFixedPoint x, DGFixedPoint y)
		{
			return epsilonEquals(x, y, DGMath.Epsilon);
		}

		public bool isUnit()
		{
			return isUnit((DGFixedPoint)0.000000001f);
		}

		public bool isUnit(DGFixedPoint margin)
		{
			return DGMath.Abs(len2() - (DGFixedPoint)1f) < margin;
		}

		public bool isZero()
		{
			return x == (DGFixedPoint)0 && y == (DGFixedPoint)0;
		}

		public bool isZero(DGFixedPoint margin)
		{
			return len2() < margin;
		}

		public bool isOnLine(DGVector2 other)
		{
			return DGMath.IsZero(x * other.y - y * other.x);
		}

		public bool isOnLine(DGVector2 other, DGFixedPoint epsilon)
		{
			return DGMath.IsZero(x * other.y - y * other.x, epsilon);
		}

		public bool isCollinear(DGVector2 other, DGFixedPoint epsilon)
		{
			return isOnLine(other, epsilon) && dot(other) > (DGFixedPoint)0f;
		}

		public bool isCollinear(DGVector2 other)
		{
			return isOnLine(other) && dot(other) > (DGFixedPoint)0f;
		}

		public bool isCollinearOpposite(DGVector2 other, DGFixedPoint epsilon)
		{
			return isOnLine(other, epsilon) && dot(other) < (DGFixedPoint)0f;
		}

		public bool isCollinearOpposite(DGVector2 other)
		{
			return isOnLine(other) && dot(other) < (DGFixedPoint)0f;
		}

		public bool isPerpendicular(DGVector2 vector)
		{
			return DGMath.IsZero(dot(vector));
		}

		public bool isPerpendicular(DGVector2 vector, DGFixedPoint epsilon)
		{
			return DGMath.IsZero(dot(vector), epsilon);
		}

		public bool hasSameDirection(DGVector2 vector)
		{
			return dot(vector) > (DGFixedPoint)0;
		}

		public bool hasOppositeDirection(DGVector2 vector)
		{
			return dot(vector) < (DGFixedPoint)0;
		}

		public DGVector2 setZero()
		{
			this.x = (DGFixedPoint)0;
			this.y = (DGFixedPoint)0;
			return this;
		}
	}
}
