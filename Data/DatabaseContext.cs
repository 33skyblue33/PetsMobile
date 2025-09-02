using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetsMobile.Entities;

namespace PetsMobile.Data
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Breed).WithMany().HasForeignKey(p=>p.Id).IsRequired();
        }
    }

    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.HasKey(b => b.Id);
        }
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
        }
    }
}
