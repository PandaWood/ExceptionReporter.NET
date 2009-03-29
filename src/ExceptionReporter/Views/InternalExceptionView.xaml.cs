using System;
using System.Windows;

namespace ExceptionReporting.Wpf.Views
{
	/// <summary>
	/// Interaction logic for InternalExceptionView.xaml
	/// </summary>
	public partial class InternalExceptionView : Window, IInternalExceptionView
	{
		public InternalExceptionView()
		{
			InitializeComponent();
		}

		public void ShowException(string message, Exception exception)
		{
			throw new NotImplementedException();
		}
	}
}
