using Swsk33.ReadAndWriteSharp.Model;
using System;
using System.Diagnostics;
using System.Threading;

namespace Swsk33.ReadAndWriteSharp
{
	/// <summary>
	/// 终端实用类
	/// </summary>
	public class TerminalUtils
	{
		/// <summary>
		/// 调用命令行并获取执行结果，该方法为同步方法，会堵塞线程
		/// </summary>
		/// <param name="command">命令</param>
		/// <param name="args">参数</param>
		/// <returns>命令输出结果，为string数组，数组的第一个为标准输出流，第二个为标准错误流</returns>
		public static string[] RunCommand(string command, string args)
		{
			string[] result = new string[2];
			string output = "";
			string err = "";
			Process process = new Process();
			process.StartInfo.FileName = command;
			process.StartInfo.Arguments = args;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			try
			{
				process.Start();
				string outLine = null;
				string errLine = null;
				while ((outLine = process.StandardOutput.ReadLine()) != null || (errLine = process.StandardError.ReadLine()) != null)
				{
					if (outLine != null)
					{
						output = output + outLine + "\r\n";
					}
					if (errLine != null)
					{
						err = err + errLine + "\r\n";
					}
				}
				process.WaitForExit();
			}
			catch (Exception)
			{
				//none
			}
			finally
			{
				process.Close();
			}
			result[0] = output;
			result[1] = err;
			return result;
		}

		/// <summary>
		/// 异步执行命令行并将输出结果实时储存在一个TerminalResult类型的实例中
		/// </summary>
		/// <param name="command">调用命令</param>
		/// <param name="args">命令参数</param>
		/// <param name="result">TerminalResult类型的实例，位于Swsk33.ReadAndWriteSharp.Model下，用于实时存放命令行的输出结果，先实例化一个TerminalResult对象并传入，然后可以在主线程实时获取结果</param>
		public static void RunCommandAsynchronously(string command, string args, TerminalResult result)
		{
			new Thread(() =>
			{
				Process process = new Process();
				process.StartInfo.FileName = command;
				process.StartInfo.Arguments = args;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				try
				{
					process.Start();
					string outLine = null;
					string errLine = null;
					while ((outLine = process.StandardOutput.ReadLine()) != null || (errLine = process.StandardError.ReadLine()) != null)
					{
						if (outLine != null)
						{
							result.StandardOutput = result.StandardOutput + outLine + "\r\n";
						}
						if (errLine != null)
						{
							result.ErrorOutput = result.ErrorOutput + errLine + "\r\n";
						}
					}
					process.WaitForExit();
				}
				catch (Exception)
				{
					//none
				}
				finally
				{
					process.Close();
					result.Finished = true;
				}
			}).Start();
		}
	}
}