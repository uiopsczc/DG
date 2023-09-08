/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/5/21
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/


using System.Numerics;
using System.Runtime.CompilerServices;

namespace DG
{
	public partial struct DGPlane
	{
		/// <summary>
		///   <para>Returns a copy of the plane that faces in the opposite direction.</para>
		/// </summary>
		public DGPlane flipped => new DGPlane(-this.normal, -this.d);

		/// <summary>
		/// Constructs a Plane from the X, Y, and Z components of its normal, and its distance from the origin on that normal.
		/// </summary>
		/// <param name="x">The X-component of the normal.</param>
		/// <param name="y">The Y-component of the normal.</param>
		/// <param name="z">The Z-component of the normal.</param>
		/// <param name="d">The distance of the Plane along its normal from the origin.</param>
		public DGPlane(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint d)
		{
			normal = new DGVector3(x, y, z);
			this.d = d;
		}


		/// <summary>
		/// Constructs a Plane from the given Vector4.
		/// </summary>
		/// <param name="value">A vector whose first 3 elements describe the normal vector, 
		/// and whose W component defines the distance along that normal from the origin.</param>
		public DGPlane(DGVector4 value)
		{
			normal = new DGVector3(value.x, value.y, value.z);
			d = value.w;
		}

#if UNITY_STANDALONE
		public DGPlane(UnityEngine.Plane value)
		{
			normal = new DGVector3(value.normal);
			d = (DGFixedPoint) value.distance;
		}
#endif
		/*************************************************************************************
		* 模块描述:Equal ToString
		*************************************************************************************/
		/// <summary>
		/// Returns a boolean indicating whether the given Plane is equal to this Plane instance.
		/// </summary>
		/// <param name="other">The Plane to compare this instance to.</param>
		/// <returns>True if the other Plane is equal to this instance; False otherwise.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(DGPlane other)
		{
//			if (Vector.IsHardwareAccelerated)
//				return this.normal.Equals(other.normal) && this.d == other.d;

			return (normal.x == other.normal.x &&
			        normal.y == other.normal.y &&
			        normal.z == other.normal.z &&
			        d == other.d);
		}

		/// <summary>
		/// Returns a boolean indicating whether the given Object is equal to this Plane instance.
		/// </summary>
		/// <param name="obj">The Object to compare against.</param>
		/// <returns>True if the Object is equal to this Plane; False otherwise.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			if (obj is DGPlane plane)
				return Equals(plane);

			return false;
		}

		public override int GetHashCode()
		{
			return normal.GetHashCode() + d.GetHashCode();
		}

		/*************************************************************************************
		* 模块描述:关系运算符
		*************************************************************************************/
		/// <summary>
		/// Returns a boolean indicating whether the two given Planes are equal.
		/// </summary>
		/// <param name="value1">The first Plane to compare.</param>
		/// <param name="value2">The second Plane to compare.</param>
		/// <returns>True if the Planes are equal; False otherwise.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(DGPlane value1, DGPlane value2)
		{
			return (value1.normal.x == value2.normal.x &&
			        value1.normal.y == value2.normal.y &&
			        value1.normal.z == value2.normal.z &&
			        value1.d == value2.d);
		}

		/// <summary>
		/// Returns a boolean indicating whether the two given Planes are not equal.
		/// </summary>
		/// <param name="value1">The first Plane to compare.</param>
		/// <param name="value2">The second Plane to compare.</param>
		/// <returns>True if the Planes are not equal; False if they are equal.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(DGPlane value1, DGPlane value2)
		{
			return (value1.normal.x != value2.normal.x ||
			        value1.normal.y != value2.normal.y ||
			        value1.normal.z != value2.normal.z ||
			        value1.d != value2.d);
		}
		/*************************************************************************************
		* 模块描述:StaticUtil
		*************************************************************************************/

