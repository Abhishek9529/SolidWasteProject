using Microsoft.EntityFrameworkCore;
using InterviewManagementPortal.Models;

namespace InterviewManagementPortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                // SQL Server table name
                entity.ToTable("users");

                // Primary Key
                entity.HasKey(u => u.Id);

                // Id
                entity.Property(u => u.Id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                // Name
                entity.Property(u => u.Name)
                      .HasColumnName("name")
                      .HasMaxLength(100)
                      .IsRequired();

                // Email
                entity.Property(u => u.Email)
                      .HasColumnName("email")
                      .HasMaxLength(255)
                      .IsRequired();

                // Password
                entity.Property(u => u.Password)
                      .HasColumnName("password")
                      .HasMaxLength(255)
                      .IsRequired();

                // Role
                entity.Property(u => u.Role)
                      .HasColumnName("role")
                      .HasMaxLength(10)
                      .IsRequired();

                // Title
                entity.Property(u => u.Title)
                      .HasColumnName("title")
                      .HasMaxLength(100)
                      .IsRequired(false);

                // Department
                entity.Property(u => u.Department)
                      .HasColumnName("department")
                      .HasMaxLength(100)
                      .IsRequired(false);

                // CreatedAt
                entity.Property(u => u.CreatedAt)
                      .HasColumnName("created_at")
                      .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
