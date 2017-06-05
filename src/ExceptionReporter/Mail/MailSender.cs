using System;
using System.IO;
using System.Net.Mail;
using ExceptionReporting.Core;
using ExceptionReporting.Extensions;
using Ionic.Zip;
using Win32Mapi;

namespace ExceptionReporting.Mail
{
	class MailSender
	{
		public delegate void CompletedMethodDelegate(bool success);
		private readonly ExceptionReportInfo _reportInfo;
		private AttachAdapter _attacher;

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
			_attacher = new AttachAdapter(mailMessage);

			smtpClient.SendCompleted += delegate { setEmailCompletedState.Invoke(true); };
			smtpClient.SendAsync(mailMessage, null);
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

			if (_reportInfo.ScreenshotAvailable)
				mailMessage.Attachments.Add(
					new Attachment(ScreenshotTaker.GetImageAsFile(_reportInfo.ScreenshotImage), ScreenshotTaker.ScreenshotMimeType));
			AttachFiles();

			return mailMessage;
		}

		/// <summary>
		/// Send SimpleMAPI email
		/// </summary>
		public void SendMapi(string exceptionReport)
		{
			var mapi = new Mapi();
			_attacher = new AttachAdapter(mapi);
			
			var emailAddress = _reportInfo.EmailReportAddress.IsEmpty()
								? _reportInfo.ContactEmail
								: _reportInfo.EmailReportAddress;

			mapi.AddRecipient(emailAddress, null, false);
			
			if (_reportInfo.ScreenshotAvailable)
				_attacher.Attach(ScreenshotTaker.GetImageAsFile(_reportInfo.ScreenshotImage));

			AttachFiles();
			mapi.Send(EmailSubject, exceptionReport);
		}

		private void AttachFiles()
		{
			var zipfileName = Path.Combine(Path.GetTempPath(), "exceptionreport-attachments.zip");
			if (File.Exists(zipfileName)) File.Delete(zipfileName);

			using (var zip = new ZipFile(zipfileName))
			{
				foreach (var f in _reportInfo.FilesToAttach)
				{ // try not to add already zipped files to the "single zip" attachment, by checking for "zip" extension
					if (!File.Exists(f)) continue;

					if (!f.EndsWith(".zip")) { 
						zip.AddFile(f, ""); 							// add file to zip attachment
					}
					else {
						_attacher.Attach(zipfileName);		// attach zip file separately
					}
				}
				zip.Save();
			}
			_attacher.Attach(zipfileName);
		}

		public string EmailSubject
		{
			get { return _reportInfo.MainException.Message; }
		}
	}
}