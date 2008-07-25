using System;
using System.Drawing;
using System.Drawing.Printing;
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
		private Exception _exception;
		private StringBuilder _exceptionString;
	
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

//		private void buildExceptionString(bool blnGeneral, bool blnExceptions, bool blnAssemblies, bool blnSettings,
//										  bool blnEnvironment, bool blnContact, bool blnForPrint)
//		{
//			try
//			{
//				var _exceptionString = new StringBuilder();
//				var swWriter = new StringWriter(_exceptionString);
//
//
//				if (blnGeneral)
//				{
//					if (!blnForPrint)
//					{
//						swWriter.WriteLine(lblGeneral.Text);
//						swWriter.WriteLine((String)null);
//						swWriter.WriteLine("-----------------------------");
//						swWriter.WriteLine((String)null);
//					}
//					swWriter.WriteLine("General");
//					swWriter.WriteLine((String)null);
//					swWriter.WriteLine("Application: " + txtApplication.Text);
//					swWriter.WriteLine("Version:     " + txtVersion.Text);
//					swWriter.WriteLine("Region:      " + txtRegion.Text);
//					swWriter.WriteLine("Machine:     " + " " + txtMachine.Text);
//					swWriter.WriteLine("User:        " + txtUserName.Text);
//					swWriter.WriteLine("-----------------------------");
//					if (!blnForPrint)
//					{
//						swWriter.WriteLine((String)null);
//						swWriter.WriteLine("Date: " + txtDate.Text);
//						swWriter.WriteLine("Time: " + txtTime.Text);
//						swWriter.WriteLine("-----------------------------");
//					}
//					swWriter.WriteLine((String)null);
//					swWriter.WriteLine("Explanation");
//					swWriter.WriteLine(txtExplanation.Text.Trim());
//					swWriter.WriteLine((String)null);
//					swWriter.WriteLine("-----------------------------");
//					swWriter.WriteLine((String)null);
//				}
//
//				if (blnExceptions)
//				{
//					swWriter.WriteLine("Exceptions");
//					swWriter.WriteLine((String)null);
//					exceptionHeirarchyToString(swWriter);
//					swWriter.WriteLine((String)null);
//					swWriter.WriteLine("-----------------------------");
//					swWriter.WriteLine((String)null);
//				}
//
//				if (blnAssemblies)
//				{
//					swWriter.WriteLine("Assemblies");
//					swWriter.WriteLine((String)null);
//					referencedAssembliesToString(swWriter);
//					swWriter.WriteLine("-----------------------------");
//					swWriter.WriteLine((String)null);
//				}
//
//				if (blnSettings)
//				{
//					treeToString(tvwSettings, swWriter);
//					swWriter.WriteLine("-----------------------------");
//					swWriter.WriteLine((String)null);
//				}
//
//				if (blnEnvironment)
//				{
//					treeToString(tvwEnvironment, swWriter);
//					swWriter.WriteLine("-----------------------------");
//					swWriter.WriteLine((String)null);
//				}
//
//				if (blnContact)
//				{
//					swWriter.WriteLine("Contact");
//					swWriter.WriteLine((String)null);
//					swWriter.WriteLine("E-Mail: " + lnkEmail.Text);
//					swWriter.WriteLine("Web:    " + lnkWeb.Text);
//					swWriter.WriteLine("Phone:  " + txtPhone.Text);
//					swWriter.WriteLine("Fax:    " + txtFax.Text);
//					swWriter.WriteLine("-----------------------------");
//					swWriter.WriteLine((String)null);
//				}
//			}
//			catch (Exception ex)
//			{
//				handleError(
//					"There has been a problem building exception details into a string for printing, copying, saving or e-mailing", ex);
//			}
//
//			return;
//		}

		public string BuildExceptionString(bool b, bool b1, bool b2, bool b3, bool b4, bool b5, bool b6)
		{
			throw new NotImplementedException();
		}

		public string ReferencedAssembliesToString(Assembly assembly)
		{
			return ExceptionUtils.ReferencedAssembliesToString(assembly);
		}

		public string ExceptionHierarchyToString(Exception exception)
		{
			return ExceptionUtils.ExceptionHierarchyToString(exception);
		}

		public void SendMapiEmail(string email, StringBuilder exceptionString, IntPtr windowHandle)
		{
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

				ma.Send("An Exception has occured", _exceptionString.ToString(), true);
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
			ExceptionPrinter printer = new ExceptionPrinter();
			printer.PrintException();
		}
	}
}