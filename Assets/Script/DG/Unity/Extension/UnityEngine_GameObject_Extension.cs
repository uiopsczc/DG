using System;
using UnityEngine;
using UnityEngine.UI;

namespace DG
{
	public static class UnityEngine_GameObject_Extension
	{
		/// <summary>
		///   有T返回T，没T添加T
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="gameObject"></param>
		/// <returns></returns>
		public static Component GetOrAddComponent(this GameObject self, Type type)
		{
			return GameObjectUtil.GetOrAddComponent(self, type);
		}

		public static T GetOrAddComponent<T>(this GameObject self) where T : Component
		{
			return GameObjectUtil.GetOrAddComponent<T>(self);
		}

		/// <summary>
		///   使某个类型的组件enable
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="gameObject"></param>
		/// <param name="isEnable"></param>
		public static void EnableComponents(this GameObject self, Type type, bool isEnable)
		{
			GameObjectUtil.EnableComponents(self, type, isEnable);
		}

		public static void EnableComponents<T>(this GameObject self, bool isEnable) where T : MonoBehaviour
		{
			GameObjectUtil.EnableComponents<T>(self, isEnable);
		}

		/// <summary>
		///   销毁子孩子节点
		/// </summary>
		/// <param name="self"></param>
		public static void DestroyChildren(this GameObject self)
		{
			GameObjectUtil.DestroyChildren(self);
		}


		/// <summary>
		///   只有包含全部的Components才会返回True
		/// </summary>
		/// <param name="self"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		public static bool IsHasComponents(this GameObject self, params Type[] types)
		{
			return GameObjectUtil.IsHasComponents(self, types);
		}

		public static bool IsHasComponent(this GameObject self, Type type)
		{
			return GameObjectUtil.IsHasComponent(self, type);
		}

		public static bool IsHasComponent<T>(this GameObject self) where T : Component
		{
			return GameObjectUtil.IsHasComponent<T>(self);
		}

		/// <summary>
		///   获取该gameObject下的组件，不包括剔除的组件类型
		/// </summary>
		/// <param name="self"></param>
		/// <param name="excludeComponentTypes">剔除的组件类型</param>
		/// <returns></returns>
		public static Component[] GetComponentsExclude(this GameObject self, params Type[] excludeComponentTypes)
		{
			return GameObjectUtil.GetComponentsExclude(self, excludeComponentTypes);
		}

		/// <summary>
		///   获取该gameObject下的组件，不包括剔除的组件类型
		/// </summary>
		/// <param name="self"></param>
		/// <param name="excludeComponentTypes">剔除的组件类型</param>
		/// <param name="excludeSeparator"></param>
		/// <returns></returns>
		public static Component[] GetComponentsExclude(this GameObject self, string excludeComponentTypes,
			string excludeSeparator = StringConst.STRING_VERTICAL)
		{
			return GameObjectUtil.GetComponentsExclude(self, excludeComponentTypes, excludeSeparator);
		}

		public static GameObject GetSocketGameObject(this GameObject self, string socketName = null)
		{
			return GameObjectUtil.GetSocketGameObject(self, socketName);
		}

				public static void SetPause(GameObject gameObject, object cause)
				{
					PauseUtil.SetPause(gameObject, cause);
				}

		public static RectTransform RectTransform(this GameObject self)
		{
			return GameObjectUtil.RectTransform(self);
		}

		public static bool IsSceneGameObject(this GameObject self)
		{
			return GameObjectUtil.IsSceneGameObject(self);
		}

				public static void DeSpawn(this GameObject self)
				{
					GameObjectUtil.DeSpawn(self);
				}

		public static void SetCache(this GameObject self, string key, object obj)
		{
			GameObjectUtil.SetCache(self, key, obj);
		}

		public static void SetCache(this GameObject self, string key, string subKey, object obj)
		{
			GameObjectUtil.SetCache(self, key, subKey, obj);
		}

		public static T GetCache<T>(this GameObject self, string key = null)
		{
			return GameObjectUtil.GetCache<T>(self, key);
		}


		public static T GetCache<T>(this GameObject self, string key, string subKey)
		{
			return GameObjectUtil.GetCache<T>(self, key, subKey);
		}

		public static T GetOrAddCache<T>(this GameObject self, string key, Func<T> defaultFunc)
		{
			return GameObjectUtil.GetOrAddCache(self, key, defaultFunc);
		}

