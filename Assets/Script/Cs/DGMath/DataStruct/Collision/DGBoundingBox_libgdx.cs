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

public partial class DGBoundingBox
{
	private static DGVector3 tmpVector = new DGVector3();

	/** Minimum vector. All XYZ components should be inferior to corresponding {@link #max} components. Call {@link #update()} if
	 * you manually change this vector. */
	public DGVector3 min;

	/** Maximum vector. All XYZ components should be superior to corresponding {@link #min} components. Call {@link #update()} if
	 * you manually change this vector. */
	public DGVector3 max;

	private DGVector3 cnt;
	private DGVector3 dim;

	/** @param out The {@link Vector3} to receive the center of the bounding box.
	 * @return The vector specified with the out argument. */
	public DGVector3 getCenter(ref DGVector3 outCenter)
	{
		var result = getCenter();
		outCenter = result;
		return result;
	}
	public DGVector3 getCenter()
	{
		return cnt.cpy();
	}

	public DGFixedPoint getCenterX()
	{
		return cnt.x;
	}

	public DGFixedPoint getCenterY()
	{
		return cnt.y;
	}

	public DGFixedPoint getCenterZ()
	{
		return cnt.z;
	}

	public DGVector3 getCorner000(ref DGVector3 outCorner)
	{
		if(outCorner == DGVector3.Null)
			return new DGVector3(min.x, min.y, min.z);
		return outCorner.set(min.x, min.y, min.z);
	}

	public DGVector3 getCorner000()
	{
		return getCorner000(ref DGVector3.Null);
	}

	public DGVector3 getCorner001(ref DGVector3 outCorner)
	{
		if (outCorner == DGVector3.Null)
			return new DGVector3(min.x, min.y, max.z);
		return outCorner.set(min.x, min.y, max.z);
	}

	public DGVector3 getCorner001()
	{
		return getCorner001(ref DGVector3.Null);
	}

	public DGVector3 getCorner010(ref DGVector3 outCorner)
	{
		if (outCorner == DGVector3.Null)
			return new DGVector3(min.x, max.y, min.z);
		return outCorner.set(min.x, max.y, min.z);
	}

	public DGVector3 getCorner010()
	{
		return getCorner010(ref DGVector3.Null);
	}

	public DGVector3 getCorner011(ref DGVector3 outCorner)
	{
		if (outCorner == DGVector3.Null)
			return new DGVector3(min.x, max.y, max.z);
		return outCorner.set(min.x, max.y, max.z);
	}

	public DGVector3 getCorner011()
	{
		return getCorner011(ref DGVector3.Null);
	}

	public DGVector3 getCorner100(ref DGVector3 outCorner)
	{
		if (outCorner == DGVector3.Null)
			return new DGVector3(max.x, min.y, min.z);
		return outCorner.set(max.x, min.y, min.z);
	}

	public DGVector3 getCorner100()
	{
		return getCorner100(ref DGVector3.Null);
	}

	public DGVector3 getCorner101(ref DGVector3 outCorner)
	{
		if (outCorner == DGVector3.Null)
			return new DGVector3(max.x, min.y, max.z);
		return outCorner.set(max.x, min.y, max.z);
	}

	public DGVector3 getCorner101()
	{
		return getCorner101(ref DGVector3.Null);
	}

	public DGVector3 getCorner110(ref DGVector3 outCorner)
	{
		if (outCorner == DGVector3.Null)
			return new DGVector3(max.x, max.y, min.z);
		return outCorner.set(max.x, max.y, min.z);
	}

	public DGVector3 getCorner110()
	{
		return getCorner110(ref DGVector3.Null);
	}

	public DGVector3 getCorner111(ref DGVector3 outCorner)
	{
		if (outCorner == DGVector3.Null)
			return new DGVector3(max.x, max.y, max.z);
		return outCorner.set(max.x, max.y, max.z);
	}

	public DGVector3 getCorner111()
	{
		return getCorner111(ref DGVector3.Null);
	}

	/** @param out The {@link Vector3} to receive the dimensions of this bounding box on all three axis.
	 * @return The vector specified with the out argument */
	public DGVector3 getDimensions(ref DGVector3 outCorner)
	{
		if (outCorner == DGVector3.Null)
			return dim.cpy();
		return outCorner.set(dim);
	}

