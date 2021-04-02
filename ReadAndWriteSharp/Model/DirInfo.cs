using System.Collections.Generic;

namespace Swsk33.ReadAndWriteSharp.Model
{
    public class DirInfo
    {
        /// <summary>
        /// 文件夹大小，单位字节
        /// </summary>
        private long Size;

        /// <summary>
        /// 文件夹中的所有文件列表，绝对路径
        /// </summary>
        private List<string> FileList = new List<string>();

        public long GetSize()
        {
            return Size;
        }

        public void AddSize(long size)
        {
            this.Size = this.Size + size;
        }

        public List<string> GetFileList()
        {
            return FileList;
        }

        public void AppendFileList(string item)
        {
            this.FileList.Add(item);
        }
    }
}