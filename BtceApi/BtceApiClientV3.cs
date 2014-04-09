using System;

namespace BtcE
{
    public class BtceApiClientV3 : BtceApiPublicClientV3, IBtceApiClient, IDisposable
    {
        private readonly object _disposeLock = new object();
        private BtceApi _apiv2;
        private bool _disposed;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="key">The account key.</param>
        /// <param name="secret">The account secret.</param>
        /// <param name="exchangeHost">The exchange host (default: "https://btc-e.com/";).</param>
        public BtceApiClientV3(string key, string secret, string exchangeHost = null)
        {
            _apiv2 = new BtceApi(key, secret, exchangeHost);
        }

        public CancelOrderAnswer CancelOrder(int orderId)
        {
            return _apiv2.CancelOrder(orderId);
        }

        public OrderList GetActiveOrders(BtcePair? pair = null)
        {
            return _apiv2.GetActiveOrders(pair);
        }

        public UserInfo GetInfo()
        {
            return _apiv2.GetInfo();
        }

        public TradeHistory GetTradeHistory(
            int? from = null,
            int? count = null,
            int? fromId = null,
            int? endId = null,
            bool? orderAsc = null,
            DateTime? since = null,
            DateTime? end = null,
            BtcePair? pair = null)
        {
            return _apiv2.GetTradeHistory(from, count, fromId, endId, orderAsc, since, end, pair);
        }

        public TransHistory GetTransHistory(
            int? from = null,
            int? count = null,
            int? fromId = null,
            int? endId = null,
            bool? orderAsc = null,
            DateTime? since = null,
            DateTime? end = null)
        {
            return _apiv2.GetTransHistory(from, count, fromId, endId, orderAsc, since, end);
        }

        public TradeAnswer Trade(BtcePair pair, TradeType type, decimal rate, decimal amount)
        {
            return _apiv2.Trade(pair, type, rate, amount);
        }

        /// <summary>
        ///     Clean up
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="BtceApi" /> class.
        /// </summary>
        ~BtceApiClientV3()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Clean up
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            // make thread safe
            lock (_disposeLock)
            {
                // conform to reference example: http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx
                if (!_disposed)
                {
                    if (disposing)
                    {
                        // null check
                        if (_apiv2 != null)
                        {
                            // invoke disposing
                            _apiv2.Dispose();
                            _apiv2 = null; // null just to br safe
                        }
                        _disposed = true;
                    }
                }
            }
        }
    }
}