using Microsoft.EntityFrameworkCore;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}