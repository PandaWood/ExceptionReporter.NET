using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#pragma warning disable 1591

namespace ExceptionReporting.Core
{
	/// <summary>
	/// a bag of information (some of which is stored and retrieved from config)
	/// </summary>
	public class ExceptionReportInfo : Disposable
	{
		private readonly List<Exception> _exceptions = new List<Exception>();

		/// <summary>
		/// The Main (for the most part the 'only') exception, which is the subject of this exception 'report'
		/// Setting this property will clear any previously set exceptions
		/// <remarks>If multiple top-level exceptions are required, use SetExceptions instead</remarks>
		/// </summary>
		public Exception MainException
		{
			get { return _exceptions.Count > 0 ? _exceptions[0] : null; }
			set
			{
				_exceptions.Clear();
				_exceptions.Add(value);
			}
		}

		public IList<Exception> Exceptions
		{
			get { return _exceptions.AsReadOnly(); }
		}

		/// <summary>
		/// Add multiple exceptions to be shown (each in a separate tab if shown in dialog)
		/// <remarks>
		/// Note: Showing multiple exceptions is a special-case requirement - for only 1 top-level exception
		/// use the <see cref="MainException"/> property instead
		/// </remarks>
		/// </summary>
		public void SetExceptions(IEnumerable<Exception> exceptions)
		{
			_exceptions.Clear();
			_exceptions.AddRange(exceptions);
		}

		public string CustomMessage { get; set; }
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; set; }
		public string SmtpFromAddress { get; set; }
		public string SmtpServer { get; set; }

		/// <summary>
		/// Email that is displayed in the 'Contact Information'. See <see cref="EmailReportAddress"/> 
		/// for the email actually used to send if using SimpleMAPI or SMTP)
		/// </summary>
		public string ContactEmail { get; set; }

		/// <summary>
		/// The name of the running application using <see cref="Application.ProductName"/>
		/// </summary>
		public string AppName { get; set; }

		/// <summary>
		/// The version of the running application using <see cref="Application.ProductVersion"/>
		/// </summary>
		public string AppVersion { get; set; }

		/// <summary>
		/// Region information
		/// </summary>
		public string RegionInfo { get; set; }

		/// <summary>
		/// The name of the running application's machine using <see cref="Environment.MachineName"/>
		/// </summary>
		public string MachineName { get; set; }

		/// <summary>
		/// The name of the running application's current user. Defaults to <see cref="Environment.UserName"/>
		/// </summary>
		public string UserName { get; set; }

		public DateTime ExceptionDate { get; set; }

		/// <summary>
		/// The text filled in by the user of the Exception Reporter dialog
		/// </summary>
		public string UserExplanation { get; set; }

		/// <summary>
		/// The calling assembly of the running application
		/// If not set, will default to <see cref="Assembly.GetEntryAssembly()"/> ?? <see cref="Assembly.GetCallingAssembly()"/>
		/// </summary>
		public Assembly AppAssembly { get; set; }

		public string WebUrl { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }

		/// <summary>
		/// The company/owner of the running application.
		/// Used in the dialog label that reads '...please contact {0} support'
		/// </summary>
		public string CompanyName { get; set; }

		public bool ShowGeneralTab { get; set; }
		public bool ShowConfigTab { get; set; }
		public bool ShowContactTab { get; set; }
		public bool ShowExceptionsTab { get; set; }

        private bool _showSysInfoTab;
        public bool ShowSysInfoTab 
        { 
            get { return !ExceptionReport.IsRunningMono() && _showSysInfoTab; } 
            set { _showSysInfoTab = value; }
        }

		private bool _showAssembliesTab;
		public bool ShowAssembliesTab
		{
			get { return !ExceptionReport.IsRunningMono() && _showAssembliesTab; }
			set { _showAssembliesTab = value; }
		}

