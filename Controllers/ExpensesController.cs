using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/Expenses (Saare kharche dekhne ke liye)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            // .Include use kiya taake Category ka naam bhi saath aaye
            return await _context.Expenses.Include(e => e.Category).ToListAsync();
        }

        // 2. POST: api/Expenses 
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Expense added successfully!", data = expense });
        }
        // 3. PUT: api/Expenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest("ID mismatch");
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Expenses.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else { throw; }
            }

            return Ok(new { message = "Updated successfully!" });
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted successfully!" });
        }
        // GET: api/Expenses/search?category=Food&minAmount=100
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Expense>>> SearchExpenses(string? category, decimal? minAmount)
        {
            var query = _context.Expenses.Include(e => e.Category).AsQueryable();

            // Agar category ka naam diya gaya hai to filter karo
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(e => e.Category.Name.Contains(category));
            }

            // Agar minAmount di gayi hai to us se baray kharchay dikhao
            if (minAmount.HasValue)
            {
                query = query.Where(e => e.Amount >= minAmount.Value);
            }

            return await query.ToListAsync();
        }
    }
}