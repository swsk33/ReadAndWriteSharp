# ReadAndWriteSharp

## 介绍
使用C#，基于 .NET Framework 的文件读写与实用类库。

## 使用方法
下载发行版中dll文件，在vs2019/vs2022中项目-引用里面添加即可。

或者在nuget包管理中搜索`ReadAndWriteSharp`下载。

## 公用类及其方法

### `Swsk33.ReadAndWriteSharp.Util` - 内部实用工具

#### `StringUtils` - 字符串处理实用类
- `static string SurroundBySingleQuotes(string origin)` - 使用单引号包围字符串
- `static string SurroundByDoubleQuotes(string origin)` - 使用双引号包围字符串
- `static bool IsEmpty(string originString)` - 判断字符串是否为空
- `static string GetStringMD5(string origin)` - 计算字符串MD5值
- `static string GetStringSHA1(string origin)` - 计算字符串SHA1值

#### `FilePathUtils` - 文件路径字符串实用类

- `static string RemovePathEndSlash(string origin)` - 移除原字符串末尾斜杠(`/`)，若原字符串不以斜杠结尾则不作任何操作
- `static string RemovePathEndBackslash(string origin)` - 移除原字符串末尾反斜杠(`\`)，若原字符串不以反斜杠结尾则不作任何操作
- `static string GetFileName(string filePath)` - 传入文件（夹）路径（绝对或者相对）获取文件（夹）名
- `static string GetFileFormat(string filePath)` - 获取文件扩展名，不带`.`

### `Swsk33.ReadAndWriteSharp.FileUtil` - 文件实用工具

#### `TextFileReader` - 文本文件读取器

- `static int GetFileLine(string filePath)` - 读取文本文件的行数
- `static string ReadSpecificLine(string filePath, int line)` - 读取文本文件指定行
- `static string ReadSpecificLine(string filePath, int line, Encoding encoding)` - 使用特定的编码读取文本文档指定行
- `static string ReadSpecificRange(string filePath, int start, int end)` - 读取指定行数范围内的内容并以字符串形式储存，包含起始行和终止行
- `static string ReadSpecificRange(string filePath, int start, int end, Encoding encoding)` - 以指定的编码读取指定行数范围内的内容并以字符串形式储存，包含起始行和终止行
#### `TextFileWriter` - 文本文件写入器
- `static bool ReplaceSpecificLine(String filePath, string content, int line)` - 用指定内容替换文件指定行
- `static bool ReplaceSpecificLine(String filePath, string content, int line, Encoding encoding)` - 使用指定的字符编码，用指定内容替换文件指定行
- `static bool AppendText(string filePath, string content)` - 向文本文件末尾追加文本
- `static bool AppendText(string filePath, string content, Encoding encoding)` - 使用指定的字符编码，向文本文件末尾追加文本
- `static bool InsertText(string filePath, string content, int line)` - 在指定行之后插入一行指定文本
- `static bool InsertText(string filePath, string content, int line, Encoding encoding)` - 使用指定字符编码，在指定行之后插入一行指定文本
- `static bool RemoveSpecificLine(string filePath, int line)` - 移除文件指定行内容
- `static bool ClearAll(string filePath)` - 将指定文件内容清空

#### `BinaryUtils` - 二进制文件工具类

- `static byte[] ReadBinaryFile(string filePath)` - 读取二进制文件为字节数组
- `static bool WriteBinaryFile(string filePath, byte[] content)` - 将二进制数据写入指定文件，文件不存在将创建，存在将被覆盖
- `static bool CopyFile(string origin, string destination)` - 复制文件，目标位置文件存在则会被覆盖
- `static bool WriteObjectToFile<T>(string filePath, T data)` - 将可序列化的对象写入文件，文件不存在将创建，存在将被覆盖
- `static T ReadObjectFromFile<T>(string filePath)` - 从文件中读取数据并反序列化为相应的对象
- `static bool ExtractNormalFileInResx(byte[] resource, string path)` - 释放嵌入在resx里面的普通类型文件
- `static bool ExtractAudioFileInResx(Stream fileInResx, string path)` - 释放嵌入在resx文件里面的音频资源文件
- `static bool ExtractImageFileInResx(Bitmap image, string path)` - 释放嵌入在resx里的图片资源文件
- `static bool ExtractTextFileInResx(string textFile, string path)` - 释放嵌入在resx中的文本文件
- `static bool IsBinaryFile(string origin)` 检测一个文件是否是二进制文件，即判断是否是不可用文本方式打开的文件
- `static string GetFileMD5(string filePath)` - 获取文件MD5值
- `static string GetFileSHA1(string filePath)` - 获取文件SHA1值

#### `DirectoryUtils` - 文件夹实用类

- `static string[] GetAllFilesInDirectory(string directoryPath)` 递归获取一个文件夹中全部文件
- `static string[] GetAllFilesInDirectory(string directoryPath, string[] excludeFileName, string[] exculdeDirectoryName)` - 递归获取一个文件夹中全部文件，并指定要排除的文件名和文件夹名列表
- `static bool CopyDirectory(string origin, string destination)` 复制一整个文件夹
- `static void CopyDirectoryAsync(string origin, string destination, CopyDirectoryStatus status)` 异步复制一整个文件夹

#### `PropertiesOperator` - properties文件读写类

- `static Dictionary<string, string> ReadPropertiesFile(string filePath)` - 读取一个properties文件，将里面的键值读取储存为一个字典返回
- `static void WritePropertiesFile(Dictionary<string, string> keyValues, string filePath)` - 把一个字典对象写入为一个properties文件，文件不存在将创建，存在则直接覆盖

#### `Swsk33.ReadAndWriteSharp.FileUtil.Param` - 一些用于提供文件复制的参数类

##### `CopyDirectoryStatus` - 文件夹异步复制的状态类，用于作为参数传入异步复制文件夹方法之后，可以获取当时正在复制的文件以及是否复制完成

### `Swsk33.ReadAndWriteSharp.System` - 系统实用工具

#### `TerminalUtils` - 终端实用类

- `static string[] RunCommand(string command, string args)` - 调用命令行并获取执行结果，该方法为同步方法，会堵塞线程
- `static string[] RunCommand(string command, string[] args)` - 调用命令行并获取执行结果，该方法为同步方法，会堵塞线程
- `static void RunCommandAsynchronously(string command, string args, TerminalResult result)` - 异步执行命令行并将输出结果实时储存在一个TerminalResult类型的实例中\
- `static void RunCommandAsynchronously(string command, string[] args, TerminalResult result)` - 异步执行命令行并将输出结果实时储存在一个TerminalResult类型的实例中

#### `RegUtils` - 注册表实用类

- `static bool IsItemExists(RegistryKey mainKey, string itemName)` - 判断注册表项是否存在
- `static bool IsValueExists(RegistryKey mainKey, string itemName, string valueName)` - 判断注册表某一项的某个值是否存在
- `static bool OperateBootOption(string name, string exec, bool isAddOption)` - 添加/移除开机启动项
- `static bool OperateFileRightMenu(string name, string exec, bool isAddOption)` - 添加/移除文件右键菜单
- `static bool OperateFileRightMenu(string name, string iconPath, string exec, bool isAddOption)` - 添加/移除文件右键菜单，且在添加操作时指定其图标
- `static bool OperateDirectoryRightMenu(string name, string exec, bool isAddOption)` - 添加/移除文件夹右键菜单
- `static bool OperateDirectoryRightMenu(string name, string iconPath, string exec, bool isAddOption)` - 添加/移除文件夹右键菜单，且在添加操作时指定其图标
- `static bool OperateDirectoryBackgroundMenu(string name, string exec, bool isAddOption)` - 添加/移除文件夹背景/桌面右键菜单
- `static bool OperateDirectoryBackgroundMenu(string name, string iconPath, string exec, bool isAddOption)` - 添加/移除文件夹背景/桌面右键菜单，且在添加操作时指定其图标
- `static bool OperateAppUninstallItem(AppUninstallInfo appInfo, bool isAddOption)` - 添加/删除应用程序卸载信息条目
- `static string GetEnvironmentVariable(string name)` - 获取系统环境变量但不展开变量引用
- `static string GetEnvironmentVariable(string name, bool expandVariables)` - 获取环境变量，指定是否展开变量引用
- `static string[] GetPathVariable(bool expandVariables)` - 获取系统`Path`变量值

#### `Swsk33.ReadAndWriteSharp.System.Param` - 一些用于提供系统工具类方法参数的命名空间

##### `AppUninstallInfo` - 软件卸载信息类

#### `Swsk33.ReadAndWriteSharp.System.Result` - 一些用于提供系统工具类结果参数的命名空间

##### `TerminalResult` - 终端实用类的输出结果

### Swsk33.ReadAndWriteSharp.Network - 网络实用工具

#### NetworkUtils - 网络实用类

- `static void SetSecurityProtocol(SecurityProtocolType securityProtocolType)` - 设定安全协议
- `static HttpResponseMessage SendCustomRequest(string url, HttpMethod method, Dictionary<string, string> headers, HttpContent requestBody)` - 发送完全自定义请求
- `static string SendGetRequest(string url)` - 发送`GET`请求
- `static string SendGetRequest(string url, Dictionary<string, string> headers)` - 发送自定义请求头的`GET`请求
- `static string SendPostRequest(string url, string contentType, string requestBody)` - 发送文本内容的`POST`请求
- `static string SendPostRequest(string url, Dictionary<string, string> headers, string requestBody)` - 发送自定义请求头的文本内容的`POST`请求
- `static bool DownloadFile(string url, string filePath)` - 下载文件
- `static bool DownloadFile(string url, Dictionary<string, string> headers, string filePath)` - 以一个自定义的请求头下载文件
- `static string UploadFile(string url, Dictionary<string, string> textArea, Dictionary<string, string> fileArea)` - 上传文件
- `static string UploadFile(string url, Dictionary<string, string> textArea, Dictionary<string, string> fileArea, Dictionary<string, string> headers)` - 以一个自定义的请求头上传文件

#### `Swsk33.ReadAndWriteSharp.Network.Param` - 提供网络实用类参数的命名空间

##### `ContentTypeValue` - 网络请求类的请求内容类型常量值

##### `UserAgentValue` - 网络请求类中的常用UA值

------

**在vs中使用这些类即可显示其中详细的的方法与说明，前提是引用类库时必须将下载的dll和xml文件放一起，或者直接使用nuget包。**

>最后更新 - 2023.1.4