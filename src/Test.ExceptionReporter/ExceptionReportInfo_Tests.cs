using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ExceptionReporting.Tests
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
			Assert.That(_info.Exceptions[0], Is.EqualTo(_exception));
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

		[Test]
		public void Can_Set_Attachment_Filename_Zip_Added()
		{
			_info.AttachmentFilename = "test";
			Assert.That(_info.AttachmentFilename, Is.EqualTo("test.zip"));
		}

		[Test]
		public void Can_Set_Attachment_Filename_Zip_Already_Exists()
		{
			_info.AttachmentFilename = "test.zip";
			Assert.That(_info.AttachmentFilename, Is.EqualTo("test.zip"));
		}
	}
}