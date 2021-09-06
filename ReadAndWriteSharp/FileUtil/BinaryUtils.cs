﻿using Swsk33.ReadAndWriteSharp.Model.System.Param;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Swsk33.ReadAndWriteSharp.FileUtil
{
	/// <summary>
	/// 二进制文件工具类
	/// </summary>
	public class BinaryUtils
	{
		/// <summary>
		/// 读取二进制文件为字节数组
		/// </summary>
		/// <param name="filePath">待读取文件</param>
		/// <returns>读取的内容，为字节数组形式</returns>
		public static byte[] ReadBinaryFile(string filePath)
		{
			FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
			byte[] result = new byte[file.Length];
			BinaryReader reader = new BinaryReader(file);
			reader.Read(result, 0, result.Length);
			reader.Close();
			file.Close();
			return result;
		}

		/// <summary>
		/// 将二进制数据写入指定文件，文件不存在将创建，存在将被覆盖
		/// </summary>
		/// <param name="filePath">待写入文件</param>
		/// <param name="content">写入内容</param>
		/// <returns>是否写入成功</returns>
		public static bool WriteBinaryFile(string filePath, byte[] content)
		{
			bool success = false;
			FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
			BinaryWriter writer = new BinaryWriter(file);
			writer.Write(content);
			writer.Close();
			file.Close();
			if (File.Exists(filePath))
			{
				success = true;
			}
			return success;
		}

		/// <summary>
		/// 将可序列化的对象写入文件，文件不存在将创建，存在将被覆盖
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <param name="filePath">待写入文件</param>
		/// <param name="data">可序列化对象数据</param>
		/// <returns>是否写入成功</returns>
		public static bool WriteObjectToFile<T>(string filePath, T data)
		{
			BinaryFormatter serializer = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			serializer.Serialize(memoryStream, data);
			memoryStream.Position = 0;
			byte[] dataBuffer = new byte[memoryStream.Length];
			memoryStream.Read(dataBuffer, 0, dataBuffer.Length);
			bool success = WriteBinaryFile(filePath, dataBuffer);
			return success;
		}

		/// <summary>
		/// 从文件中读取数据并反序列化为相应的对象
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <param name="filePath">待读取文件类型</param>
		/// <returns>反序列化的对象</returns>
		public static T ReadObjectFromFile<T>(string filePath)
		{
			byte[] data = ReadBinaryFile(filePath);
			BinaryFormatter serializer = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(data, 0, data.Length);
			memoryStream.Position = 0;
			T result = (T)serializer.Deserialize(memoryStream);
			return result;
		}

		/// <summary>
		/// 获取文件夹信息并储存在一个DirInfo实例中
		/// </summary>
		/// <param name="dirPath">文件夹路径</param>
		/// <param name="info">DirInfo实例</param>
		public static void GetDirectoryInfo(string dirPath, DirInfo info)
		{
			string[] files = Directory.GetFiles(dirPath);
			info.Size = 0;
			if (files.Length != 0)
			{
				foreach (string file in files)
				{
					info.addFileToList(file);
					info.addSize(new FileInfo(file).Length);
				}
			}
			string[] dirs = Directory.GetDirectories(dirPath);
			if (dirs.Length != 0)
			{
				foreach (string dir in dirs)
				{
					GetDirectoryInfo(dir, info);
				}
			}
		}

		/// <summary>
		/// 复制文件夹
		/// </summary>
		/// <param name="origin">原文件夹</param>
		/// <param name="dest">复制到的指定位置</param>
		public static void CopyDir(string origin, string dest)
		{
			DirInfo info = new DirInfo();
			GetDirectoryInfo(origin, info);
			if (origin.EndsWith("\\"))
			{
				origin = origin.Substring(0, origin.Length - 1);
			}
			if (dest.EndsWith("\\"))
			{
				dest = dest.Substring(0, dest.Length - 1);
			}
			string destDirPath = dest + "\\" + origin.Substring(origin.LastIndexOf("\\") + 1);
			foreach (string file in info.FileList)
			{
				string destFilePath = destDirPath + "\\" + file.Substring(origin.Length + 1);
				if (File.Exists(file))
				{
					string parentDir = destFilePath.Substring(0, destFilePath.LastIndexOf("\\"));
					if (!Directory.Exists(parentDir))
					{
						Directory.CreateDirectory(parentDir);
						File.Copy(file, destFilePath);
					}
				}
			}
		}

		/// <summary>
		/// 释放嵌入在resx里面的普通类型文件
		/// </summary>
		/// <param name="resource">resx里面的资源</param>
		/// <param name="path">释放到的路径</param>
		/// <returns>是否释放成功</returns>
		public static bool ExtractNormalFileInResx(byte[] resource, string path)
		{
			bool success = false;
			FileStream file = new FileStream(path, FileMode.Create);
			file.Write(resource, 0, resource.Length);
			file.Flush();
			file.Close();
			if (File.Exists(path))
			{
				success = true;
			}
			return success;
		}

		/// <summary>
		/// 释放嵌入在resx文件里面的音频资源文件
		/// </summary>
		/// <param name="fileInResx">在resx里面的音频文件</param>
		/// <param name="path">释放到的路径</param>
		/// <returns>是否释放成功</returns>
		public static bool ExtractAudioFileInResx(Stream fileInResx, string path)
		{
			bool success = false;
			Stream input = fileInResx;
			FileStream output = new FileStream(path, FileMode.Create);
			byte[] data = new byte[1024];
			int lengthEachRead;
			while ((lengthEachRead = input.Read(data, 0, data.Length)) > 0)
			{
				output.Write(data, 0, lengthEachRead);
			}
			output.Flush();
			output.Close();
			if (File.Exists(path))
			{
				success = true;
			}
			return success;
		}

		/// <summary>
		/// 释放嵌入在resx里的图片资源文件
		/// </summary>
		/// <param name="image">resx里的图片资源</param>
		/// <param name="path">释放到的路径</param>
		/// <returns>是否释放成功</returns>
		public static bool ExtractImageFileInResx(Bitmap image, string path)
		{
			bool success = false;
			MemoryStream memoryStream = new MemoryStream();
			image.Save(memoryStream, image.RawFormat);
			byte[] data = memoryStream.ToArray();
			FileStream file = new FileStream(path, FileMode.Create);
			file.Write(data, 0, data.Length);
			file.Flush();
			file.Close();
			if (File.Exists(path))
			{
				success = true;
			}
			return success;
		}

		/// <summary>
		/// 释放嵌入在resx中的文本文件
		/// </summary>
		/// <param name="textFile">resx里的文本文件</param>
		/// <param name="path">释放到路径</param>
		/// <returns>是否释放成功</returns>
		public static bool ExtractTextFileInResx(string textFile, string path)
		{
			bool success = false;
			File.WriteAllText(path, textFile);
			if (File.Exists(path))
			{
				success = true;
			}
			return success;
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

		/// <summary>
		/// 获取文件MD5值
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <returns>MD5值</returns>
		public static string GetFileMD5(string filePath)
		{
			FileStream file = new FileStream(filePath, FileMode.Open);
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] retVal = md5.ComputeHash(file);
			file.Close();
			StringBuilder result = new StringBuilder();
			for (int i = 0; i < retVal.Length; i++)
			{
				result.Append(retVal[i].ToString("x2"));
			}
			return result.ToString();
		}

		/// <summary>
		/// 获取文件SHA1值
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <returns>SHA1值</returns>
		public static string GetFileSHA1(string filePath)
		{
			FileStream file = new FileStream(filePath, FileMode.Open);
			SHA1 sha1 = new SHA1CryptoServiceProvider();
			byte[] retval = sha1.ComputeHash(file);
			file.Close();
			StringBuilder result = new StringBuilder();
			for (int i = 0; i < retval.Length; i++)
			{
				result.Append(retval[i].ToString("x2"));
			}
			return result.ToString();
		}
	}
}