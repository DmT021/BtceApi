namespace BtcE
{
    public interface IClientFactory
    {
        IBtceApiClient CreateClient(string key, string secret);

        IBtceApiPublicClient CreatePublicClient();
    }
}