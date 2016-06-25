using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DAL.Entity;

namespace DAL
{
    public class FindContext : DbContext
    {
        public FindContext()
            : base("Local")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Path> Pathes { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Path_Task> Path_Task { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
            .HasMany(a => a.Pathes)
            .WithMany()
            .Map(x =>
            {
                x.MapLeftKey("TaskId");
                x.MapRightKey("PathId");
                x.ToTable("Path_Task");
            });

            modelBuilder.Entity<State>()
            .HasRequired(a => a.UserProfile)
            .WithRequiredPrincipal();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public virtual IEnumerable<Task> GetCurrentTask(int userId)
        {
            return this.Database.SqlQuery<Task>("EXEC GetCurrentTask @userId",
                new SqlParameter("userId", userId));
        }
    }
}
