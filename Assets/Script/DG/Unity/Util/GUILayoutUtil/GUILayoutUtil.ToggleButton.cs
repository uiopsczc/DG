using UnityEngine;

namespace DG
{
    public partial class GUILayoutUtil
    {
        public static bool ToggleButton(string label, bool value)
        {
            GUIStyle buttonStyle = StringConst.STRING_BUTTON;
            if (GUILayout.Button(label,
                    value
                        ? new GUIStyle(StringConst.STRING_BUTTON)
                            { normal = { background = buttonStyle.active.background } }
                        : StringConst.STRING_BUTTON))
                value = !value;
            return value;
        }
    }
}