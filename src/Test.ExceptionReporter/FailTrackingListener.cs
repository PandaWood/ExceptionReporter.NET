using System.Diagnostics;

namespace ExceptionReporter.Tests
{
    internal class FailTrackingListener : DefaultTraceListener
    {
        public override void Fail(string message, string detailMessage)
        {
            FailCalled = true;
        }

        public bool FailCalled { get; set; }
    }
}