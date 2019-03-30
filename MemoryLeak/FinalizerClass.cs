using System;

namespace MemoryLeak
{
    class FinalizerClass : IDisposable
    {

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.            
            }

            // Free any unmanaged objects here.
            disposed = true;
        }

        ~FinalizerClass()
        {
            Console.WriteLine("Finalizer is running!");
            Dispose(false);
        }
    }
}
