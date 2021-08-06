using ExceptionReporting.Templates;

namespace ExceptionReporting.Mail
{
	internal class EmailReporter
	{
		private readonly ExceptionReportInfo _info;

		public EmailReporter(ExceptionReportInfo info)
		{
			_info = info;
		}

		public string Create()
		{
			var template = new TemplateRenderer(new EmailIntroModel
			{
				ScreenshotTaken = _info.TakeScreenshot
			});
			var emailIntro = template.RenderPreset();
			var report = new ReportGenerator(_info).Generate();

			return emailIntro + report;
		}
	}
}