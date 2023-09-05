using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DG
{
	public static class ICollectionUtil
	{
		public static bool IsNullOrEmpty(ICollection collection)
		{
			return collection == null || collection.Count == 0;
		}

		public static T[] ToArray<T>(ICollection collection)
		{
			T[] result = new T[collection.Count];
			int curIndex = -1;
			var iterator = collection.GetEnumerator();
			while (iterator.MoveNext(ref curIndex))
				result[curIndex] = (T)iterator.Current;
			return result;
		}

		public static List<T> ToList<T>(ICollection collection)
		{
			List<T> result = new List<T>(collection.Count);
			int curIndex = -1;
			var iterator = collection.GetEnumerator();
			while (iterator.MoveNext(ref curIndex))
				result.Add((T)iterator.Current);
			return result;
		}


		#region DGToString

		public static string DGToString(ICollection collection, bool isFillStringWithDoubleQuote = false)
		{
			bool isFirst = true;
			var stringBuilder = new StringBuilder();
			switch (collection)
			{
				case Array _:
					stringBuilder.Append(StringConst.String_LeftRoundBrackets);
					break;
				case IList _:
					stringBuilder.Append(StringConst.String_LeftSquareBrackets);
					break;
				case IDictionary _:
					stringBuilder.Append(StringConst.String_LeftCurlyBrackets);
					break;
			}

			if (collection is IDictionary dictionary)
			{
				foreach (DictionaryEntry dictionaryEntry in dictionary)
				{
					var key = dictionaryEntry.Key;
					var value = dictionaryEntry.Value;
					if (isFirst)
						isFirst = false;
					else
						stringBuilder.Append(StringConst.String_Comma);
					stringBuilder.Append(key.DGToString(isFillStringWithDoubleQuote));
					stringBuilder.Append(StringConst.String_Colon);
					stringBuilder.Append(value.DGToString(isFillStringWithDoubleQuote));
				}
			}
			else //list
			{
				foreach (var o in collection)
				{
					if (isFirst)
						isFirst = false;
					else
						stringBuilder.Append(StringConst.String_Comma);
					stringBuilder.Append(o.DGToString(isFillStringWithDoubleQuote));
				}
			}

			switch (collection)
			{
				case Array _:
					stringBuilder.Append(StringConst.String_RightRoundBrackets);
					break;
				case IList _:
					stringBuilder.Append(StringConst.String_RightSquareBrackets);
					break;
				case IDictionary _:
					stringBuilder.Append(StringConst.String_RightCurlyBrackets);
					break;
			}

			return stringBuilder.ToString();
		}

		#endregion

	}
}

