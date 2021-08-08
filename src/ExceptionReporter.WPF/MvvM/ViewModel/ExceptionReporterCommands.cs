using System.Windows.Input;

// ReSharper disable CheckNamespace
namespace ExceptionReporting.WPF.MvvM.ViewModel
{
	public class ExceptionReporterCommands
	{
		public static readonly RoutedUICommand EmailCommand;
		public static readonly RoutedUICommand ShowDetailsCommand;

		static ExceptionReporterCommands()
		{
			EmailCommand = new RoutedUICommand("Send email", "EmailReport", typeof(ExceptionReporterCommands));
			ShowDetailsCommand = new RoutedUICommand("Show details", "ShowDetails", typeof(ExceptionReporterCommands));
		}
	}
}