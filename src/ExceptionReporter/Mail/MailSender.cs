using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using ExceptionReporting.Core;
using ExceptionReporting.Extensions;
using Win32Mapi;

namespace ExceptionReporting.Mail
{
	class MailSender
	{
		readonly ExceptionReportInfo _config;
		IEmailSendEvent _emailEvent;
		Attacher _attacher;

		internal MailSender(ExceptionReportInfo reportInfo)
		{
			_config = reportInfo;
			_attacher = new Attacher(reportInfo);
		}

		/// <summary>
		/// Send SMTP email, uses native .NET4 SmtpClient library
		/// Requires following ExceptionReportInfo properties to be set:
		/// SmtpPort, SmtpUseSsl, SmtpUsername, SmtpPassword, SmtpFromAddress, EmailReportAddress
		/// </summary>
		public void SendSmtp(string exceptionReport, IEmailSendEvent emailEvent)
		{
			_emailEvent = emailEvent;
			var smtpClient = new SmtpClient(_config.SmtpServer)
			{
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Port = _config.SmtpPort,
				EnableSsl = _config.SmtpUseSsl,
				UseDefaultCredentials = _config.SmtpUseSsl,
				Credentials = new NetworkCredential(_config.SmtpUsername, _config.SmtpPassword),
			};

			var mailMessage = new MailMessage(_config.SmtpFromAddress, _config.EmailReportAddress)
			{
				BodyEncoding = Encoding.UTF8,
				SubjectEncoding = Encoding.UTF8,
				Body = exceptionReport,
				Subject = EmailSubject
			};

			_attacher.AttachFiles(new AttachAdapter(mailMessage));

			smtpClient.SendCompleted += (sender, e) =>
			{
				try
				{
					if (e.Error != null)
					{
						_emailEvent.Completed(false);
						_emailEvent.ShowError(e.Error.Message, e.Error);
					}
					else
					{
						_emailEvent.Completed(true);
					}
				}
				finally 
				{
					smtpClient.Dispose();
					mailMessage.Dispose();
				}
			};

			smtpClient.SendAsync(mailMessage, "Exception Report");
		}

		/// <summary>
		/// Send email via installed client - uses Simple-MAPI.NET library - https://github.com/PandaWood/Simple-MAPI.NET
		/// </summary>
		public void SendMapi(string exceptionReport)
		{
			var mapi = new SimpleMapi();

			mapi.AddRecipient(_config.EmailReportAddress, null, false);
			_attacher.AttachFiles(new AttachAdapter(mapi));

			mapi.Send(EmailSubject, exceptionReport);
		}

		public string EmailSubject
		{
			get { return _config.MainException.Message?.Truncate(100) ?? "Exception Report"; }
		}
	}
}