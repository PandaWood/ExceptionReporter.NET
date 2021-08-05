using System.Windows.Input;

// ReSharper disable CheckNamespace
namespace ExceptionReporting.WPF.MvvM.ViewModel
{
	public class ExceptionReporterCommands
	{
		public static readonly RoutedUICommand EmailCommand;

		static ExceptionReporterCommands()
		{
			EmailCommand = new RoutedUICommand("Send email", "EmailReport", typeof(ExceptionReporterCommands));
		}
	}
}