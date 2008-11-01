using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ExceptionReporting.Core
{
	public static class ScreenshotHelper
	{
		public static Bitmap ScreenShot()
		{
			Rectangle totalSize = Rectangle.Empty;

			foreach (Screen s in Screen.AllScreens)
				totalSize = Rectangle.Union(totalSize, s.Bounds);

			var screenShotBMP = new Bitmap(
				totalSize.Width, totalSize.Height,
				PixelFormat.Format32bppArgb);

			Graphics screenShotGraphics = Graphics.FromImage(screenShotBMP);

			screenShotGraphics.CopyFromScreen(
				totalSize.X, totalSize.Y,
				0, 0, totalSize.Size,
				CopyPixelOperation.SourceCopy);

			screenShotGraphics.Dispose();

			return screenShotBMP;
		}

		public static string GetBitmapAsFile(Bitmap bitmap)
		{
			string tempFileName = Path.GetTempPath() + "screenshot.bmp";
			bitmap.Save(tempFileName);
			return tempFileName;
		}
	}
}