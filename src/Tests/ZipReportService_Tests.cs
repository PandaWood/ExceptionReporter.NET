using System.Collections.Generic;
using System.Linq;
using ExceptionReporting;
using ExceptionReporting.Core;
using ExceptionReporting.Zip;
using Moq;
using NUnit.Framework;

namespace Tests.ExceptionReporting
{
	public class ZipReportService_Tests
	{
		private Mock<IScreenShooter> _screenshotTaker;
		private Mock<IFileService> _fileService;
		private Mock<IZipper> _zipper;

		[SetUp]
		public void SetUp()
		{
			_screenshotTaker = new Mock<IScreenShooter>();
			_fileService = new Mock<IFileService>();
			_zipper = new Mock<IZipper>();
		}

		[Test]
		public void None_Files_To_Add_To_Archive_ReturnsEmptyString()
		{
			const string zipFilename = "Test.zip";

			_fileService.Setup(f => f.TempFile(zipFilename)).Returns(zipFilename);
			_zipper.Setup(z => z.Zip(zipFilename, It.IsAny<IEnumerable<string>>()));

			var config = new ExceptionReportInfo
			{
				FilesToAttach = new List<string>().ToArray(), 
				TakeScreenshot = false, 
				AttachmentFilename = zipFilename
			};
			var zip = new ZipAttachmentService(_zipper.Object, _screenshotTaker.Object, _fileService.Object);
			var result = zip.CreateZipReport(config);

			Assert.IsTrue(result == string.Empty);
			_zipper.Verify(z => z.Zip(zipFilename, It.IsAny<IEnumerable<string>>()), Times.Never);
		}

		[Test]
		public void Two_Files_But_None_Exists_ReturnsEmptyString()
		{
			const string logFile1 = "file1.log";
			const string logFile2 = "file2.log";
			const string zipFilename = "Test.zip";

			_fileService.Setup(f => f.Exists(logFile1)).Returns(false);
			_fileService.Setup(f => f.Exists(logFile2)).Returns(false);
			_fileService.Setup(f => f.Exists(zipFilename)).Returns(true);
			_fileService.Setup(f => f.TempFile(zipFilename)).Returns(zipFilename);
			_zipper.Setup(z => z.Zip(zipFilename, It.IsAny<IEnumerable<string>>()));

			var filesToAttach = new List<string>
			{
				logFile1, logFile2
			};

			var config = new ExceptionReportInfo
			{
				FilesToAttach = filesToAttach.ToArray(), 
				TakeScreenshot = false, 
				AttachmentFilename = zipFilename
			};
			var zip = new ZipAttachmentService(_zipper.Object, _screenshotTaker.Object, _fileService.Object);
			var result = zip.CreateZipReport(config);

			Assert.That(result, Is.EqualTo(string.Empty));
			_zipper.Verify(z => z.Zip(zipFilename, It.IsAny<IEnumerable<string>>()), Times.Never);
		}

		[Test]
		public void One_File_To_Add_To_Archive_ReturnsArchiveWithOneFile()
		{
			const string logFile = "file1.log";
			const string zipFilename = "Test.zip";

			_fileService.Setup(f => f.Exists(logFile)).Returns(true);
			_fileService.Setup(f => f.Exists(zipFilename)).Returns(true);
			_fileService.Setup(f => f.TempFile(zipFilename)).Returns(zipFilename);

			var filesToAttach = new List<string> {logFile};
			var config = new ExceptionReportInfo
			{
				FilesToAttach = filesToAttach.ToArray(), 
				TakeScreenshot = false, 
				AttachmentFilename = zipFilename
			};
			var zip = new ZipAttachmentService(_zipper.Object, _screenshotTaker.Object, _fileService.Object);
			var result = zip.CreateZipReport(config);

			Assert.That(result, Is.EqualTo(zipFilename));
			_zipper.Verify(z => z.Zip(zipFilename, It.Is<IEnumerable<string>>(en => en.Count() == 1)), Times.AtLeastOnce);
		}

