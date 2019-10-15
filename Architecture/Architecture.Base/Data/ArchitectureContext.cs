using Architecture.Common.Types;
using System.Data.Entity;

namespace Architecture.Base
{
    internal class ArchitectureContext:ContextBase
    {
        public ArchitectureContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

        public virtual DbSet<AddressContact> Address { get; set; }
        public virtual DbSet<CityContract> City { get; set; }
        public virtual DbSet<CompanyContactContract> CompanyContact { get; set; }
        public virtual DbSet<CountryContract> Country { get; set; }
        public virtual DbSet<CountyContract> County { get; set; }
        public virtual DbSet<EmailContact> Email { get; set; }
        public virtual DbSet<FaxContact> Fax { get; set; }
        public virtual DbSet<PhoneContact> Phone { get; set; }
        public virtual DbSet<PotentialContactContract> PotentialContact { get; set; }
        public virtual DbSet<ActionContract> Action { get; set; }
        public virtual DbSet<AuthenticationContract> Authentication { get; set; }
        public virtual DbSet<CompanyContract> Company { get; set; }
        public virtual DbSet<CompanyAuthenticationContract> CompanyAuthentication { get; set; }
        public virtual DbSet<CustomerDetailContract> CustomerDetail { get; set; }
        public virtual DbSet<DepartmentContract> Department { get; set; }
        public virtual DbSet<EmployeeDetailContract> EmployeeDetail { get; set; }
        public virtual DbSet<MessageContract> Message { get; set; }
        public virtual DbSet<ParameterContract> Parameter { get; set; }
        public virtual DbSet<PotentialContract> Potential { get; set; }
        public virtual DbSet<ResourceContract> Resource { get; set; }
        public virtual DbSet<ResourceActionContract> ResourceAction { get; set; }
        public virtual DbSet<RoleContract> Role { get; set; }
        public virtual DbSet<UserDetailContract> UserDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }   
    }
}
