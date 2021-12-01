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
	}
}