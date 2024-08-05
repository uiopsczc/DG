using UnityEngine;

namespace DG
{
    public class GizmosUtil
    {
        public static GizmosColorScope Color(Color newColor)
        {
            return new GizmosColorScope(newColor);
        }
    }
}