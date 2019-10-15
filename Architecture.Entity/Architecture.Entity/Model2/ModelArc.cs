namespace Architecture.Entity.Model2
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelArc : DbContext
    {
        public ModelArc()
            : base("name=ModelArc")
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<CompanyContact> CompanyContact { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<County> County { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<Fax> Fax { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<PotentialContact> PotentialContact { get; set; }
        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<Authentication> Authentication { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyAuthentication> CompanyAuthentication { get; set; }
        public virtual DbSet<CustomerDetail> CustomerDetail { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<EmployeeDetail> EmployeeDetail { get; set; }
        public virtual DbSet<Human> Human { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Parameter> Parameter { get; set; }
        public virtual DbSet<Potential> Potential { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }
        public virtual DbSet<ResourceAction> ResourceAction { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserDetail> UserDetail { get; set; }
        public virtual DbSet<ErrorCodes> ErrorCodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasMany(e => e.CompanyContact)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.CompanyContact1)
                .WithOptional(e => e.Address1)
                .HasForeignKey(e => e.Address2Id);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.PotentialContact)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.PotentialContact1)
                .WithOptional(e => e.Address1)
                .HasForeignKey(e => e.Address2Id);

            modelBuilder.Entity<Company>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.HostName)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.UpdateUserName)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.HostIp)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.CompanyContact)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.CompanyAuthentication)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Department)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Message)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Parameter)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Potential)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Resource)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.HostName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.UpdateUserName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.HostIp)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.EmployeeDetail)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Human>()
                .Property(e => e.Ssn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Human>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.HostName)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.UpdateUserName)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.HostIp)
                .IsUnicode(false);

            modelBuilder.Entity<Potential>()
                .Property(e => e.Ssn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Potential>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Potential>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Potential>()
                .Property(e => e.HostName)
                .IsUnicode(false);

            modelBuilder.Entity<Potential>()
                .Property(e => e.UpdateUserName)
                .IsUnicode(false);

            modelBuilder.Entity<Potential>()
                .Property(e => e.HostIp)
                .IsUnicode(false);

            modelBuilder.Entity<Potential>()
                .HasMany(e => e.PotentialContact)
                .WithRequired(e => e.Potential)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Potential>()
                .HasMany(e => e.Authentication)
                .WithRequired(e => e.Potential)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Potential>()
                .HasMany(e => e.CustomerDetail)
                .WithRequired(e => e.Potential)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Potential>()
                .HasOptional(e => e.Role)
                .WithRequired(e => e.Potential);

            modelBuilder.Entity<Potential>()
                .HasMany(e => e.UserDetail)
                .WithRequired(e => e.Potential)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resource>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Resource>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Resource>()
                .Property(e => e.HostName)
                .IsUnicode(false);

            modelBuilder.Entity<Resource>()
                .Property(e => e.UpdateUserName)
                .IsUnicode(false);

            modelBuilder.Entity<Resource>()
                .HasMany(e => e.ResourceAction)
                .WithRequired(e => e.Resource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ErrorCodes>()
                .Property(e => e.ErrorDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ErrorCodes>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<ErrorCodes>()
                .Property(e => e.HostName)
                .IsUnicode(false);

            modelBuilder.Entity<ErrorCodes>()
                .Property(e => e.HosIp)
                .IsUnicode(false);
        }
    }
}
