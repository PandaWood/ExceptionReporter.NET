using System;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.Core;

namespace ExceptionReporting.Views
{
	internal partial class ExceptionReportView : Form, IExceptionReportView
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
			urlEmail.Text = reportInfo.ContactEmail;
			txtFax.Text = reportInfo.Fax;
			lblContactMessageBottom.Text = reportInfo.ContactMessageBottom;
			lblContactMessageTop.Text = reportInfo.ContactMessageTop;
			txtPhone.Text = reportInfo.Phone;
			urlWeb.Text = reportInfo.WebUrl;
			lblExplanation.Text = reportInfo.UserExplanationLabel;
			txtExceptionMessage.Text = reportInfo.Exception.Message;

			txtDate.Text = reportInfo.ExceptionDate.ToShortDateString();
			txtTime.Text = reportInfo.ExceptionDate.ToShortTimeString();
			txtUserName.Text = reportInfo.UserName;
			txtMachine.Text = reportInfo.MachineName;
			txtRegion.Text = reportInfo.RegionInfo;
			txtApplicationName.Text = reportInfo.AppName;
			txtVersion.Text = reportInfo.AppVersion;

			btnCopy.FlatStyle = 
				btnEmail.FlatStyle = 
				btnSave.FlatStyle = (reportInfo.ShowFlatButtons ? FlatStyle.Flat : FlatStyle.Standard);
		}

		~ExceptionReportView()
		{
			Dispose();
		}

		private void WireUpEvents()
		{
			btnEmail.Click += Email_Click;
			listviewExceptions.SelectedIndexChanged += ExceptionsSelectedIndexChanged;
			btnCopy.Click += Copy_Click;
			urlEmail.LinkClicked += EmailLink_Clicked;
			btnSave.Click += Save_Click;
			urlWeb.LinkClicked += UrlLink_Clicked;
		}

		public string ProgressMessage
		{
			set
			{
				lblProgressMessage.Visible = true;	// force visibility
				lblProgressMessage.Text = value;
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

		public void SetEmailCompletedState(bool success)
		{
			ProgressMessage = success ? "Email sent" : "Failed to send Email";
			ShowProgressBar = false;
			btnEmail.Enabled = true;
		}

		public void SetEmailCompletedState_WithMessageIfSuccess(bool success, string successMessage)
		{
			SetEmailCompletedState(success);

			if (success)
				ProgressMessage = successMessage;
		}

		public void PopulateTabs()
		{
			if (!_presenter.ReportInfo.ShowGeneralTab)
			{
				tabControl.TabPages.Remove(tabGeneral);
			}
			if (!_presenter.ReportInfo.ShowExceptionsTab)
			{
				tabControl.TabPages.Remove(tabExceptions);
			}
			if (!_presenter.ReportInfo.ShowAssembliesTab)
			{
				tabControl.TabPages.Remove(tabAssemblies);
			}
			if (!_presenter.ReportInfo.ShowConfigTab)
			{
				tabControl.TabPages.Remove(tabConfig);
			}
			if (!_presenter.ReportInfo.ShowSysInfoTab)
			{
				tabControl.TabPages.Remove(tabSysInfo);
			}
			if (!_presenter.ReportInfo.ShowContactTab)
			{
				tabControl.TabPages.Remove(tabContact);
			}
		}

		//TODO consider putting on a background thread - and avoid the OnActivated event altogether
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			if (_isDataRefreshRequired)
				_presenter.PopulateReport();

			_isDataRefreshRequired = false;
		}

		public void SetProgressCompleteState()
		{
			Cursor = Cursors.Default;
			ShowProgressLabel = ShowProgressBar = false;
		}

		public void ShowExceptionReport()
		{
			_isDataRefreshRequired = true;
			ShowDialog();
		}

		public void SetInProgressState()
		{
			Cursor = Cursors.WaitCursor;
			ShowProgressLabel = true;
			ShowProgressBar = true;		//TODO this is redundant until we place the work on a separate thread
			Application.DoEvents();
		}

		public void PopulateAssembliesTab()
		{
			listviewAssemblies.Clear();
			listviewAssemblies.Columns.Add("Name", 320, HorizontalAlignment.Left);
			listviewAssemblies.Columns.Add("Version", 150, HorizontalAlignment.Left);

			foreach (AssemblyName assemblyName in _presenter.AppAssembly.GetReferencedAssemblies())
			{
				var listViewItem = new ListViewItem {Text = assemblyName.Name};
				listViewItem.SubItems.Add(assemblyName.Version.ToString());
				listviewAssemblies.Items.Add(listViewItem);
			}
		}

		public void PopulateConfigTab(TreeNode rootNode)
		{
			treeviewSettings.Nodes.Add(rootNode);
			rootNode.Expand();
		}

		public void PopulateSysInfoTab(TreeNode rootNode)
		{
			treeEnvironment.Nodes.Add(rootNode);
			rootNode.Expand();
		}

		//TODO Label='EH' - move this logic out (is duplicated almost entirely (without ListView) in ExceptionStringBuilder)
		public void PopulateExceptionTab(Exception rootException)
		{
			listviewExceptions.Clear();
			listviewExceptions.Columns.Add("Level", 100, HorizontalAlignment.Left);
			listviewExceptions.Columns.Add("Exception Type", 150, HorizontalAlignment.Left);
			listviewExceptions.Columns.Add("Target Site / Method", 150, HorizontalAlignment.Left);

			var listViewItem = new ListViewItem {Text = "Top Level"};
			listViewItem.SubItems.Add(rootException.GetType().ToString());
			listViewItem.SubItems.Add(rootException.TargetSite.ToString());
			listViewItem.Tag = "0";
			listviewExceptions.Items.Add(listViewItem);
			listViewItem.Selected = true;

			int index = 0;
			Exception currentException = rootException;
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

			txtExceptionTabStackTrace.Text = rootException.StackTrace;
			txtExceptionTabMessage.Text = rootException.Message;
		}

		private void Copy_Click(object sender, EventArgs e)
		{
			_presenter.CopyReportToClipboard();
		}

		private void Email_Click(object sender, EventArgs e)
		{
			_presenter.SendReportByEmail(Handle);
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
				_presenter.SaveReportToFile(saveDialog.FileName);
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

		private void UrlLink_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_presenter.NavigateToWebsite();
		}

		private void EmailLink_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_presenter.SendContactEmail();
		}

		public void ShowErrorDialog(string message, Exception exception)
		{
			var internalExceptionView = new InternalExceptionView();
			internalExceptionView.ShowException(message, exception);
		}
	}
}