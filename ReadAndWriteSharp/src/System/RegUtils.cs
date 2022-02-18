using Microsoft.Win32;
using Swsk33.ReadAndWriteSharp.System.Param;
using Swsk33.ReadAndWriteSharp.Util;
using System;

namespace Swsk33.ReadAndWriteSharp.System
{
	/// <summary>
	/// 注册表实用类
	/// </summary>
	public class RegUtils
	{
		/// <summary>
		/// 判断注册表某一项是否存在
		/// </summary>
		/// <param name="mainKey">主键，例如HKEY_LOCAL_MACHINE表示为Registry.LocalMachine</param>
		/// <param name="itemName">完整项名，例如HKEY_LOCAL_MACHINE\SOFTWARE\Clients的完整项名是：SOFTWARE\Clients</param>
		/// <returns>项是否存在</returns>
		public static bool IsItemExists(RegistryKey mainKey, string itemName)
		{
			if (itemName.StartsWith("\\"))
			{
				itemName = itemName.Substring(1, itemName.Length - 1);
			}
			itemName = FilePathUtils.RemovePathEndBackslash(itemName);
			string[] items = itemName.Split('\\');
			string[] subKeys;
			bool subKeyExists;
			foreach (string item in items)
			{
				subKeyExists = false;
				subKeys = mainKey.GetSubKeyNames();
				foreach (string subKey in subKeys)
				{
					if (subKey.Equals(item, StringComparison.CurrentCultureIgnoreCase))
					{
						mainKey = mainKey.OpenSubKey(item);
						subKeyExists = true;
						break;
					}
				}
				if (!subKeyExists)
				{
					mainKey.Close();
					return false;
				}
			}
			mainKey.Close();
			return true;
		}

		/// <summary>
		/// 判断注册表某一项的某个值是否存在
		/// </summary>
		/// <param name="mainKey">主键，例如HKEY_LOCAL_MACHINE表示为Registry.LocalMachine</param>
		/// <param name="itemName">完整项名，例如HKEY_LOCAL_MACHINE\SOFTWARE\Clients的完整项名是：SOFTWARE\Clients</param>
		/// <param name="valueName">值的名称</param>
		/// <returns>值是否存在</returns>
		public static bool IsValueExists(RegistryKey mainKey, string itemName, string valueName)
		{
			bool result = false;
			if (!IsItemExists(mainKey, itemName))
			{
				return false;
			}
			string[] valueNames = mainKey.OpenSubKey(itemName).GetValueNames();
			foreach (string name in valueNames)
			{
				if (name.Equals(valueName, StringComparison.CurrentCultureIgnoreCase))
				{
					result = true;
					break;
				}
			}
			mainKey.Close();
			return result;
		}

		/// <summary>
		/// 添加/移除开机启动项
		/// </summary>
		/// <param name="name">启动项名</param>
		/// <param name="exec">启动项执行的命令，可以是可执行文件绝对路径，也可以是命令+参数。如果是移除操作此项无效</param>
		/// <param name="isAddOption">此值为true时则进行添加相应启动项，值为false时则为移除相应启动项</param>
		/// <returns>是否操作成功</returns>
		public static bool OperateBootOption(string name, string exec, bool isAddOption)
		{
			string optionName = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
			RegistryKey key = Registry.LocalMachine.OpenSubKey(optionName, true);
			if (isAddOption)
			{
				key.SetValue(name, exec);
			}
			else
			{
				key.DeleteValue(name, false);
			}
			key.Close();
			return !isAddOption ^ IsValueExists(Registry.LocalMachine, optionName, name);
		}

		/// <summary>
		/// 添加/移除文件右键菜单
		/// </summary>
		/// <param name="name">右键菜单显示名</param>
		/// <param name="exec">执行命令，如果是移除操作此项无效（一般是"可执行文件路径" "参数"的格式，此处在编程时可以写作：\"可执行文件路径\" \"参数\"，其中参数中右键被选中的文件表示为"%l"</param>
		/// <param name="isAddOption">此值为true时则进行添加相应项，值为false时则为移除相应项</param>
		/// <returns>是否操作成功</returns>
		public static bool OperateFileRightMenu(string name, string exec, bool isAddOption)
		{
			string optionName = "*\\shell\\" + name;
			if (isAddOption)
			{
				RegistryKey key = Registry.ClassesRoot.CreateSubKey(optionName);
				key.SetValue("", name);
				RegistryKey commandKey = key.CreateSubKey("command");
				commandKey.SetValue("", exec);
				commandKey.Close();
				key.Close();
			}
			else
			{
				Registry.ClassesRoot.DeleteSubKeyTree(optionName, false);
			}
			return !isAddOption ^ IsItemExists(Registry.ClassesRoot, optionName);
		}

