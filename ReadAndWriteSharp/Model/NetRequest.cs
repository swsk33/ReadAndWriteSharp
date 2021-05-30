namespace Swsk33.ReadAndWriteSharp.Model
{
	/// <summary>
	/// 网络请求参数类
	/// </summary>
	public class NetRequest
	{

		private string host;

		private string referer;

		private string userAgent;

		private string contentType;

		private string requestBody;

		/// <summary>
		/// Host值
		/// </summary>
		public string Host
		{
			get
			{
				return host;
			}

			set
			{
				host = value;
			}
		}

		/// <summary>
		/// Referer值
		/// </summary>
		public string Referer
		{
			get
			{
				return referer;
			}

			set
			{
				referer = value;
			}
		}

		/// <summary>
		/// User-Agent，可以使用Swsk33.ReadAndWriteSharp.Param下的UserAgentValue中的常量值也可以自定义
		/// </summary>
		public string UserAgent
		{
			get
			{
				return userAgent;
			}

			set
			{
				userAgent = value;
			}
		}

		/// <summary>
		/// 内容类型，用于POST请求
		/// </summary>
		public string ContentType
		{
			get
			{
				return contentType;
			}

			set
			{
				contentType = value;
			}
		}

		/// <summary>
		/// 请求体，用于POST请求
		/// </summary>
		public string RequestBody
		{
			get
			{
				return requestBody;
			}

			set
			{
				requestBody = value;
			}
		}
	}
}
