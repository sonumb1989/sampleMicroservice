using CustomerManagementAPI.Model;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementAPI.DataAccess
{
    public class CustomerManagementDBContext: DbContext
    {
        public CustomerManagementDBContext(DbContextOptions<CustomerManagementDBContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasKey(m => m.CustomerId);
            builder.Entity<Customer>().ToTable("Customer");
            base.OnModelCreating(builder);
        }

        public void MigrateDB()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.Migrate());
        }
    }
}
