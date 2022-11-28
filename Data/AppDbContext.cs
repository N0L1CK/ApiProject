using ApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Target> Targets { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<TargetUser> TargetUsers { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProjectUser>().HasKey(e=> new 
            {
                e.UserId,
                e.ProjectId
            });

            modelBuilder.Entity<ProjectUser>().HasOne(e => e.User).WithMany(e=>e.Projects).HasForeignKey(k=>k.UserId);
            modelBuilder.Entity<ProjectUser>().HasOne(p => p.Project).WithMany(e => e.Users).HasForeignKey(k => k.ProjectId);

            modelBuilder.Entity<TargetUser>().HasKey(e => new
            {
                e.UserId,
                e.TargetId
            });

            modelBuilder.Entity<TargetUser>().HasOne(e => e.User).WithMany(e => e.Targets).HasForeignKey(k => k.UserId);
            modelBuilder.Entity<TargetUser>().HasOne(p => p.Target).WithMany(e => e.Users).HasForeignKey(k => k.TargetId);


            
            modelBuilder.Entity<Login>().HasOne(l => l.User).WithOne(p => p.Login);
            modelBuilder.Entity<User>().HasOne(l => l.Login).WithOne(p => p.User);




            modelBuilder.Entity<Project>().HasMany(u => u.Targets).WithOne(p => p.Project).HasForeignKey(p => p.ProjectId);
            modelBuilder.Entity<Target>().HasOne(t => t.Project).WithMany(p => p.Targets);

            




            base.OnModelCreating(modelBuilder);
        }
    }
}
