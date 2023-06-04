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

using FP = DGFixedPoint;
using FPVector3 = DGVector3;
#if UNITY_5_3_OR_NEWER
using UnityEngine;

#endif


public partial struct DGRay
{
#if UNITY_5_3_OR_NEWER
	public DGRay(Ray ray)
	{
		this.origin = new FPVector3(ray.origin);
		this.direction = new FPVector3(ray.direction);
	}
#endif

	/*************************************************************************************
	* 模块描述:Equals ToString
	*************************************************************************************/
	public override bool Equals(object obj)
	{
		if (obj == null)
			return false;
		var other = (DGRay) obj;
		return Equals(other);
	}

	public bool Equals(DGRay other)
	{
		return other.origin == origin && other.direction == direction;
	}

	public override int GetHashCode()
	{
		int prime = 73;
		int result = 1;
		result = prime * result + this.direction.GetHashCode();
		result = prime * result + this.origin.GetHashCode();
		return result;
	}

	/*************************************************************************************
	* 模块描述:Member Util
	*************************************************************************************/
	/// <summary>
	///   <para>Returns a point at distance units along the ray.</para>
	/// </summary>
	/// <param name="distance"></param>
	public FPVector3 GetPoint(FP distance)
	{
		return this.origin + this.direction * distance;
	}
}