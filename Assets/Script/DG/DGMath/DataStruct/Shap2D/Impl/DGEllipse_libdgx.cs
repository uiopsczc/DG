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

namespace DG
{
	public struct DGEllipse : IDGShape2D
	{
		public DGFixedPoint x, y;
		public DGFixedPoint width, height;


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
		public DGEllipse(DGFixedPoint x, DGFixedPoint y, DGFixedPoint width, DGFixedPoint height)
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
		public DGEllipse(DGVector2 position, DGFixedPoint width, DGFixedPoint height)
		{
			this.x = position.x;
			this.y = position.y;
			this.width = width;
			this.height = height;
		}

		public DGEllipse(DGVector2 position, DGVector2 size)
		{
			this.x = position.x;
			this.y = position.y;
			this.width = size.x;
			this.height = size.y;
		}

		/** Constructs a new {@link Ellipse} from the position and radius of a {@link Circle} (since circles are special cases of
		 * ellipses).
		 * @param circle The circle to take the values of */
		public DGEllipse(DGCircle circle)
		{
			this.x = circle.x;
			this.y = circle.y;
			this.width = circle.radius * (DGFixedPoint)2f;
			this.height = circle.radius * (DGFixedPoint)2f;
		}

		/** Checks whether or not this ellipse contains the given point.
		 * 
		 * @param x X coordinate
		 * @param y Y coordinate
		 * 
		 * @return true if this ellipse contains the given point; false otherwise. */
		public bool contains(DGFixedPoint x, DGFixedPoint y)
		{
			x = x - this.x;
			y = y - this.y;

			return (x * x) / (width * (DGFixedPoint)0.5f * width * (DGFixedPoint)0.5f) +
				   (y * y) / (height * (DGFixedPoint)0.5f * height * (DGFixedPoint)0.5f) <= (DGFixedPoint)1.0f;
		}

		/** Checks whether or not this ellipse contains the given point.
		 * 
		 * @param point Position vector
		 * 
		 * @return true if this ellipse contains the given point; false otherwise. */
		public bool contains(DGVector2 point)
		{
			return contains(point.x, point.y);
		}

		/** Sets a new position and size for this ellipse.
		 * 
		 * @param x X coordinate
		 * @param y Y coordinate
		 * @param width the width of the ellipse
		 * @param height the height of the ellipse */
		public void set(DGFixedPoint x, DGFixedPoint y, DGFixedPoint width, DGFixedPoint height)
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

		public void set(DGCircle circle)
		{
			this.x = circle.x;
			this.y = circle.y;
			this.width = circle.radius * (DGFixedPoint)2f;
			this.height = circle.radius * (DGFixedPoint)2f;
		}

		public void set(DGVector2 position, DGVector2 size)
		{
			this.x = position.x;
			this.y = position.y;
			this.width = size.x;
			this.height = size.y;
		}

		/** Sets the x and y-coordinates of ellipse center from a {@link Vector2}.
		 * @param position The position vector
		 * @return this ellipse for chaining */
		public DGEllipse setPosition(DGVector2 position)
		{
			this.x = position.x;
			this.y = position.y;

			return this;
		}

		/** Sets the x and y-coordinates of ellipse center
		 * @param x The x-coordinate
		 * @param y The y-coordinate
		 * @return this ellipse for chaining */
		public DGEllipse setPosition(DGFixedPoint x, DGFixedPoint y)
		{
			this.x = x;
			this.y = y;

			return this;
		}

		/** Sets the width and height of this ellipse
		 * @param width The width
		 * @param height The height
		 * @return this ellipse for chaining */
		public DGEllipse setSize(DGFixedPoint width, DGFixedPoint height)
		{
			this.width = width;
			this.height = height;

			return this;
		}

		/** @return The area of this {@link Ellipse} as {@link MathUtils#PI} * {@link Ellipse#width} * {@link Ellipse#height} */
		public DGFixedPoint area()
		{
			return DGMath.PI * (this.width * this.height) / (DGFixedPoint)4;
		}

		/** Approximates the circumference of this {@link Ellipse}. Oddly enough, the circumference of an ellipse is actually difficult
		 * to compute exactly.
		 * @return The Ramanujan approximation to the circumference of an ellipse if one dimension is at least three times longer than
		 *         the other, else the simpler approximation */
		public DGFixedPoint circumference()
		{
			DGFixedPoint a = this.width / (DGFixedPoint)2;
			DGFixedPoint b = this.height / (DGFixedPoint)2;
			if (a * (DGFixedPoint)3 > b || b * (DGFixedPoint)3 > a)
			{
				// If one dimension is three times as long as the other...
				return DGMath.PI * (((DGFixedPoint)3 * (a + b)) - DGMath.Sqrt(((DGFixedPoint)3 * a + b) * (a + (DGFixedPoint)3 * b)));
			}

			// We can use the simpler approximation, then
			return DGMath.TwoPI * DGMath.Sqrt((a * a + b * b) / (DGFixedPoint)2);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			var other = (DGEllipse)obj;
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
}
