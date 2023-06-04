/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/5/12
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

using System;
using FP = DGFixedPoint;
using FPVector3 = DGVector3;
using FPMatrix4x4 = DGMatrix4x4;

/** Encapsulates a ray having a starting position and a unit length direction.
 * 
 * @author badlogicgames@gmail.com */
public partial struct DGRay
{
	public FPVector3 origin;
	public FPVector3 direction;
	

	/** Constructor, sets the starting position of the ray and the direction.
	 * 
	 * @param origin The starting position
	 * @param direction The direction */
	public DGRay(FPVector3 origin, FPVector3 direction)
	{
		this.origin = default;
		this.direction = default;
		this.origin.set(origin);
		this.direction.set(direction).nor();
	}

	/** @return a copy of this ray. */
	public DGRay cpy()
	{
		return new DGRay(this.origin, this.direction);
	}

	/** Returns the endpoint given the distance. This is calculated as startpoint + distance * direction.
	 * @param out The vector to set to the result
	 * @param distance The distance from the end point to the start point.
	 * @return The out param */
	public FPVector3 getEndPoint(FPVector3 outVector, FP distance)
	{
		return outVector.set(direction).scl(distance).add(origin);
	}

	static FPVector3 tmp = new FPVector3();

	/** Multiplies the ray by the given matrix. Use this to transform a ray into another coordinate system.
	 * 
	 * @param matrix The matrix
	 * @return This ray for chaining. */
	public DGRay mul(FPMatrix4x4 matrix)
	{
		tmp.set(origin).add(direction);
		tmp.mul(matrix);
		origin.mul(matrix);
		direction.set(tmp.sub(origin)).nor();
		return this;
	}

	/** {@inheritDoc} */
	public override string ToString()
	{
		return "ray [" + origin + ":" + direction + "]";
	}

	/** Sets the starting position and the direction of this ray.
	 * 
	 * @param origin The starting position
	 * @param direction The direction
	 * @return this ray for chaining */
	public DGRay set(FPVector3 origin, FPVector3 direction)
	{
		this.origin.set(origin);
		this.direction.set(direction).nor();
		return this;
	}

	/** Sets this ray from the given starting position and direction.
	 * 
	 * @param x The x-component of the starting position
	 * @param y The y-component of the starting position
	 * @param z The z-component of the starting position
	 * @param dx The x-component of the direction
	 * @param dy The y-component of the direction
	 * @param dz The z-component of the direction
	 * @return this ray for chaining */
	public DGRay set(FP x, FP y, FP z, FP dx, FP dy, FP dz)
	{
		this.origin.set(x, y, z);
		this.direction.set(dx, dy, dz).nor();
		return this;
	}

	/** Sets the starting position and direction from the given ray
	 * 
	 * @param ray The ray
	 * @return This ray for chaining */
	public DGRay set(DGRay ray)
	{
		this.origin.set(ray.origin);
		this.direction.set(ray.direction).nor();
		return this;
	}
}