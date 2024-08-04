using System;
using System.Collections.Generic;

namespace DG
{
	public partial class TypeUtil
	{
		private static readonly Dictionary<string, Type> _cacheDict = new();
		public static Dictionary<string, Type> GetCacheDict() => _cacheDict;
	}
}