using System.Security.Cryptography;
using System.Text;

namespace Swsk33.ReadAndWriteSharp.Util
{
	/// <summary>
	/// 字符串处理实用类
	/// </summary>
	public class StringUtils
	{
		/// <summary>
		/// 判断字符串是否为空
		/// </summary>
		/// <param name="originString">传入待判断字符串</param>
		/// <returns>若传入字符串是null或者""的时候则为true</returns>
		public static bool IsEmpty(string originString)
		{
			return originString == null || originString.Equals("");
		}

		/// <summary>
		/// 使用单引号包围字符串
		/// </summary>
		/// <param name="origin">原字符串</param>
		/// <returns>被单引号包围的字符串</returns>
		public static string SurroundBySingleQuotes(string origin)
		{
			return "\'" + origin + "\'";
		}

		/// <summary>
		/// 使用双引号包围字符串
		/// </summary>
		/// <param name="origin">原字符串</param>
		/// <returns>被双引号包围的字符串</returns>
		public static string SurroundByDoubleQuotes(string origin)
		{
			return "\"" + origin + "\"";
		}

		/// <summary>
		/// 计算字符串MD5值
		/// </summary>
		/// <param name="origin">传入字符串</param>
		/// <returns>传入字符串的MD5</returns>
		public static string GetStringMD5(string origin)
		{
			MD5 md5 = MD5.Create();
			byte[] originBytes = Encoding.UTF8.GetBytes(origin);
			byte[] resultBytes = md5.ComputeHash(originBytes);
			StringBuilder result = new StringBuilder();
			foreach (byte eachByte in resultBytes)
			{
				result.Append(eachByte.ToString("x2"));
			}
			return result.ToString();
		}

		/// <summary>
		/// 计算字符串SHA1值
		/// </summary>
		/// <param name="origin">传入字符串</param>
		/// <returns>传入字符串的SHA1值</returns>
		public static string GetStringSHA1(string origin)
		{
			SHA1 sha1 = SHA1.Create();
			byte[] originBytes = Encoding.UTF8.GetBytes(origin);
			byte[] resultBytes = sha1.ComputeHash(originBytes);
			StringBuilder result = new StringBuilder();
			foreach (byte eachByte in resultBytes)
			{
				result.Append(eachByte.ToString("x2"));
			}
			return result.ToString();
		}
	}
}