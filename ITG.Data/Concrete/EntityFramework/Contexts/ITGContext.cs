﻿using ITG.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ITG.Data.Concrete.EntityFramework.Contexts
{
    public class ITGContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server = ((localdb)\Karaman; Database = ITGDb; Trusted_Connection = True; Connect Timeout = 30; MultipleActiveResultSets = True;");
        }

    }
}
