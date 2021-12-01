using Swsk33.ReadAndWriteSharp.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace Swsk33.ReadAndWriteSharp.FileUtil
{
	/// <summary>
	/// 文件夹实用类
	/// </summary>
	public class DirectoryUtils
	{
		/// <summary>
		/// 递归获取一个文件夹中全部文件-递归部分
		/// </summary>
		/// <param name="directoryPath">文件夹路径</param>
		/// <param name="result">用于储存遍历结果的列表</param>
		/// <param name="excludeFileName">要排除的文件名列表，只要是该列表中的文件名都不会在结果中，不需要排除传入null</param>
		/// <param name="excludeDirectoryName">要排除的目录名列表，只要是该列表中的文件夹名都不会在结果中，不需要排除传入null</param>
		private static void getAllFilesInDirectory(string directoryPath, List<string> result, string[] excludeFileName, string[] excludeDirectoryName)
		{
			string[] directories = Directory.GetDirectories(directoryPath);
			foreach (string directory in directories)
			{
				if (excludeDirectoryName != null && Array.IndexOf(excludeDirectoryName, FilePathUtils.GetFileName(directory)) != -1)
				{
					continue;
				}
				getAllFilesInDirectory(directory, result, excludeFileName, excludeDirectoryName);
			}
			string[] files = Directory.GetFiles(directoryPath);
			foreach (string file in files)
			{
				if (excludeFileName != null && Array.IndexOf(excludeFileName, FilePathUtils.GetFileName(file)) != -1)
				{
					continue;
				}
				result.Add(file);
			}
		}

		/// <summary>
		/// 递归获取一个文件夹中全部文件
		/// </summary>
		/// <param name="directoryPath">文件夹路径</param>
		/// <returns>该文件夹及其所有子文件夹的全部文件路径</returns>
		public static string[] GetAllFilesInDirectory(string directoryPath)
		{
			List<string> result = new List<string>();
			getAllFilesInDirectory(directoryPath, result, null, null);
			return result.ToArray();
		}

		/// <summary>
		/// 递归获取一个文件夹中全部文件，并指定要排除的文件名和文件夹名列表
		/// </summary>
		/// <param name="directoryPath">文件夹路径</param>
		/// <param name="excludeFileName">排除文件名列表，只要是该列表中的文件名都不会在结果中，不需要排除传入null</param>
		/// <param name="excludeDirectoryName">排除文件夹名列表，只要是该列表中的文件夹名都不会在结果中，不需要排除传入null</param>
		/// <returns>该文件夹及其所有子文件夹的全部文件路径</returns>
		public static string[] GetAllFilesInDirectory(string directoryPath, string[] excludeFileName, string[] excludeDirectoryName)
		{
			List<string> result = new List<string>();
			getAllFilesInDirectory(directoryPath, result, excludeFileName, excludeDirectoryName);
			return result.ToArray();
		}

		/// <summary>
		/// 复制一整个文件夹
		/// </summary>
		/// <param name="origin">要被复制的文件夹路径</param>
		/// <param name="destination">复制到目标文件夹路径</param>
		/// <returns>是否复制成功</returns>
		public static bool CopyDirectory(string origin, string destination)
		{
			// 规范化路径
			origin = origin.Replace("/", "\\");
			origin = FilePathUtils.RemovePathEndBackslash(origin) + "\\";
			destination = destination.Replace("/", "\\");
			destination = FilePathUtils.RemovePathEndBackslash(destination) + "\\";
			string[] files = GetAllFilesInDirectory(origin);
			foreach (string file in files)
			{
				string destinationPath = destination + file.Substring(origin.Length);
				string fileParentPath = destinationPath.Substring(0, destinationPath.LastIndexOf("\\"));
				if (!Directory.Exists(fileParentPath))
				{
					Directory.CreateDirectory(fileParentPath);
				}
				BinaryUtils.CopyFile(file, destinationPath);
			}
			return Directory.Exists(destination);
		}
	}
}