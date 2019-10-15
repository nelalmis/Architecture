
using System.Data.Entity;
using MVCProjectBase.Core.Domain;
using MVCProjectBase.Data.Mapping;
using MVCProjectBase.Core.Domain.Entity;

namespace MVCProjectBase.Data.Context
{
    public class MvcProjectBaseContext : DbContext
    {
        public MvcProjectBaseContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
