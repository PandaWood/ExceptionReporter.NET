using System.Collections.Generic;
using ExceptionReporting.Core;
using ExceptionReporting.Mail;
using Moq;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	[TestFixture]
	public class Attacher_Tests
	{
		Mock<IAttach> _iattach;
		Mock<IFileService> _file;
		Mock<IZipper> _zip;

		[SetUp]
		public void SetUp()
		{
			_iattach = new Mock<IAttach>();
			_file = new Mock<IFileService>();
			_zip = new Mock<IZipper>();
		}

		[Test]
		public void can_attach_nothing_if_none_to_attach()
		{
			var attacher = new Attacher(new ExceptionReportInfo
			{
				FilesToAttach = new string[] {}
			})
			{
				File = _file.Object,
				Zipper = _zip.Object
			};

			attacher.AttachFiles(_iattach.Object);

			_iattach.Verify((a) => a.Attach(""), Times.Never);
		}

		[Test]
		public void can_attach_1_that_exists()
		{
			_file.Setup((f) => f.Exists("file1")).Returns(true);
			_file.Setup((f) => f.TempFile(It.IsAny<string>())).Returns("temp/file.zip");

			var attacher = new Attacher(new ExceptionReportInfo
			{
				FilesToAttach = new string[] { "file1" }
			})
			{
				File = _file.Object,
				Zipper = _zip.Object
			};

			attacher.AttachFiles(_iattach.Object);

			_iattach.Verify((a) => a.Attach("temp/file.zip"), Times.Once);
		}
	}
}
