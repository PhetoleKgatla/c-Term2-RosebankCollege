using RaceDay.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure the named HttpClient for the API
builder.Services.AddHttpClient("RaceDayAPI", client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ApiSettings:BaseUrl"]!);
});

// Register custom API services for dependency injection
builder.Services.AddScoped<EventApiService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register general HttpClient factory support
builder.Services.AddHttpClient();

// Add Session services for tracking user authentication state and roles (Step 2)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable Session middleware (must be placed AFTER UseRouting and BEFORE UseAuthorization)
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();