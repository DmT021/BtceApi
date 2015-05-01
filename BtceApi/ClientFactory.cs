namespace BtcE
{
    public class ClientFactory : IClientFactory
    {
        private readonly string _exchangeHost;

        public ClientFactory()
        {
            _exchangeHost = null;
        }

        public ClientFactory(string exchangeHost)
        {
            _exchangeHost = exchangeHost;
        }

        public IBtceApiClient CreateClient(string key, string secret)
        {
            return new BtceApiClientV3(key, secret, _exchangeHost);
        }

        public IBtceApiPublicClient CreatePublicClient()
        {
            return new BtceApiPublicClientV3();
        }
    }
}