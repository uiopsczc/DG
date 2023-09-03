using System;
using System.Text;

namespace DG
{
	public static class ObjectUtil
	{
		/// <summary>
		/// o1�Ƿ��o2���
		/// </summary>
		public new static bool Equals(object o1, object o2)
		{
			return o1?.Equals(o2) ?? o2 == null;
		}

		public static bool Equals<T>(T o1, T o2)
		{
			return o1?.Equals(o2) ?? o2 == null;
		}

		/// <summary>
		/// o1��o2�Ƚϴ�С
		/// </summary>
		public static int Compares(object o1, object o2)
		{
			if (o1 == o2)
				return 0;
			if (o1 != null && o2 == null)
				return 1;
			switch (o1)
			{
				case null when o2 != null:
					return -1;
				case IComparable comparable:
					return comparable.CompareTo(o2);
			}

			return o2 is IComparable comparable1 ? comparable1.CompareTo(o1) : o1.ToString().CompareTo(o2.ToString());
		}


		public static int GetHashCode(params object[] objs)
		{
			int result = int.MinValue;
			bool isFoundFirstNotNullObject = false;
			for (var i = 0; i < objs.Length; i++)
			{
				var obj = objs[i];
				if (obj == null) continue;
				if (isFoundFirstNotNullObject)
					result ^= obj.GetHashCode();
				else
				{
					result = obj.GetHashCode();
					isFoundFirstNotNullObject = true;
				}
			}

			return result;
		}

		public static int GetHashCode<T>(params T[] objs)
		{
			int result = int.MinValue;
			bool isFoundFirstNotNullObject = false;
			for (var i = 0; i < objs.Length; i++)
			{
				var obj = objs[i];
				if (obj == null) continue;
				if (isFoundFirstNotNullObject)
					result ^= obj.GetHashCode();
				else
				{
					result = obj.GetHashCode();
					isFoundFirstNotNullObject = true;
				}
			}

			return result;
		}

		/// <summary>
		/// ��������object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static void Swap<T>(ref T a, ref T b)
		{
			T c = b;
			b = a;
			a = c;
		}

		public static string ToString(params object[] objs)
		{
			var stringBuilder = new StringBuilder();
			for (int i = 0; i < objs.Length; i++)
			{
				var obj = objs[i];
				if (i == objs.Length - 1)
					stringBuilder.Append(obj);
				else
					stringBuilder.Append(obj + StringConst.String_Space);
			}

			return stringBuilder.ToString();
		}

		public static string DGToString(params object[] objs)
		{
			var stringBuilder = new StringBuilder();
			for (int i = 0; i < objs.Length; i++)
			{
				var obj = objs[i];
				if (i == objs.Length - 1)
					stringBuilder.Append(obj.DGToString());
				else
					stringBuilder.Append(obj.DGToString() + StringConst.String_Space);
			}

			return stringBuilder.ToString();
		}
	}
}