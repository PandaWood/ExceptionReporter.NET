using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ExceptionReporter.Core
{
	/// <summary>
	/// utility to take a screenshot and return as a file 
	/// </summary>
	public static class ScreenshotTaker
    {
    	private const string ScreenshotFileName = "ExceptionReport_Screenshot.jpg";
        public const string ScreenshotMimeType = "image/jpeg";

		/// <summary> Take a screenshot (supports multiple monitors) </summary>
		/// <returns>Bitmap of the screen, as at the time called</returns>
        public static Bitmap TakeScreenShot()
        {
            Rectangle rectangle = Rectangle.Empty;

            foreach (Screen screen in Screen.AllScreens)
            {
                rectangle = Rectangle.Union(rectangle, screen.Bounds);
            }

            var bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);
            }

            return bitmap;
        }

		/// <summary>
		/// Return the supplied Bitmap, as a file on the system, in JPEG format
		/// </summary>
		/// <param name="bitmap">The Bitmap to save (most likely one created using TakeScreenshot()</param>
		/// <returns></returns>
        public static string GetImageAsFile(Bitmap bitmap)
        {
            string tempFileName = Path.GetTempPath() + ScreenshotFileName;
            bitmap.Save(tempFileName, ImageFormat.Jpeg);
            return tempFileName;
        }
    }
}