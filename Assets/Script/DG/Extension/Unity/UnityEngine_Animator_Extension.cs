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

        public static T GetBehaviour<T>(this Animator self, string name) where T : StateMachineBehaviour
        {
            return AnimatorUtil.GetBehaviour<T>(self, name);
        }


        /// <summary>
        /// 设置暂停
        /// </summary>
        /// <param name="self"></param>
        /// <param name="cause"></param>
        public static void SetPause(this Animator self, object cause)
        {
            PauseUtil.SetPause(self, cause);
        }

        public static float SetTriggerExt(this Animator self, string triggerName)
        {
            return AnimatorUtil.SetTriggerExt(self, triggerName);
        }
    }
}