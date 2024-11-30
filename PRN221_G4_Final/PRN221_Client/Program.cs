using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Client.ViewModel;
using PRN221_DataAccess;
using PRN221_DataAccess.DAOs;
using PRN221_Models.DTO;
using PRN221_Models.Models;
using PRN221_Repository.AccountRepo;
using PRN221_Repository.BookingRepo;
using PRN221_Repository.CategoryPostRepo;
using PRN221_Repository.CommentRepo;
using PRN221_Repository.FollowRepo;
using PRN221_Repository.LikePostRepo;
using PRN221_Repository.NewsRepo;
using PRN221_Repository.PostImageRepo;
using PRN221_Repository.PostsRepo;
using PRN221_Repository.RateRepo;
using PRN221_Repository.RoleRepo;
using PRN221_Repository.ServiceRepo;
using PRN221_Repository.SharePostRepo;
using PRN221_Repository.ViewRepo;
using Newtonsoft.Json;
using PRN221_Repository.ConversRepo;
using PRN221_Client.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSignalR();


builder.Services.AddDbContext<PRN221_DataAccess.Prn221Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    // Sử dụng Cookie làm phương thức xác thực chính
    options.DefaultScheme = "CookiesPRN221";
})
.AddCookie("CookiesPRN221", options =>
{
    options.LoginPath = "/Access/Login";
    options.LogoutPath = "/Access/Logout";
    options.Cookie.Path = "/";
    options.AccessDeniedPath = "/Access/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Access/Login";
    options.LogoutPath = "/Access/Logout";
    options.AccessDeniedPath = "/Access/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
})
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientID").Value;
    options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
    options.CallbackPath = "/signin-google";
    options.Scope.Add("profile");
})
.AddFacebook(FacebookDefaults.AuthenticationScheme, options =>
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

     // Thêm sự kiện bắt trường hợp thoát đăng nhập Fb
     options.Events.OnRemoteFailure = context =>
     {
         context.Response.Redirect("/Access/Login");
         context.HandleResponse();

         return Task.FromResult(0);
     };
 });

builder.Services.Configure<Sender>(builder.Configuration.GetSection("Sender"));

// Thêm Scoped service cho Sender
builder.Services.AddScoped<Sender>(sp =>
{
    var senderOptions = sp.GetRequiredService<IOptions<Sender>>().Value;
    return senderOptions;
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

builder.Services.AddScoped<ICategoryNewsRepository, CategoryNewsRepository>();
builder.Services.AddScoped<CategoryNewsDAO>();
builder.Services.AddScoped<CategoryNewsRepository>();

builder.Services.AddScoped<IRequirementService, RequirementService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ServiceDAO>();
builder.Services.AddScoped<Service>();

builder.Services.AddScoped<IBookingService, PRN221_BusinessLogic.Service.BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<BookingDAO>();

builder.Services.AddScoped<IRateService, RateService>();
builder.Services.AddScoped<IRateRepository, RateRepository>();
builder.Services.AddScoped<RateDAO>();

builder.Services.AddScoped<IConversService, ConversService>();
builder.Services.AddScoped<IConversRepository, ConversRepository>();
builder.Services.AddScoped<ConversationDAO>();

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<FirebaseConfig>();


builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<PostDAO>();
builder.Services.AddScoped<PostDTO>();

builder.Services.AddScoped<IPostImageService, PostImageService>();
builder.Services.AddScoped<IPostImageRepository, PostImageRepository>();
builder.Services.AddScoped<PostImageDAO>();

builder.Services.AddScoped<ILikePostRepository, LikePostRepository>();
builder.Services.AddScoped<LikePostRepository>();
builder.Services.AddScoped<LikePostDAO>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<CommentPostDAO>();

builder.Services.AddScoped<ISharePostRepository, SharePostRepository>();
builder.Services.AddScoped<SharePostRepository>();
builder.Services.AddScoped<SharePostDAO>();

builder.Services.AddScoped<IViewService, ViewService>();
builder.Services.AddScoped<IViewRepository, ViewRepository>();
builder.Services.AddScoped<ViewDAO>();

builder.Services.AddScoped<ICategoryPostService, CategoryPostService>();
builder.Services.AddScoped<ICategoryPostRepository, CategoryPostRepository>();
builder.Services.AddScoped<CategoryPostDAO>();

builder.Services.AddScoped<PostViewModel>();

builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();
builder.Services.AddScoped<FollowDAO>();

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

app.MapHub<ChatHub>("/chatHub");
//app.MapHub<SignalRServer>("/SignalRServer");

app.Run();
