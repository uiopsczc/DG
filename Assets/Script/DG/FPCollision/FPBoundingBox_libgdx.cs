/*************************************************************************************
 * 描    述:
 * 创 建 者:  czq
 * 创建时间:  2023/8/16
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
 *************************************************************************************/

using System.Collections.Generic;

namespace DG
{
	public class FPBoundingBox
	{
		private static FPVector3 tmpVector;

		/** Minimum vector. All XYZ components should be inferior to corresponding {@link #max} components. Call {@link #update()} if
		 * you manually change this vector. */
		public FPVector3 min;

		/** Maximum vector. All XYZ components should be superior to corresponding {@link #min} components. Call {@link #update()} if
		 * you manually change this vector. */
		public FPVector3 max;

		private FPVector3 cnt;
		private FPVector3 dim;

		/** @param out The {@link Vector3} to receive the center of the bounding box.
		 * @return The vector specified with the out argument. */
		public FPVector3 getCenter(ref FPVector3 outCenter)
		{
			var result = getCenter();
			outCenter = result;
			return result;
		}
		public FPVector3 getCenter()
		{
			return cnt.cpy();
		}

		public FP getCenterX()
		{
			return cnt.x;
		}

		public FP getCenterY()
		{
			return cnt.y;
		}

		public FP getCenterZ()
		{
			return cnt.z;
		}

		public FPVector3 getCorner000(ref FPVector3 outCorner)
		{
			if (outCorner == FPVector3.Null)
				return new FPVector3(min.x, min.y, min.z);
			return outCorner.set(min.x, min.y, min.z);
		}

		public FPVector3 getCorner000()
		{
			return getCorner000(ref FPVector3.Null);
		}

		public FPVector3 getCorner001(ref FPVector3 outCorner)
		{
			if (outCorner == FPVector3.Null)
				return new FPVector3(min.x, min.y, max.z);
			return outCorner.set(min.x, min.y, max.z);
		}

		public FPVector3 getCorner001()
		{
			return getCorner001(ref FPVector3.Null);
		}

		public FPVector3 getCorner010(ref FPVector3 outCorner)
		{
			if (outCorner == FPVector3.Null)
				return new FPVector3(min.x, max.y, min.z);
			return outCorner.set(min.x, max.y, min.z);
		}

		public FPVector3 getCorner010()
		{
			return getCorner010(ref FPVector3.Null);
		}

		public FPVector3 getCorner011(ref FPVector3 outCorner)
		{
			if (outCorner == FPVector3.Null)
				return new FPVector3(min.x, max.y, max.z);
			return outCorner.set(min.x, max.y, max.z);
		}

		public FPVector3 getCorner011()
		{
			return getCorner011(ref FPVector3.Null);
		}

		public FPVector3 getCorner100(ref FPVector3 outCorner)
		{
			if (outCorner == FPVector3.Null)
				return new FPVector3(max.x, min.y, min.z);
			return outCorner.set(max.x, min.y, min.z);
		}

		public FPVector3 getCorner100()
		{
			return getCorner100(ref FPVector3.Null);
		}

		public FPVector3 getCorner101(ref FPVector3 outCorner)
		{
			if (outCorner == FPVector3.Null)
				return new FPVector3(max.x, min.y, max.z);
			return outCorner.set(max.x, min.y, max.z);
		}

		public FPVector3 getCorner101()
		{
			return getCorner101(ref FPVector3.Null);
		}

		public FPVector3 getCorner110(ref FPVector3 outCorner)
		{
			if (outCorner == FPVector3.Null)
				return new FPVector3(max.x, max.y, min.z);
			return outCorner.set(max.x, max.y, min.z);
		}

		public FPVector3 getCorner110()
		{
			return getCorner110(ref FPVector3.Null);
		}

		public FPVector3 getCorner111(ref FPVector3 outCorner)
		{
			if (outCorner == FPVector3.Null)
				return new FPVector3(max.x, max.y, max.z);
			return outCorner.set(max.x, max.y, max.z);
		}

