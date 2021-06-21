namespace Swsk33.ReadAndWriteSharp
{
	/// <summary>
	/// 文本处理实用类
	/// </summary>
	public class TextUtils
	{
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
		/// 将原字符串中特殊字符转义存储（暂不支持Unicode）
		/// </summary>
		/// <param name="origin">原字符串</param>
		/// <returns>转义存储后字符串</returns>
		public static string EscapeCharacterInString(string origin)
		{
			string result = "";
			char[] originCharArray = origin.ToCharArray();
			foreach (char eachChar in originCharArray)
			{
				switch (eachChar)
				{
					case '\r':
						result = result + "\\r";
						break;
					case '\n':
						result = result + "\\n";
						break;
					case '\"':
						result = result + "\\\"";
						break;
					case '\'':
						result = result + "\\\'";
						break;
					case '\\':
						result = result + "\\\\";
						break;
					default:
						result = result + eachChar;
						break;
				}
			}
			return result;
		}
	}
}