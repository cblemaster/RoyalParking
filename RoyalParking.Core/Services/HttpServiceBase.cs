namespace RoyalParking.Core.Services;

public class HttpServiceBase
{
    internal readonly HttpClient _client;
    private const string BASE_URI = "https://localhost:7230/";

    public HttpServiceBase() => _client = new HttpClient
    {
        BaseAddress = new Uri(BASE_URI)
    };
}
