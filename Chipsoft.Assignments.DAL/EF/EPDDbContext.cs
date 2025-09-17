using Microsoft.EntityFrameworkCore;
using Chipsoft.Assignments.Domain;
using Microsoft.Extensions.Logging;

namespace Chipsoft.Assignments.DAL.EF;

public class EPDDbContext(DbContextOptions<EPDDbContext> options) : DbContext(options)
{
    // The following configures EF to create a Sqlite database file in the
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=epd.db").UseLoggerFactory(LoggerFactory.Create(_ => {}));
    
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Physician> Physicians { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Physician)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Appointment>().Property("PatientId");
        modelBuilder.Entity<Appointment>().Property("PhysicianId");
        modelBuilder.Entity<Appointment>().HasKey("PatientId", "PhysicianId", "AppointmentAt");
    }
}

