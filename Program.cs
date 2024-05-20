// TS-00:11:19-00:56:37 
using GameStore.Frontend.Clients;
using GameStore.Frontend.Components;

// TS-00:11:19-00:56:37 register all the services needed by application
// TS-00:11:19-00:56:37 it uses the dependency injection pattern
var builder = WebApplication.CreateBuilder(args);

// TS-00:11:19-00:56:37 one of the services used here is RazorComponents
// TS-03:55:36-04:20:39 Upgrading from static SSR to Interactive SSR with the .AddInteractiveServerComponents
// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// TS-03:55:36-04:20:39 API Endpoint Base URL
// TS-04:20:39-04:26:59 Replacing hardcoded 
// TS-04:20:39-04:26:59 base url endpoint with configuration which is stored in appsettings.json along with null check
var gameStoreApiUrl = builder.Configuration["GameStoreApiUrl"] ??
    throw new Exception("GameStoreApiUrl is not set");

// TS-03:55:36-04:20:39 API Endpoint Base URL Initializing the Clients to interact with the backend
builder.Services.AddHttpClient<GamesClient>(
    client => client.BaseAddress = new Uri(gameStoreApiUrl));
builder.Services.AddHttpClient<GenresClient>(
    client => client.BaseAddress = new Uri(gameStoreApiUrl));

// TS-02:32:35-02:53:03 Registering the dependency injection for GamesClient with Singleton Lifecycle
// TS-03:55:36-04:20:39 builder.Services.AddSingleton<GamesClient>();

// TS-02:32:35-02:53:03 Registering the dependency injection for GenresClient with Singleton Lifecycle
// TS-03:55:36-04:20:39 builder.Services.AddSingleton<GenresClient>();

// TS-00:11:19-00:56:37 webapplication is the host.
// TS-00:11:19-00:56:37 it encapsulates all of the application resources
// TS-00:11:19-00:56:37 middleware,dependency injection,etc
// TS-00:11:19-00:56:37 build generates the instance of webapplication
// TS-00:11:19-00:56:37 it sets up castra web server for . appsettings, environment variables, 
// TS-00:11:19-00:56:37 login output login providers everything is invoked
var app = builder.Build();

// TS-00:11:19-00:56:37 from here till app.run configuration of pipeline, how to handle different requests
// TS-00:11:19-00:56:37 middleware are services in app that handles cross coding concerns in application
// Configure the HTTP request pipeline.

// TS-00:11:19-00:56:37 this logic here throws user to error page if the environment is not development environment
if (!app.Environment.IsDevelopment())
{
    // TS-00:11:19-00:56:37 
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // TS-00:11:19-00:56:37 ensures only HTTPS access to this application
    app.UseHsts();
}

// TS-00:11:19-00:56:37 ensures only HTTPS access to this application
// TS-00:11:19-00:56:37 comment this as per the video to avoid the warning in terminal which keeps checking for usage of https
// TS-00:11:19-00:56:37 app.UseHttpsRedirection();

// TS-00:11:19-00:56:37 needed to serve static files in application like html css images javascript
app.UseStaticFiles();

// TS-00:11:19-00:56:37 protecting website against forgery and threats
// TS-00:11:19-00:56:37 when using forms this is an important aspect
app.UseAntiforgery();

// TS-00:11:19-00:56:37 maprazorcomponent configures middleware to discover razor components in application
// TS-00:11:19-00:56:37 App component in Components folder is the root Component and from there we are going to be discovering all of the other components for our application
// TS-03:55:36-04:20:39 Upgrading from static SSR to Interactive SSR with the .AddInteractiveServerRenderMode
app.MapRazorComponents<App>()
.AddInteractiveServerRenderMode();

// TS-00:11:19-00:56:37 start the host of application so that its ready to receive requests.
app.Run();
