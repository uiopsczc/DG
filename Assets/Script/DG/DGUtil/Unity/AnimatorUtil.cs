using UnityEngine;

namespace DG
{
	public class AnimatorUtil
	{
		/// <summary>
		/// 获取Animator中指定name的AnimationClip
		/// </summary>
		/// <param name="animator"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static AnimationClip GetAnimationClip(Animator animator, string name)
		{
			for (var i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
			{
				var animationClip = animator.runtimeAnimatorController.animationClips[i];
				if (animationClip.name == name)
					return animationClip;
			}

			return null;
		}


		public static string GetAnimatorStateLayerName(string stateName, string separator = StringConst.String_Dot)
		{
			return stateName.IndexOf(separator) != -1 ? stateName.Substring(0, stateName.IndexOf(separator)) : null;
		}

		public static string GetAnimatorStateLastName(string stateName, string separator = StringConst.String_Dot)
		{
			return stateName.IndexOf(separator) != -1
				? stateName.Substring(stateName.LastIndexOf(separator) + separator.Length)
				: stateName;
		}
	}
}