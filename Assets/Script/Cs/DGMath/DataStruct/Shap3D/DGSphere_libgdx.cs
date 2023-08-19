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


using System;

public partial struct DGSphere
{
	public DGFixedPoint radius;
	/** the center of the sphere **/
	public DGVector3 center;


	/** Constructs a sphere with the given center and radius
	 * @param center The center
	 * @param radius The radius */
	public DGSphere(DGVector3 center, DGFixedPoint radius)
	{
		this.center = center;
		this.radius = radius;
	}

	/** @param sphere the other sphere
	 * @return whether this and the other sphere overlap */
	public bool overlaps(DGSphere sphere)
	{
		return center.dst2(sphere.center) < (radius + sphere.radius) * (radius + sphere.radius);
	}
	public DGFixedPoint volume()
	{
		return DGFixedPoint.FourPiDiv3 * this.radius * this.radius * this.radius;
	}

	public DGFixedPoint surfaceArea()
	{
		return (DGFixedPoint)4 * DGFixedPoint.Pi * this.radius * this.radius;
	}

	public override int GetHashCode()
	{
		int prime = 71;
		int result = 1;
		result = prime * result + this.center.GetHashCode();
		result = prime * result + this.radius.GetHashCode();
		return result;
	}

	public override bool Equals(object o)
	{
		var other = (DGSphere)o;
		return this.center == other.center && this.radius == other.radius;
	}

	public override string ToString()
	{
		return string.Format("(center = {0}, radius = {1})", this.center, this.radius);
	}
}