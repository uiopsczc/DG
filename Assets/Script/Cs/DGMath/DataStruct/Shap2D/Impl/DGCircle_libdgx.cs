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


public struct DGCircle : IDGShape2D
{
	public DGFixedPoint x, y;
	public DGFixedPoint radius;


	/** Constructs a new circle with the given X and Y coordinates and the given radius.
	 * 
	 * @param x X coordinate
	 * @param y Y coordinate
	 * @param radius The radius of the circle */
	public DGCircle(DGFixedPoint x, DGFixedPoint y, DGFixedPoint radius)
	{
		this.x = x;
		this.y = y;
		this.radius = radius;
	}

	/** Constructs a new circle using a given {@link Vector2} that contains the desired X and Y coordinates, and a given radius.
	 * 
	 * @param position The position {@link Vector2}.
	 * @param radius The radius */
	public DGCircle(DGVector2 position, DGFixedPoint radius)
	{
		this.x = position.x;
		this.y = position.y;
		this.radius = radius;
	}

	/** Copy constructor
	 * 
	 * @param circle The circle to construct a copy of. */
	public DGCircle(DGCircle circle)
	{
		this.x = circle.x;
		this.y = circle.y;
		this.radius = circle.radius;
	}

	/** Creates a new {@link Circle} in terms of its center and a point on its edge.
	 * 
	 * @param center The center of the new circle
	 * @param edge Any point on the edge of the given circle */
	public DGCircle(DGVector2 center, DGVector2 edge)
	{
		this.x = center.x;
		this.y = center.y;
		this.radius = DGVector2.len(center.x - edge.x, center.y - edge.y);
	}

	/** Sets a new location and radius for this circle.
	 * 
	 * @param x X coordinate
	 * @param y Y coordinate
	 * @param radius Circle radius */
	public void set(DGFixedPoint x, DGFixedPoint y, DGFixedPoint radius)
	{
		this.x = x;
		this.y = y;
		this.radius = radius;
	}

	/** Sets a new location and radius for this circle.
	 * 
	 * @param position Position {@link Vector2} for this circle.
	 * @param radius Circle radius */
	public void set(DGVector2 position, DGFixedPoint radius)
	{
		this.x = position.x;
		this.y = position.y;
		this.radius = radius;
	}

	/** Sets a new location and radius for this circle, based upon another circle.
	 * 
	 * @param circle The circle to copy the position and radius of. */
	public void set(DGCircle circle)
	{
		this.x = circle.x;
		this.y = circle.y;
		this.radius = circle.radius;
	}

	/** Sets this {@link Circle}'s values in terms of its center and a point on its edge.
	 * 
	 * @param center The new center of the circle
	 * @param edge Any point on the edge of the given circle */
	public void set(DGVector2 center, DGVector2 edge)
	{
		this.x = center.x;
		this.y = center.y;
		this.radius = DGVector2.len(center.x - edge.x, center.y - edge.y);
	}

	/** Sets the x and y-coordinates of circle center from vector
	 * @param position The position vector */
	public void setPosition(DGVector2 position)
	{
		this.x = position.x;
		this.y = position.y;
	}

	/** Sets the x and y-coordinates of circle center
	 * @param x The x-coordinate
	 * @param y The y-coordinate */
	public void setPosition(DGFixedPoint x, DGFixedPoint y)
	{
		this.x = x;
		this.y = y;
	}

	/** Sets the x-coordinate of circle center
	 * @param x The x-coordinate */
	public void setX(DGFixedPoint x)
	{
		this.x = x;
	}

	/** Sets the y-coordinate of circle center
	 * @param y The y-coordinate */
	public void setY(DGFixedPoint y)
	{
		this.y = y;
	}

	/** Sets the radius of circle
	 * @param radius The radius */
	public void setRadius(DGFixedPoint radius)
	{
		this.radius = radius;
	}

	/** Checks whether or not this circle contains a given point.
	 * 
	 * @param x X coordinate
	 * @param y Y coordinate
	 * 
	 * @return true if this circle contains the given point. */
	public bool contains(DGFixedPoint x, DGFixedPoint y)
	{
		x = this.x - x;
		y = this.y - y;
		return x * x + y * y <= radius * radius;
	}

	/** Checks whether or not this circle contains a given point.
	 * 
	 * @param point The {@link Vector2} that contains the point coordinates.
	 * 
	 * @return true if this circle contains this point; false otherwise. */
	public bool contains(DGVector2 point)
	{
		DGFixedPoint dx = x - point.x;
		DGFixedPoint dy = y - point.y;
		return dx * dx + dy * dy <= radius * radius;
	}

	/** @param c the other {@link Circle}
	 * @return whether this circle contains the other circle. */
	public bool contains(DGCircle c)
	{
		DGFixedPoint radiusDiff = radius - c.radius;
		if (radiusDiff < (DGFixedPoint) 0f) return false; // Can't contain bigger circle
		DGFixedPoint dx = x - c.x;
		DGFixedPoint dy = y - c.y;
		DGFixedPoint dst = dx * dx + dy * dy;
		DGFixedPoint radiusSum = radius + c.radius;
		return (!(radiusDiff * radiusDiff < dst) && (dst < radiusSum * radiusSum));
	}

	/** @param c the other {@link Circle}
	 * @return whether this circle overlaps the other circle. */
	public bool overlaps(DGCircle c)
	{
		DGFixedPoint dx = x - c.x;
		DGFixedPoint dy = y - c.y;
		DGFixedPoint distance = dx * dx + dy * dy;
		DGFixedPoint radiusSum = radius + c.radius;
		return distance < radiusSum * radiusSum;
	}

	/** Returns a {@link String} representation of this {@link Circle} of the form {@code x,y,radius}. */
	public override string ToString()
	{
		return x + "," + y + "," + radius;
	}

	/** @return The circumference of this circle (as 2 * {@link MathUtils#PI2}) * {@code radius} */
	public DGFixedPoint circumference()
	{
		return this.radius * DGMath.TwoPI;
	}

	/** @return The area of this circle (as {@link MathUtils#PI} * radius * radius). */
	public DGFixedPoint area()
	{
		return this.radius * this.radius * DGMath.PI;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
			return false;
		var other = (DGCircle) obj;
		return Equals(other);
	}

	public bool Equals(DGCircle other)
	{
		return other.x == x && other.y == y && other.radius == radius;
	}

	public override int GetHashCode()
	{
		int prime = 41;
		int result = 1;
		result = prime * result + this.x.GetHashCode();
		result = prime * result + this.y.GetHashCode();
		result = prime * result + this.radius.GetHashCode();
		return result;
	}
}