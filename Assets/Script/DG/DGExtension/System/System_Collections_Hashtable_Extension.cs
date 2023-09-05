using System.Collections;
using System.Collections.Generic;

namespace DG
{
	public static class System_Collections_Hashtable_Extension
	{
		public static Dictionary<K, V> ToDict<K, V>(this Hashtable self)
		{
			return HashtableUtil.ToDict<K, V>(self);
		}
	}
}