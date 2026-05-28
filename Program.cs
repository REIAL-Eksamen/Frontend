using Frontend.Components;
using Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<TokenProvider>();
builder.Services.AddTransient<AuthHeaderHandler>();

builder.Services.AddHttpClient<AuthClientService>(c =>
{
    c.BaseAddress = new Uri("http://nginx:4000/auth/");
});

builder.Services.AddHttpClient<ClassClientService>(c =>
    {
        c.BaseAddress = new Uri("http://nginx:4000/classes/");
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<BookingClientService>(c =>
    {
        c.BaseAddress = new Uri("http://nginx:4000/bookings/");
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<UserClientService>(c =>
    {
        c.BaseAddress = new Uri("http://nginx:4000/users/");
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();