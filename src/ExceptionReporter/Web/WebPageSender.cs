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

namespace ExceptionReporting.Web {
  internal class WebPageSender {
	//private const string JSON = "application/json";
	private readonly ExceptionReportInfo _info;
	private readonly IReportSendEvent _sendEvent;

	internal WebPageSender(ExceptionReportInfo info, IReportSendEvent sendEvent) {
	  _info = info;
	  _sendEvent = sendEvent;
	}

	public bool Send(string report) {

	  using (WebClient webClient = new ExceptionReporterWebClient(_info.WebReportUrlTimeout)) {
		webClient.Encoding = Encoding.UTF8;


		// https://stackoverflow.com/questions/5401501/how-to-post-data-to-specific-url-using-webclient-in-c-sharp
		// webClient.Headers.Add(HttpRequestHeader.ContentType, JSON);
		// webClient.Headers.Add(HttpRequestHeader.Accept, JSON);
		webClient.UploadStringCompleted += OnUploadCompleted(webClient);

		var reqparm = new System.Collections.Specialized.NameValueCollection {
		  { "AppName", _info.AppName },
		  { "AppVersion", _info.AppVersion },
		  { "ExceptionMessage", _info.MainException.Message },
		  { "Report", report }
		};

		byte[] responsebytes = webClient.UploadValues(_info.WebReportUrl, "POST", reqparm);

		string result = System.Text.Encoding.UTF8.GetString(responsebytes);

		// asume result text dont contains the word "error" its ok 
		// maybe need check for http errors.
		if (!result.Contains("Error") && !result.Contains("error"))
		  return true;

	  }

	  return false;

	}

	private UploadStringCompletedEventHandler OnUploadCompleted(IDisposable webClient) {
	  return (sender, e) => {
		try {
		  if (e.Error == null) {
			_sendEvent.Completed(success: true);
		  }
		  else {
			_sendEvent.Completed(success: false);
			_sendEvent.ShowError("WebPage: " +
				(e.Error.InnerException != null ? e.Error.InnerException.Message : e.Error.Message), e.Error);
		  }
		}
		finally {
		  webClient.Dispose();
		}
	  };
	}
  }

  /*
internal class ExceptionReporterWebClient : WebClient {
  private readonly int _timeout;

  public ExceptionReporterWebClient(int timeout) {
	_timeout = timeout;
  }

  protected override WebRequest GetWebRequest(Uri address) {
	var wr = base.GetWebRequest(address);
	wr.Timeout = _timeout * 1000;
	return wr;
  }
}
  */


}