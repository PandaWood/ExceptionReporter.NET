using ExceptionReporting.MVP;
using ExceptionReporting.Network.Events;
using ExceptionReporting.Network.Senders;
using Moq;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	public class SenderFactory_Tests
	{
		private IReportSendEvent _sendEvent;

		[SetUp]
		public void SetUp()
		{
			_sendEvent = new Mock<IReportSendEvent>().Object;
		}
		
		[Test]
		public void Can_Get_Default()
		{
			var factory = new SenderFactory(new ExceptionReportInfo { 
				SendMethod = ReportSendMethod.None
			}, _sendEvent);
			
			Assert.That(factory.Get(), Is.TypeOf<MapiMailSender>());
		}
		
		[Test]
		public void Can_Get_MAPI_Sender()
		{
			var factory = new SenderFactory(new ExceptionReportInfo
			{
				SendMethod = ReportSendMethod.SimpleMAPI
			}, _sendEvent);
			
			Assert.That(factory.Get(), Is.TypeOf<MapiMailSender>());
		}
		
		[Test]
		public void Can_Get_SMTP_Sender()
		{
			var factory = new SenderFactory(new ExceptionReportInfo
			{
				SendMethod = ReportSendMethod.SMTP
			}, _sendEvent);
			
			Assert.That(factory.Get(), Is.TypeOf<SmtpMailSender>());
		}
		
		[Test]
		public void Can_Get_WebService_Sender()
		{
			var factory = new SenderFactory(new ExceptionReportInfo
			{
				SendMethod = ReportSendMethod.WebService
			}, _sendEvent);
			
			Assert.That(factory.Get(), Is.TypeOf<WebServiceSender>());
		}
		
		[Test]
		public void Can_Get_Obsolete_MailMethod_SMTP_Sender()
		{
			var factory = new SenderFactory(new ExceptionReportInfo
			{
				MailMethod= ExceptionReportInfo.EmailMethod.SMTP
			}, _sendEvent);
			
			Assert.That(factory.Get(), Is.TypeOf<SmtpMailSender>());
		}
		
		[Test]
		public void Can_Get_Obsolete_MailMethod_MAPI_Sender()
		{
			var factory = new SenderFactory(new ExceptionReportInfo
			{
				MailMethod= ExceptionReportInfo.EmailMethod.SimpleMAPI
			}, _sendEvent);
			
			Assert.That(factory.Get(), Is.TypeOf<MapiMailSender>());
		}
	}
}