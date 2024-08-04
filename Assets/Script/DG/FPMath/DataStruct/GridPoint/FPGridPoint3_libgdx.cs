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
	public struct FPGridPoint3
	{
		public int x;
		public int y;
		public int z;


		/** Constructs a 3D grid point.
		 * 
		 * @param x X coordinate
		 * @param y Y coordinate
		 * @param z Z coordinate */
		public FPGridPoint3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/** Copy constructor
		 * 
		 * @param point The 3D grid point to make a copy of. */
		public FPGridPoint3(FPGridPoint3 point)
		{
			x = point.x;
			y = point.y;
			z = point.z;
		}

		/** Sets the coordinates of this 3D grid point to that of another.
		 * 
		 * @param point The 3D grid point to copy coordinates of.
		 * 
		 * @return this GridPoint3 for chaining. */
		public FPGridPoint3 set(FPGridPoint3 point)
		{
			x = point.x;
			y = point.y;
			z = point.z;
			return this;
		}

		/** Sets the coordinates of this GridPoint3D.
		 * 
		 * @param x X coordinate
		 * @param y Y coordinate
		 * @param z Z coordinate
		 * 
		 * @return this GridPoint3D for chaining. */
		public FPGridPoint3 set(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			return this;
		}

		/** @param other The other point
		 * @return the squared distance between this point and the other point. */
		public FP dst2(FPGridPoint3 other)
		{
			int xd = other.x - x;
			int yd = other.y - y;
			int zd = other.z - z;

			return (xd * xd + yd * yd + zd * zd);
		}

		/** @param x The x-coordinate of the other point
		 * @param y The y-coordinate of the other point
		 * @param z The z-coordinate of the other point
		 * @return the squared distance between this point and the other point. */
		public FP dst2(int x, int y, int z)
		{
			int xd = x - this.x;
			int yd = y - this.y;
			int zd = z - this.z;

			return (xd * xd + yd * yd + zd * zd);
		}

		/** @param other The other point
		 * @return the distance between this point and the other vector. */
		public FP dst(FPGridPoint3 other)
		{
			int xd = other.x - x;
			int yd = other.y - y;
			int zd = other.z - z;

			return FPMath.Sqrt((xd * xd + yd * yd + zd * zd));
		}

		/** @param x The x-coordinate of the other point
		 * @param y The y-coordinate of the other point
		 * @param z The z-coordinate of the other point
		 * @return the distance between this point and the other point. */
		public FP dst(int x, int y, int z)
		{
			int xd = x - this.x;
			int yd = y - this.y;
			int zd = z - this.z;

			return FPMath.Sqrt((xd * xd + yd * yd + zd * zd));
		}

		/** Adds another 3D grid point to this point.
		 *
		 * @param other The other point
		 * @return this 3d grid point for chaining. */
		public FPGridPoint3 add(FPGridPoint3 other)
		{
			x += other.x;
			y += other.y;
			z += other.z;
			return this;
		}

		/** Adds another 3D grid point to this point.
		 *
		 * @param x The x-coordinate of the other point
		 * @param y The y-coordinate of the other point
		 * @param z The z-coordinate of the other point
		 * @return this 3d grid point for chaining. */
		public FPGridPoint3 add(int x, int y, int z)
		{
			this.x += x;
			this.y += y;
			this.z += z;
			return this;
		}

		/** Subtracts another 3D grid point from this point.
		 *
		 * @param other The other point
		 * @return this 3d grid point for chaining. */
		public FPGridPoint3 sub(FPGridPoint3 other)
		{
			x -= other.x;
			y -= other.y;
			z -= other.z;
			return this;
		}

		/** Subtracts another 3D grid point from this point.
		 *
		 * @param x The x-coordinate of the other point
		 * @param y The y-coordinate of the other point
		 * @param z The z-coordinate of the other point
		 * @return this 3d grid point for chaining. */
		public FPGridPoint3 sub(int x, int y, int z)
		{
			this.x -= x;
			this.y -= y;
			this.z -= z;
			return this;
		}

		/** @return a copy of this grid point */
		public FPGridPoint3 cpy()
		{
			return new FPGridPoint3(this);
		}

		public override bool Equals(object o)
		{
			var other = (FPGridPoint3)o;
			return x == other.x && y == other.y && z == other.z;
		}

		public override int GetHashCode()
		{
			int prime = 17;
			int result = 1;
			result = prime * result + x;
			result = prime * result + y;
			result = prime * result + z;
			return result;
		}

		public override string ToString()
		{
			return "(" + x + ", " + y + ", " + z + ")";
		}
	}
}
