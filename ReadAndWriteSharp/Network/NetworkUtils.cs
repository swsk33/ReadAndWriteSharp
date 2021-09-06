using Swsk33.ReadAndWriteSharp.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
		/// 发送完全自定义的请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="method">请求类型，例如GET请求就传入HttpMethod.Get</param>
		/// <param name="headers">存放自定义的请求头的键值对，其中可以设定Content-Type、User-Agent值等等，不设定自定义请求头可以传入null</param>
		/// <param name="requestBody">请求体，自行定义请求体数据，然后写入Stream传入进来作为请求体发送，没有请求体（例如GET请求）传入null</param>
		/// <returns>响应内容的流数据</returns>
		public static Stream SendCustomRequest(string url, HttpMethod method, Dictionary<string, string> headers, Stream requestBody)
		{
			url = fixUrl(url);
			HttpRequestMessage request = new HttpRequestMessage();
			request.Method = method;
			request.RequestUri = new Uri(url);
			if (headers != null)
			{
				foreach (string key in headers.Keys)
				{
					request.Headers.TryAddWithoutValidation(key, headers[key]);
				}
			}
			if (requestBody != null)
			{
				request.Content = new StreamContent(requestBody);
			}
			HttpClient client = new HttpClient();
			HttpResponseMessage response = client.SendAsync(request).Result;
			Stream result = response.Content.ReadAsStreamAsync().Result;
			return result;
		}

		/// <summary>
		/// 发送GET请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <returns>响应内容</returns>
		public static string SendGetRequest(string url)
		{
			Stream streamResult = SendCustomRequest(url, HttpMethod.Get, null, null);
			StreamReader reader = new StreamReader(streamResult);
			string result = reader.ReadToEnd();
			reader.Close();
			streamResult.Close();
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
			Stream streamResult = SendCustomRequest(url, HttpMethod.Get, headers, null);
			StreamReader reader = new StreamReader(streamResult);
			string result = reader.ReadToEnd();
			reader.Close();
			streamResult.Close();
			return result;
		}

		/// <summary>
		/// 发送文本内容的POST请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="contentType">请求数据类型，可以使用ContentTypeValue类的常量值</param>
		/// <param name="requestBody">请求体，注意的是不同的内容类型有不同的请求体格式，例如application/x-www-form-urlencoded中请求体通常是：键1=值1&amp;键2=值2&amp;...，而application/json就是json字符串格式</param>
		/// <returns>响应内容</returns>
		public static string SendPostRequest(string url, string contentType, string requestBody)
		{
			url = fixUrl(url);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
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
		/// 发送自定义请求头的文本内容的POST请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="headers">存放自定义的请求头的键值对，其中可以设定Content-Type、User-Agent值等等</param>
		/// <param name="requestBody">请求体，注意的是不同的内容类型有不同的请求体格式，例如application/x-www-form-urlencoded中请求体通常是：键1=值1&amp;键2=值2&amp;...，而application/json就是json字符串格式</param>
		/// <returns>响应内容</returns>
		public static string SendPostRequest(string url, Dictionary<string, string> headers, string requestBody)
		{
			url = fixUrl(url);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
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
			Stream streamResult = SendCustomRequest(url, HttpMethod.Get, null, null);
			Stream fileStream = new FileStream(filePath, FileMode.Create);
			byte[] fileBuffer = new byte[1024];
			int sizeEachRead;
			while ((sizeEachRead = streamResult.Read(fileBuffer, 0, fileBuffer.Length)) != 0)
			{
				fileStream.Write(fileBuffer, 0, sizeEachRead);
			}
			fileStream.Close();
			streamResult.Close();
			return File.Exists(filePath);
		}

		/// <summary>
		/// 以一个特定的请求头下载文件
		/// </summary>
		/// <param name="url">下载地址</param>
		/// <param name="filePath">文件保存位置，已存在将覆盖</param>
		/// <param name="headers">存放自定义的请求头的键值对，其中可以设定Content-Type、User-Agent值等等</param>
		/// <returns>是否下载成功</returns>
		public static bool DownloadFile(string url, string filePath, Dictionary<string, string> headers)
		{
			Stream streamResult = SendCustomRequest(url, HttpMethod.Get, headers, null);
			Stream fileStream = new FileStream(filePath, FileMode.Create);
			byte[] fileBuffer = new byte[1024];
			int sizeEachRead;
			while ((sizeEachRead = streamResult.Read(fileBuffer, 0, fileBuffer.Length)) != 0)
			{
				fileStream.Write(fileBuffer, 0, sizeEachRead);
			}
			fileStream.Close();
			streamResult.Close();
			return File.Exists(filePath);
		}

		/// <summary>
		/// 上传文件（发送POST请求，使用multipart表单）
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="textArea">请求内容文本域，key为字段，value为对应值</param>
		/// <param name="fileArea">请求内容文件域，key为字段，value为对应文件路径</param>
		/// <returns>响应结果</returns>
		public static string UploadFile(string url, Dictionary<string, string> textArea, Dictionary<string, string> fileArea)
		{
			var formData = new MultipartFormDataContent();
			foreach (string key in textArea.Keys)
			{
				formData.Add(new StringContent(textArea[key]), key);
			}
			foreach (string key in fileArea.Keys)
			{
				Stream fileStream = new FileStream(fileArea[key], FileMode.Open, FileAccess.Read);
				byte[] data = new byte[fileStream.Length];
				fileStream.Read(data, 0, data.Length);
				fileStream.Close();
				formData.Add(new ByteArrayContent(data), key, TextUtils.GetFileNameFromPath(fileArea[key]));
			}
			HttpClient client = new HttpClient();
			HttpResponseMessage result = client.PostAsync(url, formData).Result;
			string responseResult = result.Content.ReadAsStringAsync().Result;
			return responseResult;
		}

		/// <summary>
		/// 使用自定义请求头上传文件（发送POST请求，使用multipart表单）
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="textArea">请求内容文本域，key为字段，value为对应值</param>
		/// <param name="fileArea">请求内容文件域，key为字段，value为对应文件路径</param>
		/// <param name="headers">自定义的请求头，其中可以设定User-Agent值等等</param>
		/// <returns>响应结果</returns>
		public static string UploadFile(string url, Dictionary<string, string> textArea, Dictionary<string, string> fileArea, Dictionary<string, string> headers)
		{
			HttpRequestMessage requestMessage = new HttpRequestMessage();
			requestMessage.Method = HttpMethod.Post;
			var formData = new MultipartFormDataContent();
			foreach (string key in headers.Keys)
			{
				requestMessage.Headers.TryAddWithoutValidation(key, headers[key]);
			}
			foreach (string key in textArea.Keys)
			{
				formData.Add(new StringContent(textArea[key]), key);
			}
			foreach (string key in fileArea.Keys)
			{
				Stream fileStream = new FileStream(fileArea[key], FileMode.Open, FileAccess.Read);
				byte[] data = new byte[fileStream.Length];
				fileStream.Read(data, 0, data.Length);
				fileStream.Close();
				formData.Add(new ByteArrayContent(data), key, TextUtils.GetFileNameFromPath(fileArea[key]));
			}
			requestMessage.RequestUri = new Uri(url);
			requestMessage.Content = formData;
			HttpClient client = new HttpClient();
			HttpResponseMessage result = client.SendAsync(requestMessage).Result;
			string responseResult = result.Content.ReadAsStringAsync().Result;
			return responseResult;
		}
	}
}