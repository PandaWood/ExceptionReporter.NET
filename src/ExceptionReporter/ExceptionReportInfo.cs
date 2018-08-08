/*
 * https://github.com/PandaWood/ExceptionReporter.NET
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mail;
using System.Reflection;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

#pragma warning disable 1591

namespace ExceptionReporting
{
	/// <summary>
	/// A bag of configuration and data
	/// </summary>
	public class ExceptionReportInfo : Disposable
	{
		readonly List<Exception> _exceptions = new List<Exception>();

		/// <summary>
		/// The Main (for the most part the 'only') exception, which is the subject of this exception 'report'
		/// Setting this property will clear any previously set exceptions
		/// <remarks>If multiple top-level exceptions are required, use <see cref="SetExceptions(IEnumerable{Exception})"/> instead</remarks>
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
		/// Add multiple exceptions
		/// <remarks>
		/// Note: Showing multiple exceptions is a special-case requirement
		/// To set only 1 top-level exception use <see cref="MainException"/> property instead
		/// </remarks>
		/// </summary>
		public void SetExceptions(IEnumerable<Exception> exceptions)
		{
			_exceptions.Clear();
			_exceptions.AddRange(exceptions);
		}

		/// <summary>
		/// Override the Exception.Message property
		/// ie a custom message to show in place of the Exception Message
		/// NB this can also be set in the 1st parameter of <see cref="ExceptionReporter.Show(string, Exception[]))"/>
		/// </summary>
		public string CustomMessage { get; set; }

		#region SMTP settings
		
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; set; }
		public string SmtpFromAddress { get; set; } = "";
		public string SmtpServer { get; set; }
		
		/// <summary>
		/// Uses default port if not set (ie 25)
		/// </summary>
		public int SmtpPort { get; set; }

		/// <summary>
		/// Whether SMTP uses SSL
		/// </summary>
		public bool SmtpUseSsl { get; set; }
		
		/// <summary>
		/// Use default credentials of the user (alternatively set false supply SmtpUsername/SmtpPassword)
		/// </summary>
		public bool SmtpUseDefaultCredentials { get; set; }

		/// <summary>
		/// Priority of the Email message
		/// </summary>
		public MailPriority SmtpMailPriority { get; set; } = MailPriority.Normal;
		
		#endregion

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
		/// Region information - set automatically
		/// </summary>
		public string RegionInfo { get; set; }

		/// <summary>
		/// Date/time of the exception being raised - set automatically
		/// </summary>
		public DateTime ExceptionDate { get; set; }

		/// <summary>
		/// Whether to report the date/time of the exception in local time or Coordinated Universal Time (UTC).
		/// Defaults to UTC if not specified.
		/// </summary>
		public DateTimeKind ExceptionDateKind { get; set; } = DateTimeKind.Utc;

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
		public bool ShowGeneralTab { get; set; } = true;
		public bool ShowContactTab { get; set; } = false;
		public bool ShowExceptionsTab { get; set; } = true;

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
		/// Email address used to send the report via email
		/// Appears in the 'to:' field in the default email client if
		/// <see cref="SendMethod"/> is <see cref="ReportSendMethod.SimpleMAPI"/>
		/// </summary>
		public string EmailReportAddress { get; set; } = "";

		/// <summary>
		/// The URL to be used to submit the exception report to a RESTful WebService
		/// Requires <see cref="SendMethod"/> is set to <see cref="ReportSendMethod.WebService"/>
		/// </summary>
		public string WebServiceUrl { get; set; }

		/// <summary>
		/// Timeout (in seconds) for the WebService
		/// </summary>
		public int WebServiceTimeout { get; set; } = 15;

		//TODO it would also be logical to assume ShowEmailButton to be false if ReportSendMethod.None
		// but we will have to wait until we fully remove the obsolete MailMethod enumeration because
		// it doesn't have a None option and so there is no way to make it backwards compatible
		// when this is ready we will add something like: get { return SendReportMethod.None || !_showEmailButton } 
		
		/// <summary>
		/// Whether or not to show/display the button labelled "Email"
		/// </summary>
		public bool ShowEmailButton { get; set; } = true;

		/// <summary>
		/// The title of the main ExceptionReporter dialog
		/// </summary>
		public string TitleText { get; set; } = "Error Report";

		/// <summary>
		/// Background color of the dialog - generally best to avoid changing this
		/// </summary>
		public Color BackgroundColor { get; set; } = Color.WhiteSmoke;

		/// <summary>
		/// The font size of the user input text box
		/// </summary>
		public float UserExplanationFontSize { get; set; } = 12f;

		/// <summary>
		/// Take a screenshot automatically at the point of calling <see cref="ExceptionReporter.Show(System.Exception[])"/>
		/// which will then be available if sending an email using the ExceptionReporter dialog functionality
		/// </summary>
		public bool TakeScreenshot { get; set; } = false;

		/// <summary>
		/// The Screenshot Bitmap, used internally but exposed for flexibility
		/// </summary>
		public Bitmap ScreenshotImage { get; set; }

		/// <summary>
		/// The method used to send the report
		/// </summary>
		public ReportSendMethod SendMethod { get; set; } = ReportSendMethod.None;
		
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
		public bool TopMost { get; set; } = false;

		/// <summary>
		/// Any additional files to attach to the outgoing email report (SMTP or SimpleMAPI) 
		/// This is in addition to the automatically attached screenshot, if configured
		/// All files (exception those already with .zip extension) will be added into a single zip file and attached to the email
		/// </summary>
		public string[] FilesToAttach { get; set; } = {};

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

		/// <summary>
		/// The text to show in the label that prompts the user to input any relevant message
		/// </summary>
		public string UserExplanationLabel { get; set; } = DefaultLabelMessages.DefaultExplanationLabel;

		public string ContactMessageTop { get; set; } = DefaultLabelMessages.DefaultContactMessageTop;

		/// <summary>
		/// Show buttons in the "flat" (non 3D) style
		/// </summary>
		public bool ShowFlatButtons { get; set; } = true;
		
		/// <summary>
		/// Show the button that gives user the option to switch between "Less Detail/More Detail"
		/// </summary>
		public bool ShowLessMoreDetailButton { get; set; }

		public bool ShowFullDetail { get; set; } = true;

		/// <summary>
		/// Whether to show relevant icons on the buttons
		/// </summary>
		public bool ShowButtonIcons { get; set; } = true;

		public ExceptionReportInfo()
		{
			SetDefaultValues();
		}

		private void SetDefaultValues()
		{
			ShowEmailButton = true;
			ShowAssembliesTab = true;
			ShowSysInfoTab = true;
			AttachmentFilename = "ExceptionReport";
		}

		public bool IsSimpleMAPI()
		{
			return SendMethod == ReportSendMethod.SimpleMAPI ||
			       MailMethod == EmailMethod.SimpleMAPI;		// backwards compatible
		}

		/// <summary>
		/// Supported e-mail mechanisms 
		/// </summary>
		[Obsolete("Replace 'ExceptionReportInfo.EmailMethod' with 'ReportSendMethod'")]
		public enum EmailMethod
		{
			///<summary>Tries to launch the installed Email client on Windows (default) </summary>
			SimpleMAPI,
			///<summary>Sends Email via an SMTP server - requires other config (host/port etc) properties starting with 'Smtp'</summary>
			SMTP
		}
		
		[Obsolete("use 'SendMethod' property instead")]
		public EmailMethod MailMethod { get; set; }

		protected override void DisposeManagedResources()
		{
			ScreenshotImage?.Dispose();
			base.DisposeManagedResources();
		}
	}
	
	/// <summary>
	/// The supported methods to send a report 
	/// </summary>
	public enum ReportSendMethod
	{
		///<summary>No sending of reports (default) </summary>
		None,

		///<summary>Tries to use the Windows default Email client eg Outlook</summary>
		SimpleMAPI,

		///<summary>Sends Email via an SMTP server - requires other config (host/port etc) properties starting with 'Smtp'</summary>
		SMTP,

		/// <summary>
		/// WebService - requires a REST API server accepting content-type 'application/json' of type POST and a
		/// JSON packet containing the properties represented in the DataContract class 'ExceptionReportPacket'
		/// (an example .NET Core REST project doing exactly what is required is included in the ExceptionReporter.NET solution)
		/// </summary>
		WebService
	}

	internal static class DefaultLabelMessages
	{
		public const string DefaultExplanationLabel = "Please enter a brief explanation of events leading up to this exception";
		public const string DefaultContactMessageTop = "The following details can be used to obtain support for this application";
	}
}

#pragma warning restore 1591