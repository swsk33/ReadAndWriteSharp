namespace Swsk33.ReadAndWriteSharp.System.Result
{
	/// <summary>
	/// 终端实用类的输出结果
	/// </summary>
	public class TerminalResult
	{

		private string standardOutput;

		private string errorOutput;

		private bool finished = false;

		/// <summary>
		/// 标准输出
		/// </summary>
		public string StandardOutput { get => standardOutput; set => standardOutput = value; }

		/// <summary>
		/// 标准错误
		/// </summary>
		public string ErrorOutput { get => errorOutput; set => errorOutput = value; }

		/// <summary>
		/// 是否执行完成
		/// </summary>
		public bool Finished { get => finished; set => finished = value; }
	}
}