
using System.Numerics;
using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Master> Masters { get; set; }
        public DbSet<VendorDetails> VendorDetails { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<MainSkillMapping> MainSkillMappings { get; set; }
        public DbSet<SubSkillMapping> SubSkillMappings { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectResourceMapping> ProjectResourceMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

                modelBuilder.Entity<Resource>()
                .HasMany(r => r.Contacts)
                .WithOne()  // No navigation property in Contact
                .HasForeignKey(c => c.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Resource>()
            //.HasMany(r => r.SubSkills)
            //.WithOne()
            //.HasForeignKey(ss => ss.ResourceId)
            //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
