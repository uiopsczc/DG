using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    public static partial class GUIToolbarConst
    {
        public static Color Active_Color = new(1f, 1f, 0f, 0.8f);
        public static Color Disable_Color = new(0.5f, 0.5f, 0.5f, 0.4f);
        public static Color Highlith_Color = new(1f, 0f, 0f, 0.8f);

        public static Dictionary<int, Texture2D> Texture_Dict = new();
    }
}