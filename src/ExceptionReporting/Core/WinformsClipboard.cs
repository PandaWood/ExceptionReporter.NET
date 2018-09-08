namespace ExceptionReporting.Core
{
	internal class WinFormsClipboard
	{
		public static void CopyTo(string text)
		{
			System.Windows.Forms.Clipboard.SetDataObject(text, true);
		}
	}
}