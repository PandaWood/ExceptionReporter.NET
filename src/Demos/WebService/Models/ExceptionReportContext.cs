using Microsoft.EntityFrameworkCore;

namespace Demo.WebService.Models
{
	public class ExceptionReportContext : DbContext
	{
		public ExceptionReportContext(DbContextOptions<ExceptionReportContext> options)
			: base(options)
		{ }

		public DbSet<ExceptionReportItem> ExceptionReportItems { get; set; }
	}
}