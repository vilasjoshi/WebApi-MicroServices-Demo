﻿using DatabaseService.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UniqueString> UniqueStrings { get; set; }
    }
}

