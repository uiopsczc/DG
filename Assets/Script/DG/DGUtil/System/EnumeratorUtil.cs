using System;
using System.Collections;
using System.Collections.Generic;

namespace DG
{
	public static class EnumeratorUtil
	{
		/// <summary>
		/// ȥ��ָ��index
		/// </summary>
		/// <param name="enumerator"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static IEnumerator GoToIndex(IEnumerator enumerator, int index)
		{
			enumerator.Reset();
			int curIndex = 0;
			while (curIndex <= index)
			{
				enumerator.MoveNext();
				curIndex++;
			}

			return enumerator;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="enumerator"></param>
		/// <param name="curIndex">��-1��ʼ</param>
		/// <returns></returns>
		public static bool MoveNext(IEnumerator enumerator, ref int curIndex)
		{
			curIndex++;
			var result = enumerator.MoveNext();
			return result;
		}

		public static EnumeratorScope<T> Scope<T>(IEnumerator<T> enumerator)
		{
			return new EnumeratorScope<T>(enumerator);
		}
	}
}