		public FPVector3 getCorner111()
		{
			return getCorner111(ref FPVector3.Null);
		}

		/** @param out The {@link Vector3} to receive the dimensions of this bounding box on all three axis.
		 * @return The vector specified with the out argument */
		public FPVector3 getDimensions(ref FPVector3 outCorner)
		{
			if (outCorner == FPVector3.Null)
				return dim.cpy();
			return outCorner.set(dim);
		}

		public FPVector3 getDimensions()
		{
			return getDimensions(ref FPVector3.Null);
		}

		public FP getWidth()
		{
			return dim.x;
		}

		public FP getHeight()
		{
			return dim.y;
		}

		public FP getDepth()
		{
			return dim.z;
		}

		/** @param out The {@link Vector3} to receive the minimum values.
		 * @return The vector specified with the out argument */
		public FPVector3 getMin(ref FPVector3 outMin)
		{
			if (outMin == FPVector3.Null)
				return min.cpy();
			return outMin.set(min);
		}

		public FPVector3 getMin()
		{
			return getMin(ref FPVector3.Null);
		}

		/** @param out The {@link Vector3} to receive the maximum values.
		 * @return The vector specified with the out argument */
		public FPVector3 getMax(ref FPVector3 outMax)
		{
			if (outMax == FPVector3.Null)
				return max.cpy();
			return outMax.set(max);
		}
		public FPVector3 getMax()
		{
			return getMax(ref FPVector3.Null);
		}


		/** Constructs a new bounding box with the minimum and maximum vector set to zeros. */
		public FPBoundingBox()
		{
			clr();
		}

		/** Constructs a new bounding box from the given bounding box.
		 *
		 * @param bounds The bounding box to copy */
		public FPBoundingBox(FPBoundingBox bounds)
		{
			set(bounds);
		}

		/** Constructs the new bounding box using the given minimum and maximum vector.
		 *
		 * @param minimum The minimum vector
		 * @param maximum The maximum vector */
		public FPBoundingBox(FPVector3 minimum, FPVector3 maximum)
		{
			set(minimum, maximum);
		}

		/** Sets the given bounding box.
		 *
		 * @param bounds The bounds.
		 * @return This bounding box for chaining. */
		public FPBoundingBox set(FPBoundingBox bounds)
		{
			return set(bounds.min, bounds.max);
		}

		/** Sets the given minimum and maximum vector.
		 *
		 * @param minimum The minimum vector
		 * @param maximum The maximum vector
		 * @return This bounding box for chaining. */
		public FPBoundingBox set(FPVector3 minimum, FPVector3 maximum)
		{
			min.set(minimum.x < maximum.x ? minimum.x : maximum.x, minimum.y < maximum.y ? minimum.y : maximum.y,
				minimum.z < maximum.z ? minimum.z : maximum.z);
			max.set(minimum.x > maximum.x ? minimum.x : maximum.x, minimum.y > maximum.y ? minimum.y : maximum.y,
				minimum.z > maximum.z ? minimum.z : maximum.z);
			update();
			return this;
		}

		/** Should be called if you modify {@link #min} and/or {@link #max} vectors manually. */
		public void update()
		{
			cnt = cnt.set(min).add(max).scl(0.5f);
			dim = dim.set(max).sub(min);
		}

		/** Sets the bounding box minimum and maximum vector from the given points.
		 *
		 * @param points The points.
		 * @return This bounding box for chaining. */
		public FPBoundingBox set(FPVector3[] points)
		{
			inf();
			for (int i = 0; i < points.Length; i++)
			{
				var l_point = points[i];
				ext(l_point);
			}

			return this;
		}

		/** Sets the bounding box minimum and maximum vector from the given points.
		 *
		 * @param points The points.
		 * @return This bounding box for chaining. */
		public FPBoundingBox set(List<FPVector3> points)
		{
			inf();
			for (int i = 0; i < points.Count; i++)
			{
				var l_point = points[i];
				ext(l_point);
			}

			return this;
		}

