using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
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
			btnEmail.Click += Email_Click;
			btnPrint.Click += cmdPrint_Click;
			listviewExceptions.SelectedIndexChanged += ExceptionsSelectedIndexChanged;
			lblApplication.Click += lblApplication_Click;
			btnCopy.Click += cmdCopy_Click;
			lnkEmail.LinkClicked += lnkEmail_LinkClicked;
			btnSave.Click += Save_Click;
			lnkWeb.LinkClicked += UrlClicked;
		}

		public bool ShowGeneralTab { get; set; }
		public bool EnumeratePrinters { get; set; }
		public bool ShowSettingsTab { get; set; }
		public bool ShowContactTab { get; set; }
		public bool ShowExceptionsTab { get; set; }
		public bool ShowEnvironmentTab { get; set; }
		public bool ShowAssembliesTab { get; set; }

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

		public int ProgressValue
		{
			get { return progressBar.Value; }
			set { progressBar.Value = value; }
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
			_presenter.SendMapiEmail(Handle);
		}

		public void sendSMTPEmail()
		{
			_presenter.SendSmtpMail();
		}

		public void SetTabs()
		{
			tabControl.TabPages.Clear(); 

			// add back the tabs one by one that are configured to be shown
			if (ShowGeneralTab)
			{
				tabControl.TabPages.Add(tabGeneral);
			}
			if (ShowExceptionsTab)
			{
				tabControl.TabPages.Add(tabExceptions);
			}
			if (ShowAssembliesTab)
			{
				tabControl.TabPages.Add(tabAssemblies);
			}
			if (ShowSettingsTab)
			{
				tabControl.TabPages.Add(tabSettings);
			}
			if (ShowEnvironmentTab)
			{
				tabControl.TabPages.Add(tabEnvironment);
			}
			if (ShowContactTab)
			{
				tabControl.TabPages.Add(tabContact);
			}
		}

		public void HandleError(string message, Exception ex)
		{
			var simpleExceptionView = new InternalExceptionView();
			simpleExceptionView.ShowException(message, ex);
		}

		private string BuildExceptionString()
		{
			return _presenter.BuildExceptionString();
		}

		//TODO put on a background thread
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			PopulateAll();
		}

		private void PopulateAll()
		{
			try
			{
				if (!_refreshData)	// only refresh when we need to
					return;

				_refreshData = false;	// next time we won't refresh unless this flag is set back to true
				Cursor = Cursors.WaitCursor;
				lblProgress.Visible = true;
				progressBar.Visible = true;
				progressBar.Value = 0;
				progressBar.Maximum = 13;

				PopulateGeneralTab(); progressBar.Value++;
				PopulateEnvironmentTree(); progressBar.Value++;
				PopulateSettingsTab(); progressBar.Value++;
				PopulateExceptionHierarchyTree(_presenter.TheException); progressBar.Value++;
				PopulateAssemblyInfo(); progressBar.Value++;

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
			IEnumerator configEnum = ConfigurationManager.AppSettings.GetEnumerator();

			while (configEnum.MoveNext())
			{
				settingsRoot.Nodes.Add(
					new TreeNode(configEnum.Current + " : " + ConfigurationManager.AppSettings.Get(configEnum.Current.ToString())));
			}

			treeSettings.Nodes.Add(settingsRoot);
			settingsRoot.Expand();
		}

		private void PopulateEnvironmentTree()
		{
			var root = new TreeNode("Environment");

			_presenter.AddEnvironmentNode("Operating System", "Win32_OperatingSystem", root, false, string.Empty);
			_presenter.AddEnvironmentNode("CPU", "Win32_Processor", root, true, string.Empty);
			_presenter.AddEnvironmentNode("Memory", "Win32_PhysicalMemory", root, true, string.Empty);
			_presenter.AddEnvironmentNode("Drives", "Win32_DiskDrive", root, true, string.Empty);
			_presenter.AddEnvironmentNode("Environment Variables", "Win32_Environment", root, true, string.Empty);
			_presenter.AddEnvironmentNode("System", "Win32_ComputerSystem", root, true, string.Empty);

			if (EnumeratePrinters)
				_presenter.AddEnvironmentNode("Printers", "Win32_Printer", root, true, string.Empty);

			treeEnvironment.Nodes.Add(root);
			root.Expand();
		}

		private void PopulateGeneralTab()
		{
			txtDate.Text = _presenter.Info.ExceptionDate.ToShortDateString();
			txtTime.Text = _presenter.Info.ExceptionDate.ToShortTimeString();
			txtUserName.Text = _presenter.Info.UserName;
			txtMachine.Text = _presenter.Info.MachineName;
			txtRegion.Text = _presenter.Info.RegionInfo;
			txtApplication.Text = _presenter.Info.AppName;
			txtVersion.Text = _presenter.Info.AppVersion;
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

		private void Email_Click(object sender, EventArgs e)
		{
			if (_sendMailType == ExceptionReporter.slsMailType.SimpleMAPI)
				sendMAPIEmail();
			
			if (_sendMailType == ExceptionReporter.slsMailType.SMTP)
				sendSMTPEmail();
		}

		private void Save_Click(object sender, EventArgs e)
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

		private void UrlClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
			var simpleExceptionView = new InternalExceptionView();
			simpleExceptionView.ShowException(strMessage, ex);
		}
	}
}
