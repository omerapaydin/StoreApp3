using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreApp3.Data.Abstract;
using StoreApp3.Data.Concrete.EfCore;
using StoreApp3.Entity;
using StoreApp3.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<IdentityContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sql_connection"));
});

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
new SmtpEmailSender(
    builder.Configuration["EmailSender:Host"],
    builder.Configuration.GetValue<int>("EmailSender:Port"),
    builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
    builder.Configuration["EmailSender:Username"],
    builder.Configuration["EmailSender:Password"]

));

builder.Services.Configure<IdentityOptions>(options=>{
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;  
    options.User.RequireUniqueEmail = true;  
    options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>{
    options.LoginPath = "/Account/Login"; 
   
});

builder.Services.AddScoped<IProductRepository,EfProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddScoped<IOrderRepository,EfOrderRepository>();
builder.Services.AddScoped<Cart>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


var app = builder.Build();
app.UseSession();
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();
