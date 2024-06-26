﻿using DeskBookerApp.Infrastructure;
using DeskBookerApp.Interfaces;
using DeskBookerApp.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DeskBookerWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            var connectionString = "DataSource=:memory:";
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            services.AddDbContext<DeskBookerContext>(options =>
                options.UseSqlite(connection)
            );
            EnsureDatabaseExists(connection);

            //services.AddTransient<IDeskRepository, DeskRepository>();
            //services.AddTransient<IDeskBookingRepository, DeskBookingRepository>();
            services.AddTransient<IDeskBookingService, DeskBookingService>();
        }

        private static void EnsureDatabaseExists(SqliteConnection connection)
        {
            var builder = new DbContextOptionsBuilder<DeskBookerContext>();
            builder.UseSqlite(connection);

            using var context = new DeskBookerContext(builder.Options);
            context.Database.EnsureCreated();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
