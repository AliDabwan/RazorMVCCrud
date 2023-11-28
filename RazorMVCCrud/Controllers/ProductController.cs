using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorMVCCrud.Data;
using RazorMVCCrud.Models;

namespace RazorMVCCrud.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductController
        public async Task< ActionResult> Index()
        {
          IEnumerable<Product> products =await _context.Products.ToListAsync();
            if(products==null) return NotFound();
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (!string.IsNullOrEmpty(product.Name)){
                var duplicat = _context.Products
                    .FirstOrDefault(p => p.Name.ToLower() == product.Name.ToLower());
                if (duplicat!=null)
                {
                    //"name" the error is for properity name only
                    //"" the error is for all model 
                    ModelState.AddModelError("name", "this name is duplicated");
                }

            }
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                
                    return RedirectToAction(nameof(Index));
                
            }
            return View(product);
            
        }

        // GET: ProductController/Update/5
        public ActionResult Update(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var product = _context.Products
                .FirstOrDefault(p => p.Id == id);
            if(product==null) return NotFound();
            
           

            return View(product);
        }

        // POST: ProductController/Update/5
        [HttpPost]
        public ActionResult Update(int id, Product product)
        {
            if (!string.IsNullOrEmpty(product.Name))
            {
                var duplicat = _context.Products
                    .FirstOrDefault(p => p.Name.ToLower() == product.Name.ToLower()
                    && p.Id!=product.Id
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
                _context.ChangeTracker.Clear();
                _context.Products.Update(product);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            return View(product);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }
            var product = _context.Products
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();



            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
          var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