		/// <summary>
		/// 添加/移除文件右键菜单，且在添加操作时指定其图标
		/// </summary>
		/// <param name="name">右键菜单显示名</param>
		/// <param name="iconPath">图标文件位置，可以是ico文件也可以是exe可执行文件位置，如果是移除操作此项无效</param>
		/// <param name="exec">执行命令，如果是移除操作此项无效（一般是"可执行文件路径" "参数"的格式，此处在编程时可以写作：\"可执行文件路径\" \"参数\"，其中参数中右键被选中的文件表示为"%l"</param>
		/// <param name="isAddOption">此值为true时则进行添加相应项，值为false时则为移除相应项</param>
		/// <returns>是否操作成功</returns>
		public static bool OperateFileRightMenu(string name, string iconPath, string exec, bool isAddOption)
		{
			string optionName = "*\\shell\\" + name;
			if (isAddOption)
			{
				RegistryKey key = Registry.ClassesRoot.CreateSubKey(optionName);
				key.SetValue("", name);
				key.SetValue("Icon", iconPath);
				RegistryKey commandKey = key.CreateSubKey("command");
				commandKey.SetValue("", exec);
				commandKey.Close();
				key.Close();
			}
			else
			{
				Registry.ClassesRoot.DeleteSubKeyTree(optionName, false);
			}
			return !isAddOption ^ IsItemExists(Registry.ClassesRoot, optionName);
		}

		/// <summary>
		/// 添加/移除文件夹右键菜单
		/// </summary>
		/// <param name="name">右键菜单显示名</param>
		/// <param name="exec">执行命令，如果是移除操作此项无效（一般是"可执行文件路径" "参数"的格式，此处在编程时可以写作：\"可执行文件路径\" \"参数\"，其中参数中右键被选中的文件表示为"%l"</param>
		/// <param name="isAddOption">此值为true时则进行添加相应项，值为false时则为移除相应项</param>
		/// <returns>是否操作成功</returns>
		public static bool OperateDirectoryRightMenu(string name, string exec, bool isAddOption)
		{
			string optionName = "Directory\\shell\\" + name;
			if (isAddOption)
			{
				RegistryKey key = Registry.ClassesRoot.CreateSubKey(optionName);
				key.SetValue("", name);
				RegistryKey commandKey = key.CreateSubKey("command");
				commandKey.SetValue("", exec);
				commandKey.Close();
				key.Close();
			}
			else
			{
				Registry.ClassesRoot.DeleteSubKeyTree(optionName, false);
			}
			return !isAddOption ^ IsItemExists(Registry.ClassesRoot, optionName);
		}

		/// <summary>
		/// 添加/移除文件夹右键菜单，且在添加操作时指定其图标
		/// </summary>
		/// <param name="name">右键菜单显示名</param>
		/// <param name="iconPath">图标文件位置，可以是ico文件也可以是exe可执行文件位置，如果是移除操作此项无效</param>
		/// <param name="exec">执行命令，如果是移除操作此项无效（一般是"可执行文件路径" "参数"的格式，此处在编程时可以写作：\"可执行文件路径\" \"参数\"，其中参数中右键被选中的文件表示为"%l"</param>
		/// <param name="isAddOption">此值为true时则进行添加相应项，值为false时则为移除相应项</param>
		/// <returns>是否操作成功</returns>
		public static bool OperateDirectoryRightMenu(string name, string iconPath, string exec, bool isAddOption)
		{
			string optionName = "Directory\\shell\\" + name;
			if (isAddOption)
			{
				RegistryKey key = Registry.ClassesRoot.CreateSubKey(optionName);
				key.SetValue("", name);
				key.SetValue("Icon", iconPath);
				RegistryKey commandKey = key.CreateSubKey("command");
				commandKey.SetValue("", exec);
				commandKey.Close();
				key.Close();
			}
			else
			{
				Registry.ClassesRoot.DeleteSubKeyTree(optionName, false);
			}
			return !isAddOption ^ IsItemExists(Registry.ClassesRoot, optionName);
		}

		/// <summary>
		/// 添加/移除文件夹背景/桌面右键菜单
		/// </summary>
		/// <param name="name">右键菜单名</param>
		/// <param name="exec">执行命令，最好用双引号包围，如果是移除操作此项无效</param>
		/// <param name="isAddOption">此值为true时则进行添加相应项，值为false时则为移除相应项</param>
		/// <returns>是否操作成功</returns>
		public static bool OperateDirectoryBackgroundMenu(string name, string exec, bool isAddOption)
		{
			string optionName = "Directory\\Background\\shell\\" + name;
			if (isAddOption)
			{
				RegistryKey key = Registry.ClassesRoot.CreateSubKey(optionName);
				key.SetValue("", name);
				RegistryKey commandKey = key.CreateSubKey("command");
				commandKey.SetValue("", exec);
				commandKey.Close();
				key.Close();
			}
			else
			{
				Registry.ClassesRoot.DeleteSubKeyTree(optionName, false);
			}
			return !isAddOption ^ IsItemExists(Registry.ClassesRoot, optionName);
		}

