using System;
using System.Threading;

namespace ExceptionReporting.Core
{
    /// <summary>
    /// Base class for all classes wanting to implement <see cref="IDisposable"/>.
    /// </summary>
    /// <remarks>
    /// Base on an article by Davy Brion 
    /// <see href="http://davybrion.com/blog/2008/06/disposing-of-the-idisposable-implementation/"/>.
    /// </remarks>
    public abstract class Disposable : IDisposable
    {
        private int disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExceptionReporting.Core.Disposable"/> class.
        /// </summary>
        protected Disposable()
        {
            disposed = 0;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:ExceptionReporting.Core.Disposable"/> is disposed.
        /// </summary>
        /// <value><c>true</c> if is disposed; otherwise, <c>false</c>.</value>
        public bool IsDisposed
        {
            get { return disposed == 1; }
        }

        /// <summary>
        /// Releases all resource used by the <see cref="T:ExceptionReporting.Core.Disposable"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="T:ExceptionReporting.Core.Disposable"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="T:ExceptionReporting.Core.Disposable"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:ExceptionReporting.Core.Disposable"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:ExceptionReporting.Core.Disposable"/> was occupying.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the specified disposing.
        /// </summary>
        /// <returns>The dispose.</returns>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0)
            {
                if (disposing)
                {
                    DisposeManagedResources();
                }
                DisposeUnmanagedResources();
            }
        }

        /// <summary>
        /// Disposes the managed resources.
        /// </summary>
    	protected virtual void DisposeManagedResources() {}

        ///
        protected virtual void DisposeUnmanagedResources() {}

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="T:ExceptionReporting.Core.Disposable"/> is reclaimed by garbage collection.
        /// </summary>
        ~Disposable()
        {
            Dispose(false);
        }
    }
}