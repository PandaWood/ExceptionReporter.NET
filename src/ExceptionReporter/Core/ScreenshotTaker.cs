using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ExceptionReporting.Core
{
	internal interface IScreenshotTaker
	{
		Bitmap TakeScreenShot();
		string GetImageAsFile(Bitmap bitmap);
	}

	/// <summary>
	/// Utility to take a screenshot and return as a graphic file 
	/// </summary>
	internal class ScreenshotTaker : IScreenshotTaker
	{
		private const string ScreenshotFileName = "exceptionreport-screenshot.jpg";
		
		/// <summary> Take a screenshot (supports multiple monitors) </summary>
		/// <returns>Bitmap of the screen, as at the time called</returns>
		public Bitmap TakeScreenShot()
		{
      if (ExceptionReporter.IsRunningMono()) return null;

			var rectangle = Rectangle.Empty;

			foreach (var screen in Screen.AllScreens)
			{
				rectangle = Rectangle.Union(rectangle, screen.Bounds);
			}

			var bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);

			using (var graphics = Graphics.FromImage(bitmap))
			{
				graphics.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);
			}

			return bitmap;
		}

		/// <summary>
		/// Return the supplied Bitmap, as a file on the system, in JPEG format
		/// </summary>
		/// <param name="bitmap">The Bitmap to save</param>
		/// <returns></returns>
		public string GetImageAsFile(Bitmap bitmap)
		{
			var tempFileName = Path.GetTempPath() + ScreenshotFileName;		// the image is not deleted but the same file is reused every time
			bitmap.Save(tempFileName, ImageFormat.Jpeg);
			return tempFileName;
		}
	}
}