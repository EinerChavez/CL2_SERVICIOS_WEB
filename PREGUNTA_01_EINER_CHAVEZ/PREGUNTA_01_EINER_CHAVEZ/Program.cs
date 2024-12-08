using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PREGUNTA_01_EINER_CHAVEZ.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PREGUNTA_01_EINER_CHAVEZContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PREGUNTA_01_EINER_CHAVEZContext") ?? throw new InvalidOperationException("Connection string 'PREGUNTA_01_EINER_CHAVEZContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Videotecas}/{action=Index}/{id?}");
app.Run();
