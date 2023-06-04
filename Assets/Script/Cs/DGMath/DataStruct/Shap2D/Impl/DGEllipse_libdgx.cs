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
using UnityEditor;
using FP = DGFixedPoint;
using FPVector2 = DGVector2;
using FPCircle = DGCircle;

public struct DGEllipse : IDGShape2D
{
	public FP x, y;
	public FP width, height;


	/** Copy constructor
	 * 
	 * @param ellipse Ellipse to construct a copy of. */
	public DGEllipse(DGEllipse ellipse)
	{
		this.x = ellipse.x;
		this.y = ellipse.y;
		this.width = ellipse.width;
		this.height = ellipse.height;
	}

	/** Constructs a new ellipse
	 * 
	 * @param x X coordinate
	 * @param y Y coordinate
	 * @param width the width of the ellipse
	 * @param height the height of the ellipse */
	public DGEllipse(FP x, FP y, FP width, FP height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	/** Costructs a new ellipse
	 * 
	 * @param position Position vector
	 * @param width the width of the ellipse
	 * @param height the height of the ellipse */
	public DGEllipse(FPVector2 position, FP width, FP height)
	{
		this.x = position.x;
		this.y = position.y;
		this.width = width;
		this.height = height;
	}

	public DGEllipse(FPVector2 position, FPVector2 size)
	{
		this.x = position.x;
		this.y = position.y;
		this.width = size.x;
		this.height = size.y;
	}

	/** Constructs a new {@link Ellipse} from the position and radius of a {@link Circle} (since circles are special cases of
	 * ellipses).
	 * @param circle The circle to take the values of */
	public DGEllipse(FPCircle circle)
	{
		this.x = circle.x;
		this.y = circle.y;
		this.width = circle.radius * (FP) 2f;
		this.height = circle.radius * (FP) 2f;
	}

	/** Checks whether or not this ellipse contains the given point.
	 * 
	 * @param x X coordinate
	 * @param y Y coordinate
	 * 
	 * @return true if this ellipse contains the given point; false otherwise. */
	public bool contains(FP x, FP y)
	{
		x = x - this.x;
		y = y - this.y;

		return (x * x) / (width * (FP) 0.5f * width * (FP) 0.5f) +
		       (y * y) / (height * (FP) 0.5f * height * (FP) 0.5f) <= (FP) 1.0f;
	}

	/** Checks whether or not this ellipse contains the given point.
	 * 
	 * @param point Position vector
	 * 
	 * @return true if this ellipse contains the given point; false otherwise. */
	public bool contains(FPVector2 point)
	{
		return contains(point.x, point.y);
	}

	/** Sets a new position and size for this ellipse.
	 * 
	 * @param x X coordinate
	 * @param y Y coordinate
	 * @param width the width of the ellipse
	 * @param height the height of the ellipse */
	public void set(FP x, FP y, FP width, FP height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	/** Sets a new position and size for this ellipse based upon another ellipse.
	 * 
	 * @param ellipse The ellipse to copy the position and size of. */
	public void set(DGEllipse ellipse)
	{
		x = ellipse.x;
		y = ellipse.y;
		width = ellipse.width;
		height = ellipse.height;
	}

	public void set(FPCircle circle)
	{
		this.x = circle.x;
		this.y = circle.y;
		this.width = circle.radius * (FP) 2f;
		this.height = circle.radius * (FP) 2f;
	}

	public void set(FPVector2 position, FPVector2 size)
	{
		this.x = position.x;
		this.y = position.y;
		this.width = size.x;
		this.height = size.y;
	}

	/** Sets the x and y-coordinates of ellipse center from a {@link Vector2}.
	 * @param position The position vector
	 * @return this ellipse for chaining */
	public DGEllipse setPosition(FPVector2 position)
	{
		this.x = position.x;
		this.y = position.y;

		return this;
	}

	/** Sets the x and y-coordinates of ellipse center
	 * @param x The x-coordinate
	 * @param y The y-coordinate
	 * @return this ellipse for chaining */
	public DGEllipse setPosition(FP x, FP y)
	{
		this.x = x;
		this.y = y;

		return this;
	}

	/** Sets the width and height of this ellipse
	 * @param width The width
	 * @param height The height
	 * @return this ellipse for chaining */
	public DGEllipse setSize(FP width, FP height)
	{
		this.width = width;
		this.height = height;

		return this;
	}

	/** @return The area of this {@link Ellipse} as {@link MathUtils#PI} * {@link Ellipse#width} * {@link Ellipse#height} */
	public FP area()
	{
		return DGMath.PI * (this.width * this.height) / (FP) 4;
	}

	/** Approximates the circumference of this {@link Ellipse}. Oddly enough, the circumference of an ellipse is actually difficult
	 * to compute exactly.
	 * @return The Ramanujan approximation to the circumference of an ellipse if one dimension is at least three times longer than
	 *         the other, else the simpler approximation */
	public FP circumference()
	{
		FP a = this.width / (FP) 2;
		FP b = this.height / (FP) 2;
		if (a * (FP) 3 > b || b * (FP) 3 > a)
		{
			// If one dimension is three times as long as the other...
			return DGMath.PI * (((FP) 3 * (a + b)) - DGMath.Sqrt(((FP) 3 * a + b) * (a + (FP) 3 * b)));
		}

		// We can use the simpler approximation, then
		return DGMath.TwoPI * DGMath.Sqrt((a * a + b * b) / (FP) 2);
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
			return false;
		var other = (DGEllipse) obj;
		return Equals(other);
	}

	public bool Equals(DGEllipse other)
	{
		return other.x == x && other.y == y && other.width == width && other.height == height;
	}

	public override int GetHashCode()
	{
		int prime = 53;
		int result = 1;
		result = prime * result + this.x.GetHashCode();
		result = prime * result + this.y.GetHashCode();
		result = prime * result + this.width.GetHashCode();
		result = prime * result + this.height.GetHashCode();
		return result;
	}
}