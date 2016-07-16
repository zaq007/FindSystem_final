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

        public DbSet<Scoreboard> Scoreboard { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
            .HasMany(c => c.Path_Task)
            .WithRequired()
            .HasForeignKey(c => c.TaskId);
            
            modelBuilder.Entity<Path>()
            .HasMany(c => c.Path_Task)
            .WithRequired()
            .HasForeignKey(c => c.PathId);

            modelBuilder.Entity<State>()
            .HasRequired(a => a.UserProfile)
            .WithRequiredPrincipal();

            base.OnModelCreating(modelBuilder);
        }

        public virtual IEnumerable<Task> GetCurrentTask(int userId)
        {
            return this.Database.SqlQuery<Task>("EXEC GetCurrentTask @userId",
                new SqlParameter("userId", userId));
        }
    }
}
