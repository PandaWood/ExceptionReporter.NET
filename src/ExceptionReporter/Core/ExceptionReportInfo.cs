using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

#pragma warning disable 1591

namespace ExceptionReporting.Core
{
	/// <summary>
	/// A bag of configuration properties
	/// </summary>
	public class ExceptionReportInfo : Disposable
	{
		readonly List<Exception> _exceptions = new List<Exception>();

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
		
		// SMTP settings
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; set; }
		public string SmtpFromAddress { get; set; }
		public string SmtpServer { get; set; }
		public int SmtpPort { get; set; }
		public bool SmtpUseSsl { get; set; }

		/// <summary>
		/// Email that is displayed in the 'Contact Information'
		/// (ie not the email for sending the report to)
		/// </summary>
		public string ContactEmail { get; set; }

		/// <summary>
		/// The name of the running application calling the exception report
		/// </summary>
		public string AppName { get; set; }

		/// <summary>
		/// The version of the running application calling the exception report
		/// </summary>
		public string AppVersion { get; set; }

		/// <summary>
		/// Region information
		/// </summary>
		public string RegionInfo { get; set; }

		/// <summary>
		/// Date/time of the exception being raised
		/// </summary>
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

		// user/company details to make available
		public string WebUrl { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }

		/// <summary>
		/// The company/owner of the running application.
		/// Used in the dialog label that reads '...please contact {0} support'
		/// </summary>
		public string CompanyName { get; set; }

		// whether to show certain tabs in the 'More Detail' mode of the main dialog
		public bool ShowGeneralTab { get; set; }
		public bool ShowContactTab { get; set; }
		public bool ShowExceptionsTab { get; set; }

		// cater for mono, which can't access the windows api's to get SysInfo and Assemblies
		private bool _showSysInfoTab;
		public bool ShowSysInfoTab
		{
			get { return !ExceptionReporter.IsRunningMono() && _showSysInfoTab; }
			set { _showSysInfoTab = value; }
		}

		private bool _showAssembliesTab;
		public bool ShowAssembliesTab
		{
			get { return !ExceptionReporter.IsRunningMono() && _showAssembliesTab; }
			set { _showAssembliesTab = value; }
		}

		/// <summary>
		/// Email address used to send the report to via email (eg appears in the 'to:' field in the default email client if simpleMAPI)
		/// </summary>
		public string EmailReportAddress { get; set; }

		private bool _silentReportSend;

		/// <summary>
		/// Send the exception report automatically and silently via the WebServiceUrl - without showing dialog or prompting the user
		/// NB The EmailMethod must be set to WebService for this to return true ie SilentReportSend will only work when using WebService
		/// </summary>
		public bool SilentReportSend
		{
			get { return _silentReportSend && MailMethod == EmailMethod.WebService; }
			set { _silentReportSend = value; }
		}

		/// <summary>
		/// The URL to be used to submit the exception report when EmailMethod is set to WebService
		/// A JSON package containing the textual Exception Report, will be posted to this URL
		/// The string that would normally be the body of an email report, will be in the root JSON property 'ExceptionReport'
		/// </summary>
		public string WebServiceUrl { get; set; }

		/// <summary>
		/// Timeout (in seconds) for the WebService
		/// </summary>
		public int WebServiceTimeout { get; set; } = 15;

		private bool _showEmailButton = true;

		/// <summary>
		/// Whether or not to show/display the button labelled "Email"
		/// ShowEmailButton will assume false if EmailMethod is None
		/// </summary>
		public bool ShowEmailButton {
			get
			{	// ReSharper disable once SimplifyConditionalTernaryExpression
				return MailMethod == EmailMethod.None ? false : _showEmailButton;
			}
			set { _showEmailButton = value; }
		}

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
		/// SMTP requires various other settings (host/port/credentials etc) starting with 'SMTP'
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
		/// This can be quite important in some environments (eg Office Addins) where it might get covered by other UI
		/// </summary>
		public bool TopMost { get; set; }

		/// <summary>
		/// Any additional files to attach to the outgoing email report (SMTP or SimpleMAPI) 
		/// This is in addition to the automatically attached screenshot, if configured
		/// All files (exception those already with .zip extension) will be added into a single zip file and attached to the email
		/// </summary>
		public string[] FilesToAttach { get; set; }

		string _attachmentFilename = "ex";
		/// <summary>
		/// Gets or sets the attachment filename.
		/// </summary>
		/// <value>The attachment filename, extension .zip applied automatically if not provided</value>
		public string AttachmentFilename
		{
			get { return _attachmentFilename.EndsWith(".zip") ? _attachmentFilename : _attachmentFilename + ".zip"; }
			set { _attachmentFilename = value; }
		}

		public string UserExplanationLabel { get; set; }
		public string ContactMessageTop { get; set; }
		public bool ShowFlatButtons { get; set; }
		public bool ShowLessMoreDetailButton { get; set; }
		public bool ShowFullDetail { get; set; }
		public bool ShowButtonIcons { get; set; }

		public ExceptionReportInfo()
		{
			SetDefaultValues();
		}

		private void SetDefaultValues()
		{
			ShowFlatButtons = true;
			ShowFullDetail = true;
			ShowButtonIcons = true;
			ShowEmailButton = true;
			BackgroundColor = Color.WhiteSmoke;
			ShowExceptionsTab = true;
			ShowContactTab = false;
			ShowAssembliesTab = true;
			ShowSysInfoTab = true;
			ShowGeneralTab = true;
			UserExplanationLabel = DefaultLabelMessages.DefaultExplanationLabel;
			ContactMessageTop = DefaultLabelMessages.DefaultContactMessageTop;
			EmailReportAddress = "support@acompany.com"; // SimpleMAPI won't work if this is blank, so show dummy place-holder
			TitleText = "Error Report";
			UserExplanationFontSize = 12f;
			TakeScreenshot = false;
			TopMost = false;
			FilesToAttach = new string[]{};
			AttachmentFilename = "ExceptionReport";
			SmtpFromAddress = "";
		}

		/// <summary>
		/// Enumerated type used to represent supported e-mail mechanisms 
		/// </summary>
		public enum EmailMethod
		{
			None,
			SimpleMAPI,
			SMTP,
			WebService
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