		/** Sets the minimum and maximum vector to positive and negative infinity.
		 *
		 * @return This bounding box for chaining. */
		public FPBoundingBox inf()
		{
			min.set(FP.MAX_VALUE, FP.MAX_VALUE, FP.MAX_VALUE);
			max.set(FP.MIN_VALUE, FP.MIN_VALUE, FP.MIN_VALUE);
			cnt.set(0, 0, 0);
			dim.set(0, 0, 0);
			return this;
		}

		/** Extends the bounding box to incorporate the given {@link Vector3}.
		 * @param point The vector
		 * @return This bounding box for chaining. */
		public FPBoundingBox ext(FPVector3 point)
		{
			return set(min.set(FPMath.Min(min.x, point.x), FPMath.Min(min.y, point.y), FPMath.Min(min.z, point.z)),
				max.set(FPMath.Max(max.x, point.x), FPMath.Max(max.y, point.y), FPMath.Max(max.z, point.z)));
		}

		/** Sets the minimum and maximum vector to zeros.
		 * @return This bounding box for chaining. */
		public FPBoundingBox clr()
		{
			return set(min.set(0, 0, 0),
				max.set(0, 0, 0));
		}

		/** Returns whether this bounding box is valid. This means that {@link #max} is greater than or equal to {@link #min}.
		 * @return True in case the bounding box is valid, false otherwise */
		public bool isValid()
		{
			return min.x <= max.x && min.y <= max.y && min.z <= max.z;
		}

		/** Extends this bounding box by the given bounding box.
		 *
		 * @param a_bounds The bounding box
		 * @return This bounding box for chaining. */
		public FPBoundingBox ext(FPBoundingBox a_bounds)
		{
			return set(min.set(FPMath.Min(min.x, a_bounds.min.x), FPMath.Min(min.y, a_bounds.min.y), FPMath.Min(min.z, a_bounds.min.z)),
				max.set(FPMath.Max(max.x, a_bounds.max.x), FPMath.Max(max.y, a_bounds.max.y), FPMath.Max(max.z, a_bounds.max.z)));
		}

		/** Extends this bounding box by the given sphere.
		 *
		 * @param center Sphere center
		 * @param radius Sphere radius
		 * @return This bounding box for chaining. */
		public FPBoundingBox ext(FPVector3 center, FP radius)
		{
			return set(
				min.set(FPMath.Min(min.x, center.x - radius), FPMath.Min(min.y, center.y - radius), FPMath.Min(min.z, center.z - radius)),
				max.set(FPMath.Max(max.x, center.x + radius), FPMath.Max(max.y, center.y + radius), FPMath.Max(max.z, center.z + radius)));
		}

		/** Extends this bounding box by the given transformed bounding box.
		 *
		 * @param bounds The bounding box
		 * @param transform The transformation matrix to apply to bounds, before using it to extend this bounding box.
		 * @return This bounding box for chaining. */
		public FPBoundingBox ext(FPBoundingBox bounds, FPMatrix4x4 transform)
		{
			ext(tmpVector.set(bounds.min.x, bounds.min.y, bounds.min.z).mul(transform));
			ext(tmpVector.set(bounds.min.x, bounds.min.y, bounds.max.z).mul(transform));
			ext(tmpVector.set(bounds.min.x, bounds.max.y, bounds.min.z).mul(transform));
			ext(tmpVector.set(bounds.min.x, bounds.max.y, bounds.max.z).mul(transform));
			ext(tmpVector.set(bounds.max.x, bounds.min.y, bounds.min.z).mul(transform));
			ext(tmpVector.set(bounds.max.x, bounds.min.y, bounds.max.z).mul(transform));
			ext(tmpVector.set(bounds.max.x, bounds.max.y, bounds.min.z).mul(transform));
			ext(tmpVector.set(bounds.max.x, bounds.max.y, bounds.max.z).mul(transform));
			return this;
		}

