using System.Text;
using System.IO;

namespace Swsk33.ReadAndWriteSharp
{
    /// <summary>
    /// 文本文件读取器
    /// </summary>
    public class TextReader
    {
        /// <summary>
        /// 读取文本文件的行数
        /// </summary>
        /// <param name="filePath">待读取文件</param>
        /// <returns>行数</returns>
        public static int GetFileLine(string filePath)
        {
            int line = 0;
            StreamReader reader = new StreamReader(filePath);
            while(reader.ReadLine() != null)
            {
                line++;
            }
            reader.Close();
            return line;
        }

        /// <summary>
        /// 读取文本文件指定行
        /// </summary>
        /// <param name="filePath">待读取文件路径</param>
        /// <param name="line">指定行</param>
        /// <returns>读取结果</returns>
        public static string ReadSpecificLine(string filePath, int line)
        {
            string result = "";
            StreamReader reader = new StreamReader(filePath);
            for (int i = 0; i < line; i++)
            {
                result = reader.ReadLine();
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// 使用特定的编码读取文本文档指定行
        /// </summary>
        /// <param name="filePath">待读取文件路径</param>
        /// <param name="line">指定行</param>
        /// <param name="encoding">指定编码。例如GBK可填入Encoding.GetEncoding("gbk")，UTF8填入Encoding.GetEncoding("utf-8")</param>
        /// <returns>读取结果</returns>
        public static string ReadSpecificLine(string filePath, int line, Encoding encoding)
        {
            string result = "";
            StreamReader reader = new StreamReader(filePath, encoding);
            for (int i = 0; i < line; i++)
            {
                result = reader.ReadLine();
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// 读取指定行数范围内的内容并以字符串形式储存，包含起始行和终止行
        /// </summary>
        /// <param name="filePath">待读取文件</param>
        /// <param name="start">起始行</param>
        /// <param name="end">终止行</param>
        /// <returns>读取结果</returns>
        public static string ReadSpecificRange(string filePath, int start, int end)
        {
            string result = "";
            StreamReader reader = new StreamReader(filePath);
            int currentLine;
            for (currentLine = 0; currentLine < start; currentLine++)
            {
                result = reader.ReadLine();
            }
            for (int i = currentLine; i < end; i++)
            {
                result = result + "\r\n" + reader.ReadLine();
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// 以指定的编码读取指定行数范围内的内容并以字符串形式储存，包含起始行和终止行
        /// </summary>
        /// <param name="filePath">待读取文件</param>
        /// <param name="start">起始行</param>
        /// <param name="end">终止行</param>
        /// <param name="encoding">指定编码。例如GBK可填入Encoding.GetEncoding("gbk")，UTF8填入Encoding.GetEncoding("utf-8")</param>
        /// <returns>读取结果</returns>
        public static string ReadSpecificRange(string filePath, int start, int end, Encoding encoding)
        {
            string result = "";
            StreamReader reader = new StreamReader(filePath, encoding);
            int currentLine;
            for (currentLine = 0; currentLine < start; currentLine++)
            {
                result = reader.ReadLine();
            }
            for (int i = currentLine; i < end; i++)
            {
                result = result + "\r\n" + reader.ReadLine();
            }
            reader.Close();
            return result;
        }
    }
}