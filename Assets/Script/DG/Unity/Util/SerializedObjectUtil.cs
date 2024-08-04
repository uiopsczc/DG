#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace DG
{
	public class SerializedObjectUtil
	{
		public static T TargetObject<T>(SerializedObject serializedObject) where T : Object
		{
			return serializedObject.targetObject as T;
		}

		public static T[] TargetObjects<T>(SerializedObject serializedObject) where T : Object
		{
			return serializedObject.targetObjects.ToArray<T>();
		}
	}
}
#endif