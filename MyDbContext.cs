using Microsoft.EntityFrameworkCore;
using yeaa2.Entities;

namespace yeaa2;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ImperialMeasurement> ImperialMeasurements { get; set; }
    public DbSet<MetricMeasurement> MetricMeasurements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=bmi_calculator;User Id=root;Password=7824;");
    }
}