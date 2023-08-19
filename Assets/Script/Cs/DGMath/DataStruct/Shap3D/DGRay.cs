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

#if UNITY_STANDALONE
using UnityEngine;

#endif


public partial struct DGRay
{
#if UNITY_STANDALONE
	public DGRay(Ray ray)
	{
		this.origin = new DGVector3(ray.origin);
		this.direction = new DGVector3(ray.direction);
	}
#endif

	/*************************************************************************************
	* ģ������:Equals ToString
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
	* ģ������:Member Util
	*************************************************************************************/
	/// <summary>
	///   <para>Returns a point at distance units along the ray.</para>
	/// </summary>
	/// <param name="distance"></param>
	public DGVector3 GetPoint(DGFixedPoint distance)
	{
		return this.origin + this.direction * distance;
	}
}