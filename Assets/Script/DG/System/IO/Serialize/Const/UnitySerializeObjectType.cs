using System;
using UnityEngine;

namespace DG
{
    public static class UnitySerializeObjectType
    {
        public static Type Vector2Type = typeof(Vector2);
        public static Type Vector3Type = typeof(Vector3);
        public static Type Vector4Type = typeof(Vector4);
        public static Type QuaternionType = typeof(Quaternion);
        public static Type BoundsType = typeof(Bounds);
        public static Type ColorType = typeof(Color);
        public static Type RectType = typeof(Rect);


        public static bool IsSerializeType(Type type)
        {
            return type == Vector2Type || type == Vector3Type ||
                   type == Vector4Type || type == QuaternionType ||
                   type == BoundsType || type == ColorType ||
                   type == RectType;
        }
    }
}