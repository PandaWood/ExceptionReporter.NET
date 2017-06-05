using System;
namespace ExceptionReporting.Core
{
    class WinFormsClipboard
    {       
        public void CopyTo(string text)
        {
            System.Windows.Forms.Clipboard.SetDataObject(text, true);
        }
    }
}
