using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Text;

//-------------------------------------------------------------------------
// ExceptionReporter - Error Reporting Component for .Net
//
// Copyright (C) 2004  Phillip Pettit / Stratalogic Software
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//--------------------------------------------------------------------------

namespace SLSExceptionReporter
{
	/// <summary>
	/// Namespace containing all classes and types that for part of the StrataLogic Software Exception Reporter
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(ExceptionReporter),"ExceptionReporter.ico")]
	public class ExceptionReporter : System.ComponentModel.Component
	{
		/**********************************************************
		 * Exception Reporter Class - the main interface to the 
		 * StrataLogic Software Exception Reporter
		 * 
		 * Developer		Date		Comment  
		 * Phillip Pettit	Mar/04		Initial Creation
		 **********************************************************/
			 
		/// <summary>
		/// Exception Reporter Component.  Used to display detailed 
		/// exception and system information on occurence of application error.
		/// Can be added as a component to a form or be created dynamically
		/// </summary>
		private System.ComponentModel.Container components = null;
		private bool blnLicensed = true;
		
		// Enumerated type used to represent supported e-mail mechanisms
		public enum slsMailType {SimpleMAPI, SMTP};

		public ExceptionReporter(System.ComponentModel.IContainer container)
		{
			/**********************************************************
			 * Constructor
			 * 
			 * Pass:	container - the container to which this component
			 * 						will be added
			 * Returns: 
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// Contstructor. Takes a Container object as a parameter
			/// </summary>
			/// <param name="container">The object that will contain the new ExceptionReporter</param>
			container.Add(this);
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public ExceptionReporter()
		{
			/**********************************************************
			 * Constructor
			 * 
			 * Pass:	
			 * Returns: 
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// Constructor. Initializes the Exception Reporter
			/// </summary>
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
		
		// pivate boolean variable to store state of tab page
		// default to true
		private bool blnGeneralTab = true;
		private bool blnEnvironmentTab = true;
		private bool blnSettingsTab = true;
		private bool blnContactTab = true;
		private bool blnExceptionsTab = true;
		private bool blnAssembliesTab = true;

		private bool blnEnumeratePrinters = true;
		
		// public property used to set/get visibility of Tab
		public bool ShowGeneralTab
		{
			/**********************************************************
			 * Property: ShowGeneralTab
			 * 
			 *  If true the General Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ShowGeneralTab property. Used to set (or get) the visibility of one of the Exception Reporters tabs
			/// </summary>
			get
			{
				//return visibility
				return blnGeneralTab;
			}
			set
			{
				// set visibility
				blnGeneralTab = value;
				
				
			}
		}
			
		// public property used to set/get visibility of Tab
		public bool ShowEnvironmentTab
		{
			/**********************************************************
			 * Property: ShowEnvironmentTab
			 * 
			 *  If true the Environment Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ShowEnvironmentTab property. Used to set (or get) the visibility of one of the Exception Reporters tabs
			/// </summary>
			get
			{
				//return visibility
				return blnEnvironmentTab;
			}
			set
			{
				// set visibility
				blnEnvironmentTab= value;
				// hide or show as appropriate
				
				
			}
		}
		
		// public property used to set/get Printer Enumeration setting
		public bool EnumeratePrinters
		{
			/**********************************************************
			 * Property: EnumeratePrinters
			 * 
			 *  If true the set of Printers is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/05		Initial Creation
			 **********************************************************/
			/// <summary>
			/// EnumeratePrinters property. Used to set (or get) the visibility of one of the Exception Reporters tabs
			/// </summary>
			get
			{
				//return setting
				return blnEnumeratePrinters;
			}
			set
			{
				// get setting
				blnEnumeratePrinters= value;
				// hide or show as appropriate
				
				
			}
		}
		
		// public property used to set/get visibility of Tab
		public bool ShowAssembliesTab
		{
			/**********************************************************
			 * Property: ShowAssembliesTab
			 * 
			 *  If true the Assemblies Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ShowAssembliesTab property. Used to set (or get) the visibility of one of the Exception Reporters tabs
			/// </summary>
			get
			{
				//return visibility
				return blnAssembliesTab;
			}
			set
			{
				// set visibility
				blnAssembliesTab= value;
				// hide or show as appropriate
				
			}
		}
		private String strSMTPServer = null;
		// public property used to set/get SMTP Server
		public String SMTPServer
		{
			/**********************************************************
			 * Property: SMTPServer
			 * 
			 * Relevant for SMTP mail only..the server to send e-mail
			 * through..if none is specified the local machine is used
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ShowAssembliesTab property. Used to set (or get) the visibility of one of the Exception Reporters tabs
			/// </summary>
			get
			{
				//return SMTP Server
				return strSMTPServer;
			}
			set
			{
				// set SMTP Server
				strSMTPServer = value;
				
			}
		}
		private String strSMTPUsername = null;
		// public property used to set/get UserName
		public String SMTPUsername
		{
			/**********************************************************
			 * Property: SMTPUsername
			 * 
			 * Relevant for SMTP mail only..the username used for
			 *  SMTP authentication. Supported in .Net 1.1 + only
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			get
			{
				//return UserName
				return strSMTPUsername;
			}
			set
			{
				// set UserName
				strSMTPUsername = value;
				
			}
		}
		private String strSMTPPassword = null;
		// public property used to set/get Password
		public String SMTPPassword
		{
			/**********************************************************
			 * Property: SMTPPassword
			 * 
			 * Relevant for SMTP mail only..the password used for
			 *  SMTP authentication. Supported in .Net 1.1 + only
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			get
			{
				//return Password
				return strSMTPPassword;
			}
			set
			{
				// set Password
				strSMTPPassword = value;
				
			}
		}
		private String strSMTPFromAddress = null;
		// public property used to set/get From Address
		public String SMTPFromAddress
		{
			/**********************************************************
			 * Property: SMTPFromAddress
			 * 
			 * Relevant for SMTP mail only..the address from which it will
			 * appear that Exception Reporter e-mail has come from
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			get
			{
				//return From Address
				return strSMTPFromAddress;
			}
			set
			{
				// set From Address
				strSMTPFromAddress = value;
				
			}
		}
		private String strSendEmailAddress = null;
		// public property used to set/get send email address
		public String SendEmailAddress
		{
			/**********************************************************
			 * Property: SendEmailAddress
			 * 
			 * The e-mail address to which Email will be sent...if not specified
			 * then e-mail is sent to the ContactEmail address
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			get
			{
				//return send email address
				return strSendEmailAddress;
			}
			set
			{
				// set send email address
				strSendEmailAddress = value;
				
			}
		}
		
		// public property used to set/get visibility of Tab
		public bool ShowSettingsTab
		{
			/**********************************************************
			 * Property: ShowSettingsTab
			 * 
			 *  If true the Settings Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ShowSettingsTab property. Used to set (or get) the visibility of one of the Exception Reporters tabs
			/// </summary>
			get
			{
				//return visibility
				return blnSettingsTab;
			}
			set
			{
				// set visibility
				blnSettingsTab = value;
				// hide or show as appropriate
			}
		}
		// public property used to set/get visibility of Tab
		public bool ShowContactTab
		{
			/**********************************************************
			 * Property: ShowContactTab
			 * 
			 *  If true the Contact Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ShowContactTab property. Used to set (or get) the visibility of one of the Exception Reporters tabs
			/// </summary>
			get
			{
				//return visibility
				return blnContactTab;
			}
			set
			{
				// set visibility
				blnContactTab = value;
				// hide or show as appropriate
			}
		}

		// public property used to set/get visibility of Tab
		public bool ShowExceptionsTab
		{	/**********************************************************
			 * Property: ShowExceptionsTab
			 * 
			 *  If true the Exceptions Tab of the Exception Reporter is
			 *  displayed
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ShowExceptionsTab property. Used to set (or get) the visibility of one of the Exception Reporters tabs
			/// </summary>
			get
			{
				//return visibility
				return blnExceptionsTab;
			}
			set
			{
				// set visibility
				blnExceptionsTab = value;
			}
		}

		private bool visibleCopyButton = true;
		
		public bool ShowCopyButton
		{
			/**********************************************************
			 * Property: ShowCopyButton
			 * 
			 *  If true the Copy (to clipboard) Button is displayed on the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ShowCopyButton property. Used to set (or get) the visibility of one of the Exception Reporters control buttons
			/// </summary>
			get
			{
				//return visibility
				return visibleCopyButton;
			}
			set
			{
				// set visibility
				visibleCopyButton = value;
			}
		}
		
		private bool visibleEmailButton = true;
		
		public bool ShowEmailButton
		{
			/**********************************************************
			 * Property: ShowEmailButton
			 * 
			 *  If true the E-Mail Button is displayed on the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ShowEmailButton property. Used to set (or get) the visibility of one of the Exception Reporters control buttons
			/// </summary>
			/// <remarks>If it is known that the application is deployed into an environment that does not have mail capability
			/// then it is recommended that the email button be hidden.</remarks>
			get
			{
				//return visibility
				return visibleEmailButton;
			}
			set
			{
				// set visibility
				visibleEmailButton = value;
			}
		}
		private bool visibleSaveButton = true;
		public bool ShowSaveButton
		{
			/**********************************************************
			 * Property: ShowSaveButton
			 * 
			 *  If true the Save Button is displayed on the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ShowSaveButton property. Used to set (or get) the visibility of one of the Exception Reporters control buttons
			/// </summary>
			get
			{
				//return visibility
				return visibleSaveButton;
			}
			set
			{
				// set visibility
				visibleSaveButton = value;
			}
		}
		private bool visiblePrintButton = true;
		public bool ShowPrintButton
		{
			/**********************************************************
			 * Property: ShowPrintButton
			 * 
			 *  If true the Print Button is displayed on the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ShowPrintButton property. Used to set (or get) the visibility of one of the Exception Reporters control buttons
			/// </summary>
			get
			{
				//return visibility
				return visiblePrintButton;
			}
			set
			{
				// set visibility
				visiblePrintButton = value;
			}
		}

		// public property used to set/get contact email
		string strContactEmail;
		
		public String ContactEmail
		{
			/**********************************************************
			 * Property: ContactEmail
			 * 
			 *  The Contact Email address displayed by the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ContactEmail property. Used to set (or get) the email address to be displayed by the Exception Reporter
			/// </summary>
			/// <remarks>If no SendEmailAddress property is set, this address is also used as the recipient address when the
			/// Email button is pressed.</remarks>
			get
			{
				//return email
				return strContactEmail;
			}
			set
			{
				// set email
				strContactEmail = value;
				
			}
		}

		// public property used to set/get contact email
		string strContactWeb;
		
		public String ContactWeb
		{
			/**********************************************************
			 * Property: ContactWeb
			 * 
			 *  The Contact Web address displayed by the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ContactWeb property. Used to set (or get) the web address to be displayed as the support URL link by the Exception Reporter
			/// </summary>
			get
			{
				//return Web
				return strContactWeb;
			}
			set
			{
				// set Web
				strContactWeb = value;
				
			}
		}

		// public property used to set/get contact email
		string strContactPhone;
		
		public String ContactPhone
		{
			/**********************************************************
			 * Property: ContactWeb
			 * 
			 *  The Contact Phone displayed by the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ContactPhone property. Contact Phone number displayed by the Exception Reporter
			/// </summary>
			get
			{
				//return Phone
				return strContactPhone;
			}
			set
			{
				// set Phone
				strContactPhone = value;
				
			}
		}


		// public property used to set/get contact email
		string strContactFax;
		
		public String ContactFax
		{
			/**********************************************************
			 * Property: ContactFax
			 * 
			 *  The Contact Fax displayed by the Exception Reporter
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ContactFax property. Contact Fax number displayed by the Exception Reporter
			/// </summary>
			get
			{
				//return Fax
				return strContactFax;
			}
			set
			{
				// set Fax
				strContactFax = value;
				
			}
		}

		string strContactMessageTop = "The following details can be used to obtain support for this application..";
		public String ContactMessageTop
		{
			/**********************************************************
			 * Property: ContactMessageTop
			 * 
			 *  The Contact Message displayed above the contact details
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ContactMessageTop property. Message displayed above the contact details on the Contact tab of the Exception Reporter
			/// Can be set so that a custom message is displayed instead of the default.
			/// </summary>
			get
			{
				//return top contact message
				return strContactMessageTop;
			}
			set
			{
				// set top contact message
				strContactMessageTop = value;
				
			}
		}

		string strContactMessageBottom = "The information shown on this form describing the error and envrionment may be relevant when contacting support.";
		public String ContactMessageBottom
		{
			/**********************************************************
			 * Property: ContactMessageBottom
			 * 
			 *  The Contact Message displayed below the contact details
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ContactMessageBottom property. Message displayed below the contact details on the Contact tab of the Exception Reporter
			/// Can be set so that a custom message is displayed instead of the default.
			/// </summary>
			get
			{
				//return bottom contact message
				return strContactMessageBottom;
			}
			set
			{
				// set bottom contact message
				strContactMessageBottom = value;
				
			}
		}

		string strGeneralMessage = "An error has occured in this application.";
		public String GeneralMessage
		{
			/**********************************************************
			 * Property: GeneralMessage
			 * 
			 *  The General Message displayed at the top of the General Tab
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// GeneralMessage property. Message displayed at the top of the General tab of the Exception Reporter.
			/// Can be set so that a custom message is displayed instead of the default
			/// </summary>
			get
			{
				//return General Message
				return strGeneralMessage;
			}
			set
			{
				// set General Message
				strGeneralMessage = value;
				
			}
		}
		
		private slsMailType sendMailType;
		
		public slsMailType MailType
		{
			/**********************************************************
			 * Property: MailType
			 * 
			 *  Specifies wether SMTP or SimpleMAPI is used
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// MailType property. The Exception Reporter can use either MAPI or SMTP to send e-mail. This property
			/// is used to control the method the Exception Reporter will use. Values can be either slsMailType.SimpleMAPI or slsMailType.SMTP
			/// </summary>
			get
			{
				//return Mail Type
				return sendMailType;
			}
			set
			{
				// set Mail Type
				sendMailType = value;
				
			}
		}
		
		string strExplanationMessage = "Please enter a brief explanation detailing the actions and events leading up to the occurrence of this error.";
		public String ExplanationMessage
		{
			/**********************************************************
			 * Property: ExplanationMessage
			 * 
			 * The message displayed above the text box for entering
			 * User Explanation
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// ExplanationMessage property. Message displayed above the text box intended for the to type
			/// an explanatory message. Can be set to a custom message instead of the default.
			/// </summary>
			get
			{
				//return Explanation Message
				return strExplanationMessage;
			}
			set
			{
				// set Explanation Message
				strExplanationMessage = value;
				
			}
		}
		
		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
		public void LoadPropertiesFromConfig() {
			/**********************************************************
			 * Property: LoadPropertiesFromConfig
			 * 
			 * Loads Exception Reporter properties with values stored in
			 * the Applications Config file
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// Method used to set the Exception Reporters properties from the Application Config file.
			/// Where a property is not present in the config file, it is left unchanged.
			/// 
			/// Entries within the Config file can be one or more of
			/// SLS_ER_CONTACT_EMAIL - text
			/// SLS_ER_CONTACT_WEB - text
			/// SLS_ER_CONTACT_PHONE - text
			/// SLS_ER_CONTACT_FAX - text
			/// SLS_ER_SHOW_GENERAL - "Y" or "N"
			/// SLS_ER_SHOW_EXCEPTIONS - "Y" or "N"
			/// SLS_ER_SHOW_ASSEMBLIES - "Y" or "N"
			/// SLS_ER_SHOW_SETTINGS - "Y" or "N"
			/// SLS_ER_SHOW_ENVIRONMENT - "Y" or "N"
			/// SLS_ER_SHOW_CONTACT - "Y" or "N"
			/// SLS_ER_PRINT_BUTTON - "Y" or "N"
			/// SLS_ER_SAVE_BUTTON - "Y" or "N"
			/// SLS_ER_COPY_BUTTON - "Y" or "N"
			/// SLS_ER_EMAIL_BUTTON - "Y" or "N"
			/// SLS_ER_MAIL_TYPE - "SMTP" or "MAPI"
			/// SLS_ER_SMTP_SERVER - text
			/// SLS_ER_SMTP_USERNAME - text
			/// SLS_ER_SMTP_PASSWORD - text
			/// SLS_ER_SMTP_FROM - text
			/// SLS_ER_SEND_ADDRESS - text
			/// SLS_ER_GENERAL_MESSAGE - text
			/// SLS_ER_EXPLANATION_MESSAGE - text
			/// SLS_ER_CONTACT_MESSAGE_1 - text
			/// SLS_ER_CONTACT_MESSAGE_2 - text
			/// SLS_ER_SERIAL_NUMBER - text
			/// SLS_ENUMERATE_PRINTERS
			/// </summary>
			/// <example>An example set of entries in the config file (ie appname.exe.config) may look like:
			/// <?xml version="1.0" encoding="utf-8" ?>
			/// <configuration>
			/// \<appSettings>
			/// \<add key="SLS_ER_CONTACT_EMAIL" value="TheAddress@theserver.com"/> 
			/// \<add key="SLS_ER_CONTACT_PHONE" value="012 3456 7890"/> 
			/// \</appSettings>
			/// \</configuration>
			/// </example>
			/// <remarks>This method provides a flexible way of changing the Exception Reporters behaviour without having 
			/// to change properties on a form at design time and recompile.
			/// Important: This method must be explicitly called within your code, properties are not automatically read from the config file</remarks>
			try {
				
				// read the properties that DO exist in the config file
				// the assignIfNotNull function will return the currently assigned value if a property is missing from the file
				// or the value from the file where it exists
				ContactEmail = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_CONTACT_EMAIL"),ContactEmail);
				ContactWeb = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_CONTACT_WEB"),ContactWeb);
				ContactPhone = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_CONTACT_PHONE"),ContactPhone);
				ContactFax = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_CONTACT_FAX"),ContactFax);
				
				ShowAssembliesTab = assignBoolValue(ConfigurationSettings.AppSettings.Get("SLS_ER_SHOW_GENERAL"),ShowAssembliesTab);
				ShowExceptionsTab = assignBoolValue(ConfigurationSettings.AppSettings.Get("SLS_ER_SHOW_EXCEPTIONS"),ShowExceptionsTab);
				ShowAssembliesTab = assignBoolValue(ConfigurationSettings.AppSettings.Get("SLS_ER_SHOW_ASSEMBLIES"),ShowAssembliesTab);
				ShowSettingsTab = assignBoolValue(ConfigurationSettings.AppSettings.Get("SLS_ER_SHOW_SETTINGS"),ShowSettingsTab);
				ShowEnvironmentTab = assignBoolValue(ConfigurationSettings.AppSettings.Get("SLS_ER_SHOW_ENVIRONMENT"),ShowEnvironmentTab);
				ShowContactTab = assignBoolValue(ConfigurationSettings.AppSettings.Get("SLS_ER_SHOW_CONTACT"),ShowContactTab);
				
				ShowPrintButton= assignBoolValue(ConfigurationSettings.AppSettings.Get("SLS_ER_PRINT_BUTTON"),ShowPrintButton);
				ShowSaveButton = assignBoolValue(ConfigurationSettings.AppSettings.Get("SLS_ER_SAVE_BUTTON"),ShowSaveButton);
				ShowCopyButton = assignBoolValue(ConfigurationSettings.AppSettings.Get("SLS_ER_COPY_BUTTON"),ShowCopyButton);
				ShowEmailButton = assignBoolValue(ConfigurationSettings.AppSettings.Get("SLS_ER_EMAIL_BUTTON"),ShowEmailButton);
				
				EnumeratePrinters= assignBoolValue(ConfigurationSettings.AppSettings.Get("SLS_ENUMERATE_PRINTERS"),EnumeratePrinters);
				
				// determine the Mail Type (SMTP or Simple MAPI)
				string strCompare = "SMTP";		
				string strCompare2 = "SIMPLEMAPI";
				if (!(ConfigurationSettings.AppSettings.Get("SLS_ER_MAIL_TYPE") == null)) {
					if (strCompare.Equals(ConfigurationSettings.AppSettings.Get("SLS_ER_MAIL_TYPE"))) {
						sendMailType = slsMailType.SMTP;
					}
					strCompare = "MAPI";
					if (strCompare.Equals(ConfigurationSettings.AppSettings.Get("SLS_ER_MAIL_TYPE"))
						|| strCompare2.Equals(ConfigurationSettings.AppSettings.Get("SLS_ER_MAIL_TYPE"))) {
						sendMailType = slsMailType.SimpleMAPI;
					}
				}
				// mail settings
				SMTPServer = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_SMTP_SERVER"),SMTPServer);
				SMTPUsername = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_SMTP_USERNAME"),SMTPUsername);
				SMTPPassword = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_SMTP_PASSWORD"),SMTPPassword);
				SMTPFromAddress = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_SMTP_FROM"),SMTPFromAddress);
				SendEmailAddress = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_SEND_ADDRESS"),SendEmailAddress);
				
				// messages displayed on the form
				GeneralMessage = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_GENERAL_MESSAGE"),GeneralMessage);
				ExplanationMessage = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_EXPLANATION_MESSAGE"),ExplanationMessage);			
				ContactMessageTop = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_CONTACT_MESSAGE_1"),ContactMessageTop);
				ContactMessageBottom = assignIfNotNull(ConfigurationSettings.AppSettings.Get("SLS_ER_CONTACT_MESSAGE_2"),ContactMessageBottom);			
				
				
			} catch (Exception ex) {
				handleError("There has been a problem loading Exception Reporter properties from the config file.",ex);
			}
		
		}
		private string assignIfNotNull(string strNew, string strCurrent) {
			/**********************************************************
			 * assignIfNotNull
			 * 
			 * Pass: strNew - the new value to assign
			 * 		strCurrent - the current value
			 * 
			 * Returns: the new value if it is specified, OR the current 
			 * value if the new value is not specified
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// Returns the strNew if it is not NULL or 0 length, otherwise the current value is returned
			/// </summary>
			if (strNew == null) {
				return strCurrent;
			} 
			if (strNew.Length == 0) {
				return strCurrent;
			}
			return strNew;
			
		}
		private bool assignBoolValue(string strNew, bool boolCurrent) {
			/**********************************************************
			 * assignBoolValue
			 * 
			 * Pass: strNew - the new value to assign
			 * 		strCurrent - the current value
			 * 
			 * Returns: boolean the new value if it is specified 
			 * (and represents a true or false value) "Y" or "N", OR the current 
			 * value if the new value is not specified
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// Returns the true if strNew is a string representing Yes (Y) or false if it represents No (N)
			/// if strNew is NULL or zero length the current value is returned
			/// </summary>
			if (strNew == null) {
				return boolCurrent;
			}
			if (strNew.Length == 0) {
				return boolCurrent;
			}
			if (strNew.Equals("Y")) {
					return true;
			}
			if (strNew.Equals("N")) {
					return false;
			}
			return boolCurrent;
			
		}
		
		public void DisplayException(System.Exception ex) {
			/**********************************************************
			 * DisplayException
			 * 
			 * Main method that begins the Exception Reporting process
			 * 
			 * Pass: ex - the exception to display details for
			 * Returns: void
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
		
			if (ex == null){
				// no exception was provided
				MessageBox.Show("The Exception Reporter was called, but no Exception was provided!","Exception Reporter called without an Exception");
				return;
			}
			
			try {
				// created a new Exception display form
				frmER frmException = new frmER();
				
				// set the properties of the form based on the properties assigned to this object
				
				frmException.EnumeratePrinters = this.EnumeratePrinters;
				
				// contact details
				frmException.ContactEmail = strContactEmail;
				frmException.ContactWeb = strContactWeb;
				frmException.ContactPhone = strContactPhone;
				frmException.ContactFax = strContactFax;
				// display tabs
				frmException.ShowAssembliesTab = blnAssembliesTab;
				frmException.ShowEnvironmentTab = blnEnvironmentTab;
				frmException.ShowGeneralTab = blnGeneralTab;
				frmException.ShowSettingsTab = blnSettingsTab;
				frmException.ShowContactTab = blnContactTab;
				frmException.ShowExceptionsTab = blnExceptionsTab;
				// determine which buttons to display
				frmException.ShowPrintButton = visiblePrintButton;
				frmException.ShowCopyButton = visibleCopyButton;
				frmException.ShowSaveButton = visibleSaveButton;
				frmException.ShowEmailButton = visibleEmailButton;
				// mail settings
				frmException.MailType = sendMailType;
				frmException.SMTPServer = strSMTPServer;
				frmException.SMTPUsername = strSMTPUsername;
				frmException.SMTPPassword = strSMTPPassword;
				frmException.SMTPFromAddress = strSMTPFromAddress;
				frmException.SendEmailAddress = strSendEmailAddress;
				// messages shown on the form
				frmException.GeneralMessage = strGeneralMessage;
				frmException.ExplanationMessage = strExplanationMessage;
				frmException.ContactMessageBottom = ContactMessageBottom;
				frmException.ContactMessageTop = ContactMessageTop;
				// call the main method that starts the display
				frmException.displayException(ex,Assembly.GetCallingAssembly(),blnLicensed);	
			} catch (Exception exc) {
				handleError("There has been a problem displaying the Exception Reporter",exc);
			}
		}
		
		private void handleError(string strMessage,Exception ex) {
			/**********************************************************
			 * Handle the occurrence of application error
			 * 
			 * Pass:	strMessage - message to display in form
			 * 			ex - the exception that has occurred
			 * Returns: Nothing
			 * 
			 * 
			 * Developer		Date		Comment  
			 * Phillip Pettit	Mar/04		Initial Creation
			 **********************************************************/
			/// <summary>
			/// Handles application error by displaying a simple error form to the user
			/// The form contains 2 tabs, one for simple information and the other for a more detailed
			/// exception message
			/// </summary>

			frmSimpleException frm = new frmSimpleException();
			frm.ShowException(strMessage,ex);
		}
	}
}
