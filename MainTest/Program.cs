using Swsk33.ReadAndWriteSharp;
using System;
using System.Text;

namespace MainTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TextWriter.ReplaceSpecificLine(@"E:\中转\a.txt", "177", 7));
        }
    }
}
