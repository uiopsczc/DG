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
	public struct FPRectangle : IFPShape2D
	{
		//不能修改Null的值
		public static FPRectangle Null = Max.cpy();
		public static FPRectangle Max = new(FP.MAX_VALUE, FP.MAX_VALUE, FP.MAX_VALUE, FP.MAX_VALUE);
		/** Static temporary rectangle. Use with care! Use only when sure other code will not also use this. */
		public static FPRectangle tmp = default;

		/** Static temporary rectangle. Use with care! Use only when sure other code will not also use this. */
		public static FPRectangle tmp2 = default;

		public FP x, y;
		public FP width, height;


		/** Constructs a new rectangle with the given corner point in the bottom left and dimensions.
		 * @param x The corner point x-coordinate
		 * @param y The corner point y-coordinate
		 * @param width The width
		 * @param height The height */
		public FPRectangle(FP x, FP y, FP width, FP height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		/** Constructs a rectangle based on the given rectangle
		 * @param rect The rectangle */
		public FPRectangle(FPRectangle rect)
		{
			x = rect.x;
			y = rect.y;
			width = rect.width;
			height = rect.height;
		}

		/** @param x bottom-left x coordinate
		 * @param y bottom-left y coordinate
		 * @param width width
		 * @param height height
		 * @return this rectangle for chaining */
		public FPRectangle set(FP x, FP y, FP width, FP height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;

			return this;
		}

		/** @return the x-coordinate of the bottom left corner */
		public FP getX()
		{
			return x;
		}

		/** Sets the x-coordinate of the bottom left corner
		 * @param x The x-coordinate
		 * @return this rectangle for chaining */
		public FPRectangle setX(FP x)
		{
			this.x = x;

			return this;
		}

		/** @return the y-coordinate of the bottom left corner */
		public FP getY()
		{
			return y;
		}

		/** Sets the y-coordinate of the bottom left corner
		 * @param y The y-coordinate
		 * @return this rectangle for chaining */
		public FPRectangle setY(FP y)
		{
			this.y = y;

			return this;
		}

		/** @return the width */
		public FP getWidth()
		{
			return width;
		}

		/** Sets the width of this rectangle
		 * @param width The width
		 * @return this rectangle for chaining */
		public FPRectangle setWidth(FP width)
		{
			this.width = width;

			return this;
		}

		/** @return the height */
		public FP getHeight()
		{
			return height;
		}

		/** Sets the height of this rectangle
		 * @param height The height
		 * @return this rectangle for chaining */
		public FPRectangle setHeight(FP height)
		{
			this.height = height;

			return this;
		}

		/** return the Vector2 with coordinates of this rectangle
		 * @param position The Vector2 */
		public FPVector2 getPosition(FPVector2 position)
		{
			return position.set(x, y);
		}

		/** Sets the x and y-coordinates of the bottom left corner from vector
		 * @param position The position vector
		 * @return this rectangle for chaining */
		public FPRectangle setPosition(FPVector2 position)
		{
			x = position.x;
			y = position.y;

			return this;
		}

		/** Sets the x and y-coordinates of the bottom left corner
		 * @param x The x-coordinate
		 * @param y The y-coordinate
		 * @return this rectangle for chaining */
		public FPRectangle setPosition(FP x, FP y)
		{
			this.x = x;
			this.y = y;

			return this;
		}

		/** Sets the width and height of this rectangle
		 * @param width The width
		 * @param height The height
		 * @return this rectangle for chaining */
		public FPRectangle setSize(FP width, FP height)
		{
			this.width = width;
			this.height = height;

			return this;
		}

		/** Sets the squared size of this rectangle
		 * @param sizeXY The size
		 * @return this rectangle for chaining */
		public FPRectangle setSize(FP sizeXY)
		{
			width = sizeXY;
			height = sizeXY;

			return this;
		}

		/** @return the Vector2 with size of this rectangle
		 * @param size The Vector2 */
		public FPVector2 getSize(FPVector2 size)
		{
			return size.set(width, height);
		}

		/** @param x point x coordinate
		 * @param y point y coordinate
		 * @return whether the point is contained in the rectangle */
		public bool contains(FP x, FP y)
		{
			return this.x <= x && this.x + width >= x && this.y <= y && this.y + height >= y;
		}

		/** @param point The coordinates vector
		 * @return whether the point is contained in the rectangle */
		public bool contains(FPVector2 point)
		{
			return contains(point.x, point.y);
		}

		/** @param circle the circle
		 * @return whether the circle is contained in the rectangle */
		public bool contains(FPCircle circle)
		{
			return (circle.x - circle.radius >= x) && (circle.x + circle.radius <= x + width) &&
				   (circle.y - circle.radius >= y)
				   && (circle.y + circle.radius <= y + height);
		}

		/** @param rectangle the other {@link Rectangle}.
		 * @return whether the other rectangle is contained in this rectangle. */
		public bool contains(FPRectangle rectangle)
		{
			FP xmin = rectangle.x;
			FP xmax = xmin + rectangle.width;

			FP ymin = rectangle.y;
			FP ymax = ymin + rectangle.height;

			return ((xmin > x && xmin < x + width) && (xmax > x && xmax < x + width))
				   && ((ymin > y && ymin < y + height) && (ymax > y && ymax < y + height));
		}

		/** @param r the other {@link Rectangle}
		 * @return whether this rectangle overlaps the other rectangle. */
		public bool overlaps(FPRectangle r)
		{
			return x < r.x + r.width && x + width > r.x && y < r.y + r.height && y + height > r.y;
		}

		/** Sets the values of the given rectangle to this rectangle.
		 * @param rect the other rectangle
		 * @return this rectangle for chaining */
		public FPRectangle set(FPRectangle rect)
		{
			x = rect.x;
			y = rect.y;
			width = rect.width;
			height = rect.height;

			return this;
		}

		/** Merges this rectangle with the other rectangle. The rectangle should not have negative width or negative height.
		 * @param rect the other rectangle
		 * @return this rectangle for chaining */
		public FPRectangle merge(FPRectangle rect)
		{
			FP minX = FPMath.Min(x, rect.x);
			FP maxX = FPMath.Max(x + width, rect.x + rect.width);
			x = minX;
			width = maxX - minX;

			FP minY = FPMath.Min(y, rect.y);
			FP maxY = FPMath.Max(y + height, rect.y + rect.height);
			y = minY;
			height = maxY - minY;

			return this;
		}

		/** Merges this rectangle with a point. The rectangle should not have negative width or negative height.
		 * @param x the x coordinate of the point
		 * @param y the y coordinate of the point
		 * @return this rectangle for chaining */
		public FPRectangle merge(FP x, FP y)
		{
			FP minX = FPMath.Min(this.x, x);
			FP maxX = FPMath.Max(this.x + width, x);
			this.x = minX;
			width = maxX - minX;

			FP minY = FPMath.Min(this.y, y);
			FP maxY = FPMath.Max(this.y + height, y);
			this.y = minY;
			height = maxY - minY;

			return this;
		}

		/** Merges this rectangle with a point. The rectangle should not have negative width or negative height.
		 * @param vec the vector describing the point
		 * @return this rectangle for chaining */
		public FPRectangle merge(FPVector2 vec)
		{
			return merge(vec.x, vec.y);
		}

		/** Merges this rectangle with a list of points. The rectangle should not have negative width or negative height.
		 * @param vecs the vectors describing the points
		 * @return this rectangle for chaining */
		public FPRectangle merge(FPVector2[] vecs)
		{
			FP minX = x;
			FP maxX = x + width;
			FP minY = y;
			FP maxY = y + height;
			for (int i = 0; i < vecs.Length; ++i)
			{
				FPVector2 v = vecs[i];
				minX = FPMath.Min(minX, v.x);
				maxX = FPMath.Max(maxX, v.x);
				minY = FPMath.Min(minY, v.y);
				maxY = FPMath.Max(maxY, v.y);
			}

			x = minX;
			width = maxX - minX;
			y = minY;
			height = maxY - minY;
			return this;
		}

		/** Calculates the aspect ratio ( width / height ) of this rectangle
		 * @return the aspect ratio of this rectangle. Returns Float.NaN if height is 0 to avoid ArithmeticException */
		public FP getAspectRatio()
		{
			return width / height;
		}

		/** Calculates the center of the rectangle. Results are located in the given Vector2
		 * @param vector the Vector2 to use
		 * @return the given vector with results stored inside */
		public FPVector2 getCenter(ref FPVector2 vector)
		{
			vector.x = x + width / 2;
			vector.y = y + height / 2;
			return vector;
		}

		/** Moves this rectangle so that its center point is located at a given position
		 * @param x the position's x
		 * @param y the position's y
		 * @return this for chaining */
		public FPRectangle setCenter(FP x, FP y)
		{
			setPosition(x - width / 2, y - height / 2);
			return this;
		}

		/** Moves this rectangle so that its center point is located at a given position
		 * @param position the position
		 * @return this for chaining */
		public FPRectangle setCenter(FPVector2 position)
		{
			setPosition(position.x - width / 2, position.y - height / 2);
			return this;
		}

		/** Fits this rectangle around another rectangle while maintaining aspect ratio. This scales and centers the rectangle to the
		 * other rectangle (e.g. Having a camera translate and scale to show a given area)
		 * @param rect the other rectangle to fit this rectangle around
		 * @return this rectangle for chaining
		 * @see Scaling */
		public FPRectangle fitOutside(FPRectangle rect)
		{
			FP ratio = getAspectRatio();

			if (ratio > rect.getAspectRatio())
			{
				// Wider than tall
				setSize(rect.height * ratio, rect.height);
			}
			else
			{
				// Taller than wide
				setSize(rect.width, rect.width / ratio);
			}

			setPosition((rect.x + rect.width / 2) - width / 2, (rect.y + rect.height / 2) - height / 2);
			return this;
		}

		/** Fits this rectangle into another rectangle while maintaining aspect ratio. This scales and centers the rectangle to the
		 * other rectangle (e.g. Scaling a texture within a arbitrary cell without squeezing)
		 * @param rect the other rectangle to fit this rectangle inside
		 * @return this rectangle for chaining
		 * @see Scaling */
		public FPRectangle fitInside(FPRectangle rect)
		{
			FP ratio = getAspectRatio();

			if (ratio < rect.getAspectRatio())
			{
				// Taller than wide
				setSize(rect.height * ratio, rect.height);
			}
			else
			{
				// Wider than tall
				setSize(rect.width, rect.width / ratio);
			}

			setPosition((rect.x + rect.width / 2) - width / 2, (rect.y + rect.height / 2) - height / 2);
			return this;
		}

		/** Converts this {@code Rectangle} to a string in the format {@code [x,y,width,height]}.
		 * @return a string representation of this object. */
		public override string ToString()
		{
			return "[" + x + "," + y + "," + width + "," + height + "]";
		}


		public FP area()
		{
			return width * height;
		}

		public FP perimeter()
		{
			return 2 * (width + height);
		}

		public FPRectangle cpy()
		{
			return new FPRectangle(x, y, width, height);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			var other = (FPRectangle)obj;
			return Equals(other);
		}

		public bool Equals(FPRectangle other)
		{
			return other.x == x && other.y == y && other.width == width && other.height == height;
		}

		public override int GetHashCode()
		{
			int prime = 31;
			int result = 1;
			result = prime * result + x.GetHashCode();
			result = prime * result + y.GetHashCode();
			result = prime * result + width.GetHashCode();
			result = prime * result + height.GetHashCode();
			return result;
		}
	}
}
