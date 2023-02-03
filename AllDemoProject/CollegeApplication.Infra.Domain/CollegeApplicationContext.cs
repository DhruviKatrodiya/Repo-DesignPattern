using CollegeApplication.Infra.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Infra.Domain
{
    public class CollegeApplicationContext : DbContext
    {
        public CollegeApplicationContext(DbContextOptions<CollegeApplicationContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(new List<Department>
            {
                new Department(1,"Dept1"),
                new Department(2,"Dept2"),
                new Department(3,"Dept3")
            });
        }
    }
}
