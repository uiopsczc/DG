using UnityEngine;

namespace DG
{
    public partial class GUIUtil
    {
        public static GUILabelAlignScope LabelAlign(TextAnchor textAnchor)
        {
            return new GUILabelAlignScope(textAnchor);
        }
    }
}