		[Test]
		public void Only_Screenshot_To_Add_To_Archive_ReturnsArchiveWithOneFile()
		{
			const string zipFilename = "Test.zip";
			const string screenshotFilename = "Screenshot.jpg";

			_screenshotTaker.Setup(s => s.TakeScreenShot()).Returns(screenshotFilename);
			_fileService.Setup(f => f.Exists(screenshotFilename)).Returns(true);
			_fileService.Setup(f => f.Exists(zipFilename)).Returns(true);
			_fileService.Setup(f => f.TempFile(zipFilename)).Returns(zipFilename);
			_zipper.Setup(z => z.Zip(zipFilename, It.Is<IEnumerable<string>>(en => en.Count() == 1)));

			var config = new ExceptionReportInfo
			{
				FilesToAttach = new string[]{}, 
				TakeScreenshot = true, 
				AttachmentFilename = zipFilename
			};
			var zip = new ZipAttachmentService(_zipper.Object, _screenshotTaker.Object, _fileService.Object);
			var result = zip.CreateZipReport(config);

			Assert.That(result, Is.EqualTo(zipFilename));
			_zipper.Verify(z => z.Zip(zipFilename, It.Is<IEnumerable<string>>(en => en.Count() == 1)), Times.AtLeastOnce);
		}

		[Test]
		public void Two_Files_To_Add_To_Archive_ReturnsArchiveWithTwoFiles()
		{
			const string logFile1 = "file1.log";
			const string logFile2 = "file2.log";
			const string zipFilename = "Test.zip";

			_fileService.Setup(f => f.Exists(logFile1)).Returns(true);
			_fileService.Setup(f => f.Exists(logFile2)).Returns(true);
			_fileService.Setup(f => f.Exists(zipFilename)).Returns(true);
			_fileService.Setup(f => f.TempFile(zipFilename)).Returns(zipFilename);

			var filesToAttach = new List<string> {logFile1, logFile2};
			var config = new ExceptionReportInfo
			{
				FilesToAttach = filesToAttach.ToArray(), TakeScreenshot = false, AttachmentFilename = zipFilename
			};
			var zip = new ZipAttachmentService(_zipper.Object, _screenshotTaker.Object, _fileService.Object);
			var result = zip.CreateZipReport(config);

			Assert.That(result, Is.EqualTo(zipFilename));
			_zipper.Verify(z => z.Zip(zipFilename, It.Is<IEnumerable<string>>(en => en.Count() == 2)), Times.AtLeastOnce);
		}

		[Test]
		public void Take_Screenshot_True_MakesScreenshot()
		{
			const string zipFilename = "Test.zip";
			const string screenshotFilename = "Screenshot.jpg";

			_screenshotTaker.Setup(s => s.TakeScreenShot()).Returns(screenshotFilename);
			_fileService.Setup(f => f.Exists(screenshotFilename)).Returns(true);
			_fileService.Setup(f => f.TempFile(zipFilename)).Returns(zipFilename);

			var config = new ExceptionReportInfo
			{
				TakeScreenshot = true, 
				AttachmentFilename = zipFilename
			};
			var zip = new ZipAttachmentService(_zipper.Object, _screenshotTaker.Object, _fileService.Object);
			zip.CreateZipReport(config);

			_screenshotTaker.Verify(s => s.TakeScreenShot(), Times.Once());
		}

		[Test]
		public void Take_Screenshot_False_DoesNotMakeScreenshot()
		{
			const string zipFilename = "Test.zip";
			const string screenshotFilename = "Screenshot.jpg";

			_screenshotTaker.Setup(s => s.TakeScreenShot()).Returns(screenshotFilename);
			_fileService.Setup(f => f.Exists(screenshotFilename)).Returns(true);
			_fileService.Setup(f => f.TempFile(zipFilename)).Returns(zipFilename);

			var config = new ExceptionReportInfo
			{
				TakeScreenshot = false, 
				AttachmentFilename = zipFilename
			};
			var zip = new ZipAttachmentService(_zipper.Object, _screenshotTaker.Object, _fileService.Object);
			zip.CreateZipReport(config);

			_screenshotTaker.Verify(s => s.TakeScreenShot(), Times.Never());
		}
	}
}