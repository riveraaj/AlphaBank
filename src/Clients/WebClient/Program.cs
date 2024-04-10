using Database.AlphaBank;
using Interfaces.AnalyzeLoanOpportunities.Repositories;
using Interfaces.AnalyzeLoanOpportunities.Services;
using Interfaces.BankAccounts.Repositories;
using Interfaces.BankAccounts.Services;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;
using Interfaces.Security.Repositories;
using Interfaces.Security.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Repository.AnalyzeLoanOpportunities;
using Repository.BankAccounts;
using Repository.Common;
using Repository.Security;
using Service.AnalyzeLoanOpportunities;
using Service.BankAccounts;
using Service.Common;
using Service.Security;
using WebClient.Services;

var builder = WebApplication.CreateBuilder(args);

//Add cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/Security/Login";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.AccessDeniedPath = "/Home";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add database services
builder.Services.AddDbContext<AlphaBankDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories Scope
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITypeLoanRepository, TypeLoanRepository>();
builder.Services.AddScoped<IDeadlineRepository, DeadlineRepository>();
builder.Services.AddScoped<ITypeCurrencyRepository, TypeCurrencyRepository>();
builder.Services.AddScoped<IInterestRepository, InterestRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITypeAccountRepository, TypeAccountRepository>();
builder.Services.AddScoped<IOccupationRepository, OccupationRepository>();
builder.Services.AddScoped<ITypePhoneRepository, TypePhoneRepository>();
builder.Services.AddScoped<ITypeIdentificationRepository, TypeIdentificationRepository>();
builder.Services.AddScoped<INationalityRepository, NationalityRepository>();
builder.Services.AddScoped<IMaritalStatusRepository, MaritalStatusRepository>();
builder.Services.AddScoped<IInterestRepository, InterestRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();


//Services Scoped
builder.Services.AddScoped<IUserAuthenticatorService, UserAuthenticatorService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ITypeLoanService, TypeLoanService>();
builder.Services.AddScoped<IDeadlineService, DeadlineService>();
builder.Services.AddScoped<ITypeCurrencyService, TypeCurrencyService>();
builder.Services.AddScoped<IOccupationService, OccupationService>();
builder.Services.AddScoped<ITypePhoneService, TypePhoneService>();
builder.Services.AddScoped<ITypeIdentificationService, TypeIdentificationService>();
builder.Services.AddScoped<INationalityService, NationalityService>();
builder.Services.AddScoped<IMaritalStatusService, MaritalStatusService>();
builder.Services.AddScoped<IInterestService, InterestService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<AnalyzeLoanApplicationService>();
builder.Services.AddScoped<CommonService>();
builder.Services.AddScoped<BankAccountService>();
builder.Services.AddScoped<ITypeAccountService, TypeAccountService>();
builder.Services.AddScoped<BankAccountService>();

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
    pattern: "{controller=Home}/{action=Index}",
    defaults: new { controller = "Home", action = "Index" });

app.Run();