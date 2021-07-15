using System;

namespace Bight.Tensor
{
    public partial class Tensor<T> : IDisposable
    {
        protected bool _disposed;
        protected IntPtr _handle;


        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            //first handle managed, they might use the unmanaged resources.
            if (disposing)
                // dispose managed state (managed objects).
                DisposeManagedResources();

            // free unmanaged memory
            if (_handle != IntPtr.Zero)
            {
                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                DisposeUnmanagedResources(_handle);
                _handle = IntPtr.Zero;
            }

            // Note disposing has been done.
            _disposed = true;
        }

        /// <summary>
        ///     Dispose any managed resources.
        /// </summary>
        /// <remarks>Equivalent to what you would perform inside <see cref="Dispose()" /></remarks>
        protected virtual void DisposeManagedResources()
        {
        }

        protected virtual void DisposeUnmanagedResources(IntPtr handle)
        {
        }
    }
}