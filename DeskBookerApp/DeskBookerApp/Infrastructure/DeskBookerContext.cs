using DeskBookerApp.Domain.Desk;
using DeskBookerApp.Domain.DeskBooking;
using DeskBookerApp.Utils;
using Microsoft.EntityFrameworkCore;

namespace DeskBookerApp.Infrastructure;

public class DeskBookerContext : DbContext 
{
    public DeskBookerContext(DbContextOptions<DeskBookerContext> options) : base(options)
    {
    }

    public DbSet<DeskBooking> DeskBookings { get; set; }
    public DbSet<Desk> Desks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Desk>().HasData(
            new Desk { Id = 1, Description = $"{Constants.DeskName} 1" },
            new Desk { Id = 1, Description = $"{Constants.DeskName} 2" }
        );
    }

}