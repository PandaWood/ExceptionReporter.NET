using System;
using System.ComponentModel;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Win32Mapi;

namespace ExceptionReporting.Views
{
	/// <summary>
	/// The IExceptionReportView and ExceptionReportPresenter is the beginning of my attempt to move this code
	/// to Model/View/Presenter pattern - mainly to reduce ExceptionReportView down from 2000 lines in one file
	/// and to separate out logic for testing and maintainability
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
		void ShowExceptionReporter();
	}

	public class ExceptionReportPresenter
	{
		private ExceptionReporter.slsMailType _sendMailType = ExceptionReporter.slsMailType.SimpleMAPI;
		private Assembly _assembly;
		private bool _refreshData;
		private String _email;

		private bool _showGeneralTab = true;
		private bool _showEnvironmentTab = true;
		private bool _showSettingsTab = true;
		private bool _showContactTab = true;
		private bool _showExceptionsTab = true;
		private bool _showAssembliesTab = true;
		private bool _showEnumeratePrinters = true;

		private readonly ExceptionReportInfo _exceptionReportInfo;
		private readonly IExceptionReportView _view;

		public ExceptionReportPresenter(IExceptionReportView view)
		{
			_view = view;
			_exceptionReportInfo = new ExceptionReportInfo
			                       	{
			                       		ExceptionDate = DateTime.Now,
			                       		UserName = Environment.UserName,
			                       		MachineName = Environment.MachineName,
			                       		AppName = Application.ProductName,
										RegionInfo = Application.CurrentCulture.DisplayName,
										AppVersion = Application.ProductVersion
			                       	};
		}

		public Exception TheException
		{
			get { return _exceptionReportInfo.Exception; }
		}

		public Assembly TheAssembly
		{
			get { return _exceptionReportInfo.AppAssembly; }
		}

		public void SendSmtpMail()
		{
			_view.ProgressMessage = "Sending email...";
			_view.EnableEmailButton = false;
			_view.ShowProgressBar = true;

			string exceptionString = BuildExceptionString();

			try
			{
				var smtpClient = new SmtpClient(_view.SMTPServer) { DeliveryMethod = SmtpDeliveryMethod.Network };
				MailMessage mailMessage = GetMailMessage(exceptionString);

				smtpClient.SendCompleted += ((sender, e) => _view.SetSendCompleteState());
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

		public void SaveToFile(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				return;

			string exceptionString = BuildExceptionString();

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

		public string BuildExceptionString()
		{
			//TODO populate ExceptionReportInfo properly
			var stringBuilder = new ExceptionStringBuilder(_exceptionReportInfo);
			return stringBuilder.Build();
		}

		public void SendMapiEmail(string email, IntPtr windowHandle)
		{
			string exceptionString = BuildExceptionString();
			try
			{
				var ma = new Mapi();
				ma.Logon(windowHandle);

				ma.Reset();
				if (_email != null)
				{
					if (_email.Length > 0)
					{
						ma.AddRecip(_email, null, false);
					}
				}

				ma.Send("An Exception has occured", exceptionString, true);
				ma.Logoff();
			}
			catch (Exception ex)
			{
				_view.HandleError(
					"There has been a problem sending e-mail. " +
					"The machine may not be configured to be able to send mail in the way required (SimpleMAPI). " +
					"Instead, use the copy button to place details of the error onto the clipboard, " +
					"and then paste directly into an email",
					ex);
				//TODO why don't copy the detail onto the clipboard for them - or too intrusive?
			}
		}

		public void PrintException()
		{
			var printer = new ExceptionPrinter();
			printer.Print();
		}

		public void DisplayException(Exception exception, Assembly assembly)
		{
			_exceptionReportInfo.Exception = exception;
			_exceptionReportInfo.AppAssembly = assembly;
			_view.ShowExceptionReporter();
		}
	}
}