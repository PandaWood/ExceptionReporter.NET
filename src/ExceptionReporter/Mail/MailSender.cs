using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using ExceptionReporting.Core;
using ExceptionReporting.Extensions;
using Ionic.Zip;
using Win32Mapi;

namespace ExceptionReporting.Mail
{
	class MailSender
	{
		private readonly ExceptionReportInfo _reportInfo;
		private IEmailSendEvent _emailEvent;

		internal MailSender(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;
		}

		/// <summary>
		/// Send SMTP email, requires following ExceptionReportInfo properties to be set
		/// SmtpPort, SmtpUseSsl, SmtpUsername, SmtpPassword, SmtpFromAddress, EmailReportAddress
		/// </summary>
		public void SendSmtp(string exceptionReport, IEmailSendEvent emailEvent)
		{
			_emailEvent = emailEvent;
			var smtpClient = new SmtpClient(_reportInfo.SmtpServer)
			{
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Port = _reportInfo.SmtpPort,
				EnableSsl = _reportInfo.SmtpUseSsl,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(_reportInfo.SmtpUsername, _reportInfo.SmtpPassword),
			};

			var mailMessage = new MailMessage(_reportInfo.SmtpFromAddress, _reportInfo.EmailReportAddress)
			{
				BodyEncoding = Encoding.UTF8,
				SubjectEncoding = Encoding.UTF8,
				Body = exceptionReport,
				Subject = EmailSubject
			};

			AttachFiles(new AttachAdapter(mailMessage));

			smtpClient.SendCompleted += (sender, e) =>
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
			};
			smtpClient.SendAsync(mailMessage, "Exception Report");
		}

		/// <summary>
		/// Send SimpleMAPI email
		/// </summary>
		public void SendMapi(string exceptionReport)
		{
			var mapi = new SimpleMapi();

			mapi.AddRecipient(_reportInfo.EmailReportAddress, null, false);

			AttachFiles(new AttachAdapter(mapi));
			mapi.Send(EmailSubject, exceptionReport);
		}

		private void AttachFiles(IAttach attacher)
		{
			var filesToAttach = new List<string>();
			if (_reportInfo.FilesToAttach.Length > 0)
			{
				filesToAttach.AddRange(_reportInfo.FilesToAttach);
			}
			if (_reportInfo.ScreenshotAvailable)
			{
				filesToAttach.Add(ScreenshotTaker.GetImageAsFile(_reportInfo.ScreenshotImage));
			}

			var existingFilesToAttach = filesToAttach.Where(File.Exists).ToList();

			foreach (var zf in existingFilesToAttach.Where(f => f.EndsWith(".zip")))
			{
				attacher.Attach(zf);    // attach external zip files separately, admittedly weak detection using just file extension
			}

			var nonzipFilesToAttach = existingFilesToAttach.Where(f => !f.EndsWith(".zip")).ToList();
			if (nonzipFilesToAttach.Any())
			{ // attach all other files (non zip) into our one zip file
				var zipFile = Path.Combine(Path.GetTempPath(), _reportInfo.AttachmentFilename);
				if (File.Exists(zipFile)) File.Delete(zipFile);

				using (var zip = new ZipFile(zipFile))
				{
					zip.AddFiles(nonzipFilesToAttach, "");
					zip.Save();
				}

				attacher.Attach(zipFile);
			}
		}

		public string EmailSubject
		{
			get
			{
				try
				{
					return _reportInfo.MainException.Message.Truncate(100);
				}
				catch (Exception)
				{
					return "Exception Report";
				}
			}
		}
	}
}