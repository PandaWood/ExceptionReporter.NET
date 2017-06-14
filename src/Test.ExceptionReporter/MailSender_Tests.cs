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
        public void CanCreateSubject()
        {
            var exception = new Exception("hello");
            var reportInfo = new ExceptionReportInfo { TitleText = ";" };
            reportInfo.SetExceptions(new []{exception});
            var mailSender = new MailSender(reportInfo);

            Assert.That(mailSender.EmailSubject, Is.EqualTo("hello"));
        }
    }
}