	public DGVector3 getDimensions()
	{
		return getDimensions(ref DGVector3.Null);
	}

	public DGFixedPoint getWidth()
	{
		return dim.x;
	}

	public DGFixedPoint getHeight()
	{
		return dim.y;
	}

	public DGFixedPoint getDepth()
	{
		return dim.z;
	}

	/** @param out The {@link Vector3} to receive the minimum values.
	 * @return The vector specified with the out argument */
	public DGVector3 getMin(ref DGVector3 outMin)
	{
		if (outMin == DGVector3.Null)
			return min.cpy();
		return outMin.set(min);
	}

	public DGVector3 getMin()
	{
		return getMin(ref DGVector3.Null);
	}

	/** @param out The {@link Vector3} to receive the maximum values.
	 * @return The vector specified with the out argument */
	public DGVector3 getMax(ref DGVector3 outMax)
	{
		if (outMax == DGVector3.Null)
			return max.cpy();
		return outMax.set(max);
	}
	public DGVector3 getMax()
	{
		return getMax(ref DGVector3.Null);
	}


	/** Constructs a new bounding box with the minimum and maximum vector set to zeros. */
	public DGBoundingBox()
	{
		this.clr();
	}

	/** Constructs a new bounding box from the given bounding box.
	 *
	 * @param bounds The bounding box to copy */
	public DGBoundingBox(DGBoundingBox bounds)
	{
		this.set(bounds);
	}

	/** Constructs the new bounding box using the given minimum and maximum vector.
	 *
	 * @param minimum The minimum vector
	 * @param maximum The maximum vector */
	public DGBoundingBox(DGVector3 minimum, DGVector3 maximum)
	{
		set(minimum, maximum);
	}

	/** Sets the given bounding box.
	 *
	 * @param bounds The bounds.
	 * @return This bounding box for chaining. */
	public DGBoundingBox set(DGBoundingBox bounds)
	{
		return this.set(bounds.min, bounds.max);
	}

