using System;

namespace ExceptionReporting.Wpf.Views
{
	/// <summary>
	/// Interaction logic for InternalExceptionView.xaml
	/// </summary>
// ReSharper disable UnusedMember.Global
	public partial class InternalExceptionView : IInternalExceptionView
	{
		///<summary>  WPF internal exception view </summary>
		public InternalExceptionView()
		{
			InitializeComponent();
		}

		///<summary> show the internal exception </summary>
		public void ShowException(string message, Exception exception)
		{
			Show();
		}
	}
// ReSharper restore UnusedMember.Global
}
