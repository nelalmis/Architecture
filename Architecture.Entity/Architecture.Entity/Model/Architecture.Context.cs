﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Architecture.Entity.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ARCHITECTUREEntities : DbContext
    {
        public ARCHITECTUREEntities()
            : base("name=ARCHITECTUREEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
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
    }
}
