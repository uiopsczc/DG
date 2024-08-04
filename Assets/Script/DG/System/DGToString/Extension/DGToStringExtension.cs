using System.Collections;

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
