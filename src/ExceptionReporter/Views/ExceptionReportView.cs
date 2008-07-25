using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;
using System.Windows.Forms;

namespace ExceptionReporting.Views
{
	public partial class ExceptionReportView : Form, IExceptionReportView
	{
		private ExceptionReporter.slsMailType _sendMailType = ExceptionReporter.slsMailType.SimpleMAPI;
		private bool _refreshData;
		private readonly ExceptionReportPresenter _presenter;

		public ExceptionReportView()
		{
			InitializeComponent();

			SetDefaults();
			WireUpEvents();
			_presenter = new ExceptionReportPresenter(this);
		}

		private void SetDefaults()
		{
			ShowAssembliesTab = true;
			ShowEnvironmentTab = true;
			EnumeratePrinters = true;
			ShowGeneralTab = true;
			ShowSettingsTab = true;
			ShowContactTab = true;
			ShowExceptionsTab = true;
		}

		~ExceptionReportView()
		{
			Dispose();
		}

		private void WireUpEvents()
		{
			btnEmail.Click += cmdEmail_Click;
			btnPrint.Click += cmdPrint_Click;
			listviewExceptions.SelectedIndexChanged += ExceptionsSelectedIndexChanged;
			lblApplication.Click += lblApplication_Click;
			btnCopy.Click += cmdCopy_Click;
			lnkEmail.LinkClicked += lnkEmail_LinkClicked;
			btnSave.Click += cmdSave_Click;
			lnkWeb.LinkClicked += LnkWebLinkClicked;
		}

		public bool ShowGeneralTab { get; set; }
		public bool EnumeratePrinters { get; set; }
		public bool ShowSettingsTab { get; set; }
		public bool ShowContactTab { get; set; }
		public bool ShowExceptionsTab { get; set; }
		public bool ShowEnvironmentTab { get; set; }
		public bool ShowAssembliesTab { get; set; }
		public string SMTPServer { get; set; }
		public string SMTPUsername { get; set; }
		public string SMTPPassword { get; set; }
		public string SMTPFromAddress { get; set; }
		public string SendEmailAddress { get; set; }

		public string ProgressMessage
		{
			set { throw new NotImplementedException(); }		//TODO add a progress message label
		}

		public bool EnableEmailButton
		{
			set { btnEmail.Enabled = value; }
		}

		public string EmailToSendTo
		{
			get { throw new NotImplementedException(); }
		}

		public bool ShowProgressBar
		{
			set { throw new NotImplementedException(); }
		}

		public string ContactEmail
		{
			get { return lnkEmail.Text; }
			set { lnkEmail.Text = value; }
		}

		public string ContactWeb
		{
			get { return lnkWeb.Text; }
			set { lnkWeb.Text = value; }
		}

		public string ContactPhone
		{
			get { return txtPhone.Text; }
			set { txtPhone.Text = value; }
		}

		public string ContactFax
		{
			get { return txtFax.Text; }
			set { txtFax.Text = value; }
		}

		public string ContactMessageTop
		{
			get { return lblContactMessageTop.Text; }
			set { lblContactMessageTop.Text = value; }
		}

		public string ContactMessageBottom
		{
			get { return lblContactMessageBottom.Text; }
			set { lblContactMessageBottom.Text = value; }
		}

		public string GeneralMessage
		{
			get { return lblGeneral.Text; }
			set { lblGeneral.Text = value; }
		}

		public ExceptionReporter.slsMailType MailType
		{
			get { return _sendMailType; }
			set { _sendMailType = value; }
		}

		public string ExplanationMessage
		{
			get { return lblExplanation.Text; }
			set { lblExplanation.Text = value; }
		}

		public void SetSendCompleteState()
		{
//			lblProgressMessage.Text = "Email sent.";		//TODO add these ui elements
			progressBar.Visible = false;
//			btnEmail.Enabled = true;
		}

		

