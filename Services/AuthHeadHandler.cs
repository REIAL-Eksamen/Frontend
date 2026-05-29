using System.Net.Http.Headers;

//sørger automatisk for at jwt token bliver sendt med i alle http kald der kræver login. 
//slipper for at tænke på det manuelt i hvert service. 
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
//hvis vi har et token sættes det på requestens authorization header. 
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}