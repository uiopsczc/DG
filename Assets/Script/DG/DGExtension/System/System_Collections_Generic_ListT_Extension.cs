using System.Collections.Generic;

namespace DG
{
	public static class System_Collections_Generic_ListT_Extension
	{
		public static List<T> Combine<T>(this List<T> self, IList<T> another, bool isUnique = false)
		{
			return ListTUtil.Combine(self, another, isUnique);
		}


		#region ����ɾ������

		/// <summary>
		///   ��RemoveRangeһ����������ɾ����Ԫ��List
		/// </summary>
		public static List<T> RemoveRange2<T>(this List<T> self, int index, int length)
		{
			return ListTUtil.RemoveRange2(self, index, length);
		}

		#endregion
	}
}