		private void lblApplication_Click(object sender, EventArgs e)
		{
			//TODO 
		}

		public void sendMAPIEmail()
		{
			_presenter.SendMapiEmail(SendEmailAddress, Handle);
		}

		public void sendSMTPEmail()
		{
			_presenter.SendSmtpMail();
		}

		public void SetTabs()
		{
			try
			{
				// remove all the tabs to start with
				tcTabs.TabPages.Clear();			//TODO should optimise this out

				// add back the tabs one by one that are configured to be shown
				if (ShowGeneralTab)
				{
					tcTabs.TabPages.Add(tpGeneral);
				}
				if (ShowExceptionsTab)
				{
					tcTabs.TabPages.Add(tpExceptions);
				}
				if (ShowAssembliesTab)
				{
					tcTabs.TabPages.Add(tpAssemblies);
				}
				if (ShowSettingsTab)
				{
					tcTabs.TabPages.Add(tpSettings);
				}
				if (ShowEnvironmentTab)
				{
					tcTabs.TabPages.Add(tpEnvironment);
				}
				if (ShowContactTab)
				{
					tcTabs.TabPages.Add(tpContact);
				}
			}
			catch (Exception ex)
			{
				ShowError("There has been a problem configuring tab page display within the Exception Reporter", ex);
			}
			return;
		}

		public void HandleError(string message, Exception ex)
		{
			var simpleExceptionView = new SimpleExceptionView();
			simpleExceptionView.ShowException(message, ex);
		}

		private string BuildExceptionString()
		{
			return _presenter.BuildExceptionString();
		}

//		//TODO this is being moved to the ExceptionStringBuilder class
//		private string BuildExceptionString(bool showGeneral, bool showExceptionHierarchy, bool showAssemblies, bool showSettings,
//		                                    bool showEnvironment, bool showContact, bool isForPrinting)
//		{

//			_exceptionString = new StringBuilder();
//
//			if (showGeneral)
//			{
//				if (!isForPrinting)
//				{
//					_exceptionString.AppendLine(lblGeneral.Text);
//					_exceptionString.AppendLine();
//					AppendDottedLine(_exceptionString);
//					_exceptionString.AppendLine();
//				}
//
//				_exceptionString.AppendLine("General");
//				_exceptionString.AppendLine();
//				_exceptionString.AppendLine("Application: " + txtApplication.Text);
//				_exceptionString.AppendLine("Version:     " + txtVersion.Text);
//				_exceptionString.AppendLine("Region:      " + txtRegion.Text);
//				_exceptionString.AppendLine("Machine:     " + " " + txtMachine.Text);
//				_exceptionString.AppendLine("User:        " + txtUserName.Text);
//				AppendDottedLine(_exceptionString);
//
//				if (!isForPrinting)
//				{
//					_exceptionString.AppendLine();
//					_exceptionString.AppendLine("Date: " + txtDate.Text);
//					_exceptionString.AppendLine("Time: " + txtTime.Text);
//					AppendDottedLine(_exceptionString);
//				}
//
//				_exceptionString.AppendLine();
//				_exceptionString.AppendLine("Explanation");
//				_exceptionString.AppendLine(txtExplanation.Text.Trim());
//				_exceptionString.AppendLine();
//				AppendDottedLine(_exceptionString);
//				_exceptionString.AppendLine();
//			}
//
//			if (showExceptionHierarchy)
//			{
//				_exceptionString.AppendLine("Exceptions");
//				_exceptionString.AppendLine();
//				_exceptionString.AppendLine(_presenter.ExceptionHierarchyToString(_exception));
//				_exceptionString.AppendLine();
//				AppendDottedLine(_exceptionString);
//				_exceptionString.AppendLine();
//			}
//
//			if (showAssemblies)
//			{
//				_exceptionString.AppendLine("Assemblies");
//				_exceptionString.AppendLine();
//				_exceptionString.AppendLine(_presenter.ReferencedAssembliesToString(_assembly));
//				AppendDottedLine(_exceptionString);
//				_exceptionString.AppendLine();
//			}
//
//			if (showSettings)
//			{
////					TreeToString(tvwSettings, stringBuilder);		//TODO put back in but isolate the functionality out of here
//				AppendDottedLine(_exceptionString);
//				_exceptionString.AppendLine();
//			}
//
//			if (showEnvironment)
//			{
////					TreeToString(tvwEnvironment, stringBuilder);
//				AppendDottedLine(_exceptionString);
//				_exceptionString.AppendLine();
//			}
//
//			if (showContact)
//			{
//				_exceptionString.AppendLine("Contact");
//				_exceptionString.AppendLine();
//				_exceptionString.AppendLine("E-Mail: " + lnkEmail.Text);
//				_exceptionString.AppendLine("Web:    " + lnkWeb.Text);
//				_exceptionString.AppendLine("Phone:  " + txtPhone.Text);
//				_exceptionString.AppendLine("Fax:    " + txtFax.Text);
//				_exceptionString.AppendLine("-----------------------------");
//				_exceptionString.AppendLine();
//			}
//
//			return _exceptionString.ToString();
//		}

