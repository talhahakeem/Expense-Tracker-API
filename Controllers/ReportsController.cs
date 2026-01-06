using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTrackerAPI.Data;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Total Kharcha Kitna Hua?
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalExpense()
        {
            var total = await _context.Expenses.SumAsync(e => e.Amount);
            return Ok(new { TotalAmount = total });
        }

        // 2. Category wise Summary (Food pe kitna, Rent pe kitna?)
        [HttpGet("category-summary")]
        public async Task<IActionResult> GetCategorySummary()
        {
            var summary = await _context.Expenses
                .GroupBy(e => e.Category.Name)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(e => e.Amount),
                    Count = g.Count()
                })
                .ToListAsync();

            return Ok(summary);
        }
    }
}