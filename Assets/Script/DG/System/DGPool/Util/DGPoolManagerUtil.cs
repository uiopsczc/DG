namespace DG
{
	public static partial class DGPoolManagerUtil
	{
		public static string GetPrefabPoolDefaultName(UnityEngine.Object prefab)
		{
			return prefab.name + prefab.GetInstanceID();
		}
	}
}