namespace Swsk33.ReadAndWriteSharp.Model
{
	/// <summary>
	/// 软件卸载信息类
	/// </summary>
	public class AppUninstallInfo
	{

		private string displayName;

		private string displayIcon;

		private string displayVersion;

		private string publisher;

		private long estimatedSize;

		private string installPath;

		private string uninstallString;

		private string modifyPath;

		private bool noModify = true;

		private bool noRepair = true;

		/// <summary>
		/// 普通构造器
		/// </summary>
		public AppUninstallInfo()
		{

		}

		/// <summary>
		/// 简单构造器
		/// </summary>
		/// <param name="displayName">设定显示名称</param>
		/// <param name="uninstallString">设定卸载命令</param>
		public AppUninstallInfo(string displayName, string uninstallString)
		{
			this.displayName = displayName;
			this.uninstallString = uninstallString;
		}

		/// <summary>
		/// 显示名称
		/// </summary>
		public string DisplayName
		{
			get
			{
				return displayName;
			}

			set
			{
				displayName = value;
			}
		}

		/// <summary>
		/// 显示图标
		/// </summary>
		public string DisplayIcon
		{
			get
			{
				return displayIcon;
			}

			set
			{
				displayIcon = value;
			}
		}

		/// <summary>
		/// 显示版本
		/// </summary>
		public string DisplayVersion
		{
			get
			{
				return displayVersion;
			}

			set
			{
				displayVersion = value;
			}
		}

		/// <summary>
		/// 发布者
		/// </summary>
		public string Publisher
		{
			get
			{
				return publisher;
			}

			set
			{
				publisher = value;
			}
		}

		/// <summary>
		/// 软件大小（单位kb）
		/// </summary>
		public long EstimatedSize
		{
			get
			{
				return estimatedSize;
			}

			set
			{
				estimatedSize = value;
			}
		}

		/// <summary>
		/// 软件安装位置
		/// </summary>
		public string InstallPath
		{
			get
			{
				return installPath;
			}

			set
			{
				installPath = value;
			}
		}

		/// <summary>
		/// 卸载命令
		/// </summary>
		public string UninstallString
		{
			get
			{
				return uninstallString;
			}

			set
			{
				uninstallString = value;
			}
		}

		/// <summary>
		/// 修改命令
		/// </summary>
		public string ModifyPath
		{
			get
			{
				return modifyPath;
			}

			set
			{
				modifyPath = value;
			}
		}

		/// <summary>
		/// 是否禁用修改
		/// </summary>
		public bool NoModify
		{
			get
			{
				return noModify;
			}

			set
			{
				noModify = value;
			}
		}

		/// <summary>
		/// 是否禁用修复
		/// </summary>
		public bool NoRepair
		{
			get
			{
				return noRepair;
			}

			set
			{
				noRepair = value;
			}
		}
	}
}