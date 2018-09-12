using System;
using System.Collections.Generic;
using ExceptionReporting;
using ExceptionReporting.MVP.Views;
using Moq;
using NUnit.Framework;

namespace Tests.ExceptionReporting
{
	/// <summary>
	/// Testing ExceptionReporter is mostly a case of integration testing (ie using the demo)
	/// However, we test all the logical inputs and return values here
	/// </summary>
	public class ExceptionReporter_Tests
	{
		[Test]
		public void Can_Init_App_Assembly()
		{
			var er = new ExceptionReporter();
			Assert.That(er.Config.AppAssembly, Is.Null);
		}

		[TestCase(null,                     ExpectedResult = false)]
		[TestCase(default(List<Exception>), ExpectedResult = false)]
		public bool Can_Prevent_Showing_If_Null_Exception(params Exception[] exceptions)
		{
			var er = new ExceptionReporter
			{
				ViewMaker = new Mock<IViewMaker>().Object
			};
			return er.Show(exceptions);
		}
		
		[Test]
		public void Can_Show()
		{
			var viewMock = new Mock<IViewMaker>();
			viewMock.Setup(v => v.Create()).Returns(new Mock<IExceptionReportView>().Object);
			
			var er = new ExceptionReporter
			{
				ViewMaker = viewMock.Object
			};
			Assert.That(er.Show(new TestException()), Is.True);
		}
		
		[Test]
		public void Can_Not_Show_If_No_Exception()
		{
			var er = new ExceptionReporter
			{
				ViewMaker = new Mock<IViewMaker>().Object
			};
			Assert.That(er.Show(), Is.False);
		}

		[Test]
		public void Can_Init_Config_Using_Object_Initializer()
		{
			var er = new ExceptionReporter
			{
				Config =
				{	// test that this style of initialization (settings properties directly on config) remains possible
					AppName = "PhotoFuzz",
					AppVersion = "1.0",
					CompanyName = "photofuzz",
					SendMethod = ReportSendMethod.WebService,
					WebServiceUrl = "http://photofuzz/apiv1", 
					FilesToAttach = new[] {"app.log"}
				}
			};
			er.Show();
		}
	}
}