		private static void TreeToString(TreeView treeView, TextWriter treeWriter)	//TODO this will need to be reinstated shortly
		{
			TreeNodeToString(treeView.Nodes[0], treeWriter, 0);
			return;
		}

		private static void TreeNodeToString(TreeNode tnNode, TextWriter swWriter, int level)
		{
			string space = string.Empty;

			for (int intCount = 0; intCount < (level*4); intCount++)
				space += " ";

			if (level <= 2)
				swWriter.WriteLine(string.Empty);

			swWriter.WriteLine(space + tnNode.Text);
			foreach (TreeNode tnChild in tnNode.Nodes)
			{
				TreeNodeToString(tnChild, swWriter, level + 1);
			}
		}

		//TODO put on a background thread
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);	

			try
			{
				if (!_refreshData)	// only refresh when we need to
					return;

				_refreshData = false;	// next time we won't refresh unless this flag is set back to true
				Cursor = Cursors.WaitCursor;
				lblProgress.Visible = true;
				progressBar.Visible = true;
				progressBar.Maximum = 14;
				progressBar.Value = 0;

				PopulateGeneralTab();
				PopulateEnvironmentTree();
				PopulateSettingsTab();
				PopulateExceptionHierarchyTree(_presenter.TheException);
				PopulateAssemblyInfo();

				SetTabs();
			}
			finally
			{
				Cursor = Cursors.Default;
				progressBar.Value = progressBar.Maximum;
				lblProgress.Visible = false;
				progressBar.Visible = false;
			}
		}

		private void PopulateAssemblyInfo()
		{
			lstAssemblies.Clear();
			lstAssemblies.Columns.Add("Name", 100, HorizontalAlignment.Left);
			lstAssemblies.Columns.Add("Version", 150, HorizontalAlignment.Left);
			lstAssemblies.Columns.Add("Culture", 150, HorizontalAlignment.Left);

			foreach (AssemblyName assemblyName in _presenter.TheAssembly.GetReferencedAssemblies())
			{
				var listViewItem = new ListViewItem {Text = assemblyName.Name};
				listViewItem.SubItems.Add(assemblyName.Version.ToString());
				listViewItem.SubItems.Add(assemblyName.CultureInfo.EnglishName);
				lstAssemblies.Items.Add(listViewItem);
			}
		}

		private void PopulateSettingsTab()
		{
			var settingsRoot = new TreeNode("Application Settings");
			IEnumerator ienum = ConfigurationManager.AppSettings.GetEnumerator();

			while (ienum.MoveNext())
			{
				settingsRoot.Nodes.Add(
					new TreeNode(ienum.Current + " : " + ConfigurationManager.AppSettings.Get(ienum.Current.ToString())));
			}

			tvwSettings.Nodes.Add(settingsRoot);
			settingsRoot.Expand();
			progressBar.Value++;
			Application.DoEvents();
		}

		private void PopulateEnvironmentTree()
		{
			var root = new TreeNode("Environment");

			try
			{
				AddEnvironmentNode("Operating System", "Win32_OperatingSystem", root, false, "");
			}
			catch { /* do nothing */ }
			finally
			{
				progressBar.Value++;
				Application.DoEvents();
			}
			try
			{
				AddEnvironmentNode("CPU", "Win32_Processor", root, true, "");
			}
			catch { /* do nothing */ }
			finally
			{
				progressBar.Value++;
				Application.DoEvents();
			}
			try
			{
				AddEnvironmentNode("Memory", "Win32_PhysicalMemory", root, true, "");
			}
			catch { /* do nothing */ }
			finally
			{
				progressBar.Value++;
				Application.DoEvents();
			}

			try
			{
				AddEnvironmentNode("Drives", "Win32_DiskDrive", root, true, "");
			}
			catch { /* do nothing */ }
			finally
			{
				progressBar.Value++;
				Application.DoEvents();
			}

			try
			{
				AddEnvironmentNode("Environment Variables", "Win32_Environment", root, true, "");
			}
			catch { /* do nothing */ }
			finally
			{
				progressBar.Value++;
				Application.DoEvents();
			}

			try
			{
				if (EnumeratePrinters)
				{
					AddEnvironmentNode("Printers", "Win32_Printer", root, true, "");
				}
			}
			catch { /* do nothing */ }
			finally
			{
				progressBar.Value ++;
				Application.DoEvents();
			}

			try
			{
				AddEnvironmentNode("System", "Win32_ComputerSystem", root, true, "");
			}
			catch { /* do nothing */ }
			finally
			{
				progressBar.Value++;
				Application.DoEvents();
			}

			tvwEnvironment.Nodes.Add(root);
			root.Expand();

			return;
		}

		private void PopulateGeneralTab()
		{
			txtDate.Text = DateTime.Now.ToShortDateString();
			txtTime.Text = DateTime.Now.ToLongTimeString();
			txtUserName.Text = Environment.UserName;
			txtMachine.Text = Environment.MachineName;

			txtRegion.Text = Application.CurrentCulture.DisplayName;
			txtApplication.Text = Application.ProductName;
			txtVersion.Text = Application.ProductVersion;
		}

		public void ShowExceptionReporter()
		{
			ShowDialog();
		}

		public void DisplayException(Exception ex, Assembly callingAssembly)
		{
			_refreshData = true;
			_presenter.DisplayException(ex, callingAssembly);
		}

		private void PopulateExceptionHierarchyTree(Exception e)
		{
			listviewExceptions.Clear();
			listviewExceptions.Columns.Add("Level", 100, HorizontalAlignment.Left);
			listviewExceptions.Columns.Add("Exception Type", 150, HorizontalAlignment.Left);
			listviewExceptions.Columns.Add("Target Site / Method", 150, HorizontalAlignment.Left);

			var listViewItem = new ListViewItem {Text = "Top Level"};
			listViewItem.SubItems.Add(e.GetType().ToString());
			listViewItem.SubItems.Add(e.TargetSite.ToString());
			listViewItem.Tag = "0";
			listviewExceptions.Items.Add(listViewItem);
			listViewItem.Selected = true;

			int index = 0;
			Exception exCurrent = e;
			bool blnContinue = (exCurrent.InnerException != null);

			while (blnContinue)
			{
				index++;
				exCurrent = exCurrent.InnerException;
				listViewItem = new ListViewItem {Text = ("Inner Exception " + index)};
				listViewItem.SubItems.Add(exCurrent.GetType().ToString());
				listViewItem.SubItems.Add(exCurrent.TargetSite.ToString());
				listViewItem.Tag = index.ToString();
				listviewExceptions.Items.Add(listViewItem);

				blnContinue = (exCurrent.InnerException != null);
			}

			txtStackTrace.Text = e.StackTrace;
			txtMessage.Text = e.Message;
		}

		private static void AddEnvironmentNode(string caption, string className, TreeNode parentNode, bool useName, string where)
		{
			string strDisplayField = useName ? "Name" : "Caption";
			var treeNode1 = new TreeNode(caption);
			var objectSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM {0} {1}", className, where));

			foreach (ManagementObject managementObject in objectSearcher.Get())
			{
				var treeNode2 = new TreeNode(managementObject.GetPropertyValue(strDisplayField).ToString().Trim());
				treeNode1.Nodes.Add(treeNode2);
				foreach (PropertyData propertyData in managementObject.Properties)
				{
					var propertyNode = new TreeNode(propertyData.Name + ':' + Convert.ToString(propertyData.Value));
					treeNode2.Nodes.Add(propertyNode);
				}
			}
			parentNode.Nodes.Add(treeNode1);
		}

		private void cmdPrint_Click(object sender, EventArgs e)
		{
			_presenter.PrintException();
		}

		private void lnkEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				string strLink = lnkEmail.Text;

				if (!strLink.Substring(0, 7).ToUpper().Equals("MAILTO:"))
				{
					strLink = "MailTo:" + strLink;
				}

				Process.Start(strLink);
			}
			catch (Exception ex)
			{
				ShowError("There has been a problem handling the e-mail link", ex);
			}
		}

		private void cmdCopy_Click(object sender, EventArgs e)
		{
			try
			{
				Clipboard.SetDataObject(BuildExceptionString(), true);
			}
			catch (Exception ex)
			{
				ShowError("There has been a problem copying to clipboard", ex);
			}
		}

		private void cmdEmail_Click(object sender, EventArgs e)
		{
			if (SendEmailAddress == null)
				SendEmailAddress = lnkEmail.Text;

			if (_sendMailType == ExceptionReporter.slsMailType.SimpleMAPI)
				sendMAPIEmail();
			
			if (_sendMailType != ExceptionReporter.slsMailType.SMTP) return;

			if (SendEmailAddress != null)
				sendSMTPEmail();
			else
			{
				MessageBox.Show(
					"It is not possible to send e-mail as a recipient address has not been configured by the application.",
					"To Address Missing");
			}
		}

		private void cmdSave_Click(object sender, EventArgs e)
		{
			var saveDialog = new SaveFileDialog
			              	{
			              		Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*",
			              		FilterIndex = 1,
			              		RestoreDirectory = true
			              	};

			if (saveDialog.ShowDialog() == DialogResult.OK)
			{
				_presenter.SaveToFile(saveDialog.FileName);
			}
		}

		private void ExceptionsSelectedIndexChanged(object sender, EventArgs e)
		{
			Exception displayException = _presenter.TheException;
			foreach (ListViewItem listViewItem in listviewExceptions.Items)
			{
				if (!listViewItem.Selected) continue;
				for (int count = 0; count < int.Parse(listViewItem.Tag.ToString()); count++)
				{
					displayException = displayException.InnerException;
				}
			}

			txtStackTrace.Text = string.Empty;
			txtMessage.Text = string.Empty;

			if (displayException == null) displayException = _presenter.TheException;
			if ((displayException == null)) return;

			txtStackTrace.Text = displayException.StackTrace;
			txtMessage.Text = displayException.Message;
		}

		private void LnkWebLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				var psi = new ProcessStartInfo(lnkWeb.Text) { UseShellExecute = true };
				Process.Start(psi);
			}
			catch (Exception ex)
			{
				ShowError("There has been a problem handling the web link click", ex);
			}
		}

		private static void ShowError(string strMessage, Exception ex)
		{
			var simpleExceptionView = new SimpleExceptionView();
			simpleExceptionView.ShowException(strMessage, ex);
		}
	}
}
