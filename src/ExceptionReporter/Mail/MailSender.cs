using System;
using System.Net.Mail;
using ExceptionReporting.Core;
using ExceptionReporting.Extensions;
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
		public void SendSmtp(string exceptionReport, CompletedMethodDelegate setEmailCompletedState)
		{
			var smtpClient = new SmtpClient(_reportInfo.SmtpServer)
								 {
									 DeliveryMethod = SmtpDeliveryMethod.Network
								 };
			var mailMessage = CreateMailMessage(exceptionReport);

			smtpClient.SendCompleted += delegate { setEmailCompletedState.Invoke(true); };
			smtpClient.SendAsync(mailMessage, null);
		}

		/// <summary>
		/// Send SimpleMAPI email
		/// </summary>
		public void SendMapi(string exceptionReport)
		{
			var mapi = new Mapi();
			var emailAddress = _reportInfo.EmailReportAddress.IsEmpty()
								? _reportInfo.ContactEmail
								: _reportInfo.EmailReportAddress;

			mapi.AddRecipient(emailAddress, null, false);
			AddMapiAttachments(mapi);
			mapi.Send(EmailSubject, exceptionReport, true);
		}

		private void AddMapiAttachments(Mapi mapi)
		{
			if (_reportInfo.ScreenshotAvailable)
				mapi.Attach(ScreenshotTaker.GetImageAsFile(_reportInfo.ScreenshotImage));

			foreach (var file in _reportInfo.FilesToAttach)
			{
				mapi.Attach(file);
			}
		}

		private MailMessage CreateMailMessage(string exceptionReport)
		{
			var mailMessage = new MailMessage
			  {
				From = new MailAddress(_reportInfo.SmtpFromAddress, null),
                #pragma warning disable CS0618 // Type or member is obsolete
                ReplyTo = new MailAddress(_reportInfo.SmtpFromAddress, null),
                #pragma warning restore CS0618 // Type or member is obsolete
                Body = exceptionReport,
				Subject = EmailSubject
			  };

			mailMessage.To.Add(new MailAddress(_reportInfo.ContactEmail));
			AddAnyAttachments(mailMessage);
			
			return mailMessage;
		}

		private void AddAnyAttachments(MailMessage mailMessage)
		{
			AttachScreenshot(mailMessage);
			AttachFiles(mailMessage);
		}

		private void AttachFiles(MailMessage mailMessage)
		{
			foreach (var f in _reportInfo.FilesToAttach)
			{
				mailMessage.Attachments.Add(new Attachment(f));
			}
		}

		private void AttachScreenshot(MailMessage mailMessage)
		{
			if (_reportInfo.ScreenshotAvailable)
				mailMessage.Attachments.Add(new Attachment(ScreenshotTaker.GetImageAsFile(_reportInfo.ScreenshotImage),
														   ScreenshotTaker.ScreenshotMimeType));
		}

		public string EmailSubject
		{
			get { return _reportInfo.MainException.Message; }
		}
	}
}