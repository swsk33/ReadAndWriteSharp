using System;
using Microsoft.Win32;
using Swsk33.ReadAndWriteSharp;
using Swsk33.ReadAndWriteSharp.Param;

namespace MainTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(RegUtils.IsItemExists(Registry.LocalMachine, @"SOFTWARE"));
		}
	}
}