		/// <summary>
		/// Creates a Plane that contains the three given points.
		/// </summary>
		/// <param name="point1">The first point defining the Plane.</param>
		/// <param name="point2">The second point defining the Plane.</param>
		/// <param name="point3">The third point defining the Plane.</param>
		/// <returns>The Plane containing the three points.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DGPlane CreateFromVertices(DGVector3 point1, DGVector3 point2, DGVector3 point3)
		{
//			if (Vector.IsHardwareAccelerated)
//			{
//				DGVector3 a = point2 - point1;
//				DGVector3 b = point3 - point1;
//
//				// N = Cross(a, b)
//				DGVector3 n = DGVector3.Cross(a, b);
//				DGVector3 normal = DGVector3.Normalize(n);
//
//				// D = - Dot(N, point1)
//				DGFixedPoint d = -DGVector3.Dot(normal, point1);
//
//				return new DGPlane(normal, d);
//			}
//			else
			{
				DGFixedPoint ax = point2.x - point1.x;
				DGFixedPoint ay = point2.y - point1.y;
				DGFixedPoint az = point2.z - point1.z;

				DGFixedPoint bx = point3.x - point1.x;
				DGFixedPoint by = point3.y - point1.y;
				DGFixedPoint bz = point3.z - point1.z;

				// N=Cross(a,b)
				DGFixedPoint nx = ay * bz - az * by;
				DGFixedPoint ny = az * bx - ax * bz;
				DGFixedPoint nz = ax * by - ay * bx;

				// Normalize(N)
				DGFixedPoint ls = nx * nx + ny * ny + nz * nz;
				DGFixedPoint invNorm = (DGFixedPoint) 1.0f / DGMath.Sqrt(ls);

				DGVector3 normal = new DGVector3(
					nx * invNorm,
					ny * invNorm,
					nz * invNorm);

				return new DGPlane(
					normal,
					-(normal.x * point1.x + normal.y * point1.y + normal.z * point1.z));
			}
		}

		/// <summary>
		/// Creates a new Plane whose normal vector is the source Plane's normal vector normalized.
		/// </summary>
		/// <param name="value">The source Plane.</param>
		/// <returns>The normalized Plane.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DGPlane Normalize(DGPlane value)
		{
//			if (Vector.IsHardwareAccelerated)
//			{
//				DGFixedPoint normalLengthSquared = value.normal.sqrMagnitude;
//				if (DGMath.Abs(normalLengthSquared - (DGFixedPoint) 1.0f) < DGMath.Epsilon)
//				{
//					// It already normalized, so we don't need to farther process.
//					return value;
//				}
//
//				DGFixedPoint normalLength = DGMath.Sqrt(normalLengthSquared);
//				return new DGPlane(
//					value.normal / normalLength,
//					value.d / normalLength);
//			}

			DGFixedPoint f = value.normal.x * value.normal.x + value.normal.y * value.normal.y +
			                 value.normal.z * value.normal.z;

			if (DGMath.Abs(f - (DGFixedPoint) 1.0f) < DGMath.Epsilon)
			{
				return value; // It already normalized, so we don't need to further process.
			}

			DGFixedPoint fInv = (DGFixedPoint) 1.0f / DGMath.Sqrt(f);

			return new DGPlane(
				value.normal.x * fInv,
				value.normal.y * fInv,
				value.normal.z * fInv,
				value.d * fInv);
		}

		/// <summary>
		/// Transforms a normalized Plane by a Matrix.
		/// </summary>
		/// <param name="plane"> The normalized Plane to transform. 
		/// This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
		/// <param name="matrix">The transformation matrix to apply to the Plane.</param>
		/// <returns>The transformed Plane.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DGPlane Transform(DGPlane plane, DGMatrix4x4 matrix)
		{
			DGMatrix4x4 m = DGMatrix4x4.Invert(matrix);

			DGFixedPoint x = plane.normal.x, y = plane.normal.y, z = plane.normal.z, w = plane.d;

			return new DGPlane(
				x * m.sm11 + y * m.sm12 + z * m.sm13 + w * m.sm14,
				x * m.sm21 + y * m.sm22 + z * m.sm23 + w * m.sm24,
				x * m.sm31 + y * m.sm32 + z * m.sm33 + w * m.sm34,
				x * m.sm41 + y * m.sm42 + z * m.sm43 + w * m.sm44);
		}

