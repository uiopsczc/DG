#if UNITY_EDITOR
using UnityEngine;

namespace DG
{
    public class HandleUtil
    {
        public static SetDefaultControlScope SetDefaultControl(FocusType focusType)
        {
            return new SetDefaultControlScope(focusType);
        }
    }
}
#endif