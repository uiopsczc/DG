#if UNITY_EDITOR
using UnityEngine;

namespace DG
{
    public partial class HandlesUtil
    {
        public static HandlesColorScope Color(Color newColor)
        {
            return new HandlesColorScope(newColor);
        }
    }
}
#endif