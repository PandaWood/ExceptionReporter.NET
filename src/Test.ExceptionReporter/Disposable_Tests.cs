using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ExceptionReporter.Core;
using NUnit.Framework;

namespace ExceptionReporter.Tests
{
    [TestFixture]
    public class DisposableTests
    {
        [SetUp]
        public void SetUp()
        {
            DisposeUnmanagedResourcesCalled = false;
            DisposeManagedResourcesCalled = false;
        }

        public static bool DisposeUnmanagedResourcesCalled { get; set; }
        public static bool DisposeManagedResourcesCalled { get; set; }

        public class MockDisposable : Disposable
        {
            protected override void DisposeManagedResources()
            {
                DisposeManagedResourcesCalled = true;
            }

            protected override void DisposeUnmanagedResources()
            {
                DisposeUnmanagedResourcesCalled = true;
            }
        }

        [Test]
        public void FailFromFinalize()
        {
            var list = new List<TraceListener>();
            foreach (TraceListener listener in Debug.Listeners)
            {
                list.Add(listener);
            }

            Trace.Listeners.Clear();
            var myListener = new FailTrackingListener();
            Trace.Listeners.Add(myListener);

            try
            {
                new MockDisposable();
                GC.Collect();
                //Wait for finalize
                Thread.Sleep(1000);
            }
            finally
            {
                foreach (var listener in list)
                {
                    Trace.Listeners.Add(listener);
                }
            }

            Assert.IsFalse(DisposeManagedResourcesCalled);
            Assert.IsTrue(DisposeUnmanagedResourcesCalled);
        }

        [Test]
        public void TestDispose()
        {
            var disposable = new MockDisposable();
            disposable.Dispose();
            Assert.IsTrue(disposable.IsDisposed);
            Assert.IsTrue(DisposeManagedResourcesCalled);
            Assert.IsTrue(DisposeUnmanagedResourcesCalled);
        }
    }
}