using System;
using System.IO;
using System.Management;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Forms;
using Win32Mapi;

namespace ExceptionReporting.Views
{
	/// <summary>
	/// The interface (contract) for an ExceptionReportView
	/// </summary>
	public interface IExceptionReportView
	{
		string ProgressMessage { set; }
		bool EnableEmailButton { set; }
		bool ShowProgressBar { set; }
		int ProgressValue { get;  set; }
		void HandleError(string message, Exception ex);
		void SetSendCompleteState();
		void ShowExceptionReport();
	}

	/// <summary>
	/// ExceptionReportPresenter - the 'Presenter' in this implementation of M-V-P (Model-View-Presenter), passive-view
	/// </summary>
	public class ExceptionReportPresenter
	{
		private readonly IExceptionReportView _view;

		public ExceptionReportPresenter(IExceptionReportView view, ExceptionReportInfo info)
		{
			_view = view;
			Info = info;
		}

		public Exception TheException
		{
			get { return Info.Exception; }
		}

		public Assembly TheAssembly
		{
			get { return Info.AppAssembly; }
		}

		public ExceptionReportInfo Info { get; private set; }

		private void SendSmtpMail()
		{
			_view.ProgressMessage = "Sending email...";
			_view.EnableEmailButton = false;
			_view.ShowProgressBar = true;

			string exceptionString = BuildExceptionString();

			try
			{
				var smtpClient = new SmtpClient(Info.SmtpServer) { DeliveryMethod = SmtpDeliveryMethod.Network };
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
			                  		From = new MailAddress(Info.SmtpFromAddress, null),
									ReplyTo = new MailAddress(Info.SmtpFromAddress, null),
			                  		Body = exceptionString,
			                  		Subject = "Exception"
			                  	};
			mailMessage.To.Add(new MailAddress(Info.ContactEmail));
			return mailMessage;
		}

		public void SaveExceptionReportToFile(string fileName)
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
			var stringBuilder = new ExceptionStringBuilder(Info);
			return stringBuilder.Build();
		}

		private void SendMapiEmail(IntPtr windowHandle)
		{
			string exceptionString = BuildExceptionString();
			try
			{
				var ma = new Mapi();
				ma.Logon(windowHandle);
				ma.Reset();
				if (!string.IsNullOrEmpty(Info.ContactEmail))
				{
					ma.AddRecip(Info.ContactEmail, null, false);
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
					"and then paste directly into an email", ex);
				//TODO why don't copy the detail onto the clipboard for them - or too intrusive?
			}
		}

		public void PrintException()
		{
			var printer = new ExceptionPrinter();
			printer.Print();
		}

		public void AddEnvironmentNode(string caption, string className, TreeNode parentNode, bool useName, string where)
		{
			try
			{
				string strDisplayField = useName ? "Name" : "Caption";
				var treeNode1 = new TreeNode(caption);
				var objectSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM {0} {1}", className, where));

				foreach (ManagementObject managementObject in objectSearcher.Get())
				{
					var treeNode2 = new TreeNode(managementObject.GetPropertyValue(strDisplayField).ToString().Trim());
					treeNode1.Nodes.Add(treeNode2);
					foreach (PropertyData propertyData in managementObject.Properties)
					{
						var propertyNode = new TreeNode(propertyData.Name + ':' + Convert.ToString(propertyData.Value));
						treeNode2.Nodes.Add(propertyNode);
					}
				}
				parentNode.Nodes.Add(treeNode1);
			}
			finally
			{
				_view.ProgressValue++;
			}
		}

		public void SendExceptionReportByEmail(IntPtr handle)
		{
			if (Info.MailType == ExceptionReportInfo.slsMailType.SimpleMAPI)
				SendMapiEmail(handle);

			if (Info.MailType == ExceptionReportInfo.slsMailType.SMTP)
				SendSmtpMail();
		}

		public void CopyExceptionReportToClipboard()
		{
			Clipboard.SetDataObject(BuildExceptionString(), true);
		}
	}
}