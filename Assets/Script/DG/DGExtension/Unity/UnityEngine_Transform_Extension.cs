using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DG
{
	public static class UnityEngine_Transform_Extension
	{
		/// <summary>
		/// GetName    赋值物体的时候，名字可能出现去掉（），空格等，去掉这些冗余得到的名字
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static string GetName(this Transform self)
		{
			return TransformUtil.GetName(self);
		}

		#region Find children

		/// <summary>
		/// 找到一个符合条件的TransformA后，不会再在该TransformA中继续查找，而是找TransformA的下一个兄弟节点
		/// </summary>
		/// <param name="self"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Component[] FindComponentsInChildren(this Transform self, Type type, string name,
			bool isRecursive = true, bool isStartWith = true)
		{
			return TransformUtil.FindComponentsInChildren(self, type, name, isRecursive, isStartWith);
		}

		public static T[] FindComponentsInChildren<T>(this Transform self, string name, bool isRecursive = true,
			bool isStartWith = true) where T : Component
		{
			return TransformUtil.FindComponentsInChildren<T>(self, name, isRecursive, isStartWith);
		}

		public static Component FindComponentInChildren(this Transform self, Type type, string name,
			bool isRecursive = true, bool isStartWith = true)
		{
			return TransformUtil.FindComponentInChildren(self, type, name, isRecursive, isStartWith);
		}

		public static T FindComponentInChildren<T>(this Transform self, string name, bool isRecursive = true,
			bool isStartWith = true) where T : Component
		{
			return TransformUtil.FindComponentInChildren<T>(self, name, isRecursive, isStartWith);
		}

		public static Component FindComponentWithTagInChildren(this Transform self, Type type, string tagName,
			bool isRecursive = true, bool isStartWith = true)
		{
			return TransformUtil.FindComponentWithTagInChildren(self, type, tagName, isRecursive, isStartWith);
		}

		public static T FindComponentWithTagInChildren<T>(this Transform self, string tagName,
			bool isRecursive = true,
			bool isStartWith = true) where T : Component
		{
			return TransformUtil.FindComponentWithTagInChildren<T>(self, tagName, isRecursive, isStartWith);
		}

		public static Component[] FindComponentsWithTagInChildren(this Transform self, Type type, string tagName,
			bool isRecursive = true, bool isStartWith = true)
		{
			return TransformUtil.FindComponentsWithTagInChildren(self, type, tagName, isRecursive, isStartWith);
		}

		public static T[] FindComponentsWithTagInChildren<T>(this Transform self, string tagName,
			bool isRecursive = true,
			bool isStartWith = true) where T : Component
		{
			return TransformUtil.FindComponentsWithTagInChildren<T>(self, tagName, isRecursive, isStartWith);
		}

		#endregion

		#region Find parent

		/// <summary>
		/// 
		/// </summary>
		/// <param name="self"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Component[] FindComponentsInParent(this Transform self, Type type, string name,
			bool isStartsWith = true)
		{
			return TransformUtil.FindComponentsInParent(self, type, name, isStartsWith);
		}

		public static T[] FindComponentsInParent<T>(this Transform self, string name, bool isStartsWith = true)
			where T : Component
		{
			return TransformUtil.FindComponentsInParent<T>(self, name, isStartsWith);
		}

		public static Component FindComponentInParent(this Transform self, Type type, string name,
			bool isStartsWith = true)
		{
			return TransformUtil.FindComponentInParent(self, type, name, isStartsWith);
		}

		public static T FindComponentInParent<T>(this Transform self, string name, bool isStartsWith = true)
			where T : Component
		{
			return TransformUtil.FindComponentInParent<T>(self, name, isStartsWith);
		}

		#endregion

		/// <summary>
		/// 获取直接子孩子节点
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Transform[] GetChildren(this Transform self)
		{
			return TransformUtil.GetChildren(self);
		}

		/// <summary>
		/// 销毁子节点
		/// </summary>
		/// <param name="self"></param>
		public static void DestroyChildren(this Transform self)
		{
			TransformUtil.DestroyChildren(self);
		}

		/// <summary>
		/// Find子Object，包括Disable的Object也会遍历获取
		/// </summary>
		public static Transform FindChildRecursive(this Transform self, string childName)
		{
			return TransformUtil.FindChildRecursive(self, childName);
		}


		/// <summary>
		/// 从根物体到当前物体的全路径, 以/分隔
		/// </summary>
		public static string GetFullPath(this Transform self, Transform rootTransform = null,
			string separator = StringConst.String_Slash)
		{
			return TransformUtil.GetFullPath(self, rootTransform, separator);
		}

		/// <summary>
		/// 递归设置layer
		/// </summary>
		public static void SetLayerRecursive(this Transform self, int layer)
		{
			TransformUtil.SetLayerRecursive(self, layer);
		}

		/// <summary>
		/// 重置
		/// </summary>
		/// <param name="self"></param>
		public static void Reset(this Transform self,
			TransformMode transformMode =
				TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
		{
			TransformUtil.Reset(self, transformMode);
		}

		public static void ResetToParent(this Transform self, GameObject parent,
			TransformMode transformMode =
				TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
		{
			TransformUtil.ResetToParent(self, parent, transformMode);
		}

		public static void ResetToParent(this Transform self, Transform parent,
			TransformMode transformMode =
				TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
		{
			TransformUtil.ResetToParent(self, parent, transformMode);
		}

		public static (bool, string) GetRelativePath(this Transform self, Transform parentTransform = null)
		{
			return TransformUtil.GetRelativePath(self, parentTransform);
		}

		#region SetPositon,LocalPosition,Euler,LocalEuler,Rotation, LocalRotation,LocalScale,LossyScale

		#region position

		public static void SetPositionX(this Transform self, float value)
		{
			TransformUtil.SetPositionX(self, value);
		}

		public static void SetPositionY(this Transform self, float value)
		{
			TransformUtil.SetPositionY(self, value);
		}

		public static void SetPositionZ(this Transform self, float value)
		{
			TransformUtil.SetPositionZ(self, value);
		}

		public static void SetLocalPositionX(this Transform self, float value)
		{
			TransformUtil.SetLocalPositionX(self, value);
		}

		public static void SetLocalPositionY(this Transform self, float value)
		{
			TransformUtil.SetLocalPositionY(self, value);
		}

		public static void SetLocalPositionZ(this Transform self, float value)
		{
			TransformUtil.SetLocalPositionZ(self, value);
		}

		#endregion

		#region eulerAngles

		public static void SetEulerAnglesX(this Transform self, float value)
		{
			TransformUtil.SetEulerAnglesX(self, value);
		}

		public static void SetEulerAnglesY(this Transform self, float value)
		{
			TransformUtil.SetEulerAnglesY(self, value);
		}

		public static void SetEulerAnglesZ(this Transform self, float value)
		{
			TransformUtil.SetEulerAnglesZ(self, value);
		}

		public static void SetLocalEulerAnglesX(this Transform self, float value)
		{
			TransformUtil.SetLocalEulerAnglesX(self, value);
		}

		public static void SetLocalEulerAnglesY(this Transform self, float value)
		{
			TransformUtil.SetLocalEulerAnglesY(self, value);
		}

		public static void SetLocalEulerAnglesZ(this Transform self, float value)
		{
			TransformUtil.SetLocalEulerAnglesZ(self, value);
		}

		#endregion

		#region Rotation

		public static void SetRotationX(this Transform self, float value)
		{
			TransformUtil.SetRotationX(self, value);
		}

		public static void SetRotationY(this Transform self, float value)
		{
			TransformUtil.SetRotationY(self, value);
		}

		public static void SetRotationZ(this Transform self, float value)
		{
			TransformUtil.SetRotationZ(self, value);
		}

		public static void SetRotationW(this Transform self, float value)
		{
			TransformUtil.SetRotationW(self, value);
		}

		public static void SetLocalRotationX(this Transform self, float value)
		{
			TransformUtil.SetLocalRotationX(self, value);
		}

		public static void SetLocalRotationY(this Transform self, float value)
		{
			TransformUtil.SetLocalRotationY(self, value);
		}

		public static void SetLocalRotationZ(this Transform self, float value)
		{
			TransformUtil.SetLocalRotationZ(self, value);
		}

		public static void SetLocalRotationW(this Transform self, float value)
		{
			TransformUtil.SetLocalRotationW(self, value);
		}

		#endregion

		#region scale

		public static void SetLocalScaleX(this Transform self, float value)
		{
			TransformUtil.SetLocalScaleX(self, value);
		}

		public static void SetLocalScaleY(this Transform self, float value)
		{
			TransformUtil.SetLocalScaleY(self, value);
		}

		public static void SetLocalScaleZ(this Transform self, float value)
		{
			TransformUtil.SetLocalScaleZ(self, value);
		}

		public static Vector3 GetLossyScaleOfParent(this Transform self)
		{
			return TransformUtil.GetLossyScaleOfParent(self);
		}

		public static void SetLossyScaleX(this Transform self, float value)
		{
			TransformUtil.SetLossyScaleX(self, value);
		}

		public static void SetLossyScaleY(this Transform self, float value)
		{
			TransformUtil.SetLossyScaleY(self, value);
		}

		public static void SetLossyScaleZ(this Transform self, float value)
		{
			TransformUtil.SetLossyScaleZ(self, value);
		}

		public static void SetLossyScale(this Transform self, Vector3 value)
		{
			TransformUtil.SetLossyScale(self, value);
		}

		#endregion

		#endregion

		#region AddPositon,LocalPosition,Euler,LocalEuler,Rotation,LocalRotation,LocalScale,LossyScale

		#region position

		public static void AddPositionX(this Transform self, float value)
		{
			TransformUtil.AddPositionX(self, value);
		}

		public static void AddPositionY(this Transform self, float value)
		{
			TransformUtil.AddPositionY(self, value);
		}

		public static void AddPositionZ(this Transform self, float value)
		{
			TransformUtil.AddPositionZ(self, value);
		}

		public static void AddLocalPositionX(this Transform self, float value)
		{
			TransformUtil.AddLocalPositionX(self, value);
		}

		public static void AddLocalPositionY(this Transform self, float value)
		{
			TransformUtil.AddLocalPositionY(self, value);
		}

		public static void AddLocalPositionZ(this Transform self, float value)
		{
			TransformUtil.AddLocalPositionZ(self, value);
		}

		#endregion

		#region eulerAngles

		public static void AddEulerAnglesX(this Transform self, float value)
		{
			TransformUtil.AddEulerAnglesX(self, value);
		}

		public static void AddEulerAnglesY(this Transform self, float value)
		{
			TransformUtil.AddEulerAnglesY(self, value);
		}

		public static void AddEulerAnglesZ(this Transform self, float value)
		{
			TransformUtil.AddEulerAnglesZ(self, value);
		}

		public static void AddLocalEulerAnglesX(this Transform self, float value)
		{
			TransformUtil.AddLocalEulerAnglesX(self, value);
		}

		public static void AddLocalEulerAnglesY(this Transform self, float value)
		{
			TransformUtil.AddLocalEulerAnglesY(self, value);
		}

		public static void AddLocalEulerAnglesZ(this Transform self, float value)
		{
			TransformUtil.AddLocalEulerAnglesZ(self, value);
		}

		#endregion

		#region Rotation

		public static void AddRotationX(this Transform self, float value)
		{
			TransformUtil.AddRotationX(self, value);
		}

		public static void AddRotationY(this Transform self, float value)
		{
			TransformUtil.AddRotationY(self, value);
		}

		public static void AddRotationZ(this Transform self, float value)
		{
			TransformUtil.AddRotationZ(self, value);
		}

		public static void AddRotationW(this Transform self, float value)
		{
			TransformUtil.AddRotationW(self, value);
		}

		public static void AddLocalRotationX(this Transform self, float value)
		{
			TransformUtil.AddLocalRotationX(self, value);
		}

		public static void AddLocalRotationY(this Transform self, float value)
		{
			TransformUtil.AddLocalRotationY(self, value);
		}

		public static void AddLocalRotationZ(this Transform self, float value)
		{
			TransformUtil.AddLocalRotationZ(self, value);
		}

		public static void AddLocalRotationW(this Transform self, float value)
		{
			TransformUtil.AddLocalRotationW(self, value);
		}

		#endregion

		#region scale

		public static void AddLocalScaleX(this Transform self, float value)
		{
			TransformUtil.AddLocalScaleX(self, value);
		}

		public static void AddLocalScaleY(this Transform self, float value)
		{
			TransformUtil.AddLocalScaleY(self, value);
		}

		public static void AddLocalScaleZ(this Transform self, float value)
		{
			TransformUtil.AddLocalScaleZ(self, value);
		}

		public static void AddLossyScaleX(this Transform self, float value)
		{
			TransformUtil.AddLossyScaleX(self, value);
		}

		public static void AddLossyScaleY(this Transform self, float value)
		{
			TransformUtil.AddLossyScaleY(self, value);
		}

		public static void AddLossyScaleZ(this Transform transform, float value)
		{
			TransformUtil.AddLossyScaleZ(transform, value);
		}

		#endregion

		#endregion

		public static void SetIsGray(this Transform self, bool isGray, bool isRecursive = true)
		{
			TransformUtil.SetIsGray(self, isGray, isRecursive);
		}

		public static void SetAlpha(this Transform self, float alpha, bool isRecursive = true)
		{
			TransformUtil.SetAlpha(self, alpha, isRecursive);
		}

		public static void SetColor(this Transform self, Color color, bool isNotUseColorAlpha = false,
			bool isRecursive = true)
		{
			TransformUtil.SetColor(self, color, isNotUseColorAlpha, isRecursive);
		}

		public static Matrix4x4 LocalMatrix(this Transform self)
		{
			return TransformUtil.LocalMatrix(self);
		}

		public static Matrix4x4 WorldMatrix(this Transform self)
		{
			return TransformUtil.WorldMatrix(self);
		}

		public static void SetParentFactor(this Transform self, Transform mutiplyTransform)
		{
			TransformUtil.SetParentFactor(self, mutiplyTransform);
		}

		public static Transform GetPeer(this Transform self, string peerName)
		{
			return TransformUtil.GetPeer(self, peerName);
		}

		public static T GetPeer<T>(Transform self, string peerName)
		{
			return TransformUtil.GetPeer<T>(self, peerName);
		}

		/// <summary>
		/// 设置目标物体下所有子物体的显隐状态
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="isActive"></param>
		public static void SetChildrenActive(this Transform self, bool isActive)
		{
			TransformUtil.SetChildrenActive(self, isActive);
		}

		public static Transform GetLastChild(this Transform self)
		{
			return TransformUtil.GetLastChild(self);
		}

		public static Transform GetFirstChild(this Transform self)
		{
			return TransformUtil.GetFirstChild(self);
		}

		public static void CopyFrom(this Transform self, Transform fromTransform,
			TransformMode transformMode =
				TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
		{
			TransformUtil.CopyFrom(self, fromTransform, transformMode);
		}

		public static void CopyTo(this Transform self, Transform targetTransform,
			TransformMode transformMode =
				TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
		{
			TransformUtil.CopyTo(self, targetTransform, transformMode);
		}

		public static Transform GetSocketTransform(this Transform self, string socketName = null)
		{
			return TransformUtil.GetSocketTransform(self, socketName);
		}

		public static TransformPosition ToTransformPosition(this Transform self)
		{
			return TransformUtil.ToTransformPosition(self);
		}

		public static void DoActionRecursive(this Transform self, Action<Transform> doAction)
		{
			TransformUtil.DoActionRecursive(self, doAction);
		}

		public static float GetParticleSystemDuration(this Transform self, bool isRecursive = true)
		{
			return TransformUtil.GetParticleSystemDuration(self, isRecursive);
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

		public static Tween DOWait(this Transform self, float duration)
		{
			return TransformUtil.DOWait(self, duration);
		}

		public static Sequence DOJump(
			this Transform self,
			Vector3 endValue,
			float jumpPower,
			int jumpNum,
			float duration,
			AxisConstraint aix,
			bool snapping = false)
		{
			return TransformUtil.DOJump(self, endValue, jumpPower, jumpNum, duration, aix, snapping);
		}

		#endregion
	}
}