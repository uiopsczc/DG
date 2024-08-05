using UnityEngine;

namespace DG
{
    public partial class GUIUtil
    {
        public static GUISkinScope Skin(GUISkin skin)
        {
            return new GUISkinScope(skin);
        }
    }
}