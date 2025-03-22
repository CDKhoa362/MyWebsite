using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Areas.Admin.Models.Location;
using MyWebsite.Models.MyInfor;
namespace MyWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UserPosting> UserPostings { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserPosting>(entity =>
            {
                entity.HasKey(e => e.UserPostingId);
                entity.ToTable("UserPosting");

                entity.Property(e => e.UserPostingId)
                    .HasMaxLength(50)
                    .HasColumnName("UserPostingId");

                entity.Property(e => e.FirstName)
                    .IsRequired(false);

                entity.Property(e => e.LastName)
                    .IsRequired(false);

                entity.Property(e => e.DOB)
                    .IsRequired(false)
                    .HasConversion(v => v.ToString(),
                                   v => string.IsNullOrEmpty(v) ? default : DateOnly.Parse(v));

                entity.Property(e => e.HouseNumber)
                    .IsRequired(false)
                    .HasMaxLength(20);

                entity.Property(e => e.Address)
                      .IsRequired(false)
                      .HasMaxLength(50);

                entity.Property(e => e.Major)
                      .IsRequired(false);

                entity.Property(e => e.Avatar)
                      .IsRequired(false)
                      .HasColumnType("varbinary(max)");


                entity.HasOne(e => e.User)
                      .WithOne()
                      .HasForeignKey<UserPosting>(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            }
            );

        }


    }
}
