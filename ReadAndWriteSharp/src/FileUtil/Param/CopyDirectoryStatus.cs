namespace Swsk33.ReadAndWriteSharp.FileUtil.Param
{
	/// <summary>
	/// 文件夹异步复制的状态类，用于作为参数传入异步复制文件夹方法之后，可以获取当时正在复制的文件以及是否复制完成
	/// </summary>
	public class CopyDirectoryStatus
	{
		private string currentFile;

		private bool copyDone = false;

		private double schedule;

		/// <summary>
		/// 当前正在被复制的文件的原路径
		/// </summary>
		public string CurrentFile { get => currentFile; set => currentFile = value; }

		/// <summary>
		/// 是否复制完成
		/// </summary>
		public bool CopyDone { get => copyDone; set => copyDone = value; }

		/// <summary>
		/// 当前复制进度，单位为百分比，100则为完成
		/// </summary>
		public double Schedule { get => schedule; set => schedule = value; }
	}
}