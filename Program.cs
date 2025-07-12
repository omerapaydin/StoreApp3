using Microsoft.EntityFrameworkCore;
using StoreApp3.Data.Abstract;
using StoreApp3.Data.Concrete.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sql_connection"));
});
builder.Services.AddScoped<IProductRepository,EfProductRepository>();



var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
