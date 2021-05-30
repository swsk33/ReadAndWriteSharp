using System;
using Swsk33.ReadAndWriteSharp;
using Swsk33.ReadAndWriteSharp.Param;

namespace MainTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(NetworkUtil.SendGetRequest("https://cn.bing.com", UserAgentValue.CHROME));
		}
	}
}
