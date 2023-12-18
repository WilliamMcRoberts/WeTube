using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeTube.Client.Pages;
using WeTube.Components;
using WeTube.Components.Account;
using WeTube.Data;
using WeTube.Endpoints.api;
using WeTube.Endpoints.Htmx;
using WeTube.Models;
using WeTube.Processors;
using WeTube.Repositories;
using WeTube.DataAccess;
using Microsoft.AspNetCore.StaticFiles;
using WeTube;
using Microsoft.Extensions.FileProviders;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
{
    policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
}));

builder.Services.Configure<FormOptions>(options =>
{

    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});


builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddSingleton<IRender, Render>();
builder.Services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
builder.Services.AddSingleton<ISqlConnection, SqlConnection>();
builder.Services.AddScoped<IVideoFileProcessor, VideoFileProcessor>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IVideoUsersRepository, VideoUsersRepository>();

// Default
builder.Services.AddRazorComponents()
.AddInteractiveServerComponents().AddHubOptions(options =>
{
    options.MaximumReceiveMessageSize = 4000000000;
})
.AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseWhen(context => context.Request.Path.StartsWithSegments("/uploads"), appBuilder =>
{
    appBuilder.UseCors("CorsPolicy");

    var provider = new FileExtensionContentTypeProvider();

    provider.Mappings[".m3u8"] = "application/x-mpegURL";
    provider.Mappings[".ts"] = "video/MP2T";

    appBuilder.UseStaticFiles(new StaticFileOptions
    {
        ContentTypeProvider = provider,
        FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "uploads")),
        RequestPath = "/uploads"
    });
});

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

// endpoints
app.ConfigureApplicationUserApi();
app.ConfigureApplicationUserHtmxApi();

app.Run();
