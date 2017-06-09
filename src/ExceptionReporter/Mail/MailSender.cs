using System.IO;
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
		private AttachAdapter _attacher;
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
			_attacher = new AttachAdapter(mailMessage);

			if (_reportInfo.ScreenshotAvailable)
				mailMessage.Attachments.Add(new Attachment(ScreenshotTaker.GetImageAsFile(_reportInfo.ScreenshotImage), ScreenshotTaker.ScreenshotMimeType));

			AttachFiles();
			return mailMessage;
		}

		private void SmtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				_view.SetEmailCompletedState(true);
				_view.ShowErrorDialog(e.Error.Message, e.Error);
			}
			else
			{
				_view.SetEmailCompletedState(false);
			}
		}

		/// <summary>
		/// Send SimpleMAPI email
		/// </summary>
		public void SendMapi(string exceptionReport)
		{
			var mapi = new SimpleMapi();
			_attacher = new AttachAdapter(mapi);
			
			mapi.AddRecipient(_reportInfo.EmailReportAddress, null, false);
			
			if (_reportInfo.ScreenshotAvailable)
				_attacher.Attach(ScreenshotTaker.GetImageAsFile(_reportInfo.ScreenshotImage));

			AttachFiles();
			mapi.Send(EmailSubject, exceptionReport);
		}

		private void AttachFiles()
		{
			if (_reportInfo.FilesToAttach.Length == 0) return;

			var zipfileName = Path.Combine(Path.GetTempPath(), "exceptionreport-attachments.zip");
			if (File.Exists(zipfileName)) File.Delete(zipfileName);

			using (var zip = new ZipFile(zipfileName))
			{
				foreach (var f in _reportInfo.FilesToAttach)
				{ // try not to add already zipped files to our "zip" attachment, by checking for "zip" extension
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