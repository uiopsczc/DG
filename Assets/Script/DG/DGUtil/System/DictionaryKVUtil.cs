using System;
using System.Collections.Generic;

namespace DG
{
	public static class DictionaryKVUtil
	{
		public static Dictionary<K, V> EmptyIfNull<K, V>(Dictionary<K, V> dict)
		{
			return dict ?? new Dictionary<K, V>();
		}
	}
}