using System;
using UnityEngine;

namespace DG
{
	public class Vector2IntUtil
	{
		public static bool IsDefault(Vector2Int v, bool isMin = false)
		{
			return isMin ? v == Vector2IntConst.DEFAULT_MIN : v == Vector2IntConst.DEFAULT_MAX;
		}

		public static string ToStringOrDefault(Vector2Int v, string toDefaultString = null,
			Vector2Int defaultValue = default)
		{
			return ObjectUtil.Equals(v, defaultValue) ? toDefaultString : v.ToString();
		}

		public static Vector2Int Abs(Vector2Int v)
		{
			return new Vector2Int(Math.Abs(v.x), Math.Abs(v.y));
		}


		public static bool IsZero(Vector2Int v)
		{
			return v.Equals(Vector2Int.zero);
		}


		public static bool IsOne(Vector2Int v)
		{
			return v.Equals(Vector2Int.one);
		}
	}
}