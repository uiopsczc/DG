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


using System;
using FP = DGFixedPoint;
using FPVector3 = DGVector3;
using FPRay = DGRay;
#if UNITY_5_3_OR_NEWER
using UnityEngine;

#endif

public struct DGBounds : IEquatable<DGBounds>
{
	private FPVector3 _center;
	private FPVector3 _extents;

	/// <summary>
	///   <para>The center of the bounding box.</para>
	/// </summary>
	public FPVector3 center
	{
		get => this._center;
		set => this._center = value;
	}

	/// <summary>
	///   <para>The total size of the box. This is always twice as large as the extents.</para>
	/// </summary>
	public FPVector3 size
	{
		get => this._extents * (FP) 2f;
		set => this._extents = value * (FP) 0.5f;
	}

	/// <summary>
	///   <para>The extents of the Bounding Box. This is always half of the size of the Bounds.</para>
	/// </summary>
	public FPVector3 extents
	{
		get => this._extents;
		set => this._extents = value;
	}

	/// <summary>
	///   <para>The minimal point of the box. This is always equal to center-extents.</para>
	/// </summary>
	public FPVector3 min
	{
		get => this.center - this.extents;
		set => this.SetMinMax(value, this.max);
	}

	/// <summary>
	///   <para>The maximal point of the box. This is always equal to center+extents.</para>
	/// </summary>
	public FPVector3 max
	{
		get => this.center + this.extents;
		set => this.SetMinMax(this.min, value);
	}

	/// <summary>
	///   <para>Creates a new Bounds.</para>
	/// </summary>
	/// <param name="center">The location of the origin of the Bounds.</param>
	/// <param name="size">The dimensions of the Bounds.</param>
	public DGBounds(FPVector3 center, FPVector3 size)
	{
		this._center = center;
		this._extents = size * (FP) 0.5f;
	}

#if UNITY_5_3_OR_NEWER
	public DGBounds(Bounds bounds)
	{
		this._center = new FPVector3(bounds.center);
		this._extents = new FPVector3(bounds.extents);
	}
#endif

	/*************************************************************************************
	* 模块描述:Equal ToString
	*************************************************************************************/
	public override bool Equals(object other)
	{
		if (!(other is DGBounds))
			return false;
		return this.Equals((DGBounds) other);
	}

	public bool Equals(DGBounds other)
	{
		return this.center.Equals(other.center) && this.extents.Equals(other.extents);
	}

	public override int GetHashCode()
	{
		FPVector3 vector3 = this.center;
		int hashCode = vector3.GetHashCode();
		vector3 = this.extents;
		int num = vector3.GetHashCode() << 2;
		return hashCode ^ num;
	}

	/// <summary>
	///   <para>Returns a nicely formatted string for the bounds.</para>
	/// </summary>
	/// <param name="format"></param>
	public override string ToString()
	{
		return string.Format("center: {0}, extents: {1}", this._center, this._extents);
	}

	/*************************************************************************************
	* 模块描述:关系运算符
	*************************************************************************************/
	public static bool operator ==(DGBounds lhs, DGBounds rhs)
	{
		return lhs.center == rhs.center && lhs.extents == rhs.extents;
	}

