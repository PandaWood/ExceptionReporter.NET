using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace ExceptionReporting.WPF.MvvM.ViewModel
{
	public class MainViewModel : INotifyPropertyChanged
	{
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

		// a quick/simple implementation of observable object - we only need it once
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}