using System;

namespace ExceptionReporting.WinForms
{
	internal static class WinFormsClipboard
	{
		/// <summary>
		/// copy text to cipboard
		/// </summary>
		/// <param name="text"></param>
		/// <returns>whether the clipboard operation succeeded</returns>
		public static bool CopyTo(string text)
		{
			try
			{
				System.Windows.Forms.Clipboard.SetDataObject(text, true);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}