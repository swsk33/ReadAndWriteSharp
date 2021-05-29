using System.Collections.Generic;

namespace Swsk33.ReadAndWriteSharp.Model
{
	/// <summary>
	/// 文件夹信息类
	/// </summary>
	public class DirInfo
	{

		private long size;

		private List<string> fileList = new List<string>();

		/// <summary>
		/// 文件夹大小，单位字节
		/// </summary>
		public long Size
		{
			get
			{
				return size;
			}

			set
			{
				size = value;
			}
		}

		/// <summary>
		/// 文件夹中的所有文件列表，绝对路径
		/// </summary>
		public List<string> FileList
		{
			get
			{
				return fileList;
			}

			set
			{
				fileList = value;
			}
		}

		/// <summary>
		/// 添加文件信息至文件列表
		/// </summary>
		/// <param name="file">文件路径</param>
		public void addFileToList(string file)
		{
			fileList.Add(file);
		}

		/// <summary>
		/// 追加大小
		/// </summary>
		/// <param name="size">追加部分大小</param>
		public void addSize(long size)
		{
			this.size = this.size + size;
		}
	}
}