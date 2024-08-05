using UnityEngine;

namespace DG
{
    public static class UnityEngine_Quaternion_Extension
    {
        public static string DGToString(this Quaternion self)
        {
            return QuaternionUtil.DGToString(self);
        }

        public static System.Numerics.Quaternion To_System_Numerics_Quaternion(this Quaternion self)
        {
            return QuaternionUtil.To_System_Numerics_Quaternion(self);
        }


        public static bool IsDefault(this Quaternion self, bool isMin = false)
        {
            return QuaternionUtil.IsDefault(self, isMin);
        }

        public static Quaternion Inverse(this Quaternion self)
        {
            return QuaternionUtil.Inverse(self);
        }

        public static Vector3 Forward(this Quaternion self)
        {
            return QuaternionUtil.Forward(self);
        }

        public static Vector3 Back(this Quaternion self)
        {
            return QuaternionUtil.Back(self);
        }

        public static Vector3 Up(this Quaternion self)
        {
            return QuaternionUtil.Up(self);
        }

        public static Vector3 Down(this Quaternion self)
        {
            return QuaternionUtil.Down(self);
        }

        public static Vector3 Left(this Quaternion self)
        {
            return QuaternionUtil.Left(self);
        }

        public static Vector3 Right(this Quaternion self)
        {
            return QuaternionUtil.Right(self);
        }

        public static bool IsZero(this Quaternion self)
        {
            return QuaternionUtil.IsZero(self);
        }

        public static Quaternion GetNotZero(this Quaternion self, Quaternion? defaultValue = null)
        {
            return QuaternionUtil.GetNotZero(self, defaultValue);
        }
    }
}