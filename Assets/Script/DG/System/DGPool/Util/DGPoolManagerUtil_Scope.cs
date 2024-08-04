using System;

namespace DG
{
	public static partial class DGPoolManagerUtil
	{
		public static T SpawnScope<T>(DGPoolManager poolManager, Action<T> onSpawnCallback = null) where T : DGPoolScope
		{
			return poolManager.Spawn(null, null, onSpawnCallback).poolItem.GetValue();
		}
	}
}