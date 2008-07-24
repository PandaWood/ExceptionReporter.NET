using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Management;
using System.Reflection;
using System.Text;
using System.Web.Mail;
using System.Windows.Forms;
using Win32Mapi;

namespace ExceptionReporting.Views
{
	public partial class ExceptionReportView : Form, IExceptionReportView
	{
		private Exception exSelected;
		private StringBuilder sbExceptionString;
		private StringBuilder sbPrintString;
		private StringReader sPrintReader;
		private int intCharactersLine;
		private int intLinesPage;
		private Font printFont;
		private Font boldFont;
		private int drawWidth;
		private int drawHeight;
		private int PageCount;
		private ExceptionReporter.slsMailType sendMailType = ExceptionReporter.slsMailType.SimpleMAPI;
		private Assembly cAssembly;
		private bool refreshData;
		private String strSendEmailAddress;
		private String strSMTPServer;

		private ExceptionReportPresenter _presenter;

		public ExceptionReportView()
		{
			InitializeComponent();

			WireUpEvents();
			_presenter = new ExceptionReportPresenter(this);
		}

		private void WireUpEvents()
		{
			btnEmail.Click += cmdEmail_Click;
			btnPrint.Click += cmdPrint_Click;
			lstExceptions.SelectedIndexChanged += LstExceptionsSelectedIndexChanged;
			lblApplication.Click += lblApplication_Click;
			btnCopy.Click += cmdCopy_Click;
			lnkEmail.LinkClicked += lnkEmail_LinkClicked;
			btnSave.Click += cmdSave_Click;
			lnkWeb.LinkClicked += LnkWebLinkClicked;
		}

		private void lblApplication_Click(object sender, EventArgs e)
		{
			//TODO 
		}

		public void sendMAPIEmail()
		{
			try
			{
				var ma = new Mapi();
				ma.Logon(Handle);

				ma.Reset();
				if (strSendEmailAddress != null)
				{
					if (strSendEmailAddress.Length > 0)
					{
						ma.AddRecip(strSendEmailAddress, null, false);
					}
				}

				ma.Send("An Exception has occured", sbExceptionString.ToString(), true);
				ma.Logoff();
			}
			catch (Exception ex)
			{
				handleError(
					"There has been a problem sending e-mail through Simple MAPI. A suitable mail client or required protocols may not be configured on the machine. Instead, you can use the copy button to place details of the error onto the clipboard, you can then paste this information directly into your mail client",
					ex);
			}
		}

