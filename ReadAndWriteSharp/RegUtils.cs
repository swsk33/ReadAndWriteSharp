using Microsoft.Win32;

namespace Swsk33.ReadAndWriteSharp
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
		/// <param name="itemName">完整项名</param>
		/// <returns>项是否存在</returns>
		public static bool IsItemExists(RegistryKey mainKey, string itemName)
		{
			bool result = false;
			string simpleItemName;
			string[] itemNames;
			if (itemName.Contains("\\"))
			{
				simpleItemName = itemName.Substring(itemName.LastIndexOf("\\") + 1, itemName.Length - itemName.LastIndexOf("\\") - 1);
				itemNames = mainKey.OpenSubKey(itemName.Substring(0, itemName.LastIndexOf("\\"))).GetSubKeyNames();
			}
			else
			{
				simpleItemName = itemName;
				itemNames = mainKey.GetSubKeyNames();
			}
			foreach (string name in itemNames)
			{
				if (name.Equals(simpleItemName))
				{
					result = true;
					break;
				}
			}
			mainKey.Close();
			return result;
		}

		/// <summary>
		/// 判断注册表某一项的某个值是否存在
		/// </summary>
		/// <param name="mainKey">主键，例如HKEY_LOCAL_MACHINE表示为Registry.LocalMachine</param>
		/// <param name="itemName">完整项名</param>
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
				if (name.Equals(valueName))
				{
					result = true;
					break;
				}
			}
			mainKey.Close();
			return result;
		}
	}
}
