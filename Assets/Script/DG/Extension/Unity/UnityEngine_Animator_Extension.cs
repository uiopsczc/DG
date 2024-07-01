using UnityEngine;

namespace DG
{
	public static class UnityEngine_Animator_Extension
	{
		/// <summary>
		/// 获取Animator中指定name的AnimationClip
		/// </summary>
		/// <param name="self"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static AnimationClip GetAnimationClip(this Animator self, string name)
		{
			return AnimatorUtil.GetAnimationClip(self, name);
		}

		
	}
}