		/// <summary>
		/// Email address used to send an email (eg appears in the 'to:' field in the default email client if simpleMAPI)
		/// </summary>
		public string EmailReportAddress { get; set; }

		/// <summary>
		/// Default is <see cref="DefaultLabelMessages.DefaultExplanationLabel"/>
		/// </summary>
		public string UserExplanationLabel { get; set; }

		public string ContactMessageTop { get; set; }

		public bool ShowFlatButtons { get; set; }
		public bool ShowLessMoreDetailButton{ get; set; }
		public bool ShowFullDetail { get; set; }
		public bool ShowButtonIcons { get; set; }

		/// <summary>
		/// Dialog title text
		/// </summary>
		public string TitleText { get; set; }

		public Color BackgroundColor { get; set; }
		public float UserExplanationFontSize { get; set; }

		/// <summary>
		/// Take a screenshot automatically at the point of calling <see cref="ExceptionReporter.Show(System.Exception[])"/>
		/// which will then be available if sending an email using the ExceptionReporter dialog functionality
		/// </summary>
		public bool TakeScreenshot { get; set; }

		/// <summary>
		/// The Screenshot Bitmap, used internally
		/// </summary>
		public Bitmap ScreenshotImage { get; set; }

		/// <summary>
		/// Which email method to use (SMTP or SimpleMAPI) 
		/// SimpleMAPI basically means it will try to use an installed Email client on the user's machine (eg Outlook)
		/// </summary>
		public EmailMethod MailMethod { get; set; }

		/// <summary>
		/// Whether a screenshot is configured to be taken and that it has been taken - used internally
		/// </summary>
		public bool ScreenshotAvailable 
		{ 
			get { return TakeScreenshot && ScreenshotImage != null; }
		}

		/// <summary>
		/// Show the Exception Reporter as a "TopMost" window (ie TopMost property on a WinForm)
		/// </summary>
		public bool TopMost { get; set; }

		/// <summary>
		/// Any files to attach to email (SMTP or SimpleMAPI) 
		/// This is in addition to the automatically attached screenshot, if configured via TakeScreenshot
		/// Any file attached needs to be a registered MIME type extension to work(?)
		/// (attaching .tmp doesn't get attached in SimpleMAPI - testing using Outlook2013)
		/// </summary>
		public string[] FilesToAttach { get; set; }

		public ExceptionReportInfo()
		{
			SetDefaultValues();
		}

		private void SetDefaultValues()
		{
			ShowFlatButtons = true;
			ShowFullDetail = true;
			ShowButtonIcons = true;
			BackgroundColor = Color.WhiteSmoke;
			ShowExceptionsTab = true;
			ShowContactTab = false;
			ShowConfigTab = true;
			ShowAssembliesTab = true;
			ShowSysInfoTab = true;
			ShowGeneralTab = true;
			UserExplanationLabel = DefaultLabelMessages.DefaultExplanationLabel;
			ContactMessageTop = DefaultLabelMessages.DefaultContactMessageTop;
			EmailReportAddress = "support@acompany.com"; // SimpleMAPI won't work if this is blank, so show dummy place-holder
			TitleText = "Exception Report";
			UserExplanationFontSize = 12f;
			TakeScreenshot = false;
			TopMost = false;
			FilesToAttach = new string[]{};
		}

		/// <summary>
		/// Enumerated type used to represent supported e-mail mechanisms 
		/// </summary>
		public enum EmailMethod
		{
			SimpleMAPI,
			SMTP
		};

		protected override void DisposeManagedResources()
		{
			if (ScreenshotImage != null)
			{
				ScreenshotImage.Dispose();
			}
			base.DisposeManagedResources();
		}
	}

	public static class DefaultLabelMessages
	{
		public const string DefaultExplanationLabel = "Please enter a brief explanation of events leading up to this exception";
		public const string DefaultContactMessageTop = "The following details can be used to obtain support for this application";
	}
}
#pragma warning restore 1591