	public static bool operator !=(DGBounds lhs, DGBounds rhs)
	{
		return !(lhs == rhs);
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	public static bool IntersectRayAABB(FPRay ray, DGBounds bounds, out FP distance)
	{
		return bounds.IntersectRay(ray, out distance);
	}
	/*************************************************************************************
	* 模块描述:MemberUtil
	*************************************************************************************/
	/// <summary>
	///   <para>Sets the bounds to the min and max value of the box.</para>
	/// </summary>
	/// <param name="min"></param>
	/// <param name="max"></param>
	public void SetMinMax(FPVector3 min, FPVector3 max)
	{
		this.extents = (max - min) * (FP) 0.5f;
		this.center = min + this.extents;
	}

	/// <summary>
	///   <para>Grows the Bounds to include the point.</para>
	/// </summary>
	/// <param name="point"></param>
	public void Encapsulate(FPVector3 point)
	{
		this.SetMinMax(FPVector3.Min(this.min, point), FPVector3.Max(this.max, point));
	}

	/// <summary>
	///   <para>Grow the bounds to encapsulate the bounds.</para>
	/// </summary>
	/// <param name="bounds"></param>
	public void Encapsulate(DGBounds bounds)
	{
		this.Encapsulate(bounds.center - bounds.extents);
		this.Encapsulate(bounds.center + bounds.extents);
	}

	/// <summary>
	///   <para>Expand the bounds by increasing its size by amount along each side.</para>
	/// </summary>
	/// <param name="amount"></param>
	public void Expand(FP amount)
	{
		amount *= (FP) 0.5f;
		this.extents += new FPVector3(amount, amount, amount);
	}

	/// <summary>
	///   <para>Expand the bounds by increasing its size by amount along each side.</para>
	/// </summary>
	/// <param name="amount"></param>
	public void Expand(FPVector3 amount)
	{
		this.extents += amount * (FP) 0.5f;
	}

	/// <summary>
	///   <para>Does another bounding box intersect with this bounding box?</para>
	/// </summary>
	/// <param name="bounds"></param>
	public bool Intersects(DGBounds bounds)
	{
		return this.min.x <= bounds.max.x && this.max.x >= bounds.min.x &&
		       (this.min.y <= bounds.max.y && this.max.y >= bounds.min.y) && this.min.z <= bounds.max.z &&
		       this.max.z >= bounds.min.z;
	}

	/// <summary>
	///   <para>Does ray intersect this bounding box?</para>
	/// </summary>
	/// <param name="ray"></param>
	public (bool isIntersect, FP distance) IntersectRay(FPRay ray)
	{
		var tmin = DGMath.MinValue;
		var tmax = DGMath.MaxValue;
		FP t0, t1, f;
		FPVector3 t = this.center - ray.origin;
		var p = new FPVector3(t.x, t.y, t.z);
		t = this.extents;
		var extent = new FPVector3(t.x, t.y, t.z);
		t = ray.direction;
		var dir = new FPVector3(t.x, t.y, t.z);

		for (int i = 0; i < 3; i++)
		{
			f = (FP) 1 / dir[i];
			t0 = (p[i] + extent[i]) * f;
			t1 = (p[i] - extent[i]) * f;
			if (t0 < t1)
			{
				if (t0 > tmin) tmin = t0;
				if (t1 < tmax) tmax = t1;
				if (tmin > tmax) return (false, default);
				if (tmax < (FP) 0) return (false, default);
			}
			else
			{
				if (t1 > tmin) tmin = t1;
				if (t0 < tmax) tmax = t0;
				if (tmin > tmax) return (false, default);
				if (tmax < (FP) 0) return (false, default);
			}
		}

		return (true, tmin);
	}

	public bool IntersectRay(FPRay ray, out FP inDistance)
	{
		var (isIntersect, distance) = IntersectRay(ray);
		inDistance = distance;
		return isIntersect;
	}


	/// <summary>
	///   <para>Is point contained in the bounding box?</para>
	/// </summary>
	/// <param name="point"></param>
	public bool Contains(FPVector3 point)
	{
		var min = this.min;
		var max = this.max;
		if (point.x < min.x || point.y < min.y || point.z < min.z || point.x > max.x || point.y > max.y ||
		    point.z > max.z)
			return false;
		return true;
	}

	/// <summary>
	///   <para>The smallest squared distance between the point and this bounding box.</para>
	/// </summary>
	/// <param name="point"></param>
	public FP SqrDistance(FPVector3 point)
	{
		var (closestPoint, distance) = this.ClosestPoint(point);
		return distance;
	}
	/// <summary>
	///   <para>The closest point on the bounding box.</para>
	/// </summary>
	/// <param name="point">Arbitrary point.</param>
	/// <returns>
	///   <para>The point on the bounding box or inside the bounding box.</para>
	/// </returns>
	public (FPVector3 closestPoint, FP distance) ClosestPoint(FPVector3 point)
	{
		var t = point - this.center;
		var closest = new FPVector3(t.x, t.y, t.z);
		var et = this.extents;
		var extent = new FPVector3(et.x, et.y, et.z);
		var sqrtDistance = (FP) 0;
		FP delta;

		for (int i = 0; i < 3; i++)
		{
			if (closest[i] < -extent[i])
			{
				delta = closest[i] + extent[i];
				sqrtDistance = sqrtDistance + delta * delta;
				closest[i] = -extent[i];
			}
			else if (closest[i] > extent[i])
			{
				delta = closest[i] - extent[i];
				sqrtDistance = sqrtDistance + delta * delta;
				closest[i] = extent[i];
			}
		}

		if (sqrtDistance == (FP) 0)
			return (point, (FP) 0);
		var outPoint = closest + this.center;
		return (outPoint, sqrtDistance);
	}
}