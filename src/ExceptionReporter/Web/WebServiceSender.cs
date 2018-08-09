// MIT License
// Copyright (c) 2008-2018 Peter van der Woude
// https://github.com/PandaWood/ExceptionReporter.NET
//

using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using ExceptionReporting.Core;

namespace ExceptionReporting.Web
{
	internal class WebServiceSender
	{
		private const string JSON = "application/json";
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

			webClient.Headers.Add(HttpRequestHeader.ContentType, JSON);
			webClient.Headers.Add(HttpRequestHeader.Accept, JSON);
			webClient.UploadStringCompleted += OnUploadCompleted(webClient);

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

		private UploadStringCompletedEventHandler OnUploadCompleted(IDisposable webClient)
		{
			return (sender, e) =>
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
			var wr = base.GetWebRequest(address);
			wr.Timeout = _timeout * 1000;
			return wr;
		}
	}
}