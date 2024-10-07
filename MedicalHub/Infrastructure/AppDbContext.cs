

using MedicalHub.Entities.Consultation;
using MedicalHub.Entities.Doctors;

using MedicalHub.Entities.Patients;
using MedicalHub.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace MedicalHub.Infrastructure;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        try
        {
            var dbCreation = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (dbCreation == null) return;
            if (!dbCreation.CanConnect()) dbCreation.Create();
            if (!dbCreation.HasTables()) dbCreation.CreateTables();

        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }
    public DbSet<UserAdmin> UserAdmin { get; set; }
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<Consultation> Consultations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAdmin>(table =>
        {
            table.HasKey(u => u.Id);

            table.Property(name => name.nameUser).IsRequired().HasMaxLength(20);
            table.Property(password => password.passwordUser).IsRequired();
            
            table.Property(email => email.emailUser).IsRequired();
            table.HasIndex(email => email.emailUser).IsUnique();
        });
        
        modelBuilder.Entity<Patient>(table =>
        {
            table.HasKey(p => p.Id);
            table.Property(p => p.IdentificationNumber)
                .IsRequired()
                .HasMaxLength(10);
            table.Property(name => name.FullName).IsRequired();
            table.HasIndex(p => p.IdentificationNumber).IsUnique();
            table.Property(p => p.Email).IsRequired();
            table.Property(p => p.DateOfBirth).IsRequired();
        });

        modelBuilder.Entity<Doctor>(table =>
        {
            table.HasKey(d => d.Id);
            table.Property(d => d.IdentificationNumber)
                .IsRequired()
                .HasMaxLength(10);
            table.Property(d => d.FullName).IsRequired();
            table.HasIndex(d => d.IdentificationNumber).IsUnique();
            table.Property(d => d.Email).IsRequired();
        });

        // Relations table
        modelBuilder.Entity<Consultation>(table =>
        {
            table.HasKey(c => c.Id);
            table.Property(c => c.ConsultationsDate).IsRequired();
            table.Property(c => c.ReasonForConsultations)
                .IsRequired()
                .HasMaxLength(255);
            table.Property(c => c.Diagnosis)
                .HasMaxLength(255);
            table.Property(c => c.Status).HasDefaultValue(Status.Pending);

            // Relation Patient 
            table.HasOne<Patient>()
                .WithMany()
                .HasForeignKey(c => c.PatientId) 
                .HasPrincipalKey(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation Doctor
            table.HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(c => c.DoctorId)
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
