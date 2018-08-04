using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using ExceptionReporting.Core;

namespace ExceptionReporting.Mail
{
	internal class WebServiceSender
	{
		private readonly ExceptionReportInfo _info;
		private readonly IReportSendEvent _sendEvent;

		internal WebServiceSender(ExceptionReportInfo info, IReportSendEvent sendEvent)
		{
			_info = info;
			_sendEvent = sendEvent;
		}

		public void Send(string report)
		{
			var webClient = new ExceptionReporterWebClient(_info.WebServiceTimeout)
			{
				Encoding = Encoding.UTF8
			};

			webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
			webClient.Headers.Add(HttpRequestHeader.Accept, "application/json");
			webClient.UploadStringCompleted += (sender, e) =>
			{
				try
				{
					if (e.Error == null)
					{
						_sendEvent.Completed(success: true);
					}
					else
					{
						_sendEvent.Completed(success: false);
						_sendEvent.ShowError("WebService: " + 
							(e.Error.InnerException != null ? e.Error.InnerException.Message : e.Error.Message), e.Error);
					}
				}
				finally
				{
					webClient.Dispose();
				}
			};

			using (var jsonStream = new MemoryStream())
			{
				var sz = new DataContractJsonSerializer(typeof(ExceptionReportItem));
				sz.WriteObject(jsonStream, new ExceptionReportItem
				{
					AppName = _info.AppName,
					AppVersion = _info.AppVersion,
					ExceptionMessage = _info.MainException.Message,
					ExceptionReport = report
				});
				var jsonString = Encoding.UTF8.GetString(jsonStream.ToArray());
				webClient.UploadStringAsync(new Uri(_info.WebServiceUrl), jsonString);
			}
		}
	}

	internal class ExceptionReporterWebClient : WebClient
	{
		private readonly int _timeout;

		public ExceptionReporterWebClient(int timeout)
		{
			_timeout = timeout;
		}

		protected override WebRequest GetWebRequest(Uri address)
		{
			var w = base.GetWebRequest(address);
			w.Timeout = _timeout * 1000;
			return w;
		}
	}
}
