using Frontend.Components;
using Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); ;

builder.Services.AddSingleton<TokenProvider>();
builder.Services.AddSingleton<AuthHeaderHandler>();

builder.Services.AddHttpClient<AuthClientService>(c =>
{
    c.BaseAddress = new Uri("http://authservice:8080/");
});

builder.Services.AddHttpClient<ClassClientService>(c =>
{
    c.BaseAddress = new Uri("http://classservice:8080/");
});

builder.Services.AddHttpClient<BookingClientService>(c =>
{
    c.BaseAddress = new Uri("http://bookingservice:8080/");
});

builder.Services.AddHttpClient<UserClientService>(c =>
    {
        c.BaseAddress = new Uri("http://userservice:8080/");
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
