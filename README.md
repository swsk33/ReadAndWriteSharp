# ReadAndWriteSharp

#### 介绍
使用C#，基于 .NET Framework 的文件读写类库。<br>
是使用C#对[ReadAndWriteJ](https://gitee.com/swsk33/ReadAndWriteJ)的实现。<br>
使用 .NET 5 的dll的请前往[ReadAndWriteDotNet](https://gitee.com/swsk33/ReadAndWriteDotNet)<br>

#### 使用方法
下载发行版中dll文件，在vs2019中项目-引用里面添加即可。<br>
然后使用下列语句添加命名空间：<br>
```
using Swsk33.ReadAndWriteSharp.Model;
using Swsk33.ReadAndWriteSharp;
```

#### 公用类
位于命名空间*Swsk33.ReadAndWriteSharp*下：<br>
**TextReader:** 文本文件读取器<br>
**TextWriter:** 文本文件写入器<br>
**BinaryUtils:** 二进制文件工具类<br>
**TerminalUtils:** 终端实用类<br>
位于命名空间*Swsk33.ReadAndWriteSharp.Model*下：<br>
**TerminalResult：** 终端实用类的输出结果类<br>
**DirInfo：** 文件夹信息类，用于BinaryUtils类中文件夹方法的使用形参<br>
在vs中使用这些类即可显示其中的方法与说明。<br>