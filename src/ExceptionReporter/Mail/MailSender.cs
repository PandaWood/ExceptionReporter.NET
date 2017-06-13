using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using ExceptionReporting.Core;
using Ionic.Zip;
using Win32Mapi;

namespace ExceptionReporting.Mail
{
	class MailSender
	{
		private readonly ExceptionReportInfo _reportInfo;
		private IExceptionReportView _view;

		internal MailSender(ExceptionReportInfo reportInfo)
		{
			_reportInfo = reportInfo;
		}

		/// <summary>
		/// Send SMTP email
		/// </summary>
		public void SendSmtp(string exceptionReport, IExceptionReportView view)
		{
			_view = view;
			var smtpClient = new SmtpClient(_reportInfo.SmtpServer)
			{
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Port = _reportInfo.SmtpPort,
				EnableSsl = _reportInfo.SmtpUseSsl,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(_reportInfo.SmtpUsername, _reportInfo.SmtpPassword),
			};

			var mailMessage = CreateMailMessage(exceptionReport);

			smtpClient.SendCompleted += SmtpClient_SendCompleted;
			smtpClient.SendAsync(mailMessage, "Exception Report");
		}

		private MailMessage CreateMailMessage(string exceptionReport)
		{
			var mailMessage = new MailMessage(_reportInfo.SmtpFromAddress, _reportInfo.EmailReportAddress)
			{
				BodyEncoding = Encoding.UTF8,
				Body = exceptionReport,
				Subject = EmailSubject
			};

			AttachFiles(new AttachAdapter(mailMessage));
			return mailMessage;
		}

		private void SmtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				_view.SetEmailCompletedState(false);
				_view.ShowErrorDialog(e.Error.Message, e.Error);
			}
			else
			{
				_view.SetEmailCompletedState(true);
			}
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

			foreach (var zf in existingFilesToAttach.Where(f => f.EndsWith(".zip"))) {
				attacher.Attach(zf);		// attach external zip files separately
			}

			var nonzipFilesToAttach = existingFilesToAttach.Where(f => !f.EndsWith(".zip")).ToList();

			if (nonzipFilesToAttach.Any())
			{		// attach all other files (non zip) into our one zip file
				var zipfileName = Path.Combine(Path.GetTempPath(), "exceptionreport.zip");
				if (File.Exists(zipfileName)) File.Delete(zipfileName);

				using (var zip = new ZipFile(zipfileName))
				{
					foreach (var f in nonzipFilesToAttach)
					{
						zip.AddFile(f, "");
					}
					zip.Save();
				}

				attacher.Attach(zipfileName);
			}
		}

		public string EmailSubject
		{
			get { return _reportInfo.MainException.Message; }
		}
	}
}