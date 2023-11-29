using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorApp.Data;
using RazorApp.Models;

namespace RazorApp.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        [BindProperty]
        public Product Product { get; set; }

        public DeleteModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  void OnGet(int id)
        {
            Product = _dbContext.Products
                .FirstOrDefault(p => p.Id == id)!;
            if (Product == null )
            {
                return;
            }
         
        }

        public async Task<IActionResult> OnPost()
        {
           
               _dbContext.Products.Remove(Product);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Index");
          

        }
    }
}
