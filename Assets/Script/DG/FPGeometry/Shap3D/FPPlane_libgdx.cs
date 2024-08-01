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
	/// <summary>
	/// Enum specifying on which side a point lies respective to the plane and it's normal. {@link PlaneSide#Front} is the side to
	/// which the normal points.
	/// </summary>
	public enum DGPlaneSide
	{
		OnPlane,
		Back,
		Front
	}

	/// <summary>
	/// A plane defined via a unit length normal and the distance from the origin, as you learned in your math class.
	/// </summary>
	public partial struct FPPlane
	{
		public FPVector3 normal;
		public FP d;


		/** Constructs a new plane based on the normal and distance to the origin.
		 * 
		 * @param normal The plane normal
		 * @param d The distance to the origin */
		public FPPlane(FPVector3 normal, FP d)
		{
			this.normal = default;
			this.normal = this.normal.set(normal).nor();
			this.d = d;
		}

		/** Constructs a new plane based on the normal and a point on the plane.
		 * 
		 * @param normal The normal
		 * @param point The point on the plane */
		public FPPlane(FPVector3 normal, FPVector3 point)
		{
			this.normal = default;
			this.normal = this.normal.set(normal).nor();
			this.d = -this.normal.dot(point);
		}

		/** Constructs a new plane out of the three given points that are considered to be on the plane. The normal is calculated via a
		 * cross product between (point1-point2)x(point2-point3)
		 * 
		 * @param point1 The first point
		 * @param point2 The second point
		 * @param point3 The third point */
		public FPPlane(FPVector3 point1, FPVector3 point2, FPVector3 point3)
		{
			normal = default;
			normal = normal.set(point1).sub(point2).crs(point2.x - point3.x, point2.y - point3.y, point2.z - point3.z).nor();
			d = -point1.dot(normal);
		}

		/** Sets the plane normal and distance to the origin based on the three given points which are considered to be on the plane.
		 * The normal is calculated via a cross product between (point1-point2)x(point2-point3)
		 * 
		 * @param point1
		 * @param point2
		 * @param point3 */
		public void set(FPVector3 point1, FPVector3 point2, FPVector3 point3)
		{
			normal = normal.set(point1).sub(point2).crs(point2.x - point3.x, point2.y - point3.y, point2.z - point3.z).nor();
			d = -point1.dot(normal);
		}

		/** Sets the plane normal and distance
		 * 
		 * @param nx normal x-component
		 * @param ny normal y-component
		 * @param nz normal z-component
		 * @param d distance to origin */
		public void set(FP nx, FP ny, FP nz, FP d)
		{
			normal.set(nx, ny, nz);
			this.d = d;
		}

		/** Calculates the shortest signed distance between the plane and the given point.
		 * 
		 * @param point The point
		 * @return the shortest signed distance between the plane and the point */
		public FP distance(FPVector3 point)
		{
			return normal.dot(point) + d;
		}

		/** Returns on which side the given point lies relative to the plane and its normal. PlaneSide.Front refers to the side the
		 * plane normal points to.
		 * 
		 * @param point The point
		 * @return The side the point lies relative to the plane */
		public DGPlaneSide testPoint(FPVector3 point)
		{
			FP dist = normal.dot(point) + d;

			if (dist == 0)
				return DGPlaneSide.OnPlane;
			if (dist < 0)
				return DGPlaneSide.Back;
			return DGPlaneSide.Front;
		}

		/** Returns on which side the given point lies relative to the plane and its normal. PlaneSide.Front refers to the side the
		 * plane normal points to.
		 * 
		 * @param x
		 * @param y
		 * @param z
		 * @return The side the point lies relative to the plane */
		public DGPlaneSide testPoint(FP x, FP y, FP z)
		{
			FP dist = normal.dot(x, y, z) + d;

			if (dist == 0)
				return DGPlaneSide.OnPlane;
			if (dist < 0)
				return DGPlaneSide.Back;
			return DGPlaneSide.Front;
		}

		/** Returns whether the plane is facing the direction vector. Think of the direction vector as the direction a camera looks in.
		 * This method will return true if the front side of the plane determined by its normal faces the camera.
		 * 
		 * @param direction the direction
		 * @return whether the plane is front facing */
		public bool isFrontFacing(FPVector3 direction)
		{
			FP dot = normal.dot(direction);
			return dot <= 0;
		}

		/** @return The normal */
		public FPVector3 getNormal()
		{
			return normal;
		}

		/** @return The distance to the origin */
		public FP getD()
		{
			return d;
		}

		/** Sets the plane to the given point and normal.
		 * 
		 * @param point the point on the plane
		 * @param normal the normal of the plane */
		public void set(FPVector3 point, FPVector3 normal)
		{
			this.normal.set(normal);
			d = -point.dot(normal);
		}

		public void set(FP pointX, FP pointY, FP pointZ, FP norX, FP norY,
			FP norZ)
		{
			this.normal.set(norX, norY, norZ);
			d = -(pointX * norX + pointY * norY + pointZ * norZ);
		}

		/** Sets this plane from the given plane
		 * 
		 * @param plane the plane */
		public void set(FPPlane plane)
		{
			this.normal.set(plane.normal);
			this.d = plane.d;
		}

		public override string ToString()
		{
			return normal + ", " + d;
		}
	}
}
