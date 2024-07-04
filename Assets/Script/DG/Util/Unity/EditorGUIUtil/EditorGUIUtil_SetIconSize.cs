#if UNITY_EDITOR
using UnityEngine;

namespace DG
{
	public partial class EditorGUIUtil
	{
		public EditorGUISetIconSizeScope SetIconSize(Vector2 newSize)
		{
			return new EditorGUISetIconSizeScope(newSize);
		}
	}
}
#endif