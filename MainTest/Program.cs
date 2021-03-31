using Swsk33.ReadAndWriteSharp;
using System;
using System.Text;

namespace MainTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine(BinaryUtils.ExtractTextFileInResx(Properties.Resources.testFile, @"E:\中转\o.txt"));
        }
    }
}
