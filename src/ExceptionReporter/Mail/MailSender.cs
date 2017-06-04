using System;
using System.IO;
using System.Net.Mail;
using ExceptionReporting.Core;
using ExceptionReporting.Extensions;
using Ionic.Zip;
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
			mapi.Send(EmailSubject, exceptionReport);
		}

		private void AddMapiAttachments(Mapi mapi)
		{
			if (_reportInfo.ScreenshotAvailable)
				mapi.Attach(ScreenshotTaker.GetImageAsFile(_reportInfo.ScreenshotImage));

			AttachFiles(new AttachAdapter(mapi));
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
			AddSmtpAttachments(mailMessage);

			return mailMessage;
		}

		private void AddSmtpAttachments(MailMessage mailMessage)
		{
			AttachScreenshot(mailMessage);
			AttachFiles(new AttachAdapter(mailMessage));
		}

		private void AttachFiles(IAttach attacher)
		{
			var zipfileName = Path.Combine(Path.GetTempPath(), "exceptionreport.zip");
			if (File.Exists(zipfileName)) File.Delete(zipfileName);

			using (var zip = new ZipFile(zipfileName))
			{
				foreach (var f in _reportInfo.FilesToAttach)
				{
					if (File.Exists(f)) zip.AddFile(f, "");
				}
				zip.Save();
			}
			attacher.Attach(zipfileName);
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