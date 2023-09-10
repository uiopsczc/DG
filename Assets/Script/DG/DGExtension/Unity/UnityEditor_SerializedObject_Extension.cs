#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace DG
{
	public static class UnityEditor_SerializedObject_Extension
	{
		public static T TargetObject<T>(this SerializedObject self) where T : Object
		{
			return SerializedObjectUtil.TargetObject<T>(self);
		}

		public static T[] TargetObjects<T>(this SerializedObject self) where T : Object
		{
			return SerializedObjectUtil.TargetObjects<T>(self);
		}
	}
}
#endif

