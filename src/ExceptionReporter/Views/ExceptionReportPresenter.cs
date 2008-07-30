using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.Mail;
using ExceptionReporting.SystemInfo;

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

		public void SaveReportToFile(string fileName)
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

		public void SendReportByEmail(IntPtr handle)
		{
			if (_reportInfo.MailType == ExceptionReportInfo.slsMailType.SimpleMAPI)
				SendMapiEmail(handle);

			if (_reportInfo.MailType == ExceptionReportInfo.slsMailType.SMTP)
				SendSmtpMail();
		}

		public void CopyReportToClipboard()
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

		public TreeNode CreateConfigSettingsTree()
		{
			//TODO the presenters doing too much here, and we need to reuse it - extract out (and test)
			var rootNode = new TreeNode("Configuration Settings");

			foreach (var appSetting in ConfigurationManager.AppSettings)
			{
				string settingText = ConfigurationManager.AppSettings.Get(appSetting.ToString());
				string nodeText = appSetting + " : " + settingText;
				var treeNode = new TreeNode(nodeText);
				rootNode.Nodes.Add(treeNode);
			}

			return rootNode;
		}

		public TreeNode CreateSysInfoTree()
		{
			var retriever = new SysInfoRetriever();
			var mapper = new SysInfoResultMapper();

			var rootNode = new TreeNode("System");
			mapper.AddTreeViewNode(rootNode, retriever.Retrieve(SysInfoQueries.CPU));
			mapper.AddTreeViewNode(rootNode, retriever.Retrieve(SysInfoQueries.Environment));
			mapper.AddTreeViewNode(rootNode, retriever.Retrieve(SysInfoQueries.Memory));
			mapper.AddTreeViewNode(rootNode, retriever.Retrieve(SysInfoQueries.OperatingSystem));
			mapper.AddTreeViewNode(rootNode, retriever.Retrieve(SysInfoQueries.System));

			return rootNode;
		}
	}
}