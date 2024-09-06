using dotnetdev_assessment.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;
    public DbSet<Employee> Employees { get; set; }

    public ApplicationDbContext(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=dotnetdev_assessment;Integrated Security=SSPI;AttachDBFilename=" + Path.Combine(_environment.ContentRootPath, "App_Data") + "\\dotnetdev_assessment.mdf;TrustServerCertificate=True");
        //optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ApplicationDbContext"));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>().HasData(
            new Employee 
            {
                Id=1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                Name = "Admin Test",
                Department = "Admin",
                IsAdmin = true,
                Position = "Admin"
            },
            new Employee
            {
                Id = 2,
                Username = "user",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                Name = "User Test",
                Department = "Engineering",
                IsAdmin = false,
                Position = "Engineering"
            }
          );
    }
}