	/** Sets the given minimum and maximum vector.
	 *
	 * @param minimum The minimum vector
	 * @param maximum The maximum vector
	 * @return This bounding box for chaining. */
	public DGBoundingBox set(DGVector3 minimum, DGVector3 maximum)
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
		cnt = cnt.set(min).add(max).scl((DGFixedPoint) 0.5f);
		dim = dim.set(max).sub(min);
	}

	/** Sets the bounding box minimum and maximum vector from the given points.
	 *
	 * @param points The points.
	 * @return This bounding box for chaining. */
	public DGBoundingBox set(DGVector3[] points)
	{
		this.inf();
		for (int i = 0; i < points.Length; i++)
		{
			var l_point = points[i];
			this.ext(l_point);
		}

		return this;
	}

	/** Sets the bounding box minimum and maximum vector from the given points.
	 *
	 * @param points The points.
	 * @return This bounding box for chaining. */
	public DGBoundingBox set(List<DGVector3> points)
	{
		this.inf();
		for (int i = 0; i < points.Count; i++)
		{
			var l_point = points[i];
			this.ext(l_point);
		}

		return this;
	}

	/** Sets the minimum and maximum vector to positive and negative infinity.
	 *
	 * @return This bounding box for chaining. */
	public DGBoundingBox inf()
	{
		min.set(DGFixedPoint.MaxValue, DGFixedPoint.MaxValue, DGFixedPoint.MaxValue);
		max.set(DGFixedPoint.MinValue, DGFixedPoint.MinValue, DGFixedPoint.MinValue);
		cnt.set((DGFixedPoint) 0, (DGFixedPoint) 0, (DGFixedPoint) 0);
		dim.set((DGFixedPoint) 0, (DGFixedPoint) 0, (DGFixedPoint) 0);
		return this;
	}

	/** Extends the bounding box to incorporate the given {@link Vector3}.
	 * @param point The vector
	 * @return This bounding box for chaining. */
	public DGBoundingBox ext(DGVector3 point)
	{
		return this.set(min.set(DGMath.Min(min.x, point.x), DGMath.Min(min.y, point.y), DGMath.Min(min.z, point.z)),
			max.set(DGMath.Max(max.x, point.x), DGMath.Max(max.y, point.y), DGMath.Max(max.z, point.z)));
	}

	/** Sets the minimum and maximum vector to zeros.
	 * @return This bounding box for chaining. */
	public DGBoundingBox clr()
	{
		return this.set(min.set((DGFixedPoint) 0, (DGFixedPoint) 0, (DGFixedPoint) 0),
			max.set((DGFixedPoint) 0, (DGFixedPoint) 0, (DGFixedPoint) 0));
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
	public DGBoundingBox ext(DGBoundingBox a_bounds)
	{
		return this.set(min.set(DGMath.Min(min.x, a_bounds.min.x), DGMath.Min(min.y, a_bounds.min.y), DGMath.Min(min.z, a_bounds.min.z)),
			max.set(DGMath.Max(max.x, a_bounds.max.x), DGMath.Max(max.y, a_bounds.max.y), DGMath.Max(max.z, a_bounds.max.z)));
	}

	/** Extends this bounding box by the given sphere.
	 *
	 * @param center Sphere center
	 * @param radius Sphere radius
	 * @return This bounding box for chaining. */
	public DGBoundingBox ext(DGVector3 center, DGFixedPoint radius)
	{
		return this.set(
			min.set(DGMath.Min(min.x, center.x - radius), DGMath.Min(min.y, center.y - radius), DGMath.Min(min.z, center.z - radius)),
			max.set(DGMath.Max(max.x, center.x + radius), DGMath.Max(max.y, center.y + radius), DGMath.Max(max.z, center.z + radius)));
	}

	/** Extends this bounding box by the given transformed bounding box.
	 *
	 * @param bounds The bounding box
	 * @param transform The transformation matrix to apply to bounds, before using it to extend this bounding box.
	 * @return This bounding box for chaining. */
	public DGBoundingBox ext(DGBoundingBox bounds, DGMatrix4x4 transform)
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
	public DGBoundingBox mul(DGMatrix4x4 transform)
	{
		DGFixedPoint x0 = min.x, y0 = min.y, z0 = min.z, x1 = max.x, y1 = max.y, z1 = max.z;
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
	public bool contains(DGBoundingBox b)
	{
		return !isValid() || (min.x <= b.min.x && min.y <= b.min.y && min.z <= b.min.z && max.x >= b.max.x &&
		                      max.y >= b.max.y
		                      && max.z >= b.max.z);
	}

	/** Returns whether the given oriented bounding box is contained in this oriented bounding box.
	 * @param obb The bounding box
	 * @return Whether the given oriented bounding box is contained */
	public bool contains(DGOrientedBoundingBox obb)
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
	public bool intersects(DGBoundingBox b)
	{
		if (!isValid()) return false;

		// test using SAT (separating axis theorem)

		DGFixedPoint lx = DGMath.Abs(this.cnt.x - b.cnt.x);
		DGFixedPoint sumx = (this.dim.x / (DGFixedPoint) 2.0f) + (b.dim.x / (DGFixedPoint) 2.0f);

		DGFixedPoint ly = DGMath.Abs(this.cnt.y - b.cnt.y);
		DGFixedPoint sumy = (this.dim.y / (DGFixedPoint) 2.0f) + (b.dim.y / (DGFixedPoint) 2.0f);

		DGFixedPoint lz = DGMath.Abs(this.cnt.z - b.cnt.z);
		DGFixedPoint sumz = (this.dim.z / (DGFixedPoint) 2.0f) + (b.dim.z / (DGFixedPoint) 2.0f);

		return (lx <= sumx && ly <= sumy && lz <= sumz);
	}

	/** Returns whether the given vector is contained in this bounding box.
	 * @param v The vector
	 * @return Whether the vector is contained or not. */
	public bool contains(DGVector3 v)
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
	public DGBoundingBox ext(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		return this.set(min.set(DGMath.Min(min.x, x), DGMath.Min(min.y, y), DGMath.Min(min.z, z)),
			max.set(DGMath.Max(max.x, x), DGMath.Max(max.y, y), DGMath.Max(max.z, z)));
	}
}