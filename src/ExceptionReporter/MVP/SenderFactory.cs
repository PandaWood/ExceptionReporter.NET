using ExceptionReporting.Network;
using ExceptionReporting.Network.Events;
using ExceptionReporting.Network.Senders;

namespace ExceptionReporting.MVP
{
	internal class SenderFactory
	{
		private readonly ExceptionReportInfo _config;
		private readonly IReportSendEvent _sendEvent;

		public SenderFactory(ExceptionReportInfo config, IReportSendEvent sendEvent)
		{
			_config = config;
			_sendEvent = sendEvent;
		}

		public IReportSender Get()
		{
			if (_config.SendMethod == ReportSendMethod.WebService)
			{
				return new WebServiceSender(_config, _sendEvent);
			}
			if (_config.SendMethod == ReportSendMethod.SMTP ||
					_config.MailMethod == ExceptionReportInfo.EmailMethod.SMTP)		// for backwards compatibility
			{
				return new SmtpMailSender(_config, _sendEvent);
			}
			if (_config.SendMethod == ReportSendMethod.SimpleMAPI ||
					_config.MailMethod == ExceptionReportInfo.EmailMethod.SimpleMAPI)		// for backwards compatibility
			{		// this option must be last for compatibility because MailMethod.SimpleMAPI was previously 0/default
				return new MapiMailSender(_config, _sendEvent);
			}
			
			// default (only required by obsolete MailMethod who's default was 0/SimpleMAPI)
			return new MapiMailSender(_config, _sendEvent);
		}
	}
}