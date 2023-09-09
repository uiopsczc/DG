using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DG
{
	public partial class UnityObjectUtil
	{
		public static void Destroy(Object o)
		{
#if UNITY_EDITOR
			if (o.IsAsset())
			{
				AssetDatabase.DeleteAsset(o.GetAssetPath());
				return;
			}
#endif

			if (Application.isPlaying)
				Object.Destroy(o);
			else
			{
				Object.DestroyImmediate(o);
			}
		}

		public static bool IsNull(Object o)
		{
			return o == null;
		}
	}
}