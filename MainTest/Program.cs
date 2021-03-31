using Swsk33.ReadAndWriteSharp;
using System;
using System.Text;

namespace MainTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding e = Encoding.GetEncoding("gbk");
            Console.WriteLine(TextWriter.ClearAll(@"E:\中转\a.txt"));
        }
    }
}
