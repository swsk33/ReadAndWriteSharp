using Swsk33.ReadAndWriteSharp.Model;
using Swsk33.ReadAndWriteSharp;
using System.Threading;
using System.Text;
using System;

namespace MainTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TerminalResult result = new TerminalResult();
            //TerminalUtils.RunCommandAsynchronously("cmd", "/c 7z x \"E:\\中转\\公主连结拆包合集.zip\" -o\"C:\\Users\\swsk33\\Downloads\\a\"", result);
            TerminalUtils.RunCommandAsynchronously("ping", "www.baidu.com", result);
            while (!result.IsFinished())
            {
                Console.Write(result.GetStandardOutput());
                Console.Write(result.GetErrorOutput());
            }
        }
    }
}
