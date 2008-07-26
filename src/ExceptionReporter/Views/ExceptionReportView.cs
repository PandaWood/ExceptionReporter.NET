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
			ContactEmail = reportInfo.ContactEmail;
			ContactFax = reportInfo.Fax;
			ContactMessageBottom = reportInfo.ContactMessageBottom;
			ContactMessageTop  = reportInfo.ContactMessageTop;
			ContactPhone = reportInfo.Phone;
			ContactWebUrl = reportInfo.WebUrl;
			UserExplanationLabel = reportInfo.UserExplanationLabel;
			ExceptionOccuredMessage = reportInfo.ExceptionOccuredMessage;
			ExceptionMessage = reportInfo.Exception.Message;
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
			lblApplication.Click += lblApplication_Click;
			btnCopy.Click += CopyButton_Click;
			lnkEmail.LinkClicked += EmailLink_Click;
			btnSave.Click += SaveButton_Click;
			urlWeb.LinkClicked += UrlClicked;
		}

		public string ProgressMessage
		{
			set
			{
				lblProgressMessage.Text = value;
				lblProgressMessage.Refresh();
			}
		}

		public bool EnableEmailButton
		{
			set { btnEmail.Enabled = value; }
		}

		public string ExceptionMessage
		{
			set { txtExceptionMessage.Text = value; }
		}

		public bool ShowProgressBar
		{
			set { progressBar.Visible = value; }
		}

		public int ProgressValue
		{
			get { return progressBar.Value; }
			set { progressBar.Value = value; }
		}

		public string ContactEmail
		{
			set { lnkEmail.Text = value; }
		}

		public string ContactWebUrl
		{
			set { urlWeb.Text = value; }
		}

		public string ContactPhone
		{
			set { txtPhone.Text = value; }
		}

		public string ContactFax
		{
			set { txtFax.Text = value; }
		}

		public string ContactMessageTop
		{
			set { lblContactMessageTop.Text = value; }
		}

		public string ContactMessageBottom
		{
			set { lblContactMessageBottom.Text = value; }
		}

		public string ExceptionOccuredMessage
		{
			set { lblGeneral.Text = value; }
		}

		public string UserExplanationLabel
		{
			set { lblExplanation.Text = value; }
		}

		public void SetSendCompleteState()
		{
			ProgressMessage = "Email sent.";
			ShowProgressBar = false;
			btnEmail.Enabled = true;
		}

		private void lblApplication_Click(object sender, EventArgs e)
		{
			//TODO 
		}

		public void PopulateTabs()
		{
			tabControl.TabPages.Clear(); 

			if (_presenter.Info.ShowGeneralTab)
			{
				tabControl.TabPages.Add(tabGeneral);
			}
			if (_presenter.Info.ShowExceptionsTab)
			{
				tabControl.TabPages.Add(tabExceptions);
			}
			if (_presenter.Info.ShowAssembliesTab)
			{
				tabControl.TabPages.Add(tabAssemblies);
			}
			if (_presenter.Info.ShowSettingsTab)
			{
				tabControl.TabPages.Add(tabSettings);
			}
			if (_presenter.Info.ShowEnvironmentTab)
			{
				tabControl.TabPages.Add(tabEnvironment);
			}
			if (_presenter.Info.ShowContactTab)
			{
				tabControl.TabPages.Add(tabContact);
			}
		}

		public void HandleError(string message, Exception ex)
		{
			var simpleExceptionView = new InternalExceptionView();
			simpleExceptionView.ShowException(message, ex);
		}

		//TODO put on a background thread
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			PopulateAll();
		}

		private void PopulateAll()
		{
			if (!_isDataRefreshRequired)		// only refresh when we need to
				return;

			_isDataRefreshRequired = false;		// next time we won't refresh unless this flag is set back to true

			try
			{
				Cursor = Cursors.WaitCursor;
				lblProgressMessage.Visible = true;
				progressBar.Visible = true;
				progressBar.Value = 0;
				progressBar.Maximum = 13;

				PopulateGeneralTab(); progressBar.Value++;
				PopulateEnvironmentTree(); progressBar.Value++;
				PopulateSettingsTab(); progressBar.Value++;
				PopulateExceptionHierarchyTree(_presenter.TheException); progressBar.Value++;
				PopulateAssemblyInfo(); progressBar.Value++;

				PopulateTabs();
			}
			finally
			{
				Cursor = Cursors.Default;
				progressBar.Value = progressBar.Maximum;
				lblProgressMessage.Visible = false;
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

			if (_presenter.Info.EnumeratePrinters)
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
			Exception exCurrent = e;
			bool shouldContinue = (exCurrent.InnerException != null);

			while (shouldContinue)
			{
				index++;
				exCurrent = exCurrent.InnerException;
				listViewItem = new ListViewItem {Text = ("Inner Exception " + index)};
				listViewItem.SubItems.Add(exCurrent.GetType().ToString());

				if (exCurrent.TargetSite != null)
					listViewItem.SubItems.Add(exCurrent.TargetSite.ToString());

				listViewItem.Tag = index.ToString();
				listviewExceptions.Items.Add(listViewItem);

				shouldContinue = (exCurrent.InnerException != null);
			}

			txtStackTrace.Text = e.StackTrace;
			txtMessage.Text = e.Message;
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
				//TODO should be gotten from the ReportInfo, not the UI
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
			{
				//TODO this should be gotten from the ReportInfo, not the UI
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

		private static void ShowError(string strMessage, Exception ex)
		{
			var simpleExceptionView = new InternalExceptionView();
			simpleExceptionView.ShowException(strMessage, ex);
		}
	}
}