		public void sendSMTPEmail()
		{
			try
			{
				var objMyMessage = new MailMessage
				                   	{
				                   		To = strSendEmailAddress,
				                   		From = strSMTPFromAddress,
				                   		Subject = "An Error has occured",
				                   		Body = sbExceptionString.ToString(),
				                   		BodyFormat = MailFormat.Text
				                   	};

#if SMTP_AUTH
	// conditionally include support for SMTP authentication
				if (strSMTPUsername != null)
				{
					objMyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
					//basic authentication
					objMyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendUSERNAME", strSMTPUsername);
					//set your USERNAME here

					if (strSMTPPassword != null)
					{
						objMyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", strSMTPPassword);
						//set your password here
					}
				}
#endif

				if (strSMTPServer != null)
				{
					SmtpMail.SmtpServer = strSMTPServer;
				}

				SmtpMail.Send(objMyMessage);
			}
			catch (Exception ex)
			{
				handleError(
					"There has been a problem sending e-mail through SMTP. Suitable configuration details or required protocols may not be configured on the machine. Instead, you can use the copy button to place details of the error onto the clipboard, you can then paste this information directly into your mail client",
					ex);
			}
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

		private void setTabs()
		{
			try
			{
				// remove all the tabs to start with
				tcTabs.TabPages.Clear();

				// add back the tabs one by one that have the appropriate
				// property set
				if (blnGeneralTab)
				{
					tcTabs.TabPages.Add(tpGeneral);
				}
				if (blnExceptionsTab)
				{
					tcTabs.TabPages.Add(tpExceptions);
				}
				if (blnAssembliesTab)
				{
					tcTabs.TabPages.Add(tpAssemblies);
				}
				if (blnSettingsTab)
				{
					tcTabs.TabPages.Add(tpSettings);
				}
				if (blnEnvironmentTab)
				{
					tcTabs.TabPages.Add(tpEnvironment);
				}
				if (blnContactTab)
				{
					tcTabs.TabPages.Add(tpContact);
				}
			}
			catch (Exception ex)
			{
				handleError("There has been a problem configuring tab page display within the Exception Reporter", ex);
			}
			return;
		}

		private void setButtons()
		{
			const int intXSpacer = 10;

			try
			{
				int intX2Pos = Width - 30;

				if (btnEmail.Visible)
				{
					btnEmail.Left = intX2Pos - btnEmail.Width;
					intX2Pos -= btnEmail.Width;
					intX2Pos -= intXSpacer;
				}
				if (btnSave.Visible)
				{
					btnSave.Left = intX2Pos - btnSave.Width;
					intX2Pos -= btnSave.Width;
					intX2Pos -= intXSpacer;
				}
				if (btnCopy.Visible)
				{
					btnCopy.Left = intX2Pos - btnCopy.Width;
					intX2Pos -= btnCopy.Width;
					intX2Pos -= intXSpacer;
				}
				if (btnPrint.Visible)
				{
					btnPrint.Left = intX2Pos - btnPrint.Width;
				}
			}
			catch (Exception ex)
			{
				handleError("There has been a problem configuring command button display within the Exception Reporter", ex);
			}
			return;
		}

		// public property used to set/get visibility of Tab
		public bool ShowGeneralTab
		{
			get { return blnGeneralTab; }
			set
			{
				blnGeneralTab = value;
				setTabs();
			}
		}

		public bool EnumeratePrinters
		{
			get { return blnEnumeratePrinters; }
			set { blnEnumeratePrinters = value; }
		}

		public bool ShowEnvironmentTab
		{
			get { return blnEnvironmentTab; }
			set
			{
				blnEnvironmentTab = value;
				setTabs();
			}
		}

		public bool ShowAssembliesTab
		{
			get { return blnAssembliesTab; }
			set
			{
				blnAssembliesTab = value;
				setTabs();
			}
		}

		public string ProgressMessage
		{
			set { throw new NotImplementedException(); }		//TODO add a progress message label
		}

		public bool EnableEmailButton
		{
			set { btnEmail.Enabled = value; }
		}

		public String SMTPServer
		{
			get { return strSMTPServer; }
			set { strSMTPServer = value; }
		}

		public string SMTPUsername { get; set; }

		public String SMTPPassword { get; set; }

		private String strSMTPFromAddress;

		public String SMTPFromAddress
		{
			get { return strSMTPFromAddress; }
			set { strSMTPFromAddress = value; }
		}

		public string EmailToSendTo
		{
			get { throw new NotImplementedException(); }
		}

		public bool ShowProgressBar
		{
			set { throw new NotImplementedException(); }
		}

		public void HandleError(string message, Exception ex)
		{
			var simpleExceptionView = new SimpleExceptionView();
			simpleExceptionView.ShowException(message, ex);
		}

		public void SetSendCompleteState()
		{
//			lblProgressMessage.Text = "Email sent.";		//TODO add these ui elements
			progressBar.Visible = false;
//			btnEmail.Enabled = true;
		}

		public String SendEmailAddress
		{
			get { return strSendEmailAddress; }
			set { strSendEmailAddress = value; }
		}

		public bool ShowSettingsTab
		{
			get { return blnSettingsTab; }
			set
			{
				blnSettingsTab = value;
				setTabs();
			}
		}

		public bool ShowContactTab
		{
			get { return blnContactTab; }
			set
			{
				blnContactTab = value;
				setTabs();
			}
		}

		public bool ShowExceptionsTab
		{
			get { return blnExceptionsTab; }
			set
			{
				blnExceptionsTab = value;
				setTabs();
			}
		}

		public bool ShowCopyButton
		{
			get { return btnCopy.Visible; }
			set
			{
				btnCopy.Visible = value;
				setButtons();
			}
		}

		public bool ShowEmailButton
		{
			get { return btnEmail.Visible; }
			set
			{
				btnEmail.Visible = value;
				setButtons();
			}
		}

		public bool ShowSaveButton
		{
			get { return btnSave.Visible; }
			set
			{
				btnSave.Visible = value;
				setButtons();
			}
		}

		public bool ShowPrintButton
		{
			get { return btnPrint.Visible; }
			set
			{
				btnPrint.Visible = value;
				setButtons();
			}
		}

		public String ContactEmail
		{
			get { return lnkEmail.Text; }
			set { lnkEmail.Text = value; }
		}

		public String ContactWeb
		{
			get { return lnkWeb.Text; }
			set { lnkWeb.Text = value; }
		}

		public String ContactPhone
		{
			get { return txtPhone.Text; }
			set { txtPhone.Text = value; }
		}

		public String ContactFax
		{
			get { return txtFax.Text; }
			set { txtFax.Text = value; }
		}

		public String ContactMessageTop
		{
			get { return lblContactMessageTop.Text; }
			set { lblContactMessageTop.Text = value; }
		}

		public String ContactMessageBottom
		{
			get { return lblContactMessageBottom.Text; }
			set { lblContactMessageBottom.Text = value; }
		}

		public String GeneralMessage
		{
			get { return lblGeneral.Text; }
			set { lblGeneral.Text = value; }
		}

		public ExceptionReporter.slsMailType MailType
		{
			get { return sendMailType; }
			set { sendMailType = value; }
		}

		public String ExplanationMessage
		{
			get { return lblExplanation.Text; }
			set { lblExplanation.Text = value; }
		}

		private void buildExceptionString()
		{
			buildExceptionString(true, true, true, true, true, true, false);
			return;
		}

		private void buildExceptionString(bool blnGeneral, bool blnExceptions, bool blnAssemblies, bool blnSettings,
		                                  bool blnEnvironment, bool blnContact, bool blnForPrint)
		{
			try
			{
				sbExceptionString = new StringBuilder();
				var swWriter = new StringWriter(sbExceptionString);


				if (blnGeneral)
				{
					if (!blnForPrint)
					{
						swWriter.WriteLine(lblGeneral.Text);
						swWriter.WriteLine((String) null);
						swWriter.WriteLine("-----------------------------");
						swWriter.WriteLine((String) null);
					}
					swWriter.WriteLine("General");
					swWriter.WriteLine((String) null);
					swWriter.WriteLine("Application: " + txtApplication.Text);
					swWriter.WriteLine("Version:     " + txtVersion.Text);
					swWriter.WriteLine("Region:      " + txtRegion.Text);
					swWriter.WriteLine("Machine:     " + " " + txtMachine.Text);
					swWriter.WriteLine("User:        " + txtUserName.Text);
					swWriter.WriteLine("-----------------------------");
					if (!blnForPrint)
					{
						swWriter.WriteLine((String) null);
						swWriter.WriteLine("Date: " + txtDate.Text);
						swWriter.WriteLine("Time: " + txtTime.Text);
						swWriter.WriteLine("-----------------------------");
					}
					swWriter.WriteLine((String) null);
					swWriter.WriteLine("Explanation");
					swWriter.WriteLine(txtExplanation.Text.Trim());
					swWriter.WriteLine((String) null);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String) null);
				}

				if (blnExceptions)
				{
					swWriter.WriteLine("Exceptions");
					swWriter.WriteLine((String) null);
					exceptionHeirarchyToString(swWriter);
					swWriter.WriteLine((String) null);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String) null);
				}

