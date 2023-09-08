using System;
using System.Collections.Generic;

namespace DG
{
	public static class System_Collections_Generic_DictionaryKV_Extension
	{
		
		public static Dictionary<K, V> EmptyIfNull<K, V>(Dictionary<K, V> dict)
		{
			return DictionaryKVUtil.EmptyIfNull(dict);
		}

	}
}