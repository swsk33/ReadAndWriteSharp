using Swsk33.ReadAndWriteSharp.Network.Param;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Swsk33.ReadAndWriteSharp.Network
{
	/// <summary>
	/// 网络实用类
	/// </summary>
	public class NetworkUtils
	{
		/// <summary>
		/// 修补url，当url没有以http或者https开头，就会补上协议头，否则不执行任何操作
		/// </summary>
		/// <param name="origin">传入网址</param>
		/// <returns>修补后网址</returns>
		private static string fixUrl(string origin)
		{
			if (!origin.StartsWith("http://") && !origin.StartsWith("https://"))
			{
				origin = "http://" + origin;
			}
			return origin;
		}

		/// <summary>
		/// 设定指定请求的标头
		/// </summary>
		/// <param name="request">请求对象</param>
		/// <param name="headers">自定义请求头</param>
		private static void setHeaders(HttpWebRequest request, Dictionary<string, string> headers)
		{
			foreach (string key in headers.Keys)
			{
				if (key.Equals("Accept", StringComparison.OrdinalIgnoreCase))
				{
					request.Accept = headers[key];
				}
				else if (key.Equals("Connection", StringComparison.OrdinalIgnoreCase))
				{
					request.Connection = headers[key];
				}
				else if (key.Equals("Content-Length", StringComparison.OrdinalIgnoreCase))
				{
					request.ContentLength = long.Parse(headers[key]);
				}
				else if (key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
				{
					request.ContentType = headers[key];
				}
				else if (key.Equals("Date", StringComparison.OrdinalIgnoreCase))
				{
					request.Date = DateTime.Parse(headers[key]);
				}
				else if (key.Equals("Expect", StringComparison.OrdinalIgnoreCase))
				{
					request.Expect = headers[key];
				}
				else if (key.Equals("Host", StringComparison.OrdinalIgnoreCase))
				{
					request.Host = headers[key];
				}
				else if (key.Equals("If-Modified-Since", StringComparison.OrdinalIgnoreCase))
				{
					request.IfModifiedSince = DateTime.Parse(headers[key]);
				}
				else if (key.Equals("Referer", StringComparison.OrdinalIgnoreCase))
				{
					request.Referer = headers[key];
				}
				else if (key.Equals("Transfer-Encoding", StringComparison.OrdinalIgnoreCase))
				{
					request.TransferEncoding = headers[key];
				}
				else if (key.Equals("User-Agent", StringComparison.OrdinalIgnoreCase))
				{
					request.UserAgent = headers[key];
				}
				else if (key.Equals("Proxy-Connection", StringComparison.OrdinalIgnoreCase))
				{
					string proxyUrl = headers[key];
					string proxyHost = proxyUrl;
					int proxyPort;
					if (proxyUrl.Contains(":"))
					{
						proxyHost = proxyUrl.Substring(0, proxyUrl.IndexOf(":"));
						proxyPort = int.Parse(proxyUrl.Substring(proxyUrl.IndexOf(":") + 1));
					}
					else
					{
						if (proxyUrl.StartsWith("https://"))
						{
							proxyPort = 443;
						}
						else
						{
							proxyPort = 80;
						}
					}
					request.Proxy = new WebProxy(proxyHost, proxyPort);
				}
				else
				{
					request.Headers.Add(key, headers[key]);
				}
			}
		}

		/// <summary>
		/// 设置网络安全协议
		/// </summary>
		/// <param name="securityProtocolType">
		/// <para>SecurityProtocolType枚举，枚举值如下：</para>
		/// <para>SecurityProtocolType.Tls - TLS 1.0传输协议</para>
		/// <para>SecurityProtocolType.Tls11 - TLS 1.1传输协议</para>
		/// <para>SecurityProtocolType.Tls12 - TLS 1.2传输协议</para>
		/// <para>SecurityProtocolType.Ssl3 - SSL 3.0安全协议</para>
		/// </param>
		public static void SetSecurityProtocol(SecurityProtocolType securityProtocolType)
		{
			ServicePointManager.SecurityProtocol = securityProtocolType;
		}

		/// <summary>
		/// 发送GET请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <returns>响应内容</returns>
		public static string SendGetRequest(string url)
		{
			url = fixUrl(url);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = RequestMethod.GET.ToString();
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
			url = fixUrl(url);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = RequestMethod.GET.ToString();
			setHeaders(request, headers);
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
		/// <param name="contentType">请求数据类型，ContentTypeValue类的常量值</param>
		/// <param name="requestBody">请求体，注意的是不同的内容类型有不同的请求体格式，例如application/x-www-form-urlencoded中请求体通常是：键1=值1&amp;键2=值2&amp;...</param>
		/// <returns>响应内容</returns>
		public static string SendPostRequest(string url, string contentType, string requestBody)
		{
			url = fixUrl(url);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = RequestMethod.POST.ToString();
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
		/// 发送自定义请求头的POST请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="headers">存放自定义的请求头的键值对，其中可以设定Content-Type、User-Agent值等等</param>
		/// <param name="requestBody">请求体，注意的是不同的内容类型有不同的请求体格式，例如application/x-www-form-urlencoded中请求体通常是：键1=值1&amp;键2=值2&amp;...</param>
		/// <returns>响应内容</returns>
		public static string SendPostRequest(string url, Dictionary<string, string> headers, string requestBody)
		{
			url = fixUrl(url);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = RequestMethod.POST.ToString();
			setHeaders(request, headers);
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
			url = fixUrl(url);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = RequestMethod.GET.ToString();
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
			url = fixUrl(url);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = RequestMethod.GET.ToString();
			setHeaders(request, headers);
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
		/// 发送完全自定义的请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="method">请求类型</param>
		/// <param name="headers">存放自定义的请求头的键值对，其中可以设定Content-Type、User-Agent值等等</param>
		/// <param name="requestBody">请求体，自行定义请求体数据，然后写入Stream传入进来作为请求体发送</param>
		/// <returns>响应内容的流数据</returns>
		public static Stream SendCustomRequest(string url, RequestMethod method, Dictionary<string, string> headers, Stream requestBody)
		{
			url = fixUrl(url);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = method.ToString();
			setHeaders(request, headers);
			byte[] bodyData = new byte[requestBody.Length];
			requestBody.Position = 0;
			requestBody.Read(bodyData, 0, bodyData.Length);
			requestBody.Close();
			request.GetRequestStream().Write(bodyData, 0, bodyData.Length);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			return response.GetResponseStream();
		}
	}
}