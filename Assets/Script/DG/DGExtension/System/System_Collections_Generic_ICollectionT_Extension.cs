using System.Collections;
using System.Collections.Generic;

namespace DG
{
	public static class System_Collections_Generic_ICollectionT_Extension
	{
		public static bool IsNullOrEmpty(this ICollection self)
		{
			return CollectionUtil.IsNullOrEmpty(self);
		}

		public static T[] ToArray<T>(this ICollection self)
		{
			return CollectionUtil.ToArray<T>(self);
		}

		public static List<T> ToList<T>(this ICollection self)
		{
			return CollectionUtil.ToList<T>(self);
		}


		
	}
}