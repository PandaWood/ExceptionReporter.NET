using System;
using System.Drawing;
using System.Reflection;

namespace ExceptionReporting.Core
{
  
    public class DefaultLabelMessages
	{
		public const string DefaultExplanationLabel = "Please enter a brief explanation of events leading up to this exception";
		public const string DefaultContactMessageTop = "The following details can be used to obtain support for this application";
	}

	/// <summary>
	/// a bag of information (partly config) that is passed around and used in the Exception Report
	/// </summary>
    public class ExceptionReportInfo : Disposable
	{
        public string CustomMessage { get; set; }
	    public string SmtpUsername { get; set; }
		public string SmtpPassword { get; set; }
		public string SmtpFromAddress { get; set; }
		public string SmtpServer { get; set; }
		public Exception Exception { get; set; }
		public string ContactEmail { get; set; }
		public string AppName { get; set; }
		public string AppVersion { get; set; }
		public string RegionInfo { get; set; }
		public string MachineName { get; set; }
		public string UserName { get; set; }
		public DateTime ExceptionDate { get; set; }
		public string UserExplanation { get; set; }
		public Assembly AppAssembly { get; set; }
		public string WebUrl { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }

		public bool ShowGeneralTab { get; set; }
		public bool ShowConfigTab { get; set; }
		public bool ShowContactTab { get; set; }
		public bool ShowExceptionsTab { get; set; }
		public bool ShowSysInfoTab { get; set; }
		public bool ShowAssembliesTab { get; set; }

		public string EmailReportAddress { get; set; }
		public string UserExplanationLabel { get; set; }
		public string ContactMessageTop { get; set; }

		public bool ShowFlatButtons { get; set; }
		public bool ShowFullDetail { get; set; }
		public bool ShowButtonIcons { get; set; }
		public string TitleText { get; set; }

		public Color BackgroundColor { get; set; }
		public float UserExplanationFontSize { get; set; }

		public bool TakeScreenshot { get; set; }
		public Bitmap ScreenshotImage { get; set; }
		public EmailMethod MailMethod { get; set; }

		public bool ScreenshotAvailable 
		{ 
			get { return TakeScreenshot && ScreenshotImage != null; }
		}

		public ExceptionReportInfo()
		{
			// defaults
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
			EmailReportAddress = "support@email.com";		// the SimpleMAPI won't work if this is blank, so make one up
			TitleText = "Exception Report";
			UserExplanationFontSize = 12f;
			TakeScreenshot = false;
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
}
