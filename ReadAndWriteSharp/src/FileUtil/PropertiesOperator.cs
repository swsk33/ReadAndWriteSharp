using System.Collections.Generic;
using System.IO;

namespace Swsk33.ReadAndWriteSharp.FileUtil
{
	/// <summary>
	/// properties文件读写
	/// </summary>
	public class PropertiesOperator
	{
		/// <summary>
		/// 读取一个properties文件，将里面的键值读取储存为一个字典返回
		/// </summary>
		/// <param name="filePath">待读取文件位置</param>
		/// <returns>读取结果，为一个字典</returns>
		public static Dictionary<string, string> ReadPropertiesFile(string filePath)
		{
			string[] contents = File.ReadAllLines(filePath);
			Dictionary<string, string> result = new Dictionary<string, string>();
			foreach (string line in contents)
			{
				// 跳过注释和无效值
				if (line.Trim().StartsWith("#") || !line.Contains("="))
				{
					continue;
				}
				string lineTrimed = line.Trim();
				string key = lineTrimed.Substring(0, lineTrimed.IndexOf("="));
				string value = lineTrimed.Substring(lineTrimed.IndexOf("=") + 1);
				result.Add(key.Trim(), value.Trim());
			}
			return result;
		}

		/// <summary>
		/// 把一个字典对象写入为一个properties文件，文件不存在将创建，存在则直接覆盖
		/// </summary>
		/// <param name="keyValues">待写入的键值对</param>
		/// <param name="filePath">待写入文件</param>
		public static void WritePropertiesFile(Dictionary<string, string> keyValues, string filePath)
		{
			List<string> contents = new List<string>();
			foreach (string key in keyValues.Keys)
			{
				contents.Add(key.Trim() + "=" + keyValues[key].Trim());
			}
			File.WriteAllLines(filePath, contents);
		}
	}
}