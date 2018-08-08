using System.Net;
using System.Net.Mail;
using System.Text;
using ExceptionReporting.Mail;
using ExceptionReporting.Network.Events;

namespace ExceptionReporting.Network.Senders
{
	internal class SmtpMailSender : MailSender, IReportSender
	{
		public SmtpMailSender(ExceptionReportInfo reportInfo, IReportSendEvent sendEvent) : 
			base(reportInfo, sendEvent)
		{ }
		
		public override string Description
		{
			get { return "SMTP"; }
		}

		/// <summary>
		/// Send SMTP email, uses native .NET SmtpClient library
		/// Requires following ExceptionReportInfo properties to be set:
		/// SmtpPort, SmtpUseSsl, SmtpUsername, SmtpPassword, SmtpFromAddress, EmailReportAddress
		/// </summary>
		public void Send(string report)
		{
			var smtp = new SmtpClient(_config.SmtpServer)
			{
				DeliveryMethod = SmtpDeliveryMethod.Network,
				EnableSsl = _config.SmtpUseSsl,
				UseDefaultCredentials = _config.SmtpUseDefaultCredentials,
			};

			if (_config.SmtpPort != 0)		// the default port is used if not set in config
				smtp.Port = _config.SmtpPort;
			if (!_config.SmtpUseDefaultCredentials)
				smtp.Credentials = new NetworkCredential(_config.SmtpUsername, _config.SmtpPassword);

			var message = new MailMessage(_config.SmtpFromAddress, _config.EmailReportAddress)
			{
				BodyEncoding = Encoding.UTF8,
				SubjectEncoding = Encoding.UTF8,
				Priority = _config.SmtpMailPriority,
				Body = report,
				Subject = EmailSubject
			};

			_attacher.AttachFiles(new AttachAdapter(message));

			smtp.SendCompleted += (sender, e) =>
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
						_sendEvent.ShowError(string.Format("{0}: ", Description) +
							(e.Error.InnerException != null ? e.Error.InnerException.Message : e.Error.Message), e.Error);
					}
				}
				finally 
				{
					message.Dispose();
					smtp.Dispose();
				}
			};

			smtp.SendAsync(message, "Exception Report");
		}
	}
}