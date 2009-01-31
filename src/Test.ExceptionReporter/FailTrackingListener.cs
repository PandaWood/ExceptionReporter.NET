using System.Diagnostics;

namespace Test.ExceptionReporter
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