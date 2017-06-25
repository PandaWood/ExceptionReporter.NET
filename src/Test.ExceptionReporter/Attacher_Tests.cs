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
			var attacher = new Attacher(new ExceptionReportInfo { FilesToAttach = new string[] {} })
			{
				File = _file.Object,
				Zipper = _zip.Object
			};

			attacher.AttachFiles(_iattach.Object);

			_iattach.Verify((a) => a.Attach(""), Times.Never);
		}

		[Test]
		public void can_attach_1_with_1_file()
		{
			_file.Setup((f) => f.Exists("file1")).Returns(true);
			_file.Setup((f) => f.TempFile(It.IsAny<string>())).Returns("attach.zip");

			var attacher = new Attacher(new ExceptionReportInfo { FilesToAttach = new string[] { "file1" } })
			{
				File = _file.Object,
				Zipper = _zip.Object
			};

			attacher.AttachFiles(_iattach.Object);

			_iattach.Verify((a) => a.Attach("attach.zip"), Times.Once);
		}

		[Test]
		public void can_attach_1_with_multiple_files()
		{
			_file.Setup((f) => f.Exists(It.IsAny<string>())).Returns(true);
			_file.Setup((f) => f.TempFile(It.IsAny<string>())).Returns("attach.zip");

			var attacher = new Attacher(new ExceptionReportInfo { FilesToAttach = new string[] { "file1", "file2", "file3" } })
			{
				File = _file.Object,
				Zipper = _zip.Object
			};

			attacher.AttachFiles(_iattach.Object);

			_iattach.Verify((a) => a.Attach("attach.zip"), Times.Once);
		}

		[Test]
		public void can_attach_0_when_multiple_files_are_provided_but_dont_actually_exist()
		{
			_file.Setup((f) => f.Exists(It.IsAny<string>())).Returns(false);
			_file.Setup((f) => f.TempFile(It.IsAny<string>())).Returns("attach.zip");

			var attacher = new Attacher(new ExceptionReportInfo { FilesToAttach = new string[] { "file1", "file2", "file3" } })
			{
				File = _file.Object,
				Zipper = _zip.Object
			};

			attacher.AttachFiles(_iattach.Object);

			_iattach.Verify((a) => a.Attach("attach.zip"), Times.Never());
		}

		[Test]
		public void can_attach_1_for_each_already_zipped()
		{
			_file.Setup((f) => f.Exists(It.IsAny<string>())).Returns(true);
			var attacher = new Attacher(new ExceptionReportInfo { FilesToAttach = new string[] { "file1.zip", "file2.zip" } })
			{
				File = _file.Object,
				Zipper = _zip.Object
			};

			attacher.AttachFiles(_iattach.Object);

			_iattach.Verify((a) => a.Attach("file1.zip"), Times.Once());
			_iattach.Verify((a) => a.Attach("file2.zip"), Times.Once());
		}

		[Test]
		public void can_attach_1_already_zipped_and_1_filestoattach()
		{
			_file.Setup((f) => f.TempFile(It.IsAny<string>())).Returns("attach.zip");
			_file.Setup((f) => f.Exists(It.IsAny<string>())).Returns(true);

			var attacher = new Attacher(new ExceptionReportInfo { FilesToAttach = new string[] { "file1.zip" , "file2.txt"} })
			{
				File = _file.Object,
				Zipper = _zip.Object
			};

			attacher.AttachFiles(_iattach.Object);

			_iattach.Verify((a) => a.Attach("file1.zip"), Times.Once());
			_iattach.Verify((a) => a.Attach("attach.zip"), Times.Once());
		}
	}
}
