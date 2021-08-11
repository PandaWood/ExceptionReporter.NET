using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ExceptionReporting.Mail;
using ExceptionReporting.Network;
using ExceptionReporting.Network.Events;
using ExceptionReporting.Plumbing;
using ExceptionReporting.Report;

// ReSharper disable once CheckNamespace
namespace ExceptionReporting.WPF.MvvM.ViewModel
{
	public class ExceptionReporterViewModel : ObservableObject
	{
		private RelayCommand _copyCommand;
		private RelayCommand _emailCommand;
		private RelayCommand _showDetailsCommand;

		public ExceptionReportInfo Info { get; }
		private readonly ReportGenerator _reportGenerator;
		private bool _showingDetails;

		public ExceptionReporterViewModel(ExceptionReportInfo info)
		{
			Info = info;
			_reportGenerator = new ReportGenerator(Info);
		}

		public ICommand CopyCommand
		{
			get { return _copyCommand ?? (_copyCommand = new RelayCommand(_ => Copy(), _ => true)); }
		}

		public ICommand EmailCommand
		{
			get { return _emailCommand ?? (_emailCommand = new RelayCommand(_ => SendEmail(), _ => true)); }
		}

		public ICommand ShowDetailsCommand
		{
			get { return _showDetailsCommand ?? (_showDetailsCommand = new RelayCommand(_ => ShowDetails(), _ => true)); }
		}

		public bool ShowingSummary => !ShowingDetails;

		public bool ShowingDetails
		{
			get => _showingDetails;
			set
			{
				_showingDetails = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(ShowingSummary));
			}
		}

		private void Copy()
		{
			Clipboard.SetText(_reportGenerator.Generate());
		}

		private void SendEmail()
		{
			var report = Info.IsSimpleMAPI() ? new EmailReporter(Info).Create() : _reportGenerator.Generate();
			var sendFactory = new SenderFactory(Info, new SilentSendEvent(), new NoScreenShot()).Get();
			sendFactory.Send(report);
		}

		private void ShowDetails()
		{
			ShowingDetails = !ShowingDetails;
		}
	}
}