		public static object GetOrAddCache(this GameObject self, string key, Func<object> defaultFunc)
		{
			return GameObjectUtil.GetOrAddCache(self, key, defaultFunc);
		}

		public static T GetOrAddCache<T>(this GameObject self, string key, string subKey, Func<T> defaultFunc)
		{
			return GameObjectUtil.GetOrAddCache(self, key, subKey, defaultFunc);
		}

		public static bool IsCacheContainsKey(this GameObject self, string key)
		{
			return GameObjectUtil.IsCacheContainsKey(self, key);
		}

		public static GameObject NewChildGameObject(this GameObject self, string path = null)
		{
			return GameObjectUtil.NewChildGameObject(self, path);
		}

		public static Component NewChildWithComponent(this GameObject self, Type componentType, string path = null)
		{
			return GameObjectUtil.NewChildWithComponent(self, componentType, path);
		}

		public static T NewChildWithComponent<T>(this GameObject self, string path = null) where T : Component
		{
			return GameObjectUtil.NewChildWithComponent<T>(self, path);
		}

		public static RectTransform NewChildWithRectTransform(this GameObject self, string path = null)
		{
			return GameObjectUtil.NewChildWithRectTransform(self, path);
		}

		public static Image NewChildWithImage(this GameObject self, string path = null)
		{
			return GameObjectUtil.NewChildWithImage(self, path);
		}

		public static Text NewChildWithText(this GameObject self, string path = null, string content = null,
			int fontSize = 20, Color? color = null, TextAnchor? alignment = null, Font font = null)
		{
			return GameObjectUtil.NewChildWithText(self, path, content, fontSize, color, alignment, font);
		}

		public static void SetIsGray(this GameObject self, bool isGray, bool isRecursive = true)
		{
			GameObjectUtil.SetIsGray(self, isGray, isRecursive);
		}

		public static void DoActionRecursive(this GameObject self, Action<GameObject> doAction)
		{
			GameObjectUtil.DoActionRecursive(self, doAction);
		}

		public static void SetAlpha(this GameObject self, float alpha, bool isRecursive = true)
		{
			GameObjectUtil.SetAlpha(self, alpha, isRecursive);
		}

		public static void SetColor(this GameObject self, Color color, bool isNotUseColorAlpha = false,
			bool isRecursive = true)
		{
			GameObjectUtil.SetColor(self, color, isNotUseColorAlpha, isRecursive);
		}


		public static (bool, string) GetRelativePath(this GameObject self, GameObject parentGameObject = null)
		{
			return GameObjectUtil.GetRelativePath(self, parentGameObject);
		}

		public static float GetParticleSystemDuration(this GameObject self, bool isRecursive = true)
		{
			return GameObjectUtil.GetParticleSystemDuration(self, isRecursive);
		}


		#region GameObject 反射

		#region FiledValue

		public static T GetFieldValue<T>(this GameObject self, string fieldInfoString, T defaultValue,
			params Type[] excludeComponentTypes)
		{
			return GameObjectUtil.GetFieldValue(self, fieldInfoString, defaultValue, excludeComponentTypes);
		}

		public static void SetFieldValue(this GameObject self, string fieldInfoString, object value,
			params Type[] excludeComponentTypes)
		{
			GameObjectUtil.SetFieldValue(self, fieldInfoString, value, excludeComponentTypes);
		}

		#endregion

		#region ProperyValue

		public static T GetPropertyValue<T>(this GameObject self, string propertyInfoString, T defaultValue,
			object[] index = null, params Type[] excludeComponentTypes)
		{
			return GameObjectUtil.GetPropertyValue(self, propertyInfoString, defaultValue, index, excludeComponentTypes);
		}

		public static void SetPropertyValue(this GameObject self, string fieldInfoString, object value,
			object[] index = null, params Type[] excludeComponentTypes)
		{
			GameObjectUtil.SetPropertyValue(self, fieldInfoString, value, index, excludeComponentTypes);
		}

		#endregion

		#region Invoke

		/// <summary>
		///   调用callMethod的方法
		/// </summary>
		/// <param name="self"></param>
		/// <param name="invokeMethodName"></param>
		/// <param name="excludeComponentTypes"></param>
		/// <param name="parameters"></param>
		public static void Invoke(this GameObject self, string invokeMethodName, string excludeComponentTypes = null,
			params object[] parameters)
		{
			GameObjectUtil.Invoke(self, invokeMethodName, excludeComponentTypes, parameters);
		}

		#endregion

		#endregion

	}
}


