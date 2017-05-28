using System;
namespace ExceptionReporting.Core
{
    /// <summary>
    /// Window forms clipboard.
    /// </summary>
    public class WinFormsClipboard
    {
        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="text">Text.</param>
        public void CopyTo(string text)
        {
            System.Windows.Forms.Clipboard.SetDataObject(text, true);
        }
    }
}
