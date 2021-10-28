using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options)
        {

        }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
