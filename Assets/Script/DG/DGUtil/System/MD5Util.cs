using System.Security.Cryptography;
using System.Text;

namespace DG
{
	public class MD5Util
	{
		/// <summary>
		///   MD5加密
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string Encrypt(string s)
		{
			var md5Hash = MD5.Create();
			var datas = md5Hash.ComputeHash(s.GetBytes());
			var stringBuilder = new StringBuilder();
			for (var i = 0; i < datas.Length; i++)
			{
				var data = datas[i];
				stringBuilder.Append(data.ToString(StringConst.STRING_x2));
			}

			return stringBuilder.ToString();
		}
	}
}