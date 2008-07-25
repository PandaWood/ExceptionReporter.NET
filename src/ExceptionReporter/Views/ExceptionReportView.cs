using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ExceptionReporting.Views
{
	public partial class ExceptionReportView : Form, IExceptionReportView
	{
		private Exception _exception;
		private StringBuilder _exceptionString;
		private ExceptionReporter.slsMailType _sendMailType = ExceptionReporter.slsMailType.SimpleMAPI;
		private Assembly _assembly;
		private bool _refreshData;
		private string _email;

		private bool _showGeneralTab = true;
		private bool _showEnvironmentTab = true;
		private bool _showSettingsTab = true;
		private bool _showContactTab = true;
		private bool _showExceptionsTab = true;
		private bool _showAssembliesTab = true;
		private bool _showEnumeratePrinters = true;

		private readonly ExceptionReportPresenter _presenter;

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
			_presenter.SendMapiEmail(_email, _exceptionString, Handle);
		}

		public void sendSMTPEmail()
		{
			_presenter.SendSmtpMail(_exceptionString.ToString());
		}

		private void setTabs()
		{
			try
			{
				// remove all the tabs to start with
				tcTabs.TabPages.Clear();

				// add back the tabs one by one that have the appropriate
				// property set
				if (_showGeneralTab)
				{
					tcTabs.TabPages.Add(tpGeneral);
				}
				if (_showExceptionsTab)
				{
					tcTabs.TabPages.Add(tpExceptions);
				}
				if (_showAssembliesTab)
				{
					tcTabs.TabPages.Add(tpAssemblies);
				}
				if (_showSettingsTab)
				{
					tcTabs.TabPages.Add(tpSettings);
				}
				if (_showEnvironmentTab)
				{
					tcTabs.TabPages.Add(tpEnvironment);
				}
				if (_showContactTab)
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
				ShowError("There has been a problem configuring command button display within the Exception Reporter", ex);
			}
			return;
		}

		// public property used to set/get visibility of Tab
		public bool ShowGeneralTab
		{
			get { return _showGeneralTab; }
			set
			{
				_showGeneralTab = value;
				setTabs();
			}
		}

		public bool EnumeratePrinters
		{
			get { return _showEnumeratePrinters; }
			set { _showEnumeratePrinters = value; }
		}

		public bool ShowEnvironmentTab
		{
			get { return _showEnvironmentTab; }
			set
			{
				_showEnvironmentTab = value;
				setTabs();
			}
		}

		public bool ShowAssembliesTab
		{
			get { return _showAssembliesTab; }
			set
			{
				_showAssembliesTab = value;
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

		public string SMTPServer { get; set; }
		public string SMTPUsername { get; set; }
		public string SMTPPassword { get; set; }
		public string SMTPFromAddress { get; set; }

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

		public string SendEmailAddress
		{
			get { return _email; }
			set { _email = value; }
		}

		public bool ShowSettingsTab
		{
			get { return _showSettingsTab; }
			set
			{
				_showSettingsTab = value;
				setTabs();
			}
		}

		public bool ShowContactTab
		{
			get { return _showContactTab; }
			set
			{
				_showContactTab = value;
				setTabs();
			}
		}

		public bool ShowExceptionsTab
		{
			get { return _showExceptionsTab; }
			set
			{
				_showExceptionsTab = value;
				setTabs();
			}
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

		private void BuildExceptionString()
		{
			BuildExceptionString(true, true, true, true, true, true, false);
			return;
		}

		//TODO this is being moved to the ExceptionStringBuilder class
		private void BuildExceptionString(bool blnGeneral, bool blnExceptions, bool blnAssemblies, bool blnSettings,
		                                  bool blnEnvironment, bool blnContact, bool blnForPrint)
		{
			try
			{
				_exceptionString = new StringBuilder();

				if (blnGeneral)
				{
					if (!blnForPrint)
					{
						_exceptionString.AppendLine(lblGeneral.Text);
						_exceptionString.AppendLine();
						AppendDottedLine(_exceptionString);
						_exceptionString.AppendLine();
					}
					_exceptionString.AppendLine("General");
					_exceptionString.AppendLine();
					_exceptionString.AppendLine("Application: " + txtApplication.Text);
					_exceptionString.AppendLine("Version:     " + txtVersion.Text);
					_exceptionString.AppendLine("Region:      " + txtRegion.Text);
					_exceptionString.AppendLine("Machine:     " + " " + txtMachine.Text);
					_exceptionString.AppendLine("User:        " + txtUserName.Text);
					AppendDottedLine(_exceptionString);
					if (!blnForPrint)
					{
						_exceptionString.AppendLine();
						_exceptionString.AppendLine("Date: " + txtDate.Text);
						_exceptionString.AppendLine("Time: " + txtTime.Text);
						AppendDottedLine(_exceptionString);
					}
					_exceptionString.AppendLine();
					_exceptionString.AppendLine("Explanation");
					_exceptionString.AppendLine(txtExplanation.Text.Trim());
					_exceptionString.AppendLine();
					AppendDottedLine(_exceptionString);
					_exceptionString.AppendLine();
				}

				if (blnExceptions)
				{
					_exceptionString.AppendLine("Exceptions");
					_exceptionString.AppendLine();
					_exceptionString.AppendLine(_presenter.ExceptionHierarchyToString(_exception));
					_exceptionString.AppendLine();
					AppendDottedLine(_exceptionString);
					_exceptionString.AppendLine();
				}

				if (blnAssemblies)
				{
					_exceptionString.AppendLine("Assemblies");
					_exceptionString.AppendLine();
					_exceptionString.AppendLine(_presenter.ReferencedAssembliesToString(_assembly));
					AppendDottedLine(_exceptionString);
					_exceptionString.AppendLine();
				}

				if (blnSettings)
				{
//					TreeToString(tvwSettings, stringBuilder);		//TODO put back in but isolate the functionality out of here
					AppendDottedLine(_exceptionString);
					_exceptionString.AppendLine();
				}

				if (blnEnvironment)
				{
//					TreeToString(tvwEnvironment, stringBuilder);
					AppendDottedLine(_exceptionString);
					_exceptionString.AppendLine();
				}

				if (blnContact)
				{
					_exceptionString.AppendLine("Contact");
					_exceptionString.AppendLine();
					_exceptionString.AppendLine("E-Mail: " + lnkEmail.Text);
					_exceptionString.AppendLine("Web:    " + lnkWeb.Text);
					_exceptionString.AppendLine("Phone:  " + txtPhone.Text);
					_exceptionString.AppendLine("Fax:    " + txtFax.Text);
					_exceptionString.AppendLine("-----------------------------");
					_exceptionString.AppendLine();
				}
			}
			catch (Exception ex)
			{
				ShowError("There has been a problem building exception details into a string for printing, copying, saving or e-mailing", ex);
			}
		}

		private static void AppendDottedLine(StringBuilder stringBuilder)
		{
			stringBuilder.AppendLine("-----------------------------");
		}

		private static void TreeToString(TreeView treeView, TextWriter treeWriter)	//TODO this will need to be reinstated shortly
		{
			TreeNodeToString(treeView.Nodes[0], treeWriter, 0);
			return;
		}

		private static void TreeNodeToString(TreeNode tnNode, TextWriter swWriter, int level)
		{
			string space = "";

			for (int intCount = 0; intCount < (level*4); intCount++)
			{
				space = space + " ";
			}

			if (level <= 2)
			{
				swWriter.WriteLine(string.Empty);
			}

			swWriter.WriteLine(space + tnNode.Text);
			foreach (TreeNode tnChild in tnNode.Nodes)
			{
				TreeNodeToString(tnChild, swWriter, level + 1);
			}
		}

		//TODO 'extract method' on this
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			try
			{
				// only refresh when we need to
				if (!_refreshData)
				{
					return;
				}
				// next time we won't refresh unless this flag is set back to true
				_refreshData = false;

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
					AddEnvironmentNode2("Operating System", "Win32_OperatingSystem", root, false, "");
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
					AddEnvironmentNode2("CPU", "Win32_Processor", root, true, "");
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
					AddEnvironmentNode2("Memory", "Win32_PhysicalMemory", root, true, "");
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
					AddEnvironmentNode2("Drives", "Win32_DiskDrive", root, true, "");
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
					AddEnvironmentNode2("Environment Variables", "Win32_Environment", root, true, "");
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
						AddEnvironmentNode2("Printers", "Win32_Printer", root, true, "");
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
					AddEnvironmentNode2("System", "Win32_ComputerSystem", root, true, "");
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

				IEnumerator ienum = ConfigurationManager.AppSettings.GetEnumerator();

				while (ienum.MoveNext())
				{
					settingsRoot.Nodes.Add(
						new TreeNode(ienum.Current + " : " + ConfigurationManager.AppSettings.Get(ienum.Current.ToString())));
				}

				tvwSettings.Nodes.Add(settingsRoot);
				settingsRoot.Expand();
				progressBar.Value = progressBar.Value + 1;
				Application.DoEvents();

				buildExceptionHeirarchy(_exception);
				progressBar.Value = progressBar.Value + 1;
				Application.DoEvents();

				lstAssemblies.Clear();
				lstAssemblies.Columns.Add("Name", 100, HorizontalAlignment.Left);
				lstAssemblies.Columns.Add("Version", 150, HorizontalAlignment.Left);
				lstAssemblies.Columns.Add("Culture", 150, HorizontalAlignment.Left);

				foreach (AssemblyName assemblyName in _assembly.GetReferencedAssemblies())
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
				ShowError("There has been a problem setting up the Exception Reporter display", ex);
			}
		}

		public void DisplayException(Exception ex, Assembly callingAssembly)
		{
			_assembly = callingAssembly;
			_exception = ex;
			_refreshData = true;

			ShowDialog();
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
				ShowError("There has been a problem building the Exception Heirarchy list", ex);
			}
		}


		private static void AddEnvironmentNode2(string strCaption, string strClass, TreeNode parentNode, Boolean blnUseName, string strWhere)
		{
			try
			{
				string strDisplayField = blnUseName ? "Name" : "Caption";
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
				ShowError("There has been a problem adding an Environment node.", ex);
				return;
			}
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
				BuildExceptionString();
				Clipboard.SetDataObject(_exceptionString.ToString(), true);
			}
			catch (Exception ex)
			{
				ShowError("There has been a problem copying to clipboard", ex);
			}
		}

		private void cmdEmail_Click(object sender, EventArgs e)
		{
			BuildExceptionString();
			try
			{
				if (_email == null)
				{
					_email = lnkEmail.Text;
				}

				if (_sendMailType == ExceptionReporter.slsMailType.SimpleMAPI)
				{
					sendMAPIEmail();
				}
				if (_sendMailType == ExceptionReporter.slsMailType.SMTP)
				{
					if (_email != null)
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
				ShowError("There has been a problem sending e-mail", ex);
			}
		}

		~ExceptionReportView()
		{
			Dispose();
		}

		private void cmdSave_Click(object sender, EventArgs e)
		{
			BuildExceptionString();

			var saveDialog = new SaveFileDialog
			              	{
			              		Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*",
			              		FilterIndex = 1,
			              		RestoreDirectory = true
			              	};

			if (saveDialog.ShowDialog() == DialogResult.OK)
			{
				_presenter.Save(_exceptionString.ToString(), saveDialog.FileName);
			}
		}


		private void LstExceptionsSelectedIndexChanged(object sender, EventArgs e)
		{
			Exception displayException = _exception;
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
				if (displayException == null) displayException = _exception;
				if (!(displayException == null))
				{
					txtStackTrace.Text = displayException.StackTrace;
					txtMessage.Text = displayException.Message;
				}
			}
			catch (Exception ex)
			{
				ShowError("There has been a problem handling the change of selected exception", ex);
			}
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
