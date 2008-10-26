using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.Config;
using ExceptionReporting.Core;
using ExceptionReporting.Mail;
using ExceptionReporting.SystemInfo;

namespace ExceptionReporting.Views
{
	/// <summary>
	/// The interface (contract) for an ExceptionReportView
	/// </summary>
	internal interface IExceptionReportView
	{
		string ProgressMessage { set; }
		bool EnableEmailButton { set; }
		bool ShowProgressBar { set; }
		int ProgressValue { get;  set; }
		string UserExplanation { get; }
		void ShowErrorDialog(string message, Exception exception);
		void SetEmailCompletedState(bool success);
		void SetEmailCompletedState_WithMessageIfSuccess(bool success, string successMessage);
		void ShowExceptionReport();
		void SetInProgressState();
		void PopulateConfigTab(TreeNode rootNode);
		void PopulateExceptionTab(Exception exception);
		void PopulateAssembliesTab();
		void PopulateSysInfoTab(TreeNode rootNode);
		void PopulateTabs();
		void SetProgressCompleteState();
	}

	/// <summary>
	/// ExceptionReportPresenter - the 'Presenter' in this implementation of M-V-P (Model-View-Presenter), passive-view
	/// </summary>
	internal class ExceptionReportPresenter
	{
		private readonly IExceptionReportView _view;
		private readonly ExceptionReportInfo _reportInfo;
		private readonly ICollection<SysInfoResult> _results = new List<SysInfoResult>();

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
			ReportInfo.UserExplanation = _view.UserExplanation;
			var stringBuilder = new ExceptionStringBuilder(ReportInfo, _results);
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
				_view.ShowErrorDialog(string.Format("Unable to save file '{0}'", fileName), exception);
			}
		}

		public void SendReportByEmail(IntPtr handle)
		{
			if (ReportInfo.MailMethod == ExceptionReportInfo.EmailMethod.SimpleMAPI)
				SendMapiEmail(handle);

			if (ReportInfo.MailMethod == ExceptionReportInfo.EmailMethod.SMTP)
				SendSmtpMail();
		}

		public void CopyReportToClipboard()
		{
			string exceptionString = BuildExceptionString();
			Clipboard.SetDataObject(exceptionString, true);
			_view.ProgressMessage = "Exception Report copied to clipboard";
		}

		private void SendSmtpMail()
		{
			string exceptionString = BuildExceptionString();

			_view.ProgressMessage = "Sending email via SMTP...";
			_view.EnableEmailButton = false;
			_view.ShowProgressBar = true;

			try
			{
				var mailSender = new MailSender(ReportInfo);
				mailSender.SendSmtp(exceptionString, _view.SetEmailCompletedState);
			}
			catch (Exception exception)
			{
				_view.SetEmailCompletedState(false);
				_view.ShowErrorDialog("Unable to send email using SMTP", exception);
			}
		}

		private void SendMapiEmail(IntPtr windowHandle)
		{
			string exceptionString = BuildExceptionString();

			_view.ProgressMessage = "Launching email program...";
			_view.EnableEmailButton = false;

			bool wasSuccessful = false;

			try
			{
				var mailSender = new MailSender(ReportInfo);
				mailSender.SendMapi(exceptionString, windowHandle);
				wasSuccessful = true;
			}
			catch (Exception exception)
			{
				wasSuccessful = false;
				_view.ShowErrorDialog("Unable to send Email using 'Simple MAPI'", exception);
			}
			finally
			{
				_view.SetEmailCompletedState_WithMessageIfSuccess(wasSuccessful, string.Empty);
			}
		}

		public void PrintReport()
		{	
			//TODO ignore printing for the moment, come back to it
		}

		public TreeNode CreateConfigTree()
		{
			//TODO there's a case to be made, that this should be done by another class/mapper (not the presenter)
			var rootNode = new TreeNode("Configuration Settings");

			foreach (string configString in ConfigReader.GetConfigKeyValuePairsToString())
			{
				rootNode.Nodes.Add(new TreeNode(configString));
			}

			return rootNode;
		}

		public TreeNode CreateSysInfoTree()
		{
			var retriever = new SysInfoRetriever();
			var mapper = new SysInfoResultMapper();
			var rootNode = new TreeNode("System");

			SysInfoResult osResult = retriever.Retrieve(SysInfoQueries.OperatingSystem);
			SysInfoResult machineResult = retriever.Retrieve(SysInfoQueries.Machine);

			mapper.AddTreeViewNode(rootNode, osResult);
			mapper.AddTreeViewNode(rootNode, machineResult);

			_results.Add(osResult);		// store the result for later (TODO I'm not liking how this separate concern is 'dropped' in here)
			_results.Add(machineResult);
			
			return rootNode;
		}

		public void SendContactEmail()
		{
			ShellExecute(string.Format("mailto:{0}", ReportInfo.ContactEmail));
		}

		public void NavigateToWebsite()
		{
			ShellExecute(ReportInfo.WebUrl);
		}

		private void ShellExecute(string executeString)
		{
			try
			{
				var psi = new ProcessStartInfo(executeString) { UseShellExecute = true };
				Process.Start(psi);
			}
			catch (Exception exception)
			{
				_view.ShowErrorDialog(string.Format("Unable to (Shell) Execute '{0}'", executeString), exception);
			}
		}

		public void PopulateReport()
		{
			try
			{
				_view.SetInProgressState();

				_view.PopulateExceptionTab(TheException);
				_view.PopulateAssembliesTab();
				_view.PopulateConfigTab(CreateConfigTree());
				_view.PopulateSysInfoTab(CreateSysInfoTree());
			}
			finally
			{
				_view.SetProgressCompleteState();
			}
		}
	}
}