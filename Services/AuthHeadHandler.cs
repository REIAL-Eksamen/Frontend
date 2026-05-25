using System.Net.Http.Headers;

namespace Frontend.Services;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly TokenProvider _tokenProvider;

    public AuthHeaderHandler(TokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = _tokenProvider.Token;

        Console.WriteLine("HANDLER TOKEN: " + token);
        Console.WriteLine("REQUEST: " + request.RequestUri);

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}