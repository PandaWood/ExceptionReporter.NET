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
		readonly ExceptionReportInfo _config;
		IEmailSendEvent _emailEvent;

		internal MailSender(ExceptionReportInfo reportInfo)
		{
			_config = reportInfo;
		}

		/// <summary>
		/// Send SMTP email, requires following ExceptionReportInfo properties to be set
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

			mapi.AddRecipient(_config.EmailReportAddress, null, false);

			AttachFiles(new AttachAdapter(mapi));
			mapi.Send(EmailSubject, exceptionReport);
		}

		private void AttachFiles(IAttach attacher)
		{
			var filesToAttach = new List<string>();
			if (_config.FilesToAttach.Length > 0)
			{
				filesToAttach.AddRange(_config.FilesToAttach);
			}
			if (_config.ScreenshotAvailable)
			{
				filesToAttach.Add(ScreenshotTaker.GetImageAsFile(_config.ScreenshotImage));
			}

			var existingFilesToAttach = filesToAttach.Where(File.Exists).ToList();

			foreach (var zf in existingFilesToAttach.Where(f => f.EndsWith(".zip")))
			{
				attacher.Attach(zf);    // attach external zip files separately, admittedly weak detection using just file extension
			}

			var nonzipFilesToAttach = existingFilesToAttach.Where(f => !f.EndsWith(".zip")).ToList();
			if (nonzipFilesToAttach.Any())
			{ // attach all other files (non zip) into our one zip file
				var zipFile = Path.Combine(Path.GetTempPath(), _config.AttachmentFilename);
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
					return _config.MainException.Message.Truncate(100);
				}
				catch (Exception)
				{
					return "Exception Report";
				}
			}
		}
	}
}