

using Microsoft.EntityFrameworkCore;
using ResumeTemplate.Entities;

namespace ResumeTemplate.Data
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> option) : base(option)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkills> UserSkills { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }



        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var deletedEntries = ChangeTracker.Entries()
                                        .Where(p => p.State == EntityState.Deleted);


            foreach (var entry in deletedEntries)
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
            }
            
            return base.SaveChanges();
        }

    }
}
