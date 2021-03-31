using Swsk33.ReadAndWriteSharp;
using System;
using System.Text;

namespace MainTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TextReader.GetFileLine(@"E:\中转\a.txt"));
        }
    }
}
