// ReSharper disable UnusedMember.Global
// ReSharper disable once CheckNamespace

namespace ExceptionReporting
{
	/// <summary>
	/// Output format of the report 
	/// </summary>
	/// <remarks>
	/// This needs to be in the 1st-level namespace to avoid forcing users to include 2 namespaces
	/// </remarks>
	public enum TemplateFormat
	{
		/// <summary> Plain text </summary>
		Text,
		/// <summary> HTML (5) </summary>
		Html,
		/// <summary> Standard markdown </summary>
		Markdown
	}
}