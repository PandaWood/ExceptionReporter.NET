using System;
using System.Threading;

#if DEBUG
using System.Diagnostics;
#endif


namespace ExceptionReporting.Core
{
    public abstract class Disposable : IDisposable
    {

        private int disposed;
        private bool failOnFinalize = true;

        protected Disposable()
        {
            disposed = 0;
        }

        public bool IsDisposed
        {
            get
            {
                return disposed == 1;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0)
            {
                try
                {
                    if (disposing)
                    {
                        DisposeManagedResources();
                    }
                    DisposeUnmanagedResources();
                }
                catch (Exception)
                {
                    //Exception already occurred dont bother with the Assert.Fail in the finalize
                    failOnFinalize = false;
                    throw;
                }
            }
        }

        /// <summary>
        /// Helper method so subclasses can easily throw if disposed
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        protected virtual void DisposeManagedResources()
        {
        }


        protected virtual void DisposeUnmanagedResources()
        {
        }

        ~Disposable()
        {
            Dispose(false);
#if DEBUG
            if (failOnFinalize)
            {
                Debug.Fail(string.Format("Not disposed: {0}", GetType().Name));
            }
#endif
        }

    }
}