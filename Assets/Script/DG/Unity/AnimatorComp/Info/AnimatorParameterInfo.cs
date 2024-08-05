using System;
using UnityEngine;

namespace DG
{
    public class AnimatorParameterInfo
    {
        public Animator animator;
        public AnimatorControllerParameter animatorControllerParameter;
        public string name;
        public object value;
        public AnimatorControllerParameterType animatorControllerParameterType;

        public AnimatorParameterInfo(Animator animator, AnimatorControllerParameter animatorControllerParameter)
        {
            this.animator = animator;
            this.animatorControllerParameter = animatorControllerParameter;
            name = this.animatorControllerParameter.name;
            value = GetValue();
            animatorControllerParameterType = animatorControllerParameter.type;
        }

        public object GetValue()
        {
            switch (animatorControllerParameterType)
            {
                case AnimatorControllerParameterType.Bool:
                    return animator.GetBool(name);
                case AnimatorControllerParameterType.Float:
                    return animator.GetFloat(name);
                case AnimatorControllerParameterType.Int:
                    return animator.GetInteger(name);
                case AnimatorControllerParameterType.Trigger:
                    return null;
                default:
                    throw new Exception("no animatorControllerParameterType");
            }
        }

        public void SetValue(object value = null)
        {
            switch (animatorControllerParameterType)
            {
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(name, value.To<bool>());
                    break;
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(name, value.To<float>());
                    break;
                case AnimatorControllerParameterType.Int:
                    animator.SetInteger(name, value.To<int>());
                    break;
                case AnimatorControllerParameterType.Trigger:
                    animator.SetTrigger(name);
                    break;
                default:
                    throw new Exception("no animatorControllerParameterType");
            }

            animator.Update(0);
        }
    }
}