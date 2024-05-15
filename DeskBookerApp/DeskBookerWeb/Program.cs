using DeskBookerApp.Interfaces;
using DeskBookerApp.Services;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddTransient<IDeskRepository, DeskRepository>();
//builder.Services.AddTransient<IDeskBookingRepository, DeskBookingRepository>();
builder.Services.AddTransient<IDeskBookingService, DeskBookingService>();

var connectionString = "DataSource=:memory";
var connection = new SqliteConnection(connectionString);
connection.Open();

//builder.Services.AddDbContext<DeskBookerContext>(options => options.UseSqlite);

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
