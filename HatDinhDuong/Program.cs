using HatDinhDuong.Applications.CloudinaryService;
using HatDinhDuong.Data;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
// add services to the DataContext(DbContext)
builder.Services.AddDbContext<DataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Việc AddDatabaseDeveloperPageExceptionFiltercung cấp thông tin lỗi hữu ích trong môi trường phát triển .
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddTransient<IPhotoService, PhotoService>();



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
