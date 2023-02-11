using Microsoft.EntityFrameworkCore;

namespace TutorialProject.Models
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options)
            : base(options) { }

        public DbSet<Person> People { get; set; }
    }
}