		/// <summary>
		///  Transforms a normalized Plane by a Quaternion rotation.
		/// </summary>
		/// <param name="plane"> The normalized Plane to transform.
		/// This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
		/// <param name="rotation">The Quaternion rotation to apply to the Plane.</param>
		/// <returns>A new Plane that results from applying the rotation.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DGPlane Transform(DGPlane plane, DGQuaternion rotation)
		{
			// Compute rotation matrix.
			DGFixedPoint x2 = rotation.x + rotation.x;
			DGFixedPoint y2 = rotation.y + rotation.y;
			DGFixedPoint z2 = rotation.z + rotation.z;

			DGFixedPoint wx2 = rotation.w * x2;
			DGFixedPoint wy2 = rotation.w * y2;
			DGFixedPoint wz2 = rotation.w * z2;
			DGFixedPoint xx2 = rotation.x * x2;
			DGFixedPoint xy2 = rotation.x * y2;
			DGFixedPoint xz2 = rotation.x * z2;
			DGFixedPoint yy2 = rotation.y * y2;
			DGFixedPoint yz2 = rotation.y * z2;
			DGFixedPoint zz2 = rotation.z * z2;

			DGFixedPoint m11 = (DGFixedPoint) 1.0f - yy2 - zz2;
			DGFixedPoint m21 = xy2 - wz2;
			DGFixedPoint m31 = xz2 + wy2;

			DGFixedPoint m12 = xy2 + wz2;
			DGFixedPoint m22 = (DGFixedPoint) 1.0f - xx2 - zz2;
			DGFixedPoint m32 = yz2 - wx2;

			DGFixedPoint m13 = xz2 - wy2;
			DGFixedPoint m23 = yz2 + wx2;
			DGFixedPoint m33 = (DGFixedPoint) 1.0f - xx2 - yy2;

			DGFixedPoint x = plane.normal.x, y = plane.normal.y, z = plane.normal.z;

			return new DGPlane(
				x * m11 + y * m21 + z * m31,
				x * m12 + y * m22 + z * m32,
				x * m13 + y * m23 + z * m33,
				plane.d);
		}

		/// <summary>
		/// Calculates the dot product of a Plane and Vector4.
		/// </summary>
		/// <param name="plane">The Plane.</param>
		/// <param name="value">The Vector4.</param>
		/// <returns>The dot product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DGFixedPoint Dot(DGPlane plane, DGVector4 value)
		{
			return plane.normal.x * value.x +
			       plane.normal.y * value.y +
			       plane.normal.z * value.z +
			       plane.d * value.w;
		}

		/// <summary>
		/// Returns the dot product of a specified Vector3 and the normal vector of this Plane plus the distance (D) value of the Plane.
		/// </summary>
		/// <param name="plane">The plane.</param>
		/// <param name="value">The Vector3.</param>
		/// <returns>The resulting value.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DGFixedPoint DotCoordinate(DGPlane plane, DGVector3 value)
		{
//			if (Vector.IsHardwareAccelerated)
//			{
//				return DGVector3.Dot(plane.normal, value) + plane.d;
//			}

			return plane.normal.x * value.x +
			       plane.normal.y * value.y +
			       plane.normal.z * value.z +
			       plane.d;
		}

		/// <summary>
		/// Returns the dot product of a specified Vector3 and the Normal vector of this Plane.
		/// </summary>
		/// <param name="plane">The plane.</param>
		/// <param name="value">The Vector3.</param>
		/// <returns>The resulting dot product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DGFixedPoint DotNormal(DGPlane plane, DGVector3 value)
		{
//			if (System.Numerics.Vector.IsHardwareAccelerated)
//			{
//				return DGVector3.Dot(plane.normal, value);
//			}

			return plane.normal.x * value.x +
			       plane.normal.y * value.y +
			       plane.normal.z * value.z;
		}

