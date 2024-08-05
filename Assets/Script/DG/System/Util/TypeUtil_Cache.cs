using System;
using System.Collections.Generic;

namespace DG
{
    public partial class TypeUtil
    {
        private static readonly Dictionary<string, Type> _name2TypeCache = new();
        public static Dictionary<string, Type> GetName2TypeCache() => _name2TypeCache;
    }
}