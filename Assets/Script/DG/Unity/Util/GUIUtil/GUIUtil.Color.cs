using UnityEngine;

namespace DG
{
    public partial class GUIUtil
    {
        public static GUIColorScope Color(Color newColor)
        {
            return new GUIColorScope(newColor);
        }
    }
}