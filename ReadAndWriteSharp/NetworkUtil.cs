using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Swsk33.ReadAndWriteSharp
{
	/// <summary>
	/// 网络实用类
	/// </summary>
	public class NetworkUtil
	{
		/// <summary>
		/// 发送GET请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <returns>响应内容</returns>
		public static string SendGetRequest(string url)
		{
			string urlProtocol; // 网址协议
			string urlContent; // 网址内容
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
			{
				urlProtocol = "http://";
				urlContent = url;
				url = urlProtocol + url;
			}
			else
			{
				urlProtocol = url.Substring(0, url.IndexOf("//") + 2);
				urlContent = url.Substring(url.IndexOf("//") + 2);
			}
			string urlHost;
			if (urlContent.Contains("/"))
			{
				urlHost = urlContent.Substring(0, urlContent.IndexOf("/"));
			}
			else
			{
				urlHost = urlContent;
			}
			string urlReferer = urlProtocol + urlHost + "/";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			request.Referer = urlReferer;
			request.Host = urlHost;
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			return result;
		}

		/// <summary>
		/// 以一个特定的User-Agent发送GET请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="userAgent">User-Agent，可以使用Swsk33.ReadAndWriteSharp.Param下的UserAgentValue中的常量值也可以自定义</param>
		/// <returns>响应内容</returns>
		public static string SendGetRequest(string url, string userAgent)
		{
			string urlProtocol; // 网址协议
			string urlContent; // 网址内容
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
			{
				urlProtocol = "http://";
				urlContent = url;
				url = urlProtocol + url;
			}
			else
			{
				urlProtocol = url.Substring(0, url.IndexOf("//") + 2);
				urlContent = url.Substring(url.IndexOf("//") + 2);
			}
			string urlHost;
			if (urlContent.Contains("/"))
			{
				urlHost = urlContent.Substring(0, urlContent.IndexOf("/"));
			}
			else
			{
				urlHost = urlContent;
			}
			string urlReferer = urlProtocol + urlHost + "/";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			request.Referer = urlReferer;
			request.Host = urlHost;
			request.UserAgent = userAgent;
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			return result;
		}

		/// <summary>
		/// 发送自定义请求头的GET请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="headers">存放自定义的请求头的键值对，其中可以设定Content-Type、User-Agent值等等</param>
		/// <returns>响应内容</returns>
		public static string SendGetRequest(string url, Dictionary<string, string> headers)
		{
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
			{
				url = "http://" + url;
			}
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			foreach (string key in headers.Keys)
			{
				request.Headers.Add(key, headers[key]);
			}
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			return result;
		}

		/// <summary>
		/// 发送POST请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="contentType">请求数据类型，可以使用Swsk33.ReadAndWriteSharp.Param下的中的ContentTypeValue类的常量值</param>
		/// <param name="requestBody">请求体，注意的是不同的内容类型有不同的请求体格式，例如application/x-www-form-urlencoded中请求体通常是：键1=值1&amp;键2=值2&amp;...</param>
		/// <returns>响应内容</returns>
		public static string SendPostRequest(string url, string contentType, string requestBody)
		{
			string urlProtocol; // 网址协议
			string urlContent; // 网址内容
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
			{
				urlProtocol = "http://";
				urlContent = url;
				url = urlProtocol + url;
			}
			else
			{
				urlProtocol = url.Substring(0, url.IndexOf("//") + 2);
				urlContent = url.Substring(url.IndexOf("//") + 2);
			}
			string urlHost;
			if (urlContent.Contains("/"))
			{
				urlHost = urlContent.Substring(0, urlContent.IndexOf("/"));
			}
			else
			{
				urlHost = urlContent;
			}
			string urlReferer = urlProtocol + urlHost + "/";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.Referer = urlReferer;
			request.Host = urlHost;
			request.ContentType = contentType;
			byte[] data = Encoding.UTF8.GetBytes(requestBody);
			using (Stream stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			return result;
		}

		/// <summary>
		/// 以一个特定的User-Agent发送POST请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="userAgent">User-Agent，可以使用Swsk33.ReadAndWriteSharp.Param下的UserAgentValue中的常量值也可以自定义</param>
		/// <param name="contentType">请求数据类型，可以使用Swsk33.ReadAndWriteSharp.Param下的中的ContentTypeValue类的常量值</param>
		/// <param name="requestBody">请求体，注意的是不同的内容类型有不同的请求体格式，例如application/x-www-form-urlencoded中请求体通常是：键1=值1&amp;键2=值2&amp;...</param>
		/// <returns>响应内容</returns>
		public static string SendPostRequest(string url, string userAgent, string contentType, string requestBody)
		{
			string urlProtocol; // 网址协议
			string urlContent; // 网址内容
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
			{
				urlProtocol = "http://";
				urlContent = url;
				url = urlProtocol + url;
			}
			else
			{
				urlProtocol = url.Substring(0, url.IndexOf("//") + 2);
				urlContent = url.Substring(url.IndexOf("//") + 2);
			}
			string urlHost;
			if (urlContent.Contains("/"))
			{
				urlHost = urlContent.Substring(0, urlContent.IndexOf("/"));
			}
			else
			{
				urlHost = urlContent;
			}
			string urlReferer = urlProtocol + urlHost + "/";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.Referer = urlReferer;
			request.Host = urlHost;
			request.ContentType = contentType;
			request.UserAgent = userAgent;
			byte[] data = Encoding.UTF8.GetBytes(requestBody);
			using (Stream stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			return result;
		}

		/// <summary>
		/// 发送自定义请求头的POST请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="headers">存放自定义的请求头的键值对，其中可以设定Content-Type、User-Agent值等等</param>
		/// <param name="requestBody">请求体，注意的是不同的内容类型有不同的请求体格式，例如application/x-www-form-urlencoded中请求体通常是：键1=值1&amp;键2=值2&amp;...</param>
		/// <returns>响应内容</returns>
		public static string SendPostRequest(string url, Dictionary<string, string> headers, string requestBody)
		{
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
			{
				url = "http://" + url;
			}
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			foreach (string key in headers.Keys)
			{
				request.Headers.Add(key, headers[key]);
			}
			byte[] data = Encoding.UTF8.GetBytes(requestBody);
			using (Stream stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			return result;
		}

		/// <summary>
		/// 下载文件
		/// </summary>
		/// <param name="url">下载地址</param>
		/// <param name="filePath">文件保存位置，已存在将覆盖</param>
		/// <returns>是否下载成功</returns>
		public static bool DownloadFile(string url, string filePath)
		{
			string urlProtocol; // 网址协议
			string urlContent; // 网址内容
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
			{
				urlProtocol = "http://";
				urlContent = url;
				url = urlProtocol + url;
			}
			else
			{
				urlProtocol = url.Substring(0, url.IndexOf("//") + 2);
				urlContent = url.Substring(url.IndexOf("//") + 2);
			}
			string urlHost;
			if (urlContent.Contains("/"))
			{
				urlHost = urlContent.Substring(0, urlContent.IndexOf("/"));
			}
			else
			{
				urlHost = urlContent;
			}
			string urlReferer = urlProtocol + urlHost + "/";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			request.Referer = urlReferer;
			request.Host = urlHost;
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			Stream fileStream = new FileStream(filePath, FileMode.Create);
			byte[] dataByte = new byte[1024];
			int size;
			while ((size = responseStream.Read(dataByte, 0, dataByte.Length)) > 0)
			{
				fileStream.Write(dataByte, 0, size);
			}
			responseStream.Close();
			fileStream.Close();
			return File.Exists(filePath);
		}

		/// <summary>
		/// 以一个特定的User-Agent下载文件
		/// </summary>
		/// <param name="url">下载地址</param>
		/// <param name="userAgent">User-Agent，可以使用Swsk33.ReadAndWriteSharp.Param下的UserAgentValue中的常量值也可以自定义</param>
		/// <param name="filePath">文件保存位置，已存在将覆盖</param>
		/// <returns>是否下载成功</returns>
		public static bool DownloadFile(string url, string userAgent, string filePath)
		{
			string urlProtocol; // 网址协议
			string urlContent; // 网址内容
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
			{
				urlProtocol = "http://";
				urlContent = url;
				url = urlProtocol + url;
			}
			else
			{
				urlProtocol = url.Substring(0, url.IndexOf("//") + 2);
				urlContent = url.Substring(url.IndexOf("//") + 2);
			}
			string urlHost;
			if (urlContent.Contains("/"))
			{
				urlHost = urlContent.Substring(0, urlContent.IndexOf("/"));
			}
			else
			{
				urlHost = urlContent;
			}
			string urlReferer = urlProtocol + urlHost + "/";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			request.Referer = urlReferer;
			request.Host = urlHost;
			request.UserAgent = userAgent;
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			Stream fileStream = new FileStream(filePath, FileMode.Create);
			byte[] dataByte = new byte[1024];
			int size;
			while ((size = responseStream.Read(dataByte, 0, dataByte.Length)) > 0)
			{
				fileStream.Write(dataByte, 0, size);
			}
			responseStream.Close();
			fileStream.Close();
			return File.Exists(filePath);
		}

		/// <summary>
		/// 以一个特定的请求头下载文件
		/// </summary>
		/// <param name="url">下载地址</param>
		/// /// <param name="headers">存放自定义的请求头的键值对，其中可以设定Content-Type、User-Agent值等等</param>
		/// <param name="filePath">文件保存位置，已存在将覆盖</param>
		/// <returns>是否下载成功</returns>
		public static bool DownloadFile(string url, Dictionary<string, string> headers, string filePath)
		{
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
			{
				url = "http://" + url;
			}
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			foreach (string key in headers.Keys)
			{
				request.Headers.Add(key, headers[key]);
			}
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			Stream fileStream = new FileStream(filePath, FileMode.Create);
			byte[] dataByte = new byte[1024];
			int size;
			while ((size = responseStream.Read(dataByte, 0, dataByte.Length)) > 0)
			{
				fileStream.Write(dataByte, 0, size);
			}
			responseStream.Close();
			fileStream.Close();
			return File.Exists(filePath);
		}
	}
}