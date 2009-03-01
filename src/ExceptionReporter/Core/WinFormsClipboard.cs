using System.Windows.Forms;

namespace ExceptionReporter.Core
{
    /// <summary>
    /// WinFormsClipboard exists to shift a dependency from Windows.Forms assembly to IClipboard
    /// </summary>
    public class WinFormsClipboard: IClipboard
    {
        public void CopyTo(string text)
        {
            Clipboard.SetDataObject(text, true);
        }
    }
}