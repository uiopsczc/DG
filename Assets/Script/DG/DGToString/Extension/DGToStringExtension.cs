using System;
using System.Collections;
using System.Text;

namespace DG
{
	public static class DGToStringExtension
	{
		/// <summary>
		///   用于Object的ToString2，有ToString2的类必须在这里添加对应的处理
		/// </summary>
		public static string DGToString(this object o, bool isFillStringWithDoubleQuote = false)
		{
			switch (o)
			{
				//			case JsonData jsonData:
				//				return jsonData.ToJsonWithUTF8();
				case ICollection collection:
					return collection.DGToString(isFillStringWithDoubleQuote);
				case IDGToString obj:
					return obj.DGToString(isFillStringWithDoubleQuote);
			}

			var result = o.ToString();
			if (o is string && isFillStringWithDoubleQuote) result = _WarpWithDoubleQuotes(result);
			return result;
		}

		public static string DGToString(this ICollection self, bool isFillStringWithDoubleQuote = false)
		{
			bool isFirst = true;
			StringBuilder stringBuilder = new StringBuilder(100);
			switch (self)
			{
				case Array _:
					stringBuilder.Append("(");
					break;
				case IList _:
					stringBuilder.Append("[");
					break;
				case IDictionary _:
					stringBuilder.Append("{");
					break;
			}

			if (self is IDictionary dictionary)
			{
				foreach (DictionaryEntry dictionaryEntry in dictionary)
				{
					var key = dictionaryEntry.Key;
					var value = dictionaryEntry.Value;
					if (isFirst)
						isFirst = false;
					else
						stringBuilder.Append(",");
					stringBuilder.Append(key.DGToString(isFillStringWithDoubleQuote));
					stringBuilder.Append(":");
					stringBuilder.Append(value.DGToString(isFillStringWithDoubleQuote));
				}
			}
			else //list
			{
				foreach (var o in self)
				{
					if (isFirst)
						isFirst = false;
					else
						stringBuilder.Append(",");
					stringBuilder.Append(o.DGToString(isFillStringWithDoubleQuote));
				}
			}

			switch (self)
			{
				case Array _:
					stringBuilder.Append(")");
					break;
				case IList _:
					stringBuilder.Append("]");
					break;
				case IDictionary _:
					stringBuilder.Append("}");
					break;
			}

			return stringBuilder.ToString();
		}

		#region  private
		private static string _WarpWithDoubleQuotes(string content) //双引号
		{
			return _WarpBoth(content, "\"");
		}

		private static string _WarpBoth(string content, string wrap)
		{
			return _Wrap(content, wrap, wrap);
		}

		public static string _Wrap(string content, string leftWrap, string rightWrap)
		{
			return string.Format("{0}{1}{2}", leftWrap, content, rightWrap);
		}



		#endregion

	}
}
