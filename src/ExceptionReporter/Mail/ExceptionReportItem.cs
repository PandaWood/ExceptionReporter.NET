using System.Runtime.Serialization;
#pragma warning disable 1591

namespace ExceptionReporting.Mail
{
	/// <summary>
	/// JSON packet that is sent to the configured WebService
	/// </summary>
	[DataContract]
	public class ExceptionReportItem
	{
		[DataMember]
		public string AppName { get; set; }
		[DataMember]
		public string AppVersion { get; set; }
		[DataMember]
		public string ExceptionMessage { get; set; }
		[DataMember]
		public string ExceptionReport { get; set; }
	}
}