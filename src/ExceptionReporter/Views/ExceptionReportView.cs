using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ExceptionReporting.Core;

namespace ExceptionReporting.Views
{
	internal partial class ExceptionReportView : Form, IExceptionReportView
	{
		private bool _isDataRefreshRequired;
		private readonly ExceptionReportPresenter _presenter;
	    private bool showFullDetail = true;

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
			lblContactMessageTop.Text = reportInfo.ContactMessageTop;
			txtPhone.Text = reportInfo.Phone;
			urlWeb.Text = reportInfo.WebUrl;
			lblExplanation.Text = reportInfo.UserExplanationLabel;
		    showFullDetail = reportInfo.ShowFullDetail;
		    ToggleShowFullDetail();
			btnDetailToggle.Visible = reportInfo.ShowLessMoreDetailButton;
		    txtExceptionMessageLarge.Text = txtExceptionMessage.Text = 
					!string.IsNullOrEmpty(reportInfo.CustomMessage) ? reportInfo.CustomMessage : reportInfo.Exception.Message;

		    txtDate.Text = reportInfo.ExceptionDate.ToShortDateString();
			txtTime.Text = reportInfo.ExceptionDate.ToShortTimeString();
			txtUserName.Text = reportInfo.UserName;
			txtMachine.Text = reportInfo.MachineName;
			txtRegion.Text = reportInfo.RegionInfo;
			txtApplicationName.Text = reportInfo.AppName;
			txtVersion.Text = reportInfo.AppVersion;

			btnClose.FlatStyle = 
				btnDetailToggle.FlatStyle = 
				btnCopy.FlatStyle = 
				btnEmail.FlatStyle = 
				btnSave.FlatStyle = (reportInfo.ShowFlatButtons ? FlatStyle.Flat : FlatStyle.Standard);

			listviewExceptions.BackColor = 
				listviewAssemblies.BackColor =
				txtExceptionTabStackTrace.BackColor = 
				txtFax.BackColor = 
				txtMachine.BackColor =
				txtPhone.BackColor =
				txtRegion.BackColor =
				txtTime.BackColor =
				txtTime.BackColor =
				txtUserName.BackColor =
				txtVersion.BackColor =
				txtApplicationName.BackColor =
				txtDate.BackColor =
				txtExceptionMessageLarge.BackColor =
				txtExceptionMessage.BackColor =
				txtExceptionTabMessage.BackColor = reportInfo.BackgroundColor;

			if (!reportInfo.ShowButtonIcons)
			{	
				RemoveButtonIcons();
			}

			Text = reportInfo.TitleText;
			txtUserExplanation.Font = new Font(txtUserExplanation.Font.FontFamily, reportInfo.UserExplanationFontSize);

			if (reportInfo.TakeScreenshot)
				reportInfo.ScreenshotImage = ScreenshotHelper.TakeScreenShot();
		}

	    private void RemoveButtonIcons() 
		{
			// removing the icons, requires a bit of reshuffling of positions
			btnCopy.Image = btnEmail.Image = btnSave.Image = null;
			btnClose.Height = btnDetailToggle.Height = btnCopy.Height = btnEmail.Height = btnSave.Height = 27;
            btnClose.TextAlign = btnDetailToggle.TextAlign = btnCopy.TextAlign = btnEmail.TextAlign = btnSave.TextAlign = ContentAlignment.MiddleCenter;
			btnClose.Font = btnDetailToggle.Font = btnSave.Font = btnEmail.Font = btnCopy.Font = new Font(btnCopy.Font.FontFamily, 8.25f);
			ShiftDown3Pixels(new[] { btnClose, btnDetailToggle, btnCopy, btnEmail, btnSave });
		}

		private static void ShiftDown3Pixels(IEnumerable<Control> buttons)
		{
			foreach (var button in buttons)
				button.Location = Point.Add(button.Location, new Size(new Point(0, 3)));
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
			btnClose.Click += Close_Click;
			btnDetailToggle.Click += Detail_Click;
			urlEmail.LinkClicked += EmailLink_Clicked;
			btnSave.Click += Save_Click;
			urlWeb.LinkClicked += UrlLink_Clicked;
			KeyPreview = true;
			KeyDown += ExceptionReportView_KeyDown;
		}

		void ExceptionReportView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();
		}

		public string ProgressMessage
		{
			set
			{
				lblProgressMessage.Visible = true;
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

		public bool ShowFullDetail
		{
			get { return showFullDetail; }
			set { showFullDetail = value; }
		}

		public void ToggleShowFullDetail()
		{
			if (showFullDetail)
			{
				btnDetailToggle.Text = "Less Detail";
				tabControl.Visible = true;
			}
			else
			{
				btnDetailToggle.Text = "More Detail";
				tabControl.Visible = false;
			}
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

		    var assemblies = new List<AssemblyName>(_presenter.AppAssembly.GetReferencedAssemblies());
            assemblies.Sort(new Comparison<AssemblyName>((x, y) => string.Compare(x.Name, y.Name)));
		    foreach (AssemblyName assemblyName in assemblies)
			{
				var listViewItem = new ListViewItem {Text = assemblyName.Name};
				listViewItem.SubItems.Add(assemblyName.Version.ToString());
				listviewAssemblies.Items.Add(listViewItem);
			}
		}

	    public void PopulateConfigTab(string configFileAsXml)
        {
            webBrowserConfig.DocumentText = configFileAsXml;
        }

	    protected override void OnClosing(CancelEventArgs e)
        {
            _presenter.Dispose();
            webBrowserConfig.Dispose();
            base.OnClosing(e);
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

			var listViewItem = new ListViewItem
			                       {
			                           Text = "Top Level"
			                       };
			listViewItem.SubItems.Add(rootException.GetType().ToString());

		    AddTargetSite(listViewItem, rootException);
		    listViewItem.Tag = "0";
			listviewExceptions.Items.Add(listViewItem);
			listViewItem.Selected = true;

			int index = 0;
			Exception currentException = rootException;

			while (currentException.InnerException != null)
			{
				index++;
				currentException = currentException.InnerException;
				listViewItem = new ListViewItem
				                   {
				                       Text = "Inner Exception " + index
				                   };
				listViewItem.SubItems.Add(currentException.GetType().ToString());
                AddTargetSite(listViewItem, currentException);
				listViewItem.Tag = index.ToString();
				listviewExceptions.Items.Add(listViewItem);
			}

			txtExceptionTabStackTrace.Text = rootException.StackTrace;
			txtExceptionTabMessage.Text = rootException.Message;
		}

	    private static void AddTargetSite(ListViewItem listViewItem, Exception exception)
	    {
            //TargetSite can be null (http://msdn.microsoft.com/en-us/library/system.exception.targetsite.aspx)
	        if (exception.TargetSite != null)
	        {
	            listViewItem.SubItems.Add(exception.TargetSite.ToString());
	        }
	    }

	    private void Copy_Click(object sender, EventArgs e)
		{
			_presenter.CopyReportToClipboard();
		}
		private void Close_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void Detail_Click(object sender, EventArgs e)
		{
			_presenter.ToggleDetail();
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
			{
			    _presenter.SaveReportToFile(saveDialog.FileName);
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
