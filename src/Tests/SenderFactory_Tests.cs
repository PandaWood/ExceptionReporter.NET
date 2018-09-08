using System;
using ExceptionReporting;
using ExceptionReporting.Network;
using ExceptionReporting.Network.Events;
using ExceptionReporting.Network.Senders;
using Moq;
using NUnit.Framework;

namespace Tests.ExceptionReporting
{
	public class SenderFactory_Tests
	{
		[TestCase(ReportSendMethod.None,       ExpectedResult = typeof(GhostSender))]
		[TestCase(ReportSendMethod.SimpleMAPI, ExpectedResult = typeof(MapiMailSender))]
		[TestCase(ReportSendMethod.SMTP,       ExpectedResult = typeof(SmtpMailSender))]
		[TestCase(ReportSendMethod.WebService, ExpectedResult = typeof(WebServiceSender))]
		public Type Can_Determine_Sender(ReportSendMethod method)
		{
			var factory = new SenderFactory(new ExceptionReportInfo 
			{ 
				SendMethod = method
			}, new Mock<IReportSendEvent>().Object);

			return factory.Get().GetType();
		}
	}
}