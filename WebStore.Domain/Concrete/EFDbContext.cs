﻿using WebStore.Domain.Entities;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebStore.Domain.Concrete 
{

    public class EFDbContext : DbContext 
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // To resolve "cause cycles or multiple cascade paths" when entity framework is creating database
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }

}