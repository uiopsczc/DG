using System;

namespace DG
{
	public static class ArrayUtil
	{
		public static Array AddFirst(Array sourceArray, params object[] toAdds)
		{
			var elementType = sourceArray != null
				? sourceArray.GetType().GetElementType()
				: toAdds.GetType().GetElementType();
			var sourceArrayLength = sourceArray?.Length ?? 0;
			var targetArray = Array.CreateInstance(elementType, sourceArrayLength + 1);
			if (sourceArray != null && sourceArray.Length > 0)
				Array.Copy(sourceArray, 0, targetArray, toAdds.Length, sourceArrayLength);
			Array.Copy(toAdds, 0, targetArray, 0, toAdds.Length);
			return targetArray;
		}


		public static Array AddLast(Array sourceArray, params object[] toAdds)
		{
			var elementType = sourceArray != null
				? sourceArray.GetType().GetElementType()
				: toAdds.GetType().GetElementType();
			var sourceArrayLength = sourceArray?.Length ?? 0;
			var array = Array.CreateInstance(elementType, sourceArrayLength + 1);
			if (sourceArray != null && sourceArray.Length > 0)
				Array.Copy(sourceArray, array, sourceArrayLength);
			Array.Copy(toAdds, 0, array, sourceArrayLength, toAdds.Length);
			return array;
		}


		public static Array Remove(Array sourceArray, object o)
		{
			var elementType = sourceArray != null ? sourceArray.GetType().GetElementType() : o.GetType();
			var sourceArrayLength = sourceArray?.Length ?? 0;
			if (sourceArrayLength == 0)
				return sourceArray;
			int toRemoveIndex = -1;
			for (int i = 0; i < sourceArrayLength; i++)
			{
				if (!ObjectUtil.Equals(sourceArray.GetValue(i), o)) continue;
				toRemoveIndex = i;
				break;
			}

			if (toRemoveIndex == -1)
				return sourceArray;


			var array = Array.CreateInstance(elementType, sourceArrayLength - 1);
			if (toRemoveIndex != 0)
				Array.Copy(sourceArray, 0, array, 0, toRemoveIndex);
			if (toRemoveIndex != sourceArrayLength - 1)
				Array.Copy(sourceArray, toRemoveIndex + 1, array, toRemoveIndex, sourceArrayLength - toRemoveIndex - 1);
			return array;
		}


		public static Array RemoveAt(Array sourceArray, int index)
		{
			var elementType = sourceArray.GetType().GetElementType();
			var sourceArrayLength = sourceArray.Length;
			if (sourceArrayLength == 0)
				return sourceArray;
			int toRemoveIndex = index;
			if (toRemoveIndex < 0 || toRemoveIndex >= sourceArrayLength)
			{
				DGLog.Error("index out of boundary");
				return sourceArray;
			}

			var array = Array.CreateInstance(elementType, sourceArrayLength - 1);
			if (toRemoveIndex != 0)
				Array.Copy(sourceArray, 0, array, 0, toRemoveIndex);
			if (toRemoveIndex != sourceArrayLength - 1)
				Array.Copy(sourceArray, toRemoveIndex + 1, array, toRemoveIndex,
					sourceArrayLength - toRemoveIndex - 1);
			return array;
		}

		public static Array Resize_Array(Array array, int length)
		{
			Type elementType = array.GetType().GetElementType();
			Array newArray = Array.CreateInstance(elementType, length);
			Array.Copy(array, 0, newArray, 0, Math.Min(array.Length, length));
			return newArray;
		}

		public static Array Insert_Array(Array array, int index, object value)
		{
			int newArrayLength = index < array.Length ? array.Length + 1 : index + 1;

			Type elementType = array.GetType().GetElementType();
			Array newArray = Array.CreateInstance(elementType, newArrayLength);
			Array.Copy(array, 0, newArray, 0, Math.Min(newArrayLength, array.Length));
			newArray.SetValue(value, index);
			if (index < array.Length)
				Array.Copy(array, index, newArray, index + 1, array.Length - index);
			return newArray;
		}

		public static Array RemoveAt_Array(Array array, int index)
		{
			Type elementType = array.GetType().GetElementType();
			Array newArray = Array.CreateInstance(elementType, array.Length - 1);
			Array.Copy(array, 0, newArray, 0, index);
			Array.Copy(array, index + 1, newArray, index, array.Length - index - 1);
			return newArray;
		}
		
	}
}