		/// <summary>
		///   <para>Returns a copy of the given plane that is moved in space by the given translation.</para>
		/// </summary>
		/// <param name="plane">The plane to move in space.</param>
		/// <param name="translation">The offset in space to move the plane with.</param>
		/// <returns>
		///   <para>The translated plane.</para>
		/// </returns>
		public static DGPlane Translate(DGPlane plane, DGVector3 translation)
		{
			return new DGPlane(plane.normal, plane.d += DGVector3.Dot(plane.normal, translation));
		}

		/*************************************************************************************
		* 模块描述:Member Util
		*************************************************************************************/
		/// <summary>
		///   <para>Sets a plane using a point that lies within it along with a normal to orient it.</para>
		/// </summary>
		/// <param name="inNormal">The plane's normal vector.</param>
		/// <param name="inPoint">A point that lies on the plane.</param>
		public void SetNormalAndPosition(DGVector3 inNormal, DGVector3 inPoint)
		{
			this.normal = DGVector3.Normalize(inNormal);
			this.d = -DGVector3.Dot(inNormal, inPoint);
		}

		/// <summary>
		///   <para>Sets a plane using three points that lie within it.  The points go around clockwise as you look down on the top surface of the plane.</para>
		/// </summary>
		/// <param name="a">First point in clockwise order.</param>
		/// <param name="b">Second point in clockwise order.</param>
		/// <param name="c">Third point in clockwise order.</param>
		public void Set3Points(DGVector3 a, DGVector3 b, DGVector3 c)
		{
			this.normal = DGVector3.Normalize(DGVector3.Cross(b - a, c - a));
			this.d = -DGVector3.Dot(this.normal, a);
		}

		public void Flip()
		{
			this.normal = -this.normal;
			this.d = -this.d;
		}

		/// <summary>
		///   <para>Moves the plane in space by the translation vector.</para>
		/// </summary>
		/// <param name="translation">The offset in space to move the plane with.</param>
		public void Translate(DGVector3 translation)
		{
			this.d += DGVector3.Dot(this.normal, translation);
		}

		/// <summary>
		///   <para>For a given point returns the closest point on the plane.</para>
		/// </summary>
		/// <param name="point">The point to project onto the plane.</param>
		/// <returns>
		///   <para>A point on the plane that is closest to point.</para>
		/// </returns>
		public DGVector3 ClosestPointOnPlane(DGVector3 point)
		{
			DGFixedPoint num = DGVector3.Dot(this.normal, point) + this.d;
			return point - this.normal * num;
		}

		/// <summary>
		///   <para>Returns a signed distance from plane to point.</para>
		/// </summary>
		/// <param name="point"></param>
		public DGFixedPoint GetDistanceToPoint(DGVector3 point)
		{
			return DGVector3.Dot(this.normal, point) + this.d;
		}

		/// <summary>
		///   <para>Is a point on the positive side of the plane?</para>
		/// </summary>
		/// <param name="point"></param>
		public bool GetSide(DGVector3 point)
		{
			return DGVector3.Dot(this.normal, point) + this.d > (DGFixedPoint) 0.0f;
		}

		/// <summary>
		///   <para>Are two points on the same side of the plane?</para>
		/// </summary>
		/// <param name="inPt0"></param>
		/// <param name="inPt1"></param>
		public bool SameSide(DGVector3 inPt0, DGVector3 inPt1)
		{
			DGFixedPoint distanceToPoint1 = this.GetDistanceToPoint(inPt0);
			DGFixedPoint distanceToPoint2 = this.GetDistanceToPoint(inPt1);
			return distanceToPoint1 > (DGFixedPoint) 0.0 && distanceToPoint2 > (DGFixedPoint) 0.0 ||
			       distanceToPoint1 <= (DGFixedPoint) 0.0 && distanceToPoint2 <= (DGFixedPoint) 0.0;
		}

		public bool Raycast(DGRay ray, out DGFixedPoint enter)
		{
			DGFixedPoint a = DGVector3.Dot(ray.direction, this.normal);
			DGFixedPoint num = -DGVector3.Dot(ray.origin, this.normal) - this.d;
			if (DGMath.IsApproximatelyZero(a))
			{
				enter = (DGFixedPoint) 0.0f;
				return false;
			}

			enter = num / a;
			return (double) enter > 0.0;
		}
	}
}