using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_DataAccess;
using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using PRN221_Repository.AccountRepo;
using PRN221_Repository.NewsRepo;
using PRN221_Repository.RoleRepo;
using PRN221_Repository.ServiceRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<Prn221Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddAuthentication("CookiesPRN221").AddCookie("CookiesPRN221", options =>
{
    options.LoginPath = "/Access/Login";
    options.LogoutPath = "/Access/Logout";
    options.AccessDeniedPath = "/Access/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
}).
AddCookie().
AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientID").Value;
    options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
    options.CallbackPath = "/signin-google";
    options.Scope.Add("profile");
})
.AddFacebook(options =>
 {
     options.AppId = builder.Configuration["Facebook:AppId"];
     options.AppSecret = builder.Configuration["Facebook:AppSecret"];
     options.SaveTokens = true;
     options.CallbackPath = builder.Configuration["Facebook:CallbackPath"];

     // Thêm quyền và trường để yêu cầu từ Facebook
     options.Scope.Add("public_profile");
     options.Fields.Add("id");       // Facebook ID
     options.Fields.Add("name");     // Name
     options.Fields.Add("email");    // Email
     options.Fields.Add("picture");  // Avatar (Profile Picture)
 });



builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IAuthenService, AuthenService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountDAO>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<Account>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<RoleDAO>();

builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<NewsDAO>();
builder.Services.AddScoped<NewsService>();
builder.Services.AddScoped<NewsRepository>();

builder.Services.AddScoped<IRequirementService, RequirementService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ServiceDAO>();
builder.Services.AddScoped<Service>();

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<FirebaseConfig>();

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache(); // For storing session data in memory
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
