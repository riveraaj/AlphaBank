using Database.AlphaBank;
using Interfaces.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Repository.Security;
using Service.Security;

var builder = WebApplication.CreateBuilder(args);

//Add cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.AccessDeniedPath = "/";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add database services
builder.Services.AddDbContext<AlphaBankDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories Scoped
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Services Scoped
builder.Services.AddScoped<IUserService>( provider => {
    var oUserRepository = provider.GetRequiredService<IUserRepository>();
    return new UserService(oUserRepository);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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