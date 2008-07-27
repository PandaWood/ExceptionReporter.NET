using System;
using System.Configuration;
using System.IO;
using System.Management;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.Mail;

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
		string UserExplanation { get; }
		void ShowError(string message, Exception exception);
		void SetSendMailCompletedState();
		void ShowExceptionReport();
	}

	/// <summary>
	/// ExceptionReportPresenter - the 'Presenter' in this implementation of M-V-P (Model-View-Presenter), passive-view
	/// </summary>
	public class ExceptionReportPresenter
	{
		private readonly IExceptionReportView _view;
		private readonly ExceptionReportInfo _reportInfo;

		public ExceptionReportPresenter(IExceptionReportView view, ExceptionReportInfo info)
		{
			_view = view;
			_reportInfo = info;
		}

		public Exception TheException
		{
			get { return _reportInfo.Exception; }
		}

		public Assembly AppAssembly
		{
			get { return _reportInfo.AppAssembly; }
		}

		public ExceptionReportInfo ReportInfo
		{
			get { return _reportInfo; }
		}

		public string BuildExceptionString()
		{
			_reportInfo.UserExplanation = _view.UserExplanation;
			var stringBuilder = new ExceptionStringBuilder(_reportInfo);
			return stringBuilder.Build();
		}

		public void SaveExceptionReportToFile(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				return;

			string exceptionString = BuildExceptionString();

			try
			{
				using (FileStream stream = File.OpenWrite(fileName))
				{
					var writer = new StreamWriter(stream);
					writer.Write(exceptionString);
					writer.Flush();
				}
			}
			catch (Exception exception)
			{
				_view.ShowError(string.Format("Unable to save '{0}'", fileName), exception);
			}
		}

		public void SendExceptionReportByEmail(IntPtr handle)
		{
			if (_reportInfo.MailType == ExceptionReportInfo.slsMailType.SimpleMAPI)
				SendMapiEmail(handle);

			if (_reportInfo.MailType == ExceptionReportInfo.slsMailType.SMTP)
				SendSmtpMail();
		}

		public void CopyExceptionReportToClipboard()
		{
			string exceptionString = BuildExceptionString();
			Clipboard.SetDataObject(exceptionString, true);
		}

		private void SendSmtpMail()
		{
			string exceptionString = BuildExceptionString();

			_view.ProgressMessage = "Sending email...";
			_view.EnableEmailButton = false;
			_view.ShowProgressBar = true;

			try
			{
				var mailSender = new MailSender(_reportInfo);
				mailSender.SendSmtp(exceptionString, _view.SetSendMailCompletedState);
			}
			catch (Exception exception)
			{
				_view.ProgressMessage = "Unable to send email";
				_view.ShowError("Unable to send Email using SMTP", exception);
			}
		}

		private void SendMapiEmail(IntPtr windowHandle)
		{
			string exceptionString = BuildExceptionString();

			try
			{
				var mailSender = new MailSender(_reportInfo);
				mailSender.SendMapi(exceptionString, windowHandle);
			}
			catch (Exception exception)
			{
				_view.ShowError("Unable to send Email using 'Simple MAPI'", exception);
			}
		}

		public void PrintException()
		{	//TODO I'm basically ignoring printing for the moment, come back to it
			var printer = new ExceptionPrinter();
			printer.Print();
		}

		public void AddEnvironmentNode(string caption, string className, TreeNode parentNode, bool useName, string where)
		{
			try
			{	//TODO the presenters probably doing too much here, extract out and test it
				string displayField = useName ? "Name" : "Caption";
				var treeNode1 = new TreeNode(caption);
				var objectSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM {0} {1}", className, where));

				foreach (ManagementObject managementObject in objectSearcher.Get())
				{
					var treeNode2 = new TreeNode(managementObject.GetPropertyValue(displayField).ToString().Trim());
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

		public TreeNode CreateSettingsTree()
		{
			var rootNode = new TreeNode("Application Settings");

			foreach (var appSetting in ConfigurationManager.AppSettings)
			{
				string settingText = ConfigurationManager.AppSettings.Get(appSetting.ToString());
				string nodeText = appSetting + " : " + settingText;
				var treeNode = new TreeNode(nodeText);
				rootNode.Nodes.Add(treeNode);
			}

			return rootNode;
		}
	}
}