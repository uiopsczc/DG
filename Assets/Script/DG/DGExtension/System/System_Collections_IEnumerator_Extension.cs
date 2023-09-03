using System.Collections;
using System.Collections.Generic;

namespace DG
{
	public static class System_Collections_IEnumerator_Extension
	{
		/// <summary>
		/// 去到指定index
		/// </summary>
		/// <param name="self"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static IEnumerator GoToIndex(this IEnumerator self, int index)
		{
			return EnumeratorUtil.GoToIndex(self, index);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="self"></param>
		/// <param name="curIndex">从-1开始</param>
		/// <returns></returns>
		public static bool MoveNext(this IEnumerator self, ref int curIndex)
		{
			return EnumeratorUtil.MoveNext(self, ref curIndex);
		}

		public static EnumeratorScope<T> Scope<T>(this IEnumerator<T> self)
		{
			return EnumeratorUtil.Scope(self);
		}
	}
}