				if (blnAssemblies)
				{
					swWriter.WriteLine("Assemblies");
					swWriter.WriteLine((String) null);
					referencedAssembliesToString(swWriter);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String) null);
				}

				if (blnSettings)
				{
					treeToString(tvwSettings, swWriter);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String) null);
				}

				if (blnEnvironment)
				{
					treeToString(tvwEnvironment, swWriter);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String) null);
				}

				if (blnContact)
				{
					swWriter.WriteLine("Contact");
					swWriter.WriteLine((String) null);
					swWriter.WriteLine("E-Mail: " + lnkEmail.Text);
					swWriter.WriteLine("Web:    " + lnkWeb.Text);
					swWriter.WriteLine("Phone:  " + txtPhone.Text);
					swWriter.WriteLine("Fax:    " + txtFax.Text);
					swWriter.WriteLine("-----------------------------");
					swWriter.WriteLine((String) null);
				}
			}
			catch (Exception ex)
			{
				handleError(
					"There has been a problem building exception details into a string for printing, copying, saving or e-mailing", ex);
			}

			return;
		}

		private static void treeToString(TreeView tvConvert, TextWriter swTreeWriter)
		{
			treeNodeToString(tvConvert.Nodes[0], swTreeWriter, 0);
			return;
		}

		private static void treeNodeToString(TreeNode tnNode, TextWriter swWriter, int level)
		{
			String space = "";

			for (Int32 intCount = 0; intCount < (level*4); intCount++)
			{
				space = space + " ";
			}
			if (level <= 2)
			{
				swWriter.WriteLine((String) null);
			}
			swWriter.WriteLine(space + tnNode.Text);
			foreach (TreeNode tnChild in tnNode.Nodes)
			{
				treeNodeToString(tnChild, swWriter, level + 1);
			}
			//bubble error back
			return;
		}

		private void referencedAssembliesToString(TextWriter swWriter)
		{
			if (cAssembly == null)
			{
				return;
			}
			foreach (AssemblyName a in
				cAssembly.GetReferencedAssemblies())
			{
				swWriter.WriteLine(a.FullName);
				swWriter.WriteLine((String) null);
			}
			//bubble error back

			return;
		}

		/// <summary>
		/// convert a an exception and it's hierarchy of inner exceptions to a string for use 
		/// within the Exception String (for printing etc)
		/// </summary>
		/// <param name="swWriter"></param>
		private void exceptionHeirarchyToString(TextWriter swWriter)
		{
			int intCount = 0;
			Exception current = exSelected;

			while (current != null)
			{
				if (intCount == 0)
				{
					swWriter.WriteLine("Top Level Exception");
				}
				else
				{
					swWriter.WriteLine("Inner Exception " + intCount);
				}
				swWriter.WriteLine("Type:        " + current.GetType());
				swWriter.WriteLine("Message:     " + current.Message);
				swWriter.WriteLine("Source:      " + current.Source);
				swWriter.WriteLine("Stack Trace: " + current.StackTrace.Trim());
				swWriter.WriteLine((String) null);

				current = current.InnerException;
				intCount++;
			}
			// bubble error back
			return;
		}

		private static void wrapText(TextReader sr, TextWriter sw, int intMaxLineChars)
		{
			String strSubLine;

			string strLine = sr.ReadLine();
			while (strLine != null)
			{
				// handle blank lines
				if (strLine.Length == 0)
				{
					sw.WriteLine(strLine);
				}

				// handle long lines
				while (strLine.Length > intMaxLineChars)
				{
					strSubLine = strLine.Substring(0, intMaxLineChars);
					int intPos = strSubLine.LastIndexOf(" ");
					if (intPos > intMaxLineChars - 7)
					{
						// ie if space occurs within last set of characters
						// then wrap at the space (not in the middle of a word)
						strSubLine = strSubLine.Substring(0, intPos);
					}
					sw.WriteLine(strSubLine);
					strLine = strLine.Substring(strSubLine.Length);
				}

				// now just add remaining chars if there are any
				if (strLine.Length > 0)
				{
					sw.WriteLine(strLine);
				}

				// get the next line
				strLine = sr.ReadLine();
			}
			// bubble error back
			return;
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			try
			{
				// only refresh when we need to
				if (!refreshData)
				{
					return;
				}
				// next time we won't refresh unless this flag is set back to true
				refreshData = false;

				setButtons();
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();

				lblProgress.Visible = true;
				progressBar.Visible = true;
				progressBar.Maximum = 14;

				progressBar.Value = 0;

				// general tab
				txtDate.Text = DateTime.Now.ToShortDateString();
				txtTime.Text = DateTime.Now.ToLongTimeString();
				txtUserName.Text = Environment.UserName;
				txtMachine.Text = Environment.MachineName;

				txtRegion.Text = Application.CurrentCulture.DisplayName;
				txtApplication.Text = Application.ProductName;
				txtVersion.Text = Application.ProductVersion;

				progressBar.Value = progressBar.Value + 1;
				Application.DoEvents();

				var root = new TreeNode("Environment");

				try
				{
					addEnvironmentNode2("Operating System", "Win32_OperatingSystem", root, false, "");
				}
				catch
				{
					// do nothing, some environment nodes aren't available on all machines
				}
				finally
				{
					progressBar.Value++;
					Application.DoEvents();
				}
				try
				{
					addEnvironmentNode2("CPU", "Win32_Processor", root, true, "");
				}
				catch
				{
					// do nothing, some environment nodes aren't available on all machines
				}
				finally
				{
					progressBar.Value = progressBar.Value + 1;
					Application.DoEvents();
				}
				try
				{
					addEnvironmentNode2("Memory", "Win32_PhysicalMemory", root, true, "");
				}
				catch
				{
					// do nothing, some environment nodes aren't available on all machines
				}
				finally
				{
					progressBar.Value = progressBar.Value + 1;
					Application.DoEvents();
				}

				try
				{
					addEnvironmentNode2("Drives", "Win32_DiskDrive", root, true, "");
				}
				catch
				{
					// do nothing, some environment nodes aren't available on all machines
				}
				finally
				{
					progressBar.Value = progressBar.Value + 1;
					Application.DoEvents();
				}

				try
				{
					addEnvironmentNode2("Environment Variables", "Win32_Environment", root, true, "");
				}
				catch
				{
					// do nothing, some environment nodes aren't available on all machines
				}
				finally
				{
					progressBar.Value = progressBar.Value + 1;
					Application.DoEvents();
				}

				try
				{
					if (EnumeratePrinters)
					{
						addEnvironmentNode2("Printers", "Win32_Printer", root, true, "");
					}
				}
				catch
				{
					// do nothing, some environment nodes aren't available on all machines
				}
				finally
				{
					progressBar.Value = progressBar.Value + 1;
					Application.DoEvents();
				}

				try
				{
					addEnvironmentNode2("System", "Win32_ComputerSystem", root, true, "");
				}
				catch
				{
					// do nothing, some environment nodes aren't available on all machines
				}
				finally
				{
					progressBar.Value = progressBar.Value + 1;
					Application.DoEvents();
				}

				tvwEnvironment.Nodes.Add(root);
				root.Expand();

				// fill the settings tab
				var settingsRoot = new TreeNode("Application Settings");

				IEnumerator ienum = ConfigurationSettings.AppSettings.GetEnumerator();

				while (ienum.MoveNext())
				{
					settingsRoot.Nodes.Add(
						new TreeNode(ienum.Current + " : " + ConfigurationSettings.AppSettings.Get(ienum.Current.ToString())));
				}

				tvwSettings.Nodes.Add(settingsRoot);
				settingsRoot.Expand();
				progressBar.Value = progressBar.Value + 1;
				Application.DoEvents();

				buildExceptionHeirarchy(exSelected);
				progressBar.Value = progressBar.Value + 1;
				Application.DoEvents();

				lstAssemblies.Clear();
				lstAssemblies.Columns.Add("Name", 100, HorizontalAlignment.Left);
				lstAssemblies.Columns.Add("Version", 150, HorizontalAlignment.Left);
				lstAssemblies.Columns.Add("Culture", 150, HorizontalAlignment.Left);

				foreach (AssemblyName assemblyName in cAssembly.GetReferencedAssemblies())
				{
					var listViewItem = new ListViewItem {Text = assemblyName.Name};
					listViewItem.SubItems.Add(assemblyName.Version.ToString());
					listViewItem.SubItems.Add(assemblyName.CultureInfo.EnglishName);
					lstAssemblies.Items.Add(listViewItem);
				}

				progressBar.Value = progressBar.Maximum;
				Application.DoEvents();

				lblProgress.Visible = false;
				progressBar.Visible = false;

				setTabs();

				Cursor = Cursors.Default;
			}
			catch (Exception ex)
			{
				handleError("There has been a problem setting up the Exception Reporter display", ex);
			}
		}

		public bool displayException(Exception ex, Assembly callingAssembly)
		{
			cAssembly = callingAssembly;
			exSelected = ex;
			refreshData = true;

			ShowDialog();

			return true;
		}

		private void buildExceptionHeirarchy(Exception e)
		{
			try
			{
				lstExceptions.Clear();

				lstExceptions.Columns.Add("Level", 100, HorizontalAlignment.Left);
				lstExceptions.Columns.Add("Exception Type", 150, HorizontalAlignment.Left);
				lstExceptions.Columns.Add("Target Site / Method", 150, HorizontalAlignment.Left);


				var listViewItem = new ListViewItem {Text = "Top Level"};
				listViewItem.SubItems.Add(e.GetType().ToString());
				listViewItem.SubItems.Add(e.TargetSite.ToString());
				listViewItem.Tag = "0";
				lstExceptions.Items.Add(listViewItem);
				listViewItem.Selected = true;

				int intIndex = 0;

				Exception exCurrent = e;
				bool blnContinue = (exCurrent.InnerException != null);
				while (blnContinue)
				{
					intIndex++;
					exCurrent = exCurrent.InnerException;
					listViewItem = new ListViewItem {Text = ("Inner Exception " + intIndex)};
					listViewItem.SubItems.Add(exCurrent.GetType().ToString());
					listViewItem.SubItems.Add(exCurrent.TargetSite.ToString());
					listViewItem.Tag = intIndex.ToString();
					lstExceptions.Items.Add(listViewItem);

					blnContinue = (exCurrent.InnerException != null);
				}
				txtStackTrace.Text = e.StackTrace;
				txtMessage.Text = e.Message;
			}
			catch (Exception ex)
			{
				handleError("There has been a problem building the Exception Heirarchy list", ex);
			}
		}


		private static void addEnvironmentNode2(String strCaption, String strClass, TreeNode parentNode, Boolean blnUseName, String strWhere)
		{
			try
			{
				String strDisplayField = blnUseName ? "Name" : "Caption";

				var tn = new TreeNode(strCaption);

				var searcher = new ManagementObjectSearcher("SELECT * FROM " + strClass + " " + strWhere);

				foreach (ManagementObject mo in searcher.Get())
				{
					var tn2 = new TreeNode(mo.GetPropertyValue(strDisplayField).ToString().Trim());
					tn.Nodes.Add(tn2);
					foreach (PropertyData iPropData in mo.Properties)
					{
						var propertyNode = new TreeNode(iPropData.Name + ':' + Convert.ToString(iPropData.Value));
						tn2.Nodes.Add(propertyNode);
					}
				}
				parentNode.Nodes.Add(tn);
			}
			catch (Exception ex)
			{
				handleError("There has been a problem adding an Environment node.", ex);
				return;
			}
		}

		private void cmdPrint_Click(object sender, EventArgs e)
		{
			var printSelectView = new PrintSelectionView();
			bool showGeneral = false;
			bool showExceptions = false;
			bool showAssemblies = false;
			bool showSettings = false;
			bool showEnvironment = false;
			bool showContact = false;

			try
			{
				if (
					!printSelectView.selectPrintDetails(ref showGeneral, ref showExceptions, ref showAssemblies, ref showSettings, ref showEnvironment,
					                          ref showContact))
				{
					//user has cancelled print
					return;
				}

				if (showGeneral == false && showExceptions == false && showAssemblies == false && showSettings == false &&
				    showEnvironment == false && showContact == false)
				{
					MessageBox.Show("No items have been selected for print. Printing has been cancelled.", "Printing Cancelled");
					return;
				}

				buildExceptionString(showGeneral, showExceptions, showAssemblies, showSettings, showEnvironment, showContact, true);

				PrintEventHandler peHandler = printDocument1_BeginPrint;
				printDocument1.BeginPrint += peHandler;
			}
			catch (Exception ex)
			{
				handleError("There has been a problem preparing to Print", ex);
			}


			printDialog1.Document = printDocument1;
			DialogResult dr = printDialog1.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				try
				{
					printDocument1.Print();
				}
				catch (Exception ex)
				{
					handleError("There has been a problem printing", ex);
				}
			}
		}

		private void lnkEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				String strLink = lnkEmail.Text;

				if (!strLink.Substring(0, 7).ToUpper().Equals("MAILTO:"))
				{
					strLink = "MailTo:" + strLink;
				}

				Process.Start(strLink);
			}
			catch (Exception ex)
			{
				handleError("There has been a problem handling the e-mail link", ex);
			}
		}


		private void cmdCopy_Click(object sender, EventArgs e)
		{
			try
			{
				buildExceptionString();
				Clipboard.SetDataObject(sbExceptionString.ToString(), true);
			}
			catch (Exception ex)
			{
				handleError("There has been a problem copying to clipboard", ex);
			}
		}

		private void cmdEmail_Click(object sender, EventArgs e)
		{
			buildExceptionString();
			try
			{
				if (strSendEmailAddress == null)
				{
					strSendEmailAddress = lnkEmail.Text;
				}

				if (sendMailType == ExceptionReporter.slsMailType.SimpleMAPI)
				{
					sendMAPIEmail();
				}
				if (sendMailType == ExceptionReporter.slsMailType.SMTP)
				{
					if (strSendEmailAddress != null)
					{
						sendSMTPEmail();
					}
					else
					{
						MessageBox.Show(
							"It is not possible to send e-mail as a recipient address has not been configured by the application.",
							"To Address Missing");
					}
				}
			}
			catch (Exception ex)
			{
				handleError("There has been a problem sending e-mail", ex);
			}
		}


		private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
		{
			PageCount = 0;
		}

		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			int leftMargin = e.MarginBounds.Left;
			int rightMargin = e.MarginBounds.Right;
			int topMargin = e.MarginBounds.Top;
			bool blnSkip;
			int intCount = 0;

			PageCount ++;
			if (PageCount == 1)
			{
				printFont = new Font("Courier New", 12);
				boldFont = new Font("Courier New", 12, FontStyle.Bold);
			}

			SizeF fSize = e.Graphics.MeasureString("WWWWWWWWWWWWWWWWWWWWWWWWWWWWWW", printFont);
			float fltFontWidth = fSize.Width/30;

			if (PageCount == 1)
			{
				// setup for first page
				drawWidth = e.MarginBounds.Size.Width; //- (e.MarginBounds.Left + e.MarginBounds.Right);
				drawHeight = e.MarginBounds.Size.Height; //- (e.MarginBounds.Top+ e.MarginBounds.Bottom);

				intCharactersLine = (int) (drawWidth/fltFontWidth); //fSize.ToSize().Width;
				intLinesPage = (int) (drawHeight/printFont.GetHeight());


				sbPrintString = new StringBuilder();
				var swPrint = new StringWriter(sbPrintString);
				var srException = new StringReader(sbExceptionString.ToString());
				wrapText(srException, swPrint, intCharactersLine);
				sPrintReader = new StringReader(sbPrintString.ToString());
			}
			// draw the border
			var rect = new Rectangle(leftMargin, topMargin, drawWidth, drawHeight);
			e.Graphics.DrawRectangle(Pens.Black, rect);

			//draw the header
			string strLine = "Error Report: " + txtApplication.Text;
			e.Graphics.DrawString(strLine, boldFont, Brushes.Black, leftMargin, topMargin + ((intCount)*printFont.GetHeight()));
			intCount++;
			strLine = "Date/Time:    " + txtDate.Text + " " + txtTime.Text;
			e.Graphics.DrawString(strLine, boldFont, Brushes.Black, leftMargin, topMargin + ((intCount)*printFont.GetHeight()));
			intCount++;
			e.Graphics.DrawLine(Pens.Black, leftMargin, topMargin + ((intCount)*printFont.GetHeight()), rightMargin,
			                    topMargin + ((intCount)*printFont.GetHeight()));
			intCount++; // leave a space from header


			// draw the footer
			strLine = "Page: " + PageCount;
			e.Graphics.DrawLine(Pens.Black, leftMargin, topMargin + ((intLinesPage - 2)*printFont.GetHeight()), rightMargin,
			                    topMargin + ((intLinesPage - 2)*printFont.GetHeight()));
			e.Graphics.DrawString(strLine, boldFont, Brushes.Black, leftMargin,
			                      topMargin + ((intLinesPage - 1)*printFont.GetHeight()));


			//loop for the number of lines a page
			while (intCount <= (intLinesPage - 3)) // - 1 because of footer
			{
				Font currentFont = printFont;
				blnSkip = false;
				// read the line
				strLine = sPrintReader.ReadLine();
				if (strLine == null)
				{
					intCount = intLinesPage + 1; //exit the loop
				}
				else
				{
					if (strLine.Length >= 5)
					{
						if (strLine.Substring(1, 4).Equals("----"))
						{
							//draw a seperator line
							e.Graphics.DrawLine(Pens.Black, leftMargin, topMargin + ((intCount)*printFont.GetHeight()), rightMargin,
							                    topMargin + ((intCount)*printFont.GetHeight()));
							blnSkip = true;
						}
					}
					if (!blnSkip)
					{
						// check if the line should be bold
						if (strLine.Equals("General") || strLine.Equals("Exceptions") || strLine.Equals("Explanation") ||
						    strLine.Equals("Assemblies") || strLine.Equals("Application Settings") || strLine.Equals("Environment") ||
						    strLine.Equals("Contact"))
						{
							currentFont = boldFont;
						}

						// output the text line
						e.Graphics.DrawString(strLine, currentFont, Brushes.Black, leftMargin,
						                      topMargin + ((intCount)*printFont.GetHeight()));
					}
				}
				intCount++;
			}

			e.HasMorePages = sPrintReader.Peek() != -1;
			// let error bubble back
		}


		~ExceptionReportView()
		{
			Dispose();
		}

		private void cmdSave_Click(object sender, EventArgs e)
		{
			try
			{
				buildExceptionString();

				Stream strStream;
				var dlgSave = new SaveFileDialog
				              	{
				              		Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*",
				              		FilterIndex = 1,
				              		RestoreDirectory = true
				              	};

				if (dlgSave.ShowDialog() == DialogResult.OK)
				{
					if ((strStream = dlgSave.OpenFile()) != null)
					{
						var strWriter = new StreamWriter(strStream);

						strWriter.Write(sbExceptionString.ToString());

						strStream.Close();
					}
				}
			}
			catch (Exception ex)
			{
				handleError("There has been a problem saving error details to file", ex);
			}
		}


		private void LstExceptionsSelectedIndexChanged(object sender, EventArgs e)
		{
			Exception displayException = exSelected;
			try
			{
				foreach (ListViewItem lvi in lstExceptions.Items)
				{
					if (!lvi.Selected) continue;
					// work out which exception to display
					for (Int32 intCount = 0; intCount < Int32.Parse(lvi.Tag.ToString()); intCount++)
					{
						displayException = displayException.InnerException;
					}
				}

				txtStackTrace.Text = "";
				txtMessage.Text = "";
				if (displayException == null) displayException = exSelected;
				if (!(displayException == null))
				{
					txtStackTrace.Text = displayException.StackTrace;
					txtMessage.Text = displayException.Message;
				}
			}
			catch (Exception ex)
			{
				handleError("There has been a problem handling the change of selected exception", ex);
			}
		}

		private void LnkWebLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				String strLink = lnkWeb.Text;
				Process.Start(strLink);
			}
			catch (Exception ex)
			{
				handleError("There has been a problem handling the web link click", ex);
			}
		}

		private static void handleError(string strMessage, Exception ex)
		{
			var simpleExceptionView = new SimpleExceptionView();
			simpleExceptionView.ShowException(strMessage, ex);
		}
	}
}
