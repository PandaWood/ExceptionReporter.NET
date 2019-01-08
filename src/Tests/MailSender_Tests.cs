using System;
using ExceptionReporting;
using ExceptionReporting.Network.Senders;
using NUnit.Framework;

namespace Tests.ExceptionReporting
{
	public class MailSender_Tests
	{
		[Test]
		public void Can_Create_Subject()
		{
			var exception = new Exception("hello");
			var reportInfo = new ExceptionReportInfo { TitleText = "test" };
			reportInfo.SetExceptions(new[] { exception });
			var mailSender = new MapiMailSender(reportInfo, null);

			Assert.That(mailSender.EmailSubject, Is.EqualTo("hello"));
		}

		/// <summary>
		/// Logically and because the MailMessage class will throw an exception, we don't want CR/LF
		/// </summary>
		[Test]
		public void Can_Create_Subject_Without_CrLf()
		{
			var reportInfo = new ExceptionReportInfo();
			reportInfo.SetExceptions(new[] { new Exception("hello\r\nagain") });
			var mailSender = new MapiMailSender(reportInfo, null);

			Assert.That(mailSender.EmailSubject, Does.Not.Contain("\r"));
			Assert.That(mailSender.EmailSubject, Does.Not.Contain("\n"));
		}

		[Test]
		public void Can_Create_Subject_If_Exception_Is_Null()
		{
			var mailSender = new MapiMailSender(new ExceptionReportInfo(), null);		// no exceptions set, so message will be null, does mail cater for it?

			Assert.That(mailSender.EmailSubject, Is.EqualTo("Empty Exception"));		// reverts to a default message
		}

		[Test]
		public void Can_Use_Custom_Subject()
		{
			var exception = new Exception("Exception");
			var reportInfo = new ExceptionReportInfo { TitleText = "test" };
			reportInfo.SetExceptions(new[] { exception });
			reportInfo.EmailReportSubject  = "hello";
			var mailSender = new MapiMailSender(reportInfo, null);

			Assert.That(mailSender.EmailSubject, Is.EqualTo("hello"));
		}
  }
}