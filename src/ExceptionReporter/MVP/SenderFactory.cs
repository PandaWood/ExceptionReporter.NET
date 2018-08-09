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
			
			// Default - this is required as the default because the obsolete MailMethod default was 0/SimpleMAPI
			// NB even if SendMethod == None - this could still mean someone is using the obsolete MailMethod == SimpleMapi 
			// When MailMethod is removed, SendMethod == None should return a SilentReportSender
			return new MapiMailSender(_config, _sendEvent);
		}
	}
}