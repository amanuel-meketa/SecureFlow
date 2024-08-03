using Logto.Authentication.extensions;
using Logto.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
static void CheckSameSite(HttpContext httpContext, CookieOptions options)
{
    if (options.SameSite == SameSiteMode.None && options.Secure == false)
    {
        options.SameSite = SameSiteMode.Unspecified;
    }
}

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
    options.OnAppendCookie = cookieContext => CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
    options.OnDeleteCookie = cookieContext => CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
});
// Use the extension method to add Logto authentication services
builder.Services.AddLogtoAuthentication(options =>
{
    options.Endpoint = builder.Configuration["Logto:Endpoint"]!;
    options.AppId = builder.Configuration["Logto:AppId"]!;
    options.AppSecret = builder.Configuration["Logto:AppSecret"];
    options.Scopes = new string[] {
        LogtoParameters.Scopes.Email,
        LogtoParameters.Scopes.Phone,
        LogtoParameters.Scopes.CustomData,
        LogtoParameters.Scopes.Identities
    };
    options.Resource = builder.Configuration["Logto:Resource"];
    options.GetClaimsFromUserInfoEndpoint = true;
    options.RequireHttpsMetadata = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
