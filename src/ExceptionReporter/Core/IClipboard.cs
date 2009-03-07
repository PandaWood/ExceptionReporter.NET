namespace ExceptionReporter.Core
{
    ///<summary>
    /// interface to abstract the concept of a clipboard - required because of the differences between WPF and WinForms
    ///</summary>
    public interface IClipboard
    {
		/// <summary> copy text to clipboard </summary>
		/// <param name="text">plain-text string</param>
        void CopyTo(string text);
    }
}