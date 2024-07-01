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

namespace DG
{
	public partial struct DGGridPoint2
	{
		public int x;
		public int y;


		/** Constructs a new 2D grid point.
		 * 
		 * @param x X coordinate
		 * @param y Y coordinate */
		public DGGridPoint2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		/** Copy constructor
		 * 
		 * @param point The 2D grid point to make a copy of. */
		public DGGridPoint2(DGGridPoint2 point)
		{
			this.x = point.x;
			this.y = point.y;
		}

		/** Sets the coordinates of this 2D grid point to that of another.
		 * 
		 * @param point The 2D grid point to copy the coordinates of.
		 * 
		 * @return this 2D grid point for chaining. */
		public DGGridPoint2 set(DGGridPoint2 point)
		{
			this.x = point.x;
			this.y = point.y;
			return this;
		}

		/** Sets the coordinates of this 2D grid point.
		 * 
		 * @param x X coordinate
		 * @param y Y coordinate
		 * 
		 * @return this 2D grid point for chaining. */
		public DGGridPoint2 set(int x, int y)
		{
			this.x = x;
			this.y = y;
			return this;
		}

		/** @param other The other point
		 * @return the squared distance between this point and the other point. */
		public DGFixedPoint dst2(DGGridPoint2 other)
		{
			int xd = other.x - x;
			int yd = other.y - y;

			return (DGFixedPoint)(xd * xd + yd * yd);
		}

		/** @param x The x-coordinate of the other point
		 * @param y The y-coordinate of the other point
		 * @return the squared distance between this point and the other point. */
		public DGFixedPoint dst2(int x, int y)
		{
			int xd = x - this.x;
			int yd = y - this.y;

			return (DGFixedPoint)(xd * xd + yd * yd);
		}

		/** @param other The other point
		 * @return the distance between this point and the other vector. */
		public DGFixedPoint dst(DGGridPoint2 other)
		{
			int xd = other.x - x;
			int yd = other.y - y;

			return DGFixedPointMath.Sqrt((DGFixedPoint)(xd * xd + yd * yd));
		}

		/** @param x The x-coordinate of the other point
		 * @param y The y-coordinate of the other point
		 * @return the distance between this point and the other point. */
		public DGFixedPoint dst(int x, int y)
		{
			int xd = x - this.x;
			int yd = y - this.y;

			return DGFixedPointMath.Sqrt((DGFixedPoint)(xd * xd + yd * yd));
		}

		/** Adds another 2D grid point to this point.
		 *
		 * @param other The other point
		 * @return this 2d grid point for chaining. */
		public DGGridPoint2 add(DGGridPoint2 other)
		{
			x += other.x;
			y += other.y;
			return this;
		}

		/** Adds another 2D grid point to this point.
		 *
		 * @param x The x-coordinate of the other point
		 * @param y The y-coordinate of the other point
		 * @return this 2d grid point for chaining. */
		public DGGridPoint2 add(int x, int y)
		{
			this.x += x;
			this.y += y;
			return this;
		}

		/** Subtracts another 2D grid point from this point.
		 *
		 * @param other The other point
		 * @return this 2d grid point for chaining. */
		public DGGridPoint2 sub(DGGridPoint2 other)
		{
			x -= other.x;
			y -= other.y;
			return this;
		}

		/** Subtracts another 2D grid point from this point.
		 *
		 * @param x The x-coordinate of the other point
		 * @param y The y-coordinate of the other point
		 * @return this 2d grid point for chaining. */
		public DGGridPoint2 sub(int x, int y)
		{
			this.x -= x;
			this.y -= y;
			return this;
		}

		/** @return a copy of this grid point */
		public DGGridPoint2 cpy()
		{
			return new DGGridPoint2(this);
		}

		public override bool Equals(object o)
		{
			var other = (DGGridPoint2)o;
			return this.x == other.x && this.y == other.y;
		}

		public override int GetHashCode()
		{
			int prime = 53;
			int result = 1;
			result = prime * result + this.x;
			result = prime * result + this.y;
			return result;
		}

		public override string ToString()
		{
			return "(" + x + ", " + y + ")";
		}
	}
}

