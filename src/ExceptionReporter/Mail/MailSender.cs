using System;
using System.Net.Mail;
using Win32Mapi;

namespace ExceptionReporting.Mail
{
	public class MailSender
	{
		public delegate void CompletedMethodDelegate();
		private readonly ExceptionReportInfo _reportInfo;

		public MailSender(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;
		}

		public void SendSmtp(string exceptionString, CompletedMethodDelegate SetCompletedStateInTheUI)
		{
			var smtpClient = new SmtpClient(_reportInfo.SmtpServer) { DeliveryMethod = SmtpDeliveryMethod.Network };
			MailMessage mailMessage = CreateMailMessage(exceptionString);

			smtpClient.SendCompleted += delegate { SetCompletedStateInTheUI(); };
			smtpClient.SendAsync(mailMessage, null);
		}

		public void SendMapi(string exceptionString, IntPtr windowHandle)
		{
			var mapi = new Mapi();
			mapi.Logon(windowHandle);
			mapi.Reset();
			if (!string.IsNullOrEmpty(_reportInfo.ContactEmail))
			{
				mapi.AddRecip(_reportInfo.ContactEmail, null, false);
			}

			mapi.Send("An Exception has occured", exceptionString, true);
			mapi.Logoff();
		}

		private MailMessage CreateMailMessage(string exceptionString)
		{
			var mailMessage = new MailMessage
			                  	{
			                  		From = new MailAddress(_reportInfo.SmtpFromAddress, null),
			                  		ReplyTo = new MailAddress(_reportInfo.SmtpFromAddress, null),
			                  		Body = exceptionString,
			                  		Subject = string.Format("Exception Report for {0} v{1}", _reportInfo.AppName, _reportInfo.AppVersion) 
			                  	};
			mailMessage.To.Add(new MailAddress(_reportInfo.ContactEmail));
			return mailMessage;
		}
	}
}