		/// <summary>
		/// 添加/移除文件夹背景/桌面右键菜单，且在添加操作时指定其图标
		/// </summary>
		/// <param name="name">右键菜单名</param>
		/// <param name="iconPath">图标文件位置，可以是ico文件也可以是exe可执行文件位置，如果是移除操作此项无效</param>
		/// <param name="exec">执行命令，最好用双引号包围，如果是移除操作此项无效</param>
		/// <param name="isAddOption">此值为true时则进行添加相应项，值为false时则为移除相应项</param>
		/// <returns>是否操作成功</returns>
		public static bool OperateDirectoryBackgroundMenu(string name, string iconPath, string exec, bool isAddOption)
		{
			string optionName = "Directory\\Background\\shell\\" + name;
			if (isAddOption)
			{
				RegistryKey key = Registry.ClassesRoot.CreateSubKey(optionName);
				key.SetValue("", name);
				key.SetValue("Icon", iconPath);
				RegistryKey commandKey = key.CreateSubKey("command");
				commandKey.SetValue("", exec);
				commandKey.Close();
				key.Close();
			}
			else
			{
				Registry.ClassesRoot.DeleteSubKeyTree(optionName, false);
			}
			return !isAddOption ^ IsItemExists(Registry.ClassesRoot, optionName);
		}

		/// <summary>
		/// 添加/删除应用程序卸载信息条目
		/// </summary>
		/// <param name="appInfo">应用程序卸载信息实例，位于Swsk33.ReadAndWriteSharp.Model下，先实例化AppUninstallInfo类并设定其中的各个参数信息，然后传入，添加操作时其中除了应用程序显示名称和卸载命令之外都可以缺省，如果是移除操作就只需要设定其中的应用程序显示名称</param>
		/// <param name="isAddOption">此值为true时则进行添加相应项，值为false时则为移除相应项</param>
		/// <returns>是否操作成功</returns>
		public static bool OperateAppUninstallItem(AppUninstallInfo appInfo, bool isAddOption)
		{
			if ((isAddOption && appInfo.DisplayName == null && appInfo.UninstallString == null) || (!isAddOption && appInfo.DisplayName == null))
			{
				throw new Exception("添加应用程序信息时必须要有程序名和卸载命令！移除应用程序信息时必须要有程序名！");
			}
			string optionName = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + appInfo.DisplayName;
			if (isAddOption)
			{
				RegistryKey key = Registry.LocalMachine.CreateSubKey(optionName);
				key.SetValue("DisplayName", appInfo.DisplayName);
				key.SetValue("UninstallString", appInfo.UninstallString);
				if (appInfo.DisplayIcon != null)
				{
					key.SetValue("DisplayIcon", appInfo.DisplayIcon);
				}
				if (appInfo.DisplayVersion != null)
				{
					key.SetValue("DisplayVersion", appInfo.DisplayVersion);
				}
				if (appInfo.Publisher != null)
				{
					key.SetValue("Publisher", appInfo.Publisher);
				}
				if (appInfo.EstimatedSize != 0)
				{
					key.SetValue("EstimatedSize", appInfo.EstimatedSize, RegistryValueKind.DWord);
				}
				if (appInfo.InstallPath != null)
				{
					key.SetValue("InstallLocation", appInfo.InstallPath);
				}
				if (appInfo.ModifyPath != null)
				{
					key.SetValue("ModifyPath", appInfo.ModifyPath);
				}
				int noModify = 1;
				int noRepair = 1;
				if (!appInfo.NoModify)
				{
					noModify = 0;
				}
				if (!appInfo.NoRepair)
				{
					noRepair = 0;
				}
				key.SetValue("NoModify", noModify, RegistryValueKind.DWord);
				key.SetValue("NoRepair", noRepair, RegistryValueKind.DWord);
				key.Close();
			}
			else
			{
				Registry.LocalMachine.DeleteSubKeyTree(optionName, false);
			}
			return !isAddOption ^ IsItemExists(Registry.LocalMachine, optionName);
		}

		/// <summary>
		/// 获取系统环境变量
		/// </summary>
		/// <param name="name">环境变量名</param>
		/// <returns>该环境变量的值，若指定环境变量名不存在则返回null</returns>
		public static string GetEnvironmentVariable(string name)
		{
			string optionName = "SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment";
			if (!IsValueExists(Registry.LocalMachine, optionName, name))
			{
				return null;
			}
			RegistryKey key = Registry.LocalMachine.OpenSubKey(optionName);
			string value = key.GetValue(name, "", RegistryValueOptions.DoNotExpandEnvironmentNames).ToString();
			key.Close();
			return value;
		}

		/// <summary>
		/// 获取系统Path变量值
		/// </summary>
		/// <param name="expandVariables">是否展开Path中的变量形式，例如把%JAVA_HOME%转为它对应的实际的路径值</param>
		/// <returns>Path变量值，为数组形式</returns>
		public static string[] GetPathVariable(bool expandVariables)
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment");
			RegistryValueOptions option;
			if (expandVariables)
			{
				option = RegistryValueOptions.None;
			}
			else
			{
				option = RegistryValueOptions.DoNotExpandEnvironmentNames;
			}
			string value = key.GetValue("Path", "", option).ToString();
			while (value.EndsWith(";"))
			{
				value = value.Substring(0, value.Length - 1);
			}
			string[] values = value.Split(';');
			key.Close();
			return values;
		}
	}
}