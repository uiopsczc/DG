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

#if UNITY_STANDALONE
using UnityEngine;

#endif

namespace DG
{
	public partial struct FPRay
	{
#if UNITY_STANDALONE
		public static implicit operator Ray(FPRay value)
		{
			return new Ray(value.origin, value.direction);
		}

		public static implicit operator FPRay(Ray value)
		{
			return new FPRay(value.origin, value.direction);
		}
#endif

		/*************************************************************************************
		* 模块描述:Equals ToString
		*************************************************************************************/
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			var other = (FPRay)obj;
			return Equals(other);
		}

		public bool Equals(FPRay other)
		{
			return other.origin == origin && other.direction == direction;
		}

		public override int GetHashCode()
		{
			int prime = 73;
			int result = 1;
			result = prime * result + direction.GetHashCode();
			result = prime * result + origin.GetHashCode();
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
			return origin + direction * distance;
		}
	}
}
