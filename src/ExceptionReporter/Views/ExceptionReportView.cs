using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace ExceptionReporting.Views
{
	public partial class ExceptionReportView : Form, IExceptionReportView
	{
		private bool _isDataRefreshRequired;
		private readonly ExceptionReportPresenter _presenter;

		public ExceptionReportView(ExceptionReportInfo reportInfo)
		{
			InitializeComponent();

			_presenter = new ExceptionReportPresenter(this, reportInfo);

			WireUpEvents();
			PopulateTabs();
			PopulateReportInfo(reportInfo);
		}

		private void PopulateReportInfo(ExceptionReportInfo reportInfo)
		{
			lnkEmail.Text = reportInfo.ContactEmail;
			txtFax.Text = reportInfo.Fax;
			lblContactMessageBottom.Text = reportInfo.ContactMessageBottom;
			lblContactMessageTop.Text = reportInfo.ContactMessageTop;
			txtPhone.Text = reportInfo.Phone;
			urlWeb.Text = reportInfo.WebUrl;
			lblExplanation.Text = reportInfo.UserExplanationLabel;
			lblGeneral.Text = reportInfo.ExceptionOccuredMessage;
			txtExceptionMessage.Text = reportInfo.Exception.Message;

			txtDate.Text = reportInfo.ExceptionDate.ToShortDateString();
			txtTime.Text = reportInfo.ExceptionDate.ToShortTimeString();
			txtUserName.Text = reportInfo.UserName;
			txtMachine.Text = reportInfo.MachineName;
			txtRegion.Text = reportInfo.RegionInfo;
			txtApplicationName.Text = reportInfo.AppName;
			txtVersion.Text = reportInfo.AppVersion;
		}

		~ExceptionReportView()
		{
			Dispose();
		}

		private void WireUpEvents()
		{
			btnEmail.Click += EmailButton_Click;
			btnPrint.Click += PrintButton_Click;
			listviewExceptions.SelectedIndexChanged += ExceptionsSelectedIndexChanged;
			btnCopy.Click += CopyButton_Click;
			lnkEmail.LinkClicked += EmailLink_Click;
			btnSave.Click += SaveButton_Click;
			urlWeb.LinkClicked += UrlClicked;
		}

		public string ProgressMessage
		{
			set
			{
				lblProgressMessage.Visible = true;	// force visibility
				lblProgressMessage.Text = value;
				lblProgressMessage.Refresh();
			}
		}

		public bool EnableEmailButton
		{
			set { btnEmail.Enabled = value; }
		}

		public bool ShowProgressBar
		{
			set { progressBar.Visible = value; }
		}

		public bool ShowProgressLabel
		{
			set { lblProgressMessage.Visible = value; }
		}

		public int ProgressValue
		{
			get { return progressBar.Value; }
			set { progressBar.Value = value; }
		}

		public string UserExplanation
		{
			get { return txtUserExplanation.Text; }
		}

		public void SetSendMailCompletedState()
		{
			ProgressMessage = "Email sent.";
			ShowProgressBar = false;
			btnEmail.Enabled = true;
		}

		public void PopulateTabs()
		{
			tabControl.TabPages.Clear(); 

			if (_presenter.ReportInfo.ShowGeneralTab)
			{
				tabControl.TabPages.Add(tabGeneral);
			}
			if (_presenter.ReportInfo.ShowExceptionsTab)
			{
				tabControl.TabPages.Add(tabExceptions);
			}
			if (_presenter.ReportInfo.ShowAssembliesTab)
			{
				tabControl.TabPages.Add(tabAssemblies);
			}
			if (_presenter.ReportInfo.ShowSettingsTab)
			{
				tabControl.TabPages.Add(tabSettings);
			}
			if (_presenter.ReportInfo.ShowEnvironmentTab)
			{
				tabControl.TabPages.Add(tabEnvironment);
			}
			if (_presenter.ReportInfo.ShowContactTab)
			{
				tabControl.TabPages.Add(tabContact);
			}
		}

		//TODO put on a background thread - and avoid the OnActivated event altogether
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			PopulateAll();
		}

		private void PopulateAll()
		{
			if (!_isDataRefreshRequired)
				return;

			_isDataRefreshRequired = false;

			try
			{	
				SetInProgressState();

				//TODO the progress bar's progress precision needs some work
				PopulateConfigSettingsTab(); progressBar.Value++;
				PopulateExceptionHierarchyTree(_presenter.TheException); progressBar.Value++;
				PopulateAssemblyInfoTab(); progressBar.Value++;
				PopulateEnvironmentTree(); progressBar.Value++;

				PopulateTabs();
			}
			finally
			{
				SetProgressCompleteState();
			}
		}

		private void SetProgressCompleteState()
		{
			Cursor = Cursors.Default;
			ShowProgressLabel = ShowProgressBar = false;
		}

		private void SetInProgressState()
		{
			Cursor = Cursors.WaitCursor;
			ShowProgressLabel = true;
			progressBar.Maximum = 12;
			progressBar.Value = 2;
			ShowProgressBar = true;
			progressBar.Refresh();
		}

		private void PopulateAssemblyInfoTab()
		{
			lstAssemblies.Clear();
			lstAssemblies.Columns.Add("Name", 320, HorizontalAlignment.Left);
			lstAssemblies.Columns.Add("Version", 150, HorizontalAlignment.Left);

			//TODO extract out the reference to AssemblyName to the presenter (eg return a DTO)
			foreach (AssemblyName assemblyName in _presenter.AppAssembly.GetReferencedAssemblies())
			{
				var listViewItem = new ListViewItem {Text = assemblyName.Name};
				listViewItem.SubItems.Add(assemblyName.Version.ToString());
				lstAssemblies.Items.Add(listViewItem);
			}
		}

		private void PopulateConfigSettingsTab()
		{
			TreeNode rootNode = _presenter.CreateConfigSettingsTree();
			treeviewSettings.Nodes.Add(rootNode);
			rootNode.Expand();
		}

		private void PopulateEnvironmentTree()
		{
			//TODO the calls to AddEnvironmentNode should all originate from the presenter as the result of a single call - passing the root node
			var root = new TreeNode("Environment");

			_presenter.AddEnvironmentNode("Operating System", "Win32_OperatingSystem", root, false, string.Empty); progressBar.Value++;
			_presenter.AddEnvironmentNode("CPU", "Win32_Processor", root, true, string.Empty); progressBar.Value++;
			_presenter.AddEnvironmentNode("Memory", "Win32_PhysicalMemory", root, true, string.Empty); progressBar.Value++;
			_presenter.AddEnvironmentNode("Drives", "Win32_DiskDrive", root, true, string.Empty); progressBar.Value++;
			_presenter.AddEnvironmentNode("Environment Variables", "Win32_Environment", root, true, string.Empty); progressBar.Value++;
			_presenter.AddEnvironmentNode("System", "Win32_ComputerSystem", root, true, string.Empty); progressBar.Value++;

			if (_presenter.ReportInfo.EnumeratePrinters)
				_presenter.AddEnvironmentNode("Printers", "Win32_Printer", root, true, string.Empty);

			treeEnvironment.Nodes.Add(root);
			root.Expand();
		}

		public void ShowExceptionReport()
		{
			_isDataRefreshRequired = true;
			ShowDialog();
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
			Exception currentException = e;
			bool shouldContinue = (currentException.InnerException != null);

			while (shouldContinue)
			{
				index++;
				currentException = currentException.InnerException;
				listViewItem = new ListViewItem {Text = ("Inner Exception " + index)};
				listViewItem.SubItems.Add(currentException.GetType().ToString());

				if (currentException.TargetSite != null)
					listViewItem.SubItems.Add(currentException.TargetSite.ToString());

				listViewItem.Tag = index.ToString();
				listviewExceptions.Items.Add(listViewItem);

				shouldContinue = (currentException.InnerException != null);
			}

			txtExceptionTabStackTrace.Text = e.StackTrace;
			txtExceptionTabMessage.Text = e.Message;
		}

		private void PrintButton_Click(object sender, EventArgs e)
		{
			_presenter.PrintException();
		}

		private void CopyButton_Click(object sender, EventArgs e)
		{
			_presenter.CopyExceptionReportToClipboard();
		}

		private void EmailButton_Click(object sender, EventArgs e)
		{
			_presenter.SendExceptionReportByEmail(Handle);
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			var saveDialog = new SaveFileDialog
			              	{
			              		Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*",
			              		FilterIndex = 1,
			              		RestoreDirectory = true
			              	};

			if (saveDialog.ShowDialog() == DialogResult.OK)
			{
				_presenter.SaveExceptionReportToFile(saveDialog.FileName);
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

			txtExceptionTabStackTrace.Text = string.Empty;
			txtExceptionTabMessage.Text = string.Empty;

			if (displayException == null) displayException = _presenter.TheException;
			if (displayException == null) return;

			txtExceptionTabStackTrace.Text = displayException.StackTrace;
			txtExceptionTabMessage.Text = displayException.Message;
		}

		private void UrlClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{	//TODO move this out to presenter (ie single call to presenter here)
				var psi = new ProcessStartInfo(urlWeb.Text) { UseShellExecute = true };
				Process.Start(psi);
			}
			catch (Exception ex)
			{
				ShowError("There has been a problem handling the web link click", ex);
			}
		}

		private void EmailLink_Click(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{	//TODO move this out to presenter (ie single call to presenter here)
				string emailAddress = lnkEmail.Text.Trim();

				const string MAILTO = "MAILTO:";
				if (!emailAddress.Substring(0, MAILTO.Length).ToUpper().Equals(MAILTO))
					emailAddress = MAILTO + emailAddress;

				var psi = new ProcessStartInfo(emailAddress) { UseShellExecute = true };
				Process.Start(psi);
			}
			catch (Exception ex)
			{
				ShowError("There has been a problem handling the e-mail link", ex);
			}
		}

		public void ShowError(string message, Exception exception)
		{
			var simpleExceptionView = new InternalExceptionView();
			simpleExceptionView.ShowException(message, exception);
		}
	}
}
