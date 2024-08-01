using System;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DG
{
	public partial class TransformUtil
	{
		/// <summary>
		/// GetName    赋值物体的时候，名字可能出现去掉（），空格等，去掉这些冗余得到的名字
		/// </summary>
		/// <param name="transform"></param>
		/// <returns></returns>
		public static string GetName(Transform transform)
		{
			string prefabName = transform.name;
			int removeIndex = -1;
			if ((removeIndex = prefabName.IndexOf(StringConst.STRING_LEFT_ROUND_BRACKETS)) >= 0)
				prefabName = prefabName.Remove(removeIndex);

			if ((removeIndex = prefabName.IndexOf(StringConst.STRING_SPACE)) >= 0)
				prefabName = prefabName.Remove(removeIndex);

			return prefabName;
		}

		#region Find children

		/// <summary>
		/// 找到一个符合条件的TransformA后，不会再在该TransformA中继续查找，而是找TransformA的下一个兄弟节点
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Component[] FindComponentsInChildren(Transform transform, Type type, string name,
			bool isRecursive = true, bool isStartsWith = true)
		{
			List<Component> list = new List<Component>();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (isStartsWith)
				{
					if (child.name.StartsWith(name))
						list.Add(child.GetComponent(type));
				}
				else if (child.name.Equals(name))
					list.Add(child.GetComponent(type));

				if (!isRecursive) continue;
				Component[] components = FindComponentsInChildren(child, type, name, isRecursive, isStartsWith);
				if (components == null || components.Length <= 0) continue;
				list.AddRange(components);
			}

			return list.Count == 0 ? null : list.ToArray();
		}

		public static T[] FindComponentsInChildren<T>(Transform transform, string name, bool isRecursive = true,
			bool isStartsWith = true) where T : Component
		{
			Component[] components = FindComponentsInChildren(transform, typeof(T), name, isRecursive, isStartsWith);
			return components?.ToArray<T>();
		}

		public static Component FindComponentInChildren(Transform transform, Type type, string name,
			bool isRecursive = true, bool isStartsWith = true)
		{
			if (name.IndexOf(CharConst.CHAR_SLASH) != -1)
				return transform.Find(name).GetComponent(type);

			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (isStartsWith)
				{
					if (child.name.StartsWith(name))
						return child.GetComponent(type);
				}
				else if (child.name.Equals(name))
					return child.GetComponent(type);

				if (isRecursive)
				{
					Component t = FindComponentInChildren(child, type, name, isRecursive, isStartsWith);
					if (t != null)
						return t;
				}
			}

			return null;
		}

		public static T FindComponentInChildren<T>(Transform transform, string name, bool isRecursive = true,
			bool isStartsWith = true) where T : Component
		{
			return FindComponentInChildren(transform, typeof(T), name, isRecursive, isStartsWith) as T;
		}

		public static Component FindComponentWithTagInChildren(Transform transform, Type type, string tagName,
			bool isRecursive = true, bool isStartsWith = true)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (isStartsWith)
				{
					if (child.tag.StartsWith(tagName))
						return child.GetComponent(type);
				}
				else if (child.tag.Equals(tagName))
					return child.GetComponent(type);

				if (!isRecursive) continue;
				Component component = FindComponentWithTagInChildren(child, type, tagName, true, isStartsWith);
				if (component != null)
					return component;
			}

			return null;
		}

		public static T FindComponentWithTagInChildren<T>(Transform transform, string tagName,
			bool isRecursive = true,
			bool isStartsWith = true) where T : Component
		{
			return FindComponentWithTagInChildren(transform, typeof(T), tagName, isRecursive, isStartsWith) as T;
		}

		public static Component[] FindComponentsWithTagInChildren(Transform transform, Type type, string tagName,
			bool isRecursive = true, bool isStartsWith = true)
		{
			List<Component> list = new List<Component>();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (isStartsWith)
				{
					if (child.tag.StartsWith(tagName))
						list.Add(child.GetComponent(type));
				}
				else if (child.CompareTag(tagName))
					list.Add(child.GetComponent(type));

				if (!isRecursive) continue;
				Component[] components =
					FindComponentsWithTagInChildren(child, type, tagName, isRecursive, isStartsWith);
				if (components == null || components.Length <= 0) continue;
				list.AddRange(components);
			}

			return list.Count == 0 ? null : list.ToArray();
		}

		public static T[] FindComponentsWithTagInChildren<T>(Transform transform, string tagName,
			bool isRecursive = true,
			bool isStartsWith = true) where T : Component
		{
			Component[] components =
				FindComponentsWithTagInChildren(transform, typeof(T), tagName, isRecursive, isStartsWith);
			return components?.ToArray<T>();
		}

		#endregion

		#region Find parent

		/// <summary>
		/// 
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Component[] FindComponentsInParent(Transform transform, Type type, string name,
			bool isStartsWith = true)
		{
			List<Component> list = new List<Component>();
			Transform current = transform;
			while (current != null)
			{
				Component component = current.GetComponent(type);
				if (component != null)
				{
					if (isStartsWith)
					{
						if (current.name.StartsWith(name))
							list.Add(component);
					}
					else if (current.name.Equals(name))
						list.Add(component);
				}

				current = current.parent;
			}

			return list.Count == 0 ? null : list.ToArray();
		}

		public static T[] FindComponentsInParent<T>(Transform transform, string name, bool isStartsWith = true)
			where T : Component
		{
			Component[] components = FindComponentsInParent(transform, typeof(T), name, isStartsWith);
			return components?.ToArray<T>();
		}

		public static Component FindComponentInParent(Transform transform, Type type, string name,
			bool isStartsWith = true)
		{
			Transform current = transform;
			while (current != null)
			{
				Component component = current.GetComponent(type);
				if (component != null)
				{
					if (isStartsWith)
					{
						if (current.name.StartsWith(name))
							return component;
					}
					else if (current.name.Equals(name))
						return component;
				}

				current = current.parent;
			}

			return null;
		}

		public static T FindComponentInParent<T>(Transform transform, string name, bool isStartsWith = true)
			where T : Component
		{
			return FindComponentInParent(transform, typeof(T), name, isStartsWith) as T;
		}

		#endregion

		/// <summary>
		/// 获取直接子孩子节点
		/// </summary>
		/// <param name="root"></param>
		/// <returns></returns>
		public static Transform[] GetChildren(Transform root)
		{
			int count = root.childCount;
			Transform[] transforms = new Transform[count];
			for (int i = 0; i < count; i++)
			{
				Transform transform = root.GetChild(i);
				transforms[i] = transform;
			}

			return transforms;
		}

		/// <summary>
		/// 销毁子节点
		/// </summary>
		/// <param name="root"></param>
		public static void DestroyChildren(Transform root)
		{
			for (int i = root.childCount - 1; i >= 0; i--)
				root.GetChild(i).Destroy();
		}

		/// <summary>
		/// Find子Object，包括Disable的Object也会遍历获取
		/// </summary>
		public static Transform FindChildRecursive(Transform parent, string childName)
		{
			Transform[] transforms = parent.GetComponentsInChildren<Transform>(true);
			for (var i = 0; i < transforms.Length; i++)
			{
				Transform transform = transforms[i];
				if (transform.name.Equals(childName))
					return transform;
			}

			return null;
		}


		/// <summary>
		/// 从根物体到当前物体的全路径, 以/分隔
		/// </summary>
		public static string GetFullPath(Transform transform, Transform rootTransform = null,
			string separator = StringConst.STRING_SLASH)
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append(transform.name);
			Transform iterator = transform.parent;
			while (iterator != rootTransform || iterator != null)
			{
				stringBuilder.Insert(0, separator);
				stringBuilder.Insert(0, iterator.name);
				iterator = iterator.parent;
			}

			return stringBuilder.ToString();
		}

		/// <summary>
		/// 递归设置layer
		/// </summary>
		public static void SetLayerRecursive(Transform transform, int layer)
		{
			if (transform == null)
				return;
			transform.gameObject.layer = layer;
			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = transform.GetChild(i);
				SetLayerRecursive(child, layer);
			}
		}

		/// <summary>
		/// 重置
		/// </summary>
		/// <param name="transform"></param>
		public static void Reset(Transform transform,
			TransformMode transformMode =
				TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
		{
			if (transformMode.Contains(TransformMode.localPosition))
				transform.localPosition = Vector3.zero;
			if (transformMode.Contains(TransformMode.localRotation))
				transform.localRotation = Quaternion.identity;
			if (transformMode.Contains(TransformMode.localScale))
				transform.localScale = Vector3.one;
			if (transformMode.Contains(TransformMode.position))
				transform.position = Vector3.zero;
			if (transformMode.Contains(TransformMode.rotation))
				transform.rotation = Quaternion.identity;
		}

		public static void ResetToParent(Transform transform, GameObject parent,
			TransformMode transformMode =
				TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
		{
			transform.ResetToParent(parent.transform, transformMode);
		}

		public static void ResetToParent(Transform transform, Transform parent,
			TransformMode transformMode =
				TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
		{
			transform.SetParent(parent);
			Reset(transform, transformMode);
		}

		public static (bool, string) GetRelativePath(Transform transform, Transform parentTransform = null)
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append(transform.name);
			if (transform == parentTransform)
				return (true, stringBuilder.ToString());
			Transform parentNode = transform.parent;
			while (!(parentNode == null || parentNode == parentTransform))
			{
				stringBuilder.Insert(0, parentNode.name + StringConst.STRING_SLASH);
				parentNode = parentNode.parent;
			}

			bool isFound = parentTransform == parentNode;
			if (isFound && parentNode != null)
				stringBuilder.Insert(0, parentNode.name + StringConst.STRING_SLASH);
			return (isFound, stringBuilder.ToString());
		}

		#region SetPositon,LocalPosition,Euler,LocalEuler,Rotation, LocalRotation,LocalScale,LossyScale

		#region position

		public static void SetPositionX(Transform transform, float value)
		{
			transform.position = new Vector3(value, transform.position.y, transform.position.z);
		}

		public static void SetPositionY(Transform transform, float value)
		{
			transform.position = new Vector3(transform.position.x, value, transform.position.z);
		}

		public static void SetPositionZ(Transform transform, float value)
		{
			transform.localPosition = new Vector3(transform.position.x, transform.position.y, value);
		}

		public static void SetLocalPositionX(Transform transform, float value)
		{
			transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z);
		}

		public static void SetLocalPositionY(Transform transform, float value)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
		}

		public static void SetLocalPositionZ(Transform transform, float value)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, value);
		}

		#endregion

		#region eulerAngles

		public static void SetEulerAnglesX(Transform transform, float value)
		{
			transform.eulerAngles = new Vector3(value, transform.eulerAngles.y, transform.eulerAngles.z);
		}

		public static void SetEulerAnglesY(Transform transform, float value)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, value, transform.eulerAngles.z);
		}

		public static void SetEulerAnglesZ(Transform transform, float value)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, value);
		}

		public static void SetLocalEulerAnglesX(Transform transform, float value)
		{
			transform.localEulerAngles = new Vector3(value, transform.localEulerAngles.y, transform.localEulerAngles.z);
		}

		public static void SetLocalEulerAnglesY(Transform transform, float value)
		{
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, value, transform.localEulerAngles.z);
		}

		public static void SetLocalEulerAnglesZ(Transform transform, float value)
		{
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, value);
		}

		#endregion

		#region Rotation

		public static void SetRotationX(Transform transform, float value)
		{
			transform.rotation =
				new Quaternion(value, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		}

		public static void SetRotationY(Transform transform, float value)
		{
			transform.rotation =
				new Quaternion(transform.rotation.x, value, transform.rotation.z, transform.rotation.w);
		}

		public static void SetRotationZ(Transform transform, float value)
		{
			transform.rotation =
				new Quaternion(transform.rotation.x, transform.rotation.y, value, transform.rotation.w);
		}

		public static void SetRotationW(Transform transform, float value)
		{
			transform.rotation =
				new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, value);
		}

		public static void SetLocalRotationX(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(value, transform.localRotation.y, transform.localRotation.z,
				transform.localRotation.w);
		}

		public static void SetLocalRotationY(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, value, transform.localRotation.z,
				transform.localRotation.w);
		}

		public static void SetLocalRotationZ(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, value,
				transform.localRotation.w);
		}

		public static void SetLocalRotationW(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y,
				transform.localRotation.z, value);
		}

		#endregion

		#region scale

		public static void SetLocalScaleX(Transform transform, float value)
		{
			transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
		}

		public static void SetLocalScaleY(Transform transform, float value)
		{
			transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);
		}

		public static void SetLocalScaleZ(Transform transform, float value)
		{
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value);
		}

		public static Vector3 GetLossyScaleOfParent(Transform transform)
		{
			Vector3 result = Vector3.one;
			Transform current = transform.parent;
			while (current != null)
			{
				result = result.Multiply(current.localScale);
				current = current.parent;
			}

			return result;
		}

		public static void SetLossyScaleX(Transform transform, float value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale =
				new Vector3(
					Math.Abs(lossyScale.x) <= float.Epsilon
						? 0
						: value / lossyScale.x, transform.localScale.y, transform.localScale.z);
		}

		public static void SetLossyScaleY(Transform transform, float value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale = new Vector3(transform.localScale.x,
				Math.Abs(lossyScale.y) <= float.Epsilon
					? 0
					: value / lossyScale.y, transform.localScale.z);
		}

		public static void SetLossyScaleZ(Transform transform, float value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
				Math.Abs(lossyScale.z) <= float.Epsilon
					? 0
					: value / lossyScale.z);
		}

		public static void SetLossyScale(Transform transform, Vector3 value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			var valueX = Math.Abs(lossyScale.x) <= float.Epsilon ? 0 : value.x / lossyScale.x;
			var valueY = Math.Abs(lossyScale.y) <= float.Epsilon ? 0 : value.y / lossyScale.y;
			var valueZ = Math.Abs(lossyScale.z) <= float.Epsilon ? 0 : value.z / lossyScale.z;
			transform.localScale = new Vector3(valueX, valueY, valueZ);
		}

		#endregion

		#endregion

		#region AddPositon,LocalPosition,Euler,LocalEuler,Rotation,LocalRotation,LocalScale,LossyScale

		#region position

		public static void AddPositionX(Transform transform, float value)
		{
			transform.position = new Vector3(transform.position.x + value, transform.position.y, transform.position.z);
		}

		public static void AddPositionY(Transform transform, float value)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + value, transform.position.z);
		}

		public static void AddPositionZ(Transform transform, float value)
		{
			transform.localPosition =
				new Vector3(transform.position.x, transform.position.y, transform.position.z + value);
		}

		public static void AddLocalPositionX(Transform transform, float value)
		{
			transform.localPosition = new Vector3(transform.localPosition.x + value, transform.localPosition.y,
				transform.localPosition.z);
		}

		public static void AddLocalPositionY(Transform transform, float value)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + value,
				transform.localPosition.z);
		}

		public static void AddLocalPositionZ(Transform transform, float value)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
				transform.localPosition.z + value);
		}

		#endregion

		#region eulerAngles

		public static void AddEulerAnglesX(Transform transform, float value)
		{
			transform.eulerAngles =
				new Vector3(transform.eulerAngles.x + value, transform.eulerAngles.y, transform.eulerAngles.z);
		}

		public static void AddEulerAnglesY(Transform transform, float value)
		{
			transform.eulerAngles =
				new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + value, transform.eulerAngles.z);
		}

		public static void AddEulerAnglesZ(Transform transform, float value)
		{
			transform.eulerAngles =
				new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + value);
		}

		public static void AddLocalEulerAnglesX(Transform transform, float value)
		{
			transform.localEulerAngles = new Vector3(value, transform.localEulerAngles.y,
				transform.localEulerAngles.x + transform.localEulerAngles.z);
		}

		public static void AddLocalEulerAnglesY(Transform transform, float value)
		{
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + value,
				transform.localEulerAngles.z);
		}

		public static void AddLocalEulerAnglesZ(Transform transform, float value)
		{
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y,
				transform.localEulerAngles.z + value);
		}

		#endregion

		#region Rotation

		public static void AddRotationX(Transform transform, float value)
		{
			transform.rotation = new Quaternion(transform.rotation.x + value, transform.rotation.y,
				transform.rotation.z,
				transform.rotation.w);
		}

		public static void AddRotationY(Transform transform, float value)
		{
			transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + value,
				transform.rotation.z,
				transform.rotation.w);
		}

		public static void AddRotationZ(Transform transform, float value)
		{
			transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y,
				transform.rotation.z + value,
				transform.rotation.w);
		}

		public static void AddRotationW(Transform transform, float value)
		{
			transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z,
				transform.rotation.w + value);
		}

		public static void AddLocalRotationX(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x + value, transform.localRotation.y,
				transform.localRotation.z, transform.localRotation.w);
		}

		public static void AddLocalRotationY(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y + value,
				transform.localRotation.z, transform.localRotation.w);
		}

		public static void AddLocalRotationZ(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y,
				transform.localRotation.z + value, transform.localRotation.w);
		}

		public static void AddLocalRotationW(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y,
				transform.localRotation.z, transform.localRotation.w + value);
		}

		#endregion

		#region scale

		public static void AddLocalScaleX(Transform transform, float value)
		{
			transform.localScale =
				new Vector3(transform.localScale.x + value, transform.localScale.y, transform.localScale.z);
		}

		public static void AddLocalScaleY(Transform transform, float value)
		{
			transform.localScale =
				new Vector3(transform.localScale.x, transform.localScale.y + value, transform.localScale.z);
		}

		public static void AddLocalScaleZ(Transform transform, float value)
		{
			transform.localScale =
				new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + value);
		}

		public static void AddLossyScaleX(Transform transform, float value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale =
				new Vector3(
					Math.Abs(lossyScale.x) <= float.Epsilon
						? (0 + value)
						: 1 + (value / lossyScale.x), transform.localScale.y, transform.localScale.z);
		}

		public static void AddLossyScaleY(Transform transform, float value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale = new Vector3(transform.localScale.x,
				Math.Abs(lossyScale.y) <= float.Epsilon
					? (0 + value)
					: 1 + (value / lossyScale.y), transform.localScale.z);
		}

		public static void AddLossyScaleZ(Transform transform, float value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
				Math.Abs(lossyScale.z) <= float.Epsilon
					? (0 + value)
					: 1 + (value / lossyScale.z));
		}

		#endregion

		#endregion

		public static void SetIsGray(Transform transform, bool isGray, bool isRecursive = true)
		{
			_SetIsGray(transform, isGray);
			if (!isRecursive) return;
			for (int i = 0; i < transform.childCount; i++)
				SetIsGray(transform.GetChild(i), isGray, isRecursive);
		}

		static void _SetIsGray(Transform transform, bool isGray)
		{
			transform.GetComponent<Image>()?.SetIsGray(isGray);
			transform.GetComponent<Text>()?.SetIsGray(isGray);
		}

		public static void SetAlpha(Transform transform, float alpha, bool isRecursive = true)
		{
			if (!isRecursive)
				_SetAlpha(transform, alpha);
			else
				transform.DoActionRecursive(tf => SetAlpha(tf, alpha));
		}

		static void _SetAlpha(Transform transform, float alpha)
		{
			transform.GetComponent<Image>()?.SetAlpha(alpha);
			transform.GetComponent<Text>()?.SetAlpha(alpha);
		}

		public static void SetColor(Transform transform, Color color, bool isNotUseColorAlpha = false,
			bool isRecursive = true)
		{
			if (!isRecursive)
				_SetColor(transform, color, isNotUseColorAlpha);
			else
				transform.DoActionRecursive(tf => _SetColor(tf, color, isNotUseColorAlpha));
		}

		static void _SetColor(Transform transform, Color color, bool isNotUseColorAlpha = false)
		{
			transform.GetComponent<Image>()?.SetColor(color, isNotUseColorAlpha);
			transform.GetComponent<Text>()?.SetColor(color, isNotUseColorAlpha);
		}

		public static Matrix4x4 LocalMatrix(Transform transform)
		{
			return Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
		}

		public static Matrix4x4 WorldMatrix(Transform transform)
		{
			return Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
		}

		public static void SetParentFactor(Transform transform, Transform mutiplyTransform)
		{
			Transform originSelfParentTransform = transform.parent;
			transform.SetParent(mutiplyTransform, false);
			transform.SetParent(originSelfParentTransform);
		}

		public static Transform GetPeer(Transform transform, string peerName)
		{
			if (transform.parent == null)
			{
				GameObject gameObject = GameObject.Find(peerName);
				if (gameObject != null && gameObject.transform.parent == null)
					return gameObject.transform;
			}
			else
			{
				return transform.parent.Find(peerName);
			}

			return null;
		}

		public static T GetPeer<T>(Transform transform, string peerName)
		{
			return transform.GetPeer(peerName).GetComponent<T>();
		}

		/// <summary>
		/// 设置目标物体下所有子物体的显隐状态
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="isActive"></param>
		public static void SetChildrenActive(Transform transform, bool isActive)
		{
			for (int i = 0; i < transform.childCount; ++i)
				transform.GetChild(i).gameObject.SetActive(isActive);
		}

		public static Transform GetLastChild(Transform transform)
		{
			return transform.GetChild(transform.childCount - 1);
		}

		public static Transform GetFirstChild(Transform transform)
		{
			return transform.GetChild(0);
		}

		public static void CopyFrom(Transform transform, Transform fromTransform,
			TransformMode transformMode =
				TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
		{
			fromTransform.CopyTo(transform, transformMode);
		}

		public static void CopyTo(Transform transform, Transform targetTransform,
			TransformMode transformMode =
				TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
		{
			if (transformMode.Contains(TransformMode.position))
				targetTransform.position = transform.position;
			if (transformMode.Contains(TransformMode.localPosition))
				targetTransform.localPosition = transform.localPosition;

			if (transformMode.Contains(TransformMode.rotation))
				targetTransform.rotation = transform.rotation;
			if (transformMode.Contains(TransformMode.localRotation))
				targetTransform.localRotation = transform.localRotation;

			if (transformMode.Contains(TransformMode.scale))
				targetTransform.SetLossyScale(transform.lossyScale);
			if (transformMode.Contains(TransformMode.localScale))
				targetTransform.localScale = transform.localScale;

			//有rect的，rect也一起copy
			if (transform.GetComponent<RectTransform>() != null && targetTransform.GetComponent<RectTransform>() != null)
				targetTransform.GetComponent<RectTransform>().CopyFrom(transform.GetComponent<RectTransform>());
		}

		public static Transform GetSocketTransform(Transform transform, string socketName = null)
		{
			socketName = socketName ?? StringConst.STRING_EMPTY;
			Transform socketTransform = transform.gameObject.GetOrAddCache(StringConst.STRING_SOCKET, socketName, () =>
			{
				if (socketName.IsNullOrWhiteSpace())
					return transform;
				Transform result = transform.FindChildRecursive(socketName);
				result = result ?? transform;
				return result;
			});
			return socketTransform;
		}

		public static TransformPosition ToTransformPosition(Transform transform)
		{
			return new TransformPosition(transform);
		}

		public static void DoActionRecursive(Transform transform, Action<Transform> doAction)
		{
			doAction(transform);
			var children = transform.GetChildren();
			for (var i = 0; i < children.Length; i++)
			{
				var child = children[i];
				doAction(child);
			}
		}

		public static float GetParticleSystemDuration(Transform transform, bool isRecursive = true)
		{
			return transform.gameObject.GetParticleSystemDuration(isRecursive);
		}

		#region DOTween

		#region act

		//    public static Tween DOLocalMoveXOfAct(this Transform self, float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOLocalMoveX(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.Next(); });
		//    }
		//    public static Tween DOLocalMoveYOfAct(this Transform self, float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOLocalMoveY(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.Next(); });
		//    }
		//    public static Tween DOLocalMoveZOfAct(this Transform self,float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOLocalMoveZ(endValue,duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }
		//    public static Tween DOLocalMoveOfAct(this Transform self, Vector3 endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOLocalMove(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }
		//
		//    public static Tween DOMoveXOfAct(this Transform self, float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOMoveX(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }
		//    public static Tween DOMoveYOfAct(this Transform self, float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOMoveY(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }
		//    public static Tween DOMoveZOfAct(this Transform self, float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOMoveZ(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }
		//    public static Tween DOMoveOfAct(this Transform self, Vector3 endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOMove(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }

		#endregion

		public static Tween DOWait(Transform transform, float duration)
		{
			return transform.DOBlendableLocalMoveBy(Vector3.zero, duration).SetDOTweenId();
		}

		public static Sequence DOJump(
			Transform transform,
			Vector3 endValue,
			float jumpPower,
			int jumpNum,
			float duration,
			AxisConstraint aix,
			bool snapping = false)
		{
			if (jumpNum < 1)
				jumpNum = 1;
			float startPosOfAix = 0.0f;
			float offsetAix = -1f;
			bool isOffsetAixSetted = false;
			Sequence sequence = DOTween.Sequence();

			Vector3 jumpVector3 = new Vector3(aix == AxisConstraint.X ? jumpPower : 0,
				aix == AxisConstraint.Y ? jumpPower : 0, aix == AxisConstraint.Z ? jumpPower : 0);
			Tween yTween = DOTween
				.To(() => transform.position, x => transform.position = x, jumpVector3, duration / (jumpNum * 2))
				.SetOptions(aix, snapping).SetEase(Ease.OutQuad).SetRelative().SetLoops(jumpNum * 2, LoopType.Yoyo)
				.OnStart(
					() =>
					{
						switch (aix)
						{
							case AxisConstraint.X:
								startPosOfAix = transform.position.x;
								break;
							case AxisConstraint.Y:
								startPosOfAix = transform.position.y;
								break;
							case AxisConstraint.Z:
								startPosOfAix = transform.position.z;
								break;
						}
					});


			switch (aix)
			{
				case AxisConstraint.X:
					sequence.Append(DOTween
						.To(() => transform.position, x => transform.position = x, new Vector3(0, endValue.y, 0.0f), duration)
						.SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.Linear));
					sequence.Join(DOTween
						.To(() => transform.position, x => transform.position = x, new Vector3(0.0f, 0.0f, endValue.z),
							duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear));
					break;
				case AxisConstraint.Y:
					sequence.Append(DOTween
						.To(() => transform.position, x => transform.position = x, new Vector3(endValue.x, 0.0f, 0.0f), duration)
						.SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear));
					sequence.Join(DOTween
						.To(() => transform.position, x => transform.position = x, new Vector3(0.0f, 0.0f, endValue.z),
							duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear));
					break;
				case AxisConstraint.Z:
					sequence.Append(DOTween
						.To(() => transform.position, x => transform.position = x, new Vector3(endValue.x, 0.0f, 0.0f), duration)
						.SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear));
					sequence.Join(DOTween
						.To(() => transform.position, x => transform.position = x, new Vector3(0.0f, endValue.y, 0),
							duration).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.Linear));
					break;
			}


			sequence.Join(yTween).SetTarget(transform).SetEase(DOTween.defaultEaseType);
			yTween.OnUpdate(() =>
			{
				if (!isOffsetAixSetted)
				{
					isOffsetAixSetted = true;
					switch (aix)
					{
						case AxisConstraint.X:
							offsetAix = sequence.isRelative ? endValue.x : endValue.x - startPosOfAix;
							break;
						case AxisConstraint.Y:
							offsetAix = sequence.isRelative ? endValue.y : endValue.y - startPosOfAix;
							break;
						case AxisConstraint.Z:
							offsetAix = sequence.isRelative ? endValue.z : endValue.z - startPosOfAix;
							break;
					}
				}

				float y = DOVirtual.EasedValue(0.0f, offsetAix, yTween.ElapsedPercentage(), Ease.OutQuad);
				Vector3 position = transform.position + new Vector3(aix == AxisConstraint.X ? y : 0,
									   aix == AxisConstraint.Y ? y : 0, aix == AxisConstraint.Z ? y : 0);

				transform.position = position;
			});
			return sequence;
		}

		#endregion
	}
}