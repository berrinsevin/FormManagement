
using FormManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormManagement.DataAccess
{
    public class FormDbContext : DbContext
    {
        public FormDbContext(DbContextOptions<FormDbContext> options) : base(options)
        {
        }

        public DbSet<Form> Forms { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>()
                .HasMany(f => f.Fields)
                .WithOne(f => f.Form)
                .HasForeignKey(f => f.FormId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Forms)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.CreatedBy);

            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
