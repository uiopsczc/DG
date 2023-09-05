using System.Collections.Generic;

namespace DG
{
	public static class ListTUtil
	{
		public static List<T> Combine<T>(List<T> list, IList<T> another, bool isUnique = false)
		{
			list.AddRange(another);
			if (isUnique)
				list.Unique();
			return list;
		}
		

		#region 插入删除操作

		

		/// <summary>
		///   跟RemoveRange一样，但返回删除的元素List
		/// </summary>
		public static List<T> RemoveRange2<T>(List<T> list, int index, int length)
		{
			var lastIndex = index + length - 1 <= list.Count - 1 ? index + length - 1 : list.Count - 1;
			var resultList = new List<T>(list.Count);
			for (var i = lastIndex; i >= index; i--)
				resultList.Add(list[i]);
			resultList.Reverse();
			list.Clear();
			list.AddRange(resultList);
			return list;
		}

		#endregion
		
	}
}

