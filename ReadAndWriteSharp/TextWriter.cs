using System;
using System.Text;
using System.IO;

namespace Swsk33.ReadAndWriteSharp
{
    /// <summary>
    /// 文本文件写入器
    /// </summary>
    public class TextWriter
    {
        /// <summary>
        /// 用指定内容替换文件指定行
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">替换内容</param>
        /// <param name="line">指定行</param>
        /// <returns>是否替换成功</returns>
        public static bool ReplaceSpecificLine(String filePath, string content, int line)
        {
            bool success = false;
            int totalLine = TextReader.GetFileLine(filePath);
            if (line <= 0)
            {
                return success;
            }
            if (line > totalLine)
            {
                return success;
            }
            string text = "";
            StreamReader reader = new StreamReader(filePath);
            for (int i = 0; i < line - 1; i++)
            {
                text = text + reader.ReadLine() + "\r\n";
            }
            reader.ReadLine();
            text = text + content + "\r\n";
            for (int i = 0; i < totalLine - line; i++)
            {
                text = text + reader.ReadLine() + "\r\n";
            }
            reader.Close();
            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(text);
            writer.Close();
            if (TextReader.ReadSpecificLine(filePath, line).Equals(content))
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// 使用指定的字符编码，用指定内容替换文件指定行
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">替换内容</param>
        /// <param name="line">指定行</param>
        /// <param name="encoding">指定编码。例如GBK可填入Encoding.GetEncoding("gbk")，UTF8填入Encoding.GetEncoding("utf-8")</param>
        /// <returns>是否替换成功</returns>
        public static bool ReplaceSpecificLine(String filePath, string content, int line, Encoding encoding)
        {
            bool success = false;
            int totalLine = TextReader.GetFileLine(filePath);
            if (line <= 0)
            {
                return success;
            }
            if (line > totalLine)
            {
                return success;
            }
            string text = "";
            StreamReader reader = new StreamReader(filePath, encoding);
            for (int i = 0; i < line - 1; i++)
            {
                text = text + reader.ReadLine() + "\r\n";
            }
            reader.ReadLine();
            text = text + content + "\r\n";
            for (int i = 0; i < totalLine - line; i++)
            {
                text = text + reader.ReadLine() + "\r\n";
            }
            reader.Close();
            StreamWriter writer = new StreamWriter(filePath, false, encoding);
            writer.Write(text);
            writer.Close();
            if (TextReader.ReadSpecificLine(filePath, line, encoding).Equals(content))
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// 向文本文件末尾追加文本
        /// </summary>
        /// <param name="filePath">待写入文件</param>
        /// <param name="content">追加内容</param>
        /// <returns>是否追加成功</returns>
        public static bool AppendText(string filePath, string content)
        {
            bool success = false;
            string originContent = content;
            if (!File.ReadAllText(filePath).EndsWith("\r\n"))
            {
                content = "\r\n" + content;
            }
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(content);
            writer.Close();
            if (TextReader.ReadSpecificLine(filePath, TextReader.GetFileLine(filePath)).Equals(originContent))
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// 使用指定的字符编码，向文本文件末尾追加文本
        /// </summary>
        /// <param name="filePath">待写入文件</param>
        /// <param name="content">追加内容</param>
        /// <param name="encoding">指定编码。例如GBK可填入Encoding.GetEncoding("gbk")，UTF8填入Encoding.GetEncoding("utf-8")</param>
        /// <returns>是否追加成功</returns>
        public static bool AppendText(string filePath, string content, Encoding encoding)
        {
            bool success = false;
            string originContent = content;
            if (!File.ReadAllText(filePath).EndsWith("\r\n"))
            {
                content = "\r\n" + content;
            }
            StreamWriter writer = new StreamWriter(filePath, true, encoding);
            writer.WriteLine(content);
            writer.Close();
            if (TextReader.ReadSpecificLine(filePath, TextReader.GetFileLine(filePath), encoding).Equals(originContent))
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// 在指定行之后插入一行指定文本
        /// </summary>
        /// <param name="filePath">待写入文件</param>
        /// <param name="content">插入的内容</param>
        /// <param name="line">指定插入在第几行之后，若该值为0则插入至第一行之前</param>
        /// <returns>是否插入成功</returns>
        public static bool InsertText(string filePath, string content, int line)
        {
            bool success = false;
            int totalLine = TextReader.GetFileLine(filePath);
            if (line < 0)
            {
                return success;
            }
            if (line > totalLine)
            {
                return success;
            }
            string text = "";
            StreamReader reader = new StreamReader(filePath);
            for (int i = 0; i < line; i++)
            {
                text = text + reader.ReadLine() + "\r\n";
            }
            text = text + content + "\r\n";
            for (int i = 0; i < totalLine - line; i++)
            {
                text = text + reader.ReadLine() + "\r\n";
            }
            reader.Close();
            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(text);
            writer.Close();
            if (TextReader.ReadSpecificLine(filePath, line + 1).Equals(content))
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// 使用指定字符编码，在指定行之后插入一行指定文本
        /// </summary>
        /// <param name="filePath">待写入文件</param>
        /// <param name="content">插入的内容</param>
        /// <param name="line">指定插入在第几行之后，若该值为0则插入至第一行之前</param>
        /// <param name="encoding">指定编码。例如GBK可填入Encoding.GetEncoding("gbk")，UTF8填入Encoding.GetEncoding("utf-8")</param>
        /// <returns>是否插入成功</returns>
        public static bool InsertText(string filePath, string content, int line, Encoding encoding)
        {
            bool success = false;
            int totalLine = TextReader.GetFileLine(filePath);
            if (line < 0)
            {
                return success;
            }
            if (line > totalLine)
            {
                return success;
            }
            string text = "";
            StreamReader reader = new StreamReader(filePath, encoding);
            for (int i = 0; i < line; i++)
            {
                text = text + reader.ReadLine() + "\r\n";
            }
            text = text + content + "\r\n";
            for (int i = 0; i < totalLine - line; i++)
            {
                text = text + reader.ReadLine() + "\r\n";
            }
            reader.Close();
            StreamWriter writer = new StreamWriter(filePath, false, encoding);
            writer.Write(text);
            writer.Close();
            if (TextReader.ReadSpecificLine(filePath, line + 1, encoding).Equals(content))
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// 移除文件指定行内容
        /// </summary>
        /// <param name="filePath">待操作文件路径</param>
        /// <param name="line">待移除行</param>
        /// <returns>是否移除成功</returns>
        public static bool RemoveSpecificLine(string filePath, int line)
        {
            bool success = false;
            int totalLine = TextReader.GetFileLine(filePath);
            if (line <= 0)
            {
                return success;
            }
            if (line > totalLine)
            {
                return success;
            }
            string text = "";
            StreamReader reader = new StreamReader(filePath);
            for (int i = 0; i < line - 1; i++)
            {
                text = text + reader.ReadLine() + "\r\n";
            }
            reader.ReadLine();
            for (int i = 0; i < totalLine - line; i++)
            {
                text = text + reader.ReadLine() + "\r\n";
            }
            reader.Close();
            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(text);
            writer.Close();
            if (File.ReadAllText(filePath).Equals(text))
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// 将指定文件内容清空
        /// </summary>
        /// <param name="filePath">待清空的文件</param>
        /// <returns>是否清空成功</returns>
        public static bool ClearAll(string filePath)
        {
            bool success = false;
            File.WriteAllText(filePath, "");
            if (TextReader.GetFileLine(filePath) == 0)
            {
                success = true;
            }
            return success;
        }
    }
}