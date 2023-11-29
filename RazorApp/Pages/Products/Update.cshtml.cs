using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorApp.Data;
using RazorApp.Models;

namespace RazorApp.Pages.Products
{
    public class UpdateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        [BindProperty]
        public Product Product { get; set; }

        public UpdateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  void OnGet(int id)
        {
            Product = _dbContext.Products
                .FirstOrDefault(p => p.Id == id);
            
         
        }

        public async Task<IActionResult> OnPost()
        {
            if (!string.IsNullOrEmpty(Product.Name))
            {
                var duplicat = _dbContext.Products
                    .FirstOrDefault(p => p.Name.ToLower() == Product.Name.ToLower()
                    && p.Id != Product.Id
                    );
                if (duplicat != null)
                {
                    //"name" the error is for properity name only
                    //"" the error is for all model 
                    ModelState.AddModelError("name", "this name is duplicated");
                }

            }
            if (ModelState.IsValid)
            {
                _dbContext.Products.Update(Product);
                await _dbContext.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
