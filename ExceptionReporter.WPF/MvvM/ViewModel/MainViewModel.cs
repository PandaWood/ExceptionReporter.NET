using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExceptionReporting;

namespace ExceptionReporter.WPF.MvvM.ViewModel
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private Exception _exception;
		private ExceptionReportInfo _info;

		public MainViewModel(ExceptionReportInfo info)
		{
			_info = info;
		}

		public ExceptionReportInfo Info
		{
			get => _info;
			set
			{
				_info = value;
				NotifyPropertyChanged();
			}
		}

		public Exception ExceptionSource
		{
			get => _exception;
			set
			{
				_exception = value;
				NotifyPropertyChanged();
			}
		}

		// a quick/simple implementation of observable object - we only need it once
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}