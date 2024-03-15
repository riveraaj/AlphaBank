using Database.AlphaBank;
using Interfaces.Common;
using Interfaces.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Repository.Security;
using Service.Common;
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
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();

//Services Scoped
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserAuthenticatorService, UserAuthenticatorService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

//Add App Insights
builder.Services.AddApplicationInsightsTelemetry();

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