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

	}
}
