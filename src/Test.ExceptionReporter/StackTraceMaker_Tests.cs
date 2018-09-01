using System;
using ExceptionReporting.Report;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	public class StackTraceMaker_Tests
	{
		[Test]
		public void Can_Create_StackTrace_With_1_Exception()
		{
			var maker = new StackTraceMaker(new TestException());
			var stackTrace = maker.FullStackTrace();
			
			Assert.That(stackTrace, 
				Is.EqualTo(string.Format(
					"Top-level Exception{0}Type:    ExceptionReporting.Tests.TestException{0}Message: NullRef{0}Source:{0}", 
						Environment.NewLine)));
		}
		
		[Test]
		public void Can_Create_StackTrace_With_2_Exceptions()
		{
			// both exceptions are identical, but this still serves our testing purposes
			var maker = new StackTraceMaker(new TestException(), new TestException());
			var stackTrace = maker.FullStackTrace();
			
			Assert.That(stackTrace, 
				Is.EqualTo(string.Format(
					"Top-level Exception{0}Type:    ExceptionReporting.Tests.TestException{0}Message: NullRef{0}Source:{0}" + 
					"Top-level Exception{0}Type:    ExceptionReporting.Tests.TestException{0}Message: NullRef{0}Source:{0}", 
						Environment.NewLine)));
		}
	}
}