		/** Multiplies the bounding box by the given matrix. This is achieved by multiplying the 8 corner points and then calculating
		 * the minimum and maximum vectors from the transformed points.
		 *
		 * @param transform The matrix
		 * @return This bounding box for chaining. */
		public FPBoundingBox mul(FPMatrix4x4 transform)
		{
			FP x0 = min.x, y0 = min.y, z0 = min.z, x1 = max.x, y1 = max.y, z1 = max.z;
			inf();
			ext(tmpVector.set(x0, y0, z0).mul(transform));
			ext(tmpVector.set(x0, y0, z1).mul(transform));
			ext(tmpVector.set(x0, y1, z0).mul(transform));
			ext(tmpVector.set(x0, y1, z1).mul(transform));
			ext(tmpVector.set(x1, y0, z0).mul(transform));
			ext(tmpVector.set(x1, y0, z1).mul(transform));
			ext(tmpVector.set(x1, y1, z0).mul(transform));
			ext(tmpVector.set(x1, y1, z1).mul(transform));
			return this;
		}

		/** Returns whether the given bounding box is contained in this bounding box.
		 * @param b The bounding box
		 * @return Whether the given bounding box is contained */
		public bool contains(FPBoundingBox b)
		{
			return !isValid() || (min.x <= b.min.x && min.y <= b.min.y && min.z <= b.min.z && max.x >= b.max.x &&
								  max.y >= b.max.y
								  && max.z >= b.max.z);
		}

		/** Returns whether the given oriented bounding box is contained in this oriented bounding box.
		 * @param obb The bounding box
		 * @return Whether the given oriented bounding box is contained */
		public bool contains(FPOrientedBoundingBox obb)
		{
			return contains(obb.getCorner000(ref tmpVector)) && contains(obb.getCorner001(ref tmpVector))
															 && contains(obb.getCorner010(ref tmpVector)) &&
															 contains(obb.getCorner011(ref tmpVector))
															 && contains(obb.getCorner100(ref tmpVector)) &&
															 contains(obb.getCorner101(ref tmpVector))
															 && contains(obb.getCorner110(ref tmpVector)) &&
															 contains(obb.getCorner111(ref tmpVector));
		}

		/** Returns whether the given bounding box is intersecting this bounding box (at least one point in).
		 * @param b The bounding box
		 * @return Whether the given bounding box is intersected */
		public bool intersects(FPBoundingBox b)
		{
			if (!isValid()) return false;

			// test using SAT (separating axis theorem)

			FP lx = FPMath.Abs(cnt.x - b.cnt.x);
			FP sumx = (dim.x / 2.0f) + (b.dim.x / 2.0f);

			FP ly = FPMath.Abs(cnt.y - b.cnt.y);
			FP sumy = (dim.y / 2.0f) + (b.dim.y / 2.0f);

			FP lz = FPMath.Abs(cnt.z - b.cnt.z);
			FP sumz = (dim.z / 2.0f) + (b.dim.z / 2.0f);

			return (lx <= sumx && ly <= sumy && lz <= sumz);
		}

		/** Returns whether the given vector is contained in this bounding box.
		 * @param v The vector
		 * @return Whether the vector is contained or not. */
		public bool contains(FPVector3 v)
		{
			return min.x <= v.x && max.x >= v.x && min.y <= v.y && max.y >= v.y && min.z <= v.z && max.z >= v.z;
		}

		public override string ToString()
		{
			return "[" + min + "|" + max + "]";
		}

		/** Extends the bounding box by the given vector.
		 *
		 * @param x The x-coordinate
		 * @param y The y-coordinate
		 * @param z The z-coordinate
		 * @return This bounding box for chaining. */
		public FPBoundingBox ext(FP x, FP y, FP z)
		{
			return set(min.set(FPMath.Min(min.x, x), FPMath.Min(min.y, y), FPMath.Min(min.z, z)),
				max.set(FPMath.Max(max.x, x), FPMath.Max(max.y, y), FPMath.Max(max.z, z)));
		}
	}
}
