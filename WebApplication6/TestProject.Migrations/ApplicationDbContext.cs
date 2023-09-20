using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProject.Data.Entities;

namespace TestProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<JobExperience> JobExperiences { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasQueryFilter(x => x.IsDeleted == false);

            modelBuilder.Entity<JobExperience>()
                .HasKey(vc => new { vc.PersonId, vc.ProgrammingLanguageId });

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();
        }
    }
}