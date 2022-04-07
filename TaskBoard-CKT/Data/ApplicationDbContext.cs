using System;
using Microsoft.EntityFrameworkCore;
using TaskBoard_CKT.Models;

namespace TaskBoard_CKT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<User> Users {get; set; }
    }
}