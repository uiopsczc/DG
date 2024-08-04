using System;

namespace DG
{
	public static class System_Array_Extension
	{
		public static Array AddFirst(this Array sourceArray, params object[] toAdds)
		{
			return ArrayUtil.AddFirst(sourceArray, toAdds);
		}


		public static Array AddLast(this Array sourceArray, params object[] toAdds)
		{
			return ArrayUtil.AddLast(sourceArray, toAdds);
		}


		public static Array Remove(this Array sourceArray, object o)
		{
			return ArrayUtil.Remove(sourceArray, o);
		}


		public static Array RemoveAt(this Array sourceArray, int index)
		{
			return ArrayUtil.RemoveAt(sourceArray, index);
		}

		public static Array Resize_Array(this Array self, int length)
		{
			return ArrayUtil.Resize_Array(self, length);
		}

		public static Array Insert_Array(this Array self, int index, object value)
		{
			return ArrayUtil.Insert_Array(self, index, value);
		}

		public static Array RemoveAt_Array(this Array self, int index)
		{
			return ArrayUtil.RemoveAt_Array(self, index);
		}
	}
}