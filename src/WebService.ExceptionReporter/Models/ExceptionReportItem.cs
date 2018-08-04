namespace WebService.ExceptionReporter.Models
{
	public class ExceptionReportItem
	{
		public long ID { get; set; }
		public string AppName { get; set; }
		public string AppVersion { get; set; }
		public string ExceptionReport { get; set; }
		public string ExceptionMessage { get; set; }
	}
}