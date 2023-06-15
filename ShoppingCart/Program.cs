using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoppingCart.Data;
using ShoppingCart.Repositories;
using Microsoft.AspNetCore.Identity;
using ShoppingCart.Utility.DbInitializer;
using Microsoft.AspNetCore.Identity.UI.Services;
using ShoppingCart.Utility;
using Stripe;
// using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommers"));
});

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("PaymentSettings"));

//options => options.SignIn.RequireConfirmedAccount = true

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializerRepo>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    option.LoginPath = $"/Identity/Account/Login";
    option.LogoutPath = $"/Identity/Account/Logut";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapRazorPages();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

dataSedding();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("PaymentSettings").GetSection("SecretKey").Get<string>();


app.UseAuthentication();;

app.UseAuthorization();


//app.MapControllerRoute(
//            name: "default",
//            pattern: "{area:Customer}/{controller=Home}/{action=Index}/{id?}"
//          );

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");



app.Run();

void dataSedding()
{
    using (var scope = app.Services.CreateScope())
    {
        var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DbInitializer.Initializer();
    }
}
