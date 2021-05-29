using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swsk33.ReadAndWriteSharp
{
	/// <summary>
	/// JSON简单实用类
	/// </summary>
	public class JSONUtils
	{
		/// <summary>
		/// 转义字符
		/// </summary>
		/// <param name="escapeChar">原字符</param>
		/// <returns>转义字符</returns>
		private static char getEscapeChar(char escapeChar)
		{
			if (escapeChar == '\"')
			{
				return '\"';
			}
			if (escapeChar == '\'')
			{
				return '\'';
			}
			if (escapeChar == '\\')
			{
				return '\\';
			}
			if (escapeChar == 'r')
			{
				return '\r';
			}
			if (escapeChar == 'n')
			{
				return '\n';
			}
			return '0';
		}

		/// <summary>
		/// 将字符串中字符转义
		/// </summary>
		/// <param name="origin">原字符串</param>
		/// <returns>转义后字符串</returns>
		private static string replaceEscapeInString(string origin)
		{
			string result = "";
			for (int i = 0; i < origin.Length; i++)
			{
				char current = origin.ElementAt(i);
				if (current == '\r')
				{
					result = result + "\\r";
				}
				else if (current == '\n')
				{
					result = result + "\\n";
				}
				else if (current == '\"')
				{
					result = result + "\\\"";
				}
				else if (current == '\'')
				{
					result = result + "\\\'";
				}
				else if (current == '\\')
				{
					result = result + "\\\\";
				}
				else
				{
					result = result + current;
				}
			}
			return result;
		}

		/// <summary>
		/// 是否是无用字符
		/// </summary>
		/// <param name="judgeChar">字符</param>
		/// <returns>是否为无用字符</returns>
		private static bool isUselessChar(char judgeChar)
		{
			if (judgeChar == ' ' || judgeChar == '\r' || judgeChar == '\n' || judgeChar == '	')
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 引号包围字符串
		/// </summary>
		/// <param name="str">原字符串</param>
		/// <returns>被引号包围字符串</returns>
		private static string surByQut(String str)
		{
			return "\"" + str + "\"";
		}


	}
}
