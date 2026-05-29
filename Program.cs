using Frontend.Components;
using Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

//tokenprovider holder styr på jwt token på tværs af sessions. 
//authheaderhandler sørger for at token vliver sendt med i alle kald der krvæver login.
builder.Services.AddSingleton<TokenProvider>();
builder.Services.AddTransient<AuthHeaderHandler>();

//auth er det eneste endpoint der ikke kræver token, da man jo ikke er logget ind endnu. 
builder.Services.AddHttpClient<AuthClientService>(c =>
{
    c.BaseAddress = new Uri("http://10.0.1.9:8080/auth");
});
//resten af kaldene går gennem nginx og kræver gyldigt jwt token. 
builder.Services.AddHttpClient<ClassClientService>(c =>
    {
        c.BaseAddress = new Uri("http://10.0.1.6:8080/api/classes");
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<BookingClientService>(c =>
    {
        c.BaseAddress = new Uri("http://10.0.1.7:8080/api/bookings");
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<UserClientService>(c =>
    {
        c.BaseAddress = new Uri("http://10.0.1.10:8080/api/users");
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
//sender bruger til /not-found i stedet for en grim fejlside ved 404 og lignende. 
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();