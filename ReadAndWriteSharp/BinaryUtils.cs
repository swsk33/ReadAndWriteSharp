using System.Drawing;
using System.IO;
using System.Reflection;

namespace Swsk33.ReadAndWriteSharp
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
        public static void WriteBinaryFile(string filePath, byte[] content)
        {
            FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            BinaryWriter writer = new BinaryWriter(file);
            writer.Write(content);
            writer.Close();
            file.Close();
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
    }
}