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


public partial struct DGSegment
{
	public DGVector3 a;

	/** the ending position **/
	public DGVector3 b;

	/** Constructs a new Segment from the two points given.
	 * 
	 * @param a the first point
	 * @param b the second point */
	public DGSegment(DGVector3 a, DGVector3 b)
	{
		this.a = a;
		this.b = b;
	}

	/** Constructs a new Segment from the two points given.
	 * @param aX the x-coordinate of the first point
	 * @param aY the y-coordinate of the first point
	 * @param aZ the z-coordinate of the first point
	 * @param bX the x-coordinate of the second point
	 * @param bY the y-coordinate of the second point
	 * @param bZ the z-coordinate of the second point */
	public DGSegment(DGFixedPoint aX, DGFixedPoint aY, DGFixedPoint aZ, DGFixedPoint bX, DGFixedPoint bY, DGFixedPoint bZ)
	{
		this.a = new DGVector3(aX, aY, aZ);
		this.b = new DGVector3(bX, bY, bZ);
	}

	public DGFixedPoint len()
	{
		return a.dst(b);
	}

	public DGFixedPoint len2()
	{
		return a.dst2(b);
	}

	public override int GetHashCode()
	{
		int prime = 71;
		int result = 1;
		result = prime * result + this.a.GetHashCode();
		result = prime * result + this.b.GetHashCode();
		return result;
	}

	public override bool Equals(object o)
	{
		var other = (DGSegment) o;
		return this.a == other.a && this.b == other.b;
	}

	public override string ToString()
	{
		return "(" + a + ", " + b + ")";
	}
}