namespace ExceptionReporting.Core
{
	/// <summary>
	/// winforms and wpf have different means of taking screenshots, hence this abstraction
	/// </summary>
	public interface IScreenShooter
	{
		/// <summary />
		/// <returns></returns>
		string TakeScreenShot();
	}
}