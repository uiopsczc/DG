using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
	public static class UnityEngine_Vector3_Extension
	{
		public static string DGToString(this Vector3 v)
		{
			return Vector3Util.DGToString(v);
		}

		public static System.Numerics.Vector3 To_System_Numerics_Vector3(this Vector3 v)
		{
			return Vector3Util.To_System_Numerics_Vector3(v);
		}

		#region ZeroX/Y/Z;

		/// <summary>
		/// X的值为0
		/// </summary>
		/// <param name="vector3"></param>
		/// <returns></returns>
		public static Vector3 SetZeroX(this Vector3 self)
		{
			return Vector3Util.SetZeroX(self);
		}

		/// <summary>
		/// y的值为0
		/// </summary>
		/// <param name="vector3"></param>
		/// <returns></returns>
		public static Vector3 SetZeroY(this Vector3 self)
		{
			return Vector3Util.SetZeroY(self);
		}

		/// <summary>
		/// Z的值为0
		/// </summary>
		/// <param name="vector3"></param>
		/// <returns></returns>
		public static Vector3 SetZeroZ(this Vector3 self)
		{
			return Vector3Util.SetZeroZ(self);
		}

		#endregion

		public static Vector2 XY(this Vector3 self)
		{
			return Vector3Util.XY(self);
		}


		public static Vector2 YZ(this Vector3 self)
		{
			return Vector3Util.YZ(self);
		}


		public static Vector2 XZ(this Vector3 self)
		{
			return Vector3Util.XZ(self);
		}

		#region 各种To ToXXX

		public static Vector2 ToVector2(this Vector3 self, string format = StringConst.String_x_y)
		{
			return Vector3Util.ToVector2(self, format);
		}

		/// <summary>
		/// Vector3.ToString只保留小数后2位，看起来会卡，所以需要ToStringDetail
		/// </summary>
		public static string ToStringDetail(this Vector3 self, string separator = StringConst.String_Comma)
		{
			return Vector3Util.ToStringDetail(self, separator);
		}

		/// <summary>
		/// 将逗号改成对应的separator
		/// </summary>
		public static string ToStringReplaceSeparator(this Vector3 self, string separator = StringConst.String_Comma)
		{
			return Vector3Util.ToStringReplaceSeparator(self, separator);
		}

		public static Dictionary<string, float> ToDictionary(this Vector3 self) //
		{
			return Vector3Util.ToDictionary(self);
		}

		#endregion

		/// <summary>
		/// v1乘v2
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>
		public static Vector3 Multiply(this Vector3 self, Vector3 v2)
		{
			return Vector3Util.Multiply(self, v2);
		}


		#region Other

		public static float GetFormat(Vector3 self, string format)
		{
			return Vector3Util.GetFormat(self, format);
		}

		#endregion

		public static Vector3 Average(this Vector3[] selfs)
		{
			return Vector3Util.Average(selfs);
		}


		public static Vector3 SetX(this Vector3 self, float args)
		{
			return Vector3Util.SetX(self, args);
		}

		public static Vector3 SetY(this Vector3 self, float args)
		{
			return Vector3Util.SetY(self, args);
		}

		public static Vector3 SetZ(this Vector3 self, float args)
		{
			return Vector3Util.SetZ(self, args);
		}

		public static Vector3 AddX(this Vector3 self, float args)
		{
			return Vector3Util.AddX(self, args);
		}

		public static Vector3 AddY(this Vector3 self, float args)
		{
			return Vector3Util.AddY(self, args);
		}

		public static Vector3 AddZ(this Vector3 self, float args)
		{
			return Vector3Util.AddZ(self, args);
		}


		public static Vector3 Set(this Vector3 self, string format, params float[] args)
		{
			return Vector3Util.Set(self, format, args);
		}

		public static Vector3 Abs(this Vector3 self)
		{
			return Vector3Util.Abs(self);
		}


		public static bool IsDefault(this Vector3 self, bool isMin = false)
		{
			return Vector3Util.IsDefault(self, isMin);
		}


		public static Vector3 Clamp(this Vector3 self, Vector3 minPosition, Vector3 maxPosition)
		{
			return Vector3Util.Clamp(self, minPosition, maxPosition);
		}

		public static Vector3Position ToVector3Position(this Vector3 self)
		{
			return Vector3Util.ToVector3Position(self);
		}

		//将v Round四舍五入snap_size的倍数的值
		//Rounds value to the closest multiple of snap_size.
		public static Vector3 Snap(this Vector3 self, Vector3 snapSize)
		{
			return Vector3Util.Snap(self, snapSize);
		}

		public static Vector3 Snap2(this Vector3 self, Vector3 snapSize)
		{
			return Vector3Util.Snap2(self, snapSize);
		}

		public static Vector3 ConvertElement(this Vector3 self, Func<float, float> convertElementFunc)
		{
			return Vector3Util.ConvertElement(self, convertElementFunc);
		}


		public static Vector3Int ToVector3Int(this Vector3 self)
		{
			return Vector3Util.ToVector3Int(self);
		}


		public static string ToStringOrDefault(this Vector3 self, string toDefaultString = null,
			Vector3 defaultValue = default)
		{
			return Vector3Util.ToStringOrDefault(self, toDefaultString, defaultValue);
		}

		public static bool IsZero(this Vector3 self)
		{
			return Vector3Util.IsZero(self);
		}

		public static bool IsOne(this Vector3 self)
		{
			return Vector3Util.IsOne(self);
		}
	}
}