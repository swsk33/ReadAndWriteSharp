using System;
using System.Diagnostics;

namespace Swsk33.ReadAndWriteSharp
{
    /// <summary>
    /// 终端实用类
    /// </summary>
    public class TerminalUtils
    {
        /// <summary>
        /// 调用命令行并获取执行结果
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
    }
}