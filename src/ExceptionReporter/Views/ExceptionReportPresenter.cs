using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ExceptionReporting.Config;
using ExceptionReporting.Core;
using ExceptionReporting.Mail;
using ExceptionReporting.SystemInfo;

namespace ExceptionReporting.Views
{
	/// <summary>
	/// ExceptionReportPresenter - the 'Presenter' in this implementation of M-V-P (Model-View-Presenter), passive-view
	/// </summary>
    internal class ExceptionReportPresenter : Disposable
	{
		private readonly IExceptionReportView _view;
		private readonly ExceptionReportGenerator _reportGenerator;

		public ExceptionReportPresenter(IExceptionReportView view, ExceptionReportInfo info)
		{
			_view = view;
			ReportInfo = info;
			_reportGenerator = new ExceptionReportGenerator(ReportInfo);
		}

		public Exception TheException
		{
			get { return ReportInfo.Exception; }
		}

		public Assembly AppAssembly
		{
			get { return ReportInfo.AppAssembly; }
		}

		public ExceptionReportInfo ReportInfo { get; private set; }

		private string CreateExceptionReport()
		{
			ReportInfo.UserExplanation = _view.UserExplanation;
			return _reportGenerator.CreateExceptionReport();
		}

        protected override void DisposeManagedResources()
        {
            if (ReportInfo != null)
            {
                ReportInfo.Dispose();
            }
            base.DisposeManagedResources();
        }

		public void SaveReportToFile(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) return;

			string exceptionString = CreateExceptionReport();

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
			string exceptionReport = CreateExceptionReport();
			Clipboard.SetDataObject(exceptionReport, true);
			_view.ProgressMessage = string.Format("{0} copied to clipboard", ReportInfo.TitleText);
		}

		public void ToggleDetail()
		{
			_view.ShowFullDetail = !_view.ShowFullDetail;
		}

		private string BuildEmailExceptionString()
		{
			var stringBuilder = new StringBuilder()
				.AppendLine(
				@"The email is ready to be sent. 
					Information about your machine and the exception is included.
					Please feel free to add any relevant information or attach any files.")
				.AppendLine().AppendLine();

			if (ReportInfo.TakeScreenshot)
			{
				stringBuilder.AppendFormat("This email contains a screenshot taken when the exception occurred.")
		                     .AppendLine("If you do not want the screenshot to be sent, you may delete it before sending.")
							 .AppendLine().AppendLine();
			}

			stringBuilder.Append(CreateExceptionReport());

			return stringBuilder.ToString();
		}


		private void SendSmtpMail()
		{
			string exceptionString = BuildEmailExceptionString();

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
			string exceptionString = BuildEmailExceptionString();

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

		private static string GetConfigAsHtml()
		{
			var converter = new ConfigHtmlConverter();
			return converter.Convert();
		}

		private TreeNode CreateSysInfoTree()
		{
			var mapper = new SysInfoResultMapper();
			var rootNode = new TreeNode("System");

			foreach (var sysInfoResult in _reportGenerator.GetOrFetchSysInfoResults())
			{
				mapper.AddTreeViewNode(rootNode, sysInfoResult);
			}

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
				_view.PopulateConfigTab(GetConfigAsHtml());
				_view.PopulateSysInfoTab(CreateSysInfoTree());
			}
			finally
			{
				_view.SetProgressCompleteState();
			}
		}
	}
}
