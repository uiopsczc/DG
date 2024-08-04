using UnityEngine;

namespace DG
{
	public static partial class DGPoolManagerUtil
	{
		public static string GetPrefabPoolDefaultName(Object prefab)
		{
			return prefab.name + prefab.GetInstanceID();
		}
	}
}