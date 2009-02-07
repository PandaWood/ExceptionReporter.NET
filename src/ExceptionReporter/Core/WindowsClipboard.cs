using System.Windows.Forms;

namespace ExceptionReporting.Core
{
	/// <summary>
	/// WindowsClipboard exists to shift a dependency from Windows.Forms assembly to IClipboard
	/// </summary>
	public class WindowsClipboard: IClipboard
	{
		public void CopyTo(string text)
		{
			Clipboard.SetDataObject(text, true);
		}
	}
}