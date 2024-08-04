using System.Collections.Generic;
using UnityEngine;

namespace DG
{
	public class AnimatorComp
	{
		public string curAnimationName;

		public Dictionary<Animator, Dictionary<string, AnimatorParameterInfo>> animatorsParameterInfoDict = new();

		public void Destroy()
		{
			animatorsParameterInfoDict.Clear();
			curAnimationName = null;
		}


		public void OnBuild()
		{
		}

		public void OnBuildOk(GameObject gameObject)
		{
			var animators = gameObject.GetComponentsInChildren<Animator>();
			foreach (var animator in animators)
				SaveAnimator(animator);
		}

		public void SaveAnimator(Animator animator)
		{
			var parameters = animator.parameters;
			var animatorParameterInfoDict = new Dictionary<string, AnimatorParameterInfo>();
			for (var i = 0; i < parameters.Length; i++)
			{
				var parameter = parameters[i];
				animatorParameterInfoDict[parameter.name] = new AnimatorParameterInfo(animator, parameter);
			}

			animatorsParameterInfoDict[animator] = animatorParameterInfoDict;
		}

		public void PlayAnimation(string animationName, object parameterValue = null, float speed = 1)
		{
			if (AnimationNameConst.DIE.Equals(curAnimationName))
				return;
			bool isChanged = false;
			foreach (var keyValue in animatorsParameterInfoDict)
			{
				var animator = keyValue.Key;
				var animatorParameterInfoDict = animatorsParameterInfoDict[animator];
				//停掉上一个动画
				if (!curAnimationName.IsNullOrWhiteSpace()
				    && animatorParameterInfoDict.ContainsKey(curAnimationName)
				    && animatorParameterInfoDict[curAnimationName].animatorControllerParameterType ==
				    AnimatorControllerParameterType.Bool)
					animatorParameterInfoDict[curAnimationName].SetValue(false);
				//设置更改的动画
				if (animatorParameterInfoDict.ContainsKey(animationName))
				{
					animator.speed = speed;
					animatorParameterInfoDict[animationName].SetValue(parameterValue);
					isChanged = true;
				}

				if (isChanged)
					curAnimationName = animationName;
			}
		}
	}
}