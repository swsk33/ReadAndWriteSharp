namespace Swsk33.ReadAndWriteSharp.Util
{
	/// <summary>
	/// 文件路径实用类
	/// </summary>
	public class FilePathUtils
	{
		/// <summary>
		/// 移除原路径字符串末尾斜杠(/)，若原路径字符串不以斜杠结尾则不作任何操作
		/// </summary>
		/// <param name="origin">原路径字符串</param>
		/// <returns>移除末尾斜杠后的路径字符串</returns>
		public static string RemovePathEndSlash(string origin)
		{
			if (origin.EndsWith("/"))
			{
				origin = origin.Substring(0, origin.LastIndexOf("/"));
			}
			return origin;
		}

		/// <summary>
		/// 移除原路径字符串末尾反斜杠(\)，若原路径字符串不以反斜杠结尾则不作任何操作
		/// </summary>
		/// <param name="origin">原路径字符串</param>
		/// <returns>移除末尾反斜杠后的路径字符串</returns>
		public static string RemovePathEndBackslash(string origin)
		{
			if (origin.EndsWith("\\"))
			{
				origin = origin.Substring(0, origin.LastIndexOf("\\"));
			}
			return origin;
		}

		/// <summary>
		/// 获取文件（夹）名
		/// </summary>
		/// <param name="filePath">传入文件路径，可以是绝对路径或者相对路径</param>
		/// <returns>文件（夹）名</returns>
		public static string GetFileName(string filePath)
		{
			filePath = filePath.Replace('\\', '/');
			filePath = RemovePathEndSlash(filePath);
			string fileName;
			if (!filePath.Contains("/"))
			{
				fileName = filePath;
			}
			else
			{
				fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
			}
			return fileName;
		}

		/// <summary>
		/// 获取文件扩展名，不带.
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <returns>文件扩展名，无扩展名返回null</returns>
		public static string GetFileFormat(string filePath)
		{
			if (!filePath.Contains("."))
			{
				return null;
			}
			return filePath.Substring(filePath.LastIndexOf(".") + 1);
		}
	}
}