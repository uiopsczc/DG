using UnityEngine;

namespace DG
{
    public class AnimatorStateInfoUtil
    {
        public static bool IsName(AnimatorStateInfo animatorStateInfo, string prefix, params string[] suffixes)
        {
            if (animatorStateInfo.IsName(prefix))
                return true;
            for (var i = 0; i < suffixes.Length; i++)
            {
                var suffix = suffixes[i];
                var name = prefix + suffix;
                if (animatorStateInfo.IsName(name))
                    return true;
            }

            return false;
        }
    }
}