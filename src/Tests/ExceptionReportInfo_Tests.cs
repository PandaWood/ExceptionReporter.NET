using System;
using System.Collections.Generic;
using System.Linq;
using ExceptionReporting;
using NUnit.Framework;

namespace Tests.ExceptionReporting
{
	public class ExceptionReportInfo_Tests
	{
		private ExceptionReportInfo _info;
		private Exception _exception;

		[SetUp]
		public void SetUp()
		{
			_info = new ExceptionReportInfo();
			_exception = new Exception("test");
		}

		[Test]
		public void Can_Get_And_Set_1_Exception_Without_Knowing_There_Can_Be_Many()
		{
			_info.MainException = _exception;
			Assert.That(_info.MainException, Is.EqualTo(_exception));
		}

		[Test]
		public void Can_Show_Same_Exception_Set_By_Main_Exception_Property()
		{
			_info.MainException = _exception;

			Assert.That(_info.Exceptions.Count, Is.EqualTo(1));
			Assert.That(_info.Exceptions.First(), Is.EqualTo(_exception));
		}

		[Test]
		public void Main_Exception_Shows_First_Exception()
		{
			_info.SetExceptions(new List<Exception>
													{
														new Exception("test1"),
														new Exception("test2")
													});

			Assert.That(_info.MainException.Message, Is.EqualTo("test1"));
		}

		[Test]
		public void Can_Set_Multiple_Exceptions()
		{
			_info.SetExceptions(new List<Exception>
													{
														new Exception("test1"),
														new Exception("test2")
													});

			Assert.That(_info.Exceptions.Count, Is.EqualTo(2));
		}

		[Test]
		public void When_Exception_Already_Exists_Other_Exceptions_Are_Cleared()
		{
			_info.MainException = _exception;
			_info.SetExceptions(new List<Exception>
													{
														new Exception("test1"),
														new Exception("test2")
													});

			Assert.That(_info.Exceptions.Count, Is.Not.EqualTo(3));
			Assert.That(_info.Exceptions.Count, Is.EqualTo(2));
		}

		[Test]
		public void When_Multiple_Exceptions_Already_Exist_Other_Exceptions_Are_Cleared()
		{
			_info.SetExceptions(new List<Exception>
													{
														new Exception("test1"),
														new Exception("test2")
													});

			Assert.That(_info.Exceptions.Count, Is.EqualTo(2));
			_info.MainException = _exception;
			Assert.That(_info.Exceptions.Count, Is.EqualTo(1));
		}
		
		[TestCase("att",     ExpectedResult = "att.zip")]
		[TestCase("att.zip", ExpectedResult = "att.zip")]
		[TestCase("att.ZIP", ExpectedResult = "att.ZIP")]
		public string Can_Determine_AttachmentFilename(string attachment)
		{
			_info.AttachmentFilename = attachment;
			return _info.AttachmentFilename;
		}

		[TestCase(ReportSendMethod.None,       ExpectedResult = false)]
		[TestCase(ReportSendMethod.SimpleMAPI, ExpectedResult = true)]
		[TestCase(ReportSendMethod.SMTP,       ExpectedResult = false)]
		[TestCase(ReportSendMethod.WebService, ExpectedResult = false)]
		public bool Can_Determine_IsSimpleMAPI(ReportSendMethod method)
		{
			_info.SendMethod = method;
			return _info.IsSimpleMAPI();
		}
		
		[TestCase(ReportSendMethod.None, true,       ExpectedResult = false)]
		[TestCase(ReportSendMethod.SMTP, true,       ExpectedResult = true)]
		[TestCase(ReportSendMethod.SMTP, false,      ExpectedResult = false)]
		[TestCase(ReportSendMethod.SimpleMAPI, true, ExpectedResult = true)]
		[TestCase(ReportSendMethod.WebService, true, ExpectedResult = true)]
		public bool Can_Determine_ShowEmailButton(ReportSendMethod method, bool show)
		{
			_info.SendMethod = method;
			_info.ShowEmailButton = show;
			return _info.ShowEmailButton;
		}
	}
}