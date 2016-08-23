using Autofac.Extras.NLog;
using System;
using System.Diagnostics;

namespace AspNetWebApiStaThreading
{
    public class GoodService : IGoodService
    {
        private readonly ILogger _logger;

        public GoodService(ILogger logger)
        {
            _logger = logger;
            Debug.WriteLine("XXX: Service Instance created!!!");
        }

        public void DisplayName()
        {
            _logger.Info("Display name called !!!");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GoodService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Debug.WriteLine("XXX: Service instance disposed!!!");
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public interface IGoodService : IDisposable
    {
        void DisplayName();
    }
}
