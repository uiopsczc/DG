/*************************************************************************************
 * ��    ��:  
 * �� �� ��:  czq
 * ����ʱ��:  2023/5/12
 * ======================================
 * ��ʷ���¼�¼
 * �汾:V          �޸�ʱ��:         �޸���:
 * �޸�����:
 * ======================================
*************************************************************************************/

using FP = DGFixedPoint;
using FPVector3 = DGVector3;
#if UNITY_5_3_OR_NEWER
using UnityEngine;

#endif

public struct DGRay
{
	private FPVector3 _direction;

	/// <summary>
	///   <para>The origin point of the ray.</para>
	/// </summary>
	public FPVector3 origin { get; set; }

	/// <summary>
	///   <para>The direction of the ray.</para>
	/// </summary>
	public FPVector3 direction
	{
		get => this._direction;
		set => this._direction = value.normalized;
	}

	/// <summary>
	///   <para>Creates a ray starting at origin along direction.</para>
	/// </summary>
	/// <param name="origin"></param>
	/// <param name="direction"></param>
	public DGRay(FPVector3 origin, FPVector3 direction)
	{
		this.origin = origin;
		this._direction = direction.normalized;
	}

#if UNITY_5_3_OR_NEWER
	public DGRay(Ray ray)
	{
		this.origin = new FPVector3(ray.origin);
		this._direction = new FPVector3(ray.direction);
	}
#endif

	/*************************************************************************************
	* ģ������:ToString
	*************************************************************************************/
	public override string ToString()
	{
		return "{origin:" + origin + ", direction:" + direction + "}";
	}

	/*************************************************************************************
	* ģ������:Member Util
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