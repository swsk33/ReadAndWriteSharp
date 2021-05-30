using System;
using Swsk33.ReadAndWriteSharp;
using Swsk33.ReadAndWriteSharp.Param;

namespace MainTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(NetworkUtil.DownloadFile("https://img-home.csdnimg.cn/images/20201124032511.png", "E:\\中转\\a.png"));
		}
	}
}
