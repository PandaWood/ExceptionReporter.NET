using System;
using System.Net.Mail;
using ExceptionReporting.Core;
using Win32Mapi;

namespace ExceptionReporting.Mail
{
	internal class MailSender
	{
		public delegate void CompletedMethodDelegate(bool success);
		private readonly ExceptionReportInfo _reportInfo;

		internal MailSender(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;
		}

		/// <summary>
		/// Send SMTP email
		/// </summary>
		public void SendSmtp(string exceptionString, CompletedMethodDelegate setEmailCompletedState)
		{
			var smtpClient = new SmtpClient(_reportInfo.SmtpServer) { DeliveryMethod = SmtpDeliveryMethod.Network };
			MailMessage mailMessage = CreateMailMessage(exceptionString);

			smtpClient.SendCompleted += delegate { setEmailCompletedState.Invoke(true); };
			smtpClient.SendAsync(mailMessage, null);
		}

		/// <summary>
		/// Send SimpleMAPI email
		/// </summary>
		public void SendMapi(string exceptionString, IntPtr windowHandle)
		{
			var mapi = new Mapi();
			mapi.Logon(windowHandle);
			mapi.Reset();

			string emailAddress = (string.IsNullOrEmpty(_reportInfo.EmailReportAddress))
			                      	? _reportInfo.ContactEmail
			                      	: _reportInfo.EmailReportAddress;

			mapi.AddRecipient(emailAddress, null, false);

			if (_reportInfo.ScreenshotAvailable)
				mapi.Attach(ScreenshotHelper.GetBitmapAsFile(_reportInfo.ScreenshotBitmap));

			mapi.Send(CreateSubject(), exceptionString, true);
			mapi.Logoff();
		}

		private MailMessage CreateMailMessage(string exceptionString)
		{
			var mailMessage = new MailMessage
			                  	{
			                  		From = new MailAddress(_reportInfo.SmtpFromAddress, null),
			                  		ReplyTo = new MailAddress(_reportInfo.SmtpFromAddress, null),
			                  		Body = exceptionString,
									Subject = CreateSubject() 
			                  	};

			mailMessage.To.Add(new MailAddress(_reportInfo.ContactEmail));

			if (_reportInfo.ScreenshotAvailable)
				mailMessage.Attachments.Add(
					new Attachment(ScreenshotHelper.GetBitmapAsFile(_reportInfo.ScreenshotBitmap), "image/bmp"));

			return mailMessage;
		}

		/// <summary>
		/// Create the subject (in a separate method to avoid duplication)
		/// </summary>
		private string CreateSubject()
		{
			return string.Format(_reportInfo.TitleText + " for {0} v{1}", _reportInfo.AppName, _reportInfo.AppVersion);
		}
	}
}