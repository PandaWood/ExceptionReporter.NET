using System;
using System.Windows;

namespace ExceptionReporter.Views
{
	/// <summary>
	/// Interaction logic for InternalExceptionView.xaml
	/// </summary>
	public partial class InternalExceptionView : Window
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
