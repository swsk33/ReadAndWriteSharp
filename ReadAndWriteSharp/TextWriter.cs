using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
