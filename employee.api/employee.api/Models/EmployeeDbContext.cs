using Microsoft.EntityFrameworkCore;

namespace employee.api.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Department> Departments { get; set; }


    }
}
