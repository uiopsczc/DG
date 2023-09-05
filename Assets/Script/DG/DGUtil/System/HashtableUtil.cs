using System.Collections;
using System.Collections.Generic;

namespace DG
{
	public static class HashtableUtil
	{
		public static Dictionary<K, V> ToDict<K, V>(Hashtable hashtable)
		{
			Dictionary<K, V> dict = new Dictionary<K, V>();
			foreach (DictionaryEntry dictionaryEntry in hashtable)
			{
				var key = dictionaryEntry.Key.To<K>();
				var value = dictionaryEntry.Value.To<V>();
				dict[key] = value;
			}
			return dict;
		}

	}
}

