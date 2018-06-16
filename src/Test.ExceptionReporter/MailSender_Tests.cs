using System;
using ExceptionReporting.Core;
using ExceptionReporting.Mail;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class MailSender_Tests
	{
		[Test]
		public void Can_Create_Subject()
		{
			var exception = new Exception("hello");
			var reportInfo = new ExceptionReportInfo { TitleText = ";" };
			reportInfo.SetExceptions(new[] { exception });
			var mailSender = new MailSender(reportInfo);

			Assert.That(mailSender.EmailSubject, Is.EqualTo("hello"));
		}

		/// <summary>
		/// Logically and because the MailMessage class will throw an exception, we don't want CR/LF
		/// </summary>
		[Test]
		public void Can_Create_Subject_Without_CrLf()
		{
			var reportInfo = new ExceptionReportInfo { TitleText = ";" };
			reportInfo.SetExceptions(new[] { new Exception("hello\r\nagain") });
			var mailSender = new MailSender(reportInfo);

			Assert.That(mailSender.EmailSubject, Does.Not.Contain("\r"));
			Assert.That(mailSender.EmailSubject, Does.Not.Contain("\n"));
		}
	}
}