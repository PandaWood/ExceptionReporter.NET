// MIT License
// Copyright (c) 2008-2018 Peter van der Woude
// https://github.com/PandaWood/ExceptionReporter.NET
//

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using ExceptionReporting;
using ExceptionReporting.Core;

namespace Demo.WinForms {
  public partial class DemoAppView : Form {

	static Exception exception1 = new IOException(
"Unable to establish a connection with the Foo bank account service: " + Guid.NewGuid().ToString(),
new Exception(
"This is an Inner Exception message - with a message that is not too small but perhaps it should be smaller"));



	public DemoAppView() {
	  InitializeComponent();

	  urlDefault.Click += Show_Default_Report;
	  urlHideDetail.Click += Show_HideDetailView_Click;
	  urlEmailTest.Click += Show_Email_Attachment_Test;
	  urlDialogless.Click += Send_Silent_Report;

	}

	private void Show_Email_Attachment_Test(object sender, EventArgs e) {
	  throw new NotImplementedException();
	}

	static void Show_Default_Report(object sender, EventArgs e) {
	  ThrowAndShowExceptionReporter();
	}

	static void Show_HideDetailView_Click(object sender, EventArgs e) {
	  ThrowAndShowExceptionReporter(detailView: true);
	}

	void Send_Silent_Report(object sender, EventArgs e) {
	  throw new NotImplementedException();
	}

	ExceptionReporter CreateEmailReadyReporter() {
	  var exceptionReporter = new ExceptionReporter();
	  //			ConfigureSmtpEmail(exceptionReporter.Config);		// comment one in/out to test SMTP or WebService
	  ConfigureWebService(exceptionReporter.Config);
	  return exceptionReporter;
	}

	void ConfigureWebService(ExceptionReportInfo config) {
	  //config.MailMethod = ExceptionReportInfo.EmailMethod.WebService;
	  config.WebServiceUrl = "http://localhost:24513/api/er";
	}

	void ConfigureSmtpEmail(ExceptionReportInfo config) {
	  //--- Test SMTP - recommended:
	  // 1. MailSlurper https://github.com/mailslurper
	  // 2. https://github.com/rnwood/smtp4dev
	  //config.MailMethod = ExceptionReportInfo.EmailMethod.SMTP;
	  config.SmtpServer = "127.0.0.1";
	  config.SmtpPort = 2500;
	  config.SmtpUsername = "";
	  config.SmtpPassword = "";
	  config.SmtpFromAddress = "test@test.com";
	  config.EmailReportAddress = "support@support.com";
	  config.SmtpUseSsl = false;
	}

	static void ThrowAndShowExceptionReporter(bool detailView = false) {
	  try {
		SomeMethodThatThrows();
	  }
	  catch (Exception exception) {
		var exceptionReporter = new ExceptionReporter();

		if (detailView) {
		  exceptionReporter.Config.ShowFullDetail = false;
		  exceptionReporter.Config.ShowLessMoreDetailButton = true;
		  //					exceptionReporter.Config.ShowEmailButton = false;		// just for testing that removing email button works well positioning etc
		}
		exceptionReporter.Show(exception);
	  }
	}

	static void SomeMethodThatThrows() {
	  CallAnotherMethod();
	}

	static void CallAnotherMethod() {
	  AndAnotherOne();
	}

	static void AndAnotherOne() {
	  throw exception1;
	}

	private void linkLabelSentWebEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
	  //TODO: send atachments

	  try {
		throw exception1;
	  }
	  catch (Exception) {

		var er = new ExceptionReporter();

		//er.Config.WebReportUrl = "http://localhost/ExceptionReporter.Demo.WebServer/PHP/ERServer.php?o=1";
		//er.Send(ExceptionReportInfo.SendMethod.WebPage, exception1);

		MessageBox.Show("Tried");

	  }


	}

	private void linkLabelSentWebGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
	  //TODO: send atachments

	  // https://help.github.com/articles/file-attachments-on-issues-and-pull-requests/

	  try {
		throw exception1;
	  }
	  catch (Exception) {

		var er = new ExceptionReporter();

		//er.Config.WebReportUrl = "http://localhost/ExceptionReporter.Demo.WebServer/PHP/ERServer.php?o=2";
		//er.Send(ExceptionReportInfo.SendMethod.WebPage, exception1);

		MessageBox.Show("Tried");

	  }



	}


	#region globalization
	private void buttonLang_Click(object sender, EventArgs e) {
	  if (comboBoxLang.SelectedItem == null)
		return;

	  string lang = comboBoxLang.SelectedItem.ToString();
	  if (!string.IsNullOrEmpty(lang)) {
		//System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-BE");

		if (lang.Contains("default-en")) {
		  System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
		}
		else {
		  System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
		}

		ComponentResourceManager resources = new ComponentResourceManager(typeof(DemoAppView));
		resources.ApplyResources(this, "$this");
		applyResources(resources, this.Controls);
	  }

	}

	private void applyResources(ComponentResourceManager resources, Control.ControlCollection ctls) {
	  foreach (Control ctl in ctls) {
		resources.ApplyResources(ctl, ctl.Name);
		applyResources(resources, ctl.Controls);
	  }
	}
	#endregion globalization

  }
}