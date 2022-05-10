using Demo.Domain.Entities;
using Demo.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure.Data.Context
{
    public class EFContext : DbContext
    {
        public EFContext()
        { }

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }
    }
}
