using System;
using System.IO;
using System.Net.Mail;

namespace ExceptionReporting.Views
{
	/// <summary>
	/// The IExceptionReportView and ExceptionReportPresenter is the beginning of my attempt to move this code
	/// to Model/View/Presenter pattern - anything to reduce ExceptionReportView down from 2000 lines in one file
	/// </summary>
	public interface IExceptionReportView
	{
		string ProgressMessage { set; }
		bool EnableEmailButton { set; }
		string SMTPServer { get; }
		string SMTPFromAddress { get; }
		string EmailToSendTo { get; }
		bool ShowProgressBar { set; }
		void HandleError(string message, Exception ex);
		void SetSendCompleteState();
	}

	public class ExceptionReportPresenter
	{
		private readonly IExceptionReportView _view;

		public ExceptionReportPresenter(IExceptionReportView view)
		{
			_view = view;
		}

		public void SendSmtpMail(string exceptionString)
		{
			_view.ProgressMessage = "Sending email...";
			_view.EnableEmailButton = false;
			_view.ShowProgressBar = true;

			try
			{
				var smtpClient = new SmtpClient(_view.SMTPServer) { DeliveryMethod = SmtpDeliveryMethod.Network };
				MailMessage mailMessage = GetMailMessage(exceptionString);

				smtpClient.SendCompleted += delegate { _view.SetSendCompleteState();	};
				smtpClient.SendAsync(mailMessage, null);
			}
			catch (Exception ex)
			{
				_view.ProgressMessage= string.Empty;
				_view.ShowProgressBar = false;
				_view.EnableEmailButton = true;
				_view.HandleError("Problem sending SMTP Mail", ex);
			}
		}

		private MailMessage GetMailMessage(string exceptionString)
		{
			var mailMessage = new MailMessage
			                  	{
			                  		From = new MailAddress(_view.SMTPFromAddress, "FromAddress"),
			                  		ReplyTo = new MailAddress(_view.SMTPFromAddress, "ReplyToAddress"),
			                  		Body = exceptionString,
			                  		Subject = "Exception"
			                  	};
			mailMessage.To.Add(new MailAddress(_view.EmailToSendTo));

			return mailMessage;
		}

		public void Save(string exceptionString, string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				return;

			try
			{
				using (var stream = File.OpenWrite(fileName))
				{
					var writer = new StreamWriter(stream);
					writer.Write(exceptionString);
					writer.Flush();
				}
			}
			catch (Exception ex)
			{
				_view.HandleError("Error saving to file", ex);
			}
		}

		public string BuildExceptionString(bool b, bool b1, bool b2, bool b3, bool b4, bool b5, bool b6)
		{
			throw new NotImplementedException();
		}
	}
}