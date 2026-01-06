using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerAPI.Data;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor: Yahan hum DbContext inject kar rahe hain
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }
    }
}