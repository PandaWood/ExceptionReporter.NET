using System;
using System.Collections.Generic;
using ExceptionReporter.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace ExceptionReporter.Tests
{
	[TestFixture]
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

		[TearDown]
        public void TearDown()
		{
			_info.Dispose();
		}

		[Test]
		public void Can_GetAndSet_1_Exception_WithoutKnowingOrCaring_ThereCanBe_Many()
		{
			_info.MainException = _exception;

			Assert.That(_info.MainException, Is.EqualTo(_exception));
		}

		[Test]
		public void ExceptionsProperty_ShowsSameException_SetBy_MainExceptionProperty()
		{
			_info.MainException = _exception;

			Assert.That(_info.Exceptions.Count, Is.EqualTo(1));
			Assert.That(_info.Exceptions[0], Is.EqualTo(_exception));
		}

		[Test]
		public void OnSetExceptions_MainException_ShowsFirstException()
		{
			_info.SetExceptions(new List<Exception>
			                    {
			                    	new Exception("test1"),
			                    	new Exception("test2")
			                    });

			Assert.That(_info.MainException.Message, Is.EqualTo("test1"));
		}

		[Test]
		public void CanSet_MultipleExceptions()
		{
			_info.SetExceptions(new List<Exception>
			                    {
			                    	new Exception("test1"),
			                    	new Exception("test2")
			                    });

			Assert.That(_info.Exceptions.Count, Is.EqualTo(2));
		}

		[Test]
		public void OnSetExceptions_WhenExceptionAlreadyExists_OtherExceptionsAreCleared()
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
		public void OnMainExceptionSet_WhenExceptionsAlreadyExist_OtherExceptionsAreCleared()
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
	}
}